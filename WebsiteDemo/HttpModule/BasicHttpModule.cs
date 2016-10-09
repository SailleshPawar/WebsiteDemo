using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;
using System.Text;
using System.Security.Principal;
using System.Web.Security;
using System.Web.UI;

namespace WebsiteDemo.HttpModule
{
    public class BasicHttpModule : IHttpModule
    {
        public void Init(HttpApplication context)
        {
            //registering the CheckMediaRequest method on BeginRequest event
            context.BeginRequest += CheckMediaRequest;
        }


        private void CheckMediaRequest(object source, EventArgs eventArgs)
        {
            var httpApplication = (HttpApplication) source;
            //get the path which we want to restrict from web crawler
            var restrictPath = ConfigurationManager.AppSettings["disAllow"];
            HttpBrowserCapabilities myBrowserCaps = httpApplication.Request.Browser;

            //checking if it's the browser is a crawler, HTTP verb is GET and url contains the restrict path
            if (((System.Web.Configuration.HttpCapabilitiesBase) myBrowserCaps).Crawler 
                && httpApplication.Request.HttpMethod.Equals("GET")
                && httpApplication.Request.Url.AbsoluteUri.Contains(restrictPath))
            {

               DenyAccess(httpApplication);
            }
        }


     
        private void DenyAccess(HttpApplication app)
        {
            
            app.Response.StatusCode = 401;
            app.Response.StatusDescription = "Access Denied";
            app.Response.Write("401 Access Denied");
            app.CompleteRequest();
        }

        private void AllowAccess(HttpApplication app)
        {
            app.Response.StatusCode = 200;
            app.Response.StatusDescription = "Access Allowed";
        }

        public void Dispose()
        {
        }
    }
}