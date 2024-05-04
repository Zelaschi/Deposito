using BusinessLogic;
using Domain;

namespace ControllerLayer
{
    public class Controller
    {
        private readonly ClienteLogic _clienteLogic;
        private readonly AdministradorLogic _administradorLogic;
        private readonly DepositoLogic _depositoLogic;
        private readonly PromocionLogic _promocionLogic;
        private readonly ReservaLogic _reservaLogic;

        public Controller(AdministradorLogic administradorLogic, ClienteLogic clienteLogic, DepositoLogic depositoLogic, PromocionLogic promocionLogic, ReservaLogic reservaLogic)
        {
            _administradorLogic = administradorLogic;
            _clienteLogic = clienteLogic;
            _depositoLogic = depositoLogic;
            _promocionLogic = promocionLogic;
            _reservaLogic = reservaLogic; 
        }

        public void RegistrarCliente(DTOCliente aDTOCliente)
        {
            Cliente aCliente = new Cliente(aDTOCliente.NombreYApellido, aDTOCliente.Mail, aDTOCliente.Password);
            _clienteLogic.AgregarCliente(aCliente);
        }
    }
}
