<%@ Page language="vb" Codebehind="referals.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.referals" Debug="false" trace="false" %>
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
				<td ><font size="3pt"><b>Referals</b></font></td>
			</tr>
			<tr>
				<td colspan=2><font size=2><i>Search Fields- Last & First Name, Home & Cell Phone, Email</i></font></td>
					
			</tr>
		</table>
		<table width=60%>
			<tr>
				<td width=190 colspan=2><b><u>Search</u></b> </td>
				<td colspan=3><b><u>Filters</u></b> </td>
				
			</tr>
			
			<tr>
				<td><asp:textbox id="l_search" runat=server size=20 /></td>
				<td><asp:Button  id="btn_search" OnClick="btnsearch" Text="Search" runat="server" /></td>
				<td width=0 align=left></td>
				<td align="right">
					<asp:Label ID="lblleadtype" Text="Lead Type: " Runat="server" />
					<asp:DropDownList ID="ddlleadtypeFilter"
                  AutoPostBack="True"
                  OnSelectedIndexChanged="ChangetypeFilter"
                  DataValueField="x_descr" 
                  Runat="server" />
            </td>
				<td width=0 align=left></td>
				<td align="right">
					<asp:Label ID="lblstatus" Text="Status: " Runat="server" />
					<asp:DropDownList ID="ddlstatusFilter"
                  AutoPostBack="True"
                  OnSelectedIndexChanged="ChangeFilter"
                  DataValueField="bpostatus" 
                  Runat="server" />
            </td>
          </tr>
      </table>
		<table>
			<tr>
				<td>
					                	
      <asp:DataGrid 
				ID="lead_status" 
            AutoGenerateColumns=False
            Width="100%"
   
            ColumnHeadersVisible = FALSE  
            ItemStyle-BackColor=white
            ItemStyle-Font-Name="arial"
            ItemStyle-Font-Size="24px"
            BorderColor="#ffffff"
            AllowPaging="True" 
            PageSize="10" 
            PagerStyle-Mode="NumericPages"  
				PagerStyle-HorizontalAlign="Right" 
				PagerStyle-NextPageText="Next" PagerStyle-PrevPageText="Prev"
				OnPageIndexChanged="MyDataGrid_Page" 

            Runat=server>

            <HeaderStyle Font-Size="24px" Font-Bold="True" BackColor="lightgray"></HeaderStyle>
            
            <Columns >
           		<asp:hyperlinkcolumn runat="server" datatextfield ="tbl_leads_pk" headertext="Lead #"
            		DataNavigateUrlField ="tbl_leads_pk"
            		DataNavigateUrlFormatString="addlead.aspx?action=view&id={0}"  ItemStyle-HorizontalAlign=center/>
        		
        			<asp:BoundColumn HeaderText="Refered to"  DataField="refer_company" ItemStyle-Width="160px"    />
        		
      			
		     </Columns>
		</asp:DataGrid>
		</td>
	</tr>
	</table>
	<table><tr>
				<td><asp:Button  id="btnAddlead" OnClick="newlead" Text="Add Lead" runat="server" /></td>
				<td><asp:Button  id="btnrefresh" OnClick="refresh" Text="Refresh" runat="server" /></td>
				<td><asp:Button  id="btnreferals" OnClick="referals" Text="Referals" runat="server" /></td>
			</tr>
	</table>
	</form>
	</body>	
</HTML>
