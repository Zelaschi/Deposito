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
        private Deposito? deposito2;


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

            deposito1 = new Deposito( area1, tamanio1, climatizacion1);
            deposito2 = new Deposito( area2, tamanio2, climatizacion2);
        }
        [TestCleanup]
        public void clear() 
        {
            Deposito.UltimoID = 0;
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

            Assert.AreEqual(deposito1.IdDeposito, depositoRetorno1.IdDeposito);
            Assert.AreEqual(deposito2.IdDeposito, depositoRetorno2.IdDeposito);
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

        public void BuscarDepositoPorIdQueNoExisteYDevuelvaNullTest()
        {
            var depositoNoEncontradoPorId = _depositoLogic.buscarDepositoPorId(4);

            Assert.IsNull(depositoNoEncontradoPorId);
        }

        [TestMethod]

        public void EliminarDepositoTest()
        {
            _depositoLogic.AddDeposito(deposito1);
            _depositoLogic.EliminarDeposito(deposito1.IdDeposito);
            var depositoEliminado = _depositoLogic.buscarDepositoPorId(deposito1.IdDeposito);

            Assert.IsNull(depositoEliminado);
        }
        [TestMethod]
        public void AgregarPromocionADepositoYHayPromocionHoyTest() 
        {
            
            Promocion promo = new Promocion("Promo Test", 20, DateTime.Now, DateTime.Now.AddDays(10));

            deposito1.AgregarPromocionADeposito(promo);
            
            Promocion promocionEncontrada = deposito1.mejorPromocionHoy();

            Assert.AreEqual(promo.IdPromocion, promocionEncontrada.IdPromocion);
        }

    }
}
