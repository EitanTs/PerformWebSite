using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DALC4NET;
using System.Data;
using System.Data.SqlClient;

public partial class admin_AdminReport : System.Web.UI.Page
{
    Select mySelect = new Select();
    PrintHtml printHtml = new PrintHtml();
    DBConnector dbConn = new DBConnector();
    Table table = new Table();
    DBParameterCollection dbp = new DBParameterCollection();

    public void PrintToolBar()
    {
        Response.Write(printHtml.GetFrame("AdminToolBar.html"));
    }
    public void PrintDepName()
    {
        dbp.Add(new DBParameter("@DepIndx", Session["DepIndx"].ToString()));
        Response.Write(dbConn.ExecuteQuery(AdminQueries.GetDep, dbp).Rows[0]["DepName"]);
    }
    public void PrintUnitType()
    {
        var dt = dbConn.ExecuteQuery(AdminQueries.GetUnitType);
        foreach (DataRow dr in dt.Rows)
        {
            Response.Write(string.Format("<option value='{0}'>{1}</option>", 
                dr["SupportDTLIndx"].ToString(), dr["SupportDTLName"].ToString()));
        }
    }
    public void PrintUnit()
    { 
        dbp.Add(new DBParameter("@UnitIndx", Session["UnitIndx"]));
        Response.Write(dbConn.ExecuteQuery(AdminQueries.GetUnit, dbp).Rows[0]["UnitName"]);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        PrintToolBar();
    }
}