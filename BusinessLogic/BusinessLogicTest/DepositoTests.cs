using BusinessLogic; 

namespace BusinessLogicTest
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
        public void Deposito_InicializadoConValoresCorrectos()
        {
            Assert.AreEqual("A", deposito.Area);
            Assert.AreEqual("Grande", deposito.Tamanio);
            Assert.IsTrue(deposito.Climatizacion);
        }

        [TestMethod]

        public void Deposito_Lanzar_Excepcion_Si_Invalido()
        {
            var exepcion = Assert.ThrowsException<ArgumentException>(() => new Deposito("Z", "Gigante", true));
            Assert.AreEqual("Deposito invalido", exepcion.Message);
        }

        [TestMethod]
        public void Incrementar_Id_Cuando_Se_Crea_Deposito()
        {
            Deposito.ResetId();

            Deposito deposito1 = new Deposito("A", "Grande", false);
            Deposito deposito2 = new Deposito("C", "Mediano", false);
            Deposito deposito3 = new Deposito("B", "Pequenio", true);

            Assert.AreEqual(1, deposito1.Id);
            Assert.AreEqual(2, deposito2.Id);
            Assert.AreEqual(3, deposito3.Id);
        }

        [TestMethod]
        public void AgregarPromocionADepositoTest()
        {
            Promocion promoTest = new Promocion(0, "promo", 20, DateTime.Now , DateTime.Now.AddDays(10));
            Promocion promoReturn = deposito.AgregarPromocionADeposito(promoTest);
        }
    }
}