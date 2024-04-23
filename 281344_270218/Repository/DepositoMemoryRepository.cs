using Domain;

namespace Repository
{
    public class DepositoMemoryRepository : IRepository<Deposito>
    {
        private List<Deposito> _depositos = new List<Deposito>();
        public Deposito Add(Deposito oneElement)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Deposito? Find(Func<Deposito, bool> filter)
        {
            throw new NotImplementedException();
        }

        public IList<Deposito> FindAll()
        {
            throw new NotImplementedException();
        }

        public Deposito? Update(Deposito updatedEntity)
        {
            throw new NotImplementedException();
        }
    }
}
