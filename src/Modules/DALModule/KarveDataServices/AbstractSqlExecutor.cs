﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;

namespace KarveDataServices
{
    /// <summary>
    /// Abstract SQL class that implements the common interface for executing sql queries.
    /// </summary>
    public abstract class AbstractSqlExecutor : ISqlExecutor
    {

        /// <summary>
        /// Database connection
        /// </summary>
        protected IDbConnection _connection;
        /// <summary>
        /// ConnectionState
        /// </summary>
        protected ConnectionState _currentState;
        /// <summary>
        /// Returns the connection state;
        /// </summary>
        public ConnectionState State => _currentState;
        /// <summary>
        ///  Returns the connection;
        /// </summary>
        public IDbConnection Connection
        {   get => _connection;
            set => _connection = value;
        }
        public string ConnectionString { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        /// <summary>
        /// Abstact method for sql query.
        /// </summary>
        /// <param name="sqlQuery">Kind of query</param>
        /// <returns></returns>
        public abstract Task<DataSet> AsyncDataSetLoad(string sqlQuery);
        /// <summary>
        /// Begin a transaction
        /// </summary>
        public abstract void BeginTransaction();
        /// <summary>
        /// Call a stored procedure
        /// </summary>
        /// <param name="statementProcedureName">Name of the stored procedure</param>
        /// <param name="statementProcList">Name list of the stored procedure</param>
        public abstract void CallStoredProcedure(string statementProcedureName, IList<string> statementProcList);
        /// <summary>
        ///  Close the connection.
        /// </summary>
        /// <returns></returns>
        public abstract bool Close();
        /// <summary>
        ///  Commit the current transaction. Be aware that the best way to do transaction is transaction scope.
        /// </summary>
        public abstract void Commit();
        /// <summary>
        ///  Execute DML (Data modeling language) a query.
        /// </summary>
        /// <param name="commandName">Name of the command</param>
        /// <param name="cmdType">Type of the command</param>
        /// <param name="param">Parameters of the command</param>
        /// <returns></returns>
        public abstract bool ExecuteNonQuery(string commandName, CommandType cmdType, DataParameter param);
        /// <summary>
        /// Execute a select command
        /// </summary>
        /// <param name="commandName">Name of the command</param>
        /// <param name="cmdType"></param>
        /// <param name="param"></param>
        /// <returns></returns>
        public abstract DataTable ExecuteParamerizedSelectCommand(string CommandName, CommandType cmdType, DataParameter param);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public abstract string ExecuteQueryDataTable(string sqlQuery);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="CommandName"></param>
        /// <param name="cmdType"></param>
        /// <returns></returns>
        public abstract DataTable ExecuteSelectCommand(string CommandName, CommandType cmdType);
        /// <summary>
        ///  Load a dataset given a query.
        /// </summary>
        /// <param name="sqlQuery">Query to be executed</param>
        /// <returns></returns>
        public abstract DataSet LoadDataSet(string sqlQuery);
        /// <summary>
        ///  Load a data set by tables
        /// </summary>
        /// <param name="sqlQuery">Query to be executed</param>
        /// <param name="tableAlias">List of alias table</param>
        /// <returns>A dataset</returns>
        public abstract DataSet LoadDataSetByTables(string sqlQuery, IList<string> tableAlias);
        /// <summary>
        ///  Open a connection to the database
        /// </summary>
        /// <returns></returns>
        public abstract bool Open();
        /// <summary>
        ///  Rollback a connection to the database.
        /// </summary>
        public abstract void Rollback();
        /// <summary>
        ///  Update a dataset.
        /// </summary>
        /// <param name="sqlQuery">Query to be executed</param>
        /// <param name="dts">Dataset to be executed</param>
        public abstract void UpdateDataSet(string sqlQuery, ref DataSet dts);
        /// <summary>
        /// Load a dataset filling the schema in batch way.
        /// </summary>
        /// <param name="queryList">Dictionary. The key contains the name of the table and the value the query.</param>
        /// <returns></returns>
        public abstract Task<DataSet> AsyncDataSetLoadBatch(IDictionary<string, string> queryList);

        /// <summary>
        ///  Load a data table for an async data table.
        /// </summary>
        /// <param name="sqlQuery"></param>
        /// <returns></returns>
        public abstract Task<DataTable> QueryAsyncForDataTable(string sqlQuery);

        //  public abstract IList<T> ExecuteQueryFields(string sqlQuery);
        /// <summary>
        ///  Check if two values have the same columns
        /// </summary>
        /// <param name="tableName">Name of the table</param>
        /// <param name="columnName">First column name</param>
        /// <param name="whereClause">Clause of the match</param>
        /// <returns> True if the match in the clause is correct.</returns>
        public bool SameColumn(string tableName, string columnName, string whereClause)
        {
            string sqlString = string.Format(" SELECT {0} FROM {1} {2}", columnName, tableName, whereClause);
            string value = ExecuteQueryDataTable(sqlString);
            return (!string.IsNullOrEmpty(value));
        }

        /// <summary>
        /// This method show the exact line number for the exception
        /// </summary>
        /// <param name="message">Error message</param>
        /// <param name="lineNumber">Position where it happens in the code</param>
        /// <param name="caller">Stack caller</param>
        /// <returns></returns>
        protected string ShowMessage(string message,
            [CallerLineNumber] int lineNumber = 0,
            [CallerMemberName] string caller = null)
        {
            string outMessage = message + " at line " + lineNumber + " (" + caller + ")";
            return outMessage;
        }
        /// <summary>
        /// Get a list of fields form a sql query
        /// </summary>
        /// <param name="sqlQuery">Query to be tokenized</param>
        /// <returns></returns>
        public abstract IList<string> ExecuteQueryFields(string sqlQuery);
        /// <summary>
        ///  Open a new database connection
        /// </summary>
        /// <returns>Return a new connection from ADO.NET connection pool</returns>
        public abstract IDbConnection OpenNewDbConnection();
        /// <summary>
        ///  Dispose 
        /// </summary>
        public abstract void Dispose();
    }
}
