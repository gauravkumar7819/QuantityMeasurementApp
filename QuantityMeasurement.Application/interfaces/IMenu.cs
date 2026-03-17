using QuantityMeasurement.Business.Interfaces;

namespace QuantityMeasurement.Application
{
    public interface IMenu
    {
        void Start(IQuantityMeasurementService service);
    }
}