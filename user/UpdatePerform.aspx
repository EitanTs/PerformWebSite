<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UpdatePerform.aspx.cs" Inherits="user_UpdateProfile" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

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
    <div class="container">
        <div class="col-md-offset-0">
       
            <div class="panel panel-info">
            <div class="panel-heading" dir="rtl">
            עדכון ביצועים
        </div>
        <div class="panel-body" dir="rtl">
           <form class="form-inline" action="UpdatePerform.aspx" method="post">
                <table class="table" dir="rtl">
                    <tbody>
                
                        <tr>
                            <td>שם עובד:</td><td><input type ="text" name="WorkerName" id="WorkerName" value="<% Response.Write(Session["FullName"].ToString()); %>"/></td>
                            <td>סוג פעילות:</td><td><select name="MsrEventIndx" ><%MeasureToEvent(); %></select></td>
                            <td></td>   
                            <td></td>
                        </tr>
                        <tr>
                             <td>שנה:</td>
                            <td>
                                <select name="year">
                                    <% 
                                        Select mySelect = new Select();
                                        Response.Write(mySelect.GetYears());
                                    %>
                                </select>
                            </td>
                        
       
                            <td>חודש:</td>  
                            <td>  
                            <select name="month">
                                    <% PrintMonths();; %>
                            </select>
                            </td>
                            <td>יום:</td> 
                            <td>  
                            <select name="day">
                                    <% PrintDays(); %>
                            </select>
                            </td>
                            </tr>
                        <tr>
                            <td>תיאור פעילות:</td><td><input type ="text" name="description" value="<% PrintValue("Description"); %>"/></td>
                            <td>כמות:</td><td><input type ="text" name="quantity" value="<% PrintValue("Quntity"); %>" /></td>
                            <td>אחוז ביצוע:</td><td><input type ="text" name="percent" value="<% PrintValue("Persent"); %>"/></td>
                        </tr>
                        <tr><td><br /></td><td></td><td></td><td></td></tr>
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
 
</body>
</html>
