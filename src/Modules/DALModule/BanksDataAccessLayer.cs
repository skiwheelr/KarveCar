using KarveCommon.Generic;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using KarveDataAccessLayer.DataObjects;
using KarveDataServices;

namespace KarveDataAccessLayer
{
    /// <summary>
    /// A DAL class for managing the lifecycle of Banks objects. This DAL implementation
    /// uses a DataSet for persisting to the database.
    /// </summary>
    public class BanksDataAccessLayer 
    {
        private readonly string _id = RecopilatorioEnumerations.EOpcion.rbtnBancosClientes.ToString();
        private Type _dalType = typeof(BancoDataObject);
        private ISqlQueryExecutor sqlQueryExecutor;
        public BanksDataAccessLayer(ISqlQueryExecutor queryExecutor)
        {
            sqlQueryExecutor = queryExecutor;
        } 
        /// <summary>
        /// 
        ///  This method queries the data mapper and return an observable collection.
        /// </summary>
        /// <param name="mapper"> DataMapper value </param>
        /// <param name="collection">Collection to be filled in output</param>
        /*
        private void QueryCopy( out ObservableCollection<BancoDataObject> collection)
        {
            ICollection<BancoDataObject> banks = _mapper.QueryForList<BancoDataObject>("Auxiliares.GetAllBanks", null);
            collection = new ObservableCollection<BancoDataObject>();
            foreach (var bank in banks)
            {
                collection.Add(bank);
            }

        }
        /// <summary>
        /// Lists all banks
        /// </summary>
        /// <returns>Returns a table of all available banks</returns>
        public DataTable GetAllBanksTable()
        {
            DataTable table = new DataTable();
            ICollection<BancoDataObject> charges = _mapper.QueryForList<BancoDataObject>("Auxiliares.GetAllBanks", null);
            table.Columns.Add(new DataColumn("Codigo", typeof(string)));
            table.Columns.Add(new DataColumn("Definicion", typeof(string)));
            foreach (BancoDataObject item in charges)
            {
                if ((item.Codigo != null) && (item.Definicion != null))
                {
                    var row = table.NewRow();
                    row["Codigo"] = item.Codigo;
                    row["Definicion"] = item.Definicion;
                    table.Rows.Add(row);
                }
            }
            return table;
        }
        /// <summary>
        ///  Get an observable collection of banks.
        /// </summary>
        /// <returns></returns>
        public ObservableCollection<BancoDataObject> GetBanks()
        {
            ObservableCollection<BancoDataObject> dataCollection = new ObservableCollection<BancoDataObject>();
            QueryCopy(_mapper, out dataCollection);
            return dataCollection;
        }
        /// <summary>
        /// For each item in the collection bank. It does an update.
        /// </summary>
        /// <param name="banks"></param>
        public void UpdateBanks(ObservableCollection<BancoDataObject> banks)
        {
            IList<BancoDataObject> current = new List<BancoDataObject>();
            foreach (var bank in banks)
            {
                current.Add(bank);
            }
            int ret = _mapper.Update("Auxiliares.UpdateBanks", current);
        }
        */
    }
}
