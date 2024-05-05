using Domain;
using BusinessLogic;
using Repository;


namespace BusinessLogicTest
{
    [TestClass]
    public class ReservaLogicTest
    {
        private ReservaLogic? _reservaLogic;
        private IRepository<Reserva> _reservaRepository;
        private Reserva? reserva;
        private int idReserva = 1;
        private Deposito? deposito;
        private int idDeposito = 1;
        private Cliente? cliente;
        private int idCliente = 1;


        [TestInitialize]

        public void setUp()
        {
            _reservaRepository = new ReservaMemoryRepository();
            _reservaLogic = new ReservaLogic(_reservaRepository);

            reserva = new Reserva(idReserva, DateTime.Today, DateTime.Today.AddDays(1), deposito, 100, cliente);
            deposito = new Deposito(idDeposito, "A", "Pequenio", true);
            cliente = new Cliente(idCliente, "Juan Perez", "juanperez@hotmail.com", "Pasword1!");
        }

        [TestMethod]
        public void AgregarReservaTest()
        {
            Reserva reservaRetorno = _reservaLogic.AgregarReserva(reserva);

            Assert.AreEqual(idReserva, reservaRetorno.IdReserva);
            Assert.AreEqual(reserva.FechaDesde, reservaRetorno.FechaDesde);
            Assert.AreEqual(reserva.FechaHasta, reservaRetorno.FechaHasta);
            Assert.AreEqual(reserva.Deposito, reservaRetorno.Deposito);
            Assert.AreEqual(reserva.Precio, reservaRetorno.Precio);
            Assert.AreEqual(reserva.Cliente, reservaRetorno.Cliente);
            Assert.AreEqual(reserva.Estado, reservaRetorno.Estado);
        }

        [TestMethod]

        public void ListarTodasLasReservasTest()
        {
            Reserva reservaRetorno = _reservaLogic.AgregarReserva(reserva);

            IList<Reserva> resultReservas = _reservaLogic.ListarTodasLasReservas();
            Assert.AreEqual(reserva.IdReserva, resultReservas.FirstOrDefault(x => x.IdReserva == reserva.IdReserva).IdReserva);
        }

        [TestMethod]

        public void EncontrarReservaPorIdTest()
        {
            _reservaLogic.AgregarReserva(reserva);

            Reserva reservaPorId = _reservaLogic.BuscarReservaPorId(reserva.IdReserva);

            Assert.AreEqual(reserva.IdReserva, reservaPorId.IdReserva);
        }

        [TestMethod]

        public void BuscarReservaPorIdQueNoExisteYDevuelvaNullTest()
        {
            var promocionNoEncontrada = _reservaLogic.BuscarReservaPorId(1);

            Assert.IsNull(promocionNoEncontrada);
        }

        [TestMethod]

        public void EliminarReservaTest()
        {
            _reservaLogic.AgregarReserva(reserva);

            _reservaLogic.EliminarReserva(reserva.IdReserva);

            var reservaEliminada = _reservaLogic.BuscarReservaPorId(reserva.IdReserva);

            Assert.IsNull(reservaEliminada);
        }

        [TestMethod]

        public void AceptarReservaTest()
        {
            _reservaLogic.AgregarReserva(reserva);
            string etiquetaAceptada = "Aceptada";
            Reserva reservaActualizada = new Reserva(reserva.IdReserva, reserva.FechaDesde, reserva.FechaHasta, reserva.Deposito, reserva.Precio, reserva.Cliente);
            reservaActualizada.Estado = etiquetaAceptada;

            Reserva reservaActualizadaRetorno = _reservaLogic.ActualizarReserva(reservaActualizada);

            Assert.AreEqual(reservaActualizada.IdReserva, reserva.IdReserva);
            Assert.AreEqual(reservaActualizada.FechaDesde, reserva.FechaDesde);
            Assert.AreEqual(reservaActualizada.FechaHasta, reserva.FechaHasta);
            Assert.AreEqual(reservaActualizada.Deposito, reserva.Deposito);
            Assert.AreEqual(reservaActualizada.Precio, reserva.Precio);
            Assert.AreEqual(reservaActualizada.Cliente, reserva.Cliente);
            Assert.AreEqual(reservaActualizada.Estado, "Aceptada");
        }

        [TestMethod]

        public void RechazarReservaTest()
        {
            _reservaLogic.AgregarReserva(reserva);
            string etiquetaAceptada = "Rechazada";
            Reserva reservaActualizada = new Reserva(reserva.IdReserva, reserva.FechaDesde, reserva.FechaHasta, reserva.Deposito, reserva.Precio, reserva.Cliente);
            reservaActualizada.Estado = etiquetaAceptada;

            Reserva reservaActualizadaRetorno = _reservaLogic.ActualizarReserva(reservaActualizada);

            Assert.AreEqual(reservaActualizada.IdReserva, reserva.IdReserva);
            Assert.AreEqual(reservaActualizada.FechaDesde, reserva.FechaDesde);
            Assert.AreEqual(reservaActualizada.FechaHasta, reserva.FechaHasta);
            Assert.AreEqual(reservaActualizada.Deposito, reserva.Deposito);
            Assert.AreEqual(reservaActualizada.Precio, reserva.Precio);
            Assert.AreEqual(reservaActualizada.Cliente, reserva.Cliente);
            Assert.AreEqual(reservaActualizada.Estado, "Rechazada");
        }

    }
}
