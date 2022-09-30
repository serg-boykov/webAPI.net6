using System.ComponentModel.DataAnnotations;
using webAPI.Enum;
using webAPI.Models.Base;

namespace webAPI.Models
{
    public class CarBrake : BaseModel
    {
        [Required]
        public BrakeType? BrakeType { get; set; }

        [MaxLength(20)]
        [Required]
        public string? Name { get; set; }

        public bool Checked { get; set; }

        public int CarId { get; set; }
    }
}
