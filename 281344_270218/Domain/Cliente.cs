using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain
{
    public class Cliente : Persona
    {
        public Cliente( string nombreYApellido, string mail, string password) : base( nombreYApellido, mail, password) { }

        public Cliente(int id, string nombreYApellido, string mail, string password) : base(id, nombreYApellido, mail, password) { }
    }
}
