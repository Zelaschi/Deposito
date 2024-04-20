using BusinessLogic;


namespace BusinessLogicTest
{
    [TestClass]
    internal class UsuarioTest
    {
        private Cliente cliente;
        [TestInitialize]
        public void crearClienteConParametrosTest() {
            var idTest = 0;
            var nombreTest = "Pedro";
            var apellidoTest = "Azambuja";
            var mailTest = "pedroazambuja@gmail.com";
            var passwordTest = "Password1!";

            cliente = new Cliente(idTest, nombreTest, apellidoTest, mailTest, passwordTest);
        }
    }
}
