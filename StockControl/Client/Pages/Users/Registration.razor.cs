using Microsoft.AspNetCore.Components;
using MudBlazor;
using StockControl.Shared.ModelsDto;

namespace StockControl.Client.Pages.Users
{
    public partial class Registration
    {
        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; }
        public UserRegistrationDto UserRegistrationDto { get; set; }

        private async Task CreateUser()
        {
        }
    }
}