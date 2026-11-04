using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Data.Common;
//using Oracle.ManagedDataAccess.Client;
using Microsoft.Data.SqlClient;
using Microsoft.Data.SqlClient;

/*****************************************************************************
 * DALC4NET IS AN OPEN SOURCE DATA ACCESS LAYER
 * THIS DOES NOT REQUIRE ANY KIND OF LICENSEING
 * USERS ARE FREE TO MODIFY THE SOURCE CODE AS PER REQUIREMENT 
 ******************************************************************************/
namespace DataManager
{
    internal class DBParamBuilder
    {

        private string _providerName = string.Empty;
        //private AssemblyProvider _assemblyProvider = null;

        internal DBParamBuilder(string providerName)
        {
            //_assemblyProvider = new AssemblyProvider(providerName);
            //_providerName = providerName;
        }

        internal SqlParameter GetParameter(SqlParameter parameter)
        {
            SqlParameter dbParam = GetParameter();         
            dbParam.ParameterName = parameter.ParameterName;         
            dbParam.Value = parameter.Value;
            dbParam.Direction = parameter.Direction;            
            dbParam.SqlDbType = parameter.SqlDbType;

            return dbParam;            
        }

        /*internal List<DbParameter> GetParameterCollection(MySqlParameterCollection parameterCollection)
        {
            List<DbParameter> dbParamCollection = new List<DbParameter>();
            DbParameter dbParam = null;
            foreach(MySqlParameter param in parameterCollection.)
            {
                dbParam = GetParameter(param);
                dbParamCollection.Add(dbParam);
            }
            
            return dbParamCollection;
        }*/

        #region Private Methods
        private SqlParameter GetParameter()
        {
            //string typeName = AssemblyProvider.GetInstance().GetParameterType();
            //IDbDataParameter dbParam = (IDbDataParameter)AssemblyProvider.GetInstance().DBProviderAssembly.CreateInstance(typeName);
            //return dbParam;

            //DbParameter dbParam = AssemblyProvider.GetInstance(this._providerName).Factory.CreateParameter();
            SqlParameter dbParam =new SqlParameter();   //_assemblyProvider.Factory.CreateParameter();
            return dbParam;

        }

  

        #endregion
    }
}
