using Domain;
using BusinessLogic;
using Repository;

namespace BusinessLogicTest
{
    [TestClass]
    public class ClienteLogicTest
    {
        private  ClienteLogic _clienteLogic;
        private IRepository<Cliente> _clienteRepository;
        private Cliente cliente1;
        private int idCliente1 = 1;
        private Cliente cliente2;
        private int idCliente2 = 2;

        [TestInitialize]
        public void setUp() {
            _clienteRepository = new ClienteMemoryRepository();
            _clienteLogic = new ClienteLogic(_clienteRepository);

            
            var nombreYApellidoTest1 = "Tomas Zelaschi";
            var mailTest1 = "tomaszelaschi@gmail.com";
            var passwordTest1 = "Password1!";

            var nombreYApellidoTest2 = "Pedro Azambuja";
            var mailTest2 = "tomaszelaschi@gmail.com";
            var passwordTest2 = "Password1!";

            cliente1 = new Cliente(idCliente1, nombreYApellidoTest1, mailTest1, passwordTest1);
            cliente2 = new Cliente(idCliente2, nombreYApellidoTest2, mailTest2, passwordTest2);
        }
        [TestMethod]
        public void AgregarClienteTest()
        {
            Cliente clienteRetorno = _clienteLogic.AddCliente(cliente1);

            Assert.AreEqual(1, clienteRetorno.IdPersona);
            Assert.AreEqual(cliente1.NombreYApellido, clienteRetorno.NombreYApellido);
            Assert.AreEqual(cliente2.Mail, clienteRetorno.Mail);
            Assert.AreEqual(cliente2.Password, clienteRetorno.Password);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AgregarDosClientesMismoMailTest() { 

            _clienteLogic.AddCliente(cliente1);
            _clienteLogic.AddCliente(cliente2);
        }

        [TestMethod]
        public void AgregarDosClientesTest()
        {

            Cliente clienteRetorno1 = _clienteLogic.AddCliente(cliente1);
            Cliente clienteRetorno2 = _clienteLogic.AddCliente(cliente2);

            Assert.AreEqual(idCliente1, clienteRetorno1.IdPersona);
            Assert.AreEqual(idCliente2 , clienteRetorno2.IdPersona);
        }

        [TestMethod]
        public void ListarTodosLosCLientesTest()
        {

            Cliente clienteRetorno1 = _clienteLogic.AddCliente(cliente1);
            Cliente clienteRetorno2 = _clienteLogic.AddCliente(cliente2);

            IList<Cliente> resultClientes = _clienteLogic.GetAll();

            Assert.AreEqual(cliente1.NombreYApellido, resultClientes.FirstOrDefault(x => x.IdPersona == 1).NombreYApellido);
            Assert.AreEqual(cliente2.NombreYApellido, resultClientes.FirstOrDefault(x => x.IdPersona == 2).NombreYApellido);

        }


    }
}
