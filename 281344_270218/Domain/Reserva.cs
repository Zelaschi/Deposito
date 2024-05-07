namespace Domain
{
    public class Reserva
    {
        public static int UltimoID { get; set; } = 0;
        public int IdReserva { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public Deposito Deposito { get; set; }
        public int Precio { get; set; }
        public string Estado { get; set; } = "Pendiente";

        private string _justificacionRechazo = "en caso de ser rechazada aca esta la justificacion";
        public string? JustificacionRechazo
        {
            get
            {
                return _justificacionRechazo;
            }
            set
            {
                if (value.Length > 300)
                {
                    throw new ArgumentException("El largo de la justificacion debe ser menor a 300 caracteres.");
                }
                _justificacionRechazo = value;
            }
        }
        public Cliente Cliente { get; set; }
        public Promocion PromocionAplicada { get; set; }

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
            Precio = CalculoPrecioDeReserva();
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
            Precio = CalculoPrecioDeReserva();
            Cliente = cliente;
        }
        public int CalculoPrecioDeReserva() {
            int precioPorDiaDependiendoDelTamaño = 0;
            TimeSpan diferencia = FechaHasta - FechaDesde;
            int cantidadDeDias = diferencia.Days;
            double descuento = 0;
            var porcentajeDescuentoPromocion = Deposito.mejorPromocionHoy();
            int precioReserva;

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

            precioReserva = precioPorDiaDependiendoDelTamaño * cantidadDeDias;

            if (Deposito.Climatizacion)
            {
                precioReserva += 20 * cantidadDeDias;
            }

            double precioConDescuento = precioReserva * (1 - descuento);
            precioReserva = (int)precioConDescuento;

            if (porcentajeDescuentoPromocion != null) 
            {
                PromocionAplicada = porcentajeDescuentoPromocion;
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
