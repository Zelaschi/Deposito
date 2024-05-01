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
            int precioPorDiaDependiendoDelTamaño = 0;
            TimeSpan diferencia = FechaHasta - FechaDesde;
            int cantidadDeDias = diferencia.Days;
            double descuento = 0;

            switch (Deposito.Tamanio)
            {
                case "Pequenio":
                    precioPorDiaDependiendoDelTamaño = 50;
                    break;
                case "Mediano":
                    precioPorDiaDependiendoDelTamaño = 75;
                    break;
                case "Grande":
                    precioPorDiaDependiendoDelTamaño = 100;
                    break;
            }

            if (cantidadDeDias >= 7 && cantidadDeDias <= 14)
            {
                descuento = 0.05;
            }
            else if (cantidadDeDias > 14) {
                descuento = 0.10;
            }

            int precioReserva = precioPorDiaDependiendoDelTamaño * cantidadDeDias;

            if (Deposito.Climatizacion)
            {
                precioReserva += 20 * cantidadDeDias;
            }

            double precioConDescuento = precioReserva * (1 - descuento);
            precioReserva = (int)precioConDescuento;

            var porcentajeDescuentoPromocion = Deposito.mejorPromocionHoy();
            if (porcentajeDescuentoPromocion != null) 
            {
                double descuentoPromocion = ((100 - porcentajeDescuentoPromocion.PorcentajeDescuento) * 0.01);
                double precioConDescuentoPromocion = precioReserva * descuentoPromocion;
                precioReserva = (int)precioConDescuentoPromocion;
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
