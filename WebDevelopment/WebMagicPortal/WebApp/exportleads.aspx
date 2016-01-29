<%@ Page language="vb" Codebehind="exportleads.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.exportleads" Debug="false" trace="false" validateRequest=false  %>
<%@ Register TagPrefix="ed" Namespace="OboutInc.Editor" Assembly="obout_Editor" %>
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
	            <td class=pgheaders>Lead Export</td>
	        </tr>
	    </table>
		<br />
	    <asp:Panel ID="pnlnoleads" runat=server visible=false>
	     <table>
	            <tr>
	                <td colspan=3><font size=3><b>No Leads Selected</b></font></td>
	            </tr>
	            <tr height=10>
	                <td>&nbsp</td></tr>
	            <tr>
	                <td colspan=3>Please choose an option</td>
	            </tr>
	            <tr>
	                <td><asp:Button  id="btnexportall" OnClick="exportall" Text="Export All Leads" runat="server" class=frmbuttonsXLG /></td>
	                <td><asp:Button  id="btnmangeque" OnClick="manageq" Text="Manage Queues" runat="server" class=frmbuttonsXLG /></td>	            
	                <td><asp:Button  id="btnreturn" OnClick="backtoleads" Text="Return" runat="server" class=frmbuttonsXLG /></td>	            
	      </tr>
	     </table>
	    </asp:Panel>
	    <asp:panel ID="pnladdingleads" runat=server visible=false>
	        <table width=90%>
	            <tr>
	                <td colspan=3><b>Selection Criteria</b></td>
	            </tr>
	            <tr>
	                <td align=right>Search=</td>
	                <td><font color=red><asp:Label ID="search" Enabled=true runat=server /></font></td>
	                <td align=right>Lead Type=</td>
	                <td><font color=red><asp:Label ID="ldtype" Enabled=true runat=server /></font></td>
	                <td align=right>Lead Status=</td>
	                <td colspan=2><font color=red><asp:Label ID="ldstat" Enabled=true runat=server /></font></td>
	           
	                <td align=right>Assigned Status=</td>
	                <td><font color=red><asp:Label ID="assignedstat" Enabled=true runat=server /></font></td>
	                <td align=right>Assigned To=</td>
	                <td><font color=red><asp:Label ID="assignedto" Enabled=true runat=server /></font></td>
	                <td align=right>Assigned By=</td>
	                <td><font color=red><asp:Label ID="assigendby" Enabled=true runat=server /></font></td>
	                <td align=right>AD Code=</td>
	                <td><font color=red><asp:Label ID="adcode" Enabled=true runat=server /></font></td>  
	                 <td align=right>Entry Source=</td>
	                <td><font color=red><asp:Label ID="entrysource" Enabled=true runat=server /></font></td>                               
	            </tr>
	            <tr>
	                <td colspan=16>
	                    <table width=50%>
	                        <tr>
	                            <td colspan=5><b>Filter By Add Date</b></td>
	                        </tr>
	                        <tr>
	                            <td width=15></td>
	                            <td>From</td>
	                            <td><asp:TextBox ID="fdate" runat=server /></td>
	                            <td>To</td>
	                            <td><asp:TextBox ID="tdate" runat=server /></td>
	                            <td><asp:Button ID=btnfltr runat=server Text="Filter" OnClick="fltrbydate" class=frmbuttons /></td>
	                             <td><asp:Button ID=btnclrflt runat=server Text="Clear" OnClick="clearfltr" class=frmbuttons /></td>
	                        </tr>
	                   </table>
	                </td>
	            </tr>
	            <tr height=10><td></td></tr>
	            <tr>
	                
	                <td colspan=2 align=left><b>Total Leads Selected=</b></td>
	                <td align=left><b><asp:Label ID="totleadcount" runat=server /></b></td>
	                    <td><asp:Button  id="btnviewleads" OnClick="previewleads" Text="Preview Leads" runat="server" class=frmbuttons /></td>	            
	            </tr>
	        </table>
	        <hr />
	       
	           <asp:Panel ID="pnlleadpreview" runat=server Visible=false >
	        
	         <table width=80%>
	            <tr>
	                <td>
	                <div style="vertical-align top; height: 220px; overflow:auto;">
			  				<asp:DataGrid 
								ID="dgleadpreview" 
						    	AutoGenerateColumns=False
						    	Width="100%"
			                ColumnHeadersVisible = FALSE  
							  	 AllowPaging="false"  Runat=server CssClass="dg" >
							  	 	<HeaderStyle CssClass="dgheaders" />
		        					<ItemStyle CssClass="dgitems" />
		        					<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>
   							   
								 <Columns >
								 	<asp:hyperlinkcolumn runat="server" datatextfield ="tbl_leads_pk" headertext="Lead #" ItemStyle-CssClass="dglinks"
            		                    DataNavigateUrlField ="tbl_leads_pk"
            		                    DataNavigateUrlFormatString="addlead.aspx?action=view&id={0}"  ItemStyle-HorizontalAlign=center ItemStyle-Width="60px" />
                            	
							      	<asp:BoundColumn HeaderText="leadNo"  DataField="tbl_leads_pk" visible=false   />
								    	<asp:BoundColumn HeaderText="Name"  DataField="FullName"   />
								      <asp:BoundColumn HeaderText="Email"  DataField="ld_email"   />
								  		<asp:BoundColumn HeaderText="AD Code"  DataField="ld_adcode"   />							
								
								 <asp:TemplateColumn HeaderText="" ItemStyle-Width="100px" ItemStyle-CssClass="dgitemsNOBD" >
				                        <ItemTemplate >
				                            <table width=100%>
				                               <tr>
				                                    <td><asp:button id="removeleadP" runat=server text="Remove" onclick="removeleadqP" CausesValidation="false" cssclass=frmbuttons /></td>		
    			         
				                              </tr>    
				                            </table>   
					                    </ItemTemplate>                                                     
				                    </asp:TemplateColumn>
				                    </Columns>
						</asp:DataGrid>
				</div></td>
	            </tr>
	        </table>          
	        <hr />
	        </asp:Panel>
	        <table>
	                <tr><td></td></tr>
	        		<tr>
	        			<td colspan=4><b>Available Actions</b></td>
	        		</tr>
	            <tr>
	                <td><asp:Button  id="Button2" OnClick="exportleads" Text="Export To File" runat="server" cssclass=frmbuttonsXLG /></td>	            
	                <td><asp:Button  id="Button3" OnClick="emailleadsA" Text="Email" runat="server" cssclass=frmbuttonsXLG /></td>	            
	            
	                <td><asp:Button  id="btnback" OnClick="backtoleads" Text="Return" runat="server" cssclass=frmbuttonsXLG /></td>	            
	             
	    </tr>
	        </table>
	          </asp:panel>
	        
	          <asp:Panel ID="pnlldexp" runat=server Visible=false >
	          <table>
	        		<tr>
	        			<td colspan=4><b>Available Actions</b></td>
	        		</tr>
	        		<tr>
	        		<td><asp:Button  id="btnexport" OnClick="exportleads" Text="Add to A Queue" runat="server" cssclass=frmbuttons /></td>	            
	               
	        		</tr>
	          </table>
	          
	          </asp:Panel>
	   
	       
	        <asp:Panel ID="pnladdtoq" runat=server Visible=false >	 
	        <table width=70%>
	            <tr>
	                <td>You can add your selected leads to an export queue, which will create a saved list.
	                You will then be able to process these leads as many times as you wish.  Once your leads have
	                been added to a queue you can then Export to a file. </td>
	             </tr>
	             <tr>
	                <td>OR you can select 'Quick Export' and 
	                have you leads sent directly to a Comma Delimeted file.  They will not be saved in a queue.</td>
	                
	            </tr>
	            <tr height=10><td></td></tr>
	        </table>       
	        <table width=70%>
	            <tr>
	                <td colspan=3><b>Select Export Queue</b></td>
	            </tr>
	           <tr><td></td></tr>
	             <tr>
	                <td width=20></td>
	                <td><b>Export Queue Name</b></td> 
	                <td><b>Queue Description</b></td>
	                <td><b>Current Queue Count</b></td>
	             </tr>
	             <tr>
	             <td width=20></td>
	                <td><asp:DropDownList ID="ddexportque"
                  		AutoPostBack="True" OnSelectedIndexChanged="chgqdesc"                  		
                  		DataValueField="eq_tbl_pk" 
                  		Runat="server" />&nbsp&nbsp<asp:linkbutton id="addq" Text= "Add New" runat="server" cssclass=linkbuttons visible =true onClick="addque" /></td>
                  		<td><asp:Label ID="qdescription" Enabled=true runat=server /></td>	
                  		<td><asp:Label ID="qcount" Enabled=true runat=server /></td>	
                  
                  	
	            </tr>
	        </table>
	        <br />
	        <table>
	        	<tr>
	        			<td colspan=3><b>Available Actions</b></td>
	        		</tr>
	            <tr>
	                <td><asp:Button  id="btnaddq" OnClick="addque" Visible=false Text="Add A New Que" runat="server" cssclass=frmbuttonsXLG /></td>
	                <td><asp:Button  id="btnaddtoq" OnClick="addtoque" Text="Add Seleceted Leads" runat="server" cssclass=frmbuttonsXLG /></td>
	             <td><asp:Button  id="btnquickexport" OnClick="quickexport" Text="Quick Export" runat="server" cssclass=frmbuttonsXLG /></td>	            
	             
	                <td><asp:Button  id="btnmanageque" OnClick="manageque" Text="Manage All Queues" runat="server" cssclass=frmbuttonsLG /></td>
	             
	            </tr>
	        </table>
	    </asp:Panel>
	    
	     <asp:panel ID="pnlmgmtq" runat=server visible=false >
	        <table>
	            <tr>
	                <td><font size=3><b>Manage Export Queues</b></font></td>
	            </tr>
	        </table>
	         <table>
	            <tr>
	                <td><asp:button id="btnaddnewq" runat=server text="Add" onclick="addque" CausesValidation="false" cssclass=frmbuttons /></td>		
    			    <td><asp:Button  id="Button1" OnClick="backtoleads" Text="Return" runat="server" cssclass=frmbuttons /></td>	            
	      
	            </tr>
	        </table>
	         <table>
	            <tr>
	                <td>
	                	<asp:DataGrid 
								ID="exportq" 
						    	AutoGenerateColumns=False
						    	Width="100%"
			               ColumnHeadersVisible = FALSE  
						    	AllowPaging="True" 
                         PageSize="10" 
                         PagerStyle-Mode="NumericPages" 
                         OnPageIndexChanged="myDataGridA_PageChanger" 
                           Runat=server CssClass="dg">
                           <HeaderStyle CssClass="dgheaders" />
			        				<ItemStyle CssClass="dgitems" />
			        				<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>
						
						   	 <Columns >
								 
							       <asp:BoundColumn HeaderText="Queue #"  DataField="eq_tbl_pk"   />
								    <asp:BoundColumn HeaderText="Name"  DataField="eq_name"   />
								    <asp:BoundColumn HeaderText="Create Date"  DataField="eq_createdate"   />							
								    <asp:BoundColumn HeaderText="Last Export Date"  DataField="eq_lastexportdate"   />
	 							    <asp:BoundColumn HeaderText="Total Count" ItemStyle-Width=100 DataField="cnt"   />
	 							    
	 							    <asp:TemplateColumn HeaderText="" ItemStyle-Width="100px" ItemStyle-CssClass="dgitemsNOBD" >
				                        <ItemTemplate >
				                            <table width=100%>
				                               <tr>
				                                    <td><asp:button id="manleads" runat=server text="View Leads" onclick="mangaeleads" CausesValidation="false" cssclass=frmbuttonsLG /></td>		
    			         
				                              </tr>    
				                            </table>   
					                    </ItemTemplate>                                                     
				                    </asp:TemplateColumn>
				                    <asp:TemplateColumn HeaderText="" ItemStyle-Width="100px" ItemStyle-CssClass="dgitemsNOBD" >
				                        <ItemTemplate >
				                            <table width=100%>
				                               <tr>
				                                    <td><asp:button id="exportld" runat=server text="Export Leads" onclick="exportleadsQ" CausesValidation="false" cssclass=frmbuttonsLG /></td>		
    			         
				                              </tr>    
				                            </table>   
					                    </ItemTemplate>                                                     
				                    </asp:TemplateColumn>
				                    <asp:TemplateColumn HeaderText="" ItemStyle-Width="100px" ItemStyle-CssClass="dgitemsNOBD"  >
				                        <ItemTemplate >
				                            <table width=100%>
				                               <tr>
				                                    <td><asp:button id="emailq" runat=server text="Email Leads" onclick="EmailQ" CausesValidation="false" cssclass=frmbuttonsLG /></td>		
    			         
				                              </tr>    
				                            </table>   
					                    </ItemTemplate>                                                     
				                    </asp:TemplateColumn>
				                     <asp:TemplateColumn HeaderText="" ItemStyle-Width="100px" ItemStyle-CssClass="dgitemsNOBD"  >
				                        <ItemTemplate >
				                            <table width=100%>
				                               <tr>
				                                    <td><asp:button id="recoveq" runat=server text="Remove Queue" onclick="RemoveQ" CausesValidation="false" cssclass=frmbuttonsLG /></td>		
    			         
				                              </tr>    
				                            </table>   
					                    </ItemTemplate>                                                     
				                    </asp:TemplateColumn>
								</Columns>
						</asp:DataGrid>
				</td>
	            </tr>
	        </table>  
	       
	       
	    </asp:panel>
	    <br />
	    <asp:Panel ID="pnladdqstuff" visible=false runat=server >
	    <table> 
	        <tr>
	            <td>Queue Name</td>
	            <td><asp:TextBox ID="newqname" runat=server size=25 /></td>
	            <td>Description</td>
	            <td><asp:TextBox ID="newqdesc" runat=server size=40 /></td>
	        </tr>
	        <tr>
	            <td><asp:Button  id="bsavenewq" OnClick="savenewq" Text="Save" runat="server" cssclass=frmbuttons /></td>
	           <td><asp:Button  id="btncaneladdq" OnClick="canceladdq" Text="Cancel" runat="server" cssclass=frmbuttons /></td>
	        </tr>  
	   </table>
	    
	    </asp:Panel>
	     <asp:panel ID="pnlqleads" runat=server visible=false>
	    <table width=60%>
	            <tr>
	                <td>
	               	<asp:DataGrid 
								ID="qleads" 
						    	AutoGenerateColumns=False
						    	Width="100%"
			                ColumnHeadersVisible = FALSE  
						    	AllowPaging="True" 
                            PageSize="10" 
                            PagerStyle-Mode="NumericPages" 
                            OnPageIndexChanged="myDataGrid_PageChanger" 
                            Runat=server CssClass="dg" >
										<HeaderStyle CssClass="dgheaders" />
				        				<ItemStyle CssClass="dgitems" />
				        				<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>
   							               
								 <Columns >
								 
							        <asp:BoundColumn HeaderText="leadNo"  DataField="eqd_leadno"   />
								    <asp:BoundColumn HeaderText="Name"  DataField="FullName"   />
								    <asp:BoundColumn HeaderText="AD Code"  DataField="ld_adcode"   />
								    <asp:BoundColumn HeaderText="Email"  DataField="ld_email"   />								
								
								 <asp:TemplateColumn HeaderText="" ItemStyle-Width="100px" ItemStyle-CssClass="dgitemsNOBD"  >
				                        <ItemTemplate >
				                            <table width=100%>
				                               <tr>
				                                    <td><asp:button id="removelead" runat=server text="Remove" onclick="removeleadq" CausesValidation="false" cssclass=frmbuttons /></td>		
    			         
				                              </tr>    
				                            </table>   
					                    </ItemTemplate>                                                     
				                    </asp:TemplateColumn>
				                    <asp:BoundColumn HeaderText="AD Code" Visible=false  DataField="eqd_eq_fk"   />	
				                    </Columns>
						</asp:DataGrid>
				</td>
	            </tr>
	        </table> 
	        <table>
	            <tr>
	                <td><asp:button id="btnhideqd" runat=server text="Close" onclick="hideqd" CausesValidation="false" cssclass=frmbuttons /></td>		
                </tr>
           </table> 
	    </asp:panel>
	    <asp:Panel ID="pnlplaceholder" runat=server Visible=false ><table>
	       <tr>
	            <td><asp:DataGrid 
							ID="x" 
						    AutoGenerateColumns=False
						    Width="100%"
			                ColumnHeadersVisible = FALSE  
						    AllowPaging="false"  Runat=server
						    OnItemDataBound="ItemDataBoundEventHandler" CssClass="dg">
								<HeaderStyle CssClass="dgheaders" />
		        				<ItemStyle CssClass="dgitems" />
		        				<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>
   							               
								 <Columns >
								 
							        <asp:BoundColumn HeaderText="leadNo"  DataField="tbl_leads_pk"   />
					
				                    </Columns>
						</asp:DataGrid></td>
	       </tr>
	     </table>
	     </asp:Panel>
	     
	     <asp:Panel ID="pnlemail" visible=false runat=server >
	      <table>
	            <tr>
	                <td colspan=2><b><u>Bulk Email Send</u></b></td>
	            </tr>
	            <tr>
	                <td>From:</td>
	                <td><asp:TextBox ID="emailfrom" runat=server size=50 />&nbsp&nbsp<asp:linkbutton id="btnuseremail" CausesValidation="false" Text= "Use Your Email" runat="server"  cssclass=linkbuttons onClick="getuseremail" /> </td>            
	            </tr>
	            <tr>
	                <td></td>
	                <td colspan=2><asp:RequiredFieldValidator runat="server" id="Vemailfrom"
          				ControlToValidate="emailfrom" display="dynamic">
          				Required
      				</asp:RequiredFieldValidator></td>
	            </tr>
	            <tr>
	                <td>Template</td>
	                <td><asp:DropDownList ID="ddemailcor" AutoPostBack =true OnSelectedIndexChanged ="prefillemail"
          		                    DataValueField="email_tbl_pk" 
          		                    Runat="server" /></td>
	             </tr>
	            
	            <tr>
	                <td>Subject:</td>
	                <td><asp:TextBox ID="emailsubject" runat=server size=50 /></td>	            
	            </tr>
	            <tr> 
	                <td valign=top>Body:</td>
	                <td><ed:Editor ShowQuickFormat="false" FixedToolbar="false"  AutoFocus="false" height="340" width="900" submit="false" PathPrefix="Editor_data/" FlashBrowse="myFlashBrowse.aspx" MediaBrowse="myMediaBrowse.aspx" ImageBrowse="Editor_data/myImageBrowseClient.aspx" UrlBrowse="myUrlBrowse.aspx" id="emailbody" PreviewMode="true" runat="server"></ed:Editor></td>
			    </tr>						
	        </table>
	        <table id="Table1" runat=server visible=true>
	            <tr>
                    <td align=left ><asp:button id="btnsend" runat=server text="Send" width="100" onclick="click_sendemail" CausesValidation="true" cssclass=frmbuttons /></td>		
			        <td align=left ><asp:button id="btncancel" runat=server text="Cancel" width="100" onclick="click_cancelemail" CausesValidation="False" cssclass=frmbuttons /></td>	
				
	            </tr>
	        </table>
	     
	     </asp:Panel>
	     <asp:Panel ID="pnlemailconfirm" visible=false runat=server >
	        <table>
	            <tr>    
	                <td><asp:label ID="noemails" runat=server />&nbsp Emails have been sent</td>
	            </tr>
	            <tr>
	              <td align=left ><asp:button id="emcont" runat=server text="Continue" width="100" onclick="click_emcontinue" CausesValidation="False" cssclass=frmbuttons /></td>	
				
	            </tr>
	        </table>
	     </asp:Panel>
	</form>
	</body>	
</HTML>
