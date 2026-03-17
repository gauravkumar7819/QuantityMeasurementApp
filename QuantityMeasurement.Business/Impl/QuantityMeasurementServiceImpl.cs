﻿using System;
using QuantityMeasurement.Business.Interfaces;
using QuantityMeasurement.Model.Units;
using QuantityMeasurement.Model.Models;
using QuantityMeasurement.Model.DTO;
using QuantityMeasurement.Repository.Interfaces;
using QuantityMeasurement.Model.Entities;

namespace QuantityMeasurement.Business.Impl
{
    public class QuantityMeasurementServiceImpl : IQuantityMeasurementService
    {
        private readonly IQuantityMeasurementRepository _repo;

        public QuantityMeasurementServiceImpl(IQuantityMeasurementRepository repo)
        {
            _repo = repo;
        }

        // ------------------ Add ------------------

        public QuantityDTO Add(QuantityDTO q1, QuantityDTO q2, string targetUnit)
        {
            try
            {
                var unit = Enum.Parse<LengthUnit>(targetUnit, true);

                var l1 = new QuantityLength(q1.Value, Enum.Parse<LengthUnit>(q1.Unit, true));
                var l2 = new QuantityLength(q2.Value, Enum.Parse<LengthUnit>(q2.Unit, true));

                var c1 = l1.ConvertTo(unit);
                var c2 = l2.ConvertTo(unit);

                var result = new QuantityDTO
                {
                    Value = c1.Value + c2.Value,
                    Unit = unit.ToString()
                };

                _repo.Save(new QuantityMeasurementEntity(
                    "ADD",
                    $"{q1.Value} {q1.Unit}",
                    $"{q2.Value} {q2.Unit}",
                    $"{result.Value} {result.Unit}"
                ));

                return result;
            }
            catch (Exception ex)
            {
                _repo.Save(new QuantityMeasurementEntity(
                    "ADD",
                    $"{q1.Value} {q1.Unit}",
                    $"{q2.Value} {q2.Unit}",
                    ex.Message,
                    true
                ));

                throw;
            }
        }

        // ------------------ Subtract ------------------

        public QuantityDTO Subtract(QuantityDTO q1, QuantityDTO q2, string targetUnit)
        {
            try
            {
                var unit = Enum.Parse<LengthUnit>(targetUnit, true);

                var l1 = new QuantityLength(q1.Value, Enum.Parse<LengthUnit>(q1.Unit, true));
                var l2 = new QuantityLength(q2.Value, Enum.Parse<LengthUnit>(q2.Unit, true));

                var c1 = l1.ConvertTo(unit);
                var c2 = l2.ConvertTo(unit);

                var result = new QuantityDTO
                {
                    Value = c1.Value - c2.Value,
                    Unit = unit.ToString()
                };

                _repo.Save(new QuantityMeasurementEntity(
                    "SUBTRACT",
                    $"{q1.Value} {q1.Unit}",
                    $"{q2.Value} {q2.Unit}",
                    $"{result.Value} {result.Unit}"
                ));

                return result;
            }
            catch (Exception ex)
            {
                _repo.Save(new QuantityMeasurementEntity(
                    "SUBTRACT",
                    $"{q1.Value} {q1.Unit}",
                    $"{q2.Value} {q2.Unit}",
                    ex.Message,
                    true
                ));

                throw;
            }
        }

        // ------------------ Divide ------------------

        public double Divide(QuantityDTO q1, QuantityDTO q2)
        {
            try
            {
                if (q2.Value == 0)
                    throw new ArgumentException("Cannot divide by zero");

                double result = q1.Value / q2.Value;

                _repo.Save(new QuantityMeasurementEntity(
                    "DIVIDE",
                    $"{q1.Value} {q1.Unit}",
                    $"{q2.Value} {q2.Unit}",
                    result.ToString()
                ));

                return result;
            }
            catch (Exception ex)
            {
                _repo.Save(new QuantityMeasurementEntity(
                    "DIVIDE",
                    $"{q1.Value} {q1.Unit}",
                    $"{q2.Value} {q2.Unit}",
                    ex.Message,
                    true
                ));

                throw;
            }
        }

        // ------------------ Convert ------------------

        public QuantityDTO Convert(QuantityDTO quantity, string targetUnit)
        {
            try
            {
                var fromUnit = Enum.Parse<LengthUnit>(quantity.Unit, true);
                var toUnit = Enum.Parse<LengthUnit>(targetUnit, true);

                var length = new QuantityLength(quantity.Value, fromUnit);
                var converted = length.ConvertTo(toUnit);

                var result = new QuantityDTO
                {
                    Value = converted.Value,
                    Unit = converted.Unit.ToString(),
                };

                _repo.Save(new QuantityMeasurementEntity(
                    "CONVERT",
                    $"{quantity.Value} {quantity.Unit}",
                    "-",
                    $"{result.Value} {result.Unit}"
                ));

                return result;
            }
            catch (Exception ex)
            {
                _repo.Save(new QuantityMeasurementEntity(
                    "CONVERT",
                    $"{quantity.Value} {quantity.Unit}",
                    "-",
                    ex.Message,
                    true
                ));

                throw;
            }
        }

        // ------------------ Compare Length ------------------

        public bool AreLengthsEqual(double value1, LengthUnit unit1,
                                   double value2, LengthUnit unit2)
        {
            try
            {
                if (value1 < 0 || value2 < 0)
                    throw new ArgumentException("Length cannot be negative");

                bool result = new QuantityLength(value1, unit1)
                    .Equals(new QuantityLength(value2, unit2));

                _repo.Save(new QuantityMeasurementEntity(
                    "COMPARE_LENGTH",
                    $"{value1} {unit1}",
                    $"{value2} {unit2}",
                    result.ToString()
                ));

                return result;
            }
            catch (Exception ex)
            {
                _repo.Save(new QuantityMeasurementEntity(
                    "COMPARE_LENGTH",
                    $"{value1} {unit1}",
                    $"{value2} {unit2}",
                    ex.Message,
                    true
                ));

                throw;
            }
        }

        // ------------------ Compare Weight ------------------

        public bool AreWeightsEqual(double v1, WeightUnit u1, double v2, WeightUnit u2)
        {
            try
            {
                if (v1 < 0 || v2 < 0)
                    throw new ArgumentException("Weight cannot be negative");

                bool result = new QuantityWeight(v1, u1)
                    .Equals(new QuantityWeight(v2, u2));

                _repo.Save(new QuantityMeasurementEntity(
                    "COMPARE_WEIGHT",
                    $"{v1} {u1}",
                    $"{v2} {u2}",
                    result.ToString()
                ));

                return result;
            }
            catch (Exception ex)
            {
                _repo.Save(new QuantityMeasurementEntity(
                    "COMPARE_WEIGHT",
                    $"{v1} {u1}",
                    $"{v2} {u2}",
                    ex.Message,
                    true
                ));

                throw;
            }
        }

        // ------------------ Compare Volume ------------------

        public bool AreVolumesEqual(double v1, VolumeUnit u1, double v2, VolumeUnit u2)
        {
            try
            {
                if (v1 < 0 || v2 < 0)
                    throw new ArgumentException("Volume cannot be negative");

                bool result = new QuantityVolume(v1, u1)
                    .Equals(new QuantityVolume(v2, u2));

                _repo.Save(new QuantityMeasurementEntity(
                    "COMPARE_VOLUME",
                    $"{v1} {u1}",
                    $"{v2} {u2}",
                    result.ToString()
                ));

                return result;
            }
            catch (Exception ex)
            {
                _repo.Save(new QuantityMeasurementEntity(
                    "COMPARE_VOLUME",
                    $"{v1} {u1}",
                    $"{v2} {u2}",
                    ex.Message,
                    true
                ));

                throw;
            }
        }

        // ------------------ Compare Temperature ------------------

        public bool AreTemperaturesEqual(double v1, TemperatureUnit u1, double v2, TemperatureUnit u2)
        {
            try
            {
                bool result = new QuantityTemperature(v1, u1)
                    .Equals(new QuantityTemperature(v2, u2));

                _repo.Save(new QuantityMeasurementEntity(
                    "COMPARE_TEMPERATURE",
                    $"{v1} {u1}",
                    $"{v2} {u2}",
                    result.ToString()
                ));

                return result;
            }
            catch (Exception ex)
            {
                _repo.Save(new QuantityMeasurementEntity(
                    "COMPARE_TEMPERATURE",
                    $"{v1} {u1}",
                    $"{v2} {u2}",
                    ex.Message,
                    true
                ));

                throw;
            }
        }
    }
}