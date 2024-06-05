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
        public void Exportar(List<Reserva> reservas)
        {
            using (StreamWriter writer = new StreamWriter("../reporte.csv"))
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

