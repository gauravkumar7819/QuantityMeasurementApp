using QuantityMeasurement.Model.Units;
using QuantityMeasurement.Model.DTO;

namespace QuantityMeasurement.Business.Interfaces
{
    public interface IQuantityMeasurementService
    {
        bool AreLengthsEqual(double value1, LengthUnit unit1, double value2, LengthUnit unit2);

        bool AreWeightsEqual(double v1, WeightUnit u1, double v2, WeightUnit u2);

        bool AreVolumesEqual(double v1, VolumeUnit u1, double v2, VolumeUnit u2);

        bool AreTemperaturesEqual(double v1, TemperatureUnit u1, double v2, TemperatureUnit u2);

        QuantityDTO Add(QuantityDTO q1, QuantityDTO q2, string targetUnit);

        QuantityDTO Subtract(QuantityDTO q1, QuantityDTO q2, string targetUnit);

        double Divide(QuantityDTO q1, QuantityDTO q2);

        QuantityDTO Convert(QuantityDTO quantity, string targetUnit);
    }
}