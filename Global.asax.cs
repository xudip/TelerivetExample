using Dapper;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;

namespace TelerivetExample
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            SqlMapper.AddTypeHandler(new   DateTimeHandler());
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }

    public class DateTimeHandler : SqlMapper.TypeHandler<DateTime>
    {
        public override void SetValue(IDbDataParameter parameter, DateTime value)
        {
            parameter.Value = value;
        }

        public override DateTime Parse(object value)
        {
            TimeZoneInfo timezone = TimeZoneInfo.FindSystemTimeZoneById("Nepal Standard Time");
            DateTime localTime = TimeZoneInfo.ConvertTimeFromUtc((DateTime)value, timezone);
            return DateTime.SpecifyKind((DateTime)localTime, DateTimeKind.Local);
        }
    }
}
