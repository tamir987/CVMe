﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CVMe.Common.ExtensionMethods
{
    public static class ExceptionExtensions
    {
        public static string GetFullMessage(this Exception ex)
        {
            return ex.InnerException == null
                ? ex.Message
                : ex.Message + " --> " + ex.InnerException.GetFullMessage();
        }
    }
}
