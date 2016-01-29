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
    Public Class help
        Inherits PageTemplate

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles mybase.load

            If Not (Page.IsPostBack) Then

            End If
            pagesetup()

        End Sub
        Sub cohelp(ByVal sender As Object, ByVal e As EventArgs)
					Response.Write("<script>window.open" & _
                "('" & System.Configuration.ConfigurationManager.AppSettings("CurrentappURL") & "/help/help_um.html','_new','width=1000,height=700,resizable=1,scrollbars=1');</script>")
        End Sub
		  Sub tphelp(ByVal sender As Object, ByVal e As EventArgs)
					Response.Write("<script>window.open" & _
                "('" & System.Configuration.ConfigurationManager.AppSettings("CurrentappURL") & "/help/help_tp.html','_new','width=1000,height=700,resizable=1,scrollbars=1');</script>")
        End Sub
			Sub dphelp(ByVal sender As Object, ByVal e As EventArgs)
					Response.Write("<script>window.open" & _
                "('" & System.Configuration.ConfigurationManager.AppSettings("CurrentappURL") & "/help/help_dp.html','_new','width=1000,height=700,resizable=1,scrollbars=1');</script>")
        End Sub
        	Sub imhelp(ByVal sender As Object, ByVal e As EventArgs)
					Response.Write("<script>window.open" & _
                "('" & System.Configuration.ConfigurationManager.AppSettings("CurrentappURL") & "/help/help_im.html','_new','width=1000,height=700,resizable=1,scrollbars=1');</script>")
        End Sub
        	Sub ld3help(ByVal sender As Object, ByVal e As EventArgs)
					Response.Write("<script>window.open" & _
                "('" & System.Configuration.ConfigurationManager.AppSettings("CurrentappURL") & "/help/help_ld3.html','_new','width=1000,height=700,resizable=1,scrollbars=1');</script>")
        End Sub
 		Sub ldNotehelp(ByVal sender As Object, ByVal e As EventArgs)
					Response.Write("<script>window.open" & _
                "('" & System.Configuration.ConfigurationManager.AppSettings("CurrentappURL") & "/help/help_ldnote.html','_new','width=1000,height=700,resizable=1,scrollbars=1');</script>")
        End Sub


 		Sub ldmanualhelp(ByVal sender As Object, ByVal e As EventArgs)
			Response.Write("<script>window.open" & _
         "('" & System.Configuration.ConfigurationManager.AppSettings("CurrentappURL") & "/help/help_ldmanualhelp.html','_new','width=1000,height=700,resizable=1,scrollbars=1');</script>")
   
		   End Sub
 			Sub ldedithelp(ByVal sender As Object, ByVal e As EventArgs)
 		 						Response.Write("<script>window.open" & _
                "('" & System.Configuration.ConfigurationManager.AppSettings("CurrentappURL") & "/help/help_ldedithelp.html','_new','width=1000,height=700,resizable=1,scrollbars=1');</script>")
   
	      End Sub
	      Sub lddeletehelp(ByVal sender As Object, ByVal e As EventArgs)
								Response.Write("<script>window.open" & _
                "('" & System.Configuration.ConfigurationManager.AppSettings("CurrentappURL") & "/help/help_lddeletehelp.html','_new','width=1000,height=700,resizable=1,scrollbars=1');</script>")
   
		   End Sub
 			Sub ldtaskahelpa(ByVal sender As Object, ByVal e As EventArgs)
 		 						Response.Write("<script>window.open" & _
                "('" & System.Configuration.ConfigurationManager.AppSettings("CurrentappURL") & "/help/help_ldtaskahelpa.html','_new','width=1000,height=700,resizable=1,scrollbars=1');</script>")
   
	      End Sub
	      Sub ldtaskthelpt(ByVal sender As Object, ByVal e As EventArgs)
								Response.Write("<script>window.open" & _
                "('" & System.Configuration.ConfigurationManager.AppSettings("CurrentappURL") & "/help/help_ldtaskthelpt.html','_new','width=1000,height=700,resizable=1,scrollbars=1');</script>")
   
		   End Sub
 			Sub ldemailhelp(ByVal sender As Object, ByVal e As EventArgs)
 		 					Response.Write("<script>window.open" & _
                "('" & System.Configuration.ConfigurationManager.AppSettings("CurrentappURL") & "/help/help_ldemailhelp.html','_new','width=1000,height=700,resizable=1,scrollbars=1');</script>")
   
	      End Sub
	       Sub lduploadhelp(ByVal sender As Object, ByVal e As EventArgs)
								Response.Write("<script>window.open" & _
                "('" & System.Configuration.ConfigurationManager.AppSettings("CurrentappURL") & "/help/help_lduploadhelp.html','_new','width=1000,height=700,resizable=1,scrollbars=1');</script>")
   
		   End Sub
 			Sub ldexporthelp(ByVal sender As Object, ByVal e As EventArgs)
 		 						Response.Write("<script>window.open" & _
                "('" & System.Configuration.ConfigurationManager.AppSettings("CurrentappURL") & "/help/help_ldexporthelp.html','_new','width=1000,height=700,resizable=1,scrollbars=1');</script>")
   
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

    End Class
end namespace