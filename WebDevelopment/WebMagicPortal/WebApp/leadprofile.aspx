<%@ Page language="vb" Codebehind="leadprofile.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.leadprofile" Debug="false" trace="false" %>
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
	<form id="leadprofile" runat="server">
	<asp:Panel id="pnlprofileall" visible=false runat="server">
  		<table>
			<tr>
				<td><font size=2><b>Lead Profile</b></font></td>
				<td width=100></td>
				<td width=60 align=left><b>Profile #:</b></td>
  										<td ><b><u><asp:label id='lblprofileNo' runat=server /></u></b></td>
  										<td width=65 align=right><b>Lead #:</b></td>
 										<td ><b><u><asp:label id='lblleadno' runat=server /></u></b></td>
			</tr>
		</table>
		<table width=100%>
			<tr height=2><td colspan=10></td></tr>
			<tr ><td colspan=10><b><u>Property Details</u></b></td></tr>
			<tr>
				<td>Status</td>
				<td><asp:DropDownList id="dd_lp_status" runat="server" >
    											<asp:ListItem Value="Active" Text="Active"/>
  	    										<asp:ListItem Value="Inactive" Text="Inactive"/>
 											</asp:DropDownList> </td>
				<td>Credit</td>
				<td><asp:DropDownList id="dd_lp_credit" runat="server" DataTextField="x_descr"
					   							DataValueField="tbl_xwalk_pk" ></asp:DropDownList></td>
				
				<td>Move Date</td>
				<td><asp:textbox id="txt_lp_movedate" runat=server size=10 /></td>
				<td>Rent Minimum</td>
				<td><asp:textbox id="txt_lp_rentmin" runat=server size=10 /></td>
				<td>Rent Maximum</td>
				<td><asp:textbox id="txt_lp_rentmax" runat=server size=10 /></td>
				
				
			</tr>
		</table>
		<table width=100%>
			<tr height=2><td></td></tr>
			<tr>
				<td>Property Type</td>
				<td><asp:DropDownList id="dd_lp_proptype" runat="server" DataTextField="x_descr"
					   							DataValueField="tbl_xwalk_pk" ></asp:DropDownList></td>
				<td>Section 8</td>
				<td><asp:DropDownList id="dd_lp_sec8" runat="server" >
    											<asp:ListItem Value="No" Text="No"/>
  	    										<asp:ListItem Value="Yes" Text="Yes"/>
 											</asp:DropDownList></asp:DropDownList></td>
 				<td>Bedrooms</td>
				<td><asp:DropDownList id="dd_lp_bedrooms" runat="server" DataTextField="x_descr"
					   							DataValueField="tbl_xwalk_pk" ></asp:DropDownList></td>
				<td>Baths</td>
				<td><asp:DropDownList id="dd_lp_baths" runat="server" DataTextField="x_descr"
					   							DataValueField="tbl_xwalk_pk" ></asp:DropDownList></td>
				<td>Levels</td>
				<td><asp:DropDownList id="dd_lp_levels" runat="server" DataTextField="x_descr"
					   							DataValueField="tbl_xwalk_pk" ></asp:DropDownList></td>
			</tr>
			<tr>
				<td>Basement</td>
				<td><asp:DropDownList id="dd_lp_basement" runat="server" DataTextField="x_descr"
					   							DataValueField="tbl_xwalk_pk" ></asp:DropDownList></td>
				<td>Pets</td>
				<td><asp:DropDownList id="dd_lp_pets" runat="server" DataTextField="x_descr"
					   							DataValueField="tbl_xwalk_pk" ></asp:DropDownList></td>
				<td>Fence</td>
				<td><asp:DropDownList id="dd_lp_fence" runat="server" DataTextField="x_descr"
					   							DataValueField="tbl_xwalk_pk" ></asp:DropDownList></td>
			</tr>
		</table>
	
	
	
		<table width=100%>
		
			<tr>
				<td>County</td>
				<td><asp:DropDownList id="dd_lp_county" runat="server" DataTextField="cnty_name"
										AutoPostBack="True" OnSelectedIndexChanged="cntychange"
					   							DataValueField="tbl_county_pk" ></asp:DropDownList>
				
				</td>
				<td>School District 1</td>
				<td><asp:DropDownList id="dd_lp_schooldist" runat="server" DataTextField="agency_name"
					   							DataValueField="agency_name" ></asp:DropDownList>
				
				</td>
			</tr>	
			<tr>
				<td></td>
				<td></td>
				<td>School District 2</td>
				<td><asp:DropDownList id="dd_lp_schooldist2" runat="server" DataTextField="agency_name"
					   							DataValueField="agency_name" ></asp:DropDownList>
				
				</td>
		</table>
		<table>
			<tr>
				<td><u>Cities</u>&nbsp&nbsp<asp:linkbutton id="ctyedit" Text= "Edit" 
                     runat="server" Font-Bold="True" Font-underline="false" Style="font-family:arial; font-size:10pt; cursor:hand"
                    onClick="btn_cityedit" /></td>
			</tr>
		</table><table>	
			<tr>
				<td>
				</td>
				
			</tr>
		
		</table>
		<table width=80% border =0>
			
			<tr>
		<asp:Panel id="pnlallcities" visible=false runat="server">
  				
				<td valign=top><table>
						<tr>
							<td>All</td>
						</tr>
						<tr>
							<td>
								<asp:listbox id="lstAllcities" runat="server"  selectionmode="multiple" 
              				 DataTextField="cty_city_name" DataValueField="cty_idx_cid" />
				         </td>
				      </tr>
				    </table>
				 
				</td>
				<td valign=top width=100 align=center>
					<table width=100%>
						<tr height=25><td></td></tr>
						<tr>
							<td align=center><asp:linkbutton id="ctyadd" Text= "Add >>" 
                     runat="server" Font-Bold="True" Font-underline="false" Style="font-family:arial; font-size:8pt; cursor:hand"
                    onClick="btn_cityadd" />
                   	</td>
                   </tr><tr height=10><td></td></tr>
                   <tr>
							<td align=center><asp:linkbutton id="ctyremove" Text= "<< Remove" 
                     runat="server" Font-Bold="True" Font-underline="false" Style="font-family:arial; font-size:8pt; cursor:hand"
                    onClick="btn_cityremove" />
                   	</td>
                   </tr>
               </table>
				</td>
		
		</asp:panel>	
			
			
				
				<td valign=top>
					<table>
						<tr>
							<td>Selected</td>
						</tr>
						<tr>
							<td>
								 <asp:listbox id="lstcities" runat="server"  selectionmode="multiple" 
				               DataTextField="lpc_city_name" DataValueField="tbl_leadprofilecities_pk" />	
				         </td>
				      </tr>
				    </table>
				</td>
				<td valign=top>Notes</td>
				<td><asp:textbox id="txt_lp_notes" runat=server size=16 TextMode="MultiLine" Columns="50"  Rows="10" /></td>
				</td>						
				
			</tr>
		</table>
		
		<table>
			<tr>
				<td ><asp:button id="bt_save" runat=server text="Save" width="70" onclick="btn_saveprofile" CausesValidation="False"  Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:70px; cursor:hand" /></td>
				<td ><asp:button id="bt_savemore"  runat=server text="Save & Add" width="70" onclick="btn_saveprofileadd" CausesValidation="False"  Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:70px; cursor:hand" /></td>
				<td ><asp:button id="bt_exit"  runat=server text="Exit" width="70" onclick="btn_exit" CausesValidation="False"  Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:70px; cursor:hand" /></td>
				<td ><asp:button id="bt_match"  runat=server text="Find Properties" width="70" onclick="btn_match" CausesValidation="False"  Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:70px; cursor:hand" /></td>
			</tr>
		</table>	 
	</asp:panel>	
	<asp:Panel id="pnlmatch" visible=false runat="server">
		<table width=60%>
			<tr>
				<td colspan=32>Search Parameters</td>
			</tr>
			<tr>
				<td width=30></td>
				<td align=right>City</td>
				<td><asp:checkbox id="chkcity" runat=server /></td>
				<td align=right>Credit</td>
				<td ><asp:checkbox id="chkcredit" runat=server /></td>
				<td align=right>Move Date</td>
				<td><asp:checkbox id="chkmovedt" runat=server /></td>
				<td align=right>Rent</td>
				<td><asp:checkbox id="chkrent" runat=server /></td>
				<td align=right>Bedroom</td>
				<td><asp:checkbox id="chkbed" runat=server /></td>
				
			</tr>
			<tr>	
				<td width=30></td>
				<td align=right>Bath</td>
				<td><asp:checkbox id="chkbath" runat=server /></td>
				<td align=right>Levels</td>
				<td><asp:checkbox id="chklevel" runat=server /></td>
				<td align=right>Basement</td>
				<td><asp:checkbox id="chkbase" runat=server /></td>
				<td align=right>Fence</td>
				<td><asp:checkbox id="chkfence" runat=server /></td>
				<td align=right>Pets</td>
				<td><asp:checkbox id="chkpets" runat=server /></td>
				<td align=right>School District</td>
				<td><asp:checkbox id="chkschool" runat=server /></td>
				
			</tr>
		</table>		
		<table>
			<tr>
				<td ><asp:button id="bt_search"  runat=server text="Go" width="70" onclick="btn_Search" CausesValidation="False"  Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:70px; cursor:hand" /></td>
				<td ><asp:button id="bt_exitmatch"  runat=server text="Exit" width="70" onclick="btn_exit" CausesValidation="False"  Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:70px; cursor:hand" /></td>
			</tr>
		</table>	
		<table border=0 width=90%>
  			<tr>
  				<td >	<div style="vertical-align top; height: 150px; overflow:auto;">
			  			<asp:DataGrid 
							ID="lstproperties" 
							AutoGenerateColumns=False
							Width="100%"
			            ColumnHeadersVisible = FALSE  
							ItemStyle-BackColor=white
							ItemStyle-Font-Name="arial"
							ItemStyle-Font-Size="24px"
							BorderColor="#ffffff"
							AllowPaging="false"  Runat=server>
						
							<HeaderStyle Font-Size="24px" Font-Bold="True" BackColor="lightgray"></HeaderStyle>
					 
								 <Columns >
							
								<asp:BoundColumn HeaderText="Lead#"  DataField="tbl_leads_fk"   />
								<asp:BoundColumn HeaderText="Name"  DataField="ld_lname"   />
								
								<asp:BoundColumn HeaderText="Address"  DataField="lp_address"   />
								<asp:BoundColumn HeaderText="City"  DataField="lp_city"   />
								<asp:BoundColumn HeaderText="Zip"  DataField="lp_zip"   />
								<asp:BoundColumn HeaderText="Bed"  DataField="lp_num_bed"   />
								<asp:BoundColumn HeaderText="Bath"  DataField="lp_num_bath"   />
							
											</Columns>
											</asp:DataGrid>
										</div>
							</td>
						</tr>
		
		</table>	

	</asp:panel>					
	</form>
	</body>	
</HTML>
