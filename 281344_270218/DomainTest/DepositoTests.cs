using Domain;

namespace DomainTest
{
    [TestClass]
    public class DepositoTests
    {
        private Deposito deposito;

        [TestInitialize]
        public void TestInitialize(){
            deposito = new Deposito("A", "Grande", true);
        }
        

        [TestMethod]
        public void DepositoInicializadoConValoresCorrectos()
        {
            Assert.AreEqual("A", deposito.Area);
            Assert.AreEqual("Grande", deposito.Tamanio);
            Assert.IsTrue(deposito.Climatizacion);
        }

        [TestMethod]

        [ExpectedException(typeof(ArgumentException))]
        public void DepositoLanzarExcepcionSiInvalido()
        {
            Deposito deposito = new Deposito("Z", "Gigante", true);
        }

        [TestMethod]
        public void definirDepositoConNombreOkTest()
        {
            Deposito deposito = new Deposito("Nombre", "A", "Grande", true);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]

        public void definirDepositoConNombreConNumeroDeErrorTest()
        {
            Deposito deposito = new Deposito("Nombre1", "A", "Grande", true);
        }
        
    }
}