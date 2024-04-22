using BusinessLogic;
using Newtonsoft.Json.Bson;
namespace BusinessLogicTest
{
    [TestClass]
    public class AdministradorTest
    {
        private Administrador admin;

        [TestInitialize]
        public void crearAdministradorConParametros() {
            var idTest = 0;
            var nombreYApellidoTest = "Pedro Azambuja";
            var mailTest = "pedroazambuja@gmail.com";
            var passwordTest = "Password1!";

            admin = new Administrador(idTest, nombreYApellidoTest, mailTest, passwordTest);
        }
        [TestMethod]
        public void getParametros() {
            var idEsperado = 0;
            var nombreYApellidoEsperado = "Pedro Azambuja";
            var mailEsperado = "pedroazambuja@gmail.com";
            var passwordEsperada = "Password1!";

            Assert.AreEqual(admin.IdPersona, idEsperado);
            Assert.AreEqual(admin.NombreYApellido, nombreYApellidoEsperado);
            Assert.AreEqual(admin.Mail,mailEsperado);
            Assert.AreEqual(admin.Password, passwordEsperada);
        }
       [TestMethod]
       [ExpectedException(typeof(ArgumentException))]
        public void testFormatoEmailIncorrecto() {
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

            admin.Mail = mail1;
            admin.Mail = mail2;
            admin.Mail = mail3;
            admin.Mail = mail4;
            admin.Mail = mail5;
            admin.Mail = mail6;
            admin.Mail = mail7;
            admin.Mail = mail8;
            admin.Mail = mail9;
            admin.Mail = mail10;
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
           
            admin.Password = pasw1;
            admin.Password = pasw2;
            admin.Password = pasw3;
            admin.Password = pasw4;
            admin.Password = pasw5;
            admin.Password = pasw6;
            admin.Password = pasw7;
            admin.Password = pasw8;
            admin.Password = pasw9;
        }
      
    }
}