<%@ Page language="vb" Codebehind="WFfullscreen.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.WFfullscreen" Debug="false" trace="false" aspcompat=true validateRequest="false"  %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %> 

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script language="JavaScript" src="../_include/default.js"></script>
<link rel="stylesheet" href="../_include/default.css" type="text/css">

<HTML>
	<HEAD>
		<title>www.WebMagicPortal.com</title>
	</HEAD>
	
<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">
	<form id="forms1a" runat="server" enctype="multipart/form-data" method="post">
		<table>	
		 	<tr>
				<td width=3%></td>
				<td><FTB:FreeTextBox id="emailbody" runat="server" width="600" height=200 /></td>
			</tr>
		</table>
		<table>
			<tr>
				<td><asp:button id="aa" runat=server text="Save"  CausesValidation="false" cssclass=frmbuttons onclick="btnsave" />	</td>
			</tr>
		</table>				

	</form>
	</body>	
</HTML>
