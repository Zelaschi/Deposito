using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Domain
{
    public abstract class Persona
    {
        private static int contadorID = 1;
        public int IdPersona { get; set; }

        private bool noVacioONull(string value) {
            return value is null || value.Length == 0;
        }

        private string _nombreYApellido;
        public string? NombreYApellido
        {
            get
            {
                return _nombreYApellido;
            }
            set
            {
                if (noVacioONull(value))
                {
                    throw new ArgumentNullException("NombreYApellido no puede ser vacio ni null");
                }
                else if (value.Length > 100)
                {
                    throw new ArgumentException("El formato del correo electrónico no es válido.");
                }

                _nombreYApellido = value;
            }
        }

        private string _mail;
        public string? Mail
        {
            get { return _mail; }
            set
            {
                string pattern = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";
                if (noVacioONull(value))
                {
                    throw new ArgumentNullException("NombreYApellido no puede ser vacio ni null");

                }
                else if (Regex.IsMatch(value, pattern))
                {
                    _mail = value;
                }
                else
                {
                    throw new ArgumentException("El formato del correo electrónico no es válido.");
                }
            }
        }

        private string _password;
        public string? Password
        {
            get { return _password; }
            set
            {
                string pattern = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$";

                if (noVacioONull(value))
                {
                    throw new ArgumentNullException("Password no puede ser vacio ni null");
                }
                else if (Regex.IsMatch(value, pattern))
                {
                    _password = value;
                }
                else
                {
                    throw new ArgumentException("El formato de la contraseña no es válido.");
                }
            }

            
        }

        public Persona(int id, string nombreyApellido, string mail, string password)
        {
            IdPersona = id;
            NombreYApellido = nombreyApellido;
            Mail = mail;
            Password = password;
        }
        public Persona(string nombreyApellido, string mail, string password)
        {
            IdPersona = contadorID;
            contadorID++;
            NombreYApellido = nombreyApellido;
            Mail = mail;
            Password = password;
        }

    }
}
