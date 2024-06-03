using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class ExportadorTXT : IExportador
    {
        public string Exportar(IList<Reserva> reservas)
        {
            var reporte = new StringBuilder();
            reporte.AppendLine("DEPOSITO\tRESERVA\tPAGO");
            foreach (var reserva in reservas)
            {
                //CAMBIAR POR LOS DATOS QUE TENEMOS QUE MOSTRAR REALMENTE
                reporte.AppendLine($"{reserva.Deposito.DepositoId}\t{reserva.ReservaId}\t{reserva.Estado}");
            }
            return reporte.ToString();
        }
    }
}
