
namespace ControllerLayer
{
    public class DTOPromocion
    {
        public int PromocionId { get; set; }
        public string Etiqueta { get; set; }
        public int PorcentajeDescuento { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFIn { get; set; }
        
        public DTOPromocion (int aId, string aEtiqueta, int aPorcentajeDescuento, DateTime aFechaInicio, DateTime aFechaFin)
        {
            PromocionId = aId;
            Etiqueta = aEtiqueta;
            PorcentajeDescuento = aPorcentajeDescuento;
            FechaInicio = aFechaInicio;
            FechaFIn = aFechaFin;
        }
    }
}
