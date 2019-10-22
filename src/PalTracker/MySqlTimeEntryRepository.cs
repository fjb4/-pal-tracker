using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace PalTracker
{
    public class MySqlTimeEntryRepository : ITimeEntryRepository
    {
        private readonly TimeEntryContext _context = null;
        public MySqlTimeEntryRepository(TimeEntryContext context)
        {
            _context = context;
        }
        
        public TimeEntry Create(TimeEntry timeEntry)
        {
            var entity = _context.TimeEntryRecords.Add(timeEntry.ToRecord()).Entity;
            _context.SaveChanges();

            return entity.ToEntity();
        }

        public TimeEntry Find(long id)
        {
            var record = FindRecord(id);
            return record?.ToEntity();
        }

        public bool Contains(long id)
        {
            var record = FindRecord(id);
            return record != null;
        }

        public void Delete(long id)
        {
            var record = FindRecord(id);

            if (record != null)
            {
                _context.TimeEntryRecords.Remove(record);
                _context.SaveChanges();
            }
        }

        public TimeEntry Update(long id, TimeEntry timeEntry)
        {
            var recordToUpdate = timeEntry.ToRecord();
            recordToUpdate.Id = id;

            _context.Update(recordToUpdate);
            _context.SaveChanges();

            return Find(id);
        }

        public IEnumerable<TimeEntry> List()
        {
            var records = _context.TimeEntryRecords.AsNoTracking();
            return records.Select(ter => ter.ToEntity());
        }

        private TimeEntryRecord FindRecord(long id)
        {
            return _context.TimeEntryRecords.AsNoTracking().SingleOrDefault(ter => ter.Id == id);
        }
    }
}