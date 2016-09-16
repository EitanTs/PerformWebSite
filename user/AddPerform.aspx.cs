using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DALC4NET;
using System.Data;
using System.Data.SqlClient;

public partial class AddPerform : System.Web.UI.Page
{
    
    PrintHtml printHtml = new PrintHtml();
    DBConnector dbConn = new DBConnector();
    Table table = new Table();
    DBParameterCollection dbp = new DBParameterCollection();

    public void PrintToolBar()
    {
        Response.Write(printHtml.GetFrame("UserToolBar.html"));
    }
    public void PrintTable()
    {
        var cols = new string[2];
        cols[0] = "UserName";
        cols[1] = "Password";
        var myList = dbConn.ExecuteQuery("SELECT * FROM users");
        Response.Write(table.DataTableToHtml(cols, myList));

    }
    public string GetDate()
    {
        return string.Format("{0}-{1}-{2}", Request.Form["year"], Request.Form["month"], Request.Form["day"]);
    }
    public void InsertValuesParameters()
    {

        dbp.Add(new DBParameter("@UserJbGrpIndx", Session["UserJbGrpIndx"]));
        dbp.Add(new DBParameter("@DateEvent", DateTime.Parse(string.Format("{0}-{1}-{2}", Request.Form["year"],
            Request.Form["month"]   , Request.Form["day"]))));
        dbp.Add(new DBParameter("@Persent", Request.Form["percent"].ToString()));
        dbp.Add(new DBParameter("@Description", Request.Form["description"].ToString()));
        dbp.Add(new DBParameter("@Update_User", Session["UserIndx"].ToString()));
        dbp.Add(new DBParameter("@Quntity", Request.Form["quantity"].ToString()));
        dbp.Add(new DBParameter("@MsrEventIndx", Request.Form["MsrEventIndx"].ToString()));
        dbp.Add(new DBParameter("@status", 1));
    }
    public void InsertUpdateDB()
    {
        Response.Write("<h1>" + Request.Form["MsrEventIndx"].ToString() + "</h1>");
        InsertValuesParameters();
        dbConn.ExecuteQuery(Queries.InsertUserEvent, dbp);
        Response.Redirect(string.Format("SpecifyPerforms.aspx?Indx={0}", Session["SpecifyIndx"]));
    }
    public string MeasureToEvent()
    {
        var str = "";
        var dbp = new DBParameterCollection();
        dbp.Add(new DBParameter("@MeasureIndx", Session["MeasureIndx"]));
        var dt = dbConn.ExecuteQuery(Queries.GetEventName, dbp);
        foreach (DataRow dr in dt.Rows)
        {
            Response.Write(string.Format("<option value='{0}'>{1}</option>", dr["MsrEventIndx"].ToString(), dr["EventName"]));
        }
        return str;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!(new IsLogOn().IsLogedOn()))
            Response.Redirect(@"..\LogIn.aspx");
        if (Request.Form["percent"] != null)
            InsertUpdateDB();
        PrintToolBar();
    }
}