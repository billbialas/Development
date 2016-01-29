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
	public class sysusermaint 
	   inherits PageTemplate
	   
	   Public strstatusFilter As String = "type<>'X'" 
 
	   Protected WithEvents users As System.Web.UI.WebControls.DataGrid
  		Protected ddlstatusFilter As System.Web.UI.WebControls.DropDownList
  		Protected WithEvents lblstatus As System.Web.UI.WebControls.Label
  	   
	   private Sub Page_Load(byval sender as object, byval e As EventArgs) handles mybase.load
				if  not (Page.IsPostBack) then
				   FillstatusDropDown()
		   		bindgrid()
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
				
			'LeftNav.Controls.Add(LoadControl("navigation2.ascx"))
			'LeftNav.VAlign = "top"
			'LeftNav.Controls.Add(new LiteralControl("Some text."))

			'adjust size of LeftNav (just for the heck of it)
			LeftNav.Width = "100"
			
			'RightNav contents are included here, but try commenting
			'out the code below, to see how the page template dynamically
			'modifies itself (same goes with the LeftNav)
			'RightNav.Controls.Add(LoadControl("quicklink1.ascx"))
			'MiddleNav.Controls.Add(LoadControl("navigation.ascx"))
			MiddleNav.Controls.Add(LoadControl("sysadmin.ascx"))
			footer.controls.add(LoadControl("footer.ascx"))
	  end sub
	   
	   
	 Sub BindGrid()
			'Dim strpropID as String = Request.QueryString("id")
			'Dim strUID as String = session("userid")
             	' Create Instance of Connection and Command Object
   	         Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
               dim mycommand as string
           	 	mycommand = "Select * from dbo.tbl_users"
             	   	
               Try
      				Dim dataAdapter As New SqlDataAdapter(myCommand, myConnection)
      				Dim dataSet As New DataSet()
      				dataAdapter.Fill(dataSet, "tbl_users")
      				Dim dvProducts As New DataView(dataSet.Tables("tbl_users"))
      				dvProducts.RowFilter = strstatusFilter
      				users.DataSource = dvProducts
      				users.DataBind
    					Catch exc As System.Exception
      					Response.Write(exc.ToString())
    					Finally
      					myConnection.Dispose()
    					End Try	
               	
           	End Sub
                 
		 
	   Sub newuser(sender As Object, e As EventArgs)
     			response.redirect("newbpo.aspx")
		End Sub

	Sub FillstatusDropDown()
				Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
    			Dim myCommand As string = "Select tbl_agenttype_pk, type from dbo.tbl_agenttype" 
    			Dim objCmd As New SqlCommand(myCommand, myConnection)
    			Dim dataReader As SqlDataReader = Nothing
    				Try
      				myConnection.Open()
      				dataReader = objCmd.ExecuteReader()
      				ddlstatusFilter.DataSource = dataReader
      				ddlstatusFilter.DataTextField = "type"
      				ddlstatusFilter.DataValueField = "tbl_agenttype_pk"
      				ddlstatusFilter.DataBind()
    					Catch exc As System.Exception
      					Response.Write(exc.ToString())
    					Finally
      					myConnection.Dispose()
    					End Try
               
	  End Sub

	Sub ChangeFilter(Source As System.Object, e As System.EventArgs)
   	FilterByCategory(ddlstatusFilter.SelectedItem.text.ToString())
   	BindGrid()
  	End Sub

	Sub FilterByCategory(strCategory As String)
    	if strCategory ="All" then 
    		strstatusFilter = "type<>'X'"
    	else
    	   strstatusFilter = "type='" & strCategory & "'"
  		end if
  		'response.write (strstatusFilter)
  	End Sub

		
        end class
end namespace