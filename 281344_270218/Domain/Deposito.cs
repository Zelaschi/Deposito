namespace Domain
{
    public class Deposito
    {
        public static int UltimoID { get; set; } = 0;
        public int IdDeposito { get; set; }
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
        public List<Promocion> listaPromocionesQueAplicanADeposito = new List<Promocion>();


        public Deposito(string area, string tamanio, bool climatizacion)
        {
            Area = area;
            Tamanio = tamanio;
            Climatizacion = climatizacion;
            IdDeposito = ++UltimoID;
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



        private IList<Promocion> promocionesHoy() 
        {
            List<Promocion> promosHoy = new List<Promocion>();
            foreach (var promocion in listaPromocionesQueAplicanADeposito)
            {
                if (promocion.FechaInicio.CompareTo(DateTime.Now) <= 0 && promocion.FechaFin.CompareTo(DateTime.Now) > 0)
                {
                    promosHoy.Add(promocion);
                }
            }
            return promosHoy;
        }
        public Promocion? mejorPromocionHoy() {
            IList<Promocion> promocionesHoy = this.promocionesHoy();
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

