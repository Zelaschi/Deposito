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
            string currentDirectory = AppContext.BaseDirectory;
            string reportesPath = Path.Combine(currentDirectory, "..", "..", "..", "..", "..", "Reportes");
            Directory.CreateDirectory(reportesPath);
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

