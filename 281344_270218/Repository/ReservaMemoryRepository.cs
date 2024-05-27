using Domain;
using System.Security.Cryptography.X509Certificates;

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
            _reservas.RemoveAll(x => x.ReservaId == id);
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
            Reserva reservaEncontrada = Find(x => x.ReservaId == reservaActualizada.ReservaId);

            if (reservaEncontrada != null) {
                reservaEncontrada.FechaDesde = reservaActualizada.FechaDesde;
                reservaEncontrada.FechaHasta = reservaActualizada.FechaHasta;
                reservaEncontrada.Deposito = reservaActualizada.Deposito;
                reservaEncontrada.Cliente = reservaActualizada.Cliente;
                reservaEncontrada.Precio = reservaEncontrada.CalculoPrecioDeReserva();
                reservaEncontrada.Estado = reservaActualizada.Estado;
            }
            return reservaEncontrada;
        }
    }
}
