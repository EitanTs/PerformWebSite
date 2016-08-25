<%@ Page Language="C#" AutoEventWireup="true" CodeFile="SpecifyPerforms.aspx.cs" Inherits="user_SpecifyPerforms" %>

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
    <div class="container">
        <div class="col-md-offset-1">
        <div class="col-md-12">
            <div class="panel panel-info">
            <div class="panel-heading" dir="rtl">
            פירוט ביצועים
        </div>
        <div class="panel-body" dir="rtl">
            <% PrintTable(); %>
        </div>
            <form id="form1" runat="server" action="AddPerform.aspx">
            <div>
                <button type="submit" class="btn btn-default">הוסף רשומה</button>
            </div>
            </form>
         </div>
        </div>
        </div>
    </div>
</body>
</html>

