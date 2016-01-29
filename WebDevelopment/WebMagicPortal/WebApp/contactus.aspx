<%@ Page language="vb" Codebehind="contactus.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.contactus" Debug="false" trace="false" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<% @Import Namespace="System.Net.Mail" %>
<script language="vb" runat="server">

Sub btnSendrequest_Click(sender as Object, e as EventArgs)
 Dim mail As New MailMessage()
  mail.From = New MailAddress("system@webmagicportal.com")
  mail.To.Add("sbialas@gochoiceone.com")
 mail.Subject = "www.webmagicportal.com Client Request"
  mail.Body = "At " + DateTime.Now + " a request was  sent from the webmagicportal.com " & _
               "Web site.  Below you will find the details for the message. " & _
 					vbCrLf & _
               "____________________________________________________" & vbCrLf & vbCrLf & _
               "Contact Information" & vbcrlf & _
               "-------------------" & vbcrlf & _
               "Name:    " & txtfname.text & " " & txtlname.text & vbcrlf & _
                "Phone:   " & txtphone.text & " " & ddphonetype.selecteditem.value & vbcrlf & _
               "Email:   " & txtemail.text & vbcrlf & _
               vbcrlf & _
               "Details" & vbcrlf & _
               "------------------" & vbcrlf & _
               "Comments:" & vbcrlf & _
               "-------------------------------" & vbcrlf & _
               txtMessage.Text & vbCrLf
'send the message
  Dim smtp As New SmtpClient("smtp.comcast.net")
  smtp.Send(mail)

  panelSendEmail.Visible = false
  panelMailSent.Visible = true
End Sub

</script>
<HTML>
	<HEAD>
		<title>Choice One BMS</title>
	</HEAD>
	
<body >
	<form id="forms1a" runat="server" enctype="multipart/form-data" method="post">
	<asp:panel id="panelSendEmail" runat="server">

	    <table width=600>
					<tr>
						<td colspan=2><b><i>To help you better, please provide us as much information as possible and click SUBMIT</i></b></td>
					</tr>
					<tr>
						<td width=45% valign=top>
							<table width=100% cellpadding=2 cellspacing=2>
								<tr>
									<td align=right>Name:</td>
									<td><asp:textbox id="txtFName" size=15 runat="server" /></td>
									<td><asp:textbox id="txtLName" size=15 runat="server" /></td>
								</tr>
								<tr>
									<td class=cntform></td>
									<td class=cntform>First</td>
									<td class=cntform>Last</td>
								</tr>
								
								<tr>
									<td align=right>Phone:</td>
									<td><asp:textbox id="txtphone" size=15 runat="server" /></td>
									<td>
										<asp:DropDownList id="ddphonetype" runat="server" >
											<asp:ListItem Value="home" Text="Home"/>
											<asp:ListItem Value="work" Text="Work"/>
											<asp:ListItem Value="mobile" Text="Mobile"/>
										</asp:DropDownList>
									</td>
								</tr>
								<tr>
									<td></td>
									<td></td>
									<td class=cntform>Type</td>
								</tr>
								<tr>
									<td align=right>Email:</td>
									<td colspan=2 valign="bottom"><asp:textbox id="txtEmail" size=40 runat="server" /></td>
								</tr>
							</table>
							<table>
								<tr>
									<td>Comments:</td>
								</tr>
								<tr>
									<td colspan=2><asp:textbox id="txtMessage" TextMode="MultiLine" Columns="90" Rows="15" runat="server" /></td>
								</tr>
							</table>
							<table width=350>
							<tr>
								<td height=10></td>
							</tr>
							<tr>
								<td align=center>
									<asp:button runat="server" id="btnSendrequest" Text="Submit" OnClick="btnSendrequest_Click" />
								</td>
							</tr>
							<tr>
								<td height=10></td>
							</tr>
						</table>
						</td>
					</tr>
				</table>
				</asp:panel>
		<asp:panel id="panelMailSent" runat="server" Visible="False">
			A email message has been sent on your behalf.  You will be contacted as soon as some one is available.  Thank You!
		</asp:panel>
	</form>
	</body>	
</HTML>
