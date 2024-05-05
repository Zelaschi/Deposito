

namespace ControllerLayer
{
    public class DTOAdministrador
    {
        public string Mail { get; set; }

        public string NombreYApellido { get; set; }

        public string Password { get; set; }

        public DTOAdministrador(string aNombreYApellido, string aMail, string aPassword)
        {
            Mail = aMail;
            NombreYApellido = aNombreYApellido;
            Password = aPassword;
        }
    }
}
