﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class Cliente : Persona
    {
        public Cliente(int id, string nombreYApellido, string mail, string password) : base(id, nombreYApellido, mail, password) { }
    }
}
