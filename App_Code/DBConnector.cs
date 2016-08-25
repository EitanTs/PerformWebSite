using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using DALC4NET;
using System.Configuration;

public class DBConnector : System.Web.UI.Page
{
    DBHelper dbHelper = new DBHelper("MyConn"); 
	public DBConnector()
	{
       
	}
  

    public DataTable ExecuteQuery(string query)
    {
        return dbHelper.ExecuteDataTable(query, CommandType.Text);
    }

    public DataTable ExecuteQuery(string query, DBParameterCollection dbp)
    {
        return dbHelper.ExecuteDataTable(query, dbp, CommandType.Text);
    }

    public DataTable ExecuteProcedure(string query)
    {
        return dbHelper.ExecuteDataTable(query, CommandType.StoredProcedure);
    }

    public DataTable ExecuteProcedure(string query, DBParameterCollection dbp)
    {
        return dbHelper.ExecuteDataTable(query, dbp, CommandType.StoredProcedure);
    }


    
}