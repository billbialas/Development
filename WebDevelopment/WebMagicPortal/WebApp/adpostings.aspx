<%@ Page language="vb" Codebehind="adpostings.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.adpostings" Debug="false" trace="false" aspcompat=true  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script language="JavaScript" src="../_include/default.js"></script>
<link rel="stylesheet" href="../_include/default.css" type="text/css">

<HTML>
	<HEAD>
		<title>www.WebMagicPortal.com</title>
	</HEAD>
	
<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">
	<form id="forms1a" runat="server" enctype="multipart/form-data" method="post">
	  	<asp:panel id=pnlPMain runat=server visible=true>
	  	<asp:panel id=pnlTempHide runat=server visible=false>
	  	
			  	<table width=50%>
					<tr>
						<td style="font-size:110%;font-weight:bold;">View By</td>
						<td><asp:checkbox id=chkvwAll text="All Postings" runat=server autopostback=true OnCheckedChanged="showbydate" /></td>
					
						<td><asp:checkbox id=chkvwdate text="Date" runat=server autopostback=true OnCheckedChanged="showbydate" /></td>
					
						<td width=20%><asp:checkbox id=chkvwven text="Venue" runat=server autopostback=true OnCheckedChanged="showbydate" /></td>
					   
					 	
					</tr>
				</table>
		</asp:panel>
		<table width=30%>
			 		<tr>
			 				<td class=wfstepheaderA><asp:label id=lbpgtitle runat=server /></td>
			 				<td align=right ><asp:button id="btnexitWPosts" runat=server text="Exit"   autopostback=true onclick="exitout"  CausesValidation="false" cssclass=frmbuttonsLG /></td>		
				   </tr>
				 </table>
		<table width=99%>
           		<tr>
           			<td><b>Search</b> &nbsp <asp:linkbutton id="clear" Text= "Clear" 
                   			 runat="server" Font-Bold="True" Font-underline="True" Style="color:#ff0000; font-family:arial; font-size:9pt; cursor:hand"
                    			onClick="clearall" /></td>
           			<td class=LeadDropsNoBold><b>Target Post Date</b></td>
           			<td class=LeadDropsNoBold><b>Published Status</b></td>
           			<td class=LeadDropsNoBold><b>Ad #</b></td>
           			<td class=LeadDropsNoBold><b>AD Plan #</b></td>
           			<td class=LeadDropsNoBold><b>Venue</b></td>
           		</tr>
           		<tr>
           			<td width=20%><asp:textbox id="Pl_search" runat=server size=30  autopostback="true" ontextchanged="filterVenuesA" onmouseover="document.getElementById('myToolTip').className='activeToolTip'"
                      onmouseout="document.getElementById('myToolTip').className='idleToolTip'"  /></td>
           			<td><asp:DropDownList ID="dd_PTDue" runat=server Visible=true autopostback=true  OnSelectedIndexChanged="filterVenues" cssclass=LeadDropsNoBold>
            			  	<asp:ListItem Value="All" Text="All"/>
    	            		<asp:ListItem Value="Due Today" Text="Due Today"/>
    	            		<asp:ListItem Value="Yesterday" Text="Yesterday"/>
    	            		<asp:ListItem Value="Past Due" Text="Past Due"/>
                			<asp:ListItem Value="In 2 Days" Text="In 2 Days"/>
                			<asp:ListItem Value="In 5 Days" Text="In 5 Days"/>
                			</asp:DropDownList></td> 
                	<td><asp:DropDownList ID="dd_PStat" runat=server Visible=true autopostback=true  OnSelectedIndexChanged="filterVenues" cssclass=LeadDropsNoBold>
            			  	<asp:ListItem Value="All" Text="All"/>
    	            		<asp:ListItem Value="Published Text="Published"/>
                			<asp:ListItem Value="Unpublished" Text="Unpublished"/>
                			<asp:ListItem Value="Canceled" Text="Canceled"/>
                			</asp:DropDownList></td> 
                	<td><asp:DropDownList ID="dd_ADs" DataTextField="Ftitle" autopostback=true  OnSelectedIndexChanged="filterVenues" cssclass=LeadDropsNoBold
                          
	                        DataValueField="tbl_leadad_pk" 
	                        Runat="server" /></td>	
	              <td><asp:DropDownList ID="dd_ADPlan" DataTextField="Ftitle" autopostback=true  OnSelectedIndexChanged="filterVenues" cssclass=LeadDropsNoBold
                          
	                        DataValueField="lplanno" 
	                        Runat="server" /></td>	
	              <td><asp:DropDownList ID="dd_Venues" DataTextField="x_descr" DataValueField="tbl_xwalk_pk" cssclass=LeadDropsNoBold
	              						autopostback=true  OnSelectedIndexChanged="filterVenues" Runat="server" /></td>	
	                        
	          				    			         		    			                  
           		</tr>
           	</table>
           	</asp:panel>
           	<asp:panel id=pnlDG1 runat=server visible=false>
           	<table width=100%>   	
   				<tr>
	            	<td >
	               <div id="ppgridDate" runat=server style="vertical-align top; height: 420px; overflow:auto;">
				 			<asp:DataGrid Runat=server visible=true
						   		ID="ADVenuesPPDate" 
		                    	AutoGenerateColumns=False
		                    	Width="100%"          
						    		
											
						    		CssClass="dg">
	    	            		<HeaderStyle CssClass="dgheaders" />
				        			<ItemStyle CssClass="dgitems" />
				        			<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>
					       		<Columns >
						       		<asp:BoundColumn HeaderText="Target Post Date" visible=true DataField="APTo" readonly=true ItemStyle-Width="100px"    />
			                		  		<asp:BoundColumn HeaderText="Unpublished Count" visible=true DataField="Unpubv" readonly=true ItemStyle-Width="100px"    />
			                 		  		<asp:BoundColumn HeaderText="Published Count" visible=true DataField="Pubv" readonly=true ItemStyle-Width="100px"    />
			               
		        			  		
		        			  		</Columns>
					      </asp:DataGrid>
					  	</div>
			    		</td>
			    	</tr>
			 	</table>  
			 	</asp:panel>
			 	<asp:panel id=pnlDG2 runat=server visible=false>
			 	<asp:Label id="lblOrderBy" runat="server" Visible="False" />
 				 <table width=99%>   	
   				<tr>
	            	<td >
	               <div id="ppgrid" runat=server style="vertical-align top; height: 420px; overflow:auto;">
				 			<asp:DataGrid Runat=server visible=true
						   		ID="ADVenuesPP" 
		                    	AutoGenerateColumns=False
		                    	Width="100%"          
						    		AllowPaging="false"            
					            PageSize="8" 
					            PagerStyle-Mode="NumericPages" 
									OnPageIndexChanged="MyDataGrid_PagePP" 
									DataKeyField="tbl_leadadvenues"
									OnItemDataBound="ItemDataBoundEventHandlerAVens"
			               	AllowSorting="True"
               				OnSortCommand="SortCommand_Click"
			                   
						    		CssClass="dg">
	    	            		<HeaderStyle CssClass="dgheaders" />
				        			<ItemStyle CssClass="dgitems" />
				        			<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>
					       		<Columns >
						       		<asp:BoundColumn HeaderText="ID" visible=false DataField="venno" ItemStyle-Width="40px" readonly=true   />
		           		    		<asp:BoundColumn HeaderText="<font color=red>*</font>Venue"  DataField="av_name" ItemStyle-Width="160px" readonly=true SortExpression="av_name"    />    
		        						<asp:BoundColumn HeaderText="Can Self Publish" visible=false DataField="x_canselfpub" ItemStyle-Width="140px" readonly=true  />
		                        <asp:BoundColumn HeaderText="Ad Code" visible=false DataField="av_key" ItemStyle-Width="60px" readonly=true    />
		        	        			<asp:BoundColumn HeaderText="Status" visible=true DataField="av_adplaced" ItemStyle-Width="60px" readonly=true  />
		        			  			<asp:BoundColumn HeaderText="<font color=red>*</font>Target Date" visible=true DataField="APFTo" readonly=true ItemStyle-Width="100px" SortExpression="APFTo"    />
			                		<asp:BoundColumn HeaderText="Pub. #" visible=true DataField="av_Postingno" ItemStyle-Width="60px" readonly=true  />
		        			  			<asp:BoundColumn HeaderText="<font color=red>*</font>Pub. Date" visible=true DataField="APFrom" ItemStyle-Width="70px" readonly=true SortExpression="APFrom" />
		        			  			<asp:BoundColumn HeaderText="AD No" visible=false DataField="adno" ItemStyle-Width="60px" readonly=true  />
		        			  			<asp:BoundColumn HeaderText="AD Plan No" visible=false DataField="adplanno" ItemStyle-Width="60px" readonly=true   />
		        			  			<asp:BoundColumn HeaderText="AD Plan No" visible=false DataField="ad_stage" ItemStyle-Width="60px" readonly=true  />
		        			  			<asp:BoundColumn HeaderText="<font color=red>*</font>AD" visible=true DataField="sadtitle" ItemStyle-Width="250px" readonly=true SortExpression="sadtitle"  />
		        			  			<asp:BoundColumn HeaderText="<font color=red>*</font>AD Plan" visible=true DataField="saptitle" ItemStyle-Width="190px" readonly=true  SortExpression="saptitle"  />
		        			  			
		        			  			
		        			  			<asp:TemplateColumn HeaderText="" ItemStyle-Width="20px"  ItemStyle-CssClass="dgitemsNOBD" visible=true>
				            			<ItemTemplate >
				                			<table  cellspacing=0 cellpadding=0>
				                   			 <tr>
				                         			<td><asp:button id="BTNSetPub" runat=server text="Self Publish" autopostback=true onclick="publishposting"   CausesValidation="false" cssclass=frmbuttons /></td>		
		         								  		<td><asp:button id="BTNUpdate" runat=server text="Update"  autopostback=true onclick="updateposting"  CausesValidation="false" cssclass=frmbuttons /></td>		
		         								  		<td><asp:button id="BTNcancel" runat=server text="Cancel Date"   autopostback=true onclick="cancelposting" CausesValidation="false" cssclass=frmbuttons/></td>		
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
				<asp:panel id="pnlLSdetail" runat=server visible=false >
              	<table width=100%>
              		<tr>
              			<td>
              				<table width=75% style="margin-top:10">
              					<tr>
              						<td colspan=5 class=wfstepheaderA>Update Publishing Information</td>
              					</tr>
			              		<tr>
			              			<td><b>AD NO</b></td>
			              			<td><b>AD Stage</b></td>
			              			
			              			<td><b>Selected Venue</b></td>
			              			<td><b>Status</b></td>
			              			<td><b>Target Publish Date</td>
			              			<td><asp:linkbutton id="lnkEPubDate" Text= "Published Date" CausesValidation="false"  runat="server" cssclass=linkbuttonsRed  visible =true onClick="showcalendar2" /></td>
			              			<td><b>Posting No</b></td>
			              		</tr>
			              		<tr>
			              			<td><asp:label id=pstsadno runat=server /></td>
			              			<td><asp:label id=pstsadstage runat=server /></td>
			              			<td><asp:label id=pstsvenue runat=server /></td>
			              			<td><asp:label id=pststatus runat=server /></td>
			              			<td><asp:label id=pstEPdate runat=server /></td>
			              			<td><asp:textbox id="pstadfrom" runat=server /></td>
			              			<td><asp:textbox id="pstadto" runat=server /></td>
			              		</tr>
			              </table>
              			</td>
              			<td>
				         	<table>
									<tr>
										<td align=right><asp:linkbutton id="showcalc" Text= "Close" CausesValidation="false"  runat="server"   visible=false onClick="closecalendar2" /></td>
					                </tr>
					                <tr>
	                                    <td colspan=10><asp:calendar id="cdrCalendar2" runat="server" 
	                                            OnSelectionChanged="Calendar2_SelectionChanged"
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
							</tr>
						</table>
                 <table>
                 		<tr>
                 			<td><asp:button id="BTNSetPub" runat=server text="Save Published" onclick="MarkPublished" CausesValidation="false" cssclass=frmbuttonsxlg /></td>		
                      	<td><asp:button id="BTNPublater" runat=server text="Save Unpublished" onclick="PublishLAter" CausesValidation="false" cssclass=frmbuttonsxlg /></td>		
   			        		<td><asp:button id="BTNCancel" runat=server text="Cancel" onclick="ExitADV" CausesValidation="false" cssclass=frmbuttonsxlg /></td>		
   			      	</tr>
   			      </table>
              	 </asp:panel>	
		
				
				
	</form>
	</body>	
</HTML>
