<%@ Page language="vb" Codebehind="sysusermaint.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.sysusermaint" Debug="false" trace="false" %>
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
	<form id="sysusermaint" runat="server">
		<table>
			<tr>
				<td ><font size="3pt"><b>User Maint</b></font></td>
			</tr>
		</table><br>
		<table width=93%>
			<tr>
				<td width=0 align=left></td>
				<td align="left">
					<asp:Label ID="lblstatus" Text="Filter by Type: " Runat="server" />
					<asp:DropDownList ID="ddlstatusFilter"
                  AutoPostBack="True"
                  OnSelectedIndexChanged="ChangeFilter"
                  DataValueField="type" 
                  Runat="server" />
            </td>
          </tr>
      </table>
		<table width=100%>
			<tr>
				<td>
					                	
      <asp:DataGrid 
				ID="users" 
            AutoGenerateColumns=False
            Width="100%"
   
            ColumnHeadersVisible = FALSE  
            ItemStyle-BackColor=white
            ItemStyle-Font-Name="arial"
            ItemStyle-Font-Size="24px"
            BorderColor="#ffffff"
            Runat=server>

            <HeaderStyle Font-Size="24px" Font-Bold="True" BackColor="LightSalmon"></HeaderStyle>
            
            <Columns >
           		<asp:hyperlinkcolumn runat="server" datatextfield ="users_tbl_pk" headertext="User ID"
            		DataNavigateUrlField ="users_tbl_pk"
            		DataNavigateUrlFormatString="viewusers.aspx?id={0}"  ItemStyle-HorizontalAlign=center/>
        			
        			<asp:BoundColumn HeaderText="User ID"  DataField="UID" ItemStyle-Width="150px"    />
        			<asp:BoundColumn HeaderText="Last Name"  DataField="lname" ItemStyle-Width="150px"    />
        			<asp:BoundColumn HeaderText="First Name"  DataField="fname" ItemStyle-Width="510px"    />
        			<asp:BoundColumn HeaderText="License No"  DataField="licenseno" ItemStyle-Width="150px"    />
       
        			<asp:BoundColumn HeaderText="Type"  DataField="Type" ItemStyle-Width="200px"    />
        			
		     </Columns>
		</asp:DataGrid>
		</td>
	</tr>
	</table>
	<table><tr>
				<td><asp:Button  id="btnAddNewuser" OnClick="newuser" Text="Add New User" runat="server" /></td>
			</tr>
	</table>
	</form>
	</body>	
</HTML>
