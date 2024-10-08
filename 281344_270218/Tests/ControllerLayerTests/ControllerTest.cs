using Domain;
using BusinessLogic;
using Repository;
using ControllerLayer;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Repository.Context;
using Repository.SQL;
using System.Data;


namespace Tests.ControllerLayerTests
{
    [TestClass]
    public class ControllerTest
    {
        private Controller _controller;
        private readonly DepositoContextFactory _contextFactory = new DepositoContextEnMemoria();
        private DepositoContext _context;


        private ClienteLogic _clienteLogic;
        private ClienteRepository _clienteRepository;
        private AdministradorLogic _administradorLogic;
        private AdministradorRepository _administradorRespository;

        private DepositoLogic _depositoLogic;
        private DepositoRepository _depositoRespository;

        private PromocionLogic _promocionLogic;
        private PromocionRepository _promocionRespository;
        private ReservaLogic _reservaLogic;
        private ReservaRepository _reservaRespository;

        private const string emailTest = "totozelaschi@gmail.com";
        private const string pwdTest = "Password1!";
        private const string nombreYApellidoTest = "NombreApellido";

        private DTOCliente aDTOCliente;
        private DTOAdministrador aDTOAdministrador;
        private DTODeposito aDTODeposito;
        private DTODeposito aDTODeposito2;
        private DTOPromocion aDTOPromocion;
        private DTOReserva aDTOReserva;

        [TestInitialize]
        public void setUp()
        {
            _context = _contextFactory.CrearContext();

            _clienteRepository = new ClienteRepository(_context);
            _clienteLogic = new ClienteLogic(_clienteRepository);
            _administradorRespository = new AdministradorRepository(_context);
            _administradorLogic = new AdministradorLogic(_administradorRespository);

            _depositoRespository = new DepositoRepository(_context);
            _depositoLogic = new DepositoLogic(_depositoRespository);
            _promocionRespository = new PromocionRepository(_context);
            _promocionLogic = new PromocionLogic(_promocionRespository);
            _reservaRespository = new ReservaRepository(_context);
            _reservaLogic = new ReservaLogic(_reservaRespository);

            _controller = new Controller(_administradorLogic, _clienteLogic, _depositoLogic, _promocionLogic, _reservaLogic);

            aDTOCliente = new DTOCliente(nombreYApellidoTest, "mail@mail.com", pwdTest);
            aDTOAdministrador = new DTOAdministrador(nombreYApellidoTest, emailTest, pwdTest);
            aDTOPromocion = new DTOPromocion(0, "etiqueta", 20, DateTime.Today, DateTime.Today.AddDays(1));
            aDTODeposito = new DTODeposito(1, "A", "Grande", true);
            aDTODeposito2 = new DTODeposito("DepositoNombre", "A", "Grande", true, DateTime.Today, DateTime.Today.AddDays(16));

            aDTOReserva = new DTOReserva(1, DateTime.Today, DateTime.Today.AddDays(15), aDTODeposito, aDTOCliente, 100);
        }

        [TestCleanup]
        public void limpieza()
        {
            _context.Database.EnsureDeleted();
        }
        [TestMethod]
        public void CrearControllerOkTest()
        {
            var clienteRepository = new ClienteRepository(_context);
            var clienteLogic = new ClienteLogic(clienteRepository);
            var administradorRespository = new AdministradorRepository(_context);
            var administradorLogic = new AdministradorLogic(administradorRespository);

            var depositoRespository = new DepositoRepository(_context);
            var depositoLogic = new DepositoLogic(depositoRespository);
            var promocionRespository = new PromocionRepository(_context);
            var promocionLogic = new PromocionLogic(promocionRespository);
            var reservaRespository = new ReservaRepository(_context);
            var reservaLogic = new ReservaLogic(reservaRespository);

            new Controller(administradorLogic, clienteLogic, depositoLogic, promocionLogic, reservaLogic);

        }
        [TestMethod]
        public void LoginTest()
        {
            _controller.RegistrarAdministrador(aDTOAdministrador);
            _controller.RegistrarCliente(aDTOCliente);

            Assert.IsTrue(_controller.LogIn(aDTOCliente.Mail, aDTOCliente.Password));
        }
        [TestMethod]
        public void LoginAdminTest()
        {
            _controller.RegistrarAdministrador(aDTOAdministrador);

            Assert.IsTrue(_controller.LogIn(aDTOAdministrador.Mail, aDTOAdministrador.Password));
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void LoginConClienteNoRegistradoTireExcepcionTest()
        {
            _controller.RegistrarAdministrador(aDTOAdministrador);
            _controller.LogIn(aDTOCliente.Mail, "Password1!");
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void LoginConAdminNoRegistradoTireExcepcionTest()
        {
            _controller.LogIn(aDTOCliente.Mail, "Password1!");
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void LoginConPasswordIncorrectaAdminTireExcepcionTest()
        {
            _controller.RegistrarAdministrador(aDTOAdministrador);
            _controller.LogIn(aDTOAdministrador.Mail, "PasswordIncorrecta");
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void LoginConPasswordIncorrectaTireExcepcionTest()
        {
            _controller.RegistrarAdministrador(aDTOAdministrador);
            _controller.RegistrarCliente(aDTOCliente);
            _controller.LogIn(aDTOCliente.Mail, "PasswordIncorrecta");
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void LoginConClienteSinRegistrarTireExceptionTest()
        {
            _controller.RegistrarAdministrador(aDTOAdministrador);
            _controller.LogIn(aDTOCliente.Mail, aDTOCliente.Password);
        }
        [TestMethod]
        public void EsAdministradorTest()
        {
            _controller.RegistrarAdministrador(aDTOAdministrador);
            bool esAdmin = _controller.esAdministrador(aDTOAdministrador.Mail);
            Assert.IsTrue(esAdmin);
        }
        //CLIENTE
        [TestMethod]
        public void RegistrarClienteTest()
        {
            DTOCliente clienteTest = new("nombree", "mailvalido@gmail.com", "Password1!");
            _controller.RegistrarCliente(clienteTest);

            Assert.AreEqual(clienteTest.Mail, _clienteLogic.buscarClientePorMail(clienteTest.Mail).Mail);
            Assert.AreEqual(clienteTest.NombreYApellido, _clienteLogic.buscarClientePorMail(clienteTest.Mail).NombreYApellido);
            Assert.AreEqual(clienteTest.Password, _clienteLogic.buscarClientePorMail(clienteTest.Mail).Password);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RegistrarClienteConMailInvalidoTest()
        {
            aDTOCliente.Mail = "mailinvalido";

            _controller.RegistrarCliente(aDTOCliente);

        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RegistrarClientePasswordIncorrectaTest()
        {
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
        [ExpectedException(typeof(Exception))]
        public void BuscarClientePorMailDeErrorAlNoEncontrar() {
            _controller.buscarClientePorMail("mail");
        }

        [ExpectedException(typeof(Exception))]
        public void BuscarClientePorMailYQueTireExceptionSiNoLoEncuentraTest()
        {
            _controller.buscarClientePorMail(aDTOCliente.Mail);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void AgregarClienteConMismoMailQueElAdministradorDeErrorTest()
        {
            _controller.RegistrarAdministrador(aDTOAdministrador);
            aDTOCliente.Mail = aDTOAdministrador.Mail;
            _controller.RegistrarCliente(aDTOCliente);
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
        [TestMethod]
        public void EstaRegistradoAdministradorTest()
        {
            _controller.RegistrarAdministrador(aDTOAdministrador);
            Assert.IsTrue(_controller.EstaRegistradoAdministrador());
        }
        [TestMethod]
        public void EstaRegistradoAdministradorDeFalseCuandoNoEstaRegistradoTest()
        {
            Assert.IsFalse(_controller.EstaRegistradoAdministrador());
        }


        //DEPOSITO
        [TestMethod]
        public void RegistrarDepositoTest()
        {
            _controller.RegistrarDeposito(aDTODeposito);

            Assert.AreEqual(aDTODeposito.Id, _depositoLogic.buscarDepositoPorId(aDTODeposito.Id).DepositoId);
            Assert.AreEqual(aDTODeposito.Area, _depositoLogic.buscarDepositoPorId(aDTODeposito.Id).Area);
            Assert.AreEqual(aDTODeposito.Tamanio, _depositoLogic.buscarDepositoPorId(aDTODeposito.Id).Tamanio);
            Assert.AreEqual(aDTODeposito.Climatizacion, _depositoLogic.buscarDepositoPorId(aDTODeposito.Id).Climatizacion);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void DefinirDepositoConParametroDisponibleDesdeAnteriorALaFechaActualDeErrorTest()
        {
            var depoAAgregar = new DTODeposito("DepositoPrueba", "A", "Grande", true, DateTime.Today.AddDays(-1), DateTime.Today.AddDays(10));
            _controller.RegistrarDeposito(depoAAgregar);
        }
        [TestMethod]
        public void RegistrarDepositoDevuelvaIdTest()
        {
            int idRetorno = _controller.RegistrarDeposito(aDTODeposito);

            DTODeposito DTOEncontrado = _controller.BuscarDepositoPorId(idRetorno);

            Assert.AreEqual(aDTODeposito.Id, DTOEncontrado.Id);
            Assert.AreEqual(aDTODeposito.Area, DTOEncontrado.Area);
            Assert.AreEqual(aDTODeposito.Tamanio, DTOEncontrado.Tamanio);
            Assert.AreEqual(aDTODeposito.Climatizacion, DTOEncontrado.Climatizacion);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]

        public void RegistrarDepositoConParametrosInvalidos()
        {
            aDTODeposito.Tamanio = "Gigante";
            aDTODeposito.Area = "areaInvalida";
            _controller.RegistrarDeposito(aDTODeposito);
        }

        [TestMethod]

        public void AgregarPromocionADepositoTest()
        {
            DTODeposito aDTODeposito = new DTODeposito(0, "A", "Grande", true);
            DTOPromocion aDTOPromocion = new DTOPromocion(0, "etiquietaPromo", 20, DateTime.Today, DateTime.Today.AddDays(1));

            int nuevoIdPromocion = _controller.RegistrarPromocion(aDTOPromocion);
            aDTOPromocion.PromocionId = nuevoIdPromocion;
            int nuevoIdDeposito = _controller.RegistrarDeposito(aDTODeposito);
            aDTODeposito.Id = nuevoIdDeposito;

            Deposito depo = _depositoLogic.buscarDepositoPorId(aDTODeposito.Id);
            Promocion promo = _promocionLogic.buscarPromocionPorId(aDTOPromocion.PromocionId);

            depo.AgregarPromocionADeposito(promo);

            Assert.AreEqual(promo.PromocionId, depo.mejorPromocionHoy().PromocionId);
        }


        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AgregarPromocionDosVecesADepositoTest()
        {
            DTODeposito aDTODeposito = new DTODeposito(0, "A", "Grande", true);
            DTOPromocion aDTOPromocion = new DTOPromocion(0, "etiquietaPromo", 20, DateTime.Today, DateTime.Today.AddDays(1));

            int nuevoIdPromocion = _controller.RegistrarPromocion(aDTOPromocion);
            aDTOPromocion.PromocionId = nuevoIdPromocion;
            int nuevoIdDeposito = _controller.RegistrarDeposito(aDTODeposito);
            aDTODeposito.Id = nuevoIdDeposito;

            Deposito depo = _depositoLogic.buscarDepositoPorId(aDTODeposito.Id);
            Promocion promo = _promocionLogic.buscarPromocionPorId(aDTOPromocion.PromocionId);

            depo.AgregarPromocionADeposito(promo);
            depo.AgregarPromocionADeposito(promo);
        }

        [TestMethod]

        public void BuscarDepositoPorId()
        {
            _controller.RegistrarDeposito(aDTODeposito);
            DTODeposito deposito = _controller.BuscarDepositoPorId(aDTODeposito.Id);

            Assert.AreEqual(aDTODeposito.Id, deposito.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void EliminarDepositoTest()
        {
            _controller.RegistrarDeposito(aDTODeposito);
            _controller.EliminarDeposito(aDTODeposito);
            _controller.BuscarDepositoPorId(aDTODeposito.Id);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void EliminarDepositoConReservaAsociadaAElDeErrorTest()
        {
            _controller.RegistrarDeposito(aDTODeposito);
            _controller.RegistrarCliente(aDTOCliente);
            _controller.RegistrarReserva(aDTOReserva);
            _controller.EliminarDeposito(aDTODeposito);
        }
        [TestMethod]
        public void ListarTodasLosDepositosTest()
        {
            var DTODeposito1 = new DTODeposito(1, "A", "Pequenio", true);
            var DTODeposito2 = new DTODeposito(2, "A", "Pequenio", true);

            _controller.RegistrarDeposito(DTODeposito1);
            _controller.RegistrarDeposito(DTODeposito2);

            IList<DTODeposito> listaDTODeposito = _controller.listarTodosLosDepositos();

            Assert.AreEqual(DTODeposito1.Id, listaDTODeposito.FirstOrDefault(x => x.Id == DTODeposito1.Id).Id);
            Assert.AreEqual(DTODeposito2.Id, listaDTODeposito.FirstOrDefault(x => x.Id == DTODeposito2.Id).Id);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void validarQueDepositoNoEsteAsociadoAReservaTireExcepcion()
        {
            _controller.RegistrarDeposito(aDTODeposito);
            _controller.RegistrarCliente(aDTOCliente);
            _controller.RegistrarReserva(aDTOReserva);

            _controller.EliminarDeposito(aDTODeposito);

        }

        //PROMOCION

        [TestMethod]

        public void RegistrarPromocionTest()
        {
            int nuevoId = _controller.RegistrarPromocion(aDTOPromocion);
            aDTOPromocion.PromocionId = nuevoId;
            Assert.AreEqual(aDTOPromocion.Etiqueta, _promocionLogic.buscarPromocionPorId(aDTOPromocion.PromocionId).Etiqueta);
            Assert.AreEqual(aDTOPromocion.PorcentajeDescuento, _promocionLogic.buscarPromocionPorId(aDTOPromocion.PromocionId).PorcentajeDescuento);
            Assert.AreEqual(aDTOPromocion.FechaInicio, _promocionLogic.buscarPromocionPorId(aDTOPromocion.PromocionId).FechaInicio);
            Assert.AreEqual(aDTOPromocion.FechaFin, _promocionLogic.buscarPromocionPorId(aDTOPromocion.PromocionId).FechaFin);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void DefinirPromocionConParametroFechaInicioAnteriorALaFechaActualDeErrorTest()
        {
            var promoAAgregar = new DTOPromocion(0, "promocion abril", 20, DateTime.Now.AddDays(-1), DateTime.Now.AddDays(10));
            _controller.RegistrarPromocion(promoAAgregar);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RegistrarPromocionEtiquetaMayorA20Test()
        {
            string etiquetaMayorA20Caracteres = "Este es un string sencillo de 101 caracteres que puedes usar para tu proyecto. Este es un string sencillo de 101 caracteres que puedes usar para tu proyecto.";

            aDTOPromocion.Etiqueta = etiquetaMayorA20Caracteres;

            _controller.RegistrarPromocion(aDTOPromocion);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]

        public void RegistrarPromocionFechaFinMayorAFechaInicio()
        {
            DateTime fechaInicioMayor = DateTime.Today.AddDays(1);
            DateTime fechaFinMenor = DateTime.Today;

            aDTOPromocion.FechaInicio = fechaInicioMayor;
            aDTOPromocion.FechaFin = fechaFinMenor;

            _controller.RegistrarPromocion(aDTOPromocion);
        }

        [TestMethod]

        public void ListarTodasLasPromocionesTest()
        {
            var DTOPromocion1 = new DTOPromocion(0, "etiqueta1", 20, DateTime.Today, DateTime.Today.AddDays(1));
            var DTOPromocion2 = new DTOPromocion(0, "etiqueta2", 20, DateTime.Today, DateTime.Today.AddDays(1));

            _controller.RegistrarPromocion(DTOPromocion1);
            _controller.RegistrarPromocion(DTOPromocion2);

            IList<DTOPromocion> listaDTOPromocion = _controller.listarTodasLasPromociones();

            Assert.IsNotNull(listaDTOPromocion.FirstOrDefault(x => x.PromocionId == 1));
            Assert.IsNotNull(listaDTOPromocion.FirstOrDefault(x => x.PromocionId == 2));

        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void EliminarPromocionTest()
        {
            var DTOPromocion1 = new DTOPromocion(0, "etiqueta1", 20, DateTime.Today, DateTime.Today.AddDays(1));
            int nuevoId = _controller.RegistrarPromocion(DTOPromocion1);
            DTOPromocion1.PromocionId = nuevoId;

            _controller.ElminarPromocion(DTOPromocion1);
            _controller.BuscarPromocionPorId(DTOPromocion1.PromocionId);
        }

        [TestMethod]
        public void BuscarPromocionPorIdTest()
        {
            int nuevoId = _controller.RegistrarPromocion(aDTOPromocion);
            aDTOPromocion.PromocionId = nuevoId;
            DTOPromocion promo = _controller.BuscarPromocionPorId(aDTOPromocion.PromocionId);

            Assert.AreEqual(aDTOPromocion.PromocionId, promo.PromocionId);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BuscarPromocionPorIdYQueTireExceptionSiNoLoEncuentraTest()
        {
            _controller.RegistrarPromocion(aDTOPromocion);
            DTOPromocion promo = _controller.BuscarPromocionPorId(3);

            Assert.AreEqual(aDTOPromocion.PromocionId, promo.PromocionId);
        }

        [TestMethod]

        public void ActualizarPromocionTest()
        {
            _controller.RegistrarPromocion(aDTOPromocion);

            aDTOPromocion.Etiqueta = "nuevaEtiqueta";
            aDTOPromocion.FechaInicio = DateTime.Today.AddDays(10);
            aDTOPromocion.FechaFin = DateTime.Today.AddDays(11);
            aDTOPromocion.PorcentajeDescuento = 40;
            aDTOPromocion.PromocionId = 1;

            _controller.ActualizarPromocion(aDTOPromocion);
        }
        [ExpectedException(typeof(NullReferenceException))]
        [TestMethod]
        public void ActualizarPromocionNoDefinida() {
            aDTOPromocion.Etiqueta = "nuevaEtiqueta";
            aDTOPromocion.FechaInicio = DateTime.Today.AddDays(10);
            aDTOPromocion.FechaFin = DateTime.Today.AddDays(11);
            aDTOPromocion.PorcentajeDescuento = 40;
            aDTOPromocion.PromocionId = 1;

            _controller.ActualizarPromocion(aDTOPromocion);
        }


        //RESERVA
        [TestMethod]
        public void RegistrarReservaDTOTest()
        {
            _controller.RegistrarCliente(aDTOCliente);
            _controller.RegistrarDeposito(aDTODeposito);
            _controller.RegistrarReserva(aDTOReserva);

            DTOReserva DTOReservaEncontrado = _controller.BuscarReservaPorId(aDTOReserva.Id);

            Assert.AreEqual(aDTOReserva.Id, DTOReservaEncontrado.Id);
            Assert.AreEqual(aDTOReserva.FechaDesde, DTOReservaEncontrado.FechaDesde);
            Assert.AreEqual(aDTOReserva.FechaHasta, DTOReservaEncontrado.FechaHasta);
            Assert.AreEqual(aDTOReserva.Deposito.Area, DTOReservaEncontrado.Deposito.Area);
            Assert.AreEqual(aDTOReserva.Cliente.NombreYApellido, DTOReservaEncontrado.Cliente.NombreYApellido);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void RegistrarReservaConDatosIncorrectosTest()
        {
            _controller.RegistrarCliente(aDTOCliente);
            _controller.RegistrarDeposito(aDTODeposito);
            DTOReserva DTOReservaIncorrectaTest = new DTOReserva(1, DateTime.Today.AddDays(15), DateTime.Today.AddDays(10), aDTODeposito, aDTOCliente, 1);

            _controller.RegistrarReserva(DTOReservaIncorrectaTest);
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void BuscarReservaNoRegistradaPorIdTireExceptionTest()
        {
            _controller.BuscarReservaPorId(1);
        }
        [TestMethod]
        public void listarTodasLasReservasTest()
        {
            _controller.RegistrarCliente(aDTOCliente);
            _controller.RegistrarDeposito(aDTODeposito);

            DTOReserva aDTOReserva2 = new DTOReserva(2, DateTime.Today.AddDays(10), DateTime.Today.AddDays(11), aDTODeposito, aDTOCliente, 1);

            _controller.RegistrarReserva(aDTOReserva);
            _controller.RegistrarReserva(aDTOReserva2);

            IList<DTOReserva> DTOReservas = _controller.ListarTodasLasReservas();

            Assert.AreEqual(aDTOReserva.FechaHasta, DTOReservas.FirstOrDefault(x => x.Id == aDTOReserva.Id).FechaHasta);
            Assert.AreEqual(aDTOReserva2.FechaHasta, DTOReservas.FirstOrDefault(x => x.Id == aDTOReserva2.Id).FechaHasta);
        }

        [TestMethod]
        public void AceptarReservaTest()
        {
            _controller.RegistrarCliente(aDTOCliente);
            _controller.RegistrarDeposito(aDTODeposito2);
            aDTOReserva.FechaDesde = DateTime.Today.AddDays(1);
            _controller.RegistrarReserva(aDTOReserva);
            _controller.AceptarReserva(aDTOReserva);

            DTOReserva DTOReservaEncontrado = _controller.BuscarReservaPorId(aDTOReserva.Id);

            Assert.AreEqual(DTOReservaEncontrado.Estado, "Aceptada");
        }
        [TestMethod]
        public void PagarReservaTest()
        {
            _controller.RegistrarCliente(aDTOCliente);
            _controller.RegistrarDeposito(aDTODeposito2);
            aDTOReserva.FechaDesde = DateTime.Today.AddDays(1);
            _controller.RegistrarReserva(aDTOReserva);
            _controller.PagarReserva(aDTOReserva);

            DTOReserva DTOReservaEncontrado = _controller.BuscarReservaPorId(aDTOReserva.Id);

            Assert.AreEqual(DTOReservaEncontrado.Pago.EstadoPago, "Reservado");
        }
        [TestMethod]
        public void AceptarReservaQueAgregueFechasNoDisponiblesADeposito()
        {
            _controller.RegistrarCliente(aDTOCliente);
            int nuevoIdDep = _controller.RegistrarDeposito(aDTODeposito2);
            aDTODeposito2.Id = nuevoIdDep;
            aDTOReserva.FechaDesde = DateTime.Today.AddDays(1);
            _controller.RegistrarReserva(aDTOReserva);

            _controller.AceptarReserva(aDTOReserva);
            Deposito deposito = _depositoLogic.buscarDepositoPorId(aDTODeposito2.Id);
            Assert.IsFalse(deposito.validarDisponibilidadBool(aDTOReserva.FechaDesde, aDTOReserva.FechaHasta));
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void IntentarAceptarReservaConFechasNoDisponibles()
        {
            _controller.RegistrarCliente(aDTOCliente);
            int nuevoIdDep = _controller.RegistrarDeposito(aDTODeposito2);
            aDTODeposito2.Id = nuevoIdDep;
            aDTOReserva.FechaDesde = DateTime.Today.AddDays(1);
            _controller.RegistrarReserva(aDTOReserva);
            _controller.RegistrarReserva(aDTOReserva);

            _controller.AceptarReserva(aDTOReserva);
            _controller.AceptarReserva(aDTOReserva);
            Deposito deposito = _depositoLogic.buscarDepositoPorId(aDTODeposito2.Id);
            Assert.IsFalse(deposito.validarDisponibilidadBool(aDTOReserva.FechaDesde, aDTOReserva.FechaHasta));
        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void IntentarRegistrarReservaConFechaInicioAnteriorAHoy()
        {
            _controller.RegistrarCliente(aDTOCliente);
            int nuevoIdDep = _controller.RegistrarDeposito(aDTODeposito2);
            aDTODeposito2.Id = nuevoIdDep;
            aDTOReserva.FechaDesde = DateTime.Today.AddDays(-1);
            _controller.RegistrarReserva(aDTOReserva);

        }

        [TestMethod]
        public void RechazarReservaTest()
        {
            _controller.RegistrarCliente(aDTOCliente);
            _controller.RegistrarDeposito(aDTODeposito);
            _controller.RegistrarReserva(aDTOReserva);

            _controller.RechazarReserva(aDTOReserva);

            DTOReserva DTOReservaEncontrado = _controller.BuscarReservaPorId(aDTOReserva.Id);

            Assert.AreEqual(DTOReservaEncontrado.Estado, "Rechazada");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void EliminarPromocionEnUsoTireExceptionTest()
        {
            aDTOPromocion.PromocionId = 0;
            int nuevoId = _controller.RegistrarPromocion(aDTOPromocion);
            aDTOPromocion.PromocionId = nuevoId;
            _controller.RegistrarDeposito(aDTODeposito);
            _controller.RegistrarCliente(aDTOCliente);
            _controller.AgregarPromocionADeposito(aDTOPromocion, aDTODeposito);
            _controller.RegistrarReserva(aDTOReserva);
            _controller.ElminarPromocion(aDTOPromocion);
        }
        [TestMethod]
        public void listarResevasDeClienteTest()
        {
            _controller.RegistrarDeposito(aDTODeposito);
            _controller.RegistrarCliente(aDTOCliente);
            _controller.RegistrarReserva(aDTOReserva);

            IList<DTOReserva> reservasDeCliente = _controller.listarReservasDeCliente(aDTOCliente);

            Assert.AreEqual(aDTOReserva.Id, reservasDeCliente.FirstOrDefault(x => x.Id == aDTOReserva.Id).Id);
        }
        [TestMethod]
        public void justificarRechazo()
        {
            _controller.RegistrarDeposito(aDTODeposito);
            _controller.RegistrarCliente(aDTOCliente);
            _controller.RegistrarReserva(aDTOReserva);
            string motivoRechazo = "motivo";
            _controller.justificacionRechazo(motivoRechazo, aDTOReserva);
            Reserva encontrada = _reservaLogic.BuscarReservaPorId(aDTOReserva.Id);

            Assert.AreEqual(motivoRechazo, encontrada.JustificacionRechazo);
        }
        [TestMethod]
        public void DTOSesionTest()
        {
            string mail = "example@example.com";
            bool esAdmin = true;

            var sesion = new DTOSesion();
            sesion.SesionMail = mail;
            sesion.EsAdministrador = esAdmin;
            sesion.LogIn = true;

            Assert.AreEqual(mail, sesion.SesionMail);
            Assert.AreEqual(esAdmin, sesion.EsAdministrador);
            Assert.IsTrue(sesion.LogIn);
        }
        [TestMethod]
        public void DepositosDisponiblesParaResrvaPorFechaTest()
        {
            int nuevoId = _controller.RegistrarDeposito(aDTODeposito2);
            aDTODeposito.Id = nuevoId;
            IList<DTODeposito> depositos = _controller.DepositosDisponiblesParaReservaPorFecha(DateTime.Today.AddDays(1), DateTime.Today.AddDays(5));
            Assert.AreEqual(aDTODeposito.Id, depositos.FirstOrDefault().Id);
        }




    }
}