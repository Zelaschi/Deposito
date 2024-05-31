using Domain;
using System.Linq;

namespace Repository.SQL
{
    public class ClienteRepository : IRepository<Cliente>
    {
        private DepositoContext _repositorio;

        public ClienteRepository(DepositoContext repositorio)
        {
            _repositorio = repositorio;
        }

        public Cliente Add(Cliente cliente)
        {
            _repositorio.Personas.Add(cliente);
            _repositorio.SaveChanges();
            return _repositorio.Personas.OfType<Cliente>().FirstOrDefault(c => c.Mail == cliente.Mail);
        }

        public void Delete(int id)
        {
            Cliente clienteABorrar = _repositorio.Personas.OfType<Cliente>().FirstOrDefault(c => c.PersonaId == id);
            _repositorio.Personas.Remove(clienteABorrar);
            _repositorio.SaveChanges();
        }

        public Cliente? Find(Func<Cliente, bool> filter)
        {
            return _repositorio.Personas.OfType<Cliente>().FirstOrDefault(filter);
        }

        public IList<Cliente> FindAll()
        {
            return _repositorio.Personas.OfType<Cliente>().ToList();
        }

        public Cliente? Update(Cliente clienteActualizado)
        {
            Cliente clienteEncontrado = Find(x => x.PersonaId == clienteActualizado.PersonaId);

            if (clienteEncontrado != null)
            {
                _repositorio.Entry(clienteEncontrado).CurrentValues.SetValues(clienteActualizado);
                _repositorio.SaveChanges();
            }
            return Find(x => x.PersonaId == clienteActualizado.PersonaId);
        }
    }
}
