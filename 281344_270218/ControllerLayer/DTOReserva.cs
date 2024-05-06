
namespace ControllerLayer
{
    public class DTOReserva
    {
        private DateTime FechaInicio { get; set; }
        private DateTime FechaFin { get; set; }
        private DTODeposito Deposito { get; set; }
        private DTOCliente Cliente { get; set; }

        private int IdReservaDTO {  get; set; } 

        public DTOReserva(int idReservaDTO, DateTime fechaInicio, DateTime fechaFin, DTODeposito deposito, DTOCliente cliente)
        {
            IdReservaDTO = idReservaDTO;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
            Deposito = deposito;
            Cliente = cliente;
        }
    }
}
