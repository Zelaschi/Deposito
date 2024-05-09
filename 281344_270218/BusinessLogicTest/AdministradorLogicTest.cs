using Domain;
using Repository;
using BusinessLogic;

namespace BusinessLogicTest
{
    [TestClass]
    public class AdministradorLogicTest
    {
        private Administrador admin;
        private AdministradorLogic? _administradorLogic;
        private IRepository<Administrador>? _administradorRepository;

        [TestInitialize]
        public void setUp()
        {
            var nombreAdminTest = "Nombre del Admin";
            var mailAdmin = "mailadmin@gmail.com";
            var pswAdmin = "PasswordAdmin1!";

            admin = new Administrador(0, nombreAdminTest, mailAdmin, pswAdmin);

            _administradorRepository = new AdministradorMemoryRepository();
            _administradorLogic = new AdministradorLogic(_administradorRepository);
        }

        [TestMethod]
        public void AsignarAdministradorTest()
        {
            Administrador administradorRetorno = _administradorLogic.AsignarAdministrador(admin);

            Assert.AreEqual(0, administradorRetorno.IdPersona);
            Assert.AreEqual(admin.NombreYApellido, administradorRetorno.NombreYApellido);
            Assert.AreEqual(admin.Mail, administradorRetorno.Mail);
            Assert.AreEqual(admin.Password, administradorRetorno.Password);
        }

        [TestMethod]
        public void ActualizarInfoAdministradorTest()
        {
            _administradorLogic.AsignarAdministrador(admin);
            var nuevoNombre = "nombreActualizado";
            var nuevoMail = "nuevomail@gmail.com";
            var nuevaPswd = "NewPassword1!";

            Administrador administradorActualizado = new Administrador(0, nuevoNombre, nuevoMail, nuevaPswd);

            Administrador administradorActualizadoRetorno = _administradorLogic.ActualizarInfoAdministrador(administradorActualizado);

            Assert.AreEqual(nuevoNombre, administradorActualizadoRetorno.NombreYApellido);
            Assert.AreEqual(nuevoMail, administradorActualizadoRetorno.Mail);
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

            Assert.AreEqual(admin.IdPersona, AdminRetorno.IdPersona);
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ObtenerAdministradorSinAdministradorAsignadoTest() {

            Administrador AdminRetorno = _administradorLogic.ObtenerAdministrador();
        }
    }
}
