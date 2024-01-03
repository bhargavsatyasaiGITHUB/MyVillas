﻿using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyVillas_Api.Models
{
    public class VillaNumber
    {
        [Key,DatabaseGenerated(DatabaseGeneratedOption.None)]
       
        public int VillNo {  get; set; }
        [ForeignKey("Villa")]
        public int VillaId { get; set; }
        public Villa Villa { get; set; }
        public string SpecialDetails {  get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set;}
    }
}
