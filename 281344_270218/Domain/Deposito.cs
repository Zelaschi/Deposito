namespace Domain
{
    public class Deposito
    {
        public IList<DepositoPromocion> DepositoPromocions { get; set; }
        public IList<Reserva> Reservas { get; set; }

        public int DepositoId { get; set; }

        private string _area;
        public string? Area
        {
            get
            {
                return _area;
            }
            set
            {
                string[] areasPosibles = { "A", "B", "C", "D", "E" };
                if (areasPosibles.Contains(value))
                {
                    _area = value;
                }
                else
                {
                    throw new ArgumentException("El area tiene que estar dentro de los valores validos");
                }
            }
        }
        private string _tamanio;
        public string? Tamanio
        {
            get
            {
                return _tamanio;
            }
            set
            {
                string[] tamaniosPosibles = { "Pequenio", "Mediano", "Grande" };
                if (tamaniosPosibles.Contains(value))
                {
                    _tamanio = value;
                }
                else
                {
                    throw new ArgumentException("El tamanio tiene que estar dentro de los valores validos");
                }
            }
        }
        public bool Climatizacion { get; set; }

        private List<Promocion> listaPromocionesQueAplicanADeposito = new List<Promocion>();


        public Deposito(string area, string tamanio, bool climatizacion)
        {
            DepositoId = 0;
            Area = area;
            Tamanio = tamanio;
            Climatizacion = climatizacion;
        }

        
        public Promocion AgregarPromocionADeposito(Promocion promoParametro)
        {
            if (listaPromocionesQueAplicanADeposito.Contains(promoParametro))
            {
                throw new InvalidOperationException("El elemento ya existe en la lista.");
            }

            listaPromocionesQueAplicanADeposito.Add(promoParametro);
            return promoParametro;
        }
        public Promocion? mejorPromocionHoy() {
            int mejorDescuento = 0;
            Promocion mejorPromocion = null;

            foreach (var promocion in listaPromocionesQueAplicanADeposito) 
            {
                if (promocion.PorcentajeDescuento > mejorDescuento) {
                    mejorDescuento = promocion.PorcentajeDescuento;
                    mejorPromocion = promocion;
                }
            }
            return mejorPromocion;
        }


    }
}

