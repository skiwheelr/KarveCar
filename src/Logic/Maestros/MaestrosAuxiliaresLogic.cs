﻿using KarveCar.Logic.Generic;
using KarveCar.Logic.ToolBar;
using KarveCar.Model.Classes;
using KarveCar.Model.Generic;
using KarveCar.Model.Sybase;
using KarveCar.View;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Interactivity;
using DataAccessLayer;
using KarveCar.Common;
using static KarveCar.Model.Generic.RecopilatorioCollections;
using static KarveCar.Model.Generic.RecopilatorioEnumerations;

namespace KarveCar.Logic.Maestros
{
    public class MaestrosAuxiliaresLogic
    {
        /// <summary>
        /// Añade un nuevo TabItem al TabControl según la EOpcion que recibe por param. Si el TabItem ya está mostrado, 
        /// no se carga de nuevo, simplemente se establece el foco en ese TabItem.
        /// </summary>
        /// <param name="opcion"></param>
        public static void PrepareTabItemDataGrid(EOpcion opcion)
        {
            if (tabitemdictionary.Where(p => p.Key == opcion).Count() == 0)
            {   //Se crea un nuevo GenericObservableCollection donde guardaremos los datos recibidos de la BBDD
                //Se recupera el nombre de la tabla de la BBDD
                string nombretabladb = ribbonbuttondictionary.Where(z => z.Key == opcion).FirstOrDefault().Value.nombretabladb;
                //Según la opcion recibida por params, se recuperan los datos de su correspondiente tabla de la BBDD, teniendo en cuenta su tipo de dato.
                //Se crea un nuevo DataGrid dentro de un nuevo TabItem con los datos del GenericObservableCollection.
                DalLocator locator = DalLocator.GetInstance();
                GenericObservableCollection genericobscollection = null;

                if (EOpcion.rbtnBancosClientes ==opcion)
                {
                    IDalObject dalObject = locator.FindDalObject(EOpcion.rbtnBancosClientes.ToString());
                    genericobscollection = dalObject.GetItems();
                }
                // extend polimorphism.
                switch (opcion)
                {
                    case EOpcion.rbtnBloqueFacturacion:
                        genericobscollection = MaestrosAuxiliaresModel.GetMaestrosAuxiliares(nombretabladb, BloqueFacturacion.templateinfodb, new BloqueFacturacion());
                        break;
                    case EOpcion.rbtnCanales:
                        genericobscollection = MaestrosAuxiliaresModel.GetMaestrosAuxiliares(nombretabladb, CanalCliente.templateinfodb, new CanalCliente());
                        break;
                    case EOpcion.rbtnCargosPersonal:
                        genericobscollection = MaestrosAuxiliaresModel.GetMaestrosAuxiliares(nombretabladb, CargoPersonal.templateinfodb, new CargoPersonal());
                        break;
                    case EOpcion.rbtnTipoComisionista:
                        genericobscollection = MaestrosAuxiliaresModel.GetMaestrosAuxiliares(nombretabladb, TipoComisionista.templateinfodb, new TipoComisionista());
                        break;

                    case EOpcion.rbtnFormaPagoProveedor:
                        genericobscollection = MaestrosAuxiliaresModel.GetMaestrosAuxiliares(nombretabladb, FormaPagoProveedor.templateinfodb, new FormaPagoProveedor());
                        break;
                    case EOpcion.rbtnGruposTarifa:
                        genericobscollection = MaestrosAuxiliaresModel.GetMaestrosAuxiliares(nombretabladb, GrupoTarifa.templateinfodb, new GrupoTarifa());
                        break;
                } 
                // need return value.
                CreateTabItemDataGrid(opcion, genericobscollection);
            }
            else
            {   //Si el TabItem ya está mostrado, no se carga de nuevo, simplemente se establece el foco en ese TabItem
                tabitemdictionary.Where(z => z.Key == opcion).FirstOrDefault().Value.TabItem.Focus();
            }
        }

        /// <summary>
        /// Añade a un nuevo DataGridUserControl los datos del GenericObservableCollection (genericobscollection) recibido por params. 
        /// El nombre de las propiedades del object del GenericObservableCollection (genericobscollection) corresponderán con los 
        /// respectivos Headers. Se añade el DataGridUserControl en un nuevo TabItem (tbitem).
        /// Se añade el EOpcion, el GenericObservableCollection recibido por params (como origin y copy) y el nuevo TabItem,  
        /// al Dictionary de TabItems(tabitemdictionary) que almacena los TabItems activos
        /// </summary>
        /// <param name="opcion"></param>
        /// <param name="genericobscollection"></param>
        private static void CreateTabItemDataGrid(EOpcion opcion, GenericObservableCollection genericobscollection)
        {
            if (genericobscollection.GenericObsCollection.Count != 0)
            {
                //Creamos el DataGrid
                DataGridUserControl datagrid = new DataGridUserControl();

                #region Se añade la ObservableCollection<object> directamente como el datagrid.ItemsSource, rellena las columnas según las propiedades que tenga el object, tenga o no tenga datos; el header será el nombre de cada propiedad del object

                //SetTrigger(datagrid);
                #endregion

                #region Se crean los DataGridTextColumn dinámicamente, dándole el nombre al header, y binding cada columna según establecido en la List<DBCriterios> del object; se añade cada columna individualmente al DataGrid
                ////Creamos los DataGridTextColumn
                //DataGridTextColumn col;

                //foreach (var item in templateinfodb)
                //{
                //    col = new DataGridTextColumn();
                //    col.Header = item.datagridheader; // new KarveDataGridTextColum(item.datagridheader);
                //    col.Binding = new Binding(item.nombrepropiedadobj);
                //    datagrid.Columns.Add(col);
                //}

                ////Añadimos los valores al Datagrid
                //foreach (var item in dgitemslist)
                //{
                //    datagrid.Items.Add(item);
                //}
                #endregion

                //datagrid.SetBinding(ItemsControl.ItemsSourceProperty, new Binding("SelectedItem") { Source = genericobscollection });
                //Se añade al DataGridUserControl el GenericObservableCollection recibido por params como ItemsSource
                datagrid.ItemsSource = genericobscollection.GenericObsCollection;

                //Se crea el Tabitem
                TabItem tabitem = TabItemLogic.CreateTabItemDataGrid(opcion);

                //Se añade el EOpcion, el GenericObservableCollection recibido por params (como origin y copy) y el nuevo TabItem,  
                //al Dictionary de TabItems(tabitemdictionary) que almacena los TabItems activos
                tabitemdictionary.Add(opcion, new TemplateInfoTabItem(genericobscollection, genericobscollection, tabitem));

                //Se añade el DataGridUserControl al TabItem
                tabitem.Content = datagrid;

                //Se habilitan/deshabilitan los Buttons del ToolBar según corresponda
                ToolBarLogic.EnabledDisabledToolBarButtonsByEOpcion(opcion);
            }
        }

        public static void SetTrigger(DataGrid contentControl) //Posiblemente se pueda eliminar este método
        {
            // create the command action and bind the command to it
            var invokeCommandAction = new InvokeCommandAction { CommandParameter = "datagridcommandtest" };
            var binding = new Binding { Path = new PropertyPath("CloseWindowCommand") };
            BindingOperations.SetBinding(invokeCommandAction, InvokeCommandAction.CommandParameterProperty, binding);

            // create the event trigger and add the command action to it
            var eventTrigger = new System.Windows.Interactivity.EventTrigger { EventName = "MouseEnter" };
            eventTrigger.Actions.Add(invokeCommandAction);

            // attach the trigger to the control
            var triggers = Interaction.GetTriggers(contentControl);
            triggers.Add(eventTrigger);
        }
    }
}
