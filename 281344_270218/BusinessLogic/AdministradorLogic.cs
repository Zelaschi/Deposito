using Domain;
using Repository;
using Repository.SQL;

namespace BusinessLogic
{
    public class AdministradorLogic
    {
        private readonly AdministradorRepository _repository;
        public AdministradorLogic(AdministradorRepository repositorio) {
            _repository = repositorio;
        }
        public Administrador AsignarAdministrador(Administrador admin)
        {
            if (_repository.FindAll().Count > 0) {
                throw new InvalidOperationException("Ya existe un administrador, utilice ActualizarInFoAdministrador");
            }
            return _repository.Add(admin);
        }

        private IList<Administrador>? getAdministrador() {
            return _repository.FindAll();
        }
        public Administrador ObtenerAdministrador()
        {
            IList<Administrador> administradors = getAdministrador();
            if (administradors.Count == 0)
            {
                throw new InvalidOperationException("Administrador No Registrado");
            }
            return administradors.FirstOrDefault();
        }
        public Administrador ActualizarInfoAdministrador(Administrador administradorActualizado) {
            return _repository.Update(administradorActualizado);
        }
    }
}
