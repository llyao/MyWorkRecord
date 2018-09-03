using System;
using System.Collections.Generic;
using System.Data;
using ClientCore.Logging;
using ClientCore.Runtime;
using MySql.Data.MySqlClient;
using MySqlX.XDevAPI;

namespace ClientCore.DB
{
    public class DbManager:Manager<DbManager>
    {
        /// <summary>
        /// MySqlConnection连接对象
        /// </summary>
        private MySqlConnection connection;

        public void Connect(string ip, int port, string database, string account, string password)
        {
	        string result =
		        string.Format("Server={0};Database={1}; User ID={2};Password={3};port={4};CharSet=utf8;pooling=true;",
			        ip,
			        database,
			        account,
			        password,
			        port);
            connection = new MySqlConnection(result);
        }

        /// <summary>
        /// 运行sql语句查询整个TableData
        /// 例如 UserTableData user = GetTableData<UserTableData>("select * from user where id = 1");
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="SQL"></param>
        /// <returns></returns>
        public T GetTableData<T>(string SQL) where T:BaseTableData, new()
        {
            DataSet ds = new DataSet();
            MySqlDataAdapter Da = new MySqlDataAdapter(SQL, GetOpenConnection());
            try
            {
                Da.Fill(ds, "ts");
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

            if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
            {
                return null;
            }

            T result = new T();
            result.ReadFrom(ds.Tables[0].Rows[0]);
            return result;
		}

	    /// <summary>
	    /// 运行sql语句查询整个TableData
	    /// 例如 UserTableData user = GetTableData<UserTableData>("select * from user");
	    /// </summary>
	    /// <typeparam name="T"></typeparam>
	    /// <param name="SQL"></param>
	    /// <returns></returns>
	    public List<T> GetTableDatas<T>(string SQL) where T : BaseTableData, new()
	    {
		    DataSet ds = new DataSet();
		    MySqlDataAdapter Da = new MySqlDataAdapter(SQL, GetOpenConnection());
		    try
		    {
			    Da.Fill(ds, "ts");
		    }
		    catch (Exception Ex)
		    {
			    throw Ex;
		    }

		    if (ds.Tables.Count == 0 || ds.Tables[0].Rows.Count == 0)
		    {
			    return null;
		    }

		    List<T> result = new List<T>();
		    for(int i = 0; i < ds.Tables[0].Rows.Count; i++)
		    {
				T t = new T();
				t.ReadFrom(ds.Tables[0].Rows[i]);
			    result.Add(t);
			}
		    
		    return result;
	    }

		/// <summary>
		/// 执行sql语句，返回插入后的自增id
		/// </summary>
		/// <param name="SQLString"></param>
		/// <returns></returns>
		public int ExecuteNonQuery(string SQLString, UserTableData user)
		{
			if(user == null)
			{
				Logger.Error("致命错误，操作DB用户是空的");
				throw new Exception("致命错误，操作DB用户是空的");
			}
			string log = string.Format("【操作DB，userID={0}，昵称={1}】，SQL：{2}", user.ID, user.Nick, SQLString);
			Logger.Info(log);
            using (MySqlCommand cmd = new MySqlCommand(SQLString, GetOpenConnection()))
            {
                try
                {
                    int rows = cmd.ExecuteNonQuery();
                    return (int)cmd.LastInsertedId;
                }
                catch (MySql.Data.MySqlClient.MySqlException e)
                {
                    throw e;
                }
            }
        }

	    private MySqlConnection GetOpenConnection()
	    {
		    if(connection.State != ConnectionState.Open)
		    {
			    connection.Open();
		    }
		    return connection;
	    }
	}
}