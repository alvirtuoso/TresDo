using System;
using System.Data;
using Npgsql;
using System.Data.SqlClient;
using System.Collections.Generic;
using Dapper;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace ThreeDo.DbContext
{
	public class ConnectionFactory
	{

		public IDbConnection GetDapperConnection
		{
			get
			{
                var con = ConnectionSetting.DefaultConnection;
       
				return new NpgsqlConnection(con);

			}
		}

		public bool ExecuteProc(string sql, List<SqlParameter> paramList = null)
		{

			try
			{
				using (IDbConnection conn = GetDapperConnection)
				{
					DynamicParameters dp = new DynamicParameters();
					if (paramList != null)
						foreach (SqlParameter sp in paramList)
							dp.Add(sp.ParameterName, sp.SqlValue, sp.DbType); 
					conn.Open();
					return conn.Execute(sql, dp, commandType: CommandType.StoredProcedure) > 0;
				}
			}
			catch (Exception)
			{
				//do logging
				return false;
			}
		}
	}
}
