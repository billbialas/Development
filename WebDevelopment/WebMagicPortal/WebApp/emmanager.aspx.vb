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
Imports System.Text
Imports FreeTextBoxControls

Namespace PageTemplate
    Public Class emmanager
        Inherits PageTemplate

        Protected MyDataGrid,dgadresults As System.Web.UI.WebControls.DataGrid
        Protected CheckBox As System.Web.UI.WebControls.CheckBox
        Protected OutputMsg As System.Web.UI.HtmlControls.HtmlGenericControl
        Protected objConnect As SqlConnection
        Protected myDataAdapter As SqlDataAdapter
        Protected myCommand As SqlCommand
        Protected DS as Dataset
        Protected dgItem As DataGridItem
			Protected ddlleadprogramFilter,ddlleadtypeFilter, ddlMarketFilter,ddlcstatusFilter, ddlassignedtoFilter, ddlassignedbyFilter, ddlleadtypeupload, ddadcode As System.Web.UI.WebControls.DropDownList
        Public deletedIds As String = ""
        Public ChkdItems As String = ""
        Public SortField As String = ""
        Public ChkBxIndex As String = ""
        Public BxChkd As Boolean = False
        Public CheckedItems As ArrayList
        Public Results() As String
        Public myList As ArrayList = New ArrayList

        Public lb_leadno As Label
        Protected WithEvents lblstatus,lblPageCount,lblviewtype As System.Web.UI.WebControls.Label
        Public ads, dgresponese, dgvinfo, APstat As DataGrid
        Public btnstatus, btnresponses,btnshwinactives,btnchartresults As Button
        public l_search as textbox
       
        Public searchtype As String
        
        Public Nextbutton, Lastbutton, Prevbutton, Firstbutton As LinkButton
        Public pnlrspmanager, pnladmanager, pnlchart, pnlvifno, pnlAPstat,pnlpostings,pnlentposts,pnlLSdetail As Panel
        public fadresults 
        public ADVenuesPP  as datagrid
        public dd_ADs,advenue,ddvenonline,dd_ADPlan,dd_PStat,dd_PTDue as dropdownlist
			public pnladdvenueN,pnladdvenue,pnlPPdetail as panel
			public venuname,venuecode,venueurl,pstEPdate,pstadfrom,pstadto,Pl_search,Pl_searchA as textbox
			public acctsetup,privateven as checkbox
			public pstsvenue,pststatus,pstadkey,lblOrderBy as label
			
			Public adtext
			
        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

            If Not (Page.IsPostBack) Then
            	
                clearsessions()
            	Dim msg As String
                msg = ""
                msg = msg & "<Script Language='JavaScript'>"
                msg = msg & "if (self != top) top.location = self.location;"
                msg = msg & "</Script>"
                Response.Write(msg)
            	removefromlist(-1)
                session("cnter") = 0
                searchtype = ""
           		bindads()
            End If
            
            pagesetup()
        End Sub
        Sub clearcks(ByVal sender As Object, ByVal e As EventArgs)
            Response.Redirect("emmanager.aspx")
        End Sub
        Sub EditPosting(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim ADPK As String = item.Cells(2).Text
            session("adno") = ADPK
            Response.Redirect("editad.aspx?&adno=" & ADPK & "&action=edit")

        End Sub
        Sub NewPosting(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(2).Text
            Dim contentstat As String = item.Cells(7).Text
            session("adno") = content
            session("adstage") = contentstat
            response.redirect("postadi.aspx?source=admgrpost")

        End Sub
        Sub clearsessions()


        End Sub

        Sub createad(ByVal sender As Object, ByVal e As EventArgs)
            Response.Redirect("createem.aspx?action=new")
        End Sub
        
        Sub Managecamps(ByVal sender As Object, ByVal e As EventArgs)
            Response.Redirect("createem.aspx?action=new")
        End Sub
       
       
        Sub newleads_PageChanger(ByVal Source As Object, ByVal E As DataGridPageChangedEventArgs)
            dgvinfo.CurrentPageIndex = E.NewPageIndex

        End Sub
        Sub bindads()
         
   
        End Sub

    

        Sub clearall(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Session("keepadmfilters") = "false"
            Response.Redirect("admanager.aspx?search=*")
        End Sub
        
        Sub filterVenuesAADS(ByVal Source As System.Object, ByVal e As System.EventArgs)
         dim y as textbox = Source
           if y.ID = "l_search" then
           		if l_search.text.length > 0 then
           			session("PubSearchFAD")="true"
           			session("PubSearchFADV")=l_search.text
           		else
           			session("PubSearchFAD")="false"
           			session.remove("PubSearchFADV")
           		end if
           	end if
           	 
           ads.CurrentPageIndex = 0
           bindads()

        end sub
         Sub filterVenuesAADSLK(ByVal Source As System.Object, ByVal e As System.EventArgs)
         dim y as linkbutton = Source
           if y.ID = "l_search" then
           		if l_search.text.length > 0 then
           			session("PubSearchFAD")="true"
           			session("PubSearchFADV")=l_search.text
           		else
           			session("PubSearchFAD")="false"
           			session.remove("PubSearchFADV")
           		end if
           	end if
           	 
           ads.CurrentPageIndex = 0
           bindads()

        end sub
        
         Sub filterADS(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Dim x As dropdownlist = Source
            ads.CurrentPageIndex=0
             if x.ID = "ddlleadprogramFilter" then
             	if ddlleadprogramFilter.selecteditem.text = "All" then
             		session("LeadPF")="false"
             		session("LeadPFV")="All"
             	else
             		session("LeadPF")="true"
             		session("LeadPFV")=ddlleadprogramFilter.selecteditem.text
             	end if
            
             elseif x.ID = "ddlleadtypeFilter" then
             	if ddlleadtypeFilter.selecteditem.text = "All" then
             		session("LeadTF")="false"
             		session("LeadTFV")="All"
             	else
             		session("LeadTF")="true"
             		session("LeadTFV")=ddlleadtypeFilter.selecteditem.text
             	end if
             	
             elseif x.ID = "ddlMarketFilter" then
             	if ddlMarketFilter.selecteditem.text = "All" then
             		session("LeadMF")="false"
             		session("LeadMFV")="All"
             	else
             		session("LeadMF")="true"
             		session("LeadMFV")=ddlMarketFilter.selecteditem.text 
             	end if
            
             	
             end if
           	
           
           ads.CurrentPageIndex = 0
          	bindads()

        End Sub
        
        
        Sub PagerButtonClick(ByVal sender As Object, ByVal e As EventArgs)

            GetCheckBoxValues()
            'used by external paging UI
            Dim arg As String = sender.CommandArgument

            Select Case arg
                Case "next" 'The next Button was Clicked
                    If (ads.CurrentPageIndex < (ads.PageCount - 1)) Then
                        ads.CurrentPageIndex += 1
                    End If

                Case "prev" 'The prev button was clicked
                    If (ads.CurrentPageIndex > 0) Then
                        ads.CurrentPageIndex -= 1
                    End If

                Case "last" 'The Last Page button was clicked
                    ads.CurrentPageIndex = (ads.PageCount - 1)

                Case Else 'The First Page button was clicked
                    ads.CurrentPageIndex = Convert.ToInt32(arg)
            End Select

            'Now, bind the data!
            Session("pgindex") = ads.CurrentPageIndex
            bindads()


            RePopulateCheckBoxes()
        End Sub

        Sub Prev_Buttons()
            Dim PrevSet As String

            If ads.CurrentPageIndex + 1 <> 1 And session("ResultCount") <> -1 Then
                PrevSet = ads.PageSize
                Prevbutton.Text = ("< Prev " & PrevSet)

                If ads.CurrentPageIndex + 1 = ads.PageCount Then
                    Firstbutton.Text = ("<< 1st Page")
                End If
            End If
        End Sub

        Sub Next_Buttons()
            Dim NextSet As String

            If ads.CurrentPageIndex + 1 < ads.PageCount Then
                NextSet = ads.PageSize
                Nextbutton.Text = ("Next " & NextSet & " >")

            End If

            If ads.CurrentPageIndex + 1 = ads.PageCount - 1 Then
                Dim EndCount As Integer = session("filterrowcount") - (ads.PageSize * (ads.CurrentPageIndex + 1))
                Nextbutton.Text = ("Next " & EndCount & " >")

            End If
        End Sub
      
        Sub MyDataGrid_Page(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
            GetCheckBoxValues()

            ads.CurrentPageIndex = e.NewPageIndex
            RePopulateCheckBoxes()

        End Sub

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

        Sub ItemDataBoundEventHandler(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

					
 					Dim itemCellADPK As TableCell = e.Item.Cells(2)
					Dim itemCellADStatus As TableCell = e.Item.Cells(3)
					Dim itemCellADLeadCnt As TableCell = e.Item.Cells(4)
					
					Dim itemCellADPKTEXT as string = itemCellADPK.text
					Dim itemCellADStatusTEXT as string = itemCellADStatus.text
					Dim itemCellADLeadCntTEXT as string = itemCellADLeadCnt.text

                Dim ChkSelected As Button
                ChkSelected = e.Item.Cells(0).FindControl("changestatDG")

                Dim chkb As CheckBox
                chkb = e.Item.Cells(0).FindControl("myCheckbox")

                If itemCellADLeadCntTEXT <= 0 Then
                    chkb.Enabled = False
                Else
                    chkb.Enabled = True
                End If
                

                If itemCellADStatusTEXT = "Active" Then
                    ChkSelected.Text = "Inactivate"

                Else
                    ChkSelected.Text = "Activate"

                End If
                if session("CadRslts")="true" then
                	ads.Columns(0).Visible = true
                else
                	ads.Columns(0).Visible = false
                end if


            End If
        End Sub

      
        
        
        
           Sub SortCommand_Click(sender As Object, e As DataGridSortCommandEventArgs)
           'session("PubSearchFAD")="false"
           'session("LeadPF")="false"
           'session("LeadPF")="false"
          ' session("LeadMF")="false"
				if session("sortexpression") =  e.SortExpression then 
					if session("sortdirection") = "ASC" then
						session("sortdirection") = "DESC"
					else
						session("sortdirection") = "ASC" 
					end if
				else 
					session("sortexpression") =  e.SortExpression
					session("sortdirection") = "ASC" 
				end if
				
          	lblOrderBy.Text =  session("sortexpression") + " " + session("sortdirection") 
     			bindads()

        
        End Sub
         Sub GetSelections_Click2(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Dim x As CheckBox = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(2).Text

            If x.Checked = True Then
               
               addtolist(content)
             
            Else
                removefromlist(content)
            End If

        End Sub
        Sub addtolist(ByVal id As String)
        
            Dim strUID As String = Session("userid")
            Dim strSql As String = "insert into tbl_tmpad (tmpad_uid,tmpad_adno) values ('" & Session("userid") & "','" & id & "')"
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try

        End Sub
        Sub removefromlist(ByVal id As String)
            Dim strUID As String = Session("userid")
            Dim strSql As String
            If id = -1 Then
                strSql = "delete from tbl_tmpad where tmpad_uid='" & Session("userid") & "'"
            Else
                strSql = "delete from tbl_tmpad where tmpad_uid='" & Session("userid") & "' and tmpad_adno='" & id & "'"
            End If

            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try

        End Sub
        Public Sub GetSelections_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)

            Dim rowCount As Integer = 0
            Dim gridSelections As StringBuilder = New StringBuilder()

            'Loop through each DataGridItem, and determine which CheckBox controls
            'have been selected.
            Dim DemoGridItem As DataGridItem

            Dim y As Integer
            For Each DemoGridItem In ads.Items

                Dim myCheckbox As CheckBox = CType(DemoGridItem.Cells(10).Controls(1), CheckBox)
                'Dim x As checkbox = sender
                Dim cell As TableCell = myCheckbox.Parent
                Dim item As DataGridItem = cell.Parent
                Dim content As String = item.Cells(1).Text

                If myCheckbox.Checked = True Then
                    rowCount += 1
                    gridSelections.AppendFormat(content & ",")


                End If
            Next
            'gridSelections.Append("<hr>")
            'gridSelections.AppendFormat("Total number selected is: {0}<br>", rowCount.ToString())
            ' Response.Write(gridSelections.ToString())
            session("adstocmp") = gridSelections.ToString()
            session("cnter") = rowCount
        End Sub

        Sub GetCheckBoxValues()
            'response.write("her")
            'As paging occurs store checkbox values    
            CheckedItems = New ArrayList
            'Loop through DataGrid Items  

            For Each dgItem In ads.Items
                'Retrieve key value of each record based on DataGrids        
                ' DataKeyField property        
                ChkBxIndex = ads.DataKeys(dgItem.ItemIndex)
                CheckBox = dgItem.FindControl("myCheckbox")
                'Add ArrayList to Session if it doesnt exist        
                If Not IsNothing(Session("CheckedItems")) Then
                    CheckedItems = Session("CheckedItems")
                End If
                If CheckBox.Checked Then
                    BxChkd = True
                    'Add to Session if it doesnt already exist            
                    If Not CheckedItems.Contains(ChkBxIndex) Then
                        CheckedItems.Add(ChkBxIndex.ToString())
                    End If
                Else
                    'Remove value from Session when unchecked            
                    CheckedItems.Remove(ChkBxIndex.ToString())
                End If
            Next
            'Update Session with the list of checked items    
            Session("CheckedItems") = CheckedItems

        End Sub

        Sub RePopulateCheckBoxes()
            CheckedItems = New ArrayList
            CheckedItems = Session("CheckedItems")
            If Not IsNothing(CheckedItems) Then
                'Loop through DataGrid Items        
                For Each dgItem In ads.Items
                    ChkBxIndex = ads.DataKeys(dgItem.ItemIndex)
                    'Repopulate DataGrid with items found in Session            
                    If CheckedItems.Contains(ChkBxIndex) Then
                        CheckBox = CType(dgItem.FindControl("myCheckbox"), CheckBox)
                        CheckBox.Checked = True
                    End If
                Next
            End If
            'Copy ArrayList to a new array    
            Results = CheckedItems.ToArray(GetType(String))
            'Concatenate ArrayList with comma to properly send for deletion    
            deletedIds = String.Join(",", Results)
        End Sub
     
        
   End Class
End Namespace