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
        private DTOAdministrador aDTOAdministrador;
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
            aDTOAdministrador = new DTOAdministrador(nombreYApellidoTest, emailTest, pwdTest);
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

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RegistrarClientePasswordIncorrectaTest() { 
            aDTOCliente.Password = "password";

            _controller.RegistrarCliente(aDTOCliente);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RegistrarClienteNombreMuyLargoTest()
        {
            string nombreCon101Caracteres = "Este es un string sencillo de 101 caracteres que puedes usar para tu proyecto. Este es un string sencillo de 101 caracteres que puedes usar para tu proyecto.";

            aDTOCliente.NombreYApellido = nombreCon101Caracteres;

            _controller.RegistrarCliente(aDTOCliente);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RegistrarClienteMailNullTest()
        {
            aDTOCliente.Mail = null;

            _controller.RegistrarCliente(aDTOCliente);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RegistrarClienteMailVacioTest()
        {
            aDTOCliente.Mail = "";

            _controller.RegistrarCliente(aDTOCliente);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RegistrarPasswordNullTest()
        {
            aDTOCliente.Password = null;

            _controller.RegistrarCliente(aDTOCliente);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RegistrarClientePasswordVacioTest()
        {
            aDTOCliente.Password = "";

            _controller.RegistrarCliente(aDTOCliente);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RegistrarClienteNombreYApellidoNullTest()
        {
            aDTOCliente.NombreYApellido = null;

            _controller.RegistrarCliente(aDTOCliente);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RegistrarClienteNombreYApellidolVacioTest()
        {
            aDTOCliente.NombreYApellido = "";

            _controller.RegistrarCliente(aDTOCliente);
        }
        [TestMethod]
        public void RegistrarAdministradorTest()
        {
            _controller.RegistrarAdministrador(aDTOAdministrador);

            Assert.AreEqual(aDTOAdministrador.Mail, _administradorLogic.(aDTOAdministrador.Mail).Mail);
            Assert.AreEqual(aDTOAdministrador.NombreYApellido, _administradorLogic.buscarAdministradorPorMail(aDTOAdministrador.Mail).NombreYApellido);
            Assert.AreEqual(aDTOAdministrador.Password, _administradorLogic.buscarAdministradorPorMail(aDTOAdministrador.Mail).Password);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RegistrarAdministradorConMailInvalidoTest()
        {
            aDTOAdministrador.Mail = "mailinvalido";

            _controller.RegistrarAdministrador(aDTOAdministrador);

        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RegistrarAdministradorPasswordIncorrectaTest()
        {
            aDTOAdministrador.Password = "password";

            _controller.RegistrarAdministrador(aDTOAdministrador);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RegistrarAdministradorNombreMuyLargoTest()
        {
            string nombreCon101Caracteres = "Este es un string sencillo de 101 caracteres que puedes usar para tu proyecto. Este es un string sencillo de 101 caracteres que puedes usar para tu proyecto.";

            aDTOAdministrador.NombreYApellido = nombreCon101Caracteres;

            _controller.RegistrarAdministrador(aDTOAdministrador);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RegistrarAdministradorMailNullTest()
        {
            aDTOAdministrador.Mail = null;

            _controller.RegistrarAdministrador(aDTOAdministrador);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RegistrarAdministradorMailVacioTest()
        {
            aDTOAdministrador.Mail = "";

            _controller.RegistrarAdministrador(aDTOAdministrador);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RegistrarPasswordNullTest()
        {
            aDTOAdministrador.Password = null;

            _controller.RegistrarAdministrador(aDTOAdministrador);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RegistrarAdministradorPasswordVacioTest()
        {
            aDTOAdministrador.Password = "";

            _controller.RegistrarAdministrador(aDTOAdministrador);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RegistrarAdministradorNombreYApellidoNullTest()
        {
            aDTOAdministrador.NombreYApellido = null;

            _controller.RegistrarAdministrador(aDTOAdministrador);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RegistrarAdministradorNombreYApellidolVacioTest()
        {
            aDTOAdministrador.NombreYApellido = "";

            _controller.RegistrarAdministrador(aDTOAdministrador);
        }

    }
}