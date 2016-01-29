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
    Public Class leaddropdownsSYS
        Inherits PageTemplate
        Public lstleadtypes, leadstatus, leadsource, leadprograms, tasks As ListBox
        Public pnladdleadtype, confirmremove, pnlldstatwarn, pnladdleadstatus, pnlldsourcewarn, pnlleadsourceadd, pnlldprogramswarn, pnlleadprogramsadd As Panel
        Public pnltaskwarn, pnladdtasktype As Panel
        Public newleadtype, newleadstatus, newleadsource, newleadprograms, newtasktype As TextBox
        Public ldtyperemove, Removeleadstat, ldsource, ldprograms, deltasktypecan, savettype, rmvtask As Button

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

            If Not (Page.IsPostBack) Then
                bindleadtypes()
                bindleadstatus()
                bindleadsource()
                bindleadprograms()
                bindtasktypes()
            End If
            pagesetup()

        End Sub

        Sub bindleadtypes()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='leadtype'", myConnection)

            Try
                myConnection.Open()
                lstleadtypes.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
                lstleadtypes.DataBind()
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            End Try
            'lstleadtypes.SelectedIndex = 0

        End Sub
        Sub bindleadstatus()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='contactstatus'", myConnection)

            Try
                myConnection.Open()
                leadstatus.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
                leadstatus.DataBind()
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            End Try
            'leadstatus.SelectedIndex = 0

        End Sub
        Sub bindleadsource()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='leadsource'", myConnection)

            Try
                myConnection.Open()
                leadsource.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
                leadsource.DataBind()
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            End Try
            'leadsource.SelectedIndex = 0

        End Sub
        Sub bindleadprograms()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='leadprogram'", myConnection)

            Try
                myConnection.Open()
                leadprograms.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
                leadprograms.DataBind()
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            End Try
            'leadprograms.SelectedIndex = 0
        End Sub
        Sub bindtasktypes()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='tasktype'", myConnection)

            Try
                myConnection.Open()
                tasks.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
                tasks.DataBind()
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            End Try
            'leadprograms.SelectedIndex = 0
        End Sub
        Sub Addleadtype(ByVal sender As Object, ByVal e As EventArgs)
            newleadtype.Text = ""
            pnladdleadtype.Visible = True
        End Sub
        Sub Addleadstatus(ByVal sender As Object, ByVal e As EventArgs)
            newleadstatus.Text = ""
            pnladdleadstatus.Visible = True
        End Sub
        Sub Addleadsource(ByVal sender As Object, ByVal e As EventArgs)
            newleadsource.Text = ""
            pnlleadsourceadd.Visible = True
        End Sub
        Sub Addleadprograms(ByVal sender As Object, ByVal e As EventArgs)
            newleadprograms.Text = ""
            pnlleadprogramsadd.Visible = True
        End Sub
        Sub Addtasktype(ByVal sender As Object, ByVal e As EventArgs)
            newtasktype.Text = ""
            pnladdtasktype.Visible = True
        End Sub
        Sub Saveleadtype(ByVal sender As Object, ByVal e As EventArgs)
            If checkifcodeexists(newleadtype.Text, "leadtype") = False Then

                Dim strConnection As String
                Dim sqlConn As SqlConnection
                Dim sqlCmd As SqlCommand

                Dim strSql As String
                strSql = "insert into dbo.tbl_xwalk  (x_type,x_descr,x_UID,x_company) values('leadtype','" & newleadtype.Text & "','" & Session("userid") & "','" & Session("company") & "')"

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
                pnladdleadtype.Visible = False
                bindleadtypes()

            Else
                newleadtype.Text = "EXISTS"
            End If
        End Sub
        Sub saveleadstatus(ByVal sender As Object, ByVal e As EventArgs)
            If checkifcodeexists(newleadstatus.Text, "contactstatus") = False Then

                Dim strConnection As String
                Dim sqlConn As SqlConnection
                Dim sqlCmd As SqlCommand

                Dim strSql As String
                strSql = "insert into dbo.tbl_xwalk  (x_type,x_descr,x_UID,x_company) values('contactstatus','" & newleadstatus.Text & "','" & Session("userid") & "','" & Session("company") & "')"

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
                pnladdleadstatus.Visible = False
                bindleadstatus()

            Else
                newleadstatus.Text = "EXISTS"
            End If
        End Sub
        Sub saveleadsource(ByVal sender As Object, ByVal e As EventArgs)
            If checkifcodeexists(newleadsource.Text, "leadsource") = False Then

                Dim strConnection As String
                Dim sqlConn As SqlConnection
                Dim sqlCmd As SqlCommand

                Dim strSql As String
                strSql = "insert into dbo.tbl_xwalk  (x_type,x_descr,x_UID,x_company) values('leadsource','" & newleadsource.Text & "','" & Session("userid") & "','" & Session("company") & "')"

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
                pnlleadsourceadd.Visible = False
                bindleadsource()

            Else
                newleadsource.Text = "EXISTS"
            End If
        End Sub
        Sub saveleadprograms(ByVal sender As Object, ByVal e As EventArgs)
            If checkifcodeexists(newleadprograms.Text, "leadprogram") = False Then

                Dim strConnection As String
                Dim sqlConn As SqlConnection
                Dim sqlCmd As SqlCommand

                Dim strSql As String
                strSql = "insert into dbo.tbl_xwalk  (x_type,x_descr,x_UID,x_company) values('leadprogram','" & newleadprograms.Text & "','" & Session("userid") & "','" & Session("company") & "')"

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
                pnlleadprogramsadd.Visible = False
                bindleadprograms()

            Else
                newleadprograms.Text = "EXISTS"
            End If
        End Sub
        Sub savetasktype(ByVal sender As Object, ByVal e As EventArgs)
            If checkifcodeexists(newleadprograms.Text, "tasktype") = False Then

                Dim strConnection As String
                Dim sqlConn As SqlConnection
                Dim sqlCmd As SqlCommand

                Dim strSql As String
                strSql = "insert into dbo.tbl_xwalk  (x_type,x_descr,x_UID,x_company) values('tasktype','" & newtasktype.Text & "','" & Session("userid") & "','" & Session("company") & "')"

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
                pnladdtasktype.Visible = False
                bindtasktypes()

            Else
                newtasktype.Text = "EXISTS"
            End If
        End Sub

        Public Function checkifcodeexists(ByVal code As String, ByVal type As String) As Boolean

            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select * from tbl_xwalk where x_type='" & type & "' and x_descr='" & code & "' and (x_company='All' or x_company='" & Session("company") & "' or x_uid='" & Session("userid") & "')"

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

        Sub leadtypecommit(ByVal sender As Object, ByVal e As EventArgs)
            If (lstleadtypes.SelectedItem.Text = "Unknown" Or lstleadtypes.SelectedItem.Text = "All" Or lstleadtypes.SelectedItem.Text Is Nothing Or lstleadtypes.SelectedItem.Text = "") Then

            Else
                If checkifleadattached(lstleadtypes.SelectedItem.Text, "leadtype") Then

                    confirmremove.Visible = True
                    ldtyperemove.Visible = False
                Else

                    confirmremove.Visible = False
                    ldtyperemove.Visible = True
                    leadtyperemoveNOBT()
                End If
            End If
        End Sub
        Sub leadstatcommit(ByVal sender As Object, ByVal e As EventArgs)

            If (leadstatus.SelectedItem.Text = "Unknown" Or leadstatus.SelectedItem.Text = "All" Or leadstatus.SelectedItem.Text Is Nothing) Then

            Else
                If checkifleadattached(leadstatus.SelectedItem.Text, "contactstatus") Then
                    pnlldstatwarn.Visible = True
                    Removeleadstat.Visible = False
                Else
                    pnlldstatwarn.Visible = False
                    Removeleadstat.Visible = True
                    leadstatremoveNOBT()
                End If
            End If
        End Sub
        Sub leadsourcecommit(ByVal sender As Object, ByVal e As EventArgs)

            If (leadsource.SelectedItem.Text = "Unknown" Or leadsource.SelectedItem.Text = "All") Then

            Else
                If leadsource.SelectedItem IsNot Nothing Then
                    If checkifleadattached(leadsource.SelectedItem.Text, "leadsource") Then
                        pnlldsourcewarn.Visible = True
                        ldsource.Visible = False
                    Else
                        pnlldsourcewarn.Visible = False
                        ldsource.Visible = True
                        leadsourceremoveNOBT()
                    End If
                End If
            End If

        End Sub
        Sub leadprogramscommit(ByVal sender As Object, ByVal e As EventArgs)

            If (leadprograms.SelectedItem.Text = "Unknown" Or leadprograms.SelectedItem.Text = "All" Or leadprograms.SelectedItem.Text Is Nothing) Then

            Else
                If checkifleadattached(leadprograms.SelectedItem.Text, "leadprogram") Then
                    pnlldprogramswarn.Visible = True
                    ldprograms.Visible = False
                Else
                    pnlldprogramswarn.Visible = False
                    ldprograms.Visible = True
                    leadprogramsremoveNOBT()
                End If
            End If
        End Sub
        Sub tasktypecommit(ByVal sender As Object, ByVal e As EventArgs)

            If (tasks.SelectedItem.Text = "Mail" Or tasks.SelectedItem.Text = "Email" Or tasks.SelectedItem.Text Is Nothing Or tasks.SelectedItem.Text = "Phone Call" Or tasks.SelectedItem.Text = "Other") Then

            Else

                pnltaskwarn.Visible = True
                rmvtask.Visible = False

            End If
        End Sub

        Sub leadtyperemove(ByVal sender As Object, ByVal e As EventArgs)
            If lstleadtypes.SelectedItem IsNot Nothing Then
                Dim i As Integer
                i = 0
                While lstleadtypes.SelectedItem IsNot Nothing
                    updateleads(lstleadtypes.SelectedItem.Text, "leadtype")
                    removetype(lstleadtypes.SelectedItem.Value)

                    lstleadtypes.Items.Remove(lstleadtypes.SelectedItem)
                End While
            End If
            confirmremove.Visible = False
            ldtyperemove.Visible = True

        End Sub
        Sub leadtyperemoveNOBT()
            If lstleadtypes.SelectedItem IsNot Nothing Then
                Dim i As Integer
                i = 0
                While lstleadtypes.SelectedItem IsNot Nothing

                    removetype(lstleadtypes.SelectedItem.Value)
                    lstleadtypes.Items.Remove(lstleadtypes.SelectedItem)
                End While
            End If
            confirmremove.Visible = False
            ldtyperemove.Visible = True
        End Sub

        Sub leadstatremove(ByVal sender As Object, ByVal e As EventArgs)
            If leadstatus.SelectedItem IsNot Nothing Then
                Dim i As Integer
                i = 0
                While leadstatus.SelectedItem IsNot Nothing
                    updateleads(leadstatus.SelectedItem.Text, "contactstatus")
                    removetype(leadstatus.SelectedItem.Value)
                    leadstatus.Items.Remove(leadstatus.SelectedItem)
                End While
            End If
            pnlldstatwarn.Visible = False
            Removeleadstat.Visible = True
        End Sub
        Sub leadstatremoveNOBT()
            If leadstatus.SelectedItem IsNot Nothing Then
                Dim i As Integer
                i = 0
                While leadstatus.SelectedItem IsNot Nothing

                    removetype(leadstatus.SelectedItem.Value)
                    leadstatus.Items.Remove(leadstatus.SelectedItem)
                End While
            End If
            pnlldstatwarn.Visible = False
            Removeleadstat.Visible = True
        End Sub
        Sub leadsourceremove(ByVal sender As Object, ByVal e As EventArgs)
            If leadsource.SelectedItem IsNot Nothing Then
                Dim i As Integer
                i = 0
                While leadsource.SelectedItem IsNot Nothing
                    updateleads(leadsource.SelectedItem.Text, "leadsource")
                    removetype(leadsource.SelectedItem.Value)
                    leadsource.Items.Remove(leadsource.SelectedItem)
                End While
            End If
            pnlldsourcewarn.Visible = False
            ldsource.Visible = True
        End Sub
        Sub leadsourceremoveNOBT()
            If leadsource.SelectedItem IsNot Nothing Then
                Dim i As Integer
                i = 0
                While leadsource.SelectedItem IsNot Nothing

                    removetype(leadsource.SelectedItem.Value)
                    leadsource.Items.Remove(leadsource.SelectedItem)
                End While
            End If
            pnlldsourcewarn.Visible = False
            ldsource.Visible = True
        End Sub

        Sub leadprogramsremove(ByVal sender As Object, ByVal e As EventArgs)
            If leadprograms.SelectedItem IsNot Nothing Then
                Dim i As Integer
                i = 0
                While leadprograms.SelectedItem IsNot Nothing
                    updateleads(leadprograms.SelectedItem.Text, "leadprogram")
                    removetype(leadprograms.SelectedItem.Value)
                    leadprograms.Items.Remove(leadprograms.SelectedItem)
                End While
            End If
            pnlldprogramswarn.Visible = False
            ldprograms.Visible = True
        End Sub
        Sub leadprogramsremoveNOBT()
            If leadprograms.SelectedItem IsNot Nothing Then
                Dim i As Integer
                i = 0
                While leadprograms.SelectedItem IsNot Nothing

                    removetype(leadprograms.SelectedItem.Value)
                    leadprograms.Items.Remove(leadprograms.SelectedItem)
                End While
            End If
            pnlldprogramswarn.Visible = False
            ldprograms.Visible = True
        End Sub


        Sub tasktyperemove(ByVal sender As Object, ByVal e As EventArgs)
            If tasks.SelectedItem IsNot Nothing Then
                Dim i As Integer
                i = 0
                While tasks.SelectedItem IsNot Nothing
                    'updateleads(leadprograms.SelectedItem.Text, "leadprogram")
                    removetype(tasks.SelectedItem.Value)
                    tasks.Items.Remove(tasks.SelectedItem)
                End While
            End If
            pnltaskwarn.Visible = False
            rmvtask.Visible = True
        End Sub
        Sub tasktyperemoveNOBT()
            If tasks.SelectedItem IsNot Nothing Then
                Dim i As Integer
                i = 0
                While tasks.SelectedItem IsNot Nothing

                    removetype(tasks.SelectedItem.Value)
                    tasks.Items.Remove(tasks.SelectedItem)
                End While
            End If
            pnltaskwarn.Visible = False
            rmvtask.Visible = True
        End Sub


        Sub leadtypecancel(ByVal sender As Object, ByVal e As EventArgs)
            confirmremove.Visible = False
            ldtyperemove.Visible = True
        End Sub

        Sub leadstatcancel(ByVal sender As Object, ByVal e As EventArgs)
            pnlldstatwarn.Visible = False
            Removeleadstat.Visible = True
        End Sub
        Sub leadsourcecancel(ByVal sender As Object, ByVal e As EventArgs)
            pnlldsourcewarn.Visible = False
            ldsource.Visible = True
        End Sub
        Sub leadprogramscancel(ByVal sender As Object, ByVal e As EventArgs)
            pnlldprogramswarn.Visible = False
            ldprograms.Visible = True
        End Sub
        Sub tasktypecancel(ByVal sender As Object, ByVal e As EventArgs)
            pnltaskwarn.Visible = False
            rmvtask.Visible = True
        End Sub

        Public Sub removetype(ByVal ddtype As String)
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "delete from tbl_xwalk where tbl_xwalk_pk='" & ddtype & "' and (x_company='" & Session("company") & "' or x_uid='" & Session("userid") & "')"

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

        Public Function checkifleadattached(ByVal ldvalue As String, ByVal type As String) As Boolean

            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String
            If type = "leadtype" Then
                strSql = "select * from tbl_leads where ld_type='" & ldvalue & "' and (ld_assignedbyuid='" & Session("userid") & "' or ld_assignedtouid='" & Session("userid") & "')"
            ElseIf type = "contactstatus" Then
                strSql = "select * from tbl_leads where ld_pstatus='" & ldvalue & "' and (ld_assignedbyuid='" & Session("userid") & "' or ld_assignedtouid='" & Session("userid") & "')"
            ElseIf type = "leadsource" Then
                strSql = "select * from tbl_leads where ld_adsource='" & ldvalue & "' and (ld_assignedbyuid='" & Session("userid") & "' or ld_assignedtouid='" & Session("userid") & "')"
            ElseIf type = "leadprogram" Then
                strSql = "select * from tbl_leads where ld_program='" & ldvalue & "' and (ld_assignedbyuid='" & Session("userid") & "' or ld_assignedtouid='" & Session("userid") & "')"

            End If

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

        Public Sub updateleads(ByVal ldvalue As String, ByVal type As String)
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String
            If type = "leadtype" Then


                strSql = "update tbl_leads set ld_type='Unknown' from tbl_leads where ld_type='" & ldvalue & "' and (ld_assignedbyuid='" & Session("userid") & "' or ld_assignedtouid='" & Session("userid") & "')"
            ElseIf type = "contactstatus" Then
                strSql = "update tbl_leads set ld_pstatus='Unknown' from tbl_leads where ld_pstatus='" & ldvalue & "' and (ld_assignedbyuid='" & Session("userid") & "' or ld_assignedtouid='" & Session("userid") & "')"
            ElseIf type = "leadsource" Then
                strSql = "update tbl_leads set ld_adsource='Unknown' from tbl_leads where ld_adsource='" & ldvalue & "' and (ld_assignedbyuid='" & Session("userid") & "' or ld_assignedtouid='" & Session("userid") & "')"
            ElseIf type = "leadprogram" Then
                strSql = "update tbl_leads set ld_program='Unknown' from tbl_leads where ld_adsource='" & ldvalue & "' and (ld_assignedbyuid='" & Session("userid") & "' or ld_assignedtouid='" & Session("userid") & "')"

            End If

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
end namespace