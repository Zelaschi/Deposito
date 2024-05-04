
namespace ControllerLayer
{
    public class DTOCliente
    {
        public string Mail { get; set; }

        public string NombreYApellido { get; set; }

        public string Password { get; set; }

        public DTOCliente(string aMail, string aNombreYApellido, string aPassword)
        {
            Mail = aMail;
            NombreYApellido = aNombreYApellido;
            Password = aPassword;
        }
    }
}
