namespace Domain
{
    public class Deposito
    {
        public static int UltimoID { get; set; } = 0;
        public int IdDeposito { get; set; }
        public string Area { get; set; }
        public string Tamanio { get; set; }
        public bool Climatizacion { get; set; }
        public List<Promocion> listaPromocionesQueAplicanADeposito = new List<Promocion>();

        public Deposito(string area, string tamanio, bool climatizacion)
        {
            if (!ValidarArea(area) || !ValidarTamanio(tamanio))
            {
                throw new ArgumentException("Deposito invalido");
            }
            Area = area;
            Tamanio = tamanio;
            Climatizacion = climatizacion;
            IdDeposito = ++UltimoID;
        }

        public Deposito(int idDeposito, string area, string tamanio, bool climatizacion) { }
        private bool ValidarArea(string area)
        {
            return new string[] { "A", "B", "C", "D", "E" }.Contains(area);
        }

        private bool ValidarTamanio(string tamanio)
        {
            return new string[] { "Pequenio", "Mediano", "Grande" }.Contains(tamanio);
        }

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

    }
}

