using System.Collections.Generic;
using QuantityMeasurement.Model;
using QuantityMeasurement.Repository.Interfaces;
using QuantityMeasurement.Model.Entities;

namespace QuantityMeasurement.Repository.Implementations
{
    public class QuantityMeasurementCacheRepository : IQuantityMeasurementRepository
    {
        private readonly List<QuantityMeasurementEntity> cache = new();

        public void Save(QuantityMeasurementEntity entity)
        {
            cache.Add(entity);
        }

        public List<QuantityMeasurementEntity> GetAll()
        {
            return cache;
        }
    }
}