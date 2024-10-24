using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class RangLista : BaseEntity
    {

        [Required]
        public DateTime KrajSezone { get; set; }


        public List<Plasman>? Plasman { get; set; }
        public List<int>? PlasmanID { get; set; }

        public Kategorija? Kategorija { get; set; }
        public int KategorijaId { get; set; }
    }
}
