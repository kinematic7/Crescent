using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Primitives;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreService.Data
{
    public class AuthenticateUser : ActionFilterAttribute
    {
        private StringValues token;
        
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            try
            {
                filterContext.HttpContext.Request.Headers.TryGetValue("Token", out token);
                if (!String.IsNullOrEmpty(token))
                {
                    var decryptedString = Cryptography.DecryptString(Settings.EncryptionKey, token);
                    var tokenTicks = decryptedString.Split('|')[0];
                    var tokenLoginId = decryptedString.Split('|')[1];
                    var who = filterContext.HttpContext.Request.Query["who"].ToString();
                    if (who != tokenLoginId)
                        throw new Exception();

                    var differnce = (DateTime.Now - new DateTime(long.Parse(tokenTicks))).Minutes;
                    
                    if(differnce > Settings.KeepTokenAliveInMinutes)
                        throw new Exception();

                }
            }
            catch (Exception)
            {
                filterContext.HttpContext.Abort();
            }
        }
    }
}
