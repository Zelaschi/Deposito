﻿using Domain;
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
            _depositos.RemoveAll(x => x.DepositoId == id);
        }

        public Deposito? Find(Func<Deposito, bool> filter)
        {
            return _depositos.Where(filter).FirstOrDefault();
        }

        public IList<Deposito> FindAll()
        {
            return _depositos;
        }

        public Deposito? Update(Deposito updatedEntity)
        {
            throw new InvalidOperationException();
        }
    }
}
