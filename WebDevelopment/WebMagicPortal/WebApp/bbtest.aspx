<%@ Page language="vb" Codebehind="bbtest.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.bbtest" Debug="false" trace="false" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script language="JavaScript" src="../_include/default.js"></script>
<HTML>
	<HEAD>
		<title>Choice One Realty- Michigan</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	
<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">
	<form id="test" runat="server">

		<table>
			<tr>
				<td>
					<asp:listbox id="lstAllcities" runat="server"  selectionmode="multiple" 
					 DataTextField="cty_city_name" DataValueField="cty_idx_cid" />
		   	</td>
			</tr>
		</table>
	</form>
	</body>	
</HTML>
