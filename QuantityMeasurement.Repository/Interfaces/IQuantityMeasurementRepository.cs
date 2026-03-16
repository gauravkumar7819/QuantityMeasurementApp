using System.Collections.Generic;
using QuantityMeasurement.Model.Entities;

namespace QuantityMeasurement.Repository.Interfaces
{
    public interface IQuantityMeasurementRepository
    {
        void SaveMeasurement(QuantityMeasurementEntity entity);

        List<QuantityMeasurementEntity> GetAllMeasurements();

        void ClearMeasurements();
    }
}