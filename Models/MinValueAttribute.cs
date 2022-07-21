using System.ComponentModel.DataAnnotations;

namespace LogisticsAssistant.Models
{
    public class MinValueAttribute : ValidationAttribute
    {
        private readonly int _minimumValue;
        public MinValueAttribute(int minimumValue)
        {
            _minimumValue = minimumValue;
        }
        public override bool IsValid(object value)
        {
            return (int)value >= _minimumValue;
        }
    }
}
