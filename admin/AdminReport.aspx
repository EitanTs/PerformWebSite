<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminReport.aspx.cs" Inherits="admin_AdminReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">

        function GetUrl() {
            url = "AdminReport.aspx?unitId=" + unit_index + "&unitV=" + unit_value + "&subId=" + sub_index + "&subV=" + sub_value
                    + "&teamId=" + team_index + "&teamV=" + team_value + "&search=" + "1"
            return url;
            }
        function SetUrl() {
            var unit_index = GetSelectIndex("unit"); ;
            var unit_value = GetSelectValue("unit");
            var sub_index = GetSelectIndex("SubUnit");
            var sub_value = GetSelectValue("SubUnit");
            var team_index = GetSelectIndex("team");
            var team_value = GetSelectValue("team");
            url = "AdminReport.aspx?unitId=" + unit_index + "&unitV=" + unit_value + "&subId=" + sub_index + "&subV=" + sub_value
                    + "&teamId=" + team_index + "&teamV=" + team_value + "&search=" + QueryString["search"];
            window.location = url;
        }
        function GetSelectValue(id) {
            try {
                select = document.getElementById(id);
                value = select.options[select.selectedIndex].value; 
            }
            catch (err) {
                value = "0";
            }
            return value;
        }
        function GetSelectIndex(id) {
            select = document.getElementById(id);
            if (select.selectedIndex == "-1")
                return "0";
            return select.selectedIndex;
        }
        function SelectValues() {
            document.getElementById("unit").selectedIndex = QueryString["unitId"];
            document.getElementById("SubUnit").selectedIndex = QueryString["subId"];
            document.getElementById("team").selectedIndex = QueryString["teamId"];
            unitValue = QueryString["unitV"];
            subUnitValue = QueryString["subV"];
            teamValue = QueryString["teamV"];
            flag = false;
            if (GetSelectValue("unit") != QueryString["unitV"]) {
                unitValue = GetSelectValue("unit");
                flag = true;
            }
            if (GetSelectValue("SubUnit") != QueryString["subV"]) {
                subUnitValue = GetSelectValue("SubUnit");
                flag = true;
            }
            if (GetSelectValue("team") != QueryString["teamV"]) {
                teamValue = GetSelectValue("team");
                flag = true;
            }

            url = "AdminReport.aspx?unitId=" + QueryString["unitId"] + "&unitV=" + unitValue + "&subId=" + QueryString["subId"] + "&subV=" + subUnitValue
                    + "&teamId=" + QueryString["teamId"] + "&teamV=" + teamValue;
            if (QueryString["search"] != null)
                url += "&search=" + QueryString["search"];
            if(flag)
                window.location = url;
        }
        function IsNumber() {
            if (document.getElementById("plan").value > -1) {
                document.getElementById("search").disabled = false;
                document.getElementById("search").className = "button";
            }
            else {
                alert("Please enter a number");
                document.getElementById("search").className = "btn-disable";
            }
        }
        var QueryString = function () {
            // This function is anonymous, is executed immediately and 
            // the return value is assigned to QueryString!
            var query_string = {};
            var query = window.location.search.substring(1);
            var vars = query.split("&");
            for (var i = 0; i < vars.length; i++) {
                var pair = vars[i].split("=");
                // If first entry with this name
                if (typeof query_string[pair[0]] === "undefined") {
                    query_string[pair[0]] = decodeURIComponent(pair[1]);
                    // If second entry with this name
                } else if (typeof query_string[pair[0]] === "string") {
                    var arr = [query_string[pair[0]], decodeURIComponent(pair[1])];
                    query_string[pair[0]] = arr;
                    // If third or later entry with this name
                } else {
                    query_string[pair[0]].push(decodeURIComponent(pair[1]));
                }
            }
            return query_string;
        } ();
        function ManipulateButtons() {
        i = 1;
            while (document.getElementById("choose" + i) != null) {
            if (document.getElementById("choose" + i).checked == true) {
                document.getElementById("approve").disabled = false;
                document.getElementById("reject").disabled = false;
                document.getElementById("reply").disabled = false;
                document.getElementById("AddComment").disabled = false;
                document.getElementById("DownloadExcel").disabled = false;

                document.getElementById("approve").className = "button"
                document.getElementById("reject").className = "button"
                document.getElementById("reply").className = "button"
                document.getElementById("AddComment").className = "button"
                document.getElementById("DownloadExcel").className = "button"
                return;
                
              }
              i++;
            }
            
                document.getElementById("approve").disabled = true;
                document.getElementById("reject").disabled = true;
                document.getElementById("reply").disabled = true;
                document.getElementById("AddComment").disabled = true;
                document.getElementById("DownloadExcel").disabled = true;
                
                document.getElementById("approve").className = "btn-disable"
                document.getElementById("reject").className = "btn-disable"
                document.getElementById("reply").className = "btn-disable"
                document.getElementById("AddComment").className = "btn-disable"
                document.getElementById("DownloadExcel").className = "btn-disable"
            }
            function OpenWindow(href) {
                window.open(href, "", "height=300,width=800,left=300,top=300");
            }
    </script>
        <style type="text/css">
    .btn-disable
        {
        cursor: default;
        pointer-events: none;

        /*Button disabled - CSS color class*/
        color: #c0c0c0;
        background-color: #ffffff;

        }
   
</style>
 <style type ="text/css">
    .table td
    {
        text-align:center;
    }
    .table th
    {
        text-align:center;    
    }
  </style>
    <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.2/jquery.min.js"></script>
    <link rel='stylesheet' href='http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css'/>
  <script type='text/javascript' src='https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js'>
  </script>
  <script type='text/javascript' src='http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js'></script>
  
</head>
<body onload="SelectValues()">
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
    <br />
      <div class="container">
        <div class="col-md-offset-0">
       
            <div class="panel panel-info">
            <div class="panel-heading" dir="rtl">
            מסך דיווח מנהל
        </div>
        <div class="panel-body" dir="rtl">
            <form class="form-inline" action="AdminReport.aspx?unitId=0&unitV=1&subId=0&subV=1&teamId=0&teamV=1&search=1" method="post">
                <table class="table" dir="rtl">
                    <tbody>
                        <tr>
                            <td>חודש:</td>
                             <td>  
                            <select name="month">
                                    <% 
                                        Select mySelect = new Select();
                                        Response.Write(mySelect.GetSelectAsc(1, 12, "{0}", DateTime.Now.Month));
                                    %>
                            </select>
                            </td>
                            <td>שנה:</td>
                            <td>
                                 <select name="year">
                                    <% 
                                        Response.Write(mySelect.GetYears());
                                    %>
                                </select>
                            </td> 
                             <td>אגף:</td>
                             <td><% PrintDepName(); %></td>
                             <td>סוג יחידה:</td>
                             <td><select name="unitType">
                                 <% PrintUnitType(); %>
                                 </select>
                                 </td>   
                            </tr>
                        <tr>
                            <td>יחידה/ מחוז:</td><td><select name="unit" id="unit" onchange="SetUrl()"><% PrintUnit(); %></select></td>
                            <td>קבוצה:</td><td><select name="SubUnit" id="SubUnit" onchange="SetUrl()"><% PrintSubUnit(); %></select></td>
                            <td>צוות:</td><td><select name="team" id="team" onchange="SetUrl()"><% PrintTeam(); %></select></td>
                            <td>סטטוס דיווח:</td><td><select name="ReportStatus"><% PrintReportStatus(); %></select></td>
                        </tr>
                        <tr>
                            <td>תפקיד: </td><td><select name="hirarchy"><% PrintHirarchy(); %></select></td>
                            <td>חובת דיווח: </td><td><input type="checkbox" name="MustReport" id="MustReport" checked="checked" /></td>
                            <td>שם עובד: </td><td><input type="text" name="WorkerName" list="AllWorkers"/>
                                                    <datalist id="AllWorkers">
                                                        <% PrintWorkers(); %>
                                                    </datalist></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr><td><br /></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>
                        <tr><td>תכנון/ ביצוע קטן מ-</td><td><input type="text" name="plan" id="plan" value="100" onchange="IsNumber()"/></td>
                        <td><input type="submit" id="search" value="חפש" />
                        </td><td></td><td></td><td></td><td></td><td></td></tr>
                        </tbody>
                    </table>
               <br />
               <br />
                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp       
            </form>
            <br /><br /><br /><br />
            
          
                
              <form class="form-inline" action="AdminReport.aspx?unitId=0&unitV=1&subId=0&subV=1&teamId=0&teamV=1&search=1" method="post">
                <% 
                    CreateHtmlHeader();
                    if (Request.Form["month"] != null)
                    {
                        CreateHtmlTable(GetMainTableValue(GetQuery()));
                    }
                    PrintHtmlTable();
                %>
                <br /><br /><br /><br /><br />
                <input type="submit" id="approve" name="approve" value="אשר דיווח" disabled="disabled" class="btn-disable" />
                <input type="submit" id="reject" name="reject" value="דחה דיווח" disabled="disabled" class="btn-disable"/>
                <input type="submit" id="reply" name="reply" value="החזר דיווח" disabled="disabled" class="btn-disable"/>
                <input type="submit" id="AddComment" name="AddComment" value="הוסף/עדכן הערה" disabled="disabled" class="btn-disable"/>
                <button disabled="disabled" id="DownloadExcel" class="btn-disable"><a href="../excel/AdminTable.csv" download="AdminTable.csv" >הפק דוח אקסל</a></button>
              </form>
            </div>
                </div>
            </div>
          </div>

    <form id="form1" runat="server">
    <div>

    </div>
    </form>
</body>
</html>
