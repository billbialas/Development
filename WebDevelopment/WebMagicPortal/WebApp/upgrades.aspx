<%@ Page language="vb" Codebehind="upgradesA.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.upgradesA" Debug="false" trace="false" aspcompat=true  %>
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
				<td><font size=3><b>Avaible Upgrades</b></font></td>
			</tr>
		</table>
		<br />
		<asp:panel id="pnlbranding" runat=server visible=false>
		<table width=60% class=tblbordera cellspacing=5 cellpadding=5>
			<tr>
				<td width=80>Branding<br><img id="brandicon" alt="" src="images/branding.jpg" height=60 width=80 border="0" runat=server></td>
				<td width=70% valign=middle>This is what brading is and why you REALLY REALLY REALLY REALLY should buy it.  I need to send my 3 kids to college so... Come on help me out!!</td>
				<td width=20%>
					<table>
						<tr>
							<td>Your Cost:&nbsp <asp:label id="bcost" runat=server /></td>
						</tr>
						<tr>
							<td><asp:button id="btnupbuy" Visible=true  runat=server text="Purchase" width="70" onclick="click_upgrade" CausesValidation="False"  Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:10pt;width:90px; cursor:hand" /></td>					
						</tr>
					</table>
				</td>
			</tr>
			<tr>
				<td></td>
				<td colspan=2><font size=1><b><i>NOTE: Your cost is based on todays date to the end date of your currently active subscription date</i></b></font></td>
			</tr>
		</table>
		</asp:panel>
		<asp:panel id="pnlautopostcredits" runat=server visible=false>
		<table width=60% class=tblbordera cellspacing=5 cellpadding=5>
			<tr>
				<td width=80 align=center>Auto Posting Credits<br><img id="apcreditsicon" alt="" src="images/typewriter.jpg" height=60 width=80 border="0" runat=server></td>
				<td width=70% valign=middle>Please Buy these credits they are a minumu of 10 per purchase.  You can use these to have US post your ad for you!  Yippie that would really make your life a WHOLE lot easier, don't ya think?</td>
				<td width=20%>
					<table>
						<tr>
							<td colspan=2>Cost/Post:&nbsp <asp:label id="apcost" runat=server /></td>
						</tr>
						<tr>
							<td><asp:DropDownList id="dd_apqty" runat="server" >    							               
    							                 <asp:ListItem Value="10" Text="10"/>
  	    						                 <asp:ListItem Value="20" Text="20"/>
  	    						                 <asp:ListItem Value="30" Text="30"/>
  	    						                 <asp:ListItem Value="40" Text="40"/>
  	    						                 <asp:ListItem Value="50" Text="50"/>
  	    						                 <asp:ListItem Value="60" Text="60"/>
  	    						                 <asp:ListItem Value="70" Text="70"/>
  	    						                 <asp:ListItem Value="80" Text="80"/>
  	    						                 <asp:ListItem Value="90" Text="90"/>
  	    						                 <asp:ListItem Value="100" Text="100"/>
  	    						                 <asp:ListItem Value="150" Text="150"/>
  	    						                 <asp:ListItem Value="200" Text="200"/> 						                
  	    						                </asp:DropDownList>
							<td><asp:button id="btnupbuyAP" Visible=true  runat=server text="Purchase" width="70" onclick="click_appurchase" CausesValidation="False"  Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:10pt;width:90px; cursor:hand" /></td>					
						</tr>
					</table>
				</td>
			</tr>
			
		</table>
		</asp:panel>

		<br /><br />
		<table width=60%>
			<tr>
				  <td align=right><asp:button id="btnupcancel" Visible=true  runat=server text="Cancel" width="70" onclick="click_cancelup" CausesValidation="False"  Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:10pt;width:80px; cursor:hand" /></td>					
			</tr>
		</table>	
				
 
		
	</form>
</body>	
</HTML>
