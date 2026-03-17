using System.Collections.Generic;
using QuantityMeasurement.Model;
using QuantityMeasurement.Model.Entities;

namespace QuantityMeasurement.Repository.Interfaces
{
    public interface IQuantityMeasurementRepository
    {
        void Save(QuantityMeasurementEntity entity);

        List<QuantityMeasurementEntity> GetAll();
    }
}