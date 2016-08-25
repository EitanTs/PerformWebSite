using System;
using System.Collections.Generic;
using System.Web;

/// <summary>
/// Summary description for Select
/// </summary>
public class Select
{
    public string GetSelectAsc(int min, int count, string format)
    {
        var html = "";
        for (int i = min; i < min + count; i++)
        {
            html += string.Format("<option value='{0}'>{0}</option>", string.Format(format, i));
        }
        return html;
    }
    public string GetSelectAsc(int min, int count, string format, int selected)
    {
        var html = "";
        for (int i = min; i < min + count; i++)
        {
            if (i == selected)
                html += string.Format("<option value='{0}' selected>{0}</option>", string.Format(format, i));
            else
                html += string.Format("<option value='{0}'>{0}</option>", string.Format(format, i));
        }
        return html;
    }
    public string GetSelectDesc(int max, int count, string format)
    {
        var html = "";
        for (int i = max; i > max - count; i--)
        {
            html += string.Format("<option value='{0}'>{0}</option>", string.Format(format, i));
        }
        return html;
    }
    public string GetSelectDesc(int max, int count, string format, int selected)
    {
        var html = "";
        for (int i = max; i > max - count; i--)
        {
            if(i == selected)
                html += string.Format("<option value='{0}' selected>{0}</option>", string.Format(format, i));
            else
                html += string.Format("<option value='{0}'>{0}</option>", string.Format(format, i));
        }
        return html;
    }
    public string GetYears()
    {
        var year = DateTime.Now.Year;
        return string.Format(@"<option value='{0}'>{0}</option>
                    <option value='{1}' selected=true>{1}</option>
                    <option value='{2}'>{2}</option>
                    <option value='{3}'>{3}</option>", year+1, year, year-1, year - 2);
    }
}