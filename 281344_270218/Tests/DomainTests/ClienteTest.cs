using Domain;

namespace Tests.DomainTests
{
    public class ClienteTest
    {
        private Cliente cliente;

        [TestInitialize]
        public void setUp()
        {
            var nombreYApellidoTest = "Tomas Zelaschi";
            var mailTest = "tomaszelaschi@gmail.com";
            var passwordTest = "Password1!";

            cliente = new Cliente(nombreYApellidoTest, mailTest, passwordTest);
        }

        [TestMethod]
        public void crearClienteConParametrosTest()
        {
            var nombreYApellidoTest = "Pablo Zelaschi";
            var mailTest = "pablozelaschi@gmail.com";
            var passwordTest = "Password1!";

            Cliente cliente = new Cliente(nombreYApellidoTest, mailTest, passwordTest);
        }

        [TestMethod]
        public void getParametros()
        {
            var nombreYApellidoEsperado = "Pablo Zelaschi";
            var mailEsperado = "pablozelaschi@gmail.com";
            var passwordEsperada = "Password1!";

            Cliente cliente = new Cliente(nombreYApellidoEsperado, mailEsperado, passwordEsperada);

            Assert.AreEqual(cliente.NombreYApellido, nombreYApellidoEsperado);
            Assert.AreEqual(cliente.Mail, mailEsperado);
            Assert.AreEqual(cliente.Password, passwordEsperada);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AgregarClienteConNombreYApellidoVacioTest()
        {
            Cliente cliente = new Cliente("", "totozelaschi@gmail.com", "Password1!");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AgregarClienteConMailVacioTest()
        {
            Cliente cliente = new Cliente("z", "", "Password1!");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AgregarClienteConPasswordVaciaTest()
        {
            Cliente cliente = new Cliente("z", "totozelaschi@gmail.com", "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AgregarClienteNombreYApellidoNullTest()
        {
            Cliente cliente = new Cliente(null, "totozelaschi@gmail.com", "Passworod1!");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AgregarClienteMailNullTest()
        {
            Cliente cliente = new Cliente("z", null, "Password1!S");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AgregarClientePasswordNullTest()
        {
            Cliente cliente = new Cliente("z", "totozelaschi@gmail.com", null);
        }

        [TestMethod]
        public void testFormatoEmailIncorrecto()
        {
            var mailsInvalidos = new List<string> {
                "invalido@example", // Falta el dominio .com, .org, etc.
                "invalido@ejemplo com", // Falta el punto entre el nombre del dominio y el dominio
                "invalido@ejemplo.", // Falta el dominio .com, .org, etc. después del punto
                "invalido@@ejemplo.com", // Doble símbolo '@'
                "invalido@ejemplo,com", // Coma en lugar de punto entre el nombre de dominio y el dominio
                "invalido@ejemplo..com", // Doble punto consecutivo
                "invalido.ejemplo.com", // Falta el símbolo '@' para separar el nombre del usuario del dominio
                "@ejemplo.com", // Falta el nombre de usuario antes del símbolo '@'
                "invalido@.com", // Falta el nombre del dominio antes del símbolo '.'
                "invalido@ejemplo_com" // Guion bajo en lugar de punto entre el nombre de dominio y el dominio
            };

            foreach (var mail in mailsInvalidos)
            {
                Assert.ThrowsException<ArgumentException>(() => cliente.Mail = mail);
            }
        }
        [TestMethod]
        public void testFormatoPasswordIncorrecto()
        {
            var passwordsInvalidas = new List<string> {
                "P@ss",            // Contraseña con menos de 8 caracteres
                "Passw0rd",         // Contraseña sin símbolos
                "PASSWORD1@",       // Contraseña sin letras minúsculas
                "password1@",       // Contraseña sin letras mayúsculas
                "Password@",        // Contraseña sin dígitos
                "password",         // Contraseña sin símbolos, letras minúsculas y dígitos
                "PASSWORD",         // Contraseña sin símbolos, letras mayúsculas y dígitos
                "12345678",         // Contraseña sin símbolos, letras minúsculas y letras mayúsculas
                "abc"               // Contraseña que no cumple con ninguna regla
            };

            foreach (var password in passwordsInvalidas)
            {
                Assert.ThrowsException<ArgumentException>(() => cliente.Password = password);
            }
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NombreYApellido101CaracteresTest()
        {
            string nombreCon101Caracteres = "Este es un string sencillo de 101 caracteres que puedes usar para tu proyecto. Este es un string sencillo de 101 caracteres que puedes usar para tu proyecto.";
            cliente.NombreYApellido = nombreCon101Caracteres;
        }

        [TestMethod]
        public void DosClientesIdDistintoTest()
        {
            var nombreYApellidoTest1 = "Tomas Zelaschi";
            var mailTest1 = "tomaszelaschi@gmail.com";
            var passwordTest1 = "Password1!";

            Cliente cliente1 = new Cliente(nombreYApellidoTest1, mailTest1, passwordTest1);

            var nombreYApellidoTest2 = "Tomas Zelaschi";
            var mailTest2 = "tomaszelaschi@gmail.com";
            var passwordTest2 = "Password1!";

            Cliente cliente2 = new Cliente(nombreYApellidoTest2, mailTest2, passwordTest2);

            Assert.AreNotEqual(cliente1.PersonaId, cliente2.PersonaId);
        }
    }
}
