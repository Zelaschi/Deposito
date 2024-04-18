using BusinessLogic;
namespace BusinessLogicTest
{
    [TestClass]
    public class AdministradorTest
    {
        private Administrador admin;


        [TestMethod]
        public void crearAdministrador()
        {
            Administrador admin = new Administrador();
        }
        [TestMethod]
        public void crearAdministradorConParametros() {
            var idTest = 0;
            var nombreTest = "Pedro";
            var apellidoTest = "Azambuja";
            var mailTest = "pedroazambuja@gmail.com";
            var passwordTest = "password";

            Administrador admin = new Administrador(idTest, nombreTest, apellidoTest, mailTest, passwordTest);
        }
    }
}