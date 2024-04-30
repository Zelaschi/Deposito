namespace Domain
{
    public class Promocion
    {
        private static int contadorPromo = 0;
        public int Id { get; set; }
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
        public int PorcentajeDescuento { get; set; }
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
            Id = contadorPromo;
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
            Id = id;
            Etiqueta = etiqueta;
            PorcentajeDescuento = porcentajeDescuento;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
        }
    }
}
