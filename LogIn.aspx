<%@ Page Language="C#" AutoEventWireup="true" CodeFile="LogIn.aspx.cs" Inherits="LogIn" Debug="true" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link rel="Stylesheet" type="text/css" href="bootstrap\css\bootstrap.css" />
    <link rel="Stylesheet" type="text/css" href="bootstrap\css\bootstrap.min.css" />
    <title>moran project</title>
</head>
<body style = "background-color:#A9A9A9;">
<div class="container">
    <div class="row">
        <div class="col-sm-6 col-md-4 col-md-offset-4">
         <img class="profile-img" src="https://lh5.googleusercontent.com/-b0-k99FZlyE/AAAAAAAAAAI/AAAAAAAAAAA/eu7opA4byxI/photo.jpg?sz=120"
                    alt=""/>
            
            <div class="account-wall">
    <form id="form1" class="form-signin" action="LogIn.aspx" method="post">
    <h1 class="text-center login-title">כניסה למערכת למדידת ביצועים</h1>
    <br />
    <br />
    <div>
        <table>
            <tr>
                <td>שם משתמש: </td> <td> <input type="text" id="user" name="user" class="form-control" /> </td>
            </tr>
            <tr><td><br /></td></tr>
            <tr> 
                <td> סיסמה: </td> <td> <input type="password" id="password" name="password" class="form-control"/> </td>
            </tr>
        <tr><td><br /></td></tr>
        <tr><td></td><td align="center"><button class="btn btn-lg btn-primary btn-block" type="submit"> התחבר</button></td></tr>
        
        </table>
    </div>
    </form>
    
              </div>
            </div>
       </div>
    </div>
</body>
</html>
