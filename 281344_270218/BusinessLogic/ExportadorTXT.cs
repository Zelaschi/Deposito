using Domain;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class ExportadorTXT : IExportador
    {
        public void Exportar(IList<Reserva> reservas)
        {
            //string path = Path.Combine(AppContext.BaseDirectory, "Reportes", "reporte.csv");
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string reportesPath = Path.Combine(desktopPath, "Reportes");
            Directory.CreateDirectory(reportesPath);
            //Directory.CreateDirectory(Path.GetDirectoryName(path));
            string path = Path.Combine(reportesPath, "reporte.txt");
            using (StreamWriter writer = new StreamWriter(path))
            {
                writer.WriteLine("DEPOSITO\tRESERVA\tPAGO");
                foreach (var reserva in reservas)
                {
                    var depositoDatos = $"ID: {reserva.Deposito.DepositoId}\tNombre: {reserva.Deposito.Nombre}\tArea: {reserva.Deposito.Area}\tTamanio: {reserva.Deposito.Tamanio}\tClimatizacion: {reserva.Deposito.Climatizacion}";
                    var reservaDatos = $"ID: {reserva.ReservaId}\tDesde: {reserva.FechaDesde}\tHasta: {reserva.FechaHasta}\tPrecio: {reserva.Precio}\tEstado: {reserva.Estado}";
                    if(reserva.Pago.EstadoPago != null)
                    {
                        writer.WriteLine($"{depositoDatos}\t{reservaDatos}\tEstado del pago: {reserva.Pago.EstadoPago}");
                    }
                    else
                    {
                        writer.WriteLine($"{depositoDatos}\t{reservaDatos}\tLa reserva a sido rechazada");
                    }
                    
                }
            }
        }
    }
}

