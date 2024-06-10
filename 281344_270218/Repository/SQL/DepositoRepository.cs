using Domain;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Repository.SQL.Exceptions;


namespace Repository.SQL
{
    public class DepositoRepository : IRepository<Deposito>
    {
        private DepositoContext _repositorio;

        public DepositoRepository(DepositoContext repositorio)
        {
            try
            {
                _repositorio = repositorio;
            }
            catch (SqlException )
            {
                throw new DatabaseException("Error relacionado con la base de datos, por favor verifique la conexion y el estado del contenedor docker");
            }
        }

        public Deposito Add(Deposito deposito)
        {
            try
            {
                _repositorio.Depositos.Add(deposito);
                _repositorio.SaveChanges();
                return _repositorio.Depositos.Include(d => d.fechasNoDisponibles).FirstOrDefault(d => d.DepositoId == deposito.DepositoId);
            }
            catch (SqlException )
            {
                throw new DatabaseException("Error relacionado con la base de datos, por favor verifique la conexion y el estado del contenedor docker");
            }
        }

        public void Delete(int id)
        {
            try
            {
                Deposito depositoABorrar = _repositorio.Depositos.FirstOrDefault(d => d.DepositoId == id);
                _repositorio.Depositos.Remove(depositoABorrar);
                _repositorio.SaveChanges();
            }
            catch (SqlException )
            {
                throw new DatabaseException("Error relacionado con la base de datos, por favor verifique la conexion y el estado del contenedor docker");
            }
        }

        public Deposito? Find(Func<Deposito, bool> filter)
        {
            try
            {
                return _repositorio.Depositos.Include(d => d.fechasNoDisponibles).FirstOrDefault(filter);
            }
            catch (SqlException )
            {
                throw new DatabaseException("Error relacionado con la base de datos, por favor verifique la conexion y el estado del contenedor docker");
            }
        }

        public IList<Deposito> FindAll()
        {
            try
            {
                return _repositorio.Depositos.Include(d => d.fechasNoDisponibles).ToList();
            }
            catch (SqlException )
            {
                throw new DatabaseException("Error relacionado con la base de datos, por favor verifique la conexion y el estado del contenedor docker");
            }
        }

        public Deposito? Update(Deposito updatedEntity)
        {
            throw new InvalidOperationException();
        }
    }
}

