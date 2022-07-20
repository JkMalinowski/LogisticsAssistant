using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LogisticsAssistant.Models
{
    public class Lorries
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Lorry brand is required!")]
        [DisplayName("Lorry brand")]
        public string? LorryBrand { get; set; }
        [Required(ErrorMessage = "Max speed should be in range 1 and 100!")]
        [Range(1, 100)]
        [DisplayName("Max speed in km/h")]
        public int MaxSpeed { get; set; }
        [Required(ErrorMessage = "Break should be grater than 30 minutes!")]
        // TODO: Custom validation message
        [DisplayName("Break in minutes")]
        [Range(30, double.MaxValue)]
        public int BreakInMinutes{ get; set; }
    }
}