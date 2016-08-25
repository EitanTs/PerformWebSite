<%@ Page Language="C#" AutoEventWireup="true" CodeFile="AdminReport.aspx.cs" Inherits="admin_AdminReport" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
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
            <form class="form-inline" action="AddPerform.aspx" method="post">
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
                            <td>יחידה/ מחוז:</td><td><input type ="text" name="unit" value="<% PrintUnit(); %>"/></td>
                            <td>קבוצה:</td><td><input type ="text" name="group" /></td>
                            <td>צוות:</td><td><input type ="text" name="team"/></td>
                            <td>סטטוס דיווח:</td><td></td>
                        </tr>
                        <tr>
                            <td>עובדים: </td><td><select></select></td>
                            <td>חובת דיווח: </td><td><input type="checkbox" name="MustReport" /></td>
                            <td>שם עובד: </td><td><input type="text" name="WorkerName" /></td>
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
