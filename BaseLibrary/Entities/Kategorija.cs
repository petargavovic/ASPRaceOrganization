using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class Kategorija : BaseEntity
    {
        [Required]
        public string? NazivKategorije { get; set; }

        public List<RangLista>? RangLista { get; set; }
        public List<int>? RangListaID { get; set; }


        public List<Trka>? Trka { get; set; }
        public List<int>? TrkaID { get; set; }
    }
}
