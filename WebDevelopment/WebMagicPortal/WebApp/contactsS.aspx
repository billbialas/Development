<%@ Page language="vb" Codebehind="leads.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.contactsS" Debug="false" trace="false" %>
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
	<form id="bpo" runat="server">
		<table>
			<tr>
				<td ><font size="3pt"><b>Contacts</b></font></td>
			</tr>
		</table><br>
		<table width=93%>
			<tr>
				<td width=0 align=left></td>
		      </td>
          </tr>
      </table>
		<table>
			<tr>
				<td>
					                	
      <asp:DataGrid 
				ID="contact_status" 
            AutoGenerateColumns=False
            Width="100%"
   
            ColumnHeadersVisible = FALSE  
            ItemStyle-BackColor=white
            ItemStyle-Font-Name="arial"
            ItemStyle-Font-Size="24px"
            BorderColor="#ffffff"
            Runat=server>

            <HeaderStyle Font-Size="24px" Font-Bold="True" BackColor="lightgray"></HeaderStyle>
            
            <Columns >
           		<asp:hyperlinkcolumn runat="server" datatextfield ="tbl_contact_pk" headertext="Contact #"
            		DataNavigateUrlField ="tbl_contact_pk"
            		DataNavigateUrlFormatString="viewcontact.aspx?id={0}"  ItemStyle-HorizontalAlign=center/>
        			
        			<asp:BoundColumn HeaderText="Last Name"  DataField="cn_lname" ItemStyle-Width="50px"    />
        			<asp:BoundColumn HeaderText="First Name"  DataField="cn_fname" ItemStyle-Width="50px"    />
        			<asp:BoundColumn HeaderText="Phone #"  DataField="cn_hphone" ItemStyle-Width="150px"    />
        			<asp:BoundColumn HeaderText="Address"  DataField="cn_haddress" ItemStyle-Width="150px"    />
        			<asp:BoundColumn HeaderText="city"  DataField="cn_hcity" ItemStyle-Width="150px"    />
        	
		     </Columns>
		</asp:DataGrid>
		</td>
	</tr>
	</table>
	<table><tr>
				<td><asp:Button  id="btnAddcontact" OnClick="newContact" Text="Add Contact" runat="server" /></td>
			</tr>
	</table>
	</form>
	</body>	
</HTML>
