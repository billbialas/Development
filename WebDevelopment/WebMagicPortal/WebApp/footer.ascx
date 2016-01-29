<script language="JavaScript" src="_include/default.js"></script>
<link rel="stylesheet" href="../_include/default.css" type="text/css">


<script language="vb" runat="server">

	Sub contactclick(ByVal sender As Object, ByVal e As EventArgs)
	
      response.redirect("contactus.aspx")
   end sub
   Sub userclick(ByVal sender As Object, ByVal e As EventArgs)
	
      response.redirect("useragreement.aspx")
   end sub
     
     Sub bugclick(ByVal sender As Object, ByVal e As EventArgs)
	
      response.redirect("bugreport.aspx")
   end sub
     
     
     

</script>

<div id="footer">
<table border=0 width="100%" >
  <tr>	
  		<td width=77%>Copyright © 2009 B-Squared Consulting, Inc. All rights reserved</td>
		<td align=center><asp:linkbutton id="contact" Text= "Contact Us" onClick="contactclick" CommandArgument="contact" runat="server" cssclass=linkbuttons /></td> 
      <td>|</td>
		<td align=center ><asp:linkbutton id="useragree" Text= "User Agreement" onClick="userclick" CommandArgument="useragree" runat="server" cssclass=linkbuttons /></td> 
      <td>|</td>
		<td align=center><asp:linkbutton id="bugr" Text= "Report A Bug"  onClick="bugclick" CommandArgument="bug" runat="server" cssclass=linkbuttons /></td> 
  </tr>
</table>
</div>