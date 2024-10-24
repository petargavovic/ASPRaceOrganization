using BaseLibrary.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.DTOs.EntityDTOs
{
    public class TrkaDTO : BaseEntity
    {
        [Required]
        [MaxLength(32, ErrorMessage = "Naziv može imati najviše 32 karaktera.")]
        public string Naziv { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Broj krugova mora biti veći od 0.")]
        public int BrojKrugova { get; set; }
        [Required]
        public DateTime DatumTrke { get; set; }
        public string? Staza { get; set; }
        [Required]
        public KategorijaDTO Kategorija { get; set; }
        public List<UcinakDTO>? Ucinak { get; set; }
    }
}
