using Domain;
using BusinessLogic;
using Repository;
using Repository.Context;
using Repository.SQL;

namespace Tests.BusinessLogicTests
{
    [TestClass]
    public class PromocionLogicTest
    {
        private PromocionLogic? _promocionLogic;
        private PromocionRepository? _promocionRepository;
        private readonly DepositoContextFactory _contextFactory = new DepositoContextEnMemoria();
        private DepositoContext _context; 
        private Promocion promo;

        [TestInitialize]
        public void setUp()
        {
            _context = _contextFactory.CrearContext();
            _promocionRepository = new PromocionRepository(_context);
            _promocionLogic = new PromocionLogic(_promocionRepository);
          

            promo = new Promocion("Promo",20,DateTime.Now, DateTime.Now.AddDays(10));
        }

        [TestCleanup]
        public void borrarDB()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void AgregarPromocionTest() {
            Promocion retorno = _promocionLogic.AgregarPromocion(promo);

            Assert.AreEqual(promo.PromocionId, retorno.PromocionId);
        }
        [TestMethod]
        public void ListarTodasLasPromocionesTest ()
        {
            Promocion promo1 = new Promocion("Promo1", 5, DateTime.Now, DateTime.Now.AddDays(10));
            Promocion promo2 = new Promocion("Promo2", 5, DateTime.Now, DateTime.Now.AddDays(15));

             _promocionLogic.AgregarPromocion(promo1);
             _promocionLogic.AgregarPromocion(promo2);

            IList<Promocion> listaPromociones = _promocionLogic.listarTodasLasPromociones();

            Assert.AreEqual(promo1.PromocionId, listaPromociones.FirstOrDefault(x => x.PromocionId == promo1.PromocionId).PromocionId);
            Assert.AreEqual(promo2.PromocionId, listaPromociones.FirstOrDefault(x => x.PromocionId == promo2.PromocionId).PromocionId);
        }

        [TestMethod]
        public void EncontrarPromocionPorIdTest()
        {
            _promocionLogic.AgregarPromocion(promo);

            Promocion promoPorFind = _promocionLogic.buscarPromocionPorId(promo.PromocionId);

            Assert.AreEqual(promo.PromocionId, promoPorFind.PromocionId);
        }

        [TestMethod]
        public void BuscarPromocionPorIdQueNoExisteYDevuelvaNullTest()
        {
            var promocionNoEncontrada = _promocionLogic.buscarPromocionPorId(1);

            Assert.IsNull(promocionNoEncontrada);
        }

        [TestMethod]
        public void EliminarPromocionTest()
        {
            _promocionLogic.AgregarPromocion(promo);

            _promocionLogic.EliminarPromocion(promo.PromocionId);

            var promocionEliminada = _promocionLogic.buscarPromocionPorId(promo.PromocionId);

            Assert.IsNull(promocionEliminada);
        }

        [TestMethod]
        public void ActualizarPromocionTest()
        {

            _promocionLogic.AgregarPromocion(promo);

            string etiquetaActualizada = "new Etiqueta";
            int porcentajeActualizado = 32;
            DateTime fechaInicioActualizada = DateTime.Now;
            DateTime fechaFinActualizada = DateTime.Now.AddDays(16);

            Promocion promoActualizada = new Promocion(1, etiquetaActualizada, porcentajeActualizado, fechaInicioActualizada, fechaFinActualizada);

            Promocion promoActualizdaRetorno = _promocionLogic.ActualizarInfoPromocion(promoActualizada);

            Assert.AreEqual(etiquetaActualizada, promoActualizdaRetorno.Etiqueta);
            Assert.AreEqual(porcentajeActualizado, promoActualizdaRetorno.PorcentajeDescuento);
            Assert.AreEqual(fechaInicioActualizada, promoActualizdaRetorno.FechaInicio);
            Assert.AreEqual(fechaFinActualizada, promoActualizdaRetorno.FechaFin);
        }

        [TestMethod]
        public void ActualizarPromocionQueNoExisteTest()
        {
            Promocion promocionQueNoExsiteEnBD = new Promocion("Promo1", 5, DateTime.Now, DateTime.Now.AddDays(10));

            Promocion promoNull = _promocionLogic.ActualizarInfoPromocion(promocionQueNoExsiteEnBD);

            Assert.IsNull(promoNull);
        }

    }
}
