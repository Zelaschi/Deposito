
using Repository;
using Domain;


namespace BusinessLogic
{
    public class DepositoLogic
    {
        public readonly IRepository<Deposito> _repository;

        public DepositoLogic(IRepository<Deposito> DepositoRepository)
        {
            _repository = DepositoRepository;
        }

        public Deposito AddDeposito(Deposito deposito) 
        {
            return _repository.Add(deposito);
        }

        public IList<Deposito> GetAll() 
        { 
            return _repository.FindAll();
        }
    }
}
