using DemoUserManagement.UtilityLayer;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;

namespace DemoUserManagement.web
{
    public class GetFile : IHttpHandler
    {
        public void ProcessRequest(HttpContext context)
        {
           
        }

        public bool IsReusable
        {
            get { return false; }
        }     
        
    }
}
