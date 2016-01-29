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

namespace PageTemplate
	public class forms1a 
	   inherits PageTemplate
	   
	   public Welcomemessage as label
	   
	   
	   private Sub Page_Load(byval sender as object, byval e As EventArgs) handles mybase.load
				
	    	if  not (Page.IsPostBack) then
				BindWelcome()
			end if
		
		
			'the page template code below represents only a few of the things that
			'you can do. Play around with it, and you'll see just how much power is
			'in your hands

			'width will be calculated automatically, but it is sometimes
			'important to specify height
			Body.Height = "400"
			Body.VAlign = "top"
			'RightNav.VAlign = "top"
			Layout.border = 0
			Header.Controls.Add(LoadControl("headersys.ascx"))
				
			LeftNav.Controls.Add(LoadControl("navigation2.ascx"))
			LeftNav.VAlign = "top"
			'LeftNav.Controls.Add(new LiteralControl("Some text."))

			'adjust size of LeftNav (just for the heck of it)
			LeftNav.Width = "100"
			

			'RightNav contents are included here, but try commenting
			'out the code below, to see how the page template dynamically
			'modifies itself (same goes with the LeftNav)
			'RightNav.Controls.Add(LoadControl("quicklink1.ascx"))
			'MiddleNav.Controls.Add(LoadControl("navigation.ascx"))
			footer.controls.add(LoadControl("footer.ascx"))
			
			
			
	   end sub
	   
	    Sub BindWelcome()
	    		Dim strUID as String = session("userid")
  				Dim strSql as String = "SELECT * from tbl_users where UID='"& strUID &"'"
         	Dim sqlCmd As SqlCommand
	 		
	 		   Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
        		sqlCmd = New SqlCommand(strSql, myConnection)
              	
               Try
               	myConnection.Open()
                 	Dim Sqldr as SqlDataReader = sqlCmd.ExecuteReader
         				if Sqldr.read() then
         					if Sqldr("lname") IsNot DBNull.Value  then
         						Welcomemessage.text= Sqldr("fname") & " " & sqldr("lname")
         					end if    				 	
            			end if
      				Catch SQLexc As SqlException
                       Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
               	finally
               		myConnection.close()
               End Try
		   End Sub
		   
     end class
end namespace