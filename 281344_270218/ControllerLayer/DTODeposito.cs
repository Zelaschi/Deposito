
using Domain;

namespace ControllerLayer
{
    public class DTODeposito
    {
        public string Area { get; set; }
        public string Tamanio { get; set; }
        public bool Climatizacion { get; set; }
        public List<Promocion> listaPromocionesQueAplicanADeposito = new List<Promocion>();

        public DTODeposito(string area, string tamanio, bool climatizacion)
        {
            Area = area;
            Tamanio = tamanio;
            Climatizacion = climatizacion;
        }
    }
}
