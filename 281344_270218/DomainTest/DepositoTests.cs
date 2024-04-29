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
        public void Deposito_Lanzar_Excepcion_Si_Invalido()
        {
            Deposito deposito = new Deposito("Z", "Gigante", true);
        }

        [TestMethod]
        public void Incrementar_Id_Cuando_Se_Crea_Deposito()
        {
            Deposito.ResetId();

            Deposito deposito1 = new Deposito("A", "Grande", false);
            Deposito deposito2 = new Deposito("C", "Mediano", false);
            Deposito deposito3 = new Deposito("B", "Pequenio", true);

            Assert.AreEqual(1, deposito1.IdDeposito);
            Assert.AreEqual(2, deposito2.IdDeposito);
            Assert.AreEqual(3, deposito3.IdDeposito);
        }

        [TestMethod]
        public void AgregarPromocionADepositoTest()
        {
            Promocion promoTest = new Promocion(0, "promo", 20, DateTime.Now , DateTime.Now.AddDays(10));
            Promocion promoReturn = deposito.AgregarPromocionADeposito(promoTest);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AgregarPromocionRepetidaTest()
        {
            Promocion promoTest = new Promocion(0, "promo", 20, DateTime.Now, DateTime.Now.AddDays(10));
            Promocion promoReturn1 = deposito.AgregarPromocionADeposito(promoTest);
            Promocion promoReturn2 = deposito.AgregarPromocionADeposito(promoTest);
        }

        [TestMethod]
        public void EliminarPromocionTest()
        {
            Promocion promoTest = new Promocion(0, "promo", 20, DateTime.Now, DateTime.Now.AddDays(10));
            Promocion promoReturn1 = deposito.AgregarPromocionADeposito(promoTest);
            deposito.EliminarPromocionDeDeposito(promoTest);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void EliminarPromocionQueNoExisteTest()
        {
            Promocion promoTest = new Promocion(0, "promo", 20, DateTime.Now, DateTime.Now.AddDays(10));
            deposito.EliminarPromocionDeDeposito(promoTest);
        }
    }
}