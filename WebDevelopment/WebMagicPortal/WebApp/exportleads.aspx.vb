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
Imports System.Configuration
Imports System.Text
Imports System.Net.Mail

namespace PageTemplate
    Public Class exportleads
        Inherits PageTemplate

        Public ddexportque As DropDownList
        Public pnlemailconfirm, pnlemail, pnlnoleads, pnladdingleads, pnlmgmtq, pnlqleads, pnladdqstuff, pnlleadpreview, pnladdtoq As Panel
        Public exportq, qleads, x, dgleadpreview As DataGrid
        Public qdescription, qcount, search, ldtype, ldstat, assignedstat, assignedto, assigendby, adcode, totleadcount, entrysource As Label
        Public newqname, newqdesc, emailfrom, emailsubject As TextBox
        public emailbody
        Public btnmanageque, btnaddtoq, btnviewleads, btnquickexport As Button
        Public exp
        Public ddemailcor As DropDownList
        Public noemails As Label
        Public fdate, tdate As TextBox

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles mybase.load

            If Not (Page.IsPostBack) Then
            	clearsessions()
                checkqstring()
                fillexportque()

            End If
            pagesetup()

        End Sub
        sub clearsessions()
        	session("filterrowcount")=0
        		session("selectedleadcount")=0
        		session("totqueuecount")=0
        		session("exporttype")=""
      
        
        end sub
        Sub addque(ByVal Source As System.Object, ByVal e As System.EventArgs)
            pnladdqstuff.Visible = True
            newqname.Text = ""
            newqdesc.Text = ""
            bindexportq()
        End Sub
        Sub exportleads(ByVal Source As System.Object, ByVal e As System.EventArgs)
            pnlnoleads.Visible = False
            pnladdingleads.Visible = False
            pnlleadpreview.Visible = False
            pnladdtoq.Visible = True

            'addleadstoq(ddexportque.SelectedItem.Value)

        End Sub
        Sub emailleadsA(ByVal Source As System.Object, ByVal e As System.EventArgs)
            pnlnoleads.Visible = False
            pnladdingleads.Visible = False
            pnlleadpreview.Visible = False
            pnladdtoq.Visible = True
            btnquickexport.visible = False
        End Sub

        Sub exportgo(ByVal Source As System.Object, ByVal e As System.EventArgs)
            
        End Sub
        Sub exportall(ByVal Source As System.Object, ByVal e As System.EventArgs)
            pnladdingleads.Visible = True
            addleadstoq("5")
            If session("selectedleadcount") = 0 Then
                pnlnoleads.Visible = True
                pnladdingleads.Visible = False
            Else
                totleadcount.Text = session("selectedleadcount")
                pnlnoleads.Visible = False
            End If
        End Sub
        Sub manageq(ByVal Source As System.Object, ByVal e As System.EventArgs)
            pnlmgmtq.Visible = True
            pnlnoleads.Visible = False
            pnladdingleads.Visible = False
            bindexportq()

        End Sub
        Sub returnp(ByVal Source As System.Object, ByVal e As System.EventArgs)


        End Sub
        Sub Addq(ByVal Source As System.Object, ByVal e As System.EventArgs)
           
        End Sub
        Sub removeleadq(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(0).Text
            Dim content2 As String = item.Cells(5).Text
            qleads.CurrentPageIndex = 0

            removeleadq(content)
            bindqleads(content2)
            bindexportq()

        End Sub
        Sub removeleadqP(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(0).Text

            removeleadPreview(content)
            updatepreviewcount()
            bindpreviewleads()

        End Sub
        Sub savenewq(ByVal Source As System.Object, ByVal e As System.EventArgs)
            If checkqueueexists(newqname.Text) Then
                newqname.BackColor = Red
                newqname.Text = "Name Already Exists"
            Else
                Dim strConnection As String
                Dim sqlConn As SqlConnection
                Dim sqlCmd As SqlCommand
                Dim strSql As String = "insert into tbl_leadexportqueue (eq_name,eq_uid,eq_description) values ('" & newqname.Text & "','" & Session("userid") & "','" & newqdesc.Text & "')"
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

                pnladdqstuff.Visible = False
                fillexportque()
                bindexportq()
                ddexportque.SelectedIndex = ddexportque.Items.IndexOf(ddexportque.Items.FindByText(newqname.Text))

                'ddexportque.SelectedItem.Text = newqname.Text
                changeddexpdesc()
            End If

        End Sub
        Public Sub changeddexpdesc()
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select eq_description,count(eqd_eq_fk) as 'cnt' " _
                  & "from tbl_leadexportqueue " _
                  & "left join tbl_leadexportqueuedetail on eqd_eq_fk = eq_tbl_pk " _
                  & "where eq_tbl_pk='" & ddexportque.SelectedItem.Value & "' " _
                  & "group by eq_description"
            Dim cs As String
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    qdescription.Text = Sqldr("eq_description")
                    qcount.Text = Sqldr("cnt")
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
        End Sub
        Public Function checkqueueexists(ByVal name As String) As Boolean

            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select * from tbl_leadexportqueue where eq_name='" & newqname.Text & "' and eq_uid='" & Session("userid") & "'"
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
        Sub quickexport(ByVal Source As System.Object, ByVal e As System.EventArgs)
            session("exporttype") = "Quick"
            Session("state") = "quick"
            Dim openWindowScript As String = "<script Language='JavaScript'>window.open('ldexport.aspx?exptype=quick');</script>"
            Response.Write(openWindowScript)

            'If session("exporttype") = "Quick" Then

            'getleads("Quick")
            'deletetempq()
            'Else
            'getleads("Queue")
            'End If
        End Sub

        Sub mangaeleads(ByVal Source As System.Object, ByVal e As System.EventArgs)
            pnlqleads.Visible = True
            exportq.Visible = False
            qleads.CurrentPageIndex = 0
            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(0).Text
            'Response.Write(content)
            bindqleads(content)
            Session("cqueue") = content
        End Sub
        Sub manageque(ByVal Source As System.Object, ByVal e As System.EventArgs)
            pnlmgmtq.Visible = True
            pnladdqstuff.Visible = False
            pnladdtoq.Visible = False
            pnlleadpreview.Visible = False
            pnladdingleads.Visible = False

        End Sub
        Sub canceladdq(ByVal Source As System.Object, ByVal e As System.EventArgs)
            pnladdqstuff.Visible = False

        End Sub
        Sub exportleadsQ(ByVal Source As System.Object, ByVal e As System.EventArgs)
            session("exporttype") = "Queue"
            Session("state") = "queue"
            Dim openWindowScript As String = "<script Language='JavaScript'>window.open('ldexport.aspx?exptype=Queue&queue=" & ddexportque.SelectedItem.Value & "');</script>"
            Response.Write(openWindowScript)

        End Sub
        Sub RemoveQ(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(0).Text
            exportq.CurrentPageIndex = 0
            deleteexpque(content)
            bindexportq()

        End Sub
        Sub EmailQ(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Session("currentq") = ""
            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(0).Text
            showemailpnl()
            Session("currentq") = content
        End Sub
        Sub showemailpnl()
            emailfrom.Text = ""
            emailsubject.Text = ""
            emailbody.content = ""
            pnlmgmtq.Visible = False
            pnlemail.Visible = True
            Fillemailcor()
        End Sub
        Sub sendemail(ByVal ldno As String, ByVal emailid As String)
            Dim mail As New MailMessage()

            'Set the properties - send the email to the person who filled out the
            mail.From = New MailAddress(emailfrom.Text)
            mail.To.Add(emailid)
            mail.bcc.add(emailid)
            'If emailcc.Text <> "" Then
            'mail.CC.Add(emailcc.Text)
            'End If
            If emailsubject.Text <> "" Then
                mail.Subject = emailsubject.Text.Replace(vbCrLf, "")
            Else
                mail.Subject = ""
            End If

            'Set the body
            If emailbody.content <> "" Then
                mail.Body = emailbody.content
            End If
            mail.IsBodyHtml = True
            'send the message
           'Dim smtp As New SmtpClient("192.168.235.11")
				Dim smtp As New SmtpClient("192.168.235.12")
           smtp.ServicePoint.MaxIdleTime = 0
           smtp.Send(mail)
           
            inserthistoryrecord(ldno, emailid)

        End Sub
        Sub inserthistoryrecord(ByVal id As String, ByVal emto As String)

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
            prmleadno.Value = id
            myCommandADD.Parameters.Add(prmleadno)

            Dim prmcapdate As New SqlParameter("@capdate", SqlDbType.DateTime)
            prmcapdate.Value = rightNow
            myCommandADD.Parameters.Add(prmcapdate)

            Dim prmnotes As New SqlParameter("@l_notes", SqlDbType.Text)
            Dim nt As String
            nt = "To: " & emto & vbCrLf
            nt = nt & "Subject: " & emailsubject.Text & vbCrLf
            nt = nt & "Body: " & emailbody.content & vbCrLf
            prmnotes.Value = nt
            myCommandADD.Parameters.Add(prmnotes)

            Dim prmuid As New SqlParameter("@uid", SqlDbType.VarChar, 50)
            prmuid.Value = Session("userid")
            myCommandADD.Parameters.Add(prmuid)

            Dim prmtype As New SqlParameter("@LHType", SqlDbType.VarChar, 50)
            prmtype.Value = "Email"
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
            prmwho.Value = "Lead"
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
       
        Public Sub click_sendemail(ByVal sender As Object, ByVal e As EventArgs)
           
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            mycommand = "Select eqd_leadno,case when (ld_email is null or ld_email ='') then ld_email2 else ld_email end as 'ldemail' " _
                & "from tbl_leads join tbl_leadexportqueuedetail on eqd_leadno=tbl_leads_pk " _
                & "where (eqd_eq_fk ='" & Session("currentq") & "' and ((ld_email is not null and ld_email <> '') or " _
                & "(ld_email2 is not null and ld_email2 <> ''))) "
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

            Dim i As Integer
            For i = 0 To ds.Tables(0).Rows.Count - 1
                'Response.Write(ds.Tables(0).Rows(i)(1).ToString())
                sendemail(ds.Tables(0).Rows(i)(0).ToString(), ds.Tables(0).Rows(i)(1).ToString())
            Next
            noemails.Text = ds.Tables(0).Rows.Count
            pnlmgmtq.Visible = False
            pnlemail.Visible = False
            pnlemailconfirm.Visible = True

        End Sub
        Sub previewleads(ByVal Source As System.Object, ByVal e As System.EventArgs)
            If btnviewleads.Text = "Preview Leads" Then
                pnlleadpreview.Visible = True
                bindpreviewleads()
                btnviewleads.Text = "Hide Preview"
            Else
                pnlleadpreview.Visible = False

                btnviewleads.Text = "Preview Leads"
            End If


           
        End Sub
        Sub backtoleads(ByVal Source As System.Object, ByVal e As System.EventArgs)
            deletetempq()
            Session("state") = ""

            If Request.QueryString("source") = "single" Then
                Response.Redirect("addlead.aspx?action=view&id=" & Request.QueryString("leadno"))

            Else
                Response.Redirect(Session("qstring"))
            End If

        End Sub
       
        Sub click_emcontinue(ByVal Source As System.Object, ByVal e As System.EventArgs)
            pnlmgmtq.Visible = True
            pnlemail.Visible = False
            pnlemailconfirm.Visible = False
        End Sub

        Sub hideqd(ByVal Source As System.Object, ByVal e As System.EventArgs)
            pnlqleads.Visible = False
            exportq.Visible = True
            Session("cqueue") = ""
            qleads.CurrentPageIndex = 0
        End Sub

        Sub addtoque(ByVal Source As System.Object, ByVal e As System.EventArgs)
            insertleadsintoq()

            btnmanageque.Visible = True
            btnaddtoq.Visible = False
            deletetempq()
            bindexportq()
            pnlmgmtq.Visible = True
            pnladdtoq.Visible = False
            If Request.QueryString("source") = "single" Then
                inserthistory("Lead added to export queue")
            End If
        End Sub
        Sub insertleadsintoq()
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "insert into tbl_leadexportqueuedetail (eqd_eq_fk,eqd_leadno) select '" & ddexportque.SelectedItem.Value & "' , tmpq_leadpk from tbl_exporttemp " _
            & "where tmpq_userid='" & Session("userid") & "' and tmpq_leadpk not in (select eqd_leadno from  tbl_leadexportqueuedetail where tmpq_userid='" & Session("userid") & "' and eqd_eq_fk='" & ddexportque.SelectedItem.Value & "')"

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

        Sub fillexportque()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select distinct eq_tbl_pk,eq_name from tbl_leadexportqueue where eq_uid='" & Session("userid") & "'"
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                ddexportque.DataSource = dataReader
                ddexportque.DataTextField = "eq_name"
                ddexportque.DataValueField = "eq_tbl_pk"

                ddexportque.DataBind()

            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
            Dim a As Integer = ddexportque.Items.Count
            If a > 0 Then
                getdescript()
                btnmanageque.Visible = True
                btnaddtoq.Visible = True
            Else
                ddexportque.Items.Insert(0, New ListItem("None Found", "99999"))
                btnmanageque.Visible = False
                btnaddtoq.Visible = False

            End If

        End Sub
        Sub fltrbydate(ByVal Source As System.Object, ByVal e As System.EventArgs)
            deletetempq()
            addleadstoq("5")
            totleadcount.Text = session("selectedleadcount")

        End Sub
        Sub clearfltr(ByVal Source As System.Object, ByVal e As System.EventArgs)
            deletetempq()
            addleadstoq("5")
            totleadcount.Text = session("selectedleadcount")

        End Sub

        Sub chgqdesc(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select eq_description,count(eqd_eq_fk) as 'cnt' " _
                  & "from tbl_leadexportqueue " _
                  & "left join tbl_leadexportqueuedetail on eqd_eq_fk = eq_tbl_pk " _
                  & "where eq_tbl_pk='" & ddexportque.SelectedItem.Value & "' " _
                  & "group by eq_description"
            Dim cs As String
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    qdescription.Text = Sqldr("eq_description")
                    qcount.Text = Sqldr("cnt")
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try

        End Sub
        Public Sub getdescript()
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select eq_description,count(eqd_eq_fk) as 'cnt' " _
                & "from tbl_leadexportqueue " _
                & "left join tbl_leadexportqueuedetail on eqd_eq_fk = eq_tbl_pk " _
                & "where eq_tbl_pk='" & ddexportque.SelectedItem.Value & "' " _
                & "group by eq_description"
            Dim cs As String
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    If Sqldr("eq_description") IsNot DBNull.Value Then
                        qdescription.Text = Sqldr("eq_description")
                        qcount.Text = Sqldr("cnt")
                    Else
                        qdescription.Text = ""
                        qcount.Text = ""
                    End If
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
        End Sub
        Public Sub checkqstring()
            If Session("state") = "quick" Or Session("state") = "queue" Then
                pnlnoleads.Visible = False
                pnladdingleads.Visible = False
                pnlmgmtq.Visible = True
                bindexportq()
                deletetempq()
                Session("state") = ""
            Else

                If (Request.QueryString("search") = "*" And Request.QueryString("leadtype") = "*" And Request.QueryString("status") = "*" And _
                    Request.QueryString("constatus") = "*" And Request.QueryString("assignedto") = "*" And Request.QueryString("assignedby") = "*" And _
                    Request.QueryString("adcode") = "*" And Request.QueryString("entrysource") = "*") Then
                    pnlnoleads.Visible = True
                    pnladdingleads.Visible = False

                ElseIf Request.QueryString("type") = "single" Then
                    pnlnoleads.Visible = False
                    pnladdtoq.Visible = True
                    addsinglelead()
                Else
                    pnladdingleads.Visible = True
                    addleadstoq("5")
                    If session("selectedleadcount") = 0 Then

                        pnlnoleads.Visible = True
                        pnladdingleads.Visible = False
                    Else
                        totleadcount.Text = session("selectedleadcount")

                    End If
                End If
                search.Text = Request.QueryString("search")
                ldtype.Text = Request.QueryString("leadtype")
                ldstat.Text = Request.QueryString("status")
                assignedstat.Text = Request.QueryString("constatus")
                assignedto.Text = Request.QueryString("assignedto")
                assigendby.Text = Request.QueryString("assignedby")
                adcode.Text = Request.QueryString("adcode")
                entrysource.Text = Request.QueryString("entrysource")
            End If

        End Sub
        Public Sub addsinglelead()
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "insert into tbl_exporttemp (tmpq_userid,tmpq_leadpk) values ('" & Session("userid") & "' , " & Request.QueryString("leadno") & ")"

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

        Sub bindexportq()
            'Dim strpropID As String = Session("clead")
            'Dim strUID As String = Session("userid")
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            mycommand = "Select eq_tbl_pk,eq_name,eq_createdate, eq_lastexportdate ,count(eqd_tbl_pk ) as 'cnt' " _
                    & "from tbl_leadexportqueue " _
                    & "left join dbo.tbl_leadexportqueuedetail  on eqd_eq_fk = eq_tbl_pk " _
                    & "where eq_uid='" & Session("userid") & "' " _
                    & "group by eq_tbl_pk,eq_name,eq_createdate, eq_lastexportdate"

            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_leadexportqueue")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_leadexportqueue"))
                exportq.DataSource = dvProducts
                exportq.DataBind()
                session("totqueuecount") = dvProducts.Count
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
            'If session("totqueuecount") = 0 Then
            'exp.Attributes("style") = "vertical-align top; height: 30px; overflow:auto;"
            'ElseIf session("totqueuecount") <= 2 Then
            'exp.Attributes("style") = "vertical-align top; height: 100px; overflow:auto;"
            'ElseIf session("totqueuecount") > 2 And totqueuecount <= 4 Then
            'exp.Attributes("style") = "vertical-align top; height: 160px; overflow:auto;"
            'ElseIf totqueuecount > 5 And totqueuecount <= 8 Then
            'exp.Attributes("style") = "vertical-align top; height: 220px; overflow:auto;"
            'ElseIf totqueuecount > 8 Then 'And totqueuecount <= 10 Then
            'exp.Attributes("style") = "vertical-align top; height: 270px; overflow:auto;"
            'End If

        End Sub
        Sub bindqleads(ByVal id As String)
            'Dim strpropID As String = Session("clead")
            Dim strUID As String = Session("userid")
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            mycommand = "Select *, ld_fname + ' ' + ld_lname as 'FullName' from tbl_leadexportqueuedetail join tbl_leads on tbl_leads_pk = eqd_leadno where eqd_eq_fk='" & id & "'"
            'mycommand = "Select * from tbl_leadexportqueuedetail where eqd_eq_fk='" & id & "'"

            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_leadexportqueuedetail")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_leadexportqueuedetail"))
                qleads.DataSource = dvProducts
                qleads.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
        End Sub
        Sub bindpreviewleads()
            'Dim strpropID As String = Session("clead")
            Dim strUID As String = Session("userid")
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            mycommand = "Select *, ld_fname + ' ' + ld_lname as 'FullName' from tbl_exporttemp join tbl_leads on tbl_leads_pk=tmpq_leadpk where tmpq_userid='" & Session("userid") & "'"
            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_exporttemp")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_exporttemp"))
                dgleadpreview.DataSource = dvProducts
                dgleadpreview.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
        End Sub
        Sub getleads(ByVal fromsrc As String)
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            If fromsrc = "Quick" Then
                mycommand = "Select ld_fname + ' ' + ld_lname as 'FullName', ld_address,ld_city,ld_state,ld_zip,ld_hphone,ld_cphone,ld_email,ld_email2 from tbl_leads where tbl_leads_pk in (select tmpq_leadpk from tbl_exporttemp where tmpq_userid ='" & Session("userid") & "')"
            Else
                mycommand = "Select ld_fname + ' ' + ld_lname as 'FullName', ld_address,ld_city,ld_state,ld_zip,ld_hphone,ld_cphone,ld_email,ld_email2 from tbl_leads where tbl_leads_pk in (select eqd_leadno from tbl_leadexportqueuedetail where eqd_eq_fk ='" & ddexportque.SelectedItem.Value & "')"
            End If
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

            Response.Clear()
            Response.AddHeader("content-disposition", "attachment;filename=test.csv")
            Response.Charset = ""
            Response.Cache.SetCacheability(HttpCacheability.NoCache)
            'Response.ContentType = "application/vnd.text"
            Response.ContentType = "text/csv"
            str.Append("Name,Address,City,State,Zip,Phone 1,Phone 2,Email 1,Email 2")
            str.Append(vbCrLf)
            For i = 0 To ds.Tables(0).Rows.Count - 1

                Dim j As Integer
                For j = 0 To ds.Tables(0).Columns.Count - 1
                    str.Append(ds.Tables(0).Rows(i)(j).ToString())
                    str.Append(",")
                Next j

                str.Append(vbCrLf)

            Next



            Dim stringWrite As New System.IO.StringWriter()
            Dim htmlWrite = New HtmlTextWriter(stringWrite)

            Response.Write(str.ToString())


        End Sub
        Public Sub addleadstoq(ByVal id As String)


            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            If (Session("role") = "Administrator" Or Session("role") = "GOD") Then
                If fdate.Text.Length > 0 And fdate.Text <> "" Then
                    'Response.Write("here")
                    mycommand = "Select  distinct 	ld_fname,ld_lname,ld_email,ld_hphone,ld_cphone,cast (ld_notes as varchar(max)) as 'ld_notes',convert(varchar(20),ld_adddate,101) as 'adate',cast(tbl_leads_pk as varchar(20)) as 'leadnos',cast (av_leadads_FK as varchar(20)) as 'ADNO', cast(tbl_leads_pk as varchar(20)) as 'leadpk', tbl_leads_pk, '' as 'nodays', convert(varchar(20),ld_adddate,101) as ld_adddatef, fname + ' ' + lname as assignedby,case when (ld_lname is null) then ld_fname else ld_fname + ' ' + ld_lname end as 'Contact',case when (select distinct cnt_followup from dbo.tbl_leadscontacthistory where tbl_leads_fk = tbl_leads_pk and cnt_followup='Yes') = 'Yes' then 'Yes' else 'No' end as 'Followup' from dbo.tbl_leads left join dbo.tbl_users on Uid=ld_assignedbyuid left join tbl_LeadADVenues on av_key=ld_adcode left join tbl_LeadADs  on tbl_leadad_pk =av_leadads_FK where company_pk='" & Session("company_pk") & "' and ld_adddate >='" & fdate.Text & "' and ld_adddate <='" & tdate.Text & "' order by tbl_leads_pk desc"

                Else
                    'Response.Write("here2")
                    'mycommand = "Select *,convert(varchar(20),ld_adddate,101) as 'adate',cast(tbl_leads_pk as varchar(20)) as 'leadnos',cast (av_leadads_FK as varchar(20)) as 'ADNO', cast(tbl_leads_pk as varchar(20)) as 'leadpk', tbl_leads_pk, '' as 'nodays', convert(varchar(20),ld_adddate,101) as ld_adddatef, fname + ' ' + lname as assignedby,case when (ld_lname is null) then ld_fname else ld_fname + ' ' + ld_lname end as 'Contact',case when (select distinct cnt_followup from dbo.tbl_leadscontacthistory where tbl_leads_fk = tbl_leads_pk and cnt_followup='Yes') = 'Yes' then 'Yes' else 'No' end as 'Followup' from dbo.tbl_leads left join dbo.tbl_users on Uid=ld_assignedbyuid left join tbl_LeadADVenues on av_key=ld_adcode left join tbl_LeadADs  on tbl_leadad_pk =av_leadads_FK where company_pk='" & Session("company_pk") & "' and ld_adddate >='09/01/2008' and ld_adddate <='09/07/2008' order by tbl_leads_pk desc"
                    mycommand = "Select distinct ld_fname,ld_lname,ld_email,ld_hphone,ld_cphone,cast (ld_notes as varchar(max)) as 'ld_notes',convert(varchar(20),ld_adddate,101) as 'adate',cast(tbl_leads_pk as varchar(20)) as 'leadnos',cast (av_leadads_FK as varchar(20)) as 'ADNO', cast(tbl_leads_pk as varchar(20)) as 'leadpk', tbl_leads_pk, '' as 'nodays', convert(varchar(20),ld_adddate,101) as ld_adddatef, fname + ' ' + lname as assignedby,case when (ld_lname is null) then ld_fname else ld_fname + ' ' + ld_lname end as 'Contact',case when (select distinct cnt_followup from dbo.tbl_leadscontacthistory where tbl_leads_fk = tbl_leads_pk and cnt_followup='Yes') = 'Yes' then 'Yes' else 'No' end as 'Followup' from dbo.tbl_leads left join dbo.tbl_users on Uid=ld_assignedbyuid left join tbl_LeadADVenues on av_key=ld_adcode left join tbl_LeadADs  on tbl_leadad_pk =av_leadads_FK where company_pk='" & Session("company_pk") & "' order by tbl_leads_pk desc"
                End If


            Else
                'Response.Write("here3")
                mycommand = "Select distinct ld_fname,ld_lname,ld_email,ld_hphone,ld_cphone,cast (ld_notes as varchar(max)) as 'ld_notes',cast(tbl_leads_pk as varchar(20)) as 'leadnos',cast (av_leadads_FK as varchar(20)) as 'ADNO', cast(tbl_leads_pk as varchar(20)) as 'leadpk', tbl_leads_pk, '' as 'nodays', convert(varchar(20),ld_adddate,101) as ld_adddatef, fname + ' ' + lname as assignedby,case when (ld_lname is null) then ld_fname else ld_fname + ' ' + ld_lname end as 'Contact',case when (select distinct cnt_followup from dbo.tbl_leadscontacthistory where tbl_leads_fk = tbl_leads_pk and cnt_followup='Yes') = 'Yes' then 'Yes' else 'No' end as 'Followup' from dbo.tbl_leads left join dbo.tbl_users on Uid=ld_assignedbyuid left join tbl_LeadADVenues on av_key=ld_adcode left join tbl_LeadADs  on tbl_leadad_pk =av_leadads_FK where company_pk='" & Session("company_pk") & "' and (ld_assignedbyuid='" & Session("userid") & "' or ld_assignedtouid='" & Session("userid") & "') order by tbl_leads_pk desc"

            End If

            Dim str As New StringBuilder()

            Try
                Dim ad As New SqlDataAdapter(mycommand, myConnection)
                Dim ds As New DataSet()
                ad.Fill(ds)

                Dim dvProducts As New DataView(ds.Tables(0))
                dvProducts.RowFilter = "(leadnos like '%" & Request.QueryString("search") & "%' or ld_fname + ' ' + ld_lname like '%" & Request.QueryString("search") & "%' or ld_lname LIKE '%" & Request.QueryString("search") & "%' or ld_fname LIKE '%" & Request.QueryString("search") & "%' or ld_email LIKE '%" & Request.QueryString("search") & "%' or ld_hphone LIKE '%" & Request.QueryString("search") & "%' or ld_cphone LIKE '%" & Request.QueryString("search") & "%' or ld_notes LIKE '%" & Request.QueryString("search") & "%') "
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
                session("selectedleadcount") = 	session("filterrowcount")
                'Response.Write(	session("filterrowcount"))
                'was x

                x.DataSource = dvProducts
                x.DataBind()

            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try


        End Sub
        Sub ItemDataBoundEventHandler(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
            If e.Item.ItemType = ListItemType.Item Or _
               e.Item.ItemType = ListItemType.AlternatingItem Then
                'Check to see if the price is below a certain threshold
                Dim itemCellwho As TableCell = e.Item.Cells(0)
                Dim itemCellwhotext As String = itemCellwho.Text
                'Response.Write(itemCellwhotext)
                writeintotempq(itemCellwhotext, Session("userid"))
            End If
        End Sub
        Public Sub writeintotempq(ByVal ldno As Integer, ByVal id As String)
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "insert into tbl_exporttemp (tmpq_userid,tmpq_leadpk) values ('" & id & "' , " & ldno & ")"

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
        Public Sub deleteexpque(ByVal id As String)
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "delete from tbl_leadexportqueue where eq_tbl_pk='" & id & "'"

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

            strSql = "delete from tbl_leadexportqueuedetail where eqd_eq_fk='" & id & "'"

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
        Sub removeleadPreview(ByVal id As String)
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "delete from tbl_exporttemp where tmpq_leadpk='" & id & "'"
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
        Sub removeleadq(ByVal id As String)
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "delete from tbl_leadexportqueuedetail where eqd_leadno='" & id & "'"
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
        Public Sub updatepreviewcount()
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select count (*) as 'cnt' from tbl_exporttemp where tmpq_userid='" & Session("userid") & "'"

            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    totleadcount.Text = Sqldr("cnt")
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
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

        Sub myDataGrid_PageChanger(ByVal Source As Object, _
                   ByVal E As DataGridPageChangedEventArgs)
            ' Set the CurrentPageIndex before binding the grid 
            qleads.CurrentPageIndex = E.NewPageIndex
            bindqleads(Session("cqueue"))

        End Sub
        Sub myDataGridA_PageChanger(ByVal Source As Object, _
                           ByVal E As DataGridPageChangedEventArgs)
            ' Set the CurrentPageIndex before binding the grid 
            exportq.CurrentPageIndex = E.NewPageIndex
            bindexportq()

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
            prmleadno.Value = Request.QueryString("leadno")
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
       
        Public Sub click_cancelemail(ByVal sender As Object, ByVal e As EventArgs)
            pnlmgmtq.Visible = True
            pnlemail.Visible = False
            pnlemailconfirm.Visible = False
        End Sub
        Sub prefillemail(ByVal Source As System.Object, ByVal e As System.EventArgs)
            If ddemailcor.SelectedItem.Text = "Clear" Then
                emailsubject.Text = ""
                emailbody.content = ""
            ElseIf ddemailcor.SelectedItem.Text = "Add" Then
                Response.Redirect("emailmainteditadd.aspx?action=new&source=sleademail")
            Else
                getemailcor()
            End If

        End Sub
        Sub Fillemailcor()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select * from  tbl_emails where email_uid='" & Session("userid") & "'"
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                ddemailcor.DataSource = dataReader
                ddemailcor.DataTextField = "email_name"
                ddemailcor.DataValueField = "email_tbl_pk"
                ddemailcor.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
            ddemailcor.Items.Insert(0, New ListItem("Select..", "9999"))
            'ddemailcor.Items.Insert(0, New ListItem("Add New", "99992"))
            ddemailcor.Items.Insert(1, New ListItem("Clear", "99992"))

        End Sub
        Public Sub getemailcor()

            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select * from tbl_emails where email_tbl_pk='" & ddemailcor.SelectedItem.Value & "'"

            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    If Sqldr("email_subject") IsNot DBNull.Value Then
                        emailsubject.Text = Sqldr("email_subject")
                    End If
                    If Sqldr("email_text") IsNot DBNull.Value Then
                        emailbody.content = Sqldr("email_text")
                    End If

                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try

        End Sub
        Public Sub getuseremail(ByVal sender As Object, ByVal e As EventArgs)
            Dim strSql As String = "SELECT email from tbl_users where UID='" & Session("userid") & "'"
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    If Sqldr("email") IsNot DBNull.Value Then
                        emailfrom.Text = Sqldr("email")
                    Else
                        emailfrom.Text = "None Found"
                    End If
                End If

            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try

        End Sub


    End Class
end namespace