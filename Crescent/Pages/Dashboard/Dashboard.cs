using Crescent.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Crescent.Pages.Dashboard
{
    public partial class Dashboard
    {
        public string LoginId { get; set; }
        protected override async Task OnInitializedAsync()
        {
            await AuthHelper.Authenticate();
            LoginId = AuthHelper.LoginId;
            base.StateHasChanged();
            await Task.CompletedTask;            
        }       
    }
}
