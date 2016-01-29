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
	public class AddListing 	
	   inherits PageTemplate
	   
	   public ComboBox1
	   
	    Protected WithEvents c1_listings As System.Web.UI.WebControls.DataGrid
	   
	    private Sub Page_Load(byval sender as object, byval e As EventArgs) handles mybase.load
		   
	    	if  not (Page.IsPostBack) then
	    	 	bindcontact()
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
	   
	   
  Sub bindcontact()
		  
			Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
         Dim myCommand As New SqlCommand("Select tbl_bpocompanys_pk, company from dbo.tbl_bpocompanys", myConnection)
               	
               Try
               	myConnection.Open()
                  ComboBox1.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
                  ComboBox1.DataBind()
                  Catch SQLexc As SqlException
                       Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
               End Try
     	end sub
    
	   
	end class
end namespace