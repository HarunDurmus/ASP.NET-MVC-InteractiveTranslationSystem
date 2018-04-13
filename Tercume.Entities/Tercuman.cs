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
    [Table("Tercumanlar")]
    public class Tercuman: EntityBase
    {
        [DisplayName("İsim"),
            StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Name { get; set; }

        [DisplayName("Soyad"),
            StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Surname { get; set; }
        [DisplayName("Meslek"),
            StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Meslek { get; set; }

        [DisplayName("Tercuman E-Posta"),
            Required(ErrorMessage = "{0} alanı gereklidir tercuman."),
            StringLength(70, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Email { get; set; }

        [DisplayName("Şifre"),
            Required(ErrorMessage = "{0} alanı gereklidir tercuman."),
            StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Password { get; set; }


        [DisplayName("Biyografi"),
            StringLength(155, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Biyografi { get; set; }

        [DisplayName("Twitter Link(örn:'www.twitter.com/harun-durmus)'"),
           StringLength(155, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string TwitterLink { get; set; }

        [DisplayName("Linkedin Link(örn:'www.linkedin.com-harun-durmus')"),
           StringLength(155, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string LinkeninLink { get; set; }

        [DisplayName("Ana Dil"),
           StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string AnaDil { get; set; }




        [StringLength(30), ScaffoldColumn(false)]
        public string ProfileImageFilename { get; set; }

        [DisplayName("Kayıt Tarihi")]
        public DateTime Create_on { get; set; }

        public Guid ActivateGuid { get; set; }
        public bool IsActive { get; set; }




        public virtual List<DilTercumen> Tercumanlar { get; set; }

        public virtual List<Translate> Translates { get; set; }
        public virtual List<Fatura> Faturalar { get; set; }
        public virtual List<Mesaj> Mesajlar { get; set; }
        public virtual List<ToDoList> ToDoList { get; set; }
        public Tercuman()
        {
            Translates = new List<Translate>();
            Faturalar = new List<Fatura>();
            Mesajlar = new List<Mesaj>();
            ToDoList = new List<ToDoList>();
        }
    }
}
