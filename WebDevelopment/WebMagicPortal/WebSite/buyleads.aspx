<%@ Page language="vb" Codebehind="buyleads.aspx.vb"  Inherits="PageTemplate.buyleads" AutoEventWireup="false" Debug="false" trace="false" validateRequest=true  %>


<HTML>
	<HEAD>
		<title>WebMagicPortal.com</title>
			<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	
	<body >
		<form  name="intakeform"  runat="server" >
			
			<asp:panel id=pnlleadnotavail runat=server visible=false>
				   <table width=100%>
        <tr>
            <td width=130><img  height="90" width="100" alt="" src="../images/Magic-128x128.png" border="0"></td>
            <td valign=top><img  alt="" src="../images/head.jpg" border="0"></td>
        </tr>
        <tr>
        		<td colspan=2 align=left><hr /></td>
        </tr>
    </table><table>
					<tr>
						<td><h3>Sorry! But this lead has already been purchased.</h3></td>
					</tr>
				</table><table >
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
</HTML>	
