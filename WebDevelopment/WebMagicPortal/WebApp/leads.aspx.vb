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
imports System.io


namespace PageTemplate
	public class leads 
	   inherits PageTemplate

        Public FileName, FileContent, FileSize, UploadDetails, MyFile,totleads, span1, span2
        Public strstatusFilter As String = "ld_status<>''"
        Public strleadFilter As String = "ld_type<>''"
        Public strcstatusFilter As String = "ld_pstatus<>''"
        Public strassignedtoFilter As String = "ld_agent<>''"
        
        Protected WithEvents lead_status, delleads As System.Web.UI.WebControls.DataGrid
        Protected ddlstatusFilter, dd_esource,ddlleadstateupload As System.Web.UI.WebControls.DropDownList
        Protected ddlleadprogramFilter,ddlleadtypeFilter, ddlMarketFilter,ddlcstatusFilter, ddlassignedtoFilter, ddlassignedbyFilter, ddlleadtypeupload, ddadcode As System.Web.UI.WebControls.DropDownList
        Protected WithEvents lblstatus, lblPageCount, lblviewtype, lbladcode, totalleads,lblsplittext As System.Web.UI.WebControls.Label
  			public l_search,l_searchldnum,l_aging As System.Web.UI.WebControls.textbox
        Public btnreferals, btnupload, btnrefresh,btndeltedleads,btnreports,btnbacktoworkflow As Button
        Public pnlupload, pnlleads, pnlaging, pnluploadresult, pnldelleads As Panel
  			public searchtype as string
  			public lblwfleadadd as label
       
        Public Nextbutton, Lastbutton, Prevbutton, Firstbutton As LinkButton
  	
        Public btnnocontact, btnaging, btnpropmatch, btnexport,btnAddlead As Button
        public dd_leadtype,dd_ldstat,dd_leadprogram,dd_source as dropdownlist
        
       
	   private Sub Page_Load(byval sender as object, byval e As EventArgs) handles mybase.load
            If Not (Page.IsPostBack) Then
            	clearsessions()
                'Set Vars
                If Request.QueryString("source") = "nav" Then
                    Session("pgindex") = 0
                End If
                searchtype = ""
                Session("state") = ""
                deletetempq()
                Session("fullscreen") = "No"
                'Set Buttons
                btnrefresh.Visible = False
               
                If Session("company") = "Choice One" And Session("role") = "Administrator" Then
                    'btnnocontact.Visible = True
                    'btnaging.Visible = True
                    'btnpropmatch.Visible = True
                Else
                    btnnocontact.Visible = False
                    btnaging.Visible = False
                    btnpropmatch.Visible = False
                End If
                If Session("role") = "Administrator" Or Session("role") = "GOD" Then
                    btnupload.Visible = True
                Else
                    btnupload.Visible = true
                End If
               

                'Set DataGrid
                lead_status.Columns(8).Visible = False
                If Session("nocontactrpt") = "True" Then
                    lblviewtype.Visible = True
                    lblviewtype.Text = "Filter: No Contact"
                    lblviewtype.ForeColor = System.Drawing.Color.Red
                    btnnocontact.Text = "All"
                    'lead_status.Columns(12).Visible = false
                ElseIf Session("aging") = "True" Then
                    lblviewtype.Visible = True
                    lblviewtype.Text = "Filter: Aging"
                    lblviewtype.ForeColor = System.Drawing.Color.Red
                    'lead_status.Columns(12).Visible = true
                Else
                    'lead_status.Columns(12).Visible = false
                End If

                'Set Panels
                pnlaging.Visible = False
                pnlleads.Visible = True
                pnlupload.Visible = False
                pnluploadresult.Visible = False

                'Fill Drop Downs
                FillstatusDropDown()
                FillLeadTypeDropDown()
                FillmktTypeDropDown()
                FillLeadprogramDropDown()
                FillcontactstatusDropDown()
                FillassignedtoDropDown()
                FillassignedbyDropDown()
                filladcode()

                'Other
                checkquerystring()
                lead_status.CurrentPageIndex = Convert.ToInt32(Session("pgindex"))

                BindGrid()
                btnreferals.visible=false
					 if request.querystring("source")="workflow" then
					 	btnAddlead.visible=false
					 	btnupload.visible=false
					 	btnexport.visible=false
					 	btndeltedleads.visible=false
					 	btnrefresh.visible=false
					 	btnreferals.visible=false
					 	btnnocontact.visible=false
					 	btnaging.visible=false
					 	btnreports.visible=false
					 	btnpropmatch.visible=false
						lead_status.Columns(13).Visible = true	
						btnbacktoworkflow.visible=true				 	
					 end if
            End If

            pagesetup()
        End Sub
        sub clearsessions()
        		session("uplname")=""
        		session("upfname")=""
        		session("upadd1")=""
        		session("upadd2")=""
        		session("upcity")=""
        		session("upstate")=""
        		session("upzip")=""
        		session("upphone1")=""
        		session("upphone2")=""
        		session("upemail1")=""
        		session("upemail2")=""
        		session("upnotes")=""
        		session("newleadno")=""
        		session("ResultCount")=0
        		session("filterrowcount")=0
        		session("querystring")=""
        		session("upfax")=""
        		session("upfile")=""
        		session("enteredbyname")=""
        		session("agingdays")=""
        		session("pubtblleadpk")=""
        		
  		
  		end sub
        Public Sub deletetempq()
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "delete from tbl_exporttemp where tmpq_userid='" & Session("userid") & "'"

            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
        End Sub
	   
	   Sub MyDataGrid_Page(sender As Object, e As DataGridPageChangedEventArgs) 
			lead_status.CurrentPageIndex = e.NewPageIndex 
			BindGrid() 
		End Sub 

		Sub refresh (Source As System.Object, e As System.EventArgs)
	   	 response.redirect(session("qstring"))
	  	End Sub

		Sub PagerButtonClick(sender As Object, e As EventArgs)
  			'used by external paging UI
  			Dim arg As String = sender.CommandArgument

  			Select arg
		     Case "next":  'The next Button was Clicked
		        If (lead_status.CurrentPageIndex < (lead_status.PageCount - 1)) Then
		            lead_status.CurrentPageIndex += 1
		        End If 
		
		     Case "prev":   'The prev button was clicked
		         If (lead_status.CurrentPageIndex > 0) Then
		             lead_status.CurrentPageIndex -= 1
		         End If
		
		     Case "last":   'The Last Page button was clicked
		         lead_status.CurrentPageIndex = (lead_status.PageCount - 1)
		
		     Case Else:     'The First Page button was clicked
		         lead_status.CurrentPageIndex = Convert.ToInt32(arg)
			End Select

    		'Now, bind the data!
   		session("pgindex")=lead_status.CurrentPageIndex
   		BindGrid()
		End Sub

		Sub Prev_Buttons()
	  		Dim PrevSet As String
	
	  		If lead_status.CurrentPageIndex+1 <> 1 and session("ResultCount") <> -1 Then
			   PrevSet = lead_status.PageSize
			   PrevButton.Text = ("< Prev " & PrevSet)
		
	    		If lead_status.CurrentPageIndex+1 = lead_status.PageCount Then
	     			FirstButton.Text = ("<< 1st Page")
	    		End If
	  		End If
		End Sub

		Sub Next_Buttons()
		  Dim NextSet As String
		
		  If lead_status.CurrentPageIndex+1 < lead_status.PageCount Then
		    NextSet = lead_status.PageSize
		    NextButton.Text = ("Next " & NextSet & " >")
		   
		  End If
		
		  If lead_status.CurrentPageIndex+1 = lead_status.PageCount-1 Then
		    Dim EndCount As Integer = session("filterrowcount") - (lead_status.PageSize * (lead_status.CurrentPageIndex+1))
		    NextButton.Text = ("Next " & EndCount & " >")
		 	
		  End If
		End Sub

		Sub change (Source As System.Object, e As System.EventArgs)
			response.write("HEEJ")
		end sub

		Sub referals (Source As System.Object, e As System.EventArgs)
			 response.redirect("referals.aspx")
	  	End Sub
	   
        Sub BindGrid()

            Dim strUID As String = Session("userid")
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            Dim RcdCount As Integer
            if request.querystring("source")<>"workflow" then
		             If Session("role") = "Administrator" Or Session("role") = "GOD" Then
		                mycommand = "select distinct ld_marketingprg,ld_hphone,ld_Cphone,ld_email,ld_pstatus,ld_entrysource,ld_adcode,ld_fname,ld_lname, " _
		                & "cast (ld_notes as varchar(900)) as 'ld_notes', " _
		                & "ld_adcode,ld_fname,ld_lname,ld_status,ld_type,ld_pstatus,ld_agent, " _
							 & "ld_assignedbyuid,ld_entrysource,ld_program,tbl_leadad_pk, " _
		                & "cast(tbl_leads_pk as varchar(20)) as 'leadnos', cast (av_leadads_FK as varchar(20)) as 'ADNO', " _
		                & "cast(tbl_leads_pk as varchar(20)) as 'leadpk', tbl_leads_pk, '' as 'nodays', convert(varchar(20), " _
		                & "ld_adddate,101) as ld_adddatef, fname + ' ' + lname as assignedby, " _
		                & "case when (ld_lname is null) then ld_fname else ld_fname + ' ' + ld_lname end as 'Contact', " _
		                & "case when (select distinct cnt_followup from dbo.tbl_leadscontacthistory where tbl_leads_fk = tbl_leads_pk " _
		                & "and cnt_followup='Yes') = 'Yes' then 'Yes' else 'No' end as 'Followup' from dbo.tbl_leads left " _
		                & "join dbo.tbl_users on Uid=ld_assignedbyuid left join tbl_LeadADVenues on av_key=ld_adcode " _
		                & "left join tbl_LeadADs  on tbl_leadad_pk =av_leadads_FK where company_pk='" & Session("company_pk") & "' order by tbl_leads_pk desc"
		            Else
		               mycommand = "select distinct ld_marketingprg,ld_hphone,ld_Cphone,ld_email,ld_pstatus,ld_entrysource,ld_adcode,ld_fname,ld_lname, " _
		                & "cast (ld_notes as varchar(900)) as 'ld_notes', " _
		                & "ld_adcode,ld_fname,ld_lname,ld_status,ld_type,ld_pstatus,ld_agent, " _
							 & "ld_assignedbyuid,ld_entrysource,ld_program,tbl_leadad_pk, " _
							 & "cast(tbl_leads_pk as varchar(20)) as 'leadnos', cast (av_leadads_FK as varchar(20)) as 'ADNO', " _
		                & "cast(tbl_leads_pk as varchar(20)) as 'leadpk', tbl_leads_pk, '' as 'nodays', convert(varchar(20), " _
		                & "ld_adddate,101) as ld_adddatef, fname + ' ' + lname as assignedby, " _
		                & "case when (ld_lname is null) then ld_fname else ld_fname + ' ' + ld_lname end as 'Contact', " _
		                & "case when (select distinct cnt_followup from dbo.tbl_leadscontacthistory where tbl_leads_fk = tbl_leads_pk " _
		                & "and cnt_followup='Yes') = 'Yes' then 'Yes' else 'No' end as 'Followup' from dbo.tbl_leads left " _
		                & "join dbo.tbl_users on Uid=ld_assignedbyuid left join tbl_LeadADVenues on av_key=ld_adcode " _
		                & "left join tbl_LeadADs  on tbl_leadad_pk =av_leadads_FK " _
		                & "left join tbl_Usr2UsrRoles on tuid = '" & strUID & "' " _ 
		                & "Where ((ld_assignedtouid='" & strUID & "' or ld_assignedbyuid='" & strUID & "') and " _
		                & "(ld_status='Unaccepted' or ld_status='Accepted' or ld_status='Closed')) or " _
		                & "((ld_assignedbyuid='" & strUID & "') and ld_status='Draft') " _
		                & "or (ld_assignedtouid=suid and LeadView='True' or ld_assignedtouid=suid and LeadEdit='True') " _
		                & "order by tbl_leads_pk desc"
		               
		            End If
		      else
		      
		      	If Session("role") = "Administrator" Or Session("role") = "GOD" Then
		                mycommand = "select distinct ld_marketingprg,ld_hphone,ld_Cphone,ld_email,ld_pstatus,ld_entrysource,ld_adcode,ld_fname,ld_lname, " _
		                & "cast (ld_notes as varchar(900)) as 'ld_notes', " _
		                & "ld_adcode,ld_fname,ld_lname,ld_status,ld_type,ld_pstatus,ld_agent, " _
							 & "ld_assignedbyuid,ld_entrysource,ld_program,tbl_leadad_pk, " _
		                & "cast(tbl_leads_pk as varchar(20)) as 'leadnos', cast (av_leadads_FK as varchar(20)) as 'ADNO', " _
		                & "cast(tbl_leads_pk as varchar(20)) as 'leadpk', tbl_leads_pk, '' as 'nodays', convert(varchar(20), " _
		                & "ld_adddate,101) as ld_adddatef, fname + ' ' + lname as assignedby, " _
		                & "case when (ld_lname is null) then ld_fname else ld_fname + ' ' + ld_lname end as 'Contact', " _
		                & "case when (select distinct cnt_followup from dbo.tbl_leadscontacthistory where tbl_leads_fk = tbl_leads_pk " _
		                & "and cnt_followup='Yes') = 'Yes' then 'Yes' else 'No' end as 'Followup' from dbo.tbl_leads left " _
		                & "join dbo.tbl_users on Uid=ld_assignedbyuid left join tbl_LeadADVenues on av_key=ld_adcode " _
		                & "left join tbl_leadWorkFlowsStatus on lwfs_lead_fk = tbl_leads_pk " _
		                & "left join tbl_LeadADs  on tbl_leadad_pk =av_leadads_FK where company_pk='" & Session("company_pk") & "' and lwfs_tbl_pk is null order by tbl_leads_pk desc"
		            Else
		               mycommand = "select distinct ld_marketingprg,ld_hphone,ld_Cphone,ld_email,ld_pstatus,ld_entrysource,ld_adcode,ld_fname,ld_lname, " _
		                & "cast (ld_notes as varchar(900)) as 'ld_notes', " _
		                & "ld_adcode,ld_fname,ld_lname,ld_status,ld_type,ld_pstatus,ld_agent, " _
							 & "ld_assignedbyuid,ld_entrysource,ld_program,tbl_leadad_pk, " _
							 & "cast(tbl_leads_pk as varchar(20)) as 'leadnos', cast (av_leadads_FK as varchar(20)) as 'ADNO', " _
		                & "cast(tbl_leads_pk as varchar(20)) as 'leadpk', tbl_leads_pk, '' as 'nodays', convert(varchar(20), " _
		                & "ld_adddate,101) as ld_adddatef, fname + ' ' + lname as assignedby, " _
		                & "case when (ld_lname is null) then ld_fname else ld_fname + ' ' + ld_lname end as 'Contact', " _
		                & "case when (select distinct cnt_followup from dbo.tbl_leadscontacthistory where tbl_leads_fk = tbl_leads_pk " _
		                & "and cnt_followup='Yes') = 'Yes' then 'Yes' else 'No' end as 'Followup' from dbo.tbl_leads left " _
		                & "join dbo.tbl_users on Uid=ld_assignedbyuid left join tbl_LeadADVenues on av_key=ld_adcode " _
		                & "left join tbl_leadWorkFlowsStatus on lwfs_lead_fk = tbl_leads_pk " _
		                & "left join tbl_LeadADs  on tbl_leadad_pk =av_leadads_FK " _
		                & "left join tbl_Usr2UsrRoles on tuid = '" & strUID & "' " _
		                & "Where ((ld_assignedtouid='" & strUID & "' or ld_assignedbyuid='" & strUID & "') and " _
		                & "(ld_status='Unaccepted' or ld_status='Accepted' or ld_status='Closed')) or " _
		                & "((ld_assignedbyuid='" & strUID & "') and ld_status='Draft')  " _
		                & "or (ld_assignedtouid=suid and LeadView='True' or ld_assignedtouid=suid and LeadEdit='True') " _
		                & "and lwfs_tbl_pk is null order by tbl_leads_pk desc"
		               
		            End If
		      
		      
		      
		      end if
		      

            Dim i As Integer
            i = 0
            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_leads")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_leads"))
                dvProducts.RowFilter = "(leadnos like '%" & Request.QueryString("search") & "%' or ld_fname + ' ' + ld_lname like '%" & Request.QueryString("search") & "%' or ld_lname LIKE '%" & Request.QueryString("search") & "%' or ld_fname LIKE '%" & Request.QueryString("search") & "%' or ld_email LIKE '%" & Request.QueryString("search") & "%' or ld_hphone LIKE '%" & Request.QueryString("search") & "%' or ld_cphone LIKE '%" & Request.QueryString("search") & "%' or ld_notes LIKE '%" & Request.QueryString("search") & "%') "
                'and ld_status like '" & Request.QueryString("status") & "' and ld_type like '" & Request.QueryString("leadtype") & "' and ld_pstatus like '" & Request.QueryString("constatus") & 
                '"' and ld_agent like '" & Request.QueryString("assignedto") & "' and ld_assignedbyuid like '" & session("enteredbyname") & "' and ADNO like '" & Request.QueryString("adcode") & "' and ld_entrysource like '" & Request.QueryString("entrysource") & "'"
                If Request.QueryString("status") <> "*" Then
                    dvProducts.RowFilter = dvProducts.RowFilter + " and ld_status like '" & Request.QueryString("status") & "'"
                End If
                If Request.QueryString("leadtype") <> "*" Then
                    dvProducts.RowFilter = dvProducts.RowFilter + " and ld_type like '" & Request.QueryString("leadtype") & "'"
                End If
                If Request.QueryString("constatus") <> "*" Then
                    dvProducts.RowFilter = dvProducts.RowFilter + " and ld_pstatus like '" & Request.QueryString("constatus") & "'"
                End If
                If Request.QueryString("assignedto") <> "*" Then
                    dvProducts.RowFilter = dvProducts.RowFilter + " and ld_agent like '" & Request.QueryString("assignedto") & "'"
                End If
                If Request.QueryString("assignedby") <> "*" Then
                    dvProducts.RowFilter = dvProducts.RowFilter + " and ld_assignedbyuid like '" & session("enteredbyname") & "'"
                End If
                If Request.QueryString("adcode") <> "*" Then
                    dvProducts.RowFilter = dvProducts.RowFilter + " and ADNO like '" & Request.QueryString("adcode") & "'"
                End If
                If Request.QueryString("entrysource") <> "*" Then
                    dvProducts.RowFilter = dvProducts.RowFilter + " and ld_entrysource like '" & Request.QueryString("entrysource") & "'"
                End If
                If Request.QueryString("program") <> "*" Then
                    dvProducts.RowFilter = dvProducts.RowFilter + " and ld_program like '" & Request.QueryString("program") & "'"
                End If
                 If Request.QueryString("mprogram") <> "*" Then
                    dvProducts.RowFilter = dvProducts.RowFilter + " and ld_marketingprg like '" & Request.QueryString("mprogram") & "'"
                End If
                session("filterrowcount") = dvProducts.Count
                totalleads.Text = dvProducts.Count()
                lead_status.DataSource = dvProducts
                lead_status.DataBind()
                RcdCount = dataSet.Tables("tbl_leads").Rows.Count.ToString()

                session("ResultCount") = RcdCount

            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

            If lead_status.CurrentPageIndex <> 0 Then
                Call Prev_Buttons()
                Firstbutton.Visible = True
                Prevbutton.Visible = True
            Else
                Firstbutton.Visible = False
                Prevbutton.Visible = False
            End If

            If lead_status.CurrentPageIndex <> (lead_status.PageCount - 1) Then
                Call Next_Buttons()
                Nextbutton.Visible = True
                Lastbutton.Visible = True
            Else
                Nextbutton.Visible = False
                Lastbutton.Visible = False
            End If

            lblPageCount.Text = "Page " & lead_status.CurrentPageIndex + 1 & " of " & lead_status.PageCount

        End Sub
                 
		
	   Sub newlead(sender As Object, e As EventArgs)
     			response.redirect("addlead.aspx?action=new")
     	End Sub
			 	
	 	Sub aginggo(sender As Object, e As EventArgs)
     			session("aging")="True"
				lblviewtype.visible=true
				lblviewtype.text = "Filter: Aging"
				lblviewtype.ForeColor = System.Drawing.Color.Red
		
				session("agingdays") = l_aging.text
				
			bindgrid()
            'lead_status.Columns(12).Visible = true
        End Sub
		
		
		Sub uploadleads(sender As Object, e As EventArgs)
     		if pnlupload.visible=true then
     			pnlupload.visible=false
     			pnlleads.visible=true
     		else
     			pnlupload.visible=true
     			pnlleads.visible=false
     			'bindddlleadtypeupload()
     			'bindddlleadstatusupload()
     			bindsource()          
            FillLeadProgram()
            FillLeadtype()
            FillLeadstatus()
     		end if
        End Sub
        
        Sub FillLeadtype()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='Leadtype' and (x_company='All' or x_uid='" & Session("userid") & "')"
            'x_company='" & Session("company") & "' or 
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                dd_leadtype.DataSource = dataReader
                dd_leadtype.DataTextField = "x_descr"
                dd_leadtype.DataValueField = "tbl_xwalk_pk"
                dd_leadtype.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
        End Sub

        Sub FillLeadstatus()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='contactstatus' and (x_company='All' or x_uid='" & Session("userid") & "')"
            'x_company='" & Session("company") & "' or 
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                dd_ldstat.DataSource = dataReader
                dd_ldstat.DataTextField = "x_descr"
                dd_ldstat.DataValueField = "tbl_xwalk_pk"
                dd_ldstat.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

        End Sub
        
         Sub FillLeadProgram()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='LeadProgram' and ( x_company='All' or x_uid='" & Session("userid") & "')"
            'x_company='" & Session("company") & "' or
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                dd_leadprogram.DataSource = dataReader
                dd_leadprogram.DataTextField = "x_descr"
                dd_leadprogram.DataValueField = "tbl_xwalk_pk"
                dd_leadprogram.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
        End Sub
        
        Sub bindsource()

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='leadsource' and (x_company='All' or x_uid='" & Session("userid") & "')", myConnection)
            'x_company='" & Session("company") & "' or 
            Try
                myConnection.Open()
                dd_source.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
                dd_source.DataBind()
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            End Try

            dd_source.Items.Insert(0, New ListItem("File Upload", "9999"))
				dd_source.enabled=false
        End Sub
        
        Sub exportleads(ByVal sender As Object, ByVal e As EventArgs)

            Response.Redirect(Session("qstringEXP"))
        End Sub
     	
     	Sub continue_Click(sender As Object, e As EventArgs)
     		pnlupload.visible=false
     		pnluploadresult.visible=false
     		pnlleads.visible=true
     		bindgrid()
	      
     	end sub 
     	
     	Sub Upload_Click(sender As Object, e As EventArgs)
     		pnluploadresult.visible=true
     		pnlupload.visible=false
     		Dim MyPath, MyName as string
  			MyPath = "\\c1-plankton\leadsystem"      
    		MyName = Dir(MyPath, vbDirectory)   			
         'UploadDetails.visible = True
         
         Dim strFileName as string = MyFile.PostedFile.FileName
        	Dim c as string = System.IO.Path.GetFileName(strFileName) 
        	session("upfile") = MyPath + "\" + c
         	Try 
	      		 MyFile.PostedFile.SaveAs("\\c1-plankton\leadsystem\" + c)
                      
  
                catch Exp as exception
                  span1.InnerHtml = "An Error occured. Please check the attached  file"
                  
                        UploadDetails.visible = false
                       span2.visible=false
                End Try
			
			
		     	Dim filetoread as string = session("upfile")
		      dim filestream as StreamReader
		   	filestream = File.Opentext(filetoread)		   	
		   	Dim readcontents as String		   	
		   	readcontents = fileStream.ReadToEnd()
		   	
		   	dim eof as boolean = false
		   	dim crfeed as string  = vbcrlf
			   dim crline = Split(readcontents,crfeed)
		   	dim x as integer 
		   
				   	for x = 0 to ubound(crline)
				   		
					   		'lblsplittext.Text &= "<b>Split </b>" & x+1 & ")   " & crline(x)& "<br>"
							   Dim textdelimiter as String = ","
							   Dim splitout = Split(crline(x),textdelimiter)
							   dim i as integer
							   for i=0 to Ubound(splitout)
							  		 if i= 0 then
							  		 	session("uplname") = splitout(i)
							  		 elseif i=1 then
							  		 	session("upfname") = splitout(i)
							  		 elseif i=2 then
							  		 	session("upadd1") = splitout(i)
							  		 elseif i=3 then
							  		 session("upcity") = splitout(i)
							  		 elseif i=4 then
							  		 	session("upstate") = splitout(i)
							  		 elseif i=5 then
							  		 	session("upzip") = splitout(i)
							  		 elseif i=6 then
							  		 	session("upphone1") = splitout(i)
							  		 elseif i=7 then
							  		 	session("upphone2") = splitout(i)
							  		 elseif i=8 then
							  		 		session("upfax") = splitout(i)
							  		 elseif i=9 then
							  		 session("upemail1") = splitout(i)
							  		 elseif i=10 then
							  		 	session("upemail2") = splitout(i)
							  		 elseif i=11 then
							  		 	if (splitout(i) is nothing or splitout(i).Length = 0) then
							  		 		session("upnotes") = "	"
							  		 	else
							  		 		session("upnotes") = splitout(i)
							  		 	end if
							  		 end if 
							  		'lblsplittext.Text &= "<b>Split </b>" & i & ")   " & splitout(i)& "<br>"
							  	next		
							  	if (Ubound(splitout) = 0) then
							  		eof = true
							  	else
								  	getnewleadno()
								  	insertLeaddb()
								  	eof = false
								end if								
			
					next
			
		   filestream.Close()  
     		FileName.InnerHtml = MyFile.PostedFile.FileName
         FileContent.InnerHtml = MyFile.PostedFile.ContentType 
         FileSize.InnerHtml = MyFile.PostedFile.ContentLength
     		if eof=true then
     			x=x-1     		     			
     		end if
     		totleads.InnerHtml = x.tostring
     	end sub
     	
     	
     	
     	
		Sub Upload_Click_old(sender As Object, e As EventArgs)
     		pnluploadresult.visible=true
     		pnlupload.visible=false
     		Dim MyPath, MyName as string
  			MyPath = "\\c1-plankton\leadsystem"                      ' Set the path.
    		MyName = Dir(MyPath, vbDirectory)   ' Retrieve the first entry.
			'	response.write(MyName)
			
     		'FileName.InnerHtml = MyFile.PostedFile.FileName
         'FileContent.InnerHtml = MyFile.PostedFile.ContentType 
         'FileSize.InnerHtml = MyFile.PostedFile.ContentLength
         UploadDetails.visible = True
         
         Dim strFileName as string
        	strFileName = MyFile.PostedFile.FileName
        	Dim c as string = System.IO.Path.GetFileName(strFileName) ' only the attched file name not its path
         	Try 

	      		 MyFile.PostedFile.SaveAs("\\c1-plankton\leadsystem\" + c)
                        Span1.InnerHtml = "Your File Uploaded Sucessfully at server as :" & c
  
                catch Exp as exception
                  span1.InnerHtml = "An Error occured. Please check the attached  file"
                  response.write("T:\" + c)
                        UploadDetails.visible = false
                       span2.visible=false
                End Try
    		
    	updateuploadhistory(c)	
     	end sub

		sub updateuploadhistory(filename as string)
			Dim rightNow as DateTime = DateTime.Now.toShortDateString() 
 			Dim RightNowAdd as datetime = datetime.now
   		Dim supportedFormats() As String = New String() {"M/dd/yyyy","M/d/yyyy","MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
		   Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
  			dim sqlproc as string
  		  	dim fname as string
  		  	fname = filename
 			sqlproc = "sp_updateloadhistory"
  	     	
  	     	Dim myCommandADD As New SqlCommand( sqlproc, myConnectionADD)
  		 	myCommandADD.CommandType = CommandType.StoredProcedure
    	    
         ' Add Parameters to SPROC
         	Dim prmfilename As New SqlParameter("@filename", SqlDbType.varchar, 50)
				prmfilename.Value = fname
  				myCommandADD.Parameters.Add(prmfilename)
         
         	Dim prmdate As New SqlParameter("@filedate", SqlDbType.datetime)
				prmdate.Value = RightNowAdd
  				myCommandADD.Parameters.Add(prmdate)
  				  				   
            Dim prmuid As New SqlParameter("@uid", SqlDbType.VarChar, 50)
				prmuid.Value = session("userid")
  				myCommandADD.Parameters.Add(prmuid)
  		
            Dim prmleadtype As New SqlParameter("@leadtype", SqlDbType.VarChar, 50)
				prmleadtype.Value = ddlleadtypeupload.selecteditem.text
  				myCommandADD.Parameters.Add(prmleadtype)
  				
            Dim prmstatus As New SqlParameter("@status", SqlDbType.VarChar, 50)
				prmstatus.Value = "Pending"
  				myCommandADD.Parameters.Add(prmstatus)
  		
           Try
               myConnectionADD.Open()
               myCommandADD.ExecuteNonQuery()
               myConnectionADD.Close()
                Catch SQLexc As SqlException
                      Response.Write("Insert Failed. Error Details are: " & SQLexc.ToString())
                 End Try
        End Sub

		Sub FillstatusDropDown()
				Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='leadstatus' and (x_company='" & Session("company_pk") & "' or x_company='All' or x_uid='" & Session("userid") & "') order by x_descr"
    			Dim objCmd As New SqlCommand(myCommand, myConnection)
    			Dim dataReader As SqlDataReader = Nothing
    				Try
      				myConnection.Open()
      				dataReader = objCmd.ExecuteReader()
      				ddlstatusFilter.DataSource = dataReader
      				ddlstatusFilter.DataTextField = "x_descr"
      				ddlstatusFilter.DataValueField = "tbl_xwalk_pk"
      				ddlstatusFilter.DataBind()
    					Catch exc As System.Exception
      					Response.Write(exc.ToString())
    					Finally
      					myConnection.Dispose()
    					End Try

        End Sub
        Sub filladcode()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select distinct av_leadads_FK from tbl_leads join dbo.tbl_LeadADVenues on av_key = ld_adcode where ld_assignedbyuid='" & Session("userid") & "' or ld_assignedtouid='" & Session("userid") & "' ORDER BY av_leadads_FK"
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                ddadcode.DataSource = dataReader
                ddadcode.DataTextField = "av_leadads_FK"
                ddadcode.DataValueField = "av_leadads_FK"
                ddadcode.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
            ddadcode.Items.Insert(0, New ListItem("All", "9999"))
        End Sub

			Sub FillmktTypeDropDown()
  				Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='marketprogram' and  (x_company='All' or x_uid='" & Session("userid") & "') order by x_descr"
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

        End Sub
        
        
		
		Sub FillcontactstatusDropDown()
			Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='contactstatus' and ( x_company='All' or x_uid='" & Session("userid") & "') order by x_descr"
            'x_company='" & Session("company") & "' or
            Dim objCmd As New SqlCommand(myCommand, myConnection)
    		Dim dataReader As SqlDataReader = Nothing
    			Try
      			myConnection.Open()
      			dataReader = objCmd.ExecuteReader()
      			ddlcstatusFilter.DataSource = dataReader
      			ddlcstatusFilter.DataTextField = "x_descr"
      			ddlcstatusFilter.DataValueField = "tbl_xwalk_pk"
      			ddlcstatusFilter.DataBind()
    				Catch exc As System.Exception
      				Response.Write(exc.ToString())
    				Finally
      				myConnection.Dispose()
    				End Try

	  	End Sub      
			
        
        Sub FillassignedtoDropDown()

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String

            myCommand = "Select UID, fname + ' ' + lname as 'name' from dbo.tbl_users where company_pk ='" & Session("company_pk") & "'"
            Dim objCmd As New SqlCommand(myCommand, myConnection)

            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                ddlassignedtoFilter.DataSource = dataReader
                ddlassignedtoFilter.DataTextField = "name"
                ddlassignedtoFilter.DataValueField = "UID"
                ddlassignedtoFilter.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
            ddlassignedtoFilter.Items.Insert(0, New ListItem("All", "9999"))
            ddlassignedtoFilter.Items.Insert(1, New ListItem("Any", "0000"))


        End Sub
	  	
        Sub FillassignedbyDropDown()

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String
            myCommand = "Select UID, fname + ' ' + lname as 'nameby' from dbo.tbl_users where company_pk ='" & Session("company_pk") & "' order by fname + ' ' + lname"
            Dim objCmd As New SqlCommand(myCommand, myConnection)

            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                ddlassignedbyFilter.DataSource = dataReader
                ddlassignedbyFilter.DataTextField = "nameby"
                ddlassignedbyFilter.DataValueField = "UID"
                ddlassignedbyFilter.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
            ddlassignedbyFilter.Items.Insert(0, New ListItem("All", "9999"))

        End Sub
        Sub ChangetypeFilter(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Session("pgindex") = 0
            searchstring()
        End Sub
				
        Sub btnsearch(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Session("pgindex") = 0

            searchstring()
        End Sub
        
        
        Sub returntoworkflow (Source As System.Object, e As System.EventArgs)
        
				Response.Redirect("addeditwf.aspx?action=view&id=" & session("WFMPK") & "&nav=wfsdetails")
		
		
        end sub
        
		
		Sub btndopropmatch (Source As System.Object, e As System.EventArgs)
			Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
  			dim sqlproc as string = "sp_propmatchprocess"
  		  	Dim myCommandADD As New SqlCommand( sqlproc, myConnectionADD)
  		 	myCommandADD.CommandType = CommandType.StoredProcedure  
  		 			
			Try
            myConnectionADD.Open()
            myCommandADD.ExecuteNonQuery()
            myConnectionADD.Close()
            Catch SQLexc As SqlException
                Response.Write("Insert Failed. Error Details are: " & SQLexc.ToString())
            End Try
    					
        End Sub

        Sub deletedleads(ByVal Source As System.Object, ByVal e As System.EventArgs)
            pnlleads.Visible = False
            pnldelleads.Visible = True
            binddelleads()

        End Sub
        Sub MyDataGridR_Page(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
            delleads.CurrentPageIndex = e.NewPageIndex
            binddelleads()
        End Sub

		Sub clearall (Source As System.Object, e As System.EventArgs)
			session("nocontactrpt")="False"
			session("aging")="False"
			lblviewtype.visible=false
            'lead_status.Columns(12).Visible = false
            If Request.QueryString("source") = "nav" Then
                Response.Redirect("leads.aspx?search=*&leadtype=*&status=*&constatus=*&assignedto=*&assignedby=*&adcode=*&source=nav&entrysource=*&program=*&mprogram=*")
            Else
                Response.Redirect("leads.aspx?search=*&leadtype=*&status=*&constatus=*&assignedto=*&assignedby=*&adcode=*&entrysource=*&program=*&mprogram=*")
            End If
       end sub 
			
		Sub btn_nocontact (Source As System.Object, e As System.EventArgs)
			if session("nocontactrpt")="True" then
				session("nocontactrpt")="False"
				btnnocontact.text="No Contact"
				lblviewtype.visible=false
				'lblviewtype.text = "Filter: No Contact"
				'lblviewtype.ForeColor = System.Drawing.Color.Red

			else
				session("nocontactrpt")="True"
				btnnocontact.text="All"
				session("aging")="False"
				lblviewtype.visible=true
				lblviewtype.text = "Filter: No Contact"
				lblviewtype.ForeColor = System.Drawing.Color.Red

			end if
			bindgrid()
		end sub 
        Sub btn_aging(ByVal Source As System.Object, ByVal e As System.EventArgs)

            pnlaging.Visible = True
        End Sub
		
		Sub btn_reports (Source As System.Object, e As System.EventArgs)
			response.redirect("http://reports.gochoiceone.com/reports")
		end sub 
		
		protected sub Item_Click(ByVal sender As Object, ByVal e As EventArgs)
			response.write("HELLO")
        End Sub
        Protected Sub showleads(ByVal sender As Object, ByVal e As EventArgs)
            pnlleads.Visible = True
            pnldelleads.Visible = False
            BindGrid()

        End Sub

        Sub searchstring()

            Dim psearch As String
            Dim pleadtype As String
            Dim pstatus As String
            Dim pconstatus As String
            Dim passigned As String
            Dim passignedby As String
            Dim padcode As String
            Dim pentrysource As String
            dim pprogram as string
            dim mprogram as string

            'check search
            If l_search.Text = "" Then
                psearch = "*"
            Else
                psearch = l_search.Text
                Session("search") = "text"
            End If

            If dd_esource.SelectedItem.Text = "All" Then
                pentrysource = "*"
            Else
                pentrysource = dd_esource.SelectedItem.Text
            End If
            If ddlleadtypeFilter.SelectedItem.Text = "All" Then
                pleadtype = "*"
            Else
                pleadtype = ddlleadtypeFilter.SelectedItem.Text
            End If

            If ddlstatusFilter.SelectedItem.Text = "All" Then
                pstatus = "*"
            Else
                pstatus = ddlstatusFilter.SelectedItem.Text
            End If

            If ddlcstatusFilter.SelectedItem.Text = "All" Then
                pconstatus = "*"
            Else
                pconstatus = ddlcstatusFilter.SelectedItem.Text
            End If

            If ddlassignedtoFilter.SelectedItem.Text = "All" Then
                passigned = "*"
            Else
                passigned = ddlassignedtoFilter.SelectedItem.Text
            End If

            If ddlassignedbyFilter.SelectedItem.Text = "All" Then
                passignedby = "*"
            Else
                passignedby = ddlassignedbyFilter.SelectedItem.Text
            End If

            If passignedby = "*" Then
                session("enteredbyname") = passignedby
            Else
                findenteredbyname(passignedby)
            End If

            If ddadcode.SelectedItem.Text = "All" Then
                padcode = "*"
            Else
                padcode = ddadcode.SelectedItem.Text
            End If
            If ddlleadprogramFilter.SelectedItem.Text = "All" Then
                pprogram = "*"
            Else
                pprogram= ddlleadprogramFilter.SelectedItem.Text
            End If
            If ddlMarketFilter.SelectedItem.Text = "All" Then
                mprogram = "*"
            Else
                mprogram= ddlMarketFilter.SelectedItem.Text
            End If
				
				dim xx as string = Request.QueryString("source")
  				If xx.length > 0 then  
	         	session("querystring") = "leads.aspx?search=" & psearch & "&leadtype=" & pleadtype & "&status=" & pstatus & "&constatus=" & pconstatus & "&assignedto=" & passigned & "&assignedby=" & passignedby & "&adcode=" & padcode & "&entrysource=" & pentrysource & "&program=" & pprogram & "&mprogram=" & mprogram & "&source=" & Request.QueryString("source")

	       	else
	       	  session("querystring") = "leads.aspx?search=" & psearch & "&leadtype=" & pleadtype & "&status=" & pstatus & "&constatus=" & pconstatus & "&assignedto=" & passigned & "&assignedby=" & passignedby & "&adcode=" & padcode & "&entrysource=" & pentrysource & "&program=" & pprogram & "&mprogram=" & mprogram

	       	end if
	       
          
            Response.Redirect(session("querystring"))
        End Sub
		
        Sub checkquerystring()

            If Request.QueryString("search") <> "*" Then
                Session("search") = "Text"
                l_search.Text = Request.QueryString("search")
            Else
                l_search.Text = ""
            End If

            If Request.QueryString("leadtype") = "*" Then
                ddlleadtypeFilter.SelectedIndex = ddlleadtypeFilter.Items.IndexOf(ddlleadtypeFilter.Items.FindByText("All"))
            Else
                ddlleadtypeFilter.SelectedIndex = ddlleadtypeFilter.Items.IndexOf(ddlleadtypeFilter.Items.FindByText(Request.QueryString("leadtype")))
            End If

            If Request.QueryString("status") = "*" Then
                ddlstatusFilter.SelectedIndex = ddlstatusFilter.Items.IndexOf(ddlstatusFilter.Items.FindByText("All"))
            Else
                ddlstatusFilter.SelectedIndex = ddlstatusFilter.Items.IndexOf(ddlstatusFilter.Items.FindByText(Request.QueryString("status")))
            End If

            If Request.QueryString("constatus") = "*" Then
                ddlcstatusFilter.SelectedIndex = ddlcstatusFilter.Items.IndexOf(ddlcstatusFilter.Items.FindByText("All"))
            Else
                ddlcstatusFilter.SelectedIndex = ddlcstatusFilter.Items.IndexOf(ddlcstatusFilter.Items.FindByText(Request.QueryString("constatus")))
            End If

            If Request.QueryString("assignedto") = "*" Then
                ddlassignedtoFilter.SelectedIndex = ddlassignedtoFilter.Items.IndexOf(ddlassignedtoFilter.Items.FindByText("All"))
            Else
                ddlassignedtoFilter.SelectedIndex = ddlassignedtoFilter.Items.IndexOf(ddlassignedtoFilter.Items.FindByText(Request.QueryString("assignedto")))
            End If

            If Request.QueryString("assignedby") = "*" Then
                ddlassignedbyFilter.SelectedIndex = ddlassignedbyFilter.Items.IndexOf(ddlassignedbyFilter.Items.FindByText("All"))
                session("enteredbyname") = "*"
            Else
                ddlassignedbyFilter.SelectedIndex = ddlassignedbyFilter.Items.IndexOf(ddlassignedbyFilter.Items.FindByText(Request.QueryString("assignedby")))
            End If

            If Request.QueryString("adcode") = "*" Then
                ddadcode.SelectedIndex = ddadcode.Items.IndexOf(ddadcode.Items.FindByText("All"))
            Else
                ddadcode.SelectedIndex = ddadcode.Items.IndexOf(ddadcode.Items.FindByText(Request.QueryString("adcode")))
            End If

            If Request.QueryString("entrysource") = "*" Then
                dd_esource.SelectedIndex = dd_esource.Items.IndexOf(dd_esource.Items.FindByText("All"))
            Else
                dd_esource.SelectedIndex = dd_esource.Items.IndexOf(dd_esource.Items.FindByText(Request.QueryString("entrysource")))
            End If
            
            If Request.QueryString("program") = "*" Then
                ddlleadprogramFilter.SelectedIndex = ddlleadprogramFilter.Items.IndexOf(ddlleadprogramFilter.Items.FindByText("All"))
            Else
                ddlleadprogramFilter.SelectedIndex = ddlleadprogramFilter.Items.IndexOf(ddlleadprogramFilter.Items.FindByText(Request.QueryString("program")))
            End If
             If Request.QueryString("mprogram") = "*" Then
                ddlMarketFilter.SelectedIndex = ddlMarketFilter.Items.IndexOf(ddlMarketFilter.Items.FindByText("All"))
            Else
                ddlMarketFilter.SelectedIndex = ddlMarketFilter.Items.IndexOf(ddlMarketFilter.Items.FindByText(Request.QueryString("mprogram")))
            End If
            dim xx as string = Request.QueryString("source")
  				If xx.length > 0 then  
	            Session("qstring") = "leads.aspx?search=" & Request.QueryString("search") & "&leadtype=" & Request.QueryString("leadtype") & "&status=" & Request.QueryString("status") & "&constatus=" & Request.QueryString("constatus") & "&assignedto=" & Request.QueryString("assignedto") & "&assignedby=" & Request.QueryString("assignedby") & "&adcode=" & Request.QueryString("adcode") & "&entrysource=" & Request.QueryString("entrysource") & "&program=" & Request.QueryString("program") & "&mprogram=" & Request.QueryString("mprogram") & "&source=" & Request.QueryString("source")
	            Session("qstringEXP") = "exportleads.aspx?search=" & Request.QueryString("search") & "&leadtype=" & Request.QueryString("leadtype") & "&status=" & Request.QueryString("status") & "&constatus=" & Request.QueryString("constatus") & "&assignedto=" & Request.QueryString("assignedto") & "&assignedby=" & Request.QueryString("assignedby") & "&adcode=" & Request.QueryString("adcode") & "&entrysource=" & Request.QueryString("entrysource") & "&program=" & Request.QueryString("program") & "&mprogram=" & Request.QueryString("mprogram")  & "&source=" & Request.QueryString("source")
				else
				   Session("qstring") = "leads.aspx?search=" & Request.QueryString("search") & "&leadtype=" & Request.QueryString("leadtype") & "&status=" & Request.QueryString("status") & "&constatus=" & Request.QueryString("constatus") & "&assignedto=" & Request.QueryString("assignedto") & "&assignedby=" & Request.QueryString("assignedby") & "&adcode=" & Request.QueryString("adcode") & "&entrysource=" & Request.QueryString("entrysource") & "&program=" & Request.QueryString("program") & "&mprogram=" & Request.QueryString("mprogram") 
	            Session("qstringEXP") = "exportleads.aspx?search=" & Request.QueryString("search") & "&leadtype=" & Request.QueryString("leadtype") & "&status=" & Request.QueryString("status") & "&constatus=" & Request.QueryString("constatus") & "&assignedto=" & Request.QueryString("assignedto") & "&assignedby=" & Request.QueryString("assignedby") & "&adcode=" & Request.QueryString("adcode") & "&entrysource=" & Request.QueryString("entrysource") & "&program=" & Request.QueryString("program") & "&mprogram=" & Request.QueryString("mprogram")
			
				end if
        End Sub
        
         Sub addleadtoworkflow(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim leadno As String = item.Cells(15).Text
        		session("leadno") = leadno
        		AddLeadtoWorkflow(leadno)
        		BindGrid()
        		lblwfleadadd.text ="Lead added to workflow"
        		lblwfleadadd.visible=true
			
        End Sub
        
        sub AddLeadtoWorkflow(leadno as string)
        		
         	insertWFStatusRec(leadno)
				getWFMasterData(leadno)
				getWFStatusRecPK(leadno) 
				getWFSteps(leadno)
		  
		  end sub
		  
		   sub getWFStatusRecPK(leadno as string)
		  
		  		Dim strUID As String = Session("userid")
            Dim strSql As String = "select max(lwfs_tbl_pk) as 'maxpk' from tbl_leadWorkFlowsStatus where lwfs_userid_fk='" & session("userid") & "' " _
            							& "and lwfs_lead_fk='" &  leadno & "' and lwfs_wfm_fk='" & session("WFMPK") & "'" 
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                	session("lswfpk") = sqldr("maxpk")
                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
		  
		  end sub
		  
		  
		  
		  
		   sub insertWFStatusRec(leadno as string)
			
			
				Dim strUID As String = Session("userid")
            Dim strSql As String = "insert into tbl_leadWorkFlowsStatus (lwfs_lead_fk,lwfs_userid_fk,lwfs_wfm_fk,lwfs_leadststatus) " _
                                   & "values ('" & leadno & "','" & session("userid") & "', '" & session("WFMPK") & "', 'Active' )"  
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
			
			
         sub getWFMasterData(leadno as string)
				 Dim strSql As String = "SELECT * from tbl_WorkFlowMaster where wfm_tbl_pk='" & session("WFMPK") & "'"
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    session("WFMStartDate")=Sqldr("wfm_effdate")
                    session("WFMEndDate")=Sqldr("wfm_enddate")
                    
                    
                End If

            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
			
			end sub
			
			sub  getWFSteps(leadno as string)

				Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            mycommand = "select wfs_tbl_pk,wfs_Freq,wfs_Sunday,Wfs_Monday,wfs_Tuesday,wfs_Wednesday,wfs_Thursday,wfs_Friday,wfs_Saturday,wfs_DayofMonth, wfs_DependantStep,wfs_stepno,wfs_basedateoffset,* from tbl_WorkFlowSteps where wfs_wfm_fk='" & session("WFMPK") & "' and wfs_status='Active' order by wfs_stepno "
                        
            Dim ad As New SqlDataAdapter(mycommand, myConnection)
            Dim ds As New DataSet()
          
            Dim i As Integer

            Try
                ad.Fill(ds)
                'ds.Tables(0).TableName = "bill"
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

				For i = 0 To ds.Tables(0).Rows.Count - 1
					session("WFSPK")=ds.Tables(0).Rows(i)(0).ToString()
					session("WFSNo")=ds.Tables(0).Rows(i)(11).ToString()
			
					if ds.Tables(0).Rows(i)(1).ToString() = "Weekly" then
						if ds.Tables(0).Rows(i)(2).ToString() = "Y" then
							
							InsertWFSteps(ds.Tables(0).Rows(i)(1).ToString(),"Sunday","",ds.Tables(0).Rows(i)(10).ToString(),0)
						end if
						if ds.Tables(0).Rows(i)(3).ToString() = "Y" then
						
							InsertWFSteps(ds.Tables(0).Rows(i)(1).ToString(),"Monday","",ds.Tables(0).Rows(i)(10).ToString(),0)
						end if
						if ds.Tables(0).Rows(i)(4).ToString() = "Y" then
						
							InsertWFSteps(ds.Tables(0).Rows(i)(1).ToString(),"Tuesday","",ds.Tables(0).Rows(i)(10).ToString(),0)
						end if
						if ds.Tables(0).Rows(i)(5).ToString() = "Y" then
						
							InsertWFSteps(ds.Tables(0).Rows(i)(1).ToString(),"Wednesday","",ds.Tables(0).Rows(i)(10).ToString(),0)
						end if
						if ds.Tables(0).Rows(i)(6).ToString() = "Y" then
						
							InsertWFSteps(ds.Tables(0).Rows(i)(1).ToString(),"Thursday","",ds.Tables(0).Rows(i)(10).ToString(),0)
						end if
						if ds.Tables(0).Rows(i)(7).ToString() = "Y" then
						
							InsertWFSteps(ds.Tables(0).Rows(i)(1).ToString(),"Friday","",ds.Tables(0).Rows(i)(10).ToString(),0)
						end if
						if ds.Tables(0).Rows(i)(8).ToString() = "Y" then
						
							InsertWFSteps(ds.Tables(0).Rows(i)(1).ToString(),"Saturday","",ds.Tables(0).Rows(i)(10).ToString(),0)
						end if
					elseif ds.Tables(0).Rows(i)(1).ToString() = "Monthly" then
						InsertWFSteps(ds.Tables(0).Rows(i)(1).ToString(),"",ds.Tables(0).Rows(i)(9).ToString(),ds.Tables(0).Rows(i)(10).ToString(),0)
					elseif ds.Tables(0).Rows(i)(1).ToString() = "Quarterly" then
						InsertWFSteps(ds.Tables(0).Rows(i)(1).ToString(),"",ds.Tables(0).Rows(i)(9).ToString(),ds.Tables(0).Rows(i)(10).ToString(),0)
						
					else
               	InsertWFSteps(ds.Tables(0).Rows(i)(1).ToString(),"","",ds.Tables(0).Rows(i)(10).ToString(),Convert.ToInt32(ds.Tables(0).Rows(i)(12).ToString()))
              	end if
		      

            Next

			end sub
			
         
        sub  InsertWFSteps(freq as string, DOW as string, TOM as string, DStep as string, offset as integer)
				dim sdate, sdateNEW as datetime 
				response.write(DStep)
				if DStep="0" then
					sdate =DateTime.Now
				else
					response.write("H1")
					sdate = GetnewStepDate(dstep)
					response.write(sdate)
				end if
				
				
				dim sdateDOW as string = sdate.DayOfWeek.tostring()
				dim sdateDOWString as string
			
				'response.write(freq)
				'response.write("-->")
				if freq="Once" or Freq="Daily" then
					sdateNEW = sdate
				elseif freq="Weekly" then
					
					if DOW = sdateDOW then
						sdateNEW = sdate
					else
						
						while sdateDOW <> DOW
							
							sdate = sdate.AddDays(1)
							sdateDOW = sdate.DayOfWeek.tostring()
						end while
						sdateNEW = sdate
					
					end if							
					
				elseif freq="Monthly" then
					if TOM="First Day of the Month" then
						sdateNEW = sdate.AddDays((sdate.Day - 1) * -1).AddMonths(1)
					else
						sdateNEW = DateAdd(DateInterval.Day, 	(Day(DateAdd(DateInterval.Month, 1, sdate).AddMonths(1))) * -1,  DateAdd(DateInterval.Month, 1, sdate).AddMonths(1))
					end if
					'response.write(Now.AddDays((Now.Day - 1) * -1).AddMonths(1))
					'response.write(DateAdd(DateInterval.Day, 	(Day(DateAdd(DateInterval.Month, 1, sdate).AddMonths(1))) * -1,  DateAdd(DateInterval.Month, 1, sdate).AddMonths(1)))
				elseif freq="Quarterly" then
					Dim currQuarter As Integer = (sdate.Month - 1) / 3 + 1
					response.write(currQuarter)
					if TOM="First Day of the Quarter" then
						
						Dim dtFirstDay As New  DateTime(sdate.Year, 3 * currQuarter-2, 1)
						sdateNEW = dtFirstDay
					else
						Dim dtLastDay As New  DateTime(sdate.Year, 3 * currQuarter, DateTime.DaysInMonth(sdate.Year,3 * currQuarter))
						sdateNEW = dtLastDay
					end if
					'response.write(sdateNEW)
				
				end if
					
				Dim strUID As String = Session("userid")
            Dim strSql As String = "insert into tbl_LeadWorkFlows (lwf_lead_fk,lwf_userid_fk,lwf_wfm_fk,lwf_wfs_fk,lwf_stepno,lwf_status,lwf_freq,lwf_DependantStep,lwf_startdate,lwf_lwfs_fk) " _
                                   & "values ('" & session("leadno") & "','" & session("userid") & "', '" & session("WFMPK") & "', " _ 
                                   & "'" & session("WFSPK") & "', '" &  session("WFSNo") & "','Pending','" & freq & "','" & DStep & "','" & sdateNEW & "','" & session("lswfpk") & "')"
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

 public function GetnewStepDate(dstep as string) as datetime
				Dim strUID As String = Session("userid")
            Dim strSql As String = "select max(lwf_startdate) as 'NewDate' from tbl_LeadWorkFlows where lwf_lead_fk='" & session("leadno") & "' and lwf_userid_fk='" &  session("userid") & "' " _
            								& "and lwf_wfm_fk='" & session("WFMPK")  & "' and lwf_stepno='" & dstep & "'"
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                	return sqldr("NewDate")
                else
                	return Now
                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try

			
			end function
         
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        
        

        Sub findenteredbyname(ByVal aby As String)

            Dim strSql As String = "Select UID from dbo.tbl_users where fname + ' ' + lname='" & aby & "'"
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)
            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    session("enteredbyname") = Sqldr("UID")
                End If

            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try

        End Sub

        Sub ItemDataBoundEventHandler(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
            If e.Item.ItemType = ListItemType.Item Or _
               e.Item.ItemType = ListItemType.AlternatingItem Then
                'Check to see if the price is below a certain threshold

                Dim stat, lstat As String
                stat = DataBinder.Eval(e.Item.DataItem, "followup")
                lstat = DataBinder.Eval(e.Item.DataItem, "ld_status")

                If lstat = "Escalated" Then
                    e.Item.BackColor = System.Drawing.Color.Red
                ElseIf stat = "Yes" Then
                    e.Item.BackColor = System.Drawing.Color.Yellow
                End If
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
        Sub binddelleads()

            Dim strUID As String = Session("userid")
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            Dim RcdCount As Integer
            mycommand = "select *,cast (tbl_leads_pk as varchar(255)) as 'dleadno', case when (ld_lname is null) then ld_fname else ld_fname + ' ' + ld_lname end  as 'name' from tbl_leadsDeleted where (ld_assignedbyuid='" & Session("userid") & "' or ld_assignedtouid='" & Session("userid") & "') order by tbl_leads_pk desc"
            Dim i As Integer
            i = 0
            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_leadsDeleted")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_leadsDeleted"))
                'dvProducts.RowFilter = "ad_title like '%" & Request.QueryString("search") & "%' or ad_text LIKE '%" & Request.QueryString("search") & "%'"
                session("filterrowcount") = dvProducts.Count
                delleads.DataSource = dvProducts
                delleads.DataBind()
                RcdCount = dataSet.Tables("tbl_leadsDeleted").Rows.Count.ToString()
                session("ResultCount") = RcdCount

            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try



        End Sub
        Sub ItemDataBoundEventHandlerDL(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

            End If

        End Sub
        Public Sub restorelead(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(0).Text
            getnewlead()
            Movelead(content)
            movetasks(content)
            movehistory(content)
            movereferals(content)
            binddelleads()
            'delleads(content)
        End Sub
        Sub getnewlead()
            Dim strSql As String = "SELECT max(tbl_leads_pk)+1 as 'pk' from tbl_leads "
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    session("pubtblleadpk") = Sqldr("pk")
                End If

            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try

        End Sub
        Public Sub Movelead(ByVal id As String)

            Dim strSql As String = "insert into dbo.tbl_leads ([tbl_leads_pk],[ld_lname],[ld_fname],[ld_hphone],[ld_cphone],[ld_fax],[ld_address], " _
               & "[ld_city],[ld_state],[ld_zip],[ld_type],[ld_notes],[ld_email],[ld_solicit],[ld_status], " _
               & "[ld_assignedbyuid],[ld_assignedtouid],[ld_agent],[ld_agent_fk],[ld_adddate],[ld_updatedate], " _
               & "[ld_compensation],[ld_refermortg],[ld_refercredit],[ld_referother],[ld_referotherexplain], " _
               & "[ld_apptdate],[ld_appttime],[ld_apptlocation],[ld_capturedate],[ld_highpri],[ld_notificationANY], " _
               & "[ld_adsource],[ld_adcode],[ld_mailtoaddress],[ld_proptolist],[ld_pstatus],[ld_email2],[ld_uploadid], " _
               & "[ld_program],[ld_statdetail],[ld_autopropmatch],[ld_password],[ld_entrysource],[ld_marketingprg]) " _
               & "Select cast('" & session("pubtblleadpk") & "' as integer), " _
               & " [ld_lname],[ld_fname],[ld_hphone],[ld_cphone],[ld_fax],[ld_address], " _
               & "[ld_city],[ld_state],[ld_zip],[ld_type],[ld_notes],[ld_email],[ld_solicit],[ld_status], " _
               & "[ld_assignedbyuid],[ld_assignedtouid],[ld_agent],[ld_agent_fk],[ld_adddate],[ld_updatedate], " _
               & "[ld_compensation],[ld_refermortg],[ld_refercredit],[ld_referother],[ld_referotherexplain], " _
               & "[ld_apptdate],[ld_appttime],[ld_apptlocation],[ld_capturedate],[ld_highpri],[ld_notificationANY], " _
               & "[ld_adsource],[ld_adcode],[ld_mailtoaddress],[ld_proptolist],[ld_pstatus],[ld_email2],[ld_uploadid], " _
               & "[ld_program],[ld_statdetail],[ld_autopropmatch],[ld_password],[ld_entrysource],[ld_marketingprg] " _
               & "From dbo.tbl_leadsDeleted Where tbl_leads_pk='" & id & "'"

            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

            strSql = "delete from dbo.tbl_leadsDeleted Where tbl_leads_pk='" & id & "'"
            myConnection = New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

        End Sub
        Public Sub movetasks(ByVal id As String)

            Dim strSql As String = "INSERT INTO [dbo].[tbl_tasksuser] " _
                & "([lt_type],[lt_status],[lt_uid],[lt_desc],[lt_duedate],[lt_createdate],[lt_leadpk_fk]) " _
                & "select [lt_type],[lt_status],[lt_uid],[lt_desc],[lt_duedate],[lt_createdate]," & session("pubtblleadpk") & " " _
                & "from dbo.tbl_tasksuserDeleted " _
                & "where lt_leadpk_fk='" & id & "'"

            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

            strSql = "delete from tbl_tasksuserDeleted Where lt_leadpk_fk='" & id & "'"
            myConnection = New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

        End Sub
        Public Sub movehistory(ByVal id As String)

            Dim strSql As String = "INSERT INTO [dbo].[tbl_leadscontacthistory] " _
                    & "([tbl_leads_FK],[cnt_date],[cnt_notes],[cnt_agentname],[cnt_agentid],[cnt_type], " _
                    & "[cnt_followup],[cnt_followupaction],[cnt_status],[cnt_who],[cnt_closedt],[cnt_fduedate] ,[cnt_leadtask]) " _
                    & "select " _
                    & session("pubtblleadpk") & ",[cnt_date],[cnt_notes],[cnt_agentname],[cnt_agentid],[cnt_type], " _
                    & "[cnt_followup],[cnt_followupaction],[cnt_status],[cnt_who],[cnt_closedt],[cnt_fduedate] ,[cnt_leadtask] " _
                    & "From dbo.tbl_leadscontacthistoryDeleted where tbl_leads_FK = '" & id & "'"

            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

            strSql = "delete from tbl_leadscontacthistoryDeleted Where tbl_leads_FK='" & id & "'"
            myConnection = New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

        End Sub

        Public Sub movereferals(ByVal id As String)

            Dim strSql As String = "INSERT INTO [dbo].[tbl_referals] " _
                    & "([refer_lead_fk],[refer_company],[refer_customer_fk],[refer_date],[refer_type], " _
                    & "[refer_note],[refer_emailSent],[refer_referby],[refer_referbyid]) " _
                    & "select " _
                    & session("pubtblleadpk") & ",[refer_company],[refer_customer_fk],[refer_date],[refer_type], " _
                    & "[refer_note],[refer_emailSent],[refer_referby],[refer_referbyid] " _
                    & "from dbo.tbl_referalsDeleted where refer_lead_fk='" & id & "'"

            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

            strSql = "delete from tbl_referalsDeleted Where refer_lead_fk='" & id & "'"
            myConnection = New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

        End Sub
        
        Sub getnewleadno()

            Dim strSql As String = "SELECT max(tbl_leads_pk)+1 as 'newpk' from dbo.tbl_leads"
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    session("newleadno") = Sqldr("newpk")
                End If

            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try


        End Sub
        
       
         Sub insertLeaddb()
            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String = "sp_addlead"
            
            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
                Dim prmleadno As New SqlParameter("@newleadno", SqlDbType.Int)
                prmleadno.Value = session("newleadno")
                myCommandADD.Parameters.Add(prmleadno)

                Dim prmuid As New SqlParameter("@uid", SqlDbType.VarChar, 50)
                prmuid.Value = Session("userid")
                myCommandADD.Parameters.Add(prmuid)
           
            Dim prmlfname As New SqlParameter("@l_fname", SqlDbType.VarChar, 50)
            
                prmlfname.Value = session("upfname")
           
            myCommandADD.Parameters.Add(prmlfname)

            Dim prmllname As New SqlParameter("@l_lname", SqlDbType.VarChar, 50)
            
                prmllname.Value = session("uplname")
            
            myCommandADD.Parameters.Add(prmllname)

            Dim prmhphone As New SqlParameter("@l_hphone", SqlDbType.VarChar, 50)
            
                prmhphone.Value = session("upphone1")
            
            myCommandADD.Parameters.Add(prmhphone)

            Dim prmcphone As New SqlParameter("@l_cphone", SqlDbType.VarChar, 50)
            
                prmcphone.Value = session("upphone2")
            
            myCommandADD.Parameters.Add(prmcphone)

            Dim prmaddress As New SqlParameter("@l_address", SqlDbType.VarChar, 30)
            
                prmaddress.Value = session("upadd1")
            
            myCommandADD.Parameters.Add(prmaddress)

  
        
            Dim prmcity As New SqlParameter("@l_city", SqlDbType.VarChar, 30)
            
                prmcity.Value = session("upcity")
            
            myCommandADD.Parameters.Add(prmcity)

            Dim prmstate As New SqlParameter("@l_state", SqlDbType.VarChar, 2)
            
                prmstate.Value = session("upstate")
            
            myCommandADD.Parameters.Add(prmstate)

            Dim prmzip As New SqlParameter("@l_zip", SqlDbType.VarChar, 50)
            
                prmzip.Value = session("upzip")
            
            myCommandADD.Parameters.Add(prmzip)

            Dim prmagent As New SqlParameter("@l_agent", SqlDbType.VarChar, 30)
            prmagent.Value =  Session("Agentname")
            myCommandADD.Parameters.Add(prmagent)

            Dim prmagentFK As New SqlParameter("@l_agent_FK", SqlDbType.VarChar, 30)
            prmagentFK.Value = Session("AgentPK")
            myCommandADD.Parameters.Add(prmagentFK)

            Dim prmstatus As New SqlParameter("@l_status", SqlDbType.VarChar, 30)
            prmstatus.Value = "Accepted"
            myCommandADD.Parameters.Add(prmstatus)

            Dim prmleadtype As New SqlParameter("@l_leadtype", SqlDbType.VarChar, 30)
            prmleadtype.Value = dd_leadtype.SelectedItem.Text
            myCommandADD.Parameters.Add(prmleadtype)

            Dim prmnotes As New SqlParameter("@l_notes", SqlDbType.Text)
                prmnotes.Value = session("upnotes")
            myCommandADD.Parameters.Add(prmnotes)
      
            Dim prmemail As New SqlParameter("@l_email", SqlDbType.VarChar, 50)
            prmemail.Value = session("upemail1")
            myCommandADD.Parameters.Add(prmemail)

            Dim prmemail2 As New SqlParameter("@l_email2", SqlDbType.VarChar, 50)
            prmemail2.Value = session("upemail2")
            myCommandADD.Parameters.Add(prmemail2)


            Dim prmadddate As New SqlParameter("@adddate", SqlDbType.DateTime)
            prmadddate.Value = RightNowAdd
            myCommandADD.Parameters.Add(prmadddate)

            Dim prmcapdate As New SqlParameter("@capdate", SqlDbType.DateTime)
                 prmcapdate.Value = RightNowAdd
             myCommandADD.Parameters.Add(prmcapdate)

            Dim prmapptdate As New SqlParameter("@apptdate", SqlDbType.DateTime)
            prmapptdate.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmapptdate)

            Dim prmappttime As New SqlParameter("@appttime", SqlDbType.VarChar, 5)
                 prmappttime.Value = DBNull.Value
             myCommandADD.Parameters.Add(prmappttime)

            Dim prmapptloc As New SqlParameter("@apptloc", SqlDbType.VarChar, 30)
            prmapptloc.Value = "NA"
            myCommandADD.Parameters.Add(prmapptloc)

            Dim prmrefermortgage As New SqlParameter("@refermortg", SqlDbType.VarChar, 5)
            prmrefermortgage.Value = "N"
            myCommandADD.Parameters.Add(prmrefermortgage)

            Dim prmrefercredit As New SqlParameter("@refercredit", SqlDbType.VarChar, 5)
            prmrefercredit.Value = "N"
            myCommandADD.Parameters.Add(prmrefercredit)

            Dim prmreferother As New SqlParameter("@referother", SqlDbType.VarChar, 5)
            prmreferother.Value = "N"
            myCommandADD.Parameters.Add(prmreferother)

            Dim prmreferotherex As New SqlParameter("@referotherex", SqlDbType.VarChar, 50)
            prmreferotherex.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmreferotherex)

            Dim prmcomp As New SqlParameter("@comp", SqlDbType.VarChar, 15)
                prmcomp.Value = DBNull.Value
             myCommandADD.Parameters.Add(prmcomp)

            Dim prmassignedagent As New SqlParameter("@assignedagent", SqlDbType.VarChar, 50)
            prmassignedagent.Value = Session("userid")
            myCommandADD.Parameters.Add(prmassignedagent)

            Dim prmhighpri As New SqlParameter("@highpri", SqlDbType.VarChar, 5)
            prmhighpri.Value = "No"
            myCommandADD.Parameters.Add(prmhighpri)

            Dim prmldsource As New SqlParameter("@leadsource", SqlDbType.VarChar, 50)
            prmldsource.Value = dd_source.SelectedItem.Text
            myCommandADD.Parameters.Add(prmldsource)

            Dim prmadcode As New SqlParameter("@adcode", SqlDbType.VarChar, 50)
            prmadcode.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmadcode)

            Dim prmmailtoaddress As New SqlParameter("@mailtoaddress", SqlDbType.VarChar, 50)
                prmmailtoaddress.Value = "N"
            myCommandADD.Parameters.Add(prmmailtoaddress)

            Dim prmproptolist As New SqlParameter("@proplist", SqlDbType.VarChar, 50)
            prmproptolist.Value = "N"
            myCommandADD.Parameters.Add(prmproptolist)

            Dim prmpstatus As New SqlParameter("@ld_pstatus", SqlDbType.VarChar, 50)
            prmpstatus.Value = dd_ldstat.SelectedItem.Text
            myCommandADD.Parameters.Add(prmpstatus)

            Dim prmprogram As New SqlParameter("@ld_program", SqlDbType.VarChar, 20)
            prmprogram.Value = dd_leadprogram.SelectedItem.Text
            myCommandADD.Parameters.Add(prmprogram)

            Dim prmstatdetail As New SqlParameter("@ld_statdetail", SqlDbType.VarChar, 50)
                prmstatdetail.Value = DBNull.Value
             myCommandADD.Parameters.Add(prmstatdetail)

            Dim prmentrysource As New SqlParameter("@ld_entrysource", SqlDbType.VarChar, 50)
            prmentrysource.Value = "Auto"
            myCommandADD.Parameters.Add(prmentrysource)
 
            Dim prmfax As New SqlParameter("@ld_fax", SqlDbType.VarChar, 50)
                 prmfax.Value = session("upfax")
             myCommandADD.Parameters.Add(prmfax)
             
                 Dim prmmkprg As New SqlParameter("@marketprog", SqlDbType.VarChar, 50)
            prmmkprg.Value = "None"
            myCommandADD.Parameters.Add(prmmkprg)
            
            Dim prmmktto As New SqlParameter("@marketto", SqlDbType.VarChar, 50)
            prmmktto.Value = "Yes"
            myCommandADD.Parameters.Add(prmmktto)
            
             Dim prmldext As New SqlParameter("@ld_ext1", SqlDbType.VarChar, 50)
            prmldext.Value = dbnull.value
            myCommandADD.Parameters.Add(prmldext)


            Try
                myConnectionADD.Open()
                myCommandADD.ExecuteNonQuery()
                myConnectionADD.Close()
            Catch SQLexc As SqlException
                Response.Write("Insert Failed. Error Details are: " & SQLexc.ToString())
            End Try


            'if Request.QueryString("action")= "new" then
            'addtasks()
            'end if

        End Sub
        
         Public Sub btn_showhelp(ByVal Source As Object, ByVal e As ImageClickEventArgs)
      		Response.Write("<script>window.open" & _
                "('" & System.Configuration.ConfigurationManager.AppSettings("CurrentappURL") & "/leadhelp.aspx','_new','width=1000,height=650,resizable=1,scrollbars=1');</script>")
     
		 end sub
     
       

    End Class
    
end namespace