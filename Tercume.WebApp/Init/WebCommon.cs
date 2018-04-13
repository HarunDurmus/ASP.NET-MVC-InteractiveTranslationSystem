using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Tercume.Common;
using Tercume.Entities;
using Tercume.WebApp.Models;

namespace Tercume.WebApp.Init
{
    public class WebCommon : ICommon
    {
        public string GetCurrentEmail()
            {
                TercumeUser user = CurrentSession.User;

                if (user != null)
                    return user.Email;
                else
                    return "system";
            }
    }
}