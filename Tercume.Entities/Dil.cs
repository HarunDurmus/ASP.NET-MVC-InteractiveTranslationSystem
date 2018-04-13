using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tercume.Entities
{
    [Table("Diller")]
    public class Dil : EntityBase
    {
        [DisplayName("yabancı dil")]
        public string Dil_isim { get; set; }


        public virtual List<DilTercumen> Dil_isimler { get; set; }
        public Dil()
        {
            Dil_isimler = new List<DilTercumen>();
        }
    }
}
