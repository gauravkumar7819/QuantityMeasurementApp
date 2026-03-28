using System;
using System.Collections.Generic;
using System.Linq;
using QuantityMeasurement.Model.Entities;
using QuantityMeasurement.Repository.Interfaces;

namespace QuantityMeasurement.Repository.Implementations
{
    public class QuantityMeasurementRepository : IQuantityMeasurementRepository
    {
        private readonly AppDbContext _context;

        public QuantityMeasurementRepository(AppDbContext context)
        {
            _context = context;
        }

        //  SAVE
        public void Save(QuantityMeasurementEntity entity)
        {
            _context.QuantityMeasurements.Add(entity);
            _context.SaveChanges();
        }

        //  GET ALL
        public List<QuantityMeasurementEntity> GetAll()
        {
            return _context.QuantityMeasurements.ToList();
        }

        //  DELETE ALL
        public void DeleteAll()
        {
            var allData = _context.QuantityMeasurements.ToList();
            _context.QuantityMeasurements.RemoveRange(allData);
            _context.SaveChanges();
        }
    }
}