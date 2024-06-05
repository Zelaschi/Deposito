namespace Domain
{
    public class Reserva
    {
        public int ReservaId { get; set; }
        public DateTime FechaDesde { get; set; }
        public DateTime FechaHasta { get; set; }
        public int DepositoId { get; set; }
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

        public int ClienteId { get; set; }
        public Cliente Cliente { get; set; }
        public int? PromocionId { get; set; }
        public Promocion? PromocionAplicada { get; set; }
        public Pago? Pago { get; set; }

        private bool ValidarFechaInicioSeaAnteriorAFechaFin(DateTime fechaDesde, DateTime fechaHasta)
        {
            return fechaDesde.CompareTo(fechaHasta) <= 0;
        }

        
        private int precioDiaDepositoPequenio = 50;
        private int precioDiaDepositoMediano = 75;
        private int precioDiaDepositoGrande = 100;
        private int precioPorDia(string tamanio) {
            switch (tamanio)
            {
                case "Pequenio":
                    return precioDiaDepositoPequenio;
                case "Mediano":
                    return precioDiaDepositoMediano;
                case "Grande":
                    return precioDiaDepositoGrande;
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
        public int CalculoPrecioDeReserva() {
            int precioPorDiaDependiendoDelTamaño = precioPorDia(Deposito.Tamanio);
            TimeSpan diferencia = FechaHasta - FechaDesde;
            int diferenciaEnCantidadDeDias = diferencia.Days;
            double descuento = descuentoDependiendoCantidadDeDias(diferenciaEnCantidadDeDias);
            int precioReserva;

            precioReserva = precioPorDiaDependiendoDelTamaño * diferenciaEnCantidadDeDias;

            if (Deposito.Climatizacion)
            {
                precioReserva += 20 * diferenciaEnCantidadDeDias;
            }

            double precioConDescuento = precioReserva * (1 - descuento);
            precioReserva = (int)precioConDescuento;

            if (PromocionAplicada != null) 
            {
                double descuentoPromocion = ((100 - PromocionAplicada.PorcentajeDescuento) * 0.01);
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
            ReservaId = 0;
            FechaDesde = fechaDesde;
            FechaHasta = fechaHasta;
            DepositoId = deposito.DepositoId;
            Deposito = deposito;
            PromocionAplicada = Deposito.mejorPromocionHoy();
            ClienteId = cliente.PersonaId;
            Cliente = cliente;
            Precio = CalculoPrecioDeReserva();
            Pago = new Pago();
        }
        //EF
        public Reserva() { }
    }
}
