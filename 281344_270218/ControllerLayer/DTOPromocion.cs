
namespace ControllerLayer
{
    public class DTOPromocion
    {
        public string Etiqueta { get; set; }
        public int PorcentajeDescuento { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFIn { get; set; }
        
        public DTOPromocion (string aEtiqueta, int aPorcentajeDescuento, DateTime aFechaInicio, DateTime aFechaFin)
        {
            Etiqueta = aEtiqueta;
            PorcentajeDescuento = aPorcentajeDescuento;
            FechaInicio = aFechaInicio;
            FechaFIn = aFechaFin;
        }
    }
}
