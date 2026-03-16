﻿using System;
using QuantityMeasurement.Business.Interfaces;
using QuantityMeasurement.Model.Units;
using QuantityMeasurement.Model.Models;
using QuantityMeasurement.Model.DTO;

namespace QuantityMeasurement.Business.Impl
{
    public class QuantityMeasurementServiceImpl : IQuantityMeasurementService
    {
        private static void ThrowIfNegative(double value, LengthUnit unit)
        {
            if (value < 0)
                throw new ArgumentException($"{unit} cannot be negative");
        }

        // ------------------ UC1: Feet equality ------------------

        public bool AreFeetEqual(double firstFeet, double secondFeet)
        {
            if (firstFeet < 0 || secondFeet < 0)
                throw new ArgumentException("Feet cannot be negative");

            var feet1 = new QuantityLength(firstFeet, LengthUnit.FEET);
            var feet2 = new QuantityLength(secondFeet, LengthUnit.FEET);

            return feet1.Equals(feet2);
        }

        // ------------------ UC2: Inches equality ------------------

        public bool AreInchesEqual(double firstInches, double secondInches)
        {
            if (firstInches < 0 || secondInches < 0)
                throw new ArgumentException("Inches cannot be negative");

            var inch1 = new QuantityLength(firstInches, LengthUnit.INCHES);
            var inch2 = new QuantityLength(secondInches, LengthUnit.INCHES);

            return inch1.Equals(inch2);
        }

        // ------------------ UC3/UC4: Generic length equality ------------------

        public bool AreQuantitiesEqual(
            double value1, LengthUnit unit1,
            double value2, LengthUnit unit2)
        {
            ThrowIfNegative(value1, unit1);
            ThrowIfNegative(value2, unit2);

            var length1 = new QuantityLength(value1, unit1);
            var length2 = new QuantityLength(value2, unit2);

            return length1.Equals(length2);
        }

        // ------------------ UC9: Weight equality ------------------

        public bool AreWeightsEqual(double v1, WeightUnit u1, double v2, WeightUnit u2)
        {
            if (v1 < 0 || v2 < 0)
                throw new ArgumentException("Weight cannot be negative");

            var w1 = new QuantityWeight(v1, u1);
            var w2 = new QuantityWeight(v2, u2);

            return w1.Equals(w2);
        }

        // ------------------ UC6/UC7/UC12: Addition ------------------

        public QuantityDTO Add(QuantityDTO q1, QuantityDTO q2)
        {
            if (q1 == null || q2 == null)
                throw new ArgumentNullException("QuantityDTO cannot be null");

            double result = q1.Value + q2.Value;

            return new QuantityDTO
            {
                Value = result,
                Unit = q1.Unit,
                
            };
        }

        // ------------------ UC12: Subtraction ------------------

        public QuantityDTO Subtract(QuantityDTO q1, QuantityDTO q2)
        {
            if (q1 == null || q2 == null)
                throw new ArgumentNullException("QuantityDTO cannot be null");

            double result = q1.Value - q2.Value;

            return new QuantityDTO
            {
                Value = result,
                Unit = q1.Unit,
               
            };
        }

        // ------------------ UC12: Division ------------------

        public double Divide(QuantityDTO q1, QuantityDTO q2)
        {
            if (q1 == null || q2 == null)
                throw new ArgumentNullException("QuantityDTO cannot be null");

            if (q2.Value == 0)
                throw new ArgumentException("Cannot divide by zero");

            return q1.Value / q2.Value;
        }
        public QuantityDTO Convert(QuantityDTO quantity, string targetUnit)
{
    if (quantity == null)
        throw new ArgumentNullException(nameof(quantity));

    var fromUnit = Enum.Parse<LengthUnit>(quantity.Unit, true);
    var toUnit = Enum.Parse<LengthUnit>(targetUnit, true);

    var length = new QuantityLength(quantity.Value, fromUnit);
    var converted = length.ConvertTo(toUnit);

    return new QuantityDTO
    {
        Value = converted.Value,
        Unit = converted.Unit.ToString(),
    };
}

        public bool AreLengthsEqual(double value1, LengthUnit unit1, double value2, LengthUnit unit2)
        {
            throw new NotImplementedException();
        }

        public bool AreVolumesEqual(double v1, VolumeUnit u1, double v2, VolumeUnit u2)
        {
            throw new NotImplementedException();
        }

        public bool AreTemperaturesEqual(double v1, TemperatureUnit u1, double v2, TemperatureUnit u2)
        {
            throw new NotImplementedException();
        }

        public QuantityDTO Add(QuantityDTO q1, QuantityDTO q2, string targetUnit)
        {
            throw new NotImplementedException();
        }

        public QuantityDTO Subtract(QuantityDTO q1, QuantityDTO q2, string targetUnit)
        {
            throw new NotImplementedException();
        }
    }
}