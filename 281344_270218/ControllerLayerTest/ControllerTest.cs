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
        }

        //[TestMethod]
        //public void TestMethod1()
        //{
        //}
    }
}