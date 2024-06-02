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
        [TestMethod]
        public void calculoDePrecioReservaGrandeSinRefrigeracionDe10DiasConPromocionDe20PorcientoTest() {
            Deposito depositoTest = new Deposito("A", "Grande", false);
            Promocion promoTest = new Promocion("etiqueta", 20, DateTime.Today, DateTime.Today.AddDays(10));
            depositoTest.AgregarPromocionADeposito(promoTest);

            Reserva reserva = new Reserva(DateTime.Today, DateTime.Today.AddDays(10), depositoTest, cliente);

            Assert.AreEqual(reserva.Precio, 760);
        }
        [TestMethod]
        public void calculoDePreciOReservaGrandeConRefrigeracionDe3DiasConVariasPromocionesQueAplicanElMismoDiaQueApliqueLaMejorTest() 
        {
            Deposito depositoTest = new Deposito("A", "Grande", true);
            Promocion promo1Test = new Promocion(0, "etiqueta", 20, DateTime.Today, DateTime.Today.AddDays(10));
            Promocion promo2Test = new Promocion(0, "etiqueta", 10, DateTime.Today, DateTime.Today.AddDays(10));
            Promocion promo3Test = new Promocion(0, "etiqueta", 70, DateTime.Today, DateTime.Today.AddDays(10));

            depositoTest.AgregarPromocionADeposito(promo1Test);
            depositoTest.AgregarPromocionADeposito(promo2Test);
            depositoTest.AgregarPromocionADeposito(promo3Test);

            Reserva reserva = new Reserva(DateTime.Today, DateTime.Today.AddDays(3), depositoTest, cliente);

            Assert.AreEqual(reserva.Precio, 108);
        }
        [TestMethod]
        public void quePromocionAplicadaSeaLaQueSeLeAplico()
        {
            Deposito depositoTest = new Deposito("A", "Grande", true);
            Promocion promo1Test = new Promocion("etiqueta", 20, DateTime.Today, DateTime.Today.AddDays(10));
            Promocion promo2Test = new Promocion("etiqueta", 10, DateTime.Today, DateTime.Today.AddDays(10));
            Promocion promo3Test = new Promocion("etiqueta", 70, DateTime.Today, DateTime.Today.AddDays(10));

            depositoTest.AgregarPromocionADeposito(promo1Test);
            depositoTest.AgregarPromocionADeposito(promo2Test);
            depositoTest.AgregarPromocionADeposito(promo3Test);

            Reserva reserva = new Reserva(DateTime.Today, DateTime.Today.AddDays(3), depositoTest, cliente);

            Assert.AreEqual(reserva.PromocionAplicada.PromocionId, promo3Test.PromocionId);
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void justificacionReservaRechazadaMayora300caracteresTest() 
        {
            Reserva reserva = new Reserva(DateTime.Today, DateTime.Today.AddDays(1), deposito, 100, cliente);
            string justificacionMayorA300 = "Lorem ipsum dolor sit amet, consectetuer adipiscing elit. Aenean commodo ligula eget dolor. Aenean massa. Cum sociis natoque penatibus et magnis dis parturient montes, nascetur ridiculus mus. Donec quam felis, ultricies nec, pellentesque eu, pretium quis, sem. Nulla consequat massa quis enim. Donec p";
            reserva.JustificacionRechazo = justificacionMayorA300;
        }

    }
}
