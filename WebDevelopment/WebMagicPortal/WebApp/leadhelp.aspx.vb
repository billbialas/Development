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
    Public Class leadhelp
        Inherits page
        
         Public frmhelp As HtmlGenericControl
			public divhelp
			public l_closehelp as button
			
        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles mybase.load

            If Not (Page.IsPostBack) Then

            End If
            

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
        	
			Sub btn_closehelp(ByVal sender As Object, ByVal e As EventArgs)
 		 		divhelp.visible=true
 		 		frmhelp.visible=false
 		 		l_closehelp.visible=false
         End Sub
         Sub btn_closehelpA(ByVal sender As Object, ByVal e As EventArgs)
 		 		    Dim msg As String = ""
                msg = msg & "<Script Language='JavaScript'>"
                msg = msg & "window.opener.location.href=window.opener.location.href;"
                msg = msg & "window.close();"
                msg = msg & "</Script>"
                Response.Write(msg)
         End Sub
         
         Sub ld3help(ByVal sender As Object, ByVal e As EventArgs)
				frmhelp.Attributes("src") = System.Configuration.ConfigurationManager.AppSettings("CurrentappURL") & "/help/help_ld3.html"
				divhelp.visible=false
 		 		frmhelp.visible=true
 		 		l_closehelp.visible=true
		   End Sub
 			Sub ldNotehelp(ByVal sender As Object, ByVal e As EventArgs)
 		 		frmhelp.Attributes("src") = System.Configuration.ConfigurationManager.AppSettings("CurrentappURL") & "/help/help_ldnote.html"
				divhelp.visible=false
 		 		frmhelp.visible=true
 		 		l_closehelp.visible=true
	      End Sub
	      
	      Sub ldmanualhelp(ByVal sender As Object, ByVal e As EventArgs)
				frmhelp.Attributes("src") = System.Configuration.ConfigurationManager.AppSettings("CurrentappURL") & "/help/help_ldmanualhelp.html"
				divhelp.visible=false
 		 		frmhelp.visible=true
 		 		l_closehelp.visible=true
		   End Sub
 			Sub ldedithelp(ByVal sender As Object, ByVal e As EventArgs)
 		 		frmhelp.Attributes("src") = System.Configuration.ConfigurationManager.AppSettings("CurrentappURL") & "/help/help_ldedithelp.html"
				divhelp.visible=false
 		 		frmhelp.visible=true
 		 		l_closehelp.visible=true
	      End Sub
	      Sub lddeletehelp(ByVal sender As Object, ByVal e As EventArgs)
				frmhelp.Attributes("src") = System.Configuration.ConfigurationManager.AppSettings("CurrentappURL") & "/help/help_lddeletehelp.html"
				divhelp.visible=false
 		 		frmhelp.visible=true
 		 		l_closehelp.visible=true
		   End Sub
 			Sub ldtaskahelpa(ByVal sender As Object, ByVal e As EventArgs)
 		 		frmhelp.Attributes("src") = System.Configuration.ConfigurationManager.AppSettings("CurrentappURL") & "/help/help_ldtaskahelpa.html"
				divhelp.visible=false
 		 		frmhelp.visible=true
 		 		l_closehelp.visible=true
	      End Sub
	      Sub ldtaskthelpt(ByVal sender As Object, ByVal e As EventArgs)
				frmhelp.Attributes("src") = System.Configuration.ConfigurationManager.AppSettings("CurrentappURL") & "/help/help_ldtaskthelpt.html"
				divhelp.visible=false
 		 		frmhelp.visible=true
 		 		l_closehelp.visible=true
		   End Sub
 			Sub ldemailhelp(ByVal sender As Object, ByVal e As EventArgs)
 		 		frmhelp.Attributes("src") = System.Configuration.ConfigurationManager.AppSettings("CurrentappURL") & "/help/help_ldemailhelp.html"
				divhelp.visible=false
 		 		frmhelp.visible=true
 		 		l_closehelp.visible=true
	      End Sub
	       Sub lduploadhelp(ByVal sender As Object, ByVal e As EventArgs)
				frmhelp.Attributes("src") = System.Configuration.ConfigurationManager.AppSettings("CurrentappURL") & "/help/help_lduploadhelp.html"
				divhelp.visible=false
 		 		frmhelp.visible=true
 		 		l_closehelp.visible=true
		   End Sub
 			Sub ldexporthelp(ByVal sender As Object, ByVal e As EventArgs)
 		 		frmhelp.Attributes("src") = System.Configuration.ConfigurationManager.AppSettings("CurrentappURL") & "/help/help_ldexporthelp.html"
				divhelp.visible=false
 		 		frmhelp.visible=true
 		 		l_closehelp.visible=true
	      End Sub
	      
	      

    End Class
end namespace