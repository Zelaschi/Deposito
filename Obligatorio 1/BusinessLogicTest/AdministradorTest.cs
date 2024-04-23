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
            // Direcciones de correo electr�nico con formato incorrecto
            var mail1 = "invalido@example"; // Falta el dominio .com, .org, etc.
            var mail2 = "invalido@ejemplo com"; // Falta el punto entre el nombre del dominio y el dominio
            var mail3 = "invalido@ejemplo."; // Falta el dominio .com, .org, etc. despu�s del punto
            var mail4 = "invalido@@ejemplo.com"; // Doble s�mbolo '@'
            var mail5 = "invalido@ejemplo,com"; // Coma en lugar de punto entre el nombre de dominio y el dominio
            var mail6 = "invalido@ejemplo..com"; // Doble punto consecutivo
            var mail7 = "invalido.ejemplo.com"; // Falta el s�mbolo '@' para separar el nombre del usuario del dominio
            var mail8 = "@ejemplo.com"; // Falta el nombre de usuario antes del s�mbolo '@'
            var mail9 = "invalido@.com"; // Falta el nombre del dominio antes del s�mbolo '.'
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
            var pasw1 = "P@ss";            // Contrase�a con menos de 8 caracteres
            var pasw2 = "Passw0rd";         // Contrase�a sin s�mbolos
            var pasw3 = "PASSWORD1@";       // Contrase�a sin letras min�sculas
            var pasw4 = "password1@";       // Contrase�a sin letras may�sculas
            var pasw5 = "Password@";        // Contrase�a sin d�gitos
            var pasw6 = "password";         // Contrase�a sin s�mbolos, letras min�sculas y d�gitos
            var pasw7 = "PASSWORD";         // Contrase�a sin s�mbolos, letras may�sculas y d�gitos
            var pasw8 = "12345678";         // Contrase�a sin s�mbolos, letras min�sculas y letras may�sculas
            var pasw9 = "abc";              // Contrase�a que no cumple con ninguna regla
           
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