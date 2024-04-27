using Domain;
using Repository;
using BusinessLogic;


namespace BusinessLogicTest
{
    [TestClass]
    public class DepositoLogicTest
    {
        private DepositoLogic? _depositoLogic;
        private IRepository<Deposito>? _depositoRepository;
        private Deposito? deposito1;
        private int idDeposito1 = 1;
        private Deposito? deposito2;
        private int idDeposito2 = 2;

        [TestInitialize]

        public void setup ()
        {
            _depositoRepository = new DepositoMemoryRepository();
            _depositoLogic = new DepositoLogic(_depositoRepository);

            var area1 = "A";
            var tamanio1 = "Pequenio";
            bool climatizacion1 = false;

            var area2 = "C";
            var tamanio2 = "Mediano";
            bool climatizacion2 = true;

            deposito1 = new Deposito(idDeposito1, area1, tamanio1, climatizacion1);
            deposito2 = new Deposito(idDeposito2, area2, tamanio2, climatizacion2);
        }

        [TestMethod]

        public void AgregarDepositoTest()
        {
            Deposito depositoRetorno = _depositoLogic.AddDeposito(deposito1);
            Assert.AreEqual(1, depositoRetorno.IdDeposito);
            Assert.AreEqual(deposito1.Tamanio, depositoRetorno.Tamanio);
            Assert.AreEqual(deposito1.Area, depositoRetorno.Area);
            Assert.AreEqual(deposito1.Climatizacion, depositoRetorno.Climatizacion);
        }

        [TestMethod]

        public void AgregarDosDepositosTest()
        {
            Deposito depositoRetorno1 = _depositoLogic.AddDeposito(deposito1);
            Deposito depositoRetorno2 = _depositoLogic.AddDeposito(deposito2);

            Assert.AreEqual(idDeposito1, depositoRetorno1.IdDeposito);
            Assert.AreEqual(idDeposito2, depositoRetorno2.IdDeposito);
        }

        [TestMethod]

        public void ListarTodosLosDepositosTest()
        {
            Deposito depositoRetorno1 = _depositoLogic.AddDeposito(deposito1);
            Deposito depositoRetorno2 = _depositoLogic.AddDeposito(deposito2);

            IList<Deposito> resultDeposito = _depositoLogic.GetAll();

            Assert.AreEqual(deposito1.IdDeposito, resultDeposito.FirstOrDefault(x => x.IdDeposito == 1).IdDeposito);
            Assert.AreEqual(deposito2.IdDeposito, resultDeposito.FirstOrDefault(x => x.IdDeposito == 2).IdDeposito);
        }

        [TestMethod]

        public void EncontrarDepositoPorIdTest()
        {
            _depositoLogic.AddDeposito(deposito1);
            Deposito depositoPeroPorFindId = _depositoLogic.buscarDepositoPorId(deposito1.IdDeposito);

            Assert.AreEqual(deposito1.IdDeposito, depositoPeroPorFindId.IdDeposito);
        }

        [TestMethod]

        public void EliminarDepositoTest()
        {
            _depositoLogic.AddDeposito(deposito1);
            _depositoLogic.EliminarDeposito(deposito1.IdDeposito);
            var depositoEliminado = _depositoLogic.buscarDepositoPorId(deposito1.IdDeposito);

            Assert.IsNull(depositoEliminado);
        }
    }
}
