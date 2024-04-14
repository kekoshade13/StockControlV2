using Microsoft.AspNetCore.Components;
using MudBlazor;
using StockControl.Client.Services;
using StockControl.Shared.ModelsDto.RequestModels;

namespace StockControl.Client.Pages
{
    public partial class Register
    {
        [Inject]
        public IAuthService AuthService { get; set; }
        [Inject]
        public NavigationManager Nav { get; set; }
        [Inject]
        public ISnackbar Snackbar { get; set; }

        private RegisterRequest _registerModel = new();

        private async Task RegisterUser()
        {
            try
            {
                RegisterResponse result = await AuthService.Register(_registerModel);
                if (result.Successful)
                {
                    Snackbar.Add("Registro exitoso", Severity.Success);
                    Nav.NavigateTo("/login");
                }
            }
            catch
            {
                Snackbar.Add("Ocurrió un error, intenta nuevamente.", Severity.Error);
            }
        }
    }
}