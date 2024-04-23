namespace BusinessLogic
{
    public class Promocion
    {
        public int Id { get; set; }
        public string Etiqueta { get; set; }
        public int PorcentajeDescuento { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }

        public Promocion(int id, string etiqueta, int porcentajeDescuento, DateTime fechaInicio, DateTime fechaFin)
        {
            Id = id;
            Etiqueta = etiqueta;
            PorcentajeDescuento = porcentajeDescuento;
            FechaInicio = fechaInicio;
            FechaFin = fechaFin;

        }
    }
}
