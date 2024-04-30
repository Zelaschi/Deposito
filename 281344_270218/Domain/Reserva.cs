namespace Domain
{
    public class Reserva
    {
        private static int UltimoID { get; set; } = 0;
        public int IdReserva { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public Deposito Deposito { get; set; }
        public int Precio { get; set; }
        public string Estado { get; set; } = "Pendiente";

        public Cliente Cliente { get; set; }

        private bool ValidarFechaInicioSeaAnteriorAFechaFin(DateTime fechaDesde, DateTime fechaHasta)
        {
            return fechaDesde.CompareTo(fechaHasta) <= 0;
        }

        public Reserva(DateTime fechaDesde, DateTime fechaHasta, Deposito deposito, int precio, Cliente cliente)
        {
            if (!ValidarFechaInicioSeaAnteriorAFechaFin(fechaDesde, fechaHasta))
            {
                throw new ArgumentException("La fecha de inicio debe ser anterior que la fecha de fin.");
            }
            IdReserva = ++UltimoID;
            FechaDesde = fechaDesde;
            FechaHasta = fechaHasta;
            Deposito = deposito;
            Precio = precio;
            Cliente = cliente;
        }

        public Reserva(int id, DateTime fechaDesde, DateTime fechaHasta, Deposito deposito, int precio, Cliente cliente)
        {
            if (!ValidarFechaInicioSeaAnteriorAFechaFin(fechaDesde, fechaHasta))
            {
                throw new ArgumentException("La fecha de inicio debe ser anterior que la fecha de fin.");
            }
            IdReserva = id;
            FechaDesde = fechaDesde;
            FechaHasta = fechaHasta;
            Deposito = deposito;
            Precio = precio;
            Cliente = cliente;
        }
        private int CalculoPrecioDeReserva() {
            int precioPorDiaDependiendoDelTamaño = 75;
            TimeSpan diferencia = FechaHasta - FechaDesde;
            int cantidadDeDias = diferencia.Days;
            int precioReserva = precioPorDiaDependiendoDelTamaño * cantidadDeDias;
            if (Deposito.Climatizacion) 
            {
                precioReserva += 20 * cantidadDeDias;
            }

            return precioReserva;

        }
        public Reserva(DateTime fechaDesde, DateTime fechaHasta, Deposito deposito, Cliente cliente) 
        {
            if (!ValidarFechaInicioSeaAnteriorAFechaFin(fechaDesde, fechaHasta))
            {
                throw new ArgumentException("La fecha de inicio debe ser anterior que la fecha de fin.");
            }
            IdReserva = ++UltimoID;
            FechaDesde = fechaDesde;
            FechaHasta = fechaHasta;
            Deposito = deposito;
            Cliente = cliente;
            Precio = CalculoPrecioDeReserva();
        }
        
    }
}
