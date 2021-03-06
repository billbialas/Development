<%@ Page language="vb" Codebehind="viewbpo.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.viewbpo" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<HTML>
	<HEAD>
		<title>Choice One Realty- Michigan</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	
	<body MS_POSITIONING="FlowLayout">
		<form  name="viewbpo"  runat="server" >
		<asp:Panel id="pnlheaderview" runat="server">	
			<table>
				<tr>
					<td><b><font size=3>View BPO Details</font></b></td>
				</tr>
			</table>
		</asp:panel>
		<asp:Panel id="pnlacceptbpo" visible=false runat="server">	
			<table>
				<tr>
					<td>This BPO has not been assigned.  Etc.lating latingasdasd</td>
				</tr>
				<tr>
					<td>Do you accept these terms?</td>
				</tr>
			</table>
			<table>	
				<tr>
					<td width=30><asp:button id="bpo_acceptterms" runat=server text="Accept" onclick="btn_acceptterms" /></td>
  					<td width=30><asp:button id="bpo_cancelterms" runat=server text="Cancel" onclick="btn_cancel" /></td>
  				</tr>	
			</table>
					
			
		</asp:panel>
		
  		<asp:Panel id="pnlviewbpo" visible=false runat="server">
  			<table width=100% border=0>
  				<tr>	
  					<td width=500></td>
					<td width=30 align=left colspan=2><asp:button id="bpo_acceptbpo" runat=server text="Accept" onclick="btn_acceptbpo" CausesValidation="False" /> 					
  					<td width=30 align=right colspan=2><asp:button id="bpo_edit" runat=server text="Edit Information" onclick="btn_editbpo" CausesValidation="False"/></td>
  					<td width=30 align=left colspan=2><asp:button id="bpo_cancel" runat=server text="Cancel" onclick="btn_cancel" CausesValidation="False" />
 				</tr>
 			</table>
 			<table>	
				<tr>
					<td width=250></td>
  					<td>C1 BPO #:</td>
  					<td width=100><asp:textbox id="bpo_number" runat=server size=5  /></td>
  					<td>BPO Status</td>
  					<td><asp:DropDownList ID="bpo_status" DataTextField="bpostatus"
					   DataValueField="tbl_bpostatus_PK" Runat="server" /></td>
  				</tr>
  			</table>
  			
 						<table cellpadding=2 cellspacing=2 border=0 width=100%>
  								<tr>
  									<td  colspan=6 align="left"><font size=2><b>Requestor Info</b></font></td>
  								</tr>
  								<tr>
  									<td width=90 align=right>Requestor</td>
  									<td width=30><asp:textbox id="bpo_requester" runat=server size=30  /></td>
  									<td width=10>	</td>
      							<td width=90 align=right>Company</td>
  									<td width=30><asp:DropDownList id="bpo_company" runat="server" >
  	    							<asp:ListItem Value="bpr" Text="Blue Print Realty"/>
  	    							<asp:ListItem Value="CH" Text="Classique Homes"/>
  	    							<asp:ListItem Value="Other" Text="Other"/>
  								</asp:DropDownList>
 
     							<td width=10></td>
							</tr>
							<tr>	
  								<td width=90 align=right>Phone</td>
  								<td width=30><asp:textbox id="bpo_phone" runat=server size=20  /></td>
  								<td width=10>	<asp:RequiredFieldValidator runat="server" id="rfvphone"
          							ControlToValidate="bpo_phone" display="dynamic">
          							Required
      							</asp:RequiredFieldValidator>
      							<asp:regularexpressionvalidator id="revPhone" controltovalidate="bpo_phone" display="dynamic"
         					validationexpression="^[2-9]\d{2}-\d{3}-\d{4}$" errormessage="Format xxx-xxx-xxxx" runat="server"/>
      						</td>
  								<td width=90 align=right>Fax</td>
  								<td width=30><asp:textbox id="bpo_fax" runat=server size=20 /></td>
  								<td width=10></td>	
  							</tr>
  							<tr>	
  								<td width=90 align=right> Choice One Compensation</td>
  								<td width=30><asp:textbox id="bpo_comp" runat=server size=10 /></td>
  								<td width=10></td>	
  				  				<td width=90 align=right>Prefered Agent</td>
  								<td width=30><asp:DropDownList ID="bpo_preferagent" DataTextField="AgentName"
					   DataValueField="users_tbl_Pk" Runat="server" /></td>
  								<td width=10></td>	
  							</tr>
  							<tr>
  								<td width=90 align=right>Rush</td>
  								<td width=30><asp:DropDownList id="bpo_rush" runat="server" >
  	    								<asp:ListItem Value="Yes" Text="Yes"/>
    									<asp:ListItem Value="No" Text="No"/>
  										</asp:DropDownList></td>
  								<td width=10></td>	
  								<td width=90 align=right>BPO Needed By</td>
  								<td width=30><asp:textbox id="txtDate" runat=server size=10 />	</td>
      						<td width=10><asp:RequiredFieldValidator runat="server" id="rfvDate"
          							ControlToValidate="txtDate" display="dynamic">
          							Required
      						</asp:RequiredFieldValidator>
      						<asp:CompareValidator runat="server" id="cvDate"
          						ControlToValidate="txtDate" display="dynamic"
          							Operator="DataTypeCheck" Type="Date">
          							Date must be in the format MM/DD/YYYY or MM-DD-YYYY.
      						</asp:CompareValidator>	</td>
							</tr>
						</table>
  						<table cellpadding=2 cellspacing=2 border=0 width=100%>	
  							<tr height=20><td colspan=6></td></tr>
  							<tr>
  								<td  colspan=6 align="left"><font size=2><b>Customer Info</b></font></td>
  							</tr>
  							<tr>
  								<td width=90 align=right>Name</td>
				  				<td width=30><asp:textbox id="bpo_cusname" runat=server size=30 /></td>
  								<td width=10></td>	
  								<td width=90 align=right>Loan No</td>
  								<td width=30><asp:textbox id="bpo_cusloanno" runat=server size=20 /></td>
  								<td width=10></td>	
  							</tr>
  							<tr>
  								<td width=90 align=right>Contact</td>
  								<td width=30><asp:textbox id="bpo_cuscontact" runat=server size=30 /></td>
  								<td width=10></td>
  								<td width=90 align=right>Contact Phone</td>
  								<td width=30><asp:textbox id="bpo_cuscontactphone" runat=server size=30 /></td>
  								<td width=10></td>
  			  				</tr>
  							<tr height=20><td colspan=6></td></tr>
  							<tr>
  								<td  colspan=6 align="left"><font size=2><b>Property Info</b></font></td>
  							</tr>
  							<tr>	
  								<td width=90 align=right>Property Address</td>
  								<td width=30><asp:textbox id="bpo_cusaddress" runat=server size=30 /></td>
  								<td width=10></td>	
  								<td width=90 align=right>Occupancy Status</td>
  								<td width=30><asp:DropDownList id="bpo_occupancy" runat="server" >
  	    							<asp:ListItem Value="Occupied" Text="Occupied"/>
    								<asp:ListItem Value="Vacant" Text="Vacant"/>
    									<asp:ListItem Value="unknown" Text="Unkown"/>
  									</asp:DropDownList></td>
  								<td width=10></td>	
  				 			</tr>
  						<tr>
  							<td colspan=6 align="right">
  								<table cellpadding=1 cellspacing=1 width=100% border=0><tr>	
  									<td width=25></td>
  									<td width=64 align=Center>City</td>
  									<td><asp:textbox id="bpo_cuscity" runat=server size=30 /></td>
  										<td width=50 align=right>State</td>
  									<td><asp:textbox id="bpo_cusstate" runat=server size=2 /></td>
  									<td width=50 align=right>Zip</td>
  								<td><asp:textbox id="bpo_cuszip" runat=server size=10 /></td>
  								</tr>
  							</table>
  						</td>
  					</tr>
  		  			<tr>
  						<td width=90 align=right>Home Owner Phone</td>
  						<td width=30><asp:textbox id="bpo_HomeOwnerphone" runat=server size=15 /></td>
  						<td width=10></td>	
  					</tr>
					<tr height=20><td colspan=6></td></tr>
  					<tr>
  						<td  colspan=6 align="left"><font size=2><b>Service Info</b></font></td>
  					</tr>
  					<tr>
  						<td width=90 align=right>Type</td>
  						<td width=30><asp:DropDownList id="bpo_type" runat="server" >
  	    						<asp:ListItem Value="RDB" Text="Regular Drive By"/>
    							<asp:ListItem Value="BIN" Text="BPO Interior"/>
  							</asp:DropDownList></td>
  				
  						<td colspan=3>
  							<table border=0>
  								<tr>
  									<td width=70 align=right>Photos</td>
  									<td width=30><asp:DropDownList id="bpo_photos" runat="server" >
  	    								<asp:ListItem Value="Yes" Text="Yes"/>
    									<asp:ListItem Value="No" Text="No"/>
  										</asp:DropDownList></td>
  									<td width=70 align=right># Interior</td>
  									<td width=30><asp:textbox id="bpo_nointerior" runat=server size=2 /></td>	
  									<td width=70 align=right># Exterior</td>
  									<td width=30><asp:textbox id="bpo_noexterior" runat=server size=2 /></td>
  								</tr>
  							</table>
  						</td>			
  					</tr>
  					<tr>	
  						<td width=90 align=right>Listed</td>
  						<td width=30><asp:DropDownList id="bpo_Listed" runat="server" >
  	    						<asp:ListItem Value="Yes" Text="Yes"/>
    							<asp:ListItem Value="No" Text="No"/>
  							</asp:DropDownList></td>
						<td width=10></td>	
  					</tr>
  					<tr>				
  						<td width=90 align=right>Listing Agent</td>
  						<td width=30><asp:textbox id="bpo_listagent" runat=server size=30 /></td>	
  						<td width=10></td>	
  						<td width=90 align=right>Phone</td>
  						<td width=30><asp:textbox id="bpo_listagentphone" runat=server size=20 /></td>				
  						<td width=10></td>	
  					</tr>
  					<tr>	
  						<td width=90 align=right>Interior Contact</td>
  						<td width=30><asp:textbox id="bpo_incontact" runat=server size=30 /></td>
  						<td width=10></td>	
						<td width=90 align=right>Phone</td>
  						<td width=30><asp:textbox id="bpo_incontactphone" runat=server size=20 /></td>	
 						<td width=10></td>	
  					</tr>
  					<tr>	
  						<td width=90 align=right >Instructions</td>
  						<td colspan=6><asp:textbox id="bpo_instructions" runat=server size=12 TextMode="MultiLine"
          				 Columns="50" Rows="5"  /></td>
  					</tr>	
  			  		<tr height=4>
  						<td></td>
  					</tr>
  			</table>	
 		</table>
 	<table border=0> 
  			  	</table>
 </asp:Panel> 


	
</form>
	</body>
</HTML>
