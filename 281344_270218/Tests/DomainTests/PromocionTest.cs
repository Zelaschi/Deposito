using Domain;
namespace Tests.DomainTests
{
    [TestClass]
    public class PromocionTest
    {
        private Promocion promo;

        [TestInitialize]
        public void setUp() { 
            promo = new Promocion( "promocion abril", 20, DateTime.Now, DateTime.Now.AddDays(10));
        }
        [TestMethod]
        public void TestInitialize()
        {
            Promocion promocion = new Promocion( "promocion abril", 20, DateTime.Now, DateTime.Now.AddDays(10));
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void FechaFinOcurreAntesQueFechaInicioTest() {
            Promocion promocion = new Promocion( "promocion abril", 20, DateTime.Now.AddDays(10), DateTime.Now);
        }
        

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void Etiqueta21CaracteresTest()
        {
            string etiqueta21Caracteres = "Este es un string sencillo de 21 caracteres";
            promo.Etiqueta = etiqueta21Caracteres;
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void PromocionConMasDe75YMenosDe5() 
        {
            promo.PorcentajeDescuento = 1;
            promo.PorcentajeDescuento = 76;
        }
    }
}
