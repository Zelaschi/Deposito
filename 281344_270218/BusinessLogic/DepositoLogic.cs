
using Repository;
using Domain;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Repository.SQL;

namespace BusinessLogic
{
    public class DepositoLogic
    {
        public readonly DepositoRepository _repository;

        public DepositoLogic(DepositoRepository repositorio)
        {
            _repository = repositorio;
        }

        public Deposito AddDeposito(Deposito deposito) 
        {
            return _repository.Add(deposito);
        }

        public IList<Deposito> GetAll() 
        { 
            return _repository.FindAll();
        }

        public Deposito? buscarDepositoPorId(int idParametro)
        {
            return _repository.Find(x => x.DepositoId ==  idParametro);
        }

        public void EliminarDeposito(int id)
        {
            _repository.Delete(id);
        }
        

    }
}
