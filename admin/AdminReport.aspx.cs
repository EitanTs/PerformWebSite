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
    string url = "AdminReport.aspx?unitId={0}&unitV={1}&subId={2}&teamId={3}&search={4}";
    string SemiUrl = "AdminReport.aspx?unitId={0}&unitV={1}&subId={2}&subV={3}&teamId={4}&search={5}";
    string FullUrl = "AdminReport.aspx?unitId={0}&unitV={1}&subId={2}&subV={3}&teamId={4}&teamV={5}&search={6}";
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
            Response.Redirect(string.Format(url, Request.QueryString["unitId"], dt.Rows[0]["UnitIndx"],"0","0", Request.QueryString["search"]));
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
            Response.Redirect(string.Format(SemiUrl, Request.QueryString["unitId"], Request.QueryString["unitV"], Request.QueryString["subId"], dt.Rows[0]["SubUnitIndx"], "0", Request.QueryString["search"]));
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
                Request.QueryString["subId"], dt.Rows[0]["SubUnitIndx"], Request.QueryString["teamId"], Request.QueryString["teamV"], Request.QueryString["search"]));
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

    public string GetQuery()
    {
        return AdminQueries.TestGetMainTableWithPlan;
        if (Request.Form["MustReport"] != null)
            return AdminQueries.GetMainTableNoPlan;
        return AdminQueries.GetMainTableWithPlan;
    }
    public DataTable GetMainTableValue(string query)
    {
        var dbp = new DBParameterCollection();
        dbp.Add(new DBParameter("@UseMonth", Request.Form["month"]));
        dbp.Add(new DBParameter("@UseYear", Request.Form["year"]));
        dbp.Add(new DBParameter("@ReportStatus", Request.Form["ReportStatus"]));
        dbp.Add(new DBParameter("@Hierarchy", Session["Hierarchy"]));
        dbp.Add(new DBParameter("@JobHierarchy", DBNull.Value));
        dbp.Add(new DBParameter("@JobIndx", Session["JobIndx"]));
        dbp.Add(new DBParameter("@DepIndx", Session["DepIndx"]));
        dbp.Add(new DBParameter("@UnitIndx", Request.Form["unit"]));
        dbp.Add(new DBParameter("@SubUnitIndx", Request.Form["SubUnit"]));
        dbp.Add(new DBParameter("@CodeTeam", Request.Form["team"]));
        dbp.Add(new DBParameter("@UserIndx", dbConn.ExecuteQuery(AdminQueries.NameToIndex,new DBParameter("@FullName",Request.Form["WorkerName"])).Rows[0]["UserIndx"]));
        var dt = dbConn.ExecuteQuery(query, dbp);
        return dt;
    }
    public string[] SetHeadersArray()
    {
        var headers = new string[10];
        headers[0] = "פירוט";
        headers[1] = "בחר";
        headers[2] = "שם עובד";
        headers[3] = "תפקיד";
        headers[4] = "תאריך סטטוס";
        headers[5] = "סטטוס דיווח";
        headers[6] = "ימי היעדרות";
        headers[7] = "ביצוע/תכנון";
        headers[8] = "הערת העובד";
        headers[9] = "הערת המאשר";
        return headers;
    }
    public void CreateHtmlTable(DataTable dt)
    {
        HtmlTable htmlTable = new HtmlTable();
        var headers = SetHeadersArray();
        htmlTable.AddHeader(headers);
        foreach (DataRow dr in dt.Rows)
        {
            htmlTable.AddRow();
            htmlTable.AddCell("<a href='#'><img src='../img/plus-icon.png' height='20' width='20'/></a>");
            htmlTable.AddCell("<form><input type='checkbox' name='choose'>");
            htmlTable.AddCell(string.Format("<a href='#'>{0}</a>",dr["FullName"].ToString()));
            htmlTable.AddCell(dr["JobName"].ToString());
            htmlTable.AddCell(DateTime.Today.ToString("MM/dd/yyyy"));
            htmlTable.AddCell(dr["ReportStatus"].ToString());
            htmlTable.AddCell(dr["DaysoffWork"].ToString());
            htmlTable.AddCell(dr["PersentIncremental"].ToString());
            htmlTable.AddCell(dr["RemarkUser"].ToString());
            htmlTable.AddCell("<input type='text'/>");
        }
        htmlTable.close();
        Response.Write(htmlTable.GetHtmlTable());
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        PrintToolBar();
    }
}