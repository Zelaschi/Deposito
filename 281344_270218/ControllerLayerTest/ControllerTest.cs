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
        private DTODeposito aDTODeposito;
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

            aDTOCliente = new DTOCliente(nombreYApellidoTest, emailTest, pwdTest);
            aDTOAdministrador = new DTOAdministrador(nombreYApellidoTest, emailTest, pwdTest);
            aDTOpromocion = new DTOPromocion(1, "etiqueta", 20, DateTime.Today, DateTime.Today.AddDays(1));
            aDTODeposito = new DTODeposito(1, "A", "Grande", true);
            aDTOreserva = new DTOReserva(DateTime.Today, DateTime.Today.AddDays(15), aDTODeposito, aDTOCliente);

        }

        //CLIENTE
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
        public void RegistrarClientePasswordNullTest()
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
        public void RegistrarDosClientesOkTest()
        {
            var DTOCliente1 = new DTOCliente(nombreYApellidoTest, emailTest, pwdTest);
            var DTOCliente2 = new DTOCliente(nombreYApellidoTest, "email2valido@gmail.com", pwdTest);

            _controller.RegistrarCliente(DTOCliente1);
            _controller.RegistrarCliente(DTOCliente2);

            Assert.AreEqual(DTOCliente1.Mail, _clienteLogic.buscarClientePorMail(DTOCliente1.Mail).Mail);
            Assert.AreEqual(DTOCliente1.NombreYApellido, _clienteLogic.buscarClientePorMail(DTOCliente1.Mail).NombreYApellido);
            Assert.AreEqual(DTOCliente1.Password, _clienteLogic.buscarClientePorMail(DTOCliente1.Mail).Password);

            Assert.AreEqual(DTOCliente2.Mail, _clienteLogic.buscarClientePorMail(DTOCliente2.Mail).Mail);
            Assert.AreEqual(DTOCliente2.NombreYApellido, _clienteLogic.buscarClientePorMail(DTOCliente2.Mail).NombreYApellido);
            Assert.AreEqual(DTOCliente2.Password, _clienteLogic.buscarClientePorMail(DTOCliente2.Mail).Password);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RegistrarDosClientesMismoMailTest()
        {
            var DTOCliente1 = new DTOCliente(nombreYApellidoTest, emailTest, pwdTest);
            var DTOCliente2 = new DTOCliente(nombreYApellidoTest, emailTest, pwdTest);

            _controller.RegistrarCliente(DTOCliente1);
            _controller.RegistrarCliente(DTOCliente2);
        }
        [TestMethod]
        public void ListarTodosLosClientesTest()
        {
            var DTOCliente1 = new DTOCliente(nombreYApellidoTest, emailTest, pwdTest);
            var DTOCliente2 = new DTOCliente(nombreYApellidoTest, "email2valido@gmail.com", pwdTest);

            _controller.RegistrarCliente(DTOCliente1);
            _controller.RegistrarCliente(DTOCliente2);

            IList<DTOCliente> listaDTOCliente = _controller.listarTodosLosClientes();

            Assert.AreEqual(DTOCliente1.NombreYApellido, listaDTOCliente.FirstOrDefault(x => x.Mail == DTOCliente1.Mail).NombreYApellido);
            Assert.AreEqual(DTOCliente2.NombreYApellido, listaDTOCliente.FirstOrDefault(x => x.Mail == DTOCliente2.Mail).NombreYApellido);
        }
        [TestMethod]
        public void BuscarClientePorMailTest()
        {
            _controller.RegistrarCliente(aDTOCliente);

            var DTOClienteRetorno = _controller.buscarClientePorMail(aDTOCliente.Mail);

            Assert.AreEqual(aDTOCliente.Mail, DTOClienteRetorno.Mail);
        }
        [TestMethod]
        public void BuscarClientePorIdTest()
        {
            _controller.RegistrarCliente(aDTOCliente);
            var DTOClienteRetorno = _controller.buscarClientePorId(1);

            Assert.AreEqual(aDTOCliente.Mail, DTOClienteRetorno.Mail);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BuscarClientePorMailYQueTireExceptionSiNoLoEncuentraTest()
        {
            _controller.buscarClientePorMail(aDTOCliente.Mail);
            _controller.buscarClientePorId(1);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void EliminarClienteTest() 
        {
            _controller.RegistrarCliente(aDTOCliente);
            _controller.EliminarCliente(aDTOCliente);
            _controller.buscarClientePorMail(aDTOCliente.Mail);
        }

        //ADMINISTRADOR
        [TestMethod]
        public void RegistrarAdministradorTest()
        {
            _controller.RegistrarAdministrador(aDTOAdministrador);

            Assert.AreEqual(aDTOAdministrador.Mail, _administradorLogic.ObtenerAdministrador().Mail);
            Assert.AreEqual(aDTOAdministrador.NombreYApellido, _administradorLogic.ObtenerAdministrador().NombreYApellido);
            Assert.AreEqual(aDTOAdministrador.Password, _administradorLogic.ObtenerAdministrador().Password);
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
        public void RegistrarAdministradorPasswordNullTest()
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
        [TestMethod]
        public void ObtenerAdministradorTest()
        {
            _controller.RegistrarAdministrador(aDTOAdministrador);
            DTOAdministrador admin = _controller.ObtenerAdministrador();
            
            Assert.AreEqual(aDTOAdministrador.Mail, admin.Mail);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void ObtenerAdministradorTiraExeptionCuandoElAdministradorNoEstaDefinido()
        {
            DTOAdministrador admin = _controller.ObtenerAdministrador();
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void IntentarAgregarSegundoAdministradorTest() 
        {
            var OtroAdmin = new DTOAdministrador("nombre", "mailvalido@gmail.com", "Password1!");

            _controller.RegistrarAdministrador(aDTOAdministrador);
            _controller.RegistrarAdministrador(OtroAdmin);
        }

        //DEPOSITO
        [TestMethod]
        public void RegistrarDepositoTest() {
            _controller.RegistrarDeposito(aDTODeposito);

            Assert.AreEqual(aDTODeposito.Id, _depositoLogic.buscarDepositoPorId(aDTODeposito.Id).IdDeposito);
            Assert.AreEqual(aDTODeposito.Area, _depositoLogic.buscarDepositoPorId(aDTODeposito.Id).Area);
            Assert.AreEqual(aDTODeposito.Tamanio, _depositoLogic.buscarDepositoPorId(aDTODeposito.Id).Tamanio);
            Assert.AreEqual(aDTODeposito.Climatizacion, _depositoLogic.buscarDepositoPorId(aDTODeposito.Id).Climatizacion);
        }

        //PROMOCION

        [TestMethod]

        public void RegistrarPromocionTest() 
        {
            _controller.RegistrarPromocion(aDTOpromocion);

            Assert.AreEqual(aDTOpromocion.IdPromocion, _promocionLogic.buscarPromocionPorId(aDTOpromocion.IdPromocion).Id);
            Assert.AreEqual(aDTOpromocion.Etiqueta, _promocionLogic.buscarPromocionPorId(aDTOpromocion.IdPromocion).Etiqueta);
            Assert.AreEqual(aDTOpromocion.PorcentajeDescuento, _promocionLogic.buscarPromocionPorId(aDTOpromocion.IdPromocion).PorcentajeDescuento);
            Assert.AreEqual(aDTOpromocion.FechaInicio, _promocionLogic.buscarPromocionPorId(aDTOpromocion.IdPromocion).FechaInicio);
            Assert.AreEqual(aDTOpromocion.FechaFIn, _promocionLogic.buscarPromocionPorId(aDTOpromocion.IdPromocion).FechaFin);
        }
    }
}