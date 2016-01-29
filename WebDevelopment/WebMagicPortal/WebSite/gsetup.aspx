<%@ Page language="vb" Codebehind="gsetup.aspx.vb" AutoEventWireup="false" Inherits="setup.gsetup" Debug="false" trace="false" %>
<link rel="stylesheet" href="../_include/default.css" type="text/css">
<html >

<head runat="server">
<meta http-equiv="Content-Language" content="en-us" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>www.WebMagicPortal.com</title>
</head>

<body style="background-color: #FFFFFF">

<form id="MAIN" runat="server">
  <table width=100%>
        <tr>
            <td width=130><img  height="90" width="100" alt="" src="../images/Magic-128x128.png" border="0"></td>
            <td valign=top><img  alt="" src="../images/head.jpg" border="0"></td>
        </tr>
        <tr>
        		<td colspan=2 align=left><hr width=80%/></td>
        </tr>
    </table>
    <asp:panel ID="pnl_NewUser" runat="server" Visible="false">
        <table>
    	    <tr>
    		    <td colspan=2><font size=3 color=red>Thank you for subscribing!!!</font></td>
    		</tr>
    	</table>
    	<br />
    	<table>
    		<tr>
    		    <td></td>
    	    </tr>
    		<tr>
    		    <td colspan=2>We appreciate your business and believe you will find US to be an invaluable tool for increasing your business</td>
    	    </tr>
    	</table>
    	
    	<br />
    	<table width=30%>
    	    <tr>
    		    <td align=right>Your userid is:&nbsp&nbsp</td>
    		    <td><font color=red><asp:Label ID="userid" runat="server" /></font></td>
    	    </tr>
    	    <tr>
    		    <td align=right>Your password is:&nbsp&nbsp</td>
    		    <td><font color=red><asp:Label ID="pass" runat="server" /></font></td>
    	    </tr>
    	    <tr>
    	        <td colspan=2>Please keep this information in a safe place.</td>
    	    </tr>
    	    <tr>
    	        <td colspan="3"><asp:button id="Button1" runat=server text="Continue To System" width="180" onclick="ExistContinue" CausesValidation="False"  Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:140px; cursor:hand" /></td>	 
            </tr>         	
        	
        </table>
    </asp:panel>  
    <asp:panel ID="pnl_ExistingUser" runat="server" Visible="false">
        <table>
    	    <tr>
    		    <td>Thank you for your purchase!</td>
    	    </tr>
    	    <tr>
    		    <td>Please click continue to be logged into the system</td>
    	    </tr>
    	    <tr>
    		    <td><asp:button id="btn_ExistContinue" runat=server text="Continue" width="70" onclick="ExistContinue" CausesValidation="False"  Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:70px; cursor:hand" /></td>	 
    	    </tr>
    	 </table>
    
    
    </asp:panel>
    
</form>
</body>

</html>
