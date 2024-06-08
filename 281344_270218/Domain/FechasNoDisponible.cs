

namespace Domain
{
    public class FechasNoDisponible
    {
        public int FechasNoDisponibleId { get; set; }
        public int DepositoId { get; set; }
        public Deposito? Deposito { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public FechasNoDisponible() { }
        public FechasNoDisponible(DateTime fechaDesde,DateTime fechaHasta) {
            FechaDesde = fechaDesde;
            FechaHasta = fechaHasta;
        }
    }
}
