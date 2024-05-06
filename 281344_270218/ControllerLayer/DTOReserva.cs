
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

<<<<<<< HEAD

        public DTOReserva(int id, DateTime fechaDesde, DateTime fechaHasta, DTODeposito deposito, DTOCliente cliente)
        {
            Id = id;
            FechaDesde = fechaDesde;
            FechaHasta = fechaHasta;
=======
        private int IdReservaDTO {  get; set; } 

        public DTOReserva(int idReservaDTO, DateTime fechaInicio, DateTime fechaFin, DTODeposito deposito, DTOCliente cliente)
        {
            IdReservaDTO = idReservaDTO;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
>>>>>>> 6cbd7b973a83617d45bb1f5eff8cd994e70dba0f
            Deposito = deposito;
            Cliente = cliente;
        }
    }
}
