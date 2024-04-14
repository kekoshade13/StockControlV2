using Blazored.LocalStorage;
using Microsoft.AspNetCore.Components;
using StockControl.Client.Shared;
using StockControl.Shared.ModelsDto.RequestModels;

namespace StockControl.Client.Pages
{
    public partial class PrincipalLogin
    {
        [Inject]
        public ILocalStorageService LocalStorage { get; set; }
        [Inject]
        public NavigationManager Nav { get; set; }
        [CascadingParameter]
        public MainLayout Layout { get; set; }

        private AuthenticationRequest _loginModel = new();

        private async Task LoginUser()
        {
            var result = await AuthService.Login(_loginModel);
            if (result.IsAuthSuccessful)
            {
                Nav.NavigateTo("/", true);
            }
        }
    }
}