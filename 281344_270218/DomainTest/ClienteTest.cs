using Domain;


namespace BusinessLogicTest
{
    [TestClass]
    public class ClienteTest
    {
        //No es necesario hacer TDD de getParametro, testFormatoEmailIncorrecto y testFormatoPasswordIncorrecto
        //Porque ya estan hechos en administrador
        public Cliente cliente;
        [TestInitialize]
        public void inicializarClienteConParametrosTest() {
            var nombreYApellidoTest = "Tomas Zelaschi";
            var mailTest = "tomaszelaschi@gmail.com";
            var passwordTest = "Password1!";

            cliente = new Cliente( nombreYApellidoTest, mailTest, passwordTest);
        }
        [TestMethod]
        public void crearClienteConParametrosTest() {
            var nombreYApellidoTest = "Tomas Zelaschi";
            var mailTest = "tomaszelaschi@gmail.com";
            var passwordTest = "Password1!";

            cliente = new Cliente( nombreYApellidoTest, mailTest, passwordTest);
        }
        [TestMethod]
        public void getParametros() { 
            var nombreYApellidoEsperado = "Tomas Zelaschi";
            var mailEsperado = "tomaszelaschi@gmail.com";
            var passwordEsperada = "Password1!";

            Assert.AreEqual(cliente.NombreYApellido, nombreYApellidoEsperado);
            Assert.AreEqual(cliente.Mail, mailEsperado);
            Assert.AreEqual(cliente.Password, passwordEsperada);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void TestFormatoEmailIncorrecto()
        {
            // Direcciones de correo electrónico con formato incorrecto
            var mail1 = "invalido@example"; // Falta el dominio .com, .org, etc.
            var mail2 = "invalido@ejemplo com"; // Falta el punto entre el nombre del dominio y el dominio
            var mail3 = "invalido@ejemplo."; // Falta el dominio .com, .org, etc. después del punto
            var mail4 = "invalido@@ejemplo.com"; // Doble símbolo '@'
            var mail5 = "invalido@ejemplo,com"; // Coma en lugar de punto entre el nombre de dominio y el dominio
            var mail6 = "invalido@ejemplo..com"; // Doble punto consecutivo
            var mail7 = "invalido.ejemplo.com"; // Falta el símbolo '@' para separar el nombre del usuario del dominio
            var mail8 = "@ejemplo.com"; // Falta el nombre de usuario antes del símbolo '@'
            var mail9 = "invalido@.com"; // Falta el nombre del dominio antes del símbolo '.'
            var mail10 = "invalido@ejemplo_com"; // Guion bajo en lugar de punto entre el nombre de dominio y el dominio

            cliente.Mail = mail1;
            cliente.Mail = mail2;
            cliente.Mail = mail3;
            cliente.Mail = mail4;
            cliente.Mail = mail5;
            cliente.Mail = mail6;
            cliente.Mail = mail7;
            cliente.Mail = mail8;
            cliente.Mail = mail9;
            cliente.Mail = mail10;
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testFormatoPasswordIncorrecto()
        {
            var pasw1 = "P@ss";            // Contraseña con menos de 8 caracteres
            var pasw2 = "Passw0rd";         // Contraseña sin símbolos
            var pasw3 = "PASSWORD1@";       // Contraseña sin letras minúsculas
            var pasw4 = "password1@";       // Contraseña sin letras mayúsculas
            var pasw5 = "Password@";        // Contraseña sin dígitos
            var pasw6 = "password";         // Contraseña sin símbolos, letras minúsculas y dígitos
            var pasw7 = "PASSWORD";         // Contraseña sin símbolos, letras mayúsculas y dígitos
            var pasw8 = "12345678";         // Contraseña sin símbolos, letras minúsculas y letras mayúsculas
            var pasw9 = "abc";              // Contraseña que no cumple con ninguna regla

            cliente.Password = pasw1;
            cliente.Password = pasw2;
            cliente.Password = pasw3;
            cliente.Password = pasw4;
            cliente.Password = pasw5;
            cliente.Password = pasw6;
            cliente.Password = pasw7;
            cliente.Password = pasw8;
            cliente.Password = pasw9;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void testNombreYApellido101Caracteres()
        {
            string nombreCon101Caracteres= "Este es un string sencillo de 101 caracteres que puedes usar para tu proyecto. Este es un string sencillo de 101 caracteres que puedes usar para tu proyecto.";
            cliente.NombreYApellido = nombreCon101Caracteres;
        }

    }
}
