<%@ Page language="vb" Codebehind="leaddropdowns.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.leaddropdowns" Debug="false" trace="false" %>
<%@ Register TagPrefix="ed" Namespace="OboutInc.Editor" Assembly="obout_Editor" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script language="JavaScript" src="../_include/default.js"></script>
<style type="text/css">
   div.idleToolTip {
     
      display: none;
      visibility:hidden;
   }
   div.activeToolTip {
      display: inline;
      visibility:visible;
      background-color:yellow;
      border:0px solid black;
      font-size: 12px;
   }
</style>

<HTML>
	<HEAD>
		<title>www.WebMagicPortal.com</title>
	</HEAD>
	
<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">
	<form id="forms1a" runat="server">
		<table>
			<tr>
					<td class=pgheaders width=95%>Drop Down Maintenance</td>
				<td><asp:ImageButton id="helpdd" runat="server"  AlternateText="View Help" ImageAlign="left" ImageUrl="../images/wizard.jpg" height=35 width=60  OnClick="btn_showhelp" /></td> 

			</tr>
		</table>
		
		<asp:panel id=pnldropmain runat=server visible=true>
		<table width=100%>
		    <tr>
		        <td valign=top width=35%>	
                    <table>
		                <tr>
			                <td><u>Lead Type</u></td>
		                </tr>
		                <tr>
			                <td>
				                <asp:listbox id="lstleadtypes" runat="server"  selectionmode="multiple" 
			                 DataTextField="x_descr" DataValueField="tbl_xwalk_pk" Height=180 width=180/>
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
		        <td valign=top width=35%> 
		            <table>
		                <tr>
			                <td><u>Lead Status</u></td>
		                </tr>
		                 <tr>
			                <td>
				                <asp:listbox id="leadstatus" runat="server"  selectionmode="multiple" 
			                 DataTextField="x_descr" DataValueField="tbl_xwalk_pk" Height=180 width=180/></td>
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
		        <td valign=top width=35%>
		            <table>
		                <tr>
			                <td><u>Lead Source</u></td>
		                </tr>
		                 <tr>
			                <td>
				                <asp:listbox id="leadsource" runat="server"  selectionmode="single" 
			                 DataTextField="x_descr" DataValueField="tbl_xwalk_pk" Height=180 width=180/></td>
			             
			             <td valign=top colspan=2>
                                <table border=0 width=10%>
                                    <tr>     
                                        <td valign=top><asp:button id="ldsource" runat=server text="Remove" onclick="leadsourcecommit" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:60px; cursor:hand" /></td>		
	                                </tr>
	                                 <tr>
                                      <td width=20><asp:button id="ldsEdit" runat=server text="Edit" onclick="leadsourceEdit" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:60px; cursor:hand" /></td>		
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
	                   </table>
	                   <table>
                        <tr>
                          <td><asp:button id="ldsourcea" runat=server text="Add" onclick="Addleadsource" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:60px; cursor:hand" /></td>		
          	   				<td>	<table>
        								<tr>
										<td><font color=red><asp:label visible=false id=lblnovensel text="Please choose a venue!" runat=server /></font></td>
									</tr>
										<tr>
										<td><font color=red><asp:label visible=false id=lblnoeditvenue text="Cannot edit this Venue!" runat=server /></font></td>
									</tr>
									</table></td>
                	    </tr>
                    </table>
		           
			             
		        </td>
		        
		        
		        
		       
		    </tr>
		    <tr>
		     <td valign=top valign=top width=35%>
		            <table>
		                <tr>
			                <td><u>Lead Programs</u></td>
		                </tr>
		                 <tr>
			                <td>
				                <asp:listbox id="leadprograms" runat="server"  selectionmode="multiple" 
			                 DataTextField="x_descr" DataValueField="tbl_xwalk_pk" Height=180 width=180 /></td>
			             
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
		        <td valign=top valign=top width=35%>
		        
		          <table>
		                <tr>
			                <td><u>Task Types</u></td>
		                </tr>
		                 <tr>
			                <td>
				                <asp:listbox id="tasks" runat="server"  selectionmode="multiple" 
			                 DataTextField="x_descr" DataValueField="tbl_xwalk_pk" Height=180 width=180/></td>
			             
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
		        </td>
		        
		         <td valign=top valign=top width=35%>
		          <table>
		                <tr>
			                <td><u>Marketing Program</u></td>
		                </tr>
		                 <tr>
			                <td>
				                <asp:listbox id="mktprg" runat="server"  selectionmode="multiple" 
			                 DataTextField="x_descr" DataValueField="tbl_xwalk_pk" Height=180 width=180/></td>
			             
			             <td valign=top>
                                <table border=0 width=10%>
                                    <tr>     
                                        <td valign=top><asp:button id="rmvmkt" runat=server text="Remove" onclick="mkttypecommit" CausesValidation="false" cssclass=frmbuttons /></td>		
	                                </tr>
		                            <asp:Panel ID="pnlmktwarn" runat=server Visible=false>
		                            <tr>
		                                <td colspan=2>Warning!! Leads exist for this type.  Removing will affect these tasks!</td>
		                            </tr>
		                            <tr>
                                        <td width=20><asp:button id="delmkttype" runat=server text="Delete" onclick="mkttyperemove" CausesValidation="false" cssclass=frmbuttons /></td>		
		                                <td><asp:button id="delmkttypecan" runat=server text="Cancel" onclick="mkttypecancel" CausesValidation="false" cssclass=frmbuttons /></td>		
		                            </tr>		           
		                           </asp:Panel>
		                        </table>
	                        </td>
	                    </tr>
                        <tr>
                          <td><asp:button id="mkttypead" runat=server text="Add" onclick="Addmkttype" CausesValidation="false" cssclass=frmbuttons /></td>		
                	    </tr>
                    </table>
		            <asp:Panel ID="pnladdmkttype" runat=server Visible=false>
		                <table>
			                <tr>
				                <td><asp:textbox id="newmkttype" runat=server size=14 /></td>
				                <td><asp:button id="savemtype" runat=server text="Save" onclick="savemkttype" CausesValidation="false" cssclass=frmbuttons /></td>		
                		  
			                </tr>
                			
		                </table>			
		            </asp:Panel>
		        </td>
		        
		        
		        
		        
		        
		        
		        </tr>
        </table>	
        </asp:panel>
         <asp:Panel ID="pnlleadsourceadd" runat=server Visible=false>
		                 <table width=85%>
                		    <tr>	
                			    <td valign=top>Lead Source Name</td>
                			     <td valign=top>Online?</td>
                			    <td valign=top>Lead Source Code <img src="../images/help_icon.jpg" alt="Help" height=15 width=15 onmouseover="document.getElementById('myToolTipc').className='activeToolTip'" onmouseout="document.getElementById('myToolTipc').className='idleToolTip'"/></td>
                			    <td valign=top>URL</td>
                			    <td>Has Account Setup?</td>
                			     <td valign=top>Private?</td>
                			     <td>HTML Text?</td>
                			  
                		    </tr>
                		    <tr>
                		     <td valign=top><asp:textbox id="venuname" runat=server /></td><asp:textbox id="venunameorg" runat=server visible=false/>
                			   <td valign=top><asp:DropDownList id="ddvenonline" runat="server" >    							               
    							                 <asp:ListItem Value="Yes" Text="Yes"/>
  	    						                 <asp:ListItem Value="No" Text="No"/>
  	    						                </asp:DropDownList></td>
                			    <td valign=top><asp:textbox id="venuecode" runat=server size=4 /></td>
                			   <td valign=top><asp:textbox id="venueurl" runat=server size=30 /></td>
                			     <td valign=top><asp:checkbox id="acctsetup" runat=server /></td>
                		    
                			    <td valign=top><asp:checkbox id="privateven" runat=server /></td>
                		    	<td valign=top><asp:checkbox id="htmltext" runat=server /></td>
                		    </tr>
                		   </table>
                		   <table>
                		    <tr>
                		    	<td>Notes</td>
                		    	<td>Instructions</td>
                		    <tr>
                		    	<td><ed:Editor ShowQuickFormat="false" Appearance="lite"  AutoFocus="false" height="200" width="550" submit="false" PathPrefix="Editor_data/" FlashBrowse="myFlashBrowse.aspx" MediaBrowse="myMediaBrowse.aspx" ImageBrowse="Editor_data/myImageBrowseClient.aspx" UrlBrowse="myUrlBrowse.aspx" id="lsnotes" PreviewMode="false" runat="server" /></td>
					             <td><ed:Editor ShowQuickFormat="false" Appearance="lite"  AutoFocus="false" height="200" width="550" submit="false" PathPrefix="Editor_data/" FlashBrowse="myFlashBrowse.aspx" MediaBrowse="myMediaBrowse.aspx" ImageBrowse="Editor_data/myImageBrowseClient.aspx" UrlBrowse="myUrlBrowse.aspx" id="lsinst" PreviewMode="false" runat="server" /></td>
					                             		
                		    
                		    </tr>
                	
                		    <tr>
                		        <td colspan=6><div id="myToolTipc" class="idleToolTip">Enter characters to help ID this AD Code, ie. Craigs List = CL</div></td></tr>
                		 </table>
                		 <table>
                		    <tr>	
                			    <td colspan=8><asp:button id="addnewv" runat=server text="Save" onclick="savenewvenue" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:80px; cursor:hand" /></td>		
    			           	        <td colspan=8><asp:button id="addnewvexit" runat=server text="Cancel" onclick="savenewvenueExit" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:80px; cursor:hand" /></td>		
    		
                		    </tr>
                	    </table>               
                	   
		            </asp:Panel>	 
	</form>
</body>	
</HTML>
