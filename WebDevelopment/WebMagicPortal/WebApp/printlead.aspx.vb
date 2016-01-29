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


namespace PageTemplate
    Public Class printlead
        Inherits Page

        Public logoarea, logoarea2
        Public coname, coname2 As Label
        
        Public lb_leadno, Label3, Label5, Label6, Label7, Label9, Label8, Label4, Label15, Label10, Label11, Label12 As Label


        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles mybase.load

            If Not (Page.IsPostBack) Then
            	clearsessions()
                GetCompanyinfo()
                logoarea.Attributes("src") = System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "/logos/company/" & session("selectedlogo")
                bindfields()
                Dim msg As String
                msg = ""
                msg = msg & "<Script Language='JavaScript'>"
                msg = msg & "window.print();"
                msg = msg & "</Script>"
                Response.Write(msg)
            End If

        End Sub
        sub clearsessions()
        	session("selectedlogo")=""
        
       
        end sub
        Sub GetCompanyinfo()
            Dim strConnection As String

            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select co_name,co_logo from dbo.tbl_company join tbl_users on company_pk=co_tbl_pk where uid='" & Session("userid") & "'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    session("selectedlogo") = Sqldr("co_logo")
                    coname.text = sqldr("co_name")
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
        End Sub

        Sub bindfields()
            Dim strUID As String = session("userid")
            Dim strSql As String = "SELECT ld_fname + ' ' + ld_lname as 'name', " _
            & "case when (select count(*) from tbl_leadscontacthistory where tbl_leads_fk = tbl_leads_pk and cnt_who <> 'System') > 0 then " _
            & "'Yes' else 'No' end as 'HasHistory',* " _
            & "from tbl_leads join dbo.tbl_users on Uid=ld_assignedbyuid where tbl_leads_pk =" & Session("clead")
            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.read() Then
                    lb_leadno.Text = Sqldr("tbl_leads_pk")
                    If Sqldr("ld_adcode") IsNot DBNull.Value Then
                        Label3.Text = Sqldr("ld_adcode")
                    End If
                    If Sqldr("ld_program") IsNot DBNull.Value Then
                        Label5.Text = Sqldr("ld_program")
                    End If
                    If Sqldr("ld_type") IsNot DBNull.Value Then
                        Label6.Text = Sqldr("ld_type")
                    End If
                    If Sqldr("ld_status") IsNot DBNull.Value Then
                        Label7.Text = Sqldr("ld_status")
                    End If
                    If Sqldr("name") IsNot DBNull.Value Then
                        Label8.Text = Sqldr("name")
                    End If
                    If Sqldr("ld_hphone") IsNot DBNull.Value Then
                        Label4.Text = Sqldr("ld_hphone")
                    End If
                    If Sqldr("ld_address") IsNot DBNull.Value Then
                        Label9.Text = Sqldr("ld_address")
                    End If
                    If Sqldr("ld_city") IsNot DBNull.Value Then
                        Label10.Text = Sqldr("ld_city")
                    End If
                    If Sqldr("ld_state") IsNot DBNull.Value Then
                        Label11.Text = Sqldr("ld_state")
                    End If
                    If Sqldr("ld_zip") IsNot DBNull.Value Then
                        Label12.Text = Sqldr("ld_zip")
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