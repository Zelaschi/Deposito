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

            Cliente cliente = new Cliente( nombreYApellidoTest, mailTest, passwordTest);

            Cliente clienteRetorno = _clienteLogic.AddCliente(cliente);

            Assert.AreEqual(1, clienteRetorno.IdPersona);
            Assert.AreEqual(cliente.NombreYApellido, clienteRetorno.NombreYApellido);
            Assert.AreEqual(cliente.Mail, clienteRetorno.Mail);
            Assert.AreEqual(cliente.Password, clienteRetorno.Password);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AgregarDosClientesMismoMailTest() { 
        
            var nombreYApellidoTest1 = "Tomas Zelaschi";
            var mailTest1 = "tomaszelaschi@gmail.com";
            var passwordTest1 = "Password1!";

            var nombreYApellidoTest2 = "Pedro Azambuja";
            var mailTest2 = "tomaszelaschi@gmail.com";
            var passwordTest2 = "Password1!";

            Cliente cliente1 = new Cliente(nombreYApellidoTest1, mailTest1, passwordTest1 );
            Cliente cliente2 = new Cliente(nombreYApellidoTest2, mailTest2, passwordTest2);

            _clienteLogic.AddCliente(cliente1);
            _clienteLogic.AddCliente(cliente2);
        }

    }
}
