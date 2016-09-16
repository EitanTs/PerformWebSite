using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using DALC4NET;
using System.Configuration;

/// <summary>
/// Summary description for IsLogOn
/// </summary>
public class IsLogOn : System.Web.UI.Page
{
	public IsLogOn()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public bool IsLogedOn()
    {
        return Session["UserIndx"] != null;
    }
}