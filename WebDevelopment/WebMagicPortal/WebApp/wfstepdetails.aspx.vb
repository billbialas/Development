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
    Public Class wfstepdetails
        Inherits PageTemplate

			public workflowSteps as datagrid
			public lblleadno,lblleadname,lblworkflowno,lblworkflowname as label
			
        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles mybase.load

            If Not (Page.IsPostBack) Then
					bindworkflowsteps()
					lblleadno.text = request.querystring("leadno")
					lblworkflowno.text = request.querystring("id")
					lblworkflowname.text = getwfstuff(request.querystring("id"))
					getleadinfo()
            End If
           
 			pagesetup()
        End Sub
        
        public function getwfstuff(id as string) as string
         	Dim strUID As String = Session("userid")
            Dim strSql As String = "select wfm_name from tbl_WorkFlowMaster where wfm_tbl_pk='" & id & "'"
            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                   return sqldr("wfm_name")                                 
                    
                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        
        end function
        
        Sub bindworkflowsteps()
            Dim strpropID As String = Request.QueryString("id")
            Dim strUID As String = Session("userid")
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
			   mycommand = "select *, convert(varchar(20),cast(lwf_startdate as datetime),101) as 'WFSdate' from dbo.tbl_LeadWorkFlows " _
			   				& "join dbo.tbl_WorkFlowSteps on wfs_tbl_pk = lwf_wfs_fk " _
			   				& "join tbl_leadWorkFlowsStatus on lwfs_tbl_pk=lwf_lwfs_fk  " _
								& "where lwf_wfm_fk ='" & request.querystring("id") & "' and lwf_lead_fk='" & request.querystring("leadno") & "' " _
								& " and lwf_status='Pending' and lwfs_leadststatus='Active' order by lwf_stepno"
            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_LeadWorkFlows")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_LeadWorkFlows"))

                workflowSteps.DataSource = dvProducts
                workflowSteps.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
        End Sub
            
         Sub ExitStepDet(ByVal Source As System.Object, ByVal e As System.EventArgs)
				 if request.querystring("nav")="wfsetup" then
				 	response.redirect("addeditwf.aspx?action=view&id=" & request.querystring("id") & "&nav=wfsdetails")
				 else
				 	response.redirect("addlead.aspx?id=" & request.querystring("leadno") & "&nav=wfsdetail&action=view")
				 end if
        	end sub
        	
        	
         Sub skipstep(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(0).Text
            skipwfstep(content)
            bindworkflowsteps

        End Sub
        
        Sub skipwfstep(id as string)
            Dim strUID As String = Session("userid")
            Dim strSql As String = "update tbl_LeadWorkFlows set lwf_status='Skipped' where lwf_tbl_pk='" & id & "'"
            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                   lblleadname.text = Sqldr("leadname")
                                  
                    
                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        End Sub
        
        
        
        Sub getleadinfo()
            Dim strUID As String = Session("userid")
            Dim strSql As String = "SELECT case when (ld_lname is null or ld_lname='') then ld_fname  when (ld_fname is null or ld_fname='') then ld_lname else ld_fname + ' ' + ld_lname end as 'leadname', * " _
          									& "from tbl_leads where tbl_leads_pk =" & Request.QueryString("leadno")
            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                   lblleadname.text = Sqldr("leadname")
                                  
                    
                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        End Sub

 		Sub ItemDataBoundEventHandlerWFS(ByVal sender As Object, ByVal e As DataGridItemEventArgs)

				  If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            	Dim btnappnd As button
               btnappnd = e.Item.Cells(0).FindControl("skipstep")
            	
            	Dim itemcellwho2 As TableCell = e.Item.Cells(3)
               Dim itemCellwhotext2 As String = itemCellwho2.Text
              
              	if itemCellwhotext2="Skipped" then
              		btnappnd.enabled=false
              	else
              		btnappnd.enabled=true
              	end if
		    	end if
		  end sub
        
        
        
         Public Sub pagesetup()

            'width will be calculated automatically, but it is sometimes
            layout.Width = System.Configuration.ConfigurationManager.AppSettings("ScreenWidth")
            leftNav.Width = System.Configuration.ConfigurationManager.AppSettings("ScreenLeftNav")
            body.Height = System.Configuration.ConfigurationManager.AppSettings("ScreenBodyHeight")
            body.Width = System.Configuration.ConfigurationManager.AppSettings("ScreenBodyWidth")
            layout.Border = System.Configuration.ConfigurationManager.AppSettings("ScreenBorder")
            footer.Controls.Add(LoadControl(System.Configuration.ConfigurationManager.AppSettings("ScreenFooter")))
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
        

    End Class
end namespace