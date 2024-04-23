using Domain;

namespace Repository
{
    public class ClienteMemoryRepository : IRepository<Cliente>
    {
        private List<Cliente> _clientes = new List<Cliente>();
        public Cliente Add(Cliente cliente)
        {
            _clientes.Add(cliente);
            return cliente;
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Cliente? Find(Func<Cliente, bool> filter)
        {
            return _clientes.Where(filter).FirstOrDefault();
        }

        public IList<Cliente> FindAll()
        {
            return _clientes;
        }

        public Cliente? Update(Cliente updatedEntity)
        {
            throw new NotImplementedException();
        }
    }
}
