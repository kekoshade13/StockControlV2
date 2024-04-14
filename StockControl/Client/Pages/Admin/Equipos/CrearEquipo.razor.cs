using Microsoft.AspNetCore.Components;
using MudBlazor;
using StockControl.Shared.ModelsDto;
using System.Net.Http.Json;

namespace StockControl.Client.Pages.Admin.Equipos
{
    public partial class CrearEquipo
    {
        [Inject]
        public HttpClient Http { get; set; }
        [Inject]
        public ISnackbar Snackbar { get; set; }
        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; }
        [Parameter]
        public EquiposDto EquipoDto { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                if (EquipoDto is not null)
                {
                    HttpResponseMessage response = await Http.GetAsync($"api/equipos/{EquipoDto.Id_Equip}");
                    if (response.IsSuccessStatusCode)
                    {
                        EquipoDto = await response.Content.ReadFromJsonAsync<EquiposDto>();
                    }
                    else
                    {
                        Snackbar.Add("No se pudieron obtener los datos del equipo", Severity.Error);
                    }
                }
                else
                {
                    EquipoDto = new();
                }
            }
            catch
            {
                Snackbar.Add("Ocurrió un error", Severity.Error);
            }
        }

        private async Task Submit()
        {
            try
            {
                HttpResponseMessage response = await Http.PostAsJsonAsync($"api/equipos", EquipoDto);
                if (response.IsSuccessStatusCode)
                {
                    MudDialog.Close(true);
                    Snackbar.Add("Equipo creado correctamente", Severity.Success);
                }
                else
                {
                    Snackbar.Add("No se pudo crear el equipo", Severity.Error);
                }
            }
            catch
            {
                Snackbar.Add("Ocurrió un error", Severity.Error);
            }
        }
    }
}