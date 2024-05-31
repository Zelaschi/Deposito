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
                Assert.ThrowsException<ArgumentException>(() => admin.Mail = mail);
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
                Assert.ThrowsException<ArgumentException>(() => admin.Password = password);
            }
        }

    }
}