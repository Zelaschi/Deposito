using System.ComponentModel;
using System.Text.RegularExpressions;

namespace BusinessLogic
{
    public abstract class Persona
    {
        private static int contadorID = 0;
        public int IdPersona { get; set; }
        public string NombreYApellido { get; set; }

        private string _mail;
        public string Mail { 
            get { return _mail; } 
            set{
                string pattern = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";
                if (Regex.IsMatch(value, pattern)){
                    _mail = value;
                }
                else {
                    throw new ArgumentException("El formato del correo electrónico no es válido.");
                }
            }
        }

        private string _password;
        public string Password { 
            get { return _password; } 
            set {
                string pattern = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$";
                if (Regex.IsMatch(value, pattern)) {
                    _password = value;
                }
                else {
                     throw new ArgumentException("El formato de la password no es válido.");
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
    }
}
