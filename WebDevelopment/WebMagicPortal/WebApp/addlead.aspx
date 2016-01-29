<%@ Page language="vb" Codebehind="addlead2.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.addlead2" Debug="false" trace="false" validateRequest=false  %>
<%@ Register TagPrefix="mbcbb" Namespace="MetaBuilders.WebControls" Assembly="MetaBuilders.WebControls" %>
<%@ Register TagPrefix="skmZ" Namespace="skmMenu" Assembly="skmMenu" %> 
<%@ Register TagPrefix="ed" Namespace="OboutInc.Editor" Assembly="obout_Editor" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script language="JavaScript" src="../_include/default.js"></script>
<link rel="stylesheet" href="../_include/default.css" type="text/css">

<HTML>
	<HEAD>
		<title>www.WebMagicPortal.com</title>
	</HEAD>
	
	<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">
		<form  name="addlead"  runat="server" >
            <asp:Panel id="pnlescalate" runat="server">
	            <table>
			        <tr>
				        <td><marquee ALIGN="Top" LOOP="infinite" BEHAVIOR="slide" BGCOLOR="#FF0000" DIRECTION="left" HEIGHT=10 WIDTH=200>
						     This lead has been ESCALATED!</marquee></td>
			        </tr>
		        </table>
            </asp:panel>	
            <asp:Panel id="pnlclose" runat="server">
		        <table>
				    <tr>
					    <td>Can not close lead untill accepted!</td>
				    </tr>
			    </table>
            </asp:panel>	
	        
			    <table width=100% border =0>
			        <tr>
				        <td width=100%>
			            <div style="vertical-align top; horizontal-align left">
  						<asp:Panel id="pnlnew" runat="server">
  						<table cellpadding=0 cellspacing=0 border=0 width=100% id="HeaderLD">
  						   	 <tr>
  				                
  				                <td class=pgheaders width=18%>Add/Edit Lead</td>
  				                <asp:Panel id="pnlassignedby" runat="server">
	  				    			<td width=70% valign="top" >	
	  								    <table><tr><td width=65 align=left><font size=3><b>Lead #:</b></font></td> <td ><b><u><font size=3><asp:label id='lblleadno' runat=server /></font></u></b></td></tr></table>
	      							</td>
      							<td>
      							    <table>
      							        <tr>     
  		             				   <td align=left valign=top><asp:ImageButton id="btn_note" runat="server"  AlternateText="Quick Note" ImageAlign="left" ImageUrl="../images/noteicon.png" height=25  OnClick="quicknote" /></td>
			             	                <td align=left valign=top><asp:ImageButton id="ImageButton4" runat="server"  AlternateText="Create Task" ImageAlign="left" ImageUrl="../images/taskicon.jpg" height=25  OnClick="createtask" /></td>
			                                <td align=left valign=top><asp:ImageButton id="btn_printlead" runat="server"  AlternateText="Print Lead" ImageAlign="left" ImageUrl="../images/printer.jpg" height=25  OnClick="printlead" /></td>
			                                <td align=left valign=top><asp:ImageButton id="ImageButton1" runat="server"  AlternateText="Email Lead" ImageAlign="left" ImageUrl="../images/email_icon.gif" height=25  OnClick="emaillead" /></td>
			                                <td align=left valign=top><asp:ImageButton id="ImageButton3" runat="server"  AlternateText="Export Lead" ImageAlign="left" ImageUrl="../images/file-export-128x128.png" height=25  OnClick="exportlead" /></td>
			             		            <td align=left valign=top><asp:ImageButton id="ImageButton2" runat="server"  AlternateText="Delete Lead" ImageAlign="left" ImageUrl="../images/deleteicon.jpg" height=25  OnClick="btn_delete" /></td>
			             		
			             	           </tr>
			             	       </table>
      							</td>
  						        </asp:panel>
  						      <td align=left valign=top><asp:ImageButton id="ImageButtonHelp" runat="server"  AlternateText="Help" ImageAlign="left" ImageUrl="../images/wizard.jpg" height=25  OnClick="leadhelp" /></td>
	
  						         <asp:Panel id="pnlplaceholder" runat="server">
  							    <td width=30%></td>
  						        </asp:panel>
  							    <td width=90 align=right><asp:label id='lblstatus' text="Status" runat=server /></td>
  							    <td width=80><asp:DropDownList id="dd_status" runat="server" >
    							                <asp:ListItem Value="Unaccepted" Text="Unaccepted"/>
  	    						                <asp:ListItem Value="Accepted" Text="Accepted"/>
  	    						                <asp:ListItem Value="Deferred" Text="Deferred"/>
  	    						                <asp:ListItem Value="Closed" Text="Closed"/>
 		 						                <asp:ListItem Value="Draft" Text="Draft"/>
 								                </asp:DropDownList></td>
  						        
  						    </tr>
  					    </table>
  					    </asp:panel><asp:linkbutton id="more" Text= "More" 
                                    CommandArgument="more" runat="server" Font-Bold="True" Font-underline="True" Style="color:#ff0000; font-family:arial; font-size:8pt; cursor:hand"
                                    visible =false onClick="MoreButtonClick" />
  					    <table cellpadding=1 cellspacing=2 border=0 width=80%>
  						   
						    <tr>
						     
							    <td  align=left><b>First Name</b></td>
  							    <td width=65 align=left><b>Last Name</b></td>
  							     <td width=90 align=left><b>Primary Phone</b></td>
  							     <td width=20 align=left><b>Ext</b></td>
  							    <td width=90 align=left><b>Cell Phone</b></td>
  							     <td width=80 align=left><b>Primary Email</b></td>
  							  
						    </tr>
						    <tr>
						      
  							    <td><asp:textbox  id="l_fname" runat=server size=20 /></td>
  							    <td><asp:textbox  id="l_lname" runat=server size=25 /></td>
							    <td ><asp:textbox id="l_hphone" runat=server size=10 /></td>
							    <td ><asp:textbox id="l_hext" runat=server size=3 /></td>
  							    <td ><asp:textbox id="l_cphone" runat=server size=10 /></td>
  							    <td ><asp:textbox id="l_email" runat=server size=40 /></td>
							    
  						    </tr>
  						    <tr>
  						    <td width=100 align=left><b>Address</b></td>
  						    <td width=80 align=left><b>City</b></td>
  						    <td width=80 align=left><b>State</b></td>
  						    <td width=90 align=left><b>Zip</b></td>
  						    <td></td>
  						      <td width=100 align=left><b>Other Email</b></td>
  						   
  						    </tr>
  						    <tr>    
  				                
  				                <td><asp:textbox id="l_address" runat=server size=28 /></td>
  				                
  				                <td ><asp:textbox id="l_city" runat=server size=20 /></td>
				                
  				                <td><asp:textbox id="l_state" runat=server size=1 /></td>
				                 <td><asp:textbox id="l_zip" runat=server size=6 /></td><td></td>
  				                 <td ><asp:textbox id="l_email2" runat=server size=40 /></td>
  				                 
  				                <td width=5><asp:checkbox id="l_mailto" runat=server size=1 visible=false /></td>
				                
  							   
			                </tr>
					    </table>
			            <asp:Panel id="pnlapptmore" visible=false runat="server">
  					        <table width=60% id="appointment">
						        <tr>
  							        <td colspan=5><b>Appointment Information</b></td>
  						        </tr>
						        <tr>				
							        <td width=20></td>
  							        <td width=100 align=right>Date</td>
  							        <td ><asp:textbox id="l_appdate" runat=server size=10 /></td>
  							        <td width=100 align=right>Time</td>
  							        <td ><asp:textbox id="l_appttime" runat=server size=10 /></td>
							        <td width=120 align=right>Location</td>
  							        <td><asp:DropDownList id="dd_apptloc"  runat="server" >
    									        <asp:ListItem Value="NA" Text="NA"/>
  	    								        <asp:ListItem Value="Sterling Office" Text="Sterling Office"/>
  	    								        <asp:ListItem Value="TBD" Text="TBD"/>
  	   								        </asp:DropDownList></td>
  						        </tr>
  					        </table>
  				        </asp:panel>	
				        <asp:Panel id="pnlmore" visible=false runat="server">	
		                
			            </asp:panel>
						<table width=100% border=0 cellspacing=0 cellpadding=0 id="LeadActions">
  						    <tr>
    							<td align=right>
							 	    <table width=10% border=0 cellspacing=2 cellpadding=0>
				  					    <tr>	
				  						   <asp:Panel id="pnlplaceholderA" runat="server">
  					 						<td width=500><asp:textbox id="l_sendemailtxt" runat=server size=30 /></td>
  					 						<td><asp:button id="l_sendemailgo" runat=server text="Send" onclick="btn_emailgo" CausesValidation="False"  Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:7pt;width:50px; cursor:hand" /></td>
				 						    </asp:panel>
				 						 
				 					    </tr>
				 				    </table>
				 			    </td>
						    </tr>
					    </table>
	    	            </div>
			            </td>
		            </tr>
	            </table>
	      	<br />
		    <table border=0  width=100% cellspacing=0 cellpadding=0 id="SubNav">
			    <tr >
				    <td width="70%">
					    <table runat="Server" id="subnac" cellspacing=0 cellpadding=0 width=100%>
						    <tr height=22>
							    <td id="subnavLP" align=center width=110><asp:linkbutton id="lLP" Text= "Lead Profile"  runat="server" Font-underline="false" Style="cursor:hand" onClick="btn_LP" /> </td>
							    <td id="spacer0" class="tblcelltestc" width=1>&nbsp</td> 			           
							    <td id="subnavAcnt" visible=true align=center width=110><asp:linkbutton id="lSCA" Text= "Additional Info"  runat="server" Font-underline="false" Style="cursor:hand" onClick="btn_SC" /> </td>
							    <td id="spacer0a" class="tblcelltestc" width=1>&nbsp</td> 			           
							    
							    
							    <td id="subnavnotes" align=center width=110><asp:linkbutton id="lNotes" Text= "Notes"  runat="server" Font-underline="false" Style="cursor:hand" onClick="btn_notes" /> </td>
							    <td id="spacer1" class="tblcelltestc" width=1>&nbsp</td> 			           
							    <td id="subnavfinance" align=center width=110><asp:linkbutton id="lfinance" Text= "Financials"  runat="server" Font-underline="false" Style="cursor:hand" onClick="btn_finance" /> </td>
							    <td id="spacer1a" class="tblcelltestc" width=1>&nbsp</td> 			           
		                        <td id="subnavprofile" align=center width=110><asp:linkbutton id="lProfile" Text= "Property Profile" runat="server" Font-underline="false" Style="cursor:hand" onClick="btn_profile" /> </td>
		                        <td id="spacer2" class="tblcelltestc" width=1>&nbsp</td>
		                        <td id="subnavhistory" align=center width=110><asp:linkbutton id="lhistory" Text= "History" runat="server" Font-underline="false" Style="cursor:hand" onClick="btn_vwcontact" /> </td>
		                        <td id="spacer3" class="tblcelltestc" width=1>&nbsp</td>
		                        <td runat="Server" id="subnavtasks" align=center width=110><asp:linkbutton id="lTasks" Text= "Tasks" runat="server" Font-underline="false" Style="cursor:hand" onClick="btn_tasks" /> </td>
		                        <td id="spacer4" class="tblcelltestc" width=1>&nbsp</td>
		                        <td runat="Server" id="subexportqueue" align=center width=110><asp:linkbutton id="lexport" Text= "Export Queues" runat="server" Font-underline="false" Style="cursor:hand" onClick="btn_exportq" /> </td>
		                        <td id="spacer5" class="tblcelltestc" width=1>&nbsp</td>
		                        <td visible=false runat="Server" id="subrefer" align=center width=110><asp:linkbutton id="lReferrals" Text= "Referrals" runat="server"  Style="cursor:hand" Font-underline="false" onClick="btn_vwreferals" /> </td>
		                        <td id="spacer6" class="tblcelltestc" width=1>&nbsp</td>
		                        <td runat="Server" id="submatches" align=center width=110><asp:linkbutton id="lmatches" Text= "Matches" runat="server"  Style="cursor:hand" Font-underline="false" onClick="btn_matches" /> </td>
	    			               <td id="spacer6a" class="tblcelltestc" width=1>&nbsp</td>
		                        <td visible=false runat="Server" id="subWorkFlow" align=center width=110><asp:linkbutton id="lworkflow" Text= "Work Flows" runat="server"  Style="cursor:hand" Font-underline="false" onClick="btn_workflows" /> </td>
	    			            
	    			            
	    			            <td runat="Server" id="spacer7" class="tblcelltestc" width=350>&nbsp</td>
	    			            <td  runat="Server" class="tblcelltestc" width=150>
	    			                <table width=50% border=0  cellspacing=0 cellpadding=1>
	    			                    <tr height=22>
    			        	                <td align=right ><asp:button id="l_accept" runat=server text="Accept Lead"  onclick="btn_acceptlead" CausesValidation="False" cssclass=frmbuttons /></td>		
							                <td align=right ><asp:button id="l_save" runat=server text="Save & Submit" onclick="btn_save" CausesValidation="true"  cssclass=frmbuttons /></td>		
							                <td align=right ><asp:button id="l_savedraft" runat=server text="Save Draft" onclick="btn_savedraft" CausesValidation="False"  cssclass=frmbuttons /></td>
							                <td align=right ><asp:button id="l_saveexit" runat=server text="Save & Exit" onclick="btn_saveexit" CausesValidation="true"  cssclass=frmbuttons /></td>		
							                <td align=right ><asp:button id="l_cancel" runat=server text="Cancel" width="70" onclick="btn_cancel" CausesValidation="False"  cssclass=frmbuttons /></td>
							                <td align=right ><asp:button id="l_close" runat=server text="Close" width="70" onclick="btn_close" CausesValidation="False"  cssclass=frmbuttons /></td>
							                <td align=right ><asp:button id="l_sendemail" runat=server text="Email" width="70" visible=false onclick="btn_email" CausesValidation="False"   cssclass=frmbuttons /></td>
							                <td align=right ><asp:button id="l_print"  runat=server text="Print" width="70" onclick="btn_print" CausesValidation="False"   cssclass=frmbuttons /></td>
			    				        </tr>
			    				    </table>
			    		        </td>
          			        </tr>
      			        </table>
      		        </td>
      		       				
    		    </tr>
		    </table>
	         <table width=100% id="darea" border=0 cellspacing=0 cellpadding=0 class=tblcelltestb   >
		        <tr>
		         <asp:Panel id="pnlLP" runat="server" >
				    <td valign=top>		
				    <div style="margin:0;background: #4682B4;padding:2;"></div>
				        <asp:panel id="leadinfo" runat=server>
				        <div id="fieldtitles" style="vertical-align: top; height: 360px; ">	
  					        <table cellpadding=2 cellspacing=2 border=0 width=100%>
  					        		
  						        <tr>
  						         	<td align="left" width="225" valign=top><font size=2><b>Lead Information</b></font>&nbsp&nbsp<asp:linkbutton id="leadmore" Text= "More" 
                                 CommandArgument="leadmore" runat="server" Font-Bold="True" Font-underline="True" Style="color:#ff0000; font-family:arial; font-size:8pt; cursor:hand"
                                 visible=false onClick="LeadMoreButtonClick" /> &nbsp </td>
                                 <td width=85%>
  								        		<table cellpadding=2 cellspacing=2 border=0 width=70%>
  									        		<tr>
			  					        	 			<td width=65 align=right><b>Entered by:</b></td>
			 										    <td ><b><u><asp:label id='lblassignedby' runat=server /></u></b></td>
			   									        <td width=100 align=right><b>Assigned Status:</b></td>
			 										    <td ><b><u><asp:label id='lblstatusA' runat=server /></u></b></td>
			   									        <td width=100 align=right><b>Capture Date:</b></td>
			 										    <td ><b><u><asp:label id='lblcapdate' runat=server /></u></b></td>
			 										  </tr>
			 										</table>
	 										</td>
 									</tr>
                               </table>
                               <table width=60%>
                                <tr>
  							        <td width=100%>
  								        <table cellpadding=4 cellspacing=2 border=0 width=100%>
  									        <tr>
  										        <td ><b><u>Lead Type</u></b></td>
  										        <td ><b><u>Lead Status</u></b> &nbsp&nbsp<asp:linkbutton id="Escalate" Text= "Escalate" visible=false
                    									         runat="server" Font-Bold="True" Font-underline="True" Style="color:#ff0000; font-family:arial; font-size:8pt; cursor:hand"
                  								          onClick="Leadescalate" /></td>
                  							    <td ><b><u>Lead Program</u></b></td>
                  							      <td ><b><u>Marketing Program</u></b></td> 
                  							    <td><asp:linkbutton id="reassign" Text= "Reassign"  visible=false
                    							        CommandArgument="reassign" runat="server" Font-Bold="True" Font-underline="True" Style="color:#ff0000; font-family:arial; font-size:8pt; cursor:hand"
                    							        onClick="btn_unassign" />&nbsp&nbsp</td>	
                  						</tr>
                  					
                  						<tr>
                  							<td><asp:DropDownList id="dd_leadtype" AutoPostBack="false"
							                  		DataValueField="x_descr" 
							                  		Runat="server" />	</td>  
							                    <td><asp:DropDownList id="dd_ldstat" runat="server" AutoPostBack="True" OnSelectedIndexChanged="leadstatuschange" 
                  				                DataValueField="x_descr" Runat="server" /></td>
                  				                 <td><asp:DropDownList id="dd_leadprogram"  AutoPostBack="false"
							                  		DataValueField="x_descr" 
							                  		Runat="server" />	</td>
                  						 		<td><asp:DropDownList id="dd_mkprg" AutoPostBack="false"
							                  			DataValueField="x_descr" 
							                  			Runat="server" />	</td>  
												      <td ><asp:DropDownList id="dd_agent" runat="server" DataTextField="agent" AutoPostBack="True" OnSelectedIndexChanged="Agentchange"
					   							        DataValueField="users_tbl_pk" visible=false ></asp:DropDownList></td>	
                  						</tr>
                  					</table>
                  					<table width=60%>
                  						
                  						<tr>									    
                  								<td  align=left ><b><u>Lead Source</u></b></td>
                  								<td align=left><b><u>Ad Code</u></b></td>
                  								<td> <b><u>Market To</u></b></td>
                  						</tr>
                  						<tr>
                  						
							                    
                  				               <td ><asp:DropDownList id="dd_source"  runat="server" DataTextField="x_descr"
					   									DataValueField="tbl_xwalk_pk" ></asp:DropDownList></td>
					   									<td><asp:textbox id="l_adcode" runat=server size=10 /></td>
					   									 <td><asp:DropDownList id="dd_mkto"  runat="server" >
				    									        <asp:ListItem Value="Yes" Text="Yes"/>
				  	    								        <asp:ListItem Value="No" Text="No"/>
				  	    								  
				  	   								        </asp:DropDownList></td>
                  						</tr>
                  					</table>
                  					<table>
                  					
                    						<tr>        
  										         
					   						  <td><asp:DropDownList id="dd_highpri"  runat="server" visible=false >
												        <asp:ListItem Value="No" Text="No"/>
												        <asp:ListItem Value="Yes" Text="Yes"/>
												        </asp:DropDownList></td>
												        <td><asp:textbox id="l_comp" runat=server size=13 visible=false /></td>
												         
					   						</tr>
					   						
          							        
  									        <tr>  										
  										         
  										         <asp:Panel id="pnlleadsource" runat="server">
  										       
					   									
  								                </asp:Panel>	
  								                
  									        </tr>
  									    </table>
  								
  								        <table width=50%>
  							            <asp:Panel id="pnlleadmore" runat="server">
  									        <tr>
  										       
  										        
 										       
										        <asp:Panel id="pnlcapdate" runat="server">
  										        <td align=left>Capture Date</td>
  										        <td ><asp:textbox id="l_capdate" runat=server size=6 /></td>
  										        
  							                    </asp:Panel>		
 									        </tr>
 							            </asp:Panel>		
 								        </table>	
  							        </td>
  									
 						        </tr>
 					        </table>
 					        </div>
 	 		  	        </asp:panel>
 	 		  	      
 	 		  		
				</td>
				</asp:panel>
				
				 <asp:Panel id="pnlsc" runat="server" >
				 
				    <td valign=top><div id="fieldtitles" style="vertical-align: top; height: 360px; ">	
				    	<div style="margin:0;background: #4682B4;padding:2;"></div>
				    	<table cellpadding=1 cellspacing=1 border=0 width=30%>
 					            <tr>
 					            	<td align=center><asp:linkbutton id="epri" Text= "Additional"  runat="server" cssclass=linkbuttons onClick="btn_epri"  /></td> 
 					               <td>|</td>
 					               <td align=center><asp:linkbutton id="eco" Text= "Company Data"  runat="server" cssclass=linkbuttons onClick="btn_eco" /></td> 
 					            	 <td>|</td>
 					            	<td align=center><asp:linkbutton id="esec" Text= "Secondary Contact"  runat="server" cssclass=linkbuttons  onClick="btn_sec" /></td> 
 					            	 <td>|</td>
 					            	<td align=center><asp:linkbutton id="euser" Text= "User"  runat="server" cssclass=linkbuttons onClick="btn_usr" /></td> 
 					            
 					            </tr>
 					        </table>
				  			<hr>
				    		<asp:panel id="pnlapri" runat=server visible=false>
					    	   <table cellpadding=1 cellspacing=1 border=0  width=20% id="Address">
	  			                <tr>  				                
					                			           
	  				             
	  							    <td align=left>Fax No:</td>
	  							    <td><asp:TextBox ID="l_fax" runat=server /></td>
	  							   </tr>
	  							   <tr>
	  							    <td align=left>Web Site </td>
	  							    <td><asp:TextBox ID="l_web" runat=server /></td>
	  							    
	  				            </tr>
	  				            
				            </table>
				          </asp:panel>
				          <asp:panel id="pnlaco" runat=server visible=false>
					    	   <table cellpadding=1 cellspacing=1 border=0  width=30% id="Address">
	  			                <tr> 
	  			                	<td align=left>Title</td>
	  							    <td><asp:TextBox ID="co_title" runat=server /></td>				                
					              </tr>
					              <tr>  			           
	  				             
	  							    <td align=left>Company Name</td>
	  							    <td><asp:TextBox ID="co_name" runat=server /></td>
	  							   </tr>
	  							    <tr>  			           
	  				             
	  							    <td align=left>Company Phone</td>
	  							    <td><asp:TextBox ID="co_phone" runat=server /></td>
	  							   </tr>
	  							   <tr>
	  							    <td align=left>Web Site </td>
	  							    <td><asp:TextBox ID="co_web" runat=server /></td>
	  							    
	  				            </tr>
	  				            
				            </table>
				          </asp:panel>
				          
				          <asp:panel id="pnlSco" runat=server visible=false>
					    	   <table cellpadding=1 cellspacing=1 border=0  width=30% id="Address">
	  			                <tr> 
	  			                	<td align=left>Name</td>
	  							    <td><asp:TextBox ID="Sco_name" runat=server /></td>				                
					              </tr>
					              <tr>  			           
	  				             
	  							    <td align=left>Primary Phone</td>
	  							    <td><asp:TextBox ID="sco_phone1" runat=server /></td>
	  							   </tr>
	  							   <tr>  			           
	  				             
	  							    <td align=left>Other Phone</td>
	  							    <td><asp:TextBox ID="sco_phone2" runat=server /></td>
	  							   </tr>
					              <tr>  			           
	  				             
	  							    <td align=left>Address</td>
	  							    <td><asp:TextBox ID="sco_address" runat=server /></td>
	  							   </tr>
	  							    <tr>  			           
	  				             
	  							    <td align=left>City</td>
	  							    <td><asp:TextBox ID="sco_city" runat=server /></td>
	  							   </tr>
	  							   <tr>
	  							    <td align=left>State</td>
	  							    <td><asp:TextBox ID="sco_state" runat=server /></td>
	  							    
	  				            </tr>
	  				            <tr>
	  							    <td align=left>Zip</td>
	  							    <td><asp:TextBox ID="sco_zip" runat=server /></td>
	  							    
	  				            </tr>
	  				            
				            </table>
				          </asp:panel>
				          
				          
				          <asp:panel id="pnlusr" runat=server visible=false>
					    	   <table cellpadding=1 cellspacing=1 border=0  width=30% id="Address">
	  			                <tr> 
	  			                	<td align=left>
	  			                		<asp:linkbutton id="lbtxtuser1" Text= "User 1"  runat="server" cssclass=linkbuttons onClick="btn_usrt1"  /></td>
	  							    	<td><asp:TextBox ID="txtuser1" runat=server /></td>				                
					             </tr>	
					             <tr>
					             	<td colspan=2>
					              		<table>
					              			<tr>
							              		<td><asp:label id="lbltitle1" text="New Title:" runat=server visible=false />&nbsp&nbsp<asp:TextBox ID="txtuser1N" runat=server visible=false/></td>
							              		<td><asp:button id="btnsavenewtitle1" runat=server text=save visible=false onclick="btn_saventit1" /></td>
					              				<td><asp:button id="btnsavecancel1" runat=server text=canel visible=false onclick="btn_canceltitles" /></td>
					              			</tr>
					              		</table>
					              	</td>
					             </tr>	
					             <tr> 
	  			                	<td align=left><asp:linkbutton id="lbtxtuser2" Text= "User 2"  runat="server" cssclass=linkbuttons  onClick="btn_usrt1" /></td>
	  							    	<td><asp:TextBox ID="txtuser2" runat=server /></td>				                
					              </tr>					              
	  				            	<tr>
					              		<td colspan=2>
					              			<table><tr>
							              		<td><asp:label id="lbltitle2" text="New Title:" runat=server visible=false />&nbsp&nbsp<asp:TextBox ID="txtuser2N" runat=server visible=false/></td>
							              		<td><asp:button id="btnsavenewtitle2" runat=server text=save visible=false onclick="btn_saventit1" /></td>
					              				<td><asp:button id="btnsavecancel2" runat=server text=canel visible=false onclick="btn_canceltitles" /></td>
					              		</tr></table>
					              		</td>
					              	</tr>	
					              	<tr> 
	  			                	<td align=left><asp:linkbutton id="lbtxtuser3" Text= "User 3"  runat="server" cssclass=linkbuttons onClick="btn_usrt1"  /></td>
	  							    	<td><asp:TextBox ID="txtuser3" runat=server /></td>				                
					              </tr>					              
	  				            	<tr>
					              		<td colspan=2>
					              			<table><tr>
							              		<td><asp:label id="lbltitle3" text="New Title:" runat=server visible=false />&nbsp&nbsp<asp:TextBox ID="txtuser3N" runat=server visible=false/></td>
							              		<td><asp:button id="btnsavenewtitle3" runat=server text=save visible=false onclick="btn_saventit1" /></td>
					              				<td><asp:button id="btnsavecancel3" runat=server text=canel visible=false onclick="btn_canceltitles" /></td>
					              		</tr></table>
					              		</td>
					              	</tr>	
					              	<tr> 
	  			                	<td align=left><asp:linkbutton id="lbtxtuser4" Text= "User 4"  runat="server" cssclass=linkbuttons onClick="btn_usrt1"  /></td>
	  							    	<td><asp:TextBox ID="txtuser4" runat=server /></td>				                
					              </tr>					              
	  				            	<tr>
					              		<td colspan=2>
					              			<table><tr>
							              		<td><asp:label id="lbltitle4" text="New Title:" runat=server visible=false />&nbsp&nbsp<asp:TextBox ID="txtuser4N" runat=server visible=false/></td>
							              		<td><asp:button id="btnsavenewtitle4" runat=server text=save visible=false onclick="btn_saventit1" /></td>
					              				<td><asp:button id="btnsavecancel4" runat=server text=canel visible=false onclick="btn_canceltitles" /></td>
					              		</tr></table>
					              		</td>
					              	</tr>	
					              	<tr> 
	  			                	<td align=left><asp:linkbutton id="lbtxtuser5" Text= "User 5"  runat="server" cssclass=linkbuttons  onClick="btn_usrt1" /></td>
	  							    	<td><asp:TextBox ID="txtuser5" runat=server /></td>				                
					              </tr>					              
	  				            	<tr>
					              		<td colspan=2>
					              			<table><tr>
							              		<td><asp:label id="lbltitle5" text="New Title:" runat=server visible=false />&nbsp&nbsp<asp:TextBox ID="txtuser5N" runat=server visible=false/></td>
							              		<td><asp:button id="btnsavenewtitle5" runat=server text=save visible=false onclick="btn_saventit1" /></td>
					              				<td><asp:button id="btnsavecancel5" runat=server text=canel visible=false onclick="btn_canceltitles" /></td>
					              		</tr></table>
					              		</td>
					              	</tr>	
				            </table>
				          </asp:panel></div>
				    </td>
				</asp:panel>	
				
				
				
				
				
				
				
			    <asp:Panel id="pnlinitialnotes" runat="server" >	
				<td valign=top>	<div id="Div1" style="vertical-align: top; height: 360px; ">
				<div style="margin:0;background: #4682B4;padding:2;"></div>	
				    <table width=100%>
					    <tr>
						    <td  valign=top>
							    <table cellpadding=2 cellspacing=2 border=0  width=100%>
  								    <tr>
		        						<td>	<ed:Editor ShowQuickFormat="false" FixedToolbar="false"  AutoFocus="false" height="400" width="1100" submit="false" PathPrefix="Editor_data/" FlashBrowse="myFlashBrowse.aspx" MediaBrowse="myMediaBrowse.aspx" ImageBrowse="Editor_data/myImageBrowseClient.aspx" UrlBrowse="myUrlBrowse.aspx" id="l_notes" PreviewMode="true" runat="server">
					            			</ed:Editor>
					            	</td>
									</tr>
									</table>	
							</td>
						</tr>
					</table></div>
				</td>
				</asp:panel>
				<asp:Panel id="pnlviewnotes" visible="false" runat="server">
  				<td valign=top><div id="Div2" style="vertical-align: top; height: 360px; ">
  				<div style="margin:0;background: #4682B4;padding:2;"></div>
  					<table>
  					    <tr>
				            <td align=left width=10><asp:button id="l_addcontact" runat=server text="Add" onclick="btn_addcontact" CausesValidation="False" cssclass=frmbuttons />	</td>
 			           		<td ><asp:DropDownList ID="ddLHTypeH"  autopostback=true  OnSelectedIndexChanged="filterhistory"               		
                  		    DataValueField="x_descr" 
                  		    Runat="server" /> </td>	
 			            </tr>
				    </table>
				    <table border=0 width=100% id="Lead History">
  						<tr>
  							<td >	<div style="vertical-align top; height: 310px; overflow:auto;">
  						  			<asp:DataGrid 
										ID="lead_history" 
      								AutoGenerateColumns=False
      								Width="100%"						          
      								AllowPaging="false" CssClass="dg" Runat=server>									
      										<HeaderStyle CssClass="dgheaders" />
							        			<ItemStyle CssClass="dgitems" />
							        			<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>
   							 
        									 <Columns >
        								
        									 <asp:TemplateColumn HeaderText="History #" ItemStyle-Width="50px" ItemStyle-CssClass="dglinks" >
												<ItemTemplate >
													<asp:Hyperlink runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"tbl_leadcnthistory_pk")%>' NavigateUrl=<%# "leadhistory.aspx?type=history&history=" + DataBinder.Eval(Container.DataItem,"hnum")+ "&LeadNo=" + DataBinder.Eval(Container.DataItem,"lnum2")+ "&action=view&source=history" %>   ID="Hyperlink1" NAME="Hyperlink1"  />
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn HeaderText="Entity"  DataField="cnt_who" ItemStyle-Width="80px"    />
											
											<asp:BoundColumn HeaderText="Type"  DataField="cnt_type" ItemStyle-Width="80px"   />
											<asp:BoundColumn HeaderText="Date"  DataField="cnt_date" ItemStyle-Width="80px"   />
											<asp:BoundColumn HeaderText="Task #"  DataField="cnt_leadtask" ItemStyle-Width="60px"   />
										    <asp:BoundColumn HeaderText="Description"  DataField="briefnotes" visible=false ItemStyle-Width="300px"   />
										<asp:BoundColumn HeaderText="User"  DataField="cnt_agentid" ItemStyle-Width="100px"   />
													
        	 							
											</Columns>
											</asp:DataGrid>
										</div>
							</td>
						</tr>	
		            </table></div>
                </td>
                </asp:panel>
                <asp:Panel id="pnltasks" runat="server" Visible=false>	
                <td valign=top><div id="Div3" style="vertical-align: top; height: 360px; ">
                <div style="margin:0;background: #4682B4;padding:2;"></div>
  					 <table>
  					    <tr>
				            <td align=left width=10><asp:button id="btnaddtask" runat=server text="Add" onclick="click_addtask" CausesValidation="False" cssclass=frmbuttons />	</td>
 			            </tr>
				    </table>
				    <table border=0 width=100% id="tasks">
  						<tr>
  							<td >	
  							<div style="vertical-align top; height: 220px; overflow:auto;">
					  			<asp:DataGrid 
									ID="dgtasks" 
  									AutoGenerateColumns=False
  									Width="100%"
					            ColumnHeadersVisible = FALSE
  									AllowPaging="false" CssClass="dg"  Runat=server>
  										<HeaderStyle CssClass="dgheaders" />
					        			<ItemStyle CssClass="dgitems" />
					        			<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>
								
								        <Columns >
   							        	 <asp:TemplateColumn HeaderText="Task #" ItemStyle-Width="50px" ItemStyle-CssClass="dglinks" >
												<ItemTemplate >
													<asp:Hyperlink runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"ldtaskpk")%>' NavigateUrl=<%# "leadhistory.aspx?type=task&task=" + DataBinder.Eval(Container.DataItem,"ldtaskpk")+ "&LeadNo=" + DataBinder.Eval(Container.DataItem,"leadpk")+ "&action=view&source=tasks" %>   ID="Hyperlink1" NAME="Hyperlink1"  />
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:BoundColumn HeaderText="Type"  DataField="lt_type"   />											
											<asp:BoundColumn HeaderText="Status"  DataField="lt_status"   />
											<asp:BoundColumn HeaderText="Description"  DataField="briefnotes"   />
											<asp:BoundColumn HeaderText="due date"  DataField="duedate"   />					
											
											<asp:BoundColumn HeaderText="User"  DataField="lt_uid"   />
											
									</Columns>
								</asp:DataGrid>
						    </div>
							</td>
						</tr>	
		            </table></div>	
				 </td>
                </asp:Panel>
                
  			    <asp:Panel id="pnladdencoutner" runat="server">	
		        <td>
			        <table>
			            <tr>
				            <td><b>Add Entry:</b></td>
			            </tr>
			        </table>
			        <table valign=top>
			            <tr>
  				            <td width=50 align=right valign=top>Date:</td>
  				            <td valign=top><asp:textbox id="l_edate" runat=server size=8 /></td>
  				            <td width=90 align=right valign=top>Type:</td>
				            <td valign=top><asp:DropDownList ID="ddLHType"                 		
                  		            DataValueField="x_descr" 
                  		            Runat="server" /> 				
  				            <td valign=top>Notes</td>
  				            <td valign=top><asp:textbox id="l_enotes" runat=server size=16 TextMode="MultiLine" Columns="80"  Rows="5" /></td>
  			            </tr>
			        </table>
	 		        <table width=20% border=0>
  				        <tr>	
  					        <td align=left ><asp:button id="l_savecontact" runat=server text="Save" width="100" onclick="btn_savecontact" CausesValidation="False" Style="background-color:steelblue; color:#FFFFFF; font-family:arial;
  														        font-size:8pt;	width:80px; cursor:hand" /></td>		
  					        <td align=left ><asp:button id="l_cancelcontact" runat=server text="Cancel" width="100" onclick="btn_cancelcontact" CausesValidation="False" Style="background-color:steelblue; color:#FFFFFF; font-family:arial;
  														        font-size:8pt;	width:80px; cursor:hand" /></td>		
  				        </tr>
  			        </table>
	            </td>
	            </asp:panel>
	            <asp:Panel id="pnladdreferal" visible="false" runat="server">
  				<td valign=top><div id="Div4" style="vertical-align: top; height: 360px; ">
  				<div style="margin:0;background: #4682B4;padding:2;"></div>
  				    <table border=0 width=100%>
  					    <tr>
						    <td><asp:button id="l_addreferal" runat=server text="Add" width="85" onclick="btn_addrefer" CausesValidation="False" Style="background-color:steelblue; color:#FFFFFF; font-family:arial;
  									font-size:8pt;	width:80px; cursor:hand" />
  							</td>
  						</tr>
  						<tr>
  						    <td >
  						        <div style="vertical-align top; height: 220px; overflow:auto;">
			 					    <asp:DataGrid 
									    ID="referals" 
            							AutoGenerateColumns=False
            							Width="100%"
  								        	ColumnHeadersVisible = FALSE 
  								        	AllowPaging="false" CssClass="dg"  Runat=server>
            								<HeaderStyle CssClass="dgheaders" />
					        					<ItemStyle CssClass="dgitems" />
					        					<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>
           									 <Columns >
          										<asp:BoundColumn HeaderText="Referral#"  DataField="tbl_referals_pk" ItemStyle-Width="50px"    />
        	 										<asp:BoundColumn HeaderText="Referal Type"  DataField="refer_type" ItemStyle-Width="80px"    />
        	 										
        	 										<asp:BoundColumn HeaderText="Refered To"  DataField="Refer_company" ItemStyle-Width="120px"    />
        	 										<asp:BoundColumn HeaderText="Refered Date"  DataField="Date" ItemStyle-Width="80px"    />
        	 										<asp:BoundColumn HeaderText="Refered Note"  DataField="refer_note" ItemStyle-Width="100px"    />
        	 										<asp:BoundColumn HeaderText="Refered By"  DataField="refer_referby" ItemStyle-Width="100px"    />
												</Columns>
											</asp:DataGrid>
								</div>
							</td>
						</tr>
					</table></div>
  				</td>
  				</asp:panel>	
  				<asp:Panel id="pnladdreferalADD" runat="server">
  				<td valign=top >
  				    <table>
  					    <tr>
  						    <td width=90><b>Add Referral</b></td>
  						</tr>
  						<tr>
  						    <td>Type</td>
  							<td >Vendor</td>
  							<asp:Panel id="pnladdreferalADDOther1" runat="server">
  							<td>Other Email</td>
  							<td>Other Note</td>
  							</asp:Panel>
  						</tr>
  						<tr>
  							<td><asp:DropDownList id="dd_refertype"  runat="server" DataTextField="x_descr"
					   				DataValueField="tbl_xwalk_pk"  autoPostBack="True" OnSelectedIndexChanged="dorefervendor" ></asp:DropDownList></td>
					   		<td colspan=2><asp:DropDownList id="dd_refervendor"  runat="server" DataTextField="cus_company"
					   				DataValueField="tbl_customers_pk" ></asp:DropDownList></td>
							<asp:Panel id="pnladdreferalADDOther" runat="server">
  							<td><asp:textbox id="l_referalother" runat=server size=20 /></td>
 							<td><asp:textbox id="l_referalothernote" runat=server size=20 /></td>
 							</asp:panel>
 						</tr>
 						<tr>
  							<td width=90><asp:button id="l_addreferaAdd" runat=server text="Save" width="85" onclick="btn_saverefer" CausesValidation="False" Style="background-color:steelblue; color:#FFFFFF; font-family:arial;
  									font-size:8pt;	width:80px; cursor:hand"  /></td>
  							<td width=90><asp:button id="l_addreferalcancel" runat=server text="Cancel" width="85" onclick="btn_cancelrefer" CausesValidation="False" Style="background-color:steelblue; color:#FFFFFF; font-family:arial;
  									font-size:8pt;	width:80px; cursor:hand"  /></td>
  						</tr>
  					</table>
  				</td>
  				</asp:panel>
  				<asp:Panel id="pnlprofile" visible="false" runat="server">
  				<td valign=top>
  				    <table width=10%>
  					    <tr>
  						    <td align=left width=10><asp:button id="l_addprofile" runat=server text="Add" onclick="btn_addprofile" CausesValidation="False" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;	width:80px; cursor:hand" />	</td>
 							<td align=left width=10><asp:button id="l_srchcriteria" runat=server text="Criteria" onclick="btn_srchcriteria" CausesValidation="False" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;	width:80px; cursor:hand" />	</td>
							<td align=left width=10><asp:button id="l_automatch" runat=server text="Auto Match OFF" onclick="btn_automatch" CausesValidation="False" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;	width:90px; cursor:hand" />	</td>
						</tr>
					</table>
  					<table border=0 width=100%>
  					    <tr>
  						    <td >
  						        <div style="vertical-align top; height: 200px; overflow:auto;">
			 					<asp:DataGrid 
									ID="DGprofile" 
            						AutoGenerateColumns=False
            						Width="100%"
  								    ColumnHeadersVisible = FALSE  
            					    ItemStyle-BackColor=white
            						ItemStyle-Font-Name="arial"
            						ItemStyle-Font-Size="24px"
            						BorderColor="#ffffff"
            						AllowPaging="false"  Runat=server>
									<HeaderStyle Font-Size="24px" Font-Bold="True" BackColor="steelblue"></HeaderStyle>
            							 <Columns >
           									 <asp:TemplateColumn HeaderText="Profile #" ItemStyle-Width="30px"  >
													<ItemTemplate >
													<asp:Hyperlink runat="server" Text='<%#DataBinder.Eval(Container.DataItem,"tbl_lead_profile_pk")%>' NavigateUrl=<%# databinder.eval(container.dataitem,"url")+ "?profile=" + DataBinder.Eval(Container.DataItem,"pnum")+ "&LeadNo=" + DataBinder.Eval(Container.DataItem,"lnum")+ "&LeadType=" + DataBinder.Eval(Container.DataItem,"ld_type")+ "&action=view" %>   ID="Hyperlink12" NAME="Hyperlink12"  />
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn HeaderText="Profile Type"  DataField="lp_type" ItemStyle-Width="50px"    />
        	 										<asp:BoundColumn HeaderText="Bedrooms"  DataField="lp_num_bed" ItemStyle-Width="3px"    />
        	 										<asp:BoundColumn HeaderText="Bath"  DataField="lp_num_bath" ItemStyle-Width="3px"    />
        	 										<asp:BoundColumn HeaderText="City"  DataField="lp_city" ItemStyle-Width="80px"    />
        	 										<asp:BoundColumn HeaderText="Min Rent"  DataField="lp_rent_amt_min" ItemStyle-Width="2px"    />
        	 										<asp:BoundColumn HeaderText="Max Rent"  DataField="lp_rent_amt_max" ItemStyle-Width="2px"    />
        	 									
												</Columns>
									</asp:DataGrid>
								</div>
							</td>
						</tr>
					</table>
  				</td>
  				</asp:panel>
				<asp:Panel id="pnlmatch" visible=false runat="server">
				<td valign=top>
				    <table width=60%>
				        <tr>
						    <td colspan=32>Search Parameters</td>
						</tr>
						<tr>
							<td width=30></td>
							<td align=right>City</td>
							<td><asp:checkbox id="chkcity" runat=server /></td>
							<td align=right>Credit</td>
							<td ><asp:checkbox id="chkcredit" runat=server /></td>
							<td align=right>Move Date</td>
							<td><asp:checkbox id="chkmovedt" runat=server /></td>
							<td align=right>Rent</td>
							<td><asp:checkbox id="chkrent" runat=server /></td>
							<td align=right>Section 8</td>
							<td><asp:checkbox id="chksec8" runat=server /></td>
						</tr>
						<tr>	
							<td width=30></td>
							<td align=right>Bedroom</td>
							<td><asp:checkbox id="chkbed" runat=server /></td>
							<td align=right>Bath</td>
							<td><asp:checkbox id="chkbath" runat=server /></td>
							<td align=right>Levels</td>
							<td><asp:checkbox id="chklevel" runat=server /></td>
							<td align=right>School District</td>
					    	<td><asp:checkbox id="chkschool" runat=server /></td>
						</tr>
					</table>
					<table width=10%>
					    <tr>
						    <td align=left width=10><asp:button id="l_savesrch" runat=server text="Save" onclick="btn_savesrch" CausesValidation="False" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;	width:80px; cursor:hand" />	</td>
							<td align=left width=10><asp:button id="l_srchexit" runat=server text="Exit" onclick="btn_srchexit" CausesValidation="False" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;	width:80px; cursor:hand" />	</td>
						</tr>
					</table>
				</td>
				</asp:panel>					
				<asp:Panel id="pnlfinancials" runat="server">
  				<td valign=top >
  					<table>
  					    <tr>
  						    <td>A<asp:label id='lblnofinancials' runat=server /></td>
  						</tr>
  					</table>
  				</td>
  			    </asp:panel>
  				<asp:Panel id="pnlpropmatch" visible=false runat="server">
				<td valign=top > 
					<table width=99% >
					    <tr>
						    <td  >
						        <div style="vertical-align top; height: 200px; overflow:auto;">
			 						<asp:DataGrid 
										ID="DGPropMatches" 
            						AutoGenerateColumns=False
            						Width="100%"
  								   	 ColumnHeadersVisible = FALSE  
            					    ItemStyle-BackColor=white
            						ItemStyle-Font-Name="arial"
            						ItemStyle-Font-Size="24px"
            						BorderColor="#ffffff"
            						DataKeyField="tbl_propmatch_pk"
       								AllowPaging="false"  Runat=server>
       									<HeaderStyle CssClass="dgheaders" />
					        					<ItemStyle CssClass="dgitems" />
					        					<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>
					 
           									 <Columns >
           									    <asp:TemplateColumn HeaderText="Customer">
														<ItemTemplate>
														<asp:CheckBox ID="myCheckbox" Runat="server" AutoPostBack=True OnCheckedChanged="GetSelections_Click" />
														</ItemTemplate>
														</asp:TemplateColumn>
														<asp:boundcolumn DataField="tbl_propmatch_pk" HeaderText="ID" Visible="False" />   
           									    <asp:TemplateColumn HeaderText="Name" ItemStyle-Width="30px"  >
														<ItemTemplate >
															<asp:Hyperlink runat="server" Text='<%# databinder.eval(container.dataitem,"pm_landlordlname") %>'   NavigateUrl=<%# databinder.eval(container.dataitem,"url")+ "?profile=" + databinder.eval(container.dataitem,"pnum") + "&LeadNo=" + DataBinder.Eval(Container.DataItem,"lannum")+ "&LeadType=Landlord&action=view"   %>   ID="HyperlinkAA" NAME="HyperlinkAA" target="_blank" />
														</ItemTemplate>
													</asp:TemplateColumn>
           										<asp:BoundColumn HeaderText="Lead#"  DataField="pm_landlordleadno" ItemStyle-Width="3px"    />
        	 										<asp:BoundColumn HeaderText="Prop Address"  DataField="lp_address" ItemStyle-Width="3px"    />
        	 										<asp:BoundColumn HeaderText="City"  DataField="lp_city" ItemStyle-Width="80px"    />
        	 										<asp:BoundColumn HeaderText="Job#"  DataField="pm_jobnum" ItemStyle-Width="2px"    />
        	 										<asp:BoundColumn HeaderText="Match Date"  DataField="pm_rundate" ItemStyle-Width="2px"    />
        	 										
        	 										<asp:TemplateColumn HeaderText="Command" ItemStyle-Width="10px"  >
														<ItemTemplate >
														<table><tr><td>
															<asp:Hyperlink runat="server" Text='USE' NavigateUrl=<%# "leadmatchworkflow.aspx?profile=" + databinder.eval(container.dataitem,"pnum") + "&Tenleadnum=" + DataBinder.Eval(Container.DataItem,"tennum")+ "&Lanleadnum=" + DataBinder.Eval(Container.DataItem,"lannum")+ "&source=matches"   %>   ID="HyperlinkAZ" NAME="HyperlinkAZ"  />
														</td>
														<td><asp:linkbutton id="cmdDel" runat="server">Delete</asp:linkbutton></td></tr></table>
														</ItemTemplate>
													</asp:TemplateColumn>	
													

									
												</Columns>
								</asp:DataGrid>
							    </div>
						    </td>
					    </tr>
				    </table>
				</td>
				</asp:panel>	
				
				 <asp:Panel id="pnlexportque" runat="server" Visible=false>	
                <td valign=top><div id="Div5" style="vertical-align: top; height: 360px; ">
  					<div style="margin:0;background: #4682B4;padding:2;"></div>
				    <table border=0 width=100% id="Table1">
  						<tr>
  							<td >	
  							<div style="vertical-align top; height: 220px; overflow:auto;">
					  				<asp:DataGrid 
									ID="exportqueue" 
  									AutoGenerateColumns=False
  									Width="100%"
					            ColumnHeadersVisible = FALSE  
  									AllowPaging="false"  CssClass="dg" Runat=server>
										<HeaderStyle CssClass="dgheaders" />
					        			<ItemStyle CssClass="dgitems" />
					        			<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>        
					        			<Columns >
				        		      	<asp:BoundColumn HeaderText="ID"  DataField="eqd_tbl_pk" visible=false ItemStyle-Width="80px"    />
				        		       	<asp:BoundColumn HeaderText="Queue Name"  DataField="eq_name" ItemStyle-Width="80px"    />
				        		        	<asp:BoundColumn HeaderText="Queue Description"  DataField="eq_description" ItemStyle-Width="3px"    />
 											<asp:BoundColumn HeaderText="Create Date"  DataField="eq_createdate" ItemStyle-Width="80px"    />
 										  	<asp:TemplateColumn HeaderText="Action" ItemStyle-Width="100px" ItemStyle-CssClass="dgitemsNOBD"  >
					                        <ItemTemplate >
					                            <table width=100%>
					                                <tr>
					                                    <td><asp:button id="removeq" runat=server text="Remove" onclick="removeven" CausesValidation="false" cssclass=frmbuttons /></td>		
			                                        </tr>    
					                            </table>   
					                        </ItemTemplate>                                                     
				                     </asp:TemplateColumn>								
										</Columns>
									</asp:DataGrid>
						    </div>
							</td>
						</tr>	
		            </table></div>	
				 </td>
                </asp:Panel>
                
				 <asp:Panel id="pnlworkflow" runat="server" Visible=false>	
                <td valign=top><div id="Div6" style="vertical-align: top; height: 360px; ">
  						<div style="margin:0;background: #4682B4;padding:2;"></div>
  						<asp:panel id=wrkflowmain runat=server visible=true >
  						<table width=100%>
  							<tr>	
	  							<td width=300 class=wfstepheaderARED><b>Work Flows this lead is in</b></td>
	  							<td></td>
	  							<td>Lead Status</td>
	  							 <td width=55%><asp:DropDownList id="dd_WFStatFilter"  runat="server" autopostback=true OnSelectedIndexChanged="filterworkflows" >
							        <asp:ListItem Value="Active" Text="Active"/>
							        <asp:ListItem Value="Inactive" Text="Inactive"/>
							         <asp:ListItem Value="All" Text="All"/>
							  
							        </asp:DropDownList></td>
	  							
	  							<td><asp:button id="addtoWF" runat=server text="Add To A WorkFlow" onclick="AddWFA" CausesValidation="false" cssclass=frmbuttonsXLG /></td>		
			                          				
  							
  							</tr>
  						</table>
				    	<table border=0 width=100% id="Table1">
  							<tr>
  								<td >	
  								<div style="vertical-align top; height: 220px; overflow:auto;">
					  				<asp:DataGrid 
									ID="workflow" 
  									AutoGenerateColumns=False
  									Width="100%"
					            ColumnHeadersVisible = FALSE
					            OnItemDataBound="ItemDataBoundEventHandlerWF"  
  									AllowPaging="false"  CssClass="dg" Runat=server>
										<HeaderStyle CssClass="dgheaders" />
					        			<ItemStyle CssClass="dgitems" />
					        			<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>        
					        			<Columns >
				        		      	<asp:BoundColumn HeaderText="WF #"  DataField="lwfs_wfm_fk" visible=true ItemStyle-Width="80px"    />
				        		       	<asp:BoundColumn HeaderText="WF Name"  DataField="wfm_name" ItemStyle-Width="120px"    />
				        		       	<asp:BoundColumn HeaderText="Lead Stat"  DataField="lwfs_leadststatus" ItemStyle-Width="80px"    />
				        		       	<asp:BoundColumn HeaderText="WorkFlow Stat"  DataField="wfm_status" ItemStyle-Width="80px"    />
				        		       	<asp:BoundColumn HeaderText="Effective Date"  DataField="WFEffdate" ItemStyle-Width="135px"    />
 											<asp:BoundColumn HeaderText="End Date"  DataField="WFEnddate" ItemStyle-Width="135px"    />
 										  	<asp:TemplateColumn HeaderText="Actions" ItemStyle-Width="80px" ItemStyle-CssClass="dgitemsNOBD"  >
					                        <ItemTemplate >
					                            <table width=100% cellspacing=1 cellpadding=1>
					                                <tr>
					                                    <td><asp:button id="viewwfdetails" runat=server text="View Details" onclick="vwfdetails" CausesValidation="false" cssclass=frmbuttons /></td>		
			                                          <td><asp:button id="wfermove" runat=server text="Remove" onclick="removewf" CausesValidation="false" cssclass=frmbuttons /></td>		
			                          				  </tr>    
					                            </table>   
					                        </ItemTemplate>                                                     
				                     </asp:TemplateColumn>					
										</Columns>
									</asp:DataGrid>
						    	</div>
								</td>
							</tr>	
		            </table>
		            </asp:panel >
		            
		            <asp:panel id=pnladd2workflow runat=server visible=false>
		            	<table width=100%>
		            		<tr>
		            			<td class=wfstepheaderARED>Add Lead to Active Work Flow</td>
		            			<td align=right><asp:button id="btnexitaddwf" runat=server text="Exit" onclick="exitwfadd" CausesValidation="false" cssclass=frmbuttons /></td>		
			                                  
		            		</tr>
		            		<tr>
		            			<td><b>Active Workflows</b></td>
		            		</tr>
		            	</table>
		            	<table border=0 width=100% id="Table1">
  							<tr>
  								<td >	
  								<div style="vertical-align top; height: 200px; overflow:auto;">
					  				<asp:DataGrid 
									ID="workflowView" 
  									AutoGenerateColumns=False
  									Width="100%"
					            ColumnHeadersVisible = FALSE
					            
  									AllowPaging="false"  CssClass="dg" Runat=server>
										<HeaderStyle CssClass="dgheaders" />
					        			<ItemStyle CssClass="dgitems" />
					        			<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>        
					        			<Columns >
				        		      	<asp:BoundColumn HeaderText="WF #"  DataField="wfm_tbl_pk" visible=true ItemStyle-Width="40px"    />
				        		       	<asp:BoundColumn HeaderText="Name"  DataField="wfm_name" ItemStyle-Width="120px"    />
				        		       	<asp:BoundColumn HeaderText="Description"  DataField="wfm_descript" ItemStyle-Width="220px"    />
				        		       	<asp:BoundColumn HeaderText="Trigger"  DataField="wfm_trigger" ItemStyle-Width="130px"    />
				        		       	<asp:BoundColumn HeaderText="Effective Date"  DataField="WFEffdate" ItemStyle-Width="80px"    />
 											<asp:BoundColumn HeaderText="End Date"  DataField="WFEnddate" ItemStyle-Width="60px"    />
									  		<asp:TemplateColumn HeaderText="Actions" ItemStyle-Width="60px" ItemStyle-CssClass="dgitemsNOBD"  >
				                        <ItemTemplate >
				                            <table width=100% cellspacing=1 cellpadding=1>
				                                <tr>
				                                    <td><asp:button id="wfAdd" runat=server text="Add" onclick="AddWF" CausesValidation="false" cssclass=frmbuttons /></td>		
		                          				  </tr>    
				                            </table>   
				                        </ItemTemplate>                                                     
				                     </asp:TemplateColumn>			
										</Columns>
									</asp:DataGrid>
						    	</div>
								</td>
							</tr>	
		            </table>
		            
		            </asp:panel>
		            
		       	</div>	
				 </td>
                </asp:Panel>
                
                
                
                
                
                
                
                
								
		    </tr>
  	    </table>
		   
  			<asp:Panel id="pnlack" runat="server">	
		        <table width=80% border=0>
  				    <tr>	
  					    <td align=left >Your request has been completed.</td>	
  				    </tr>
  				    <tr>		
  					    <td align=left ><asp:button id="l_continue" runat=server text="Continue" width="70" onclick="btn_continue" CausesValidation="False" /></td>
 				    </tr>
 			    </table>
			</asp:panel>
		</form>
	</body>
</HTML>
	