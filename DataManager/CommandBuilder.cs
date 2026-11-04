using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.Common;
using Microsoft.Data.SqlClient;
/*****************************************************************************
 * DALC4NET IS AN OPEN SOURCE DATA ACCESS LAYER
 * THIS DOES NOT REQUIRE ANY KIND OF LICENSEING
 * USERS ARE FREE TO MODIFY THE SOURCE CODE AS PER REQUIREMENT
 
 ******************************************************************************/
namespace DataManager
{
    internal class CommandBuilder
    {
        private string _providerName = string.Empty;
        private DBParamBuilder _paramBuilder = null;
       // private AssemblyProvider _assemblyProvider = null;

        public CommandBuilder()
        {
            this._providerName = "Microsoft.Data.SqlClient";// Configuration.GetProviderName(Configuration.DefaultConnection);
            _paramBuilder = new DBParamBuilder(this._providerName);
            //_assemblyProvider = new AssemblyProvider(this._providerName);
        }

        public CommandBuilder(string providerName)
        {
            this._providerName = providerName;
            _paramBuilder = new DBParamBuilder(providerName);
            //_assemblyProvider = new AssemblyProvider(this._providerName);
        }

        

        #region Inrernal Methods
        internal SqlCommand GetCommand(string commandText, SqlConnection connection)
        {
            return GetCommand(commandText, connection, CommandType.Text);
        }


        internal SqlCommand GetCommand(string commandText, SqlConnection connection, CommandType commandType)
        {
            SqlCommand command = GetCommand();
            command.CommandText = commandText;
            command.Connection = connection;
            command.CommandType = commandType;
            return command;
        }


        internal SqlCommand GetCommand(string commandText, SqlConnection connection, SqlParameter parameter)
        {
            return GetCommand(commandText, connection, parameter, CommandType.Text);
        }

        internal SqlCommand GetCommand(string commandText, SqlConnection connection, SqlParameter parameter, CommandType commandType)
        {
            SqlParameter param = _paramBuilder.GetParameter(parameter);
            SqlCommand command = GetCommand(commandText, connection, commandType);
            command.Parameters.Add(param);
            return command;
        }

        internal SqlCommand GetCommand(string commandText, SqlConnection connection, List<SqlParameter> parameterCollection)
        {
            return GetCommand(commandText, connection,parameterCollection, CommandType.Text);
        }

        internal SqlCommand GetCommand(string commandText, SqlConnection connection, List<SqlParameter> parameterCollection, CommandType commandType)
        {
            List<SqlParameter> paramArray = parameterCollection;
            SqlCommand command = GetCommand(commandText, connection, commandType);

            foreach (SqlParameter param in paramArray)            
                command.Parameters.Add(param);
            
            return command;
        }


        #endregion

        #region Private Methods"

        private SqlCommand GetCommand()
        {          
            SqlCommand command = new SqlCommand();// _assemblyProvider.Factory.CreateCommand();            
            return command;
        }

        #endregion
    }
}
