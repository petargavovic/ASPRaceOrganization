using BaseLibrary.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.DTOs.EntityDTOs
{
    public class PlasmanDTO : BaseEntity
    {
        [Required]
        public int BrojPoena { get; set; }


        public int VozacID { get; set; }
        public string? ImeIPrezime { get; set; }
        public int RangListaID { get; set; }
    }
}
