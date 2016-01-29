imports System
imports System.Collections
imports System.Data.SqlClient
imports System.ComponentModel
imports System.Data
imports System.Drawing
imports System.Drawing.Color 
imports System.Web
imports System.Web.SessionState
imports System.Web.UI
imports System.Web.UI.WebControls
imports System.Web.UI.HtmlControls
imports System.xml
imports System.Configuration
imports system.Globalization
imports system.net.mail

namespace PageTemplate
	public class pdftest 	
	   inherits PageTemplate
	 
	private shared strLeadNo, strLeadName , strLeadHPhone as string    

	 
	   public Sub Page_Load(byval sender as object, byval e As EventArgs) handles mybase.load
	    	pagesetup()
	    	if  not (Page.IsPostBack) then
	    		'bindfields()
				fdf()
			end if
		end sub
	    	
 	public Sub bindfields()
 					Dim strUID as String = session("userid")
  					Dim strSql as String = "SELECT *,fname + ' ' + lname as assignedby from tbl_leads join dbo.tbl_users on Uid=ld_assignedbyuid  where tbl_leads_pk="& Request.QueryString("id")  
         		Dim sqlCmd As SqlCommand
	 		
			      Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
        			sqlCmd = New SqlCommand(strSql, myConnection)
              	
               Try
               	myConnection.Open()
                 	Dim Sqldr as SqlDataReader = sqlCmd.ExecuteReader
         				if Sqldr.read() then
         						strLeadNo =Sqldr("tbl_leads_pk")
         				   					
         					if Sqldr("ld_lname") IsNot dbnull.value then
            				 	   strLeadName = Sqldr("ld_fname") + " " + Sqldr("ld_lname")
            				end if
         					if Sqldr("ld_hphone") IsNot dbnull.value then
            				 	   strLeadHPhone = Sqldr("ld_hphone")
            				end if
            				
            			end if
      				Catch SQLexc As SqlException
                       Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
               	finally
               		myConnection.close()
               End Try
		   End Sub
	    	
	    	
	public sub fdf()
	   		
	   
'  DECLARE ALL THE VARIABLES
'***********************************************************************

Dim FDFAcX         ' FDF Toolkit ActiveX Version Object
Dim objFDF           ' FDF Object

 FDFAcX = Server.CreateObject("FDFApp.FDFApp")

 objFDF = FDFAcX.FDFCreate
'objFDF.FDFSetFile ("http://bmsdev.gochoiceone.com/FormTestFDF.pdf")
objFDF.FDFSetFile ("http://bmsdev.gochoiceone.com/tt.pdf")
objFDF.FDFSetValue("txtLeadNo", "Bialas", False)
'objFDF.FDFSetValue("txtLeadName", strLeadName, False)
'objFDF.FDFSetValue("txtLeadHPhone", strLeadHPhone, False)
'objFDF.FDFSetStatus ("You must complete all sections of this form.")
Response.ContentType = "application/vnd.fdf"
Response.BinaryWrite(objFDF.FDFSaveToBuf)
objFDF.FDFClose
 objFDF = Nothing
 FDFAcX = Nothing

end sub

	sub pagesetup()
	
			'width will be calculated automatically, but it is sometimes
			'important to specify height
			layout.width="1100"
			Body.Height = "400"
			Body.VAlign = "top"
			body.width = "1100"
			RightNav.VAlign = "top"
			Layout.border = 0
			Header.Controls.Add(LoadControl("headersys.ascx"))
			LeftNav.Controls.Add(LoadControl("navigation2.ascx"))
			LeftNav.VAlign = "top"
			'LeftNav.Controls.Add(new LiteralControl("Some text."))

			'adjust size of LeftNav (just for the heck of it)
			LeftNav.Width = "100"
			
			'LeftNav.Controls.Add(LoadControl("navigation.ascx"));
			'LeftNav.Controls.Add(new LiteralControl("Some text."));

			'adjust size of LeftNav (just for the heck of it)
			'LeftNav.Width = "100";

			'RightNav.Controls.Add(LoadControl("quicklink1.ascx"))
			'MiddleNav.Controls.Add(LoadControl("userid.ascx"))
			
			'response.write(session("qstring"))
		end sub



	end class
end namespace