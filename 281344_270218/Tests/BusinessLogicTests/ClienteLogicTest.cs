using Domain;
using BusinessLogic;
using Repository;
using Repository.Context;
using Repository.SQL;

namespace Tests.BusinessLogicTests
{
    [TestClass]
    public class ClienteLogicTest
    {
        private ClienteLogic? _clienteLogic;
        private ClienteRepository _clienteRepository;
        private readonly DepositoContextFactory _contextFactory = new DepositoContextEnMemoria();
        private DepositoContext _context;

        private Cliente? cliente1;
        private int idCliente1 = 1;
        private Cliente? cliente2;
        private int idCliente2 = 2;

        [TestInitialize]
        public void setUp()
        {
            _context = _contextFactory.CrearContext();
            _clienteRepository = new ClienteRepository(_context);
            _clienteLogic = new ClienteLogic(_clienteRepository);


            var nombreYApellidoTest1 = "Tomas Zelaschi";
            var mailTest1 = "tomaszelaschi@gmail.com";
            var passwordTest1 = "Password1!";

            var nombreYApellidoTest2 = "Pedro Azambuja";
            var mailTest2 = "pedroazambuja@gmail.com";
            var passwordTest2 = "Password1!";

            cliente1 = new Cliente(0, nombreYApellidoTest1, mailTest1, passwordTest1);
            cliente2 = new Cliente(0, nombreYApellidoTest2, mailTest2, passwordTest2);
        }
        [TestCleanup]
        public void borrarDB()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void AgregarClienteTest()
        {
            Cliente clienteRetorno = _clienteLogic.AgregarCliente(cliente1);

            Assert.AreEqual(1, clienteRetorno.PersonaId);
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

            Assert.AreEqual(idCliente1, clienteRetorno1.PersonaId);
            Assert.AreEqual(idCliente2, clienteRetorno2.PersonaId);
        }

        [TestMethod]
        public void ListarTodosLosCLientesTest()
        {

            Cliente clienteRetorno1 = _clienteLogic.AgregarCliente(cliente1);
            Cliente clienteRetorno2 = _clienteLogic.AgregarCliente(cliente2);

            IList<Cliente> resultClientes = _clienteLogic.listarTodosLosClientes();

            Assert.AreEqual(cliente1.NombreYApellido, resultClientes.FirstOrDefault(x => x.PersonaId == 1).NombreYApellido);
            Assert.AreEqual(cliente2.NombreYApellido, resultClientes.FirstOrDefault(x => x.PersonaId == 2).NombreYApellido);
        }
        [TestMethod]
        public void EncontrarPorIdTest()
        {
            _clienteLogic.AgregarCliente(cliente1);
            Cliente cliente1PeroPorFind = _clienteLogic.buscarClientePorId(cliente1.PersonaId);

            Assert.AreEqual(cliente1.PersonaId, cliente1PeroPorFind.PersonaId);
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
            _clienteLogic.EliminarCliente(cliente2.PersonaId);
            var clientEliminado = _clienteLogic.buscarClientePorId(cliente2.PersonaId);

            Assert.IsNull(clientEliminado);
        }

        [TestMethod]
        public void ActualizarClienteTest()
        {

            _clienteLogic.AgregarCliente(cliente2);
            string nombreActualizado = "NombreActualizado";
            string passwordActualizada = "NewPasswd1!";
            Cliente clienteActualizado = new Cliente(1, nombreActualizado, cliente2.Mail, passwordActualizada);

            Cliente clienteActualizadoRetrono = _clienteLogic.ActualizarInfoCliente(clienteActualizado);


            Assert.AreEqual(nombreActualizado, clienteActualizadoRetrono.NombreYApellido);
            Assert.AreEqual(passwordActualizada, clienteActualizadoRetrono.Password);

        }

        [TestMethod]
        public void ActualizarClienteQueNoExisteTest()
        {
            Cliente clienteQueNoExiste = new Cliente("juan", "juanMail@gmail.com", "JuanJuan1!");

            Cliente clienteNull = _clienteLogic.ActualizarInfoCliente(clienteQueNoExiste);

            Assert.IsNull(clienteNull);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]

        public void DosClientesConMismoMailTireException()
        {
            _clienteLogic.AgregarCliente(cliente1);
            _clienteLogic.AgregarCliente(cliente1);

        }

    }
}
