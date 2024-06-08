using Domain;

namespace DomainTest
{
    [TestClass]
    public class DepositoTests
    {
        private Deposito deposito;

        [TestInitialize]
        public void TestInitialize() {
            deposito = new Deposito("Nombre", "A", "Grande", true, DateTime.Now, DateTime.Now.AddDays(20));
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
        public void definirDepositoConNombreOkTest()
        {
            Deposito deposito = new Deposito("Nombre", "A", "Grande", true, DateTime.Now, DateTime.Now.AddDays(20));
        }

        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]

        public void definirDepositoConNombreConNumeroDeErrorTest()
        {
            Deposito deposito = new Deposito("Nombre1", "A", "Grande", true, DateTime.Now, DateTime.Now.AddDays(20));
        }
        [TestMethod]
        public void definirDepositoConFechasDeDisponibilidadOkTest()
        {
            Deposito deposito = new Deposito("Nombre", "A", "Grande", true, DateTime.Now, DateTime.Now.AddDays(20));
        }
        [TestMethod]
        public void agregarFechasNoDisponibleTest()
        {
            deposito.agregarFechaNoDisponible(DateTime.Now.AddDays(5), DateTime.Now.AddDays(15));
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void agregarFechasNoDisponibleFueraDeRangoDefinidoTest()
        {
            deposito.agregarFechaNoDisponible(DateTime.Now.AddDays(-2), DateTime.Now.AddDays(5));
        }
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void agregarFechasNoDisponibleAgregarEnFechaYaReservadaTest()
        {
            deposito.agregarFechaNoDisponible(DateTime.Now.AddDays(5), DateTime.Now.AddDays(15));
            deposito.agregarFechaNoDisponible(DateTime.Now.AddDays(7), DateTime.Now.AddDays(20));
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DepositoNoDebePermitirFechasInconsistentes()
        {
            new Deposito("DepositoPrueba", "A", "Grande", true, DateTime.Today.AddDays(3), DateTime.Today);
        }
        [TestMethod]
        [ExpectedException(typeof(ArgumentException))]
        public void DefinirDepositoConParametroDisponibleDesdeAnteriorALaFechaActualDeErrorTest() {
            new Deposito("DepositoPrueba", "A", "Grande", true, DateTime.Today.AddDays(-1), DateTime.Today);
        }

    }
}