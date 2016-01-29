<%@ Page language="vb" Codebehind="printlead.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.printlead" Debug="false" trace="false" validateRequest=false  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script language="JavaScript" src="../_include/default.js"></script>

<HTML>
	<HEAD>
		<title>GIMP</title>
	</HEAD>
	
	<body MS_POSITIONING="FlowLayout">
		<form  name="addlead"  runat="server" >
		
             <table width=100% border =0>
                <tr>	
    				<td width=120 ><img id="Logoarea" alt="" src="/images/default.jpg" height=80 width=100 border="0" runat=server></td>
     				<td ><font size=5><b><asp:label ID="coname" runat=server /></b></font></td>
     		
     			</tr>
	        </table>
            <br />
            <br />
            <table width=100% border =0>
                <tr>
	                <td width=100%><font size=3><b><u>Lead Information</u></b></font></td>
	            </tr>
	            <tr height=4><td></td></tr>
	        </table >
	        <table width=20% cellpadding=5>
	            <tr>
	                <td width=65><b>Lead No:</b></td>
	                <td colspan=8><b><i><asp:Label ID="lb_leadno" runat="server"></asp:Label></i></b></td>
	            </tr>
	         </table >
	         <table width=100% cellpadding=0 cellspacing=5 border=0>
	            <tr>
	                <td width=70><b>Ad Code:</b></td>
	                <td width=90 align=left><b><i><asp:Label ID="Label3" runat="server"></asp:Label></i></b></td>
	                <td width=20><b>Program:</b></td>
	                <td width=70><b><i><asp:Label ID="Label5" runat="server"></asp:Label></i></b></td>
	                <td width=20><b>Type:</b></td>
	                <td width=80><b><i><asp:Label ID="Label6" runat="server"></asp:Label></i></b></td>
	                <td width=20><b>Status:</b></td>
	                <td width=80><b><i><asp:Label ID="Label7" runat="server"></asp:Label></i></b></td>
	                <td width=70></td>
	            </tr>
	            <tr>
	                <td colspan=10><hr /></td>
	            </tr>
	         </table>
	         <br />
	         
	         <table width=100% border =0>
                <tr>
	                <td width=100%><font size=3><b><u>Contact Information</u></b></font></td>
	            </tr>
	            <tr height=4><td></td></tr>
	        </table >
	         <table width=100% cellpadding=0 cellspacing=5 border=0>
	            <tr>
	                <td width=40><b>Name:</b></td>
	                <td><b><i><asp:Label ID="Label8" runat="server"></asp:Label></i></b></td>
	            </tr>
	        </table>
	        <table cellpadding=0 cellspacing=5 border=0 width=60%>
	            <tr>
	                <td width=130><b>Primary Phone #:</b></td>
	                <td width=120><b><i><asp:Label ID="Label4" runat="server"></asp:Label></i></b></td>
	                <td width=130><b>Other Phone #:</b></td>
	                <td><b><i><asp:Label ID="Label15" runat="server"></asp:Label></i></b></td>
	            </tr>
	        </table>
	        <table cellpadding=0 cellspacing=5 border=0 width=100%>	            
	            <tr>
	                <td width=60><b>Address:</b></td>
	                <td colspan=6><b><i><asp:Label ID="Label9" runat="server"></asp:Label></i></b></td>
	            </tr>
	          </table>
	        <table cellpadding=0 cellspacing=5 border=0 width=100%>	
	            <tr>
	                <td width=30><b>City:</b></td>
	                <td width=150><b><i><asp:Label ID="Label10" runat="server"></asp:Label></i></b></td>
	                <td width=40><b>State:</b></td>
	                <td><b><i><asp:Label ID="Label11" runat="server"></asp:Label></i></b></td>
	                <td width=30><b>Zip:</b></td>
	                <td><b><i><asp:Label ID="Label12" runat="server"></asp:Label></i></b></td>
	            </tr>
	            <tr>
	                <td colspan=10><hr /></td>
	            </tr>
	         </table>
	        </table>
    		       
	    </form>
	</body>
</HTML>
	