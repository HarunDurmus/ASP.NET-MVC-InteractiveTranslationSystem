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
    [Table("TercumeUsers")]
    public class TercumeUser: EntityBase
    {
        [DisplayName("İsim"),
            StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Name { get; set; }

        [DisplayName("Soyad"),
            StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Surname { get; set; }

        [DisplayName("Kullnaıcı E-Posta"),
            Required(ErrorMessage = "{0} alanı gereklidir user."),
            StringLength(70, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Email { get; set; }

        [DisplayName("Şifre"),
            Required(ErrorMessage = "{0} alanı gereklidir user."),
            StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Password { get; set; }

        [StringLength(30), ScaffoldColumn(false)]
        public string ProfileImageFilename { get; set; }

        [DisplayName("Aktif")]
        public bool IsActive { get; set; }

        [DisplayName("Yönetici")]
        public bool IsAdmin { get; set; }


        [Required, ScaffoldColumn(false)]
        public Guid ActivateGuid { get; set; }

        public virtual List<Translate> Translates { get; set; }
        public virtual List<Fatura> Faturalar { get; set; }
        public virtual List<Mesaj> Mesajlar { get; set; }
        public TercumeUser()
        {
            Translates = new List<Translate>();
            Faturalar = new List<Fatura>();
            Mesajlar = new List<Mesaj>();
        }
    }
}
