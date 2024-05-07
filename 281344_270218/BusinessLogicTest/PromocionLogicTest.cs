using Domain;
using BusinessLogic;
using Repository;

namespace BusinessLogicTest
{
    [TestClass]
    public class PromocionLogicTest
    {
        private PromocionLogic? _promocionLogic;
        private IRepository<Promocion>? _promocionRepository;
        private Promocion promo;

        [TestInitialize]
        public void setUp()
        {
            _promocionRepository = new PromocionMemoryRepository();
            _promocionLogic = new PromocionLogic(_promocionRepository);

            promo = new Promocion(0, "Promo",20,DateTime.Now, DateTime.Now.AddDays(10));
        }
        [TestMethod]
        public void AgregarPromocionTest() {
            Promocion retorno = _promocionLogic.AgregarPromocion(promo);

            Assert.AreEqual(promo.IdPromocion, retorno.IdPromocion);
        }
        [TestMethod]
        public void ListarTodasLasPromocionesTest ()
        {
            Promocion promo1 = new Promocion("Promo1", 5, DateTime.Now, DateTime.Now.AddDays(10));
            Promocion promo2 = new Promocion("Promo2", 5, DateTime.Now, DateTime.Now.AddDays(15));

             _promocionLogic.AgregarPromocion(promo1);
             _promocionLogic.AgregarPromocion(promo2);

            IList<Promocion> listaPromociones = _promocionLogic.listarTodasLasPromociones();

            Assert.AreEqual(promo1.IdPromocion, listaPromociones.FirstOrDefault(x => x.IdPromocion == promo1.IdPromocion).IdPromocion);
            Assert.AreEqual(promo2.IdPromocion, listaPromociones.FirstOrDefault(x => x.IdPromocion == promo2.IdPromocion).IdPromocion);
        }

        [TestMethod]
        public void EncontrarPromocionPorIdTest()
        {
            _promocionLogic.AgregarPromocion(promo);

            Promocion promoPorFind = _promocionLogic.buscarPromocionPorId(promo.IdPromocion);

            Assert.AreEqual(promo.IdPromocion, promoPorFind.IdPromocion);
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

            _promocionLogic.EliminarPromocion(promo.IdPromocion);

            var promocionEliminada = _promocionLogic.buscarPromocionPorId(promo.IdPromocion);

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

            Promocion promoActualizada = new Promocion(promo.IdPromocion, etiquetaActualizada, porcentajeActualizado, fechaInicioActualizada, fechaFinActualizada);

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
