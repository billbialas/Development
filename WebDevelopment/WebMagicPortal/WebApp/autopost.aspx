<%@ Page language="vb" Codebehind="autopost.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.autopost" Debug="false" trace="false" aspcompat=true  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script language="JavaScript" src="../_include/default.js"></script>

<HTML>
	<HEAD>
		<title>www.WebMagicPortal.com</title>
	</HEAD>
	
<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start" >
	<form id="forms1a" runat="server" enctype="multipart/form-data" method="post">
		  <table>
		  		<tr>	
		  			<td width=100><font size=4>Auto Posts</font></td>
		  	    </tr>
		  </table>
		  <table width=45%>
		        <tr>
		         <td width=1%>
		            <table>
		                <tr>
		                    <td><b>Search</b>
		                    </td>
		                </tr>
		                <tr><td><asp:linkbutton id="clear" Text= "Clear" 
                    runat="server" Font-Bold="True" Font-underline="True" Style="color:#ff0000; font-family:arial; font-size:8pt; cursor:hand"
                    onClick="clearall" />
                            </td></tr></table>
                  </td>
		  		    <td valign=middle ><asp:textbox id="ap_search" runat=server size=25 ontextchanged="btnsearch" autopostback="true" /></td>
		  	 <td valign=middle width=18%><asp:linkbutton id="btn_search" Text= "Go" 
                    runat="server" Font-Bold="True" Font-underline="false" Style="color:#000000; font-family:arial; font-size:9pt; cursor:hand"
                    onClick="btnsearch" /> 
		  			  <td width=70><asp:linkbutton id="apnew" Text= "Submitted" visible=true
                    						runat="server" Font-Bold="True" Font-underline="True" Style="color:#ff0000; font-family:arial; font-size:8pt; cursor:hand"
                  							 onClick="apfilterSubmitted" /></td>
                  							 <td>|</td>
				                    <td width=75><asp:linkbutton id="apinp" Text= "Inprocess" visible=true
                    						runat="server" Font-Bold="True" Font-underline="True" Style="color:#ff0000; font-family:arial; font-size:8pt; cursor:hand"
                  							  onClick="apfilterInprocess" /></td>
                  							   <td>|</td>
                  				    <td width=75><asp:linkbutton id="apcomp" Text= "Completed" visible=true
                    						runat="server" Font-Bold="True" Font-underline="True" Style="color:#ff0000; font-family:arial; font-size:8pt; cursor:hand"
                  							 onClick="apfilterCompleted"  /></td>
		  		</tr>
		  		
		  	</table>
		  	<table width=100%>
                <tr>
                    <td><asp:DataGrid Runat=server
						    	ID="apqueue" 
		                  AutoGenerateColumns=False
				            Width="100%"          
				            ItemStyle-BackColor=white
				            ItemStyle-Font-Name="arial"
				            ItemStyle-Font-Size="12px"
				            BorderColor="#000000"
				            AllowPaging="True" 
                                        PageSize="20" 
                                        PagerStyle-Mode="NumericPages" 
                                        OnPageIndexChanged="ap_pagechanger"
								HeaderStyle-BackColor="steelblue"
								HeaderStyle-ForeColor="White"
								DataKeyField="ap_tbl_pk"
								OnItemDataBound="ItemDataBoundEventHandler">
    	            
				       <Columns >
				       		<asp:hyperlinkcolumn runat="server"  Text="EDIT" datatextfield ="ap_tbl_pk" headertext="<font color=#FFF8C6><b>EDIT</b></font>"
	            		    DataNavigateUrlField ="ap_tbl_pk"
	            		    DataNavigateUrlFormatString="autopostdetail.aspx?&id={0}&action=edit"  ItemStyle-HorizontalAlign=center ItemStyle-Width="60px" />
    	        		  
	           		     	<asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Status</b></font>"  DataField="ap_status" ItemStyle-Width="200px"   />    
	        					<asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Due</b></font>"  DataField="ap_duedate" ItemStyle-Width="200px"   />    
	        					
	        					<asp:BoundColumn HeaderText="<font color=#FFF8C6><b>AD#</b></font>" visible=true DataField="tbl_leadad_pk" ItemStyle-Width="80px"    />
	        					<asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Venue</b></font>"  DataField="tbl_leadadvenues" ItemStyle-Width="80px"    />
	        	        		<asp:BoundColumn HeaderText="<font color=#FFF8C6><b>AD Title</b></font>"  DataField="ad_title" ItemStyle-Width="80px"    />
	        	        		<asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Venue Name</b></font>"  DataField="ad_title" ItemStyle-Width="80px"    />
	        	        		
				            
			         </Columns>
		            </asp:DataGrid>
		            </td>
                </tr>
            </table>
 
		
	</form>
</body>	
</HTML>
