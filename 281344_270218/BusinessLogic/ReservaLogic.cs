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
            return _repository.Find(x => x.ReservaId == id);
        }

        public void EliminarReserva(int id) 
        {
            _repository.Delete(id);
        }
        public Reserva ActualizarReserva(Reserva reservaActualizada)
        {
            return _repository.Update(reservaActualizada);
        }
        public void AceptarReserva(Reserva reservaParametro) 
        {
            reservaParametro.Estado = "Aceptada";
            _repository.Update(reservaParametro);
        }
        public void RechazarReserva(Reserva reservaParametro)
        {
            reservaParametro.Estado = "Rechazada";
            _repository.Update(reservaParametro);
        }

    }
}
