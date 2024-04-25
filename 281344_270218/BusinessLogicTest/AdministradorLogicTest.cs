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
    }
}
