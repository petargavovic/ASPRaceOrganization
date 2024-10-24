using BaseLibrary.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.DTOs.EntityDTOs
{
    public class KategorijaDTO : BaseEntity
    {
        [Required]
        public string NazivKategorije { get; set; }
    }
}
