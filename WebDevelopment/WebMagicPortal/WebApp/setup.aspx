<%@ Page language="vb" Codebehind="setup.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.usersetupA" Debug="false" trace="false" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script language="JavaScript" src="../_include/default.js"></script>
<HTML>
	<HEAD>
		<title>WebMagicPortal.com</title>
	</HEAD>
	
<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start" >
	<form id="forms1a" runat="server">
	
		<table>
			<tr>
				<td class=pgheaders>System Administration</td>
			</tr>
		</table>
		<table>
			<tr>
				<td>
				</td>
			</tr>
		</table>
		<asp:panel id=pnlclogins runat=server visible=false>
		<table>
			<tr>
				<td class=pgsubheaders>Current logged in users</td>
			</tr>
		</table>
		 <table width=100%>
	        <tr>
	            <td> <asp:DataGrid	            
				        	ID="clogins" 
				        	AutoGenerateColumns=False
				        	Width="100%"
			        		AllowPaging="True" 
                    	PageSize="9" 
                    	PagerStyle-Mode="NumericPages" 
                    	OnPageIndexChanged="clogins_PageChanger"
			          	Runat=server CssClass="dg">
			        			<HeaderStyle CssClass="dgheaders" />
			        			<ItemStyle CssClass="dgitems" />
			        			<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>


		                <Columns >
    		             	<asp:BoundColumn HeaderText="UID"  DataField="lg_uid" visible="true" ItemStyle-Width="10px"    />
     		               <asp:BoundColumn HeaderText="Login Date/Time"  DataField="lg_logindate" visible="true" ItemStyle-Width="100px"    />
     		               <asp:BoundColumn  HeaderText="IP #"  DataField="lg_ipnumber" visible="true" ItemStyle-Width="150px"    />
     		               <asp:BoundColumn  HeaderText="Name"  DataField="fullname" visible="true" ItemStyle-Width="150px"    />
     		              <asp:BoundColumn  HeaderText="Company"  DataField="Company" visible="true" ItemStyle-Width="150px"    />
     		                 
				        </Columns>
			        </asp:DataGrid>
                 </td>
		    </tr>
	    </table>
		</asp:panel>
		
	
	</form>
</body>	
</HTML>
