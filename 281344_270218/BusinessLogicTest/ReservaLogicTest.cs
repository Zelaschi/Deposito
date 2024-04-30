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
    }
}
