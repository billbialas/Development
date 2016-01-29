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
Imports FreeTextBoxControls

namespace PageTemplate
    Public Class WFfullscreen
        Inherits page
        
        Public pg1text, pg2text, emailbody As FreeTextBox

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles mybase.load

            If Not (Page.IsPostBack) Then
 					emailbody.text=session("fscdata")
            End If
           

        End Sub
          Public Sub btnsave(ByVal Source As System.Object, ByVal e As System.EventArgs)
           
          session("fscdata")=emailbody.text
           Dim msg As String = ""
            msg = msg & "<Script Language='JavaScript'>"
            msg = msg & "window.opener.location.href=window.opener.location.href;"
            msg = msg & "window.close();"
            msg = msg & "</Script>"
            Response.Write(msg)
        End Sub
        

    End Class
end namespace