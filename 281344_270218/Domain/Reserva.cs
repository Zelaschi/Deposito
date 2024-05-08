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
        private int precioPorDia(string tamanio) {
            switch (tamanio)
            {
                case "Pequenio":
                    return 50;
                case "Mediano":
                    return 75;
                case "Grande":
                    return 100;
            }
            return 0;
        }
        private double descuentoDependiendoCantidadDeDias(int diferenciaDeDias) 
        {
            if (diferenciaDeDias >= 7 && diferenciaDeDias <= 14)
            {
                return 0.05;
            }
            else if (diferenciaDeDias > 14)
            {
                return 0.10;
            }
            return 0;
        }
        private int CalculoPrecioDeReserva() {
            int precioPorDiaDependiendoDelTamaño = precioPorDia(Deposito.Tamanio);
            TimeSpan diferencia = FechaHasta - FechaDesde;
            int diferenciaEnCantidadDeDias = diferencia.Days;
            double descuento = descuentoDependiendoCantidadDeDias(diferenciaEnCantidadDeDias);
            var porcentajeDescuentoPromocion = Deposito.mejorPromocionHoy();
            int precioReserva;

            precioReserva = precioPorDiaDependiendoDelTamaño * diferenciaEnCantidadDeDias;

            if (Deposito.Climatizacion)
            {
                precioReserva += 20 * diferenciaEnCantidadDeDias;
            }

            double precioConDescuento = precioReserva * (1 - descuento);
            precioReserva = (int)precioConDescuento;

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
