<%@ Page language="vb" Codebehind="default.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.default1" Debug="false" trace="false" validateRequest=false %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %> 
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN">
<script language="JavaScript" src="../_include/default.js"></script>
<link rel="stylesheet" href="../_include/default.css" type="text/css">
<HTML>
	<HEAD>
		<title>www.WebMagicPortal.com</title>
	
		
	</HEAD>

	
<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">

	<form id="forms1" runat="server"  >
		   <asp:panel ID="pnl_upgrade" runat="server" Visible="false">
				<table>
					<tr>
						<td>Thank you for your purchase.  please click 'Continue'</td>
					</tr>
					<tr>
						<td><asp:button id="btnupcontinue" Visible=true  runat=server text="Continue" width="70" onclick="click_upcon" CausesValidation="False"  cssclass=frmbuttons /></td>					
					</tr>			
				</table>				
		   </asp:panel>
		     	
           <asp:panel ID="pnl_activeuser" runat="server" Visible="false"> 
           <table width=100%>
                <tr>
                    <td width=10% valign=top>
                        <table width=100% border=0 cellspacing=0 cellpadding=0>
                            <tr>
                                <td  valign=top><img id="Logoarea" alt="" visible=true src="images/default.jpg" height=40 width=40 border="0" runat=server></td>
	                        </tr>
	                        <tr>    
	                            <td valign=top width=30%><font size=2><asp:label id="Welcomemessage" runat="server"/></font>  </td>
				            </tr>
				        </table>            
                    
                    </td>
                    <td>                   
		                        <table width=100% border=0 cellspacing=2 cellpadding=3>
        	                        <tr>
        	                            <td width=60% valign=top>
					     	                <asp:panel id=pnlreminders runat=server visible=false >
						     	                <table width=100%>
						     		                <tr>
						     			                <td style="color:#ff0000;font-weight:bold;font-size:110%;" width=80>Reminders:</td>
						     			                <td width=85%><asp:linkbutton id=lnkhidereminders text="Hide" runat=server onclick="hidereminders" visible=true/></td>
						     		                </tr>
						     		                <tr>
						     			                <td colspan=2><asp:label id=lblunpubads runat=server visible=false text="You have unpublished ADs due to be publsihed! <a href='adpostings.aspx?source=admanagerQ&org=default'>Click to view</a>" /></td>
						     		                </tr>
						     	                </table>    
						                   </asp:panel>		
					                     </td>
				                        <td valign=top>
			           		                <table>
				        		                <tr>
				        			                <td valign=top><asp:ImageButton id="imgbtnupgrade" runat="server"  AlternateText="Purchase Upgrades" ImageAlign="left" ImageUrl="../images/upgrade-icon.gif" height=40 OnClick="buyupgrades" /></td>
			             		                    <td align=center><asp:ImageButton id="btn_sysinfo" runat="server" visible=true AlternateText="View System Info" ImageAlign="left" ImageUrl="../images/InfoIcon.png" height=30  OnClick="viewsystinfo" /></td>
			   						                <td align=center><asp:ImageButton id="btn_Help" runat="server" visible=false AlternateText="Help" ImageAlign="left" ImageUrl="../images/helpi.jpg" height=30  OnClick="viewhelp" /></td>
			   					                    <td align=center><asp:ImageButton id="btn_video1" runat="server" visible=false AlternateText="Welcome Message" ImageAlign="left" ImageUrl="../images/videoicon.jpg" height=30  OnClick="playvideo1" /></td>
			             		                </tr>			             		
			             	                </table>
			                            </td>
			                        </tr>			      
		                        </table>
		                        <asp:panel id="pnlhelp" runat=server visible=false>	         			
	         		                <table  width=50%>
	        				                <tr>
	        					                <td><b><u>Help</u></b>&nbsp&nbsp&nbsp&nbsp<asp:linkbutton id="btnhidehelp" Text= "Hide Help screen"  runat="server"  forecolor="red" Font-underline="true" Style="cursor:hand" onClick="click_hidehelp" /></td>
	        				                </tr>
	        	                    </table>	        		
	         		                <table class=tblbordera width=50%>
                	        				
	        				                <tr>
	        					                <td><FTB:FreeTextBox id="txthelp" runat="server" width="960" height=260 />		</td>
	        				                </tr>
                	        				
	        		                </table>
	        	                </asp:panel>
		                        <asp:panel id="pnlsysinfo" runat=server visible=false>
                	         			
	         		                <table  width=50%>
	        				                <tr>
	        					                <td><b><u>Latest System Info</u></b>&nbsp&nbsp&nbsp&nbsp<asp:linkbutton id="btnhidesysnews" Text= "Hide System Info"  runat="server"  forecolor="red" Font-underline="true" Style="cursor:hand" onClick="click_hidesysnews" /></td>
	        				                </tr>
	        			                </table>	        		
	         		                <table class=tblbordera width=50%>	        				
	        				                <tr>
	        					                <td><asp:textbox id="txtsysnotes" runat=server size=16 TextMode="MultiLine" Columns="130"  Rows="20" enabled=true /></td>
	        				                </tr>	        				
	        		                </table>
	        	                </asp:panel>	        	
	        	                <asp:panel id="pnlallinfo" runat=server visible=true>	         
		        <table>
		            <tr> 
		                <td id="New Leads" width=100%>
		                    <table width=90% border=0>
		    	                <tr>
			    	                <td width=120>Lead Summary</td>
			    	                 <td width=70><asp:linkbutton id="leadnew" Text= "New Leads" visible=true
                    						runat="server" cssclass=linkbuttons onClick="leadfilternew" /></td>
				                    <td width=75><asp:linkbutton id="lead15" Text= "Last 5 Days" visible=true
                    						runat="server" cssclass=linkbuttons onClick="leadfilter15" /></td>
                  				    <td width=75><asp:linkbutton id="lead30" Text= "Over 30 Days" visible=true
                    						runat="server" cssclass=linkbuttons onClick="leadfilter30"  /></td>
			    	                
			    	                <td align=right>Total=<asp:Label ID="totnewleads" runat=server /></td>
			                    </tr>
			                </table>
			                <table width=90% border=0>
			                    <tr>
			                        <td ><div style="vertical-align: top; height: 160px; overflow:auto;margin-bottom:10;">
					  			        <asp:DataGrid 
									        	ID="newleads" 
  								        		AutoGenerateColumns=False
  								        		Width="100%"
					                    	ColumnHeadersVisible = FALSE  
  								        		AllowPaging="True" 
                                    PageSize="4" 
                                    PagerStyle-Mode="NumericPages" 
                                    OnPageIndexChanged="newleads_PageChanger" CssClass="dg" Runat=server>
  								          	<HeaderStyle CssClass="dgheaders" />
							        			<ItemStyle CssClass="dgitems" />
							        			<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>
								      
   							                <Columns >
				        		               <asp:hyperlinkcolumn runat="server" datatextfield ="tbl_leads_pk" headertext="Lead #" ItemStyle-CssClass="dglinks"
            		                            DataNavigateUrlField ="tbl_leads_pk" DataNavigateUrlFormatString="addlead.aspx?action=view&id={0}&source=home"  ItemStyle-HorizontalAlign="left" ItemStyle-Width="50px" />
        		                                <asp:BoundColumn HeaderText="Name"  DataField="fullname" visible="true" ItemStyle-Width="150px"    />
				        		                <asp:BoundColumn HeaderText="Type"  DataField="ld_type" visible="true" ItemStyle-Width="100px"    />
				        		               <asp:BoundColumn HeaderText="Source"  DataField="ld_adsource" visible="true" ItemStyle-Width="80px"    />
				        		               <asp:BoundColumn HeaderText="Ad Code"  DataField="ld_adcode" visible="false" ItemStyle-Width="100px"    />
				        		                <asp:BoundColumn HeaderText="Ad Title"  DataField="ad_title" visible="true" ItemStyle-Width="250px" ItemStyle-Font-Size=Xx-Small    />
				        		            
				        		               <asp:BoundColumn HeaderText="Date"  DataField="adddate" visible=true ItemStyle-Width="60px"    />
        				        		     		
									        </Columns>
								        </asp:DataGrid>
						            </div></td>
			                    </tr>
			                </table> 
			            </td>  
		                <td id="Leads No Contact" width=0%></td>
		            </tr>
		            
		            <tr>    
		                <td id="Tasks"  width=100%>
		                    <table width=90%>
			                    <tr>
				                   <td width=80 >My Tasks</td>
				                   <td width=70><asp:linkbutton id="Duetoday" Text= "Due Today" visible=true
                    						runat="server" cssclass=linkbuttons 	onClick="taskfilterdue"  /></td>
				                   <td width=60><asp:linkbutton id="AllOpen" Text= "All Open" visible=true
                    						runat="server"	 cssclass=linkbuttons onClick="taskfilteropen"  /></td>
                  				 <td width=70><asp:linkbutton id="Alltasks" Text= "All" visible=true
                    						runat="server" cssclass=linkbuttons	onClick="taskfilterall"  /></td>
                    				 <td width=70><asp:linkbutton id="Addtasks" Text= "Add New" visible=true
                    						runat="server" cssclass=linkbuttons	onClick="taskadd"  /></td>
                  							 <td align=right>Total=<asp:Label ID="totaltasks" runat=server /></td>
                  							 
                  							 
			                    </tr>
                            </table>
                            <asp:Label id="lblOrderBy" runat="server" Visible="False" />
			                <table width=90% border=0>
			                    <tr>
			                        <td>
					  			        <asp:DataGrid 
									        	ID="tasksdue" 
  								       	 	AutoGenerateColumns=False
  								        		Width="100%"
					                   	ColumnHeadersVisible = FALSE  
  								        		AllowPaging="True" 
                                  	PageSize="6" 
                                  	PagerStyle-Mode="NumericPages" 
                                  	AllowSorting="True"
               							OnSortCommand="SortCommand_Click"
                                  	OnPageIndexChanged="myDataGrid_PageChangerA" CssClass="dg"  Runat=server>
								      			<HeaderStyle CssClass="dgheaders" />
							        				<ItemStyle CssClass="dgitems" />
							        				<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>
   							                <Columns >
				        		                   
				                              <asp:BoundColumn HeaderText="<font color=red>*</font>Task #"  DataField="lt_tbl_pk" visible=true ItemStyle-Width="40px"  SortExpression="lt_tbl_pk"  />
				        		                	<asp:BoundColumn HeaderText="<font color=red>*</font>Lead #"  DataField="Lno" visible=true ItemStyle-Width="40px" SortExpression="lt_leadpk_fk"   />
        				        		            <asp:BoundColumn HeaderText="Name"  DataField="ldname" visible=true ItemStyle-Width="100px"    />
        				        		            <asp:BoundColumn HeaderText="<font color=red>*</font>Type"  DataField="lt_type" visible=true ItemStyle-Width="80px"   SortExpression="lt_type" />
				        		                	<asp:BoundColumn HeaderText="Status"  DataField="lt_status" visible=true ItemStyle-Width="60px"    />
				        		                	<asp:BoundColumn HeaderText="Description"  DataField="briefnotes" visible=true ItemStyle-Width="220px"    />
				        		                	<asp:BoundColumn HeaderText="<font color=red>*</font>Due Date"  DataField="Ddate" visible=true ItemStyle-Width="40px"  SortExpression="Ddate"  />
        				        		         	<asp:TemplateColumn HeaderText="" ItemStyle-Width="40px" ItemStyle-CssClass="dgitemsNOBD" >
					                                <ItemTemplate >
					                                    <table width=100%>
					                                        <tr>
					                                            <td><asp:button id="edittaskB" runat=server text="Edit" onclick="Edittask" CausesValidation="false" cssclass=frmbuttons /></td>		
			                                                               <td><asp:button id="bClosetask" runat=server text="Complete" onclick="Closetask" CausesValidation="false" cssclass=frmbuttons /></td>		
			                                   
			                                                </tr>    
					                                    </table>   
					                                </ItemTemplate>                                                     
				                                </asp:TemplateColumn>	
        				        		      
									        </Columns>
								        </asp:DataGrid>
						           </td>
			                    </tr>
		                    </table>
		                
		                </td>
		                <td id="Td2" width=0%></td>
		            </tr>
		        </table>       
		       </asp:panel>
		            </td>
		        </tr>
		    </table>
		    </asp:panel>
		    <asp:panel ID="pnl_inactiveuser" runat="server" Visible="false">
		        <table width=30%>
    	            <tr>
			            <td colspan=2><asp:Label ID="subscriptstat" runat=server /></td>
		            </tr>
		                     
		            <tr>
		              
		                <td><asp:button id="btn_renewA"  runat=server text="Click To Renew " onclick="click_renewsubscriptionA" CausesValidation="False"  cssclass=frmbuttonsXLG /></td>					
				 	    <td><asp:button id="btn_renewcont" Visible=false  runat=server text="Continue Without Renewing"  onclick="click_continuenorenew" CausesValidation="False"  cssclass=frmbuttonsXLG /></td>					
				 	
				 	</tr>
				 	
		        </table>
		    </asp:panel>		    
           
     </form>
</body>	
</HTML>
