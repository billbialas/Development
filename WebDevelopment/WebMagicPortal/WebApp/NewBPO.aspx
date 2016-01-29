<%@ Page language="vb" Codebehind="NewBPO.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.NewBPO" %>
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
		<form  name="addbpo"  runat="server" defaultfocus="bpo_requester">
	<asp:Panel id="pnlheaderAdd" runat="server">	
		<table>
			<tr>
				<td><b><font size=3>Request New BPO</font></b></td>
			</tr>
			<tr>
				<td>Please complete the form below and select Submit</td>
			</tr>	
		</table>
	</asp:panel>	
	<asp:Panel id="pnlheaderconfirm"  runat="server">	
	<table width=100%>
			<tr>
				<td colspan=2><b><font size=3>Confirm Request</font></b></td>
			</tr>
		</table>
		<table width=100%>	
			<tr>
				<td colspan=2>Please confirm the information you have entered and click 'Submit'.  Click 'Change' to edit</td>
			</tr>		
			<tr>
				<td colspan=2><hr></td>
			</tr>
		</table>	
		<table width=100%>
			<tr>
				<td width=5><asp:checkbox id="chktermaccept" checked="false" OnCheckedChanged="ClientOnChange"  AutoPostBack="false" runat="server" /></td>
				<td>THIS IS A WAVIER PLEASE CHECK THIS BOX IF YOU ACCEPT THESE TERMS</td>
			</tr>
		</table>
<asp:Panel id="pnlchk2submit" visible=false runat="server">	
	<table width=100% border=1>
			<tr>
				<td><font color="red" size=2>You must Accept the terms as stated to submit the BPO for processing with Choice One!</font></td>
			
			</tr>
			
	</table>
	
</asp:panel>	
		<table width=100%>
			<tr>
				<td align=right width=80%><asp:button id="bpo_submit" runat=server text="Submit" onclick="btn_addbpo" /></td>
				<td align=left><asp:button id="bpo_edit" runat=server text="Change" onclick="btn_edit" /></td>
			
			</tr>
			
	</table>
 </asp:panel>	
	
 <asp:Panel id="pnlnewbpo" visible=false runat="server">	
			
  	
  		<table cellpadding=2 cellspacing=2 border=0 width=100%>
  			<tr>
  				<td  colspan=6 align="left"><font size=2><b>Requestor Information</b></font>
  				</td>
  			</tr>
  			<tr>
  				<td width=90 align=right>Requestor</td>
  				<td width=30><asp:textbox id="bpo_requester" runat=server size=30 /></td>
  				<td width=10>	<asp:RequiredFieldValidator runat="server" id="rfvrequestor"
          				ControlToValidate="bpo_requester" display="dynamic">
          				Required
      				</asp:RequiredFieldValidator>
      		</td>
      		<td width=90 align=right>Company</td>
  				<td width=30><asp:DropDownList ID="bpo_company" DataTextField="company"
					   DataValueField="tbl_bpocompanys_pk" Runat="server" />	</td>
  				<td width=10></td>
			</tr>
			<tr>	
  				<td width=90 align=right>Business Phone</td>
  				<td width=30><asp:textbox id="bpo_phone" runat=server size=20 /></td>
  			
  				<td width=10>	<asp:RequiredFieldValidator runat="server" id="rfvphone"
          					ControlToValidate="bpo_phone" display="dynamic">
          					Required
      				</asp:RequiredFieldValidator>
      				<asp:regularexpressionvalidator 
         					id="revPhone"
         					controltovalidate="bpo_phone"
         					display="dynamic"
         					validationexpression="^[2-9]\d{2}-\d{3}-\d{4}$"
         					errormessage="Format xxx-xxx-xxxx"
         					runat="server"/>
      		</td>
  				<td width=90 align=right>Cell Phone</td>
  				<td width=30><asp:textbox id="bpo_cphone" runat=server size=20 /></td>
  			
  				<td width=10>	<asp:RequiredFieldValidator runat="server" id="rfvcphone"
          					ControlToValidate="bpo_cphone" display="dynamic">
          					Required
      				</asp:RequiredFieldValidator>
      				<asp:regularexpressionvalidator 
         					id="revcPhone"
         					controltovalidate="bpo_cphone"
         					display="dynamic"
         					validationexpression="^[2-9]\d{2}-\d{3}-\d{4}$"
         					errormessage="Format xxx-xxx-xxxx"
         					runat="server"/>
      		</td>
				</tr>
				<tr>
				<td width=90 align=right>Fax</td>
  				<td width=30><asp:textbox id="bpo_fax" runat=server size=20 /></td>
  				<td width=10></td>	
  				<td width=90 align=right>Notificaiton Email</td>
  				<td width=30><asp:textbox id="bpo_email" runat=server size=30 /></td>
  				<td width=10></td>
  			</tr>
  			<tr>
  			
  				<td width=90 align=right> BPO Source</td>
  				<td width=30><asp:DropDownList ID="bpo_source" DataTextField="bposource"
				     DataValueField="tbl_bposources_pk" Runat="server" />	</td>
  				<td width=10></td>
  				<td width=90 align=right>Source BPO Number</td>
  				<td width=30><asp:textbox id="bpo_cusno" runat=server size=10 /></td>
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
    							<asp:ListItem Value="No" Text="No"/>
  	    						<asp:ListItem Value="Yes" Text="Yes"/>
  							</asp:DropDownList></td>
  				<td width=10></td>	
  				<td colspan=2>
  					<table><tr>
  					<td width=90 align=right>Need By Date</td>
  					<td width=30><asp:textbox id="txtDate" runat=server size=10 /></td>
      			<td>Time</td>
      			<td><asp:textbox id="bpotime" runat=server size=5 />	
      			</tr>
      			<tr>
      			<td align=right colspan=2><asp:RequiredFieldValidator runat="server" id="rfvDate"
          				ControlToValidate="txtDate" display="dynamic">
          					Required
      				</asp:RequiredFieldValidator>
      				<asp:CompareValidator runat="server" id="cvDate"
          				ControlToValidate="txtDate" display="dynamic"
          			Operator="DataTypeCheck" Type="Date">
          			Date must be in the format MM/DD/YYYY or MM-DD-YYYY.
      				</asp:CompareValidator></td>
      				</tr>
      			</table>
      		</td>
			</tr>
			<tr>	
  				<td width=90 align=right >Instructions</td>
  				<td colspan=6><asp:textbox id="bpo_instructions" runat=server size=12 TextMode="MultiLine" Columns="60"  Rows="5" /></td>
  			</tr>	
  			
		</table>
 </asp:panel>
 <asp:Panel id="pnlbpomore" visible=false runat="server">
  		<table cellpadding=2 cellspacing=2 border=0 width=100%>	
  			<tr height=20><td colspan=6></td></tr>
  			<tr>
  				<td  colspan=6 align="left"><font size=2><b>Customer/Bank Information</b></font></td>
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
  				<td  colspan=6 align="left"><font size=2><b>Property Information</b></font></td>
  			</tr>
  			<tr>	
  				<td width=90 align=right>Property Address</td>
  				<td width=30><asp:textbox id="bpo_cusaddress" runat=server size=30 /></td>
  				<td width=10></td>	
  				<td width=90 align=right>Occupancy Status</td>
  				<td width=30><asp:DropDownList id="bpo_occupancy" runat="server" >
    							<asp:ListItem Value="Unknown" Text="Unknown"/>
  	    						<asp:ListItem Value="Occupied" Text="Occupied"/>
    							<asp:ListItem Value="Vacant" Text="Vacant"/>
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
  					</tr></table>
  				</td>
  			</tr>
  	
  			<tr>
  				<td width=90 align=right>Home Owner Phone</td>
  				<td width=30><asp:textbox id="bpo_HomeOwnerphone" runat=server size=15 /></td>
  				<td width=10></td>	
  			</tr>
			<tr height=20><td colspan=6></td></tr>
  			<tr>
  				<td  colspan=6 align="left"><font size=2><b>Service Information</b></font></td>
  			</tr>
  			<tr>
  				<td width=90 align=right>Type</td>
  				<td width=30><asp:DropDownList id="bpo_type" runat="server" >
  	    						<asp:ListItem Value="regular" Text="Regular Drive By"/>
    							<asp:ListItem Value="interiror" Text="BPO Interior"/>
    							<asp:ListItem Value="Other" Text="Other- See Instructions"/>
  							</asp:DropDownList></td>
  				
  				<td colspan=3><table border=0><tr>
  				<td width=70 align=right>Photos</td>
  				<td width=30><asp:DropDownList id="bpo_photos" runat="server" >
    							<asp:ListItem Value="No" Text="No"/>
  	    						<asp:ListItem Value="Yes" Text="Yes"/>
  							</asp:DropDownList></td>
  				<td width=70 align=right># Interior</td>
  				<td width=30><asp:textbox id="bpo_nointerior" runat=server size=2 /></td>	
  				<td width=70 align=right># Exterior</td>
  				<td width=30><asp:textbox id="bpo_noexterior" runat=server size=2 /></td>
  				</tr></table></td>			
  							
  			</tr>
  			<tr>	
  				<td width=90 align=right>Listed</td>
  				<td width=30><asp:DropDownList id="bpo_Listed" runat="server" >
  	    						<asp:ListItem Value="Unknown" Text="Unknown"/>
  	    						<asp:ListItem Value="No" Text="No"/>
  								<asp:ListItem Value="Yes" Text="Yes"/>
    						</asp:DropDownList></td>
				<td width=10></td>	
  				<td width=90 align=right>Photo Vendor</td>
  				<td width=30><asp:DropDownList ID="bpo_photovendor" DataTextField="bpophotovendor"
					   DataValueField="tbl_photovendors_pk" Runat="server" />	</td>
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
  			<tr height=4>
  				<td></td>
  			</tr>
  		</table>		
 </asp:Panel> 
<asp:Panel id="pblbposavebtn" visible=false runat="server">	
 
	<table border=0> 
  			<tr>	
  				<td width=50 align=left calspan=4><asp:button id="bpo_bpodetail" runat=server text="More.." onclick="btn_bpodetails" CausesValidation="False" />
				<td width=50 align=left calspan=4><asp:button id="bpo_confirm" runat=server text="Submit" onclick="btn_confirmbpo" /></td>
  				<td width=50 align=left calspan=4><asp:button id="bpo_savelater" runat=server text="Save and Finish Later" onclick="btn_savelater" CausesValidation="False" />
  				<td width=50 align=left calspan=4><asp:button id="bpo_cancel" runat=server text="Cancel" onclick="btn_cancel" CausesValidation="False" />

 			</tr>
  	</table>
 </asp:Panel> 

<asp:Panel id="pblbpoconfirm" visible=false runat="server">	
	<table cellspacing=2 cellpadding=2>
		<tr>
			<td>Confirmation of Information</td>
		</tr>
		<tr>
			<td align=right>Requestor:</td>
			<td><asp:Literal id="lit_requestor" runat="server"></asp:Literal></td>
			<td align=right>Company</td>
  			<td><asp:Literal id="lit_company" runat="server"></asp:Literal></td>
				
		</tr>
		
		<tr>
				<td  align=right>Phone</td>
  				<td ><asp:Literal id="Lit_phone" runat="server"></asp:Literal></td>
  				<td align=right>Fax</td>
  				<td ><asp:Literal id="lit_fax" runat="server"></asp:Literal></td>
  		</tr>
	</table>
 </asp:Panel> 
	
 	<asp:Panel id="pnlack" visible=false runat="server">	
		<table>
			<tr>
				<td><b><font size=3>Request Submitted</font></b></td>
			</tr>
			<tr>
				<td>Thank You.  Your request has been submitted</td>
			</tr>	
		</table>
	<table> 
  			<tr>	
  				<td width=70 align=right calspan=4><asp:button id="bpo_continue" runat=server text="Continue" onclick="btn_continue" /></td>
  			</tr>
  	</table>
 </asp:Panel> 


	
</form>
	</body>
</HTML>
