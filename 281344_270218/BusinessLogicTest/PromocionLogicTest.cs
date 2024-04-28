using Domain;
using BusinessLogic;
using Repository;

namespace BusinessLogicTest
{
    [TestClass]
    public class PromocionLogicTest
    {
        private ClienteLogic? _promocionLogic;
        private IRepository<Cliente>? _promocionRepository;
        private Promocion promo;

        [TestInitialize]
        public void setUp()
        {
            _promocionRepository = new PromocionMemoryRepository();
            _promocionLogic = new PromocionLogic(_clienteRepository);

            promo = new Promocion(0,"Promo",20,DateTime.Now, DateTime.Now.AddDays(10));
        }
    }
}
