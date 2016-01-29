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
    Public Class workflow
        Inherits PageTemplate

			public worflows as datagrid
			public l_search as textbox
			
        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles mybase.load

            If Not (Page.IsPostBack) Then
					bindworkflows()
            End If
            pagesetup()

        End Sub
        
         Sub workflows_PageChanger(ByVal Source As Object, _
                ByVal E As DataGridPageChangedEventArgs)
            ' Set the CurrentPageIndex before binding the grid 
            worflows.CurrentPageIndex = E.NewPageIndex
            bindworkflows()

        End Sub
        
        Public Sub bindworkflows()
            Dim strpropID As String = Request.QueryString("id")
            Dim strUID As String = Session("userid")
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            mycommand = "Select *, cast(wfm_tbl_pk as varchar(255)) as 'wfpk', convert(varchar(20),wfm_effdate,101) as 'EFdate', convert(varchar(20),wfm_enddate,101) as 'ENdate'  from tbl_WorkFlowMaster Where wfm_useridfk='" & session("userid") & "' order by wfm_tbl_pk desc"
          
            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_WorkFlowMaster")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_WorkFlowMaster"))
                dvProducts.RowFilter = "wfpk like '%'"
                if session("PubSearchWF")="true" then
                	dvProducts.RowFilter = dvProducts.RowFilter + " and (wfpk like '%" & l_search.text & "%' or wfm_name like '%" & l_search.text & "%' or wfm_descript like '%" & l_search.text & "%' or wfm_trigger like '%" & l_search.text & "%' or ENdate like '%" & l_search.text & "%' or EFdate like '%" & l_search.text & "%')"
                end if
                worflows.DataSource = dvProducts
                worflows.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
        End Sub

        	
	   Sub newWF(sender As Object, e As EventArgs)
	   	session("currentwfpk")=""
        		Session("apdate")=""
        		session("stepaction")=""
        		 session("wfstepPK")=""
     			response.redirect("addeditwf.aspx?action=new")
     	End Sub
     	
     	  Sub EditWf(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(1).Text
            response.redirect("addeditwf.aspx?action=view&id=" & content )

        End Sub
        
         Sub clearall(ByVal Source As System.Object, ByVal e As System.EventArgs)
		

            Response.Redirect("workflow.aspx")
        End Sub
        
         Sub filterWFS(ByVal Source As System.Object, ByVal e As System.EventArgs)
         dim y as textbox = Source
           if y.ID = "l_search" then
           		if l_search.text.length > 0 then
           			session("PubSearchWF")="true"
           			session("PubSearchWFV")=l_search.text
           		else
           			session("PubSearchWF")="false"
           			session.remove("PubSearchWFV")
           		end if
           	end if
           	 
           worflows.CurrentPageIndex = 0
           bindworkflows()

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
            leftNav.Controls.Add(LoadControl(System.Configuration.ConfigurationManager.AppSettings("ScreenLeftNavSetup")))
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