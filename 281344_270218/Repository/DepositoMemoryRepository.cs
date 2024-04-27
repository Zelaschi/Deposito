using Domain;
using System.Runtime.CompilerServices;

namespace Repository
{
    public class DepositoMemoryRepository : IRepository<Deposito>
    {
        private List<Deposito> _depositos = new List<Deposito>();
        public Deposito Add(Deposito deposito)
        {
            _depositos.Add(deposito);
            return deposito;
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
            return _depositos;
        }

        public Deposito? Update(Deposito updatedEntity)
        {
            throw new NotImplementedException();
        }
    }
}
