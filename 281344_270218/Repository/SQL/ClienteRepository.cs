using Domain;
using Microsoft.Data.SqlClient;
using Repository.SQL.Exceptions;
using System.Linq;

namespace Repository.SQL
{
    public class ClienteRepository : IRepository<Cliente>
    {
        private DepositoContext _repositorio;

        public ClienteRepository(DepositoContext repositorio)
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

        public Cliente Add(Cliente cliente)
        {
            try
            {
                _repositorio.Personas.Add(cliente);
                _repositorio.SaveChanges();
                return _repositorio.Personas.OfType<Cliente>().FirstOrDefault(c => c.Mail == cliente.Mail);
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
                Cliente clienteABorrar = _repositorio.Personas.OfType<Cliente>().FirstOrDefault(c => c.PersonaId == id);
                _repositorio.Personas.Remove(clienteABorrar);
                _repositorio.SaveChanges();
            }
            catch (SqlException )
            {
                throw new DatabaseException("Error relacionado con la base de datos, por favor verifique la conexion y el estado del contenedor docker");
            }
        }

        public Cliente? Find(Func<Cliente, bool> filter)
        {
            try
            {
                return _repositorio.Personas.OfType<Cliente>().FirstOrDefault(filter);
            }
            catch (SqlException )
            {
                throw new DatabaseException("Error relacionado con la base de datos, por favor verifique la conexion y el estado del contenedor docker");
            }
        }

        public IList<Cliente> FindAll()
        {
            try
            {
                return _repositorio.Personas.OfType<Cliente>().ToList();
            }
            catch (SqlException )
            {
                throw new DatabaseException("Error relacionado con la base de datos, por favor verifique la conexion y el estado del contenedor docker");
            }
        }

        public Cliente? Update(Cliente clienteActualizado)
        {
            try
            {
                Cliente clienteEncontrado = Find(x => x.PersonaId == clienteActualizado.PersonaId);

                if (clienteEncontrado != null)
                {
                    _repositorio.Entry(clienteEncontrado).CurrentValues.SetValues(clienteActualizado);
                    _repositorio.SaveChanges();
                }
                return Find(x => x.PersonaId == clienteActualizado.PersonaId);
            }
            catch (SqlException )
            {
                throw new DatabaseException("Error relacionado con la base de datos, por favor verifique la conexion y el estado del contenedor docker");
            }
        }
    }
}
