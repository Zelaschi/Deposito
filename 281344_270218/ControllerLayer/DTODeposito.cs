
using Domain;

namespace ControllerLayer
{
    public class DTODeposito
    {
        public string Nombre = "undefined";

        public int Id { get; set; } = 0;
        public string Area { get; set; }
        public string Tamanio { get; set; }
        public bool Climatizacion { get; set; }
        public DateTime DisponibleDesde { get; set; }
        public DateTime DisponibleHasta { get; set; }
        public List<Promocion> listaPromocionesQueAplicanADeposito = new List<Promocion>();

        public DTODeposito(string nombre, string area, string tamanio, bool climatizacion, DateTime disponibleDesde, DateTime disponibleHasta)
        {
            Id = 0;
            Area = area;
            Tamanio = tamanio;
            Climatizacion = climatizacion;
            Nombre = nombre;
            DisponibleDesde = disponibleDesde;
            DisponibleHasta = disponibleHasta;
        }
        public DTODeposito( int id, string area, string tamanio, bool climatizacion)
        {
            Id = id;
            Area = area;
            Tamanio = tamanio;
            Climatizacion = climatizacion;
            DisponibleDesde = DateTime.Today;
            DisponibleHasta = DateTime.Today.AddDays(1);
        }
        public DTODeposito(string nombre, int id, string area, string tamanio, bool climatizacion)
        {
            Id = id;
            Area = area;
            Tamanio = tamanio;
            Climatizacion = climatizacion;
            Nombre = nombre;
            DisponibleDesde = DateTime.Today;
            DisponibleHasta = DateTime.Today.AddDays(1);
        }
    }
}
