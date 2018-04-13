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
    [Table("ToDolists")]
    public class ToDoList:EntityBase
    {
        [DisplayName("Oluşturma Tarihi")]
        public DateTime Create_on { get; set; }
        [DisplayName("Yapılcak iş"),
           StringLength(55, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string Text { get; set; }

        public virtual Tercuman owner { get; set; }
    }
}
