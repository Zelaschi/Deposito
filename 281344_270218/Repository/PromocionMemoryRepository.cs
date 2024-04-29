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
            _promociones.RemoveAll(x => x.Id == id);
        }

        public Promocion? Find(Func<Promocion, bool> filter)
        {
            return _promociones.Where(filter).FirstOrDefault();
        }

        public IList<Promocion> FindAll()
        {
            return _promociones;
        }

        public Promocion? Update(Promocion updatedEntity)
        {
            throw new NotImplementedException();
        }
    }
}
