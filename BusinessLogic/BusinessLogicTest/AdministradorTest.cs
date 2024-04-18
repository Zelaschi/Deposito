using BusinessLogic;
using Newtonsoft.Json.Bson;
namespace BusinessLogicTest
{
    [TestClass]
    public class AdministradorTest
    {
        private Administrador admin;

        [TestInitialize]
        public void crearAdministradorConParametrosYGet() {
            var idTest = 0;
            var nombreTest = "Pedro";
            var apellidoTest = "Azambuja";
            var mailTest = "pedroazambuja@gmail.com";
            var passwordTest = "password";

            admin = new Administrador(idTest, nombreTest, apellidoTest, mailTest, passwordTest);
        }
        [TestMethod]
        public void getParametros() {
            var idEsperado = 0;
            var nombreEsperado = "Pedro";
            var apellidoEsperado = "Azambuja";
            var mailEsperado = "pedroazambuja@gmail.com";
            var passwordEsperada = "password";

            Assert.AreEqual(admin.Id, idEsperado);
            Assert.AreEqual(admin.Nombre, nombreEsperado);
            Assert.AreEqual(admin.Apellido, apellidoEsperado);
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
    }
}