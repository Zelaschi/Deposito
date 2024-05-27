
namespace Domain
{
    public class DepositoPromocion
    {
        public int DepositoId { get; set; }
        public Deposito Deposito { get; set; }
        public int PromocionId { get; set; }
        public Promocion Promocion { get; set; }
    }
}
