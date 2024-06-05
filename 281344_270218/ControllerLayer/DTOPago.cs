namespace ControllerLayer
{
    public class DTOPago
    {
        public int PagoId { get; set; }
        public string EstadoPago { get; set; }
        public DTOPago(int pagoId, string estadoPago) {
            PagoId = pagoId;
            EstadoPago = estadoPago;
        }
    }
}
