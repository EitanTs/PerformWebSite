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
    DBConnector dbConn = new DBConnector();

    public void CheckUser()
    {
        
        var dbp = new DBParameterCollection();
        if (Request.Form["user"] != null)
        {
            dbp.Add(new DBParameter("UserID", Request.Form["user"]));
            dbp.Add(new DBParameter("Password", Request.Form["password"]));
            var dt = dbConn.ExecuteQuery(Queries.LoginQuery, dbp);
            if (dt.Rows.Count > 0)
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
                Session["JSubUnitIndx"] = dt.Rows[0]["SubUnitIndx"].ToString();
                Session["CodeTeam"] = dt.Rows[0]["CodeTeam"].ToString();
                Session["StartJob"] = dt.Rows[0]["StartJob"].ToString();
                Session["PrfGrpIndx"] = dt.Rows[0]["PrfGrpIndx"].ToString();
                Session["JobHierarchy"] = dt.Rows[0]["JobHierarchy"].ToString();
                Session["EndJob"] = dt.Rows[0]["EndJob"].ToString();
                Session["JobName"] = dt.Rows[0]["JobName"].ToString();
                //if (Request.Form["user"] == "U9999")
                    //Response.Redirect("admin/AdminReport.aspx");
                Response.Redirect("user/UserHomePage.aspx?Search=1");
            }
            else
            {
                Response.Write("<script>alert('login failed');</script>");
            }
        }

    }
    
    protected void Page_Load(object sender, EventArgs e)
    {
      
        CheckUser();
    }
}