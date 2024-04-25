using Domain;
using Domain;

namespace Repository
{
    public class AdministradorMemoryRepository : IRepository<Administrador>
    {
        private Administrador _administrador;
        public Administrador Add(Administrador admin)
        {
            _administrador = admin;
            return admin;
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
