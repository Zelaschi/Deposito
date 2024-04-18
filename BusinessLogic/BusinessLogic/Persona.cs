using System.ComponentModel;

namespace BusinessLogic
{
    public abstract class Persona
    {
        public int Id { get; set; }
        public string Nombre { get; set; }
        public string Apellido { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }

        public Persona(int id, string nombre, string apellido, string mail, string password)
        {
            Id = id;
            Nombre = nombre;
            Apellido = apellido;
            Mail = mail;
            Password = password;
        }
    }
}
