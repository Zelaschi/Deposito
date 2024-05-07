
using Domain;

namespace ControllerLayer
{
    public class DTODeposito
    {
        public int Id { get; set; }
        public string Area { get; set; }
        public string Tamanio { get; set; }
        public bool Climatizacion { get; set; }
        public List<Promocion> listaPromocionesQueAplicanADeposito = new List<Promocion>();

        public DTODeposito(int id, string area, string tamanio, bool climatizacion)
        {
            Id = id;
            Area = area;
            Tamanio = tamanio;
            Climatizacion = climatizacion;
        }
    }
}
