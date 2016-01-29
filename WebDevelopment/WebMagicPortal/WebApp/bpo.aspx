<%@ Page language="vb" Codebehind="bpo.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.bpo" Debug="false" trace="false" %>
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
				<td ><font size="3pt"><b>Work With BPOS</b></font></td>
			</tr>
		</table><br>
		<table width=93%>
			<tr>
				<td width=0 align=left></td>
				<td align="right">
					<asp:Label ID="lblstatus" Text="Filter by Status: " Runat="server" />
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
				ID="BPO_Status" 
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
           		<asp:hyperlinkcolumn runat="server" datatextfield ="tbl_bpo_pk" headertext="C1 BPO#"
            		DataNavigateUrlField ="tbl_bpo_pk"
            		DataNavigateUrlFormatString="viewbpo.aspx?id={0}"  ItemStyle-HorizontalAlign=center/>
        			
        			<asp:BoundColumn HeaderText="Customer BPO#"  DataField="cusorderno" ItemStyle-Width="50px"    />
        			<asp:TemplateColumn HeaderText="Status" ItemStyle-Width="140px">
                  	<ItemTemplate>
                     	<asp:Label id='Stat'
                        	Runat="server" 
										Text='<%# DataBinder.Eval(Container.DataItem, "status") %>' />
							</ItemTemplate>                               
					</asp:TemplateColumn>
				
        			<asp:BoundColumn HeaderText="Requestor"  DataField="requester" ItemStyle-Width="100px"    />
        			<asp:BoundColumn HeaderText="Requestor Phone" DataField="reqphone"  ItemStyle-Width="80px"    />
        			<asp:BoundColumn HeaderText="Property City" DataField="propcity"  ItemStyle-Width="80px"    />
         		<asp:BoundColumn HeaderText="Assigned Staff" DataField="assignedtoname"  ItemStyle-Width="80px"    />
         		
         									

		     </Columns>
		</asp:DataGrid>
		</td>
	</tr>
	</table>
	<table>
			<tr>
				<td><asp:Button  id="btnAddNewBPO" OnClick="newbpo" Text="Request BPO" runat="server" /></td>
				<td><asp:Button  id="btnrefresh" OnClick="refreshbpo" Text="Refresh" runat="server" /></td>
			
			</tr>
	</table>
	</form>
	</body>	
</HTML>
