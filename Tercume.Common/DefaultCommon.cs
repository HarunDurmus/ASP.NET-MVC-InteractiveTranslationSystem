﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tercume.Common
{
    public class DefaultCommon:ICommon
    {
        public string GetCurrentEmail()
        {
            return "system";
        }
    }
}
