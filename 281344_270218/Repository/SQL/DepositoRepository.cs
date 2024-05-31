using Domain;

namespace Repository.SQL
{
    public class DepositoRepository : IRepository<Deposito>
    {
        private DepositoContext _repositorio;

        public DepositoRepository(DepositoContext repositorio)
        {
            _repositorio = repositorio;
        }

        public Deposito Add(Deposito deposito)
        {
            _repositorio.Depositos.Add(deposito);
            _repositorio.SaveChanges(); 
            return _repositorio.Depositos.FirstOrDefault(d => d.DepositoId == deposito.DepositoId);
        }

        public void Delete(int id)
        {
            Deposito depositoABorrar = _repositorio.Depositos.FirstOrDefault(d => d.DepositoId == id);
            _repositorio.Depositos.Remove(depositoABorrar);
            _repositorio.SaveChanges();
        }

        public Deposito? Find(Func<Deposito, bool> filter)
        {
            return _repositorio.Depositos.FirstOrDefault(filter);
        }

        public IList<Deposito> FindAll()
        {
            return _repositorio.Depositos.ToList();
        }

        public Deposito? Update(Deposito updatedEntity)
        {
            throw new InvalidOperationException();
        }
    }
}

