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
    public class MailManager : ManagerBase<Mesaj>
    {
        public BusinessLayerResult<Mesaj> GetMail(int id)
        {
            BusinessLayerResult<Mesaj> res = new BusinessLayerResult<Mesaj>();
            res.Result = Find(x => x.Id == id);

            if (res.Result == null)
            {
                res.AddError(ErrorMessageCode.UserNotFound, "mesaj bulunamadı.");
            }

            return res;
        }
    }
}
