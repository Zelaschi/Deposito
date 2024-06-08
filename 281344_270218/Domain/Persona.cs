using Domain.Exceptions;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace Domain
{
    public abstract class Persona
    {
        public int PersonaId { get; set; }

        private bool NoVacioONull(string value) {
            return value is null || value.Length == 0;
        }

        private string _nombreYApellido;
        public string NombreYApellido
        {
            get
            {
                return _nombreYApellido;
            }
            set
            {
                if (NoVacioONull(value))
                {
                    throw new CampoNoPuedeSerVacioNiNull("NombreYApellido no puede ser vacio ni null");
                }
                else if (value.Length > 100)
                {
                    throw new ArgumentException("El formato del correo electrónico no es válido.");
                }

                _nombreYApellido = value;
            }
        }

        private string _mail;
        public string Mail
        {
            get { return _mail; }
            set
            {
                string patronMailCorrecto = "^[\\w-\\.]+@([\\w-]+\\.)+[\\w-]{2,4}$";
                if (NoVacioONull(value))
                {
                    throw new CampoNoPuedeSerVacioNiNull("NombreYApellido no puede ser vacio ni null");

                }
                else if (Regex.IsMatch(value, patronMailCorrecto))
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
        public string Password
        {
            get { return _password; }
            set
            {
                string patronContraseniaCorrecta = "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[@$!%*?&])[A-Za-z\\d@$!%*?&]{8,}$";

                if (NoVacioONull(value))
                {
                    throw new CampoNoPuedeSerVacioNiNull("Password no puede ser vacio ni null");
                }
                else if (Regex.IsMatch(value, patronContraseniaCorrecta))
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
            PersonaId = id;
            NombreYApellido = nombreyApellido;
            Mail = mail;
            Password = password;
        }
        public Persona(string nombreyApellido, string mail, string password)
        {
            PersonaId = 0;
            NombreYApellido = nombreyApellido;
            Mail = mail;
            Password = password;
        }
    }
}
