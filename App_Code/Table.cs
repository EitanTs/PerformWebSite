using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using DALC4NET;
using System.Linq;

/// <summary>
/// Summary description for Table
/// </summary>
public class Table
{
    string head = @"<head runat='server'>
    <title></title>
  <link rel='stylesheet' href='http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css'>
  <script type='text/javascript' src='https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js'></script>
  <script type='text/javascript' src='http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js'></script>
  <style>
    .table td
    {
        text-align:center;
    }
    .table th
    {
        text-align:center;    
    }
  </style>
</head><table class-'table'>";
    string bottom = "</table>";


    public string DataTableToHtml(string[] cols, DataTable dt)
    {
        return DataTableToHtml(cols, dt, null);
    }


    public string DataTableToHtml(string[] cols, DataTable dt, Dictionary<string, string> specials)
    {
        var htmlTable = "";
        htmlTable = CreateBody(dt, htmlTable, cols, specials);
        htmlTable += bottom;
        return htmlTable;
    }

    string CreateBody(DataTable body, string htmlTable, string[] cols, Dictionary<string, string> specials)
    {
        var dict = GetColors();
        htmlTable += "<tbody>";
        var i = 0;

        foreach (DataRow row in body.Rows)
        {
            htmlTable += string.Format("<tr class='{0}'>", dict[i % 3]);
            for (int j = 0; j < cols.Length; j++)
            {
                if (specials != null)
                {
                    if (specials.Keys.Contains(cols[j]))
                    {
                        htmlTable += string.Format("<td>{0}</td>", string.Format(specials[cols[j]], row[cols[j]]));
                    }
                    else
                    {
                        htmlTable += string.Format("<td>{0}</td>", row[cols[j]]);
                    }
                }
                else
                {
                    htmlTable += string.Format("<td>{0}</td>", row[cols[j]]);
                }
            }
            htmlTable += "</tr>";
            i++;
        }
        htmlTable += "</tbody>";
        return htmlTable;
    }
    public Dictionary<int, string> GetColors()
    {
        var dict = new Dictionary<int, string>();
        dict.Add(0, "Success");
        dict.Add(1, "Danger");
        dict.Add(2, "Info");
        return dict;
    }
}