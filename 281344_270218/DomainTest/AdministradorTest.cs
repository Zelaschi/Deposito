using Domain;
using Newtonsoft.Json.Bson;
namespace DomainTest
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

            Assert.AreEqual(admin.PersonaId, idEsperado);
            Assert.AreEqual(admin.NombreYApellido, nombreYApellidoEsperado);
            Assert.AreEqual(admin.Mail,mailEsperado);
            Assert.AreEqual(admin.Password, passwordEsperada);
        }
       [TestMethod]
        public void testFormatoEmailIncorrecto() {
            var mailsInvalidos = new List<string> {
                "invalido@example", // Falta el dominio .com, .org, etc.
                "invalido@ejemplo com", // Falta el punto entre el nombre del dominio y el dominio
                "invalido@ejemplo.", // Falta el dominio .com, .org, etc. despu�s del punto
                "invalido@@ejemplo.com", // Doble s�mbolo '@'
                "invalido@ejemplo,com", // Coma en lugar de punto entre el nombre de dominio y el dominio
                "invalido@ejemplo..com", // Doble punto consecutivo
                "invalido.ejemplo.com", // Falta el s�mbolo '@' para separar el nombre del usuario del dominio
                "@ejemplo.com", // Falta el nombre de usuario antes del s�mbolo '@'
                "invalido@.com", // Falta el nombre del dominio antes del s�mbolo '.'
                "invalido@ejemplo_com" // Guion bajo en lugar de punto entre el nombre de dominio y el dominio
            };

            foreach (var mail in mailsInvalidos)
            {
                Assert.ThrowsException<ArgumentException>(() => admin.Mail = mail);
            }
        }
        [TestMethod]
        public void testFormatoPasswordIncorrecto()
        {
            var passwordsInvalidas = new List<string> {
                "P@ss",            // Contrase�a con menos de 8 caracteres
                "Passw0rd",         // Contrase�a sin s�mbolos
                "PASSWORD1@",       // Contrase�a sin letras min�sculas
                "password1@",       // Contrase�a sin letras may�sculas
                "Password@",        // Contrase�a sin d�gitos
                "password",         // Contrase�a sin s�mbolos, letras min�sculas y d�gitos
                "PASSWORD",         // Contrase�a sin s�mbolos, letras may�sculas y d�gitos
                "12345678",         // Contrase�a sin s�mbolos, letras min�sculas y letras may�sculas
                "abc"               // Contrase�a que no cumple con ninguna regla
            };

            foreach (var password in passwordsInvalidas)
            {
                Assert.ThrowsException<ArgumentException>(() => admin.Password = password);
            }
        }

    }
}