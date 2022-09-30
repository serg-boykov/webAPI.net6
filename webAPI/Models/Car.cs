using System.ComponentModel.DataAnnotations;
using webAPI.Models.Base;

namespace webAPI.Models
{
    public class Car : BaseModel
    {
        [MaxLength(30)]
        [Required]
        public string? Name { get; set; }

        [Required]
        public decimal? Price { get; set; }

        [MaxLength(30)]
        [Required]
        public string? Model { get; set; }

        public List<CarTransmission>? Transmissions { get; set; }

        public List<CarBrake>? Brakes { get; set; }

        public CarColor? Color { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public string? Description { get; set; }
    }
}
