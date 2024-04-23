using Domain;

namespace Repository
{
    internal class ReservaMemoryRepository : IRepository<Reserva>
    {
        private List<Reserva> _reservas = new List<Reserva>();
        public Reserva Add(Reserva oneElement)
        {
            throw new NotImplementedException();
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
            throw new NotImplementedException();
        }

        public Reserva? Update(Reserva updatedEntity)
        {
            throw new NotImplementedException();
        }
    }
}
