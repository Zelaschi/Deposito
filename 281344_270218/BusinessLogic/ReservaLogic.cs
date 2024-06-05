using Repository;
using Domain;
using Repository.SQL;

namespace BusinessLogic
{
    public class ReservaLogic
    {
        private readonly ReservaRepository _repository;

        public ReservaLogic(ReservaRepository repositorio)
        {
            _repository = repositorio;
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
            reservaParametro.Pago.EstadoPago = "Capturado";
            _repository.Update(reservaParametro);
        }
        public void PagarReserva(Reserva reservaParametro) {
            reservaParametro.Pago.EstadoPago = "Reservado";
            _repository.Update(reservaParametro);
        }
        public void RechazarReserva(Reserva reservaParametro)
        {
            _repository.DesasociarPago(reservaParametro);
            reservaParametro.Estado = "Rechazada";
            _repository.Update(reservaParametro);
        }

    }
}
