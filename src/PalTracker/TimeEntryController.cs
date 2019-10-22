using System;
using System.Net;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace PalTracker
{
    [Route("/time-entries")]
    public class TimeEntryController : ControllerBase
    {
        private readonly ITimeEntryRepository _timeEntryRepository;
        private readonly IOperationCounter<TimeEntry> _operationCounter;
        
        public TimeEntryController(ITimeEntryRepository timeEntryRepository, IOperationCounter<TimeEntry> operationCounter)
        {
            _timeEntryRepository = timeEntryRepository;
            _operationCounter = operationCounter;
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            _operationCounter.Increment(TrackedOperation.Delete);
            
            if (!_timeEntryRepository.Contains(id))
            {
                return NotFound();
            }
            else
            {
                _timeEntryRepository.Delete(id);
            }

            return NoContent();
        }

        [HttpGet("{id}", Name = "GetTimeEntry")]
        public IActionResult Read(int id)
        {
            _operationCounter.Increment(TrackedOperation.Read);
            var timeEntry = _timeEntryRepository.Find(id);

            if (timeEntry == null)
                return NotFound();
            
            return Ok(timeEntry);
        }

        [HttpPost]
        public IActionResult Create([FromBody] TimeEntry timeEntry)
        {
            _operationCounter.Increment(TrackedOperation.Create);
            var createdTimeEntry = _timeEntryRepository.Create(timeEntry);
            return CreatedAtRoute("GetTimeEntry", new {id = createdTimeEntry.Id}, createdTimeEntry);
        }

        [HttpGet]
        public IActionResult List()
        {
            _operationCounter.Increment(TrackedOperation.List);
            var list = _timeEntryRepository.List();
            return Ok(list);
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] TimeEntry theUpdate)
        {
            _operationCounter.Increment(TrackedOperation.Update);
            if (_timeEntryRepository.Contains(id))
            {
               return Ok(_timeEntryRepository.Update(id, theUpdate));
            }
            else
            {
                return NotFound();
            }
        }
    }
}