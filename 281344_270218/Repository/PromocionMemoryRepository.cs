﻿using Domain;

namespace Repository
{
    internal class PromocionMemoryRepository : IRepository<Promocion>
    {
        private List<Promocion> _promociones= new List<Promocion>();
        public Promocion Add(Promocion oneElement)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Promocion? Find(Func<Promocion, bool> filter)
        {
            throw new NotImplementedException();
        }

        public IList<Promocion> FindAll()
        {
            throw new NotImplementedException();
        }

        public Promocion? Update(Promocion updatedEntity)
        {
            throw new NotImplementedException();
        }
    }
}
