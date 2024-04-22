namespace BusinessLogic
{   
    public class Deposito
    {
        public static int UltimoID { get; set; } = 0;
        public int Id { get; set; }
        public String Area { get; set; }
        public String Tamanio { get; set; }
        public bool Climatizacion { get; set; }
        public List<Promocion> listaPromocionesQueAplicanADeposito = new List<Promocion>();

        public Deposito(string area, string tamanio, bool climatizacion)
        {
            if(!ValidarArea(area) || !ValidarTamanio(tamanio))
            {
                throw new ArgumentException("Deposito invalido");
            }
            Area = area;
            Tamanio = tamanio;
            Climatizacion = climatizacion;
            Id = ++UltimoID;
        }
        private bool ValidarArea(String area)
        {
            return new string[] {"A", "B", "C", "D", "E"}.Contains(area);
        }

        private bool ValidarTamanio(String tamanio)
        {
            return new string[] {"Pequenio", "Mediano", "Grande"}.Contains(tamanio);
        }

        public static void ResetId()
        {
            UltimoID = 0;
        }

        public Promocion AgregarPromocionADeposito(Promocion promoParametro)
        {

            listaPromocionesQueAplicanADeposito.Add(promoParametro);
            return promoParametro;
        }
    }
}

