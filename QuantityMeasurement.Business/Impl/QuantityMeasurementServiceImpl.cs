using System;
using QuantityMeasurement.Business.Interfaces;
using QuantityMeasurement.Model.Units;
using QuantityMeasurement.Model.Models;
using QuantityMeasurement.Model.DTO;
using QuantityMeasurement.Repository.Interfaces;
using QuantityMeasurement.Model.Entities;
using QuantityMeasurement.Repository.Implementations;

namespace QuantityMeasurement.Business.Impl
{
    public class QuantityMeasurementServiceImpl : IQuantityMeasurementService
    {
        private readonly IQuantityMeasurementRepository _repo;

        public QuantityMeasurementServiceImpl(IQuantityMeasurementRepository repo)
        {
            _repo = repo;
        }

        public QuantityMeasurementServiceImpl() : this(new QuantityMeasurementCacheRepository())
        {
        }

        private QuantityDTO PerformOperation(string opName, QuantityDTO q1, QuantityDTO q2, string? targetUnit, Func<QuantityDTO, QuantityDTO, string?, QuantityDTO> operation)
        {
            try
            {
                var result = operation(q1, q2, targetUnit);
                _repo.Save(new QuantityMeasurementEntity(opName, q1.ToString(), q2.ToString(), result.ToString()));
                return result;
            }
            catch (Exception ex)
            {
                _repo.Save(new QuantityMeasurementEntity(opName, q1.ToString(), q2.ToString(), ex.Message, true));
                throw;
            }
        }

        public QuantityDTO Add(QuantityDTO q1, QuantityDTO q2, string targetUnit)
        {
            return PerformOperation("ADD", q1, q2, targetUnit, (a, b, target) =>
            {
                var info1 = GetBaseInfo(a.Value, a.Unit) ?? throw new ArgumentException("Invalid unit 1.");
                var info2 = GetBaseInfo(b.Value, b.Unit) ?? throw new ArgumentException("Invalid unit 2.");

                if (info1.category != info2.category || info1.category == "Temperature")
                    throw new ArgumentException("Invalid addition.");

                double sumBase = info1.baseValue + info2.baseValue;
                return ConvertFromBase(sumBase, target!, info1.category);
            });
        }

        public QuantityDTO Subtract(QuantityDTO q1, QuantityDTO q2, string targetUnit)
        {
            return PerformOperation("SUBTRACT", q1, q2, targetUnit, (a, b, target) =>
            {
                var info1 = GetBaseInfo(a.Value, a.Unit) ?? throw new ArgumentException("Invalid unit 1.");
                var info2 = GetBaseInfo(b.Value, b.Unit) ?? throw new ArgumentException("Invalid unit 2.");

                if (info1.category != info2.category || info1.category == "Temperature")
                    throw new ArgumentException("Invalid subtraction.");

                double diffBase = info1.baseValue - info2.baseValue;
                return ConvertFromBase(diffBase, target!, info1.category);
            });
        }

        private QuantityDTO ConvertFromBase(double baseVal, string targetUnit, string category)
        {
            double result = category switch
            {
                "Length" when Enum.TryParse<LengthUnit>(targetUnit, true, out var l) => l.ConvertFromBaseUnit(baseVal),
                "Weight" when Enum.TryParse<WeightUnit>(targetUnit, true, out var w) => w.ConvertFromBaseUnit(baseVal),
                "Volume" when Enum.TryParse<VolumeUnit>(targetUnit, true, out var v) => v.ConvertFromBaseUnit(baseVal),
                "Temperature" when Enum.TryParse<TemperatureUnit>(targetUnit, true, out var t) => t.ConvertFromBaseUnit(baseVal),
                _ => throw new ArgumentException("Invalid target unit or category mismatch.")
            };
            return new QuantityDTO(result, targetUnit);
        }

        public double Divide(QuantityDTO q1, QuantityDTO q2)
        {
            try
            {
                if (q2.Value == 0) throw new ArgumentException("Cannot divide by zero");
                double result = q1.Value / q2.Value;
                _repo.Save(new QuantityMeasurementEntity("DIVIDE", q1.ToString(), q2.ToString(), result.ToString()));
                return result;
            }
            catch (Exception ex)
            {
                _repo.Save(new QuantityMeasurementEntity("DIVIDE", q1.ToString(), q2.ToString(), ex.Message, true));
                throw;
            }
        }

        public QuantityDTO Convert(QuantityDTO quantity, string targetUnit)
        {
            try
            {
                QuantityDTO result = ConvertInternal(quantity, targetUnit);
                _repo.Save(new QuantityMeasurementEntity("CONVERT", quantity.ToString(), "-", result.ToString()));
                return result;
            }
            catch (Exception ex)
            {
                _repo.Save(new QuantityMeasurementEntity("CONVERT", quantity.ToString(), "-", ex.Message, true));
                throw;
            }
        }

        private (double baseValue, string category)? GetBaseInfo(double val, string unitName)
        {
            if (val < 0)
            {
                if (Enum.TryParse<LengthUnit>(unitName, true, out _) || 
                    Enum.TryParse<WeightUnit>(unitName, true, out _) || 
                    Enum.TryParse<VolumeUnit>(unitName, true, out _))
                {
                    throw new ArgumentException($"{unitName.ToUpper()} cannot be negative");
                }
            }

            if (Enum.TryParse<LengthUnit>(unitName, true, out var l)) return (l.ConvertToBaseUnit(val), "Length");
            if (Enum.TryParse<WeightUnit>(unitName, true, out var w)) return (w.ConvertToBaseUnit(val), "Weight");
            if (Enum.TryParse<VolumeUnit>(unitName, true, out var v)) return (v.ConvertToBaseUnit(val), "Volume");
            if (Enum.TryParse<TemperatureUnit>(unitName, true, out var t)) return (t.ConvertToBaseUnit(val), "Temperature");
            return null;
        }

        public bool Compare(QuantityDTO q1, QuantityDTO q2) => CompareInternal("COMPARE", q1, q2);

        private bool CompareInternal(string op, QuantityDTO q1, QuantityDTO q2)
        {
            try
            {
                var info1 = GetBaseInfo(q1.Value, q1.Unit);
                var info2 = GetBaseInfo(q2.Value, q2.Unit);

                if (info1 == null || info2 == null) throw new ArgumentException("Invalid units.");

                bool result = info1.Value.category == info2.Value.category && 
                             Math.Abs(info1.Value.baseValue - info2.Value.baseValue) < 0.001;

                _repo.Save(new QuantityMeasurementEntity(op, q1.ToString(), q2.ToString(), result.ToString()));
                return result;
            }
            catch (Exception ex)
            {
                _repo.Save(new QuantityMeasurementEntity(op, q1.ToString(), q2.ToString(), ex.Message, true));
                throw;
            }
        }

        private QuantityDTO ConvertInternal(QuantityDTO q, string targetUnit)
        {
            var info = GetBaseInfo(q.Value, q.Unit) ?? throw new ArgumentException("Invalid from unit.");

            double result = info.category switch
            {
                "Length" when Enum.TryParse<LengthUnit>(targetUnit, true, out var l) => l.ConvertFromBaseUnit(info.baseValue),
                "Weight" when Enum.TryParse<WeightUnit>(targetUnit, true, out var w) => w.ConvertFromBaseUnit(info.baseValue),
                "Volume" when Enum.TryParse<VolumeUnit>(targetUnit, true, out var v) => v.ConvertFromBaseUnit(info.baseValue),
                "Temperature" when Enum.TryParse<TemperatureUnit>(targetUnit, true, out var t) => t.ConvertFromBaseUnit(info.baseValue),
                _ => throw new ArgumentException("Invalid target unit or category mismatch.")
            };

            return new QuantityDTO(result, targetUnit);
        }

        public bool AreLengthsEqual(double v1, LengthUnit u1, double v2, LengthUnit u2) => CompareInternal("COMPARE_LENGTH", new(v1, u1.ToString()), new(v2, u2.ToString()));
        public bool AreWeightsEqual(double v1, WeightUnit u1, double v2, WeightUnit u2) => CompareInternal("COMPARE_WEIGHT", new(v1, u1.ToString()), new(v2, u2.ToString()));
        public bool AreVolumesEqual(double v1, VolumeUnit u1, double v2, VolumeUnit u2) => CompareInternal("COMPARE_VOLUME", new(v1, u1.ToString()), new(v2, u2.ToString()));
        public bool AreTemperaturesEqual(double v1, TemperatureUnit u1, double v2, TemperatureUnit u2) => CompareInternal("COMPARE_TEMPERATURE", new(v1, u1.ToString()), new(v2, u2.ToString()));

        private bool CompareQuantities(string op, double v1, string u1, double v2, string u2)
        {
            try
            {
                bool result = Compare(new QuantityDTO(v1, u1), new QuantityDTO(v2, u2));
                return result;
            }
            catch (Exception ex)
            {
                _repo.Save(new QuantityMeasurementEntity(op, $"{v1} {u1}", $"{v2} {u2}", ex.Message, true));
                throw;
            }
        }
    }
}