Imports System
Imports System.Collections
Imports System.Data.SqlClient
Imports System.ComponentModel
Imports System.Data
Imports System.Drawing
Imports System.Drawing.Color
Imports System.Web
Imports System.Web.SessionState
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Xml
Imports System.Configuration
Imports System.Globalization
Imports System.Net.Mail
Imports System.Text
Imports FreeTextBoxControls


Namespace PageTemplate
    Public Class addlead2
        Inherits PageTemplate
        'public l_notesB,l_tasks,l_questions,l_vwcontact,l_vwreferals,
        Public subnavAcnt,subnavLP, subexportqueue, subnavtasks, subnavnotes, subnavprofile, subnavhistory, subrefer, subtasks, subnavfinance, submatches,subWorkFlow As HtmlTableCell
        Public spacer0, spacer0a, spacer1, spacer1a, spacer2, spacer3, spacer4, spacer5, spacer6,spacer6a As HtmlTableCell
			public workflow,workflowView as datagrid
        Public l_addreferal, l_print, l_saveexit As System.Web.UI.WebControls.Button
        Public lblleadno, lblassignedby, lblstatusA, lblcapdate, lblnofinancials As Label
        Public dd_agent, dd_refertype, dd_refervendor, dd_ldstat, ddLHType, dd_apptloc,ddLHTypeH As DropDownList
        Public dd_status, dd_leadtype, dd_highpri, dd_source, dd_leadprogram, dd_statdetail,dd_mkprg As DropDownList
        Public l_fname, l_lname, l_hphone, l_cphone, l_email, l_address, l_city, l_state, l_zip,  l_email2, l_editcontact As TextBox
        public l_notes
        Public l_appdate, l_appttime, l_comp, l_adcode, l_sendemailtxt As TextBox
        Public scc, wls, npp, lsc, cwks, clp, clpo As CheckBox
        Public l_mailto As System.Web.UI.WebControls.CheckBox
        Public pnlnew, pnlack, pnltenant, pnlinitialnotes, pnlviewnotes, pnladdencoutner, pnlassignedby, pnladdreferal As Panel
			Public pnlplaceholder, pnladdreferalADD, pnladdreferalADDOther, pnladdreferalADDOther1, pnlplaceholderA As Panel
        Public pnlleadsource, pnlmorebutton, pnlmore, pnlcapdate, pnlprofile, pnlleadmore, pnlescalate, pnlclose, pnlmatch, leadinfo, pnlfinancials As Panel
        Public pnlsc,pnlpropmatch, pnltasks, pnlexportque, pnlLP,pnlworkflow As Panel
        Public l_edate, l_enotes, l_capdate, l_web, l_fax As TextBox
        Public l_delete, l_cancel, l_contactfollowup, l_automatch As Button
        Public l_savedraft, l_save, l_accept, l_close, l_sendemail As Button
        Public lblstatus As Label
        Public reassign, leadmore, Escalate, more, lfinance, lnk_HistScreen, lexport, lLP,lSCA  As LinkButton
        Public chkbed, chkcity, chkcredit, chkmovedt, chkrent, chkbath, chklevel, chkschool, chksec8 As CheckBox
        Protected WithEvents lNotes, lProfile, lTasks, lReferrals, lhistory, lmatches,lworkflow As LinkButton
        Public myCheckbox As CheckBox
        public ImageButton2,ImageButton1,ImageButton3,ImageButton4,btn_note as imagebutton
			public l_addcontact, btnaddtask as button
			public epri,eco,esec,euser as linkbutton
		   public pnlapri, pnlaco,pnlusr,pnlSco,pnladd2workflow,wrkflowmain as panel
        Protected WithEvents lead_history, DGprofile, referals, DGPropMatches, dgtasks, exportqueue As System.Web.UI.WebControls.DataGrid
        Public darea As Table
        public txtuser1N, l_hext,txtuser2N,txtuser3N,txtuser4N,txtuser5N as textbox
        public lbtxtuser1,lbtxtuser2,lbtxtuser3,lbtxtuser4,lbtxtuser5 as linkbutton
        public btnsavecancel,btnsavenewtitle,btnsavenewtitle1,btnsavecancel1,btnsavenewtitle2,btnsavecancel2  as button
        public btnsavenewtitle3,btnsavecancel3,btnsavenewtitle4,btnsavecancel4,btnsavenewtitle5,btnsavecancel5 as button
        public lbltitle1,lbltitle2,lbltitle3,lbltitle4,lbltitle5 as label
        public dd_mkto,dd_WFStatFilter as dropdownlist
        public txtuser1,txtuser2,txtuser3,txtuser4,txtuser5 as textbox
			public co_title,co_name,co_phone,co_web,Sco_name,sco_phone1,sco_phone2,sco_address,sco_city,sco_state,sco_zip as textbox
			


        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

            If Not (Page.IsPostBack) Then
          		clearsessions()
          		subnavfinance.Visible = False
                submatches.Visible = False
                pnlfinancials.Visible = False
                lblnofinancials.Visible = False
                Escalate.Enabled = False
                l_print.Visible = False
                pnlclose.Visible = False
                pnlleadmore.Visible = False
                leadinfo.Visible = True
                'pnltasktakeout.visible = false
                dd_status.BackColor = LightGray
                dd_status.Enabled = False
                'pnlnotasks.visible = false
                'l_questions.visible = true
                dd_status.Visible = False
                lblstatus.Visible = False
                l_sendemail.Visible = False
                pnlplaceholderA.Visible = False
                Dim msg As String = ""
                msg = msg & "<Script Language='JavaScript'>"
                msg = msg & "window.opener.location.href=window.opener.location.href;"
                msg = msg & "window.close();"
                msg = msg & "</Script>"
                Response.Write(msg)

                If Request.QueryString("action") = "new" Then
                    'All
                    session("leadentrysource") = "Manual"
                    l_automatch.Text = "Auto Match OFF"
                    pnlescalate.Visible = False
                    subnavhistory.Visible = False
                    l_cancel.Text = "Cancel"
                    l_saveexit.Visible = False
                    pnlinitialnotes.Visible = True
                    'pnlquestions.visible=false
                    'pnlnoquestions.visible=false
                    'pnlnotasks.visible=false
                    'pnlrentaltasks.visible=false
                    'pnltasktakeout.visible=false
                    pnlviewnotes.Visible = False
                    pnladdreferalADDOther.Visible = False
                    pnladdreferalADDOther1.Visible = False
                    pnladdencoutner.Visible = False
                    pnladdreferal.Visible = False
                    pnladdreferalADD.Visible = False
                    pnlprofile.Visible = False
                    reassign.Visible = True
                    pnlnew.Visible = True
                    pnlack.Visible = False
                    pnlplaceholder.Visible = True
                    pnlassignedby.Visible = False
                    pnladdreferalADD.Visible = False
                    ' l_delete.Visible = False
                    l_accept.Visible = False
                    l_close.Visible = False
                    'subquestions.Visible = False
                    submatches.Visible = False
                    'Industry Specific
                    If Session("industry") = "Real Estate" Then
                        subnavprofile.Visible = False
                    Else
                        subnavprofile.Visible = False
                    End If

                    'Bind Drop Downs
                    bindagent()
                    bindsource()
                    bindreferaltype()
                    FillLeadProgram()
                    FillLeadtype()
                    FillLeadstatus()
                    FillmarketProgram()
                    l_capdate.Text = DateTime.Now.ToShortDateString()
                    dd_agent.SelectedIndex = dd_agent.Items.IndexOf(dd_agent.Items.FindByText(Session("agentname")))
                    'dd_agent.SelectedItem.Text = Session("agentname")
                    dd_ldstat.SelectedItem.Text = "New"
                    ''.SelectedIndex = dd_ldstat.Items.IndexOf(dd_ldstat.Items.FindByText("New"))
                    subexportqueue.Visible = False

                ElseIf Request.QueryString("action") = "view" Then
                	
                		bindLHTypeH()
                    l_close.Visible = False
                    Escalate.Enabled = False
                    subnavhistory.Visible = True
                    l_cancel.Text = "Exit"
                    pnlcapdate.Visible = False
                    pnlplaceholder.Visible = False
                    pnlnew.Visible = True
                    pnlack.Visible = False
                    pnlassignedby.Visible = True
                    pnladdreferalADD.Visible = False
                    pnladdreferalADDOther.Visible = False
                    pnladdreferalADDOther1.Visible = False
                    'l_sendemail.Visible = True
                    'l_delete.Visible = False
                    l_accept.Visible = False
                    l_close.Visible = False
                    pnladdencoutner.Visible = False
                    FillLeadProgram()
                    FillmarketProgram()
                    FillLeadtype()
                    FillLeadstatus()
                    bindagent()
                    bindsource()
                    bindfields()
                    BindhistoryGrid()
                    bindreferaltype()
                    bindtasks()
                    getreferals()
                    'bindleadtasks()
                    'subquestions.Visible = False
                    If Session("industry") = "Real Estate" Then
                        subnavprofile.Visible = False
                        submatches.Visible = False
                        lProfile.Enabled = False
                    Else
                        subnavprofile.Visible = False
                        submatches.Visible = False
                        lProfile.Enabled = False
                    End If
                    pnlleadsource.Visible = True
                    If dd_status.SelectedItem.Text = "Draft" Then
                        'subquestions.Visible = False
                        l_saveexit.Visible = False
                        l_savedraft.Visible = True
                        'l_delete.Visible = True
                        l_save.Text = "Save & Submit"
                    End If

                    If lblstatusA.Text = "Escalated" Then
                        pnlescalate.Visible = True
                    Else
                        pnlescalate.Visible = False
                    End If

                    'This is Good Code
                    If dd_status.SelectedItem.Text = "Unaccepted" Then
                        l_accept.Visible = True
                        l_savedraft.Visible = False

                        If session("assignedbyUID") = Session("userid") Or Session("role") = "Administrator" Or Session("role") = "GOD" Then

                            l_saveexit.Visible = True
                            l_save.Visible = True
                            l_save.Text = "Save"
                            lhistory.Enabled = True
                            lReferrals.Enabled = True
                            'lTasks.enabled=true
                            ''lQuestions.enabled=true
                            fields("true")
                            If Session("role") = "Administrator" Then
                                'l_delete.Visible = True
                                'l_sendemail.Visible = True
                            Else
                                'l_delete.Visible = False
                                'l_sendemail.Visible = False
                            End If
                        Else

                            l_saveexit.Visible = False
                            'l_sendemail.Visible = False

                            lhistory.Enabled = False
                            lReferrals.Enabled = False
                            lTasks.Enabled = False
                            'lQuestions.Enabled = False
                            dd_agent.Enabled = False
                            l_save.Visible = False
                            'l_delete.Visible = False
                            fields("false")
                        End If
                    End If

                    If dd_status.SelectedItem.Text = "Accepted" Then
                        l_accept.Visible = False
                        l_savedraft.Visible = False
                        l_save.Text = "Save"
                        dd_agent.Enabled = False
                        If Session("role") = "Administrator" Then
                            'l_delete.Visible = True
                            'l_sendemail.Visible = True
                        Else
                            'l_delete.Visible = False
                            'l_sendemail.Visible = False
                        End If
                    End If

                    If dd_status.SelectedItem.Text = "Closed" Then
                        l_accept.Visible = False
                        l_savedraft.Visible = False
                        l_save.Visible = False
                        l_close.Visible = False
                        If Session("role") = "Administrator" Then
                            'l_delete.Visible = True
                        Else
                            ' l_delete.Visible = False
                        End If
                    End If
                End If

                If Request.QueryString("source") = "history" Then
                    subnav("History")
                ElseIf Request.QueryString("source") = "profile" Then
                    subnav("PropertyProfile")
                ElseIf Request.QueryString("source") = "task" Then
                    'subnav("History")
                    subnav("Tasks")
                Else
                    subnav("LeadProfile")
                End If
                If session("leadentrysource") = "Auto" Then
                    l_adcode.Enabled = False
                    dd_source.Enabled = False
                Else
                    l_adcode.Enabled = True
                    dd_source.Enabled = True
                End If
					 
					 If Request.QueryString("action") = "view" Then
                
						 if not chkleadisusers() then
							ImageButton2.enabled=false
							l_accept.enabled=false
							l_save.enabled=false
							l_savedraft.enabled=false
							l_saveexit.enabled=false
							btn_note.enabled=false
							ImageButton4.enabled=false
							ImageButton1.enabled=false
							ImageButton3.enabled=false
							l_fname.enabled=false
							l_lname.enabled=false
							l_hphone.enabled=false
							l_cphone.enabled=false
							l_email.enabled=false
							l_addcontact.enabled=false
							btnaddtask.enabled=false
							l_addreferal.enabled=false
							dd_leadtype.enabled=false
							dd_ldstat.enabled=false
							dd_leadprogram.enabled=false
							dd_source.enabled=false
							reassign.enabled=false
							l_adcode.enabled=false
							dd_highpri.enabled=false
							l_comp.enabled=false
							dd_agent.enabled=false
							l_capdate.enabled=false
							l_email2.enabled=false
							l_fax.enabled=false
							l_web.enabled=false
							l_address.enabled=false
							l_city.enabled=false
							l_state.enabled=false
							l_zip.enabled=false
							l_mailto.enabled=false
	
							
						 end if
					end if	 
					
					if Request.QueryString("nav") = "wfsdetail" Then
						 subnav("WorkFlow")
            		bindworkflows()
					
					end if
					dd_WFStatFilter.SelectedIndex = dd_WFStatFilter.Items.IndexOf(dd_WFStatFilter.Items.FindByText("Active"))
					
					
 					session("oleadtype")=dd_leadtype.selecteditem.text
 					session("oleadprog")=dd_leadprogram.selecteditem.text
					session("oleadstatus")=dd_ldstat.selecteditem.text
					session("omarketprog")=dd_mkprg.selecteditem.text
					if Request.QueryString("source") = "wfsetup" Then
						l_accept.visible=false
						l_save.visible=true
						l_savedraft.visible=false
						l_saveexit.visible=false
					
					end if
				
            End If
            pagesetup()

        End Sub
		sub clearsessions()
			session("strnotask")="False"
			session("strsavetype")=""
			session("hashistory")=""
			session("leadentrysource")=""
			session("assignedagentid")=""
			session("referemailsent")=""
			session("pubuidfullname")=""
			session("agentemail")=""       
			session("assignedbyUID")=""  
			session("totleads")=""   
			session("referalleadpk")=0 
			session("pstatus")=""       
	      session("publeadreassigned")=""  
	      session("pubtblleadpk")="" 
	      session("pubcuspk")="" 
	      session("pubcusemail")="" 
	      session("pubrefertype")=""  
	      session("newleadno")=""    
	      session("currentleadno")="" 
	      session("srchcriteria")=""   	       

			end sub
			public function chkleadisusers() as boolean
			 	Dim strUID As String = Session("userid")
                Dim strSql As String = "SELECT *,case when (leadedit is null) then 'False' else leadedit end as 'LeadEditA' from tbl_leads " _
                								& "left join dbo.tbl_Usr2UsrRoles on SUID=ld_assignedtouid and tUID='" & session("userid") & "' " _
                								& "where tbl_leads_pk='" & request.querystring("id") & "'"
                Dim sqlCmd As SqlCommand

                Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
                sqlCmd = New SqlCommand(strSql, myConnection)

                Try
                    myConnection.Open()
                    Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                    If Sqldr.Read() Then
                        If (Sqldr("ld_assignedbyuid") = session("userid") or Sqldr("ld_assignedtouid") = session("userid") or Sqldr("LeadEditA")="True")  Then
                            return true
                        else
                        	return false
                        End If

                    End If
                Catch SQLexc As SqlException
                    Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
                Finally
                    myConnection.Close()
                End Try
			
			end function


        Sub doRollOver()
        End Sub
 			Sub FillmarketProgram()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='marketProgram' and ( x_company='All' or x_uid='" & Session("userid") & "') order by x_descr"
            'x_company='" & Session("company") & "' or
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                dd_mkprg.DataSource = dataReader
                dd_mkprg.DataTextField = "x_descr"
                dd_mkprg.DataValueField = "tbl_xwalk_pk"
                dd_mkprg.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
            dd_mkprg.Items.Insert(0, New ListItem("None", "9999"))
        End Sub
        Sub FillLeadProgram()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='LeadProgram' and ( x_company='All' or x_uid='" & Session("userid") & "') order by x_descr"
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
            dd_leadprogram.Items.Insert(0, New ListItem("None", "9999"))
        End Sub

        Sub FillLeadtype()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='Leadtype' and (x_company='All' or x_uid='" & Session("userid") & "') order by x_descr"
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
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='contactstatus' and (x_company='All' or x_uid='" & Session("userid") & "') order by x_descr"
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

        Sub MoreButtonClick(ByVal sender As Object, ByVal e As EventArgs)

            If pnlmore.Visible = True Then
                pnlmore.Visible = False
                more.Text = "More"
            Else
                pnlmore.Visible = True
                more.Text = "Hide"
            End If
        End Sub

        Sub LeadMoreButtonClick(ByVal sender As Object, ByVal e As EventArgs)

            If pnlleadmore.Visible = True Then
                pnlleadmore.Visible = False
                leadmore.Text = "More"
            Else
                pnlleadmore.Visible = True
                leadmore.Text = "Hide"
            End If
        End Sub

        Sub Leadescalate(ByVal sender As Object, ByVal e As EventArgs)

        End Sub
        Sub quicknote(ByVal Source As Object, ByVal e As ImageClickEventArgs)
            'Response.Redirect("leadhistory.aspx?history=0&LeadNo=" & Request.QueryString("id") & "&LeadType=" & dd_leadtype.SelectedItem.Text & "&action=new")
            if dd_status.selecteditem.text="Draft" then
           		session("pstatus") = "Draft"
                getagentUID()
                insertdb()           
           	else
           		btn_saveDB()
            end if
            Response.Write("<script>window.open" & _
               "('leadhistory.aspx?history=0&type=history&LeadNo=" & Request.QueryString("id") & "&LeadType=" & dd_leadtype.SelectedItem.Text & "&action=new&source=popup','_new', 'width=800,height=500');</script>")
        End Sub
        Sub createtask(ByVal Source As Object, ByVal e As ImageClickEventArgs)
            'Response.Redirect("leadhistory.aspx?history=0&LeadNo=" & Request.QueryString("id") & "&LeadType=" & dd_leadtype.SelectedItem.Text & "&action=new")
           if dd_status.selecteditem.text="Draft" then
           		session("pstatus") = "Draft"
                getagentUID()
                insertdb()           
           	else
           		btn_saveDB()
            end if
            Response.Write("<script>window.open" & _
               "('leadhistory.aspx?history=0&type=task&LeadNo=" & Request.QueryString("id") & "&LeadType=" & dd_leadtype.SelectedItem.Text & "&action=new&source=popup','_new', 'width=800,height=500');</script>")
        End Sub
        Sub BindhistoryGrid()
            Dim strpropID As String = Request.QueryString("id")
            Dim strUID As String = Session("userid")
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            '	mycommand = "Select *, convert(varchar(20),ld_adddate,101) as ld_adddatef from dbo.tbl_leads Where ((ld_assignedtouid='"& strUID &"' or ld_assignedbyuid='"& strUID &"' or ld_agent='Any') and (ld_status='Unaccepted' or ld_status='Accepted')) or ((ld_assignedbyuid='"& strUID &"') and ld_status='Draft') order by tbl_leads_pk desc"
            If Session("histfilter") = "All" Then
                mycommand = "Select *, left(convert(varchar(255),cnt_notes),50)+'...</table>' as 'briefnotes', cast(tbl_leadcnthistory_pk as varchar(20)) as 'hnum',cast(tbl_leads_fk as varchar(20)) as 'lnum2', convert(varchar(20),cnt_date,101) as date from tbl_leadscontacthistory Where tbl_leads_FK=" & strpropID
            Else
                mycommand = "Select *, left(convert(varchar(255),cnt_notes),50)+'...</table>' as 'briefnotes', cast(tbl_leadcnthistory_pk as varchar(20)) as 'hnum',cast(tbl_leads_fk as varchar(20)) as 'lnum2', convert(varchar(20),cnt_date,101) as date from tbl_leadscontacthistory Where tbl_leads_FK=" & strpropID & " and cnt_type='" & Session("histfilter") & "'"

            End If

            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_leadscontacthistory")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_leadscontacthistory"))
                lead_history.DataSource = dvProducts
                lead_history.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
        End Sub
        Sub bindtasks()
            Dim strpropID As String = Request.QueryString("id")
            Dim strUID As String = Session("userid")
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            mycommand = "Select *, left(convert(varchar(255),lt_desc),50)+'...' as 'briefnotes',cast(lt_tbl_pk as varchar(20)) as 'ldtaskpk',cast(lt_leadpk_fk as varchar(20)) as 'leadpk', convert(varchar(20),lt_duedate,101) as 'duedate' from tbl_tasksuser Where lt_leadpk_fk='" & strpropID & "'"

            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_tasksuser")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_tasksuser"))
                dgtasks.DataSource = dvProducts
                dgtasks.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
        End Sub

        Sub bindreferaltype()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='refertype' order by x_descr ", myConnection)

            Try
                myConnection.Open()
                dd_refertype.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
                dd_refertype.DataBind()
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            End Try

        End Sub

        Sub bindreferalvendor()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("Select cus_company, tbl_customers_pk from dbo.tbl_customers where cus_refertype='" & dd_refertype.SelectedItem.Text & "' or cus_refertype='All' order by cus_refertype ", myConnection)

            Try
                myConnection.Open()
                dd_refervendor.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
                dd_refervendor.DataBind()
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            End Try

        End Sub
        Sub btn_print(ByVal Source As Object, ByVal E As EventArgs)

            Response.Redirect("pdftest.aspx?id=" & Request.QueryString("id"))

        End Sub
        Sub bindLHType()

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='LHType' order by x_descr"
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                ddLHType.DataSource = dataReader
                ddLHType.DataTextField = "x_descr"
                ddLHType.DataValueField = "tbl_xwalk_pk"
                ddLHType.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

        End Sub

        Sub dorefervendor(ByVal Source As Object, ByVal E As EventArgs)
            If dd_refertype.SelectedItem.Text <> "Other" Then
                Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
                Dim myCommand As New SqlCommand("Select cus_company, tbl_customers_pk from dbo.tbl_customers where cus_refertype='" & dd_refertype.SelectedItem.Text & "' or cus_refertype='All' order by cus_refertype ", myConnection)

                Try
                    myConnection.Open()
                    dd_refervendor.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
                    dd_refervendor.DataBind()
                Catch SQLexc As SqlException
                    Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
                End Try
            Else
                If dd_refervendor.Visible = True Then

                    dd_refervendor.Visible = False
                    pnladdreferalADDOther.Visible = True
                    pnladdreferalADDOther1.Visible = True
                Else
                    dd_refervendor.Visible = True
                    pnladdreferalADDOther.Visible = False
                    pnladdreferalADDOther1.Visible = False
                End If
            End If
        End Sub

        Sub MyDataGrid_Page(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
            lead_history.CurrentPageIndex = e.NewPageIndex
            BindhistoryGrid()
        End Sub

        Sub bindsource()

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='leadsource' and (x_company='All' or x_uid='" & Session("userid") & "') order by x_descr", myConnection)
            'x_company='" & Session("company") & "' or 
            Try
                myConnection.Open()
                dd_source.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
                dd_source.DataBind()
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            End Try

            dd_source.Items.Insert(0, New ListItem("None", "9999"))

        End Sub

        Sub getreferals()
            Dim strpropID As String = Request.QueryString("id")
            Dim strUID As String = Session("userid")
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            mycommand = "Select *,convert(varchar(20),refer_date,101) as 'Date' from dbo.tbl_referals Where refer_lead_fk=" & strpropID
            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_referals")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_referals"))

                referals.DataSource = dvProducts
                referals.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

        End Sub

        Sub btn_addrefer(ByVal sender As Object, ByVal e As EventArgs)
            pnladdreferal.Visible = False
            pnladdreferalADD.Visible = True
            pnladdreferalADDOther.Visible = False
            pnladdreferalADDOther1.Visible = False
            dd_refervendor.Visible = True
            bindreferaltype()
            bindreferalvendor()
        End Sub


        Sub btn_finance(ByVal sender As Object, ByVal e As EventArgs)

            subnav("Financials")
        End Sub


        Sub btn_notes(ByVal sender As Object, ByVal e As EventArgs)
            subnav("Notes")
        End Sub
        Sub btn_LP(ByVal sender As Object, ByVal e As EventArgs)
            subnav("LeadProfile")
        End Sub
        
        Sub btn_SC(ByVal sender As Object, ByVal e As EventArgs)
            subnav("Contactinfo")
            pnlapri.visible=true
            pnlaco.visible=false
            pnlusr.visible=false
            pnlSco.visible=false
            epri.cssclass="linkbuttonsRed"
            eco.cssclass="linkbuttons"
            esec.cssclass="linkbuttons"
            euser.cssclass="linkbuttons"
            bindusertitles()
            bindufields()
            bindacfields()
            
        End Sub       

        Sub btn_srchcriteria(ByVal sender As Object, ByVal e As EventArgs)
            bindsearchcriteria()
            pnlmatch.Visible = True
            pnlprofile.Visible = False

        End Sub

        Sub btn_matches(ByVal sender As Object, ByVal e As EventArgs)
            subnav("Matches")
            bindpropmatches()
        End Sub
        Sub btn_workflows(ByVal sender As Object, ByVal e As EventArgs)
            subnav("WorkFlow")
            bindworkflows()
        End Sub
        
        
        
         Sub btn_epri(ByVal sender As Object, ByVal e As EventArgs)
            
            pnlapri.visible=true
            pnlaco.visible=false
            pnlusr.visible=false
            pnlSco.visible=false
            epri.cssclass="linkbuttonsRed"
            eco.cssclass="linkbuttons"
            esec.cssclass="linkbuttons"
            euser.cssclass="linkbuttons"
        End Sub
         Sub btn_eco(ByVal sender As Object, ByVal e As EventArgs)
            
            pnlapri.visible=false
            pnlaco.visible=true
             pnlusr.visible=false
             pnlSco.visible=false
            epri.cssclass="linkbuttons"
            eco.cssclass="linkbuttonsRed"
            esec.cssclass="linkbuttons"
            euser.cssclass="linkbuttons"
        End Sub
        
         Sub btn_sec(ByVal sender As Object, ByVal e As EventArgs)
            
             pnlapri.visible=false
            pnlaco.visible=false
             pnlusr.visible=false
             pnlSco.visible=true
            epri.cssclass="linkbuttons"
            eco.cssclass="linkbuttons"
            esec.cssclass="linkbuttonsRed"
            euser.cssclass="linkbuttons"
        End Sub
        
         Sub btn_usr(ByVal sender As Object, ByVal e As EventArgs)
            
           pnlapri.visible=false
            pnlaco.visible=false
             pnlusr.visible=true
             pnlSco.visible=false
            epri.cssclass="linkbuttons"
            eco.cssclass="linkbuttons"
            esec.cssclass="linkbuttons"
            euser.cssclass="linkbuttonsRed"
          
            bindusertitles()
        End Sub
        
        Sub btn_canceltitles(ByVal sender As Object, ByVal e As EventArgs)
          	txtuser1N.visible=false  
           	txtuser1N.text=""
           	lbltitle1.visible=false
          	btnsavenewtitle1.visible=false
          	 btnsavecancel1.visible=false   
        End Sub
        
        Sub btn_usrt1(ByVal sender As Object, ByVal e As EventArgs)
           dim x as linkbutton = sender
           if x.id="lbtxtuser1" then
           		txtuser1N.visible=true   
          		btnsavenewtitle1.visible=true 
          		lbltitle1.visible=true  
         		btnsavecancel1.visible=true
          elseif x.id="lbtxtuser2" then
          		txtuser2N.visible=true   
          		btnsavenewtitle2.visible=true 
          		lbltitle2.visible=true  
         		btnsavecancel2.visible=true
          elseif x.id="lbtxtuser3" then
           		txtuser3N.visible=true   
          		btnsavenewtitle3.visible=true 
          		lbltitle3.visible=true  
         		btnsavecancel3.visible=true
          elseif x.id="lbtxtuser4" then
            	txtuser4N.visible=true   
          		btnsavenewtitle4.visible=true 
          		lbltitle4.visible=true  
         		btnsavecancel4.visible=true
          elseif x.id="lbtxtuser5" then
          		txtuser5N.visible=true   
          		btnsavenewtitle5.visible=true 
          		lbltitle5.visible=true  
         		btnsavecancel5.visible=true
         end if
        End Sub
        
        sub btn_saventit1(ByVal sender As Object, ByVal e As EventArgs)
         dim x as button = sender
         if usrtitleexists() then
         	if x.id="btnsavenewtitle1" then
          		updatetitle("1",txtuser1N.text)
          		txtuser1N.visible=false  
	           	txtuser1N.text=""
	           	lbltitle1.visible=false
	          	btnsavenewtitle1.visible=false
          	 	btnsavecancel1.visible=false   
          	elseif x.id="btnsavenewtitle2" then
          		updatetitle("2",txtuser2N.text)
          		txtuser2N.visible=false  
	           	txtuser2N.text=""
	           	lbltitle2.visible=false
	          	btnsavenewtitle2.visible=false
          	 	btnsavecancel2.visible=false   
          	elseif x.id="btnsavenewtitle3" then
          		updatetitle("3",txtuser3N.text)
          		txtuser3N.visible=false  
	           	txtuser3N.text=""
	           	lbltitle3.visible=false
	          	btnsavenewtitle3.visible=false
          	 	btnsavecancel3.visible=false  	
          	elseif x.id="btnsavenewtitle4" then
          		updatetitle("4",txtuser4N.text)
          		txtuser4N.visible=false  
	           	txtuser4N.text=""
	           	lbltitle4.visible=false
	          	btnsavenewtitle4.visible=false
          	 	btnsavecancel4.visible=false  
          	elseif x.id="btnsavenewtitle5" then
          		updatetitle("5",txtuser5N.text)
          		txtuser5N.visible=false  
	           	txtuser5N.text=""
	           	lbltitle5.visible=false
	          	btnsavenewtitle5.visible=false
          	 	btnsavecancel5.visible=false  
          	end if
          		
          else
          	insertitle()
          	if x.id="btnsavenewtitle1" then
          		updatetitle("1",txtuser1N.text)
          		txtuser1N.visible=false  
	           	txtuser1N.text=""
	           	lbltitle1.visible=false
	          	btnsavenewtitle1.visible=false
          	 	btnsavecancel1.visible=false   
          	elseif x.id="btnsavenewtitle2" then
          		updatetitle("2",txtuser2N.text)
          		txtuser2N.visible=false  
	           	txtuser2N.text=""
	           	lbltitle2.visible=false
	          	btnsavenewtitle2.visible=false
          	 	btnsavecancel2.visible=false   
          	elseif x.id="btnsavenewtitle3" then
          		updatetitle("3",txtuser3N.text)
          		txtuser3N.visible=false  
	           	txtuser3N.text=""
	           	lbltitle3.visible=false
	          	btnsavenewtitle3.visible=false
          	 	btnsavecancel3.visible=false  	
          	elseif x.id="btnsavenewtitle4" then
          		updatetitle("4",txtuser4N.text)
          		txtuser4N.visible=false  
	           	txtuser4N.text=""
	           	lbltitle4.visible=false
	          	btnsavenewtitle4.visible=false
          	 	btnsavecancel4.visible=false  
          	elseif x.id="btnsavenewtitle5" then
          		updatetitle("5",txtuser5N.text)
          		txtuser5N.visible=false  
	           	txtuser5N.text=""
	           	lbltitle5.visible=false
	          	btnsavenewtitle5.visible=false
          	 	btnsavecancel5.visible=false  
          	end if
          end if
          
          	 bindusertitles()
       end sub
       public function usrtitleexists() as boolean
      	      Dim strSql As String = "SELECT * from tbl_usrtitles where usrtitles_userid='" & session("userid") & "' and usrtitles_cat='" &  dd_leadtype.selecteditem.text & "'"
           		Dim sqlCmd As SqlCommand

                Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
                sqlCmd = New SqlCommand(strSql, myConnection)

                Try
                    myConnection.Open()
                    Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                    If Sqldr.Read() Then                     
                  		return true
                   else
                   		return false
                   
                    End If
                Catch SQLexc As SqlException
                    Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
                Finally
                    myConnection.Close()
                End Try
       end function
       
       
       sub insertitle()
       		Dim strSql As String = "insert into tbl_usrtitles (usrtitles_userid,usrtitles_cat) values ('" & session("userid") & "','" & dd_leadtype.selecteditem.text & "')"
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
       
       sub updatetitle(id as string, newtitle as string)
       		Dim strSql As String
       		if id = "1" then
       			strSql = "update tbl_usrtitles set usrtitles_title1='" &  newtitle & "' where usrtitles_userid='" & session("userid") & "' and usrtitles_cat='" & dd_leadtype.selecteditem.text & "'"
       		elseif id="2" then
       			strSql = "update tbl_usrtitles set usrtitles_title2='" &  newtitle & "' where usrtitles_userid='" & session("userid") & "' and usrtitles_cat='" & dd_leadtype.selecteditem.text & "'"
       		elseif id="3" then
       			strSql = "update tbl_usrtitles set usrtitles_title3='" &  newtitle & "' where usrtitles_userid='" & session("userid") & "' and usrtitles_cat='" & dd_leadtype.selecteditem.text & "'"
       		elseif id="4" then
       			strSql = "update tbl_usrtitles set usrtitles_title4='" &  newtitle & "' where usrtitles_userid='" & session("userid") & "' and usrtitles_cat='" & dd_leadtype.selecteditem.text & "'"
       		elseif id="5" then
       			strSql = "update tbl_usrtitles set usrtitles_title5='" &  newtitle & "' where usrtitles_userid='" & session("userid") & "' and usrtitles_cat='" & dd_leadtype.selecteditem.text & "'"
       		
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
       
        
        sub bindusertitles()
        
          Dim strUID As String = Session("userid")
                Dim strSql As String = "SELECT * from tbl_usrtitles where usrtitles_userid='" & session("userid") & "' and usrtitles_cat='" & dd_leadtype.selecteditem.text & "'"
                Dim sqlCmd As SqlCommand

                Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
                sqlCmd = New SqlCommand(strSql, myConnection)

                Try
                    myConnection.Open()
                    Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                    If Sqldr.Read() Then
                        If Sqldr("usrtitles_title1") IsNot DBNull.Value  Then
                        	dim ut1 as string = sqldr("usrtitles_title1")
                        	if ut1.length > 0 then
                            	lbtxtuser1.text = Sqldr("usrtitles_title1")
                           else
                           	lbtxtuser1.text = "User 1"
                           end if
                        else 
                        	lbtxtuser1.text = "User 1"
                        End If
                        If Sqldr("usrtitles_title2") IsNot DBNull.Value  Then
                        	dim ut2 as string = sqldr("usrtitles_title2")
                        	if ut2.length > 0 then
                            	lbtxtuser2.text = Sqldr("usrtitles_title2")
                           else
                           	lbtxtuser2.text = "User 2"
                           end if
                        else 
                        	lbtxtuser2.text = "User 2"
                        End If
                        If Sqldr("usrtitles_title3") IsNot DBNull.Value  Then
                        	dim ut3 as string = sqldr("usrtitles_title3")
                        	if ut3.length > 0 then
                            	lbtxtuser3.text = Sqldr("usrtitles_title3")
                           else
                           	lbtxtuser3.text = "User 3"
                           end if
                        else 
                        	lbtxtuser3.text = "User 3"
                        End If
                        If Sqldr("usrtitles_title4") IsNot DBNull.Value  Then
                        	dim ut4 as string = sqldr("usrtitles_title4")
                        	if ut4.length > 0 then
                            	lbtxtuser4.text = Sqldr("usrtitles_title4")
                           else
                           	lbtxtuser4.text = "User 4"
                           end if
                        else 
                        	lbtxtuser4.text = "User 4"
                        End If
                        If Sqldr("usrtitles_title5") IsNot DBNull.Value  Then
                        	dim ut5 as string = sqldr("usrtitles_title5")
                        	if ut5.length > 0 then
                            	lbtxtuser5.text = Sqldr("usrtitles_title5")
                           else
                           	lbtxtuser5.text = "User 5"
                           end if
                        else 
                        	lbtxtuser5.text = "User 5"
                        End If
                        
                  

                    End If
                Catch SQLexc As SqlException
                    Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
                Finally
                    myConnection.Close()
                End Try
        
        end sub
        
        Sub btn_savesrch(ByVal sender As Object, ByVal e As EventArgs)
            If session("srchcriteria") = "Existing" Then
                insertdbsearch("sp_updateprofilesearch")
            Else
                insertdbsearch("sp_insertprofilesearch")
            End If

        End Sub

        Sub insertdbsearch(ByVal sp_procedure)

            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String
            sqlproc = sp_procedure

            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

            'if sp_procedure = "sp_updateprofilesearch" then
            Dim prmleadno As New SqlParameter("@leadno", SqlDbType.Int)
            prmleadno.Value = Request.QueryString("id")
            myCommandADD.Parameters.Add(prmleadno)
            'end if


            Dim prmcity As New SqlParameter("@city", SqlDbType.VarChar, 50)
            If chkcity.Checked Then
                prmcity.Value = "Y"
            Else
                prmcity.Value = "N"
            End If
            myCommandADD.Parameters.Add(prmcity)

            Dim prmcredit As New SqlParameter("@credit", SqlDbType.VarChar, 50)
            If chkcredit.Checked Then
                prmcredit.Value = "Y"
            Else
                prmcredit.Value = "N"
            End If
            myCommandADD.Parameters.Add(prmcredit)

            Dim prmmovedt As New SqlParameter("@movedt", SqlDbType.VarChar, 50)
            If chkmovedt.Checked Then
                prmmovedt.Value = "Y"
            Else
                prmmovedt.Value = "N"
            End If
            myCommandADD.Parameters.Add(prmmovedt)

            Dim prmrent As New SqlParameter("@rent", SqlDbType.VarChar, 50)
            If chkrent.Checked Then
                prmrent.Value = "Y"
            Else
                prmrent.Value = "N"
            End If
            myCommandADD.Parameters.Add(prmrent)

            Dim prmbed As New SqlParameter("@bed", SqlDbType.VarChar, 50)
            If chkbed.Checked Then
                prmbed.Value = "Y"
            Else
                prmbed.Value = "N"
            End If
            myCommandADD.Parameters.Add(prmbed)

            Dim prmbath As New SqlParameter("@bath", SqlDbType.VarChar, 50)
            If chkbath.Checked Then
                prmbath.Value = "Y"
            Else
                prmbath.Value = "N"
            End If
            myCommandADD.Parameters.Add(prmbath)

            Dim prmlevel As New SqlParameter("@level", SqlDbType.VarChar, 50)
            If chklevel.Checked Then
                prmlevel.Value = "Y"
            Else
                prmlevel.Value = "N"
            End If
            myCommandADD.Parameters.Add(prmlevel)

            Dim prmschoool As New SqlParameter("@school", SqlDbType.VarChar, 50)
            If chkschool.Checked Then
                prmschoool.Value = "Y"
            Else
                prmschoool.Value = "N"
            End If
            myCommandADD.Parameters.Add(prmschoool)

            Dim prmsec8 As New SqlParameter("@sec8", SqlDbType.VarChar, 50)
            If chksec8.Checked Then
                prmsec8.Value = "Y"
            Else
                prmsec8.Value = "N"
            End If
            myCommandADD.Parameters.Add(prmsec8)

            Try
                myConnectionADD.Open()
                myCommandADD.ExecuteNonQuery()
                myConnectionADD.Close()
            Catch SQLexc As SqlException
                Response.Write("Insert Failed. Error Details are: " & SQLexc.ToString())
            End Try

        End Sub



        Sub btn_srchexit(ByVal sender As Object, ByVal e As EventArgs)
            pnlmatch.Visible = False
            pnlprofile.Visible = True

        End Sub


        Sub btn_profile(ByVal sender As Object, ByVal e As EventArgs)
            If Request.QueryString("action") = "new" Then
                session("pstatus") = "Draft"
                getnewleadno()
                getagentUID()
                insertdb()
                Response.Redirect("addlead.aspx?action=view&id=" & session("newleadno") & "&source=profile")
            End If

            subnav("PropertyProfile")

        End Sub


        Sub btn_tasks(ByVal sender As Object, ByVal e As EventArgs)
             If Request.QueryString("action") = "new" Then
             	if dd_status.selecteditem.text="Draft" then
           			session("pstatus") = "Draft"
                	getagentUID()
                	insertdb()
              	else
           			btn_saveDB()
            	end if
            	getleadno()
               response.redirect("addlead.aspx?action=view&source=task&id=" & session("currentleadno") )
            else
           		subnav("Tasks")
            
            end if

        End Sub
        Sub btn_exportq(ByVal sender As Object, ByVal e As EventArgs)
            subnav("queue")

        End Sub

        Sub btn_questions(ByVal sender As Object, ByVal e As EventArgs)
            'if l_questions.text="Questions" then
            '		l_questions.text="Hide Questions"
            '		if dd_leadtype.SelectedItem.Text="Tenant" then 
            '			pnlnoquestions.visible = false
            '			pnlquestions.visible=true  
            '		else	
            '			pnlnoquestions.visible = true
            '			pnlquestions.visible=false
            '		end if  
            '	else
            '			l_questions.text="Questions"
            '			pnlnoquestions.visible = false
            '			pnlquestions.visible=false
            '	end if
        End Sub

        Sub btn_email(ByVal sender As Object, ByVal e As EventArgs)
            If pnlplaceholderA.Visible = True Then
                pnlplaceholderA.Visible = False
            Else
                pnlplaceholderA.Visible = True
            End If

        End Sub

        Sub btn_emailGo(ByVal sender As Object, ByVal e As EventArgs)

            If l_sendemailtxt.Text <> "" Then
                sendemailcus(l_sendemailtxt.Text)
                pnlplaceholderA.Visible = False
            End If

        End Sub

        Sub btn_unassign(ByVal sender As Object, ByVal e As EventArgs)
            dd_agent.Enabled = True
            dd_status.SelectedIndex = dd_status.Items.IndexOf(dd_status.Items.FindByText("Unaccepted"))
            dd_agent.SelectedIndex = dd_agent.Items.IndexOf(dd_agent.Items.FindByText("Any"))
            lblstatusA.Text = "Unaccepted"
            session("publeadreassigned") = "Yes"
            dd_agent.BackColor = System.Drawing.Color.Yellow
        End Sub

        Sub btn_saverefer(ByVal sender As Object, ByVal e As EventArgs)

            If Request.QueryString("action") = "new" Then
                session("pstatus") = "Draft"
                getnewleadno()
                'response.write (session("newleadno"))
                getagentUID()
                insertdb()
                referinsert()
                Response.Redirect("addlead.aspx?action=view&id=" & session("newleadno"))

            Else
                session("pstatus") = dd_status.SelectedItem.Text
                getagentUID()

                insertdb()
                referinsert()
                '	response.redirect("addlead.aspx?action=view&id=" & Request.QueryString("id"))
                pnladdreferalADD.Visible = False
                pnladdreferal.Visible = True
                pnladdreferalADDOther.Visible = False
                pnladdreferalADDOther1.Visible = False
                getreferals()
            End If

        End Sub

        Sub btn_cancelrefer(ByVal sender As Object, ByVal e As EventArgs)
            pnladdreferalADD.Visible = False
            pnladdreferal.Visible = True
            pnladdreferalADDOther.Visible = False
            pnladdreferalADDOther1.Visible = False

        End Sub
        Sub bindagent()

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("Select users_tbl_pk, fname + ' ' + lname as agent from dbo.tbl_users where company_pk ='" & Session("company_pk") & "' and (status='Active' or status='Trial' or status='New') order by fname + ' ' + lname", myConnection)

            Try
                myConnection.Open()
                dd_agent.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
                dd_agent.DataBind()
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            End Try

            dd_agent.Items.Insert(1, New ListItem("Any", "9999"))
            dd_agent.Items.Insert(0, New ListItem("Unassigned", "9998"))


        End Sub

        Public Sub btn_vwcontact(ByVal sender As Object, ByVal e As EventArgs)
            Session("histfilter") = "All"
            bindLHTypeH()
            subnav("History")

        End Sub

        Public Sub btn_automatch(ByVal sender As Object, ByVal e As EventArgs)
            If l_automatch.Text = "Auto Match OFF" Then
                l_automatch.Text = "Auto Match ON"
                Updateautomatch("Y")
            Else
                l_automatch.Text = "Auto Match OFF"
                Updateautomatch("N")
            End If
        End Sub

        Public Sub btn_vwreferals(ByVal sender As Object, ByVal e As EventArgs)
            subnav("Referrals")
        End Sub

        Public Sub btn_addcontact(ByVal sender As Object, ByVal e As EventArgs)
            Response.Redirect("leadhistory.aspx?type=history&history=0&LeadNo=" & Request.QueryString("id") & "&LeadType=" & dd_leadtype.SelectedItem.Text & "&action=new")
        End Sub
        Public Sub click_addtask(ByVal sender As Object, ByVal e As EventArgs)
            Response.Redirect("leadhistory.aspx?type=task&history=0&LeadNo=" & Request.QueryString("id") & "&LeadType=" & dd_leadtype.SelectedItem.Text & "&action=new")
        End Sub

        Public Sub btn_contactfollowup(ByVal sender As Object, ByVal e As EventArgs)
            If l_contactfollowup.Text = "FollowUp Only" Then
                l_contactfollowup.Text = "All"
                Session("histfilter") = "filter"
                BindhistoryGrid()
            Else
                l_contactfollowup.Text = "FollowUp Only"
                Session("histfilter") = "All"
                BindhistoryGrid()
            End If
        End Sub

        Public Sub btn_addprofile(ByVal sender As Object, ByVal e As EventArgs)
            If Request.QueryString("action") = "view" Then
                session("pstatus") = dd_status.SelectedItem.Text
                getagentUID()
                insertdb()
                If dd_leadtype.Text = "Buyer" Or dd_leadtype.Text = "Tenant" Or dd_leadtype.Text = "Tenant Office" Then
                    Response.Redirect("leadprofile.aspx?profile=0&LeadNo=" & Request.QueryString("id") & "&LeadType=" & dd_leadtype.SelectedItem.Text & "&action=new")
                Else
                    Response.Redirect("leadprofileSell.aspx?profile=0&LeadNo=" & Request.QueryString("id") & "&LeadType=" & dd_leadtype.SelectedItem.Text & "&action=new")
                End If
            Else
                session("pstatus") = "Draft"
                getnewleadno()
                getagentUID()
                insertdb()
                If dd_leadtype.Text = "Buyer" Or dd_leadtype.Text = "Tenant" Or dd_leadtype.Text = "Tenant Office" Then
                    Response.Redirect("leadprofile.aspx?profile=0&LeadNo=" & session("newleadno") & "&LeadType=" & dd_leadtype.SelectedItem.Text & "&action=new")
                Else
                    Response.Redirect("leadprofileSell.aspx?profile=0&LeadNo=" & session("newleadno") & "&LeadType=" & dd_leadtype.SelectedItem.Text & "&action=new")
                End If

            End If

        End Sub

        Public Sub btn_savecontact(ByVal sender As Object, ByVal e As EventArgs)

            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String
            sqlproc = "sp_insertleadcontact"

            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
            Dim prmleadno As New SqlParameter("@leadfk", SqlDbType.Int)
            prmleadno.Value = Request.QueryString("id")
            myCommandADD.Parameters.Add(prmleadno)

            Dim prmcapdate As New SqlParameter("@capdate", SqlDbType.DateTime)
            If l_edate.Text = "" Then
                prmcapdate.Value = DBNull.Value
            Else
                prmcapdate.Value = DateTime.ParseExact(l_edate.Text, supportedFormats, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None)
            End If
            myCommandADD.Parameters.Add(prmcapdate)

            Dim prmnotes As New SqlParameter("@l_notes", SqlDbType.Text)
            If l_enotes.Text = "" Then
                prmnotes.Value = DBNull.Value
            Else
                prmnotes.Value = l_enotes.Text
            End If
            myCommandADD.Parameters.Add(prmnotes)

            Dim prmuid As New SqlParameter("@uid", SqlDbType.VarChar, 50)
            prmuid.Value = Session("userid")
            myCommandADD.Parameters.Add(prmuid)

            Dim prmtype As New SqlParameter("@LHType", SqlDbType.VarChar, 50)
            prmtype.Value = ddLHType.SelectedItem.Text
            myCommandADD.Parameters.Add(prmtype)

            Try
                myConnectionADD.Open()
                myCommandADD.ExecuteNonQuery()
                myConnectionADD.Close()
            Catch SQLexc As SqlException
                Response.Write("Insert Failed. Error Details are: " & SQLexc.ToString())
            End Try

            BindhistoryGrid()
            pnladdencoutner.Visible = False
            pnlviewnotes.Visible = True

        End Sub

        Sub btn_cancelcontact(ByVal sender As Object, ByVal e As EventArgs)
            pnladdencoutner.Visible = False
            pnlviewnotes.Visible = True

        End Sub

        Public Sub btn_close(ByVal sender As Object, ByVal e As EventArgs)
            dd_status.SelectedIndex = dd_status.Items.IndexOf(dd_status.Items.FindByText("Closed"))
            session("pstatus") = dd_status.SelectedItem.Text
            getagentUID()
            insertdb()

        End Sub


        Public Sub btn_acceptlead(ByVal sender As Object, ByVal e As EventArgs)
            getagentfullname()
            dd_agent.SelectedIndex = dd_agent.Items.IndexOf(dd_agent.Items.FindByText(session("pubuidfullname")))
            dd_status.SelectedIndex = dd_status.Items.IndexOf(dd_status.Items.FindByText("Accepted"))
            lblstatusA.Text = "Accepted"
            l_accept.Visible = False
            l_savedraft.Visible = False
            l_save.Visible = True
            session("pstatus") = "Accepted"
            getagentUID()
            insertdb()
            test()
            inserthistory("Lead Accepted by " & Session("userid"))
            Response.Redirect("addlead.aspx?action=view&id=" & Request.QueryString("id"))

        End Sub

        Public Sub btn_save(ByVal sender As Object, ByVal e As EventArgs)
            If Request.QueryString("action") = "new" Then
                session("strsavetype") = "exit"
            Else
                session("strsavetype") = ""
            End If
            btn_saveDB()
        End Sub

        Public Sub btn_saveexit(ByVal sender As Object, ByVal e As EventArgs)
            session("strsavetype") = "exit"
            btn_saveDB()
        End Sub

        Sub btn_saveDB()
            Dim agent As String
            If Request.QueryString("action") = "new" Then
                session("pstatus") = dd_status.SelectedItem.Text
                getnewleadno()
                'Response.Write(session("pstatus"))
                getagentUID()
                If dd_agent.SelectedItem.Text <> Session("Agentname") Then
                    If dd_status.SelectedItem.Text = "Unaccepted" Then
                        getagentemail()
                        If dd_agent.SelectedItem.Text <> "Any" Then
                            sendemailagents(session("agentemail"))
                        End If
                    End If
                End If
                If dd_agent.SelectedItem.Text = Session("Agentname") Then
                    session("pstatus") = "Accepted"
                End If
                insertdb()
                test()
                inserthistory("Lead Submitted")
                agent = dd_agent.SelectedItem.Text
                inserthistory("Lead Assigned to " & agent)
                'response.write("here")
                session("leadno")=session("newleadno")
						checkforworkflowNew()

            ElseIf Request.QueryString("action") = "view" Then
            	session("leadno")=request.querystring("id")
                session("pstatus") = dd_status.SelectedItem.Text
                If session("pstatus") = "Draft" Then
                    If dd_agent.SelectedItem.Text = Session("Agentname") Then
                        session("pstatus") = "Accepted"
                    Else
                        session("pstatus") = "Unaccepted"
                        getagentemail()
                        If dd_agent.SelectedItem.Text <> "Any" Then
                            sendemailagents(session("agentemail"))
                        End If
                    End If
                    inserthistory("Lead Submitted")
                    agent = dd_agent.SelectedItem.Text
                    inserthistory("Lead Assigned to " & agent)
                    session("strsavetype") = "exit"
                End If
                If session("publeadreassigned") = "Yes" Then
                    If dd_agent.SelectedItem.Text <> "Any" Then
                        'session("pstatus")= "Accepted"
                        getagentemail()
                        sendemailagents(session("agentemail"))
                    End If
                    agent = dd_agent.SelectedItem.Text
                    inserthistory("Lead Re-Assigned to " & agent)
                    session("publeadreassigned") = "No"
                End If
                If dd_ldstat.SelectedItem.Text = "Referred Out" Or dd_ldstat.SelectedItem.Text = "Sold Out" Then
                    session("pstatus") = "Accepted"
                    inserthistory("Lead Sold or Referred")
                End If

                getagentUID()
						'assignedagentid=session("userid")
                insertdb()
                'test()
                
                checkforworkflowSch()
                
                
                'response.write(session("oleadtype"))
                'response.write(dd_leadtype.selecteditem.text)
                if  session("oleadtype") <> dd_leadtype.selecteditem.text
                		'response.write("here1")
               		checkforworkflowLT()
						end if
                
                if session("oleadprog") <>dd_leadprogram.selecteditem.text
               	 checkforworkflowLP()
                end if
                
                if session("oleadstatus") <> dd_ldstat.selecteditem.text
                	checkforworkflowLS()
                end if
                
                if session("omarketprog") <>dd_mkprg.selecteditem.text
                	checkforworkflowMP()
                end if
                
                
            End If

            If session("strsavetype") = "exit" Then
                session("strsavetype") = ""
                If Request.QueryString("source") = "home" Then
                    Response.Redirect("default.aspx")
                Else
                    Response.Redirect(Session("qstring"))
                End If

            Else
                session("strsavetype") = ""
            End If
        End Sub

        Public Sub btn_savedraft(ByVal sender As Object, ByVal e As EventArgs)
            session("pstatus") = "Draft"

            If Request.QueryString("action") = "new" Then

                getnewleadno()

                'response.write (session("newleadno"))
                getagentUID()
                insertdb()
            ElseIf Request.QueryString("action") = "view" Then
                getagentUID()
                insertdb()
            End If
            Response.Redirect(Session("qstring"))
        End Sub

        Public Sub btn_delete(ByVal Source As Object, ByVal e As ImageClickEventArgs)

            Dim strpropID As String = Request.QueryString("id")
            Dim strSql As String = "insert into dbo.tbl_leadsDeleted select * from dbo.tbl_leads Where tbl_leads_pk=" & strpropID
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

            strSql = "delete from dbo.tbl_leads Where tbl_leads_pk=" & strpropID
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
            deletereferal()
            deletetasks()
            deletecontacthistory()
            If Request.QueryString("source") = "home" Then
                Response.Redirect("default.aspx")
            Else
            	if Session("qstring") isnot nothing then
                	Response.Redirect(Session("qstring"))
               else
               	Response.Redirect("leads.aspx?search=*&leadtype=*&status=*&constatus=*&assignedto=*&assignedby=*&adcode=*&source=nav&entrysource=*")
               end if
            End If

        End Sub

        Sub deletetasks()
            Dim strpropID As String = Request.QueryString("id")
            Dim strSql As String = "insert into dbo.tbl_tasksuserDeleted  select * from dbo.tbl_tasksuser Where lt_leadpk_fk=" & strpropID

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

            strSql = "delete from dbo.tbl_tasksuser Where lt_leadpk_fk=" & strpropID

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

        Sub deletecontacthistory()
            Dim strpropID As String = Request.QueryString("id")
            Dim strSql As String = "insert into dbo.tbl_leadscontacthistoryDeleted select * from dbo.tbl_leadscontacthistory Where tbl_leads_fk=" & strpropID

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

            strSql = "delete from dbo.tbl_leadscontacthistory Where tbl_leads_fk=" & strpropID

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

        Sub deletereferal()
            Dim strpropID As String = Request.QueryString("id")
            Dim strSql As String = "insert into dbo.tbl_referalsDeleted select * from  dbo.tbl_referals Where refer_lead_fk=" & strpropID

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

            strSql = "delete from dbo.tbl_referals Where refer_lead_fk=" & strpropID

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

        Public Sub btn_cancel(ByVal sender As Object, ByVal e As EventArgs)
            If Request.QueryString("source") = "home" Then
                Response.Redirect("default.aspx")
            Elseif Request.QueryString("source") = "wfsetup" Then
            	Response.Redirect("addeditwf.aspx?action=view&id=" & request.querystring("wfpk") & "&nav=wfsdetails")
            else
                If Session("qstring") = "" Then
                    Response.Redirect("leads.aspx?search=*&leadtype=*&status=*&constatus=*&assignedto=*&assignedby=*")
                Else
                    Response.Redirect(Session("qstring"))
                End If
            End If
        End Sub

        Public Sub btn_continue(ByVal sender As Object, ByVal e As EventArgs)
            Response.Redirect(Session("qstring"))
        End Sub
        Public Sub exportlead(ByVal Source As Object, ByVal e As ImageClickEventArgs)
            Response.Redirect("exportleads.aspx?type=single&leadno=" & Request.QueryString("id") & "&source=single")
        End Sub

        Sub Updateautomatch(ByVal value As String)
            Dim automatchval As String = value
            Dim strSql As String = "update tbl_leads set ld_autopropmatch ='" & automatchval & "' where tbl_leads_pk=" & Request.QueryString("id")
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
        End Sub

        Sub insertdb()
            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String

            If Request.QueryString("action") = "new" Then
                sqlproc = "sp_addlead"
            ElseIf Request.QueryString("action") = "view" Then
                sqlproc = "sp_updatelead"
            End If
            'response.write("OK")

            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
            If Request.QueryString("action") = "new" Then
                Dim prmleadno As New SqlParameter("@newleadno", SqlDbType.Int)
                prmleadno.Value = session("newleadno")
                myCommandADD.Parameters.Add(prmleadno)

                Dim prmuid As New SqlParameter("@uid", SqlDbType.VarChar, 50)
                prmuid.Value = Session("userid")
                myCommandADD.Parameters.Add(prmuid)
            End If

            Dim prmlfname As New SqlParameter("@l_fname", SqlDbType.VarChar, 50)
            If l_fname.Text = "" Then
                prmlfname.Value = DBNull.Value
            Else
                prmlfname.Value = l_fname.Text
            End If
            myCommandADD.Parameters.Add(prmlfname)

            Dim prmllname As New SqlParameter("@l_lname", SqlDbType.VarChar, 50)
            If l_lname.Text = "" Then
                prmllname.Value = DBNull.Value
            Else
                prmllname.Value = l_lname.Text
            End If
            myCommandADD.Parameters.Add(prmllname)

            Dim prmhphone As New SqlParameter("@l_hphone", SqlDbType.VarChar, 50)
            If l_hphone.Text = "" Then
                prmhphone.Value = DBNull.Value
            Else
                prmhphone.Value = l_hphone.Text
            End If
            myCommandADD.Parameters.Add(prmhphone)

            Dim prmcphone As New SqlParameter("@l_cphone", SqlDbType.VarChar, 50)
            If l_cphone.Text = "" Then
                prmcphone.Value = DBNull.Value
            Else
                prmcphone.Value = l_cphone.Text
            End If
            myCommandADD.Parameters.Add(prmcphone)

            Dim prmaddress As New SqlParameter("@l_address", SqlDbType.VarChar, 30)
            If l_address.Text = "" Then
                prmaddress.Value = DBNull.Value
            Else
                prmaddress.Value = l_address.Text
            End If
            myCommandADD.Parameters.Add(prmaddress)

            Dim prmcity As New SqlParameter("@l_city", SqlDbType.VarChar, 30)
            If l_city.Text = "" Then
                prmcity.Value = DBNull.Value
            Else
                prmcity.Value = l_city.Text
            End If
            myCommandADD.Parameters.Add(prmcity)

            Dim prmstate As New SqlParameter("@l_state", SqlDbType.VarChar, 2)
            If l_state.Text = "" Then
                prmstate.Value = DBNull.Value
            Else
                prmstate.Value = l_state.Text
            End If
            myCommandADD.Parameters.Add(prmstate)

            Dim prmzip As New SqlParameter("@l_zip", SqlDbType.VarChar, 50)
            If l_zip.Text = "" Then
                prmzip.Value = DBNull.Value
            Else
                prmzip.Value = l_zip.Text
            End If
            myCommandADD.Parameters.Add(prmzip)

            Dim prmagent As New SqlParameter("@l_agent", SqlDbType.VarChar, 30)
            prmagent.Value = dd_agent.SelectedItem.Text
            myCommandADD.Parameters.Add(prmagent)

            Dim prmagentFK As New SqlParameter("@l_agent_FK", SqlDbType.VarChar, 30)
            prmagentFK.Value = dd_agent.SelectedItem.Value
            myCommandADD.Parameters.Add(prmagentFK)

            Dim prmstatus As New SqlParameter("@l_status", SqlDbType.VarChar, 30)
            prmstatus.Value = session("pstatus")
            myCommandADD.Parameters.Add(prmstatus)

            Dim prmleadtype As New SqlParameter("@l_leadtype", SqlDbType.VarChar, 30)
            prmleadtype.Value = dd_leadtype.SelectedItem.Text
            myCommandADD.Parameters.Add(prmleadtype)

            Dim prmnotes As New SqlParameter("@l_notes", SqlDbType.Text)
            If l_notes.content = "" Then
                prmnotes.Value = DBNull.Value
            Else
                prmnotes.Value = l_notes.content
            End If
            myCommandADD.Parameters.Add(prmnotes)

            Dim prmemail As New SqlParameter("@l_email", SqlDbType.VarChar, 50)
            prmemail.Value = l_email.Text
            myCommandADD.Parameters.Add(prmemail)

            Dim prmemail2 As New SqlParameter("@l_email2", SqlDbType.VarChar, 50)
            prmemail2.Value = l_email2.Text
            myCommandADD.Parameters.Add(prmemail2)


            Dim prmadddate As New SqlParameter("@adddate", SqlDbType.DateTime)
            prmadddate.Value = RightNowAdd
            myCommandADD.Parameters.Add(prmadddate)

            If Request.QueryString("action") = "view" Then
                Dim prmtblleadpk As New SqlParameter("@ld_tbl_pk", SqlDbType.Int)
                prmtblleadpk.Value = Request.QueryString("id")
                myCommandADD.Parameters.Add(prmtblleadpk)
            End If

            Dim prmcapdate As New SqlParameter("@capdate", SqlDbType.DateTime)
            If l_capdate.Text = "" Then
                prmcapdate.Value = DBNull.Value
            Else
                prmcapdate.Value = l_capdate.Text
                'DateTime.ParseExact(l_capdate.Text, supportedFormats, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None)
            End If
            myCommandADD.Parameters.Add(prmcapdate)

            Dim prmapptdate As New SqlParameter("@apptdate", SqlDbType.DateTime)
            prmapptdate.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmapptdate)

            Dim prmappttime As New SqlParameter("@appttime", SqlDbType.VarChar, 5)
            If l_appttime.Text = "" Then
                prmappttime.Value = DBNull.Value
            Else
                prmappttime.Value = l_appttime.Text
            End If
            myCommandADD.Parameters.Add(prmappttime)

            Dim prmapptloc As New SqlParameter("@apptloc", SqlDbType.VarChar, 30)
            prmapptloc.Value = dd_apptloc.SelectedItem.Value
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
            If l_comp.Text = "" Then
                prmcomp.Value = DBNull.Value
            Else
                prmcomp.Value = l_comp.Text
            End If
            myCommandADD.Parameters.Add(prmcomp)

            Dim prmassignedagent As New SqlParameter("@assignedagent", SqlDbType.VarChar, 50)
            prmassignedagent.Value = session("assignedagentid")
            myCommandADD.Parameters.Add(prmassignedagent)

            Dim prmhighpri As New SqlParameter("@highpri", SqlDbType.VarChar, 5)
            prmhighpri.Value = dd_highpri.SelectedItem.Value
            myCommandADD.Parameters.Add(prmhighpri)

            Dim prmldsource As New SqlParameter("@leadsource", SqlDbType.VarChar, 50)
            prmldsource.Value = dd_source.SelectedItem.Text
            myCommandADD.Parameters.Add(prmldsource)

            Dim prmadcode As New SqlParameter("@adcode", SqlDbType.VarChar, 50)
            prmadcode.Value = l_adcode.Text
            myCommandADD.Parameters.Add(prmadcode)

            Dim prmmailtoaddress As New SqlParameter("@mailtoaddress", SqlDbType.VarChar, 50)
            If l_mailto.Checked Then
                prmmailtoaddress.Value = "Y"
            Else
                prmmailtoaddress.Value = "N"
            End If
            myCommandADD.Parameters.Add(prmmailtoaddress)

            Dim prmproptolist As New SqlParameter("@proplist", SqlDbType.VarChar, 50)
            prmproptolist.Value = "N"
            myCommandADD.Parameters.Add(prmproptolist)

            Dim prmpstatus As New SqlParameter("@ld_pstatus", SqlDbType.VarChar, 50)
            prmpstatus.Value = dd_ldstat.SelectedItem.Text
            myCommandADD.Parameters.Add(prmpstatus)

            Dim prmprogram As New SqlParameter("@ld_program", SqlDbType.VarChar, 50)
            prmprogram.Value = dd_leadprogram.SelectedItem.Text
            myCommandADD.Parameters.Add(prmprogram)

            Dim prmstatdetail As New SqlParameter("@ld_statdetail", SqlDbType.VarChar, 50)
            If l_web.text = "" Then
                prmstatdetail.Value = DBNull.Value
            Else
                prmstatdetail.Value = l_web.text

            End If
            
            myCommandADD.Parameters.Add(prmstatdetail)

            Dim prmentrysource As New SqlParameter("@ld_entrysource", SqlDbType.VarChar, 50)
            prmentrysource.Value = session("leadentrysource")
            myCommandADD.Parameters.Add(prmentrysource)

            Dim prmfax As New SqlParameter("@ld_fax", SqlDbType.VarChar, 50)
            If l_fax.Text = "" Then
                prmfax.Value = DBNull.Value
            Else
                prmfax.Value = l_fax.Text
            End If
            myCommandADD.Parameters.Add(prmfax)

 				Dim prmmkprg As New SqlParameter("@marketprog", SqlDbType.VarChar, 50)
            prmmkprg.Value = dd_mkprg.SelectedItem.Text
            myCommandADD.Parameters.Add(prmmkprg)
            
            Dim prmmkto As New SqlParameter("@marketto", SqlDbType.VarChar, 50)
            prmmkto.Value = dd_mkto.SelectedItem.Text
            myCommandADD.Parameters.Add(prmmkto)
            
             Dim prmext1 As New SqlParameter("@ld_ext1", SqlDbType.VarChar, 50)
            prmext1.Value = l_hext.Text
            myCommandADD.Parameters.Add(prmext1)
            
            Try
                myConnectionADD.Open()
                myCommandADD.ExecuteNonQuery()
                myConnectionADD.Close()
            Catch SQLexc As SqlException
                Response.Write("Insert Failed. Error Details are: " & SQLexc.ToString())
            End Try

				saveaddcontacts()
				saveusercontacts()
        End Sub
        
            
         sub saveaddcontacts()
        		Dim strUID As String = Session("userid")
            Dim strSql As String 
            Dim sqlCmd As SqlCommand
        		if ADDcontactexists() then       			
        			
        		  strSql  = "update tbl_leadsAddContact set ldai_pfaxno='" & l_fax.text & "',ldai_pwebsite='" & l_web.text & "', " _
        		            & "ldai_pcotitle='" & co_title.text & "',ldai_pconame='" & co_name.text & "',ldai_pcophone='" & co_phone.text & "', " _
        		            & "ldai_pcowebsite='" & co_web.text & "',ldai_sname='" & Sco_name.text & "',ldai_sphone1='" & sco_phone1.text & "', " _
        		           	& "ldai_sphone2='" & sco_phone2.text & "',ldai_saddress='" & sco_address.text & "',ldai_scity='" & sco_city.text & "', " _
        		           	& "ldai_sstate='" & sco_state.text & "',ldai_szip='" & sco_zip.text & "' " _
        		            & "where ldai_lead_fk='" &  request.querystring("id") & "' and ldai_userid_fk='" & session("userid") & "'"
        		           
        		else
        		   strSql = "insert into tbl_leadsAddContact (ldai_lead_fk,ldai_userid_fk,ldai_pfaxno,ldai_pwebsite,ldai_pcotitle,ldai_pconame,ldai_pcophone,ldai_pcowebsite, " _
        		   			& "ldai_sname,ldai_sphone1,ldai_sphone2,ldai_saddress,ldai_scity,ldai_sstate,ldai_szip) " _
        		             & "values ('" & request.querystring("id") & "','" & session("userid") & "','" & l_fax.text & "','" & l_web.text & "', " _
        		             & "'" & co_title.text & "','" & co_name.text & "','" & co_phone.text & "', " _
        		             & "'" & co_web.text & "','" & Sco_name.text & "','" & sco_phone1.text & "', " _
        		             & "'" & sco_phone2.text & "','" & sco_address.text & "','" & sco_city.text & "', " _
        		             & "'" & sco_state.text & "','" & sco_zip.text & "')"  
        		end if
        		

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
        
        sub saveusercontacts()
        		Dim strUID As String = Session("userid")
            Dim strSql As String 
            Dim sqlCmd As SqlCommand
        		if usercontactexists() then
        			'response.write("H1")
        			'response.write(request.querystring("id"))
        			'response.write(session("userid"))
        			'response.write(dd_leadtype.selecteditem.text)
        			
        		  strSql  = "update tbl_leadsAddUser set ldaui_userfield1='" & txtuser1.text & "',ldaui_userfield2='" & txtuser2.text & "', " _
        		            & "ldaui_userfield3='" & txtuser3.text & "',ldaui_userfield4='" & txtuser4.text & "',ldaui_userfield5='" & txtuser5.text & "' " _
        		            & "where ldaui_lead_fk='" &  request.querystring("id") & "' and ldaui_userid_fk='" & session("userid") & "' " _
        		            & "and ldaui_leadtype='" & dd_leadtype.selecteditem.text & "'"
        		else
        		   strSql = "insert into tbl_leadsAddUser (ldaui_lead_fk,ldaui_userid_fk,ldaui_leadtype,ldaui_userfield1,ldaui_userfield2,ldaui_userfield3,ldaui_userfield4,ldaui_userfield5) " _
        		             & "values ('" & request.querystring("id") & "','" & session("userid") & "','" & dd_leadtype.selecteditem.text & "','" & txtuser1.text & "','" & txtuser2.text & "','" & txtuser3.text & "','" & txtuser4.text & "','" & txtuser5.text & "')"
        		end if
        		

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
        
        
          public function ADDcontactexists()  as boolean
        		Dim strUID As String = Session("userid")
            Dim strSql As String 
            Dim sqlCmd As SqlCommand
        		
        		   strSql = "select * from tbl_leadsAddContact where ldai_lead_fk='" & request.querystring("id") & "' and ldai_userid_fk='" & session("userid") & "'"
        		
        		

                Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
                sqlCmd = New SqlCommand(strSql, myConnection)

                Try
                    myConnection.Open()
                    Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                    If Sqldr.Read() Then
                        return true
                    else
                    		return false

                    End If
                Catch SQLexc As SqlException
                    Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
                Finally
                    myConnection.Close()
                End Try
        
        end function
        
        
        public function usercontactexists() as boolean
        		Dim strUID As String = Session("userid")
            Dim strSql As String 
            Dim sqlCmd As SqlCommand
        		
        		   strSql = "select * from tbl_leadsAddUser where ldaui_lead_fk='" & request.querystring("id") & "' and ldaui_userid_fk='" & session("userid") & "' and  ldaui_leadtype='" & dd_leadtype.selecteditem.text & "'"
        		
        		

                Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
                sqlCmd = New SqlCommand(strSql, myConnection)

                Try
                    myConnection.Open()
                    Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                    If Sqldr.Read() Then
                        return true
                    else
                    		return false

                    End If
                Catch SQLexc As SqlException
                    Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
                Finally
                    myConnection.Close()
                End Try
        
        end function
        

        Sub addtasks()
            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String

            If Request.QueryString("action") = "new" Or session("strnotask") = "True" Then
                sqlproc = "sp_addleadtask"
            Else
                sqlproc = "sp_updateleadtask"
            End If

            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
            If Request.QueryString("action") = "new" Then
                Dim prmleadno As New SqlParameter("@leadfk", SqlDbType.Int)
                prmleadno.Value = session("newleadno")
                myCommandADD.Parameters.Add(prmleadno)
            Else
                Dim prmleadno As New SqlParameter("@leadfk", SqlDbType.Int)
                prmleadno.Value = Request.QueryString("id")
                myCommandADD.Parameters.Add(prmleadno)
            End If

            Dim prmleadtype As New SqlParameter("@leadtype", SqlDbType.VarChar, 30)
            prmleadtype.Value = dd_leadtype.SelectedItem.Text
            myCommandADD.Parameters.Add(prmleadtype)


            Dim prmscc As New SqlParameter("@scc", SqlDbType.VarChar, 6)
            prmscc.Value = "No"
            myCommandADD.Parameters.Add(prmscc)

            Dim prmwls As New SqlParameter("@wls", SqlDbType.VarChar, 6)
            prmwls.Value = "No"
            myCommandADD.Parameters.Add(prmwls)

            Dim prmnpp As New SqlParameter("@npp", SqlDbType.VarChar, 6)
            prmnpp.Value = "No"
            myCommandADD.Parameters.Add(prmnpp)

            Dim prmlsc As New SqlParameter("@lsc", SqlDbType.VarChar, 6)
            prmlsc.Value = "No"
            myCommandADD.Parameters.Add(prmlsc)

            Dim prmcwks As New SqlParameter("@cwks", SqlDbType.VarChar, 6)
            prmcwks.Value = "No"
            myCommandADD.Parameters.Add(prmcwks)

            Dim prmclp As New SqlParameter("@clp", SqlDbType.VarChar, 6)
            prmclp.Value = "No"
            myCommandADD.Parameters.Add(prmclp)

            Dim prmclpo As New SqlParameter("@clpo", SqlDbType.VarChar, 6)
            prmclpo.Value = "No"
            myCommandADD.Parameters.Add(prmclpo)

            If Request.QueryString("action") = "new" Or session("strnotask") = "True" Then
                Dim prmadddate As New SqlParameter("@adddate", SqlDbType.DateTime)
                prmadddate.Value = RightNowAdd
                myCommandADD.Parameters.Add(prmadddate)
            End If

            Try
                myConnectionADD.Open()
                myCommandADD.ExecuteNonQuery()
                myConnectionADD.Close()
            Catch SQLexc As SqlException
                Response.Write("Insert Failed. Error Details are: " & SQLexc.ToString())
            End Try

            session("strnotask") = "false"
        End Sub

        Public Sub getagentemail()
            If dd_agent.SelectedItem.Text <> "Any" Then
                Dim strUID As String = Session("userid")
                Dim strSql As String = "SELECT * from tbl_users where users_tbl_pk='" & dd_agent.SelectedItem.Value & "'"
                Dim sqlCmd As SqlCommand

                Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
                sqlCmd = New SqlCommand(strSql, myConnection)

                Try
                    myConnection.Open()
                    Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                    If Sqldr.Read() Then
                        If Sqldr("email") IsNot DBNull.Value Then
                            session("agentemail") = Sqldr("email2")
                        End If

                    End If
                Catch SQLexc As SqlException
                    Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
                Finally
                    myConnection.Close()
                End Try
            Else
                session("agentemail") = "BPONotifications@gochoiceone.com"
            End If
        End Sub

        Public Sub sendemailagents(ByVal mail_to As String)
            'notificationemail = bpo_email.text
            Dim mail As New MailMessage()
            Dim prefered As String
            Dim mailleadno As Integer
            'Set the properties - send the email to the person who filled out the
            mail.From = New MailAddress("mychoice@gochoiceone.com")
            mail.To.Add(mail_to)
            'mail.cc.add("sbialas@gochoiceone.com")
            If Request.QueryString("action") = "new" Then
                mailleadno = session("newleadno")
            Else
                mailleadno = Request.QueryString("id")
            End If
            If dd_agent.SelectedItem.Text = "Any" Then
                If dd_highpri.Text = "Yes" Then
                    mail.Subject = "New Lead Notification- Open to all"
                Else
                    mail.Subject = "New Lead Notification"
                End If
            Else
                If dd_highpri.Text = "Yes" Then
                    mail.Subject = "New Lead Notification-HIGH PRIORITY"
                Else
                    mail.Subject = "New Lead Notification"
                End If
            End If
            'Set the body
            If dd_agent.SelectedItem.Text = "Any" Then
                prefered = "There is not a preferred Agent and this Lead is open for all agents."
            Else
                prefered = "There is a preferred Agent for this Lead.  IF that Agent does not accept within 30 minutes then it will become available to all Agents."
            End If

            mail.Body = "Below you will find the details for this lead. " & vbCrLf & _
                       "____________________________________________________" & vbCrLf & vbCrLf & _
                       "Lead Information" & vbCrLf & _
                       "-------------------" & vbCrLf & _
                       "Lead #: " & mailleadno & vbCrLf & _
                       "Lead Type:   " & dd_leadtype.SelectedItem.Text & vbCrLf & _
                       "Lead Date:   " & l_capdate.Text & vbCrLf & _
                       "Name:        " & l_fname.Text & " " & l_lname.Text & vbCrLf & _
                       "Home Phone:  " & l_hphone.Text & vbCrLf & _
                       "Cell Phone:  " & l_cphone.Text & vbCrLf & _
                       "Email:       " & l_email.Text & vbCrLf & _
                       "Other Email: " & l_email2.Text & vbCrLf & _
                       "Notes:       " & vbCrLf & _
                       l_notes.content

            'send the message
            Dim smtp As New SmtpClient("smtp.comcast.net")
            smtp.Send(mail)

        End Sub
  
  			 Sub   bindacfields()
  			 
  			  Dim strUID As String = Session("userid")
            Dim strSql As String = "select * from tbl_leadsAddContact where ldai_lead_fk='" & request.querystring("id") & "' and ldai_userid_fk='" & session("userid") & "'"  
            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                                   
                    If Sqldr("ldai_pfaxno") IsNot DBNull.Value Then
                        l_fax.text = 	Sqldr("ldai_pfaxno")
                    End If
                    If Sqldr("ldai_pwebsite") IsNot DBNull.Value Then
                        l_web.text = 	Sqldr("ldai_pwebsite")
                    End If
                    If Sqldr("ldai_pcotitle") IsNot DBNull.Value Then
                        co_title.text = 	Sqldr("ldai_pcotitle")
                    End If
                    If Sqldr("ldai_pconame") IsNot DBNull.Value Then
                        co_name.text = 	Sqldr("ldai_pconame")
                    End If
                    
                    If Sqldr("ldai_pcophone") IsNot DBNull.Value Then
                        co_phone.text = 	Sqldr("ldai_pcophone")
                    End If
                    If Sqldr("ldai_pcowebsite") IsNot DBNull.Value Then
                        co_web.text = 	Sqldr("ldai_pcowebsite")
                    End If
                    If Sqldr("ldai_sname") IsNot DBNull.Value Then
                        Sco_name.text = 	Sqldr("ldai_sname")
                    End If
                    If Sqldr("ldai_sphone1") IsNot DBNull.Value Then
                        sco_phone1.text = 	Sqldr("ldai_sphone1")
                    End If
                    If Sqldr("ldai_sphone2") IsNot DBNull.Value Then
                        sco_phone2.text = 	Sqldr("ldai_sphone2")
                    End If
                    If Sqldr("ldai_saddress") IsNot DBNull.Value Then
                        sco_address.text = 	Sqldr("ldai_saddress")
                    End If
                    If Sqldr("ldai_scity") IsNot DBNull.Value Then
                        sco_city.text = 	Sqldr("ldai_scity")
                    End If
                 
                     If Sqldr("ldai_sstate") IsNot DBNull.Value Then
                        sco_state.text = 	Sqldr("ldai_sstate")
                    End If
                    
                     If Sqldr("ldai_szip") IsNot DBNull.Value Then
                        sco_zip.text = 	Sqldr("ldai_szip")
                    End If
  			 
  			    End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
  			 
  			 
  			 end sub
  			 

  
  			 Sub  bindufields()
  			 
  			  Dim strUID As String = Session("userid")
            Dim strSql As String = "select * from tbl_leadsAddUser where ldaui_lead_fk='" & request.querystring("id") & "' and ldaui_userid_fk='" & session("userid") & "' and ldaui_leadtype='" & dd_leadtype.selecteditem.text & "'"  
            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                                   
                    If Sqldr("ldaui_userfield1") IsNot DBNull.Value Then
                        txtuser1.text = 	Sqldr("ldaui_userfield1")
                    End If
                    If Sqldr("ldaui_userfield2") IsNot DBNull.Value Then
                        txtuser2.text = 	Sqldr("ldaui_userfield2")
                    End If
                    If Sqldr("ldaui_userfield3") IsNot DBNull.Value Then
                        txtuser3.text = 	Sqldr("ldaui_userfield3")
                    End If
                    If Sqldr("ldaui_userfield4") IsNot DBNull.Value Then
                        txtuser4.text = 	Sqldr("ldaui_userfield4")
                    End If
                    If Sqldr("ldaui_userfield5") IsNot DBNull.Value Then
                        txtuser5.text = 	Sqldr("ldaui_userfield5")
                    End If
  			 
  			    End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
  			 
  			 
  			 end sub
  			 







        Sub bindfields()
            Dim strUID As String = Session("userid")
            Dim strSql As String = "SELECT fname + ' ' + lname as assignedby, " _
          				& "case when (select count(*) from tbl_leadscontacthistory where tbl_leads_fk = tbl_leads_pk and cnt_who <> 'System') > 0 then " _
          				& "'Yes' else 'No' end as 'HasHistory',* " _
          				& "from tbl_leads join dbo.tbl_users on Uid=ld_assignedbyuid " _
          				& "left join dbo.tbl_LeadADVenues on av_key=ld_adcode " _
          				& "where tbl_leads_pk =" & Request.QueryString("id")
            'Dim strSql as String = "SELECT *,fname + ' ' + lname as assignedby from tbl_leads join dbo.tbl_users on Uid=ld_assignedbyuid  where tbl_leads_pk="& Request.QueryString("id")  
            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    lblleadno.Text = Sqldr("tbl_leads_pk")
                    If Sqldr("hashistory") = "Yes" Then
                        session("hashistory") = "Yes"
                    Else
                        session("hashistory") = "No"
                    End If
                     If Sqldr("av_leadads_FK") IsNot DBNull.Value Then
                       session("ladno") = Sqldr("av_leadads_FK")
                    End If
                    If Sqldr("ld_status") IsNot DBNull.Value Then
                        dd_status.SelectedIndex = dd_status.Items.IndexOf(dd_status.Items.FindByText(Sqldr("ld_status")))
                        lblstatusA.Text = Sqldr("ld_status")
                        'dd_status.selecteditem.text= Sqldr("ld_status")
                    End If
                    If Sqldr("ld_type") IsNot DBNull.Value Then
                        dd_leadtype.SelectedIndex = dd_leadtype.Items.IndexOf(dd_leadtype.Items.FindByText(Sqldr("ld_type")))
                        'dd_leadtype.selecteditem.text= Sqldr("ld_type")
                    End If
                    If Sqldr("ld_agent") IsNot DBNull.Value Then
                        dd_agent.SelectedIndex = dd_agent.Items.IndexOf(dd_agent.Items.FindByText(Sqldr("ld_agent")))
                        'dd_agent.selecteditem.text= Sqldr("ld_agent")
                    End If
                    If Sqldr("ld_fname") IsNot DBNull.Value Then
                        l_fname.Text = Sqldr("ld_fname")
                    End If
                    If Sqldr("ld_lname") IsNot DBNull.Value Then
                        l_lname.Text = Sqldr("ld_lname")
                    End If
                    If Sqldr("ld_hphone") IsNot DBNull.Value Then
                        l_hphone.Text = Sqldr("ld_hphone")
                    End If
                    If Sqldr("ld_cphone") IsNot DBNull.Value Then
                        l_cphone.Text = Sqldr("ld_cphone")
                    End If
                    If Sqldr("ld_email") IsNot DBNull.Value Then
                        l_email.Text = Sqldr("ld_email")
                    End If
                    If Sqldr("ld_email2") IsNot DBNull.Value Then
                        l_email2.Text = Sqldr("ld_email2")
                    End If

                    If Sqldr("ld_address") IsNot DBNull.Value Then
                        l_address.Text = Sqldr("ld_address")
                    End If
                    If Sqldr("ld_city") IsNot DBNull.Value Then
                        l_city.Text = Sqldr("ld_city")
                    End If
                    If Sqldr("ld_state") IsNot DBNull.Value Then
                        l_state.Text = Sqldr("ld_state")
                    End If
                    If Sqldr("ld_zip") IsNot DBNull.Value Then
                        l_zip.Text = Sqldr("ld_zip")
                    End If
                    If Sqldr("ld_notes") IsNot DBNull.Value Then
                        l_notes.content = Sqldr("ld_notes")
                    End If
                    If Sqldr("ld_capturedate") IsNot DBNull.Value Then
                        lblcapdate.Text = Sqldr("ld_capturedate")
                        l_capdate.Text = Sqldr("ld_capturedate")
                    End If
                    If Sqldr("ld_apptdate") IsNot DBNull.Value Then
                        l_appdate.Text = Sqldr("ld_apptdate")
                    End If
                    If Sqldr("ld_appttime") IsNot DBNull.Value Then
                        l_appttime.Text = Sqldr("ld_appttime")
                    End If
                    If Sqldr("ld_compensation") IsNot DBNull.Value Then
                        l_comp.Text = Sqldr("ld_compensation")
                    End If
                    If Sqldr("ld_apptlocation") IsNot DBNull.Value Then
                        dd_apptloc.SelectedIndex = dd_apptloc.Items.IndexOf(dd_apptloc.Items.FindByText(Sqldr("ld_apptlocation")))
                        'dd_leadtype.selecteditem.text= Sqldr("ld_type")
                    End If
                    lblassignedby.Text = Sqldr("assignedby")
                    session("assignedbyUID") = Sqldr("ld_assignedbyuid")
                    dd_highpri.SelectedIndex = dd_highpri.Items.IndexOf(dd_highpri.Items.FindByText(Sqldr("ld_highpri")))

                    If Sqldr("ld_mailtoaddress") IsNot DBNull.Value Then
                        If Sqldr("ld_mailtoaddress") = "Y" Then
                            l_mailto.Checked = True
                        Else
                            l_mailto.Checked = False
                        End If
                    End If


                    If Sqldr("ld_adcode") IsNot DBNull.Value Then
                        l_adcode.Text = Sqldr("ld_adcode")
                    End If
                    dd_source.SelectedIndex = dd_source.Items.IndexOf(dd_source.Items.FindByText(Sqldr("ld_adsource")))

                    If Sqldr("ld_pstatus") IsNot DBNull.Value Then
                        dd_ldstat.SelectedIndex = dd_ldstat.Items.IndexOf(dd_ldstat.Items.FindByText(Sqldr("ld_pstatus")))
                    End If

                    If Sqldr("ld_program") IsNot DBNull.Value Then
                        dd_leadprogram.SelectedIndex = dd_leadprogram.Items.IndexOf(dd_leadprogram.Items.FindByText(Sqldr("ld_program")))
                    End If

                    If Sqldr("ld_statdetail") IsNot DBNull.Value Then
                        l_web.Text = Sqldr("ld_statdetail")
                    End If


                    If Sqldr("ld_autopropmatch") = "Y" Then
                        l_automatch.Text = "Auto Match ON"
                    End If
                    If Sqldr("ld_entrysource") IsNot DBNull.Value Then
                        session("leadentrysource") = Sqldr("ld_entrysource")
                    End If
                    If Sqldr("ld_fax") IsNot DBNull.Value Then
                        l_fax.Text = Sqldr("ld_fax")
                    End If
 							If Sqldr("ld_marketingprg") IsNot DBNull.Value Then
                        dd_mkprg.SelectedIndex = dd_mkprg.Items.IndexOf(dd_mkprg.Items.FindByText(Sqldr("ld_marketingprg")))
                    End If
                    	If Sqldr("ld_extenstion1") IsNot DBNull.Value Then
                        l_hext.text=Sqldr("ld_extenstion1")
                    End If
                    	If Sqldr("ld_marketto") IsNot DBNull.Value Then
                        dd_mkto.SelectedIndex = dd_mkto.Items.IndexOf(dd_mkto.Items.FindByText(Sqldr("ld_marketto")))
                    End If

                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        End Sub

        Sub bindleadtasks()
            Dim strUID As String = Session("userid")
            Dim strSql As String = "SELECT * from tbl_leadtasks where leads_fk=" & Request.QueryString("id")
            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then

                    If Sqldr("tsk_searchcriteria") = "Yes" Then
                        scc.Checked = True
                    Else
                        scc.Checked = False
                    End If
                    If Sqldr("tsk_welcomelettersent") = "Yes" Then
                        wls.Checked = True
                    Else
                        wls.Checked = False
                    End If
                    If Sqldr("tsk_newprospectprofilesetup") = "Yes" Then
                        npp.Checked = True
                    Else
                        npp.Checked = False
                    End If
                    If Sqldr("tsk_initialfollowupdone") = "Yes" Then
                        lsc.Checked = True
                    Else
                        lsc.Checked = False
                    End If
                    If Sqldr("tsk_clientwantstokeepsearching") = "Yes" Then
                        cwks.Checked = True
                    Else
                        cwks.Checked = False
                    End If
                    If Sqldr("tsk_clientleasedc1") = "Yes" Then
                        clp.Checked = True
                    Else
                        clp.Checked = False
                    End If
                    If Sqldr("tsk_clientleasedother") = "Yes" Then
                        clpo.Checked = True
                    Else
                        clpo.Checked = False
                    End If
                Else
                    session("strnotask") = "True"
                End If

            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try

        End Sub

        Sub displayquestions(ByVal Source As System.Object, ByVal e As System.EventArgs)
            'if dd_leadtype.selecteditem.text="Tenant" then
            '	pnltenant.visible=true
            'else
            '	pnltenant.visible=false
            'end if
        End Sub

        Sub leadstatuschange(ByVal Source As System.Object, ByVal e As System.EventArgs)
            If dd_ldstat.SelectedItem.Text = "Referred Out" Or dd_ldstat.SelectedItem.Text = "Sold Out" Then
                l_comp.BackColor = System.Drawing.Color.Yellow
            End If
            If dd_ldstat.SelectedItem.Text = "Closed" And dd_status.SelectedIndex = dd_status.Items.IndexOf(dd_status.Items.FindByText("Unaccepted")) Then
                pnlclose.Visible = True
                dd_ldstat.SelectedIndex = dd_ldstat.Items.IndexOf(dd_ldstat.Items.FindByText("Precontract"))
            End If

        End Sub
        Sub Agentchange(ByVal Source As System.Object, ByVal e As System.EventArgs)
            If dd_agent.SelectedItem.Text = "ADD" Then
                '  l_comp.BackColor = System.Drawing.Color.Yellow
            End If


        End Sub
        
        Sub filterhistory(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Session("histfilter")=ddLHTypeH.selecteditem.text
            BindhistoryGrid()

        End Sub


        Sub getagentUID()
            If (dd_agent.SelectedItem.Value <> 9999 and  dd_agent.SelectedItem.Value <> 9998)  Then
                Dim strSql As String = "SELECT UID from tbl_users where users_tbl_PK=" & dd_agent.SelectedItem.Value
                Dim sqlCmd As SqlCommand
                Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
                sqlCmd = New SqlCommand(strSql, myConnection)

                Try
                    myConnection.Open()
                    Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                    If Sqldr.Read() Then
                        session("assignedagentid") = Sqldr("UID")

                    End If
                Catch SQLexc As SqlException
                    Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
                Finally
                    myConnection.Close()
                End Try
            Else
                session("assignedagentid") = "Any"
            End If
        End Sub

        Sub getagentfullname()
            Dim strSql As String = "SELECT fname + ' ' + lname as 'agentname' from tbl_users where UID='" & Session("userid") & "'"
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    session("pubuidfullname") = Sqldr("agentname")

                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try

        End Sub

        Sub processreferal()
            '	if referal.Items(0).enabled=true or referal.Items(1).enabled=true or referal.Items(2).enabled=true then
            '		if Request.QueryString("action")= "new" then
            '			getleadpk()
            '		else 
            '			pubtblleadpk= Request.QueryString("id")
            '		end if
            '	
            '		if referal.Items(0).Selected and referal.Items(0).enabled=true then
            '			pubcuspk = dd_crtype.selecteditem.value
            '			pubreferco = dd_crtype.selecteditem.text
            '			pubrefertype = "Credit"
            '			'check if referal email already sent
            '			referemailcheck()
            '			if  referemailsent= "No" then
            '				getcusemail()
            '				sendemailcus(pubcusemail)
            '			end if
            '			referal.Items(0).enabled=false
            '		else if referal.Items(1).Selected and referal.Items(1).enabled=true then
            '			pubcuspk = dd_mgtype.selecteditem.value
            '			pubreferco = dd_mgtype.selecteditem.text
            '			pubrefertype = "Mortgage"
            '			referemailcheck()
            '			if  referemailsent= "No" then
            '				getcusemail()
            '				sendemailcus(pubcusemail)
            '			end if
            '			referal.Items(1).enabled=false
            '		else if referal.Items(2).Selected and referal.Items(2).enabled=true then
            '			pubcuspk = 9999
            '			pubreferco = "Other"
            '			pubrefertype = "Other"
            '			referal.Items(2).enabled=false
            '			l_referalother.enabled=false
            '			l_referalothernote.enabled=false
            '			pubcusemail=l_referalother.text
            '		end if
            '		'response.write("HERE")
            '		'response.write(pubtblleadpk)
            '		'response.write(pubcuspk)
            '		'response.write(pubcusemail)
            '		'response.write(dd_crtype.selecteditem.text)
            '		referinsert()
            '		response.write(pubcusemail)
            '		
            '	else
            '  	response.write("DO Nothing")
            ' end if
        End Sub

        Sub referinsert()
            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String
            sqlproc = "sp_addreferal"
            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
            Dim prmleadpk As New SqlParameter("@leadpk", SqlDbType.Int)
            If Request.QueryString("action") = "new" Then
                prmleadpk.Value = session("newleadno")
            Else
                prmleadpk.Value = Request.QueryString("id")
            End If
            myCommandADD.Parameters.Add(prmleadpk)

            Dim prmcompany As New SqlParameter("@company", SqlDbType.VarChar, 50)
            prmcompany.Value = dd_refervendor.SelectedItem.Text
            myCommandADD.Parameters.Add(prmcompany)

            Dim prmcuspk As New SqlParameter("@cuspk", SqlDbType.Int)
            prmcuspk.Value = dd_refervendor.SelectedItem.Value
            myCommandADD.Parameters.Add(prmcuspk)

            Dim prmadddate As New SqlParameter("@adddate", SqlDbType.DateTime)
            prmadddate.Value = RightNowAdd
            myCommandADD.Parameters.Add(prmadddate)

            Dim prmrefertype As New SqlParameter("@refertype", SqlDbType.VarChar, 50)
            prmrefertype.Value = dd_refertype.SelectedItem.Text
            myCommandADD.Parameters.Add(prmrefertype)

            getagentfullname()

            Dim prmreferby As New SqlParameter("@referby", SqlDbType.VarChar, 50)
            prmreferby.Value = session("pubuidfullname")
            myCommandADD.Parameters.Add(prmreferby)

            Dim prmreferbyid As New SqlParameter("@referbyid", SqlDbType.VarChar, 50)
            prmreferbyid.Value = Session("userid")
            myCommandADD.Parameters.Add(prmreferbyid)


            Try
                myConnectionADD.Open()
                myCommandADD.ExecuteNonQuery()
                myConnectionADD.Close()
            Catch SQLexc As SqlException
                Response.Write("Insert Failed. Error Details are: " & SQLexc.ToString())
            End Try

        End Sub


        Sub getleadpk()
            Dim strUID As String = Session("userid")
            Dim strSql As String = "SELECT max(tbl_leads_pk) as 'pk' from tbl_leads where ld_assignedbyuid='" & Session("userid") & "'"
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

        Sub getcusemail()
            'Response.Write(dd_refervendor.SelectedItem.Value)
            'Dim strUID as String = session("userid")
            'Dim strSql as String = "SELECT *  from tbl_customers where tbl_customers_pk='" & dd_refervendor.selecteditem.value  & "'"  
            'Dim sqlCmd As SqlCommand
            'Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            'sqlCmd = New SqlCommand(strSql, myConnection)

            '  Try
            ' 	myConnection.Open()
            '     	Dim Sqldr as SqlDataReader = sqlCmd.ExecuteReader
            '			if Sqldr.read() then
            '				pubcusemail = Sqldr("cus_email")
            '			end if
            '
            ''  				Catch SQLexc As SqlException
            '                   Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            '           	finally
            '          		myConnection.close()
            '    End Try
        End Sub


        Public Sub sendemailcus(ByVal mail_to As String)
            Dim mail As New MailMessage()
            If Request.QueryString("action") = "new" Then
                session("pubtblleadpk") = session("newleadno")
            Else
                session("pubtblleadpk") = Request.QueryString("id")
            End If
            'Set the properties - send the email to the person who filled out the
            mail.From = New MailAddress("mychoice@gochoiceone.com")
            mail.To.Add(mail_to)
            'mail.cc.add("sbialas@gochoiceone.com")
            mail.Subject = "Lead Notification"
            'Set the body

            mail.Body = "Below you will find the details for this lead. " & vbCrLf & _
                       "____________________________________________________" & vbCrLf & vbCrLf & _
                       "Lead Information" & vbCrLf & _
                       "-------------------" & vbCrLf & _
                       "Lead #: " & session("pubtblleadpk") & vbCrLf & _
                       "Lead Type:   " & dd_leadtype.SelectedItem.Text & vbCrLf & _
                       "Lead Date:   " & l_capdate.Text & vbCrLf & _
                       "Name:        " & l_fname.Text & " " & l_lname.Text & vbCrLf & _
                       "Home Phone:  " & l_hphone.Text & vbCrLf & _
                       "Cell Phone:  " & l_cphone.Text & vbCrLf & _
                       "Email:       " & l_email.Text & vbCrLf & _
                       "Other Email: " & l_email2.Text & vbCrLf & _
                       "Notes:       " & vbCrLf & _
                       l_notes.content



            'send the message
            Dim smtp As New SmtpClient("smtp.comcast.net")
            smtp.Send(mail)

        End Sub

        Sub referemailcheck()
            Dim strUID As String = Session("userid")
            Dim strSql As String = "SELECT * from tbl_referals where  refer_lead_fk = '" & session("pubtblleadpk") & "' and refer_type ='" &  session("pubrefertype") & "' and refer_customer_fk = '" &  session("pubcuspk") & "'"
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    session("referemailsent") = "Yes"
                Else
                    session("referemailsent") = "No"
                End If

            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        End Sub
		
			Sub getleadno()

            Dim strSql As String = "SELECT max(tbl_leads_pk) as 'newpk' from dbo.tbl_leads"
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    session("currentleadno") = Sqldr("newpk")
                End If

            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
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

        Sub test()
            Dim strUID As String = Session("userid")
            Dim strSql As String
            If Request.QueryString("action") = "new" Then
                strSql = "SELECT count(*) as 'cnt' from tbl_referals where refer_lead_fk=" & session("newleadno") & " and (refer_emailsent is null or refer_emailsent='No')"
            Else
                strSql = "SELECT count(*) as 'cnt' from tbl_referals where refer_lead_fk=" & Request.QueryString("id") & " and (refer_emailsent is null or refer_emailsent='No')"
            End If
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    session("totleads") = Sqldr("cnt")
                    'response.write(Sqldr("cnt"))
                End If

            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try

            If session("totleads") > 0 Then
                sm(session("totleads"))
            End If
        End Sub


        Sub sm(ByVal i As Integer)
            Dim d As Integer
            For d = 1 To i
                Dim strUID As String = Session("userid")
                Dim strSql As String = "SELECT tbl_referals_pk,cus_email, cus_company from tbl_customers join tbl_referals on refer_customer_fk = tbl_customers_pk  where refer_lead_fk=" & Request.QueryString("id") & " and (refer_emailsent is null or refer_emailsent='No')"
                Dim sqlCmd As SqlCommand
                Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
                sqlCmd = New SqlCommand(strSql, myConnection)

                Try
                    myConnection.Open()
                    Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                    If Sqldr.Read() Then
                        'response.write(Sqldr("cus_company"))
                        'response.write(Sqldr("cus_email"))
                         session("pubcusemail") = Sqldr("cus_email")
                        session("referalleadpk") = Sqldr("tbl_referals_pk")
                    End If

                Catch SQLexc As SqlException
                    Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
                Finally
                    myConnection.Close()
                End Try
                updatereferemail(session("referalleadpk"))
                'response.write (pubcusemail)
                sendemailcus( session("pubcusemail"))
            Next d
        End Sub

        Sub updatereferemail(ByVal j As Integer)
            Dim strpropID As String = Request.QueryString("id")
            Dim strSql As String = "update tbl_referals set refer_emailsent='Yes' where tbl_referals_pk=" & j
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
        End Sub
        Sub fields(ByVal stat As String)

            l_fname.Enabled = stat
            l_lname.Enabled = stat
            l_hphone.Enabled = stat
            l_cphone.Enabled = stat
            l_email.Enabled = stat
            l_email2.Enabled = stat

            l_address.Enabled = stat
            l_city.Enabled = stat
            l_state.Enabled = stat
            l_zip.Enabled = stat
            'l_notes.Enabled = stat
            l_appdate.Enabled = stat
            l_appttime.Enabled = stat
            dd_apptloc.Enabled = stat
            dd_leadprogram.Enabled = stat
            'dd_statdetail.enabled=stat
            l_comp.Enabled = stat
            l_adcode.Enabled = stat
            'l_notesB.enabled=stat
            l_sendemailtxt.Enabled = stat
            'l_vwcontact.enabled=stat
            dd_agent.Enabled = stat
            dd_refertype.Enabled = stat
            dd_refervendor.Enabled = stat
            dd_ldstat.Enabled = stat
            dd_status.Enabled = stat
            dd_leadtype.Enabled = stat
            dd_highpri.Enabled = stat
            dd_source.Enabled = stat
            If stat = "true" Then
                l_mailto.Enabled = True

            Else
                l_mailto.Enabled = False

            End If
            'l_delete.enabled=stat
            l_capdate.Enabled = stat
            l_addreferal.Enabled = stat

        End Sub

        Sub bindprofile()
            Dim strpropID As String = Request.QueryString("id")
            Dim strUID As String = Session("userid")
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String

            If dd_leadtype.Text = "Buyer" Or dd_leadtype.Text = "Tenant" Or dd_leadtype.Text = "Tenant Office" Then
                mycommand = "Select 'leadprofile.aspx' as 'url',*,cast(tbl_lead_profile_pk as varchar(20)) as 'pnum', cast(tbl_leads_fk as varchar(20)) as 'lnum' from dbo.tbl_lead_profile join tbl_leads on tbl_leads_pk = tbl_leads_fk Where tbl_leads_fk=" & strpropID
            Else
                mycommand = "Select 'leadprofileSell.aspx' as 'url',*,cast(tbl_lead_profile_pk as varchar(20)) as 'pnum', cast(tbl_leads_fk as varchar(20)) as 'lnum' from dbo.tbl_lead_profile join tbl_leads on tbl_leads_pk = tbl_leads_fk Where tbl_leads_fk=" & strpropID
            End If

            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_lead_profile")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_lead_profile"))

                DGprofile.DataSource = dvProducts
                DGprofile.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

        End Sub



        Sub inserthistory(ByVal note As String)
            Dim prmnote As String = note

            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String
            sqlproc = "sp_insertleadcontact"


            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

            Dim prmleadno As New SqlParameter("@leadfk", SqlDbType.Int)
            If Request.QueryString("action") = "new" Then
                prmleadno.Value = session("newleadno")
            Else
                prmleadno.Value = Request.QueryString("id")
            End If
            myCommandADD.Parameters.Add(prmleadno)

            Dim prmcapdate As New SqlParameter("@capdate", SqlDbType.DateTime)
            prmcapdate.Value = rightNow
            myCommandADD.Parameters.Add(prmcapdate)

            Dim prmnotes As New SqlParameter("@l_notes", SqlDbType.Text)
            prmnotes.Value = prmnote
            myCommandADD.Parameters.Add(prmnotes)

            Dim prmuid As New SqlParameter("@uid", SqlDbType.VarChar, 50)
            prmuid.Value = Session("userid")
            myCommandADD.Parameters.Add(prmuid)

            Dim prmtype As New SqlParameter("@LHType", SqlDbType.VarChar, 50)
            prmtype.Value = "Internal"
            myCommandADD.Parameters.Add(prmtype)

            Dim prmfollowup As New SqlParameter("@followup", SqlDbType.VarChar, 50)
            prmfollowup.Value = "No"
            myCommandADD.Parameters.Add(prmfollowup)

            Dim prmaction As New SqlParameter("@followupactions", SqlDbType.Text)
            prmaction.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmaction)

            Dim prmstatus As New SqlParameter("@LHstat", SqlDbType.VarChar, 50)
            prmstatus.Value = "Closed"
            myCommandADD.Parameters.Add(prmstatus)

            Dim prmwho As New SqlParameter("@LHwho", SqlDbType.VarChar, 50)
            prmwho.Value = "System"
            myCommandADD.Parameters.Add(prmwho)

            Dim prmclosedt As New SqlParameter("@closedate", SqlDbType.DateTime)
            prmclosedt.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmclosedt)

            Dim prmfduedt As New SqlParameter("@fduedate", SqlDbType.DateTime)
            prmfduedt.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmfduedt)

            Try
                myConnectionADD.Open()
                myCommandADD.ExecuteNonQuery()
                myConnectionADD.Close()
            Catch SQLexc As SqlException
                Response.Write("Insert Failed. Error Details are: " & SQLexc.ToString())
            End Try


        End Sub


        Sub subnav(ByVal button As String)
            Dim clickedbutton As String = button

            'Set cell class
            subnavLP.Attributes.Add("class", "tblcelltest")
            subnavnotes.Attributes.Add("class", "tblcelltest")
            subnavprofile.Attributes.Add("class", "tblcelltest")
            subnavhistory.Attributes.Add("class", "tblcelltest")
            subrefer.Attributes.Add("class", "tblcelltest")
            'subquestions.Attributes.Add("class", "tblcelltest")
            subnavtasks.Attributes.Add("class", "tblcelltest")
            subnavfinance.Attributes.Add("class", "tblcelltest")
            submatches.Attributes.Add("class", "tblcelltest")
            subexportqueue.Attributes.Add("class", "tblcelltest")
				subnavAcnt.Attributes.Add("class", "tblcelltest")
				subWorkFlow.Attributes.Add("class", "tblcelltest")
				
				
            'Set button font color
            lLP.ForeColor = System.Drawing.Color.Black
            lNotes.ForeColor = System.Drawing.Color.Black
            lProfile.ForeColor = System.Drawing.Color.Black
            lhistory.ForeColor = System.Drawing.Color.Black
            lfinance.ForeColor = System.Drawing.Color.Black
            lTasks.ForeColor = System.Drawing.Color.Black
            'lQuestions.ForeColor = System.Drawing.Color.Black
            lexport.ForeColor = System.Drawing.Color.Black
            lReferrals.ForeColor = System.Drawing.Color.Black
            lmatches.ForeColor = System.Drawing.Color.Black
            lSCA.ForeColor = System.Drawing.Color.Black
				lworkflow.ForeColor = System.Drawing.Color.Black
				
            'Set spacers
            spacer0.Visible = True
            spacer0a.Visible = True
            spacer1.Visible = True
            'spacer1a.Visible = True
            spacer2.Visible = True
            spacer3.Visible = True
            spacer4.Visible = True
            spacer5.Visible = True
            spacer6.Visible = false
 				spacer6a.Visible = True

            'Set Panels
            pnlLP.Visible = False
            pnlinitialnotes.Visible = False
            'pnlquestions.visible=false
            'pnlnoquestions.visible=false
            'pnlnotasks.visible=false
            'pnlrentaltasks.visible=false
            'pnltasktakeout.visible=false
            pnltasks.Visible = False
            pnlviewnotes.Visible = False
            pnladdencoutner.Visible = False
            pnladdreferal.Visible = False
            pnladdreferalADD.Visible = False
            pnlprofile.Visible = False
            pnlexportque.Visible = False
            'leadinfo.visible = true
            pnlpropmatch.Visible = False
            pnlsc.Visible = False
            pnlworkflow.Visible = False
            
            If Session("fullscreen") = "Yes" Then
                leadinfo.Visible = False
            Else
                leadinfo.Visible = True
            End If

            If clickedbutton = "Notes" Then
                subnavnotes.Attributes.Add("class", "tblcelltestSelected")
                lNotes.ForeColor = System.Drawing.Color.White
                spacer1.Visible = False
                spacer0a.Visible = False
                 spacer1a.Visible = False
                  spacer2.Visible = False
                pnlinitialnotes.Visible = True

            ElseIf clickedbutton = "LeadProfile" Then
                subnavLP.Attributes.Add("class", "tblcelltestSelected")
                lLP.ForeColor = System.Drawing.Color.White
                spacer0.Visible = False
                'spacer1.Visible = False
                spacer1a.Visible = False
                spacer2.Visible = False
                pnlLP.Visible = True


            ElseIf clickedbutton = "History" Then
                subnavhistory.Attributes.Add("class", "tblcelltestSelected")
                lhistory.ForeColor = System.Drawing.Color.White
                spacer1.Visible = False
                
                 spacer1a.Visible = False
                 spacer2.Visible = False
                spacer3.Visible = False
                pnlviewnotes.Visible = True
                BindhistoryGrid()

            ElseIf clickedbutton = "Financials" Then
                subnavfinance.Attributes.Add("class", "tblcelltestSelected")
                lfinance.ForeColor = System.Drawing.Color.White
                spacer1.Visible = False
                spacer1a.Visible = False
                leadinfo.Visible = False
                checkfinancial()
            ElseIf clickedbutton = "PropertyProfile" Then
                subnavprofile.Attributes.Add("class", "tblcelltestSelected")
                lProfile.ForeColor = System.Drawing.Color.White
                spacer1a.Visible = False
                spacer2.Visible = False
                pnlprofile.Visible = True
                If Request.QueryString("action") = "view" Then
                    bindprofile()
                End If
            ElseIf clickedbutton = "Tasks" Then
					
                subnavtasks.Attributes.Add("class", "tblcelltestSelected")
                lTasks.ForeColor = System.Drawing.Color.White
                spacer3.Visible = False
                spacer4.Visible = False
                 spacer1a.Visible = False
                spacer2.Visible = False

                pnltasks.Visible = True
                bindtasks()

            ElseIf clickedbutton = "queue" Then
                subexportqueue.Attributes.Add("class", "tblcelltestSelected")
                lexport.ForeColor = System.Drawing.Color.White
                spacer4.Visible = False
                spacer5.Visible = False
                 spacer1a.Visible = False
                spacer2.Visible = False
                pnlexportque.Visible = True
                bindexportq()


            ElseIf clickedbutton = "Referrals" Then
                subrefer.Attributes.Add("class", "tblcelltestSelected")
                lReferrals.ForeColor = System.Drawing.Color.White
                spacer5.Visible = False
                spacer6.Visible = False
                 spacer1a.Visible = False
                spacer2.Visible = False
                pnladdreferal.Visible = True
                If Request.QueryString("action") = "view" Then
                    getreferals()
                End If

            ElseIf clickedbutton = "Matches" Then
                submatches.Attributes.Add("class", "tblcelltestSelected")
                lmatches.ForeColor = System.Drawing.Color.White
                spacer6.Visible = False
                pnlpropmatch.Visible = True
                
            ElseIf clickedbutton = "Contactinfo" Then
                subnavAcnt.Attributes.Add("class", "tblcelltestSelected")
                lSCA.ForeColor = System.Drawing.Color.White
                spacer0.Visible = False
                spacer0a.Visible = False
                pnlsc.Visible = True
                  spacer1a.Visible = False
                  spacer2.Visible = False
            ElseIf clickedbutton = "WorkFlow" Then
                subWorkFlow.Attributes.Add("class", "tblcelltestSelected")
                lworkflow.ForeColor = System.Drawing.Color.White
                pnlworkflow.Visible = True
                             
            End If
        End Sub
        Public Sub bindexportq()
            Dim strpropID As String = Request.QueryString("id")
            Dim strUID As String = Session("userid")
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            mycommand = "Select * from tbl_leadexportqueuedetail join tbl_leadexportqueue on eq_tbl_pk=eqd_eq_fk Where eqd_leadno=" & strpropID
            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_leadexportqueuedetail")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_leadexportqueuedetail"))
                exportqueue.DataSource = dvProducts
                exportqueue.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try


        End Sub

        Sub checkfinancial()
            Dim strUID As String = Session("userid")
            Dim strSql As String
            strSql = "SELECT * from tbl_financials where  tbl_lead_fk = '" & Request.QueryString("id") & "'"
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    session("referemailsent") = "Yes"
                Else
                    lblnofinancials.Visible = True
                    lblnofinancials.Text = "No Profile pleass add"
                End If

            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try

        End Sub

        Sub bindsearchcriteria()
            Dim strUID As String = Session("userid")
            Dim strSql As String
            strSql = "SELECT * from tbl_profilesearchreq where  tbl_leads_fk = '" & Request.QueryString("id") & "'"
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    If Sqldr("sr_bed") = "Y" Then
                        chkbed.Checked = True
                    Else
                        chkbed.Checked = False
                    End If
                    If Sqldr("sr_bath") = "Y" Then
                        chkbath.Checked = True
                    Else
                        chkbath.Checked = False
                    End If
                    If Sqldr("sr_levels") = "Y" Then
                        chklevel.Checked = True
                    Else
                        chklevel.Checked = False
                    End If
                    If Sqldr("sr_rent") = "Y" Then
                        chkrent.Checked = True
                    Else
                        chkrent.Checked = False
                    End If
                    If Sqldr("sr_city") = "Y" Then
                        chkcity.Checked = True
                    Else
                        chkcity.Checked = False
                    End If
                    If Sqldr("sr_credit") = "Y" Then
                        chkcredit.Checked = True
                    Else
                        chkcredit.Checked = False
                    End If
                    If Sqldr("sr_movedt") = "Y" Then
                        chkmovedt.Checked = True
                    Else
                        chkmovedt.Checked = False
                    End If
                    If Sqldr("sr_school") = "Y" Then
                        chkschool.Checked = True
                    Else
                        chkschool.Checked = False
                    End If
                    If Sqldr("sr_sec8") = "Y" Then
                        chksec8.Checked = True
                    Else
                        chksec8.Checked = False
                    End If

                    session("srchcriteria") = "Existing"

                Else
                    session("srchcriteria") = "New"

                End If

            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try

        End Sub


        Sub bindpropmatches()
            Dim strpropID As String = Request.QueryString("id")
            Dim strUID As String = Session("userid")
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String

            If dd_leadtype.Text = "Buyer" Or dd_leadtype.Text = "Tenant" Or dd_leadtype.Text = "Tenant Office" Then
                mycommand = "select 'leadprofileSell.aspx' as 'url',*,cast(tbl_propmatch_pk as varchar(20)) as 'pnum', cast(pm_tenantleadno as varchar(20)) as 'tennum', " _
                    & "cast(pm_landlordleadno as varchar(20)) as 'lannum' from choiceonedev.dbo.tbl_propmatch " _
                    & "join tbl_leads on tbl_leads_pk = pm_landlordleadno " _
                    & "join tbl_lead_profile on tbl_lead_profile_pk= pm_landlordprofileno " _
                    & "where pm_tenantleadno = " & strpropID
            Else
                mycommand = "select 'leadprofile.aspx' as 'url',*,cast(pm_landlordprofileno as varchar(20)) as 'pnum', cast(pm_tenantleadno as varchar(20)) as 'tennum', " _
                    & "cast(pm_landlordleadno as varchar(20)) as 'lannum' from choiceonedev.dbo.tbl_propmatch " _
                    & "join tbl_leads on tbl_leads_pk = pm_landlordleadno " _
                    & "join tbl_lead_profile on tbl_lead_profile_pk= pm_landlordprofileno " _
                    & "where pm_landlordleadno = " & strpropID
            End If

            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_propmatch")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_propmatch"))

                DGPropMatches.DataSource = dvProducts
                DGPropMatches.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
        End Sub
 			
 			Sub bindworkflows()
            Dim strpropID As String = Request.QueryString("id")
            Dim strUID As String = Session("userid")
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            if dd_WFStatFilter.selecteditem.text = "Active" then
			   	mycommand = "select *, convert(varchar(20),wfm_effdate ,101) as 'WFEffdate',convert(varchar(20),wfm_enddate ,101) as 'WFEnddate' from dbo.tbl_leadWorkFlowsStatus join dbo.tbl_WorkFlowMaster on wfm_tbl_pk=lwfs_wfm_fk where lwfs_lead_fk='" & request.querystring("id") & "' and lwfs_leadststatus='Active' order by wfm_tbl_pk"
          	elseif dd_WFStatFilter.selecteditem.text = "Inactive" then
          		mycommand = "select *, convert(varchar(20),wfm_effdate ,101) as 'WFEffdate',convert(varchar(20),wfm_enddate ,101) as 'WFEnddate'  from dbo.tbl_leadWorkFlowsStatus join dbo.tbl_WorkFlowMaster on wfm_tbl_pk=lwfs_wfm_fk where lwfs_lead_fk='" & request.querystring("id") & "' and lwfs_leadststatus='Inactive' order by wfm_tbl_pk"
         
          	else
          	   mycommand = "select *, convert(varchar(20),wfm_effdate ,101) as 'WFEffdate',convert(varchar(20),wfm_enddate ,101) as 'WFEnddate'  from dbo.tbl_leadWorkFlowsStatus join dbo.tbl_WorkFlowMaster on wfm_tbl_pk=lwfs_wfm_fk where lwfs_lead_fk='" & request.querystring("id") & "' order by wfm_tbl_pk"
         
          	end if
            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_leadWorkFlowsStatus")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_leadWorkFlowsStatus"))

                workflow.DataSource = dvProducts
                workflow.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
        End Sub


        Sub DGPropMatches_ItemDataBound(ByVal sender As Object, ByVal e As DataGridItemEventArgs) Handles DGPropMatches.ItemDataBound
            Dim l As LinkButton
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                l = CType(e.Item.Cells(6).FindControl("cmdDel"), LinkButton)
                l.Attributes.Add("onclick", "return confirm('Are you sure you want to delete this item?');")
            End If
        End Sub

        Sub DGPropMatches_ItemCommand(ByVal sender As Object, ByVal e As DataGridCommandEventArgs) Handles DGPropMatches.ItemCommand
            Dim iid As Integer = Convert.ToInt32(e.Item.Cells(0).Text) 'Grab the ID from the hidden column             
            Response.Write(iid)
            'Dim myConnection As New SqlConnection(Your Northwind ConnectionString Here)
            'Dim myDeleteCommand As SqlCommand = New SqlCommand("DELETE FROM Customers WHERE CustomerID =  '" & iid & "'", myConnection)             
            'myDeleteCommand.CommandType = CommandType.Text             
            'myConnection.Open() 'Open the connection
            'myDeleteCommand.ExecuteNonQuery() 'Delete the record             
            'myConnection.Close() 'Close the connection    
        End Sub
        Public Sub PrintLead(ByVal Source As Object, ByVal e As ImageClickEventArgs)
            Session("Clead") = Request.QueryString("ID")
            if dd_status.selecteditem.text="Draft" then
           		session("pstatus") = "Draft"
                getagentUID()
                insertdb()
           
           	else
           		btn_saveDB()
            end if
            Response.Write("<script>window.open" & _
                "('printlead.aspx?id=','_new', 'width=700,height=500');</script>")
        End Sub
        Public Sub emaillead(ByVal Source As Object, ByVal e As ImageClickEventArgs)
            Session("Clead") = Request.QueryString("ID")
           	if dd_status.selecteditem.text="Draft" then
           		session("pstatus") = "Draft"
                getagentUID()
                insertdb()           
           	else
           		btn_saveDB()
            end if
            
            Response.Write("<script>window.open" & _
                "('emaillead.aspx?id=" & Request.QueryString("id") & "','_new','width=1000,height=724,resizable=1,scrollbars=1');</script>")
        End Sub
         Public Sub leadhelp(ByVal Source As Object, ByVal e As ImageClickEventArgs)
                       
            Response.Write("<script>window.open" & _
                "('leadhelp.aspx','_new','width=1000,height=724,resizable=1,scrollbars=1');</script>")
        End Sub
        Public Sub printleadAA()
            Dim msg As String
            msg = ""
            msg = msg & "<Script Language='JavaScript'>"
            msg = msg & "window.print();"
            msg = msg & "</Script>"
            Response.Write(msg)
        End Sub
        Public Sub GetSelections_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
            Dim rowCount As Integer = 0
            Dim gridSelections As StringBuilder = New StringBuilder()

            'Loop through each DataGridItem, and determine which CheckBox controls
            'have been selected.
            Dim DemoGridItem As DataGridItem
            For Each DemoGridItem In DGPropMatches.Items

                Dim myCheckbox As CheckBox = CType(DemoGridItem.Cells(0).Controls(1), CheckBox)
                If myCheckbox.Checked = True Then
                    rowCount += 1
                    Response.Write("The checkbox for " & DGPropMatches.DataKeys(DemoGridItem.ItemIndex).ToString())
                    'gridSelections.AppendFormat("The checkbox for " &  DGPropMatches.DataKeys(DemoGridItem.ItemIndex).ToString() & "was selected<br>")
                    ', _
                    '                           DGPropMatches.DataKeys(DemoGridItem.ItemIndex).ToString())
                End If
            Next
            gridSelections.Append("<hr>")
            gridSelections.AppendFormat("Total number selected is: {0}<br>", rowCount.ToString())
            Response.Write(gridSelections.ToString())

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
        Sub removeven(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(0).Text
            removeleadfromq(content)
            bindexportq()

        End Sub
        
        
        Sub AddWFA(ByVal Source As System.Object, ByVal e As System.EventArgs)
				wrkflowmain.visible=false
				pnladd2workflow.visible=true
				bindallWFs()
	
	
			end sub
			
			Sub exitwfadd(ByVal Source As System.Object, ByVal e As System.EventArgs)
				wrkflowmain.visible=true
				pnladd2workflow.visible=false
				bindworkflows()
	
	
			end sub
			
			
			Sub bindallWFs()
            Dim strpropID As String = Request.QueryString("id")
            Dim strUID As String = Session("userid")
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            
          	   mycommand = "select *, convert(varchar(20),wfm_effdate ,101) as 'WFEffdate',convert(varchar(20),wfm_enddate ,101) as 'WFEnddate' " _
          	   			& "from dbo.tbl_WorkFlowMaster where wfm_useridfk='" & session("userid") & "' and wfm_status='Active' " _
          	   			& "and wfm_tbl_pk not in (select lwfs_wfm_fk from tbl_leadWorkFlowsStatus where lwfs_lead_fk='" & request.querystring("id") & "' and lwfs_leadststatus='Active') "
         
            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_WorkFlowMaster")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_WorkFlowMaster"))

                workflowView.DataSource = dvProducts
                workflowView.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
        End Sub
        
        
        
        
        Sub vwfdetails(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(0).Text
            response.redirect("wfstepdetails.aspx?id=" & content & "&leadno=" & request.querystring("id"))

        End Sub
        
          
          Sub AddWF(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(0).Text
            session("WFMPK")=content
            session("leadno")=request.querystring("id")
         	AddLeadtoWorkflow(content)
         	wrkflowmain.visible=true
				pnladd2workflow.visible=false
				bindworkflows()

        End Sub
         
        sub AddLeadtoWorkflow(id as string)
        		session("WFMPK")=id
         	insertWFStatusRec(id)
         	getWFStatusRecPK(id) 
				getWFMasterData(id)
				getWFSteps(id)
		  
		  end sub
		  
		  sub getWFStatusRecPK(wrkflwid as string)
		  
		  	Dim strUID As String = Session("userid")
            Dim strSql As String = "select max(lwfs_tbl_pk) as 'maxpk' from tbl_leadWorkFlowsStatus where lwfs_userid_fk='" & session("userid") & "' " _
            							& "and lwfs_lead_fk='" &  session("leadno") & "' and lwfs_wfm_fk='" & wrkflwid & "'" 
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
		  
		  sub insertWFStatusRec(wrkflwid as string)
			
			
				Dim strUID As String = Session("userid")
            Dim strSql As String = "insert into tbl_leadWorkFlowsStatus (lwfs_lead_fk,lwfs_userid_fk,lwfs_wfm_fk,lwfs_leadststatus) " _
                                   & "values ('" & session("leadno") & "','" & session("userid") & "', '" & wrkflwid & "', 'Active' )"  
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
         
         sub getWFMasterData(id as string)
				 Dim strSql As String = "SELECT * from tbl_WorkFlowMaster where wfm_tbl_pk='" & id & "'"
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
			
			sub  getWFSteps(id as string)

				Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            mycommand = "select wfs_tbl_pk,wfs_Freq,wfs_Sunday,Wfs_Monday,wfs_Tuesday,wfs_Wednesday,wfs_Thursday,wfs_Friday,wfs_Saturday,wfs_DayofMonth, wfs_DependantStep,wfs_stepno,wfs_basedateoffset,* from tbl_WorkFlowSteps where wfs_wfm_fk='" & id & "' and wfs_status='Active' order by wfs_stepno "
                        
            Dim ad As New SqlDataAdapter(mycommand, myConnection)
            Dim ds As New DataSet()
            Dim str As New StringBuilder()

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
				'response.write(DStep)
				if DStep="0" then
					sdate =DateTime.Now
				else
					'response.write("H1")
					sdate = GetnewStepDate(dstep)
					'response.write(sdate)
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
					'response.write(currQuarter)
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
         
         
         
         
        Sub filterworkflows(ByVal Source As System.Object, ByVal e As System.EventArgs)
         	bindworkflows()
        end sub
        
        
        
        Sub removewf(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(0).Text
            removeleadfromWF(content)
            bindworkflows()

        End Sub
        
        Sub removeleadfromWF(ByVal id As String)
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "update tbl_leadWorkFlowsStatus set lwfs_leadststatus='Inactive' where lwfs_wfm_fk ='" & id & "' and lwfs_lead_fk='" & request.querystring("id") & "'"
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
        
        Sub removeleadfromq(ByVal id As String)
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "delete from tbl_leadexportqueuedetail where eqd_tbl_pk='" & id & "'"
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
        
		Sub bindLHTypeH()

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='LHType' order by x_descr"
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                ddLHTypeH.DataSource = dataReader
                ddLHTypeH.DataTextField = "x_descr"
                ddLHTypeH.DataValueField = "tbl_xwalk_pk"
                ddLHTypeH.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
 			ddLHTypeH.Items.Insert(0, New ListItem("All", "999998"))
 			if Session("histfilter")<>"" then
 				ddLHTypeH.SelectedIndex = ddLHTypeH.Items.IndexOf(ddLHTypeH.Items.FindByText(Session("histfilter")))
 				bindhistorygrid()
 			end if
 			
        End Sub



 
          Sub ItemDataBoundEventHandlerWF(ByVal sender As Object, ByVal e As DataGridItemEventArgs)

				  If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            	Dim btnappnd As button
               btnappnd = e.Item.Cells(0).FindControl("wfermove")
            	
            	Dim itemcellwho2 As TableCell = e.Item.Cells(2)
               Dim itemCellwhotext2 As String = itemCellwho2.Text
              
              	if itemCellwhotext2="Inactive" then
              		btnappnd.enabled=false
              	else
              		btnappnd.enabled=true
              	end if
		    	end if
		  end sub
		  
		  sub checkforworkflowNew()
		 		'Check for matching workflows and store in data table
				Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
				 'response.write("here2")
            mycommand = "begin " _
								& "select * " _
								& "into #tmpa " _ 
								& "from dbo.tbl_WorkFlowMaster " _
								& "left join dbo.tbl_WorkFlowFilters on wffilters_wfm_fk = wfm_tbl_pk " _
								& "left join dbo.tbl_xwalk on tbl_xwalk_pk = wffilters_value and wffilters_type <> 'AD' " _
								& "where wfm_useridfk='" & session("userid") & "' " _
								& "and wfm_effdate <= getdate() and wfm_enddate >= getdate() " _
								& "and wfm_status = 'Active' " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpLTMatch " _
								& "from #tmpa " _
								& "where (wfm_filter1Usage='Include' and (x_descr='" & dd_leadtype.selecteditem.text & "' and x_type='leadtype') or wfm_filter1Usage='Do Not Use') " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpLPMatch " _
								& "from #tmpa " _
								& "where (wfm_filter2Usage='Include' and (x_descr='" & dd_leadprogram.selecteditem.text & "' and x_type='leadprogram') or wfm_filter2Usage='Do Not Use') " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpLSMatch " _
								& "from #tmpa " _
								& "where (wfm_filter3Usage='Include' and (x_descr='" & lblstatusA.text & "' and x_type='leadstatus') or wfm_filter3Usage='Do Not Use') " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpCONMatch " _
								& "from #tmpa " _
								& "where (wfm_filter4Usage='Include' and (x_descr='" & dd_ldstat.selecteditem.text & "' and x_type='contactstatus') or wfm_filter4Usage='Do Not Use') " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpADMatch " _
								& "from #tmpa " _
								& "where (wfm_filter5Usage='Include' and (wffilters_value='" & session("ladno") & "' and wffilters_type='AD') or wfm_filter5Usage='Do Not Use') " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpMKTMatch " _
								& "from #tmpa " _
								& "where (wfm_filter6Usage='Include' and (x_descr='" & dd_mkprg.selecteditem.text & "' and x_type='marketprogram') or wfm_filter6Usage='Do Not Use') " _
								& "select  distinct a.wfm_tbl_pk " _
								& "from #tmpa a " _
								& "join #tmpLTMatch b on b.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "join #tmpLPMatch c on c.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "join #tmpLSMatch d on d.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "join #tmpCONMatch e on e.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "join #tmpADMatch f on f.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "join #tmpMKTMatch g on g.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "end"
                        
          
            Dim ad As New SqlDataAdapter(mycommand, myConnection)
            Dim ds As New DataSet()
            Dim str As New StringBuilder()

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
				  session("WFMPK")=ds.Tables(0).Rows(i)(0).ToString()
              AddLeadtoWorkflow(ds.Tables(0).Rows(i)(0).ToString())
              

            Next
			
		  
		  end sub
		  
		  sub checkforworkflowSch()
		  	'Check for matching workflows and store in data table
				Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
				
		  
             		mycommand = "begin " _
								& "select * " _
								& "into #tmpa " _ 
								& "from dbo.tbl_WorkFlowMaster " _
								& "left join dbo.tbl_WorkFlowFilters on wffilters_wfm_fk = wfm_tbl_pk " _
								& "left join dbo.tbl_xwalk on tbl_xwalk_pk = wffilters_value and wffilters_type <> 'AD' " _
								& "where wfm_useridfk='" & session("userid") & "' " _
								& "and wfm_trigger = 'On A Schedule' " _								
								& "and wfm_effdate <= getdate() and wfm_enddate >= getdate() " _
								& "and wfm_status = 'Active' " _
								& "and '" & session("leadno") & "' not in (select lwfs_lead_fk  from tbl_leadWorkFlowsStatus) " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpLTMatch " _
								& "from #tmpa " _
								& "where (wfm_filter1Usage='Include' and (x_descr='Owner' and x_type='leadtype') or wfm_filter1Usage='Do Not Use') " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpLPMatch " _
								& "from #tmpa " _
								& "where (wfm_filter2Usage='Include' and (x_descr='Take5' and x_type='leadprogram') or wfm_filter2Usage='Do Not Use') " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpLSMatch " _
								& "from #tmpa " _
								& "where (wfm_filter3Usage='Include' and (x_descr='Take5' and x_type='leadstatus') or wfm_filter3Usage='Do Not Use') " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpCONMatch " _
								& "from #tmpa " _
								& "where (wfm_filter4Usage='Include' and (x_descr='Take5' and x_type='contactstatus') or wfm_filter4Usage='Do Not Use') " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpADMatch " _
								& "from #tmpa " _
								& "where (wfm_filter5Usage='Include' and (wffilters_value='853' and wffilters_type='AD') or wfm_filter5Usage='Do Not Use') " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpMKTMatch " _
								& "from #tmpa " _
								& "where (wfm_filter6Usage='Include' and (x_descr='Take5' and x_type='marketprogram') or wfm_filter6Usage='Do Not Use') " _
								& "select  distinct a.wfm_tbl_pk " _
								& "from #tmpa a " _
								& "join #tmpLTMatch b on b.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "join #tmpLPMatch c on c.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "join #tmpLSMatch d on d.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "join #tmpCONMatch e on e.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "join #tmpADMatch f on f.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "join #tmpMKTMatch g on g.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "end"
		  
            Dim ad As New SqlDataAdapter(mycommand, myConnection)
            Dim ds As New DataSet()
            Dim str As New StringBuilder()

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
				
              AddLeadtoWorkflow(ds.Tables(0).Rows(i)(0).ToString())
              

            Next
			
		  
		  end sub
		  
		   sub checkforworkflowLT()
		   	'response.write("AAAAA")
		  		'Check for matching workflows and store in data table
				Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
				
		  
             		mycommand = "begin " _
								& "select * " _
								& "into #tmpa " _ 
								& "from dbo.tbl_WorkFlowMaster " _
								& "left join dbo.tbl_WorkFlowFilters on wffilters_wfm_fk = wfm_tbl_pk " _
								& "left join dbo.tbl_xwalk on tbl_xwalk_pk = wffilters_value and wffilters_type <> 'AD' " _
								& "where wfm_useridfk='" & session("userid") & "' " _
								& "and wfm_trigger = 'On Lead Type Change' " _								
								& "and wfm_effdate <= getdate() and wfm_enddate >= getdate() " _
								& "and wfm_status = 'Active' " _
								& "and '" & session("leadno") & "' not in (select lwfs_lead_fk  from tbl_leadWorkFlowsStatus where lwfs_wfm_fk= wfm_tbl_pk and lwfs_leadststatus='Active' )  " _
								& "and wfm_leadstatto='" & dd_leadtype.selecteditem.text & "' " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpLTMatch " _
								& "from #tmpa " _
								& "where (wfm_filter1Usage='Include' and (x_descr='Owner' and x_type='leadtype') or wfm_filter1Usage='Do Not Use') " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpLPMatch " _
								& "from #tmpa " _
								& "where (wfm_filter2Usage='Include' and (x_descr='Take5' and x_type='leadprogram') or wfm_filter2Usage='Do Not Use') " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpLSMatch " _
								& "from #tmpa " _
								& "where (wfm_filter3Usage='Include' and (x_descr='Take5' and x_type='leadstatus') or wfm_filter3Usage='Do Not Use') " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpCONMatch " _
								& "from #tmpa " _
								& "where (wfm_filter4Usage='Include' and (x_descr='Take5' and x_type='contactstatus') or wfm_filter4Usage='Do Not Use') " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpADMatch " _
								& "from #tmpa " _
								& "where (wfm_filter5Usage='Include' and (wffilters_value='853' and wffilters_type='AD') or wfm_filter5Usage='Do Not Use') " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpMKTMatch " _
								& "from #tmpa " _
								& "where (wfm_filter6Usage='Include' and (x_descr='Take5' and x_type='marketprogram') or wfm_filter6Usage='Do Not Use') " _
								& "select  distinct a.wfm_tbl_pk " _
								& "from #tmpa a " _
								& "join #tmpLTMatch b on b.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "join #tmpLPMatch c on c.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "join #tmpLSMatch d on d.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "join #tmpCONMatch e on e.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "join #tmpADMatch f on f.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "join #tmpMKTMatch g on g.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "end"
		  
            Dim ad As New SqlDataAdapter(mycommand, myConnection)
            Dim ds As New DataSet()
            Dim str As New StringBuilder()

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
 					'response.write("____")
					'response.write(ds.Tables(0).Rows(i)(0).ToString())
              AddLeadtoWorkflow(ds.Tables(0).Rows(i)(0).ToString())
              

            Next
			
		  
		  end sub
		  sub checkforworkflowLP()
		  	'Check for matching workflows and store in data table
				Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
				
		  
             		mycommand = "begin " _
								& "select * " _
								& "into #tmpa " _ 
								& "from dbo.tbl_WorkFlowMaster " _
								& "left join dbo.tbl_WorkFlowFilters on wffilters_wfm_fk = wfm_tbl_pk " _
								& "left join dbo.tbl_xwalk on tbl_xwalk_pk = wffilters_value and wffilters_type <> 'AD' " _
								& "where wfm_useridfk='" & session("userid") & "' " _
								& "and wfm_trigger = 'On Lead Program Change' " _								
								& "and wfm_effdate <= getdate() and wfm_enddate >= getdate() " _
								& "and wfm_status = 'Active' " _
								& "and '" & session("leadno") & "' not in (select lwfs_lead_fk  from tbl_leadWorkFlowsStatus) " _
								& "and wfm_leadstatto='" & dd_leadprogram.selecteditem.text & "' " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpLTMatch " _
								& "from #tmpa " _
								& "where (wfm_filter1Usage='Include' and (x_descr='Owner' and x_type='leadtype') or wfm_filter1Usage='Do Not Use') " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpLPMatch " _
								& "from #tmpa " _
								& "where (wfm_filter2Usage='Include' and (x_descr='Take5' and x_type='leadprogram') or wfm_filter2Usage='Do Not Use') " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpLSMatch " _
								& "from #tmpa " _
								& "where (wfm_filter3Usage='Include' and (x_descr='Take5' and x_type='leadstatus') or wfm_filter3Usage='Do Not Use') " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpCONMatch " _
								& "from #tmpa " _
								& "where (wfm_filter4Usage='Include' and (x_descr='Take5' and x_type='contactstatus') or wfm_filter4Usage='Do Not Use') " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpADMatch " _
								& "from #tmpa " _
								& "where (wfm_filter5Usage='Include' and (wffilters_value='853' and wffilters_type='AD') or wfm_filter5Usage='Do Not Use') " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpMKTMatch " _
								& "from #tmpa " _
								& "where (wfm_filter6Usage='Include' and (x_descr='Take5' and x_type='marketprogram') or wfm_filter6Usage='Do Not Use') " _
								& "select  distinct a.wfm_tbl_pk " _
								& "from #tmpa a " _
								& "join #tmpLTMatch b on b.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "join #tmpLPMatch c on c.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "join #tmpLSMatch d on d.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "join #tmpCONMatch e on e.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "join #tmpADMatch f on f.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "join #tmpMKTMatch g on g.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "end"
		  
            Dim ad As New SqlDataAdapter(mycommand, myConnection)
            Dim ds As New DataSet()
            Dim str As New StringBuilder()

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
				
              AddLeadtoWorkflow(ds.Tables(0).Rows(i)(0).ToString())
              

            Next
			
		  
		  end sub
		  
		   sub checkforworkflowMP()
		  	'Check for matching workflows and store in data table
				Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
				
		  
             		mycommand = "begin " _
								& "select * " _
								& "into #tmpa " _ 
								& "from dbo.tbl_WorkFlowMaster " _
								& "left join dbo.tbl_WorkFlowFilters on wffilters_wfm_fk = wfm_tbl_pk " _
								& "left join dbo.tbl_xwalk on tbl_xwalk_pk = wffilters_value and wffilters_type <> 'AD' " _
								& "where wfm_useridfk='" & session("userid") & "' " _
								& "and wfm_trigger = 'On Marketing Program Change' " _								
								& "and wfm_effdate <= getdate() and wfm_enddate >= getdate() " _
								& "and wfm_status = 'Active' " _
								& "and '" & session("leadno") & "' not in (select lwfs_lead_fk  from tbl_leadWorkFlowsStatus) " _
								& "and wfm_leadstatto='" & dd_mkprg.selecteditem.text & "' " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpLTMatch " _
								& "from #tmpa " _
								& "where (wfm_filter1Usage='Include' and (x_descr='Owner' and x_type='leadtype') or wfm_filter1Usage='Do Not Use') " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpLPMatch " _
								& "from #tmpa " _
								& "where (wfm_filter2Usage='Include' and (x_descr='Take5' and x_type='leadprogram') or wfm_filter2Usage='Do Not Use') " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpLSMatch " _
								& "from #tmpa " _
								& "where (wfm_filter3Usage='Include' and (x_descr='Take5' and x_type='leadstatus') or wfm_filter3Usage='Do Not Use') " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpCONMatch " _
								& "from #tmpa " _
								& "where (wfm_filter4Usage='Include' and (x_descr='Take5' and x_type='contactstatus') or wfm_filter4Usage='Do Not Use') " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpADMatch " _
								& "from #tmpa " _
								& "where (wfm_filter5Usage='Include' and (wffilters_value='853' and wffilters_type='AD') or wfm_filter5Usage='Do Not Use') " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpMKTMatch " _
								& "from #tmpa " _
								& "where (wfm_filter6Usage='Include' and (x_descr='Take5' and x_type='marketprogram') or wfm_filter6Usage='Do Not Use') " _
								& "select  distinct a.wfm_tbl_pk " _
								& "from #tmpa a " _
								& "join #tmpLTMatch b on b.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "join #tmpLPMatch c on c.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "join #tmpLSMatch d on d.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "join #tmpCONMatch e on e.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "join #tmpADMatch f on f.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "join #tmpMKTMatch g on g.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "end"
		  
            Dim ad As New SqlDataAdapter(mycommand, myConnection)
            Dim ds As New DataSet()
            Dim str As New StringBuilder()

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
				
              AddLeadtoWorkflow(ds.Tables(0).Rows(i)(0).ToString())
              

            Next
			
		  
		  end sub
		  
		  sub checkforworkflowLS()
		  	'Check for matching workflows and store in data table
				Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
				
		  
             		mycommand = "begin " _
								& "select * " _
								& "into #tmpa " _ 
								& "from dbo.tbl_WorkFlowMaster " _
								& "left join dbo.tbl_WorkFlowFilters on wffilters_wfm_fk = wfm_tbl_pk " _
								& "left join dbo.tbl_xwalk on tbl_xwalk_pk = wffilters_value and wffilters_type <> 'AD' " _
								& "where wfm_useridfk='" & session("userid") & "' " _
								& "and wfm_trigger = 'On Lead Status Change' " _								
								& "and wfm_effdate <= getdate() and wfm_enddate >= getdate() " _
								& "and wfm_status = 'Active' " _
								& "and '" & session("leadno") & "' not in (select lwfs_lead_fk  from tbl_leadWorkFlowsStatus) " _
								& "and wfm_leadstatto='" & dd_ldstat.selecteditem.text & "' " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpLTMatch " _
								& "from #tmpa " _
								& "where (wfm_filter1Usage='Include' and (x_descr='Owner' and x_type='leadtype') or wfm_filter1Usage='Do Not Use') " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpLPMatch " _
								& "from #tmpa " _
								& "where (wfm_filter2Usage='Include' and (x_descr='Take5' and x_type='leadprogram') or wfm_filter2Usage='Do Not Use') " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpLSMatch " _
								& "from #tmpa " _
								& "where (wfm_filter3Usage='Include' and (x_descr='Take5' and x_type='leadstatus') or wfm_filter3Usage='Do Not Use') " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpCONMatch " _
								& "from #tmpa " _
								& "where (wfm_filter4Usage='Include' and (x_descr='Take5' and x_type='contactstatus') or wfm_filter4Usage='Do Not Use') " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpADMatch " _
								& "from #tmpa " _
								& "where (wfm_filter5Usage='Include' and (wffilters_value='853' and wffilters_type='AD') or wfm_filter5Usage='Do Not Use') " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpMKTMatch " _
								& "from #tmpa " _
								& "where (wfm_filter6Usage='Include' and (x_descr='Take5' and x_type='marketprogram') or wfm_filter6Usage='Do Not Use') " _
								& "select  distinct a.wfm_tbl_pk " _
								& "from #tmpa a " _
								& "join #tmpLTMatch b on b.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "join #tmpLPMatch c on c.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "join #tmpLSMatch d on d.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "join #tmpCONMatch e on e.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "join #tmpADMatch f on f.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "join #tmpMKTMatch g on g.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "end"
		  
            Dim ad As New SqlDataAdapter(mycommand, myConnection)
            Dim ds As New DataSet()
            Dim str As New StringBuilder()

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
				
              AddLeadtoWorkflow(ds.Tables(0).Rows(i)(0).ToString())
              

            Next
			
		  
		  end sub
		  
		  
    End Class
End Namespace