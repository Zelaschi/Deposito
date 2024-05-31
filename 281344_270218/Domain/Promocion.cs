namespace Domain
{
    public class Promocion
    {
        public IList<DepositoPromocion> DepositoPromocions { get; set; }
        public IList<Reserva> Reservas { get; set; }

        public int PromocionId { get; set; }
        private string _etiqueta;
        public string Etiqueta {
            get
            {
                return _etiqueta;
            }
            set
            {
                if (value.Length > 20)
                {
                    throw new ArgumentException("El largo de la etiqueta debe ser menor a 21 caracteres.");
                }
                _etiqueta = value;
            }
        }
        private int _porcentajeDescuento;
        private int minPorcentaje = 5;
        private int maxPorcentaje = 75;
        public int PorcentajeDescuento { 
            get { return _porcentajeDescuento; } 
            set {
                if (value < minPorcentaje || value > maxPorcentaje) {
                    throw new ArgumentException("El porcentaje de descuento debe ser mayor a 5 y menor a 75");
                }
                _porcentajeDescuento = value;
            } 
        }
        public DateTime FechaInicio { get; set; }

        public DateTime FechaFin { get; set; }

        private bool ValidarFechaInicioSeaAnteriorAFechaFin(DateTime fechaInicio, DateTime fechaFin)
        {
            return fechaInicio.CompareTo(fechaFin) <= 0;
        }

        public Promocion(string etiqueta, int porcentajeDescuento, DateTime fechaInicio, DateTime fechaFin)
        {
            if (!ValidarFechaInicioSeaAnteriorAFechaFin(fechaInicio, fechaFin)){
                throw new ArgumentException("La fecha de inicio debe ser anterior que la fecha de fin.");
            }
            PromocionId = 0;
            Etiqueta = etiqueta;
            PorcentajeDescuento = porcentajeDescuento;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
        }
        public Promocion(int id, string etiqueta, int porcentajeDescuento, DateTime fechaInicio, DateTime fechaFin)
        {
            if (!ValidarFechaInicioSeaAnteriorAFechaFin(fechaInicio, fechaFin)){
                throw new ArgumentException("La fecha de inicio debe ser anterior que la fecha de fin.");
            }
            PromocionId = id;
            Etiqueta = etiqueta;
            PorcentajeDescuento = porcentajeDescuento;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
        }
    }
}
