using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class Plasman : BaseEntity
    {
        [Required]
        public int BrojPoena { get; set; }


        public Vozac? Vozac { get; set; }
        public int VozacID { get; set; }


        public RangLista? RangLista { get; set; }
        public int RangListaID { get; set; }
    }
}
