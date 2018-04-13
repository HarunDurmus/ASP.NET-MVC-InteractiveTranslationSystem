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
    public class TranslateManager : ManagerBase<Translate>
    {
        public BusinessLayerResult<Translate> GetTranslate(int id)
        {
            BusinessLayerResult<Translate> res = new BusinessLayerResult<Translate>();
            res.Result = Find(x => x.Id == id);

            if (res.Result == null)
            {
                res.AddError(ErrorMessageCode.UserNotFound, "Kullanıcı bulunamadı.");
            }

            return res;
        }
    }
}
