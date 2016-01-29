<%@ Page language="vb" Codebehind="usermaint.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.usermaint" Debug="false" trace="false" %>
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
		<title>www.WebMagicPortal.com</title>
	</HEAD>
	
<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">
	<form id="forms1a" runat="server">
		<table>
			<tr>
				<td class=pgheaders>User Maintenance</td>
			</tr>
		</table>
		 <table WIDTH=48%>
	    	<tr>
	    		<td class=pgsubheaders align=right>Search</td>
	    		<td><asp:textbox id="l_search" runat=server size=25 ontextchanged="btnsearch" autopostback="true"  />&nbsp&nbsp
	    			<asp:linkbutton id="clear" Text= "Clear"  runat="server"  cssclass="linkbuttons"  onClick="clearall" /></td>
	    		<td>Status</td>
	    		<td><asp:DropDownList id="dd_userstat"  AutoPostBack="True"
                  		OnSelectedIndexChanged="ChangetypeFilter"
               		DataValueField="x_descr" 
               		Runat="server" /></td>		    
			    
	    		<td>Role</td>
	    		<td ><asp:DropDownList id="dd_userrole"  AutoPostBack="True"
                  		OnSelectedIndexChanged="ChangetypeFilter"
               		DataValueField="x_descr" 
               		Runat="server" /></td>
	    		
	    	</tr>
	    </table>
		<table>
			<tr>
				<td>
					<asp:DataGrid Runat=server
					ID="users" 
	            AutoGenerateColumns=False
	            Width="100%"    	           
					AllowPaging="True" 
               PageSize="12" 
	          	PagerStyle-Mode="NumericPages" 
	          	OnPageIndexChanged="myDataGrid_PageChangerA"					
					 OnItemDataBound="ItemDataBoundEventHandler"
					    OnDeleteCommand="removeuser"
					    DataKeyField="users_tbl_PK"
					    runat=server CssClass="dg">
	            	<HeaderStyle CssClass="dgheaders" />
							        				<ItemStyle CssClass="dgitems" />
							        				<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>
				   <Columns >
	           		<asp:hyperlinkcolumn runat="server" datatextfield ="users_tbl_PK" headertext="User ID" ItemStyle-CssClass="dglinks"
	            		DataNavigateUrlField ="users_tbl_PK"
	            		DataNavigateUrlFormatString="usermaintdetail.aspx?action=edit&id={0}"  ItemStyle-HorizontalAlign=center ItemStyle-Width="60px" />
	        		  	<asp:BoundColumn HeaderText="First Name"  DataField="fname" ItemStyle-Width="120px"   />
	        			<asp:BoundColumn HeaderText="Last Name"  DataField="lname" ItemStyle-Width="120px"    />
	        			<asp:BoundColumn HeaderText="UID"  DataField="UID" ItemStyle-Width="80px"   />
	        		   <asp:BoundColumn HeaderText="Exp Date"  DataField="expdate" ItemStyle-Width="160px"    />
	        			<asp:BoundColumn HeaderText="Status"  DataField="status" ItemStyle-Width="160px"    />
	        			<asp:BoundColumn HeaderText="Role"  DataField="role" ItemStyle-Width="160px"    />
	        			<asp:BoundColumn HeaderText="Business Phone #" visible=false DataField="bphone" ItemStyle-Width="150px"    />
	        			<asp:BoundColumn HeaderText="Cell Phone #"  visible=false DataField="cphone" ItemStyle-Width="150px"    />
	        		    <asp:TemplateColumn HeaderText="" ItemStyle-Width="100px" ItemStyle-CssClass="dgitemsNOBD"  >
				            <ItemTemplate >
				                <table width=100%>
				                    <tr>
				                         <td><asp:button id="bbremoveuser" CommandName="delete"  runat=server text="Remove"  CausesValidation="false" cssclass=frmbuttons /></td>		
		                        		 <td><asp:button id="bbloguserout" onclick="Logoutuser"  runat=server text="Logout"  CausesValidation="false" cssclass=frmbuttons /></td>		
		         
				                    </tr>    
				                </table>   
				            </ItemTemplate>                                                     
			            </asp:TemplateColumn>
		        </Columns>
		

		</asp:DataGrid></td></tr></table>
		<table>
			<tr>
				<td><asp:Button  id="btnadduser" OnClick="newuser" Text="Add User" runat="server" cssclass=frmbuttons /></td>
				<td><asp:Button  id="btnstatus" OnClick="togglestatus" Text="Show All" runat="server"  cssclass=frmbuttons /></td>
				<td><asp:Button  id="btnconvert" OnClick="convertuser" Text="Subscribe" runat="server"  cssclass=frmbuttons /></td>
	
			</tr>
		</table>
	
	</form>
</body>	
</HTML>
