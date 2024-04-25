using Domain;
using Repository;

namespace BusinessLogic
{
    public class AdministradorLogic
    {
        private IRepository<Administrador> _repository;
        public AdministradorLogic(IRepository<Administrador> administradorRepository) {
            _repository = administradorRepository;
        }
    }
}
