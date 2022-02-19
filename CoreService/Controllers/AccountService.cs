using CoreService.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoreService.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AccountService : ControllerBase
    {        
        public AccountService()
        {
            // Do Nothing
        }

        [Route("UpdateAccount")]
        public IActionResult UpdateAccount(Models.Account account)
        {
            Response result = new Response() { IsSuccess = true };
            using (var context = new CrescentDbContext())
            {
                var hasAccount = context.Accounts.Any(acc => acc.LoginId.ToLower() == account.LoginId.ToLower());
                if (!hasAccount)
                {
                    try
                    {
                        context.Accounts.Add(account);
                        context.SaveChanges();
                        result.IsSuccess = true;
                    }
                    catch(Exception ex)
                    {
                        result.IsSuccess = false;
                        result.ErrorMessage = ex.Message;
                    }
                }
                else
                {
                    result.IsSuccess = false;
                    result.ErrorMessage = "Login Id already exists";
                }
                return new OkObjectResult(result);
            }
        }

        [Route("IsValidLogin")]
        public IActionResult IsValidLogin(Models.Account account)
        {
            var result = new Response();
            using (var context = new CrescentDbContext())
            {
                result.IsSuccess = context.Accounts.Any(
                    acc => acc.LoginId.ToLower() == account.LoginId.ToLower() && acc.Password == account.Password);
                if(result.IsSuccess)
                {
                    result.SuccessMessage = Cryptography.EncryptString(Settings.EncryptionKey, $"{DateTime.Now.Ticks.ToString()}|{account.LoginId}");
                }
            }

            return new OkObjectResult(result);
        }

        [Route("GetUserAuthentication")]
        [AuthenticateUser]
        public IActionResult GetUserAuthentication(string who)
        {
            var result = "SUCCESS";           
            return new OkObjectResult(result);            
        }
    }    
}
