using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DALC4NET;
using System.Data;
using System.Data.SqlClient;

public partial class user_UpdateProfile : System.Web.UI.Page
{
    PrintHtml printHtml = new PrintHtml();
    DBConnector dbConn = new DBConnector();
    Table table = new Table();
    DBParameterCollection dbp = new DBParameterCollection();
    DataRow EventValues = null;
    Select mySelect = new Select();
    public void PrintValue(string col)
    {
        Response.Write(EventValues[col]);
    }
    public void PrintToolBar()
    {
        Response.Write(printHtml.GetFrame("UserToolBar.html"));
    }
    public void PrintYears()
    {
        if (EventValues != null)
        {
            Response.Write(mySelect.GetSelectDesc(2016, 15, "{0}", DateTime.Parse(EventValues["DateEvent"].ToString()).Year));
        }
        else
        {
            Response.Write(mySelect.GetSelectDesc(2016, 15, "{0}"));
        }
    }
    public void PrintMonths()
    {
        if (EventValues != null)
        {
            Response.Write(mySelect.GetSelectAsc(1, 12, "{0}", DateTime.Parse(EventValues["DateEvent"].ToString()).Month));
        }
        else
        {
            Response.Write(mySelect.GetSelectAsc(1, 12, "{0}"));
        }
}
    public void PrintDays()
    {
        if (EventValues != null)
        {
            Response.Write(mySelect.GetSelectAsc(1, 31, "{0}", DateTime.Parse(EventValues["DateEvent"].ToString()).Day));
        }
        else
        {
            Response.Write(mySelect.GetSelectAsc(1, 31, "{0}"));
        }
    }
    public void PrintTable()
    {
        var cols = new string[2];
        cols[0] = "UserName";
        cols[1] = "Password";
        var myList = dbConn.ExecuteQuery("SELECT * FROM users");
        Response.Write(table.DataTableToHtml(cols, myList));

    }
    public void SetEventValues()
    {
        var param = new DBParameterCollection();
        param.Add(new DBParameter("@UserEventIndx", Session["UserEventIndx"]));
        var dt = dbConn.ExecuteQuery(Queries.GetEventValues, param);
        EventValues = dt.Rows[0];
    }
    public void InsertValuesParameters()
    {
        dbp.Add(new DBParameter("@UserEventIndx", Session["UserEventIndx"]));
        dbp.Add(new DBParameter("@DateEvent", DateTime.Parse(string.Format("{0}-{1}-{2}", Request.Form["year"],
        Request.Form["month"], Request.Form["day"]))));
        dbp.Add(new DBParameter("@Percent", Request.Form["percent"].ToString()));
        dbp.Add(new DBParameter("@Description", Request.Form["description"].ToString()));
        dbp.Add(new DBParameter("@Quantity", Request.Form["quantity"].ToString()));
    }
    public void InsertUpdateDB()
    {
        if (Request.Form["percent"] != null)
        {
            InsertValuesParameters();
            dbConn.ExecuteQuery(Queries.UpdateUserEvent, dbp);
            Response.Redirect(string.Format("SpecifyPerforms.aspx?Indx={0}", Session["SpecifyIndx"]));
        }

    }
    public string MeasureToEvent()
    {
        var str = "";
        var dbp = new DBParameterCollection();
        dbp.Add(new DBParameter("@MeasureIndx", Session["MeasureIndx"]));
        var dt = dbConn.ExecuteQuery(Queries.GetEventName, dbp);
        foreach (DataRow dr in dt.Rows)
        {
            if(dr["MsrEventIndx"].ToString() == EventValues["MsrEventIndx"].ToString())
                Response.Write(string.Format("<option value='{0}' selected>{1}</option>", dr["MsrEventIndx"].ToString(), dr["EventName"]));
            else
                Response.Write(string.Format("<option value='{0}'>{1}</option>", dr["MsrEventIndx"].ToString(), dr["EventName"]));
    
        }
        return str;
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["percent"] != null)
            InsertUpdateDB();
        if (Request.QueryString["id"] != null)
        {
            Session["UserEventIndx"] = Request.QueryString["id"];
            SetEventValues();
        }
        
        PrintToolBar();
    }
}