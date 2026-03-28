using QuantityMeasurement.Business.Interfaces;
using QuantityMeasurement.Business.Impl;
using QuantityMeasurement.Repository.Implementations;
using QuantityMeasurement.Repository.Interfaces;


namespace QuantityMeasurement.Application
{
    class Program
    {
        static void Main()
        {
            IQuantityMeasurementRepository repo = new QuantityMeasurementRepository();
            IQuantityMeasurementService service = new QuantityMeasurementServiceImpl(repo);

            IMenu menu = new Menu();
            menu.Start(service);
        }
    }
}