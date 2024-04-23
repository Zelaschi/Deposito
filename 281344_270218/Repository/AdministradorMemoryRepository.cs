using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class AdministradorMemoryRepository : IRepository<Administrador>
    {
        private Administrador _administrador;
        public Administrador Add(Administrador oneElement)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Administrador? Find(Func<Administrador, bool> filter)
        {
            throw new NotImplementedException();
        }

        public IList<Administrador> FindAll()
        {
            throw new NotImplementedException();
        }

        public Administrador? Update(Administrador updatedEntity)
        {
            throw new NotImplementedException();
        }
    }
}
