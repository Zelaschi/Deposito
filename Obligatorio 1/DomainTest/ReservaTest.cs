using Domain;

namespace BusinessLogicTest
{
    [TestClass]
    public class ReservaTest
    {
        private Deposito deposito;

        [TestInitialize]
        public void TestInitialize()
        {
            deposito = new Deposito("A", "Mediano", true);
        }

        [TestMethod]
        public void TestReservaDebeInicializarseConValoresCorrectos()
        {
            Reserva reserva = new Reserva(DateTime.Today, DateTime.Today.AddDays(1), deposito, 100);

            Assert.AreEqual(DateTime.Today, reserva.FechaDesde);
            Assert.AreEqual(DateTime.Today.AddDays(1), reserva.FechaHasta);
            Assert.AreEqual(deposito, reserva.Deposito);
            Assert.AreEqual(100, reserva.Precio);
            Assert.AreEqual("Pendiente", reserva.Estado);
        }

        [TestMethod]
        public void Reserva_No_Debe_Permitir_Fechas_Inconsistentes()
        {
            Assert.ThrowsException<ArgumentException>(() => new Reserva(DateTime.Today.AddDays(1), DateTime.Today, deposito, 100), "Ingrese un periodo de fechas valido");
        }
    }
}
