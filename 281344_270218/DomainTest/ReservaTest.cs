﻿using Domain;

namespace DomainTest
{
    [TestClass]
    public class ReservaTest
    {
        private Deposito deposito;
        private Cliente cliente;

        [TestInitialize]
        public void TestInitialize()
        {
            deposito = new Deposito("A", "Mediano", true);
            cliente = new Cliente("Juan Perez", "juan@hotmail.com", "Password1!");
        }

        [TestMethod]
        public void TestReservaDebeInicializarseConValoresCorrectos()
        {
            Reserva reserva = new Reserva(DateTime.Today, DateTime.Today.AddDays(1), deposito, 100, cliente);

            Assert.AreEqual(DateTime.Today, reserva.FechaDesde);
            Assert.AreEqual(DateTime.Today.AddDays(1), reserva.FechaHasta);
            Assert.AreEqual(deposito, reserva.Deposito);
            Assert.AreEqual(100, reserva.Precio);
            Assert.AreEqual("Pendiente", reserva.Estado);

        }

        [TestMethod]
        public void ReservaNoDebePermitirFechasInconsistentes()
        {
            Assert.ThrowsException<ArgumentException>(() => new Reserva(DateTime.Today.AddDays(1), DateTime.Today, deposito, 100, cliente), "Ingrese un periodo de fechas valido");
        }

        [TestMethod]
        public void calculoDePrecioDeDepositoMedianoUnDiaConRefrigeracion() { 
            Reserva reserva = new Reserva(DateTime.Today, DateTime.Today.AddDays(1), deposito, cliente);
            
            Assert.AreEqual(reserva.Precio, 95);
        }
        


    }
}
