using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BaseLibrary.Entities
{
    public class Ucinak : BaseEntity
    {
        [Required]
        public int StartnaPozicija { get; set; }
        [Required]
        public int Plasman { get; set; }
        [Required]
        public string? Vremena { get; set; }

        public Vozac? Vozac { get; set; }
        public int VozacID { get; set; }


        public Trka? Trka { get; set; }
        public int TrkaID { get; set; }
    }
}
