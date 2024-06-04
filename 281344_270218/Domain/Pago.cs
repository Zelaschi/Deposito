using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Pago
    {
        public int PagoId { get; set; }
        public int ReservaId { get; set; }
        public Reserva Reserva { get; set; }
        public string EstadoPago { get; set; } = "Reservado";
        public Pago() { }
    }
}
