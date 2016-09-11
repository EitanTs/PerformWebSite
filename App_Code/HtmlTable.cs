using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for HtmlTable
/// </summary>
public class HtmlTable
{
    string head = @"<head> <link rel='stylesheet' href='http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css'/>
  <script type='text/javascript' src='https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js'>
  </script>
  <script type='text/javascript' src='http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js'></script>
  <style type ='text/css'>
    .table td
    {
        text-align:center;
    }
    .table th
    {
        text-align:center;    
    }
  </style></head><table class='table' dir='rtl'>";
    string id = "";
    string html = "";
    string row = "";
	public HtmlTable(bool enable=true, string id="table")
	{
        this.html = this.head;
	}
   public void AddHeader(string[] headers)
    {
        this.html += "<thead><tr>";
        foreach (string header in headers)
        {
            this.html += string.Format("<th>{0}</th>", header);
        }
        this.html += "</tr></thead>";
        this.html += "<tbody>";
    }
    public void AddRow()
    {
        if (this.row != "")
        {
            this.row += "</tr>";
            this.html += this.row;
        }
        this.row = "<tr>";
    }
    public void AddCell(string value)
    {
        string cell = string.Format("<td>{0}</td>", value);
        this.row += cell;
    }
    public void AddCell(string attributeName, string attributeValue, string value)
    {
        string cell = string.Format("<td {0}='{1}'>{2}</td>", attributeName, attributeValue, value);
        this.row += cell;
    }
    public void close()
    {
        this.html += this.row;
        this.html += "</tbody></table>";
    }
    public string GetHtmlTable()
    {
        return this.html;
    }
}

