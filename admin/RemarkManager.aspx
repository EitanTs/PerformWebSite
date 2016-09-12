<%@ Page Language="C#" AutoEventWireup="true" CodeFile="RemarkManager.aspx.cs" Inherits="admin_RemarkManager" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script type="text/javascript">
        function Approval() {
            if (confirm('Format the hard disk?'))
                window.location = "RemarkManager.aspx?del=1";

        }
        function ValidateRemark() {
            if (document.getElementById("remark").value.length > 0) {
                document.getElementById("save").disabled = false;
                document.getElementById("save").className = "submit";
            }
            else {
                document.getElementById("save").disabled = true;
                document.getElementById("save").className = "btn-disable";
            } 
        }
    </script>
     <script type="text/javascript" src="https://ajax.googleapis.com/ajax/libs/jquery/1.12.2/jquery.min.js"></script>
    <link rel='stylesheet' href='http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css'/>
  <script type='text/javascript' src='https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js'>
  </script>
  <script type='text/javascript' src='http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js'></script>
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
</head>
<body style="background-color:#e0e0d1" onmousemove="ValidateRemark()" >
<br />
    <form id="form1" runat="server">
    <div>
    
    </div>
    </form>
    
    <table class="table" dir="rtl">
        <tr >
            <td><form action="RemarkManager.aspx?id=1" method="post">הערה:</td> <td> <input type="text" id="remark" size="50" onchange="ValidateRemark()" id="remark"/></td><td></td><td></td>
        </tr> 
        <tr><td></td><td></td><td></td> <td><br /></td></tr>
        <tr style="background-color:#e0e0d1">
            <td> שלח מייל</td> <td> <input type="checkbox" name="SendMail" id="SendMail"  /></td> 
            <td><input type="submit" value="שמור" id="save" name="save" disabled="disabled" class="btn-disable"  /></form></td>
            
            <td><button name="DeleteAllMessages" onclick="Approval()">מחק הערות קודמות</button></td>
        </tr>
    </table>
    
</body>
</html>
