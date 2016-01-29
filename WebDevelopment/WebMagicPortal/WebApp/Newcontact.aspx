<%@ Page language="vb" Codebehind="newcontact.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.newcontact" %>
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
		<form  name="addbpo"  runat="server" defaultfocus="bpo_requester">
	<asp:Panel id="pnlheaderAdd" runat="server">	
		<table>
			<tr>
				<td><b><font size=3>Add Contact</font></b></td>
			</tr>
			<tr>
				<td>Please complete the form below and select Submit</td>
			</tr>	
		</table>
	</asp:panel>	
	
 	<asp:Panel id="pnlnewbpo" visible=true runat="server">	
		<table cellpadding=2 cellspacing=2 border=0 width=100%>
  			<tr>
  				<td  colspan=6 align="left"><font size=2><b>Information</b></font>
  				</td>
  			</tr>
  			<tr>
  				<td width=90 align=right>Client Type</td>
  				<td width=30><asp:DropDownList id="contacttype" runat="server" >
  	    						<asp:ListItem Value="Buyer" Text="Buyer"/>
    							<asp:ListItem Value="Seller" Text="Seller"/>
    							<asp:ListItem Value="Both" Text="Both"/>
  							</asp:DropDownList></td>
  			</tr>
  			<tr>
  				<td width=90 align=right>Last Name</td>
  				<td width=30><asp:textbox id="lname" runat=server size=30 /></td>
      		<td width=90 align=right>First Name</td>
  				<td width=30><asp:textbox ID="fname"  runat=server size=30 /></td>
 			</tr>
			<tr>	
  				<td width=90 align=right>Home Address</td>
  				<td width=30><asp:textbox id="haddress" runat=server size=30 /></td>
  			</tr>
  			<tr>
  				<td colspan=6 align="right">
  					<table cellpadding=1 cellspacing=1 width=100% border=0><tr>	
  						<td width=25></td>
  						<td width=64 align=Center>City</td>
  						<td><asp:textbox id="hcity" runat=server size=30 /></td>
  						<td width=50 align=right>State</td>
  						<td><asp:textbox id="hstate" runat=server size=2 /></td>
  						<td width=50 align=right>Zip</td>
  						<td><asp:textbox id="hzip" runat=server size=10 /></td>
  					</tr></table>
  				</td>
  			</tr>
  			<tr>
  				<td colspan=2><asp:checkbox id="chkaddresscopy" checked="false" OnCheckedChanged="ClientOnChange"  AutoPostBack="true" runat="server" />
  						Check Box if Mailing Address is the same as Home</td>
  			</tr>
  			<tr>	
  				<td width=90 align=right>Mailing Address</td>
  				<td width=30><asp:textbox id="maddress" runat=server size=30 /></td>
  			</tr>
  			<tr>
  				<td colspan=6 align="right">
  					<table cellpadding=1 cellspacing=1 width=100% border=0><tr>	
  						<td width=25></td>
  						<td width=64 align=Center>City</td>
  						<td><asp:textbox id="mcity" runat=server size=30 /></td>
  						<td width=50 align=right>State</td>
  						<td><asp:textbox id="mstate" runat=server size=2 /></td>
  						<td width=50 align=right>Zip</td>
  						<td><asp:textbox id="mzip" runat=server size=10 /></td>
  					</tr></table>
  				</td>
  			</tr>
  			<tr>	
  				<td width=90 align=right>Home Phone</td>
  				<td width=30><asp:textbox id="hphone" runat=server size=30 /></td>
  				<td width=90 align=right>Cell Phone</td>
  				<td width=30><asp:textbox id="cphone" runat=server size=30 /></td>
  	  		</tr>
  			<tr>
  				<td width=90 align=right>Email</td>
  				<td width=30><asp:textbox id="email" runat=server size=30 /></td>
  				
  			</tr>
  			<tr>
  				<td width=90 align=right>Notes</td>
  				<td width=30><asp:textbox id="notes" runat=server size=30 /></td>
  				
  			</tr>
  		</table>	
		<table border=0> 
  			<tr>	
  				<td width=50 align=left calspan=4><asp:button id="savecontact" runat=server text="Save" onclick="btn_savecontact" CausesValidation="False" />
				<td width=50 align=left calspan=4><asp:button id="addlisting" runat=server text="Add Listing" onclick="btn_addlisting" /></td>
  		</tr>
  	</table>
  		
 </asp:Panel> 
	
</form>
	</body>
</HTML>
