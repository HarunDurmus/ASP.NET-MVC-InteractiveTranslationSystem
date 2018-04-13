using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tercume.Entities
{
    [Table("Faturalar")]
    public class Fatura: EntityBase
    {
        public virtual TercumeUser TercumeUser { get; set; }
        //public virtual Tercuman Tercuman { get; set; }
        public virtual Translate Translate { get; set; }
        public virtual Tercuman Translator { get; set; }

        [DisplayName("Kullanıcı Adı"),
           StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string UserName { get; set; }
        [DisplayName("Tercüman ismi"),
           StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string TranslatorName { get; set; }
        [DisplayName("Toplam ücret")]
        public int ToplamUcret { get; set; }
        [DisplayName("Tercüman ücreti")]
        public double TercumanUcret { get; set; }

    }
}
