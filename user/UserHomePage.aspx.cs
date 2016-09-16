using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DALC4NET;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading;
using System.IO;

public partial class UserHomePage : System.Web.UI.Page
{
    bool IsReportEnded = false;
    bool isPrint = false;
    string month;
    string year;
    Dictionary<string, string> values = new Dictionary<string, string>();
    DBParameterCollection dbp = new DBParameterCollection();
    PrintHtml printHtml = new PrintHtml();
    Table table = new Table();
    DBConnector dbConn = new DBConnector();
    
    string htmlTable = "";
   
    public void PrintToolBar()
    {
        Response.Write(printHtml.GetFrame("UserToolBar.html"));
    }
    public void InsertValuesToParameters()
    {
        
        dbp.Add(new DBParameter("@UserIndx", Session["UserIndx"].ToString()));
        dbp.Add(new DBParameter("@JobIndx", Session["JobIndx"]));
        dbp.Add(new DBParameter("@JobType", Session["JobType"]));
        if (Request.QueryString["Search"] != null)
        {
            dbp.Add(new DBParameter("@UseMonth", Session["Month"]));
            dbp.Add(new DBParameter("@UseYear", Session["Year"]));
        }
        else
        {
            Session["Month"] = month;
            Session["Year"] = year;
            dbp.Add(new DBParameter("@UseMonth", month));
            dbp.Add(new DBParameter("@UseYear", year));
        }
    }
    public void PrintMainTable()
    {
        var updateUser = "";
        var updateDate = "";
            htmlTable += @"    
            <table class='table'>
                <thead>
                <tr>
                    <th>תיאור</th><th>סוג מדד</th><th>משקולות</th><th>תכנון חודשי</th><th>ביצוע חודשי</th><th>אחוז ביצוע/תכנון</th>
                    <th>תכנון מצטבר</th><th>ביצוע מצטבר</th><th>אחוז ביצוע/תכנון</th><th>הערה</th><th>פירוט</th>
                </tr>
                </thead>
                <tbody>
       ";
        var cols = new string[10];
        cols[0] = "MeasureName";
        cols[1] = "MeasureType";
        cols[2] = "Score";
        cols[3] = "PlanMonth";
        cols[4] = "DoneMonth";
        cols[5] = "PresentMonth";
        cols[6] = "IncrementalPlan";
        cols[7] = "IncrementalDone";
        cols[8] = "PresentIncremental";
        cols[9] = "Remark";
        var dt = dbConn.ExecuteQuery(Queries.HomePageMainTable, dbp);
        if (dt.Rows.Count > 0)
        {
            updateUser = dt.Rows[0]["Update_User"].ToString();
            updateDate = DateTime.Parse(dt.Rows[0]["update_date"].ToString()).ToString("MM/dd/yyyy");
        }
        Excel.ParseDtToXL(dt, string.Format(ConfigurationManager.AppSettings["path"].ToString(), "MainTable.csv"),cols);
        var dict = table.GetColors();
        var i = 0;
        bool flag = true;
        foreach (DataRow row in dt.Rows)
        {
            Session["MeasureIndx"] = row["MeasureIndx"];
            if (flag)
                htmlTable += "<tr bgcolor='#FFE4C4'>";
            else
                htmlTable += "<tr>";
            flag = !flag;
            for (int j = 0; j < cols.Length; j++)
            {
                if (cols[j] == "DoneMonth")
                {
                    htmlTable += string.Format("<td><input type='text' name='DoneMonth" + i + "' value='{0}'/></td>", row[cols[j]]);
                }
                else
                {
                    if (cols[j] == "Remark")
                    {
                        htmlTable += "<td>";

                        htmlTable += string.Format("<input type='text' name='Remark" + i + "' value='{0}'/></td>", row[cols[j]]);
                    }
                    else
                    {
                        htmlTable += "<td>";
                        htmlTable += row[cols[j]];
                        htmlTable += "</td>";
                    }
                    Session[string.Format("PrfMsrIndx{0}",i)] = row["PrfMsrIndx"].ToString();
                    Session[string.Format("PlanMonth{0}",i)] = row["PlanMonth"].ToString();
                    Session[string.Format("IncrementalDone{0}",i)] = row["IncrementalDone"].ToString();
                    Session[string.Format("DoneMonthOld{0}",i)] = row["DoneMonth"].ToString();
                    Session[string.Format("IncrementalPlan{0}",i)] = row["IncrementalPlan"].ToString();
                }
            }
            htmlTable += GetColor(row["MeasureIndx"].ToString());
            htmlTable += "</tr>";
            i++;
        }
        htmlTable += string.Format("<input type=hidden name='Length' id='Length' value='{0}'/>", i);
        htmlTable += "</tbody>";
        var disabled = "";
        if (IsReportEnded)
        {
            disabled = "disabled='disabled' class='btn-disable'";
        }
        htmlTable += string.Format(@"</table> <br /> <br />
            <table dir='ltr' align='left'>
            <tr>
            <td>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</td>           
            <td><input type='submit' value='שמור' style='float:left;' name='save' {0}/></td>
            <td>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</td>
            <td><input type='submit' value='סיים דיווח' style='float:left;' name='EndReport' {0}/></td>
            <td>&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp</td>
            <td><input type='submit' value='בטל דיווח' style='float:left;' name='CancelReport' /></td>
            <td>&nbsp &nbsp <button><a href='../excel/MainTable.csv' download>הפק דוח אקסל</a></button></td>
            <td>&nbsp &nbsp <button onclick='OpenAlert(""Update User: {1} \r\n Update Date: {2}"")'>הצג משתמש מעדכן</button></td>
            </tr>
            </table>
            </form>", disabled, updateUser, updateDate);
    
        Response.Write(htmlTable);
    }
    public string GetColor(string measureIndx)
    {
        var dbpSpecify = new DBParameterCollection();
        dbpSpecify.Add(new DBParameter("@MeasureIndx", measureIndx));
        dbpSpecify.Add(new DBParameter("@UserJbGrpIndx", Session["UserJbGrpIndx"]));
        var dt = dbConn.ExecuteQuery(Queries.SpecifyDetails, dbpSpecify);
        if (dt.Rows.Count == 0)
        {
            return string.Format("<td bgcolor='#5c5c3d'><a href=SpecifyPerforms.aspx?Indx={0} style='color:#F0FFFF';>פירוט</a></td>", measureIndx);
        }
        foreach (DataRow dr in dt.Rows)
        {
            try
            {
                if (DateTime.Parse(dr["DateEvent"].ToString()).Month == int.Parse(Request.Form["month"].ToString()))
                {
                    return string.Format("<td  bgcolor='#ff6600'><a href=SpecifyPerforms.aspx?Indx={0} style='color:#F0FFFF';>פירוט</a></td>", measureIndx);
                }
            }
            catch
            {
                if (DateTime.Parse(dr["DateEvent"].ToString()).Month == int.Parse(Session["Month"].ToString()))
                {
                    return string.Format("<td bgcolor='#ff6600'><a href=SpecifyPerforms.aspx?Indx={0} style='color:#F0FFFF';>פירוט</a></td>", measureIndx);
                }
            }
        }
        return string.Format("<td bgcolor='#0052cc'><a href=SpecifyPerforms.aspx?Indx={0} style='color:#F0FFFF';>פירוט</a></td>", measureIndx);
    }
    public void PrintSecondTable()
    {
        var htmlTable= "";
        if (values["RemarkManager"].Length < 2)
            htmlTable += string.Format(@"<form action='UserHomePage.aspx?Search=1' method='post'><table class='table' dir='rtl'>
            <tr > <td>הערת מנהל:</td> <td>{0}</td> <td>הערת משתמש:</td><td><input type='text' id='RemarkUser' name='RemarkUser' value='{1}'/>
            </td><td></td><td></td><td></td><td><br/><br/></td></tr>
            <tr><td><br /></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>
            <tr bgcolor='#FFE4C4'><td title='ימי חופשה מתחילת השנה כולל חודש נוכחי'>ימי חופשה מצטברים:</td><td>{2}</td>
            <td title='ימי מחלה מתחילת השנה כולל חודש נוכחי'>ימי מחלה מצטברים:</td><td>{3}</td>
            <td>ימי חופשה:</td><td><input type='text' name='daysOffWork' value='{4}'/></td>   
            <td>ימי מחלה:</td><td><input type='text' name='sickDays' value='{5}'/></td></tr>
            </table>", "אין", values["RemarkUser"], values["DaysOffWorkYear"], values["SickDaysYear"],values["DaysOffWork"], values["SickDays"]);
        else
            htmlTable += string.Format(@"<form action='UserHomePage.aspx' method='post'><table class='table' dir='rtl'>
            <tr > <td>הערת מנהל:</td> <td>{0}</td> <td>הערת משתמש:</td><td><input type='text' id='RemarkUser' name='RemarkUser' value='{1}'/>
            </td><td></td><td></td><td></td><td><br/><br/></td></tr>
            <tr><td><br /></td></tr>
            <tr bgcolor='#FFE4C4'><td title='ימי חופשה מתחילת השנה כולל חודש נוכחי'>ימי חופשה מצטברים:</td><td>{2}</td>
            <td title='ימי מחלה מתחילת השנה כולל חודש נוכחי'>ימי מחלה מצטברים:</td><td>{3}</td>
            <td>ימי חופשה:</td><td><input type='text' name='daysOffWork' value='{4}'/></td>   
            <td>ימי מחלה:</td><td><input type='text' name='sickDays' value='{5}'/></td></tr>
            </table>", values["RemarkManager"], values["RemarkUser"], values["DaysOffWorkYear"], values["SickDaysYear"], values["DaysOffWork"], values["SickDays"]);
        Response.Write(htmlTable);
    }
    public void GetTableValues(Dictionary<string, string> a)
    {
        var dt = dbConn.ExecuteQuery(Queries.HomePageSecondTable, dbp);
        var dbParam = new DBParameterCollection();
        if (dt.Rows.Count == 0)
        {
            a.Add("RemarkManager", "");
            a.Add("RemarkUser", "");
            a.Add("DaysOffWorkYear", "");
            a.Add("SickDaysYear", "");
            a.Add("DaysOffWork", "");
            a.Add("SickDays", "");
            a.Add("PersentPlnYear", "");
            a.Add("ReportStatus", "");
            a.Add("PersentIncremental", "");
            a.Add("G_Update_date", "");
            a.Add("ReportStatusName", "");
            return;
        }
        var row = dt.Rows[0];
        if (row["ReportStatus"].ToString() == "2")
            IsReportEnded = true;
        Session["sickDays"] = row["sickDays"];
        Session["daysOffWork"] = row["daysOffWork"];
        Session["sickDaysYear"] = row["sickDaysYear"];
        Session["daysOffWorkYear"] = row["daysOffWorkYear"];
        Session["RemarkManager"] = row["RemarkManager"].ToString();
        a.Add("RemarkManager", row["RemarkManager"].ToString());
        a.Add("RemarkUser", row["RemarkUser"].ToString());
        a.Add("DaysOffWorkYear", row["DaysOffWorkYear"].ToString());
        a.Add("SickDaysYear", row["SickDaysYear"].ToString());
        a.Add("DaysOffWork", row["DaysOffWork"].ToString());
        a.Add("SickDays", row["SickDays"].ToString());
        a.Add("PersentPlnYear", row["PersentPlnYear"].ToString());
        a.Add("ReportStatus", row["ReportStatus"].ToString());
        a.Add("PersentIncremental", row["PersentIncremental"].ToString());
        a.Add("G_Update_date", row["G_Update_date"].ToString());
        dbParam.Add(new DBParameter("ReportStatus", a["ReportStatus"]));
        a.Add("ReportStatusName", dbConn.ExecuteQuery(Queries.GetStatusName, dbParam).Rows[0]["SupportDTLName"].ToString());
    }
    public Dictionary<string, string> GetValuesDictionary()
    {
        return values;
    }
    public bool InsertFormValues()
    {
        if (Request.Form["month"] != null && Request.Form["year"] != null)
        {
            month = Request.Form["month"].ToString();
            year = Request.Form["year"].ToString();
            Session["Month"] = month;
            Session["Year"] = year;
            return true;
        }
        else
        {
            try
            {
                month = Session["Month"].ToString();
                year = Session["Year"].ToString();
            }
            catch
            {
                month = "";
                year = "";
            }
            }
        return isPrint;
    }
    public bool GetPrint()
    {
        return isPrint;
    }
    public void UpdateReport()
    {
        var UpdateDbp = new DBParameterCollection();
        UpdateDbp.Add(new DBParameter("@RemarkUser", Request.Form["RemarkUser"].ToString()));
        UpdateDbp.Add(new DBParameter("@DaysOffWork", Request.Form["daysOffWork"].ToString()));
        UpdateDbp.Add(new DBParameter("@DaysSick", Request.Form["sickDays"].ToString()));
        UpdateDbp.Add(new DBParameter("@UseMonth", Session["Month"].ToString()));
        UpdateDbp.Add(new DBParameter("@UserJbGrpIndx", Session["UserJbGrpIndx"].ToString()));
        UpdateDbp.Add(new DBParameter("@SickDaysYear", CalculateDays("sickDays")));
        UpdateDbp.Add(new DBParameter("@DaysOffWorkYear", CalculateDays("daysOffWork")));
        dbConn.ExecuteQuery(Queries.UpdateSecondReport, UpdateDbp);

    }
    public void UpdateEndReport(int reportStatus)
    {
        var UpdateDbp = new DBParameterCollection();
        UpdateDbp.Add(new DBParameter("@UserJbGrpIndx", Session["UserJbGrpIndx"].ToString()));
        UpdateDbp.Add(new DBParameter("@UpdateReportStatus", reportStatus));
        if (Session["Month"] != null)
        {
            UpdateDbp.Add(new DBParameter("@UseMonth", Session["Month"])); 
        }
        else
        {
            UpdateDbp.Add(new DBParameter("@UseMonth", DateTime.Now.Month));
        }
        dbConn.ExecuteQuery(Queries.UpdateEndReport, UpdateDbp);
    }
    public int CalculateDays(string type)
    {
        var typeYear = type + "Year";
        int daysYear = int.Parse(Session[typeYear].ToString());
        int daysNew = int.Parse(Request.Form[type].ToString());
        int daysOld = int.Parse(Session[type].ToString());
        return daysYear + daysNew - daysOld;
    }
    public void UpdateMainReport()
    {
        
        for (int i = 0; i < int.Parse(Request.Form["Length"].ToString()); i++)
        {
            var dbp = new DBParameterCollection();
            dbp.Add(new DBParameter("@DoneMonth", Request.Form[string.Format("DoneMonth{0}", i)]));
            dbp.Add(new DBParameter("@PresentMonth", CalculateMonthPercent(i).ToString()));
            dbp.Add(new DBParameter("@IncrementalDone", CalculateIncremental(i)));
            dbp.Add(new DBParameter("@PresentIncremental", CalculateIncrementalPercent(i)));
            dbp.Add(new DBParameter("@PrfMsrIndx", Session[string.Format("PrfMsrIndx{0}", i)]));
            dbp.Add(new DBParameter("@Remark", Request.Form[string.Format("Remark{0}", i)]));
            dbConn.ExecuteQuery(Queries.UpdateReport, dbp);
            dbp.RemoveAll();
        }
    }
    public int CalculateMonthPercent(int i)
    {
        double done = double.Parse(Request.Form[string.Format("DoneMonth{0}", i)].ToString());
        double plan = double.Parse(Session[string.Format("PlanMonth{0}", i)].ToString());
        return int.Parse(Math.Round(done / plan * 100).ToString());
    }
    public int CalculateIncremental(int i)
    {
        int DoneNew = int.Parse(Request.Form[string.Format("DoneMonth{0}", i)].ToString());
        int DoneOld = int.Parse(Session[string.Format("DoneMonthOld{0}", i)].ToString());
        int incremental = int.Parse(Session[string.Format("IncrementalDone{0}", i)].ToString());
        return incremental + DoneNew - DoneOld;
    }
    public int CalculateIncrementalPercent(int i)
    {
        double incrementalDone = double.Parse(CalculateIncremental(i).ToString());
        double incrementalPlan = double.Parse(Session[string.Format("IncrementalPlan{0}", i)].ToString());
        return int.Parse(Math.Round(incrementalDone / incrementalPlan * 100).ToString());
    }
    public void UpdateAllReports()
    {
        if (Request.Form["RemarkUser"] != null)
        {
            UpdateReport();
            UpdateMainReport();
            if (Request.Form["EndReport"] != null)
            {
                UpdateEndReport(2);
            }
            if (Request.Form["CancelReport"] != null)
            {
                UpdateEndReport(1);
            }
        }
    }
    public void AutoSearch()
    {
        if(Request.QueryString["Search"] != null)
            isPrint = true;
    }
    public string GetJobName()
    {
        dbp.Add(new DBParameter("@SupportDTLNum", Session["JobType"]));
        var jobType = dbConn.ExecuteQuery(Queries.GetJobType, dbp).Rows[0]["SupportDTLName"].ToString();
        return jobType;
    }
    public string GetLastUserUpdate()
    {
        var lastUser = "";
        
        return lastUser;
    }

    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(new IsLogOn().IsLogedOn()))
            Response.Redirect(@"..\LogIn.aspx");
        AutoSearch();
        UpdateAllReports();
        isPrint = InsertFormValues();
        InsertValuesToParameters();
        GetTableValues(values);
        PrintToolBar();
    }
}