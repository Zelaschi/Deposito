using BusinessLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogicTest
{
    [TestClass]
    internal class UsuarioTest
    {
        private Cliente cliente;
        [TestInitialize]
        public void crearUsuarioConParametrosTest() {
            var idTest = 0;
            var nombreTest = "Pedro";
            var apellidoTest = "Azambuja";
            var mailTest = "pedroazambuja@gmail.com";
            var passwordTest = "Password1!";

            cliente = new Cliente(idTest, nombreTest, apellidoTest, mailTest, passwordTest);
        }
    }
}
