﻿using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LogisticsAssistant.Models
{
    public class Lorries
    {
        [Key, DisplayName("Lorry ID")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Lorry brand is required!"), DisplayName("Lorry brand"), MaxLength(50)]
        public string? LorryBrand { get; set; }

        [Required(ErrorMessage = "Max speed should be in range 1 and 100!"), Range(1, 100), DisplayName("Max speed in km/h")]
        public int MaxSpeed { get; set; }

        [Required(ErrorMessage = "Break should be grater than 30 minutes!"), DisplayName("Break in minutes"), Range(30, int.MaxValue, ErrorMessage = "Break should be grater than 30!")]
        public int BreakInMinutes{ get; set; }

        [Required, DisplayName("Required break after certain time in hours"), Range(1.0, double.MaxValue, ErrorMessage = "Value should be grater than 1!")]
        public double BreakAfterRideInHours { get; set; }
    }
}