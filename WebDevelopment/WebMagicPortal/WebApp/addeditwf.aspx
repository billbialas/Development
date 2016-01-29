<%@ Page language="vb" Codebehind="addeditwf.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.addeditwf" Debug="false" trace="false" enableEventValidation="false"  validateRequest="false"  %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %> 
<%@ Register TagPrefix="ed" Namespace="OboutInc.Editor" Assembly="obout_Editor" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<html xmlns="http://www.w3.org/1999/xhtml">
<script language="JavaScript" src="../_include/default.js"></script>
<link rel="stylesheet" href="../_include/default.css" type="text/css">
<script type="text/javascript">

// toolbar status variable
var toolbarIsShown = true;

function toggleToolbar()
{
  // get client-side object
  var editor = oboutGetEditor("emailbody");

  if(editor.mode() != "html") return;

  // save original 'mode' function
  var modeSaved = editor.mode;

  // set our 'mode' function
  editor.mode = function () { return toolbarIsShown?"text":"html"; };

  // top toolbar 'chMode' function
  var chm = editor.clientID+"_top_chMode()";

  // call it
  eval(chm);

  // change our status variable
  toolbarIsShown = !toolbarIsShown;

  // restore 'mode' function
  editor.mode = modeSaved;

  // try to resize editing panel
  editor._onresize();
}

// dummy 'chMode' function
function newChMode(newMode,afterFunction)
{
  // switching to 'design' mode
  if(newMode=="html")
  {
    if(!toolbarIsShown) // don't show toolbar's buttons now
    {
      toolbarIsShown = true;
      window.savedChMode(newMode,function(){toggleToolbar();});
      return;
    }
  }
  window.savedChMode(newMode,afterFunction);
}

// when Editor is loaded
function EditorOnLoad(editor)
{
  // main 'chMode' function name
  var chm = editor.clientID+"_chMode";

  //save original
  window.savedChMode = eval(chm);

  // set our dummy function
  eval("window."+chm+" = function(newMode,afterFunction){newChMode(newMode,afterFunction);}");

  // hide toolbar buttons initially
  toolbarIsShown = false;
  toggleToolbar();
}
</script>
<HTML>
	<HEAD>
		<title>www.WebMagicPortal.com</title>
	</HEAD>
	
<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">
	<form id="forms1a" runat="server" enctype="multipart/form-data" method="post">
		<table cellpadding=0 cellspacing=0 border=0 width=100% id="HeaderLD">
	   	    <tr>  				                
                <td class=pgheaders width=18%>Add/Edit Workflows</td>
            </tr>
 		</table>
 		<table border=0  width=100% cellspacing=0 cellpadding=0 id="SubNav">
			<tr >
				<td width="100%">
				    <table runat="Server" id="subnac" cellspacing=0 cellpadding=0 width=100% border=0>
						<tr height=22>
							<td id="subnavGen" align=center width=110><asp:linkbutton id="Lgen" Text= "General Setup"  runat="server" Font-underline="false" Style="cursor:hand" onClick="btn_Gen" /> </td>
							<td id="spacer0" class="tblcelltestc" width=1>&nbsp</td> 			           
							<td id="subnavPage1" align=center width=110><asp:linkbutton id="lpage1" Text= "Steps"  runat="server" Font-underline="false" Style="cursor:hand" onClick="btn_pg1" /> </td>
							<td id="spacer1" class="tblcelltestc" width=1>&nbsp</td> 			           
							<td id="subnavPage2" align=center width=110><asp:linkbutton id="lpage2" Text= "Leads"  runat="server" Font-underline="false" Style="cursor:hand" onClick="btn_pg2" /> </td>
							<td id="spacer2" class="tblcelltestc" width=1>&nbsp</td> 			           
							<td  runat="Server" class="tblcelltestc" width=800>
		               	        <table width=100% border=0  cellspacing=0 cellpadding=1>
		               		        <tr height=22>
		               		        				<td width=90%></td>
		               				   		   <td align=left><asp:button onclick="SaveWFGen" id="bsaveGen" runat=server text="Save"  CausesValidation="false" cssclass=frmbuttonsXLG /></td>
															<td align=left><asp:button onclick="SaveWFGen" id="bsaveGenAddS" runat=server text="Save & Add Step"  CausesValidation="false" cssclass=frmbuttonsXLG /></td>
															<td align=left><asp:button onclick="MakeActiveWFGen" id="bwfactive" runat=server text="Make Active"  CausesValidation="false" cssclass=frmbuttonsXLG /></td>
															<td align=left><asp:button onclick="CancelWFGen" id="bwfcancel" runat=server text="Cancel"  CausesValidation="false" cssclass=frmbuttonsXLG /></td>
															<td align=left><asp:button onclick="CloneWFGen" id="bwfclone" runat=server text="Clone"  CausesValidation="false" cssclass=frmbuttonsXLG /></td>
									   		</tr>           	
		               	        </table>
		                    </td>
 			        	</tr>
 			        </table>
 			        <table class=tblcelltestb width=100% cellpadding=0 cellspacing=0>
 			      	    <tr>
 			      		    <td>
 			      		 	    <asp:panel id="pnlgeneral" runat=server visible=false >
 			      		 	        <div style="margin:0;background: #4682B4;padding:1;"></div>
 			       				    <div id="fieldtitles" style="vertical-align: top; height: 480px;margin:5; ">
 			       					    <table cellpadding=2 cellspacing=2 width=40%>
					 						<tr>
					 							<td width=90><b>Status</b></td>
					 							<td><b>Name</b></td>
					 							
					 							<td><b>Description</b></td>
					 						</tr>
					 						<tr>
					 							<td><asp:DropDownList id="dd_wfstat" runat="server" enabled=false >
    							                 	<asp:ListItem Value="Inactive" Text="Inactive"/>
    							                 	<asp:ListItem Value="Active" Text="Active"/>
  	    						                	
 								                	</asp:DropDownList></td>				
					 							<td><asp:TextBox ID="txtname" runat=server size=60 autopostback=true ontextchanged="namecheckdup" /></td>
											
					 							<td><asp:TextBox ID="txtdesc" runat=server size=60 /></td>
											</tr>
										</table>
										<table width=80% border=0>
								 			<tr>
								 				<td  valign=top>
								 					<table width=100% border=0 style="margin-bottom:6;">
								 						<tr>
								 							<td valign=top >
										 						
										 						
										 						<table width=100%>
											 						<tr>
											 							<td><b>Trigger</b></td>
											 							<td ><b><asp:label visible=false id=lbllsfrom runat=server text="Update From" /></b></td>
							 											<td><b><asp:label visible=false id=lbllsto runat=server text="Update To" /></b></td>
													 		         <td><asp:linkbutton id="showcal1" Text= "Start Date" CausesValidation="false"  runat="server" cssclass=linkbuttonsRed  visible =true onClick="showcalendar" /> </td>
														 				<td><asp:linkbutton id="showcal2" Text= "End Date" CausesValidation="false"  runat="server" cssclass=linkbuttonsRed  visible =true onClick="showcalendar" /> </td>
											 						</tr>
																	<tr>
																		<td><asp:DropDownList id="dd_trigger" runat="server"  AutoPostBack="true" onselectedindexchanged="showldfromto" >
						    							                 	<asp:ListItem Value="Select.." Text="Select.."/>
						    							                 	<asp:ListItem Value="On New Lead" Text="On New Lead"/>
						  	    						                	<asp:ListItem Value="On Lead Type Change" Text="On Lead Type Change"/>  	    						               
						  	    					                		<asp:ListItem Value="On Lead Program Change" Text="On Lead Program Change"/>  	    						               
						  	    					                		<asp:ListItem Value="On Marketing Program Change" Text="On Marketing Program Change"/>  	    						               
						  	    					                		<asp:ListItem Value="On Lead Status Change" Text="On Lead Status Change"/>  	    						               
						  	    					                		<asp:ListItem Value="On A Schedule" Text="On A Schedule"/>  	    						               
						  	    					                		
						  	    					                		</asp:DropDownList></td>
						  	    					               <td><asp:DropDownList id="dd_leadstatusFrom" AutoPostBack="false"
												                  		DataValueField="x_descr" 
												                  		Runat="server" visible=false />	</td>
  	    					                						<td><asp:DropDownList id="dd_leadstatusTo" AutoPostBack="false"
														                  		DataValueField="x_descr" 
														                  		Runat="server" visible=false />	</td>
																		<td><asp:TextBox ID="txtsdate" runat=server size=10 /></td>
																		<td><asp:TextBox ID="txtedate" runat=server size=10 /></td>
																	</tr>
																</table>
															</td>
															<td >
																<table>
																	<tr>
																		<td align=right><asp:linkbutton id="showcalc" Text= "Close" CausesValidation="false"  runat="server"   visible=false onClick="closecalendar" /></td>
													                </tr>
													                <tr>
									                                    <td colspan=10><asp:calendar id="cdrCalendar" runat="server" 
									                                            OnSelectionChanged="Calendar1_SelectionChanged"
								                                                backcolor="#ffffff" width="150px" height="100px" 
								                                                font-size="12px" font-names="Arial" borderwidth="2px"
								                                                bordercolor="#000000" nextprevformat="shortmonth" 
								                                                daynameformat="firsttwoletters" Visible="false">
								                                                <TodayDayStyle ForeColor="White" BackColor="Black"></TodayDayStyle>
								                                                <NextPrevStyle Font-Size="12px" Font-Bold="True" ForeColor="#333333">
								                                                </NextPrevStyle>
								                                                <DayHeaderStyle Font-Size="12px" Font-Bold="True"></DayHeaderStyle>
								                                                <TitleStyle Font-Size="14px" Font-Bold="True" BorderWidth="2px"
								                                                 ForeColor="#000055"></TitleStyle>
								                                                <OtherMonthDayStyle ForeColor="#CCCCCC"></OtherMonthDayStyle>
								                                                    </asp:calendar>
								                                        </td>
												                    </tr>
												                </table>
															</td>
															<td width=55%></td>
														</tr>
													</table>	
												</td>
											</tr>
					 			 		</table>
					 					
						 					<table width=18%>
								 				<tr>		
								 					<td class=wfstepheaderA >Filters</td>
								 					<td><asp:linkbutton id="SHWFFilters" Text="Show Filters"  runat="server" cssclass=linkbuttonsRED onClick="btn_SHWFFilters"  /></td> 
								 				</tr>
								 				<tr>
								 					<td colspan=2><asp:label id=lblwffilterstat runat=server visible=true /></td>
								 				</tr>
											</table>
										<asp:panel id=pnlWFfilters runat=server visible=false>
												<table width=100% cellpadding=2 cellspacing=2>
													<tr>
														<td width=300><asp:Label ID="lblleadtype" Text="Lead Type" Runat="server" font-bold="true"/></td>
														<td width=300><b>Lead Program</b></td>
														<td align="left"><asp:Label ID="lblstatus" Text="Assigned Status" Runat="server" font-bold="true" /></td>
													</tr>
													<tr>
														<td><asp:DropDownList id="dd_ldtypeinc" runat="server" AutoPostBack="true" onselectedindexchanged="updatefilters"    >
		    							                	<asp:ListItem Value="Do Not Use" Text="Do Not Use"/>    							              
		    							                	<asp:ListItem Value="Include" Text="Include"/>  	    						                	
		  	    						             		</asp:DropDownList></td>
		  	    					                    <td><asp:DropDownList id="dd_ldpginc" runat="server" AutoPostBack="true" onselectedindexchanged="updatefilters"  >
		    							                    <asp:ListItem Value="Do Not Use" Text="Do Not Use"/>    							              
		    							                    <asp:ListItem Value="Include" Text="Include"/>  	    						                    
		  	    					                        </asp:DropDownList></td>
		  	    					                    <td><asp:DropDownList id="dd_ldAstatinc" runat="server" AutoPostBack="true" onselectedindexchanged="updatefilters"  >
		    							                 	<asp:ListItem Value="Do Not Use" Text="Do Not Use"/>    							              
		    							                	<asp:ListItem Value="Include" Text="Include"/>  	    						               
		  	    					                		</asp:DropDownList></td>
		  	    					         	    </tr>
													<tr>
														<td><asp:listbox id="ddlleadtypeFilter" runat="server"  selectionmode="multiple" 
		              				 						height=100 width=200 DataTextField="x_descr" DataValueField="tbl_xwalk_pk" enabled=false /></td>
														<td><asp:listbox id="ddlleadprogramFilter" runat="server"  selectionmode="multiple" 
		              				 						height=100 width=200 DataTextField="x_descr" DataValueField="tbl_xwalk_pk" enabled=false /></td>
														<td><asp:listbox id="ddlstatusFilter" runat="server"  selectionmode="multiple" 
		              				 						height=100 width=200 DataTextField="x_descr" DataValueField="tbl_xwalk_pk" enabled=false /></td>
													        </tr>
													  </table>
													  <table  width=100% cellpadding=2 cellspacing=2>
		                  					        <tr>
		                  					        		<td width=300><b>Marketting Program</b></td>
		                  									<td width=300><asp:Label ID="lblcstatus" Text="Lead Status" Runat="server" font-bold="true" /></td>
																	<td><b>ADs</b></td>
													        </tr>
		                  					      <tr>
		                  								<td><asp:DropDownList id="dd_MTPinc" runat="server" AutoPostBack="true" onselectedindexchanged="updatefilters"  >
		    							                 	<asp:ListItem Value="Do Not Use" Text="Do Not Use"/>    							              
		    							                	<asp:ListItem Value="Include" Text="Include"/>
		  	    						               
		  	    					                		</asp:DropDownList></td>
		  	    					                   
		                  							   <td><asp:DropDownList id="dd_ldstatinc" runat="server" AutoPostBack="true" onselectedindexchanged="updatefilters"  >
		    							                 	<asp:ListItem Value="Do Not Use" Text="Do Not Use"/>    							              
		    							                	<asp:ListItem Value="Include" Text="Include"/>
		  	    						               
		  	    					                		</asp:DropDownList></td>
		  	    					                    <td><asp:DropDownList id="dd_adsinc" runat="server" AutoPostBack="true" onselectedindexchanged="updatefilters"  >
		    							                 	<asp:ListItem Value="Do Not Use" Text="Do Not Use"/>    							              
		    							                	<asp:ListItem Value="Include" Text="Include"/>
		  	    						                	
		  	    					                		</asp:DropDownList></td>
															</tr>
		                  							<tr>
		                  									<td><asp:listbox id="ddMKFilter" runat="server"  selectionmode="multiple" 
		              				 							height=100 width=200 DataTextField="x_descr" DataValueField="tbl_xwalk_pk" enabled=false /></td>
		                  					
		                  								<td><asp:listbox id="ddlcstatusFilter" runat="server"  selectionmode="multiple" 
		              				 							height=100 width=200 DataTextField="x_descr" DataValueField="tbl_xwalk_pk" enabled=false /></td>
		              				 				    	<td><asp:listbox id="ddadFilter" runat="server"  selectionmode="multiple" 
		              				 							height=100 width=200 DataTextField="x_descr" DataValueField="tbl_xwalk_pk" enabled=false /></td>
		              				 							</tr>
		                  	
		                  	
		                  				        </table>
		                  				        <table width=58%>
		                  					        <tr>
		                  						        	
		                  									<td width=80%></td>
													</tr>
												</table>
											</asp:panel>
					 		 		</div>
 			       			    </asp:panel>
 			       			    <asp:panel id="pnlpage1" runat=server visible=false >
 			       				    <div id="fieldtitles" style="vertical-align: top; height: 480px; " >
									<div style="margin:0;background: #4682B4;padding:2;"></div>
						  				<asp:panel id="pnlstepmain" runat=server visible=true >
						  					<table width=55%>
							  					<tr>
							  						<td class=wfstepheaderA width=200>Current Steps</td>
							  							<td>Step Status</td>
							  							<td><asp:DropDownList ID="dd_StepStat" runat=server Visible=true autopostback=true  OnSelectedIndexChanged="filterVenues">
								            			  	<asp:ListItem Value="Active" Text="Active"/>
								    	            		<asp:ListItem Value="Inactive" Text="Inactive"/>
								    	            		<asp:ListItem Value="All" Text="All"/>
								                			
								                			</asp:DropDownList></td> 
							  							<td><asp:button id="baddstep" runat=server text="Add Step"  CausesValidation="false" cssclass=frmbuttonsxlg onclick="btnaddstep" />	</td>
														<td><asp:button id="breorder" runat=server text="Reorder Steps"  CausesValidation="false" cssclass=frmbuttonsxlg onclick="stepreorderA" />	</td>
														<td><asp:button id="bsavereorder" runat=server text="Save"  CausesValidation="false" cssclass=frmbuttonsxlg onclick="savestepreorder" visible=false />	</td>
														<td><asp:button id="bcancelreorder" runat=server text="Cancel"  CausesValidation="false" cssclass=frmbuttonsxlg onclick="cancelstepreorder" visible=false />	</td>
													
							  						</tr>
							  				</table>
							  	 			<table width=90% border=0>
			                    			    <tr>
			                        		        <td>
			                        			        <div style="vertical-align: top; height: 430px; overflow:auto;">
					  			       				 	<asp:DataGrid 
												        	ID="WFsteps" 
			  								        		AutoGenerateColumns=False
			  								        		Width="100%"
			  								        		OnItemDataBound="ItemDataBoundEventHandlerPP"
								                    	    ColumnHeadersVisible = FALSE  
			  								        		AllowPaging="True" 
			                                                PageSize="10" 
			                                                PagerStyle-Mode="NumericPages" 
			                                                OnPageIndexChanged="newwfs_PageChanger" CssClass="dg" Runat=server>
			  								          	    <HeaderStyle CssClass="dgheaders" />
										        			<ItemStyle CssClass="dgitems" />
										        			<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>
											      
			   							                    <Columns >
							        		                   <asp:BoundColumn HeaderText="Step No"  DataField="wfs_tbl_pk" visible="false" ItemStyle-Width="150px"    />
							        		                   <asp:BoundColumn HeaderText="Step No"  DataField="wfs_stepno" visible="true" ItemStyle-Width="80px"    />
							        		                   <asp:TemplateColumn HeaderText="New Step No" ItemStyle-Width="40px" ItemStyle-CssClass="dgitemsNOBD" visible=false>
					                                		      
					                                		        <ItemTemplate >
					                                    	            <table width=100%>
					                                        	            <tr>
					                                                          <td>					                                                          
					                                                          <asp:DropDownList ID="dd_Stepreorder" DataTextField="wfs_stepno"
					                                                           autopostback=true OnSelectedIndexChanged="StepReorder"
										                                                DataValueField="wfs_stepno" 
										                  		                        Runat="server" /></td>		                  		                        	
			                                   					            </tr>    
					                                    	            </table>   
					                                		        </ItemTemplate>                                                     
				                                		        </asp:TemplateColumn>	
							        		                   <asp:BoundColumn HeaderText="Description"  DataField="wfs_Desc" visible="true" ItemStyle-Width="140px"    />
							        		                   <asp:BoundColumn HeaderText="Step Action"  DataField="wfs_type" visible="true" ItemStyle-Width="140px"    />
							        		              	    <asp:BoundColumn HeaderText="Status"  DataField="wfs_status" visible="true" ItemStyle-Width="140px"    />
							        		              	    
							        		              	    <asp:TemplateColumn HeaderText="" ItemStyle-Width="40px" ItemStyle-CssClass="dgitemsNOBD" >
					                                		        <ItemTemplate >
					                                    	            <table width=100%>
					                                        	            <tr>
					                                                         <td><asp:button id="beditstep" runat=server onclick="Editstep" text="Edit"  CausesValidation="false" cssclass=frmbuttons /></td>		
			                                                               <td><asp:button id="bdeletestep" runat=server text="Remove" onclick="Removestep" CausesValidation="false" cssclass=frmbuttons /></td>		
			                                   					            </tr>    
					                                    	            </table>   
					                                		        </ItemTemplate>                                                     
				                                		        </asp:TemplateColumn>	
				                                		    <asp:BoundColumn HeaderText="Description"  DataField="wfs_PrevStepNo" visible="false" ItemStyle-Width="140px"    />
							        		                   <asp:BoundColumn HeaderText="Step Action"  DataField="wfs_newstepno" visible="false" ItemStyle-Width="140px"    />
							        		              	   
												            </Columns>
											        	</asp:DataGrid>
									            	</div>
									                </td>
			                    			    </tr>
			                			    </table> 
			                			    <table>
			                			    	<tr>
			                 					</tr>
												</table>
										</asp:panel>
										
						  				<asp:panel id="pnladdstep" runat=server visible=false style="margin-left:4;margin-top:4;">
								  			<table cellpadding=2 cellspacing=2 border=0 width=100% class=tblborderbottom>
			 					                <tr>
			 					            	        <td width=45><b>Step No</b></td>
			 					            	        <td width=40><b><asp:label id="lblstepno" runat=server /></b></td>
			 					            	        <td width=60>Description</td>
			 					            	       
							 						    		<td><asp:textbox id="txtdescA" runat=server visible=true size=65 /></td>
							 						     		<td width=70></td>
			 					            	        <td align=center ><asp:linkbutton id="sconditions" Text="Step Action"  runat="server" cssclass=linkbuttons onClick="btn_scond"  /></td> 
			 					                        <td>|</td>
			 					                        <td align=left width=10%><asp:linkbutton id="sdetail" Text="Step Conditions"  runat="server" cssclass=linkbuttons onClick="btn_sdetails" /></td> 
			 					            	        <td width=6%></td>
			 					            	        <td align=left><asp:button id="btnstepnext" runat=server text="Save"  CausesValidation="false" cssclass=frmbuttons onclick="nextstep" /></td>
							      				        <td align=left><asp:button id="btnstepcancel" runat=server text="Cancel"  CausesValidation="false" cssclass=frmbuttons onclick="canceladdstep" /></td>
							      		  	    </tr>
			 					        	</table>							  		
						  					<asp:panel id="pnlstepdetails" runat=server visble=false>
						  						<table width=40%> 
								 					<tr>
								 						<td><b>Dependant Step</b></td>
								 					</tr>
								 					<tr>
								 						<td><asp:DropDownList id="dd_dstep" DataValueField="dstepno" Runat="server" AutoPostBack="true" onselectedindexchanged="updatestepdate" />							  	    						              
		 								                    </asp:DropDownList></td>
		 								          </tr>														
												</table>
												
												<table>
													<tr>
														<td><b>Frequency</b></td>
													</tr>
													<tr>
														<td width=20% valign=top>
															<table>
																<tr>
																	<td><asp:checkbox id=chkrunonce runat=server text="Once" autopostback=true oncheckedchanged="updatedreq" /></td>
																</tr>
																<tr>
																	<td><asp:checkbox id=chkrundaily runat=server text="Daily" autopostback=true oncheckedchanged="updatedreq"  /></td>
																</tr>
																<tr>
																	<td><asp:checkbox id=chkrunweekly runat=server text="Weekly" autopostback=true oncheckedchanged="updatedreq"  /></td>
																</tr>
																<tr>
																	<td><asp:checkbox id=chkrunMonthly runat=server text="Monthly" autopostback=true oncheckedchanged="updatedreq"  /></td>
																</tr>
																<tr>
																	<td><asp:checkbox id=chkrunQtrly runat=server text="Quarterly" autopostback=true oncheckedchanged="updatedreq"  /></td>
																</tr>
																<tr>
																	<td><asp:checkbox visible=false id=chkrunSched runat=server text="Schedule" autopostback=true oncheckedchanged="updatedreq"  /></td>
																</tr>
															</table>
														</td>
														<td valign=top width=80%>
															<div class=tblborder style="height:200px;width:500px" runat=server >
																<asp:panel id=pnlSdateMain runat=server visible=true >
																		<table  width=70%>
																			<tr>
													 							<td><b>Start Date</b></td>
													 							<td><b><asp:label id=lblsdateoffest text="Start Date Offset" runat=server visible=false /></b></td>
													 						</tr>
													 						<tr>
											 						      	<td><asp:DropDownList id="dd_sdate" DataValueField="x_descr" Runat="server" />							  	    						              
					 								                    		</asp:DropDownList></td>
					 								                		<td><asp:textbox id="sdoffset" runat=server  size=1 visible=false /></td>
					 								            		</tr>		
																		</table>
																		<asp:panel id=pnlselectdom runat=server visible=false>
																			<table width=70%>
																				<tr>
																					<td colspan=7><b>Select the Beginning or End of the Month</b></td>
																				</tr>
																				<tr>
																					<td><asp:DropDownList id="dd_weekselect" runat="server" >
								    							                 	<asp:ListItem Value="First Day of the Month" Text="First Day of the Month"/>    							              
								    							                	<asp:ListItem Value="Last Day of the Month" Text="Last Day of the Month"/>
								  	    						              
								  	    					                		</asp:DropDownList></td>
																				</tr>
																			</table>
																		</asp:panel>
																		<asp:panel id=pnlselectdomQ runat=server visible=false>
																			<table width=70%>
																				<tr>
																					<td colspan=7><b>Select the Beginning or End of the Quarter</b></td>
																				</tr>
																				<tr>
																					<td><asp:DropDownList id="dd_Monthselect" runat="server" >
								    							                 	<asp:ListItem Value="First Day of the Quarter" Text="First Day of the Quarter"/>    							              
								    							                	<asp:ListItem Value="Last Day of the Quarter" Text="Last Day of the Quarter"/>
								  	    						              
								  	    					                		</asp:DropDownList></td>
																				</tr>
																			</table>
																		</asp:panel>
																		<asp:panel id=pnlselectdoW runat=server visible=false>
																			<table width=70%>
																				<tr>
																					<td colspan=7><b>Select day</b></td>
																				</tr>
																				<tr>
																					<td><asp:checkbox id=chkStepSun runat=server text="Sun" /></td>
																					<td><asp:checkbox id=chkStepMon runat=server text="Mon" /></td>
																					<td><asp:checkbox id=chkStepTue runat=server text="Tue" /></td>
																					<td><asp:checkbox id=chkStepWed runat=server text="Wed" /></td>
																					<td><asp:checkbox id=chkStepThu runat=server text="Thu" /></td>
																					<td><asp:checkbox id=chkStepFri runat=server text="Fri" /></td>
																					<td><asp:checkbox id=chkStepSat runat=server text="Sat" /></td>
																				</tr>
																			</table>			
																		</asp:panel>
																</asp:panel>	
																
																<asp:panel id=pnlCalendart runat=server visible=false>
																	<table>
															        		<tr>
															        			<td>Select Date for this step to occur</td>
															        		</tr>
											                        <tr>
				                                            		<td colspan=10><asp:calendar id="cdrCalendarWFS" runat="server" 
				                                                            OnSelectionChanged="Calendar2_SelectionChanged"
			                                                                backcolor="#ffffff" width="180px" height="120px" 
			                                                                font-size="12px" font-names="Arial" borderwidth="2px"
			                                                                bordercolor="#000000" nextprevformat="shortmonth" 
			                                                                daynameformat="firsttwoletters" Visible="true">
			                                                                <TodayDayStyle ForeColor="White" BackColor="Black"></TodayDayStyle>
			                                                                <NextPrevStyle Font-Size="12px" Font-Bold="True" ForeColor="#333333">
			                                                                </NextPrevStyle>
			                                                                <DayHeaderStyle Font-Size="12px" Font-Bold="True"></DayHeaderStyle>
			                                                                <TitleStyle Font-Size="14px" Font-Bold="True" BorderWidth="2px"
			                                                                 ForeColor="#000055"></TitleStyle>
			                                                                <OtherMonthDayStyle ForeColor="#CCCCCC"></OtherMonthDayStyle>
			                                                                </asp:calendar></td>
										                            </tr>
										                        </table>
																</asp:panel>	
																
																
																
																
																														
															</div>
															
															
															
														</td>
													</tr>
												</table>
																
																<asp:panel runat=server visible=false>
																	<table>																
																		<tr>
																			<td>Perform this step</td>
							 								                <td><asp:DropDownList id="dd_freq" DataValueField="x_descr" Runat="server" AutoPostBack="true" onselectedindexchanged="updatefreqdate"  /></td>
							 								      		    <td>For the duration of </td>
							 								     			<td><asp:textbox id="dno" runat=server visible=true size=1 /><asp:textbox id="stepsdate" runat=server visible=false />&nbsp<asp:label id="lbldspacer" text="-" visible=false />&nbsp<asp:textbox id="stepedate" runat=server visible=false /></td>
																			<td><asp:DropDownList id="dd_duration" DataValueField="x_descr" Runat="server" /></td>
																		</tr>
																	</table>
																</asp:panel>
															
															<asp:panel id=pnlflowcond runat=server visible=false>
																<table>
																	<tr>
																		<td>Set Flow Condition</td>
																		<td><asp:DropDownList id="dd_condition" runat="server" AutoPostBack="True" OnSelectedIndexChanged="checkcondition"  >
					 							                 		<asp:ListItem Value="No" Text="No" />
					    						                		<asp:ListItem Value="Yes" Text="Yes"/>  
												                		</asp:DropDownList></td>
												            		    <asp:panel id=pnlconditionemail runat=server visible=false>
													           			<td>If Lead Email is Blank perform this step?</td>
													           			<td><asp:DropDownList id="dd_emailcond" runat="server"   >
						    						               			<asp:ListItem Value="Yes" Text="Yes"/> 
					    						                				<asp:ListItem Value="No" Text="No" /> 
												                				</asp:DropDownList></td>	
												           			    </asp:panel>
												         		    </tr>
												      		    </table>
												      	</asp:panel>
										     	 
										 	</asp:panel>
										    <asp:panel id="pnlstepconditions" runat=server visble=false>
											    <table width=100%>
							 					    <tr>
							 						    <td valign=top><b>Actions</b></td>
							 					    </tr>
							 					    <tr>
							 						    <td valign=top width=10%>
							 							    <table>
							 							        <tr>
							 							            <td>
							 							                <asp:listbox id="dd_action" selectionmode="single" autopostback=true onSelectedIndexChanged="chooseaction"
						    							                    DataValueField="x_descr" Runat="server" height=90 /></td>
						    								    </tr>
						    								    <tr>
						    								        <td>
						    									        <table>
																	        <tr>
																		        <td align=right><asp:linkbutton id="showcalc2" Text= "Close" CausesValidation="false"  runat="server"   visible=false onClick="closecalendar" /></td>
													                        </tr>
													                        <tr>
									                                            <td colspan=10><asp:calendar id="cdrCalendar2" runat="server" 
									                                                            OnSelectionChanged="Calendar2_SelectionChanged"
								                                                                backcolor="#ffffff" width="130px" height="90px" 
								                                                                font-size="12px" font-names="Arial" borderwidth="2px"
								                                                                bordercolor="#000000" nextprevformat="shortmonth" 
								                                                                daynameformat="firsttwoletters" Visible="false">
								                                                                <TodayDayStyle ForeColor="White" BackColor="Black"></TodayDayStyle>
								                                                                <NextPrevStyle Font-Size="12px" Font-Bold="True" ForeColor="#333333">
								                                                                </NextPrevStyle>
								                                                                <DayHeaderStyle Font-Size="12px" Font-Bold="True"></DayHeaderStyle>
								                                                                <TitleStyle Font-Size="14px" Font-Bold="True" BorderWidth="2px"
								                                                                 ForeColor="#000055"></TitleStyle>
								                                                                <OtherMonthDayStyle ForeColor="#CCCCCC"></OtherMonthDayStyle>
								                                                                </asp:calendar></td>
												                            </tr>
												                        </table>
						    									    </td>
						    									</tr>
						    							    </table>
						    					        </td>
						    						    <td valign=top width=80%>
											                <asp:panel id="pnlsendemail" runat=server visible=false >
												                <table width=100% border=0 cellpadding=1 cellspacing=1>
													                <tr>
														                <td>
															                <table width=100% border=0 cellpadding=1 cellspacing=1>
																                <tr>
																	                <td><asp:linkbutton id="vstepinfo" visible=false Text= "View Step Info"  runat="server" Font-underline="false" Style="cursor:hand" onclick="vwstepinfo"  /></td>
																                </tr>
															                </table>
															               <asp:panel id=pnlemailmain runat=server visible=true>
																	            <asp:panel id=pnlemailto runat=server visible=false>
																	            
																		            <table width=50% cellpadding=0 cellspacing=0>
																		            	<tr>
																		            	 	<td width=10></td>
																		            		<td>Email To Address&nbsp&nbsp&nbsp&nbsp<asp:linkbutton id="btnuseremail" CausesValidation="false" Text= "Use Default" runat="server"  cssclass=linkbuttonsred onClick="getuseremail" /></td>
																		            		<td>Lead Detail</td>
																		            	</tr>
																		            	<tr>
																		            		 <td width=10></td>
																				            <td><asp:TextBox ID="selfemailaddress" runat=server size=40 Enabled=true />&nbsp</td>
						    		
																					    		<td><asp:DropDownList id="dd_status" runat="server" AutoPostBack="true" OnSelectedIndexChanged="statcheck"  >
																			    							                <asp:ListItem Value="Select.." Text="Select.."/>
																			    							                <asp:ListItem Value="Basic" Text="Basic"/>
																			    							                <asp:ListItem Value="Full Information" Text="Full Information"/>
																			  	    						                  						               
																			 								                </asp:DropDownList></td>
																			 				 </tr>
																			 			</table>
																			 		</asp:panel>
																	 			<table width=100% cellpadding=0 cellspacing=0>
												   			                <tr>
												   				                <td width=10></td>
												      			                <td align=left valign=top>From Email Address&nbsp&nbsp&nbsp&nbsp<asp:linkbutton id="btnuseremailA" CausesValidation="false" Text= "Use Your Email" runat="server"  cssclass=linkbuttonsred onClick="getuseremailA" /></td>
													     			            </tr>
													     			            <tr>
													     				            <td width=10></td>
													        			            <td><asp:TextBox ID="TextBox1" runat=server size=50 Enabled=true />&nbsp&nbsp</td>
											               	                    </tr>
													    			            <tr>
													    			                <td width=10></td>
													        			            <td><asp:TextBox ID="TextBox2" runat=server size=50 Enabled=true visible=false /></td>
											               	                    </tr>
													    	 		            <tr>
												               	                    <td colspan=2></td>
												         		                </tr>
													      		                <tr>
													      			                <td width=10></td>
													        			            <td  align=left valign=top >Subject</td>
													      		                </tr>
													      		                <tr>
													      			                <td width=10></td>
													        			            <td><asp:TextBox ID="TextBox3" runat=server size=70 Enabled=true /></td>
											               
													    			            </tr>
													    		            </table>
													    	                </asp:panel>
												    		                <table width=80% border=0 cellspacing=2 cellpadding=2>
												      		                    <tr>
												      		                    	 <td  align=left width=60><b>Templates</b></td>
												      		                    	  <td colspan=3 valign=bottom width=25%><asp:DropDownList ID="ddemailcor" visible=true AutoPostBack =true OnSelectedIndexChanged ="prefillemail"
											          		                            DataValueField="email_tbl_pk"          		                    Runat="server" /></td>
											          		                 		</tr>
											          		              </table>
											          		               <asp:panel id="pnltempatespre" runat=server visible=false  >
								          					                    <table width=50%>
								          						                    <tr>
												          			                    <td>
												          				                    <asp:DataGrid	            
																        	                    ID="temppreview" 
																        	                    AutoGenerateColumns=False
																        	                    Width="100%"
															        		                    AllowPaging="false" 
                    												                    	
															          	                    Runat=server CssClass="dg"
															          	                    OnItemDataBound="ItemDataBoundEventHandlerA"	>
															        		                    <HeaderStyle CssClass="dgheaders" />
															        		                    <ItemStyle CssClass="dgitems" />
															        		                    <AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>
													                	                    <Columns >
												    		             	                    <asp:BoundColumn HeaderText="No."  DataField="email_tbl_pk" visible="true" ItemStyle-Width="10px"    />
												     		                                   <asp:BoundColumn HeaderText="Template Name"  DataField="email_name" visible="false" ItemStyle-Width="100px"    />
												     		                                   <asp:BoundColumn  HeaderText="Subject Line"  DataField="email_subject" visible="true" ItemStyle-Width="150px"    />
												     		                                   <asp:TemplateColumn HeaderText="Actions" visible=true ItemStyle-Width="300px"  >
													     		                                   <HeaderStyle cssclass = "dgheadersNOBD"></HeaderStyle>
																	           	                    <HeaderTemplate >
																	           		                    <table width=100% cellspacing=0 cellpadding=0 >
																                		                    <tr>
																                			                    <td width=50>Body</td>
																                                            <td><asp:linkbutton id="vwbodyA" runat=server text="[Detail View]"  visible=true onclick="showbdyA" CausesValidation="false" cssclass=linkbuttons /></td>		
																				                            </tr>
																						                    </table>
																		                             </HeaderTemplate>
																					                    <ItemTemplate>   
																						                    <%# DataBinder.Eval(Container.DataItem, "bdtext") %> 
																					                    </ItemTemplate>                                             
																                                </asp:TemplateColumn>				            
														     		                             <asp:TemplateColumn HeaderText="Actions" visible=true ItemStyle-Width="100px" ItemStyle-cssclass = "dgitemsNOBD" >
																			   	                    <ItemTemplate  >
																                	                    <table width=100% cellspacing=0 cellpadding=0 >
																                	                      <tr>
																                                            <td><asp:button id="AppendAll" runat=server text="Replace"  visible=true onclick="appendAll" CausesValidation="false" cssclass=frmbuttons onmouseover="document.getElementById('myToolTip').className='activeToolTip'" onmouseout="document.getElementById('myToolTip').className='idleToolTip'"  /> </td>		
																				                            </tr>
																				                            <tr>
																				                                <td><asp:button id="AppendBody" runat=server text="Append Body "  visible=true onclick="appendBody" CausesValidation="false" cssclass=frmbuttons  onmouseover="document.getElementById('myToolTipA').className='activeToolTip'" onmouseout="document.getElementById('myToolTipA').className='idleToolTip'"  /></td>		
																				                            </tr>
																				                            <tr>
																				                                <td><asp:button id="AppendSubject" runat=server text="Append Subject "  visible=false onclick="appendSubject" CausesValidation="false" cssclass=frmbuttons /></td>		
														  							                         </tr>
														  							                         <tr>
														  							                             <td><asp:button id="Cancel" runat=server text="Cancel "  visible=true onclick="Canceltemplate" CausesValidation="false" cssclass=frmbuttons /></td>		
														    								                    </tr>
																                	                    </table>   
															            		                    </ItemTemplate>                                                     
														            		                    </asp:TemplateColumn>
														              		                    <asp:BoundColumn  HeaderText="Body"  DataField="email_text" visible="false" ItemStyle-Width="450px"    />
										     		            		                    </Columns>
													        				                    </asp:DataGrid>
								          							                    </td>
								          						                    </tr>
								          					                    </table>	<div id="myToolTipA" class="idleToolTip">Will append template text to the end of the Email Body</div>
			      														                            <div id="myToolTip" class="idleToolTip">Will replace/overwrite the Subject Line and Email Body with template</div>
								        
								          				                    </asp:panel>
								          				                    <table>
											          		              	   		
											          		                 		
											          		                 		
												      		                    <tr>
												      			                    <td width=3></td>
												        			                <td  width=120 align=left valign=bottom>Email Body</td>
												        		    	            <td width=70 valign=bottom><asp:linkbutton id="bfulls" Text= "Full Screen"  runat="server" Font-underline="false" Style="cursor:hand" onClick="btn_Fullscreen" /></td>
												        			                <td valign=bottom>|</td>
												        			                <td width=160 valign=bottom><asp:linkbutton id="btoolbar" Text= "ToolBar"  runat="server" Font-underline="false" Style="cursor:hand"  /> </td>
												      			                   
																                         <td><asp:label id="dsp" runat=server width=270 text="&nbsp " visible=true /></td>
											          		  
												      		                    </tr>
												      	                    </table>
												      	                    <table width=100% border=0>
												      		                    <tr>
												      			                    <td width=3></td>
												        			                    <td >  <ed:Editor AutoFocus="false" height="275" width="800" visible=true ShowQuickFormat="false" FixedToolbar="false" submit="false"   PathPrefix="Editor_data/" FlashBrowse="myFlashBrowse.aspx" MediaBrowse="myMediaBrowse.aspx" ImageBrowse="myImageBrowse.aspx" UrlBrowse="myUrlBrowse_vb.aspx" id="emailbody" PreviewMode="true" runat="server" /></td>
														                        </tr>
															                </table>
														                </td>
														                <td valign=top>
															               
														                </td>
													                </tr>
												                </table>
											                </asp:panel>
											                <asp:Panel ID="pnl_addtask" runat="server"	visible="false">
													
													            <table width=40% cellpadding=1 cellspacing=1 border=0>
														        <tr>
															        <td>Task Type</td>
															        <td>Task Status</td>
															     </tr>
														        <tr>
															        <td><asp:DropDownList ID="ddtasktype" Runat="server"       		
										                  		            DataValueField="x_descr" Runat="server" />   </td>
										                	        <td><asp:DropDownList ID="ddtaskstat" Runat="server"       		
										                  		            DataValueField="x_descr" Runat="server" /> </td>
										                            
										  				        </tr>
													        </table>
													            <table width=70%>
														            <tr>
															            <td>Reminder</td>
															            <td>Days</td>
															            <td>Email&nbsp&nbsp&nbsp<asp:linkbutton id="btnuseremailTask" enabled=false CausesValidation="false" Text= "Use Default" runat="server"  cssclass=linkbuttonsred onClick="getuseremailT" /></td>
														            </tr>
														            <tr>
														                <td><asp:DropDownList id="dd_treminder" runat="server" AutoPostBack="true" onselectedindexchanged="updatetaskremninder">
					 							                 		    <asp:ListItem Value="No" Text="No" />
					    						                		    <asp:ListItem Value="Yes" Text="Yes"/>  
												                		    </asp:DropDownList></td>
												        	            <td ><asp:textbox id="l_treminder" runat=server size=2 enabled=false /></td>
												       	 	            <td ><asp:textbox id="l_treminderEM" runat=server size=50 enabled=false/></td>
										                            </tr>
										                        </table>
										                        <table>
										  			                <tr>
										  				                <td valign=top>Task Description</td>
										  				            </tr>
										  				            <tr>
        										                        <td><ed:Editor  AutoFocus="false" Appearance="lite" height="275" width="800" visible=true ShowQuickFormat="false" FixedToolbar="false" submit="false"   PathPrefix="Editor_data/" FlashBrowse="myFlashBrowse.aspx" MediaBrowse="myMediaBrowse.aspx" ImageBrowse="myImageBrowse_vb.aspx" UrlBrowse="myUrlBrowse_vb.aspx" id="hst_action" PreviewMode="false" runat="server" /></td>
														            </tr>
													            </table>	
											 		
										  			        </asp:Panel>
													        <asp:panel id="pnlleaddds" runat=server visible=false>
													            
													            <table cellpadding=2 cellspacing=2 width=100% border=0>	
														            <tr>
														            	<td width=10><asp:checkbox id="chklsupdate" runat=server /></td>
															            <td  width=125 align=left><b>Lead Status-></b></td>
															            <td align=right width=40>From</td>
															            <td><asp:DropDownList id="ddlstatusFilter2" DataValueField="x_descr" Runat="server" />							  	    						              
					 								                            </td>
															            <td align=right width=40>To </td>
														              <td><asp:DropDownList id="ddlstatusFilter2A" DataValueField="x_descr" Runat="server" />							  	    						              
						 								                </td>
						 								            </tr>
													            </table>
													            <table cellpadding=2 cellspacing=2 width=100% border=0>	
														            <tr>
														            	<td width=10><asp:checkbox id="chkltupdate" runat=server /></td>
															            <td width=125 align=left><b>Lead Type-></b></td>
															            <td align=right width=40>From</td>
															            <td align=left><asp:DropDownList id="ddlleadtypeFilter2" DataValueField="x_descr" Runat="server" />							  	    						              
					 								                         </td>
            						 								 
															            <td align=right width=40>To </td>
														                <td align=left><asp:DropDownList id="ddlleadtypeFilter2A" DataValueField="x_descr" Runat="server" />							  	    						              
						 								                </td>
														            </tr>
													            </table>
													            <table cellpadding=2 cellspacing=2 width=100% border=0>	
														            <tr>
														            	<td width=10><asp:checkbox id="chklpupdate"  runat=server /></td>
															            <td width=125 align=left><b>Lead Program-></b></td>
															            <td align=right width=40>From</td>
															            <td><asp:DropDownList id="ddlleadprogramFilter2" DataValueField="x_descr" Runat="server" />							  	    						              
					 								                        </td>
						 								 
															            <td align=right width=40>To </td>
														                <td><asp:DropDownList id="ddlleadprogramFilter2A" DataValueField="x_descr" Runat="server" />							  	    						              
						 								                </td>
														            </tr>
													            </table>
													            <table cellpadding=2 cellspacing=2 width=100% border=0>	
														            <tr>
														            	<td width=10><asp:checkbox id="chkMPupdate"  runat=server /></td>
															            <td width=125 align=left><b>Marketing Program-></b></td>
															            <td align=right width=40>From</td>
															            <td><asp:DropDownList id="ddlMarketprogramFilter2" DataValueField="x_descr" Runat="server" />							  	    						              
					 								                        </td>
						 								 
															            <td align=right width=40>To </td>
														                <td><asp:DropDownList id="ddlMarketprogramFilter2A" DataValueField="x_descr" Runat="server" />							  	    						              
						 								                </td>
														            </tr>
													            </table>
													        </asp:panel>
											
											            </td>
									                </tr>
							 		            </table>
										    </asp:panel>
										</asp:Panel>
									</div>
 			   				    </asp:panel>
 			   				    
 			   				    <asp:panel id=pnlpage2 runat=server visible=false>
 			   				     <div style="margin:0;background: #4682B4;padding:1;"></div>
 			       				    <div id="fieldtitles" style="vertical-align: top; height: 480px;margin:5; ">
 			   				    		<asp:panel runat=server visible=false>
 			   				    		<table cellpadding=2 cellspacing=2 border=0 width=100% class=tblborderbottom>
			 					                <tr>
			 					            	      
			 					            	        <td align=left ><asp:linkbutton id="Ldprocess" Text="Leads In Process"  runat="server" cssclass=linkbuttons   /></td> 
			 					                       <td>|</td>
			 					                        <td align=left><asp:linkbutton id="ldexception" Text="Exceptions"  runat="server" cssclass=linkbuttons /></td> 
			 					            	       <td width=68%></td>
							      		  	    </tr>
			 					        	</table>	
			 					        </asp:panel>
			 					        
			 					        	<asp:panel id=pnlinporcess runat=server visible=false>
			 					        	<table width=100%>
			 					        		<tr>
			 					        			<td class=wfstepheaderA width=160>Leads</td>
			 					        			<td width=60>Search</td>
			 					        			<td ><asp:textbox id=wflsearch runat=server size=30 /></td>
			 					        			<td width=50><asp:linkbutton id="clear" Text= "Clear" 
                   				 					runat="server" Font-Bold="True" Font-underline="True" Style="color:#ff0000; font-family:arial; font-size:9pt; cursor:hand"
                    									onClick="clearall" /></td> 
                    							<td width=70%><asp:linkbutton id="btn_search" Text= "Go" 
                    									runat="server" Font-Bold="True" Font-underline="false" Style="color:#00AF33; font-family:arial; font-size:9pt; cursor:hand"
                    									onClick="filterVenuesAADSLK" /></td>
			 					        			<td><asp:DropDownList id="dd_WFSFilter" runat="server"  AutoPostBack="true" visible=false >
	    							                 	<asp:ListItem Value="Active" Text="Active"/>
	    							                 	<asp:ListItem Value="Inactive" Text="Inactive"/>						  	    						                
	  	    					                		</asp:DropDownList></td>	
	  	    					              <td align=right><asp:button id="btnaddleadwf" runat=server text="Add New Lead" onclick="Addleadtowf" CausesValidation="false" cssclass=frmbuttonsXLG /></td>		
									                        		 					        			
			 					        		</tr>
			 					        	</table>
			 					        	<table width=100%>
												<tr>
													<td>
														<div style="vertical-align top; height: 360px; overflow:auto;">
												  				<asp:DataGrid 
																ID="WFLeads" 
																AutoGenerateColumns=False
																Width="100%"
												            ColumnHeadersVisible = FALSE  
												            	
																AllowPaging="false"  CssClass="dg" Runat=server>
																	<HeaderStyle CssClass="dgheaders" />
												        			<ItemStyle CssClass="dgitems" />
												        			<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>        
												        			<Columns >
									        		      			<asp:BoundColumn HeaderText="Lead No"  DataField="lwfs_lead_fk" visible=true ItemStyle-Width="80px"    />
											        		         <asp:BoundColumn HeaderText="Name"  DataField="LeadName" visible=true ItemStyle-Width="80px"    />
													        		   <asp:BoundColumn HeaderText="Phone #"  DataField="ld_hphone" visible=true ItemStyle-Width="80px"    />
													        		  	<asp:BoundColumn HeaderText="Email"  DataField="ld_email" visible=true ItemStyle-Width="80px"    />
													        		  
													        		  <asp:TemplateColumn HeaderText="Actions" ItemStyle-Width="80px" ItemStyle-CssClass="dgitemsNOBD"  >
											                        <ItemTemplate >
											                            <table width=100% cellspacing=1 cellpadding=1>
											                                <tr>
											                                    <td><asp:button id="viewLDdetails" runat=server text="Lead Details" onclick="showleaddetails" CausesValidation="false" cssclass=frmbuttonsXLG /></td>		
									                                   			<td><asp:button id="viewwfdetails" runat=server text="WorkFlow Details" onclick="vwfdetails" CausesValidation="false" cssclass=frmbuttonsXLG /></td>		
									                                          <td><asp:button id="wfermove" runat=server text="Remove" onclick="removewf" CausesValidation="false" cssclass=frmbuttonsXLG /></td>		
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
 			   				    </asp:panel>
 			   			    </td>
 			   		    </tr>
 			   	    </table>
 			   </td>
 			</tr>
 		</table>
 		
	</form>
	</body>	
</HTML>
