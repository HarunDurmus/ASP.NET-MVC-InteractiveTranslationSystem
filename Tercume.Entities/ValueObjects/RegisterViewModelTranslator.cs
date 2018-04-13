﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tercume.Entities.ValueObjects
{
    public class RegisterViewModelTranslator
    {

        [DisplayName("İsim"),
            Required(ErrorMessage = "{0} alanı boş geçilemez."),
            StringLength(20, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Name { get; set; }


        [DisplayName("E-posta"),
            Required(ErrorMessage = "{0} alanı boş geçilemez."),
            StringLength(70, ErrorMessage = "{0} max. {1} karakter olmalı."),
            EmailAddress(ErrorMessage = "{0} alanı için geçerli bir e-posta adresi giriniz.")]
        public string EMail { get; set; }

        [DisplayName("Şifre"),
            Required(ErrorMessage = "{0} alanı boş geçilemez."),
            DataType(DataType.Password),
            StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Password { get; set; }

        [DisplayName("Şifre Tekrar"),
            Required(ErrorMessage = "{0} alanı boş geçilemez."),
            DataType(DataType.Password),
            StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı."),
            Compare("Password", ErrorMessage = "{0} ile {1} uyuşmuyor.")]
        public string RePassword { get; set; }

        [DisplayName("Kısa Biyografi"),
            Required(ErrorMessage = "{0} alanı boş geçilemez."),
            StringLength(155, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Biyografi { get; set; }

        [DisplayName("Meslek"),
            Required(ErrorMessage = "{0} alanı boş geçilemez."),
            StringLength(25, ErrorMessage = "{0} max. {1} karakter olmalı.")]
        public string Meslek { get; set; }

        [DisplayName("Ana Dil"),
           StringLength(25, ErrorMessage = "{0} alanı max. {1} karakter olmalıdır.")]
        public string AnaDil { get; set; }
    }
}
