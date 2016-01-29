imports System
imports System.Collections
imports System.ComponentModel
imports System.Data
imports System.Drawing
imports System.Web
imports System.Web.SessionState
imports System.Web.UI
imports System.Web.UI.WebControls
imports System.Web.UI.HtmlControls
imports System.Data.SqlClient
imports System.xml
imports System.Configuration


namespace PageTemplate
	public class agentarea 
	   inherits PageTemplate
	  
 
	   Protected WithEvents agentarea As System.Web.UI.WebControls.DataGrid
  		Protected WithEvents lblstatus As System.Web.UI.WebControls.Label
  		public l_search As System.Web.UI.WebControls.textbox
  	  
	   private Sub Page_Load(byval sender as object, byval e As EventArgs) handles mybase.load
				if  not (Page.IsPostBack) then
					BindGrid()
		   	end if
		
			'the page template code below represents only a few of the things that
			'you can do. Play around with it, and you'll see just how much power is
			'in your hands

			'width will be calculated automatically, but it is sometimes
			'important to specify height
			''layout.width="1100"
			'Body.Height = "400"
			'Body.VAlign = "top"
			''body.width = "1100"
			'Body.VAlign = "top"
			'RightNav.VAlign = "top"
			Layout.border = 0
			Header.Controls.Add(new LiteralControl("Agent Geographic Areas Worked"))
				
			'LeftNav.Controls.Add(LoadControl("navigation2.ascx"))
			'LeftNav.VAlign = "top"
			'LeftNav.Controls.Add(new LiteralControl("Some text."))

			'adjust size of LeftNav (just for the heck of it)
			'LeftNav.Width = "100"
			
			'RightNav contents are included here, but try commenting
			'out the code below, to see how the page template dynamically
			'modifies itself (same goes with the LeftNav)
			'RightNav.Controls.Add(LoadControl("quicklink1.ascx"))
			'MiddleNav.Controls.Add(LoadControl("navigation.ascx"))
			'MiddleNav.Controls.Add(LoadControl("userid.ascx"))
			'footer.controls.add(LoadControl("footer.ascx"))
	  end sub
		   
 	Sub BindGrid()
			'Dim strpropID as String = Request.QueryString("id")
			Dim strUID as String = session("userid")
         Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
         dim mycommand as string
      
       	mycommand = "select fname + ' ' + lname as 'nameby', countyworked, citiesworked from dbo.tbl_users where company ='" & session("company") & "'" 
        
      	 Try
         	Dim dataAdapter As New SqlDataAdapter(myCommand, myConnection)
     			dim dataSet As New DataSet()
     			dataAdapter.Fill(dataSet, "tbl_leads")
     			Dim dvProducts As New DataView(dataSet.Tables("tbl_leads"))
      		agentarea.DataSource = dvProducts
      		agentarea.DataBind
    		
		 		Catch exc As System.Exception
      				Response.Write(exc.ToString())
    			Finally
      				myConnection.Dispose()
    			End Try	
           	
     End Sub
 
 
 
   end class
end namespace