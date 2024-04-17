using BusinessLogic; 

namespace BusinessLogicTest
{
    [TestClass]
    public class DepositoTests
    {
        private Deposito deposito;

        [TestInitialize]
        public void TestInitialize(){
            deposito = new Deposito("A", "Grande", "Si");
        }

        [TestMethod]
        public void Deposito_InicializadoConValoresCorrectos()
        {
            Assert.AreEqual("A", deposito.Area);
            Assert.AreEqual("Grande", deposito.Tamanio);
            Assert.AreEqual("Si", deposito.Climatizacion);
        }
    }
}
