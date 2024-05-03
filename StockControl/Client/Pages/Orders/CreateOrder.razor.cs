using Microsoft.AspNetCore.Components;
using MudBlazor;
using StockControl.Shared.ModelsDto;
using System.Net.Http.Json;

namespace StockControl.Client.Pages.Orders
{
    public partial class CreateOrder
    {
        [Inject]
        public HttpClient Http { get; set; }
        [Inject]
        public ISnackbar Snackbar { get; set; }
        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; }

        private List<EquiposDto> _equipos = new();

        private OrdenesTotalesDto _ordenTotalDto = new();

        protected override async Task OnInitializedAsync()
        {
            await GetEquipos();
        }

        private async Task CreateNewOrder()
        {
            try
            {
                if (!string.IsNullOrEmpty(_ordenTotalDto.nOrden))
                {
                    _ordenTotalDto.EquipoId = _ordenTotalDto.Equipos!.Id_Equip;
                    _ordenTotalDto.Equipos = null;
                    HttpResponseMessage response = await Http.PostAsJsonAsync($"api/orders", _ordenTotalDto);
                    if (response.IsSuccessStatusCode)
                    {
                        MudDialog.Close(true);
                    }
                }
            }
            catch
            {
                Snackbar.Add("Ocurrió un error", Severity.Error);
            }
        }

        private async Task GetEquipos()
        {
            try
            {
                HttpResponseMessage response = await Http.GetAsync($"api/equipos/equipos");
                if (response.IsSuccessStatusCode)
                {
                    _equipos = await response.Content.ReadFromJsonAsync<List<EquiposDto>>();
                }
                else
                {
                    Snackbar.Add("No se pudieron obtener los equipos", Severity.Error);
                }
            }
            catch
            {
                Snackbar.Add("Ocurrió un error", Severity.Error);
            }
        }
    }
}