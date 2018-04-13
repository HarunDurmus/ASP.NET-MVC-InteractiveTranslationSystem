using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace Tercume.Entities
{
    [Table("Translates")]
    public class Translate: EntityBase
    {
        [DisplayName("Tercüme Başlığı"),StringLength(60)]
        
        public string Title { get; set; }



        [DisplayName("Tercüme edilecek metin"), StringLength(100000)]
        [DataType(DataType.MultilineText)]
        public string Text { get; set; }


        public string ContentType { get; set; }
        public byte[] Data { get; set; }


        [DisplayName("Ücret")]
        public int Price { get; set; }

        [DisplayName("Başlama Tarihi")]
        public DateTime StartDate { get; set; }

        [DisplayName("Bitiş Tarihi")]
        public DateTime EndDate { get; set; }

        [DisplayName("Kaynak Dil")]
        
        public string KaynakDil { get; set; }

        [DisplayName("Hedef Dil")]
        
        public string HedefDil { get; set; }

        [DisplayName("Tercüme Metni"),StringLength(100000)]
        [DataType(DataType.MultilineText)]
        public string TercumeText{ get; set; }

        public bool? Is_active { get; set; }
        public bool? Is_finish { get; set; }
        public int ter_id { get; set; }


        //bir tercümenin bir sahibi vardır.
        //bir tercümenin bir tercümanı vardır.

        public virtual TercumeUser Owner { get; set; }
        public virtual Tercuman Translator { get; set; }
        //public virtual Fatura Fatura { get; set; }

    }
}
