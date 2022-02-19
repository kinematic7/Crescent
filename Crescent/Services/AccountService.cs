using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace Crescent.Services
{    
    public class AccountService : IAccountService
    {
        [Inject]
        SessionModel sessionWrapper { get; set; }

        private readonly HttpClient _client;
        public AccountService(HttpClient httpClient)
        {
            _client = httpClient;            
        }

        public async Task<Response> UpdateAccount(Account account)
        {            
            var response = await _client.PostAsJsonAsync($"api/AccountService/UpdateAccount", account);                        
            var result = response.Content.ReadFromJsonAsync<Response>().Result;
            return result;
        }

        public async Task<Response> IsValidLogin(Account account)
        {
            var response = await _client.PostAsJsonAsync($"api/AccountService/IsValidLogin", account);           
            return response.Content.ReadFromJsonAsync<Response>().Result;            
        }

        public async Task<bool> AuthenticateUser(SessionModel sessionModel)
        {
            bool result = false;

            try
            {
                _client.DefaultRequestHeaders.Add("Token", sessionModel.Token);                
                var response = await _client.PostAsJsonAsync($"api/AccountService/GetUserAuthentication?who={sessionModel.Who}", String.Empty);
                result = response.Content.ReadAsStringAsync().Result.ToUpper() == "SUCCESS";
            }
            catch (Exception)
            {                
                result = false;
            }
            return result;
        }
    }

    public interface IAccountService
    {
        Task<Response> UpdateAccount(Account account);
        Task<Response> IsValidLogin(Account account);
        Task<bool> AuthenticateUser(SessionModel sessionModel);
    }
}
