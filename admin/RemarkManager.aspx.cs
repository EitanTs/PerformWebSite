using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DALC4NET;
public partial class admin_RemarkManager : System.Web.UI.Page
{
    DBConnector dbConn = new DBConnector();
    public void DeleteAllMessages()
    {
        return;
    }
    public void SaveRemark(string query, string remark)
    {
        var dbp = new DBParameterCollection();
        dbp.Add(new DBParameter("@ReportStatus", 3));
        dbp.Add(new DBParameter("@RemarkManager", remark));
        dbp.Add(new DBParameter("@Update_User", Session["UserID"]));
        dbConn.ExecuteQuery(string.Format(query, Session["ChosenLines"]), dbp);
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["del"] != null)
        {
            SaveRemark(AdminQueries.UpdateRemarkManager, "");
        }
        if(Request.Form["save"] != null)
        {
            if(Request.QueryString["status"] != null)
                SaveRemark(AdminQueries.RejectReport, Request.Form["remark"]);
            else
                SaveRemark(AdminQueries.UpdateRemarkManager, Request.Form["remark"]);
            
        }
    }
}