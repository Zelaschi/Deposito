
namespace ControllerLayer
{
    public class DTOCliente
    {
        public string Mail { get; set; }

        public string NombreYApellido { get; set; }

        public string Password { get; set; }

        public DTOCliente(string aNombreYApellido, string aMail, string aPassword)
        {
            Mail = aMail;
            NombreYApellido = aNombreYApellido;
            Password = aPassword;
        }
    }
}
