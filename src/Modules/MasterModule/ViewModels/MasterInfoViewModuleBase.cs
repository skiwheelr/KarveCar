﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Threading.Tasks;
using KarveCommon.Services;
using MasterModule.Common;
using Prism.Regions;
using KarveDataServices;
using System.Windows;
using KarveCommon.Generic;
using KarveCommonInterfaces;
using System.Windows.Input;
using KarveDataServices.DataTransferObject;
using DataAccessLayer.DataObjects;

namespace MasterModule.ViewModels
{

    /// <summary>
    ///  This class has the resposabilty to build the data payload and masking overrides not needed.
    ///  In the MasterModule.ViewModels we have two kind of classes:
    ///  1. ViewModel Controllers.  A view model controller starts navigation when the 
    ///                             user clicks on a item. It will create child views as in the view-first approach.
    ///  2. ViewModel Info classes. An info view model typically has the resposability to handle logic to is associated 
    ///     view.
    /// </summary>
    public abstract class MasterInfoViewModuleBase : MasterViewModuleBase
    {
        private bool _canDelete = true;

        /// <summary>
        /// The MasterInfoViewModuleBase is a view model that overrides no requested operation in the InfoViewModels.
        /// </summary>
        /// <param name="eventManager">Event Manager. It allows the communication throu viewmodel</param>
        /// <param name="configurationService">Configuration Service. It allows the reconfiguration of the view model</param>
        /// <param name="dataServices">DataServices. It allows to fetch the data.</param>
        /// <param name="dialogService">DialogService. It allows spotting the error.</param>
        /// <param name="assistService">AssistService. It allows the magnifier to spot a new grid</param>
        /// <param name="manager">RegionManager. It handles the region manager.</param>
        public MasterInfoViewModuleBase(IEventManager eventManager,
                                        IConfigurationService configurationService,
                                        IDataServices dataServices, 
                                        IDialogService dialogService,
                                        IAssistService assistService,
                                        IRegionManager manager) : base(configurationService,eventManager,dataServices,  assistService, dialogService,manager)
        {
            _canDelete = true;
        }

        public override async Task<bool> DeleteAsync(string primaryKey, DataPayLoad payLoad)
        {
            await Task.Delay(1);
            return false;
        }

        public override void NewItem()
        {
        }

        public override void StartAndNotify()
        {

        }


        
        /// <summary>
        ///  Command for handling the province.
        /// </summary>
        /// <param item="">Item to be used.</param>
        protected async virtual void OnProvinceAssist(object item)
        {
            IHelperDataServices services = DataServices.GetHelperDataServices();
            var dtos = await services.GetMappedAllAsyncHelper<ProvinciaDto, PROVINCIA>();
            AssistService.ShowAssistView<ProvinciaDto>(KarveLocale.Properties.Resources.MasterInfoViewModuleBase_OnProviceAssist_ListadoProvincia, dtos);
            if (AssistService.SelectedItem!=null)
            {
                SetBranchProvince(AssistService.SelectedItem);
            }
        }
        /// <summary>
        /// OnContactsChargeAssist.
        /// </summary>
        /// <param name="item"></param>
        protected async virtual void OnContactChargeAssist(object item)
        {
            IEnumerable<ContactsDto> dtos = item as IEnumerable<ContactsDto>;
            IHelperDataServices services = DataServices.GetHelperDataServices();
            var currentServiceDto = await services.GetMappedAllAsyncHelper<PersonalPositionDto, PERCARGOS>();
      
            AssistService.ShowAssistView<PersonalPositionDto>(KarveLocale.Properties.Resources.MasterInfoViewModuleBase_OnProviceAssist_ListadoProvincia, currentServiceDto);
            if (AssistService.SelectedItem != null)
            {
                SetContactsCharge(AssistService.SelectedItem);
            }
        }
        
        public virtual void SetContactsCharge(object charge)
        {
        }
        public virtual void SetBranchProvince(object province)
        {
        }
        /// <summary>
        ///  This command has the resposability to be binded to the assist service 
        ///  in order to see the provinces inside a delegation.
        /// </summary>
        public ICommand DelegationProvinceMagnifierCommand { set; get; }
        /// <summary>
        ///  This command has the responsability to be binded to the 
        ///  contact charge command inside the contact.
        /// </summary>
        public ICommand ContactChargeMagnifierCommand { set; get; } 
        /// <summary>
        ///  This build a data payload, enforcing the value that cames from the UI.
        /// </summary>
        /// <param name="eventDictionary">Dictionary that it cames from the UI components</param>
        /// <returns>A payload to be sent to other view models.</returns>
        protected DataPayLoad BuildDataPayload(IDictionary<string, object> eventDictionary)
        {
            DataPayLoad payLoad = new DataPayLoad();
            if (string.IsNullOrEmpty(payLoad.PrimaryKeyValue))
            {
                payLoad.PrimaryKeyValue = PrimaryKeyValue;
                payLoad.PayloadType = DataPayLoad.Type.Update;
            }
            if (eventDictionary.ContainsKey("DataObject"))
            {
                if (eventDictionary["DataObject"] == null)
                {
                    MessageBox.Show("DataObject is null.");
                }
                var data = eventDictionary["DataObject"];
                if (eventDictionary.ContainsKey("Field"))
                {
                    var name = eventDictionary["Field"] as string;
                    GenericObjectHelper.PropertySetValue(data, name, eventDictionary["ChangedValue"]);
                }
                payLoad.DataObject = data;
                eventDictionary["DataObject"] = data;
                payLoad.DataDictionary = eventDictionary;
            }
            return payLoad;
        }
        /// <summary>
        ///  Name of the routing. We overrdie the name to provide a default.
        /// </summary>
        /// <param name="name">Name of the route</param>
        /// <returns>Empty string</returns>
        protected override string GetRouteName(string name)
        {
            return "";
        }
        /// <summary>
        ///  Specify if we can delete a region
        /// </summary>
        public override bool CanDeleteRegion
        {
            set { _canDelete = value; }
            get { return _canDelete; }
        }
        /// <summary>
        ///  Deprecated.
        /// </summary>
        /// <param name="result"></param>
        protected override void SetDataObject(object result)
        {
        }
        /// <summary>
        ///  We override the SetTable to provide a default.
        ///  The use of data table are not 
        /// </summary>
        /// <param name="table">Table to be used.</param>
        protected override void SetTable(DataTable table)
        {
        }
        protected IDictionary<string, string> ViewModelQueries { get; set; }
    }

}