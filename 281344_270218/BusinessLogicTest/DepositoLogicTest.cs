using Domain;
using Repository;
using BusinessLogic;
using Repository.Context;
using Repository.SQL;

namespace BusinessLogicTest
{
    [TestClass]
    public class DepositoLogicTest
    {
        private DepositoLogic? _depositoLogic;
        private DepositoRepository _depositoRepository;
        private readonly DepositoContextFactory _contextFactory = new DepositoContextEnMemoria();
        private DepositoContext _context;

        private Deposito? deposito1;
        private Deposito? deposito2;


        [TestInitialize]

        public void setup ()
        {
            _context = _contextFactory.CrearContext();
            _depositoRepository = new DepositoRepository(_context);
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
            _context.Database.EnsureDeleted();
        }

        [TestMethod]

        public void AgregarDepositoTest()
        {
            Deposito depositoRetorno = _depositoLogic.AddDeposito(deposito1);
            Assert.AreEqual(1, depositoRetorno.DepositoId);
            Assert.AreEqual(deposito1.Tamanio, depositoRetorno.Tamanio);
            Assert.AreEqual(deposito1.Area, depositoRetorno.Area);
            Assert.AreEqual(deposito1.Climatizacion, depositoRetorno.Climatizacion);
        }

        [TestMethod]

        public void AgregarDosDepositosTest()
        {
            Deposito depositoRetorno1 = _depositoLogic.AddDeposito(deposito1);
            Deposito depositoRetorno2 = _depositoLogic.AddDeposito(deposito2);

            Assert.AreEqual(deposito1.DepositoId, depositoRetorno1.DepositoId);
            Assert.AreEqual(deposito2.DepositoId, depositoRetorno2.DepositoId);
        }

        [TestMethod]

        public void ListarTodosLosDepositosTest()
        {
            Deposito depositoRetorno1 = _depositoLogic.AddDeposito(deposito1);
            Deposito depositoRetorno2 = _depositoLogic.AddDeposito(deposito2);

            IList<Deposito> depositos = _depositoLogic.GetAll();

            Assert.AreEqual(deposito1.DepositoId, depositos.FirstOrDefault(x => x.DepositoId == 1).DepositoId);
            Assert.AreEqual(deposito2.DepositoId, depositos.FirstOrDefault(x => x.DepositoId == 2).DepositoId);
        }

        [TestMethod]

        public void EncontrarDepositoPorIdTest()
        {
            _depositoLogic.AddDeposito(deposito1);
            Deposito depositoPeroPorFindId = _depositoLogic.buscarDepositoPorId(deposito1.DepositoId);

            Assert.AreEqual(deposito1.DepositoId, depositoPeroPorFindId.DepositoId);
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
            _depositoLogic.EliminarDeposito(deposito1.DepositoId);
            var depositoEliminado = _depositoLogic.buscarDepositoPorId(deposito1.DepositoId);

            Assert.IsNull(depositoEliminado);
        }
        [TestMethod]
        public void AgregarPromocionADepositoYHayPromocionHoyTest() 
        {
            
            Promocion promo = new Promocion("Promo Test", 20, DateTime.Now, DateTime.Now.AddDays(10));

            deposito1.AgregarPromocionADeposito(promo);
            
            Promocion promocionEncontrada = deposito1.mejorPromocionHoy();

            Assert.AreEqual(promo.PromocionId, promocionEncontrada.PromocionId);
        }

    }
}
