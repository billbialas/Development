<%@ Page language="vb" Codebehind="brandingADD.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.brandingADD" Debug="false" trace="false" validateRequest="false" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %> 
<%@ Register TagPrefix="ed" Namespace="OboutInc.Editor" Assembly="obout_Editor" %>
<link rel="stylesheet" href="../_include/default.css" type="text/css">
<script language="JavaScript" src="../_include/default.js"></script>
<HTML>
	<HEAD>
		<title>www.WebMagicPortal.com</title>
	</HEAD>
	
<body class="section-1" onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">
	<form id="forms1a" runat="server">
		<table>
			<tr>
				<td class=pgheaders>Branding Setup</td>
			</tr>
		</table>
		
		<table border=0  width=100% cellspacing=0 cellpadding=0 id="SubNav">
			<tr >
				<td width="100%">
					<table runat="Server" id="subnac" cellspacing=0 cellpadding=0 width=100% border=0>
						<tr height=22>
							<td id="subnavGen" align=center width=110><asp:linkbutton id="Lgen" Text= "General Setup"  runat="server" Font-underline="false" Style="cursor:hand" onClick="btn_Gen" /> </td>
							<td id="spacer0" class="tblcelltestc" width=1>&nbsp</td> 			           
							<td id="subnavPage1" align=center width=110><asp:linkbutton id="lpage1" Text= "Intake Page"  runat="server" Font-underline="false" Style="cursor:hand" onClick="btn_pg1" /> </td>
							<td id="spacer1" class="tblcelltestc" width=1>&nbsp</td> 			           
							<td id="subnavPage2" align=center width=110><asp:linkbutton id="lpage2" Text= "Confirmation Page"  runat="server" Font-underline="false" Style="cursor:hand" onClick="btn_pg2"  /> </td>
							<td id="spacer2" class="tblcelltestc" width=1>&nbsp</td> 			           
		               <td id="subnavresp" align=center width=110><asp:linkbutton id="lautop" Text= "Responses" runat="server" Font-underline="false" Style="cursor:hand" onClick="btn_rsp"  /> </td>
		               <td id="spacer3" class="tblcelltestc" width=1>&nbsp</td> 			           
		               <td id="subnavimgs" align=center width=110><asp:linkbutton id="limgs" Text= "Images" runat="server" Font-underline="false" Style="cursor:hand" onClick="btn_imgs"  /> </td>
		                <td id="spacer4" class="tblcelltestc" width=1>&nbsp</td> 			           
		               <td id="subnavADS" align=center width=110><asp:linkbutton id="lads" Text= "Linked ADs" runat="server" Font-underline="false" Style="cursor:hand" onClick="btn_ADS"  /> </td>
		               <td  runat="Server" class="tblcelltestc" width=550>
		               	<table width=100% border=0  cellspacing=0 cellpadding=0>
		               		<tr height=22>
		               				<td width=80%></td>
							 			  <td  align=right> 	<asp:button id="bsaveco" runat=server text="Save" onclick="updatebranding" CausesValidation="false" cssclass=frmbuttons /></td>
								        <td  align=right> 	<asp:button id="bsavecoE" runat=server text="Save & Exit" onclick="updatebrandingE" CausesValidation="false" cssclass=frmbuttons  /></td>
								   		<td align=left> 	<asp:button id="Cancel" runat=server text="Exit" onclick="Cancelbranding" CausesValidation="false" cssclass=frmbuttons  /></td>
			
								   </tr>           	
		               	</table>
		               </td>
 			        	</tr>
 			      </table>
							
 			        <table class=tblcelltestb width=100% >
 			      	<tr>
 			      		<td>
 			       	<asp:panel id="pnlgeneral" runat=server visible=false >
 			       	<div id="fieldtitles" style="vertical-align: top; height: 480px; ">
 			      		     	<table cellspacing=3 cellpadding=3>
							     		<tr>
							     			<td width=140 align=right>Status</td>
							     			<td><asp:DropDownList id="dd_Bstatus" runat="server" >
    							                <asp:ListItem Value="Active" Text="Active"/>
    							                <asp:ListItem Value="Inactive" Text="Inactive"/>    							                	    						               
 								                </asp:DropDownList></td>
							     		</tr>
							         <tr>
							             <td width=140 align=right>Branding Name</td>
							             <td><asp:TextBox ID="txtname" runat=server size =40  /></td>
							         </tr>
							     		<tr>
							             <td width=140 align=right>Description</td>
							             <td><asp:TextBox ID="txtdescription" runat=server size =100 /></td>
							         </tr>
							    		<tr>
						     			   <td width=140 align=right>Company Name</td>
					                   <td><asp:TextBox ID="txtconame" runat=server size =100/></td>
										</tr>
										<tr>
											<td width=140 align=right>Header Text Line 1</td>
							            <td><asp:TextBox ID="txthead1" runat=server size =100/></td>
										</tr>
										<tr>
										   <td width=140 align=right>Header Text Line 2</td>
							            <td><asp:TextBox ID="txthead2" runat=server size =100/></td>
										</tr>
									</table>
	             

							
					   </div>								      	
						      </asp:panel>		
		       <asp:panel id="pnlpage1" runat=server visible=false ><div id="fieldtitles" style="vertical-align: top; height: 480px; " >
		     <table cellpadding=0 cellspacing=0 width=20%>
	      		<tr>
	      			<td><b><u>Intake Configuration</u></b></td>
	   				<td><asp:linkbutton id="btn1prev" CausesValidation="false" Text= "Preview/Save" runat="server" cssclass=linkbuttonsRed onClick="previewpg1" /></td>
      
	      		</tr>
	      	</table><br>
				<table width=100% cellpadding=0 cellspacing=0 >
					<tr>
						<td colspan=4><i>Header Controls</i></td>
					</tr>
					<tr>
						<td valign=top>
							<table>
								<tr>
					             <td><asp:CheckBox ID="chkshowco" text ="Display Company Name" runat=server /></td>
			                  <td><asp:CheckBox ID="chkshowhr" text="Show Horizontal Line" runat=server  /></td>
				            </tr><tr>  
			                   <td><asp:CheckBox ID="chkhead1" text="Display Header Line1 " runat=server  /></td>
				   			    <td><asp:CheckBox ID="chkhead2" text="Display Header Line 2" runat=server /></td>
								</tr><tr>
				            </tr>
				         </table>
				     </td>
				     <td valign=top>
						     <table>
								<tr>
									<td valign=top>
											<table cellpadding=0 cellspacing=0>
												<tr>
										        <td><asp:CheckBox ID="chkshowlogo" text="Display Image" runat=server autopostback=true oncheckedchanged=" chkshowlogoA" /></td>
			           						</tr>
											
								    	   	<tr>
								         		<td><asp:DropDownList id="dd_sellogo" AutoPostBack="true" OnSelectedIndexChanged="refreshlogo"
									                     DataValueField="ui_tbl_pk" Runat="server" /></td>
								            </tr>
								         </table>     		     
			      				</td>
									<td> 
									      
							         	</td>
										</tr>
								</table>
						</td>
						<td valgn=top>
						<table cellpadding=0 cellspacing=0>
					                 		
					                       <tr>
							                         <td><img ID="imglogo" runat=server src="" Height=80 Width=120 /></td>
							                     </tr>
							                </table>
						</td>
				   </tr>
	         </table>
	         
	   
	         <table cellpadding=0 cellspacing=0>
	         	<tr>
	           		<td width=120 align=left valign=top><i>Display Text</i></td>
	           	</tr>
	           	<tr>
		        		<td><ed:Editor ShowQuickFormat="false" FixedToolbar="false"  AutoFocus="false" height="400" width="1100" submit="false" PathPrefix="Editor_data/" FlashBrowse="myFlashBrowse.aspx" MediaBrowse="myMediaBrowse.aspx" ImageBrowse="Editor_data/myImageBrowseClient.aspx" UrlBrowse="myUrlBrowse.aspx" id="pg1text" PreviewMode="true" runat="server">
					                              		
															</ed:Editor></td>
					</tr>
				</table>	</div>

      </asp:panel>
      <asp:panel id="pnlpage2" runat=server visible=false ><div id="fieldtitles" style="vertical-align: top; height: 480px; " >
    			<table width=25%>
	      		<tr>
	      			<td><b><u>Confirmation Configuration</u></b></td>
	    				<td><asp:linkbutton id="btn2prev" CausesValidation="false" Text= "Preview/Save" runat="server" cssclass=linkbuttonsRed onClick="previewpg2" /></td>
      
	      		</tr>
	      	</table>
	      	<table width=100% border=0 >
	      		<tr>
	      			<td width=100%>
				      	<table width=100% cellpadding=3 cellspacing=0 border=0>
								<tr>
									<td colspan=4><i>Header Controls</i></td>
								</tr>
								<tr>
									<td valign=top>
										<table cellpadding=0 cellspacing=0 width=80%>
											<tr>
								             	<td><asp:CheckBox ID="chkshowco2" text ="Display Company Name" runat=server  /></td>
						                  	<td><asp:CheckBox ID="chkshowhr2" text="Show Horizontal Line" runat=server  /></td>
						                     <td><asp:CheckBox ID="chkshowlogo2" text="Display Image" runat=server autopostback=true oncheckedchanged="chkshowlogoA2" /></td>
						           				<td><asp:DropDownList id="dd_sellogo2" AutoPostBack="true" OnSelectedIndexChanged="refreshlogo2"
												                     DataValueField="ui_tbl_pk" Runat="server" /></td>		
							            </tr>
							            <tr>  
						                   <td><asp:CheckBox ID="chkhead12" text="Display Header Line1 " runat=server  /></td>
							   			    <td><asp:CheckBox ID="chkhead22" text="Display Header Line 2" runat=server /></td>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td>
										<table width=100% border=0>
											<tr>
												<td colspan=4><i>Action Controls</i></td>
											</tr>
											<tr>
						             		<td><asp:CheckBox text="Show Continue Button" ID="chkshowcntbtn" runat=server autopostback=true oncheckedchanged="continbutton" /></td>
						          			 <td width=100 align=right>Redirect URL </td>
							                <td><asp:TextBox ID="txtredirecturl" runat=server size =50/></td>
							            </tr>
						        		</table>
						        	</td>
					          </tr>
							 </table>
						</td>
						<td valign=top width=40% align=left>
									<img ID="imglogo2" runat=server src="" Height=110 Width=150 />
						</td>
					</tr>
			    </table>

	        
		      <table>
	         	<tr>
	           		<td width=120 align=left valign=top><i>Display Text</i></td>
	           	</tr>
	           	<tr>
		        		<td><ed:Editor ShowQuickFormat="false" FixedToolbar="false"  AutoFocus="false" height="400" width="1100" submit="false" PathPrefix="Editor_data/" FlashBrowse="myFlashBrowse.aspx" MediaBrowse="myMediaBrowse.aspx" ImageBrowse="Editor_data/myImageBrowseClient.aspx" UrlBrowse="myUrlBrowse.aspx" id="pg2text" PreviewMode="true" runat="server">
					                              		
															</ed:Editor></td>
					</tr>
				</table>	</div>
				
	
      
		</asp:panel>  							
      <asp:panel id="pnlnotifications" runat=server visible=false ><div id="fieldtitles" style="vertical-align: top; height: 480px; " >
    		<table>
      		<tr>
      			<td><b><u>Auto Responses</u></b></td>
      		</tr>
      	</table>
      	<table width=70%>
					<tr>
						<td colspan=4></td>
					</tr>
				</table>
			<table width=80% border=0>
				<tr>
					<td><i>Notifications</i></td>
					<td >Email Address</td>
					<td>Lead Level Information</td>
						<td>Mail/Export Queue</td>
					<td>Include Login Link</td>
				</tr>
		   	<tr>		    		
		      	<td ><asp:CheckBox ID="chkemailself" text="Primary" runat=server autopostback=true oncheckedchanged="sndpriemail"  /></td>
             	<td><asp:TextBox ID="selfemailaddress" runat=server size=40 Enabled=true />&nbsp<asp:linkbutton id="btnuseremail" CausesValidation="false" Text= "Use Default" runat="server"  cssclass=linkbuttons onClick="getuseremail" /></td>
		    		
		    		<td><asp:DropDownList id="dd_status" runat="server" AutoPostBack="true" OnSelectedIndexChanged="statcheck"  >
    							                <asp:ListItem Value="Select.." Text="Select.."/>
    							                <asp:ListItem Value="Basic" Text="Basic"/>
    							                <asp:ListItem Value="Full Information" Text="Full Information"/>
  	    						                <asp:ListItem Value="Anonymous" Text="Anonymous"/>  	    						               
 								                </asp:DropDownList></td>
 					<td><asp:DropDownList ID="ddexportque" AutoPostBack=true OnSelectedIndexChanged ="enableexpq"                 		
                  		DataValueField="eq_tbl_pk" 
                  		Runat="server" /></td>
 					<td><asp:CheckBox ID="chkemailloglink" runat=server	/></td>	                
		    	</tr>
		    	<tr>		    		
		      	<td ><asp:CheckBox ID="chkemailself2" text="Additional" runat=server autopostback=true oncheckedchanged="sndsecemail" /></td>
             	<td><asp:TextBox ID="selfemailaddress2" runat=server size=40 Enabled=true />&nbsp<asp:linkbutton id="btnuseremail2" CausesValidation="false" Text= "Use Default" runat="server" cssclass=linkbuttons onClick="getuseremail2" /></td>
		    		
		    		<td><asp:DropDownList id="dd_status2" runat="server" AutoPostBack="true" OnSelectedIndexChanged="statcheck2" >
    							               	<asp:ListItem Value="Select.." Text="Select.."/>
    							                <asp:ListItem Value="Basic" Text="Basic"/>
    							                <asp:ListItem Value="Full Information" Text="Full Information"/>
  	    						                <asp:ListItem Value="Anonymous" Text="Anonymous"/>
 								                </asp:DropDownList></td>
 				<td></td>
 					<td><asp:CheckBox ID="chkemailloglink2"  runat=server	/></td>	
		    	</tr>
			</table>
			
			<table width=70%>
					<tr>
						<td colspan=4><i>Client Response</i></td>
						
					</tr>
				</table>
				<table width=100% border=0><tr><td width=60%>
				
    		<table width=100% border=0 cellpadding=1 cellspacing=1>
		   	<tr>
		        	<td width=90% align=left><asp:CheckBox ID="chksendemail" text="Send auto-email response"  runat=server AutoPostBack =true OnCheckedChanged ="enablebutts"/></td>
            <td valign=top align=right width=115><b>Templates</b></td>
					                <td valign=top><asp:DropDownList ID="ddemailcor" visible=true AutoPostBack =true OnSelectedIndexChanged ="prefillemail"
				          		                    DataValueField="email_tbl_pk" 
				          		                    Runat="server" /></td>
            </tr>
			</table>
			<div style="vertical-align: top; height: 325px; overflow:auto;">
			<table width=100% cellpadding=0 cellspacing=0>
		   	<tr>
		   		<td width=3%></td>
		      	<td align=left valign=top>From Email Address&nbsp&nbsp<asp:linkbutton id="btnuseremailA" CausesValidation="false" Text= "Use Your Email" runat="server"  Font-Bold="True" Font-underline="True" Style="color:#ff0000; font-family:arial; font-size:8pt; cursor:hand" onClick="getuseremailA" /></td>
		     	</tr>
		     	<tr>
		     	<td width=3%></td>
		        	<td><asp:TextBox ID="TextBox1" runat=server size=50 Enabled=false />&nbsp&nbsp</td>
               
		    	</tr>
		    	
		      <tr>
		      <td width=3%></td>
		        <td><asp:TextBox ID="TextBox2" runat=server size=50 Enabled=false visible=false /></td>
               
		    	</tr>
		    	 <tr>
	               <td colspan=2>
	              
	               </td>
	         </tr>
		      <tr>
		      <td width=3%></td>
		        <td  align=left valign=top >Subject</td>
		      </tr>
		      <tr>
		      <td width=3%></td>
		        <td><asp:TextBox ID="TextBox3" runat=server size=70 Enabled=false /></td>
               
		    	</tr>
		      <tr>
		      <td width=3%></td>
		        <td  align=left valign=top>Body</td>
		      </tr>
		      <tr>
		      <td width=3%></td>
		        <td><ed:Editor ShowQuickFormat="false" FixedToolbar="false"  AutoFocus="false" height="600" width="600" submit="false" PathPrefix="Editor_data/" FlashBrowse="myFlashBrowse.aspx" MediaBrowse="myMediaBrowse.aspx" ImageBrowse="Editor_data/myImageBrowseClient.aspx" UrlBrowse="myUrlBrowse.aspx" id="emailbody" PreviewMode="true" runat="server">
					                              		
															</ed:Editor></td>
		    	</tr>
			</table>
			</td>
			<td width=40% valign=top>
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
												                        <td><asp:button id="AppendAll" runat=server text="Replace"  visible=true onclick="appendAll" CausesValidation="false" cssclass=frmbuttons onmouseover="document.getElementById('myToolTip').className='activeToolTip'" onmouseout="document.getElementById('myToolTip').className='idleToolTip'"  /> </td>		
																        </tr><tr>     <td><asp:button id="AppendBody" runat=server text="Append Body "  visible=true onclick="appendBody" CausesValidation="false" cssclass=frmbuttons  onmouseover="document.getElementById('myToolTipA').className='activeToolTip'" onmouseout="document.getElementById('myToolTipA').className='idleToolTip'"  /></td>		
																        </tr><tr>    <td><asp:button id="AppendSubject" runat=server text="Append Subject "  visible=false onclick="appendSubject" CausesValidation="false" cssclass=frmbuttons /></td>		
										  							     </tr><tr>    <td><asp:button id="Cancel" runat=server text="Cancel "  visible=true onclick="Canceltemplate" CausesValidation="false" cssclass=frmbuttons /></td>		
										    								</tr>
												                </table>   
												            </ItemTemplate>                                                     
											            </asp:TemplateColumn>
											              <asp:BoundColumn  HeaderText="Body"  DataField="email_text" visible="false" ItemStyle-Width="450px"    />
							     		                          
											        </Columns>
										        </asp:DataGrid>
					          			</td>
					          		</tr>
					          	</table><div id="myToolTipA" class="idleToolTip">Will append template text to the end of the Email Body</div>
      										<div id="myToolTip" class="idleToolTip">Will replace/overwrite the Subject Line and Email Body with template</div>
					        
					          	</asp:panel>
					         </td>
					         </tr>
					          </table>   
			
			
			</td>
			</tr>
			</table></div></div>
		
    	</asp:panel> 
    	 <asp:panel id="pnlimages" runat=server visible=false >
    	 <div id="Div1" style="vertical-align: top; height: 480px; " >

    		<table width=30%>
      		<tr>
      			<td class=wfstepheaderA>Images</td>
      			<td><asp:Button visible=false id="bntuploadimg" OnClick="uploadimage" Text="Upload Image" runat="server" cssclass="frmbuttons" /></td>
      		</tr>
      	</table>
      	 <table width=90% border=0>
            <tr>
                <td >
  			        <asp:DataGrid 
				        ID="dgimages" 
			        		AutoGenerateColumns=False
			        		Width="100%"
                    	ColumnHeadersVisible = FALSE  
			            AllowPaging="True" 
                    	PageSize="5" 
                    	PagerStyle-Mode="NumericPages" 
                    	OnPageIndexChanged="images_PageChanger"
                    	OnItemDataBound="ItemDataBoundEventHandler"
                     Runat=server CssClass="dg">
			        			<HeaderStyle CssClass="dgheaders" />
			        			<ItemStyle CssClass="dgitems" />
			        			<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>	
		                <Columns >
    		               <asp:TemplateColumn ItemStyle-Width=80 >
									<ItemTemplate>
										<asp:hyperlink ID="Hyperlink1" runat="server" NavigateUrl=<%# DataBinder.Eval(Container.DataItem, "ui_location").tostring + DataBinder.Eval(Container.DataItem, "ui_filename").tostring %> >
										<asp:Image ID="Image1" Width="80" Height="70" 									
												src='<%# DataBinder.Eval(Container.DataItem, "ui_location").tostring +  DataBinder.Eval(Container.DataItem, "ui_filename").tostring %>'
										  Runat=server />
										  </asp:hyperlink>
									</ItemTemplate>                               
								</asp:TemplateColumn>
						      
    		               <asp:BoundColumn HeaderText="Number"  DataField="ui_tbl_pk" visible="false" readonly=true ItemStyle-Width="10px"    />
    		               <asp:BoundColumn HeaderText="Type"  readonly=true DataField="ui_type" visible="false" ItemStyle-Width="80px"    />
    		               <asp:BoundColumn HeaderText="Name"  DataField="ui_name" visible="true" ItemStyle-Width="100px"    />
    		               <asp:BoundColumn HeaderText="Description"  DataField="ui_descrip" visible="false" ItemStyle-Width="250px"    />
    		               <asp:BoundColumn HeaderText="Image URL"  DataField="imgurl" visible="true" ItemStyle-Width="250px"    />
    		               <asp:BoundColumn HeaderText="Image HTML"  DataField="imghtml" visible="false" ItemStyle-Width="250px"    />
    		              
    		               <asp:BoundColumn HeaderText="File Name" ReadOnly=true  DataField="ui_filename" visible="true" ItemStyle-Width="80px"    />
    		              
    		               
				        </Columns>
			        </asp:DataGrid>
	            </td>
            </tr>
        </table> 
        </div>    
    	</asp:panel> 
    	<asp:panel id="pnlADS" runat=server visible=false >
    	 	<div  style="vertical-align: top; height: 480px; " >
				<table width=30%>
      			<tr>
      				<td class=wfstepheaderA>Linked ADs</td>
      			</tr>
      		</table>			
      		 <table width=100%>
                <tr>
                    <td><asp:DataGrid Runat=server
						    		ID="dglinkedADS" 
		                    	AutoGenerateColumns=False
				            	Width="100%"          
				             	AllowPaging="True" 
                           PageSize="18" 
                           PagerStyle-Mode="NumericPages" 
                           OnPageIndexChanged="MyDataGridR_PageLADS">
                           <HeaderStyle CssClass="dgheaders" />
			        				<ItemStyle CssClass="dgitems" />
			        				<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>
    	            
				       <Columns >
	           		     		<asp:BoundColumn HeaderText="AD NO"  visible=true DataField="tbl_leadad_pk" ItemStyle-Width="300px"   />
	           		     		<asp:BoundColumn HeaderText="Title"  DataField="ad_title" ItemStyle-Width="300px"   />
	           		     		<asp:BoundColumn HeaderText="Status"  visible=true DataField="ad_stage" ItemStyle-Width="100px"   />
	           		     		
	        			 </Columns>
		            </asp:DataGrid>
		            </td>
                </tr>
            </table>
      		
      		
      		
      		
      		
      		
      		
      		
      		
      		
      		
      		
      		
      		
      		
      		
      			
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
