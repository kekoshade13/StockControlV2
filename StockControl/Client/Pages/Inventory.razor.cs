using System.Net.Http.Json;
using MudBlazor;
using StockControl.Shared.ModelsDto;
using StockControl.Shared.Models;
using static MudBlazor.CategoryTypes;
using StockControl.Client.Shared.Dialogs;

namespace StockControl.Client.Pages
{
    public partial class Inventory
    {
        private MudTable<RepuestosEstadosDto> _table { get; set; }
        public async Task<TableData<RepuestosEstadosDto>> ServerReload(TableState state)
        {
            try
            {
                var tableContent = new TableData<RepuestosEstadosDto>();
                List<string> parameters = new()
                {
                    $"page={state.Page}"
                };
                var response = await Http.GetAsync($"api/repuestosEstados/repuestosEstados?{string.Join("&", parameters)}");

                if (response.IsSuccessStatusCode)
                {
                    PaginatedResponse<RepuestosEstadosDto> spareParts = await response.Content.ReadFromJsonAsync<PaginatedResponse<RepuestosEstadosDto>>();
                    tableContent.Items = spareParts!.Results;
                    tableContent.TotalItems = spareParts!.TotalItems;
                }

                return tableContent;
            }
            catch
            {
                return new();
            }
        }

        private async Task AddSparePart()
        {
            try
            {
                var options = new DialogOptions { CloseOnEscapeKey = true };
                var showDialog = DialogService.Show<CreateSparePart>("Crear repuesto", options);
                var result = await showDialog.Result;

                if (!result.Canceled)
                {
                    await _table.ReloadServerData();
                }
            }
            catch
            {

            }
        }

        private async Task AddQtySparePart()
        {
            try
            {

            }
            catch
            {

            }
        }

        private async Task RemoveQtySparePart()
        {
            try
            {

            }
            catch
            {

            }
        }
    }
}