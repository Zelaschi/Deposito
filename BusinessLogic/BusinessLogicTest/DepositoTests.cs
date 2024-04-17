using BusinessLogic; 

namespace BusinessLogicTest
{
    [TestClass]
    public class TestDeposito
    {
        private Deposito deposito;

        [TestInitialize]
        public void TestInitialize(){
            deposito = new Deposito("A", "Grande", "Si");
        }

        [TestMethod]
        public void Deposito_InicializadoConValoresCorrectos()
        {
            Assert.AreEqual("b", deposito.Area);
            Assert.AreEqual("Grande", deposito.Tamanio);
            Assert.AreEqual("Si", deposito.Climatizacion);
        }

        [TestMethod]
        public void Area_DebeSerValida()
        {
            bool esValida = deposito.EsValidaArea();
            Assert.IsTrue(esValida);
        }
    }
}
