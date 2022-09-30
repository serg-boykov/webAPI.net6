using System.ComponentModel.DataAnnotations;
using webAPI.Models.Base;

namespace webAPI.Models
{
    public class CarColor : BaseModel
    {
        [MaxLength(20)]
        [Required]
        public string? Name { get; set; }

        public int CarId { get; set; }
    }
}
