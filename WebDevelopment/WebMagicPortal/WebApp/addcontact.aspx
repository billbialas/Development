<%@ Page language="vb" Codebehind="addcontact.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.addcontact" Debug="false" trace="false"  %>
<%@ Register TagPrefix="mbcbb" Namespace="MetaBuilders.WebControls" Assembly="MetaBuilders.WebControls" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script language="JavaScript" src="../_include/default.js"></script>

<HTML>
	<HEAD>
		<title>Choice One Realty- Michigan</title>
	</HEAD>
	
	<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">
		<form  name="addlead"  runat="server" >
		<asp:Panel id="pnlnew" runat="server">	
		<table width=100%><tr><td width=80%>
			
  			
			<table cellpadding=3 cellspacing=3 border=0 width=100%>
  			
  			<tr>
  				<td  colspan=4 align="left"><font size=2><b>Contact Information</b></font>
  				</td>
  			</tr>
  			</table>
			<table cellpadding=3 cellspacing=3 border=0 width=100%>
  			<tr>
  				<td width=90 align=right>Status</td>
  				<td><asp:DropDownList id="dd_status" runat="server" >
    							<asp:ListItem Value="Active" Text="Unassigned"/>
  	    						<asp:ListItem Value="Closed" Text="Assigned"/>
 						</asp:DropDownList>
  				</td>
  				<td width=90 align=right>Capture Date</td>
  			</tr>
			<tr>
				<td width=90 align=right>First Name</td>
  				<td colspan=1><asp:textbox id="l_fname" runat=server size=25 /></td>
  				<td width=90 align=right>Last Name</td>
  				<td colspan=2><asp:textbox id="l_lname" runat=server size=25 /></td>
			</tr>
			<tr>
  				<td width=90 align=right>Home Phone</td>
  				<td colspan=1><asp:textbox id="l_hphone" runat=server size=25 /></td>
  				<td width=90 align=right>Cell Phone</td>
  				<td colspan=2><asp:textbox id="l_cphone" runat=server size=25 /></td>
			</tr>
			<tr>
  				<td width=90 align=right>Email</td>
  				<td colspan=4><asp:textbox id="l_email" runat=server size=50 /></td>
  		</tr>
  		</table>
  		<table width=100%>
			<tr>
  				<td width=100 align=right>Appointment Date</td>
  				<td ><asp:textbox id="l_appdate" runat=server size=10 /></td>
  				<td width=100 align=right>Appointment Time</td>
  				<td ><asp:textbox id="l_appttime" runat=server size=10 /></td>
				<td width=120 align=right>Appointment Location</td>
  				<td><asp:DropDownList id="dd_apptloc"  runat="server" >
    							<asp:ListItem Value="NA" Text="NA"/>
  	    						<asp:ListItem Value="Sterling Office" Text="Sterling Office"/>
  	    						<asp:ListItem Value="TBD" Text="TBD"/>
  	   				</asp:DropDownList>
  				</td>
  			</tr>
  			
		</table>
		</td>
		<td valign=top>
			<table width=100%>
				<tr>
					<td colspan=2><b><u>Confirmed Referral</u></b></td>
				</tr>
				<tr>
					<td colspan=3><asp:checkboxlist id="referal" runat="server">
  							<asp:listitem id="option1" runat="server" value="Credit Restoration" />
	  						<asp:listitem id="option2" runat="server" value="Mortgage" />
  							<asp:listitem id="option3" runat="server" value="Other" /></asp:checkboxlist></td>
				</tr>
				<tr>
					<td width=15></td>
					<td><asp:textbox id="l_referalother" runat=server size=10 /></td>
			</table>
		
		
		</td>
		</tr>
		</table>
		<table cellpadding=1 cellspacing=1 border=0  width=75%>
  			<tr>
  				<td  colspan=8 align="left"><font size=2><b>Mailing Address</b></font>
  				</td>
  			</tr>
  			<tr>
  				<td colspan=7><table width=60%><tr>
  				<td width=150 align=right>Mail to this address?</td>
  				<td colspan=1><asp:checkbox id="l_mailto" runat=server size=30 /></td>
  				<td width=150 align=right>Is this the property to list?</td>
  				<td colspan=3><asp:checkbox id="l_addresstolist" runat=server size=30 /></td></tr></table>
  				</td>
  			</tr>
  			<tr>
  				<td width=90 align=right>Address</td>
  				<td><asp:textbox id="l_address" runat=server size=30 /></td>
  				<td width=80 align=right>City</td>
  				<td ><asp:textbox id="l_city" runat=server size=20 /></td>
				<td width=60 align=right>State</td>
  				<td><asp:textbox id="l_state" runat=server size=1 /></td>
				<td width=90 align=right>Zip</td>
  				<td><asp:textbox id="l_zip" runat=server size=6 /></td>
			
			</tr>
		</table>
		
		<asp:Panel id="pnlinitialnotes" runat="server">	
		
		<table width=100%>
			<tr>
				<td width=250 valign=top>
					<table cellpadding=2 cellspacing=2 border=0  width=100%>
  						<tr>
  							<td><b>Initial Contact Notes:</b></td>
  						</tr>
  						<tr>
							<td><asp:textbox id="l_notes" runat=server size=16 TextMode="MultiLine" Columns="40"  Rows="10" /></td>
						</tr>
					</table>	
				</td>
				<td valign=top>
						<asp:Panel id="pnltenant" visible=false runat="server">	
			
					<table border=0  width=100% cellspacing=2 cellpadding=2>
  						<tr>
  							<td><b><u>Questions:</u></b></td>
  						</tr>
  						<tr>
  							<td>1. How many bedrooms do you NEED?</td>
  						</tr>
  						<tr>
  							<td>2. When do you need to move in ?</td>
  						</tr>
  						<tr>
  							<td>3. Do you have a security deposit of any kind _$1000 or more?</td>
  						</tr>
  						<tr>
  							<td>4. Do you have an income or documented means to pay rent?</td>
  						</tr>
  						<tr>
  							<td>5.Do you have ok credit, bad credit or not sure what your credit is?</td>
  						</tr>
  						<tr>
  							<td>**If credit BAD ..Are you working with someone to restore your credit yet?</td>
  						</tr>
  						
  						<tr>
  							<td>6. Lastly, how much do you want your monthly payment to be under $?</td>
  						</tr>
  						<tr>
  							<td>7. What cities or school districts? </td>
  						</tr>
  						<tr>
  							<td>8. Do you have PETS?</td>
  						</tr>
  						<tr>
  							<td>9. Do you need single story or can you do multi-level home (stairs any special needs or limitations desired or physical)</td>
  						</tr>
  						
  						
  					</table></asp:panel>
  					
	<asp:Panel id="pnlviewnotes" runat="server">	
		<table>
			<tr>
				<td><b>Contact History:</b></td>
			</tr>
		</table>
		<table border=1 width=100%>
			
			<tr>
			<td>
			             	
      <asp:DataGrid 
				ID="lead_history" 
            AutoGenerateColumns=False
            Width="100%"
   
            ColumnHeadersVisible = FALSE  
            ItemStyle-BackColor=white
            ItemStyle-Font-Name="arial"
            ItemStyle-Font-Size="24px"
            BorderColor="#ffffff"
            AllowPaging="True" 
            PageSize="4" 
            PagerStyle-Mode="NumericPages"  
				PagerStyle-HorizontalAlign="Right" 
				PagerStyle-NextPageText="Next" PagerStyle-PrevPageText="Prev"
				OnPageIndexChanged="MyDataGrid_Page" 

            Runat=server>

            <HeaderStyle Font-Size="24px" Font-Bold="True" BackColor="lightgray"></HeaderStyle>
            
            <Columns >
          		<asp:BoundColumn HeaderText="Date"  DataField="date" ItemStyle-Width="100px"    />
        	 		<asp:BoundColumn HeaderText="Notes"  DataField="cnt_notes" ItemStyle-Width="150px"    />
        			<asp:BoundColumn HeaderText="Agent"  DataField="cnt_agentname" ItemStyle-Width="150px"    />
        			
		     </Columns>
		</asp:DataGrid>
		</td>
		</table>	
		<table>
			<tr>
				<td><asp:button id="l_addcontact" runat=server text="Add Encounter" onclick="btn_addcontact" CausesValidation="False" Style="background-color:steelblue; color:#FFFFFF; font-family:arial;
  					font-size:10pt;	width:100px; cursor:hand" /></td>
			</tr>
		</table>
  		</asp:panel>	
  					
  	</asp:panel>
	
	<asp:Panel id="pnladdencoutner" runat="server">	
		<table>
			<tr>
				<td><b>Add Contact:</b></td>
			</tr>
		</table>
		<table valign=top>
			<tr>
  				<td width=50 align=right valign=top>Date</td>
  				<td valign=top><asp:textbox id="l_edate" runat=server size=8 /></td>
  				<td valign=top>Notes</td>
  				<td valign=top><asp:textbox id="l_enotes" runat=server size=16 TextMode="MultiLine" Columns="30"  Rows="10" /></td>
  			</tr>
		</table>
	 <table width=20% border=0>
  				<tr>	
  					<td align=right ><asp:button id="l_savecontact" runat=server text="Save" width="100" onclick="btn_savecontact" CausesValidation="False" /></td>		
  				</tr>
  		</table>
	
	</asp:panel>
					</td>
  			</tr>
  		</table>
  				
		 <table width=20% border=0>
  				<tr>	
  					<td align=left ><asp:button id="l_accept" runat=server text="Accept Lead" width="100" onclick="btn_acceptlead" CausesValidation="False" /></td>		
  					<td align=left ><asp:button id="l_save" runat=server text="Save & Submit" width="100" onclick="btn_save" CausesValidation="False" /></td>		
  					<td align=left ><asp:button id="l_savedraft" runat=server text="Save Draft" width="70" onclick="btn_savedraft" CausesValidation="False" /></td>
 					<td align=left ><asp:button id="l_cancel" runat=server text="Cancel" width="70" onclick="btn_cancel" CausesValidation="False" /></td>
 					<td align=left ><asp:button id="l_close" runat=server text="Close" width="70" onclick="btn_close" CausesValidation="False" /></td>
 					<td align=left ><asp:button id="l_delete" runat=server text="Delete" width="70" onclick="btn_delete" CausesValidation="False" /></td>
 				
 				</tr>
 			</table>
		</asp:panel>
		<asp:Panel id="pnlack" runat="server">	
		 <table width=80% border=0>
  				<tr>	
  					<td align=left >Your request has been completed.</td>	
  				</tr>
  				<tr>		
  					<td align=left ><asp:button id="l_continue" runat=server text="Continue" width="70" onclick="btn_continue" CausesValidation="False" /></td>
 				</tr>
 			</table>
	
		</asp:panel>
		</form>
	</body>
</HTML>
	