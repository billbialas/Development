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


Namespace PageTemplate
    Public Class addlead
        Inherits PageTemplate
        'public l_notesB,l_tasks,l_questions,l_vwcontact,l_vwreferals,
        Public subexportqueue, subnavtasks, subnavnotes, subnavprofile, subnavhistory, subrefer, subtasks, subnavfinance, submatches As HtmlTableCell
        Public spacer1, spacer1a, spacer2, spacer3, spacer4, spacer5, spacer6 As HtmlTableCell

        Public l_addreferal, l_print, l_saveexit As System.Web.UI.WebControls.Button
        Public lblleadno, lblassignedby, lblstatusA, lblcapdate, lblnofinancials As Label
        Public dd_agent, dd_refertype, dd_refervendor, dd_ldstat, ddLHType, dd_apptloc As DropDownList
        Public dd_status, dd_leadtype, dd_highpri, dd_source, dd_leadprogram, dd_statdetail As DropDownList
        Public l_fname, l_lname, l_hphone, l_cphone, l_email, l_address, l_city, l_state, l_zip, l_notes, l_email2, l_editcontact As TextBox
        Public l_appdate, l_appttime, l_comp, l_adcode, l_sendemailtxt, l_web, l_fax As TextBox
        Public scc, wls, npp, lsc, cwks, clp, clpo As CheckBox
        Public l_mailto As System.Web.UI.WebControls.CheckBox
        Public pnlnew, pnlack, pnltenant, pnlinitialnotes, pnlviewnotes, pnladdencoutner, pnlassignedby, pnladdreferal As Panel

        Public pnlplaceholder, pnladdreferalADD, pnladdreferalADDOther, pnladdreferalADDOther1, pnlplaceholderA As Panel
        Public pnlleadsource, pnlmorebutton, pnlmore, pnlcapdate, pnlprofile, pnlleadmore, pnlescalate, pnlclose, pnlmatch, leadinfo, pnlfinancials As Panel
        Public pnlpropmatch, pnltasks, pnlexportque As Panel
        Public l_edate, l_enotes, l_capdate As TextBox
        Public l_delete, l_cancel, l_contactfollowup, l_automatch As Button
        Private Shared pstatus, publeadreassigned As String
        Private Shared pubtblleadpk, pubcuspk, pubcusemail, pubreferco, pubrefertype As String
        Public l_savedraft, l_save, l_accept, l_close, l_sendemail As Button
        Private Shared assignedagentid, referemailsent As String
        Private Shared pubuidfullname, leadno, agentemail As String
        Private Shared assignedbyUID As String
        Private Shared totleads, referalleadpk As Integer
        Public lblstatus As Label
        Public reassign, leadmore, Escalate, more, lfinance, lnk_HistScreen, lexport As LinkButton
        Private Shared strnotask As String = "False"
        Private Shared strsavetype, hashistory, leadentrysource As String
        Public chkbed, chkcity, chkcredit, chkmovedt, chkrent, chkbath, chklevel, chkschool, chksec8 As CheckBox
        Protected WithEvents lNotes, lProfile, lTasks, lReferrals, lhistory, lmatches, btn_fullscreen As LinkButton
        Public myCheckbox As CheckBox

        Protected WithEvents lead_history, DGprofile, referals, DGPropMatches, dgtasks, exportqueue As System.Web.UI.WebControls.DataGrid

        Private Shared newleadno As String = ""
        Private Shared srchcriteria As String = ""
        Public darea As Table

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

            If Not (Page.IsPostBack) Then
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
                    leadentrysource = "Manual"
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
                    l_capdate.Text = DateTime.Now.ToShortDateString()
                    dd_agent.SelectedIndex = dd_agent.Items.IndexOf(dd_agent.Items.FindByText(Session("agentname")))
                    'dd_agent.SelectedItem.Text = Session("agentname")
                    dd_ldstat.SelectedItem.Text = "New"
                    ''.SelectedIndex = dd_ldstat.Items.IndexOf(dd_ldstat.Items.FindByText("New"))
                    subexportqueue.Visible = False

                ElseIf Request.QueryString("action") = "view" Then
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

                        If assignedbyUID = Session("userid") Or Session("role") = "Administrator" Then

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
                    subnav("Notes")
                End If
                If leadentrysource = "Auto" Then
                    l_adcode.Enabled = False
                    dd_source.Enabled = False
                Else
                    l_adcode.Enabled = True
                    dd_source.Enabled = True
                End If


            End If
            pagesetup()

        End Sub

        Sub doRollOver()
        End Sub

        Sub FillLeadProgram()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='LeadProgram' and (x_company='" & Session("company") & "' or x_company='All' or x_uid='" & Session("userid") & "')"
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

        Sub FillLeadtype()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='Leadtype' and (x_company='" & Session("company") & "' or x_company='All' or x_uid='" & Session("userid") & "')"
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
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='contactstatus' and (x_company='" & Session("company") & "' or x_company='All' or x_uid='" & Session("userid") & "')"
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
        Sub btn_histscreen(ByVal sender As Object, ByVal e As EventArgs)
            If btn_fullscreen.Text = "Full Screen" Then
                btn_fullscreen.Text = "Normal"
                Session("fullscreen") = "Yes"
                leadinfo.Visible = False
                '.Attributes("height") = "200"
                'darea.Attributes("class") = "tblcelltestba"
                'darea.Attributes.Add("class", "tblcelltestba")
            Else
                btn_fullscreen.Text = "Full Screen"
                Session("fullscreen") = "No"
                leadinfo.Visible = True
                'darea.Attributes("class") = "tblcelltestb"
                'darea.Attributes.Add("class", "tblcelltestb")
                'darea.Attributes.Add("class", "tblcelltestb")
                'darea.Attributes("height") = "100"

            End If

        End Sub

        Sub Leadescalate(ByVal sender As Object, ByVal e As EventArgs)

        End Sub
        Sub quicknote(ByVal Source As Object, ByVal e As ImageClickEventArgs)
            insertdb()
            'Response.Redirect("leadhistory.aspx?history=0&LeadNo=" & Request.QueryString("id") & "&LeadType=" & dd_leadtype.SelectedItem.Text & "&action=new")
            Response.Write("<script>window.open" & _
               "('leadhistory.aspx?history=0&type=history&LeadNo=" & Request.QueryString("id") & "&LeadType=" & dd_leadtype.SelectedItem.Text & "&action=new&source=popup','_new', 'width=800,height=500');</script>")
        End Sub
        Sub createtask(ByVal Source As Object, ByVal e As ImageClickEventArgs)
            insertdb()
            'Response.Redirect("leadhistory.aspx?history=0&LeadNo=" & Request.QueryString("id") & "&LeadType=" & dd_leadtype.SelectedItem.Text & "&action=new")
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
                mycommand = "Select *, cast(tbl_leadcnthistory_pk as varchar(20)) as 'hnum',cast(tbl_leads_fk as varchar(20)) as 'lnum2', convert(varchar(20),cnt_date,101) as date from tbl_leadscontacthistory Where tbl_leads_FK=" & strpropID
            Else
                mycommand = "Select *, cast(tbl_leadcnthistory_pk as varchar(20)) as 'hnum',cast(tbl_leads_fk as varchar(20)) as 'lnum2', convert(varchar(20),cnt_date,101) as date from tbl_leadscontacthistory Where tbl_leads_FK=" & strpropID & " and cnt_followup='Yes'"

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
            mycommand = "Select *, cast(lt_tbl_pk as varchar(20)) as 'ldtaskpk',cast(lt_leadpk_fk as varchar(20)) as 'leadpk', convert(varchar(20),lt_duedate,101) as 'duedate' from tbl_tasksuser Where lt_leadpk_fk='" & strpropID & "'"

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
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='LHType'"
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
            Dim myCommand As New SqlCommand("Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='leadsource' and (x_company='" & Session("company") & "' or x_company='All' or x_uid='" & Session("userid") & "')", myConnection)

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

        Sub btn_srchcriteria(ByVal sender As Object, ByVal e As EventArgs)
            bindsearchcriteria()
            pnlmatch.Visible = True
            pnlprofile.Visible = False

        End Sub

        Sub btn_matches(ByVal sender As Object, ByVal e As EventArgs)
            subnav("Matches")
            bindpropmatches()
        End Sub

        Sub btn_savesrch(ByVal sender As Object, ByVal e As EventArgs)
            If srchcriteria = "Existing" Then
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
                pstatus = "Draft"
                getnewleadno()
                getagentUID()
                insertdb()
                Response.Redirect("addlead.aspx?action=view&id=" & newleadno & "&source=profile")
            End If

            subnav("PropertyProfile")

        End Sub


        Sub btn_tasks(ByVal sender As Object, ByVal e As EventArgs)
            subnav("Tasks")

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
            publeadreassigned = "Yes"
            dd_agent.BackColor = System.Drawing.Color.Yellow
        End Sub

        Sub btn_saverefer(ByVal sender As Object, ByVal e As EventArgs)

            If Request.QueryString("action") = "new" Then
                pstatus = "Draft"
                getnewleadno()
                'response.write (newleadno)
                getagentUID()
                insertdb()
                referinsert()
                Response.Redirect("addlead.aspx?action=view&id=" & newleadno)

            Else
                pstatus = dd_status.SelectedItem.Text
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
            Dim myCommand As New SqlCommand("Select users_tbl_pk, fname + ' ' + lname as agent from dbo.tbl_users where company ='" & Session("company") & "' and status='Active' order by fname + ' ' + lname", myConnection)

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
                pstatus = dd_status.SelectedItem.Text
                getagentUID()
                insertdb()
                If dd_leadtype.Text = "Buyer" Or dd_leadtype.Text = "Tenant" Or dd_leadtype.Text = "Tenant Office" Then
                    Response.Redirect("leadprofile.aspx?profile=0&LeadNo=" & Request.QueryString("id") & "&LeadType=" & dd_leadtype.SelectedItem.Text & "&action=new")
                Else
                    Response.Redirect("leadprofileSell.aspx?profile=0&LeadNo=" & Request.QueryString("id") & "&LeadType=" & dd_leadtype.SelectedItem.Text & "&action=new")
                End If
            Else
                pstatus = "Draft"
                getnewleadno()
                getagentUID()
                insertdb()
                If dd_leadtype.Text = "Buyer" Or dd_leadtype.Text = "Tenant" Or dd_leadtype.Text = "Tenant Office" Then
                    Response.Redirect("leadprofile.aspx?profile=0&LeadNo=" & newleadno & "&LeadType=" & dd_leadtype.SelectedItem.Text & "&action=new")
                Else
                    Response.Redirect("leadprofileSell.aspx?profile=0&LeadNo=" & newleadno & "&LeadType=" & dd_leadtype.SelectedItem.Text & "&action=new")
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
            pstatus = dd_status.SelectedItem.Text
            getagentUID()
            insertdb()

        End Sub


        Public Sub btn_acceptlead(ByVal sender As Object, ByVal e As EventArgs)
            getagentfullname()
            dd_agent.SelectedIndex = dd_agent.Items.IndexOf(dd_agent.Items.FindByText(pubuidfullname))
            dd_status.SelectedIndex = dd_status.Items.IndexOf(dd_status.Items.FindByText("Accepted"))
            lblstatusA.Text = "Accepted"
            l_accept.Visible = False
            l_savedraft.Visible = False
            l_save.Visible = True
            pstatus = "Accepted"
            getagentUID()
            insertdb()
            test()
            inserthistory("Lead Accepted by " & Session("userid"))
            Response.Redirect("addlead.aspx?action=view&id=" & Request.QueryString("id"))

        End Sub

        Public Sub btn_save(ByVal sender As Object, ByVal e As EventArgs)
            If Request.QueryString("action") = "new" Then
                strsavetype = "exit"
            Else
                strsavetype = ""
            End If
            btn_saveDB()
        End Sub

        Public Sub btn_saveexit(ByVal sender As Object, ByVal e As EventArgs)
            strsavetype = "exit"
            btn_saveDB()
        End Sub

        Sub btn_saveDB()
            Dim agent As String
            If Request.QueryString("action") = "new" Then
                pstatus = dd_status.SelectedItem.Text
                getnewleadno()
                Response.Write(pstatus)
                getagentUID()
                If dd_agent.SelectedItem.Text <> Session("Agentname") Then
                    If dd_status.SelectedItem.Text = "Unaccepted" Then
                        getagentemail()
                        If dd_agent.SelectedItem.Text <> "Any" Then
                            sendemailagents(agentemail)
                        End If
                    End If
                End If
                If dd_agent.SelectedItem.Text = Session("Agentname") Then
                    pstatus = "Accepted"
                End If
                insertdb()
                test()
                inserthistory("Lead Submitted")
                agent = dd_agent.SelectedItem.Text
                inserthistory("Lead Assigned to " & agent)


            ElseIf Request.QueryString("action") = "view" Then
                pstatus = dd_status.SelectedItem.Text
                If pstatus = "Draft" Then
                    If dd_agent.SelectedItem.Text = Session("Agentname") Then
                        pstatus = "Accepted"
                    Else
                        pstatus = "Unaccepted"
                        getagentemail()
                        If dd_agent.SelectedItem.Text <> "Any" Then
                            sendemailagents(agentemail)
                        End If
                    End If
                    inserthistory("Lead Submitted")
                    agent = dd_agent.SelectedItem.Text
                    inserthistory("Lead Assigned to " & agent)
                    strsavetype = "exit"
                End If
                If publeadreassigned = "Yes" Then
                    If dd_agent.SelectedItem.Text <> "Any" Then
                        'pstatus= "Accepted"
                        getagentemail()
                        sendemailagents(agentemail)
                    End If
                    agent = dd_agent.SelectedItem.Text
                    inserthistory("Lead Re-Assigned to " & agent)
                    publeadreassigned = "No"
                End If
                If dd_ldstat.SelectedItem.Text = "Referred Out" Or dd_ldstat.SelectedItem.Text = "Sold Out" Then
                    pstatus = "Accepted"
                    inserthistory("Lead Sold or Referred")
                End If

                getagentUID()

                'insertdb()
                'test()
            End If
            Response.Write("SHit")
            'If strsavetype = "exit" Then
            'strsavetype = ""
            'If Request.QueryString("source") = "home" Then
            ' Response.Redirect("default.aspx")
            'Else
            'Response.Redirect(Session("qstring"))
            'End If

            'Else
            'strsavetype = ""
            'End If
        End Sub

        Public Sub btn_savedraft(ByVal sender As Object, ByVal e As EventArgs)
            pstatus = "Draft"

            If Request.QueryString("action") = "new" Then

                getnewleadno()

                'response.write (newleadno)
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
            Dim strSql As String = "delete from dbo.tbl_leads Where tbl_leads_pk=" & strpropID
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
            deletereferal()
            deletetasks()
            deletecontacthistory()
            If Request.QueryString("source") = "home" Then
                Response.Redirect("default.aspx")
            Else
                Response.Redirect(Session("qstring"))
            End If
            
        End Sub

        Sub deletetasks()
            Dim strpropID As String = Request.QueryString("id")
            Dim strSql As String = "delete from dbo.tbl_leadtasks Where leads_fk=" & strpropID

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

        Sub deletecontacthistory()
            Dim strpropID As String = Request.QueryString("id")
            Dim strSql As String = "delete from dbo.tbl_leadscontacthistory Where tbl_leads_fk=" & strpropID

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

        Sub deletereferal()
            Dim strpropID As String = Request.QueryString("id")
            Dim strSql As String = "delete from dbo.tbl_referals Where refer_lead_fk=" & strpropID

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
            Else
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
            Response.Write("OK")

            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
            If Request.QueryString("action") = "new" Then
                Dim prmleadno As New SqlParameter("@newleadno", SqlDbType.Int)
                prmleadno.Value = newleadno
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
            prmstatus.Value = pstatus
            myCommandADD.Parameters.Add(prmstatus)

            Dim prmleadtype As New SqlParameter("@l_leadtype", SqlDbType.VarChar, 30)
            prmleadtype.Value = dd_leadtype.SelectedItem.Text
            myCommandADD.Parameters.Add(prmleadtype)

            Dim prmnotes As New SqlParameter("@l_notes", SqlDbType.Text)
            If l_notes.Text = "" Then
                prmnotes.Value = DBNull.Value
            Else
                prmnotes.Value = l_notes.Text
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
            prmassignedagent.Value = assignedagentid
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

            Dim prmprogram As New SqlParameter("@ld_program", SqlDbType.VarChar, 20)
            prmprogram.Value = dd_leadprogram.SelectedItem.Text
            myCommandADD.Parameters.Add(prmprogram)

            Dim prmstatdetail As New SqlParameter("@ld_statdetail", SqlDbType.VarChar, 50)
            prmstatdetail.Value = l_web.text
            myCommandADD.Parameters.Add(prmstatdetail)

            Dim prmentrysource As New SqlParameter("@ld_entrysource", SqlDbType.VarChar, 50)
            prmentrysource.Value = leadentrysource
            myCommandADD.Parameters.Add(prmentrysource)

            Dim prmfax As New SqlParameter("@ld_fax", SqlDbType.VarChar, 50)
            If l_fax.Text = "" Then
                prmfax.Value = l_fax.Text
            Else
                prmfax.Value = DBNull.Value

            End If
            myCommandADD.Parameters.Add(prmfax)

            'response.write(prmtest)
            'response.write(prmldsource.value)

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

        Sub addtasks()
            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String

            If Request.QueryString("action") = "new" Or strnotask = "True" Then
                sqlproc = "sp_addleadtask"
            Else
                sqlproc = "sp_updateleadtask"
            End If

            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
            If Request.QueryString("action") = "new" Then
                Dim prmleadno As New SqlParameter("@leadfk", SqlDbType.Int)
                prmleadno.Value = newleadno
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

            If Request.QueryString("action") = "new" Or strnotask = "True" Then
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

            strnotask = "false"
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
                            agentemail = Sqldr("email2")
                        End If

                    End If
                Catch SQLexc As SqlException
                    Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
                Finally
                    myConnection.Close()
                End Try
            Else
                agentemail = "BPONotifications@gochoiceone.com"
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
                mailleadno = newleadno
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
                       l_notes.Text

            'send the message
            Dim smtp As New SmtpClient("smtp.comcast.net")
            smtp.Send(mail)

        End Sub

        Sub bindfields()
            Dim strUID As String = Session("userid")
            Dim strSql As String = "SELECT fname + ' ' + lname as assignedby, " _
          & "case when (select count(*) from tbl_leadscontacthistory where tbl_leads_fk = tbl_leads_pk and cnt_who <> 'System') > 0 then " _
          & "'Yes' else 'No' end as 'HasHistory',* " _
          & "from tbl_leads join dbo.tbl_users on Uid=ld_assignedbyuid where tbl_leads_pk =" & Request.QueryString("id")
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
                        hashistory = "Yes"
                    Else
                        hashistory = "No"
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
                        l_notes.Text = Sqldr("ld_notes")
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
                    assignedbyUID = Sqldr("ld_assignedbyuid")
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
                        leadentrysource = Sqldr("ld_entrysource")
                    End If
                    If Sqldr("ld_fax") IsNot DBNull.Value Then
                        l_fax.Text = Sqldr("ld_fax")
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
                    strnotask = "True"
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


        Sub getagentUID()
            If dd_agent.SelectedItem.Value <> 9999 Then
                Dim strSql As String = "SELECT UID from tbl_users where users_tbl_PK=" & dd_agent.SelectedItem.Value
                Dim sqlCmd As SqlCommand
                Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
                sqlCmd = New SqlCommand(strSql, myConnection)

                Try
                    myConnection.Open()
                    Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                    If Sqldr.Read() Then
                        assignedagentid = Sqldr("UID")

                    End If
                Catch SQLexc As SqlException
                    Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
                Finally
                    myConnection.Close()
                End Try
            Else
                assignedagentid = "Any"
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
                    pubuidfullname = Sqldr("agentname")

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
                prmleadpk.Value = newleadno
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
            prmreferby.Value = pubuidfullname
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
                    pubtblleadpk = Sqldr("pk")
                End If

            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        End Sub

        Sub getcusemail()
            Response.Write(dd_refervendor.SelectedItem.Value)
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
                pubtblleadpk = newleadno
            Else
                pubtblleadpk = Request.QueryString("id")
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
                       "Lead #: " & pubtblleadpk & vbCrLf & _
                       "Lead Type:   " & dd_leadtype.SelectedItem.Text & vbCrLf & _
                       "Lead Date:   " & l_capdate.Text & vbCrLf & _
                       "Name:        " & l_fname.Text & " " & l_lname.Text & vbCrLf & _
                       "Home Phone:  " & l_hphone.Text & vbCrLf & _
                       "Cell Phone:  " & l_cphone.Text & vbCrLf & _
                       "Email:       " & l_email.Text & vbCrLf & _
                       "Other Email: " & l_email2.Text & vbCrLf & _
                       "Notes:       " & vbCrLf & _
                       l_notes.Text



            'send the message
            Dim smtp As New SmtpClient("smtp.comcast.net")
            smtp.Send(mail)

        End Sub

        Sub referemailcheck()
            Dim strUID As String = Session("userid")
            Dim strSql As String = "SELECT * from tbl_referals where  refer_lead_fk = '" & pubtblleadpk & "' and refer_type ='" & pubrefertype & "' and refer_customer_fk = '" & pubcuspk & "'"
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    referemailsent = "Yes"
                Else
                    referemailsent = "No"
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
                    newleadno = Sqldr("newpk")
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
                strSql = "SELECT count(*) as 'cnt' from tbl_referals where refer_lead_fk=" & newleadno & " and (refer_emailsent is null or refer_emailsent='No')"
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
                    totleads = Sqldr("cnt")
                    'response.write(Sqldr("cnt"))
                End If

            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try

            If totleads > 0 Then
                sm(totleads)
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
                        pubcusemail = Sqldr("cus_email")
                        referalleadpk = Sqldr("tbl_referals_pk")
                    End If

                Catch SQLexc As SqlException
                    Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
                Finally
                    myConnection.Close()
                End Try
                updatereferemail(referalleadpk)
                'response.write (pubcusemail)
                sendemailcus(pubcusemail)
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
            l_notes.Enabled = stat
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
                prmleadno.Value = newleadno
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
            subnavnotes.Attributes.Add("class", "tblcelltest")
            subnavprofile.Attributes.Add("class", "tblcelltest")
            subnavhistory.Attributes.Add("class", "tblcelltest")
            subrefer.Attributes.Add("class", "tblcelltest")
            'subquestions.Attributes.Add("class", "tblcelltest")
            subnavtasks.Attributes.Add("class", "tblcelltest")
            subnavfinance.Attributes.Add("class", "tblcelltest")
            submatches.Attributes.Add("class", "tblcelltest")
            subexportqueue.Attributes.Add("class", "tblcelltest")

            'Set button font color
            lNotes.ForeColor = System.Drawing.Color.Black
            lProfile.ForeColor = System.Drawing.Color.Black
            lhistory.ForeColor = System.Drawing.Color.Black
            lfinance.ForeColor = System.Drawing.Color.Black
            lTasks.ForeColor = System.Drawing.Color.Black
            'lQuestions.ForeColor = System.Drawing.Color.Black
            lexport.ForeColor = System.Drawing.Color.Black
            lReferrals.ForeColor = System.Drawing.Color.Black
            lmatches.ForeColor = System.Drawing.Color.Black

            'Set spacers
            spacer1.Visible = True
            spacer1a.Visible = True
            spacer2.Visible = True
            spacer3.Visible = True
            spacer4.Visible = True
            spacer5.Visible = True
            spacer6.Visible = True


            'Set Panels
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
            If Session("fullscreen") = "Yes" Then
                leadinfo.Visible = False
            Else
                leadinfo.Visible = True
            End If

            If clickedbutton = "Notes" Then
                subnavnotes.Attributes.Add("class", "tblcelltestSelected")
                lNotes.ForeColor = System.Drawing.Color.White
                spacer1.Visible = False
                pnlinitialnotes.Visible = True

            ElseIf clickedbutton = "History" Then
                subnavhistory.Attributes.Add("class", "tblcelltestSelected")
                lhistory.ForeColor = System.Drawing.Color.White
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

                pnltasks.Visible = True
                bindtasks()

            ElseIf clickedbutton = "queue" Then
                subexportqueue.Attributes.Add("class", "tblcelltestSelected")
                lexport.ForeColor = System.Drawing.Color.White
                spacer4.Visible = False
                spacer5.Visible = False
                pnlexportque.Visible = True
                bindexportq()
                

            ElseIf clickedbutton = "Referrals" Then
                subrefer.Attributes.Add("class", "tblcelltestSelected")
                lReferrals.ForeColor = System.Drawing.Color.White
                spacer5.Visible = False
                spacer6.Visible = False
                pnladdreferal.Visible = True
                If Request.QueryString("action") = "view" Then
                    getreferals()
                End If

            ElseIf clickedbutton = "Matches" Then
                submatches.Attributes.Add("class", "tblcelltestSelected")
                lmatches.ForeColor = System.Drawing.Color.White
                spacer6.Visible = False
                pnlpropmatch.Visible = True
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
                    referemailsent = "Yes"
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

                    srchcriteria = "Existing"

                Else
                    srchcriteria = "New"

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
            Response.Write("<script>window.open" & _
                "('printlead.aspx?id=','_new', 'width=700,height=500');</script>")
        End Sub
        Public Sub emaillead(ByVal Source As Object, ByVal e As ImageClickEventArgs)
            Session("Clead") = Request.QueryString("ID")
            Response.Write("<script>window.open" & _
                "('emaillead.aspx?id=" & Request.QueryString("id") & "','mywindow','_new','scrollbars=1', 'width=900,height=700','mywindow.moveto(0,0)');</script>")
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
            footer.controls.add(LoadControl(System.Configuration.ConfigurationManager.AppSettings("ScreenFooter")))
            Header.Controls.Add(LoadControl(System.Configuration.ConfigurationManager.AppSettings("ScreenHeader")))
            leftNav.Controls.Add(LoadControl(System.Configuration.ConfigurationManager.AppSettings("ScreenLeftNav")))
           
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


    End Class
End Namespace