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
    }
}