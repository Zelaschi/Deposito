namespace Domain
{
    public class Promocion
    {
        private static int contadorPromo = 0;
        public int IdPromocion { get; set; }
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
        public int PorcentajeDescuento { 
            get { return _porcentajeDescuento; } 
            set {
                if (value < 5 || value > 75) {
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
            IdPromocion = contadorPromo;
            contadorPromo++;
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
            IdPromocion = id;
            Etiqueta = etiqueta;
            PorcentajeDescuento = porcentajeDescuento;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
        }
    }
}
