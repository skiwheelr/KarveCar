﻿using KarveCar.Model.Generic;
using KarveCommon.Generic;

namespace DataAccessLayer.DataObjects
{
    public class Divisa : GenericPropertyChanged, IDataGridRowChange
    {
        #region Constructores
        public Divisa() { this.ControlCambio = RecopilatorioEnumerations.EControlCambio.Null; }
        public Divisa(string codigo, string definicion, decimal compra, decimal venta, string ultimamodificacion, string usuario)
        {
            this.codigo = codigo;
            this.definicion = definicion;
            this.compra = compra;
            this.venta = venta;
            this.ultimamodificacion = ultimamodificacion;
            this.usuario = usuario;
        }
        #endregion

        #region Propiedades
        private string codigo;
        public string Codigo
        {
            get { return codigo; }
            set
            {
                codigo = value;
                OnPropertyChanged("Codigo");
            }
        }

        private string definicion;
        public string Definicion
        {
            get { return definicion; }
            set
            {
                definicion = value;
                OnPropertyChanged("Definicion");
            }
        }

        private decimal compra;
        public decimal Compra
        {
            get { return compra; }
            set
            {
                compra = value;
                OnPropertyChanged("Compra");
            }
        }

        private decimal venta;
        public decimal Venta
        {
            get { return venta; }
            set
            {
                venta = value;
                OnPropertyChanged("Venta");
            }
        }

        private string ultimamodificacion;
        public string UltimaModificacion
        {
            get { return ultimamodificacion; }
            set
            {
                ultimamodificacion = value;
                OnPropertyChanged("UltimaModificacion");
            }
        }

        private string usuario;
        public string Usuario
        {
            get { return usuario; }
            set
            {
                usuario = value;
                OnPropertyChanged("Usuario");
            }
        }

        private RecopilatorioEnumerations.EControlCambio controlcambiodatagrid;
        public RecopilatorioEnumerations.EControlCambio ControlCambio
        {
            get { return controlcambiodatagrid; }
            set
            {
                controlcambiodatagrid = value;
                OnPropertyChanged("ControlCambio");
            }
        }
        #endregion
    }
}
