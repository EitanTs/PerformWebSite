using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

public partial class admin_RemarkManager : System.Web.UI.Page
{
    public void DeleteAllMessages()
    {
        return;
    }
    public void SaveRemark()
    { 
    
    }
    protected void Page_Load(object sender, EventArgs e)
    {
        if (Request.QueryString["del"] != null)
        {
            DeleteAllMessages();
        }
        if(Request.Form["save"] != null)
        {
            SaveRemark();    
        }
    }
}