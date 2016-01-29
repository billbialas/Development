<%@ Page language="vb" Codebehind="companymaint.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.companymaint" Debug="false" trace="false" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script language="JavaScript" src="../_include/default.js"></script>
<HTML>
	<HEAD>
		<title>WebMagicPortal.com</title>
	</HEAD>
	
<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">
	<form id="forms1a" runat="server">
		<table>
			<tr>
				<td class=pgheaders width=95%>Company Maintenance</td>
				<td><asp:ImageButton id="helpum" runat="server"  AlternateText="View Help" ImageAlign="left" ImageUrl="../images/wizard.jpg" height=50 width=80  OnClick="btn_showhelp" /></td> 
		</tr>
		</table>
		<table>
		    <tr>
		        <td width=70 align=right>ID</td>
		        <td><asp:TextBox ID="COID" runat=server /></td>		        
		    </tr>
		</table>
		<table>
		    <tr>
		        <td width=70 align=right>Name</td>
		        <td><asp:TextBox ID="CONAME" runat=server /></td>
		        <td><img src= "/logos/company/default.jpg" alt="logo" id="logoimg" runat=server height =50 width=50 /></td>
		    </tr>
		</table>
		<table>
		    <tr>
		        <td width=70 align=right>Logo</td>
		        <td><INPUT type=file id=logo name=File1 runat="server" ></td>
                <td><input type="submit" id="Submit1" value="Upload" runat="server" NAME="Submit1"></td>
		    </tr>
		</table>
		<table>
		    <tr>
		        <td align=right>Website</td>
		        <td><asp:TextBox ID="cowebsite" runat=server size=40 /></td>
		    </tr>
		    <tr>
		        <td align=right>Online Store</td>
		        <td><asp:TextBox ID="coestore" runat=server  size=40  /></td>
		    </tr>
		</table>
	    <table >
	        <tr>
	            <td align=right ><asp:button id="bsaveco" runat=server text="Save" onclick="updatecoinfo" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:80px; cursor:hand" /></td>		
		    </tr></table>
		    <br />
		<table>
	        <tr>
	            <td><b>Billing History</b></td>
	           
                          
	        </tr>
	        <tr>
	            <td colspan=3><asp:DataGrid Runat=server
					    ID="billinghist" 
	                    AutoGenerateColumns=False
	                    Width="100%"          
	                    ItemStyle-BackColor=white
	                    ItemStyle-Font-Name="arial"
	                    ItemStyle-Font-Size="12px"
	                    BorderColor="#000000"    	           
					    PagerStyle-Visible = "False"	
					    HeaderStyle-BackColor="steelblue"
					    HeaderStyle-ForeColor="White"
					     AllowPaging="True" 
                            PageSize="7" 
                            PagerStyle-Mode="NumericPages" 
                            OnPageIndexChanged="myDataGrid_PageChanger"
					    
					    >
    	            
				       <Columns >
	           		       <asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Order No</b></font>"  DataField="orderno" ItemStyle-Width="300px"   />
	        			  <asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Order Date</b></font>"  DataField="orderdate" ItemStyle-Width="300px"   />
	        			  <asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Package</b></font>"  DataField="package" ItemStyle-Width="300px"   />
	        			  <asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Cost</b></font>"  DataField="cost" ItemStyle-Width="300px"   />
	        			
			         </Columns>
		            </asp:DataGrid></td>
	       </tr>
	    </table>
	</form>
</body>	
</HTML>
