<%@ Page Language="C#" AutoEventWireup="true" CodeFile="UserHomePage.aspx.cs" Inherits="UserHomePage" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat='server'>
    <title></title>
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
    <script type="text/javascript">
        function OpenAlert(message) {
            alert(message);
        }
    </script>

  <link rel='stylesheet' href='http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/css/bootstrap.min.css'/>
  <script type='text/javascript' src='https://ajax.googleapis.com/ajax/libs/jquery/1.12.0/jquery.min.js'>
  </script>
  <script type='text/javascript' src='http://maxcdn.bootstrapcdn.com/bootstrap/3.3.6/js/bootstrap.min.js'></script>
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
</head>
    <body> <br />
    <br />

    <div class="container" >
        <div class="col-md-offset-0">
        <div class="col-md-18">
            <div class="panel panel-info">
            <div class="panel-heading" dir="rtl">
            דיווח ביצועים
        </div>
        <div class="panel-body" dir="rtl">
           <form class="form-inline" action="UserHomePage.aspx" method="post">
                <table class="table" dir="rtl">
                    <tbody>
                
                        <tr>
                            <td>שם עובד</td><td><input type ="text" name="WorkerName" id="WorkerName"  value="<% Response.Write(Session["FullName"].ToString()); %>"/></td>
                            <td>תפקיד</td><td><input type ="text" name="WorkerName" id="JobName"  value="<% Response.Write(Session["JobName"].ToString()); %>"/></td>
                            <td>סוג תפקיד</td><td><input type ="text" name="WorkerName" id="JobType"  
                                value="<% 
                                                      Response.Write(GetJobName());
                                                      
                                      %>"/>
                            </td>
                            </tr>

                            <tr>
                            <td>חודש</td><td>
                            
                                      <select name="month" id="month">
                                    <% 
                                        Select mySelect = new Select();
                                        Response.Write(mySelect.GetSelectAsc(1, 12, "{0}", DateTime.Now.Month));
                                    %>
                                </select>
                                         </td>
                            <td>שנה</td>
                            <td>
                                 <select name="year" id="year">
                                    <% Response.Write(mySelect.GetYears()); %>
                                 </select>
                           </td>
                           <td><input type="submit" value="חפש" style="width:100px;"/></td> <td></td>
                        </tr>
                    </tbody>
                    </table>
                &nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp&nbsp
                   
               </form>
               
               <% if(GetPrint()) PrintSecondTable(); %>
              
            <div class="container">
        <div class="col-md-offset-2">
        <div class="col-md-12">
            <div class="panel panel-info">
            <div class="panel-heading" dir="rtl">
            <div class='page-header'><h1> נתוני ביצוע לחודש הנבחר</h1></div>
                <table class="table" dir="rtl" style="color:black";>
                    <thead>
                        <tr>
                            <th> תאריך: </th>  <th><% try
                                                      {
                                                          Response.Write(DateTime.Parse(GetValuesDictionary()["G_Update_date"]).ToString("MM/dd/yyyy"));
                                                      }
                                                      catch
                                                      {
                                                          Response.Write(GetValuesDictionary()["G_Update_date"]);
                                                      }
                                                           %></th>
                             <th> סטטוס דיווח: </th>  <th><% Response.Write(GetValuesDictionary()["ReportStatusName"]); %></th>
                             <th> אחוז ביצוע שנתי: </th>  <th><% Response.Write(GetValuesDictionary()["PersentPlnYear"]); %></th>
                             <th> אחוז ביצוע לעומת תכנון: </th>  <th><% Response.Write(GetValuesDictionary()["PersentIncremental"]); %></th>  
                        </tr>
                    </thead>
                </table>
        </div>
                </div>
            </div>
            </div>
                </div>
         
        <% if(GetPrint()) PrintMainTable(); %>
          
        </div>
            <form id="form1" runat="server">
            <div>

            </div>
            </form>
         </div>
        </div>
        </div>
       
    </div> 
   
</body>
</html>
