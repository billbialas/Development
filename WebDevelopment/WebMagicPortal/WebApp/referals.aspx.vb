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
	public class referals 
	   inherits PageTemplate
	   
	   Public strstatusFilter As String = "ld_status<>'A'" 
	   Public strleadFilter As String = "ld_type<>'A'" 
 
	   Protected WithEvents lead_status As System.Web.UI.WebControls.DataGrid
  		Protected ddlstatusFilter As System.Web.UI.WebControls.DropDownList
  		Protected ddlleadtypeFilter As System.Web.UI.WebControls.DropDownList
  		Protected WithEvents lblstatus As System.Web.UI.WebControls.Label
  		public l_search As System.Web.UI.WebControls.textbox
  		public btnreferals
  		public searchtype as string
  		
  
	   private Sub Page_Load(byval sender as object, byval e As EventArgs) handles mybase.load
				if  not (Page.IsPostBack) then
					if session("userid") = "sbialas" then 
						btnreferals.visible = true
					else 
						btnreferals.visible = false
					end if
					searchtype=""
					FillstatusDropDown()
					FillLeadTypeDropDown()
           		bindgrid()
		   	end if
			pagesetup()
	  end sub
	    Public Sub pagesetup()

         'width will be calculated automatically, but it is sometimes
           	layout.Width = System.Configuration.ConfigurationManager.AppSettings("ScreenWidth")
            leftNav.Width = System.Configuration.ConfigurationManager.AppSettings("ScreenLeftNav")            
            body.Height = System.Configuration.ConfigurationManager.AppSettings("ScreenBodyHeight")
          	body.Width = System.Configuration.ConfigurationManager.AppSettings("ScreenBodyWidth")
            layout.Border = System.Configuration.ConfigurationManager.AppSettings("ScreenBorder")
            footer.controls.add(LoadControl(System.Configuration.ConfigurationManager.AppSettings("ScreenFooter")))
            Header.Controls.Add(LoadControl(System.Configuration.ConfigurationManager.AppSettings("ScreenHeader")))
            leftNav.Controls.Add(LoadControl(System.Configuration.ConfigurationManager.AppSettings("ScreenLeftNav")))
                	MiddleNav.Controls.Add(LoadControl(System.Configuration.ConfigurationManager.AppSettings("ScreenmiddleNav")))
      
            body.VAlign = "top"            
            leftNav.VAlign = "top"
            
            'LeftNav.Controls.Add(new LiteralControl("Some text."))
 				'rightNav.VAlign = "top"           
            'adjust size of LeftNav (just for the heck of it)           

            'LeftNav.Controls.Add(LoadControl("navigation.ascx"));
            'LeftNav.Controls.Add(new LiteralControl("Some text."));

            'adjust size of LeftNav (just for the heck of it)
            'LeftNav.Width = "100";

            'RightNav.Controls.Add(LoadControl("quicklink1.ascx"))
            'MiddleNav.Controls.Add(LoadControl("userid.ascx"))
            

        End Sub
	   Sub MyDataGrid_Page(sender As Object, e As DataGridPageChangedEventArgs) 
			lead_status.CurrentPageIndex = e.NewPageIndex 
			BindGrid() 
		End Sub 

	Sub refresh (Source As System.Object, e As System.EventArgs)
   	 response.redirect("leads.aspx")
  	End Sub

	Sub referals (Source As System.Object, e As System.EventArgs)
		 response.redirect("referals.aspx")
  	End Sub
	   
		 Sub BindGrid()
			'Dim strpropID as String = Request.QueryString("id")
			Dim strUID as String = session("userid")
         Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
         dim mycommand as string
         if session("userid") = "sbialas" then 
         	mycommand = "Select refer_lead_fk,tbl_leads_pk, refer_company,* from dbo.tbl_referals join dbo.tbl_leads on tbl_leads_pk = refer_lead_fk "
         else
        		mycommand = "Select *, convert(varchar(20),ld_adddate,101) as ld_adddatef,fname + ' ' + lname as assignedby from dbo.tbl_leads join dbo.tbl_users on Uid=ld_assignedbyuid Where ((ld_assignedtouid='"& strUID &"' or ld_assignedbyuid='"& strUID &"' or ld_agent='Any') and (ld_status='Unassigned' or ld_status='Assigned' or ld_status='Closed')) or ((ld_assignedbyuid='"& strUID &"') and ld_status='Draft') order by tbl_leads_pk desc"
         end if
               Try
      				Dim dataAdapter As New SqlDataAdapter(myCommand, myConnection)
      				Dim dataSet As New DataSet()
      				dataAdapter.Fill(dataSet, "tbl_referals")
      				Dim dvProducts As New DataView(dataSet.Tables("tbl_referals"))
      				'dvProducts.RowFilter = strstatusFilter & " and " & strleadFilter
      				'dvProducts.RowFilter = "ld_status='Closed' and ld_type='Tenant'"
      				'if searchtype="Search" then 
      				'	dvProducts.RowFilter = "ld_lname LIKE '%" & l_search.text & "%' or ld_fname LIKE '%" & l_search.text & "%' or ld_email LIKE '%" & l_search.text & "%' or ld_hphone LIKE '%" & l_search.text & "%' or ld_cphone LIKE '%" & l_search.text & "%'"
      				'else if searchtype="Filter" then
      				'	dvProducts.RowFilter = strstatusFilter & " and " & strleadFilter
      				'end if
      				
      				lead_status.DataSource = dvProducts
      				lead_status.DataBind
    					Catch exc As System.Exception
      					Response.Write(exc.ToString())
    					Finally
      					myConnection.Dispose()
    					End Try	
           	End Sub
                 
		
	   Sub newlead(sender As Object, e As EventArgs)
     			response.redirect("addlead.aspx?action=new")
     			'response.redirect("test.aspx")
		End Sub

		Sub FillstatusDropDown()
				Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
    			Dim myCommand As string = "Select tbl_ld_status_pk, status from dbo.tbl_ld_status" 
    			Dim objCmd As New SqlCommand(myCommand, myConnection)
    			Dim dataReader As SqlDataReader = Nothing
    				Try
      				myConnection.Open()
      				dataReader = objCmd.ExecuteReader()
      				ddlstatusFilter.DataSource = dataReader
      				ddlstatusFilter.DataTextField = "status"
      				ddlstatusFilter.DataValueField = "tbl_ld_status_pk"
      				ddlstatusFilter.DataBind()
    					Catch exc As System.Exception
      					Response.Write(exc.ToString())
    					Finally
      					myConnection.Dispose()
    					End Try

	  	End Sub

		Sub FillLeadTypeDropDown()
				Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
    			Dim myCommand As string = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='leadtype'" 
    			Dim objCmd As New SqlCommand(myCommand, myConnection)
    			Dim dataReader As SqlDataReader = Nothing
    				Try
      				myConnection.Open()
      				dataReader = objCmd.ExecuteReader()
      				ddlleadtypeFilter.DataSource = dataReader
      				ddlleadtypeFilter.DataTextField = "x_descr"
      				ddlleadtypeFilter.DataValueField = "tbl_xwalk_pk"
      				ddlleadtypeFilter.DataBind()
    					Catch exc As System.Exception
      					Response.Write(exc.ToString())
    					Finally
      					myConnection.Dispose()
    					End Try

	  	End Sub
		
		Sub ChangeFilter(Source As System.Object, e As System.EventArgs)
   		searchtype="Filter"
   		FilterByCategory(ddlstatusFilter.SelectedItem.text.ToString())
   		FilterByLeadType(ddlleadtypeFilter.SelectedItem.text.ToString())
   	  	BindGrid()
  		End Sub

		Sub FilterByCategory(strCategory As String)
    		if strCategory ="All" then 
    			strstatusFilter = "ld_status<>'A'"
    		else
    	   	strstatusFilter = "ld_status='" & strCategory & "'"
  			end if
  		End Sub
		
		Sub ChangetypeFilter(Source As System.Object, e As System.EventArgs)
   		searchtype="Filter"
   		FilterByLeadType(ddlleadtypeFilter.SelectedItem.text.ToString())
   		FilterByCategory(ddlstatusFilter.SelectedItem.text.ToString())
   	  	BindGrid()
  		End Sub
		
		Sub FilterByLeadType(strleadtype As String)
    		if strleadtype ="All" then 
    			strleadFilter = "ld_type<>'A'"
    		else
    	   	strleadFilter = "ld_type='" & strleadtype & "'"
  			end if
  		End Sub
		
		Sub btnsearch (Source As System.Object, e As System.EventArgs)
			lead_status.CurrentPageIndex = 0
			bindgrid()
			searchtype="Search"
			bindgrid()
		End Sub
		
		
		
		
   end class
end namespace