<%@ Page language="vb" Codebehind="loginAgent.aspx.vb" Inherits="database1.processloginAgent" Debug="false" trace="false"%>
<script language="JavaScript" src="../_include/default.js"></script>
<html>
<head>
  <title></title>
</head>
<body>
  <form id="processlogin" defaultfocus="txtUsr" runat="server">
     
     
     
     <table id="login"  width=50% >
        	<tr>
        		<td colspan=2 align=left><font size=4><b>Login</b></font></td>
        	</tr>
       	<tr>
            <td>User ID:</td>
            <td><asp:textbox id="txtUsr" runat=server size=15 /></td>
        	</tr>
        	<tr>
            <td>Password</td>
            <td><asp:textbox id="txtPwd" runat=server size=15 textmode="password" /></td>
        	</tr>
         <tr>
		     	<td colspan=2><input type="submit" value="Sign In" Onserverclick="SubmitBtn_Click" runat="server" /></td>
		   </tr>  
		   <tr>
		     <td colspan=2><a href="forgotpass.aspx"><font size=2>Forgot Password</font></a></td>
		   </tr>
		   <tr>
		   	<td colspan=2><asp:label id="outMessage" runat="server" /></td>
		   </tr>	
     </table>
       
  </form>
</body>
</html>

