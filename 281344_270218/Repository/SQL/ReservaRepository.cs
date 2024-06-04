

using Domain;

namespace Repository.SQL
{
    public class ReservaRepository : IRepository<Reserva>
    {
        private DepositoContext _repositorio;
        public ReservaRepository(DepositoContext repositorio)
        {
            _repositorio = repositorio;
        }

        public Reserva Add(Reserva reserva)
        {
            _repositorio.Reservas.Add(reserva);
            _repositorio.SaveChanges();
            return _repositorio.Reservas.FirstOrDefault(r => r.ReservaId == reserva.ReservaId);
        }

        public void Delete(int id)
        {
            Reserva reservaABorrar = _repositorio.Reservas.FirstOrDefault(r => r.ReservaId == id);
            _repositorio.Reservas.Remove(reservaABorrar);
            _repositorio.SaveChanges();
        }

        public Reserva? Find(Func<Reserva, bool> filter)
        {
            return _repositorio.Reservas.FirstOrDefault(filter);
        }

        public IList<Reserva> FindAll()
        {
            return _repositorio.Reservas.ToList();
        }

        public Reserva? Update(Reserva reservaActualizada)
        {
            Reserva reservaEncontrada = Find(r => r.ReservaId == reservaActualizada.ReservaId);
            if (reservaEncontrada != null) {
                _repositorio.Entry(reservaEncontrada).CurrentValues.SetValues(reservaActualizada);
                _repositorio.SaveChanges();
            }
            return Find(r => r.ReservaId == reservaActualizada.ReservaId);
        }
        public void DesasociarPago(Reserva reserva)
        {
            if (reserva.Pago != null)
            {
                _repositorio.Pagos.Remove(reserva.Pago);
                reserva.Pago = null;
                _repositorio.SaveChanges();
            }
        }

    }
}
