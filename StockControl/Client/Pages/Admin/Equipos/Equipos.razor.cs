using Microsoft.AspNetCore.Components;
using MudBlazor;
using StockControl.Client.Shared.Dialogs;
using StockControl.Shared.Models;
using StockControl.Shared.ModelsDto;
using System.Net.Http.Json;

namespace StockControl.Client.Pages.Admin.Equipos
{
    public partial class Equipos
    {
        [Inject]
        public HttpClient Http { get; set; }
        [Inject]
        public IDialogService DialogService { get; set; }
        [Inject]
        public ISnackbar Snackbar { get; set; }
        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; }

        private MudTable<EquiposDto> _table;

        private async Task<TableData<EquiposDto>> ServerReload(TableState state)
        {
            try
            {
                TableData<EquiposDto> tableContent = new();
                List<string> parameters = new()
                {
                    $"page={state.Page}"
                };
                HttpResponseMessage response = await Http.GetAsync($"api/equipos/equipos?{string.Join("&", parameters)}");

                if (response.IsSuccessStatusCode)
                {
                    PaginatedResponse<EquiposDto> spareParts = await response.Content.ReadFromJsonAsync<PaginatedResponse<EquiposDto>>();
                    tableContent.Items = spareParts!.Results;
                    tableContent.TotalItems = spareParts!.TotalItems;
                }

                return tableContent;
            }
            catch
            {
                Snackbar.Add("Ocurrió un error", Severity.Error);
                return new();
            }
        }

        private async Task OpenViewDialog(int equipoId)
        {
            try
            {
                DialogOptions options = new() { CloseButton = false };
                DialogParameters parameters = new() { ["EquipoId"] = equipoId };
                IDialogReference showDialog = DialogService.Show<VerEquipo>("Ver equipo", parameters, options);
                DialogResult result = await showDialog.Result;
                if (!result.Canceled && (bool)result.Data)
                {
                    await _table.ReloadServerData();
                }
            }
            catch
            {
                Snackbar.Add("Ocurrió un error", Severity.Error);
            }
        }

        private async Task Delete(int equipoId)
        {
            try
            {
                HttpResponseMessage response = await Http.DeleteAsync($"api/equipos/{equipoId}");
                if (response.IsSuccessStatusCode)
                {
                    await _table.ReloadServerData();
                    Snackbar.Add("Equipo eliminado correctamente", Severity.Success);
                }
            }
            catch
            {
                Snackbar.Add("Ocurrió un error", Severity.Error);
            }
        }

        private async Task OpenEditDialog(EquiposDto equipo) => await OpenCreateDialog(equipo);

        private async Task OpenCreateDialog(EquiposDto equipo = null)
        {
            try
            {
                DialogOptions options = new() { CloseButton = false };
                DialogParameters parameters = new()
                {
                    ["Edit"] = equipo is not null,
                    ["EquipoDto"] = equipo
                };
                IDialogReference showDialog = DialogService.Show<CrearEquipo>("Crear equipo", parameters, options);
                DialogResult result = await showDialog.Result;
                if (!result.Canceled)
                {
                    await _table.ReloadServerData();
                }
            }
            catch
            {
                Snackbar.Add("Ocurrió un error", Severity.Error);
            }
        }
    }
}