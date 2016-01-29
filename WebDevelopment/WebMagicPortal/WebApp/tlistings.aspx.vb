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
	public class tlistings 	
	   inherits PageTemplate
	   
	    Protected WithEvents c1_listings As System.Web.UI.WebControls.DataGrid
	   
	    private Sub Page_Load(byval sender as object, byval e As EventArgs) handles mybase.load
		   
	    	if  not (Page.IsPostBack) then
	    		'If Len(Request ("search")) > 0 Or Len(Request ("sub")) > 0 Then 
				'If Len(Request ("search")) > 0 Then Session.Clear() 
				'c1_listings.CurrentPageIndex = 0 'resets the Datagrid to page 1 
				'BindGrid() 
				'subsrch.visible = "true" 
				'Else 
				'subsrch.visible = "false" 
				'End if 
	   	end if
	   	
			'width will be calculated automatically, but it is sometimes
			'important to specify height
			layout.width="1000"
			Body.Height = "400"
			Body.VAlign = "top"
			body.width = "1000"
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
	   end sub
	   
	   
	   sub addlisting (sender As Object, e As EventArgs)
	   	response.redirect("addlisting.aspx")
	   end sub

		Sub BindGrid()
			' Create Instance of Connection and Command Object
   	   Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
         dim mycommand as string
         'mycommand = "Select * from dbo.tbl_Search_MLS_Actives where office_name = 'Choice One Realty LLC'" 
         mycommand = "Select cn_fname + ' ' + cn_lname as 'Contact',* from dbo.tbl_initprop join tbl_contact on tbl_contact_pk = P_contact_FK" 
         
         	Try
      			Dim dataAdapter As New SqlDataAdapter(myCommand, myConnection)
      			Dim dataSet As New DataSet()
      			dataAdapter.Fill(dataSet, "tbl_Search_MLS_Actives")
      			Dim dvlistings As New DataView(dataSet.Tables("tbl_Search_MLS_Actives"))
      			'dvlistings.RowFilter = strstatusFilter
      			c1_listings.DataSource = dvlistings
      			c1_listings.DataBind
    				
    				Catch exc As System.Exception
      				Response.Write(exc.ToString())
    				Finally
      				myConnection.Dispose()
    				End Try	
               
       End Sub
       
       Sub MyDataGrid_Page(sender As Object, e As DataGridPageChangedEventArgs) 
			c1_listings.CurrentPageIndex = e.NewPageIndex 
			BindGrid() 
		End Sub 

	end class
end namespace