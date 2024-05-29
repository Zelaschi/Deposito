using Domain;

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
            _repositorio.Clientes.Add(cliente);
            _repositorio.SaveChanges();
            return _repositorio.Clientes.FirstOrDefault(c => c.Mail == cliente.Mail);
        }

        public void Delete(int id)
        {
            Cliente clienteABorrar = _repositorio.Clientes.FirstOrDefault(c => c.PersonaId == id);
            _repositorio.Clientes.Remove(clienteABorrar);
            _repositorio.SaveChanges();
        }

        public Cliente? Find(Func<Cliente, bool> filter)
        {
            return _repositorio.Clientes.FirstOrDefault(filter);
        }

        public IList<Cliente> FindAll()
        {
            return _repositorio.Clientes.ToList();
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
