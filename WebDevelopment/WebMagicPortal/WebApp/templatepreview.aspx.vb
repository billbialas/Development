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
Imports System.Text
Imports System.IO

namespace PageTemplate
    Public Class templatepreview
        Inherits Page

        Public efilename, tname, tsubject As TextBox
        Public tptextlbl As Label
        Public tptext

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles mybase.load
            If Not (Page.IsPostBack) Then
                PInit()
                bindfields()

            End If
        End Sub
        Sub PInit()
            tptextlbl.Visible = True
            tptext.show = False
            tname.Enabled = False
            tsubject.Enabled = False
        End Sub
        Sub bindfields()
            Dim strUID As String = Session("userid")
            Dim strSql As String = "select *,email_text as 'bdtext' from tbl_emails where email_tbl_pk='" & Request.QueryString("id") & "'"

            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    tname.Text = Sqldr("email_name")
                    tsubject.Text = Sqldr("email_subject")
                    tptext.content = Sqldr("email_text")
                    tptextlbl.Text = Sqldr("email_text")
                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        End Sub
        Sub cancelexp(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Session("pupclosed") = "true"
            Dim msg As String = ""
            msg = msg & "<Script Language='JavaScript'>"
            msg = msg & "window.opener.location.href=window.opener.location.href;"
            msg = msg & "window.close();"
            msg = msg & "</Script>"
            Response.Write(msg)

        End Sub
        Sub edittemplate(ByVal Source As System.Object, ByVal e As System.EventArgs)
            tptextlbl.Visible = False
            tptext.show = True
            tname.Enabled = True
            tsubject.Enabled = True
            bindfields()

        End Sub


        
    End Class
end namespace