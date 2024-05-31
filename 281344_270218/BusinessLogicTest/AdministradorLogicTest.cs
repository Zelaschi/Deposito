using Domain;
using Repository;
using BusinessLogic;
using Repository.Context;
using Repository.SQL;

namespace BusinessLogicTest
{
    [TestClass]
    public class AdministradorLogicTest
    {
        private Administrador admin;
        private AdministradorLogic? _administradorLogic;

        private AdministradorRepository _administradorRepository;
        private readonly DepositoContextFactory _contextFactory = new DepositoContextEnMemoria();
        private DepositoContext _context;

        [TestInitialize]
        public void setUp()
        {
            var nombreAdminTest = "Nombre del Admin";
            var mailAdmin = "mailadmin@gmail.com";
            var pswAdmin = "PasswordAdmin1!";

            admin = new Administrador(0, nombreAdminTest, mailAdmin, pswAdmin);

            _context = _contextFactory.CrearContext();
            _administradorRepository = new AdministradorRepository(_context);
            _administradorLogic = new AdministradorLogic(_administradorRepository);
        }
        [TestCleanup]
        public void borrarDB()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void AsignarAdministradorTest()
        {
            Administrador administradorRetorno = _administradorLogic.AsignarAdministrador(admin);

            Assert.AreEqual(1, administradorRetorno.PersonaId);
            Assert.AreEqual(admin.NombreYApellido, administradorRetorno.NombreYApellido);
            Assert.AreEqual(admin.Mail, administradorRetorno.Mail);
            Assert.AreEqual(admin.Password, administradorRetorno.Password);
        }

        [TestMethod]
        public void ActualizarInfoAdministradorTest()
        {
            _administradorLogic.AsignarAdministrador(admin);
            var nuevoNombre = "nombreActualizado";
            var nuevaPswd = "NewPassword1!";

            Administrador administradorActualizado = new Administrador(1, nuevoNombre, "mailadmin@gmail.com", nuevaPswd);

            Administrador administradorActualizadoRetorno = _administradorLogic.ActualizarInfoAdministrador(administradorActualizado);

            Assert.AreEqual(nuevoNombre, administradorActualizadoRetorno.NombreYApellido);
            Assert.AreEqual(nuevaPswd, administradorActualizadoRetorno.Password);

        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void IntentarAgregarSegundoAdministradorTest()
        {
            Administrador admin2 = new Administrador(0, "nombre", "mail@gmail.com", "Password1!");
            _administradorLogic.AsignarAdministrador(admin);
            _administradorLogic.AsignarAdministrador(admin2);

        }
        [TestMethod]
        public void ObtenerAdministradorTest() {
            _administradorLogic.AsignarAdministrador(admin);
            Administrador AdminRetorno = _administradorLogic.ObtenerAdministrador();

            Assert.AreEqual(admin.PersonaId, AdminRetorno.PersonaId);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ObtenerAdministradorSinAdministradorAsignadoTest() {

            Administrador AdminRetorno = _administradorLogic.ObtenerAdministrador();
        }
    }
}
