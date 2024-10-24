using BaseLibrary.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.DTOs.EntityDTOs
{
    public class RangListaDTO : BaseEntity
    {
        [Required]
        public DateTime KrajSezone { get; set; }


        public List<PlasmanDTO>? Plasman { get; set; }

        public KategorijaDTO? Kategorija { get; set; }
    }
}
