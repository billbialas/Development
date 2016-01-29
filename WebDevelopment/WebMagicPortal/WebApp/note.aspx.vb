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
    Public Class note
        Inherits PageTemplate

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles mybase.load

            If Not (Page.IsPostBack) Then

            End If
            pagesetup()

        End Sub
         Public Sub pagesetup()

         'width will be calculated automatically, but it is sometimes
           	Body.Height = "400"
			body.width="600"
			Body.VAlign = "top"
			RightNav.VAlign = "top"
			RightNav.width="200"
			Layout.border = 0	
			layout.width="780"
			Header.Controls.Add(LoadControl("headersys.ascx"))
				'Header.Controls.Add(new LiteralControl("Some text."))
			'LeftNav.Controls.Add(LoadControl("navigation2.ascx.ascx"));
			'LeftNav.Controls.Add(new LiteralControl("Some text."))

			'adjust size of LeftNav (just for the heck of it)
			'LeftNav.Width = "100"

			'RightNav contents are included here, but try commenting
			'out the code below, to see how the page template dynamically
			'modifies itself (same goes with the LeftNav)
			RightNav.Controls.Add(LoadControl("navigation2.ascx"))
			MiddleNav.Controls.Add(LoadControl("headersys.ascx"))
			'MiddleNav.Controls.Add(LoadControl("navtest.ascx"))
			Footer.controls.add(LoadControl("footer.ascx"))
            

        End Sub

    End Class
end namespace