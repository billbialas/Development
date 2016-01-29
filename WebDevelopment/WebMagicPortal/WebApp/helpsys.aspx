<%@ Page language="vb" Codebehind="helpsys.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.helpsys" Debug="false" trace="false" aspcompat=true validateRequest=false  %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %> 

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script language="JavaScript" src="../_include/default.js"></script>

<HTML>
	<HEAD>
		<title>www.WebMagicPortal.com</title>
	</HEAD>
	
<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">
	<form id="forms1a" runat="server" enctype="multipart/form-data" method="post">
		<table>
			<tr>
				<td><b><u>Misc System Stuff</u></b></td>
			</tr>
		</table>
		<table>
			<tr>
				<td>Update Help Message</td>
			</tr>			
			<tr>
				<td><FTB:FreeTextBox id="txtsysnotes" runat="server" width="700" height=230 /></td>
			</tr>
			<tr>
				<td><asp:button id="btnsavesysmess" Visible=true  runat=server text="Save" width="70" onclick="updatesysmessage" CausesValidation="False"  Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:10pt;width:200px; cursor:hand" /></td>					
				 	
		</table>
		
 
		
	</form>
	</body>	
</HTML>
