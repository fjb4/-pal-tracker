using Steeltoe.Management.Endpoint.Info;

namespace PalTracker
{
    public class TimeEntryInfoContributor : IInfoContributor
    {
        private readonly IOperationCounter<TimeEntry> _counter;

        public TimeEntryInfoContributor(IOperationCounter<TimeEntry> counter)
        {
            _counter = counter;
        }

        public void Contribute(IInfoBuilder builder)
        {
            builder.WithInfo(
                _counter.Name,
                _counter.GetCounts
            );
        }
    }
}