<%@ Page language="vb" Codebehind="admanager.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.createad" Debug="false" trace="false" validateRequest="false" %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %> 
<%@ Register assembly="ASPNetSpell.NET1" namespace="ASPNetSpell" tagprefix="ASPNetSpell" %>
<%@ Register TagPrefix="ed" Namespace="OboutInc.Editor" Assembly="obout_Editor" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script language="JavaScript" src="../_include/default.js"></script>
<script type="text/javascript">

<!--
    function copy(text) {
       // var obj1 = document.getElementById("adtext");
       // var obj1value = obj1
        
        //var obj = document.getElementById('tsta');
        //obj.value = obj1value

       
        //var email = text
        //var email = document.getElementById('adtitle');
         //var emailValue = email.value;
        //copy to clipboard
        window.clipboardData.setData('Text', text);
        //window.clipboardData.setData('Text', text);
         // get the clipboard data
        // var emailText = window.clipboardData.getData('Text');
        //clear clipboard data
         //window.clipboardData.clearData();

     }
     function copy2(text) {
         var email = text;
         //ar email = document.getElementById('adtitle');
         var emailValue = email.value;
         //copy to clipboard 
         // window.clipboardData.setData('Text' , emailValue );
         window.clipboardData.setData("text", emailValue);
         // get the clipboard data
         //var emailText = window.clipboardData.getData('Text');
         //clear clipboard data
         //window.clipboardData.clearData();

     }
     
     
function insertDate()
{
	 oboutGetEditor('adtext').InsertHTML(document.getElementById("refererA").value);
}
function insertkey()
{
  oboutGetEditor('adtext').InsertHTML(document.getElementById("referer").value);
}

// toolbar status variable
var toolbarIsShown = true;

function toggleToolbar()
{
  // get client-side object
  var editor = oboutGetEditor("adtext");

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
     -->
    </script>
    
    
<HTML>
	<HEAD>
		<title>www.WebMagicPortal.com</title>
	</HEAD>
	
	<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">
		<form  name="createad"  runat="server" >
		<asp:Panel ID="pnldrspwarn" runat=server Visible=true>
		    <table>
		        <tr>
		            <td><font color="red" size=4>You have not setup your DEFAULT response page.  Please click continue to do so.</font></td>
	            </tr>
	            <tr>
	                 <td><asp:button id="btnsetupdrsp" runat=server text="Continue" onclick="setupdrsp" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:80px; cursor:hand" /></td>		
			     
	            </tr>
	         </table>
		</asp:panel>
		<table width=25%>
         <tr>
    			 	<td width=27% valign=top class=pgheaders><asp:label id="adtitleM" runat="server" /></td>
      			<td width=15% valign=middle><b><asp:label id="adno" visible=false runat="server" /></b></td>
      			<td width=23% valign=middle><font size=1 color=red><b><asp:label id="adstage" runat="server" /></b></font></td>
      			
      	</tr>
      </table>
      <asp:label id="lbltest" runat=server  />
      <asp:textbox id="lbltestA" runat=server  value="FFA" text="HH" visible=false />
   	<asp:label id="lblkey" name="lblkey" runat=server value="EAT PORK!" visible=true />
   	<input type="hidden"  name="refererA" id="refererA" value="<table><tr><td>Test</td></tr></table>" runat=server >
   	<input type="hidden"  name="referer" id="referer" value="<style>&#46;myclass {color: blue; background-color: yellow; font-size:18px; border-width: 1px; border-color: blue; border-style: solid;}</style><mycustomtag class='myclass' contenteditable='false'>%ADKEY%</mycustomtag>">
   	<textarea id="txtMessage" runat="Server"  rows=0 cols=0  value="Enter message here!"  visible=false />


		<table border=0  width=100% cellspacing=0 cellpadding=0 id="SubNav">
			<tr >
				<td width="100%">
				    <table runat="Server" id="subnac" cellspacing=0 cellpadding=0 width=100% border=0>
						<tr height=22>
							<td id="subnavGen" align=center width=110><asp:linkbutton id="Lgen" Text= "General Setup"  runat="server" Font-underline="false" Style="cursor:hand" onClick="btn_Gen" /> </td>
							<td id="spacer0" class="tblcelltestc" width=1>&nbsp</td> 			           
							<td id="subnavPage1" align=center width=110><asp:linkbutton id="lpage1" Text= "Layout"  runat="server" Font-underline="false" Style="cursor:hand" onClick="btn_pg1" /> </td>
							<td id="spacer1" class="tblcelltestc" width=1>&nbsp</td> 			           
							<td id="subnavPage2" align=center width=110><asp:linkbutton id="lpage2" Text= "Branding"  runat="server" Font-underline="false" Style="cursor:hand" onClick="btn_pg2" /> </td>
							<td id="spacer2" class="tblcelltestc" width=1>&nbsp</td> 			           
							<td id="subnavPage3" align=center width=110><asp:linkbutton id="lpage3" Text= "AD Plans"  runat="server" Font-underline="false" Style="cursor:hand" onClick="btn_pg3" /> </td>
							<td id="spacer3" class="tblcelltestc" width=1>&nbsp</td> 
							<td id="subnavPage3A" align=center width=110><asp:linkbutton id="lpage3A" Text= "Publish Now"  runat="server" Font-underline="false" Style="cursor:hand" onClick="btn_pg3A" /> </td>
							<td id="spacer3A" class="tblcelltestc" width=1>&nbsp</td> 
							
							<td id="subnavPage4" align=center width=110><asp:linkbutton id="lpage4" Text= "Templates"  runat="server" Font-underline="false" Style="cursor:hand" onClick="btn_pg4" /> </td>
							<td id="spacer4" class="tblcelltestc" width=1>&nbsp</td> 
							<td id="subnavPage5" align=center width=110><asp:linkbutton id="lpage5" Text= "Images"  runat="server" Font-underline="false" Style="cursor:hand" onClick="btn_pg5" /> </td>
							<td id="spacer5" class="tblcelltestc" width=1>&nbsp</td> 	
							<td visible=true id="subnavPage6" align=center width=110><asp:linkbutton id="lpage6" Text= "Stats"  runat="server" Font-underline="false" Style="cursor:hand" onClick="btn_pg6" /> </td>
							<td id="spacer6" class="tblcelltestc" width=1>&nbsp</td> 	           
							<td  runat="Server" class="tblcelltestc" width=550>
		              		<asp:Panel ID="pnlbuttons" runat=server Visible=true>
				         		<table>
	                        	<tr>
	                           	<td align=left ><asp:button id="B_post" runat=server text="AD Venues" onclick="postad" CausesValidation="false" cssclass=frmbuttons /></td>		
			                        <td align=left ><asp:button id="b_savee" runat=server text="Save AD" onclick="savead" CausesValidation="false" cssclass=frmbuttons /></td>		
							  		    	<td align=left ><asp:button id="B_Final" runat=server text="Finalize AD" onclick="finalizeAD" CausesValidation="false" cssclass=frmbuttons /></td>		
							  			   <td align=left ><asp:button id="B_Exit" runat=server text="Exit" onclick="ExitAD" CausesValidation="false" cssclass=frmbuttons /></td>		
							  				<td align=left ><asp:button id="qaddtext" Visible=false runat=server text="Ad Text" onclick="quickadtext" CausesValidation="false" cssclass=frmbuttons /></td>		
							  			  	<td align=left ><asp:button id="B_removead" runat=server text="Remove AD" onclick="removead" CausesValidation="false" cssclass=frmbuttons /></td>		
							  			   <td align=left><asp:button id="adclone" Visible=false runat=server text="Clone AD" onclick="clonead" CausesValidation="false" cssclass=frmbuttons /></td>		
			                   	  	<td align=left><asp:button id="adcloneP" Visible=false runat=server text="Clone AD_W Plans" onclick="clonead" CausesValidation="false" cssclass=frmbuttonsxLG /></td>		
			                  		<td align=left><asp:button id="adedit" Visible=false runat=server text="Edit AD" onclick="editad" CausesValidation="false" cssclass=frmbuttons /></td>		
			                  
				  						</tr>
				  					</table>
				  				</asp:Panel>
		            	</td>
 			        	</tr>
 			       </table>
 			       
					 <table class=tblcelltestb width=100% cellpadding=0 cellspacing=0>
		      	    <tr>
		      		    <td>
		      		    		<asp:Panel ID="pnlconfirmfinalize" runat="server" Visible="false"> 
		      		    		 <div style="margin:0;background: #4682B4;padding:1;"></div>
 			       				    <div id="fieldtitles" style="vertical-align: top; height: 460px;margin:5; ">  
					                <table>
					                    <tr>
					                        <td colspan=3><font size=4 color=red>You are about to finalize your AD!</font></td>
					                    </tr>
					                    <tr>
					                    		<td colspan=3><font size=3>
					                          Once finalized you will not be able to make changes to this AD.  You will
					                          only be allowed to add new postings.</font></td>
					                    </tr>
					                    <tr>
					                     <td width=70><asp:button id="cnffinal" runat=server text="Confirm" onclick="finalizesave" CausesValidation="false" cssclass=frmbuttons /></td>		
								     	     	<td><asp:button id="cnfreturn" runat=server text="Cancel" onclick="cancelfinalize" CausesValidation="false"  cssclass=frmbuttons /></td>		
								     			<td width=80%></td>
								     </tr>
					                </table>
					             </div>
					             </asp:Panel> 
		      		    
		      		 	    <asp:panel id="pnlgeneral" runat=server visible=false >
		      		 	    
		      		 	     <div style="margin:0;background: #4682B4;padding:1;"></div>
 			       				    <div id="fieldtitles" style="vertical-align: top; height: 460px;margin:5; ">
 			       				   	 	<asp:panel id=pnlstillgetleads runat=server visible=false>
 			       				   	 		<table>
 			       				   	 			<tr>
 			       				   	 				<td><asp:checkbox id=chkstillgetleads text="This AD has been Inactivated.  Should New leads still be accepted?" runat=server autopostback=true oncheckedchanged="updateallowleads"/></td>
 			       				   	 			</tr>
 			       				   	 		</table>
 			       				   	 	</asp:panel>
 			       				   	 	<asp:panel id=pnladcloned runat=server visible=false>
 			       				   	 		<table>
 			       				   	 			<tr>
 			       				   	 				<td><font color=red><asp:label id=lbladcloned runat=server visible=false text="AD was successfully cloned!" /></font></td>
 			       				   	 			</tr>
 			       				   	 		</table>
 			       				   	 	</asp:panel>
 			       				   	 	
 			       				   	 	
 			       				   	 	
 			       				   	 	<table width=80%>
					                        <tr>
					                          <td><b>Status</b></td>
					                          <td ><b>AD Name</b>&nbsp&nbsp<font size=1><i> Internal use</i></font></td>
					                        </tr>
					                        <tr>
					                        	<td><asp:DropDownList id="dd_status" runat="server"  AutoPostBack =true OnSelectedIndexChanged ="adstatchange">    							               
			    							                 <asp:ListItem Value="Active" Text="Active"/>
			  	    						                 <asp:ListItem Value="Inactive" Text="Inactive"/>
			  	    						                </asp:DropDownList></td>
			  	    						          <td ><asp:textbox id="adname" runat=server size=90 /></td> 
			  	    						      </tr>
			  	    						    </table>
			  	    						    <table width=80% style="margin-top:10;">
					                        <tr>
					                        	<td ><b>AD Title</b>&nbsp&nbsp<font size=1><i>This field will be published</i></font></td>
					                            
					                        </tr>
					                        <tr>
					                           <td ><asp:textbox id="adtitle" runat=server size=120 /></td>
					                        </tr>
					                    	</table>
 			       				   	 	<table width=100% style="margin-top:10;">	                        
					                        <tr>
					                        	<td colspan=4 ><b>Lead Definitions</b>&nbsp&nbsp<font size=1><i>Assigned when NEW lead is generated</i></font></td>
					                        </tr>
					                        <tr height=2><td></td></tr>
					                        <tr>
					                         	 <td width=10></td>
					                         	 <td><b>Lead Status</b></td>
					                            <td><b>Lead Type</b></td>
					                            <td><b>Lead Program</b></td>
					                            <td><b>Marketing Program</b></td>
					                   	 	</tr>
								  			 		<tr>
								  			    		<td width=10></td>
								  			    		<td>NEW</td>
				  	    								<td><asp:DropDownList ID="ddlleadtypeFilter" 
				                  		            	DataValueField="x_descr" 
				                  		            	Runat="server" /></td>	
				                  		     	<td><asp:DropDownList ID="ddlleadprogramFilter" AutoPostBack =true OnSelectedIndexChanged ="ltfilter"
				                  		            	DataValueField="x_descr" 
				                  		            	Runat="server" /></td>
				                  		      <td><asp:DropDownList id="dd_mkprg" AutoPostBack="false"
							                  			DataValueField="x_descr" 
							                  			Runat="server" />	</td>  	
				          		            	
				          		         	</tr>
					                    	</table>
					                	</div>
 			      			 </asp:panel>
 			      			 <asp:panel id="pnlpage1" runat=server visible=false >
 			      			 <div style="margin:0;background: #4682B4;padding:1;"></div>
 			       				    <div id="fieldtitles" style="vertical-align: top; height: 460px;margin:5; ">
	 			       				    <table>
	 			       				    		<tr>
								  			    		<td valign=middle ></td>
								  			    		<td><asp:DropDownList visible=false id="dd_Ltemplates" runat="server" AutoPostBack =true OnSelectedIndexChanged ="doLtemplates" >    							               
			    							                 <asp:ListItem Value="Freeform" Text="Freeform"/>
			  	    						                 <asp:ListItem Value="Template 1" Text="Template 1"/>
			  	    						                </asp:DropDownList></td>	
			  	    						     		<td valign=middle align=left width=60><b>Templates</b></td>
														<td valign=bottom ><asp:DropDownList ID="ddemailcor" visible=true AutoPostBack =true OnSelectedIndexChanged ="prefillemail" DataValueField="email_tbl_pk"          		                    Runat="server" /></td>
			  	    								</tr>	
											</table>
											<table style="margin-top:10;">	  
													 
													<tr>
														<td valign=bottom><font size=3><b><u><asp:label id=adtxtreadonlyH text="AD Text/Layout" runat=server visible=false/></u></b></font></td>
													</tr>                         
			                            	<tr>	
			                            	  		
			                            	  		<td valign=top colspan=2><asp:label id=adtxtreadonly runat=server visible=false/></td>
												
			                            	</tr>
			                            	<tr>
			                            		<td colspan=2>
			                            		<asp:button visible=false id="tstAA" runat=server text="test" onclientclick="insertDate();return false;" CausesValidation="false"  cssclass=frmbuttons />
			                            		</td>
			                            	</tr>
			                          </table>
			                            <asp:panel id="pnltempatespre" runat=server visible=false  >
		          					                    <table>
		          						                    <tr>
		          						                    		<td ><asp:linkbutton id="htemps" Text= "Hide Templates"  onclick="hidetemplates" runat="server" Font-underline="false" Style="cursor:hand"  /> </td>
													
		          						                    </tr>
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
										                                            <td><asp:linkbutton id="vwbodyA" runat=server text="[Detail View]"  visible=true onclick="showbdyA" autopostback=true CausesValidation="false" cssclass=linkbuttons /></td>		
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
										                                            <td><asp:button id="BAppendAll" runat=server text="Replace"  visible=true onclick="appendAll" CausesValidation="false" cssclass=frmbuttons onmouseover="document.getElementById('myToolTip').className='activeToolTip'" onmouseout="document.getElementById('myToolTip').className='idleToolTip'"  /> </td>		
														                            </tr>
														                            </table>   
									            		                    </ItemTemplate>                                                     
								            		                    </asp:TemplateColumn>
														                        <asp:TemplateColumn HeaderText="" visible=true ItemStyle-Width="100px" ItemStyle-cssclass = "dgitemsNOBD" >
													   	                    <ItemTemplate  >
										                	                    <table width=100% cellspacing=0 cellpadding=0 >
										                	                 <tr>
														                                <td><input id="BAppendBody" runat=server  type="button" value="Append "  visible=true  class=frmbuttons  onmouseover="document.getElementById('myToolTipA').className='activeToolTip'" onmouseout="document.getElementById('myToolTipA').className='idleToolTip'"  /></td>		
														                            </tr>
														                            </table>   
									            		                    </ItemTemplate>                                                     
								            		                    </asp:TemplateColumn>
														                       <asp:TemplateColumn HeaderText="" visible=false ItemStyle-Width="100px" ItemStyle-cssclass = "dgitemsNOBD" >
													   	                    <ItemTemplate  >
										                	                    <table width=100% cellspacing=0 cellpadding=0 >
										                	                 <tr>
														                                <td><asp:button id="BAppendSubject" runat=server text="Append Subject "  visible=false onclick="appendSubject" CausesValidation="false" cssclass=frmbuttons /></td>		
								  							                         </tr>
								  							                         </table>   
									            		                    </ItemTemplate>                                                     
								            		                    </asp:TemplateColumn>
								  							                     <asp:TemplateColumn HeaderText="" visible=true ItemStyle-Width="100px" ItemStyle-cssclass = "dgitemsNOBD" >
													   	                    <ItemTemplate  >
										                	                    <table width=100% cellspacing=0 cellpadding=0 >
										                	                 <tr>
								  							                             <td><asp:button id="BCancel" runat=server text="Exit"  visible=true onclick="Canceltemplate" CausesValidation="false" cssclass=frmbuttons /></td>		
								    								                    </tr>
										                	                    </table>   
									            		                    </ItemTemplate>                                                     
								            		                    </asp:TemplateColumn>
								              		                    <asp:BoundColumn  HeaderText="Body"  DataField="email_text" visible="false" ItemStyle-Width="450px"    />
				     		            		                    </Columns>
							        				                    </asp:DataGrid>
		          							                    </td>
		          						                    </tr>
		          					                    </table>	<div id="myToolTipA" class="idleToolTip">Position your cursor to where you want the text inserted and then click Append.</div>
													                            <div id="myToolTip" class="idleToolTip">Will replace/overwrite the Subject Line and Email Body with template</div>
		        
		          				                    </asp:panel>

			                          
			                          <asp:panel id=pnltbar runat=server visible=true>    
			                          <table width=40% border=0>
			                          		
			                          		<tr>
			                          			<td width=10></td>
			                     	     		<td align=center><asp:linkbutton id="btoolbar" Text= "Toggle ToolBar"  runat="server" Font-underline="false" Style="cursor:hand"  /> </td>
														<td width=10  align=center>|</td>
													  	<td align=center><asp:linkbutton id="bKeyPH" Text= "Insert AD Key" onclientclick="insertkey();return false;"  runat="server" Font-underline="false" Style="cursor:hand"  /> </td>
														<td width=10  align=center>|</td>
													 	<td  align=center><asp:linkbutton id="Ctxt" Text= "Clear TextBox" onclick="clrtxtbox" runat="server" Font-underline="false" Style="cursor:hand"  /> </td>
														<td width=30%</td>
													</tr>
												</table>
												</asp:panel>
												<table>
			                            	<tr>
			                            			                              
		                              		<td colspan=3><ed:Editor ShowQuickFormat="false" FixedToolbar="false"  AutoFocus="false" height="400" width="1100" submit="false" PathPrefix="Editor_data/" FlashBrowse="myFlashBrowse.aspx" MediaBrowse="myMediaBrowse.aspx" ImageBrowse="Editor_data/myImageBrowseClient.aspx" UrlBrowse="myUrlBrowse.aspx" id="adtext" PreviewMode="true" runat="server">
					                              		
															</ed:Editor>
														</td>
													 	</tr>
													 	<tr>
			                          			<td colspan=3><asp:label id=lbledittext visible=false runat=server /></td>
			                          		</tr>
		                     		</table>    
 			       				    </div>
 			      			 </asp:panel>
 			      			 <asp:panel id="pnlpage2" runat=server visible=false >
 			      			 <div style="margin:0;background: #4682B4;padding:1;"></div>
		       				   <div id="fieldtitles" style="vertical-align: top; height: 460px;margin:5; ">
		       				   	<asp:panel id="pnlbrandwarning" runat=server visible=false >
		       				   		<table>
		       				   			<tr>
 			       				    				<td colspan=2><font color=red size=4>Warning!</font>  There are multiple ADS linked to this Branding record.</td> 
 			       				    		</tr>
 			       				    		<tr height=7><td> &nbsp </td></tr>
 			       				    		<tr>
 			       				    				
 			       				    		</tr>
 			       				    	</table>
 			       				    	<table width=40%>
 			       				    		<tr>
 			       				    			<td width=4%></td>
 			       				    			<td >Are you sure you want to continue?</td> 
														<td><asp:button id="btnEBYes" runat=server text="Yes" onclick="EBYes" CausesValidation="true" cssclass=frmbuttonslg /></td>	
							      					<td><asp:button id="btnEBNo" visible=true runat=server text="No" onclick="EBNo" CausesValidation="true" cssclass=frmbuttonslg /></td>	
							      				 			       				    				
 			       				    				
 			       				    		</tr>
 			       				   	</table>
 			       				   </asp:panel>
 			       				   <asp:panel id="pnlbrandMain" runat=server visible=true >
		       				   	<table width=95% border=0>
 			       				    		
 			       				    		<tr>
 			       				    		 	<td><b>Select Branding</d><asp:linkbutton visible=false id="btn2prev" CausesValidation="false" Text= "Preview" runat="server"  cssclass=linkbuttons  onClick="previewpg2" /></td>
				      							<td><asp:DropDownList ID="ddintakeresponse" AutoPostBack =true OnSelectedIndexChanged ="addnew"
				          		               	     	DataValueField="br_name" 
				          		                  	  	Runat="server" /></td>	
		          		            		<td width=20></td>
		          		            		 <td align=center ><asp:linkbutton id="btneditbranding" Text="Edit Selected Branding" visible=true  runat="server" cssclass=linkbuttons onClick="editbranding"  /></td> 
 					                      
 					                       	<td align=center ><asp:linkbutton id="btnpg1" Text="|&nbsp View Intake Page"  runat="server" cssclass=linkbuttons onClick="btn_bpg1"  /></td> 
 					                 
 					                        <td align=left width=29%><asp:linkbutton id="btnpg2" Text="|&nbsp View Confirmation Page"  runat="server" cssclass=linkbuttons onClick="btn_bpg2" /></td> 
 					            	    
 					            	        	<td width=5%></td>
 			       				    		</tr>
 			       				    	</table>	
 			       				    	<asp:panel id=pnlbrdpg1 runat=server visible=true >
	 			       				    	<table>
	 			       				    		<tr>
	 			       				    			<td><b>Intake Form</b></td>
	 			       				     		</tr>
	 			       				    		<tr>
	 			       				    			<td><iframe id="Bgpg1" src="" height="410" width="900" runat=server /></td>
	 			       				    		</tr>
	 			       				    	</table>
	 			       				  </asp:panel>
	 			       				  <asp:panel id=pnlbrdpg2 runat=server visible=false >
	 			       				  		<table>
	 			       				    		<tr>
	 			       				    			<td><b>Response Form</b></td>
	 			       				    		</tr>
	 			       				    		<tr>
	 			       				    			<td><iframe id="Bgpg2" src="" height="410" width="900" runat=server /></td>
	 			       				    		</tr>
	 			       				    	</table>
	 			       				  </asp:panel>
		       					  </asp:panel>
						 			</div>
 			      			 </asp:panel>	
 			      			 <asp:panel id="pnlpage3" runat=server visible=false >
 			      			 <div style="margin:0;background: #4682B4;padding:1;"></div>
 			       				    <div id="fieldtitles" style="vertical-align: top; height: 460px;margin:5; ">
 			       				    	<asp:panel id="pnlNewADPlan" runat=server visible=false >
		       						 		<table>
		       						 			<tr>
		       						 				<td class=wfstepheaderA>New AD Plan</td>
		       						 			</tr>
		       						 		</table>
		       						 		<table><tr><td>
		       						 		<table>
		       						 			<tr>
		       						 				<td width=405 valign=bottom>
		       						 					<table>
		       						 						<tr>
					       						 				<td >Enter a Start and End Date for this plan</td>
					       						 			</tr>
					       						 			<tr>
					       						 				<td style="font-size:90%;"><i>*Note AD Plans must be atleast 1 week in length and no longer than 1 month*</i></td>
 			       				    						</tr>
 			       				    					</table>
 			       				    				</td>
 			       				    				<td>
		       						 					<table cellpadding=2 cellspacing=2>
		       						 						<tr>
		       						 							<td><asp:linkbutton id="lnkAPStart" ToolTip="Click for Calendar" Text= "Start Date" CausesValidation="false"  runat="server" cssclass=linkbuttonsRed  visible =true onClick="showcalendar2" /></td>
				                    							<td><asp:linkbutton id="lnkAPend"  ToolTip="Click for Calendar" Text= "End Date" CausesValidation="false"  runat="server" cssclass=linkbuttonsRed  visible =true onClick="showcalendar2" /></td>
				                    		
					       						 			</tr>
		       						 						<tr>
					       						 				<td><asp:textbox id="txtNPSdate" runat=server /></td>
					       						 				<td><asp:textbox id="txtNPEdate" runat=server /></td>
					       						 			</tr>
					       						 			<tr>	
					       						 				<td>
																	     <asp:RequiredFieldValidator runat="server" id="rfvfname"
												          				ControlToValidate="txtNPSdate" display="dynamic">
												          				Required
												      				</asp:RequiredFieldValidator></td>
												      				<td>
																	     <asp:RequiredFieldValidator runat="server" id="rfvfnameA"
												          				ControlToValidate="txtNPEdate" display="dynamic">
												          				Required
												      				</asp:RequiredFieldValidator></td>
					       						 		</table>
					       						 	</td>
		       						 			</tr>
		       						 			<tr>
		       						 			</tr>
		       						 		</table>
		       						 		<table cellpadding=2 cellspacing=2 width=100%>
		       						 			
 			       				    			<tr>
 			       				    				<td width=405>How often will you be posting ADs?</td>
 			       				    				<td><asp:DropDownList ID="dd_PPfreqNP" runat=server Visible=true autopostback=true  OnSelectedIndexChanged="updateNPlabel">
			                              			  	<asp:ListItem Value="Select.." Text="Select.."/>
					                	            		<asp:ListItem Value="Daily" Text="Daily"/>
					                	            		<asp:ListItem Value="Weekly" Text="Weekly"/>
					                            			<asp:ListItem Value="Monthly" Text="Monthly"/>
					                            			</asp:DropDownList></td> 
 			       				    			</tr>
 			       				    			<tr>
 			       				    				<td>How many times per <asp:label id=lblnopostsN runat=server /> do you expect to post?</td>
 			       				    				<td><asp:textbox id="txtNPNoposs" runat=server  size=3/></td>
 			       				    			</tr>
 			       				    			<tr>
 			       				    				<td></td>
 			       				    				<td>
																	     <asp:RequiredFieldValidator runat="server" id="rfvfnameAA"
												          				ControlToValidate="txtNPNoposs" display="dynamic">
												          				Required
												      				</asp:RequiredFieldValidator></td>
												    </tr>
 			       				    			<tr>
 			       				    				<td>How many different venues do you expect to post to?</td>
 			       				    				<td><asp:textbox id="txtNPNoVens" runat=server size=3 /></td>
 			       				    			</tr>
 			       				    			<tr>
 			       				    				<td></td>
 			       				    				<td>
																	     <asp:RequiredFieldValidator runat="server" id="rfvfnameAAA"
												          				ControlToValidate="txtNPNoVens" display="dynamic">
												          				Required
												      				</asp:RequiredFieldValidator></td>
												    </tr>
 			       				    			<tr>
 			       				    				<td>How many leads do you expect to receive for each post you do?</td>
 			       				    				<td><asp:textbox id="txtNPNoLeads" runat=server size=3 /></td>
 			       				    			</tr>
 			       				    			<tr>
 			       				    				<td></td>
 			       				    				<td>
																	     <asp:RequiredFieldValidator runat="server" id="rfvfnameAAAA"
												          				ControlToValidate="txtNPNoLeads" display="dynamic">
												          				Required
												      				</asp:RequiredFieldValidator></td>
												    </tr>
 			       				    			
 			       				    			<tr>
 			       				    				<td>Please provide a name for this plan</td>
 			       				    				<td><asp:textbox id="txtNPPName" runat=server size=50 /></td>
 			       				    			</tr>
 			       				    			
 			       				    		</table>
 			       				    	</td>
 			       				    	<td valign=top>
 			       				    		<table>
													<tr>
														<td align=right><asp:linkbutton id="showcalcZ" Text= "Close" CausesValidation="false"  runat="server"   visible=false onClick="closecalendar2" /></td>
									                </tr>
									                <tr>
					                                    <td colspan=10><asp:calendar id="cdrCalendar2Z" runat="server" 
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
    				    					</td></tr></table>
    				    		
 			       				    		
 			       				    		<br><br>
 			       				    		<table>
 			       				    			<tr>
 			       				    				<td><asp:button id="btnNPPNext" runat=server text="Next Step" onclick="NPNextStep1" CausesValidation="true" cssclass=frmbuttonslg /></td>	
							      					<td><asp:button id="btnNPPSaveE" visible=false runat=server text="Save & Exit" onclick="NPSaveStep1" CausesValidation="true" cssclass=frmbuttonslg /></td>	
							      					<td><asp:button id="btnNPPcancel" runat=server text="Cancel" onclick="NPCancel" CausesValidation="false" cssclass=frmbuttonslg /></td>	
							    
 			       				    			</tr>
 			       				    		</table>
 			       				    	</asp:panel>
 			       				    	
 			       				    	
						 			       				    	
	 			       				    	<asp:panel id="pnlNewADPlanSV" runat=server visible=false >
					 			       				   <table width=100%><tr><td>
					 			       				    		
					 			       				    		<table>
					 			       				    			<tr>
					 			       				    				<td class=wfstepheaderA>Select your venues</td>
					 			       				    			</tr>
					 			       				    			<tr>
					 			       				    				<td style="font-size:90%;"><i>*Hold CTRL or Shift key to select multiple venues</i></td>
					 			       				    			</tr>
					 			       				    			<tr>
					 			       				    				<td ><font color=red><asp:label id=lblvenselect runat=server /></font></td>
					 			       				    			</tr>
					 			       				    		</table><br>
					 			       				    		<table>
					 			       				    			<tr>
					 			       				    				<td><asp:listbox id="lb_selvenues" runat="server"  selectionmode="multiple" 
					           													DataTextField="x_descr" DataValueField="tbl_xwalk_pk" enabled=true height=300 width=300 /></td>
					           										</tr>
					           									</table>
					           									<table>
					           										<tr>
					           											<td><asp:button id="btnNPPNextV" runat=server text="Next Step" onclick="NPNextStep2" CausesValidation="false" cssclass=frmbuttonslg /></td>	
												      					<td><asp:button id="btnNPPSaveEV" visible=false runat=server text="Save & Exit" onclick="NPSaveStep2" CausesValidation="false" cssclass=frmbuttonslg /></td>	
												      					<td><asp:button id="btnNPPNotlisted" runat=server text="Not Listed" onclick="addnewvenue" CausesValidation="false" cssclass=frmbuttonslg /></td>	
												      					<td><asp:button id="btnNPPNcancel" runat=server text="Cancel" visible=false onclick="CancelNewVen" CausesValidation="false" cssclass=frmbuttonslg /></td>	
												      					<td><asp:button id="btnNPPNskip" runat=server text="Skip" visible=false onclick="SkipNextStep2" CausesValidation="false" cssclass=frmbuttonslg /></td>	
												      			
					           										</tr>
					           									</table>
	           											</td>
	           											<td valign=top>
						           									 <asp:Panel ID="pnladdvenue" runat="server" Visible="false" style="margin-top:10;">
										                	    <table width=100% class=tblbordera>
										                	    	 <tr>
										                	    	 	<td colspan=6><b>Add New Lead Source</b></td>
										                	    	 </tr>
										                		    <tr>	
										                			    <td valign=top>Lead Source Name</td>
										                			     <td valign=top>Online?</td>
										                			    <td valign=top>Lead Source Code <img src="../images/help_icon.jpg" alt="Help" height=15 width=15 onmouseover="document.getElementById('myToolTipc').className='activeToolTip'" onmouseout="document.getElementById('myToolTipc').className='idleToolTip'"/></td>
										                			    <td valign=top>URL</td>
										                			    <td>Has Account Setup?</td>
										                			     <td valign=top>Private?</td>
										                			  
										                		    </tr>
										                		    <tr>
										                		     <td valign=top><asp:textbox id="venuname" runat=server /></td>
										                			   <td valign=top><asp:DropDownList id="ddvenonline" runat="server" >    							               
										    							                 <asp:ListItem Value="Yes" Text="Yes"/>
										  	    						                 <asp:ListItem Value="No" Text="No"/>
										  	    						                </asp:DropDownList></td>
										                			    <td valign=top><asp:textbox id="venuecode" runat=server size=4 /></td>
										                			   <td valign=top><asp:textbox id="venueurl" runat=server size=30 /></td>
										                			     <td valign=top><asp:checkbox id="acctsetup" runat=server /></td>
										                		    
										                			    <td valign=top><asp:checkbox id="privateven" runat=server /></td>
										                		    
										                		    </tr>
										                		    <tr>
										                		        <td colspan=6><div id="myToolTipc" class="idleToolTip">Enter characters to help ID this AD Code, ie. Craigs List = CL</div></td></tr>
										                		 </table>
										                		 <table>
										                		    <tr>	
										                			    <td colspan=8><asp:button id="addnewv" runat=server text="Save" onclick="savenewvenue" CausesValidation="false" cssclass=frmbuttons /></td>		
										    			                <td colspan=8><asp:button id="addnewvexit" runat=server text="Exit" onclick="savenewvenueExit" CausesValidation="false" cssclass=frmbuttons /></td>		
										    			   
										                		    </tr>
										                	    </table>               
										                    
										                    </asp:panel> 
 			       				    						</td></tr></table>
 			       				    	
 			       				    	
 			       				    	</asp:panel>
 			       				    	<asp:panel id="pnlNewADPlanSVDW" runat=server visible=false >
 			       				    		<table>
 			       				    			<tr>
 			       				    				<td class=wfstepheaderA>Select your Posting Days</td>
 			       				    			</tr>
 			       				    		</table><br>
 			       				    		<table width=30%>
 			       				    			<tr>
 			       				    				<td width=100><b>Current Venue:</b></td>
 			       				    				<td><font color=red><asp:label id=lblCvenue runat=server visible=true /></font></td>
 			       				    			</tr>
 			       				    		</table>
 			       				    		<table width=30%>
 			       				    			<tr>
 			       				    				<td >
 			       				    				<asp:Calendar id="Calendar1" runat="server" OnSelectionChanged="Calendar1_SelectionChangedAA"
 			       				    				 backcolor="#ffffff" width="300px" height="300px" 
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
 			       				    		</table>
 			       				    		<table>
 			       				    			<tr>
 			       				    				<td><asp:Button id="Button1" onclick="Button1_Click" runat="server" Text="Save & Next Venue"></asp:Button></td>
														<td><asp:Button id="ButtonZ" visible=false onclick="Button2_Click" runat="server" Text="Save"></asp:Button></td>
													
														<td><asp:Button id="Button2" onclick="Button1_Click" runat="server" Text="Save for All Venues"></asp:Button></td>
														<td><asp:Button id="Button3" visible=false onclick="Button1_Click" runat="server" Text="Cancel"></asp:Button></td>
													</tr>
												
												</table>							
												<table>
           										<tr>
           											<td><asp:button id="btnNPPNextVDW" visible=false runat=server text="Next Step" onclick="NPNextStep3" CausesValidation="false" cssclass=frmbuttonslg /></td>	
							      					<td><asp:button id="btnNPPSaveEVDW" visible=false runat=server text="Save & Exit" onclick="NPSaveStep3" CausesValidation="false" cssclass=frmbuttonslg /></td>	
							      			
           										</tr>
           									</table>      
												<asp:panel id=pnlolddselect visible=false runat=server >
		 			       				    		 <table width=100%>
										                <tr>
										                    <td><asp:DataGrid Runat=server
																	    		ID="dgVenueDOW" 
													                    	AutoGenerateColumns=False
															            	Width="100%"          
															             	OnItemDataBound="ItemDataBoundEventHandlerDOW">
											                           <HeaderStyle CssClass="dgheaders" />
														        				<ItemStyle CssClass="dgitems" />
														        				<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>
											    	            
															       <Columns >
												           		        <asp:BoundColumn HeaderText="Venue kEY" visible=false DataField="LV_tbl_pk" ItemStyle-Width="300px"   />
												        			    		
												           		        	<asp:BoundColumn HeaderText="Venue Name"  DataField="lv_name" ItemStyle-Width="200px"   />
												        			    		<asp:TemplateColumn HeaderText="Sunday" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center">
							        			  										<ItemTemplate >
																							<asp:CheckBox ID="chksunday" Runat="server" autopostback=true oncheckedchanged="dosavepostdays"  />
																						</ItemTemplate>
																				</asp:TemplateColumn>
												        			    		<asp:TemplateColumn HeaderText="Monday" ItemStyle-Width="20px">
							        			  										<HeaderTemplate  >
							        			  											Monday					        			  										</HeaderTemplate>
																						<ItemTemplate >
																							<asp:CheckBox ID="chkMonday" Runat="server"   autopostback=true oncheckedchanged="dosavepostdays"  />
																						</ItemTemplate>
																				</asp:TemplateColumn>
																				<asp:TemplateColumn HeaderText="Tuesday" ItemStyle-Width="20px">
							        			  										<HeaderTemplate  >
							        			  											Tuesday					        			  										</HeaderTemplate>
																						<ItemTemplate >
																							<asp:CheckBox ID="chkTuesday" Runat="server" autopostback=true oncheckedchanged="dosavepostdays"   />
																						</ItemTemplate>
																				</asp:TemplateColumn>
												        			    		<asp:TemplateColumn HeaderText="Wednesday" ItemStyle-Width="20px">
							        			  										<HeaderTemplate  >
							        			  											Wednesday					        			  										</HeaderTemplate>
																						<ItemTemplate >
																							<asp:CheckBox ID="chkWednesday" Runat="server" autopostback=true oncheckedchanged="dosavepostdays"  />
																						</ItemTemplate>
																				</asp:TemplateColumn>
												        			    		<asp:TemplateColumn HeaderText="Thursday" ItemStyle-Width="20px">
							        			  										<HeaderTemplate  >
							        			  											Thursday					        			  										</HeaderTemplate>
																						<ItemTemplate >
																							<asp:CheckBox ID="chkThursday" Runat="server" autopostback=true oncheckedchanged="dosavepostdays"  />
																						</ItemTemplate>
																				</asp:TemplateColumn>
												        			    		<asp:TemplateColumn HeaderText="Friday" ItemStyle-Width="20px">
							        			  										<HeaderTemplate  >
							        			  											Friday					        			  										</HeaderTemplate>
																						<ItemTemplate >
																							<asp:CheckBox ID="chkFriday" Runat="server"   autopostback=true oncheckedchanged="dosavepostdays" />
																						</ItemTemplate>
																				</asp:TemplateColumn>
												        			    		<asp:TemplateColumn HeaderText="Saturday" ItemStyle-Width="20px">
							        			  										<HeaderTemplate  >
							        			  											Saturday					        			  										</HeaderTemplate>
																						<ItemTemplate >
																							<asp:CheckBox ID="chkSaturday" Runat="server" autopostback=true oncheckedchanged="dosavepostdays"  />
																						</ItemTemplate>
																				</asp:TemplateColumn>
												        			    	 
														         </Columns>
													            </asp:DataGrid>
												            </td>
										                </tr>
										            </table>
 			       				    		</asp:panel>
 			       				    		
 			       				    		
 			       				    		
 			       				    	</asp:panel>
 			       				    	<asp:panel id="pnlNewADActivateplan" runat=server visible=false cellspacing=4>
 			       				    		<table>
 			       				    			<tr>
 			       				    				<td  class=wfstepheaderA>Congratulations! </td>
 			       				    			</tr>
 			       				    			
 			       				    			<tr>
 			       				    				<td>Your Posting plan is now ready to be activated.</td>
 			       				    			</tr>
 			       				    		</table><br>
 			       				    		  
 			       				    		<table>
 			       				    			<tr>
 			       				    				<td>Would you like to active this plan?</td>
 			       				    				<td><asp:DropDownList ID="dd_ActivatePPNow" runat=server Visible=true autopostback=true  OnSelectedIndexChanged="updateNPActivate">
			                              			  	<asp:ListItem Value="Select.." Text="Select.."/>
					                	            		<asp:ListItem Value="No" Text="No"/>
					                	            		<asp:ListItem Value="Yes" Text="Yes"/>
					                            			</asp:DropDownList></td> 
 			       				    			</tr>
 			       				    		</table>
 			       				    		<asp:panel id="pnlNewADPostPlan" runat=server visible=false >
	 			       				    		<table>
	 			       				    			<tr>
	 			       				    				<td>Would you like to post to any of your venues now?</td>
	 			       				    				<td><asp:DropDownList ID="dd_PostPPNow" runat=server Visible=true autopostback=true  OnSelectedIndexChanged="updateNPPostNow">
				                              			  	<asp:ListItem Value="Select.." Text="Select.."/>
				                              			  	<asp:ListItem Value="No" Text="No"/>
						                	            		<asp:ListItem Value="Yes" Text="Yes"/>
						                            			</asp:DropDownList></td> 
	 			       				    				
	 			       				    			</tr>
	 			       				    		</table>
 			       				    		
 			       				    		</asp:panel>
 			       				    	
 			       				    	
 			       				    	
 			       				    	</asp:panel>
 			       				    	
 			       				    	
 			       				    	
 			       				    	
 			       				    	<asp:panel id="pnlpostings" runat=server visible=true >
		       						 	<table width=26%>
							       		<tr>
							       			<td class=wfstepheaderA >AD Publishing Plans</td>
							       			<td><asp:button id="btnAddVenue" runat=server text="Add New Plan" onclick="Addposting" CausesValidation="false" cssclass=frmbuttonslg /></td>	
							       		</tr>
							       	 </table>
			       				    <table width=100%>
	         							<tr>
							            	<td>
							               <div style="vertical-align top; height: 410px; overflow:auto;">
										 			<asp:DataGrid Runat=server visible=true
												   		ID="ADPlans" 
								                    	AutoGenerateColumns=False
								                    	Width="100%"          
												    		PagerStyle-Visible = "False"	
												    		CssClass="dg">
							    	            		<HeaderStyle CssClass="dgheaders" />
										        			<ItemStyle CssClass="dgitems" />
										        			<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>
											       		<Columns >
								           		   		<asp:BoundColumn HeaderText="Plan ID" visible=false DataField="LAP_tbl_pk" ItemStyle-Width="10px"   />
								           		    	 	<asp:BoundColumn HeaderText="Name" visible=true DataField="lap_name" ItemStyle-Width="180px"   />
								         					<asp:BoundColumn HeaderText="Status" visible=true DataField="lap_status" ItemStyle-Width="180px"   />
								         					
								         					<asp:BoundColumn HeaderText="Posting Frequency" visible=true DataField="lap_freq" ItemStyle-Width="90px"   />
								         						
								         					<asp:TemplateColumn HeaderText="" visible=true ItemStyle-Width="100px"  ItemStyle-CssClass="dgitemsNOBD">
												            <ItemTemplate >
												                <table width=100%>
												                    <tr>
												                        <td><asp:button id="btneditplan" runat=server text="Edit Plan"  visible=true onclick="editplan_Click" CausesValidation="false" cssclass=frmbuttonsLG /></td>		
										                     			<td><asp:button id="btngetkeys" runat=server text="Get Ad Keys"  visible=true onclick="getkeys_Click" CausesValidation="false" cssclass=frmbuttonsLG /></td>		
										  										<td><asp:button id="btnNewPost" runat=server text="Add New Post"  visible=false onclick="qpost_ClickADD" CausesValidation="false" cssclass=frmbuttonsLG /></td>		
										  										<td><asp:button id="btnRRPost" runat=server text="Rerun Posts"  visible=false onclick="qpost_Click" CausesValidation="false" cssclass=frmbuttonsLG /></td>		
										  										<td><asp:button id="btnViewPost" runat=server text="View Posts"  visible=false onclick="qpost_Click" CausesValidation="false" cssclass=frmbuttonsLG /></td>		
										  										<td><asp:button id="btnCloneP" runat=server text="Clone Plan"  visible=false onclick="qpost_Click" CausesValidation="false" cssclass=frmbuttonsLG /></td>		
										  										
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
							       	 	<asp:panel id="pnlshowadkeys" runat=server visible=false >
				       						 <table width=26%>
									       		<tr>
									       			<td class=wfstepheaderA >Ad Keys</td>
									       			<td><asp:button id="btnreturnPPs" runat=server text="Show AD Plans" onclick="showadplans" CausesValidation="false" cssclass=frmbuttonslg /></td>	
									       		</tr>
									       	 </table>
					       				    <table width=100%>
			         							<tr>
									            	<td>
									               <div style="vertical-align top; height: 410px; overflow:auto;">
												 			<asp:DataGrid Runat=server visible=true
														   		ID="ADKeys" 
										                    	AutoGenerateColumns=False
										                    	Width="100%"  
										                    	OnItemDataBound="ItemDataBoundEventHandlerKeys"        
														    		PagerStyle-Visible = "False"	
														    		CssClass="dg">
									    	            		<HeaderStyle CssClass="dgheaders" />
												        			<ItemStyle CssClass="dgitems" />
												        			<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>
													       		<Columns >
									       	 						<asp:BoundColumn HeaderText="Venue" visible=true DataField="lv_name" ItemStyle-Width="100px"   />
										           		    	 	<asp:BoundColumn HeaderText="AD Key" visible=true DataField="lv_key" ItemStyle-Width="60px"   />
										         					<asp:BoundColumn HeaderText="AD Key URL" visible=true DataField="KeyUrl" ItemStyle-Width="340px"   />
										         					<asp:TemplateColumn HeaderText="" visible=true ItemStyle-Width="20px" ItemStyle-CssClass="dgitemsNOBD" >
															            <ItemTemplate >
															                <table width=100%>
															                    <tr>
															                        <td><input id="BtnGrabKey" runat=server type="button"  value="Grab KEY" class=frmbuttons   /></td>		
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
							       	 	
							       	 	
							       	 	
							       	 	
							       	 	
							       	 	
							       	 	
							       	 	<asp:panel id=pnlPPwarn runat=server visible=false>
							       	 		<table>
							       	 			<tr>
							       	 				<td>Unable able add new posting record! </td>
							       	 			</tr>
							       	 			<tr>
							       	 				<td>This AD is NOT finalized and has an existing posting record.
							       	 				To add a new posting record you MUST finalize this Ad..</td>
							       	 			</tr>
							       	 			<tr>
							       	 				<td><asp:button id="btnPPwarn" runat=server text="Continue" onclick="ppwarncontinue" CausesValidation="false" cssclass=frmbuttons /></td>		
							       	 			</tr>
							       	 		</table>	
							       	 	
							       	 	</asp:panel>
							       	 	
							       	 	
			       				   <asp:panel id=pnlPPdetail runat=server visible=false>
			       				   	<asp:panel id=pnlPPdetailMain runat=server visible=true>
				       				   	<table>
				       				   		<tr>
				       				   			<td class=wfstepheaderA>Publishing Plan Detail</td>
				       				   		</tr>
				       				   	</table>
				       				   	<table width=100% class=tblborder>
				       				   		<tr>
				       				   			<td width=100%>
				       				   				<table width=100%>
				       				   					<tr>
				       				   						<td style="font-size:95%;font-weight:bold;">Status</td>
							       				   			<td style="font-size:95%;font-weight:bold;">Plan Name</td>
							       				   			<td style="font-size:95%;font-weight:bold;">Start Date</td>
							       				   			<td style="font-size:95%;font-weight:bold;">End Date</td>
							       				   		</tr>
							       				   		<tr>
							       				   			<td><asp:DropDownList ID="dd_PPstat" runat=server Visible=true>
					                              			  	<asp:ListItem Value="Active" Text="Active"/>
							                	            		<asp:ListItem Value="Inactive" Text="Inactive"/>
							                	            		<asp:ListItem Value="Incomplete" Text="Incomplete"/>
							                            			</asp:DropDownList></td>  	
						       				   				<td><asp:textbox id="PPName" runat=server size=30 /></td>
							       				   			<td><asp:textbox id="PPSdate" runat=server size=10 /></td>
							       				   			<td><asp:textbox id="PPEdate" runat=server size=10 /></td>
							       				   		</tr>
							       				   	</table>
							       				   </td>
							       				 </tr>
							       				 <tr>
							       				   <td width=100%>
							       				   	<table width=100%>
							       							<tr>
							       				   			<td style="font-size:95%;font-weight:bold;">Exp. Posts</td>
							       				   			<td style="font-size:95%;font-weight:bold;"> Frequency</td>
							       				   			<td colspan=2 style="font-size:95%;font-weight:bold;">For How Long</td>
							       				   			<td style="font-size:95%;font-weight:bold;">Expected Venues</td>
							       				   			<td style="font-size:95%;font-weight:bold;">ROP&nbsp <font size=1><i># Leads/Post (estimate)</i></font></td>
							       				   			<td style="font-size:95%;font-weight:bold;">Target Lead Count&nbsp&nbsp<asp:linkbutton id="btnrxleads" Text="Refresh"  runat="server"  onClick="refreshxleads"  /></td>
							       				   			<td style="font-size:95%;font-weight:bold;">Total Posts</td>
							       				   		
							       				   		</tr>
							       				   		<tr>
							       				   			<td><asp:textbox id="PPnoposts" runat=server size=3 /></td>
							       				   			<td><asp:DropDownList ID="dd_PPfreq" runat=server Visible=true>
						                              			  	<asp:ListItem Value="Daily" Text="Daily"/>
								                	            		<asp:ListItem Value="Weekly" Text="Weekly"/>
								                            			<asp:ListItem Value="Monthly" Text="Monthly"/>
								                            			</asp:DropDownList></td>  	
							       				   			<td><asp:textbox id="PPDV" runat=server size=3 /></td>
							       				   			<td><asp:DropDownList ID="dd_PPDperiod" runat=server Visible=true>
						                              			  	<asp:ListItem Value="Days" Text="Days"/>
								                	            		<asp:ListItem Value="Weeks" Text="Weeks"/>
								                            			<asp:ListItem Value="Months" Text="Months"/>
								                            			</asp:DropDownList></td> 
								                           <td><asp:textbox id="PPEXP" runat=server size=3/></td>
								                           <td><asp:textbox id="PPROP" runat=server size=3/></td>
								                           <td><asp:label id=lblxleads runat=server /></td>
								                       		<td><asp:textbox id="TotPosts" visible=true runat=server size=3 enabled=false/></td>
								                       	</tr>
								                      </table>
								                   </td>
								            	</tr>
					                    	</table>
					                    	<table>
								       			<tr>						       			
						       					   <td><asp:button id="btnSavePPlan" runat=server text="Save Plan" onclick="SavePPlan" CausesValidation="false" cssclass=frmbuttons /></td>		
				    			     				   <td ><asp:button id="btnPlanExit" runat=server text="Exit" onclick="ExitPlan" CausesValidation="false" cssclass=frmbuttons /></td>		
				    			  
				    			     			</tr>
			    			   				</table>
			    			   			</asp:panel>
			    			   			<br>
			    			   			<asp:panel id=pnlsverror runat=server visible=false>
			    			   				<table>
			    			   					<tr>
			    			   						<td>Note: These venues exist and are active.  They have were not added..</td>
			    			   					</tr>
			    			   					<tr>
			    			   						<td><font color=red><asp:label  id=lblerror runat=server /></font></td>
			    			   					</tr>
			    			   					
			    			   						
			    			   				</table>
			    			   			</asp:panel>
			    			   			
			    			   			<asp:panel id=pnlplansched runat=server visible=false>
						               <table width=100%>
						               	<tr>
						               		<td>
								               	<table width=100%>
								               		<tr>
								               			<td class=wfstepheaderA width=15%>AD Plan Schedule</td>
								               			<td width=65%><asp:linkbutton id="btnrefreshsched" Text= "Refresh" CausesValidation="false"  runat="server"   visible=true onClick="refreshsched" /></td>
								               			<td>Status</td>
								               			<td width=10%><asp:DropDownList ID="dd_schstatfilter" runat=server Visible=true autopostback=true  OnSelectedIndexChanged="schfilter" >
			                              			  	<asp:ListItem Value="All" Text="All"/>
					                	            		<asp:ListItem Value="Active" Text="Active"/>
					                            			<asp:ListItem Value="Inactive" Text="Inactive"/>
					                            			</asp:DropDownList></td> 
					                            			
								               			<td width=30%><asp:button id="btnAddV" runat=server text="Add Venue" onclick="AddnewVen" CausesValidation="false" cssclass=frmbuttons /></td>		
				    			  							 
					                            		</tr>
								               	</table>  
					    			   				<table width=100%>   	
				         								<tr>
										      	      	<td>
										         		      <div id="ppgridSCH" runat=server style="vertical-align top; height: 230px; overflow:auto;">
													 				<asp:DataGrid Runat=server visible=true
															   		ID="ADPlanSched" 
											                    	AutoGenerateColumns=False
											                    	Width="100%" 
											                    	
											                    	OnItemDataBound="ItemDataBoundEventHandlerPSCH"         
															    		CssClass="dg">
										    	            		<HeaderStyle CssClass="dgheaders" />
													        			<ItemStyle CssClass="dgitems" />
													        			<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>
														       		<Columns >
													           		        	<asp:BoundColumn HeaderText="Venue kEY" visible=true DataField="LV_tbl_pk" ItemStyle-Width="300px"   />
													        			    		<asp:BoundColumn HeaderText="Venue Name"  DataField="lv_name" ItemStyle-Width="200px"   />
													        			    		<asp:BoundColumn HeaderText="S" visible=false DataField="lv_sunday" ItemStyle-Width="200px"   />
													        			    		<asp:BoundColumn HeaderText="M" visible=false DataField="lv_monday" ItemStyle-Width="200px"   />
													        			    		<asp:BoundColumn HeaderText="T" visible=false DataField="lv_tuesday" ItemStyle-Width="200px"   />
													        			    		<asp:BoundColumn HeaderText="E" visible=false DataField="lv_wednesday" ItemStyle-Width="200px"   />
													        			    		<asp:BoundColumn HeaderText="T" visible=false DataField="lv_thursday" ItemStyle-Width="200px"   />
													        			    		<asp:BoundColumn HeaderText="F" visible=false DataField="lv_firday" ItemStyle-Width="200px"   />
													        			    		<asp:BoundColumn HeaderText="S" visible=false DataField="lv_saturday" ItemStyle-Width="200px"   />
													        			    		<asp:BoundColumn HeaderText="Ad Code" visible=true DataField="lv_key" ItemStyle-Width="200px"   />
													        			   		<asp:BoundColumn HeaderText="Status" visible=true DataField="lv_status" ItemStyle-Width="200px"   />
													        			   		<asp:BoundColumn HeaderText="# Unpublished" visible=true DataField="Unpubcnt" ItemStyle-Width="200px"   />
													        			   		<asp:BoundColumn HeaderText="# Published" visible=true DataField="Pubcnt" ItemStyle-Width="200px"   />
													        			    		<asp:TemplateColumn HeaderText="Sunday" ItemStyle-Width="20px" ItemStyle-HorizontalAlign="Center" visible=false>
								        			  										<ItemTemplate >
																								<asp:CheckBox ID="chksundayV" Runat="server" autopostback=true oncheckedchanged="dosavepostdays"  />
																							</ItemTemplate>
																					</asp:TemplateColumn>
													        			    		<asp:TemplateColumn HeaderText="Monday" ItemStyle-Width="20px" visible=false>
								        			  										<HeaderTemplate  >
								        			  											Monday					        			  										</HeaderTemplate>
																							<ItemTemplate >
																								<asp:CheckBox ID="chkMondayV" Runat="server"   autopostback=true oncheckedchanged="dosavepostdays"  />
																							</ItemTemplate>
																					</asp:TemplateColumn>
																					<asp:TemplateColumn HeaderText="Tuesday" ItemStyle-Width="20px" visible=false>
								        			  										<HeaderTemplate  >
								        			  											Tuesday					        			  										</HeaderTemplate>
																							<ItemTemplate >
																								<asp:CheckBox ID="chkTuesdayV" Runat="server" autopostback=true oncheckedchanged="dosavepostdays"   />
																							</ItemTemplate>
																					</asp:TemplateColumn>
													        			    		<asp:TemplateColumn HeaderText="Wednesday" ItemStyle-Width="20px" visible=false>
								        			  										<HeaderTemplate  >
								        			  											Wednesday					        			  										</HeaderTemplate>
																							<ItemTemplate >
																								<asp:CheckBox ID="chkWednesdayV" Runat="server" autopostback=true oncheckedchanged="dosavepostdays"  />
																							</ItemTemplate>
																					</asp:TemplateColumn>
													        			    		<asp:TemplateColumn HeaderText="Thursday" ItemStyle-Width="20px" visible=false>
								        			  										<HeaderTemplate  >
								        			  											Thursday					        			  										</HeaderTemplate>
																							<ItemTemplate >
																								<asp:CheckBox ID="chkThursdayV" Runat="server" autopostback=true oncheckedchanged="dosavepostdays"  />
																							</ItemTemplate>
																					</asp:TemplateColumn>
													        			    		<asp:TemplateColumn HeaderText="FridayV" ItemStyle-Width="20px" visible=false>
								        			  										<HeaderTemplate  >
								        			  											Friday					        			  										</HeaderTemplate>
																							<ItemTemplate >
																								<asp:CheckBox ID="chkFridayV" Runat="server"   autopostback=true oncheckedchanged="dosavepostdays" />
																							</ItemTemplate>
																					</asp:TemplateColumn>
													        			    		<asp:TemplateColumn HeaderText="SaturdayV" ItemStyle-Width="20px" visible=false>
								        			  										<HeaderTemplate  >
								        			  											Saturday					        			  										</HeaderTemplate>
																							<ItemTemplate >
																								<asp:CheckBox ID="chkSaturday" Runat="server" autopostback=true oncheckedchanged="dosavepostdays"  />
																							</ItemTemplate>
																					</asp:TemplateColumn>
																					<asp:TemplateColumn HeaderText="" visible=true ItemStyle-Width="20px" ItemStyle-CssClass="dgitemsNOBD" >
																		            <ItemTemplate >
																		                <table width=100%>
																		                    <tr>
																		                        <td><asp:button id="btnvwsched" runat=server text="Show Schedule"  visible=true onclick="ShowVschd" CausesValidation="false" cssclass=frmbuttonsXLG /></td>		
																		      					</tr>    
																		                </table>   
																		            </ItemTemplate>                                                     
																	           	 </asp:TemplateColumn>
																	           	 <asp:TemplateColumn HeaderText="" visible=true ItemStyle-Width="20px" ItemStyle-CssClass="dgitemsNOBD" >
																		            <ItemTemplate >
																		                <table width=100%>
																		                    <tr>
																		                        <td><asp:button id="btnstatchg" runat=server text="Change Status"  visible=true onclick="changevstat" CausesValidation="false" cssclass=frmbuttonsXLG /></td>		
																		      					</tr>    
																		                </table>   
																		            </ItemTemplate>                                                     
																	           	 </asp:TemplateColumn>
																	           	 <asp:TemplateColumn HeaderText="" visible=true ItemStyle-Width="20px" ItemStyle-CssClass="dgitemsNOBD" >
																		            <ItemTemplate >
																		                <table width=100%>
																		                    <tr>
																		                        <td><asp:button id="btnAddPosts" runat=server text="Add Dates"  visible=true onclick="addpdates" CausesValidation="false" cssclass=frmbuttonsXLG /></td>		
																		      					</tr>    
																		                </table>   
																		            </ItemTemplate>                                                     
																	           	 </asp:TemplateColumn>
																	           	 <asp:TemplateColumn HeaderText="" visible=true ItemStyle-Width="20px" ItemStyle-CssClass="dgitemsNOBD" >
																		            <ItemTemplate >
																		                <table width=100%>
																		                    <tr>
																		                        <td><asp:button id="btnPublish" runat=server text="Publish"  visible=true onclick="dopublish" CausesValidation="false" cssclass=frmbuttonsXLG /></td>		
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
							    			   	</td>
							    			   	<td valign=top>
														<table>
															<tr>
																<td><b><asp:label id=lblcal3 runat=server /></b></td>
																<td align=right><asp:linkbutton id="showcalc3" Text= "Close" CausesValidation="false"  runat="server"   visible=false onClick="closecalendar2" /></td>
											            </tr>
											            <tr>
				                                    <td colspan=2><asp:calendar id="cdrCalendar3" runat="server" 
				                                            enabled=false
		                                                backcolor="#ffffff" width="200px" height="200px" 
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
			    			   		</asp:panel>
			    			   			
			    			   			
			    			   			
			    			   			
			    			   			
			    			   			
			    			   			
			    			   			
		    			   					<asp:panel id=pnlentposts runat=server visible=false>
						                    	<table width=75% border=0 style="margin-top:5;">
							                    	<tr>
							                    		<td width=120><b>Entered Posts</b></td>
							                    		<td>Filter</td>
							                    		<td><asp:DropDownList ID="dd_Pubfilter" runat=server Visible=true autopostback=true  OnSelectedIndexChanged="filterVenues" >
			                              			  	<asp:ListItem Value="Published" Text="Published"/>
					                	            		<asp:ListItem Value="Unpublished" Text="Unpublished"/>
					                            			<asp:ListItem Value="All" Text="All"/>
					                            			</asp:DropDownList></td>  
							                    		
							                    		<td align=right>Search</td>
							                    		<td width=25%><asp:textbox id="ppvensearch" runat=server size=25 ontextchanged="btnsearch" autopostback="true" />
							                    		<td colspan=8><asp:button id="btnNewPPlanPost" runat=server text="New Post" onclick="NewPPlanPost" CausesValidation="false" cssclass=frmbuttons /></td>		
			    			   							<td ><asp:button id="btnrerunpost" runat=server text="Rerun Post" onclick="rerunpost" CausesValidation="false" cssclass=frmbuttons /></td>		
							    			           	<td ><asp:button id="btndelpost" runat=server text="Remove Post" onclick="deleteposts" CausesValidation="false" cssclass=frmbuttons /></td>		
							    			         	<td width=10%></td>
							                    	</tr>
								               </table>
								               <table width=100%>   	
				         							<tr>
										            	<td >
										               <div id="ppgrid" runat=server style="vertical-align top; height: 300px; overflow:auto;">
													 			<asp:DataGrid Runat=server visible=true
															   		ID="ADVenuesPP" 
											                    	AutoGenerateColumns=False
											                    	Width="100%"          
															    		AllowPaging="false"            
														            PageSize="8" 
														            PagerStyle-Mode="NumericPages" 
																		OnPageIndexChanged="MyDataGrid_PagePP" 
																		DataKeyField="tbl_leadadvenues"
																		
												               	OnItemDataBound="ItemDataBoundEventHandlerPP"
												                   
															    		CssClass="dg">
										    	            		<HeaderStyle CssClass="dgheaders" />
													        			<ItemStyle CssClass="dgitems" />
													        			<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>
														       		<Columns >
															       		<asp:TemplateColumn HeaderText="<font color=#FFF8C6><b>Select</b></font>" ItemStyle-Width="20px">
					        			  										<HeaderTemplate  >
					        			  											<asp:linkbutton id="clearcks" Text= "All" runat="server" cssclass=linkbuttons visible =true  />
					        			  										</HeaderTemplate>
																				<ItemTemplate >
																					<asp:CheckBox ID="myCheckbox" Runat="server" AutoPostBack=True OnCheckedChanged="GetSelections_Click2" />
																				</ItemTemplate>
																			</asp:TemplateColumn>
											           		   		<asp:BoundColumn HeaderText="ID" visible=true DataField="venno" ItemStyle-Width="40px" readonly=true   />
											           		    		<asp:BoundColumn HeaderText="Venue"  DataField="av_name" ItemStyle-Width="160px" readonly=true    />    
											        						<asp:BoundColumn HeaderText="Can Self Publish" visible=false DataField="x_canselfpub" ItemStyle-Width="140px" readonly=true  />
											                        <asp:BoundColumn HeaderText="Ad Code" visible=true DataField="av_key" ItemStyle-Width="60px" readonly=true    />
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
										        							
										        							
										        							<asp:TemplateColumn HeaderText="" ItemStyle-Width="20px"  ItemStyle-CssClass="dgitemsNOBD" visible=true>
													            			<ItemTemplate >
													                			<table width=100%>
													                   			 <tr>
													                         			<td><asp:button id="BTNSetPub" runat=server text="Set Published" onclick="SetVPub"  CausesValidation="false" cssclass=frmbuttonsLG /></td>		
											         								 </tr>    
													                			</table>   
													            			</ItemTemplate>                                                     
												            			</asp:TemplateColumn>
												                		<asp:TemplateColumn HeaderText="" ItemStyle-Width="20px"  ItemStyle-CssClass="dgitemsNOBD" visible=true>
													            			<ItemTemplate >
													                			<table cellpadding=0 cellspacing=0>
													                   			 <tr>
													                         			<td><asp:button id="PublishPP" runat=server text="Self Publish" onclick="pubad" CausesValidation="false" cssclass=frmbuttonslg /></td>		
											         										<td><asp:button id="UpdatePN" runat=server text="Edit Posting No" onclick="EditPNO"   CausesValidation="false" cssclass=frmbuttonsLG /></td>		
											         								
											         								 </tr>    
													                			</table>   
													            			</ItemTemplate>                                                     
												            			</asp:TemplateColumn>
												            			<asp:BoundColumn HeaderText="<font color=#FFF8C6><b></b></font>" visible=false DataField="av_leadads_FK" ItemStyle-Width="80px"    />
							        										<asp:TemplateColumn HeaderText="" ItemStyle-Width="20px"  ItemStyle-CssClass="dgitemsNOBD" visible=true>
													            			<ItemTemplate >
													                			<table width=100%>
													                   			 <tr>
													                         			<td><asp:button id="btnTD" runat=server text="Edit Target Date" onclick="ModTGD"  CausesValidation="false" cssclass=frmbuttonsLG /></td>		
											         								 </tr>    
													                			</table>   
													            			</ItemTemplate>                                                     
												            			</asp:TemplateColumn>
												            			<asp:TemplateColumn HeaderText="" ItemStyle-Width="20px"  ItemStyle-CssClass="dgitemsNOBD" visible=false>
													            			<ItemTemplate >
													                			<table width=100%>
													                   			 <tr>
													                         			<td><asp:button id="btnSTD" runat=server text="Save" onclick="ModTGDS"  CausesValidation="false" cssclass=frmbuttonsLG /></td>		
											         								 </tr>    
													                			</table>   
													            			</ItemTemplate>                                                     
												            			</asp:TemplateColumn>
												            			<asp:TemplateColumn HeaderText="" ItemStyle-Width="20px"  ItemStyle-CssClass="dgitemsNOBD" visible=false>
													            			<ItemTemplate >
													                			<table width=100%>
													                   			 <tr>
													                         			<td><asp:button id="btnSTDC" runat=server text="Cancel" onclick="ModTGDSC"  CausesValidation="false" cssclass=frmbuttonsLG /></td>		
											         								 </tr>    
													                			</table>   
													            			</ItemTemplate>                                                     
												            			</asp:TemplateColumn>
												            			<asp:BoundColumn HeaderText="Status" visible=false DataField="av_Postingno" ItemStyle-Width="60px" readonly=true  />
											        			  			<asp:BoundColumn HeaderText="Status" visible=false DataField="APFrom" ItemStyle-Width="60px" readonly=true  />
											        			  		
											        			  		</Columns>
														      </asp:DataGrid>
														  	</div>
												    		</td>
												    	</tr>
												 	</table>  
												</asp:panel>
		    			   				
			       				   </asp:panel> 
			       				   
						       	<asp:panel id="pnladdvenueN" runat=server visible=false>
							       	<table>
	                            	<tr>
	                              	<td class=wfstepheaderA>Add New Posting</td>
	                      				<td><asp:linkbutton visible=false id="apsdetail" Text= "Show Details" runat="server" cssclass=linkbuttonsRed onClick="showvendetails" /></td>
	    		    
	                            	</tr>                        
	           	            	</table>
								       	<table width=40% runat=server id="table1" style="margin-top:10">                                                 
		                           	<tr>
		                                <td width=120><b>Select Lead Source</b></td>
		                              </tr>
		                              <tr>
		                                <td width=25%><asp:DropDownList ID="advenue" DataTextField="x_descr"
		                                                autopostback=true OnSelectedIndexChanged="postadVen"
		                  		                        DataValueField="tbl_xwalk_pk" 
		                  		                        Runat="server" />
				                        	</td>  
				                          	<td width=10><asp:button id="postadV" runat=server text="Not Listed" onclick="addnewvenue" CausesValidation="false" cssclass=frmbuttons /></td>		
					                     	<td width=60%> <asp:button visible=true id="exitV" runat=server text="Exit" onclick="ExitADV" CausesValidation="false" cssclass=frmbuttons /></td>		
		            			      		<td id="cell1" visible=false>Auto Responder</td>
		                                <td visible=false>Photo</td>  
			                           </tr>
			                           <tr>
		                              	<td id="cell1a"><asp:DropDownList ID="adautor" runat=server Visible=false>
		                                  <asp:ListItem Value="No" Text="No"/>
				                            <asp:ListItem Value="Yes" Text="Yes"/>
				                            </asp:DropDownList>
				                        	</td>    
				                         	<td><asp:DropDownList ID="adphoto" runat=server Visible=false>
		                              	
				                            <asp:ListItem Value="No" Text="No"/>
				                	            <asp:ListItem Value="Yes" Text="Yes"/>
				                            </asp:DropDownList>
				                        	</td>  
		            		         	</tr>
				                    	</table>
			                    	
			       				    <asp:panel id="pnlLSdetail" runat=server visible=false >
				                    	<table><tr><td><table width=65% style="margin-top:10">
				                    		<tr>
				                    			<td><b>Selected Venue</b></td>
				                    			<td><b>Status</b></td>
				                    			<td><b>AD Key</b></td>
				                    			<td><asp:linkbutton id="lnkEPstDate" Text= "Expected Post Date" CausesValidation="false"  runat="server" cssclass=linkbuttonsRed  visible =true onClick="showcalendar2" /></td>
				                    			<td><asp:linkbutton id="lnkEPubDate" Text= "Published Date" CausesValidation="false"  runat="server" cssclass=linkbuttonsRed  visible =true onClick="showcalendar2" /></td>
				                    			<td><b>Posting No</b></td>
				                    		</tr>
				                    		<tr>
				                    			<td><asp:label id=pstsvenue runat=server /></td>
				                    			<td><asp:label id=pststatus runat=server /></td>
				                    			<td><asp:label id=pstadkey runat=server /></td>
				                    			<td><asp:textbox id="pstEPdate" runat=server /></td>
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
									</td></tr></table>
				                    <table>
				                    		<tr>
				                    			<td><asp:button id="BTNSetPub" runat=server text="Set as Published" onclick="MarkPublished" CausesValidation="false" cssclass=frmbuttonsxlg /></td>		
					                     	<td><asp:button id="BTNPubNow" visible=false runat=server text="Publish Now" onclick="PublishNow" CausesValidation="false" cssclass=frmbuttonsxlg /></td>		
		            			          	<td><asp:button id="BTNPublater" runat=server text="Save & Exit" onclick="PublishLAter" CausesValidation="false" cssclass=frmbuttonsxlg /></td>		
		            			        		<td><asp:button id="BTNCancel" runat=server text="Cancel" onclick="ExitADV" CausesValidation="false" cssclass=frmbuttonsxlg /></td>		
		            			      	</tr>
		            			      </table>
				                 	 </asp:panel>	
						       	</asp:panel>
			       				    
						      
						       	<asp:panel id=pnlgivepostno runat=server visible=false>
						       		<table>
						       			<tr>
						       				<td>Enter Posting/AD Number from this Venue?</td>
						       				<td><asp:textbox id="pstpno" runat=server /></td>
						       			</tr>
						       			<tr>
						       				<td>Update Posting From and To Dates Change?</td>
						       				<td>
						       					<table>
						       						<tr>
						       							<td>From:</td>
						       							<td><asp:textbox id="pstNDfrom" runat=server /></td>
						       							<td>To:</td>
						       							<td><asp:textbox id="pstNDto" runat=server /></td>
						       						</tr>
						       					</table>
						       				</td>
						       			</tr>
						       		</table>
						       		<table>
						       			<tr>						       			
				       					   <td colspan=8><asp:button id="btnSavePSTno" runat=server text="Save" onclick="SavePSTNO" CausesValidation="false" cssclass=frmbuttons /></td>		
		    			                	<td colspan=8><asp:button id="btnskipPSTNO" runat=server text="Skip" onclick="SkipPSTNO" CausesValidation="false" cssclass=frmbuttons /></td>		
		    			   				</tr>
		    			   			</table>
						       	
						       	
						       	</asp:panel>
						       	<asp:DataGrid Runat=server visible=false
												   		ID="ADVenues" 
								                    	AutoGenerateColumns=False
								                    	Width="100%"          
												    		PagerStyle-Visible = "False"	
												    		OnItemDataBound="ItemDataBoundEventHandler" CssClass="dg">
												    		
										        
									            </asp:DataGrid>
						       	
						       	
				                    
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
 			       				    <div id="Div1" style="vertical-align: top; height: 460px; " >
												
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
									                    	OnItemDataBound="ItemDataBoundEventHandlerIMG"
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
									    		               <asp:BoundColumn HeaderText="Image URL"  DataField="imgurl" visible="false" ItemStyle-Width="250px"    />
									    		               <asp:BoundColumn HeaderText="Image HTML"  DataField="imghtml" visible="false" ItemStyle-Width="250px"    />
									    		              
									    		               <asp:BoundColumn HeaderText="File Name" ReadOnly=true  DataField="ui_filename" visible="true" ItemStyle-Width="80px"    />
									    		              
									    		               
													        </Columns>
												        </asp:DataGrid>
										            </td>
									            </tr>
									        </table> 
									        </div>    
 			       				    
 			       				    
 			       				    </div>
 			      			 </asp:panel>
 			      			  <asp:panel id="pnlpage6" runat=server visible=false >
 			      			 <div style="margin:0;background: #4682B4;"></div>
 			       				    <div id="fieldtitles" style="vertical-align: top; height: 460px;margin:5; ">
 			       				    <table width=20% border=0>
 			       				    		<tr>
 			       				    		   <td align=center ><asp:linkbutton id="btnSpg1" Text="Summary"  runat="server" cssclass=linkbuttons onClick="btn_Sbpg1"  /></td> 
 					                        <td>|</td>
 					                        <td align=left width=15%><asp:linkbutton id="btnSpg2" Text="Detail"  runat="server" cssclass=linkbuttons onClick="btn_Sbpg2" /></td> 
 					            	        	<td width=35%></td>
 			       				    		</tr>
 			       				    	</table>	
 			       				    	
 			       				    		<asp:panel id=pnlSbrdpg1 runat=server visible=true >
 			       				    		<table style="margin-top:10;">
 			       				    		<tr>
 			       				    			<td class=wfstepheaderA >AD Plan Summary level</td>
 			       				    		</tr>
 			       				    	</table>
 			       				    	<asp:DataGrid 
													        ID="ProgramPlansS" 
												        		AutoGenerateColumns=False
												        		Width="100%"
									                    	ColumnHeadersVisible = FALSE  
												            AllowPaging="false" 									                    
									                   
									                     Runat=server CssClass="dgW">
												        			<HeaderStyle CssClass="dgheadersW" />
												        			<ItemStyle CssClass="dgitemsW" />
												        			<AlternatingItemStyle CssClass="dgAltitemsW"></AlternatingItemStyle>	
											                <Columns >
									    		              
															      
									    		               <asp:BoundColumn HeaderText="AD Plan"  DataField="lap_name" visible="true"  ItemStyle-Width="200px"    />
									    		               <asp:BoundColumn HeaderText="Exp. Pubs."   DataField="lap_totpostsA" visible="true" ItemStyle-Width="80px"    />
									    		               <asp:BoundColumn HeaderText="Actual Pubs."  DataField="pcount" visible="true" ItemStyle-Width="80px"    />
									    		               <asp:BoundColumn HeaderText="Pub. Performance"  DataField="ppercent"  visible="true" ItemStyle-Width="80px"    />
									    		               <asp:BoundColumn HeaderText="Expected Leads"  DataField="ELC"  visible="true" ItemStyle-Width="100px"    />
									    		               <asp:BoundColumn HeaderText="Actual Leads"  DataField="lcount" visible="true" ItemStyle-Width="100px"    />
									    		              
									    		               <asp:BoundColumn HeaderText="Lead Performance" ReadOnly=true  DataField="lpercent" visible="true" ItemStyle-Width="140px"    />
									    		              
									    		               
													        </Columns>
												        </asp:DataGrid>
 			       				    

 			       				    	</asp:panel>
 			       				    	<asp:panel id=pnlSbrdpg2 runat=server visible=false >
 			       				    	<table style="margin-top:0;">
 			       				    		<tr>
 			       				    			<td class=wfstepheaderA >AD Plan Detail level</td>
 			       				    		</tr>
 			       				    	</table>
 			       				    	
 			       				    	<table style="position: absolute; left: 140px; top: 190px; width:99%; "><tr><td valign=top>
 			       				    		 <div style="vertical-align top; height: 400px; overflow:auto;">
 			       				    		 <asp:DataGrid 
													        ID="ProgramPlans" 
												        		AutoGenerateColumns=False
												        		Width="85%"
									                    	ColumnHeadersVisible = FALSE  
												            AllowPaging="false" 									                    
									                   	OnItemDataBound="dtgOrders_OnItemDataBound"
									                     Runat=server CssClass="dgW">
												        			<HeaderStyle CssClass="dgheadersWw" />
												        			<ItemStyle CssClass="dgitemsW" />
												        			<AlternatingItemStyle CssClass="dgAltitemsW"></AlternatingItemStyle>	
											                <Columns >
											                  <asp:BoundColumn HeaderText="%" ReadOnly=true  DataField="LAP_tbl_pk" visible="false" ItemStyle-Width="80px"    />
									    		            
											                <asp:TemplateColumn ItemStyle-VerticalAlign="Top" >
       																	<ItemTemplate>   																	 
                   
       																	<table width="100%" border =0 cellpadding="2" cellspacing="1" style="margin-top:15;margin-bottom:2;">
																          <tr class="dgheadersW">
																             <th align="left">AD Plan</th>
																             <th align="left">Exp. Pubs.</th>
																             <th align="left">Actual Pubs.</th>
																             <th align="left">Pub. Performance</th>
																             <th align="left">Expected Leads</th>
																             <th align="left">Actual Leads</th>
																             <th align="left">Lead Performance</th>
																           </tr>


																           <tr >
																             <td align="left" valign="top">
																               <%# DataBinder.Eval(Container.DataItem, "lap_name") %> 
																             </td>
																             <td align="left" valign="top">
																               <%# DataBinder.Eval(Container.DataItem, "lap_totpostsA") %>
																             </td>
																             <td align="left" valign="top">
																               <%# DataBinder.Eval(Container.DataItem, "pcount") %>
																             </td>
																             <td align="left" valign="top">
																               <%# DataBinder.Eval(Container.DataItem, "ppercent" ) %>
																             </td>
																             <td align="left" valign="top">
																               <%# DataBinder.Eval(Container.DataItem, "ELC") %>
																             </td>
																             <td align="left" valign="top">
																               <%# DataBinder.Eval(Container.DataItem, "lcount") %>
																             </td>
																             <td align="left" valign="top">
																               <%# DataBinder.Eval(Container.DataItem, "lpercent") %>
																             </td>
																           </tr>
																         </table>
																         
 																		</ItemTemplate>
     																</asp:TemplateColumn>
									    		               

													        </Columns>
												        </asp:DataGrid></div></td></tr></table>
 			       				    	</asp:panel>
 			       				    
 			       				    
 			       				    
 			       				    </div>
 			      			 </asp:panel>	
 			      				
 			      				
							 </td>
						</tr>
					</table>
				</td>
			</tr>
		</table>		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		<asp:Panel ID="pnlALL" runat=server Visible=false>
	 
            
              
            <asp:Panel ID="admain" runat="server">
            <asp:Panel ID="pnladsubmain" runat=server Visible=false >
            <table width=100% border =0 >
                <tr>
                	<td width=20% valign=top>
                		
                   </td>
                   <td valign=top width=80%>
                        <table width=100%>
                            <tr>
                                <td width=18% valign=top colspan=2>
                                    <table width=100%>
                                        <tr>
                                            <td onmouseover="document.getElementById('myToolTipA').className='activeToolTip'"
   onmouseout="document.getElementById('myToolTipA').className='idleToolTip'" ><font size=2><b><asp:label id="autoc" runat="server" /></b></font></td>
                                        </tr>
                                       
                                    </table>
                                </td>
                                     <td valign=top><asp:linkbutton id="PAP" Text= "Purchase Credits" runat="server" cssclass=linkbuttons visible =false onClick="buycredits" /></td>                     
                            </tr>
                        </table>
                    </td>
	            </tr>
	        </table>       
	        <div id="myToolTipA" class="idleToolTip">You can select to have your AD Posted by us, freeing you up to do other things.
	        To AutoPost you purchase credits.  Each Credit is equal to one posting day.</div>
	                   
	              </asp:Panel>
	                    <asp:Panel ID="pnltxtbody" runat=server Visible=true >
	                        
	                 
	                    </asp:Panel>  
	                     
	                
	        </asp:Panel>	        
               
              
	         
            <asp:Panel ID="placead" runat="server" Visible="false">
            <table width=100%>
                    <tr>    
                        <td>
                            <hr id ='hrline' runat=server visible=false />
                        </td>
                    </tr>
                </table>           
                <asp:Panel ID="pnladtitle" runat=server Visible=true>
                    <table width=60%>
                        <tr>
                            <td width=10><font size=3><b>AD#</b></font></td>
                            <td width=500><font size=3><b><asp:label id="lbl_adnoV" runat=server /></b></font></td>
                            <td><asp:button id="B_FinalV" runat=server text="Finalize AD" onclick="finalizeAD" CausesValidation="false" cssclass=frmbuttons /></td>		
				  			        <td><font color=red size=3><asp:label id="finwarning"  runat=server /></font></td> 
				 
                        </tr>
                       
                        
                        <tr height=10>
                            <td></td>
                        </tr>
                        </table>
                    </asp:Panel>
                    
                
                <asp:Panel ID="pnlldsource" runat=server Visible=true>    
                <table width=100% border=0>
              
                    <tr>
                        <td width=24% valign=top>
                           
		                     	                    
                       </td>
                       <td width=60% >
                      </td>
                      </tr>
                    </table>
                 </asp:Panel>
                    <table width=100% border=0>
              
                    <tr>      
                    <td valign=top width=95%>
                        <asp:Panel ID="pnlnoadvenues" runat="server" Visible="false">
                       
           	            </asp:panel>
           	<asp:Panel ID="pnlapfreq" runat="server" Visible="false">
           		<table>
           			<tr>
           				<td><b><u>Set Posting Schedule</u></b></td>
           			</tr>
           		</table>
           		<table width=70% class=tblbordera>
           			<tr>
           				<td valign=top>
					           		<table border=0 width=100%>
					           			<tr>
					           				<td valign=middle>Schedule 1</td>
					           				<td valign=top>
					           				    <table cellpadding=2 cellspacing=2>
					           				        <tr>
					           				            <td><asp:linkbutton id="showcal1" Text= "From" CausesValidation="false"  runat="server" cssclass=linkbuttonsRed visible =true onClick="showcalendar" /> </td>
					           				            <td><asp:linkbutton id="showcal1a" Text= "To" CausesValidation="false" runat="server" cssclass=linkbuttonsRed  visible =true onClick="showcalendar" /> </td>
					                                  </tr>
					           				         <tr>   
					           				            <td><asp:textbox id="dd1" runat=server size=6 /></td>
					           				         
					           			
					           				        <td><asp:textbox id="dd1a" runat=server size=6/></td>
					           				         </tr>
					           				    </table>
					           				</td>
					           			</tr>
					           			
					           			<tr>
					           				<td valign=middle>Schedule 2</td>
					           				<td valign=top>
					           				    <table>
					           				        <tr>
					           				            <td><asp:linkbutton id="showcal2" Text= "From" CausesValidation="false" runat="server" cssclass=linkbuttonsRed   visible =true onClick="showcalendar" /> </td>
					           				            <td><asp:linkbutton id="showcal2a" Text= "To" CausesValidation="false" runat="server" cssclass=linkbuttonsRed    visible =true onClick="showcalendar" /> </td>
					                                  </tr>
					           				         <tr>   
					           				            <td><asp:textbox id="dd2" runat=server size=6 /></td>
					           				         
					           			
					           				        <td><asp:textbox id="dd2a" runat=server size=6/></td>
					           				         </tr>
					           				    </table>
					           				</td>
					           			</tr>
					           			
					           			
					           		</table>
					           </td>
					           <td valign=top>
					           <table border=0 width=100%>
					           			<tr>
					           				<td valign=middle>Schedule 3</td>
					           				<td valign=top>
					           				    <table cellpadding=2 cellspacing=2>
					           				        <tr>
					           				            <td><asp:linkbutton id="showcal3" Text= "From" CausesValidation="false" runat="server" cssclass=linkbuttonsRed   visible =true onClick="showcalendar" /> </td>
					           				            <td><asp:linkbutton id="showcal3a" Text= "To" CausesValidation="false" runat="server" cssclass=linkbuttonsRed visible =true onClick="showcalendar" /> </td>
					                                  </tr>
					           				         <tr>   
					           				            <td><asp:textbox id="dd3" runat=server size=6 /></td>
					           				         
					           			
					           				        <td><asp:textbox id="dd3a" runat=server size=6/></td>
					           				         </tr>
					           				    </table>
					           				</td>
					           			</tr>
					           			
					           			<tr>
					           				<td valign=middle>Schedule 4</td>
					           				<td valign=top>
					           				    <table>
					           				        <tr>
					           				            <td><asp:linkbutton id="showcal4" Text= "From" CausesValidation="false"  runat="server" cssclass=linkbuttonsRed  visible =true onClick="showcalendar" /> </td>
					           				            <td><asp:linkbutton id="showcal4a" Text= "To" CausesValidation="false" runat="server" cssclass=linkbuttonsRed visible =true onClick="showcalendar" /> </td>
					                                  </tr>
					           				         <tr>   
					           				            <td><asp:textbox id="dd4" runat=server size=6 /></td>
					           				         
					           			
					           				        <td><asp:textbox id="dd4a" runat=server size=6/></td>
					           				         </tr>
					           				    </table>
					           				</td>
					           			</tr>
					           			
					           			
					           		</table>
					           </td>
					           <td>
					           		<table>
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
		                                </asp:calendar></td>
						                </tr>
						            </table>
						        </td>
					      </tr>
					 </table>
           		<table>
           		    <tr>
           		        <td colspan=2><font size=4 color=red><asp:Label ID="lblNoAPDate" runat=server Visible=false /></font></td>
           		    </tr>
           		</table>
           		<table>
           			<tr>
             			<td><asp:button id="apsavedates" onclick="saveapdates" runat=server text="Save" CausesValidation="false" cssclass=frmbuttons /></td>		
            			<td><asp:button id="apcancel" onclick="cancelap" runat=server text="Cancel" CausesValidation="false" cssclass=frmbuttons /></td>		
                    </tr>
           		</table>
           	
           	</asp:panel>
                    
            <asp:Panel ID="pnlsavedv" runat="server" Visible="false">
            <table width=30%>
                <tr>
                  
                </tr>
            </table>
            
             </asp:panel> 
                    </td>
             
                </tr>
            </table>
            </asp:Panel>   
            
            </asp:Panel>
            
            
          
                
	    </form>
	</body>
</HTML>
	