namespace LogisticsAssistant.Models
{
    public class TripDateValidator
    {
        public static bool IsTripDateValid(IQueryable<ScheduledTrips> lorryTrips, DateTime dateOfDepartue, DateTime dateOfArrival)
        {
            foreach(var trip in lorryTrips)
            {
                if ((dateOfDepartue < trip.DateOfDepartue && dateOfArrival > trip.DateOfDepartue) ||
                    (dateOfDepartue < trip.DateOfArrival && dateOfArrival > trip.DateOfArrival) ||
                    (dateOfDepartue < trip.DateOfDepartue && dateOfArrival > trip.DateOfArrival) ||
                    (dateOfDepartue > trip.DateOfDepartue && dateOfArrival < trip.DateOfArrival))
                {
                    return false;
                }
            }
            return true;
        }
    }
}
