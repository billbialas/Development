<%@ Page language="vb" Codebehind="postADI.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.postADI" Debug="false" trace="false" aspcompat=true  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script language="JavaScript" src="../_include/default.js"></script>
<link rel="stylesheet" href="../_include/default.css" type="text/css">

<HTML>
	<HEAD>
		<title>www.WebMagicPortal.com</title>
	</HEAD>
	
<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">
	<form id="forms1a" runat="server" enctype="multipart/form-data" method="post">
		<asp:panel id=pnlpostdue runat=server visible=false>
			<table>
				<tr>
					<td class=wfstepheaderA>Publish AD</td>
				</tr>
			</table><br>
			<table>
				<tr>
					<td colspan=2><font color=red size=3><b>You have existing unpublished records associated to this ad!</b></font></td>
				</tr>
			</table><br>
			<table>
				<tr>
					<td>Would you like to view these?</td>
				
					<td><asp:button id="btnunpubposts" runat=server text="Yes"  onclick="Addexstpost" visible=true  CausesValidation="false" cssclass=frmbuttonsLG /></td>		
					<td><asp:button id="btnnewpost" runat=server text="No"  visible=true onclick="Addnewpost" CausesValidation="false" cssclass=frmbuttonsLG /></td>		
					<td><asp:button id="btnnewpostC" runat=server text="Cancel"  visible=true onclick="AddnewpostC" CausesValidation="false" cssclass=frmbuttonsLG /></td>		
									  
				</tr>
			</table><br>
			<table>
				<tr>
					<td><b><i>Select NO to publish your AD immediately.</i></b></td>
				</tr>
				<tr>
					<td><b><i>Select YES to work with pre-existing publishing records.</i></b></td>
				</tr>
			</table>	
		
		</asp:panel>
		<asp:panel id=pnlNewpost runat=server visible=false>
				<table>
									<tr>
										<td class=wfstepheaderA>Publish AD</td>
									</tr>
								</table><br><table>
				<tr>
					<td valign=top width=20%>
					
								<table cellspacing=0 cellpadding=0>
									<tr>
										<td width=140><b>Select Venues</b></td>
									
	    		    					<td width=60><asp:linkbutton visible=true id="vinstr" Text= "Details" runat="server"  onClick="shownotes" /></td>
	    		    					<td ><asp:linkbutton visible=true id="vcal" Text= "Calendar" runat="server"  onClick="showCalendar" /></td>
	    		    
										
									</tr>
									<tr>
										<td><font color=red><asp:label visible=false id=lblnovensel text="Please choose a venue!" runat=server /></font></td>
									</tr>
								</table>
								<table cellspacing=0 cellpadding=0>
									<tr>
										<td ><asp:listbox id="lb_selvenues" runat="server"  selectionmode="multiple" 	DataTextField="x_descr" DataValueField="tbl_xwalk_pk" enabled=true height=320 width=280 /></td>
									</tr>
								</table>
							</td>
							<td valign=top >
								<asp:panel id=pnlselpostdays runat=server visible=true >
										<table>
							    			<tr>
							    				<td ><b>Select your Publishing Days</b></td>
							    			</tr>
							    		
							    			<tr>
							    				<td >
							    				<asp:Calendar id="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChangedAA"
							    				 backcolor="#ffffff" width="260px" height="260px" 
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
							    				
							    					</asp:Calendar></td>
							    			</tr>
							    				<tr>
							    				<td>* Click on the date you wish to post on.  When selected calendar date will change to light gray.</td>
							   			</tr>
							   			<tr>	
							   				<td>To unselect click on the date again.</td>
							    			</tr>
							    		</table>
							    	</asp:panel>
							    
							    	
							    	
							    	
    						</td>
    						<td valign=top>
    							<asp:panel id=pnlvnotes runat=server visible=false >
    								<table>
    									<tr>
    										<td><b>Venue Notes and Instructions</b></td>
    									</tr>
    								</table>
									<table width=100%>
    									<tr>
    										<td>
    											 <div  runat=server style="vertical-align top; height: 320px; overflow:auto;">
		    						    		<asp:DataGrid Runat=server visible=true
										   		ID="VenueNotes" 
						                    	AutoGenerateColumns=False
						                    	Width="100%"          
										    		AllowPaging="false"   
													
													CssClass="dg">
					    	            		<HeaderStyle CssClass="dgheaders" />
								        			<ItemStyle CssClass="dgitems" />
								        			<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>
									       		<Columns >	
									       			<asp:BoundColumn HeaderText="ID" visible=false DataField="tbl_xwalk_pk" ItemStyle-Width="40px" readonly=true   />
				           		    				<asp:BoundColumn HeaderText="Venue"  DataField="x_descr" ItemStyle-Width="160px" readonly=true    />    
				        								<asp:BoundColumn HeaderText="Notes"  DataField="x_notes" ItemStyle-Width="160px" readonly=true    />    
				        								<asp:BoundColumn HeaderText="Instructions"  DataField="x_instructions" ItemStyle-Width="160px" readonly=true    />    
				        						
									       			
				        			  				</Columns>
							      			</asp:DataGrid></div>						    	
									    	</td>
									    </tr>
									  </table>
						    	</asp:panel>
							    	<asp:panel id=pnlvints runat=server visible=false >
							    	
							    	
							    	</asp:panel>
    						</td>
    					</tr>
    				</table>
				
			<table>
				<tr>
					<td><asp:checkbox id=chkassociatepaln text="Associate this Publishing with a pre-existing AD Plan" runat=server autopostback=true oncheckedchanged="toggleplan" /></td>
					<td><asp:dropdownlist id="DD_existplan" runat="server"  
					 				DataTextField="x_descr" DataValueField="tbl_xwalk_pk" enabled=false /></td>
				</tr>
			</table>
			
			
			<table>
				<tr>
					<td><asp:button id="btnSPExit" runat=server text="Save & Exit"  onclick="Button1_Click" visible=true  CausesValidation="false" cssclass=frmbuttonsXLG /></td>		
					<td><asp:button id="btnSPPost" runat=server text="Save & Publish" onclick="Button1_Click" visible=true  CausesValidation="false" cssclass=frmbuttonsXLG /></td>		
					<td><asp:button id="btnPexit" runat=server text="Cancel"  visible=true onclick="Button1_Click" CausesValidation="false" cssclass=frmbuttonsXLG /></td>		
				
				</tr>
			</table>
		
		</asp:panel>
		
		<asp:panel id=pnlPublishAds runat=server visible=false>
			<table>
				<tr>
					<td>Publish ADs</td>
				</tr>
			</table>
			<table>
				<tr>
					<td>Below are the venues you selected to publish.</td>
				</tr>
			</table>
			 <table width=100%>   	
      			<tr>
	            	<td >
	               <div id="ppgrid" runat=server style="vertical-align top; height: 420px; overflow:auto;">
				 			<asp:DataGrid Runat=server visible=true
						   		ID="ADVenuesPP" 
		                    	AutoGenerateColumns=False
		                    	Width="100%"          
						    		AllowPaging="false"            
					            
									DataKeyField="tbl_leadadvenues"
									CssClass="dg">
	    	            		<HeaderStyle CssClass="dgheaders" />
				        			<ItemStyle CssClass="dgitems" />
				        			<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>
					       		<Columns >
						       		<asp:BoundColumn HeaderText="ID" visible=false DataField="venno" ItemStyle-Width="40px" readonly=true   />
		           		    		<asp:BoundColumn HeaderText="Venue"  DataField="av_name" ItemStyle-Width="160px" readonly=true    />    
		        						<asp:BoundColumn HeaderText="Can Self Publish" visible=false DataField="x_canselfpub" ItemStyle-Width="140px" readonly=true  />
		                        <asp:BoundColumn HeaderText="Ad Code" visible=false DataField="av_key" ItemStyle-Width="60px" readonly=true    />
		        	        			<asp:BoundColumn HeaderText="Status" visible=true DataField="av_adplaced" ItemStyle-Width="60px" readonly=true  />
		        			  			<asp:TemplateColumn HeaderText="Target Post Date" ItemStyle-Width="100px"  ItemStyle-CssClass="dgitemsNOBD" visible=false>
				            			<ItemTemplate >
				                			<table width=100%>
				                   			 <tr>
				                         			<td><asp:textbox id="txtTPD" runat=server  /></td>		
		         								 </tr>    
				                			</table>   
				            			</ItemTemplate>                                                     
			            			</asp:TemplateColumn>
			            			<asp:BoundColumn HeaderText="Target Post Date" visible=true DataField="APFTo" readonly=true ItemStyle-Width="100px"    />
			                		<asp:TemplateColumn HeaderText="Published Date" ItemStyle-Width="100px"  ItemStyle-CssClass="dgitemsNOBD" visible=true>
				            			<ItemTemplate >
				                			<table width=100%>
				                   			 <tr>
				                   			 		<td><asp:label id=lblPdate runat=server /></td>
				                         			<td><asp:textbox id="txtPdate" runat=server visible=false  /></td>		
		         								 </tr>    
				                			</table>   
				            			</ItemTemplate>                                                     
			            			</asp:TemplateColumn>
			                		
			                		
	        							
	        							<asp:TemplateColumn HeaderText="Posting Number" ItemStyle-Width="100px"  ItemStyle-CssClass="dgitemsNOBD" visible=true>
				            			<ItemTemplate >
				                			<table width=100%>
				                   			 <tr>
				                   			 		<td><asp:label id=lblPNO runat=server /></td>
				                         			<td><asp:textbox id="txtPNO" runat=server visible=false  /></td>		
		         								 </tr>    
				                			</table>   
				            			</ItemTemplate>                                                     
			            			</asp:TemplateColumn>
	        							
	        							
	        							<asp:TemplateColumn HeaderText="" ItemStyle-Width="20px"  ItemStyle-CssClass="dgitemsNOBD" visible=false>
				            			<ItemTemplate >
				                			<table width=100%>
				                   			 <tr>
				                         			<td><asp:button id="BTNSetPub" runat=server text="Set Published"  CausesValidation="false" cssclass=frmbuttonsLG /></td>		
		         								 </tr>    
				                			</table>   
				            			</ItemTemplate>                                                     
			            			</asp:TemplateColumn>
			                		<asp:TemplateColumn HeaderText="" ItemStyle-Width="20px"  ItemStyle-CssClass="dgitemsNOBD" visible=true>
				            			<ItemTemplate >
				                			<table cellpadding=0 cellspacing=0>
				                   			 <tr>
				                         			<td><asp:button id="PublishPP" runat=server text="Self Publish"  CausesValidation="false" cssclass=frmbuttonslg /></td>		
		         										<td><asp:button id="UpdatePN" runat=server text="Edit Posting No"    CausesValidation="false" cssclass=frmbuttonsLG /></td>		
		         								
		         								 </tr>    
				                			</table>   
				            			</ItemTemplate>                                                     
			            			</asp:TemplateColumn>
			            			<asp:BoundColumn HeaderText="<font color=#FFF8C6><b></b></font>" visible=false DataField="av_leadads_FK" ItemStyle-Width="80px"    />
  										<asp:BoundColumn HeaderText="Status" visible=false DataField="av_Postingno" ItemStyle-Width="60px" readonly=true  />
		        			  			<asp:BoundColumn HeaderText="Status" visible=false DataField="APFrom" ItemStyle-Width="60px" readonly=true  />
		        			  		
		        			  		</Columns>
					      </asp:DataGrid>
					  	</div>
			    		</td>
			    	</tr>
			 	</table>  

			
			<table>
				<tr>
					<td><asp:button id="btnExitPosting" runat=server text="Exit"  visible=true  CausesValidation="false" cssclass=frmbuttonsXLG /></td>		
		
				</tr>
			</table>
			
	
	
	
		</asp:panel>
 
		
	</form>
	</body>	
</HTML>
