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
    Public Class autopost
        Inherits PageTemplate
        
        public apqueue as datagrid
        Private Shared ResultCount, filterrowcount As Integer
        Public apcomp, apinp, apnew As LinkButton
        Public ap_search As TextBox

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles mybase.load

            If Not (Page.IsPostBack) Then
                Session("apfilter") = "Submitted"
                checkfilter()
                bindaps()
            End If
            pagesetup()

        End Sub
        Sub ap_pagechanger(ByVal Source As Object, _
              ByVal E As DataGridPageChangedEventArgs)
            ' Set the CurrentPageIndex before binding the grid 
            apqueue.CurrentPageIndex = E.NewPageIndex
            bindaps()

        End Sub
        Sub clearall(ByVal Source As System.Object, ByVal e As System.EventArgs)
            ap_search.Text = ""
            bindaps()
        End Sub

        Sub bindaps()

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            Dim RcdCount As Integer
            mycommand = "select *,cast (ap_adno as varchar(20)) as 'apno' from tbl_autopostqueue join tbl_LeadADVenues on tbl_leadadvenues=ap_advenno join tbl_LeadADs on tbl_leadad_pk=ap_adno where ap_status='" & Session("apfilter") & "'"
            Dim i As Integer
            i = 0
            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_autopostqueue")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_autopostqueue"))
                If ap_search.Text <> "" Or ap_search.Text.Length > 0 Then
                    dvProducts.RowFilter = "apno like '%" & ap_search.Text & "%'"
                End If

                filterrowcount = dvProducts.Count
                apqueue.DataSource = dvProducts
                apqueue.DataBind()
                RcdCount = dataSet.Tables("tbl_autopostqueue").Rows.Count.ToString()
                ResultCount = RcdCount

            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

        End Sub
        Sub btnsearch(ByVal Source As System.Object, ByVal e As System.EventArgs)
            apqueue.CurrentPageIndex = 0
            bindaps()

        End Sub

         Sub ItemDataBoundEventHandler(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            End If
        End Sub
        Sub MyDataGrid_Page(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
            apqueue.CurrentPageIndex = e.NewPageIndex
        End Sub
        Sub apfilterSubmitted(ByVal sender As Object, ByVal e As EventArgs)
            Session("apfilter") = "Submitted"
            apqueue.CurrentPageIndex = 0
            checkfilter()
            bindaps()
        End Sub
        Sub apfilterInprocess(ByVal sender As Object, ByVal e As EventArgs)
            Session("apfilter") = "Inprocess"
            apqueue.CurrentPageIndex = 0
            checkfilter()
            bindaps()
        End Sub
        Sub apfilterCompleted(ByVal sender As Object, ByVal e As EventArgs)
            Session("apfilter") = "Completed"
            apqueue.CurrentPageIndex = 0
            checkfilter()
            bindaps()
        End Sub
        Sub checkfilter()

            If Session("apfilter") = "Inprocess" Then
                apcomp.Attributes("style") = "color:#000000; font-family:arial; font-size:8pt; cursor:hand"
                apinp.Attributes("style") = "color:#ff0000; font-family:arial; font-size:8pt; cursor:hand"
                apnew.Attributes("style") = "color:#000000; font-family:arial; font-size:8pt; cursor:hand"
            ElseIf Session("apfilter") = "Completed" Then
                apcomp.Attributes("style") = "color:#ff0000; font-family:arial; font-size:8pt; cursor:hand"
                apinp.Attributes("style") = "color:#000000; font-family:arial; font-size:8pt; cursor:hand"
                apnew.Attributes("style") = "color:#000000; font-family:arial; font-size:8pt; cursor:hand"
            ElseIf Session("apfilter") = "Submitted" Then
                apcomp.Attributes("style") = "color:#000000; font-family:arial; font-size:8pt; cursor:hand"
                apinp.Attributes("style") = "color:#000000; font-family:arial; font-size:8pt; cursor:hand"
                apnew.Attributes("style") = "color:#ff0000; font-family:arial; font-size:8pt; cursor:hand"
            Else
                Session("apfilter") = "Submitted"
                apcomp.Attributes("style") = "color:#000000; font-family:arial; font-size:8pt; cursor:hand"
                apinp.Attributes("style") = "color:#000000; font-family:arial; font-size:8pt; cursor:hand"
                apnew.Attributes("style") = "color:#ff0000; font-family:arial; font-size:8pt; cursor:hand"
            End If

        End Sub
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

    End Class
end namespace