using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LogisticsAssistant.Models
{
    public class ScheduledTrips
    {
        [Key]
        public int ScheduledTripId { get; set; }
        [ForeignKey("Lorries"), DisplayName("Lorry brand")]
        public int LorryId { get; set; }
        
        [MaxLength(2000), DisplayName("Trip description")]
        public string? TripDescription { get; set; }
        
        [DisplayName("Distance in km"), MinValue(1)]
        public int Distance { get; set; }
        
        [DataType(DataType.DateTime), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}"), DisplayName("Date of departue"), TripDateValidator]
        public DateTime DateOfDepartue { get; set; }
        
        [DataType(DataType.DateTime), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}"), DisplayName("Date of arrival")]
        public DateTime DateOfArrival { get; set; }
        
        [DataType(DataType.DateTime), DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:dd/MM/yyyy HH:mm}"), DisplayName("Date of creation trip")]
        public DateTime CreationTripDate { get; set; }
        
        [DisplayName("Lorry brand")]
        public virtual Lorries? Lorry { get; set; }
    }
}
