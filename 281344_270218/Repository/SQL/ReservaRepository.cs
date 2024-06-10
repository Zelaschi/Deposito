

using Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repository.SQL.Exceptions;

namespace Repository.SQL
{
    public class ReservaRepository : IRepository<Reserva>
    {
        private readonly DepositoContext _repositorio;
        public ReservaRepository(DepositoContext repositorio)
        {
            try
            {
                _repositorio = repositorio;
            }
            catch (SqlException)
            {
                throw new DatabaseException("Error relacionado con la base de datos, por favor verifique la conexion y el estado del contenedor docker");
            }
        }

        public Reserva Add(Reserva reserva)
        {
            try
            {
                _repositorio.Reservas.Add(reserva);
                _repositorio.SaveChanges();
                return _repositorio.Reservas.Include(d => d.Deposito).Include(c => c.Cliente).Include(p => p.Pago).Include(pr => pr.PromocionAplicada).FirstOrDefault(r => r.ReservaId == reserva.ReservaId);
            }
            catch (SqlException)
            {
                throw new DatabaseException("Error relacionado con la base de datos, por favor verifique la conexion y el estado del contenedor docker");
            }
        }

        public void Delete(int id)
        {
            try
            {
                Reserva reservaABorrar = _repositorio.Reservas.FirstOrDefault(r => r.ReservaId == id);
                _repositorio.Reservas.Remove(reservaABorrar);
                _repositorio.SaveChanges();
            }
            catch (SqlException)
            {
                throw new DatabaseException("Error relacionado con la base de datos, por favor verifique la conexion y el estado del contenedor docker");
            }
        }

        public Reserva? Find(Func<Reserva, bool> filter)
        {
            try
            {
                return _repositorio.Reservas.Include(d => d.Deposito).Include(c => c.Cliente).Include(p => p.Pago).Include(pr => pr.PromocionAplicada).FirstOrDefault(filter);
            }
            catch (SqlException)
            {
                throw new DatabaseException("Error relacionado con la base de datos, por favor verifique la conexion y el estado del contenedor docker");
            }
        }

        public IList<Reserva> FindAll()
        {
            try
            {
                return _repositorio.Reservas.Include(d => d.Deposito).Include(c => c.Cliente).Include(p => p.Pago).Include(pr => pr.PromocionAplicada).ToList();
            }
            catch (SqlException)
            {
                throw new DatabaseException("Error relacionado con la base de datos, por favor verifique la conexion y el estado del contenedor docker");
            }
        }

        public Reserva? Update(Reserva reservaActualizada)
        {
            try
            {
                Reserva reservaEncontrada = Find(r => r.ReservaId == reservaActualizada.ReservaId);
                if (reservaEncontrada != null)
                {
                    _repositorio.Entry(reservaEncontrada).CurrentValues.SetValues(reservaActualizada);
                    _repositorio.SaveChanges();
                }
                return Find(r => r.ReservaId == reservaActualizada.ReservaId);
            }
            catch (SqlException)
            {
                throw new DatabaseException("Error relacionado con la base de datos, por favor verifique la conexion y el estado del contenedor docker");
            }
        }
        public void DesasociarPago(Reserva reserva)
        {
            try
            {
                if (reserva.Pago != null)
                {
                    _repositorio.Pagos.Remove(reserva.Pago);
                    reserva.Pago = null;
                    _repositorio.SaveChanges();
                }
            }
            catch (SqlException)
            {
                throw new DatabaseException("Error relacionado con la base de datos, por favor verifique la conexion y el estado del contenedor docker");
            }
        }

    }
}
