using BusinessLogic;

namespace BusinessLogicTest
{
    [TestClass]
    public class ReservaTest
    {
        [TestMethod]
        public void Reserva_Debe_Inicializarse_Con_Valores_Correctos()
        {
            var deposito = new Deposito("A", "Mediano", true, 1);
            var reserva = new Reserva(1, DateTime.Today, DateTime.Today.addDays(1), deposito, 100);

            Assert.AreEqual(1, reserva.Id);
            Assert.AreEqual(DateTime.Today, reserva.FechaDesde);
            Assert.AreEqual(DateTime.Today.AddDays(1), reserva.FechaHasta);
            Assert.AreEqual(deposito, reserva.Deposito);
            Assert.AreEqual(100, reserva.precio);
            Assert.AreEqual("Pendiente", reserva.Estado);
        }
    }
}
