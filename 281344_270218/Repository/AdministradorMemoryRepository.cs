using Domain;
using Domain;

namespace Repository
{
    public class AdministradorMemoryRepository : IRepository<Administrador>
    {
        private List<Administrador> _administrador = new List<Administrador>();
        public Administrador Add(Administrador admin)
        {
            _administrador.Add(admin);
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
            return _administrador;
        }

        public Administrador? Update(Administrador updatedEntity)
        {
            throw new NotImplementedException();
        }
    }
}
