using Microsoft.AspNetCore.Components;
using MudBlazor;
using StockControl.Shared.ModelsDto;
using System.Net.Http.Json;
using static MudBlazor.CategoryTypes;

namespace StockControl.Client.Pages.Admin.Equipos
{
    public partial class VerEquipo
    {
        [Inject]
        public HttpClient Http { get; set; }
        [Inject]
        public IDialogService DialogService { get; set; }
        [Inject]
        public ISnackbar Snackbar { get; set; }
        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; }
        [Parameter]
        public int EquipoId { get; set; }

        private EquiposDto? _equipoDto = null;

        protected override async Task OnInitializedAsync()
        {
            try 
            {
                HttpResponseMessage response = await Http.GetAsync($"api/equipos/{EquipoId}");
                if (response.IsSuccessStatusCode)
                {
                    _equipoDto = await response.Content.ReadFromJsonAsync<EquiposDto>();
                }
                else
                {
                    Snackbar.Add("No se pudieron obtener los datos del equipo", Severity.Error);
                }
            }
            catch
            {
                Snackbar.Add("Ocurrió un error", Severity.Error);
            }
        }

        private async Task OpenEditDialog()
        {
            try
            {
                DialogOptions options = new() { CloseButton = false };
                DialogParameters parameters = new() { ["EquipoDto"] = _equipoDto };
                IDialogReference showDialog = DialogService.Show<CrearEquipo>("Editar equipo", parameters, options);
                var result = await showDialog.Result;

                if (!result.Canceled)
                {
                    MudDialog.Close(true);
                }
            }
            catch
            {
                Snackbar.Add("Ocurrió un error", Severity.Error);
            }
        }
    }
}