using Repository;
using Domain;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Repository.SQL;

namespace BusinessLogic
{
    public class ClienteLogic
    {
        private readonly ClienteRepository _repository;

        public ClienteLogic(ClienteRepository repositorio)
        {
            _repository = repositorio;
        }

        private void validarClienteMailNoRepetido(Cliente clienteParametro)
        {
            if (_repository.Find(clienteBuscado => clienteBuscado.Mail == clienteParametro.Mail) != null)
            {
                throw new InvalidOperationException("Mail Repetido!");
            }
        }
        public Cliente AgregarCliente(Cliente cliente)
        {
            validarClienteMailNoRepetido(cliente);
            return _repository.Add(cliente);
        }

        public IList<Cliente> listarTodosLosClientes()
        {
            return _repository.FindAll();
        }

        public Cliente? buscarClientePorId(int IdParametro)
        {
            return _repository.Find(x => x.PersonaId == IdParametro);
        }

        public Cliente? buscarClientePorMail(string MailParametro)
        {
            return _repository.Find(x => x.Mail.Equals(MailParametro));
        }

        public void EliminarCliente(int id)
        {
            _repository.Delete(id);
        }
        public Cliente ActualizarInfoCliente(Cliente clienteActualizado)
        {
            return _repository.Update(clienteActualizado);
        }

    }
}

