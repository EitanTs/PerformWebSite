public class Queries
{
    public static string LoginQuery = @"SELECT U.UserIndx ,U.FullName,U.UserID,
	       UJ.JobUserIndx,UJ.JobIndx,UJ.Hierarchy,UJ.Permission,UJ.JobType,
		   UJ.CompanyIndx,UJ.DepIndx,UJ.UnitIndx,UJ.SubUnitIndx,UJ.CodeTeam,UJ.StartJob,UJ.EndJob,
		   j.JobName,j.Hierarchy JobHierarchy,
		   UJFG.PrfGrpIndx,UJFM.UserJbGrpIndx
	From       [Perform-Project].dbo.MB_Users   U
	INNER JOIN [Perform-Project].dbo.MB_UserJob UJ
	      ON  U.UserIndx= UJ.UserIndx
   INNER JOIN [Perform-Project].dbo.MB_Jobs J
	      ON  UJ.JobIndx= J.JobIndx
   INNER JOIN [Perform-Project].dbo.MB_UserJobGroup UJG
	      ON  UJ.JobUserIndx= UJG.JobUserIndx
   INNER JOIN [Perform-Project].dbo.MB_UserJbPrfGrp UJFG
	      ON  UJG.UserJbGrpIndx= UJFG.UserJbGrpIndx
   INNER JOIN [Perform-Project].dbo.MB_UserJbPrfMsr UJFM
	      ON  UJFM.UserJbGrpIndx= UJFG.UserJbGrpIndx
	WHERE  UserID = @UserID
	AND Password = @Password
	And U.Status =1
	And UJ.Status =1
	And (UJ.EndJob >  GETDATE() OR UJ.EndJob IS NULL)
	And UJFG.UseMonth = MONTH(getdate())
	And UJG.UseYear = YEAR(getdate())";

    public static string HomePageMainTable = @"SELECT U.PrfMsrIndx ,U.UserJbGrpIndx, U.MeasureIndx,U.MeasureType,M.MeasureName ,U.Score,U.UseMonth,
	       U.PlanYear,U.PlanMonth,U.DoneMonth,U.PresentMonth,U.IncrementalDone,
           U.IncrementalPlan,U.PresentIncremental,U.Remark,U.Update_User,U.Update_date
	From       MB_UserJbPrfMsr U
	INNER JOIN MB_Measures     M
	       ON  U.MeasureIndx= M.MeasureIndx 
	WHERE U.UserJbGrpIndx in(Select UserJbGrpIndx 
	                         From MB_UserJobGroup JG
						     INNER JOIN MB_UserJob US
	                          ON  US.JobUserIndx= JG.JobUserIndx
						     Where JobIndx=@JobIndx
							 And   JobType =@JobType 
						     And   UserIndx=@UserIndx
	                         And   UseYear = @UseYear
						     And   US.Status =1
						     And   JG.Status =1) 
	And U.UseMonth =@UseMonth
	And U.status =1";

    public static string HomePageSecondTable= @"SELECT G.PrfGrpIndx,G.UserJbGrpIndx G_UserJbGrpIndx,G.UseMonth G_UseMonth, 
	       G.IncrementalPlan G_IncrementalPlan,G.IncrementalDone G_IncrementalDone,
           G.PlanYear G_PlanYear,G.PersentPlnYear,G.PersentIncremental,G.ReportStatus,G.RemarkManager,G.RemarkUser,
           G.DaysOffWork,G.DaysOffWorkYear,G.SickDays,G.SickDaysYear, G.Update_User,G.Update_date G_Update_date
	From       MB_UserJbPrfGrp G
	WHERE UserJbGrpIndx in(Select UserJbGrpIndx 
	                       From MB_UserJobGroup JG
						   INNER JOIN MB_UserJob US
	                          ON  US.JobUserIndx= JG.JobUserIndx
						   Where JobIndx=@JobIndx
						   And   JobType =@JobType 
						   And   UserIndx=@UserIndx
	                       And   UseYear = @UseYear
						   And   US.Status =1
						   And   JG.Status =1) 
	And UseMonth =@UseMonth
	And status =1";

    public static string GetJobType = @"SELECT * FROM [Perform-Project].[dbo].[MB_SupportDetails]
                                        WHERE SupportIndx = 2 and SupportDTLNum = @SupportDTLNum";
    public static string SpecifyDetails = @"SELECT M.EventName,U.UserEventIndx,U.MsrEventIndx,U.UserJbGrpIndx,U.DateEvent,U.Quntity,
	       U.Persent,U.Description,U.Update_User,U.Update_date
	From MB_UserEvent U
	INNER JOIN MB_MeasureEvent M
	      ON  U.MsrEventIndx= M.MsrEventIndx
	WHERE  U.status =1
	AND UserJbGrpIndx  = @UserJbGrpIndx
	AND M.MeasureIndx =@MeasureIndx";

    public static string InsertUserEvent = @"INSERT INTO [dbo].[MB_UserEvent]
           (
             MsrEventIndx  
        	,UserJbGrpIndx 
	        ,DateEvent      
	        ,Quntity       
	        ,Persent       
	        ,Description   
	        ,Update_User)
     VALUES
           (
             @MsrEventIndx  
        	,@UserJbGrpIndx 
	        ,@DateEvent      
	        ,@Quntity       
	        ,@Persent       
	        ,@Description   
	        ,@Update_User   )";

    public static string UpdateUserEvent1 = @"UPDATE [dbo].[MB_UserEvent]
   SET 	  MsrEventIndx   = @MsrEventIndx  
	     ,UserJbGrpIndx  = @UserJbGrpIndx 
	     ,DateEvent      = @DateEvent     
         ,Quntity        = @Quntity       
        , Persent        = @Persent       
         ,Description    = @Description      
        ,[Status]        = @Status
        ,[Update_date]   = GETDATE()
        ,[Update_User]   = @Update_User
 WHERE [UserEventIndx]   = @UserEventIndx";

    public static string UpdateUserEvent = @" UPDATE [dbo].[MB_UserEvent]
   SET Quntity = @Quantity
      ,persent = @Percent
      ,Description = @Description
      ,[Update_date] = GETDATE()
      ,DateEvent = @DateEvent
 WHERE [UserEventIndx]   = @UserEventIndx";

 

    public static string GetEventName = @"SELECT MsrEventIndx ,EventName
	From MB_MeasureEvent
	WHERE  Status =1
	AND MeasureIndx = @MeasureIndx";

    public static string GetEventNames = @"SELECT *
  FROM [Perform-Project].[dbo].[MB_MeasureEvent]
  WHERE Status = 1 and MeasureIndx = @MeasureIndx";

    public static string GetEventValues = @"SELECT *
    FROM [Perform-Project].[dbo].[MB_UserEvent] a
    join [Perform-Project].[dbo].[MB_MeasureEvent] b
    on a.MsrEventIndx = b.MsrEventIndx
    WHERE UserEventIndx = @UserEventIndx";

    public static string UpdateReport = @" Update MB_UserJbPrfMsr
 Set    DoneMonth          = @DoneMonth,
	    IncrementalDone    = @IncrementalDone,
		Remark             = @Remark, 
        PresentMonth       = @PresentMonth,
        PresentIncremental = @PresentIncremental, 
        Update_date        = GETDATE()
 Where  PrfMsrIndx         = @PrfMsrIndx";


    public static string UpdateSecondReport = @"
         Update MB_UserJbPrfGrp
 set    RemarkUser=@RemarkUser,
        ReportStatus=2,
		DaysOffWork=@DaysOffWork,
		SickDays=@DaysSick,
        SickDaysYear = @SickDaysYear,
        DaysOffWorkYear = @DaysOffWorkYear
 WHERE  UserJbGrpIndx = @UserJbGrpIndx
 AND    UseMonth=@UseMonth";

 public static string UpdateEndReport = @"
         Update MB_UserJbPrfGrp
 set    
        ReportStatus=@UpdateReportStatus
 WHERE  UserJbGrpIndx = @UserJbGrpIndx
 AND    UseMonth=@UseMonth";

    public static string GetStatusName = @"
select SupportDTLName
from MB_SupportDetails
where SupportIndx=9
and SupportDTLNum = @ReportStatus
and  Status=1";
    public static string DeleteEvent = @"DELETE FROM [MB_UserEvent] WHERE [UserEventIndx] = @UserEventIndx";
}