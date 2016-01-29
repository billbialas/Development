<%@ Page language="vb" Codebehind="usermaintSYS.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.usermaintSYS" Debug="false" trace="false" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script language="JavaScript" src="../_include/default.js"></script>
<script type="text/javascript">
<!--
     function confirmDelete() {
         return window.confirm("are you sure?");
     }

     -->
    </script>
<HTML>
	<HEAD>
		<title>Choice One BMS</title>
	</HEAD>
	
<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">
	<form id="forms1a" runat="server">
		<table>
			<tr>
				<td><font size=2><b>User Maintenance</b></font></td>
			</tr>
		</table>
		<table>
			<tr>
				<td>
					<asp:DataGrid Runat=server
					ID="users" 
	            AutoGenerateColumns=False
	            Width="100%"          
	            ItemStyle-BackColor=white
	            ItemStyle-Font-Name="arial"
	            ItemStyle-Font-Size="12px"
	            BorderColor="#000000"
	           
					PagerStyle-Visible = "False"	
					HeaderStyle-BackColor="steelblue"
					HeaderStyle-ForeColor="White"
					 OnItemDataBound="ItemDataBoundEventHandler"
					    OnDeleteCommand="removeuser"
					    DataKeyField="users_tbl_PK"
					    runat=server>
	            
				   <Columns >
	           		<asp:hyperlinkcolumn runat="server" datatextfield ="users_tbl_PK" headertext="<font color=#FFF8C6><b>User ID</b></font>"
	            		DataNavigateUrlField ="users_tbl_PK"
	            		DataNavigateUrlFormatString="usermaintdetail.aspx?action=edit&id={0}"  ItemStyle-HorizontalAlign=center ItemStyle-Width="60px" />
	        		  <asp:BoundColumn HeaderText="<font color=#FFF8C6><b>First Name</b></font>"  DataField="fname" ItemStyle-Width="120px"   />
	        			<asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Last Name</b></font>"  DataField="lname" ItemStyle-Width="120px"    />
	        			
	        			<asp:BoundColumn HeaderText="<font color=#FFF8C6><b>UID</b></font>"  DataField="UID" ItemStyle-Width="80px"   />
	        		    <asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Exp Date</b></font>"  DataField="expdate" ItemStyle-Width="160px"    />
	        			<asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Status</b></font>"  DataField="status" ItemStyle-Width="160px"    />
	        			<asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Role</b></font>"  DataField="role" ItemStyle-Width="160px"    />
	        			
	        		  <asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Business Phone #</b></font>" visible=false DataField="bphone" ItemStyle-Width="150px"    />
	        			<asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Cell Phone #</b></font>"  visible=false DataField="cphone" ItemStyle-Width="150px"    />
	        		    <asp:TemplateColumn HeaderText="<font color=#FFF8C6><b></b></font>" ItemStyle-Width="100px"  >
				            <ItemTemplate >
				                <table width=100%>
				                    <tr>
				                         <td><asp:button id="bbremoveuser" CommandName="delete"  runat=server text="Remove"  CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:70px; cursor:hand" /></td>		
		         
				                    </tr>    
				                </table>   
				            </ItemTemplate>                                                     
			            </asp:TemplateColumn>
		        </Columns>
		

		</asp:DataGrid></td></tr></table>
		<table>
			<tr>
				<td><asp:Button  id="btnadduser" OnClick="newuser" Text="Add User" runat="server" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;	width:80px; cursor:hand" /></td>
				<td><asp:Button  id="btnstatus" OnClick="togglestatus" Text="Show All" runat="server" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;	width:80px; cursor:hand" /></td>
	
			</tr>
		</table>
	
	</form>
</body>	
</HTML>
