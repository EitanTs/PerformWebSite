using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Data;
using System.Data.SqlClient;

public class Excel
{
	
     static void PrintHeader(string header, StreamWriter sw)
    {
        sw.Write(header);
        sw.Write(",");
    }
    public static void ParseDtToXL(DataTable dt, string strFilePath, string[] cols)
    {
        try
        {
            // Create the CSV file to which grid data will be exported.
            StreamWriter sw = new StreamWriter(strFilePath, false, System.Text.Encoding.UTF8);
            // First we will write the headers.
            int iColCount = cols.Length;
            PrintHeader("הערה", sw);
            PrintHeader("אחוז ביצוע/תכנון", sw);
            PrintHeader("ביצוע מצטבר", sw);
            PrintHeader("תכנון מצטבר", sw);
            PrintHeader("אחוז ביצוע/תכנון", sw);
            PrintHeader("ביצוע חודשי", sw);
            PrintHeader("תכנון חודשי", sw);
            PrintHeader("משקולות", sw);
            PrintHeader("סוג מדד", sw);
            PrintHeader("תיאור", sw);
            sw.Write(sw.NewLine);
            // Now write all the rows.
            foreach (DataRow dr in dt.Rows)
            {
                for (int i = iColCount - 1; i >= 0; i--)
                {
                    sw.Write(dr[cols[i]].ToString());
                    sw.Write(",");
                }

                sw.Write(sw.NewLine);
            }
            sw.Close();
        }
        catch (Exception e)
        {
            Console.Write(e.ToString());
        }
    }
    public static void AdminParseDtToXL(DataTable dt, string strFilePath)
    {
        try
        {
            // Create the CSV file to which grid data will be exported.
            StreamWriter sw = new StreamWriter(strFilePath, false, System.Text.Encoding.UTF8);
            // First we will write the headers.

            PrintHeader("הערת עובד", sw);
            PrintHeader("אחוז ביצוע/תכנון", sw);
            PrintHeader("ימי היעדרות", sw);
            PrintHeader("סטטוס דיווח", sw);
            PrintHeader("תאריך סטטוס", sw);
            PrintHeader("תפקיד", sw);
            PrintHeader("שם עובד", sw);
            sw.Write(sw.NewLine); 
            foreach (DataRow dr in dt.Rows)
            {
                var remarkUser = dr["RemarkUser"].ToString();
                if (remarkUser.ToString().Length < 2)
                    remarkUser = "אין";
                string date = DateTime.Today.ToString("MM/dd/yyyy");
                sw.Write(remarkUser + ",");
                sw.Write(dr["PersentIncremental"].ToString() + ",");
                sw.Write(dr["DaysoffWork"].ToString() + ",");
                sw.Write(dr["ReportStatus"].ToString() + ",");
                sw.Write(date + ",");
                sw.Write(dr["JobName"].ToString() + ",");
                sw.Write(dr["FullName"].ToString() + ",");
                sw.Write(sw.NewLine);
            }
            sw.Close();
        }
        catch (Exception e)
        {
            Console.Write(e.ToString());
        }
    }
}