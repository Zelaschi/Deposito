using BusinessLogic;
using Domain;
using Domain.Exceptions;
using System.Linq.Expressions;

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
            try
            {
                Cliente aCliente = new Cliente(aDTOCliente.NombreYApellido, aDTOCliente.Mail, aDTOCliente.Password);
                _clienteLogic.AgregarCliente(aCliente);
            }
            catch (ArgumentException e)
            {
                throw new Exception(e.Message);
            }
            catch (CampoNoPuedeSerVacioNiNull e)
            {
                throw new Exception(e.Message);
            }
            catch (InvalidOperationException e)
            {
                throw new Exception(e.Message);
            }

        }
        public IList<DTOCliente> listarTodosLosClientes()
        {
            IList<Cliente> listaClientes = _clienteLogic.listarTodosLosClientes();
            List<DTOCliente> listaDTOClientesRetorno = new List<DTOCliente>();
            foreach (var cliente in listaClientes)
            {
                var DTOcliente = new DTOCliente(cliente.NombreYApellido, cliente.Mail, cliente.Password);
                listaDTOClientesRetorno.Add(DTOcliente);
            }
            return listaDTOClientesRetorno;
        }

        public DTOCliente buscarClientePorMail(string mailParametro)
        {
            var clienteEncontrado = _clienteLogic.buscarClientePorMail(mailParametro);
            if (clienteEncontrado == null) {
                throw new Exception("Cliente no encontrado!");
            }
            var DTOClienteRetorno = new DTOCliente(clienteEncontrado.NombreYApellido, clienteEncontrado.Mail, clienteEncontrado.Password);

            return DTOClienteRetorno;
        }
        public DTOCliente buscarClientePorId(int IdParametro)
        {
            var clienteEncontrado = _clienteLogic.buscarClientePorId(IdParametro);

            if (clienteEncontrado == null)
            {
                throw new Exception("Cliente no encontrado!");
            }

            var DTOClienteRetorno = new DTOCliente(clienteEncontrado.NombreYApellido, clienteEncontrado.Mail, clienteEncontrado.Password);

            return DTOClienteRetorno;
        }
        public DTOCliente ActualizarInfoCliente(DTOCliente DTOClienteParametro) {

            Cliente clienteEncontradoPorMail = _clienteLogic.buscarClientePorMail(DTOClienteParametro.Mail);
            Cliente clienteActualizado = new Cliente(clienteEncontradoPorMail.IdPersona, DTOClienteParametro.NombreYApellido, DTOClienteParametro.Mail, DTOClienteParametro.Password);

            Cliente clienteRetornoActualizacion = _clienteLogic.ActualizarInfoCliente(clienteActualizado);
            DTOCliente DTOClienteRetorno = new DTOCliente(clienteRetornoActualizacion.NombreYApellido, clienteRetornoActualizacion.Mail, clienteRetornoActualizacion.Password);

            return DTOClienteRetorno;
        }
        public void EliminarCliente(DTOCliente DTOClienteParametro)
        { 
            Cliente clienteEncontradoPorMail = _clienteLogic.buscarClientePorMail(DTOClienteParametro.Mail);
            _clienteLogic.EliminarCliente(clienteEncontradoPorMail.IdPersona);
        }


        public void RegistrarAdministrador(DTOAdministrador aDTOAdministrador)
        {
            try
            {
                Administrador aAdministrador = new Administrador(0, aDTOAdministrador.NombreYApellido, aDTOAdministrador.Mail, aDTOAdministrador.Password);
                _administradorLogic.AsignarAdministrador(aAdministrador);
            }
            catch (ArgumentException e)
            {
                throw new Exception(e.Message);
            }
            catch (CampoNoPuedeSerVacioNiNull e)
            {
                throw new Exception(e.Message);
            }
            catch (InvalidOperationException e)
            {
                throw new Exception(e.Message);
            }
        }
        public DTOAdministrador ObtenerAdministrador() 
        {
            try
            {
                Administrador adminEncontrado = _administradorLogic.ObtenerAdministrador();
                DTOAdministrador DTOretorno = new DTOAdministrador(adminEncontrado.NombreYApellido, adminEncontrado.Mail, adminEncontrado.Password);

                return DTOretorno;
            }
            catch (InvalidOperationException e) 
            {
                throw new Exception(e.Message);
            }
            
        }
        public void RegistrarDeposito(DTODeposito aDTODeposito) 
        {
            Deposito aDeposito = new Deposito(aDTODeposito.Area, aDTODeposito.Tamanio, aDTODeposito.Climatizacion);
            _depositoLogic.AddDeposito(aDeposito);
        }


    }
}