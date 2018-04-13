using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tercume.BusinessLayer.Abstract;
using Tercume.BusinessLayer.Result;
using Tercume.Entities;
using Tercume.Entities.Messages;

namespace Tercume.BusinessLayer
{
    public class DilTercumenManager : ManagerBase<DilTercumen>
    {
        public BusinessLayerResult<DilTercumen> RegisterTercuman(DilTercumen data)
        {
            // Kullanıcı Name kontrolü..
            // Kullanıcı e-posta kontrolü..
            // Kayıt işlemi..
            // Aktivasyon e-postası gönderimi.
            // Tercuman user = Find(x => x.Email == data.EMail || x.Email == data.EMail);
            BusinessLayerResult<DilTercumen> res = new BusinessLayerResult<DilTercumen>();

            int dbResult = base.Insert(new DilTercumen()
            {

                Dil_isimler = data.Dil_isimler,
                Tercumanlar=data.Tercumanlar
                

            });

            return res;
        }


        public BusinessLayerResult<DilTercumen> GetDilTercumenByTranslatorId(int id)
        {
            BusinessLayerResult<DilTercumen> res = new BusinessLayerResult<DilTercumen>();
            res.Result = Find(x => x.Tercumanlar == id);

            if (res.Result == null)
            {
                res.AddError(ErrorMessageCode.UserNotFound, "Kullanıcı bulunamadı.");
            }

            return res;
        }



    }

    
}
