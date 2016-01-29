<%@ Page language="vb" Codebehind="ldexport.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.ldexport" Debug="false" trace="false" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script language="JavaScript" src="../_include/default.js"></script>
<HTML>
	<HEAD>
		<title>Choice One BMS</title>
	</HEAD>
	
<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">
	<form id="forms1a" runat="server" >
	    <asp:panel ID="pnlexport" runat=server visible=true>
	       <table>
	            <tr>
	                <td><font size=3><b>Exporting Leads</b></font></td></tr></table>
	       <br />
	       <table>
	       
		    <tr>
		        <td>File Name</td>
		        <td><asp:TextBox ID="efilename" size=20 runat=server /></td>
		    </tr>
		    <tr>	        
                <td><asp:button id="expgo" runat=server text="GO" onclick="exportgo" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:80px; cursor:hand" /></td>		
    	        <td><asp:button id="expcancel" runat=server text="Close" onclick="cancelexp" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:80px; cursor:hand" /></td>		
    	</tr>
		</table>
	    
	    </asp:panel>
	    
	</form>
	</body>	
</HTML>
