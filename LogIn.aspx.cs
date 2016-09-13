using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using DALC4NET;
using System.Configuration;


public partial class LogIn : System.Web.UI.Page
{
    string TestUser = "u9999";
    string TestPassword = "123456";
    string AdminUrl = "admin/AdminReport.aspx?unitV=0&subV=0&teamV=0";
    string UserUrl = "User/UserHomePage.aspx?Search=1";
    DBConnector dbConn = new DBConnector();
    public void SaveSessionValues(DataTable dt)
    {
        Session["Month"] = DateTime.Now.Month;
        Session["Year"] = DateTime.Now.Year;
        Session["UserIndx"] = dt.Rows[0]["UserIndx"].ToString();
        Session["FullName"] = dt.Rows[0]["FullName"].ToString();
        Session["UserID"] = dt.Rows[0]["UserID"].ToString();
        Session["JobUserIndx"] = dt.Rows[0]["JobUserIndx"].ToString();
        Session["JobIndx"] = dt.Rows[0]["JobIndx"].ToString();
        Session["Hierarchy"] = dt.Rows[0]["Hierarchy"].ToString();
        Session["Permission"] = dt.Rows[0]["Permission"].ToString();
        Session["JobType"] = dt.Rows[0]["JobType"].ToString();
        Session["CompanyIndx"] = dt.Rows[0]["CompanyIndx"].ToString();
        Session["DepIndx"] = dt.Rows[0]["DepIndx"].ToString();
        Session["UnitIndx"] = dt.Rows[0]["UnitIndx"].ToString();
        Session["UserJbGrpIndx"] = dt.Rows[0]["UserJbGrpIndx"].ToString();
        Session["SubUnitIndx"] = dt.Rows[0]["SubUnitIndx"].ToString();
        Session["CodeTeam"] = dt.Rows[0]["CodeTeam"].ToString();
        Session["StartJob"] = dt.Rows[0]["StartJob"].ToString();
        Session["PrfGrpIndx"] = dt.Rows[0]["PrfGrpIndx"].ToString();
        Session["JobHierarchy"] = dt.Rows[0]["JobHierarchy"].ToString();
        Session["EndJob"] = dt.Rows[0]["EndJob"].ToString();
        Session["JobName"] = dt.Rows[0]["JobName"].ToString();
    }
    public DataTable GetUserDetails(string user, string password)
    {
        var dbp = new DBParameterCollection();
        dbp.Add(new DBParameter("UserID", user));
        dbp.Add(new DBParameter("Password", password));
        var dt = dbConn.ExecuteQuery(Queries.LoginQuery, dbp);
        return dt;
    }
    public void CheckUser()
    {
        var dt = GetUserDetails(Request.Form["user"].ToString(), Request.Form["password"].ToString());
        if (dt.Rows.Count > 0)
        {
            SaveSessionValues(dt);
            Response.Redirect(UserUrl);
        }
        else
        {
            Response.Write("<script>alert('login failed');</script>");
        }
    }

    //I don't have admin so I created function to fake details
    public void IsAdmin()
    {
        if (Request.Form["user"] == "admin")
        {
            var dt = GetUserDetails(TestUser, TestPassword);
            SaveSessionValues(dt);
            Session["Hierarchy"] = "31";
            Response.Redirect(AdminUrl);
        }
    }
  
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.Form["user"] != null && Request.Form["password"] != null)
        {
            IsAdmin();
            CheckUser();
        }
    }
}