using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class ExportadorCSV : IExportador
    {
        public void Exportar(IList<Reserva> reservas)
        {
            //string path = Path.Combine(AppContext.BaseDirectory, "Reportes", "reporte.csv");
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string reportesPath = Path.Combine(desktopPath, "Reportes");
            Directory.CreateDirectory(reportesPath);
            //Directory.CreateDirectory(Path.GetDirectoryName(path));
            string path = Path.Combine(reportesPath, "reporte.csv");
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine("DEPOSITO,RESERVA,PAGO");
                foreach (var reserva in reservas)
                {
                    var depositoDatos = $"ID: {reserva.Deposito.DepositoId}, Nombre: {reserva.Deposito.Nombre}, Area: {reserva.Deposito.Area}, Tamanio: {reserva.Deposito.Tamanio}, Climatizacion: {reserva.Deposito.Climatizacion}";
                    var reservaDatos = $"ID: {reserva.ReservaId}, Desde: {reserva.FechaDesde}, Hasta: {reserva.FechaHasta}, Precio: {reserva.Precio}, Estado: {reserva.Estado}";
                    if (reserva.Pago.EstadoPago != null)
                    {
                        writer.WriteLine($"{depositoDatos},{reservaDatos}, Estado del pago: {reserva.Pago.EstadoPago}");
                    }
                    else
                    {
                        writer.WriteLine($"{depositoDatos},{reservaDatos}, La reserva a sido rechazada");
                    }

                }
            }
        }
    }
}

