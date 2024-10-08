﻿using System.Data;
using System.Runtime.Intrinsics.Arm;
using System.Runtime.Serialization.Formatters;
using System.Text.RegularExpressions;

namespace Domain
{
    public class Deposito
    {
        public IList<DepositoPromocion> DepositoPromocions { get; set; }
        public IList<Reserva> Reservas { get; set; }

        public int DepositoId { get; set; }

        private string _area;
        public string Area
        {
            get
            {
                return _area;
            }
            set
            {
                string[] areasPosibles = { "A", "B", "C", "D", "E" };
                if (areasPosibles.Contains(value))
                {
                    _area = value;
                }
                else
                {
                    throw new ArgumentException("El area tiene que estar dentro de los valores validos");
                }
            }
        }
        private string _tamanio;
        public string Tamanio
        {
            get
            {
                return _tamanio;
            }
            set
            {
                string[] tamaniosPosibles = { "Pequenio", "Mediano", "Grande" };
                if (tamaniosPosibles.Contains(value))
                {
                    _tamanio = value;
                }
                else
                {
                    throw new ArgumentException("El tamanio tiene que estar dentro de los valores validos");
                }
            }
        }
        public bool Climatizacion { get; set; }
        private string _nombre { get; set; }
        public string Nombre
        {
            get { return _nombre; }
            set
            {
                string patronSoloLetras = "^[a-zA-Z]+$";
                if (Regex.IsMatch(value, patronSoloLetras))
                {
                    _nombre = value;
                }
                else
                {
                    throw new ArgumentException("El nombre del deposito solo admite letras.");
                }
            }
        }
        public List<FechasNoDisponible> fechasNoDisponibles { get; set; }
        private bool ValidarFechaInicioSeaAnteriorAFechaFin(DateTime fechaDesde, DateTime fechaHasta)
        {
             return fechaDesde < fechaHasta;
        }
        public void ValidarFechaInicioNoSeaAnteriorALaDeHoy(DateTime fechaInicio) {
            if (fechaInicio < DateTime.Today) {
                throw new ArgumentException("La fecha de inicio debe ser posterior o igual que la actual.");
            }
        }
        public Deposito(string nombre, string area, string tamanio, bool climatizacion, DateTime disponibleDesde, DateTime disponibleHasta)
        {
            if (!ValidarFechaInicioSeaAnteriorAFechaFin(disponibleDesde, disponibleHasta))
            {
                throw new ArgumentException("La fecha de inicio debe ser anterior que la fecha de fin.");
            }
            DepositoId = 0;
            Area = area;
            Tamanio = tamanio;
            Climatizacion = climatizacion;
            DepositoPromocions = new List<DepositoPromocion>();
            Nombre = nombre;
            fechasNoDisponibles = new List<FechasNoDisponible>
            {
                new FechasNoDisponible(DateTime.MinValue, disponibleDesde.AddDays(-1)),
                new FechasNoDisponible(disponibleHasta.AddDays(1), DateTime.MaxValue)
            };
        }


        public Deposito(string area, string tamanio, bool climatizacion)
        {
            Nombre = "undefinded";
            DepositoId = 0;
            Area = area;
            Tamanio = tamanio;
            Climatizacion = climatizacion;
            DepositoPromocions = new List<DepositoPromocion>();
            fechasNoDisponibles = new List<FechasNoDisponible>();
        }
        private void validarDisponibilidad(DateTime fechaDesde, DateTime fechaHasta)
        {

            foreach (var par in fechasNoDisponibles)
            {
                if ((fechaDesde >= par.FechaDesde && fechaDesde <= par.FechaHasta) || (fechaHasta >= par.FechaDesde && fechaHasta <= par.FechaHasta))
                {
                    throw new InvalidOperationException("El deposito no se encuentra disponible en la fecha ingresada");
                }
            }
        }
        public bool validarDisponibilidadBool(DateTime fechaDesde, DateTime fechaHasta)
        {
            if (!ValidarFechaInicioSeaAnteriorAFechaFin(fechaDesde, fechaHasta))
            {
                throw new ArgumentException("Disponible desde debe ser mayor a disponible hasta");
            }
            foreach (var par in fechasNoDisponibles)
            {
                if ((fechaDesde >= par.FechaDesde && fechaDesde <= par.FechaHasta) || (fechaHasta >= par.FechaDesde && fechaHasta <= par.FechaHasta))
                {
                    return false;
                }
            }
            return true;
        }

        public void agregarFechaNoDisponible(DateTime fechaDesde, DateTime fechaHasta)
        {
            if (!ValidarFechaInicioSeaAnteriorAFechaFin(fechaDesde, fechaHasta))
            {
                throw new ArgumentException("Disponible desde debe ser mayor a disponible hasta");
            }
            validarDisponibilidad(fechaDesde, fechaHasta);
            fechasNoDisponibles.Add(new FechasNoDisponible(fechaDesde, fechaHasta));
        }
        public Promocion AgregarPromocionADeposito(Promocion promoParametro)
        {
            if (DepositoPromocions.FirstOrDefault(dp => dp.PromocionId == promoParametro.PromocionId) != null)
            {
                throw new InvalidOperationException("El elemento ya existe en la lista.");
            }

            DepositoPromocions.Add(new DepositoPromocion { Deposito = this, DepositoId = this.DepositoId, Promocion = promoParametro, PromocionId = promoParametro.PromocionId });
            return promoParametro;
        }

        public Promocion? mejorPromocionHoy()
        {
            int mejorDescuento = 0;
            Promocion mejorPromocion = null;

            foreach (var depositoPromocion in DepositoPromocions)
            {
                var promocion = depositoPromocion.Promocion;
                if (promocion.PorcentajeDescuento > mejorDescuento)
                {
                    mejorDescuento = promocion.PorcentajeDescuento;
                    mejorPromocion = promocion;
                }
            }
            return mejorPromocion;
        }


    }
}

