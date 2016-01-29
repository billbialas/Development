<%@ Page language="vb" Codebehind="leadhistory.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.leadhistory" Debug="false" trace="false" validateRequest=false  %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %> 
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script language="JavaScript" src="../_include/default.js"></script>
<HTML>
	<HEAD>
			<title>www.WebMagicPortal.com</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	
<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">
	<form id="leadprofile" runat="server">
		<table cellpadding=0 cellspacing=0 height=530>
			<tr>
				<td width=108 bgcolor=#006295><td>
				<td valign=top >
				<div style="margin-left:20;margin-top:5;">
		    <asp:Panel ID="pnl_leadhistory" runat="server"	>
				<table>
				    <tr>
					    <td class=pgheaders>Lead History-<asp:label id='lbltype' runat=server /></b></td>
				    </tr>
				</table>
				<div id="fieldtitles">
				<table valign=top width=50%>
				    <tr>  							
	  				    <td >Entity:</td>
					    <td ><asp:DropDownList ID="ddLHwho" Runat="server">	
	  	    						    <asp:ListItem Value="Lead" Text="Lead"/>
	  	    						    <asp:ListItem Value="Leads Behalf" Text="Leads Behalf"/>
	  	    						    <asp:ListItem Value="Other" Text="Other"/>  	    						
	 								    </asp:DropDownList></td>	  				
	  				    <td >Type:</td>				
					    <td ><asp:DropDownList ID="ddLHType"                 		
	                  		    DataValueField="x_descr" 
	                  		    Runat="server" /> </td>		
	  				    <td >Date:</td>
	  				    <td ><asp:textbox id="l_edate" runat=server size=8 /></td>
	      				
	  			    </tr>
	  			</table></div>
	  			<table>
	  			    <tr>
	  			        <td valign=top>Notes</td>
	  				    <td valign=top><FTB:FreeTextBox id="l_enotes" runat="server" width="900" height=230 />  </td>
	  			    </tr>
				</table>
				<table>
				    <tr>
				       <td align=left ><asp:button id="l_savecontact" runat=server text="Save" width="100" onclick="btn_savecontact" CausesValidation="False" cssclass=frmbuttons /></td>		
	  				    <td align=left ><asp:button id="btn_addtask" runat=server text="Add Task" width="100" onclick="click_addtask" CausesValidation="False" cssclass=frmbuttons /></td>
						<td align=left ><asp:button id="l_cancelcontact" runat=server text="Cancel" width="100" onclick="btn_cancelcontact" CausesValidation="False" cssclass=frmbuttons /></td>	
						<td align=left ><asp:button id="l_delete" runat=server text="Delete" width="100" onclick="btn_deletecontact" CausesValidation="False" cssclass=frmbuttons /></td>		
	    	
	  						  			
				    </tr>
				</table>
		    </asp:Panel>	
			<asp:Panel ID="pnl_addtask" runat="server"	visible="false">
			<table>
				    <tr>
					    <td class=pgheaders><asp:label id='lbltask' runat=server visible=false/></b></td>
				    </tr>
				</table>			
				<table>
					<tr>
						<td>Task Type:</td>
						<td><asp:DropDownList ID="ddtasktype" Runat="server"       		
	                  		    DataValueField="x_descr" 
	                  		    Runat="server" /> 	
	 								
	                 </td>
	                 <td>Task Status:</td>
						<td><asp:DropDownList ID="ddtaskstat" Runat="server"       		
	                  		    DataValueField="x_descr" 
	                  		    Runat="server" /> 	
	 								
	                 </td>
	                 <td>Due Date:</td>
	  				<td ><asp:textbox id="l_duedate" runat=server size=8 /></td>
	             </tr>
	            </table>
	            <table>
	  			    <tr>
	  				    <td valign=top>Task Description</td>
	      					
	  				    <td valign=top><asp:textbox id="hst_action" runat=server size=16 TextMode="MultiLine" Columns="70"  Rows="5" /></td>
	  			    </tr>
				</table>	
		 		<table width=20% border=0>
	  				<tr>   
	  				    <td align=left ><asp:button id="savetask" runat=server text="Save Task" width="100" onclick="click_savetask" CausesValidation="False" cssclass=frmbuttons /></td>
	  				    <td align=left ><asp:button id="canceltask" runat=server text="Cancel" width="100" onclick="btn_canceltask" CausesValidation="False" cssclass=frmbuttons /></td>	
						<td align=left ><asp:button id="deletetask" runat=server text="Delete" width="100" onclick="btn_deletetask" CausesValidation="False" cssclass=frmbuttons /></td>		
	    	
	  					
	  			    </tr>
	  			</table>
	  			</asp:Panel></div>
	  		</td>
	  	</tr></table>	
        </form>
	</body>	
</HTML>