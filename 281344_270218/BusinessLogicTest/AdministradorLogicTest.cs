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
        public void getAdministradorTest()
        {

            Administrador administradorRetorno = _administradorLogic.AsignarAdministrador(admin);

            IList<Administrador> listaConAdmin = _administradorLogic.getAdministrador();


            Assert.AreEqual(admin.NombreYApellido, listaConAdmin.FirstOrDefault(x => x.IdPersona == 0).NombreYApellido);
        }

        [TestMethod]
        public void actualizarInfoAdministradorTest()
        {
            var nuevoNombre = "nombreActualizado";
            var nuevoMail = "nuevomail@gmail.com";
            var nuevaPswd = "NewPassword1!";

            Administrador administradorActualizado = new Administrador(0, nuevoNombre, nuevoMail, nuevaPswd);

            Administrador administradorActualizadoRetorno = _administradorLogic.ActualizarInfoAdministrador(administradorActualizado);
            
            Assert.AreEqual(nuevoNombre, administradorActualizadoRetorno.NombreYApellido);
            Assert.AreEqual(nuevoMail, administradorActualizadoRetorno.Mail);
            Assert.AreEqual(nuevaPswd, administradorActualizadoRetorno.Password);

        }
    }
}
