using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crescent
{
    public class AuthenticationHelper
    {        
        
        public bool IsAuthenticUser { get; set; }
        public string LoginId { get; set; }
        private SessionModel _sessionModel { get; set; }
        private Services.IAccountService _accountService { get; set; }

        private ProtectedSessionStorage _protectedSessionStore { get; set; }

        public AuthenticationHelper(ProtectedSessionStorage ProtectedSessionStore, Services.IAccountService AccountService)
        {            
            _sessionModel = new SessionModel();
            _accountService = AccountService;
            _protectedSessionStore = ProtectedSessionStore;
        }

        public async Task Authenticate()
        {            
            _sessionModel = (await _protectedSessionStore.GetAsync<SessionModel>("Session")).Value;
            IsAuthenticUser = await _accountService.AuthenticateUser(_sessionModel);
            LoginId = _sessionModel.Who;           
            await Task.CompletedTask;
        }
    }
}
