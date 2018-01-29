﻿using System;
using KarveCommon.Services;
using KarveDataServices;
using KarveTest.Mock;
using Moq;
using NUnit.Framework;
using Prism.Regions;
using HelperModule.ViewModels;
using System.Windows.Input;
using KarveDataServices.DataTransferObject;

namespace KarveTest.ViewModels
{

    /// <summary>
    ///  This is the test helper view model.
    /// </summary>
    public class TestVehicleTypeViewModel 
    {
       /// <summary>
       ///  EventManager
       /// </summary>
        private IEventManager _eventManager = new MockEventManager();
        /// <summary>
        ///  MockDataServices
        /// </summary>
        private MockDataServices _dataServices = new MockDataServices();
        /// <summary>
        ///  Region Manager
        /// </summary>
        private Mock<IRegionManager> _regionManager= new Mock<IRegionManager>();
        /// <summary>
        ///  VehicleTypesViewModel.
        /// </summary>
        private VehicleTypesViewModel _vehicleTypeViewModel;
        
        [OneTimeSetUp]
        public void Setup()
        {
            InitHelper();
        }
 
        /// <summary>
        ///  This test simulate a vehicle type selection.
        /// </summary>
        [Test]
        private void Should_UpdateDto_After_GridSelection()
        {
            // Arrange
            ICommand command =  _vehicleTypeViewModel.SelectionChangedCommand;
            VehicleTypeDto dto  = new VehicleTypeDto();
            dto.Code = "5L";
            dto.Name = "500P";
            dto.WebName = "Fiat 500";
            dto.OfferMargin = 1;
            dto.TerminationDate = DateTime.Now;
            // Act
            command.Execute(dto);
           // Assert.
            VehicleTypeDto resultDto = _vehicleTypeViewModel.VehicleType;
            Assert.AreEqual(dto.Code, resultDto.Code);
            Assert.AreEqual(dto.Name, resultDto.Name);
            Assert.AreEqual(dto.WebName, resultDto.WebName);
            Assert.AreEqual(dto.OfferMargin, resultDto.OfferMargin);
            Assert.AreEqual(dto.TerminationDate, resultDto.TerminationDate);
        }
        /// <summary>
        ///  This should delete an entity when sent event.
        /// </summary>
        [Test]
        private void Should_Delete_Entity_When_Sent_Event()
        {

        }
        [Test]
        private void Should_Insert_Entity_When_Sent_Event()
        {   
        }
        [Test]
        private void Should_Update_After_ItemChangedCommand()
        {
            ICommand command = _vehicleTypeViewModel.ItemChangedCommand;
            VehicleTypeDto dto = new VehicleTypeDto();
            dto.Code = "5L";
            dto.Name = "500P";
            dto.WebName = "Fiat 500";
            dto.OfferMargin = 1;
            dto.TerminationDate = DateTime.Now;
            command.Execute(dto);
            VehicleTypeDto resultDto = _vehicleTypeViewModel.VehicleType;
            Assert.AreEqual(dto.Code, resultDto.Code);
            Assert.AreEqual(dto.Name, resultDto.Name);
            Assert.AreEqual(dto.WebName, resultDto.WebName);
            Assert.AreEqual(dto.OfferMargin, resultDto.OfferMargin);
            Assert.AreEqual(dto.TerminationDate, resultDto.TerminationDate);
        }
        private void InitHelper()
        {
            // I dont think at this point that the region manager is available.
            _vehicleTypeViewModel =
                new VehicleTypesViewModel(_dataServices, _regionManager.Object, _eventManager);
 
        }
    }

}