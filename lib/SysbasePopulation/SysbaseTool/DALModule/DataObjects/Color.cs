﻿using KarveCar.Model.Generic;
using KarveCommon.Generic;
using static KarveCommon.Generic.Enumerations;

namespace KarveDataAccessLayer.DataObjects
{
    public class Color : GenericPropertyChanged, IDataGridRowChange
    {
        #region Constructores
        public Color() { this.ControlCambio = EControlCambio.Null; }
        public Color(string codigo, string definicion, string tipocolor, string ultimamodificacion, string usuario)
        {
            this.codigo = codigo;
            this.definicion = definicion;
            this.tipocolor  = tipocolor;
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
                OnPropertyChanged("Code");
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

        private string tipocolor;
        public string TipoColor
        {
            get { return tipocolor; }
            set
            {
                tipocolor = value;
                OnPropertyChanged("TipoColor");
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

        private EControlCambio controlcambiodatagrid;
        public EControlCambio ControlCambio
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
