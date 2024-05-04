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
            _reservas.RemoveAll(x => x.IdReserva == id);
        }

        public Reserva? Find(Func<Reserva, bool> filter)
        {
            return _reservas.Where(filter).FirstOrDefault();
        }

        public IList<Reserva> FindAll()
        {
            return _reservas;
        }

        public Reserva? Update(Reserva reservaActualizada)
        {
            Reserva reservaEncontrada = Find(x => x.IdReserva == reservaActualizada.IdReserva);

            if(reservaEncontrada != null) 
            {
                reservaEncontrada.Estado = reservaActualizada.Estado;
            }
            return reservaEncontrada;
        }
    }
}
