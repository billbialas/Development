<%@ Page language="vb" Codebehind="emaillead.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.emaillead" Debug="false" trace="false" validateRequest=false %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %> 
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<link rel="stylesheet" href="../_include/default.css" type="text/css">
<script language="JavaScript" src="../_include/default.js"></script>
<script type="text/javascript">
<!--
    function copy(text) {
       
        //copy to clipboard
        window.clipboardData.setData('Text', text);
      }
      
       function getposition()
    {
        var txt1=document.getElementById("emailsubject");
    
        var currentRange=document.selection.createRange();   
        var workRange=currentRange.duplicate();
        txt1.select();
        var allRange=document.selection.createRange();
        var len=0;   
    
        while(workRange.compareEndPoints("StartToStart",allRange)>0)   
        {   
          workRange.moveStart("character",-1);   
          len++;   
        }   
    
        currentRange.select();   
        
        window.alert(len); 
}

  
       
       
       -->
    </script>
<link rel="stylesheet" href="../_include/default.css" type="text/css">
<HTML>
	<HEAD>
		<title>www.webmagicportal.com</title>
	</HEAD>
	
<body >
	<form id="forms1a" runat="server" enctype="multipart/form-data" method="post">
	    <table width=90% cellpadding=0 cellspacing=0 border=0>
	        <tr>
	            <td ><img src="images/email-icon.jpg" height=50 width=50 /></td>
	            <td><img src="images/test.png" height=33/></td>
	            <td width=99% >
	                <table cellpadding=0 cellspacing=0 width=25%  style="border-top: 3px solid #00A2FA;border-bottom: 3px solid #00A2FA;border-right: 3px solid #00A2FA;">
	                    <tr>
	                        <td width=30></td>
	                        <td><asp:ImageButton id="ImageButton1" runat="server"  AlternateText="Send Email" ImageAlign="left" ImageUrl="../images/emailsend.jpg" height=25  />
	                        </td>	                       
	                         <td><asp:ImageButton id="ImageButton2" runat="server"  AlternateText="Use Templates" ImageAlign="left" ImageUrl="../images/templateicon.jpg" height=25 onclick="showtemplates" CausesValidation=false />
	                        </td>
	                          <td><asp:ImageButton id="ImageButton3" runat="server"  AlternateText="Attach Lead Data" ImageAlign="left" ImageUrl="../images/icons-people-p.png" height=25  />
	                          </td>
	                          <td><asp:ImageButton id="ImageButton4" runat="server"  AlternateText="Cancel" ImageAlign="left" ImageUrl="../images/cancelicon.png" height=25  />
	                          </td>
	                         
	                         <td width=30></td>
	                    </tr>
	                </table>
	            </td>
	        </tr>
	    </table>
	    <asp:Label ID="WonderfulLabel" runat="server" Visible=true AccessKey="W"  AssociatedControlID="ImageButton2" />
	    
	      <asp:Panel ID="pnltemplates" runat=server Visible=false>
	        <table width=100%>
	            <tr>
                    <td ><b>Templates</b></td>
                    <td><a class="thumbnail" href="#thumb">Preview<span><asp:Label ID="lbltemplatepreview" runat="server" Text="test">
                            </asp:Label>
                            </span></a></td>
                    <td>Insert</td>
                </tr>
            </table>
            <table>
                <tr>                        
                    <td valign="top">
                        
                            <asp:DropDownList ID="ddemailcor" runat="server" AutoPostBack="true" DataValueField="email_tbl_pk"
                                OnSelectedIndexChanged="prefillemail" Visible="true">
                            </asp:DropDownList>
                    </td>
                </tr>
              </table>
	      
	      </asp:Panel>
	    
	    
	  			    <asp:Panel ID="emailmain" runat=server>
	  			    	 <asp:Panel ID="emailhead" runat=server>
					    <div id="fieldtitles" >
					    <br />
					        <table id="mainfrm" width=60% border=0 >
					            <tr >
					                <td width=72%><b>From:</b></td>
					                <td>[<asp:linkbutton id="btnuseremail" CausesValidation="false" Text= "Insert Your Default Email" runat="server"  cssclass=linkbuttonsRed onClick="getuseremail" />]</td>
					                          
					            </tr>
					            <tr>
					                <td colspan=2><asp:TextBox ID="emailfrom" runat=server rows=1 TextMode="multiline" Columns=70 /></td>  
					            </tr>
					            <tr>
					            	
					                <td colspan=2><asp:RequiredFieldValidator runat="server" id="Vemailfrom"
				          				ControlToValidate="emailfrom" display="dynamic">
				          				Required
				      				</asp:RequiredFieldValidator></td>
					            </tr>
					            </table>
					            <table width=60%>
					            
					            <tr>
					                <td width=81%><b>To:</b></td>
					                <td>[<asp:linkbutton id="bttnleademail" CausesValidation="false" Text= "Use Lead Email" runat="server"  cssclass=linkbuttonsred onClick="getleademail" />]</td>
					                         
					            </tr>
					            <tr> <td colspan=2><asp:TextBox ID="emailto" runat=server rows=1 TextMode="multiline" Columns=70 /> </td></tr>
					            <tr>
					            	<td></td>
					                <td colspan=2><asp:RequiredFieldValidator runat="server" id="vemailto"
				          				ControlToValidate="emailto" display="dynamic">
				          				Required
				      				</asp:RequiredFieldValidator></td>
					            </tr>
					            </table>
					            <table>
					            <tr>
					                <td ><b>CC:</b></td>	
					            </tr>
					            <tr>				             
					                <td><asp:TextBox ID="emailcc" runat=server rows=1 TextMode="multiline" Columns=70 /></td>	            
					            </tr>
					          </table> </div>
					          
					          <table width=100%>
					            <tr>
					            	
					            	<td>
					          	<asp:panel id="pnltempatespre" runat=server visible=false  >
					          	<table>
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
												                        <td><asp:button id="AppendAll" runat=server text="Replace"  visible=true onclick="appendAll" CausesValidation="false" cssclass=frmbuttons onmouseover="document.getElementById('myToolTip').className='activeToolTip'" onmouseout="document.getElementById('myToolTip').className='idleToolTip'"  /></td>		
																        </tr><tr>     <td><asp:button id="AppendBody" runat=server text="Append"  visible=true onclick="appendBody" CausesValidation="false" cssclass=frmbuttons onmouseover="document.getElementById('myToolTipA').className='activeToolTip'" onmouseout="document.getElementById('myToolTipA').className='idleToolTip'" /></td>		
																        </tr><tr>    <td><asp:button id="AppendSubject" runat=server text="Append Subject "  visible=false onclick="appendSubject" CausesValidation="false" cssclass=frmbuttons /></td>		
										  							     </tr><tr>    <td><asp:button id="Cancel" runat=server text="Cancel"  visible=true onclick="Canceltemplate" CausesValidation="false" cssclass=frmbuttons /></td>		
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
					         </td>
					         </tr>
					          </table>   
					          
					          </asp:panel>
					          	<asp:panel id="pnltempates" runat=server visible=false cssclass=pnllftborder >
						   	<table >
						   		<tr>
						   			<td>Templates</td>
						   		</tr>
						   	</table>
						   	 <table WIDTH=38%>
							    	<tr>
							    		<td class=pgsubheaders align=right>Search</td>
							    		<td><asp:textbox id="l_search" runat=server size=25 ontextchanged="btnsearch" autopostback="true"  />&nbsp&nbsp
							    			<asp:linkbutton id="clear" Text= "Clear"  runat="server"  cssclass="linkbuttons"  onClick="clearall" /></td>
							    	</tr>
							    </table>
							    <table width=100%>
	        <tr>
	            <td> <asp:DataGrid	            
				        	ID="emails" 
				        	AutoGenerateColumns=False
				        	Width="100%"
			        		AllowPaging="True" 
                    	PageSize="3" 
                    	PagerStyle-Mode="NumericPages" 
                    	OnPageIndexChanged="emails_PageChanger"
			          	Runat=server CssClass="dg"
			          	OnItemDataBound="ItemDataBoundEventHandler">
			        			<HeaderStyle CssClass="dgheaders" />
			        			<ItemStyle CssClass="dgitems" />
			        			<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>


		                <Columns >
    		             	<asp:BoundColumn HeaderText="Template #"  DataField="email_tbl_pk" visible="true" ItemStyle-Width="10px"    />
     		               <asp:BoundColumn HeaderText="Template Name"  DataField="email_name" visible="true" ItemStyle-Width="100px"    />
     		               <asp:BoundColumn  HeaderText="Subject Line"  DataField="email_subject" visible="true" ItemStyle-Width="150px"    />
     		               <asp:BoundColumn  HeaderText="Body"  DataField="email_text" visible="true" ItemStyle-Width="450px"    />
     		               <asp:TemplateColumn HeaderText="<font color=#FFF8C6><b></b></font>" visible=true ItemStyle-Width="100px"  >
					            <ItemTemplate >
					                <table width=100%>
					                    <tr>
					                        <td><input id="btnpaste" runat=server type="button"  value="Copy" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:80px; cursor:hand"   /></td>		
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
					          
					          
					          <table width=100%>  
					            <tr>
					                <td width=65 align=right><b>Subject:</b></td>
					                <td><asp:TextBox ID="emailsubject" runat=server size=60 /></td>	            
					            </tr>
					            <tr> 
					                <td valign=top  width=65 align=right><b>Body:</b></td>
					                <td ><FTB:FreeTextBox id="emailbody" runat="server" width="800" height=230 /></td>
							    </tr>						
					        </table>
					         
					        <table id="Table1" runat=server visible=true width=80%>
					            <tr>
					            	<td width=60></td>
					               <td ><asp:CheckBox ID="chkattachlead" runat=server autopostback=true text="Attach lead information" oncheckedchanged="showlfs" /> </td>
							         <td>
							         	<asp:panel id=pnlldfields runat=server visible=false >
							          		<table>
							          			<tr>
							          				<td>
							          					<table>
							          						<tr>
							          							<td>Predefined Fields</td><td>Discrete Fields</td>
							          						</tr>
							          						<tr>
							          							<td><asp:DropDownList visible=false id="dd_status" runat="server" AutoPostBack="true" OnSelectedIndexChanged="statcheck" >
					    							               	<asp:ListItem Value="Select.." Text="Select."/>
					    							                <asp:ListItem Value="Basic" Text="Basic"/>
					    							                <asp:ListItem Value="Full Information" Text="Full Information"/>
					  	    						                <asp:ListItem Value="Anonymous" Text="Anonymous"/>
					 								                </asp:DropDownList>
				 								                </td>
				 								                <td>							            		
							           								<table width=100%>
							           									<tr>
													            			<td><asp:CheckBox ID="chkldno" text="Lead No" runat=server /></td>			            			
													            			<td><asp:CheckBox ID="chkldname" text="Name" runat=server /></td>
													            			<td><asp:CheckBox ID="chkldtype" text="Lead Type" runat=server /></td>
													            			<td><asp:CheckBox ID="chklddate" text="Date" runat=server /></td>
													            			<td><asp:CheckBox ID="chkldph" text="Phone" runat=server /></td>
													            			<td><asp:CheckBox ID="chkldem" text="Email" runat=server /></td>
													            			<td><asp:CheckBox ID="chkldnotem" text="Notes" runat=server /></td>
	            														</tr>
	            													</table> 
							            						</td>
							            					</tr>
							           					</table>
							           				</td>
							           			</tr>
							           		</table>
							            	</asp:panel>
							            </td>
					            	</tr>	
					          </table>
					          <table>            
					            <tr><td width=60></td>
				                    <td align=left ><asp:button id="btnsend" runat=server text="Send"  onclick="click_sendemail" CausesValidation="true" cssclass=frmbuttons /></td>		
							        <td align=left ><asp:button id="btncancel" runat=server text="Cancel"  onclick="click_cancelemail" CausesValidation="False"  cssclass=frmbuttons /></td>	
							        <td align=left ><asp:button id="btnshowtemps" visible=false runat=server text="Templates" onclick="click_showtemplates" CausesValidation="False"  cssclass=frmbuttons /></td>	
								
					            </tr>
					           
					        </table>
					   
					     </asp:panel>
			
			
			
			
					   		
					  </td>
					</tr>
			</table>
	</form>
	</body>	
</HTML>
