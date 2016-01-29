<%@ Page language="vb" Codebehind="vinfo.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.vinfo" Debug="false" trace="false" aspcompat=true  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
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
		<title>WebMagicPortal.com</title>
	</HEAD>
	
<body >
	<form id="forms1a" runat="server" enctype="multipart/form-data" method="post">
	    <table>
	        <tr>
	            <td><font size=4>Lead Source User Information</font></td>
	        </tr>
	    </table>
	    <br />
	    <asp:Panel ID="pnladdvenue" runat="server" Visible="false">
                	    <table width=65%>
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
                			    <td colspan=8><asp:button id="addnewv" runat=server text="Save" onclick="savenewvenue" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:80px; cursor:hand" /></td>		
    			                <td colspan=8><asp:button id="addnewvexit" runat=server text="Exit" onclick="savenewvenueExit" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:80px; cursor:hand" /></td>		
    			   
                		    </tr>
                	    </table>               
                    
                    </asp:panel>
                
                   <table width=40% border=0>              
                       <tr>
                            <td><b>Lead Source</b></td>
                            <td><b>ID</td></td>    
                            <td><b>Password</td></td>                                                                    
                        </tr>
                        <tr>
                            <td><asp:DropDownList ID="advenue" DataTextField="x_descr" AutoPostBack =true OnSelectedIndexChanged ="checkifvexists"
                                        
              		                        DataValueField="tbl_xwalk_pk" 
              		                        Runat="server" /></td>
              		        <td><asp:TextBox ID="uid" runat=server size=30 /></td>
              		         <td><asp:TextBox ID="password" runat=server size=30 /></td>
              		     </tr>
              		</table>
              			<table>
              				<tr>
              					<td><asp:label visible=false id=lblrecexists runat=server text="Entry exists.  You can only have 1 entry for each Lead Source" /></td>
              				</tr>
              			</table>
              		<table>
              		    <tr>
              		        <td colspan=8><asp:button id="Button1" runat=server text="Save" onclick="insertvinfo" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:80px; cursor:hand" /></td>		
    			            <td colspan=8><asp:button id="Button2" runat=server text="Exit" onclick="savevinfoExit" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:80px; cursor:hand" /></td>		
    			        </tr>
    			   </table>
              		
                  
 
		
	</form>
	</body>	
</HTML>
