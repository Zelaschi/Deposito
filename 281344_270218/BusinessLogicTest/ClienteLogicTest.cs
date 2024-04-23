using Domain;
using BusinessLogic;
using Repository;

namespace BusinessLogicTest
{
    [TestClass]
    public class ClienteLogicTest
    {
        private  ClienteLogic? _clienteLogic;
        private IRepository<Cliente>? _clienteRepository;
        private Cliente? cliente1;
        private int idCliente1 = 1;
        private Cliente? cliente2;
        private int idCliente2 = 2;

        [TestInitialize]
        public void setUp() {
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
        public void crearClienteConParametrosTest()
        {
            var nombreYApellidoTest = "Pablo Zelaschi";
            var mailTest = "pablozelaschi@gmail.com";
            var passwordTest = "Password1!";

            Cliente cliente = new Cliente(nombreYApellidoTest, mailTest, passwordTest);
        }

        [TestMethod]
        public void getParametros()
        {
            var nombreYApellidoEsperado = "Pablo Zelaschi";
            var mailEsperado = "pablozelaschi@gmail.com";
            var passwordEsperada = "Password1!";

            Cliente cliente = new Cliente(nombreYApellidoEsperado, mailEsperado, passwordEsperada);

            Assert.AreEqual(cliente.NombreYApellido, nombreYApellidoEsperado);
            Assert.AreEqual(cliente.Mail, mailEsperado);
            Assert.AreEqual(cliente.Password, passwordEsperada);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AgregarClienteConNombreYApellidoVacioTest()
        {
            Cliente cliente = new Cliente("", "totozelaschi@gmail.com", "Password1!");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AgregarClienteConMailVacioTest()
        {
            Cliente cliente = new Cliente("z", "", "Password1!");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AgregarClienteConPasswordVaciaTest()
        {
            Cliente cliente = new Cliente("z", "totozelaschi@gmail.com", "");
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AgregarClienteNombreYApellidoNullTest()
        {
            Cliente cliente = new Cliente(null, "totozelaschi@gmail.com", "Passworod1!");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AgregarClienteMailNullTest()
        {
            Cliente cliente = new Cliente("z", null, "Password1!S");
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentNullException))]
        public void AgregarClientePasswordNullTest()
        {
            Cliente cliente = new Cliente("z", "totozelaschi@gmail.com", null);
        }




        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FormatoEmailIncorrectoTest()
        {
            // Direcciones de correo electrónico con formato incorrecto
            var mail1 = "invalido@example"; // Falta el dominio .com, .org, etc.
            var mail2 = "invalido@ejemplo com"; // Falta el punto entre el nombre del dominio y el dominio
            var mail3 = "invalido@ejemplo."; // Falta el dominio .com, .org, etc. después del punto
            var mail4 = "invalido@@ejemplo.com"; // Doble símbolo '@'
            var mail5 = "invalido@ejemplo,com"; // Coma en lugar de punto entre el nombre de dominio y el dominio
            var mail6 = "invalido@ejemplo..com"; // Doble punto consecutivo
            var mail7 = "invalido.ejemplo.com"; // Falta el símbolo '@' para separar el nombre del usuario del dominio
            var mail8 = "@ejemplo.com"; // Falta el nombre de usuario antes del símbolo '@'
            var mail9 = "invalido@.com"; // Falta el nombre del dominio antes del símbolo '.'
            var mail10 = "invalido@ejemplo_com"; // Guion bajo en lugar de punto entre el nombre de dominio y el dominio

            cliente1.Mail = mail1;
            cliente1.Mail = mail2;
            cliente1.Mail = mail3;
            cliente1.Mail = mail4;
            cliente1.Mail = mail5;
            cliente1.Mail = mail6;
            cliente1.Mail = mail7;
            cliente1.Mail = mail8;
            cliente1.Mail = mail9;
            cliente1.Mail = mail10;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FormatoPasswordIncorrectoTest()
        {
            var pasw1 = "P@ss";            // Contraseña con menos de 8 caracteres
            var pasw2 = "Passw0rd";         // Contraseña sin símbolos
            var pasw3 = "PASSWORD1@";       // Contraseña sin letras minúsculas
            var pasw4 = "password1@";       // Contraseña sin letras mayúsculas
            var pasw5 = "Password@";        // Contraseña sin dígitos
            var pasw6 = "password";         // Contraseña sin símbolos, letras minúsculas y dígitos
            var pasw7 = "PASSWORD";         // Contraseña sin símbolos, letras mayúsculas y dígitos
            var pasw8 = "12345678";         // Contraseña sin símbolos, letras minúsculas y letras mayúsculas
            var pasw9 = "abc";              // Contraseña que no cumple con ninguna regla

            cliente1.Password = pasw1;
            cliente1.Password = pasw2;
            cliente1.Password = pasw3;
            cliente1.Password = pasw4;
            cliente1.Password = pasw5;
            cliente1.Password = pasw6;
            cliente1.Password = pasw7;
            cliente1.Password = pasw8;
            cliente1.Password = pasw9;
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void NombreYApellido101CaracteresTest()
        {
            string nombreCon101Caracteres = "Este es un string sencillo de 101 caracteres que puedes usar para tu proyecto. Este es un string sencillo de 101 caracteres que puedes usar para tu proyecto.";
            cliente1.NombreYApellido = nombreCon101Caracteres;
        }

        [TestMethod]
        public void DosClientesIdDistintoTest()
        {
            var nombreYApellidoTest1 = "Tomas Zelaschi";
            var mailTest1 = "tomaszelaschi@gmail.com";
            var passwordTest1 = "Password1!";

            Cliente cliente1 = new Cliente(nombreYApellidoTest1, mailTest1, passwordTest1);

            var nombreYApellidoTest2 = "Tomas Zelaschi";
            var mailTest2 = "tomaszelaschi@gmail.com";
            var passwordTest2 = "Password1!";

            Cliente cliente2 = new Cliente(nombreYApellidoTest2, mailTest2, passwordTest2);

            Assert.AreNotEqual(cliente1.IdPersona, cliente2.IdPersona);
        }

        [TestMethod]
        public void AgregarClienteTest()
        {
            Cliente clienteRetorno = _clienteLogic.AddCliente(cliente1);

            Assert.AreEqual(1, clienteRetorno.IdPersona);
            Assert.AreEqual(cliente1.NombreYApellido, clienteRetorno.NombreYApellido);
            Assert.AreEqual(cliente1.Mail, clienteRetorno.Mail);
            Assert.AreEqual(cliente1.Password, clienteRetorno.Password);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AgregarDosClientesMismoMailTest() { 

            _clienteLogic.AddCliente(cliente1);
            _clienteLogic.AddCliente(cliente1);
        }

        [TestMethod]
        public void AgregarDosClientesTest()
        {

            Cliente clienteRetorno1 = _clienteLogic.AddCliente(cliente1);
            Cliente clienteRetorno2 = _clienteLogic.AddCliente(cliente2);

            Assert.AreEqual(idCliente1, clienteRetorno1.IdPersona);
            Assert.AreEqual(idCliente2 , clienteRetorno2.IdPersona);
        }

        [TestMethod]
        public void ListarTodosLosCLientesTest()
        {

            Cliente clienteRetorno1 = _clienteLogic.AddCliente(cliente1);
            Cliente clienteRetorno2 = _clienteLogic.AddCliente(cliente2);

            IList<Cliente> resultClientes = _clienteLogic.GetAll();

            Assert.AreEqual(cliente1.NombreYApellido, resultClientes.FirstOrDefault(x => x.IdPersona == 1).NombreYApellido);
            Assert.AreEqual(cliente2.NombreYApellido, resultClientes.FirstOrDefault(x => x.IdPersona == 2).NombreYApellido);
        }
        [TestMethod]
        public void EncontrarPorIdTest() {
            _clienteLogic.AddCliente(cliente1);
            Cliente cliente1PeroPorFind = _clienteLogic.buscarClientePorId(cliente1.IdPersona);
        
            Assert.AreEqual(cliente1.IdPersona, cliente1PeroPorFind.IdPersona);
        }
        [TestMethod]
        public void EncontrarPorMailTest()
        {
            _clienteLogic.AddCliente(cliente1);
            Cliente cliente1PeroPorFind = _clienteLogic.buscarClientePorMail(cliente1.Mail);

            Assert.AreEqual(cliente1.Mail, cliente1PeroPorFind.Mail);
        }

        [TestMethod]
        public void BuscarClientePorIdYMailQueNoExisteYDevuelvaNullTest() {
            var clienteNoEncontradoPorId = _clienteLogic.buscarClientePorId(4);
            var clienteNoEncontradoPorMail = _clienteLogic.buscarClientePorMail("tz@gmail.com");

            Assert.IsNull(clienteNoEncontradoPorId);
            Assert.IsNull(clienteNoEncontradoPorMail);
        }
        [TestMethod]
        public void EliminarCliente() {
            _clienteLogic.AddCliente(cliente2);
            _clienteLogic.EliminarCliente(cliente2);
            var clientEliminado = _clienteLogic.buscarClientePorId(cliente2.IdPersona);

            Assert.IsNull(clientEliminado);
        }
        


    }
}
