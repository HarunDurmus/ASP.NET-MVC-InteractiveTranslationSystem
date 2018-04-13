using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tercume.BusinessLayer.Abstract;
using Tercume.BusinessLayer.Result;
using Tercume.Common.Helpers;
using Tercume.Entities;
using Tercume.Entities.Messages;
using Tercume.Entities.ValueObjects;

namespace Tercume.BusinessLayer
{
    public class TercumanManager : ManagerBase<Tercuman>
    {

        public BusinessLayerResult<Tercuman> RegisterTercuman(RegisterViewModelTranslator data)
        {
            // Kullanıcı Name kontrolü..
            // Kullanıcı e-posta kontrolü..
            // Kayıt işlemi..
            // Aktivasyon e-postası gönderimi.
            Tercuman user = Find(x => x.Email == data.EMail || x.Email == data.EMail);
            BusinessLayerResult<Tercuman> res = new BusinessLayerResult<Tercuman>();

            if (user != null)
            {
                if (user.Email == data.EMail)
                {
                    res.AddError(ErrorMessageCode.NameAlreadyExists, "Kullanıcı emaili kayıtlı.");
                }

                if (user.Email == data.EMail)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi kayıtlı.");
                }
            }
            else
            {
                int dbResult = base.Insert(new Tercuman()
                {

                    Email = data.EMail,
                    Name = data.Name,
                    Create_on=DateTime.Now,
                    ProfileImageFilename = "user_boy.png",
                    Biyografi=data.Biyografi,
                    Meslek=data.Meslek,
                    Password = data.Password,
                    ActivateGuid = Guid.NewGuid(),
                    IsActive = false,
                   
                });

                if (dbResult > 0)
                {
                    res.Result = Find(x => x.Email == data.EMail && x.Password == data.Password);

                    string siteUri = ConfigHelper.Get<string>("SiteRootUri");
                    string activateUri = $"{siteUri}/Home/TercumanActivate/{res.Result.ActivateGuid}";
                    string body = $"Merhaba {res.Result.Name};<br><br>Hesabınızı aktifleştirmek için <a href='{activateUri}' target='_blank'>tıklayınız</a>.";

                    MailHelper.SendMail(body, res.Result.Email, "Tercume Hesap Aktifleştirme");
                }
            }

            return res;
        }

        public BusinessLayerResult<Tercuman> GetUserById(int id)
        {
            BusinessLayerResult<Tercuman> res = new BusinessLayerResult<Tercuman>();
            res.Result = Find(x => x.Id == id);

            if (res.Result == null)
            {
                res.AddError(ErrorMessageCode.UserNotFound, "Kullanıcı bulunamadı.");
            }

            return res;
        }

        public BusinessLayerResult<Tercuman> LoginTranslator(LoginViewModelTranslator data)
        {
            // Giriş kontrolü
            // Hesap aktive edilmiş mi?
            BusinessLayerResult<Tercuman> res = new BusinessLayerResult<Tercuman>();
            res.Result = Find(x => x.Email == data.Email && x.Password == data.Password);

            if (res.Result != null)
            {
                if (!res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserIsNotActive, "Kullanıcı aktifleştirilmemiştir.");
                    res.AddError(ErrorMessageCode.CheckYourEmail, "Lütfen e-posta adresinizi kontrol ediniz.");
                }
            }
            else
            {
                res.AddError(ErrorMessageCode.NameOrPassWrong, "Kullanıcı mail yada şifre uyuşmuyor.");
            }

            return res;
        }

        public BusinessLayerResult<Tercuman> UpdateProfile(Tercuman data)
        {
            Tercuman db_tercuman = Find(x => x.Id != data.Id && (x.Name == data.Name || x.Email == data.Email));
            BusinessLayerResult<Tercuman> res = new BusinessLayerResult<Tercuman>();

            if (db_tercuman != null && db_tercuman.Id != data.Id)
            {
                if (db_tercuman.Name == data.Name)
                {
                    res.AddError(ErrorMessageCode.NameAlreadyExists, "Kullanıcı adı kayıtlı.");
                }

                if (db_tercuman.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi kayıtlı.");
                }

                return res;
            }

            res.Result = Find(x => x.Id == data.Id);
            res.Result.Email = data.Email;
            res.Result.Name = data.Name;
            res.Result.Surname = data.Surname;
            res.Result.Password = data.Password;
            

            if (string.IsNullOrEmpty(data.ProfileImageFilename) == false)
            {
                res.Result.ProfileImageFilename = data.ProfileImageFilename;
            }

            if (base.Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.ProfileCouldNotUpdated, "Profil güncellenemedi.");
            }

            return res;
        }

        public BusinessLayerResult<Tercuman> RemoveUserById(int id)
        {
            BusinessLayerResult<Tercuman> res = new BusinessLayerResult<Tercuman>();
            Tercuman user = Find(x => x.Id == id);

            if (user != null)
            {
                if (Delete(user) == 0)
                {
                    res.AddError(ErrorMessageCode.UserCouldNotRemove, "Kullanıcı silinemedi.");
                    return res;
                }
            }
            else
            {
                res.AddError(ErrorMessageCode.UserCouldNotFind, "Kullanıcı bulunamadı.");
            }

            return res;
        }

        public BusinessLayerResult<Tercuman> ActivateTercuman(Guid activateId)
        {
            BusinessLayerResult<Tercuman> res = new BusinessLayerResult<Tercuman>();
            res.Result = Find(x => x.ActivateGuid == activateId);

            if (res.Result != null)
            {
                if (res.Result.IsActive)
                {
                    res.AddError(ErrorMessageCode.UserAlreadyActive, "Kullanıcı zaten aktif edilmiştir.");
                    return res;
                }

                res.Result.IsActive = true;
                Update(res.Result);
            }
            else
            {
                res.AddError(ErrorMessageCode.ActivateIdDoesNotExists, "Aktifleştirilecek kullanıcı bulunamadı.");
            }

            return res;
        }


        // Method hiding..
        public new BusinessLayerResult<Tercuman> Insert(Tercuman data)
        {
            Tercuman user = Find(x => x.Name == data.Name || x.Email == data.Email);
            BusinessLayerResult<Tercuman> res = new BusinessLayerResult<Tercuman>();

            res.Result = data;

            if (user != null)
            {
                if (user.Name == data.Name)
                {
                    res.AddError(ErrorMessageCode.NameAlreadyExists, "Kullanıcı adı kayıtlı.");
                }

                if (user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi kayıtlı.");
                }
            }
            else
            {
                res.Result.ProfileImageFilename = "user_boy.png";
                res.Result.ActivateGuid = Guid.NewGuid();

                if (base.Insert(res.Result) == 0)
                {
                    res.AddError(ErrorMessageCode.UserCouldNotInserted, "Kullanıcı eklenemedi.");
                }
            }

            return res;
        }

        public new BusinessLayerResult<Tercuman> Update(Tercuman data)
        {
            Tercuman db_user = Find(x => x.Name == data.Name || x.Email == data.Email);
            BusinessLayerResult<Tercuman> res = new BusinessLayerResult<Tercuman>();
            res.Result = data;

            if (db_user != null && db_user.Id != data.Id)
            {
                if (db_user.Name == data.Name)
                {
                    res.AddError(ErrorMessageCode.NameAlreadyExists, "Kullanıcı adı kayıtlı.");
                }

                if (db_user.Email == data.Email)
                {
                    res.AddError(ErrorMessageCode.EmailAlreadyExists, "E-posta adresi kayıtlı.");
                }

                return res;
            }

            res.Result = Find(x => x.Id == data.Id);
            res.Result.Email = data.Email;
            res.Result.Name = data.Name;
            res.Result.Surname = data.Surname;
            res.Result.Password = data.Password;
            res.Result.Name = data.Name;
            res.Result.IsActive = data.IsActive;
            

            if (base.Update(res.Result) == 0)
            {
                res.AddError(ErrorMessageCode.UserCouldNotUpdated, "Kullanıcı güncellenemedi.");
            }

            return res;
        }

    }
}
