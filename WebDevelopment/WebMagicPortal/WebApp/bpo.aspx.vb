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
	public class bpo 
	   inherits PageTemplate
	   
	   Public strstatusFilter As String = "status<>'A'" 
  	   private shared pubuidpk as string

	   Protected WithEvents BPO_Status As System.Web.UI.WebControls.DataGrid
  		Protected ddlstatusFilter As System.Web.UI.WebControls.DropDownList
  		Protected WithEvents lblstatus As System.Web.UI.WebControls.Label
       
	   
	   private Sub Page_Load(byval sender as object, byval e As EventArgs) handles mybase.load
				if  not (Page.IsPostBack) then
				   FillstatusDropDown()
		   		getagentid()
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
			'MiddleNav.Controls.Add(LoadControl("userid.ascx"))
			footer.controls.add(LoadControl("footer.ascx"))
	  end sub
	   
	   
		 Sub BindGrid()
			'Dim strpropID as String = Request.QueryString("id")
			Dim strUID as String = session("userid")
             	' Create Instance of Connection and Command Object
   	         Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
              	'Dim myCommand As New SqlCommand("Select * from dbo.tbl_bpo Where rqstrUID='"& strUID &"'" , myConnection)
               dim mycommand as string
               if session("sysaccess") = "Partner" or session("sysaccess") = "Broker" then
               	 mycommand = "Select * from dbo.tbl_bpo Where rqstrUID='"& strUID &"'"
         
               else if session("sysaccess") = "Agent" then
               	mycommand = "Select * from dbo.tbl_bpo Where bpoacceptedby='"& strUID &"' or (status='Submitted-Unassigned' and displayallagents='Y') or (status='Submitted-Unassigned' and agentval='" & pubuidpk & "') "
             	end if
             	   	
               Try
      				Dim dataAdapter As New SqlDataAdapter(myCommand, myConnection)
      				Dim dataSet As New DataSet()
      				dataAdapter.Fill(dataSet, "tbl_bpo")
      				Dim dvProducts As New DataView(dataSet.Tables("tbl_bpo"))
      				dvProducts.RowFilter = strstatusFilter
      				BPO_Status.DataSource = dvProducts
      				BPO_Status.DataBind
    					Catch exc As System.Exception
      					Response.Write(exc.ToString())
    					Finally
      					myConnection.Dispose()
    					End Try	
               	
               	
              ' Try
              ' 	myConnection.Open()
              '    BPO_Status.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
				'		BPO_Status.RowFilter = strstatusFilter      
             '     BPO_Status.DataBind()
             '     Catch SQLexc As SqlException
              '         Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
              ' End Try
           	End Sub
                 
		
		'Sub BPO_Edit(sender As Object, e As DataGridCommandEventArgs)
    '		BPO_status.EditItemIndex = e.Item.ItemIndex
    	'	BindGrid()
	'	End Sub

		 
	   Sub newbpo(sender As Object, e As EventArgs)
     			response.redirect("newbpo.aspx")
		End Sub


		Private Sub BPO_Status_ItemDataBound(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.DataGridItemEventArgs) Handles BPO_Status.ItemDataBound'
			'e.Item.Cells[3].Visible = false
		'	Dim rowData As DataRowView
      '	Dim varstatus As string
      '	Dim varstatus1 As System.Web.UI.WebControls.label
     
      '	Dim listPriceLabel As System.Web.UI.WebControls.Literal
      '	Dim discountedPriceLabel As System.Web.UI.WebControls.Literal
      '	Dim totalLabel As System.Web.UI.WebControls.Literal
      	'check the type of item that was databound and only take action if it 
      	'was a row in the datagrid
      	
      '	Dim qtyText As TextBox = e.Item.Cells(2).Controls(1)
      '  	Dim qty As String = qtyText.Text
       ' 	response.write(qty)

      	
      	'Select Case (e.Item.ItemType)
        	'	Case ListItemType.AlternatingItem, ListItemType.EditItem, ListItemType.Item, ListItemType.SelectedItem
         ' 	'get the data for the item being bound
         ' 	rowData = CType(e.Item.DataItem, DataRowView)
         ' 	'get the value for the list price and add it to the sum
         ' 	varstatus1 = CType(e.Item.FindControl("stat"),  System.Web.UI.WebControls.label)
       '	'	'response.write (varstatus1)
         ' 	'varstatus = CDec(rowData.Item("status"))
         ' 	 
         'End Select

	end sub

	Sub FillstatusDropDown()
				Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
    			Dim myCommand As string = "Select tbl_bpostatus_pk, bpostatus from dbo.tbl_bpostatus" 
    			Dim objCmd As New SqlCommand(myCommand, myConnection)
    			Dim dataReader As SqlDataReader = Nothing
    				Try
      				myConnection.Open()
      				dataReader = objCmd.ExecuteReader()
      				ddlstatusFilter.DataSource = dataReader
      				ddlstatusFilter.DataTextField = "bpostatus"
      				ddlstatusFilter.DataValueField = "tbl_bpostatus_pk"
      				ddlstatusFilter.DataBind()
    					Catch exc As System.Exception
      					Response.Write(exc.ToString())
    					Finally
      					myConnection.Dispose()
    					End Try

	       	' Create Instance of Connection and Command Object
   	      '   Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            '  	Dim myCommand As New SqlCommand("Select bpostatus from dbo.tbl_bpostatus" , myConnection)
            '   	
            '   Try
            '   	myConnection.Open()
            '      ddlstatusFilter.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            '      ddlstatusFilter.DataBind()
            '      Catch SQLexc As SqlException
            '           Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            '   
            ' '  End Try
               
	  End Sub

	Sub ChangeFilter(Source As System.Object, e As System.EventArgs)
   	FilterByCategory(ddlstatusFilter.SelectedItem.text.ToString())
   	
   	BindGrid()
  	End Sub


	Sub refreshbpo(Source As System.Object, e As System.EventArgs)
   	response.redirect("bpo.aspx")
  	End Sub



	Sub FilterByCategory(strCategory As String)
    	if strCategory ="All" then 
    		strstatusFilter = "status<>'A'"
    	else
    	   strstatusFilter = "status='" & strCategory & "'"
  		end if
  		'response.write (strstatusFilter)
  	End Sub


	sub getagentid()
		Dim strSql as String = "SELECT users_tbl_pk from tbl_users where UID='" & session("userid") & "'"
   	Dim sqlCmd As SqlCommand
   	Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
      sqlCmd = New SqlCommand(strSql, myConnection)
      
      	Try
         	myConnection.Open()
            Dim Sqldr as SqlDataReader = sqlCmd.ExecuteReader
         	if Sqldr.read() then
   				pubuidpk = Sqldr("users_tbl_pk")
   			end if
   			Catch SQLexc As SqlException
      	      Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
           	finally
           		myConnection.close()
         	end Try
    
	end sub

		
        end class
end namespace