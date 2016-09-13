using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DALC4NET;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
public partial class admin_AdminReport : System.Web.UI.Page
{
    string UserIndx = "";
    Select mySelect = new Select();
    PrintHtml printHtml = new PrintHtml();
    DBConnector dbConn = new DBConnector();
    HtmlTable htmlTable = new HtmlTable();
    Table table = new Table();
    string url = "AdminReport.aspx?unitId={0}&unitV={1}&subId={2}&teamId={3}&search={4}";
    string SemiUrl = "AdminReport.aspx?unitId={0}&unitV={1}&subId={2}&subV={3}&teamId={4}&search={5}";
    string FullUrl = "AdminReport.aspx?unitId={0}&unitV={1}&subId={2}&subV={3}&teamId={4}&teamV={5}&search={6}";
    string OptionString = "<option value='{0}' selected>{1}</option>"; //selected only for debug version
    string formattedIndexes = "(";
    string OpenWindow = "<script>window.open('RemarkManager.aspx?1=1', '', 'height=200,width=800,left=300,top=300');</script>";
    string OpenRejectWindow = "<script>window.open('RemarkManager.aspx?1=1&status=3', '', 'height=200,width=800,left=300,top=300');</script>";
    bool IsChanged = false;


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
    public string ConvertReportStatus(string num)
    {
        var dbp = new DBParameter("@SupportDtlNum", num);
        var dt = dbConn.ExecuteQuery(AdminQueries.ReportStatusToName, dbp);
        if (dt.Rows.Count > 0)
        {
            return dt.Rows[0]["SupportDtlName"].ToString();
        }
        return "";
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
        var dt = dbConn.ExecuteQuery(AdminQueries.NameToIndex,new DBParameter("@FullName",Request.Form["WorkerName"]));
        if (dt.Rows.Count > 0)
        {
            UserIndx = dt.Rows[0]["UserIndx"].ToString();
        }
        dbp.Add(new DBParameter("@UserIndx", UserIndx));
        dt = dbConn.ExecuteQuery(query, dbp);
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
    public string GetJobType()
    {
        if (Session["JobType"].ToString() == "1")
            return "תפקיד ראשי";
        return "תפקד משני";
    }

    public void CreateHtmlHeader()
    {
        var headers = SetHeadersArray();
        htmlTable.AddHeader(headers);
    }
    public string GetExtension()
    {
        var headers = new string[12];
        headers[0] = "תיאור";
        headers[1] = "סוג מדד";
        headers[2] = "משקולות";
        headers[3] = "תכנון שנתי";
        headers[4] = "תכנון חודשי";
        headers[5] = "ביצוע חודשי";
        headers[6] = "אחוז/ביצוע תכנון";
        headers[7] = "תכנון מצטבר";
        headers[8] = "ביצוע מצטבר";
        headers[9] = "אחוז/ביצוע מצטבר";
        headers[10] = "הערות";
        headers[11] = "פירוט";
        HtmlTable extentionTable = new HtmlTable();
        extentionTable.AddHeader(headers);
        var dbp = new DBParameterCollection();
        dbp.Add(new DBParameter("@JobIndx", Session["JobIndx"]));
        dbp.Add(new DBParameter("@JobType", Session["JobType"]));
        dbp.Add(new DBParameter("@UseYear", Request.Form["year"]));
        dbp.Add(new DBParameter("@UseMonth", Request.Form["month"]));
        dbp.Add(new DBParameter("@UserIndx", UserIndx));
        var dt = dbConn.ExecuteQuery(AdminQueries.ReportExtension, dbp);
        foreach (DataRow dr in dt.Rows)
        {
            extentionTable.AddRow();
            extentionTable.AddCell(dr["MeasureName"].ToString());
            extentionTable.AddCell(dr["MeasureType"].ToString());
            extentionTable.AddCell(dr["Score"].ToString());
            extentionTable.AddCell(dr["PlanYear"].ToString());
            extentionTable.AddCell(dr["PlanMonth"].ToString());
            extentionTable.AddCell(dr["DoneMonth"].ToString());
            extentionTable.AddCell(dr["PresentMonth"].ToString());
            extentionTable.AddCell(dr["IncrementalPlan"].ToString());
            extentionTable.AddCell(dr["IncrementalDone"].ToString());
            extentionTable.AddCell(dr["PresentIncremental"].ToString());
            extentionTable.AddCell(dr["Remark"].ToString());
            extentionTable.AddCell("bgcolor", "#0052cc",
                string.Format("<a href=../../user/SpecifyPerforms.aspx?Indx={0} style='color:#F0FFFF';>פירוט</a></td>", dr["MeasureIndx"]));
        }
        extentionTable.close();
        return extentionTable.GetHtmlTable();
    }
    public void CreateHtmlTable(DataTable dt)
    {
        var i = 1;
        foreach (DataRow dr in dt.Rows)
        {
            var remarkUser = dr["RemarkUser"].ToString();
            if (remarkUser.ToString().Length < 2)
                remarkUser = "אין";
            htmlTable.AddRow();
            htmlTable.AddCell(string.Format(@"<a href='#'><img src='../img/plus-icon.png' height='20' width='20' onclick='OpenWindow(""Extensions/ExtensionTable{0}.html"")'/>", i));
            FileHandler.WriteContent(string.Format(ConfigurationManager.AppSettings["ExtensionPath"].ToString(), i), GetExtension());
            htmlTable.AddCell(string.Format("<input type='checkbox' id='choose{0}' name='choose{0}' onclick='ManipulateButtons()'>", i));
            htmlTable.AddCell(string.Format("<a href='#'>{0}</a>",dr["FullName"].ToString()));
            htmlTable.AddCell("title", GetJobType(),dr["JobName"].ToString());
            htmlTable.AddCell(DateTime.Today.ToString("MM/dd/yyyy"));
            htmlTable.AddCell(ConvertReportStatus(dr["ReportStatus"].ToString()));
            htmlTable.AddCell(dr["DaysoffWork"].ToString());
            htmlTable.AddCell(dr["PersentIncremental"].ToString());
            htmlTable.AddCell(remarkUser);
            htmlTable.AddCell(string.Format("<input type='text' name='RemarkManager' value='{2}'/><input type='hidden' name='PrfIndex{0}' value='{1}'", 
                i, dr["PrfGrpIndx"].ToString(), dr["RemarkManager"].ToString()));
            i++;
        }
    
    }
    public void PrintHtmlTable()
    {
        htmlTable.close();
        Response.Write(htmlTable.GetHtmlTable());
    }
    public void GetChosenLines()
    {
        var i = 1;
        var chooseId = string.Format("choose{0}", i);

        var indexId = string.Format("PrfIndex{0}", i);
        while (Request.Form[indexId] != null)
        {
            if (Request.Form[chooseId] == "on")
            {
                formattedIndexes += string.Format("{0},", Request.Form[indexId]);
            }
            i++;
            chooseId = string.Format("choose{0}", i);
            indexId = string.Format("PrfIndex{0}", i);
        }
        if (formattedIndexes.Length > 1)
        {
            formattedIndexes = formattedIndexes.Remove(formattedIndexes.Length - 1);
        }
        formattedIndexes += ")";
        Session["ChosenLines"] = formattedIndexes;
    }
    public void UpdateReportStatus(int reportStatus)
    {
        GetChosenLines();
        var dbp = new DBParameterCollection();
        dbp.Add(new DBParameter("@ReportStatus", reportStatus));
        dbp.Add(new DBParameter("@RemarkManager",Request.Form["RemarkManager"]));
        dbp.Add(new DBParameter("@Update_User", Session["UserID"]));
        dbConn.ExecuteQuery(string.Format(AdminQueries.ApproveReportStatus, formattedIndexes), dbp);
    }
    public void DownloadExcel()
    {
        GetChosenLines();
        var dt = dbConn.ExecuteQuery(string.Format(AdminQueries.ExcelMainTableValue, formattedIndexes));
        Excel.AdminParseDtToXL(dt, string.Format(ConfigurationManager.AppSettings["path"].ToString(), "AdminTable.csv"));
        Response.Write("<iframe width='1' height='1' frameborder='0' src='../excel/AdminTable.csv'></iframe>");
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        PrintToolBar();
        if (Request.Form["approve"] != null)
        {
            UpdateReportStatus(4);
        }
        if (Request.Form["reply"] != null)
        {
            UpdateReportStatus(3);
        }
        if (Request.Form["excel"] != null)
        {
            DownloadExcel();
        }
        if (Request.Form["reject"] != null)
        {
            GetChosenLines();
            Response.Write(OpenRejectWindow);
        }
        if (Request.Form["AddComment"] != null)
        {
            GetChosenLines();
            Response.Write(OpenWindow);
        }
    }
}