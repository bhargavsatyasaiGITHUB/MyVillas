﻿using System.ComponentModel.DataAnnotations;

namespace MyVillas_Web.Models.Dto
{
    public class VillaNumberUpdateDTO
    {
        [Required]
        public int VillNo { get; set; }
        [Required]
        public int VillaId { get; set; }
        public string SpecialDetails { get; set; }
    }
}
