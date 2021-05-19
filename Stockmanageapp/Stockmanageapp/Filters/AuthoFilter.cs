using Stockmanageapp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;

namespace Stockmanageapp.Filters
{
    public class AuthoFilter : AuthorizationFilterAttribute
    {
        public override void OnAuthorization(HttpActionContext actionContext)
        {
            base.OnAuthorization(actionContext);
            if (actionContext.Request.Headers.Authorization == null)
            {
                HttpResponseMessage httpResponse = new System.Net.Http.HttpResponseMessage(HttpStatusCode.BadRequest);
                httpResponse.Content = new StringContent("Authorization data is missing");
                httpResponse.ReasonPhrase = "No Data for Authorization";
                actionContext.Response = httpResponse;
            }
            else
            {
                String encodedData = actionContext.Request.Headers.Authorization.Parameter;
                String decodedData = Encoding.UTF8.GetString(Convert.FromBase64String(encodedData));
                String[] udata = decodedData.Split(':');
                String uname = udata[0];
                String upass = udata[1];
                DBStockEntities dbb = new DBStockEntities();
                //check admin table username data
                Admin u1 = dbb.Admins.Where(u => u.UserName == uname && u.Password.Equals(upass)).FirstOrDefault();
                if (u1 != null)
                {
                    Thread.CurrentPrincipal = new GenericPrincipal(new GenericIdentity(u1.UserName), null);
                }
                else
                {
                    HttpResponseMessage httpResponse = new HttpResponseMessage(HttpStatusCode.Unauthorized);
                    httpResponse.Content = new StringContent("You are not an Authorize user to perform this operation!");
                    httpResponse.ReasonPhrase = "Not Authorized!";
                    actionContext.Response = httpResponse;
                }
            }
        }
    }
}