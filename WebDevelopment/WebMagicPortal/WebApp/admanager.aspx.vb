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
    Public Class admanager
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
        
        Public Nextbutton, Lastbutton, Prevbutton, Firstbutton, btnshowvens As LinkButton
        Public pnlrspmanager, pnladmanager, pnlchart, pnlvifno, pnlAPstat,pnlpostings,pnlentposts,pnlLSdetail,pnlcadresults As Panel
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
            	if request.querystring("clr")="true" then
            		 session("keepadmfilters")="false"
            	end if
            	clearsessions()
            	session("rshow")="Actives"
            	Dim msg As String
                msg = ""
                msg = msg & "<Script Language='JavaScript'>"
                msg = msg & "if (self != top) top.location = self.location;"
                msg = msg & "</Script>"
                Response.Write(msg)
            	removefromlist(-1)
                session("cnter") = 0
                searchtype = ""
               ' checkquerystring()
                If Session("svens") = "True" Then
                    btnshowvens.Text = "Hide Venues"
                    Session("svens") = "True"
                Else
                    btnshowvens.Text = "Include Venues"
                    Session("svens") = "False"
                End If
           
                If Session("branding") = "Yes" Then
                    btnresponses.Visible = false
                Else
                    btnresponses.Visible = False
                End If
                If Request.QueryString("source") = "rspmgr" Then
                    pnlrspmanager.Visible = True
                    pnladmanager.Visible = False
                    bindresponses("Actives")
                End If
                If Request.QueryString("source") = "vinfo" Then
                    pnlrspmanager.Visible = False
                    pnladmanager.Visible = False
                    pnlvifno.Visible = True
                    bindvfino()
                End If
                'bindfields()
                'Session("svens") = "False"
                If Request.QueryString("source") = "lsinfo" Then
                    showvinfoNOBT()
                End If
      			 FillLeadprogramDropDown()
      			 FillLeadTypeDropDown()
      			 FillmktTypeDropDown()
      			 if  session("LeadPF")="true"  then
      			 	ddlleadprogramFilter.SelectedIndex = ddlleadprogramFilter.Items.IndexOf(ddlleadprogramFilter.Items.FindByText( session("LeadPFV")))
      			 end if
      			 if  session("LeadtF")="true"  then
      			 	ddlleadtypeFilter.SelectedIndex = ddlleadtypeFilter.Items.IndexOf(ddlleadtypeFilter.Items.FindByText( session("LeadtFV")))
      	
      			 end if
      			 if  session("LeadMF")="true"  then
      			 	 	ddlMarketFilter.SelectedIndex = ddlMarketFilter.Items.IndexOf(ddlMarketFilter.Items.FindByText( session("LeadMFV")))
      	
      			 end if
      			 '			 session("PubSearchFAD")="false"
           		bindads()
            End If
            
            pagesetup()
        End Sub
        
        Sub FillmktTypeDropDown()
  				Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='marketprogram' and  (x_company='All' or x_uid='" & Session("userid") & "') order by x_descr "
            '(x_company='" & Session("company") & "' or
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                ddlMarketFilter.DataSource = dataReader
                ddlMarketFilter.DataTextField = "x_descr"
                ddlMarketFilter.DataValueField = "tbl_xwalk_pk"
                ddlMarketFilter.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
             ddlMarketFilter.Items.Insert(0, New ListItem("All", "9999"))

			end sub
			
			
        Sub FillLeadTypeDropDown()

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='leadtype' and  (x_company='All' or x_uid='" & Session("userid") & "') order by x_descr"
            '(x_company='" & Session("company") & "' or
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
            ddlleadtypeFilter.Items.Insert(0, New ListItem("All", "9999"))

        End Sub
        
         Sub FillLeadprogramDropDown()

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='leadprogram' and  (x_company='All' or x_uid='" & Session("userid") & "') order by x_descr"
            '(x_company='" & Session("company") & "' or
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                ddlleadprogramFilter.DataSource = dataReader
                ddlleadprogramFilter.DataTextField = "x_descr"
                ddlleadprogramFilter.DataValueField = "tbl_xwalk_pk"
                ddlleadprogramFilter.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
             ddlleadprogramFilter.Items.Insert(0, New ListItem("All", "9999"))

        End Sub
        
        
        sub clearsessions()
        		session("ResultCount")=0
        		session("filterrowcount")=0
        		session("querystring")=""
        		session("adstocmp")=""
        		session("cnter")=0
        		session("venueonline")=""
        		session("adkeycode")=""
        		session("venhtml")=""
        		session("CadRslts")="false"
        		lblOrderBy.Text = "tbl_leadad_pk desc"
        		if session("keepadmfilters")="false" then
	        		session("PubSearchFAD")="false"
	           	session("LeadPF")="false"
	           	session("LeadTF")="false"
	          	session("LeadMF")="false"
	          	session.remove("PubSearchFADV")
        			session("LeadPFV")="All"
        			session("LeadTFV")="All"
        			session("LeadMFV")="All"
	         else
	         	l_search.text=session("PubSearchFADV")
	         	ddlleadprogramFilter.SelectedIndex = ddlleadprogramFilter.Items.IndexOf(ddlleadprogramFilter.Items.FindByText(session("LeadPFV")))
	         	ddlleadtypeFilter.SelectedIndex = ddlleadtypeFilter.Items.IndexOf(ddlleadtypeFilter.Items.FindByText(session("LeadTFV")))
	         	ddlMarketFilter.SelectedIndex = ddlMarketFilter.Items.IndexOf(ddlMarketFilter.Items.FindByText(session("LeadMFV")))
	         	
	         	
	         	
        		end if		
        		
        	
			end sub
        Sub showpostings(ByVal sender As Object, ByVal e As EventArgs)
        		session("keepadmfiltersA")="false"
        		session.remove("PubSearchFV")
            	session.remove("PubStatFV")
            	session.remove("PubADSFV")
            	session.remove("PubADPlanFV")
            	session.remove("PubTargetDateFV")
            	session.remove("PubADVenueFV")
        		'pnlrspmanager.visible=false
        		'pnladmanager.visible=false
        		'pnlvifno.visible=false
        		'pnlchart.visible=false
        		'pnlAPstat.visible=false
        		'pnlpostings.visible=true
        		'bindADVenuesPP()
        		'FillADS()
        		
        		'FillPlanS("All")
        		session("adno")="All"
        		response.redirect("adpostings.aspx?source=admanager")
        end sub
        
         Sub showpostingsQ(ByVal sender As Object, ByVal e As EventArgs)
        		session("keepadmfiltersA")="false"
        		session.remove("PubSearchFV")
            	session.remove("PubStatFV")
            	session.remove("PubADSFV")
            	session.remove("PubADPlanFV")
            	session.remove("PubTargetDateFV")
            	session.remove("PubADVenueFV")
        		session("adno")="All"
        		response.redirect("adpostings.aspx?source=admanagerQ")
          end sub
        
        Sub showvenues(ByVal sender As Object, ByVal e As EventArgs)
            If btnshowvens.text = "Include Venues" Then
                btnshowvens.Text = "Hide Venues"
                Session("svens") = "True"
            Else
                btnshowvens.Text = "Include Venues"
                Session("svens") = "False"
            End If
            bindads()
        End Sub
        Sub togglestatus(ByVal sender As Object, ByVal e As EventArgs)
            If btnstatus.text = "Show All ADS" Then
                btnstatus.text = "Hide Inactive ADS"
            Else
                btnstatus.text = "Show All ADS"
            End If
            bindads()
        End Sub
        Sub clickapstat(ByVal sender As Object, ByVal e As EventArgs)
            pnladmanager.Visible = False
            pnlAPstat.Visible = True
            bindapstat()
        End Sub
        Sub toggleads(ByVal sender As Object, ByVal e As EventArgs)
            pnlrspmanager.Visible = False
            pnlvifno.Visible = False
            pnladmanager.Visible = True
            bindads()
        End Sub
       

        Sub toggleads2(ByVal sender As Object, ByVal e As EventArgs)
            session("CadRslts")="false" 
            pnlcadresults.visible=false
            pnlchart.visible=false
				pnladmanager.visible=true
				removefromlist("-1")
            bindads()
            'RePopulateCheckBoxes () 
        End Sub
        Sub toggleads3(ByVal sender As Object, ByVal e As EventArgs)
            pnladmanager.Visible = True
            pnlAPstat.Visible = False
            bindads()

        End Sub
        
        Sub Addresp(ByVal sender As Object, ByVal e As EventArgs)
            Response.Redirect("brandingadd.aspx?action=new&source=rspmgr")

        End Sub
        
        Sub showinacts(ByVal sender As Object, ByVal e As EventArgs)
           if btnshwinactives.text="Show All" then
           		session("rshow")="All"
           		dgresponese.CurrentPageIndex = 0
             bindresponses(session("rshow"))
             btnshwinactives.text="Actives"
           else
          	 session("rshow")="Actives"
          	 dgresponese.CurrentPageIndex = 0
             bindresponses(session("rshow"))
             btnshwinactives.text="Show All"
           end if
        End Sub       
        

        Sub createad(ByVal sender As Object, ByVal e As EventArgs)
            Response.Redirect("createad.aspx?action=new&type=complete")
        End Sub
        Sub quickad(ByVal sender As Object, ByVal e As EventArgs)
            Response.Redirect("createad.aspx?action=new&type=quick")
        End Sub
        
        Sub showvinfo(ByVal sender As Object, ByVal e As EventArgs)
            showvinfoNOBT()
        End Sub
        
        Sub showvinfoNOBT()
            pnlrspmanager.Visible = False
            pnladmanager.Visible = False
            pnlvifno.Visible = True
            bindvfino()
        End Sub
        
        
        
        
        Sub mgresponses(ByVal sender As Object, ByVal e As EventArgs)
            pnlrspmanager.Visible = True
            pnladmanager.Visible = False
            bindresponses(session("rshow"))
        End Sub
         Sub clearcks(ByVal sender As Object, ByVal e As EventArgs)
            Response.Redirect("admanager.aspx")
        End Sub
        Sub Addvinfo(ByVal sender As Object, ByVal e As EventArgs)
            Response.Redirect("vinfo.aspx?action=new")
        End Sub

        Sub bindfields()
            Dim strUID As String = Session("userid")
            Dim strSql As String = "SELECT fname + ' ' + lname as assignedby, " _
            & "case when (select count(*) from tbl_leadscontacthistory where tbl_leads_fk = tbl_leads_pk and cnt_who <> 'System') > 0 then " _
            & "'Yes' else 'No' end as 'HasHistory',* " _
            & "from tbl_leads join dbo.tbl_users on Uid=ld_assignedbyuid where tbl_leads_pk =" & Session("clead")
            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    lb_leadno.Text = Sqldr("tbl_leads_pk")
                    
                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        End Sub
        Sub bindvfino()
            Dim strUID As String = Session("userid")
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            Dim RcdCount As Integer
            mycommand = "select *,cast (vpass_tbl_pk as varchar(20)) as 'vno' from tbl_venueinfo where vpass_uid='" & Session("userid") & "'"
            Dim i As Integer
            i = 0
            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_venueinfo")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_venueinfo"))
                session("filterrowcount") = dvProducts.Count
                dgvinfo.DataSource = dvProducts
                dgvinfo.DataBind()
                RcdCount = dataSet.Tables("tbl_venueinfo").Rows.Count.ToString()
                session("ResultCount") = RcdCount

            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

        End Sub
        Sub newleads_PageChanger(ByVal Source As Object, ByVal E As DataGridPageChangedEventArgs)
            dgvinfo.CurrentPageIndex = E.NewPageIndex
            bindvfino()
        End Sub
        Sub Apstat_PageChanger(ByVal Source As Object, ByVal E As DataGridPageChangedEventArgs)
            APstat.CurrentPageIndex = E.NewPageIndex
            bindapstat()
        End Sub

        Sub bindapstat()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String

            mycommand = "Select *,convert(varchar(20),av_APFrom,101) as 'fdate',convert(varchar(20),av_APTo,101) as 'tdate' from tbl_LeadADVenues join tbl_LeadADs on tbl_leadad_pk=av_leadads_FK where av_autopost='Yes' and ad_userid='" & Session("userid") & "'"

            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_LeadADVenues")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_LeadADVenues"))
                APstat.DataSource = dvProducts
                APstat.DataBind()

            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
        End Sub

        Sub bindads()
            Dim strUID As String = Session("userid")
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            Dim RcdCount As Integer
                If btnstatus.Text = "Show All ADS" Then
                	
                    mycommand = "Select  tbl_leadad_pk,cast(tbl_leadad_pk as varchar(255)) as 'adno', " _
                       & "ad_title,ad_stage,ad_status,ad_leadtype,convert(varchar(20),ad_createdate,101) as 'cdate',ad_totalleadcount, cast(ad_text as varchar(8000)) as 'ad_text', " _
                       & "case when ((select distinct av_adplaced from tbl_LeadADVenues " _
                       & "where av_leadads_FK = tbl_leadad_pk and av_adplaced='Published' )='Published') then 'Yes' " _
                       & " Else 'No' end as 'ADPlaced',ad_leadprogram,ad_marketprogram,* from tbl_leadads " _
                        & "left join tbl_adbranding on tbl_branding_pk = ad_intakeresponse " _
                       & "where ad_userid ='" & Session("userid") & "' and ad_status='Active' order by " + lblOrderBy.Text

                Else
                    mycommand = "Select  tbl_leadad_pk,cast(tbl_leadad_pk as varchar(255)) as 'adno', " _
                       & "ad_title,ad_stage,ad_status,ad_leadtype,convert(varchar(20),ad_createdate,101) as 'cdate',ad_totalleadcount, cast(ad_text as varchar(8000)) as 'ad_text', " _
                       & "case when ((select distinct av_adplaced from tbl_LeadADVenues " _
                       & "where av_leadads_FK = tbl_leadad_pk and av_adplaced='Published' )='Published') then 'Yes' " _
                       & " Else 'No' end as 'ADPlaced',ad_leadprogram,ad_marketprogram,* from tbl_leadads " _
                        & "left join tbl_adbranding on tbl_branding_pk = ad_intakeresponse " _
                       & "where ad_userid ='" & Session("userid") & "' order by  " +  lblOrderBy.Text
                End If

           

            Dim i As Integer
            i = 0
            
            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_leadads")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_leadads"))
                dvProducts.RowFilter = "adno like '%'"
                if session("PubSearchFAD")="true" then
                	dvProducts.RowFilter = dvProducts.RowFilter + " and (adno like '%" & l_search.text & "%' or ad_title like '%" & l_search.text & "%' or ad_text like '%" & l_search.text & "%' or ad_leadprogram like '%" & l_search.text & "%' or ad_marketprogram like '%" & l_search.text & "%' or ad_Leadtype like '%" & l_search.text & "%')"
                end if
                if session("LeadPF")="true" then
					 	dvProducts.RowFilter = dvProducts.RowFilter + " and ad_leadprogram = '" & ddlleadprogramFilter.selecteditem.text & "'"	
                end if
                if session("LeadTF")="true" then
					 	dvProducts.RowFilter = dvProducts.RowFilter + " and ad_Leadtype = '" & ddlleadtypeFilter.selecteditem.text & "'"	
                end if
                if session("LeadMF")="true" then
					 	dvProducts.RowFilter = dvProducts.RowFilter + " and ad_marketprogram = '" & ddlMarketFilter.selecteditem.text & "'"	
                end if
                
                session("filterrowcount") = dvProducts.Count
                ads.DataSource = dvProducts
                ads.DataBind()
                RcdCount = dataSet.Tables("tbl_leadads").Rows.Count.ToString()
                session("ResultCount") = RcdCount

            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
  
  				If ads.CurrentPageIndex <> 0 Then
                Call Prev_Buttons()
                Firstbutton.Visible = True
                Prevbutton.Visible = True
            Else
                Firstbutton.Visible = False
                Prevbutton.Visible = False
            End If

            If ads.CurrentPageIndex <> (ads.PageCount - 1) Then
                Call Next_Buttons()
                Nextbutton.Visible = True
                Lastbutton.Visible = True
            Else
                Nextbutton.Visible = False
                Lastbutton.Visible = False
            End If

            lblPageCount.Text = "Page " & ads.CurrentPageIndex + 1 & " of " & ads.PageCount


        End Sub

        Sub bindresponses(stat as string)

            Dim strUID As String = Session("userid")
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            Dim RcdCount As Integer
            if stat= "Actives" then
            	mycommand = "select *,cast (tbl_branding_pk as varchar(255)) as 'rno' from tbl_adbranding where br_uid_fk='" & Session("userid") & "' and br_bstat='Active' order by tbl_branding_pk desc"
            else
            	mycommand = "select *,cast (tbl_branding_pk as varchar(255)) as 'rno' from tbl_adbranding where br_uid_fk='" & Session("userid") & "' order by tbl_branding_pk desc"
           
            end if
            Dim i As Integer
            i = 0
            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_adbranding")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_adbranding"))
                dvProducts.RowFilter = "rno like '%'"
                if session("PubSearchF")="true" then
                	dvProducts.RowFilter = dvProducts.RowFilter + " and (rno like '%" & Pl_searchA.text & "%' or br_name like '%" & Pl_searchA.text & "%' or br_description like '%" & Pl_searchA.text & "%')"
                end if
                
                 session("filterrowcount") = dvProducts.Count
                dgresponese.DataSource = dvProducts
                dgresponese.DataBind()
                RcdCount = dataSet.Tables("tbl_adbranding").Rows.Count.ToString()
                session("ResultCount") = RcdCount

            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try



        End Sub

        Sub clearall(ByVal Source As System.Object, ByVal e As System.EventArgs)
			session("keepadmfilters")="false"

            Response.Redirect("admanager.aspx?search=*")
        End Sub
        
         Sub clearallRSP(ByVal Source As System.Object, ByVal e As System.EventArgs)

				session("PubSearchF")="false"
            Response.Redirect("admanager.aspx?source=rspmgr")
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
        
        
        
      Sub cancelchart(ByVal Source As System.Object, ByVal e As System.EventArgs) 
       		session("CadRslts")="false" 
       		pnlcadresults.visible=false
            pnlchart.visible=false
				pnladmanager.visible=true
				bindads()
      end sub
        
         Sub chartresultsA(ByVal Source As System.Object, ByVal e As System.EventArgs)
        		removefromlist("-1")
        		pnlcadresults.visible=true
        		session("CadRslts")="true"
        		bindads()
        		
        	End sub

        Sub chartresults(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String = "Select * from tbl_tmpad where tmpad_uid = '" & Session("userid") & "'"
            Dim ad As New SqlDataAdapter(mycommand, myConnection)
            Dim ds As New DataSet()
            Dim str As New StringBuilder()

            Dim i As Integer

            Try
                ad.Fill(ds)

            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
            Dim url, url2 As String
            url = "adresults.aspx?"
            Dim adz As String
            If ds.Tables(0).Rows.Count > 0 Then


                For i = 0 To ds.Tables(0).Rows.Count - 1

                    If i = 0 Then
                        url2 = "ad" & i + 1 & "=" & ds.Tables(0).Rows(i)(2).ToString()
                    Else
                        url2 = url2 + "&ad" & i + 1 & "=" & ds.Tables(0).Rows(i)(2).ToString()
                    End If

                    If i = 0 Then
                        adz = ds.Tables(0).Rows(i)(2).ToString()
                    Else
                        adz = adz + "," & ds.Tables(0).Rows(i)(2).ToString()

                    End If

                Next
                'Response.Write(adz)
                bindchartres(adz)



                fadresults.Attributes("src") = url & url2 & "&total=" & ds.Tables(0).Rows.Count
                pnlchart.Visible = True
                pnladmanager.Visible = False
            End If
            'Dim sites As String() = Nothing
            'sites = adstocmp.Split(",")
            'Dim s As String
            'Dim i As Integer = 0
            'Dim arr(100) As String
            'For Each s In sites
            ' i = i + 1
            'arr(i) = s
            'Next s
            'Dim xx As Integer
            'Dim url, url2 As String
            'url = "adresults.aspx?"
            'For xx = 1 To session("cnter")
            ' If xx = 1 Then
            ' url2 = "ad" & xx & "=" & arr(xx)
            'Else
            'url2 = url2 + "&ad" & xx & "=" & arr(xx)
            'End If
            'Next
            'fadresults.Attributes("src") = url & url2 & "&total=" & session("cnter")
            'Response.Write(url & url2 & "&total=" & session("cnter"))
            'pnlchart.visible=true
            'pnladmanager.visible=false
        End Sub
        Sub bindchartres(ByVal id As String)
            Dim strUID As String = Session("userid")
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            Dim RcdCount As Integer

            mycommand = "select tbl_leadad_pk,ad_title,ad_totalLeadcount, (select sum(ad_totalLeadcount) from dbo.tbl_LeadADs  " _
       & "where ad_userid='" & Session("userid") & "' and tbl_leadad_pk in (" & id & ")) as 'TotalLeads', " _
       & "cast(cast (ad_totalLeadcount as decimal(10,2))/cast((select sum(ad_totalLeadcount) from dbo.tbl_LeadADs " _
       & "where ad_userid='" & Session("userid") & "' and tbl_leadad_pk in (" & id & ")) as decimal(10,2)) as decimal (10,2)) * 100 as 'PercentOfTotal' " _
       & "from dbo.tbl_LeadADs where  ad_userid='" & Session("userid") & "' " _
       & " and tbl_leadad_pk in (" & id & ") group by tbl_leadad_pk,ad_title,ad_totalLeadcount "
            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_LeadADs")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_LeadADs"))
                dgadresults.DataSource = dvProducts
                dgadresults.DataBind()

            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
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

        Sub btnsearch(ByVal Source As System.Object, ByVal e As System.EventArgs)
            searchstring()
        End Sub

        Sub searchstring()
            Dim psearch As String
            Dim pleadtype As String
            Dim pstatus As String
            Dim pconstatus As String
            Dim passigned As String
            Dim passignedby As String
            Dim pfollowup As String

            'check search
            If l_search.Text = "" Then
                psearch = "*"
            Else
                psearch = l_search.Text
                Session("search") = "text"
            End If

            'if ddlleadtypeFilter.selecteditem.Text ="All" then
            '	pleadtype = "*"
            'else
            '	pleadtype = ddlleadtypeFilter.selecteditem.Text
            'end if
            '
            'if ddlstatusFilter.selecteditem.Text ="All" then
            '	pstatus = "*"
            'else
            '	pstatus= ddlstatusFilter.selecteditem.Text
            'end if
            '
            'if ddlcstatusFilter.selecteditem.Text ="All" then
            '	pconstatus = "*"
            'else
            '	pconstatus= ddlcstatusFilter.selecteditem.Text
            'end if

            'if ddlassignedtoFilter.selecteditem.Text ="All" then
            '	passigned = "*"
            'else
            '	passigned= ddlassignedtoFilter.selecteditem.Text
            'end if

            'if ddlassignedbyFilter.selecteditem.Text ="All" then
            '	passignedby = "*"
            'else
            '	passignedby= ddlassignedbyFilter.selecteditem.Text
            'end if

            'if passignedby = "*" then 
            '	enteredbyname=passignedby	
            'else
            '	findenteredbyname(passignedby)
            'end if

            'if ddlfollowup.selecteditem.Text ="All" then
            '	pfollowup = "*"
            'else
            '	pfollowup= ddlfollowup.selecteditem.Text
            'end if

            session("querystring") = "admanager.aspx?search=" & psearch
            '& "&leadtype=" & pleadtype & "&status=" & pstatus & "&constatus=" & pconstatus & "&assignedto=" & passigned & "&assignedby=" & passignedby & "&followup=" & pfollowup

            Response.Redirect(session("querystring"))
        End Sub

        Sub checkquerystring()
            If Request.QueryString("search") <> "*" Then
                Session("search") = "Text"
                l_search.Text = Request.QueryString("search")
            Else
                l_search.Text = ""
            End If



            Session("qstring") = "admanager.aspx?search=" & Request.QueryString("search")
            '& "&leadtype=" & Request.QueryString("leadtype") & "&status=" & Request.QueryString("status") & "&constatus=" & Request.QueryString("constatus") & "&assignedto=" & Request.QueryString("assignedto") & "&assignedby=" & Request.QueryString("assignedby") & "&followup=" & Request.QueryString("followup")

        End Sub

        Sub MyDataGrid_Page(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
            GetCheckBoxValues()

            ads.CurrentPageIndex = e.NewPageIndex
            RePopulateCheckBoxes()

        End Sub
        Sub MyDataGridR_Page(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
            dgresponese.CurrentPageIndex = e.NewPageIndex
            bindresponses(session("rshow"))
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

        Sub Changestat(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(2).Text

            changestatdb(content)
            bindads()

        End Sub
        Sub NewPosting(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(2).Text
             dim contentstat as string = item.Cells(7).Text
				session("adno")=content
				session("adstage")=contentstat
				response.redirect("postadi.aspx?source=admgrpost")

        End Sub
        
        
        
        Sub filterVenuesAB(ByVal Source As System.Object, ByVal e As System.EventArgs)
         dim y as textbox = Source
           if y.ID = "Pl_searchA" then
           		if Pl_searchA.text.length > 0 then
           			session("PubSearchF")="true"
           		else
           			session("PubSearchF")="false"
           		end if
           	end if
           	 
           dgresponese.CurrentPageIndex = 0
         	 bindresponses(session("rshow"))
          
          

        end sub
        
        
          Sub Viewstats(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(2).Text
            dim contentstat as string = item.Cells(7).Text
				session("adno")=content
				response.redirect("createad.aspx?action=edit&adno=" & content & "&source=viewstats" )

        End Sub
         
         Sub WrkWPlans(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(2).Text
            dim contentstat as string = item.Cells(7).Text
				session("adno")=content
				response.redirect("createad.aspx?action=edit&adno=" & content & "&source=wrkpstsA" )

        End Sub
        
        
        
        
          Sub NewPostingA(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(2).Text
            dim contentstat as string = item.Cells(7).Text
				session("adno")=content
				session("adstage")=contentstat
				response.redirect("postadi.aspx?source=admgrpostOnce")

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

        
        end sub
        
        
        
         Sub EditPosting(ByVal Source As System.Object, ByVal e As System.EventArgs)
 				Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim ADPK As String = item.Cells(2).Text
            session("adno")= ADPK
            response.redirect("editad.aspx?&adno=" &  ADPK & "&action=edit")
           

			end sub
        
        
         Sub editbranding(ByVal Source As System.Object, ByVal e As System.EventArgs)
 				Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim brPK As String = item.Cells(1).Text
            session("brno")=brPK
            response.redirect("brandingadd.aspx?id=" & brPK & "&action=edit&source=rspmgr")
           

			end sub
        
        
        
        

        Sub changestatdb(ByVal id As String)

            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select ad_status from tbl_LeadADs where tbl_leadad_pk='" & id & "'"
            Dim cs As String
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    cs = Sqldr("ad_status")
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try

            If cs = "Active" Then
                cs = "Inactive"
            Else
                cs = "Active"
            End If

            strSql = "update tbl_LeadADs set ad_status='" & cs & "' where tbl_leadad_pk='" & id & "'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then

                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try

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
        Sub vinfo_PageChanger(ByVal Source As Object, _
                ByVal E As DataGridPageChangedEventArgs)
            ' Set the CurrentPageIndex before binding the grid 
            dgvinfo.CurrentPageIndex = E.NewPageIndex
            bindvfino()

        End Sub
        
        
         Public Sub bindADVenuesPP()
             Dim strUID As String = Session("userid")
            
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            
		      mycommand = "Select cast(av_leadads_FK as nvarchar(255)) as 'adno', cast(tbl_leadadvenues as nvarchar(255)) as 'venno', " _
		                & "convert(varchar(20),av_APFrom,101) as 'APFrom', convert(varchar(20),av_APTo,101) as 'APFTo', x_canselfpub,*, " _
		                & "cast(lap_adfk as nvarchar(255)) as 'adplanno', " _
		                & "convert(varchar(20),av_APTo,101) as 'APTo', convert(varchar(20),getdate(),101) as 'Today', " _
		                & "convert(varchar(20),getdate()+2,101) as 'Today2' " _
		                & "from tbl_LeadADVenues " _
		                & "join tbl_LeadADs on tbl_leadad_pk=av_leadads_FK  " _
		                & "join dbo.tbl_xwalk on x_descr  =av_name and x_type='leadsource' " _
		                & "left join dbo.tbl_leadADPlans on lap_adfk = av_leadads_FK and LAP_tbl_pk= av_lapfk  " _
		                & "where ad_userid ='" & session("userid") & "'"
		      
		      Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_LeadADVenues")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_LeadADVenues"))
					 dvProducts.RowFilter = "adno like '%*%'"
					 if session("PubStatF")="true" then
					 	dvProducts.RowFilter = dvProducts.RowFilter + " and av_adplaced = '" & dd_PStat.selecteditem.text & "'"	
                end if
                if session("PubADSF")="true" then
                	if dd_ADs.selecteditem.text="Actives" then
                		dvProducts.RowFilter = dvProducts.RowFilter + " and ad_status = 'Active'"	
            		elseif dd_ADs.selecteditem.text="Inactives" then
                		dvProducts.RowFilter = dvProducts.RowFilter + " and ad_status = 'Inactive'"	
            		else
                		dvProducts.RowFilter = dvProducts.RowFilter + " and adno= '" & dd_ADs.selecteditem.value & "'"	
                	end if
                end if
                if session("PubADPlanF")="true" then
                 	if dd_ADPlan.selecteditem.text="Actives" then
                		dvProducts.RowFilter = dvProducts.RowFilter + " and lap_status = 'Active'"	
            		elseif dd_ADPlan.selecteditem.text="Inactives" then
                		dvProducts.RowFilter = dvProducts.RowFilter + " and lap_status = 'Inactive'"	
            		else
                     	dvProducts.RowFilter = dvProducts.RowFilter + " and adplanno = '" & dd_ADPlan.selecteditem.value & "'"	
           			end if
                end if
                if session("PubSearchF")="true" then
                	dvProducts.RowFilter = dvProducts.RowFilter + " and (adno like '%" & Pl_search.text & "%' or venno like '%" & Pl_search.text & "%' or lap_name like '%" & Pl_search.text & "%' or ad_title like '%" & Pl_search.text & "%' or av_key like '%" & Pl_search.text & "%' or av_name like '%" & Pl_search.text & "%' or av_Postingno like '%" & Pl_search.text & "%')"
                end if
                if session("PubTargetDateF")="true" then
               	if dd_PTDue.selecteditem.text="Due Today" then 
               	   dvProducts.RowFilter = dvProducts.RowFilter + " and APTo = Today"
             		elseif dd_PTDue.selecteditem.text="Past Due" then 
             			dvProducts.RowFilter = dvProducts.RowFilter + " and APTo < Today"
                	else
                	  	dvProducts.RowFilter = dvProducts.RowFilter + " and APTo > Today and APTo <= Today2 "
                	end if
                end if
                
                
                
                ADVenuesPP.DataSource = dvProducts
                ADVenuesPP.DataBind()
					
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
            
            
        End Sub
        
         Sub NewPPlanPost(ByVal Source As System.Object, ByVal e As System.EventArgs)
				
						
						
						pnlentposts.visible=false
						pnladdvenueN.visible=true
						'bindADVenuesPP()
						
		
        end sub
        
         Sub FillADvenues()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='leadsource' and (x_company='All' or x_Uid='" & Session("userid") & "')"
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                advenue.DataSource = dataReader
                advenue.DataTextField = "x_descr"
                advenue.DataValueField = "tbl_xwalk_pk"
                advenue.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

            advenue.Items.Insert(0, New ListItem("Select..", "9999"))


        End Sub
        
         
        Sub ADVenuesPP_Edit(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
            ADVenuesPP.EditItemIndex = e.Item.ItemIndex
         	bindADVenuesPP()
        End Sub
        
        Sub ADVenuesPP_Cancel(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
            ADVenuesPP.EditItemIndex = -1
           bindADVenuesPP()
        End Sub
        
        Sub ADVenuesPP_Update(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
            'Read in the values of the updated row
            Dim dgID As String = e.Item.Cells(1).Text
            'CType(e.Item.Cells(0).Controls(0), TextBox).Text
            Dim dgname As String = CType(e.Item.Cells(7).Controls(0), TextBox).Text
            Dim dgdesc As String = CType(e.Item.Cells(8).Controls(0), TextBox).Text
				
				if dgname.length <= 0 then
					dgname =  DateTime.Now.ToShortDateString()
				end if
							
            Dim strSql As String 
            strSql = "update tbl_LeadADVenues set av_adplaced='Published',av_APFrom='" & dgname & "', av_Postingno='" & dgdesc & "' where tbl_leadadvenues='" & dgID & "'"
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
            ADVenuesPP.EditItemIndex = -1
            bindADVenuesPP()
            
        End Sub
  Sub rerunpost(ByVal Source As System.Object, ByVal e As System.EventArgs)
        				
			        		'response.write(session("CPlanId"))
			        		Dim strUID As String = Session("userid")
			           	 Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
			            Dim mycommand As String
			            mycommand = "Select av_leadads_FK,av_name,av_key ,av_online,av_textAll,av_lapfk,tmpad_adno from tbl_tmpad " _
																& "join dbo.tbl_LeadADVenues on tbl_leadadvenues = tmpad_adno " _
			            	            				& "where tmpad_uid='" & Session("userid") & "'"
			            Dim ad As New SqlDataAdapter(mycommand, myConnection)
			            Dim ds As New DataSet()
			            Dim str As New StringBuilder()
			
			            Try
			                ad.Fill(ds)
			                'ds.Tables(0).TableName = "bill"
			            Catch exc As System.Exception
			                Response.Write(exc.ToString())
			            Finally
			                myConnection.Dispose()
			            End Try           
			           
			            'response.write(ds.Tables(0).Rows.Count - 1)
			            Dim i As Integer
			            For i = 0 To ds.Tables(0).Rows.Count - 1
			                 cloneadv(ds.Tables(0).Rows(i)(0).ToString(), ds.Tables(0).Rows(i)(1).ToString(),ds.Tables(0).Rows(i)(2).ToString(),ds.Tables(0).Rows(i)(3).ToString(),ds.Tables(0).Rows(i)(4).ToString(),ds.Tables(0).Rows(i)(5).ToString())
			            	 removefromlist(ds.Tables(0).Rows(i)(6).ToString())
			          Next
			            bindADVenuesPP()
			         
        end sub
        
        sub cloneadv(adno as string, avname as string, adcodeS as string,vonlineS as string,adtextS as string,ppidS as string)
        		Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String
            sqlproc = "sp_AddADVenue"

            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

            Dim prmadno As New SqlParameter("@av_leadads_FK", SqlDbType.Int)
            prmadno.Value = adno
            myCommandADD.Parameters.Add(prmadno)

            Dim prmadvenue As New SqlParameter("@av_name", SqlDbType.VarChar, 50)
            prmadvenue.Value = avname
            myCommandADD.Parameters.Add(prmadvenue)

            Dim prmarepsond As New SqlParameter("@av_autorespond", SqlDbType.VarChar, 50)
            prmarepsond.Value = "No"
            myCommandADD.Parameters.Add(prmarepsond)

            Dim prmaphoto As New SqlParameter("@av_photo", SqlDbType.VarChar, 50)
            prmaphoto.Value = "No"
            myCommandADD.Parameters.Add(prmaphoto)

            Dim prmadcode As New SqlParameter("@av_key", SqlDbType.NVarChar, 255)
            prmadcode.Value = adcodeS
            myCommandADD.Parameters.Add(prmadcode)

            Dim prmadplaced As New SqlParameter("@av_adplaced", SqlDbType.VarChar, 50)
           	prmadplaced.Value = "Unpublished"
           	myCommandADD.Parameters.Add(prmadplaced)

            Dim prmonline As New SqlParameter("@av_online", SqlDbType.VarChar, 50)
            prmonline.Value = vonlineS
            myCommandADD.Parameters.Add(prmonline)

            Dim prmkeyurl As New SqlParameter("@av_keyurl", SqlDbType.NVarChar, 255)
            prmkeyurl.Value = System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "/intake.aspx?adcode=" & adcodeS
            myCommandADD.Parameters.Add(prmkeyurl)

            Dim prmAllText As New SqlParameter("@av_textAll", SqlDbType.Text)
            prmAllText.Value = adtextS
            'adtext.Text.Replace(vbCrLf, "<br>") & "<br><br>Please Click-> " & System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "/intake.aspx?adcode=" & adcode
            myCommandADD.Parameters.Add(prmAllText)

            Dim prmstat As New SqlParameter("@av_apstat", SqlDbType.VarChar, 50)
            prmstat.Value = "Master"
            myCommandADD.Parameters.Add(prmstat)

				Dim prmppid As New SqlParameter("@av_ppid", SqlDbType.int)
            prmppid.Value = ppidS
            myCommandADD.Parameters.Add(prmppid)

				Dim prmpostno As New SqlParameter("@av_postno", SqlDbType.VarChar, 50)
            prmpostno.Value = dbnull.value
            myCommandADD.Parameters.Add(prmpostno)

      		Dim prmapfrom As New SqlParameter("@av_APFrom", SqlDbType.DateTime)
            prmapfrom.Value = dbnull.value
            myCommandADD.Parameters.Add(prmapfrom)

            Dim prmapto As New SqlParameter("@av_APTo", SqlDbType.DateTime)
            prmapto.Value = dbnull.value
            myCommandADD.Parameters.Add(prmapto)

            Dim prmcnt As New SqlParameter("@av_APUnitCount", SqlDbType.Int)
            prmcnt.Value = dbnull.value
            myCommandADD.Parameters.Add(prmcnt)

            Dim prmautop As New SqlParameter("@av_autopost", SqlDbType.VarChar, 50)
            prmautop.Value = "No"
            myCommandADD.Parameters.Add(prmautop)          
           

            
            Try
                myConnectionADD.Open()
                myCommandADD.ExecuteNonQuery()
                myConnectionADD.Close()
            Catch SQLexc As SqlException
                Response.Write("Insert Failed. Error Details are: " & SQLexc.ToString())
            Finally
                myConnectionADD.Close()
            End Try
         
        end sub
        
         
          Sub ItemDataBoundEventHandlerPP(ByVal sender As Object, ByVal e As DataGridItemEventArgs)

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
   
   				'Dim myButton as Button = CType(e.Item.Cells(10).Controls(0), Button)
          		'myButton.CssClass = "frmbuttons"
          		
          		 'Check who steps are for
          		 Dim itemCellKey As TableCell = e.Item.Cells(1)
             	 Dim itemCellEDIT As TableCell = e.Item.Cells(10)                
					 Dim itemCellSelfPub As TableCell = e.Item.Cells(11) 
					 Dim itemCellTGUpdate As TableCell = e.Item.Cells(13)      
					 Dim itemCellPublished As TableCell = e.Item.Cells(5)
					 Dim itemCellPNO As TableCell = e.Item.Cells(16)
					 Dim itemCellPDate As TableCell = e.Item.Cells(17)
					 
					 Dim itemCellKeytext As String = itemCellKey.Text
                Dim itemCellEDITtext As String = itemCellEDIT.Text
                Dim itemCellPNOtext As String = itemCellPNO.Text
                Dim itemCellPDatetext As String = itemCellPDate.Text
                 
                Dim itemCellPublishedtext As String = itemCellPublished.Text
               
               Dim btnPublishPPDG As button
               btnPublishPPDG = e.Item.Cells(0).FindControl("PublishPP")	
               Dim btnUpdatePNDG As button
               btnUpdatePNDG = e.Item.Cells(0).FindControl("UpdatePN")
               
               Dim lblpnoDG As label
               lblpnoDG = e.Item.Cells(0).FindControl("lblPNO")
               lblpnoDG.text=itemCellPNO.text
               
               Dim lblPDDG As label
               lblPDDG = e.Item.Cells(0).FindControl("lblPdate")
               lblPDDG.text=itemCellPDatetext
               
                If itemCellPublishedtext = "Published" Then
                   itemCellEDIT.enabled=false
                   'itemCellSelfPub.enabled=false
                   btnPublishPPDG.visible=false
                   btnUpdatePNDG.visible=true
                   itemCellTGUpdate.enabled=false
                   
                Else
                  itemCellEDIT.enabled=true
                  'itemCellSelfPub.enabled=true
                   btnPublishPPDG.visible=true
                   btnUpdatePNDG.visible=false
                   itemCellTGUpdate.enabled=true
					 end if
					 
					 if session("UPTdate")= "true" then
					 
					  	if itemCellKeytext <> session("UPTdateValue") then
					  
					 		e.item.Visible = False
					 	end if
					 else
					 	e.item.Visible = true
					 end if
					 
					 
					 
					 
     			End If

        End Sub

 
         Sub MyDataGrid_PagePP(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
            'GetCheckBoxValues()

            ADVenuesPP.CurrentPageIndex = e.NewPageIndex
            'RePopulateCheckBoxes()

        End Sub
 '------------------------------------------------       
         Sub FillADS()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select *, cast(tbl_leadad_pk as varchar(255)) + ' ' + ad_title as 'Ftitle' from dbo.tbl_LeadADs where ad_userid='" & Session("userid") & "'"
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                dd_ADs.DataSource = dataReader
                dd_ADs.DataTextField = "Ftitle"
                dd_ADs.DataValueField = "tbl_leadad_pk"
                dd_ADs.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
            dd_ADs.Items.Insert(0, New ListItem("All", "9999"))
            dd_ADs.Items.Insert(1, New ListItem("Actives", "99998"))
            dd_ADs.Items.Insert(2, New ListItem("Inactives", "99997"))
        End Sub
        
         Sub FillPlanS(type as string)
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String 
           	if type = "All" then
            	myCommand= "Select *, cast(LAP_tbl_pk as varchar(255)) + ' ' + lap_name as 'Ftitle' from dbo.tbl_leadADPlans where lap_useridfk='" & Session("userid") & "'"
            else
            	myCommand= "Select *, cast(LAP_tbl_pk as varchar(255)) + ' ' + lap_name as 'Ftitle' from dbo.tbl_leadADPlans where lap_useridfk='" & Session("userid") & "' and lap_adfk='" & type & "'"
            end if
            
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                dd_ADPlan.DataSource = dataReader
                dd_ADPlan.DataTextField = "Ftitle"
                dd_ADPlan.DataValueField = "LAP_tbl_pk"
                dd_ADPlan.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
            dd_ADPlan.Items.Insert(0, New ListItem("All", "99999"))
            dd_ADPlan.Items.Insert(1, New ListItem("Actives", "99998"))
            dd_ADPlan.Items.Insert(2, New ListItem("Inactives", "99997"))
        End Sub
        
        
        
         Sub postadVen(ByVal sender As Object, ByVal e As EventArgs)
            'Get next ADKEY and prefix with Venue code

            'Dim adkeycode As String = Left(advenue.SelectedItem.Text, 2) + getadkey()
            'adkeycode = adkeycode + getadkey()
            ' Response.Write(advenue.SelectedItem.Value)
            If advenue.SelectedItem.Text <> "Select.." Then
                getvenueinfo()
                session("adkeycode") = session("adkeycode") + getadkey()
                'Response.Write(session("venueonline"))
                'insertadV(session("adkeycode"), session("venueonline"), "Master", DBNull.Value.ToString, DBNull.Value.ToString, DBNull.Value.ToString)
                'bindvenues()

                'pnlsavedv.Visible = True
                'advenue.SelectedIndex = advenue.Items.IndexOf(advenue.Items.FindByText("Select.."))
						
						'NEW
							pstsvenue.text=advenue.selecteditem.text
							pststatus.text="Unpublished"
							pstadkey.text= session("adkeycode")
							pnlLSdetail.visible=true
							pstEPdate.text=""
							pstadfrom.text=""
							pstadto.text=""
            End If

            'Response.Write(session("adkeycode"))

            'Response.Redirect("postad.aspx?adno=" & Session("adno") & "&venue=" & advenue.SelectedItem.Text & "&adtype=" & Session("adtype"))
        End Sub
        
          Sub addnewvenue (Source As System.Object, e As System.EventArgs)
			pnladdvenue.visible=true
			venuname.text=""
			venuecode.text=""
			venueurl.text=""
			privateven.checked=false
		end sub 
		
		 Sub ExitADV(ByVal sender As Object, ByVal e As EventArgs)
             pnlLSdetail.visible=false
        	pnladdvenue.visible=false
          
          pnladdvenueN.visible=false
          pnlpostings.visible=false
          pnlPPdetail.visible=true
          bindADVenuesPP()
        End Sub
        	
        	Sub savenewvenue (Source As System.Object, e As System.EventArgs)
			if checkifcodeexists()=false then
				
				Dim strConnection As String
	            Dim sqlConn As SqlConnection
	            Dim sqlCmd As SqlCommand
	            
	            Dim strSql As String 
	            Dim hasaccts as string
	            if acctsetup.checked then 
	               hasaccts = "Yes"
	            else
	             	hasaccts = "No"
	            end if
	            
	            
	            if privateven.checked then 
                    strSql = "insert into dbo.tbl_xwalk  (x_type,x_descr,x_id,x_url,x_UID,x_online,x_hasaccounts,x_accounturl,x_loginissue) values('leadsource','" & venuname.Text & "','" & venuecode.Text & "','" & venueurl.Text & "','" & Session("userid") & "','" & ddvenonline.SelectedItem.Text & "','" & hasaccts & "','http://" & venueurl.Text & "','No')"
	         	else
                    strSql = "insert into dbo.tbl_xwalk  (x_type,x_descr,x_company,x_id,x_url,x_UID,x_online,x_hasaccounts,x_accounturl,x_loginissue) values('leadsource','" & venuname.Text & "','All','" & venuecode.Text & "','" & venueurl.Text & "','" & Session("userid") & "','" & ddvenonline.SelectedItem.Text & "','" & hasaccts & "','" & venueurl.Text & "','No')"
	           
	           	end if
	           
	               Try
	       		  strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
	                sqlConn = New SqlConnection(strConnection)
	                sqlCmd = New SqlCommand(strSql, sqlConn)
	                sqlConn.Open()
	                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
	
	                If Sqldr.Read() Then
	                   
	                End If
	
	            Catch ex As Exception
	                Response.Write(ex.ToString())
	            Finally
	                sqlConn.Close()
	            End Try            
            	pnladdvenue.visible=false
            	FillADvenues()
            	
           else 
                venuname.Text = "EXISTS"
          end if
          
            
        End Sub
       


        Public Function checkifcodeexists() As Boolean

            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select * from tbl_xwalk where x_descr='" & venuname.Text & "' and x_type='leadsource' and (x_company='All' or x_UID='" & Session("userid") & "')"

            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try

        End Function
        
         Sub savenewvenueExit(ByVal Source As System.Object, ByVal e As System.EventArgs)
            pnladdvenue.Visible = False
        End Sub
        
         Sub MarkPublished(ByVal Source As System.Object, ByVal e As System.EventArgs)
        	 
        	 if pstadfrom.text= "" then
        	 	pstadfrom.text="Required!"
        	 	pstadfrom.backcolor=red
        	 else
		        	        	 
		        	 'insertadV(session("adkeycode"), session("venueonline"), "Master", DBNull.Value.ToString, DBNull.Value.ToString, DBNull.Value.ToString, "MP",session("CPlanId"))
		          pnlLSdetail.visible=false
		        	pnladdvenue.visible=false
		          
		          pnladdvenueN.visible=false
		          pnlpostings.visible=false
		          pnlPPdetail.visible=true
		          bindADVenuesPP()
		          
			         pstadfrom.backcolor=white
       		END IF
        end sub
        
        Sub PublishLAter(ByVal Source As System.Object, ByVal e As System.EventArgs)
         'insertadV(session("adkeycode"), session("venueonline"), "Master", DBNull.Value.ToString, DBNull.Value.ToString, DBNull.Value.ToString, "UP",session("CPlanId"))
        	pnlLSdetail.visible=false
        	pnladdvenue.visible=false
          
          pnladdvenueN.visible=false
          pnlpostings.visible=false
          pnlPPdetail.visible=true
          bindADVenuesPP()
        end sub
        
   Sub ModTGD(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim advenuePK As String = item.Cells(1).Text
           	session("UPTdate")= "true"
           	session("UPTdateControl")= "TargetDate"
           	session("UPTdateValue") = advenuePK
           	ADVenuesPP.Columns(6).Visible = true
        		ADVenuesPP.Columns(7).Visible = false
        		ADVenuesPP.Columns(10).Visible = false
        		ADVenuesPP.Columns(11).Visible = false
        		ADVenuesPP.Columns(13).Visible = false
        		ADVenuesPP.Columns(14).Visible = true
        		ADVenuesPP.Columns(15).Visible = true
        		bindADVenuesPP()
        		
        end sub
        
        Sub EditPNO(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim advenuePK As String = item.Cells(1).Text
           	session("UPTdate")= "true"
           	session("UPTdateControl")= "PostNo"
           	session("UPTdateValue") = advenuePK
           	Dim txtPNO2 As textbox
           	txtPNO2 = item.Cells(9).FindControl("txtPNO")
           	txtPNO2.visible=true
           	Dim lblPNO2 As label
           	lblPNO2 = item.Cells(9).FindControl("lblPNO")
           	lblPNO2.visible=false
           	ADVenuesPP.Columns(10).Visible = false
        		ADVenuesPP.Columns(11).Visible = false
        		ADVenuesPP.Columns(13).Visible = false
        		ADVenuesPP.Columns(14).Visible = true
        		ADVenuesPP.Columns(15).Visible = true
        	
        end sub
        
         Sub SetVPub(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim advenuePK As String = item.Cells(1).Text
           	session("UPTdate")= "true"
           	session("UPTdateControl")= "PubDate"
           	session("UPTdateValue") = advenuePK
           	Dim txtPD2 As textbox
           	txtPD2 = item.Cells(8).FindControl("txtPdate")
           	txtPD2.visible=true
           	Dim lblPD2 As label
           	lblPD2 = item.Cells(8).FindControl("lblPdate")
           	lblPD2.visible=false
           	Dim txtPNO2 As textbox
           	txtPNO2 = item.Cells(9).FindControl("txtPNO")
           	txtPNO2.visible=true
           	Dim lblPNO2 As label
           	lblPNO2 = item.Cells(9).FindControl("lblPNO")
           	lblPNO2.visible=false
           	ADVenuesPP.Columns(10).Visible = false
        		ADVenuesPP.Columns(11).Visible = false
        		ADVenuesPP.Columns(13).Visible = false
        		ADVenuesPP.Columns(14).Visible = true
        		ADVenuesPP.Columns(15).Visible = true
        		
        end sub
        
        
        
        Sub ModTGDS(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim advenuePK As String = item.Cells(1).Text
            if session("UPTdateControl")= "TargetDate" then
             	Dim txttrgdate As textbox
            	txttrgdate = item.Cells(6).FindControl("txtTPD")            	
	      		updatevTPD(advenuePK,txttrgdate.text,"")
	      	elseif session("UPTdateControl")= "PostNo" then
	      		Dim txtPNO2 As textbox
           		txtPNO2 = item.Cells(9).FindControl("txtPNO")           	
	      		updatevTPD(advenuePK,txtPNO2.text,"") 
           		txtPNO2.visible=false
           		Dim lblPNO2 As label
	           	lblPNO2 = item.Cells(9).FindControl("lblPNO")
	           	lblPNO2.visible=true
	      	elseif session("UPTdateControl")= "PubDate" then
	      		Dim txtPD2 As textbox
           		txtPD2 = item.Cells(8).FindControl("txtPdate")
           		txtPD2.visible=false
           		Dim lblPD2 As label
           		lblPD2 = item.Cells(8).FindControl("lblPdate")
           		lblPD2.visible=true
           		Dim txtPNO2 As textbox
           		txtPNO2 = item.Cells(9).FindControl("txtPNO")           	
	      		updatevTPD(advenuePK,txtPD2.text,txtPNO2.text) 
           		txtPNO2.visible=false
           		Dim lblPNO2 As label
	           	lblPNO2 = item.Cells(9).FindControl("lblPNO")
	           	lblPNO2.visible=true
           		
	      	
            end if
           	ADVenuesPP.Columns(6).Visible = false
        		ADVenuesPP.Columns(7).Visible = true
        		ADVenuesPP.Columns(10).Visible = true
        		ADVenuesPP.Columns(11).Visible = true
        		ADVenuesPP.Columns(13).Visible = true
        		ADVenuesPP.Columns(14).Visible = false
        		ADVenuesPP.Columns(15).Visible = false
           	session("UPTdate")= "false"
           	session("UPTdateValue") = advenuePK
           	session("UPTdateControl")= ""
        		bindADVenuesPP()
        		
        end sub
        
         Sub ModTGDSC(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim advenuePK As String = item.Cells(1).Text
            session("UPTdate")= "false"
           	session("UPTdateValue") = advenuePK
           	Dim txtPNO2 As textbox
           	txtPNO2 = item.Cells(9).FindControl("txtPNO")
           	txtPNO2.visible=false
           	Dim lblPNO2 As label
           	lblPNO2 = item.Cells(9).FindControl("lblPNO")
           	lblPNO2.visible=true
           	ADVenuesPP.Columns(6).Visible = false
        		ADVenuesPP.Columns(7).Visible = true
        		ADVenuesPP.Columns(10).Visible = true
        		ADVenuesPP.Columns(11).Visible = true
        		ADVenuesPP.Columns(13).Visible = true
        		ADVenuesPP.Columns(14).Visible = false
        		ADVenuesPP.Columns(15).Visible = false
      		bindADVenuesPP()
      		session("UPTdateControl")= ""
        		
        end sub
        
        sub updatevTPD(id as string, tdate as string,tdate2 as string)
        
          'Read in the values of the updated row
          
							
            Dim strSql As String 
            if session("UPTdateControl")= "TargetDate" then
           		strSql = "update tbl_LeadADVenues set av_APTo='" & tdate & "' where tbl_leadadvenues='" & id & "'"
       		elseif session("UPTdateControl")= "PostNo" then
       			strSql = "update tbl_LeadADVenues set av_Postingno='" & tdate & "' where tbl_leadadvenues='" & id & "'"
       		elseif session("UPTdateControl")= "PubDate" then
       			strSql = "update tbl_LeadADVenues set av_APFrom='" & tdate & "', av_Postingno='" & tdate2 & "', av_adplaced='Published' where tbl_leadadvenues='" & id & "'"
       	
       		end if
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
           
            
        
        end sub
       
       	public function checkforadkey(id as string) as boolean
			   Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select *  from dbo.tbl_LeadADs " _
            					& "where ad_text like '%%ADKEY%%' " _
            					& "and tbl_leadad_pk = '" & request.querystring("adno") & "' and ad_userid='" & session("userid") & "'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    
                        Return True
                    Else
                        Return False
                  

                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try

			
			
			end function	

       Public Sub getvenueinfo()

            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select * from tbl_xwalk where tbl_xwalk_pk='" & advenue.SelectedItem.Value & "'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    If Sqldr("x_online") IsNot DBNull.Value Then
                        session("venueonline") = Sqldr("x_online")
                    Else
                        session("venueonline") = "No"
                    End If
                    If Sqldr("x_id") IsNot DBNull.Value Then
                        session("adkeycode") = Sqldr("x_id")
                    Else
                        session("adkeycode") = Left(advenue.SelectedItem.Text, 2)
                    End If
                    If Sqldr("x_html") IsNot DBNull.Value Then
                        session("venhtml") = Sqldr("x_html")
                    Else
                        session("venhtml") = "No"
                    End If

                End If


            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try


        End Sub
        
           Public Function getadkey() As String
            Dim keycode As String
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select cast (AK_NextKey as nvarchar(255)) as 'kc'  from dbo.tbl_nextadkey"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    keycode = Sqldr("kc")
                    return Sqldr("kc")
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try

            strSql = "update dbo.tbl_nextadkey set AK_NextKey =  AK_NextKey+1 "
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then

                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
            Return keycode
        End Function
        
         Sub insertadV(ByVal adcode As String, ByVal vonline As String, ByVal stat As String, ByVal d1 As String, ByVal d2 As String, ByVal tot As String, adplaced as string, PPID as string)
           
        End Sub
        
        Sub pubad(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim advenuePK As String = item.Cells(1).Text
            Dim adcode As String = item.Cells(12).Text
            Dim advenue As String = item.Cells(2).Text
            'Dim adurl As String = item.Cells(25).Text
            'adurl = "http://" & adurl
            'Response.Redirect("postad.aspx?adno=" & adcode & "&venue=" & advenue & "&venueno=" & advenuePK & "&type=" & Request.QueryString("type"))
            ' If chkslfpub(advenue) Then
            if checkforadkey(advenuePK) then
            	Response.Redirect("postad.aspx?adno=" & adcode & "&venue=" & advenue & "&venueno=" & advenuePK & "&type=" & Request.QueryString("type") & "&modify=" & chkcanmod(adcode)& "&pplan=" & session("CPlanId"))
            else
            	adtext.content = adtext.content + "<br ><mycustomtag class='myclass' contenteditable='false'>%ADKEY%</mycustomtag>"
               'insertad()
               Response.Redirect("postad.aspx?adno=" & adcode & "&venue=" & advenue & "&venueno=" & advenuePK & "&type=" & Request.QueryString("type") & "&modify=" & chkcanmod(adcode)& "&pplan=" & session("CPlanId"))
       
            end if
            'Else
            'Response.Write("<script>window.open" & _
            ' "('" & adurl & "','_new', 'width=800,height=500');</script>")

            'End If

        End Sub
        
         Public Function chkcanmod(id as string) As string
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select distinct av_adplaced from dbo.tbl_LeadADs " _
							& "join dbo.tbl_LeadADVenues on av_leadads_FK = tbl_leadad_pk " _
							& "where tbl_leadad_pk =" & id & " and av_adplaced='Published'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    Return "No"
                else
                	  Return "Yes"
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try

        End Function
        
         Sub PublishNow(ByVal Source As System.Object, ByVal e As System.EventArgs)
        
        end sub
        
        Sub filterVenuesA(ByVal Source As System.Object, ByVal e As System.EventArgs)
         dim y as textbox = Source
           if y.ID = "Pl_search" then
           		if Pl_search.text.length > 0 then
           			session("PubSearchF")="true"
           		else
           			session("PubSearchF")="false"
           		end if
           	end if
           	 
           ADVenuesPP.CurrentPageIndex = 0
           bindADVenuesPP()

        end sub
        
         Sub filterVenues(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Dim x As dropdownlist = Source
             if x.ID = "dd_PStat" then
             	if dd_PStat.selecteditem.text = "All" then
             		session("PubStatF")="false"
             	else
             		session("PubStatF")="true"
             	end if
             elseif x.ID = "dd_ADs" then
             	if dd_ADs.selecteditem.text = "All" then
             		session("PubADSF")="false"
             	else
             		session("PubADSF")="true"
             		if dd_ADs.selecteditem.text = "Actives" or dd_ADs.selecteditem.text = "Inactives" then
             		else
             			FillPlanS(dd_ADs.selecteditem.value)
             		end if
             	end if
             elseif x.ID = "dd_ADPlan" then
             	if dd_ADPlan.selecteditem.text = "All" then
             		session("PubADPlanF")="false"
             	else
             		session("PubADPlanF")="true"
             	end if
             elseif x.ID = "dd_PTDue" then
             	if dd_PTDue.selecteditem.text = "All" then
             		session("PubTargetDateF")="false"
             	else
             		session("PubTargetDateF")="true"
             	end if
             
             end if
           	
           
           ADVenuesPP.CurrentPageIndex = 0
           bindADVenuesPP()

        End Sub
        
        Sub deleteposts(ByVal Source As System.Object, ByVal e As System.EventArgs)
     	
     					'response.write(session("CPlanId"))
			        		Dim strUID As String = Session("userid")
			           	 Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
			            Dim mycommand As String
			            mycommand = "Select tmpad_adno from tbl_tmpad " _
									  	& "where tmpad_uid='" & Session("userid") & "'"
			            Dim ad As New SqlDataAdapter(mycommand, myConnection)
			            Dim ds As New DataSet()
			            Dim str As New StringBuilder()
			
			            Try
			                ad.Fill(ds)
			                'ds.Tables(0).TableName = "bill"
			            Catch exc As System.Exception
			                Response.Write(exc.ToString())
			            Finally
			                myConnection.Dispose()
			            End Try           
			           
			            'response.write(ds.Tables(0).Rows.Count - 1)
			            Dim i As Integer
			            For i = 0 To ds.Tables(0).Rows.Count - 1
			                'Response.Write(ds.Tables(0).Rows(i)(1).ToString())
			                killtheposts(ds.Tables(0).Rows(i)(0).ToString())
			                removefromlist(ds.Tables(0).Rows(i)(0).ToString())
			            Next
			            bindADVenuesPP()
     	
     	
     	
     		end sub
     		
     		sub killtheposts(id as string)		
	        	Dim strUID As String = Session("userid")
            Dim strSql As String
               strSql = "delete from dbo.tbl_LeadADVenues where tbl_leadadvenues='" & id & "' and av_adplaced='Unpublished'"
            

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
	         bindADVenuesPP()
	      		
			
			            
        end sub
        
   End Class
End Namespace