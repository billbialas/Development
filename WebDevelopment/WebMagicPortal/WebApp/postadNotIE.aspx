<%@ Page language="vb" Codebehind="postad.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.postad" Debug="false" trace="false" validateRequest=false aspcompat=true   %>
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
    </script>
<style type="text/css">
   div.idleToolTip {
     
      display: none;
      visibility:hidden;
   }
   div.activeToolTip {
      display: inline;
      visibility:visible;
      background-color:yellow;
      border:0px solid black;
      font-size: 12px;
   }
</style>
<HTML>
	<HEAD>
		<title>www.WebMagicPortal.com</title>
	</HEAD>
	
	<body>
		<form  name="createad"  runat="server" >
          <table border=0 width="100%" cellspacing=0 cellpadding=0>
              <tr>	
                
                <td width=90><img  height="45" width="78" alt="" src="../images/Magic-128x128.png" border="0"></td>
                <td  valign=top> 
                    <table>
                        <tr>
                            <td valign=top><font size=4><b><i>WebMagicPortal.com</i></b></font>&nbsp&nbsp<font size=2></font></td>
                       </tr>
                       <tr>
                        <td>Posting AD</td></tr>
                       
                   </table>
                </td> 				
              </tr>
              
            </table>
          <hr />
          <asp:Panel ID="Panel1" runat=server Visible=true>
               
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
                    <td colspan=3 align=center><asp:button id="buttoncnt" runat="server" text="Continue" height=25 width=200 OnClick="logincl" /></td></tr>
                
            </table>
          
          </asp:Panel>
          <asp:Panel ID="pnlpostad" runat=server Visible=false>
           <table width=100%>
           
            <tr>
                <asp:Panel ID="pnlourside" runat=server >
                
                                <td id="ourside" width=15% valign=top>
                    <table border=0 width=100% cellpadding=1 cellspacing=1>
                      
                        </table>
                        <table border=0>
                            <tr>
                                <td><b><u><font color=red>AD Title</font></u></b></td>
                            </tr>
                            <tr>
                                <td><asp:Label ID="AdTitle" runat=server Visible=true /></td>
                            </tr>
                             <tr>
                                <td><b><u><font color=red>AD URL</font></u></b></td>
                                <td> <input id="Button2" runat=server type="button" value="Copy To ClipBoard"  visible=false
                                  Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:110px; cursor:hand"   /></td>
                         
                            </tr>
                            <tr>  
                                <td colspan=2><asp:textbox Font-Size="xx-small" ToolTip="This is your AD URL.  Select All, Right Click and copy into the clipboard.  Then paste into the selected field" ReadOnly=true ID="adresponseurl" size=55  runat=server visible=true/></td>
                          
                            </tr>
                            <tr>
                               <td ><b><u><font color=red>AD Text</font></u></b><img src="../images/help_icon.jpg" alt="Help" height=15 width=15 onmouseover="document.getElementById('myToolTipc').className='activeToolTip'" onmouseout="document.getElementById('myToolTipc').className='idleToolTip'"/></td>
                                <td><input id="Button3" runat=server type="button" value="Copy To ClipBoard"  visible=false
                                  Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:110px; cursor:hand"   /></td>
                              
                            </tr>
                            <tr>
                                <td colspan=2><div id="myToolTipc" class="idleToolTip"><br /> Select Text, Copy to Clip Board, Paste in desired field</div></td>
                            </tr>
                         
                            <tr>
                                <td colspan=2><asp:textbox ToolTip="This is your AD.  Select All, Right Click and copy into the clipboard.  Then paste into the selected field" ReadOnly=true ID="adtext" size=16 TextMode="MultiLine" Columns="30"  Rows="20" runat=server visible=true/></td>
                            </tr>
                        </table>
                         
             
                        <table width=100%>
                            <tr>
                            	<td>Please Enter your Posting/AD Number when you have completed your post.</td>
                            </tr>
                            <tr>
                            	<td align=right><asp:textbox id="pstno" runat=server size=12 /></td>
                            </tr>
                       </table>
                        <table width=100%>
                            
                            <tr> 
                                <td width=90%></td>
                                <td width=50 align=right><asp:button id="finishexit" runat=server text="Completed" onclick="finexit" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:9pt;width:90px; cursor:hand" /></td>		
                                <td align=right><asp:button id="deferadA" runat=server text="Cancel" onclick="deferad" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:9pt;width:90px; cursor:hand" /></td>		
                           </tr>
                        </table>
                        </asp:Panel>
               
                </td>
                  <td> <table width=100%>
            <tr>
           
                <td id="theirside" width=85% valign=top>
                    <table width=100%>
                        <tr>
                            <td width=100><b><asp:Label ID="sideid" runat=server /></b></td>
                            <td width=0><asp:LinkButton ID="Fscreen" runat=server Text="Full Screen" OnClick ="setfullscreen"/></td>
                            <td width=50><b><i>User ID:</i></b></td>
                            <td><asp:Label ID="uid" runat=server /></td>
                            <td width=50><b><i>Password:</i></b></td>
                            <td ><asp:Label ID="password" runat=server /></td>
                            <td width=40%></td>
                            <asp:Panel ID="pnlnewbuttons" runat=server Visible=false>
                               
                                <td ><asp:button id="cc" runat=server text="Completed" onclick="finexit" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:9pt;width:90px; cursor:hand" /></td>		
                                <td ><asp:button id="cc2" runat=server text="Cancel" onclick="deferad" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:9pt;width:90px; cursor:hand" /></td>		
                            </asp:Panel>
                        
                        
                        </tr>
                    </table>
                    
                    <iframe id="Advenue" src="https://post.craigslist.org/det" height="525" width=100% runat=server />
                    
                </td>
            </tr>
        </table></td>
               </tr>
        </table>
        
        
        <table>
                            <tr>
                                <td valign=top><asp:Label ID="adcode" runat=server Visible=false /></td>
                                 <td valign=top> </td>
                            </tr>
                        </table>
         </asp:Panel>
	    </form>
	    		</body>
</HTML>
	