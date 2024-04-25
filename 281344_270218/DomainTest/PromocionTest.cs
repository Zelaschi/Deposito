using Domain;
namespace DomainTest
{
    [TestClass]
    public class PromocionTest
    {
        

        [TestMethod]
        public void TestInitialize()
        {
            Promocion promocion = new Promocion(1, "promocion abril", 20, DateTime.Now, DateTime.Now.AddDays(10));
        }
    }
}
