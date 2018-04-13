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
    [Table("Mesajlar")]
    public class Mesaj:EntityBase
    {

        //[DisplayName("E-Posta"),
        //    Required(ErrorMessage = "{0} alanı gereklidir user."),
        //    StringLength(70, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        //public string GondericiEmail { get; set; }

        //[DisplayName("E-Posta"),
        //    Required(ErrorMessage = "{0} alanı gereklidir user."),
        //    StringLength(70, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        //public string AlicitEmail { get; set; }

        [DisplayName("Mesaj Metni"),
           StringLength(1000, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Text { get; set; }

        [DisplayName("Konu Metni"),
           StringLength(55, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Title { get; set; }

        [DisplayName("Oluşturma Tarihi")]
        public DateTime Create_on { get; set; }

        public bool is_read { get; set; }


        [DisplayName("Alıcı E-Posta"),
            Required(ErrorMessage = "{0} alanı gereklidir user."),
            StringLength(70, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string receiver_Email { get; set; }


        [DisplayName("Gönderici E-Posta"),
            StringLength(70, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string sender_Email { get; set; }


        public virtual TercumeUser TercumeUser { get; set; }
        public virtual Tercuman Tercuman { get; set; }
    }
}
