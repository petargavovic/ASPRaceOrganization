using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class Trka : BaseEntity
    {

        [Required]
        [MaxLength(32, ErrorMessage = "Naziv može imati najviše 32 karaktera.")]
        public string? Naziv { get; set; }
        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Broj krugova mora biti veći od 0.")]
        public int BrojKrugova { get; set; }
        [Required]
        public DateTime DatumTrke { get; set; }
        public string? Staza { get; set; }

        public List<Trka>? Vozac { get; set; }
        public List<int>? VozacId { get; set; }

        public List<Ucinak>? Ucinak { get; set; }
        public List<int>? UcinakId { get; set; }

        public Kategorija? Kategorija { get; set; }
        [Required]
        public int KategorijaId { get; set; }
    }
}
