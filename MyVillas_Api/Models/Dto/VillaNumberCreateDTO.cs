using System.ComponentModel.DataAnnotations;

namespace MyVillas_Api.Models.Dto
{
    public class VillaNumberCreateDTO
    {
        [Required]
        public int VillNo { get; set; }
        [Required]
        public int VillaId { get; set; }
        public string SpecialDetails { get; set; }
    }
}
