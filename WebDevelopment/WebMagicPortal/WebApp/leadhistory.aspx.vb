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
Imports FreeTextBoxControls


namespace PageTemplate
	public class leadhistory 
	   inherits PageTemplate
	   
        Public l_edate,  hst_action As TextBox
        Public ddLHType, ddlfollowup, ddLHstatus, ddLHwho, ddtasktype, ddtaskstat As DropDownList
        Public l_cancelcontact, l_delete, l_closedate, l_savecontact, deletetask, btn_addtask As Button
        Public lbltype,lbltask As Label
        Public l_duedate As TextBox
        Public pnl_addtask, pnl_leadhistory As Panel
       
        Public l_enotes As FreeTextBox
	   
        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

            If Not (Page.IsPostBack) Then
            	clearsessions()
                Session("ldhistorysaved") = "No"
                SessionInit()
                dobindings()

                If Request.QueryString("type") = "task" Then
                    bindtasktype()
                    bindtaskstat()
                    pnl_addtask.Visible = True
                    pnl_leadhistory.Visible = False
                     If Request.QueryString("action") = "view" Then
                        bindtasks()
                        deletetask.Visible = True
                    End If
                    lbltask.visible=true
                    lbltask.text="Tasks"

                Else

                    If Request.QueryString("action") = "view" Then
                        bindhistory()
                        l_cancelcontact.Text = "Exit"
                        l_delete.Visible = True
                        lbltype.Text = "Edit"
                        If session("strleadwho") = "System" Then
                            l_delete.Visible = False
                            l_savecontact.Visible = False
                        End If
                        If session("leadassigned") = "Yes" Then
                            btn_addtask.visible = False
                        End If
                    Else
                        l_cancelcontact.Text = "Cancel"
                        l_delete.Visible = False
                        lbltype.Text = "Add"
                        l_savecontact.Visible = True
                    End If

                End If
            End If
            pagesetup()

        End Sub
        sub clearsessions()
        		session("strleadwho")=""
        		session("histpk")=""
        		session("taskpk")=""
        		session("leadassigned")="No"
        
        
        end sub

        Sub SessionInit()

            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            l_edate.text = rightNow
            l_enotes.Text = ""
            l_duedate.Text = rightNow
            hst_action.Text = ""
            deletetask.Visible = False
        End Sub
	
        Sub dobindings()

            bindLHType()

        End Sub
 	
        Sub statuschange(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            If ddLHstatus.selecteditem.text = "Closed" Then
                l_closedate.text = rightNow
            End If
        End Sub
 	
        Sub btn_cancelcontact(ByVal sender As Object, ByVal e As EventArgs)
            If Request.QueryString("action") = "new" And Session("ldhistorysaved") = "Yes" Then

                gethistpk()
                Dim strpropID As String = Request.QueryString("history")
                Dim strSql As String = "delete from dbo.tbl_leadscontacthistory Where tbl_leadcnthistory_pk=" & session("histpk")
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
            End If
            If Request.QueryString("source") = "main" Then
                Response.Redirect(Session("qstring"))
            ElseIf Request.QueryString("source") = "popup" Then
                Dim msg As String = ""
                msg = msg & "<Script Language='JavaScript'>"
                msg = msg & "window.opener.location.href=window.opener.location.href;"
                msg = msg & "window.close();"
                msg = msg & "</Script>"
                Response.Write(msg)
            Else
                Response.Redirect("addlead.aspx?action=view&id=" & Request.QueryString("LeadNo") & "&source=history")
            End If
        End Sub
        Sub click_addtask(ByVal sender As Object, ByVal e As EventArgs)
            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            l_duedate.Text = rightNow
            hst_action.Text = ""
            If (Request.QueryString("action") = "new" And Request.QueryString("type") = "history") Then
                If Session("ldhistorysaved") = "No" Then
                    insertdb()
                    Session("ldhistorysaved") = "Yes"
                Else
                    gethistpk()
                    insertdb()
                End If

            Else

            End If
            bindtasktype()
            bindtaskstat()
            pnl_addtask.Visible = True
        End Sub
        Public Sub btn_savecontact(ByVal sender As Object, ByVal e As EventArgs)

            insertdb()
            If Request.QueryString("source") = "main" Then
                Response.Redirect(Session("qstring"))
            ElseIf Request.QueryString("source") = "popup" Then
                Dim msg As String = ""
                msg = msg & "<Script Language='JavaScript'>"
                msg = msg & "window.opener.location.href=window.opener.location.href;"
                msg = msg & "window.close();"
                msg = msg & "</Script>"
                Response.Write(msg)
            Else
                Response.Redirect("addlead.aspx?action=view&id=" & Request.QueryString("LeadNo") & "&source=history")
            End If
        End Sub
        Sub click_savetask(ByVal sender As Object, ByVal e As EventArgs)
            inserttask()
            If Request.QueryString("type") = "history" Then
                updatehistrecordwithtaskno()
            End If

            If Request.QueryString("source") = "popup" And Request.QueryString("type") = "task" Then
                Dim msg As String = ""
                msg = msg & "<Script Language='JavaScript'>"
                msg = msg & "window.opener.location.href=window.opener.location.href;"
                msg = msg & "window.close();"
                msg = msg & "</Script>"
                Response.Write(msg)
            ElseIf Request.QueryString("source") = "popup" And Request.QueryString("type") = "history" Then
                pnl_addtask.Visible = False
            ElseIf Request.QueryString("type") = "history" Then
                pnl_addtask.Visible = False
            Else
            		if Request.QueryString("LeadNo")="0" then
            			Response.Redirect("default.aspx")
            		else
            			Response.Redirect("addlead.aspx?action=view&id=" & Request.QueryString("LeadNo") & "&source=task")
            		end if
                

            End If
        End Sub
        Sub btn_canceltask(ByVal sender As Object, ByVal e As EventArgs)

            If Request.QueryString("source") = "popup" And Request.QueryString("type") = "task" Then
                Dim msg As String = ""
                msg = msg & "<Script Language='JavaScript'>"
                msg = msg & "window.opener.location.href=window.opener.location.href;"
                msg = msg & "window.close();"
                msg = msg & "</Script>"
                Response.Write(msg)
            ElseIf Request.QueryString("source") = "popup" And Request.QueryString("type") = "history" Then
                pnl_addtask.Visible = False
            ElseIf Request.QueryString("type") = "history" Then
                pnl_addtask.Visible = False
            Else
            if Request.QueryString("LeadNo")="0" then
            			Response.Redirect("default.aspx")
            		else
            			Response.Redirect("addlead.aspx?action=view&id=" & Request.QueryString("LeadNo") & "&source=task")
            		end if
                
               
            End If
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
        Sub bindtasktype()

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='tasktype' and (x_company='" & Session("company_pk") & "' or x_company='All' or x_uid='" & Session("userid") & "')"
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                ddtasktype.DataSource = dataReader
                ddtasktype.DataTextField = "x_descr"
                ddtasktype.DataValueField = "tbl_xwalk_pk"
                ddtasktype.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

        End Sub
        Sub bindtaskstat()

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='taskstatus' and (x_company='" & Session("company_pk") & "' or x_company='All' or x_uid='" & Session("userid") & "')"
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                ddtaskstat.DataSource = dataReader
                ddtaskstat.DataTextField = "x_descr"
                ddtaskstat.DataValueField = "tbl_xwalk_pk"
                ddtaskstat.DataBind()
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
            If Request.QueryString("action") = "new" And Session("ldhistorysaved") = "No" Then
                sqlproc = "sp_insertleadcontact"
            ElseIf Request.QueryString("action") = "new" And Session("ldhistorysaved") = "Yes" Then
                sqlproc = "sp_updateleadcontact"
            ElseIf Request.QueryString("action") = "view" Then

                sqlproc = "sp_updateleadcontact"
            End If


            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
            If Request.QueryString("action") = "view" Then
                Dim prmpk As New SqlParameter("@tblpk", SqlDbType.Int)
                prmpk.Value = Request.QueryString("history")
                myCommandADD.Parameters.Add(prmpk)
            End If
            If Request.QueryString("action") = "new" And Session("ldhistorysaved") = "Yes" Then
                Dim prmpk As New SqlParameter("@tblpk", SqlDbType.Int)
                prmpk.Value = session("histpk")
                myCommandADD.Parameters.Add(prmpk)
            End If

            Dim prmleadno As New SqlParameter("@leadfk", SqlDbType.Int)
            prmleadno.Value = Request.QueryString("leadno")
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

            Dim prmfollowup As New SqlParameter("@followup", SqlDbType.VarChar, 50)
            prmfollowup.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmfollowup)

            Dim prmaction As New SqlParameter("@followupactions", SqlDbType.Text)
            prmaction.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmaction)

            Dim prmstatus As New SqlParameter("@LHstat", SqlDbType.VarChar, 50)
            prmstatus.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmstatus)

            Dim prmwho As New SqlParameter("@LHwho", SqlDbType.VarChar, 50)
            prmwho.Value = ddLHwho.SelectedItem.Text
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
         
        Public Sub updatehistrecordwithtaskno()
            gethistpk()
            gettaskpk()
            updatehistrecord()

        End Sub

        Public Sub btn_deletecontact(ByVal sender As Object, ByVal e As EventArgs)

            Dim strpropID As String = Request.QueryString("history")
            Dim strSql As String = "delete from dbo.tbl_leadscontacthistory Where tbl_leadcnthistory_pk=" & strpropID
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


            Response.Redirect("addlead.aspx?action=view&id=" & Request.QueryString("LeadNo") & "&source=history")


        End Sub
        Public Sub btn_deletetask(ByVal sender As Object, ByVal e As EventArgs)

            Dim strpropID As String = Request.QueryString("task")
            Dim strSql As String = "delete from dbo.tbl_tasksuser Where lt_tbl_pk=" & strpropID
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
            If Request.QueryString("source") = "popup" And Request.QueryString("type") = "task" Then
                Dim msg As String = ""
                msg = msg & "<Script Language='JavaScript'>"
                msg = msg & "window.opener.location.href=window.opener.location.href;"
                msg = msg & "window.close();"
                msg = msg & "</Script>"
                Response.Write(msg)
            Else
                Response.Redirect("addlead.aspx?action=view&id=" & Request.QueryString("LeadNo") & "&source=task")

            End If
        End Sub
         
        Sub inserttask()

            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String
            If Request.QueryString("action") = "new" Then
                sqlproc = "sp_insertleadtask"
            ElseIf (Request.QueryString("action") = "view" And Request.QueryString("type") = "task") Then
                sqlproc = "sp_updateleadtaskuser"
            ElseIf Request.QueryString("type") = "history" Then
                sqlproc = "sp_insertleadtask"
            End If

            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
            If (Request.QueryString("action") = "view" And Request.QueryString("type") = "task") Then
                Dim prmpk As New SqlParameter("@tblpk", SqlDbType.Int)
                prmpk.Value = Request.QueryString("task")
                myCommandADD.Parameters.Add(prmpk)
            End If

            Dim prmuid As New SqlParameter("@uid", SqlDbType.VarChar, 50)
            prmuid.Value = Session("userid")
            myCommandADD.Parameters.Add(prmuid)

				
            Dim prmleadno As New SqlParameter("@leadfk", SqlDbType.Int)
            If (Request.QueryString("action") = "new" And Request.QueryString("type") = "task" and Request.QueryString("LeadNo")="0") Then
            	prmleadno.Value = dbnull.value
            else
            	prmleadno.Value = Request.QueryString("leadno")
            end if
            myCommandADD.Parameters.Add(prmleadno)

            Dim prmtype As New SqlParameter("@tkType", SqlDbType.VarChar, 50)
            prmtype.Value = ddtasktype.SelectedItem.Text
            myCommandADD.Parameters.Add(prmtype)

            Dim prmstatus As New SqlParameter("@tkstatus", SqlDbType.VarChar, 50)
            prmstatus.Value = ddtaskstat.SelectedItem.Text
            myCommandADD.Parameters.Add(prmstatus)

            Dim prmfduedt As New SqlParameter("@tkduedate", SqlDbType.DateTime)
            prmfduedt.Value = l_duedate.Text
            myCommandADD.Parameters.Add(prmfduedt)

            Dim prmnotes As New SqlParameter("@tkdescr", SqlDbType.Text)
            prmnotes.Value = hst_action.Text
            myCommandADD.Parameters.Add(prmnotes)

            Try
                myConnectionADD.Open()
                myCommandADD.ExecuteNonQuery()
                myConnectionADD.Close()
            Catch SQLexc As SqlException
                Response.Write("Insert Failed. Error Details are: " & SQLexc.ToString())
            End Try
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
            'leftNav.Controls.Add(LoadControl(System.Configuration.ConfigurationManager.AppSettings("ScreenLeftNav")))
                 	MiddleNav.Controls.Add(LoadControl(System.Configuration.ConfigurationManager.AppSettings("ScreenmiddleNav")))
     
            body.VAlign = "top"            
            'leftNav.VAlign = "top"
            
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
	
        Sub bindhistory()
            Dim strUID As String = Session("userid")
            Dim strSql As String = "SELECT * from tbl_leadscontacthistory Where tbl_leadcnthistory_pk=" & Request.QueryString("history")
            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    If Sqldr("cnt_date") IsNot DBNull.Value Then
                        l_edate.Text = Sqldr("cnt_date")
                    End If
                    If Sqldr("cnt_type") IsNot DBNull.Value Then
                        ddLHType.SelectedIndex = ddLHType.Items.IndexOf(ddLHType.Items.FindByText(Sqldr("cnt_type")))
                    End If
                    If Sqldr("cnt_notes") IsNot DBNull.Value Then
                        l_enotes.Text = Sqldr("cnt_notes")
                    End If
                    ' If Sqldr("cnt_followup") IsNot DBNull.Value Then
                    ' ddlfollowup.SelectedIndex = ddlfollowup.Items.IndexOf(ddlfollowup.Items.FindByText(Sqldr("cnt_followup")))
                    'End If
                    'If Sqldr("cnt_followupaction") IsNot DBNull.Value Then
                    'hst_action.Text = Sqldr("cnt_followupaction")
                    'End If

                    ' If Sqldr("cnt_closedt") IsNot DBNull.Value Then
                    'l_closedate.Text = Sqldr("cnt_closedt")
                    'End If
                    'I'f Sqldr("cnt_fduedate") IsNot DBNull.Value Then
                    'l_duedate.Text = Sqldr("cnt_fduedate")
                    'End If
                    'If Sqldr("cnt_status") IsNot DBNull.Value Then
                    'ddLHstatus.SelectedIndex = ddLHstatus.Items.IndexOf(ddLHstatus.Items.FindByText(Sqldr("cnt_status")))
                    'End If
                    If Sqldr("cnt_who") IsNot DBNull.Value Then
                        session("strleadwho") = Sqldr("cnt_who")
                        ddLHwho.SelectedIndex = ddLHwho.Items.IndexOf(ddLHwho.Items.FindByText(Sqldr("cnt_who")))
                    End If
                    If Sqldr("cnt_leadtask") IsNot DBNull.Value Then
                        session("leadassigned") = "Yes"
                    End If


                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        End Sub

        Public Sub gethistpk()
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()


            Dim strSql As String = " select max(tbl_leadcnthistory_PK) as 'histpk'  from tbl_leadscontacthistory where tbl_leads_FK='" & Request.QueryString("leadno") & "' " _
                        & " and cnt_agentid='" & Session("userid") & "'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)

                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    session("histpk") = Sqldr("histpk")
                End If
            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
        End Sub

        Public Sub gettaskpk()
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()


            Dim strSql As String = " select max(lt_tbl_pk) as 'taskpk'  from tbl_tasksuser where lt_leadpk_fk='" & Request.QueryString("leadno") & "' " _
                        & " and lt_uid='" & Session("userid") & "'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)

                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    session("taskpk") = Sqldr("taskpk")
                End If
            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
        End Sub
        Public Sub updatehistrecord()
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()


            Dim strSql As String = "update  tbl_leadscontacthistory set cnt_leadtask='" &  session("taskpk") & " ' where tbl_leadcnthistory_PK='" & session("histpk") & "'"

            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)

                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    session("taskpk") = Sqldr("taskpk")
                End If
            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
        End Sub
        Sub bindtasks()
            Dim strUID As String = Session("userid")
            Dim strSql As String = "SELECT * from tbl_tasksuser Where lt_tbl_pk='" & Request.QueryString("task") & "'"
            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    If Sqldr("lt_type") IsNot DBNull.Value Then
                        ddtasktype.SelectedIndex = ddtasktype.Items.IndexOf(ddtasktype.Items.FindByText(Sqldr("lt_type")))
                    End If
                    If Sqldr("lt_status") IsNot DBNull.Value Then
                        ddtaskstat.SelectedIndex = ddtaskstat.Items.IndexOf(ddtaskstat.Items.FindByText(Sqldr("lt_status")))
                    End If
                    If Sqldr("lt_desc") IsNot DBNull.Value Then
                        hst_action.Text = Sqldr("lt_desc")
                    End If
                    If Sqldr("lt_duedate") IsNot DBNull.Value Then
                        l_duedate.Text = Sqldr("lt_duedate")
                    End If
                End If

            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        End Sub
   end class
end namespace