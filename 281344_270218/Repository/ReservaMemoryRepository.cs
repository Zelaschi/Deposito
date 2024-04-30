using Domain;

namespace Repository
{
    public class ReservaMemoryRepository : IRepository<Reserva>
    {
        private List<Reserva> _reservas = new List<Reserva>();
        public Reserva Add(Reserva reserva)
        {
            _reservas.Add(reserva);
            return reserva;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Reserva? Find(Func<Reserva, bool> filter)
        {
            throw new NotImplementedException();
        }

        public IList<Reserva> FindAll()
        {
            return _reservas;
        }

        public Reserva? Update(Reserva updatedEntity)
        {
            throw new NotImplementedException();
        }
    }
}
