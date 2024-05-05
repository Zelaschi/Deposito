using Domain;
using BusinessLogic;
using Repository;
using ControllerLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;


namespace ControllerLayerTest
{
    [TestClass]
    public class ControllerTest
    {
        private Controller _controller;

        private ClienteLogic _clienteLogic;
        private IRepository<Cliente> _clienteRepository;
        private AdministradorLogic _administradorLogic;
        private IRepository<Administrador> _administradorRespository;
        private DepositoLogic _depositoLogic;
        private IRepository<Deposito> _depositoRespository;
        private PromocionLogic _promocionLogic;
        private IRepository<Promocion> _promocionRespository;
        private ReservaLogic _reservaLogic;
        private IRepository<Reserva> _reservaRespository;

        private const string emailTest = "totozelaschi@gmail.com";
        private const string pwdTest = "Password1!";
        private const string nombreYApellidoTest = "NombreApellido";

        private DTOCliente aDTOCliente;
        private DTOAdministrador aDTOadministrador;
        private DTODeposito aDTOdeposito;
        private DTOPromocion aDTOpromocion;
        private DTOReserva aDTOreserva;

        [TestInitialize]
        public void setUp() { 
            _clienteRepository = new ClienteMemoryRepository();
            _clienteLogic = new ClienteLogic(_clienteRepository);
            _administradorRespository = new AdministradorMemoryRepository();
            _administradorLogic = new AdministradorLogic(_administradorRespository);
            _depositoRespository = new DepositoMemoryRepository();
            _depositoLogic = new DepositoLogic(_depositoRespository);
            _promocionRespository = new PromocionMemoryRepository();
            _promocionLogic = new PromocionLogic(_promocionRespository);
            _reservaRespository = new ReservaMemoryRepository();
            _reservaLogic = new ReservaLogic(_reservaRespository);

            _controller = new Controller(_administradorLogic, _clienteLogic, _depositoLogic, _promocionLogic, _reservaLogic);

            aDTOCliente = new DTOCliente(nombreYApellidoTest,emailTest, pwdTest);
            aDTOadministrador = new DTOAdministrador(nombreYApellidoTest, emailTest, pwdTest);
            aDTOdeposito = new DTODeposito("A", "Grande", true);
            aDTOpromocion = new DTOPromocion("promo", 20, DateTime.Today, DateTime.Today.AddDays(10));
            aDTOreserva = new DTOReserva(DateTime.Today, DateTime.Today.AddDays(15), aDTOdeposito, aDTOCliente);

        }

        [TestMethod]
        public void RegistrarClienteTest()
        {
            _controller.RegistrarCliente(aDTOCliente);

            Assert.AreEqual(aDTOCliente.Mail, _clienteLogic.buscarClientePorMail(aDTOCliente.Mail).Mail);
            Assert.AreEqual(aDTOCliente.NombreYApellido, _clienteLogic.buscarClientePorMail(aDTOCliente.Mail).NombreYApellido);
            Assert.AreEqual(aDTOCliente.Password, _clienteLogic.buscarClientePorMail(aDTOCliente.Mail).Password);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RegistrarClienteConMailInvalidoTest() {
            aDTOCliente.Mail = "mailinvalido";

            _controller.RegistrarCliente(aDTOCliente);

        }
    }
}