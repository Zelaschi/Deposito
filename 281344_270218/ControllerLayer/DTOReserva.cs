
namespace ControllerLayer
{
    public class DTOReserva
    {
        private DateTime FechaInicio { get; set; }
        private DateTime FechaFin { get; set; }
        private DTODeposito Deposito { get; set; }
        private DTOCliente Cliente { get; set; }

        public DTOReserva(DateTime fechaInicio, DateTime fechaFin, DTODeposito deposito, DTOCliente cliente)
        {
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            Deposito = deposito;
            Cliente = cliente;
        }
    }
}
