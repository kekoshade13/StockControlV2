using System.Net.Http.Json;
using MudBlazor;
using StockControl.Shared.ModelsDto;
using StockControl.Shared.Models;

namespace StockControl.Client.Pages.Orders
{
    public partial class Orders
    {
        private MudTable<OrdenesTotalesDto> _table { get; set; }
        public async Task<TableData<OrdenesTotalesDto>> ServerReload(TableState state)
        {
            try
            {
                var tableContent = new TableData<OrdenesTotalesDto>();
                List<string> parameters = new()
                {
                    $"page={state.Page}"
                };
                var response = await Http.GetAsync($"api/orders/ordenesTotales?{string.Join("&", parameters)}");

                if (response.IsSuccessStatusCode)
                {
                    PaginatedResponse<OrdenesTotalesDto> spareParts = await response.Content.ReadFromJsonAsync<PaginatedResponse<OrdenesTotalesDto>>();
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

        private async Task CreateNewOrder()
        {
            try
            {
                var options = new DialogOptions { CloseOnEscapeKey = true };
                var showDialog = DialogService.Show<CreateOrder>("Crear orden", options);
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

        private async Task GoToOrder(int orderId)
        {
            Navigation.NavigateTo($"/order/{orderId}");
        }
    }
}