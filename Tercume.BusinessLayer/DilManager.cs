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
    public class DilManager : ManagerBase<Dil>
    {
        public BusinessLayerResult<Dil> GetLanguageById(string ana_dil)
        {
            BusinessLayerResult<Dil> res = new BusinessLayerResult<Dil>();
            res.Result = Find(x => x.Dil_isim == ana_dil);

            if (res.Result == null)
            {
                res.AddError(ErrorMessageCode.UserNotFound, "Dil bulunamadı.");
            }

            return res;
        }

        public BusinessLayerResult<Dil> GetLanguageByIdint(int dil)
        {
            BusinessLayerResult<Dil> res = new BusinessLayerResult<Dil>();
            res.Result = Find(x => x.Id == dil);

            if (res.Result == null)
            {
                res.AddError(ErrorMessageCode.UserNotFound, "Dil bulunamadı.");
            }

            return res;
        }
    }
}
