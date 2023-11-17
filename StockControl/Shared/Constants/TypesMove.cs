using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StockControl.Shared.Constants
{
    public class TypesMove
    {
        public const string Entrada = "Entrada";
        public const string Salida = "Salida";

        public static List<string> Types() => new()
        {
            Entrada,
            Salida
        };
    }
}
