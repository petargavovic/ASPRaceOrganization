using BaseLibrary.Entities;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.DTOs.EntityDTOs
{
    public class UcinakDTO : BaseEntity
    {
        [Required]
        public int StartnaPozicija { get; set; }
        [Required]
        public int Plasman { get; set; }
        [Required]
        public string? Vremena { get; set; }

        public int VozacID { get; set; }
        public string? ImeIPrezime { get; set; }
        public int TrkaID { get; set; }
    }
}
