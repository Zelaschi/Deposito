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
        private DateTime _fechaInicio;
        public DateTime FechaInicio {
            get { return _fechaInicio; }
            set
            {
                if (value.CompareTo(FechaFin) <= 0)
                {
                    throw new ArgumentException("La fecha de inicio debe ser anterior que la fecha de fin.");
                }
                _fechaInicio = value;
            }

        }
        private DateTime _fechaFin;
        public DateTime FechaFin
        {
            get { return _fechaFin; }
            set
            {
                if (_fechaInicio.CompareTo(value) >= 0)
                {
                    throw new ArgumentException("La fecha de fin debe ser posterior que la fecha de inicio.");
                }
                _fechaFin = value;
            }
        }

        public Promocion( string etiqueta, int porcentajeDescuento, DateTime fechaInicio, DateTime fechaFin)
        {
            if (fechaInicio.CompareTo(fechaFin) >= 0)
            {
                throw new ArgumentException("La fecha de inicio debe ser anterior que la fecha de Fin.");
            }
            Id = contadorPromo;
            contadorPromo++;
            Etiqueta = etiqueta;
            PorcentajeDescuento = porcentajeDescuento;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;
        }
    }
}
