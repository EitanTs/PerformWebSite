using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
/// <summary>
/// Summary description for FileHandler
/// </summary>
public class FileHandler
{
	public FileHandler()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    public static void WriteContent(string path, string content)
    {
        StreamWriter sw = new StreamWriter(path, false, System.Text.Encoding.UTF8);
        sw.Write(content);
        sw.Close();
    }
}