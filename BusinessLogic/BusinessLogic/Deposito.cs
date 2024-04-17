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
            if(!ValidarArea(area) || !ValidarTamanio(tamanio))
            {
                throw new ArgumentException("Deposito invalido");
            }
            Area = area;
            Tamanio = tamanio;
            Climatizacion = climatizacion;
            Id = id;
        }
        private bool ValidarArea(String area)
        {
            return new string[] {"A", "B", "C", "D", "E"}.Contains(area);
        }

        private bool ValidarTamanio(String tamanio)
        {
            return new string[] {"Pequenio", "Mediano", "Grande"}.Contains(tamanio);
        }

    }
}

