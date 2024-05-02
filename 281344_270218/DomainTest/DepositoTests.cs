﻿using Domain;

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

        [TestMethod]
        public void AgregarPromocionADepositoTest()
        {
            Promocion promoTest = new Promocion("promo", 20, DateTime.Now , DateTime.Now.AddDays(10));
            Promocion promoReturn = deposito.AgregarPromocionADeposito(promoTest);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AgregarPromocionRepetidaTest()
        {
            Promocion promoTest = new Promocion("promo", 20, DateTime.Now, DateTime.Now.AddDays(10));
            Promocion promoReturn1 = deposito.AgregarPromocionADeposito(promoTest);
            Promocion promoReturn2 = deposito.AgregarPromocionADeposito(promoTest);
        }

        [TestMethod]
        public void EliminarPromocionTest()
        {
            Promocion promoTest = new Promocion("promo", 20, DateTime.Now, DateTime.Now.AddDays(10));
            Promocion promoReturn1 = deposito.AgregarPromocionADeposito(promoTest);
            deposito.EliminarPromocionDeDeposito(promoTest);
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void EliminarPromocionQueNoExisteTest()
        {
            Promocion promoTest = new Promocion("promo", 20, DateTime.Now, DateTime.Now.AddDays(10));
            deposito.EliminarPromocionDeDeposito(promoTest);
        }
        [TestMethod]
        public void AgregarPromocionADepositoYHayPromocionHoyTest()
        {

            Promocion promo = new Promocion("Promo Test", 20, DateTime.Now, DateTime.Now.AddDays(10));

            deposito.AgregarPromocionADeposito(promo);

            Promocion promocionEncontrada = deposito.hayPromocionHoy();

            Assert.AreEqual(promo.Id, promocionEncontrada.Id);
        }
        [TestMethod]
        public void BuscarPromocionDelDiaYQueDevuelvaLaMejorPromocionQueLeAplicaTest() 
        {
            Promocion promo1 = new Promocion("Promo1 Test", 20, DateTime.Now, DateTime.Now.AddDays(10));
            Promocion promo2 = new Promocion("Promo2 Test", 25, DateTime.Now, DateTime.Now.AddDays(10));
            Promocion promo3 = new Promocion("Promo3 Test", 45, DateTime.Now, DateTime.Now.AddDays(10));

            deposito.AgregarPromocionADeposito(promo1);
            deposito.AgregarPromocionADeposito(promo2);
            deposito.AgregarPromocionADeposito(promo3);

            Promocion promocionEncontrada = deposito.mejorPromocionHoy();
            Assert.AreEqual(promo3.Id, promocionEncontrada.Id);
        }
        [TestMethod]
        public void siNoTienePromocionHoyQueDevuelvaNull() 
        {
            Promocion promoNull = deposito.mejorPromocionHoy();

            Assert.IsNull(promoNull);
        }
    }
}