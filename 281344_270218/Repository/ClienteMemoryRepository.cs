using Domain;

namespace Repository
{
    public class ClienteMemoryRepository : IRepository<Cliente>
    {
        private List<Cliente> _clientes = new List<Cliente>();
        public Cliente Add(Cliente oneElement)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Cliente? Find(Func<Cliente, bool> filter)
        {
            throw new NotImplementedException();
        }

        public IList<Cliente> FindAll()
        {
            throw new NotImplementedException();
        }

        public Cliente? Update(Cliente updatedEntity)
        {
            throw new NotImplementedException();
        }
    }
}
