using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class Vozac : BaseEntity
    {

        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Ime može sadržati samo slova.")]
        public string? Ime { get; set; }
        [Required]
        [RegularExpression(@"^[a-zA-Z]+$", ErrorMessage = "Prezime može sadržati samo slova.")]
        public string? Prezime { get; set; }
        [Required]
        public string? Drzava { get; set; }
        [Required]
        [DateBeforeToday(ErrorMessage = "Datum rođenja mora biti u prošlosti.")]
        public DateTime DatumRodjenja { get; set; }

        public List<Trka>? Trka { get; set; }
        public List<int>? TrkaId { get; set; }

        public List<Plasman>? Plasman {get; set;}
        public List<int>? PlasmanID { get; set; }

        public List<Ucinak>? Ucinak { get; set; }
        public List<int>? UcinakId { get; set; }

    }
    public class DateBeforeTodayAttribute : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            if (value is DateTime date)
            {
                return date < DateTime.Now.Date;
            }
            return false;
        }
    }
}
