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
                if (EstaRegistradoAdministrador() && aDTOCliente.Mail == ObtenerAdministrador().Mail) {
                    throw new Exception("Mail de administrador!");
                }
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
        public bool EstaRegistradoAdministrador()
        {
            try {
                ObtenerAdministrador();
                return true;
            }
            catch (Exception) {
                return false;
            }
        }
        public int RegistrarDeposito(DTODeposito aDTODeposito) 
        {
            try
            {
                Deposito aDeposito = new Deposito(aDTODeposito.Nombre, aDTODeposito.Area, aDTODeposito.Tamanio, aDTODeposito.Climatizacion, aDTODeposito.DisponibleDesde, aDTODeposito.DisponibleHasta);
                aDeposito.ValidarFechaInicioNoSeaAnteriorALaDeHoy(aDTODeposito.DisponibleDesde);
                _depositoLogic.AddDeposito(aDeposito);
                return aDeposito.DepositoId;
            }
            catch (ArgumentException e) 
            {
                throw new Exception(e.Message);
            }
            
        }

        public void AgregarPromocionADeposito(DTOPromocion aDTOPromocion, DTODeposito aDTODeposito)
        {
            try
            {
                Promocion aPromocion = _promocionLogic.buscarPromocionPorId(aDTOPromocion.PromocionId);
                Deposito aDeposito = _depositoLogic.buscarDepositoPorId(aDTODeposito.Id);
                aDeposito.AgregarPromocionADeposito(aPromocion);
            }
            catch(InvalidOperationException e)
            {
                throw new Exception(e.Message);
            }
        }

        public IList<DTODeposito> listarTodosLosDepositos()
        {
            IList<Deposito> listaDepositos = _depositoLogic.GetAll();
            List<DTODeposito> listaDTODepositos = new List<DTODeposito>();
            foreach (var deposito in listaDepositos)
            {
                var DTODeposito = new DTODeposito(deposito.DepositoId, deposito.Area, deposito.Tamanio, deposito.Climatizacion);
                listaDTODepositos.Add(DTODeposito);
            }
            return listaDTODepositos;
        }

        public DTODeposito BuscarDepositoPorId(int IdParametro)
        {
            var depositoEncontrado = _depositoLogic.buscarDepositoPorId(IdParametro);

            if (depositoEncontrado == null)
            {
                throw new Exception("Deposito no encontrado!");
            }

            var DTODepositoRetorno = new DTODeposito(depositoEncontrado.DepositoId, depositoEncontrado.Area, depositoEncontrado.Tamanio, depositoEncontrado.Climatizacion);

            return DTODepositoRetorno;
        }
        public void validarQueDepositoNoEsteAsociadoAReserva(DTODeposito aDTODeposito) 
        {
            Deposito depositoEncontradoPorId = _depositoLogic.buscarDepositoPorId(aDTODeposito.Id);
            IList<Reserva> Reservas = _reservaLogic.ListarTodasLasReservas();

            foreach (var reserva in Reservas)
            {
                if (reserva.Deposito.DepositoId == depositoEncontradoPorId.DepositoId)
                {
                    throw new Exception("No se puede eliminar un deposito que esta siendo utilizado para una reseva.");
                }
            }
        }

        public void EliminarDeposito(DTODeposito DTODepositoParametro)
        {
            Deposito depositoEncontradoPorId = _depositoLogic.buscarDepositoPorId(DTODepositoParametro.Id);
            validarQueDepositoNoEsteAsociadoAReserva(DTODepositoParametro);
            _depositoLogic.EliminarDeposito(depositoEncontradoPorId.DepositoId);
        }


        public int RegistrarPromocion(DTOPromocion aDTOPromocion)
        {
            try
            {
                Promocion aPromocion = new Promocion(aDTOPromocion.Etiqueta, aDTOPromocion.PorcentajeDescuento, aDTOPromocion.FechaInicio, aDTOPromocion.FechaFin);
                aPromocion.ValidarFechaInicioNoSeaAnteriorALaDeHoy(aDTOPromocion.FechaInicio);
                _promocionLogic.AgregarPromocion(aPromocion);
                return aPromocion.PromocionId;
            }
            catch (ArgumentException e)
            {
                throw new Exception(e.Message);
            }
        }

        public IList<DTOPromocion> listarTodasLasPromociones()
        {
            IList<Promocion> listaPromocines = _promocionLogic.listarTodasLasPromociones();
            List<DTOPromocion> listaDTOPromociones = new List<DTOPromocion>();
            foreach (var promocion in listaPromocines)
            {
                var DTOpromocion = new DTOPromocion(promocion.PromocionId, promocion.Etiqueta, promocion.PorcentajeDescuento, promocion.FechaInicio, promocion.FechaFin);
                listaDTOPromociones.Add(DTOpromocion);
            }
            return listaDTOPromociones;
        }
        private void validarQuePromocionNoEsteEnUso(DTOPromocion DTOPromocionParametro) {
            Promocion promocionEncontradaPorId = _promocionLogic.buscarPromocionPorId(DTOPromocionParametro.PromocionId);
            IList<Reserva> Reservas = _reservaLogic.ListarTodasLasReservas();

            foreach (var reserva in Reservas)
            {
                if (reserva.PromocionAplicada.PromocionId == promocionEncontradaPorId.PromocionId) {
                    throw new Exception("No se puede eliminar promocion que esta siendo utilizada para una reseva.");                
                }
            }
        }
        public void ElminarPromocion(DTOPromocion DTOPromocionParametro)
        {
            Promocion promocionEncontradaPorId = _promocionLogic.buscarPromocionPorId(DTOPromocionParametro.PromocionId);
            validarQuePromocionNoEsteEnUso(DTOPromocionParametro);

            _promocionLogic.EliminarPromocion(promocionEncontradaPorId.PromocionId);

        }

        public DTOPromocion BuscarPromocionPorId(int IdParametro)
        {
            try
            {
                var promoEncontrada = _promocionLogic.buscarPromocionPorId(IdParametro);

                var DTOPromocionRetorno = new DTOPromocion(promoEncontrada.PromocionId, promoEncontrada.Etiqueta, promoEncontrada.PorcentajeDescuento, promoEncontrada.FechaInicio, promoEncontrada.FechaFin);

                return DTOPromocionRetorno;
            }
            catch (NullReferenceException ) {
                throw new Exception("Promocion no encontrada!");
            }
            
        }

        public void ActualizarPromocion(DTOPromocion DTOPromocionParametro)
        {
            try
            {
                Promocion promocion = _promocionLogic.buscarPromocionPorId(DTOPromocionParametro.PromocionId);

                promocion.Etiqueta = DTOPromocionParametro.Etiqueta;
                promocion.FechaInicio = DTOPromocionParametro.FechaInicio;
                promocion.FechaFin = DTOPromocionParametro.FechaFin;
                promocion.PorcentajeDescuento = DTOPromocionParametro.PorcentajeDescuento;
            }
            catch (ArgumentException e)
            {
                throw new Exception(e.Message);
            }
        }

        public int RegistrarReserva(DTOReserva DTOReservaParametro) 
        {
            try
            {
                Deposito depositoEncontrado = _depositoLogic.buscarDepositoPorId(DTOReservaParametro.Deposito.Id);
                Cliente clienteEncontrado = _clienteLogic.buscarClientePorMail(DTOReservaParametro.Cliente.Mail);
                Reserva reservaAAgregar = new Reserva(DTOReservaParametro.FechaDesde, DTOReservaParametro.FechaHasta, depositoEncontrado, clienteEncontrado);
                Promocion promocionParaReserva = depositoEncontrado.mejorPromocionHoy();
                reservaAAgregar.PromocionAplicada = promocionParaReserva;

                _reservaLogic.AgregarReserva(reservaAAgregar);
                return reservaAAgregar.ReservaId;
            }
            catch (ArgumentException e)
            { 
                throw new Exception (e.Message);
            }
            
        }
        public DTOReserva BuscarReservaPorId(int idParametro)
        {
            Reserva reservaEncontrada = _reservaLogic.BuscarReservaPorId(idParametro);
            if (reservaEncontrada == null)
            {
                throw new Exception("Reserva no encontrada!");
            }

            DTOCliente clienteAuxiliar = new DTOCliente(reservaEncontrada.Cliente.NombreYApellido, reservaEncontrada.Cliente.Mail, reservaEncontrada.Cliente.Password);
            DTODeposito depositoAuxiliar = new DTODeposito(reservaEncontrada.Deposito.DepositoId, reservaEncontrada.Deposito.Area, reservaEncontrada.Deposito.Tamanio, reservaEncontrada.Deposito.Climatizacion);

            DTOReserva reservaRetorno = new DTOReserva(reservaEncontrada.ReservaId, reservaEncontrada.FechaDesde, reservaEncontrada.FechaHasta, depositoAuxiliar, clienteAuxiliar, reservaEncontrada.Precio);
            if (!reservaEncontrada.Estado.Equals("Rechazada")) {
                reservaRetorno.Pago = new DTOPago(reservaEncontrada.Pago.PagoId, reservaEncontrada.Pago.EstadoPago);
            }
            reservaRetorno.Estado = reservaEncontrada.Estado;

            return reservaRetorno; 
        }
        public IList<DTOReserva> ListarTodasLasReservas()
        {
            IList<Reserva> Reservas = _reservaLogic.ListarTodasLasReservas();
            List<DTOReserva> DTOReservas = new List<DTOReserva>();
            foreach (var reserva in Reservas)
            {
                DTOCliente clienteAuxiliar = new DTOCliente(reserva.Cliente.NombreYApellido, reserva.Cliente.Mail, reserva.Cliente.Password);
                DTODeposito depositoAuxiliar = new DTODeposito(reserva.Deposito.Nombre, reserva.Deposito.DepositoId, reserva.Deposito.Area, reserva.Deposito.Tamanio, reserva.Deposito.Climatizacion);
                DTOReserva reservaAuxiliar = new DTOReserva(reserva.ReservaId, reserva.FechaDesde, reserva.FechaHasta, depositoAuxiliar, clienteAuxiliar, reserva.Precio);
                if (reserva.Pago != null) {
                    reservaAuxiliar.Pago = new DTOPago(reserva.Pago.PagoId, reserva.Pago.EstadoPago);
                }
                reservaAuxiliar.Estado = reserva.Estado;
                DTOReservas.Add(reservaAuxiliar);
            }
            return DTOReservas;
        }
        public void PagarReserva(DTOReserva DTOReservaParametro) {
            try
            {
                Reserva ReservaEncontrada = _reservaLogic.BuscarReservaPorId(DTOReservaParametro.Id);
                _reservaLogic.PagarReserva(ReservaEncontrada);
            }
            catch (InvalidOperationException e)
            {
                throw new Exception(e.Message);
            }
        }

        public void AceptarReserva(DTOReserva DTOReservaParametro)
        {
            try
            {
                Reserva ReservaEncontrada = _reservaLogic.BuscarReservaPorId(DTOReservaParametro.Id);
                DateTime FechaHasta = ReservaEncontrada.FechaHasta;
                DateTime FechaDesde = ReservaEncontrada.FechaDesde;
                ReservaEncontrada.Deposito.agregarFechaNoDisponible(FechaDesde, FechaHasta);
                _reservaLogic.AceptarReserva(ReservaEncontrada);
            }
            catch (InvalidOperationException e) {
                throw new Exception(e.Message);
            }
        }
        public void RechazarReserva(DTOReserva DTOReservaParametro)
        {
            Reserva ReservaEncontrada = _reservaLogic.BuscarReservaPorId(DTOReservaParametro.Id);
            _reservaLogic.RechazarReserva(ReservaEncontrada);
        }


        public IList<DTOReserva> listarReservasDeCliente(DTOCliente aDTOCliente) {
            IList<Reserva> Reservas = _reservaLogic.ListarTodasLasReservas();
            List<DTOReserva> DTOReservas = new List<DTOReserva>();
            foreach (var reserva in Reservas)
            {
                if (reserva.Cliente.Mail.Equals(aDTOCliente.Mail))
                {
                    DTOCliente clienteAuxiliar = new DTOCliente(reserva.Cliente.NombreYApellido, reserva.Cliente.Mail, reserva.Cliente.Password);
                    DTODeposito depositoAuxiliar = new DTODeposito(reserva.Deposito.Nombre, reserva.Deposito.DepositoId, reserva.Deposito.Area, reserva.Deposito.Tamanio, reserva.Deposito.Climatizacion);
                    DTOReserva reservaAuxiliar = new DTOReserva(reserva.ReservaId, reserva.FechaDesde, reserva.FechaHasta, depositoAuxiliar, clienteAuxiliar, reserva.Precio);
                    if (reserva.Pago != null)
                    {
                        reservaAuxiliar.Pago = new DTOPago(reserva.Pago.PagoId, reserva.Pago.EstadoPago);
                    }
                    reservaAuxiliar.Estado = reserva.Estado;
                    DTOReservas.Add(reservaAuxiliar);
                }
            }
            return DTOReservas;

        }

        public void justificacionRechazo(String rechazo, DTOReserva DTOReservaParametro)
        {
            Reserva ReservaEncontrada = _reservaLogic.BuscarReservaPorId(DTOReservaParametro.Id);
            ReservaEncontrada.JustificacionRechazo = rechazo;
        }

        public bool LogIn(string Mail, string Pwd)
        {
            try
            {
                Administrador aAdmin = _administradorLogic.ObtenerAdministrador();
                if (Mail == aAdmin.Mail && aAdmin.Password == Pwd)
                {
                    return true;
                }
                Cliente aCliente = _clienteLogic.buscarClientePorMail(Mail);
                if (aCliente == null)
                {
                    throw new Exception("Cliente no encontrado!");

                }
                if (Mail == aCliente.Mail && Pwd == aCliente.Password) {
                    return true;
                }
                throw new Exception("Password incorrecta!");
            }
            catch (NullReferenceException)
            {
                throw new Exception("Cliente no encontrado!");

            }
            catch (InvalidOperationException e) {
                throw new Exception(e.Message);
            }
        }
        public bool esAdministrador(string mail) 
        {
            return ObtenerAdministrador().Mail.Equals(mail);
        }
        public IList<DTODeposito> DepositosDisponiblesParaReservaPorFecha(DateTime fechaDesde, DateTime fechaHasta)
        {
            try
            {
                List<DTODeposito> retorno = new List<DTODeposito>();
                IList<Deposito> depositos = _depositoLogic.GetAll();
                foreach (var deposito in depositos)
                {
                    if (deposito.validarDisponibilidadBool(fechaDesde, fechaHasta))
                    {
                        DTODeposito dtodeposito = new DTODeposito(deposito.Nombre, deposito.DepositoId, deposito.Area, deposito.Tamanio, deposito.Climatizacion);
                        retorno.Add(dtodeposito);
                    }
                }
                return retorno;
            }
            catch (ArgumentException e) {
                throw new Exception(e.Message);
            }
            
        }

        public void GenerarReporteReservas(string formato)
        {
            IList<Reserva> reservas = _reservaLogic.ListarTodasLasReservas();
            IExportador exportador;
            switch(formato.ToUpper())
            {
                case "TXT":
                    exportador = new ExportadorTXT();
                    break;
                case "CSV":
                    exportador = new ExportadorCSV();
                    break;
                default:
                    throw new Exception("Fromato no disponible");
            }
            exportador.Exportar(reservas);
        }
    }
}