<%@ Page language="vb" Codebehind="loginAgent.aspx.vb" Inherits="database1.processloginAgent" Debug="false" trace="false"%>
<script language="JavaScript" src="../_include/default.js"></script>
<link rel="stylesheet" href="../_include/login.css" type="text/css">
<html>
<head>
  <title></title>
</head>
<body>
  <form id="processlogin" defaultfocus="txtUsr" runat="server">
	 	<div id="header">
			<table>
				<tr>
					<td><img  height="70" width="100" alt="" src="../images/Magic2.png" border="0"></td>
					<td><a href="http://www.webmagicportal.com"><img  height="70" width="800" alt="" src="../images/headernew.jpg" border="0"></a></td>
				</tr>
			</table>
		</div>
		
<ul id="nav">
	<li id="nav-1"><a href="http://www.webmagicportal.com/features.aspx">Features</a></li>
	<li id="nav-2"><a href="http://www.webmagicportal.com/pricing.aspx">Pricing</a></li>
	<li id="nav-4"><a href="http://www.webmagicportal.com/contact.aspx">Contact</a></li>
	<li id="nav-5"><a href="http://app.webmagicportal.com">Log IN</a></li>
</ul>
		<div id="PG_BackGround" >
			<div class="content">
				<div class="sub-heading">
					<img src="../images/loginbk.jpg" />				
				</div>		
				<div id="bdyL" >
               	<p style="padding:10;margin-bottom:30;margin-top:1;"><img src="../images/login.jpg"  /></p>
						<h1>User Name</h1>
				      <p><asp:textbox id="txtUsr" runat=server size=50 height=25  /></p>
				 		<h1>Password</h1>
						<p><asp:textbox id="txtPwd" runat=server size=52 height=25 textmode="password" style="margin-bottom:10;margin-top:0;" /></p>
						<div style="margin-left:290;">
							<input type="submit" value="Sign In" Onserverclick="SubmitBtn_Click" runat="server" Style="background-color:#C0C0C0; color:#000000; font-family:arial; font-size:8pt; font-weight: bold;	width:110px; cursor:hand" />
		            	<input type="submit" value="Forgot Password" Onserverclick="click_emailpassword" runat="server" Style="background-color:#C0C0C0; color:#000000; font-family:arial; font-size:8pt; font-weight: bold;	width:110px; cursor:hand" />
		            	
		            </div>
		            <div style="margin-left:290;">  <br><asp:checkbox id="chkremem" text="<b><i>Remember Me</i></b>" runat=server /></div>
		            <br>
		              <table width=90% cellspacing=3 cellpadding=3>
							       	<tr>
								   	    <td align=right><font color=red><b><asp:label id="outmessage" runat="server" /></b></font></td>
          						  	</tr>
    								</table>
    							
		            <asp:Panel ID="pnlalreadyloged" runat=server Visible=false>
								    	<table width=70%>
								         <tr>
								   	  	    <td align=right><input type="submit" value="Reset Session" Onserverclick="click_resetlogon" runat="server" Style="background-color:#C0C0C0; color:#000000; font-family:arial; font-size:8pt; font-weight: bold;	width:110px; cursor:hand" /></td>
		               			 	</tr>	
							         </table>
							    	</asp:Panel> 
							    	<asp:Panel ID="pnlsecretcode" runat=server Visible=false>
									    <table width=85%>
									      	<tr>
										   	    <td align=right> <asp:TextBox ID="txtcode" runat=server /></td>
										   	    <td><input type="submit" value="Reset" Onserverclick="click_doreset" runat="server" Style="background-color:#C0C0C0; color:#000000; font-family:arial; font-size:8pt; font-weight: bold;	width:90px; cursor:hand" /></td>
									       </tr>	
											
								         </table>
								    </asp:Panel>   
			 		
				</div>
				<div id="servs"> 
				    	<table cellspacing=4 cellpadding=4>
				    	  <tr>
				    	  	<td><h5>Just a few things were pulling out of our hat for you!</h5></td>
				    	  </tr>
				        <tr>
				            <td align=center><img id="Img1" runat=server src="/images/wand.png" height="80" width="90" border=0  />
								</td>
							</tr>
				        <tr>
				            <td><h1>AD Managment</h1></td>
				        </tr>
				        <tr>
				            <td valign=top><h1>Custom Branding</h1></td>
				        </tr>
				        
				        <tr>
				            <td valign=top><h1>Real Time Lead Integration</h1></td>
				        </tr>
				        <tr>
				            <td valign=top><h1>Lead Management</h1></td>
				        </tr>
				        
				        <tr>
				            <td align=center><img id="hatimg" runat=server src="/images/hat-magic.gif" height="120" width="120" />
								</td>
				        </tr>
				    </table>
				</div>
			</div>
	</div>
		
	
<div id="footer" >
	<h1>Copyright © 2009 B-Squared Consulting, Inc. All rights reserved</h1>
</div>  </form>
</body>
</html>

