using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using Telerivet.Client;

namespace TelerivetExample.Models
{
    public class TelerivetViewModel
    {

    }
    public class TelerivetTokenStatus
    {
        public int StatusCode { get; set; }
        public bool IsValid { get; set; }
        public string Message { get; set; }
    }

    public static class TelerivetProject
    {
        public static TelerivetAPI telerivetAPI= new TelerivetAPI(ConfigurationManager.AppSettings["TelerivetAPIKey"]);
        public static Project project = telerivetAPI.InitProjectById(ConfigurationManager.AppSettings["TelerivetProjectID"]);
    }

}