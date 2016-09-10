<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminReport.aspx.cs" Inherits="admin_AdminReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">


        function GetSelectedValues() {
            var unitSelect = document.getElementById("unit");
            var unitIndex = unitSelect.selectedIndex;
            var unitValue = unitSelect.options[unitSelect.selectedIndex].value;
            window.location = "AdminReport.aspx?unitId=" + unitIndex + "&unitV=" + unitValue;
        }
        function SelectValues() {
            document.getElementById("unit").selectedIndex = QueryString["unitId"];
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
                            <td>יחידה/ מחוז:</td><td><select name="unit" id="unit" onchange="GetSelectedValues()"><% PrintUnit(); %></select></td>
                            <td>קבוצה:</td><td><select name="SubUnit" id="SubUnit"><% PrintSubUnit(); %></select></td>
                            <td>צוות:</td><td><select name="team"><% PrintTeam(); %></select></td>
                            <td>סטטוס דיווח:</td><td></td>
                        </tr>
                        <tr>
                            <td>עובדים: </td><td><select></select></td>
                            <td>חובת דיווח: </td><td><input type="checkbox" name="MustReport" /></td>
                            <td>שם עובד: </td><td><input type="text" name="WorkerName" /></td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr><td><br /></td><td></td><td></td><td></td><td></td><td></td><td></td><td></td></tr>
                        <tr><td>תכנון/ ביצוע קטן מ-</td><td></td><td><input type="submit" value="חפש" /></td><td></td><td></td><td></td><td></td><td></td></tr>
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
