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
    string url = "AdminReport.aspx?unit={0}&unitV={1}";
    string OptionString = "<option value='{0}'>{1}</option>";

    public void PrintToolBar()
    {
        Response.Write(printHtml.GetFrame("AdminToolBar.html"));
    }
    public void PrintDepName()
    {
        DBParameterCollection dbp = new DBParameterCollection();
        dbp.Add(new DBParameter("@DepIndx", Session["DepIndx"].ToString()));
        Response.Write(dbConn.ExecuteQuery(AdminQueries.GetDep, dbp).Rows[0]["DepName"]);
    }
    public void PrintUnitType()
    {
        var dt = dbConn.ExecuteQuery(AdminQueries.GetUnitType);
        PrintOptionValues(dt, "SupportDTLIndx", "SupportDTLName");
    }
    public void PrintUnit()
    {
        DBParameterCollection dbp = new DBParameterCollection();
        if (int.Parse(Session["JobHierarchy"].ToString()) > 4)
            dbp.Add(new DBParameter("@UnitIndx", DBNull.Value));
        else
            dbp.Add(new DBParameter("@UnitIndx", Session["UnitIndx"]));
        dbp.Add(new DBParameter("@DepIndx", Session["DepIndx"].ToString()));
        var dt = dbConn.ExecuteQuery(AdminQueries.GetUnit, dbp);
        if (Request.QueryString["unitV"] == null)
        {
            Response.Redirect(string.Format(url, Request.QueryString["unitId"], dt.Rows[0]["UnitIndx"]));
        }
        PrintOptionValues(dt, "UnitIndx", "UnitName");
        
    }
    public void PrintSubUnit()
    {
        DBParameterCollection dbp = new DBParameterCollection();
        if (int.Parse(Session["JobHierarchy"].ToString()) > 30)
            dbp.Add(new DBParameter("@SubUnitIndx", DBNull.Value));
        else
            dbp.Add(new DBParameter("@SubUnitIndx", Session["SubUnitIndx"]));
        dbp.Add(new DBParameter("@UnitIndx", Request.QueryString["unitV"]));
        var dt = dbConn.ExecuteQuery(AdminQueries.GetSubUnit, dbp);
        PrintOptionValues(dt,"SubUnitIndx","SubUnitName");
    }
    public void PrintTeam()
    { 
        
    }
    public void PrintOptionValues(DataTable dt, string firstCol, string secondCol)
    {
        foreach (DataRow dr in dt.Rows)
        {
            Response.Write(string.Format(OptionString, dr[firstCol].ToString(), dr[secondCol].ToString()));
        }
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        PrintToolBar();
    }
}