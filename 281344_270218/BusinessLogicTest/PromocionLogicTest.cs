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

            promo = new Promocion("Promo",20,DateTime.Now, DateTime.Now.AddDays(10));
        }
        [TestMethod]
        public void AgregarPromocionTest() {
            Promocion retorno = _promocionLogic.AgregarPromocion(promo);

            Assert.AreEqual(promo.Id, retorno.Id);
        }
        [TestMethod]
        public void ListarTodasLasPromocionesTest ()
        {
            Promocion promo1 = new Promocion("Promo1", 5, DateTime.Now, DateTime.Now.AddDays(10));
            Promocion promo2 = new Promocion("Promo2", 5, DateTime.Now, DateTime.Now.AddDays(15));

             _promocionLogic.AgregarPromocion(promo1);
             _promocionLogic.AgregarPromocion(promo2);

            IList<Promocion> listaPromociones = _promocionLogic.listarTodasLasPromociones();

            Assert.AreEqual(promo1.Id, listaPromociones.FirstOrDefault(x => x.Id == promo1.Id).Id);
            Assert.AreEqual(promo2.Id, listaPromociones.FirstOrDefault(x => x.Id == promo2.Id).Id);
        }

        [TestMethod]
        public void EncontrarPromocionPorIdTest()
        {
            _promocionLogic.AgregarPromocion(promo);

            Promocion promoPorFind = _promocionLogic.buscarPromocionPorId(promo.Id);

            Assert.AreEqual(promo.Id, promoPorFind.Id);
        }

        [TestMethod]
        public void BuscarPromocionPorIdQueNoExisteYDevuelvaNullTest()
        {
            var promocionNoEncontrada = _promocionLogic.buscarPromocionPorId(1);

            Assert.IsNull(promocionNoEncontrada);
            
        }
    }
}
