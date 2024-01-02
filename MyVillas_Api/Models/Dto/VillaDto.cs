using System.ComponentModel.DataAnnotations;

namespace MyVillas_Api.Models.Dto
{
    public class VillaDto
    {

        public int Id { get; set; }
        [Required]
        [MaxLength(30)]
        public string Name { get; set; }
        public string Details { get; set; }

        public int Occupancy {  get; set; }
        [Required]
        public double Rate { get; set; }
        public string ImageUrl { get; set; }
        public string Amenity { get; set; }

        public int sqft {  get; set; }


    }
}
