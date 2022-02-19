﻿using Crescent.Services;
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
            await Task.CompletedTask;            
        }

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                await AuthHelper.Authenticate();
                if (!AuthHelper.IsAuthenticUser)
                {
                    Navigate.NavigateTo("NotAuthenticUser");
                }
                else
                {
                    LoginId = AuthHelper.LoginId;
                }
                base.StateHasChanged();
            }

        }
    }
}