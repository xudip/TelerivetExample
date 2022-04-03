using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace TelerivetExample.Models
{
    public static class Common
    {
        public static string CompanyName = ConfigurationManager.AppSettings["CompanyName"];
    }
}