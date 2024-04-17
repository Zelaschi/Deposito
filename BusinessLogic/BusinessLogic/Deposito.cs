namespace BusinessLogic
{   
    public class Deposito
    {
        public String Area { get; set; }
        public String Tamanio { get; set; }
        public String Climatizacion { get; set; }

        public Deposito(string area, string tamanio, string climatizacion)
        {
            Area = area;
            Tamanio = tamanio;
            Climatizacion = climatizacion;
        }
    }
}

