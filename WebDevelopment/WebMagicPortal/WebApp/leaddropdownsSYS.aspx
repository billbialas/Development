<%@ Page language="vb" Codebehind="leaddropdownsSYS.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.leaddropdownsSYS" Debug="false" trace="false" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script language="JavaScript" src="../_include/default.js"></script>
<HTML>
	<HEAD>
		<title>Choice One BMS</title>
	</HEAD>
	
<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">
	<form id="forms1a" runat="server">
		<table>
			<tr>
				<td><font size="3pt"><b>Drop Downs</b></font></td>
			</tr>
		</table>
		<br />
		<table width=100%>
		    <tr>
		        <td valign=top width=25%>	
                    <table>
		                <tr>
			                <td><u>Lead Type</u></td>
		                </tr>
		                <tr>
			                <td>
				                <asp:listbox id="lstleadtypes" runat="server"  selectionmode="multiple" 
			                 DataTextField="x_descr" DataValueField="tbl_xwalk_pk" Height=90/>
                         </td>
                         <td valign=top>
                            <table border=0 width=10%>
                                <tr>     
                                    <td valign=top><asp:button id="ldtyperemove" runat=server text="Remove" onclick="leadtypecommit" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:60px; cursor:hand" /></td>		
	                            </tr>
		                        <asp:Panel ID="confirmremove" runat=server Visible=false>
		                        <tr>
		                            <td colspan=2>Warning!! Leads exist for this type.  Removing will affect these leads!</td>
		                        </tr>
		                        <tr>
                                    <td width=20><asp:button id="removeYes" runat=server text="Delete" onclick="leadtyperemove" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:60px; cursor:hand" /></td>		
		                            <td><asp:button id="removeNo" runat=server text="Cancel" onclick="leadtypecancel" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:60px; cursor:hand" /></td>		
		                        </tr>		           
		                       </asp:Panel>
		                    </table>
	                    </td>
	                  </tr>
                      <tr>
                        <td><asp:button id="B_Addleadtype" runat=server text="Add" onclick="Addleadtype" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:60px; cursor:hand" /></td>		
            		   
		              </tr>
                    </table>
		            <asp:Panel ID="pnladdleadtype" runat=server Visible=false>
		                <table>
			                <tr>
				                <td><asp:textbox id="newleadtype" runat=server size=14 /></td>
				                <td><asp:button id="bsaveleadtype" runat=server text="Save" onclick="Saveleadtype" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:60px; cursor:hand" /></td>		
                		  
			                </tr>
                			
		                </table>			
		            </asp:Panel>
		        </td>
		        <td valign=top> 
		            <table>
		                <tr>
			                <td><u>Lead Status</u></td>
		                </tr>
		                 <tr>
			                <td>
				                <asp:listbox id="leadstatus" runat="server"  selectionmode="multiple" 
			                 DataTextField="x_descr" DataValueField="tbl_xwalk_pk" Height=90/></td>
                            <td valign=top>
                                <table border=0 width=10%>
                                    <tr>     
                                        <td valign=top><asp:button id="Removeleadstat" runat=server text="Remove" onclick="leadstatcommit" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:60px; cursor:hand" /></td>		
	                                </tr>
		                            <asp:Panel ID="pnlldstatwarn" runat=server Visible=false>
		                            <tr>
		                                <td colspan=2>Warning!! Leads exist for this type.  Removing will affect these leads!</td>
		                            </tr>
		                            <tr>
                                        <td width=20><asp:button id="ldstatdel" runat=server text="Delete" onclick="leadstatremove" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:60px; cursor:hand" /></td>		
		                                <td><asp:button id="ldstatcancel" runat=server text="Cancel" onclick="leadstatcancel" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:60px; cursor:hand" /></td>		
		                            </tr>		           
		                           </asp:Panel>
		                        </table>
	                        </td>
	                    </tr>
                        <tr>
                          <td><asp:button id="ldstatadd" runat=server text="Add" onclick="Addleadstatus" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:60px; cursor:hand" /></td>		
                	    </tr>
                    </table>
		            <asp:Panel ID="pnladdleadstatus" runat=server Visible=false>
		                <table>
			                <tr>
				                <td><asp:textbox id="newleadstatus" runat=server size=14 /></td>
				                <td><asp:button id="bsaveleadstatus" runat=server text="Save" onclick="saveleadstatus" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:60px; cursor:hand" /></td>		
                		  
			                </tr>
                			
		                </table>			
		            </asp:Panel>
		     
		        </td>
		        <td valign=top>
		            <table>
		                <tr>
			                <td><u>Lead Source</u></td>
		                </tr>
		                 <tr>
			                <td>
				                <asp:listbox id="leadsource" runat="server"  selectionmode="multiple" 
			                 DataTextField="x_descr" DataValueField="tbl_xwalk_pk" Height=90/></td>
			             
			             <td valign=top>
                                <table border=0 width=10%>
                                    <tr>     
                                        <td valign=top><asp:button id="ldsource" runat=server text="Remove" onclick="leadsourcecommit" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:60px; cursor:hand" /></td>		
	                                </tr>
		                            <asp:Panel ID="pnlldsourcewarn" runat=server Visible=false>
		                            <tr>
		                                <td colspan=2>Warning!! Leads exist for this type.  Removing will affect these leads!</td>
		                            </tr>
		                            <tr>
                                        <td width=20><asp:button id="ldsourcer" runat=server text="Delete" onclick="leadsourceremove" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:60px; cursor:hand" /></td>		
		                                <td><asp:button id="ldsourcecan" runat=server text="Cancel" onclick="leadsourcecancel" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:60px; cursor:hand" /></td>		
		                            </tr>		           
		                           </asp:Panel>
		                        </table>
	                        </td>
	                    </tr>
                        <tr>
                          <td><asp:button id="ldsourcea" runat=server text="Add" onclick="Addleadsource" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:60px; cursor:hand" /></td>		
                	    </tr>
                    </table>
		            <asp:Panel ID="pnlleadsourceadd" runat=server Visible=false>
		                <table>
			                <tr>
				                <td><asp:textbox id="newleadsource" runat=server size=14 /></td>
				                <td><asp:button id="bsaveleadsource" runat=server text="Save" onclick="saveleadsource" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:60px; cursor:hand" /></td>		
                		  
			                </tr>
                			
		                </table>			
		            </asp:Panel>
			             
		        </td>
		        
		        
		        
		        <td valign=top>
		            <table>
		                <tr>
			                <td><u>Lead Programs</u></td>
		                </tr>
		                 <tr>
			                <td>
				                <asp:listbox id="leadprograms" runat="server"  selectionmode="multiple" 
			                 DataTextField="x_descr" DataValueField="tbl_xwalk_pk" Height=90/></td>
			             
			             <td valign=top>
                                <table border=0 width=10%>
                                    <tr>     
                                        <td valign=top><asp:button id="ldprograms" runat=server text="Remove" onclick="leadprogramscommit" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:60px; cursor:hand" /></td>		
	                                </tr>
		                            <asp:Panel ID="pnlldprogramswarn" runat=server Visible=false>
		                            <tr>
		                                <td colspan=2>Warning!! Leads exist for this type.  Removing will affect these leads!</td>
		                            </tr>
		                            <tr>
                                        <td width=20><asp:button id="ldprogramsr" runat=server text="Delete" onclick="leadprogramsremove" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:60px; cursor:hand" /></td>		
		                                <td><asp:button id="ldprogramscan" runat=server text="Cancel" onclick="leadprogramscancel" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:60px; cursor:hand" /></td>		
		                            </tr>		           
		                           </asp:Panel>
		                        </table>
	                        </td>
	                    </tr>
                        <tr>
                          <td><asp:button id="ldprogramsa" runat=server text="Add" onclick="Addleadprograms" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:60px; cursor:hand" /></td>		
                	    </tr>
                    </table>
		            <asp:Panel ID="pnlleadprogramsadd" runat=server Visible=false>
		                <table>
			                <tr>
				                <td><asp:textbox id="newleadprograms" runat=server size=14 /></td>
				                <td><asp:button id="bsaveleadprograms" runat=server text="Save" onclick="saveleadprograms" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:60px; cursor:hand" /></td>		
                		  
			                </tr>
                			
		                </table>			
		            </asp:Panel>
			             
		        </td>
		    </tr>
		    <tr>
		        <td>
		          <table>
		                <tr>
			                <td><u>Task Types</u></td>
		                </tr>
		                 <tr>
			                <td>
				                <asp:listbox id="tasks" runat="server"  selectionmode="multiple" 
			                 DataTextField="x_descr" DataValueField="tbl_xwalk_pk" Height=90/></td>
			             
			             <td valign=top>
                                <table border=0 width=10%>
                                    <tr>     
                                        <td valign=top><asp:button id="rmvtask" runat=server text="Remove" onclick="tasktypecommit" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:60px; cursor:hand" /></td>		
	                                </tr>
		                            <asp:Panel ID="pnltaskwarn" runat=server Visible=false>
		                            <tr>
		                                <td colspan=2>Warning!! Tasks exist for this type.  Removing will affect these tasks!</td>
		                            </tr>
		                            <tr>
                                        <td width=20><asp:button id="deltasktype" runat=server text="Delete" onclick="tasktyperemove" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:60px; cursor:hand" /></td>		
		                                <td><asp:button id="deltasktypecan" runat=server text="Cancel" onclick="tasktypecancel" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:60px; cursor:hand" /></td>		
		                            </tr>		           
		                           </asp:Panel>
		                        </table>
	                        </td>
	                    </tr>
                        <tr>
                          <td><asp:button id="tasktypead" runat=server text="Add" onclick="Addtasktype" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:60px; cursor:hand" /></td>		
                	    </tr>
                    </table>
		            <asp:Panel ID="pnladdtasktype" runat=server Visible=false>
		                <table>
			                <tr>
				                <td><asp:textbox id="newtasktype" runat=server size=14 /></td>
				                <td><asp:button id="savettype" runat=server text="Save" onclick="savetasktype" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:60px; cursor:hand" /></td>		
                		  
			                </tr>
                			
		                </table>			
		            </asp:Panel>
		        </td></tr>
        </table>		 
	</form>
</body>	
</HTML>
