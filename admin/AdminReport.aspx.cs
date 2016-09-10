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
    string url = "AdminReport.aspx?unitId={0}&unitV={1}&subId={2}&teamId={3}";
    string SemiUrl = "AdminReport.aspx?unitId={0}&unitV={1}&subId={2}&subV={3}&teamId={4}";
    string FullUrl = "AdminReport.aspx?unitId={0}&unitV={1}&subId={2}&subV={3}&teamId={4}&teamV={5}";
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
        if (int.Parse(Session["Hierarchy"].ToString()) > 4)
            dbp.Add(new DBParameter("@UnitIndx", DBNull.Value));
        else
            dbp.Add(new DBParameter("@UnitIndx", Session["UnitIndx"]));
        dbp.Add(new DBParameter("@DepIndx", Session["DepIndx"].ToString()));
        var dt = dbConn.ExecuteQuery(AdminQueries.GetUnit, dbp);
        if (Request.QueryString["unitV"] == null)
        {
            Response.Redirect(string.Format(url, Request.QueryString["unitId"], dt.Rows[0]["UnitIndx"],"0","0"));
        }
        PrintOptionValues(dt, "UnitIndx", "UnitName");
        
    }
    public void PrintSubUnit()
    {
        DBParameterCollection dbp = new DBParameterCollection();
        if (int.Parse(Session["Hierarchy"].ToString()) > 30)
            dbp.Add(new DBParameter("@SubUnitIndx", DBNull.Value));
        else
            dbp.Add(new DBParameter("@SubUnitIndx", Session["SubUnitIndx"]));
        dbp.Add(new DBParameter("@UnitIndx", Request.QueryString["unitV"]));
        var dt = dbConn.ExecuteQuery(AdminQueries.GetSubUnit, dbp);
        if (Request.QueryString["subV"] == null && dt.Rows.Count > 0)
        {
            Response.Redirect(string.Format(SemiUrl, Request.QueryString["unitId"], Request.QueryString["unitV"], Request.QueryString["subId"], dt.Rows[0]["SubUnitIndx"],"0"));
        }
        PrintOptionValues(dt,"SubUnitIndx","SubUnitName");
    }
    public void PrintTeam()
    {
        if (Request.QueryString["subV"] == null)
            return;
        DBParameterCollection dbp = new DBParameterCollection();
        if (int.Parse(Session["Hierarchy"].ToString()) > 20)
            dbp.Add(new DBParameter("@CodeTeam", DBNull.Value));
        else
            dbp.Add(new DBParameter("@CodeTeam", Session["SubUnitIndx"]));
        dbp.Add(new DBParameter("@SubUnitIndx", Request.QueryString["subV"]));
        var dt = dbConn.ExecuteQuery(AdminQueries.GetTeam, dbp);
        if (Request.QueryString["subV"] == null && dt.Rows.Count > 0)
        {
            Response.Redirect(string.Format(FullUrl, Request.QueryString["unitId"], Request.QueryString["unitV"], 
                Request.QueryString["subId"], dt.Rows[0]["SubUnitIndx"],Request.QueryString["teamId"], Request.QueryString["teamV"]));
        }
        PrintOptionValues(dt, "CodeTeam", "CodeTeam");
    }
    public void PrintReportStatus()
    {
        PrintOptionValues(dbConn.ExecuteQuery(AdminQueries.GetReportStatus), "SupportDtlIndx", "SupportDtlName");
    }
    public void PrintHirarchy()
    {
        var dbp = new DBParameter("@Hierarchy", Session["Hierarchy"]);
        PrintOptionValues(dbConn.ExecuteQuery(AdminQueries.GetHirarchy, dbp), "SupportDTLNum", "SupportDTLName");
    }
    public void PrintWorkers()
    {
        var dbp = new DBParameterCollection();
        dbp.Add(new DBParameter("@CompanyIndx", Session["CompanyIndx"]));
        dbp.Add(new DBParameter("@DepIndx", Session["DepIndx"]));
        dbp.Add(new DBParameter("@UnitIndx", Request.QueryString["unitV"]));
        dbp.Add(new DBParameter("@SubUnitIndx", Request.QueryString["subV"]));
        if (Request.QueryString["teamV"] == null)
            dbp.Add(new DBParameter("@CodeTeam", DBNull.Value));
        else
            dbp.Add(new DBParameter("@CodeTeam", Request.QueryString["teamV"]));
        dbp.Add(new DBParameter("@UserIndx", DBNull.Value));
        PrintOptionValues(dbConn.ExecuteQuery(AdminQueries.GetWorkers, dbp), "UserIndx", "FullName");
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