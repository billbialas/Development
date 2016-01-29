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
    Public Class ldexport
        Inherits Page

        Public efilename As TextBox

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles mybase.load
            If Not (Page.IsPostBack) Then

            End If
        End Sub
        Sub cancelexp(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Dim msg As String = ""
            msg = msg & "<Script Language='JavaScript'>"
            msg = msg & "window.opener.location.href=window.opener.location.href;"
            msg = msg & "window.close();"
            msg = msg & "</Script>"
            Response.Write(msg)

        End Sub
        Sub exportgoA(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Response.Write(Request.QueryString("exptype"))
        End Sub
        Sub exportgo(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            If Request.QueryString("exptype") = "quick" Then
                mycommand = "Select ld_fname + ' ' + ld_lname as 'FullName', ld_address,ld_city,ld_state,ld_zip,ld_hphone,ld_cphone,ld_email,ld_email2 from tbl_leads where tbl_leads_pk in (select tmpq_leadpk from tbl_exporttemp where tmpq_userid ='" & Session("userid") & "')"
            Else
                mycommand = "Select ld_fname + ' ' + ld_lname as 'FullName', ld_address,ld_city,ld_state,ld_zip,ld_hphone,ld_cphone,ld_email,ld_email2 from tbl_leads where tbl_leads_pk in (select eqd_leadno from tbl_leadexportqueuedetail where eqd_eq_fk ='" & Request.QueryString("queue") & "')"
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
            Response.AddHeader("content-disposition", "attachment;filename=" & efilename.Text & ".csv")
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
            Response.End()
            Dim msg As String = ""
            msg = msg & "<Script Language='JavaScript'>"
            msg = msg & "window.opener.location.href=window.opener.location.href;"
            msg = msg & "window.close();"
            msg = msg & "</Script>"
            Response.Write(msg)

        End Sub

    End Class
end namespace