Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web
Imports System.Web.Security
Imports System.Configuration

namespace database1
    Public Class loginA
        Inherits Page

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            If Not (Page.IsPostBack) Then
                ' Response.Write(Request.Cookies("Gimp-UI")("UID"))
            End If
        End Sub

    End Class
end namespace