namespace LogisticsAssistant.Controllers
{
    public class CalcDate
    {
        public static DateTime CalcArrivalDate(DateTime departueDate, int distance, int maxSpeed, int breakInMinutes, double breakAfterCertainRideTime)
        {
            double tripTimeInHours = (double)distance / (double)maxSpeed;
            double breakDurationInHours = (double)breakInMinutes / 60.0;
            double totalBreaksTimeInHours;
            if (tripTimeInHours > breakAfterCertainRideTime)
            {
                totalBreaksTimeInHours = Math.Ceiling(tripTimeInHours / breakAfterCertainRideTime) * breakDurationInHours;
            }
            else
            {
                totalBreaksTimeInHours = 0;
            }
            double totalTripTimeInHours = tripTimeInHours + totalBreaksTimeInHours;
            DateTime arrivalDate = departueDate.AddHours(totalTripTimeInHours);
            return arrivalDate;
        }
    }
}
