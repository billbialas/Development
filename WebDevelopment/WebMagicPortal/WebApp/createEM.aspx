<%@ Page language="vb" Codebehind="createEM.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.createEM" Debug="false" trace="false" validateRequest=false  %>
<%@ Register TagPrefix="ed" Namespace="OboutInc.Editor" Assembly="obout_Editor" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script language="JavaScript" src="../_include/default.js"></script>   
<script type="text/javascript">
function inserttext()
{
  oboutGetEditor('emtext').InsertHTML(document.getElementById("refererA").value);
}
</script>
<HTML>
	<HEAD>
		<title>www.WebMagicPortal.com</title>
	</HEAD>
	
	<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">
		<form  name="createad"  runat="server" >
			<input type="hidden"  name="refererA" id="refererA" value="<table><tr><td>Test</td></tr></table>" runat=server />
		    <table width=30%>
                <tr>
    			    	<td width=35% valign=top class=pgheaders><asp:label id="adtitleM" runat="server" /></td>
      			 </tr>
            </table>
            <table border=0  width=100% cellspacing=0 cellpadding=0 id="SubNav">
			    <tr >
				    <td width="100%">
				        <table runat="Server" id="subnac" cellspacing=0 cellpadding=0 width=100% border=0>
						    <tr height=22>
							    <td id="subnavGen" align=center width=110><asp:linkbutton id="Lgen" Text= "General Setup"  runat="server" Font-underline="false" Style="cursor:hand" onClick="btn_Gen" /> </td>
							    <td id="spacer0" class="tblcelltestc" width=1>&nbsp</td> 			           
							    <td id="subnavPage1" align=center width=110><asp:linkbutton id="lpage1" Text= "Email Body"  runat="server" Font-underline="false" Style="cursor:hand" onClick="btn_pg1" /> </td>
							    <td id="spacer1" class="tblcelltestc" width=1>&nbsp</td> 			           
							    <td id="subnavPage2" align=center width=110><asp:linkbutton id="lpage2" Text= "Campaigns"  runat="server" Font-underline="false" Style="cursor:hand" onClick="btn_pg2" /> </td>
							    <td id="spacer2" class="tblcelltestc" width=1>&nbsp</td> 	
							    <td id="subnavPage3" align=center width=110><asp:linkbutton id="lpage3" Text= "Send Email"  runat="server" Font-underline="false" Style="cursor:hand" onClick="btn_pg3" /> </td>
							    <td id="spacer3" class="tblcelltestc" width=1>&nbsp</td> 			           
							   		           
							    <td  runat="Server" class="tblcelltestc" width=550>
              		       	<asp:Panel ID="pnlbuttons" runat=server Visible=true>
		         		      	<table>
                     	      	<tr>
                        	      	<td align=left ><asp:button id="b_save" runat=server text="Save Email" onclick="savead" CausesValidation="false" cssclass=frmbuttons /></td>		
					  		             	<td align=left ><asp:button id="b_send" runat=server text="Send Email" onclick="sendemail" CausesValidation="false" cssclass=frmbuttons visible=false/></td>		
					  		             	<td align=left ><asp:button id="b_cancel" runat=server text="Cancel" onclick="savead" CausesValidation="false" cssclass=frmbuttons /></td>		
		  						    		</tr>
		  					    		</table>
		  				    		</asp:Panel>
		            	 	</td>
 			        	    </tr>
 			            </table> 			       
					    <table class=tblcelltestb width=100% cellpadding=0 cellspacing=0>
		      	            <tr>
		      		            <td>	      		    		   
		      		                <asp:panel id="pnlgeneral" runat=server visible=false >
		      		 	                <div style="margin:0;background: #4682B4;padding:1;"></div>
 			       				        <div id="fieldtitles" style="vertical-align: top; height: 460px;margin:5; ">
 			       				   	 	    <asp:panel id=pnlstillgetleads runat=server visible=false>
 			       				   	 		    <table>
 			       				   	 			    <tr>
 			       				   	 				    <td><asp:checkbox id=chkstillgetleads text="This AD has been Inactivated.  Should New leads still be accepted?" runat=server autopostback=true /></td>
 			       				   	 			    </tr>
 			       				   	 		    </table>
 			       				   	 	    </asp:panel>
 			       				   	 	 	<table width=80%>
					                            <tr>
					                              <td><b>Status</b></td>
					                              <td ><b>Email Name</b></td>
					                            </tr>
					                            <tr>
					                        	    <td>
					                        	        <asp:DropDownList id="dd_status" runat="server"  AutoPostBack =true >    							               
			    							                     <asp:ListItem Value="Active" Text="Active"/>
			  	    						                     <asp:ListItem Value="Inactive" Text="Inactive"/>
			  	    						                    </asp:DropDownList></td>
			  	    						        <td >
			  	    						            <asp:textbox id="emname" runat=server size=90 /></td> 
			  	    						    </tr>
			  	    			            </table><br>
			  	    			            <table width=80%>
					                            <tr>
					                              <td><b>Email From</b></td>					                              
					                            </tr>
					                            <tr>			                        	   
			  	    						        		<td >
			  	    						            	<asp:textbox id="emfrom" runat=server size=100 /></td> 
			  	    						    		</tr>
			  	    			            </table><br>
			  	    			            <table width=80%>
					                            <tr>
					                              <td><b>Email Subject</b></td>					                              
					                            </tr>
					                            <tr>			                        	   
			  	    						        		<td >
			  	    						            	<asp:textbox id="emsubject" runat=server size=160 /></td> 
			  	    						    		</tr>
			  	    			            </table><br>
			  	    			            <table width=50%>
					                            <tr>
					                              <td width=60><b>Email Key</b></td>	
					                             	<td  width=85%><asp:linkbutton id="ddcpclip" Text= "Copy To Clipboard" runat="server" Font-underline="false" Style="cursor:hand"  /> </td>				                              
					                            </tr>
					                            <tr>			                        	   
			  	    						        		<td colspan=2><font size=3>
			  	    						            	<asp:label id="emkey" text="http://www.webmagicportal.com/intake.aspx?placeholderfornow" runat=server /></font></td> 
			  	    						            
								                        
			  	    						    		</tr>
			  	    			            </table>
			  	    		        	</div>
 			      			        </asp:panel>
 			      			        <asp:panel id="pnlpage1" runat=server visible=false >
 			      			            <div style="margin:0;background: #4682B4;padding:1;"></div>
 			       				        <div id="fieldtitles" style="vertical-align: top; height: 460px;margin:5; ">
	 			       		                <table width=100%>
	 			       		                    <tr>
	 			       		                        <td width=70>Templates:</td>
	 			       		                        <td width=650><asp:DropDownList ID="ddemailcor" visible=true AutoPostBack =true OnSelectedIndexChanged ="doLtemplates"  DataValueField="email_tbl_pk" Runat="server" /></td>			  	    							
	 			       		                        <td width=70 align=center><asp:linkbutton id="ddemailPreview" Text= "Preview" onclick=tempreview runat="server" Font-underline="false" Style="cursor:hand"  /> </td>
								                        <td >|</td>
								                        <td width=70 align=center><asp:linkbutton id="ddemailInsert" Text= "Insert"  onclientclick="inserttext();return false;" runat="server" Font-underline="false" Style="cursor:hand"  /> </td>
													    		<td >|</td>
								                        <td width=150 align=center><asp:linkbutton id="ddemailsave" Text= "Save As Template" onclick="tempsave" runat="server" Font-underline="false" Style="cursor:hand"  /> </td>
													    		<td >|</td>
		 			       		                     <td width=70 align=center><asp:linkbutton id="Ctxt" Text= "Clear" onclick="clrtxtbox" runat="server" Font-underline="false" Style="cursor:hand"  /> </td>
																<td width=10%></td>
	 			       		                    </tr>
	 			       		                    <tr>
	 			       		                        <td colspan=10><ed:Editor ShowQuickFormat="false" FixedToolbar="false"  AutoFocus="false" height="450" width="1100" submit="false" PathPrefix="Editor_data/" FlashBrowse="myFlashBrowse.aspx" MediaBrowse="myMediaBrowse.aspx" ImageBrowse="Editor_data/myImageBrowseClient.aspx" UrlBrowse="myUrlBrowse.aspx" id="emtext" PreviewMode="true" runat="server" /></td>
	 			       		                    </tr>
	 			       		                </table>
 			       				        </div>
 			      			        </asp:panel>
 			      			        <asp:panel id="pnlpage2" runat=server visible=false >
 			      			            <div style="margin:0;background: #4682B4;padding:1;"></div>
		       				            <div id="fieldtitles" style="vertical-align: top; height: 460px;margin:5; ">
				       				   		<asp:panel id="pnlasscamps" runat=server visible=true >
				       				   		    	<table>
				       				   	      		<tr>
				       				   	      			<td>Associated Email Campaigns</td>
				       				   	      		</tr>
				       				   	      	</table>
				       				   	      	<table>
				          						      	<tr>
				          						         	<td>
					          				                    <asp:DataGrid	            
									        	                    ID="dgcampaigns" 
									        	                    AutoGenerateColumns=False
									        	                    Width="100%"
								        		                    AllowPaging="false" 
											                    	
								          	                    Runat=server CssClass="dg">
								        		                    <HeaderStyle CssClass="dgheaders" />
								        		                    <ItemStyle CssClass="dgitems" />
								        		                    <AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>
						                	                    <Columns >
					    		             	               
			       				   	  						  		</Columns>
									        				            </asp:DataGrid>
				          							         </td>
				          						         </tr>
				          					         </table>
				       				   	         <table>
					                     	      	<tr>
					                        	      	<td align=left ><asp:button id="b_adtocamp" runat=server text="Add To Campaign" onclick="addtocamp"  CausesValidation="false" cssclass=frmbuttonsXLG /></td>		
										  		             </tr>
							  					    		</table>
							  						</asp:panel>
							  						<asp:panel id="pnladdtocamp" runat=server visible=false >
							  							<table>
			       				   	      		<tr>
			       				   	      			<td>Add to a Campaign</td>
			       				   	      		</tr>
			       				   	      	</table>
							  							<br>
							  							<table>
							  								<tr>
							  									<td width=70>Email Campaigns:</td>
							  									<td ><asp:linkbutton id="ddaddnewcampaing" Text= "Add New"  onclick="addnewemcamp" runat="server" Font-underline="false" Style="cursor:hand"  /> </td>
								                     </tr>
								                     <tr>
	 			       		                        <td colspan=2><asp:DropDownList ID="ddcamps" visible=true  DataValueField="cmp_tbl_pk" Runat="server" /></td>			  	    							
	 			       		                     </tr>
	 			       		                  </table>
	 			       		                  <br>
	 			       		                  <table>
													<tr>
														<td><b>Frequency</b></td>
													</tr>
													<tr>
														<td width=20% valign=top>
															<table>
																<tr>
																	<td><asp:checkbox id=chkrunonce runat=server text="On New Lead" autopostback=true oncheckedchanged="updatedreq" /></td>
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
																												
															</div>
															
															
															
														</td>
													</tr>
												</table>
												
							  						</asp:panel>
							  						
							  						
							  						
						 			    		</div>
 			      			        </asp:panel>	
 			      			        
 			      			        <asp:panel id="pnlpage3" runat=server visible=false >
 			      			            <div style="margin:0;background: #4682B4;padding:1;"></div>
 			       				        <div id="fieldtitles" style="vertical-align: top; height: 460px;margin:5; ">
	 			       				    	   <table>
	 			       				    	   	<tr>
	 			       				    	   		<td>Send Email Now</td>
	 			       				    	   	</tr>
	 			       				    	   </table>
	 			       				    	   <br>
	 			       				    	   <asp:panel id="pnlsenmail" runat=server visible=true >
		 			       				    	   <table>
		 			       				    	   	<tr>
		 			       				    	   		<td>Send To:</td>
		 			       				    	   		<td><asp:DropDownList id="dd_Sendemto" runat="server"  nSelectedIndexChanged ="semrefresh" autopostback=true>
					    							                <asp:ListItem Value="Individual" Text="Individual"/>
					  	    						                <asp:ListItem Value="Leads" Text="Lead(s)"/>
					  	    						                <asp:ListItem Value="Groups" Text="Group(s)"/>				  	    						               
					 								                </asp:DropDownList></td>
		 			       				    	   	</tr>
		 			       				    	   </table>
	 			       				    	   </asp:panel>
	 			       				    	   <asp:panel id="pnlsenmailLeads" runat=server visible=true >
	 			       				    	   
	 			       				    	   
	 			       				    	   </asp:panel>
	 			       				    	   
	 			       				    	   <asp:panel id="pnlsenmailgroups" runat=server visible=true >
	 			       				    	   
	 			       				    	   
	 			       				    	   </asp:panel>
	 			       				    	   
	 			       				    	   <asp:panel id="pnlsenmailindivid" runat=server visible=true >
	 			       				    	   
	 			       				    	   
	 			       				    	   </asp:panel>
	 			       				    	   
	 			       				    	   
	 			       				    	   
	 			       				    	   
 			       				        </div>
 			      			        </asp:panel>	 	    
							 	    <asp:panel id="pnlpage4" runat=server visible=false >
 			      			            <div style="margin:0;background: #4682B4;padding:1;"></div>
 			       				        <div id="fieldtitles" style="vertical-align: top; height: 460px;margin:5; ">
 			       				        </div>
 			      			        </asp:panel>	
 			      			        <asp:panel id="pnlpage5" runat=server visible=false >
 			      			            <div style="margin:0;background: #4682B4;padding:1;"></div>
 			       				        <div id="fieldtitles" style="vertical-align: top; height: 460px;margin:5; ">
 			       				           
 			       				        </div>
 			      			        </asp:panel>
 			      			        <asp:panel id="pnlpage6" runat=server visible=false >
 			      			            <div style="margin:0;background: #4682B4;"></div>
 			       				        <div id="fieldtitles" style="vertical-align: top; height: 460px;margin:5; ">
 			       				 
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
	