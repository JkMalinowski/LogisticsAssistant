using LogisticsAssistant.Data;
using LogisticsAssistant.Models;
using Microsoft.EntityFrameworkCore;

namespace LogisticsAssistant.Repositories
{
    public class ScheduledTripRepository : IScheduledTripRepository
    {
        private readonly LogisticsAssistantContext _context;

        public ScheduledTripRepository(LogisticsAssistantContext context)
        {
            _context = context;
        }

        public void Add(ScheduledTrips scheduledTrip)
        {
            _context.ScheduledTrips.Add(scheduledTrip);
            _context.SaveChanges();
        }

        public void Delete(int scheduledTripId)
        {
            var result = _context.ScheduledTrips.SingleOrDefault(x => x.ScheduledTripId == scheduledTripId);
            if(result != null)
            {
                _context.Remove(result);
                _context.SaveChanges();
            }
        }

        public ScheduledTrips Get(int scheduledTripId)
        {
            return _context.ScheduledTrips
                .Include(s => s.Lorry)
                .SingleOrDefault(x => x.ScheduledTripId == scheduledTripId);
        }

        public IQueryable<ScheduledTrips> GetAll()
        {
            return _context.ScheduledTrips.Include(s => s.Lorry);
        }

        public IQueryable<Lorries> GetAllLorries()
        {
            return _context.Lorries;
        }

        public void Update(int scheduledTripId, ScheduledTrips scheduledTrip)
        {
            var result = _context.ScheduledTrips.SingleOrDefault(x => x.ScheduledTripId == scheduledTripId);
            if(result != null)
            {
                result.CreationTripDate = scheduledTrip.CreationTripDate;
                result.DateOfArrival = scheduledTrip.DateOfArrival;
                result.DateOfDepartue = scheduledTrip.DateOfDepartue;
                result.Distance = scheduledTrip.Distance;
                result.TripDescription = scheduledTrip.TripDescription;
                result.LorryId = scheduledTrip.LorryId;
                _context.SaveChanges();
            }
        }
    }
}
