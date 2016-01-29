<%@ Page language="vb" Codebehind="autopostDetail.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.autopostDetail" Debug="false" trace="false" aspcompat=true  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script language="JavaScript" src="../_include/default.js"></script>

<HTML>
	<HEAD>
		<title>www.WebMagicPortal.com</title>
	</HEAD>
	
<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">
	<form id="forms1a" runat="server" enctype="multipart/form-data" method="post">
		<table>
			<tr>
				<td><font size=4>AutoPosting AD Details</font></td>
			</tr>
		</table>
		<br />
		<asp:panel id="pnlapdetail" runat=server visible=true>
		<table width=50%>
			<tr>
				<td><b>AD No</b></td>
				<td><b>Venue Name</b></td>
				<td><b>User ID</b></td>
				<td><b>Due Date</b></td>
				<td><b>Start Date</b></td>
				<td><b>End Date</b></td>
			</tr>
			<tr>
				<td><asp:label id="lbladno" runat=server /></td>
				<td><asp:label id="lblvenue" runat=server /></td>
				<td><asp:label id="lbluid" runat=server /></td>
				<td><asp:label id="lbldue" runat=server /></td>
				<td><asp:label id="lblfdate" runat=server /></td>
				<td><asp:label id="lbltdate" runat=server /></td>
			</tr>
		</table>
		<hr />
		<table>
			<tr>
				<td>AD Title</td>
			</tr>
			<tr>
				<td><asp:textbox id="txtadtitle" runat=server enabled=false /></td>
			</tr>
			<tr>
				<td>AD TEXT</td>
			</tr>
			<tr>
				<td><asp:textbox id="txtadtext" runat=server size=16 TextMode="MultiLine" Columns="90"  Rows="10" enabled=false /></td>
			</tr>
		</table>
		<hr />
		<table width=45%>
			<tr>
				<td>Status</td>
				<td><asp:DropDownList id="dd_status" runat="server" >    							               
                 	<asp:ListItem Value="Submitted" Text="Submitted"/>
                 	<asp:ListItem Value="Inprocess" Text="Inprocess"/>                 
                 	<asp:ListItem Value="Completed" Text="Completed"/>
                 	<asp:ListItem Value="Canceled Text="Canceled"/>
                
                	</asp:DropDownList></td>
				<td>Posting Number</td>
				<td><asp:textbox id="txtpostno" runat=server enabled=true /></td>
			</tr>
			
		</table>
		<table>
         	<tr>
            	<td><asp:Button  id="btnsave"  onclick="savedetail" Text="Save" runat="server" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;	width:110px; cursor:hand" /></td>
      			<td><asp:Button  id="btnexit" onclick="exitdetail" Text="Exit" runat="server" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;	width:110px; cursor:hand" /></td>
  					<td><asp:Button  id="btnrqst" onclick="createrqst"  Text="Create Request" runat="server" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;	width:110px; cursor:hand" /></td>
  					<td><asp:Button  id="btnrhist" onclick="showhist" Text="View History" runat="server" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;	width:110px; cursor:hand" /></td>
  					
  				</tr>
		</table>
		</asp:panel>
		<asp:panel id="pnlaphistory" runat=server visible=false>
		<table width=100%>
             <tr>
                 <td><asp:DataGrid Runat=server
					    	ID="aphistory" 
	                  AutoGenerateColumns=False
			            Width="100%"          
			            ItemStyle-BackColor=white
			            ItemStyle-Font-Name="arial"
			            ItemStyle-Font-Size="12px"
			            BorderColor="#000000"
			            
							PagerStyle-Visible = "False"	
							HeaderStyle-BackColor="steelblue"
							HeaderStyle-ForeColor="White"
							
							>
 	            
			       <Columns >
        		   	 	<asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Hist No</b></font>"  DataField="aphist_tbl_pk" ItemStyle-Width="300px"   />
        			   	<asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Type</b></font>"  DataField="aphist_type" ItemStyle-Width="300px"   />
        			   	<asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Description Logo</b></font>"  DataField="aphist_notes" ItemStyle-Width="300px"   />
        			   	<asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Requestor</b></font>"  DataField="aphist_requestor" ItemStyle-Width="300px"   />	  
			           	<asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Date</b></font>"  DataField="aphist_requestdate" ItemStyle-Width="300px"   />	  
			        
		         </Columns>
	            </asp:DataGrid>
	            </td>
             </tr>
            </table>
				<table>
	         	<tr>
	            	<td><asp:Button  id="btnexitH" onclick="exithist"  Text="Exit" runat="server" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;	width:110px; cursor:hand" /></td>
	  					<td><asp:Button  id="btnrqstH"  onclick="createrqst"  Text="Create Request" runat="server" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;	width:110px; cursor:hand" /></td>
	  					
	  				</tr>
				</table>
 	</asp:panel>
		
	</form>
	</body>	
</HTML>
