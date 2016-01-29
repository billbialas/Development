<%@ Page language="vb" Codebehind="contact.aspx.vb" Inherits="PageTemplate.contact" Debug="false" trace="false"%>
<link rel="stylesheet" href="../_include/default.css" type="text/css">
<script type="text/javascript" src="../_include/jquery.js"></script>
<script type="text/javascript" src="../_include/default.js"></script>

<html >

<head runat="server">
 <title>www.webmagicportal.com- Pricing</title>
	
</head>

<body style="background-color: #FFFFFF">

<form id="MAIN" runat="server">
  

<ul id="nav">
	<li id="nav-1"><a href="http://www.webmagicportal.com/features.aspx">Features</a></li>
	<li id="nav-2"><a href="http://www.webmagicportal.com/pricing.aspx">Pricing</a></li>
	<li id="nav-4"><a href="http://www.webmagicportal.com/contact.aspx">Contact</a></li>
	<li id="nav-5"><a href="http://app.webmagicportal.com">Log IN</a></li>
</ul>
<div id="PG_BackGround" >
	<div class="content">
		<div class="sub-heading">
			<img src="../images/contactbk.jpg" />				
		</div>
		<asp:panel id ="contactm" runat=server visible=true>
			<div id="contactus" cellpadding=2 cellspacing=2>
				<table>
					<tr>
						<td ><h1>Message Subject</h1></td>
						<td><asp:textbox id="mssub" runat=server  size=70 /></td>
					</tr>
					<tr>
						<td valign=top><h1>Message</h1></td>
						<td><asp:textbox id="msbody" runat=server TextMode="MultiLine" Columns="55"  Rows="13"  /></td>
					<tr>
					</tr>
						<td><h1>Name</h1></td>
						<td><asp:textbox id="msname" runat=server  size=70 /></td>
					</tr>
					<tr>
						<td><h1>Email</h1></td>
						<td><asp:textbox id="msemail" runat=server  size=70 /></td>
					</tr>
				</table>
				
				<p style="margin-left:500"><asp:button id="btnsend" runat=server text="Send"  onclick="click_sendemail" CausesValidation="true" cssclass=frmbuttons /></p>			          
			</div>
		</asp:panel>
		<asp:panel id ="contactR" runat=server visible=false>
			<div id="contactus" cellpadding=2 cellspacing=2>
				<p>Thank You! Your email has been sent.</p>
			</div>
		</asp:panel>
		<div>
			<p style="margin-top:5;color: #236B9E;font-family: Georgia;text-decoration: none;font-weight: bold;font-size: 120%;">How to Reach Us!</p>
			<p style="font-size:80%;font-weight:bold;">At Webmagic we are continually working hard to improve your exeperience with us! So email is the best way to get in contact with us.</p>
			<p style="font-size:80%;font-weight:bold;">Please take a minute fill out the form and click 'Send'. One of our represenatives will contact you as soon as possible.</p>
			<p style="margin-left:900;"><img src="../images/sig.jpg" />	</p>
		</div>
	</div>
</div>
</form>
</body>

</html>
