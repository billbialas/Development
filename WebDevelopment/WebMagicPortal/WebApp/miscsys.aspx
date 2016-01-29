<%@ Page language="vb" Codebehind="miscsys.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.miscsys" Debug="false" trace="false" aspcompat=true  %>
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
				<td colspan=2><b><u>System Messages Stuff</u></b></td>
			</tr>
		</table>
		<table>
			<tr>
				<td colspan=2>Update System Message</td>
			</tr>			
			<tr>
				<td colspan=2><asp:textbox id="txtsysnotes" runat=server size=16 TextMode="MultiLine" Columns="120"  Rows="15" /></td>
			</tr>
			<tr>
				<td width=140>Reset User Status&nbsp<asp:checkbox id="resetusrstat" runat=server /></td>
			</tr>
			<tr>	
				<td><asp:button id="btnsavesysmess" Visible=true  runat=server text="Save" width="70" onclick="updatesysmessage" CausesValidation="False"  Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:10pt;width:200px; cursor:hand" /></td>					
				 	
		</table>
		
 
		
	</form>
	</body>	
</HTML>
