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
        public void IncrementarIdCuandoSeCreaDepositoTest()
        {
            Deposito.ResetId();

            Deposito deposito1 = new Deposito("A", "Grande", false);
            Deposito deposito2 = new Deposito("C", "Mediano", false);
            Deposito deposito3 = new Deposito("B", "Pequenio", true);

            Assert.AreEqual(1, deposito1.IdDeposito);
            Assert.AreEqual(2, deposito2.IdDeposito);
            Assert.AreEqual(3, deposito3.IdDeposito);
        }
    }
}