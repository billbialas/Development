Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web
Imports System.Web.Security
Imports System.Configuration
Imports System.Net.Mail
Imports FreeTextBoxControls

namespace PageTemplate
Public Class contact
   Inherits PageTemplate
	
	public mssub,msbody,msname,msemail as textbox
	public contactR,contactm as panel
	
	 		Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            If Not (Page.IsPostBack) Then


            End If
            pagesetup()
        End Sub
      
      
        
        Public Sub click_sendemail(ByVal sender As Object, ByVal e As EventArgs)
        		if msemail.text.length > 0 then
	            Dim mail As New MailMessage()
	
	            'Set the properties - send the email to the person who filled out the
	            mail.From = New MailAddress(msemail.Text)
	            mail.To.Add("sales@webmagicportal.com")
	            mail.Subject = mssub.Text
	            mail.Body = msbody.Text
	           	mail.Body = mail.Body + "<br ><br>" + msname.text
	            'send the message
	            mail.IsBodyHtml = True
	            Dim smtp As New SmtpClient("192.168.235.12")
	            smtp.Send(mail)
	            contactr.visible=true
	            contactm.visible=false
				else
					msemail.text="Required!"
				end if
        End Sub

      
         Public Sub pagesetup()

         'width will be calculated automatically, but it is sometimes
           	layout.Width = System.Configuration.ConfigurationManager.AppSettings("ScreenWidth")
            'leftNav.Width = System.Configuration.ConfigurationManager.AppSettings("ScreenLeftNav")            
            body.Height = System.Configuration.ConfigurationManager.AppSettings("ScreenBodyHeight")
          	body.Width = System.Configuration.ConfigurationManager.AppSettings("ScreenBodyWidth")
            layout.Border = System.Configuration.ConfigurationManager.AppSettings("ScreenBorder")
            footer.controls.add(LoadControl(System.Configuration.ConfigurationManager.AppSettings("ScreenFooter")))
            Header.Controls.Add(LoadControl(System.Configuration.ConfigurationManager.AppSettings("ScreenHeader")))
            'leftNav.Controls.Add(LoadControl(System.Configuration.ConfigurationManager.AppSettings("ScreenLeftNav")))
           
            body.VAlign = "top"            
            'leftNav.VAlign = "top"
            
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