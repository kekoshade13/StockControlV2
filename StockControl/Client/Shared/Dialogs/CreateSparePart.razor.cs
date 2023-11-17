using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Net.Http.Json;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.Web.Virtualization;
using Microsoft.AspNetCore.Components.WebAssembly.Http;
using Microsoft.JSInterop;
using StockControl.Client;
using StockControl.Client.Shared;
using MudBlazor;
using StockControl.Shared.Constants;
using StockControl.Shared.ModelsDto;

namespace StockControl.Client.Shared.Dialogs
{
    public partial class CreateSparePart
    {
        [Inject]
        public HttpClient Http { get; set; }

        [CascadingParameter]
        public MudDialogInstance MudDialog { get; set; }
        public string SparePartName { get; set; }
        public int SparePartCode { get; set; }
        private int _qty = 0;

        private List<EquiposDto> _equipos = new();
        private List<TipoStockDto> _typeStockList = new();
        private int _selectedEquipo { get; set; }
        private int _selectedTypeStock { get; set; }

        protected override async Task OnInitializedAsync()
        {
            await GetEquipos();
            await GetTipoStock();
        }

        private async Task GetEquipos()
        {
            try
            {
                var response = await Http.GetAsync($"api/equipos/equipos");
                if (response.IsSuccessStatusCode)
                {
                    _equipos = await response.Content.ReadFromJsonAsync<List<EquiposDto>>();
                }
            }
            catch
            {
            }
        }

        private async Task GetTipoStock()
        {
            try
            {
                var response = await Http.GetAsync($"api/tipostock/typeStocks");
                if (response.IsSuccessStatusCode)
                {
                    _typeStockList = await response.Content.ReadFromJsonAsync<List<TipoStockDto>>();
                }
            }
            catch
            {
            }
        }

        private async Task CreateNewSparePart(string name, int code)
        {
            try
            {
                if (!string.IsNullOrEmpty(SparePartName) && _selectedEquipo != null && _selectedTypeStock != null)
                {
                    SparePartsDto order = new SparePartsDto()
                    {
                        Name = SparePartName,
                        Code = SparePartCode,
                        Equipos = null,
                        TypeStockDto = null,
                        EquipoId = _selectedEquipo,
                        StockTypeId = _selectedTypeStock
                    };
                    var response = await Http.PostAsJsonAsync($"api/spareparts", order);
                    if (response.IsSuccessStatusCode)
                    {
                        var content = await response.Content.ReadAsStringAsync();
                        var sparePartId = int.Parse(content);

                        RepuestosEstadosDto sparePartState = new()
                        {
                            SparePartDtoId = sparePartId,
                            TipoStockId = _selectedTypeStock,
                            SparePartsDto = null,
                            TipoStockDto = null,
                            Amount = _qty
                        };

                        var responseRepuestoEstado = await Http.PostAsJsonAsync($"api/repuestosEstados", sparePartState);
                        if (response.IsSuccessStatusCode)
                        {
                            MudDialog.Close(true);
                        }
                    }
                }
            }
            catch
            {
            }
        }
    }
}