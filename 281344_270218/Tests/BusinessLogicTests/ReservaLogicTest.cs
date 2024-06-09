using Domain;
using BusinessLogic;
using Repository;
using Repository.SQL;
using Repository.Context;
using Microsoft.EntityFrameworkCore.Storage;


namespace Tests.BusinessLogicTests
{
    [TestClass]
    public class ReservaLogicTest
    {
        private ReservaLogic? _reservaLogic;
        private ReservaRepository _reservaRepository;
        private readonly DepositoContextFactory _contextFactory = new DepositoContextEnMemoria();
        private DepositoContext _context;


        private Reserva? reserva;
        private int idReserva = 1;
        private Deposito? deposito;
        private Cliente? cliente;
        private Deposito? deposito1;


        [TestInitialize]

        public void setUp()
        {
            _context = _contextFactory.CrearContext();
            _reservaRepository = new ReservaRepository(_context);
            _reservaLogic = new ReservaLogic(_reservaRepository);

            deposito1 = new Deposito("DepositoPrueba", "A", "Pequenio", true, DateTime.Today, DateTime.Today.AddDays(3));
            deposito = new Deposito("A", "Pequenio", true);
            cliente = new Cliente(0, "Juan Perez", "juanperez@hotmail.com", "Pasword1!");
            reserva = new Reserva( DateTime.Today, DateTime.Today.AddDays(1), deposito1, cliente);
        }

        [TestCleanup]
        public void limpieza()
        {
            _context.Database.EnsureDeleted();
        }

        [TestMethod]
        public void AgregarReservaTest()
        {
            Reserva reservaRetorno = _reservaLogic.AgregarReserva(reserva);

            Assert.AreEqual(idReserva, reservaRetorno.ReservaId);
            Assert.AreEqual(reserva.FechaDesde, reservaRetorno.FechaDesde);
            Assert.AreEqual(reserva.FechaHasta, reservaRetorno.FechaHasta);
            Assert.AreEqual(reserva.Deposito, reservaRetorno.Deposito);
            Assert.AreEqual(reserva.Cliente, reservaRetorno.Cliente);
            Assert.AreEqual(reserva.Estado, reservaRetorno.Estado);
        }

        [TestMethod]

        public void ListarTodasLasReservasTest()
        {
            Reserva reservaRetorno = _reservaLogic.AgregarReserva(reserva);

            IList<Reserva> resultReservas = _reservaLogic.ListarTodasLasReservas();
            Assert.AreEqual(reserva.ReservaId, resultReservas.FirstOrDefault(x => x.ReservaId == reserva.ReservaId).ReservaId);
        }

        [TestMethod]

        public void EncontrarReservaPorIdTest()
        {
            _reservaLogic.AgregarReserva(reserva);

            Reserva reservaPorId = _reservaLogic.BuscarReservaPorId(reserva.ReservaId);

            Assert.AreEqual(reserva.ReservaId, reservaPorId.ReservaId);
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

            _reservaLogic.EliminarReserva(reserva.ReservaId);

            var reservaEliminada = _reservaLogic.BuscarReservaPorId(reserva.ReservaId);

            Assert.IsNull(reservaEliminada);
        }
        [TestMethod]
        public void ActualizarReservaTest() {
            _reservaLogic.AgregarReserva(reserva);

            DateTime fechaHastaActualizada = DateTime.Today.AddDays(20);
            string estadoActualizado = "Aceptada";

            Reserva reservaActualizda = new Reserva(reserva.FechaDesde, fechaHastaActualizada, deposito1, cliente);
            reservaActualizda.ReservaId = reserva.ReservaId;
            reservaActualizda.Estado = estadoActualizado;

            Reserva reservaActualizadaRetorno = _reservaLogic.ActualizarReserva(reservaActualizda);

            Assert.AreEqual(fechaHastaActualizada, reservaActualizadaRetorno.FechaHasta);
            Assert.AreEqual(estadoActualizado, reservaActualizadaRetorno.Estado);   
        }
        [TestMethod]
        public void ActualizarReservaQueNoExisteDevuelvaNull() {
            Reserva reservaNull = _reservaLogic.ActualizarReserva(reserva);
            
            Assert.IsNull(reservaNull);
        }

        [TestMethod]
        public void EliminarPagoDeReservaTest()
        {
            
            _reservaLogic.AgregarReserva(reserva);
            _reservaLogic.RechazarReserva(reserva);

            
            var reservaActualizada = _reservaRepository.Find(r => r.ReservaId == reserva.ReservaId);
            Assert.IsNotNull(reservaActualizada);
            Assert.AreEqual("Rechazada", reservaActualizada.Estado);
            Assert.IsNull(reservaActualizada.Pago);
        }
        [TestMethod]
        public void PagarReserva() {
            _reservaLogic.AgregarReserva(reserva);
            _reservaLogic.PagarReserva(reserva);
            Assert.AreEqual(reserva.Pago.EstadoPago, "Reservado");
        }
    }
}
