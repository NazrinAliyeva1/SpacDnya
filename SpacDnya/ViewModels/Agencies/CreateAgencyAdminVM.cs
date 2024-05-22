using System.ComponentModel.DataAnnotations;

namespace SpacDnya.ViewModels.Agencies
{
    public class CreateAgencyAdminVM
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        public IFormFile Photo { get; set; }
    }
}
