using Repository;
using Domain;

namespace BusinessLogic
{
    public class ReservaLogic
    {
        private readonly IRepository<Reserva> _repository;

        public ReservaLogic(IRepository<Reserva> reservaRepository)
        {
            _repository = reservaRepository;
        }

        public Reserva AgregarReserva(Reserva reserva)
        {
            return _repository.Add(reserva);
        }

        public IList<Reserva> ListarTodasLasReservas()
        {
            return _repository.FindAll();
        }

        public Reserva? BuscarReservaPorId(int id) 
        {
            return _repository.Find(x => x.IdReserva == id);
        }

        public void EliminarReserva(int id) 
        {
            _repository.Delete(id);
        }
        public void AceptarReserva(Reserva reservaParametro) 
        {
            Reserva reservaEncontrada = BuscarReservaPorId(reservaParametro.IdReserva);
            reservaEncontrada.Estado = "Aceptada";
        }
    }
}
