using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DALC4NET;
using System.Data;
using System.Data.SqlClient;

public partial class user_SpecifyPerforms : System.Web.UI.Page
{
    DBConnector dbConn = new DBConnector();
    Table table = new Table();
    PrintHtml printHtml = new PrintHtml();

    string htmlTable = @"
<table class='table table-bordered' dir='rtl'>
        <thead>
            <tr>
                 <th> שם עובד</th>
                 <th> סוג פעילות</th>
                 <th>תאריך ביצוע</th>
                 <th> תאריך דיווח</th>
                 <th> כמות</th>
                 <th> אחוז ביצוע</th>
                 <th> תיאור פעילות</th>
                 <th> עריכה</th>
                 <th> מחיקה</th>
           </tr>
        </thead>";
    public void PrintToolBar()
    {
        Response.Write(printHtml.GetFrame("UserToolBar.html"));
    }
    public void PrintTable()
    {
        var dbp = new DBParameterCollection();
        dbp.Add(new DBParameter("@MeasureIndx", Request.QueryString["Indx"]));
        Session["MeasureIndx"] = Request.QueryString["Indx"];
        dbp.Add(new DBParameter("@UserJbGrpIndx", Session["UserJbGrpIndx"]));
        var cols = new string[6];
        cols[0] = "EventName";
        cols[1] = "DateEvent";
        cols[2] = "Update_date";
        cols[3] = "Quntity";
        cols[4] = "Persent";
        cols[5] = "Description";
        var dt = dbConn.ExecuteQuery(Queries.SpecifyDetails, dbp);
        var dict = table.GetColors();
        var i = 0;
        htmlTable += "<tbody>"; //add UserEventIndx as get parameter and send it to update page
        foreach (DataRow row in dt.Rows)
        {
            Session["UserEventIndx"] = row["UserEventIndx"].ToString();
            Session["MsrEventIndx"] = row["MsrEventIndx"].ToString();
            Session["UserJbGrpIndx"] = row["UserJbGrpIndx"].ToString();
            htmlTable += "<a href='UserHomePage.aspx'>";
            htmlTable += string.Format("<tr class='{0}'>", dict[i % 3]);
            htmlTable += string.Format("<td>{0}</td>", Session["FullName"].ToString());
            for (int j = 0; j < cols.Length; j++)
            {
               htmlTable += string.Format("<td>{0}</td>", row[cols[j]]);
            }
            
            htmlTable += string.Format(@"<td><a href='UpdatePerform.aspx?id="+row["UserEventIndx"].ToString()+@"'><img src = '..\img\edit-icon.png' 
                    height='20' width='20'/></a></td>", row["UserEventIndx"].ToString());
            htmlTable += string.Format(@"<td><a href='SpecifyPerforms.aspx?Indx={0}&DelId={1}'><img src = '..\img\delete-icon.png' 
                    height='20' width='20'/></a></td>", Request.QueryString["Indx"].ToString(), row["UserEventIndx"].ToString());
            htmlTable += "</tr>";
           
            i++;
        }
        htmlTable += "</tbody>";
        htmlTable += "</table>";
        Response.Write(htmlTable);
    }
    public void SaveReport()
    {
        /*     Set    DoneMonth          = @DoneMonth,
	    IncrementalDone    = @CIncrementalDone,
		Remark             = @Remark, 
        PresentMonth       = @CPresentMonth,
        PresentIncremental = @CPresentIncremental,
        Update_User        = @Update_User ,   
        Update_date        = GETDATE()
 Where  PrfMsrIndx         = @PrfMsrIndx 
*/
        var dbp = new DBParameterCollection();
        dbp.Add(new DBParameter("@DoneMonth", Request.Form["DoneMonth"]));
        dbp.Add(new DBParameter("@PrfMsrIndx", Session["PrfMsrIndx"]));
        dbp.Add(new DBParameter("@CPresentMonth", Session["CPresentMonth"]));
        dbp.Add(new DBParameter("@CPresentIncremental", Session["CPresentIncremental"]));
        dbp.Add(new DBParameter("@CIncrementalDone", Session["CIncrementalDone"]));
        dbp.Add(new DBParameter("@Update_User", Session["Update_User"]));
        dbp.Add(new DBParameter("@Remark", Session["Remark"]));
        dbConn.ExecuteQuery(Queries.UpdateReport, dbp);
    }
    public void UpdateReport()
    {
        var UpdateDbp = new DBParameterCollection();
        UpdateDbp.Add(new DBParameter("@RemarkUser", Request.Form["RemarkUser"].ToString()));
        UpdateDbp.Add(new DBParameter("@DaysOffWork", Request.Form["DaysOff"].ToString()));
        UpdateDbp.Add(new DBParameter("@DaysSick", Request.Form["DaysSick"].ToString()));
        UpdateDbp.Add(new DBParameter("@UseMonth", Session["Month"].ToString()));
        UpdateDbp.Add(new DBParameter("@UserJbGrpIndx", Session["UserJbGrpIndx"].ToString()));
        dbConn.ExecuteQuery(Queries.UpdateSecondReport, UpdateDbp);

    }
    public void DeleteEvent()
    {
        var dbParam = new DBParameterCollection();
        dbParam.Add(new DBParameter("UserEventIndx", Request.QueryString["DelId"].ToString()));
        dbConn.ExecuteQuery(Queries.DeleteEvent, dbParam);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if(!(new IsLogOn().IsLogedOn()))
            Response.Redirect(@"..\LogIn.aspx");
        Session["SpecifyIndx"] = Request.QueryString["Indx"].ToString();
        if (Request.QueryString["DelId"] != null)
            DeleteEvent();
        if (Request.Form["RemarkUser"] != null)
        {
            UpdateReport();
            Response.Write("<script>alert('הדיווח הסתיים בהצלחה!');</script>");
            Response.Redirect("UserHomePage.aspx");
        }

     
            PrintToolBar();   
    }
}