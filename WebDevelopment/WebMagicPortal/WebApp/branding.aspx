<%@ Page language="vb" Codebehind="branding.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.branding" Debug="false" trace="false" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script language="JavaScript" src="../_include/default.js"></script>
<HTML>
	<HEAD>
		<title>WebMagicPortal.com</title>
	</HEAD>
	
<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">
	<form id="forms1a" runat="server">
		<div style="vertical-align: top; height: 470px; overflow:auto;">
		<asp:panel id='pnlbrandup' runat=server visible=false>
			<table>
				<tr>
					<td><font size=4 color=red>To make changes to this page you must <a href="upgrades.aspx">upgrade</a> your subscription</font></td>
				</tr>
			</table>
		</asp:panel>
		<table>
			<tr>
				<td><font size=2><b>Default Branding Setup</b></font></td>
			</tr>
		</table>
		<table>
		    <tr>
		        <td width=120 align=left>Show Logo</td>
		        <td><asp:CheckBox ID="chkshowlogo" runat=server /></td>
		    </tr>
		</table>
		<table>
		    <tr>
		        <td width=120 align=left>Redirect url </td>
		        <td><asp:TextBox ID="txtredirecturl" runat=server size =70/></td>
		    </tr>
		</table>
		
		<table>
		    <tr>
		        <td width=120 align=left valign=top>Intake Page 1 Text</td>
		        <td><asp:textbox id="pg1text" runat=server size=16 TextMode="MultiLine" Columns="90"  Rows="8" /></td>
								
		    </tr>
		</table>
		<table>
		    <tr>
		        <td width=120 align=left>Intake Page 2 Text</td>
		        <td><asp:textbox id="pg2text" runat=server size=16 TextMode="MultiLine" Columns="90"  Rows="8" /></td>
			</td>
               
		    </tr>
		</table>
		<table>
		    <tr>
		        <td width=120 align=left>Email Response:</td>
		        <td><asp:CheckBox ID="chksendemail" runat=server AutoPostBack =true OnCheckedChanged ="enablebutts"/></td>
               
		    </tr>
		</table>
		<table width=100%>
		    <tr>
		        <td width=120 align=left>Email From:</td>
		        <td><asp:TextBox ID="TextBox1" runat=server size=50 Enabled=false /></td>
               
		    </tr>
		    <tr>
		        <td width=120 align=left>Email Reply To:</td>
		        <td><asp:TextBox ID="TextBox2" runat=server size=50 Enabled=false /></td>
               
		    </tr>
		      <tr>
		        <td width=120 align=left>Email Subject</td>
		        <td><asp:TextBox ID="TextBox3" runat=server size=70 Enabled=false /></td>
               
		    </tr>
		      <tr>
		        <td width=120 align=left>Email Body:</td>
		        <td><asp:textbox id="emailbody" runat=server size=16 TextMode="MultiLine" Columns="90"  Rows="8" Enabled=false /></td>
		    </tr>
		</table>
		<table>
		    <tr>
		        <td width=120 align=left> 	<asp:button id="bsaveco" runat=server text="Save" onclick="updatebranding" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:80px; cursor:hand" /></td>
		       
               
		    </tr>
		</table>
		</div>
	  	</form>
</body>	
</HTML>
