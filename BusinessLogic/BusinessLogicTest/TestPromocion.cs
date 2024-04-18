using BusinessLogic;

namespace BusinessLogicTest
{
    [TestClass]
    public class TestPromocion
    {
        private Promocion promocion;

        [TestInitialize]
        public void initialize()
        {
            promocion = new Promocion("promocion abril", 20, DateTime.Now, DateTime.Now.AddDays(10));
        }
    }
}
