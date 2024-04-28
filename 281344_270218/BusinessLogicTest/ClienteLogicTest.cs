using Domain;
using BusinessLogic;
using Repository;

namespace BusinessLogicTest
{
    [TestClass]
    public class ClienteLogicTest
    {
        private ClienteLogic? _clienteLogic;
        private IRepository<Cliente>? _clienteRepository;
        private Cliente? cliente1;
        private int idCliente1 = 1;
        private Cliente? cliente2;
        private int idCliente2 = 2;

        [TestInitialize]
        public void setUp()
        {
            _clienteRepository = new ClienteMemoryRepository();
            _clienteLogic = new ClienteLogic(_clienteRepository);


            var nombreYApellidoTest1 = "Tomas Zelaschi";
            var mailTest1 = "tomaszelaschi@gmail.com";
            var passwordTest1 = "Password1!";

            var nombreYApellidoTest2 = "Pedro Azambuja";
            var mailTest2 = "pedroazambuja@gmail.com";
            var passwordTest2 = "Password1!";

            cliente1 = new Cliente(idCliente1, nombreYApellidoTest1, mailTest1, passwordTest1);
            cliente2 = new Cliente(idCliente2, nombreYApellidoTest2, mailTest2, passwordTest2);
        }

        [TestMethod]
        public void AgregarClienteTest()
        {
            Cliente clienteRetorno = _clienteLogic.AgregarCliente(cliente1);

            Assert.AreEqual(1, clienteRetorno.IdPersona);
            Assert.AreEqual(cliente1.NombreYApellido, clienteRetorno.NombreYApellido);
            Assert.AreEqual(cliente1.Mail, clienteRetorno.Mail);
            Assert.AreEqual(cliente1.Password, clienteRetorno.Password);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AgregarDosClientesMismoMailTest()
        {

            _clienteLogic.AgregarCliente(cliente1);
            _clienteLogic.AgregarCliente(cliente1);
        }

        [TestMethod]
        public void AgregarDosClientesTest()
        {

            Cliente clienteRetorno1 = _clienteLogic.AgregarCliente(cliente1);
            Cliente clienteRetorno2 = _clienteLogic.AgregarCliente(cliente2);

            Assert.AreEqual(idCliente1, clienteRetorno1.IdPersona);
            Assert.AreEqual(idCliente2, clienteRetorno2.IdPersona);
        }

        [TestMethod]
        public void ListarTodosLosCLientesTest()
        {

            Cliente clienteRetorno1 = _clienteLogic.AgregarCliente(cliente1);
            Cliente clienteRetorno2 = _clienteLogic.AgregarCliente(cliente2);

            IList<Cliente> resultClientes = _clienteLogic.GetAll();

            Assert.AreEqual(cliente1.NombreYApellido, resultClientes.FirstOrDefault(x => x.IdPersona == 1).NombreYApellido);
            Assert.AreEqual(cliente2.NombreYApellido, resultClientes.FirstOrDefault(x => x.IdPersona == 2).NombreYApellido);
        }
        [TestMethod]
        public void EncontrarPorIdTest()
        {
            _clienteLogic.AgregarCliente(cliente1);
            Cliente cliente1PeroPorFind = _clienteLogic.buscarClientePorId(cliente1.IdPersona);

            Assert.AreEqual(cliente1.IdPersona, cliente1PeroPorFind.IdPersona);
        }
        [TestMethod]
        public void EncontrarPorMailTest()
        {
            _clienteLogic.AgregarCliente(cliente1);
            Cliente cliente1PeroPorFind = _clienteLogic.buscarClientePorMail(cliente1.Mail);

            Assert.AreEqual(cliente1.Mail, cliente1PeroPorFind.Mail);
        }

        [TestMethod]
        public void BuscarClientePorIdYMailQueNoExisteYDevuelvaNullTest()
        {
            var clienteNoEncontradoPorId = _clienteLogic.buscarClientePorId(4);
            var clienteNoEncontradoPorMail = _clienteLogic.buscarClientePorMail("tz@gmail.com");

            Assert.IsNull(clienteNoEncontradoPorId);
            Assert.IsNull(clienteNoEncontradoPorMail);
        }

        [TestMethod]
        public void EliminarClienteTest()
        {
            _clienteLogic.AgregarCliente(cliente2);
            _clienteLogic.EliminarCliente(cliente2.IdPersona);
            var clientEliminado = _clienteLogic.buscarClientePorId(cliente2.IdPersona);

            Assert.IsNull(clientEliminado);
        }

        [TestMethod]
        public void ActualizarClienteTest()
        {

            _clienteLogic.AgregarCliente(cliente2);
            string nombreActualizado = "NombreActualizado";
            string mailActualizado = "mailactualizado@gmail.com";
            string passwordActualizada = "NewPasswd1!";
            Cliente clienteActualizado = new Cliente(2, nombreActualizado, mailActualizado, passwordActualizada);

            Cliente clienteActualizadoRetrono = _clienteLogic.ActualizarInfoCliente(clienteActualizado);


            Assert.AreEqual(nombreActualizado, clienteActualizadoRetrono.NombreYApellido);
            Assert.AreEqual(mailActualizado, clienteActualizadoRetrono.Mail);
            Assert.AreEqual(passwordActualizada, clienteActualizadoRetrono.Password);

        }

        [TestMethod]
        public void ActualizarClienteQueNoExisteTest()
        {
            Cliente clienteQueNoExiste = new Cliente("juan", "juanMail@gmail.com", "JuanJuan1!");

            Cliente clienteNull = _clienteLogic.ActualizarInfoCliente(clienteQueNoExiste);

            Assert.IsNull(clienteNull);
        }

    }
}
