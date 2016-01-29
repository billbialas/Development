<%@ Page language="vb" Codebehind="leadprofileSell.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.leadprofileSell" Debug="false" trace="false" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<HTML>
	<HEAD>
		<title>Choice One Realty- Michigan</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	
<body >
	<form id="leadprofile" runat="server">
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
 												<asp:ListItem Value="Rented" Text="Rented"/>
 											</asp:DropDownList> </td>
				<td>Address</td>
				<td><asp:textbox id="txt_lp_address" runat=server size=20 />
				<td>City</td>
				<td><asp:textbox id="txt_lp_city" runat=server size=15	 />
				<td>State</td>
				<td><asp:DropDownList id="dd_lp_state" runat="server" >
    											<asp:ListItem Value="Michigan" Text="Michigan"/>
  	    										<asp:ListItem Value="Ohio" Text="Ohio"/>
 												<asp:ListItem Value="Indiana" Text="Indiana"/>
 											</asp:DropDownList>
				<td>Zip</td>
				<td><asp:textbox id="txt_lp_zip" runat=server size=8 />
				<td>County</td>
				<td><asp:DropDownList id="dd_lp_county" runat="server" DataTextField="cnty_name"
										AutoPostBack="True" OnSelectedIndexChanged="cntychange"
					   							DataValueField="tbl_county_pk" ></asp:DropDownList></td>
			</tr>
		</table>
		<table width=90% border=0>
			<tr height=2><td></td></tr>
			<tr>
				<td width=100 align=left>Available Date</td>
				<td><asp:textbox id="txt_lp_availdate" runat=server size=10 />
				<td width=100 align=left>Property Type</td>
				<td><asp:DropDownList id="dd_lp_proptype" runat="server" DataTextField="x_descr"
					   							DataValueField="tbl_xwalk_pk" ></asp:DropDownList></td>
				<td width=70 align=left>Levels</td>
				<td><asp:DropDownList id="dd_lp_levels" runat="server" DataTextField="x_descr"
					   							DataValueField="tbl_xwalk_pk" ></asp:DropDownList></td>
				<td width=70 align=left>Bedrooms</td>
				<td><asp:DropDownList id="dd_lp_bedrooms" runat="server" >
    											<asp:ListItem Value="1" Text="1"/>
  	    										<asp:ListItem Value="2" Text="2"/>
 												<asp:ListItem Value="3" Text="3"/>
 												<asp:ListItem Value="4" Text="4"/>
 												<asp:ListItem Value="5+" Text="5+"/>
 											</asp:DropDownList></td>
				<td width=70 align=left>Baths</td>
				<td><asp:DropDownList id="dd_lp_baths" runat="server" >
    											<asp:ListItem Value="1" Text="1"/>
    											<asp:ListItem Value="1.5" Text="1.5"/>
    											<asp:ListItem Value="2" Text="2"/>
    											<asp:ListItem Value="2.5" Text="2.5"/>
    											<asp:ListItem Value="3" Text="3"/>
    											
  	    										<asp:ListItem Value="3.5" Text="3.5"/>
 												<asp:ListItem Value="4" Text="4"/>
 												<asp:ListItem Value="4.5" Text="4.5"/>
 												<asp:ListItem Value="5+" Text="5+"/>
 											</asp:DropDownList></td>
 											</asp:DropDownList></td>
				<td width=70 align=left>Condition</td>
				<td><asp:DropDownList id="dd_lp_Condition" runat="server" >
    											<asp:ListItem Value="Updated" Text="Updated"/>
    											<asp:ListItem Value="Dated" Text="Dated" />
 											</asp:DropDownList></td>
				
			</tr>
		
			<tr>
				<td width=100 align=left>Basement</td>
				<td><asp:DropDownList id="dd_lp_basement" runat="server" >
    											<asp:ListItem Value="No" Text="No"/>
  	    										<asp:ListItem Value="Yes" Text="Yes"/>
 											</asp:DropDownList></td>
				<td width=100 align=left>Garage</td>
				<td><asp:DropDownList id="dd_lp_garage" runat="server" >
    											<asp:ListItem Value="0" Text="0"/>
  	    										<asp:ListItem Value="1" Text="1"/>
  	    										<asp:ListItem Value="1.5" Text="1.5"/>
  	    										<asp:ListItem Value="2" Text="2"/>
  	    										<asp:ListItem Value="2.5" Text="2.5"/>
  	    										
 											</asp:DropDownList></td>
				
				<td align=left>Pets</td>
				<td><asp:DropDownList id="dd_lp_pets" runat="server" >
    											<asp:ListItem Value="No" Text="No"/>
  	    										<asp:ListItem Value="Yes" Text="Yes"/>
 											</asp:DropDownList></asp:DropDownList></td>
				<td align=left>Fence</td>
				<td><asp:DropDownList id="dd_lp_fence" runat="server" >
    											<asp:ListItem Value="No" Text="No"/>
  	    										<asp:ListItem Value="Yes" Text="Yes"/>
 											</asp:DropDownList></asp:DropDownList></td>
 			</tr>
		</table>
		<table width=100% border=0>
				<tr>
				<td width =150 align=left>School District</td>
				<td align=left><asp:DropDownList id="dd_lp_schooldist" runat="server" DataTextField="agency_name"
					 	  							DataValueField="agency_name" ></asp:DropDownList></td>
				<td width=60%></td></tr>
			</table>
		<table width=100%>
			<tr height=2><td></td></tr>
			<tr>
				<td colspan=6><b><u>Financial Information</u></b></td>
			</tr>
			<tr>
				<td>Rent Minimum</td>
				<td><asp:textbox id="txt_lp_rentmin" runat=server size=10 /></td>
				<td>Rent Maximum</td>
				<td><asp:textbox id="txt_lp_rentmax" runat=server size=10 /></td>
				<td>Security Dep</td>				
 				<td><asp:DropDownList id="dd_lp_secdeposit" runat="server" >
    											<asp:ListItem Value="1 Month" Text="1 Month"/>
  	    										<asp:ListItem Value="1.5 Month" Text="1.5 Month"/>
  	    										<asp:ListItem Value="Other" Text="Other"/>
  	    										</asp:DropDownList></asp:DropDownList></td>
  	    										
				<td>Section 8</td>
				<td><asp:DropDownList id="dd_lp_sec8" runat="server" >
    											<asp:ListItem Value="No" Text="No"/>
  	    										<asp:ListItem Value="Yes" Text="Yes"/>
 											</asp:DropDownList></asp:DropDownList></td>
 				<td>Work with Realtor</td>
 				<td><asp:DropDownList id="dd_lp_workrealtor" runat="server" >
    											<asp:ListItem Value="Yes" Text="Yes"/>
  	    										<asp:ListItem Value="Maybe" Text="Maybe"/>
  	    										<asp:ListItem Value="Not Now" Text="Not Now"/>
  	    										<asp:ListItem Value="Never" Text="Never"/>
  	    										</asp:DropDownList></asp:DropDownList></td>
			</tr>
		</table>
		<table width=100%>
			<tr>	
	
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
			</tr>
		</table>	 					
	</form>
	</body>	
</HTML>
