using System;
using System.Collections.Generic;
using QuantityMeasurement.Model.Entities;
using QuantityMeasurement.Repository.Interfaces;

namespace QuantityMeasurement.Repository.Impl
{
    public class QuantityMeasurementCacheRepository : IQuantityMeasurementRepository
    {
        private readonly List<QuantityMeasurementEntity> cache;

        public QuantityMeasurementCacheRepository()
        {
            cache = new List<QuantityMeasurementEntity>();
        }

        public void SaveMeasurement(QuantityMeasurementEntity entity)
        {
            if (entity == null)
                throw new ArgumentNullException(nameof(entity));

            cache.Add(entity);
        }

        public List<QuantityMeasurementEntity> GetAllMeasurements()
        {
            return new List<QuantityMeasurementEntity>(cache);
        }

        public void ClearMeasurements()
        {
            cache.Clear();
        }
    }
}