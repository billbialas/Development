<%@ Page language="vb" Codebehind="template.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.template" Debug="false" trace="false" aspcompat=true  %>
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
				<td><asp:button id=btntest runat=server onclick="btntestA" /></td>
			</tr>
		</table>
 
		
	</form>
	</body>	
</HTML>
