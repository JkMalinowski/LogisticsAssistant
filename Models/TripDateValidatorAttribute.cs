using LogisticsAssistant.Data;
using LogisticsAssistant.Repositories;
using System.ComponentModel.DataAnnotations;

namespace LogisticsAssistant.Models
{
    public class TripDateValidatorAttribute : ValidationAttribute
    {
        protected override ValidationResult? IsValid(object value, ValidationContext validationContext)
        {
            var scheduledTrip = (ScheduledTrips)validationContext.ObjectInstance;   
            LogisticsAssistantContext db = (LogisticsAssistantContext)validationContext.GetService(typeof(LogisticsAssistantContext));
            var lorryTrips = db.ScheduledTrips.Where(x => x.LorryId == scheduledTrip.LorryId);
            foreach (var trip in lorryTrips)
            {
                Console.WriteLine($"DATE OF DEPARTUE: {trip.DateOfDepartue}");
                Console.WriteLine($"DATE OF ARRIVAL: {trip.DateOfArrival}");
                Console.WriteLine($"scheduledTrip DATE OF DEPARTUE: {scheduledTrip.DateOfDepartue}");
                // W momencie bindowania ta wartosc nie istnieje
                Console.WriteLine($"scheduledTrip DATE OF ARRIVAL: {scheduledTrip.DateOfArrival}");
                if ((scheduledTrip.DateOfDepartue < trip.DateOfDepartue && scheduledTrip.DateOfArrival > trip.DateOfDepartue) ||
                    (scheduledTrip.DateOfDepartue < trip.DateOfArrival && scheduledTrip.DateOfArrival > trip.DateOfArrival) ||
                    (scheduledTrip.DateOfDepartue < trip.DateOfDepartue && scheduledTrip.DateOfArrival > trip.DateOfArrival) ||
                    (scheduledTrip.DateOfDepartue > trip.DateOfDepartue && scheduledTrip.DateOfArrival < trip.DateOfArrival))
                {
                    Console.WriteLine("DATY SIE NACHODZĄ!***************************************");
                    return new ValidationResult("Trip overlap with another trip!");
                }
            }
            Console.WriteLine("DATY SA OK!***************************************");
            return ValidationResult.Success;
        }
    }
}
