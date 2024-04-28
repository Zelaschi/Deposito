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

            promo = new Promocion(0,"Promo",20,DateTime.Now, DateTime.Now.AddDays(10));
        }
        [TestMethod]
        public void AgregarPromocionTest() {
            _promocionLogic.AgregarPromocion(promo);
        }
    }
}
