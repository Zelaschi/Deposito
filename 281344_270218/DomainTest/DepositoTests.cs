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
        [TestCleanup]
        public void limpieza()
        {
            Deposito.UltimoID = 0;
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
            Deposito deposito1 = new Deposito("A", "Grande", false);
            Deposito deposito2 = new Deposito("C", "Mediano", false);
            Deposito deposito3 = new Deposito("B", "Pequenio", true);

            Assert.AreEqual(1, deposito1.DepositoId);
            Assert.AreEqual(2, deposito2.DepositoId);
            Assert.AreEqual(3, deposito3.DepositoId);
        }
    }
}