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
        public Administrador AsignarAdministrador(Administrador admin)
        {
            if (_repository.FindAll().Count > 0) {
                throw new InvalidOperationException("Ya existe un administrador, utilice ActualizarInFoAdministrador");
            }
            return _repository.Add(admin);
        }

        public IList<Administrador>? getAdministrador() {
            return _repository.FindAll();
        }
        public Administrador ActualizarInfoAdministrador(Administrador administradorActualizado) {
            return _repository.Update(administradorActualizado);
        }
    }
}
