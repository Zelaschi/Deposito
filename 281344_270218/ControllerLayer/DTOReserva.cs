﻿
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


        public DTOReserva(int idReservaDTO, DateTime fechaDesde, DateTime fechaHasta, DTODeposito deposito, DTOCliente cliente)
        {
            Id = idReservaDTO;
            FechaDesde = fechaDesde;
            FechaHasta = fechaHasta;
            Deposito = deposito;
            Cliente = cliente;
        }
    }
}
