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
	public class bbtest 
	   inherits PageTemplate
	   
		public lstAllcities as listbox
	   	
	   private Sub Page_Load(byval sender as object, byval e As EventArgs) handles mybase.load
				Pagesetup()
				
				if  not (Page.IsPostBack) then
					
					dobindings() 
		   	end if
		
	  end sub
	
	sub dobindings()
		bindallcities()
		
	end sub	   
 
  sub bindAllcities()
  		Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
      Dim myCommand As New SqlCommand("Select cty_idx_cid, cty_city_name from dbo.tbl_idx_cities where cty_county='Macomb'" , myConnection)
      'Dim myCommand As New SqlCommand("Select cty_idx_cid, cty_city_name from dbo.tbl_idx_cities" , myConnection)
           	
        Try
         	myConnection.Open()
            lstAllcities.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            lstAllcities.DataBind()
            Catch SQLexc As SqlException
                 Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
         End Try
  end sub
	
	
	sub pagesetup()
	
			'the page template code below represents only a few of the things that
			'you can do. Play around with it, and you'll see just how much power is
			'in your hands

			'width will be calculated automatically, but it is sometimes
			'important to specify height
			layout.width="1000"
			Body.Height = "400"
			Body.VAlign = "top"
			body.width = "1000"
			Body.VAlign = "top"
			'RightNav.VAlign = "top"
			
			Header.Controls.Add(LoadControl("headersys.ascx"))
	
			'''LeftNav.Controls.Add(LoadControl("navigation2.ascx"))
			''''LeftNav.VAlign = "top"
			'LeftNav.Controls.Add(new LiteralControl("Some text."))
			Layout.border = 0
			'adjust size of LeftNav (just for the heck of it)
			''''''	LeftNav.Width = "100"
			
			'RightNav contents are included here, but try commenting
			'out the code below, to see how the page template dynamically
			'modifies itself (same goes with the LeftNav)
			'RightNav.Controls.Add(LoadControl("quicklink1.ascx"))
			'MiddleNav.Controls.Add(LoadControl("navigation.ascx"))
			'MiddleNav.Controls.Add(LoadControl("userid.ascx"))
			'footer.controls.add(LoadControl("footer.ascx"))
	
	end sub
	
	
   end class
end namespace