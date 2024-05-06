
namespace ControllerLayer
{
    public class DTOReserva
    {
        public int Id { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public DTODeposito Deposito { get; set; }
        public DTOCliente Cliente { get; set; }
        public string Estado { get; set; } = "Pendiente";


        public DTOReserva(int id, DateTime fechaDesde, DateTime fechaHasta, DTODeposito deposito, DTOCliente cliente)
        {
            Id = id;
            FechaDesde = fechaDesde;
            FechaHasta = fechaHasta;
            Deposito = deposito;
            Cliente = cliente;
        }
    }
}
