﻿using System;
using KarveCommon.Services;
using KarveDataServices.DataObjects;
using System.Data;

namespace ProvidersModule.ViewModels
{
    public class SupplierDataPayload : ISupplierPayload
    {
        public ISupplierDataInfo SupplierDataObjectInfo { get; set ; }
        public ISupplierTypeData SupplierDataObjectType { get; set; }
        public IProvinceData SupplierProvinceDataObject { get; set; }
        public ICountryData SupplierCountryDataObject { get; set; }
        public DataTable SupplierSummaryDataTable { get; set; }
    }
}