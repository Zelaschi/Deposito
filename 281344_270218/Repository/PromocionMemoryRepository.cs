using Domain;

namespace Repository
{
    public class PromocionMemoryRepository : IRepository<Promocion>
    {
        private List<Promocion> _promociones= new List<Promocion>();
        public Promocion Add(Promocion promocion)
        {
            _promociones.Add(promocion);
            return promocion;
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
