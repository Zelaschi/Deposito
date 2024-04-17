namespace BusinessLogic
{   
    public class Deposito
    {
        public int Id { get; set; }
        public String Area { get; set; }
        public String Tamanio { get; set; }
        public bool Climatizacion { get; set; }

        public Deposito(string area, string tamanio, bool climatizacion, int id)
        {
            Area = area;
            Tamanio = tamanio;
            Climatizacion = climatizacion;
            Id = id;
        }

    }
}

