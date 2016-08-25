using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// Summary description for AdminQueries
/// </summary>
public class AdminQueries
{
    
    public static string GetDep = @"SELECT * FROM [Perform-Project].[dbo].[MB_Departments]
                                    WHERE DepIndx = @DepIndx";

    public static string GetUnitType = @"SELECT SupportDTLIndx,SupportDTLName
	From [Perform-Project].[dbo].[MB_SupportDetails]
	WHERE SupportIndx  = 3";

    public static string GetUnit = @"SELECT * FROM [Perform-Project].[dbo].[MB_Units]
                                    WHERE  Status = 1
                                    AND DepIndx =  1
                                    AND UnitIndx = 1";

    public static string GetSubUnit = @"SELECT SubUnitIndx,SubUnitName
	                                    From MB_SubUnits
	                                    WHERE  Status =1
	                                    AND UnitIndx =  @UnitIndx
	                                    AND SubUnitIndx = ISNULL(@SubUnitIndx,SubUnitIndx)";



    public static string GetMainTable = @"SELECT U.FullName,J.JobName,G.PrfGrpIndx,G.UseMonth G_UseYear,G.PersentPlnYear,G.PersentIncremental,
	G.ReportStatus,G.RemarkManager,G.RemarkUser,G.DaysOffWork,G.SickDays
	From       [Perform-Project].[dbo].[MB_UserJbPrfGrp] G
	 INNER JOIN [Perform-Project].[dbo].MB_UserJobGroup JG
	       ON   G.UserJbGrpIndx= JG.UserJbGrpIndx
	 INNER JOIN [Perform-Project].[dbo].MB_UserJob US
	       ON   US.JobUserIndx= JG.JobUserIndx
     INNER JOIN [Perform-Project].[dbo].MB_Users U
	       ON   US.UserIndx= U.UserIndx
     INNER JOIN [Perform-Project].[dbo].MB_Jobs J
	       ON   US.JobUserIndx= J.JobIndx
	WHERE US.DepIndx     = @DepIndx      
	And	  US.UnitIndx    = ISNULL(@UnitIndx,UnitIndx )    
	And	  US.SubUnitIndx = ISNULL(@SubUnitIndx, SubUnitIndx) 
	And	  US.CodeTeam    = ISNULL(@CodeTeam ,CodeTeam  )  
	And   US.JobIndx     = ISNULL(@JobIndx,US.JobIndx)
	And   US.UserIndx    = ISNULL(@UserIndx,US.UserIndx)
	And   G.ReportStatus = ISNULL(@ReportStatus,ReportStatus)
	And   J.Hierarchy   < @Hierarchy
	And   J.Hierarchy    = ISNULL(@JobHierarchy,J.Hierarchy)
	And   JG.UseYear = @UseYear
	And   UseMonth =@UseMonth
	And   US.Status =1
    And   JG.Status =1
	And    G.status =1
	And    j.Status =1
	And    U.Status =1";
}