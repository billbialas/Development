<%@ Page language="vb" Codebehind="redirect.aspx.vb" Inherits="gredirect.redirect" Debug="false" trace="false"%>
<link rel="stylesheet" href="../_include/default.css" type="text/css">
<html >

<head runat="server">
<meta http-equiv="Content-Language" content="en-us" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>www.WebMagicPortal.com</title>
</head>

<body style="background-color: #FFFFFF">

<form id="MAIN" runat="server">
   <asp:panel id="pnlthanks" runat=server visible=true>
	   <table width=100%>
        <tr>
            <td width=130><img  height="90" width="100" alt="" src="../images/Magic-128x128.png" border="0"></td>
            <td valign=top><img  alt="" src="../images/head.jpg" border="0"></td>
        </tr>
        <tr>
        		<td colspan=2 align=left><hr /></td>
        </tr>
    </table>
    <table>
	   	<tr>
	   		<td><h2>Thank You for your purchase!</h2></td>
	   	</tr>
	   	<tr>
	   		<td>The details of the lead you purchased and a receipt have been emailed to: <asp:label id=emadd runat=server /></td>
	   	</tr>
	   	<tr>
	   		<td>In case you do not receive your lead information please call us at 1-888-233-4390 and
	   			refence lead # <asp:label id=ldno runat=server /></td>
	   	</tr>	   	
	   </table><br>
	   <table >
	   	<tr>
	   		<td><h4>To buy access to unlimited lead generation for as little as $34.99 a month click here: </h4></td>
	   	
	   		<td ><h4><asp:label id=weblink runat=server /></h4></td>
	   	</tr>
	   </table>
	   <table width=100% height=300>
	   	<tr>
	   		<td valign=bottom><hr /></td>
	   	</tr>
	   </table>
    <table >
        <tr>
            <td>Copyright © 2009 B-Squared Consulting, Inc. All rights reserved</td></tr></table>
	</asp:panel>
</form>
</body>

</html>
