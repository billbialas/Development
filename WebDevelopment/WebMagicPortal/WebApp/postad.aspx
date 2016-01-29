<%@ Page language="vb" Codebehind="postad.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.postad" Debug="false" trace="false" validateRequest=false aspcompat=true   %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %> 
<%@ Register TagPrefix="ed" Namespace="OboutInc.Editor" Assembly="obout_Editor" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script language="JavaScript" src="../_include/default.js"></script>
<link rel="stylesheet" href="_include/default.css" type="text/css">
<script type="text/javascript">
   
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
   // toolbar status variable
var toolbarIsShown = true;

    </script>
<HTML>
	<HEAD>
		<title>www.WebMagicPortal.com</title>
	</HEAD>
	
	<body>
		<form  name="createad"  runat="server" >
			
		<div id="headerPost">
          <table border=0 width="100%" cellspacing=0 cellpadding=0>
              <tr>	
                
                <td width=90><img  height="50" width="78" alt="" src="../images/Magic2.png" border="0"></td>
                <td  valign=top> 
                    <table>
                        <tr>
                            <td valign=top><h1>WebMagicPortal.com</h1></td>
                        <td><h2>Posting AD</h2></td></tr>
                       
                   </table>
                </td> 				
              </tr>
              
            </table></div>             <asp:Panel ID="Panel1" runat=server Visible=false>
            
            <table width=60% class=tblbordera>
                <tr>
                	<td width=30></td>
                <td> <asp:Label ID="Label1a" runat=server /> </td>
                    <td><p><font color=red size=4>Attention:</font>  You have selected a Lead Source that requires special steps.
                          If you will be posting using your saved account YOU MUST login first.  To login
                    select "YES" from the following drop down box.  If you will not be using your account select "NO"
                    </p>
                    <p>When you select "YES", a login screen will pop up.  Login normally and then
                    close the popup window by clicking on the 'X' in the upper right corner.  This will close the pop-up window and you
                    will be brought back to this page.  Click "Continue" to begin the posting process..
                    <asp:Label ID="Label1" runat=server visible=false /> </p></td>
                 </tr>
                 <tr>
                 		<td>&nbsp</td>
                 </tr>
                 <tr>
                 <td width=30></td>
                 <td></td>
                    <td><font color=red size=4>Login:</font><asp:DropDownList id="dd_acctyesno" runat="server" AutoPostBack=true OnSelectedIndexChanged="accountset">    							               
		                  <asp:ListItem Value="Select" Text="Select"/>
		                  <asp:ListItem Value="No" Text="No"/>
		                 <asp:ListItem Value="Yes" Text="Yes"/>
		                </asp:DropDownList></td>
                </tr>
                <tr>
                    <td colspan=3 align=center><asp:button id="buttoncnt" runat="server" text="Continue" class=frmbuttons OnClick="logincl" /></td></tr>
                
            </table>
          
          </asp:Panel>
          <asp:Panel ID="pnlpostad" runat=server Visible=false>
          
           <table width=100% border=0>
           
            <tr>
               
                
                    <td id="ourside" width=30% valign=top>
                     <asp:Panel ID="pnlourside" runat=server >
                        <div id="fieldtitles" style="vertical-align: top; height: 480px;margin:5; ">
                    			<asp:panel id=pnlstartpost runat=server visible=false>
	                          <table>
	                          	<tr>
	                          		<td class=wfstepheaderA>Begin Publishing Process</td>
	                          	</tr>
	                          </table>
	                          <table class=tblborder>	
	                          	<tr>
	                          		<td style="font-size:90%;">Your AD information is presented here for easy access when placing you ad!  When you click 'Start Posting' 
	                          		a new window will appear with the website of the venue you selected.</td>
	                          	</tr>
	                          	<tr>
	                          		<td style="font-size:90%;">Once you have posted your AD, close the popup window and come back here to complete the posting process!</td>
	                          	</tr>
	                          </table>
	                          <table>
	                          	<tr>
	                          		 	<td align=left><asp:button id="bstartpost" runat=server text="Begin Publishing" onclick="StartPost" CausesValidation="false" cssclass=frmbuttonslg /></td>		
	                             		<td align=left><asp:button id="bcancelpost" runat=server text="Cancel Publishing" onclick="CancelPost" CausesValidation="false" cssclass=frmbuttonslg /></td>		
	                   
	                             </tr>
	                          </table>
                         </asp:panel>
                         <asp:panel id=pnlendpostprocess runat=server visible=false>
                          <table width=100%>
                          		<tr>
                          			<td  class=wfstepheaderA>Complete Posting Process</td>
                          		</tr>
                          	</table>
                          	<table class=tblborder>	
	                          	<tr>
	                          		<td colspan=2>Please Choose a completion option.  If you have a Posting Number please enter it.</td>
	                          	  
                            </tr>
                       
                            
                            <tr> 
                            		<td><font color=red>Posting/AD Number</font></td>
                           
                            		<td align=left><asp:textbox id="pstno" runat=server size=12 /></td>
                             </tr>
                           </table>
                           <table>
                           	<tr>
                                	<td align=left><asp:button id="finishexit" runat=server text="AD Published" onclick="finexit" CausesValidation="false" cssclass=frmbuttonslg /></td>		
                                
                                	<td align=left><asp:button id="finishexitAB" runat=server text="Publish Later" onclick="deferad" CausesValidation="false" cssclass=frmbuttonslg /></td>		
                                	<td align=left><asp:button id="finishexitA" runat=server text="Complete W\Changes" onclick="finexitCHG" visible=false CausesValidation="false" cssclass=frmbuttonsxlg /></td>		
                              	<td align=left><asp:button id="bcad" visible=false runat=server text="Revise AD" onclick="cad" CausesValidation="false" cssclass=frmbuttonslg /></td>		
                           		<td align=left><asp:button id="bsad" visible=false runat=server text="Save Changes" onclick="sad" CausesValidation="false" cssclass=frmbuttonslg /></td>		
                           		<td align=left><asp:button id="bsadCan" visible=false runat=server text="Cancel" onclick="sadCAN" CausesValidation="false" cssclass=frmbuttonslg /></td>		
                           	
                           		<td align=left><asp:button visible=false id="deferadA" runat=server text="Exit" onclick="deferad" CausesValidation="false" cssclass=frmbuttonslg /></td>		
                             		
                           </tr>
                                    </table>
                         
                         </asp:panel>
                        <div  style="vertical-align: top; height: 400px;margin:0;width: 100%;border: 0px solid #000000; ">
	                        <table width=100%>
	                        	<tr>
	                        		<td width=	100% valign=top>
	                        			<table>
	                        				<tr>
	                        					<td class=wfstepheaderA>Venue Details-</td>
	                        					<td><font color=red><asp:label id=lblvname runat=server /></font></td>
	                        				</tr>
	                        			</table>
	                        			<asp:panel id=pnlUID runat=server visible=false>
	                        				<table  width=100%>
	                        					<tr>
	                        						<td colspan=2 style="font-weight:bold;color:#ffffff;font-size:100%;text-decoration: none;margin-left:2;background: #006295;height:18px;">User ID/Password</td>
	                        					</tr>
	                        					<tr>
	                        						<td>User Id:</td>
	                        						<td><asp:Label ID="uid" runat=server /></td>
	                        					</tr>
	                        					<tr>
	                        					<td>Password:</td>
	                        					<td><asp:Label ID="password" runat=server /></td>
	                        					</tr>
	                        				</table>
	                        			</asp:panel>
	                        			<table width=100% >
	                        				<tr>
	                        					<td style="font-weight:bold;color:#ffffff;font-size:100%;text-decoration: none;margin-left:2;background: #006295;height:18px;">Special Instructions</td>
	                        				</tr>
	                        			</table>
	                        			 <div  runat=server style="vertical-align top; height: 90px; overflow:auto;">
	                        			<table style="margin-bottom:5;" >
	                        				<tr>	
	                        				
	                        					<td><asp:label id=lblvinst runat=server  /></td>
	                        			
	                        				</tr>
	                        			
	                        			</table>	</div>
	                        			<table  width=100% style="margin-bottom:2;">
	                        				<tr>
	                        					<td style="font-weight:bold;color:#ffffff;font-size:100%;text-decoration: none;margin-left:2;background: #006295;height:18px;">Special Notes</td>
	                        				</tr>
	                        			</table>
	                        			 <div  runat=server style="vertical-align top; height: 90px; overflow:auto;">
	                        			<table style="margin-bottom:5;">
	                        				<tr>	
	                        					<td><asp:label id=lblvnotes runat=server /></td>
	                        				</tr>
	                        			
	                        			</table></div>
	                        			<table width=100% cellspacing=0 cellpadding=0>
	                        				<tr>
	                        					<td style="font-weight:bold;color:#ffffff;font-size:100%;text-decoration: none;margin-left:2;background: #006295;height:18px;">WebMagic URL Key</td>
	                        					<td align=right style="font-weight:bold;color:#ffffff;font-size:100%;text-decoration: none;margin-left:2;background: #006295;height:18px;"><asp:linkbutton autopostback=false id="btn_ADwebKey" OnClientClick="copy2(document.getElementById('lblwebkey'));return false;" Text= "Copy to Clip Board" runat="server" cssclass=linkbuttonsWhite /> &nbsp </td>
	                        				</tr>
	                        				<tr>	
	                        					<td colspan=2><asp:label id=lblwebkey runat=server /></td>
	                        				</tr>
	                        			
	                        			</table>
				                     </td>
				                    
				                     
				                	</tr>
				            	</table>
				            </div>
              			</div>
                  </asp:Panel>
              	</td>
                 
                  
                  <td valign=top width=70%> 
                  <table width=99%>
	                        				<tr>
	                        					<td class=wfstepheaderA>AD # <asp:label id=lbladno runat=server /> &nbsp Details</td>
	                        				</tr>
	                        			</table>
                   <asp:Panel ID="tside" runat=server Visible=true class=tblborderPost width=99% height=520> 
                   
				                     	<table border=0 width=100% style="margin-bottom:1" cellspacing=0 cellpadding=0>
	                            			<tr>
	                            				<td  style="font-weight:bold;color:#ffffff;font-size:115%;text-decoration: none;margin-left:10;background: #006295;height:18px;" >&nbsp Title</td>
	                            				 <td align=right style="font-weight:bold;color:#ffffff;font-size:110%;text-decoration: none;background: #006295;" width=80%><asp:linkbutton autopostback=false id="btn_ADTITLE" OnClientClick="copy2(document.getElementById('AdTitle'));return false;" Text= "Copy to Clip Board" runat="server" cssclass=linkbuttonsWhite /> &nbsp</td>
	                             
	                            			</tr>
	                            		</table>
	                            		<table style="margin-bottom:7;">
	                            			<tr>
	                                			<td colspan=2 ><asp:Label ID="AdTitle" runat=server Visible=true style="font-size:110%;" /></td>
	                                   	</tr>
	                            		</table>
	                            		<asp:label id=lblcview runat=server visible=false />
	                        			<asp:panel id=pnladtextmain runat=server visible=true>
		                        			<Table width=100% cellspacing=0 cellpadding=0  style="margin-bottom:1;">
		                        				<tr>
		                        					<td width=60% style="font-weight:bold;color:#ffffff;font-size:115%;text-decoration: none;background: #006295;" >&nbsp Text</td>
		                        					<td style="font-weight:bold;color:#ffffff;text-decoration: none;background: #006295;">Select View:&nbsp<asp:DropDownList ID="dd_View" cssclass=leaddrops runat=server Visible=true autopostback=true  OnSelectedIndexChanged="Showhtml" >
			                              			  	<asp:ListItem Value="Preview" Text="Preview"/>
					                	            		<asp:ListItem Value="Plain Text" Text="Plain Text"/>
					                            			<asp:ListItem Value="HTML" Text="HTML"/>
					                            			</asp:DropDownList></td> 
		                        					<td align=right style="font-weight:bold;color:#ffffff;text-decoration: none;background: #006295;"><asp:linkbutton id="btn_ADTEXT" OnClientClick="copy2(document.getElementById('lbltest1'));return false;" CausesValidation="false" cssclass=linkbuttonsWhite Text= "Copy to Clip Board" runat="server"   /> &nbsp</td>
		                        					<td align=right style="font-weight:bold;color:#ffffff;text-decoration: none;background: #006295;"><asp:linkbutton id="btn_ADTEXTA" OnClientClick="copy2(document.getElementById('txtareaB'));return false;" CausesValidation="false" Text= "Copy to Clip Board" runat="server" cssclass=linkbuttonsWhite  /> &nbsp</td>
		                        					<asp:panel id=pnlspacer runat=server visible=false >
		                        						<td width=0 style="font-weight:bold;color:#ffffff;text-decoration: none;background: #006295;">&nbsp</td>
		                        					</asp:panel>
		                        				</tr>
		                        			</table>
		                        		</asp:panel>
	                        			<asp:panel id=pnlpreview runat=server visible=true >
	                        				<table>
	                        					<tr>
				                             		<td  style="font-size:14px;"><asp:label id=lbltest1 runat=server Visible=true style="width:780px;"/></td>
				                           	</tr>
				                           </table>
	                        			</asp:panel>
	                        			<asp:panel id=pnlplaintext runat=server visible=false >
	                        				<table>
	                        					<tr>
				                             		<td > <textarea rows="25" cols="100" id="txtareaB" runat=server></textarea>

				                             		<asp:label id=lbltest2 runat=server Visible=false style="width:780px;"/></td>
				                             </tr>
				                            </table>
	                        			</asp:panel>
	                        			<asp:panel id=pnlhtml runat=server visible=false >
	                        				<table>
	                        					<tr>
				                             		<td > <asp:textbox id="txthtml" visible=true runat="server"  columns=100 rows=25 textmode="multiline" /></td>
				                             </tr>
				                            </table>
	                        			</asp:panel>
	                        			<asp:panel id=pnledit runat=server visible=false >
	                        				<table>
	                        					<tr>
				                                  <td ><ed:Editor show="false" ShowQuickFormat="false" FixedToolbar="false"  AutoFocus="false" height="380" width="850" submit="false" PathPrefix="Editor_data/" FlashBrowse="myFlashBrowse.aspx" MediaBrowse="myMediaBrowse.aspx" ImageBrowse="Editor_data/myImageBrowseClient.aspx" UrlBrowse="myUrlBrowse.aspx" id="adtext" PreviewMode="false" runat="server" /></td>
				                            </tr>
				                            </table>
				                            <table>
	                        					<tr>
				                                  <td ><ed:Editor show="false" ShowQuickFormat="false" FixedToolbar="false"  AutoFocus="false" height="5" width="5" submit="false" PathPrefix="Editor_data/" FlashBrowse="myFlashBrowse.aspx" MediaBrowse="myMediaBrowse.aspx" ImageBrowse="Editor_data/myImageBrowseClient.aspx" UrlBrowse="myUrlBrowse.aspx" id="adtext2" PreviewMode="false" runat="server" /></td>
				                            </tr>
				                            </table>
	                        			</asp:panel>
	                        			
                   </asp:Panel>
                  </td>
                 
               </tr>
        </table>
        
        
        <table>
                            <tr>
                                <td valign=top><asp:Label ID="adcode" runat=server Visible=false /></td>
                                 <td valign=top> </td>
                                 <td> <input id="Button1" runat=server type="button" value="Copy To ClipBoard"  visible=false  Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:110px; cursor:hand"   /></td>
	            
                            </tr>
                            <tr>
	                                			<td></td>
	                                			<td> <input id="Button2" runat=server type="button" value="Copy To ClipBoard"  visible=false
	                                  					Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:110px; cursor:hand"   /></td>
	                         
	                            			</tr>
	                            			<tr>  
	                                			<td colspan=2>
	                                				<asp:textbox Font-Size="xx-small" ToolTip="This is your AD URL.  Select All, Right Click and copy into the clipboard.  Then paste into the selected field" ReadOnly=true ID="adresponseurl" size=55  runat=server visible=false/></td>
	                          				</tr>
	                          				<tr>
				                               <td ></td>
				                                <td><input id="Button3" runat=server type="button" value="Copy To ClipBoard"  visible=false  Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:110px; cursor:hand"   /></td>
				                              <td><asp:linkbutton id="btn_chgad" visible=false OnClick="changead" CausesValidation="false" Text= "Change" runat="server"  Font-Bold="True" Font-underline="True" Style="color:#ff0000; font-family:arial; font-size:9pt; cursor:hand"  /></td>
				                            	<td><asp:linkbutton visible=false id="btnShowhtml" text="Show HTML" OnClick="Showhtml" runat=server /></td>
				                            	
				                            </tr>
				                             <tr>
				                                <td colspan=6><div id="myToolTipc" class="idleToolTip"> Select Text, Copy to Clip Board, Paste in desired field</div></td>
				                            </tr>
                        </table>
         </asp:Panel>
	    </form>
	    		</body>
</HTML>
	