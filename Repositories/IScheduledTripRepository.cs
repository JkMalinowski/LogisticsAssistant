using LogisticsAssistant.Models;

namespace LogisticsAssistant.Repositories
{
    public interface IScheduledTripRepository
    {
        ScheduledTrips Get(int scheduledTripId);
        IQueryable<ScheduledTrips> GetAll();
        IQueryable<Lorries> GetAllLorries();
        void Add(ScheduledTrips scheduledTrip);
        void Update(int scheduledTripId, ScheduledTrips scheduledTrip);
        void Delete(int scheduledTripId);
    }
}
