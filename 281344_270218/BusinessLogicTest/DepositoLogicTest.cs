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
        private Deposito? deposito3;
        private Deposito? deposito4;

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

            var area3 = "D";
            var tamanio3 = "Grande";
            bool climatizacion3 = true;
            string nombre3 = "DepositoPruebaTres";

            var area4 = "E";
            var tamanio4 = "Grande";
            bool climatizacion4 = true;
            string nombre4 = "DepositoPruebaCuatro";

            deposito1 = new Deposito( area1, tamanio1, climatizacion1);
            deposito2 = new Deposito( area2, tamanio2, climatizacion2);
            deposito3 = new Deposito(nombre3, area3, tamanio3, climatizacion3, DateTime.Today, DateTime.Today.AddDays(3));
            deposito4 = new Deposito(nombre4, area4, tamanio4, climatizacion4, DateTime.Today, DateTime.Today.AddDays(3));
        }
        [TestCleanup]
        public void clear() 
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]

        public void AgregarDepositoTest()
        {
            Deposito depositoRetorno = _depositoLogic.AddDeposito(deposito3);
            Assert.AreEqual(1, depositoRetorno.DepositoId);
            Assert.AreEqual(deposito3.Tamanio, depositoRetorno.Tamanio);
            Assert.AreEqual(deposito3.Area, depositoRetorno.Area);
            Assert.AreEqual(deposito3.Climatizacion, depositoRetorno.Climatizacion);
            Assert.AreEqual(deposito3.Nombre, depositoRetorno.Nombre);


        }

        [TestMethod]

        public void AgregarDosDepositosTest()
        {
            Deposito depositoRetorno1 = _depositoLogic.AddDeposito(deposito3);
            Deposito depositoRetorno2 = _depositoLogic.AddDeposito(deposito4);

            Assert.AreEqual(deposito3.DepositoId, depositoRetorno1.DepositoId);
            Assert.AreEqual(deposito4.DepositoId, depositoRetorno2.DepositoId);
        }

        [TestMethod]

        public void ListarTodosLosDepositosTest()
        {
            Deposito depositoRetorno1 = _depositoLogic.AddDeposito(deposito3);
            Deposito depositoRetorno2 = _depositoLogic.AddDeposito(deposito4);

            IList<Deposito> depositos = _depositoLogic.GetAll();

            Assert.AreEqual(deposito3.DepositoId, depositos.FirstOrDefault(x => x.DepositoId == 1).DepositoId);
            Assert.AreEqual(deposito4.DepositoId, depositos.FirstOrDefault(x => x.DepositoId == 2).DepositoId);
        }

        [TestMethod]

        public void EncontrarDepositoPorIdTest()
        {
            _depositoLogic.AddDeposito(deposito3);
            Deposito depositoPeroPorFindId = _depositoLogic.buscarDepositoPorId(deposito3.DepositoId);

            Assert.AreEqual(deposito3.DepositoId, depositoPeroPorFindId.DepositoId);
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
            _depositoLogic.AddDeposito(deposito3);
            _depositoLogic.EliminarDeposito(deposito3.DepositoId);
            var depositoEliminado = _depositoLogic.buscarDepositoPorId(deposito3.DepositoId);

            Assert.IsNull(depositoEliminado);
        }
        [TestMethod]
        public void AgregarPromocionADepositoYHayPromocionHoyTest() 
        {
            
            Promocion promo = new Promocion("Promo Test", 20, DateTime.Now, DateTime.Now.AddDays(10));

            deposito3.AgregarPromocionADeposito(promo);
            
            Promocion promocionEncontrada = deposito3.mejorPromocionHoy();

            Assert.AreEqual(promo.PromocionId, promocionEncontrada.PromocionId);
        }

        [TestMethod]
        public void IncrementarIdCuandoSeCreaDepositoTest()
        {
            Deposito depositoRetorno1 = _depositoLogic.AddDeposito(deposito3);
            Deposito depositoRetorno2 = _depositoLogic.AddDeposito(deposito4);

            Assert.AreEqual(1, depositoRetorno1.DepositoId);
            Assert.AreEqual(2, depositoRetorno2.DepositoId);
        }

    }
}
