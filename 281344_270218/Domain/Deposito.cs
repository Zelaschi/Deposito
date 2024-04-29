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

        public static void ResetId()
        {
            UltimoID = 0;
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

        public void EliminarPromocionDeDeposito(Promocion promoParametro)
        {
            if (!listaPromocionesQueAplicanADeposito.Contains(promoParametro))
            {
                throw new InvalidOperationException("El elemento no existe en la lista");
            }
            listaPromocionesQueAplicanADeposito.Remove(promoParametro);
        }

        public Deposito(string area, string tamanio, bool climatizacion)
        {
            Area = area;
            Tamanio = tamanio;
            Climatizacion = climatizacion;
            IdDeposito = ++UltimoID;
        }

        public Deposito(int idDeposito, string area, string tamanio, bool climatizacion)
        {
            Area = area;
            Tamanio = tamanio;
            Climatizacion = climatizacion;
            IdDeposito = idDeposito;
        }

    }
}

