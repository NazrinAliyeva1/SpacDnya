using System.ComponentModel.DataAnnotations;

namespace SpacDnya.ViewModels.Agencies
{
    public class UpdateAgencyVM
    {
        [MaxLength(45), Required]
        public string Name { get; set; }
        [MaxLength(100), Required]

        public string Description { get; set; }
        [Required]
        public string Image { get; set; }
    }
}
