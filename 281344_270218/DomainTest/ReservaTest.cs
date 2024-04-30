using Domain;

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
        public void calculoDePrecioDeReservaMedianoUnDiaConRefrigeracionTest() {
            Reserva reserva = new Reserva(DateTime.Today, DateTime.Today.AddDays(1), deposito, cliente);

            Assert.AreEqual(reserva.Precio, 95);
        }
        [TestMethod]
        public void calculoDePrecioDeReservaChicoUnDiaConRefrigeracionTest()
        {
            Deposito depositoTest = new Deposito("A", "Pequenio", true);
            Reserva reserva = new Reserva(DateTime.Today, DateTime.Today.AddDays(1), depositoTest, cliente);

            Assert.AreEqual(reserva.Precio, 70);
        }
        [TestMethod]
        public void calculoDePrecioDeReservaMedianoConRefrigeracionDe7DiasTest()
        {
            Reserva reserva = new Reserva(DateTime.Today, DateTime.Today.AddDays(7), deposito, cliente);

            Assert.AreEqual(reserva.Precio, 631);
        }
        [TestMethod]
        public void calculoDePrecioDeReservaMedianoConRefrigeracionDe15DiasTest() 
        {
            Reserva reserva = new Reserva(DateTime.Today, DateTime.Today.AddDays(15), deposito, cliente);

            Assert.AreEqual(reserva.Precio, 1282);
        }


    }
}
