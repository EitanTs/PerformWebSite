using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for PrintHtml
/// </summary>
public class PrintHtml : System.Web.UI.Page
{
    public string GetFrame(string filename)
    {
        string path = Server.MapPath("./" + filename);
        return System.IO.File.ReadAllText(path);
    }

	public PrintHtml()
	{
		//
		// TODO: Add constructor logic here
		//
	}
}