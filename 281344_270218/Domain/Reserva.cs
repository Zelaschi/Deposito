namespace Domain
{
    public class Reserva
    {
        public static int UltimoID { get; set; } = 0;
        public int Id { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public Deposito Deposito { get; set; }
        public int Precio { get; set; }
        public string Estado { get; set; } = "Pendiente";

        public Reserva(DateTime fechaDesde, DateTime fechaHasta, Deposito deposito, int precio)
        {
            if (fechaDesde > fechaHasta)
            {
                throw new ArgumentException("Ingrese un periodo de fechas valido");
            }
            Id = ++UltimoID;
            FechaDesde = fechaDesde;
            FechaHasta = fechaHasta;
            Deposito = deposito;
            Precio = precio;
        }
    }
}
