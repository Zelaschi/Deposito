using Domain;
using BusinessLogic;
using Repository;

namespace BusinessLogicTest
{
    [TestClass]
    public class ClienteLogicTest
    {
        public ClienteLogic _clienteLogic;
        private IRepository<Cliente> _clienteRepository;

        [TestInitialize]
        public void setUp() {
            _clienteRepository = new ClienteMemoryRepository();
            _clienteLogic = new ClienteLogic(_clienteRepository);
        }
        [TestMethod]
        public void AgregarClienteTest()
        {
            var idTest = 1;
            var nombreYApellidoTest = "Tomas Zelaschi";
            var mailTest = "tomaszelaschi@gmail.com";
            var passwordTest = "Password1!";

            Cliente cliente = new Cliente(idTest, nombreYApellidoTest, mailTest, passwordTest);

            Cliente clienteRetorno = _clienteLogic.AddMovie(cliente);

            Assert.AreEqual(1, clienteRetorno.IdPersona);
            Assert.AreEqual(cliente.NombreYApellido, clienteRetorno.NombreYApellido);
            Assert.AreEqual(cliente.Mail, clienteRetorno.Mail);
            Assert.AreEqual(cliente.Password, clienteRetorno.Password);
        }

    }
}
