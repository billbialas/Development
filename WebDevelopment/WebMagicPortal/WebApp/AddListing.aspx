<%@ Page language="vb" Codebehind="AddListing.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.AddListing" %>
<%@ Register TagPrefix="mbcbb" Namespace="MetaBuilders.WebControls" Assembly="MetaBuilders.WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script language="JavaScript" src="../_include/default.js"></script>

<HTML>
	<HEAD>
		<title>Choice One Realty- Michigan</title>
	</HEAD>
	
	<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">
		<form  name="addlisting"  runat="server" >
			<table cellpadding=2 cellspacing=2 border=0 width=100%>
  			<tr>
  				<td  colspan=6 align="left"><font size=2><b>Property Information</b></font>
  				</td>
  			</tr>
  			<tr>
  				<td width=90 align=right>Status</td>
  				<td><asp:DropDownList id="p_status" runat="server" >
    							<asp:ListItem Value="active" Text="Active"/>
  	    						<asp:ListItem Value="Pending" Text="Pending"/>
  	    						<asp:ListItem Value="Closed" Text="Closed"/>
  	    						<asp:ListItem Value="Withdrawn" Text="Withdrawn"/>
 						</asp:DropDownList>
  				</td>
  			</tr>
  			<tr>
  				<td width=90 align=right>Address</td>
  				<td width=30><asp:textbox id="P_address" runat=server size=5 /></td>
  				<td width=30><asp:textbox id="P_Street" runat=server size=30 /></td>
			</tr>
			<tr>
  				<td width=90 align=right>City</td>
  				<td width=30><asp:textbox id="P_city" runat=server size=30 /></td>
  				<td width=90 align=right>State</td>
  				<td width=30><asp:textbox id="P_state" runat=server size=5 /></td>
				<td width=90 align=right>Zip</td>
  				<td width=30><asp:textbox id="P_zip" runat=server size=8 /></td>
			</tr>
			</table>
			<table>
			<tr>
				<td>Seller</td>
				<td><mbcbb:ComboBox id="ComboBox1" runat="server" DataTextField="company"
					   DataValueField="tbl_bpocompanys_pk" >
 </mbcbb:ComboBox>
</td>
		</tr>
		</table>		
		
		</form>
	</body>
</HTML>
