<%@ Page language="vb" Codebehind="intakep.aspx.vb"  Inherits="PageTemplate.inp1" AutoEventWireup="false" Debug="false" trace="false" validateRequest=false  %>


<HTML>
	<HEAD>
		<title>WebMagicPortal.com</title>
			<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	
	<body >
		<form  name="intakeform"  runat="server" >
			
			<asp:panel id="MainForm" runat="Server">
			<table border=0 width="60%" cellspacing=0 cellpadding=0>
  				<tr>		
  				    <td width=120 ><img id="Logoarea" alt="" src="images/default.jpg" height=80 width=100 border="0" runat=server></td>
     				<td>
     				    <table>
     				        <tr>
     				            <td ><font size=5><b><asp:label ID="coname" runat=server /></b></font></td>
     				       </tr>
     				       <tr>
     				            <td ><font size=3><asp:label ID="hdtxt1" runat=server /></font></td>
     				       </tr>
     				       <tr>
     				            <td ><font size=3><asp:label ID="hdtxt2" runat=server /></font></td>
     				       </tr>
     				    </table>
     				</td>
     				
     				
     		
     			</tr>
     			
			</table>
			
			<table width="53%">
			    <tr>
     			    <td ><hr id="pg1hr" runat=server /></td>
     			</tr>
     	    </table>
			
			<table width="60%">
				<tr>
				    <td><asp:Label ID="brandtext1" runat=server /> </td>
				</tr>
				
			</table>
			<br />
			<table width=40% cellpadding=2 cellspacing=2 border=0>
				<tr>
					<td align=left width=90>Name:</td>
					<td><asp:textbox id="txtFName" size=20 runat="server" /></td>
					<td><asp:textbox id="txtLName" size=34 runat="server" /></td>							
				</tr>
				<tr>
					<td align=left width=90></td>
					<td>First<asp:RequiredFieldValidator runat="server" id="rfvfname"
          				ControlToValidate="txtFName" display="dynamic">
          				Required
      				</asp:RequiredFieldValidator></td>
					<td>Last<asp:RequiredFieldValidator runat="server" id="rfvlname"
          				ControlToValidate="txtlName" display="dynamic">
          				Required
      				</asp:RequiredFieldValidator></td>							
				</tr>
			</table>
			<table width=40% cellpadding=2 cellspacing=2 border=0>
				<tr>
					<td align=left width=90>Phone:</td>
					<td><asp:textbox id="txtHphone" size=60 runat="server" /></td>
					
				</tr>
			</table>
			<table width=52% cellpadding=2 cellspacing=2 border=0>
				<tr>
					<td align=left width=116>Email:</td>
					<td><asp:textbox id="txtemail" size=60 runat="server" /></td>
					
				</tr>
				
			</table>
			<asp:Panel ID="pnlemailphonereq" runat=server Visible=false>
			    <table>
			        <tr>
			            <td>Please Enter a Phone Number or Email Address</td>
			        </tr>
			   </table>
			 </asp:Panel>

			<table cellpadding=2 cellspacing=2 border=0  width=100%>
				<tr>
					<td>Comments:</td>
				</tr>
				<tr>
			   	<td><asp:textbox id="txtnotes" runat=server size=16 TextMode="MultiLine" Columns="53"  Rows="10" /></td>
				</tr>
			</table>	
			<table width=52%>
					<tr>
						<td height=10></td>
					</tr>
					<tr>
						<td align=right>
							<asp:button runat="server" id="btnSendrequest" Text="Submit" enabled=false OnClick="btnSendrequest_Click" CausesValidation=false />
						</td>
					</tr>
					<tr>
						<td height=10></td>
					</tr>
				</table>
			</asp:panel>
			<asp:panel id="Thankyou" runat="Server" visible="False">
			<table border=0 width="60%" cellspacing=0 cellpadding=0>
  				<tr>	
    				<td width=120><img id="logoarea2" alt="" src="images/default.jpg" height=80 width=100 border="0" runat=server></td>
     			   <td>
     				    <table>
     				        <tr>
     				            <td ><font size=5><b><asp:label ID="coname2" runat=server /></b></font></td>
     				       </tr>
     				       <tr>
     				            <td ><font size=3><asp:label ID="hdtxt1A" runat=server /></font></td>
     				       </tr>
     				       <tr>
     				            <td ><font size=3><asp:label ID="hdtxt2A" runat=server /></font></td>
     				       </tr>
     				    </table>
     				</td>
     					     		
     			</tr>
			</table>
			<table width="60%">
			    <tr>
     			    <td ><hr id="pg2hr" runat=server /></td>
     			</tr>
     	    </table>
			
			<table width="60%">
				<tr>
					<td><asp:Label ID="brandtext2" runat=server /></td
				</tr>
				<tr>
				    <td><asp:button runat="server" id="redirect"  enabled=false Text="Continue" OnClick="btncontinue_Click" />
					</td></tr>
			</table>
			
			</asp:panel>
			<asp:textbox id="emailbody" runat=server size=16 TextMode="MultiLine" Columns="90"  Rows="20" Visible=false /></td>
                	                
		</form>
	</body>
</HTML>	
