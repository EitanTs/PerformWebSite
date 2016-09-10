<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminReport.aspx.cs" Inherits="admin_AdminReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">

        
        function SetUrl() {
            var unit_index = GetSelectIndex("unit"); ;
            var unit_value = GetSelectValue("unit");
            var sub_index = GetSelectIndex("SubUnit");
            var sub_value = GetSelectValue("SubUnit");
            var team_index = GetSelectIndex("team");
            var team_value = GetSelectValue("team");
            url = "AdminReport.aspx?unitId=" + unit_index + "&unitV=" + unit_value + "&subId=" + sub_index + "&subV=" + sub_value
                    + "&teamId=" + team_index + "&teamV=" + team_value;
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
            //document.getElementById("search").disabled = true;
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
            <form class="form-inline" action="" method="post">
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
                            <td>צוות:</td><td><select name="team" id="team" onchange="SetUrl()"w><% PrintTeam(); %></select></td>
                            <td>סטטוס דיווח:</td><td><select name="ReportStatus"><% PrintReportStatus(); %></select></td>
                        </tr>
                        <tr>
                            <td>תפקיד: </td><td><select name="hirarchy"><% PrintHirarchy(); %></select></td>
                            <td>חובת דיווח: </td><td><input type="checkbox" name="MustReport" checked="checked" /></td>
                            <td>שם עובד: </td><td><input type="text" name="WorkerName" list="AllWorkers"/>
                                                    <datalist id="AllWorkers">
                                                        <% PrintWorkers(); %>
                                                    </datalist></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr><td><br /></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>
                        <tr><td>תכנון/ ביצוע קטן מ-</td><td><input type="text" name="plan" id="plan" onchange="IsNumber()"/></td>
                        <td><input type="submit" id="search" value="חפש" disabled='disabled' class='btn-disable'/>
                        </td><td></td><td></td><td></td><td></td><td></td></tr>
                        </tbody>
                    </table>
               <br />
               <br />
                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                   <input type="submit" value="שמור" style="width:100px;"/>        
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
