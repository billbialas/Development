<%@ Page language="vb" Codebehind="gsignup.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.gsignup" Debug="false" trace="false" %>
<link rel="stylesheet" href="../_include/default.css" type="text/css">
<script type="text/javascript" src="../_include/jquery.js"></script>
<script type="text/javascript" src="../_include/default.js"></script>
<html >

<head runat="server">
<meta http-equiv="Content-Language" content="en-us" />
<meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
<title>www.WebMagicPortal.com</title>
</head>

<body style="background-color: #FFFFFF">

<form id="MAIN" runat="server">
     
<div id="headerA">
	<table>
		<tr>
			<td><img  height="70" width="100" alt="" src="../images/Magic2.png" border="0"></td>
			<td><a href="http://www.webmagicportal.com"><img  height="70" width="800" alt="" src="../images/headernew.jpg" border="0"></a></td>
		</tr>
	</table>
</div>
  
  
    <table  width=70% cellpadding=2 cellspacing=0 border=0>
 		<tr>
 		    <td width=10%></td>
 			<td align="left" width=30%><img src="../images/bs.jpg" ></td>
 			<td class="mpage" width=5%>&nbsp</td>
 			<td  align="left" width=30%><img src="../images/bsb.jpg" ></td>
 		</tr>
 	
 		<tr height=30>
 		    <td width=10%></td>
 			<td class="mpageA" align="center" valign=top width=30%>
 			    <table width=100%>
 			        <tr>
 			            <td><font size=4>With your Basic subscription you get..</font></td>
 			        </tr>
 			        <tr>
 			            <td><ul>
                            <li type=square><font size=3 color=green>Comprehensive Lead Management</font></li> 
                            <li type=square><font size=3 color=green>AD Management</font></li>
                            <li type=square><font size=3 color=green>Etc...</font></li>
                            </ul> 
                         </td>
 			        </tr>
 			    </table>
 		    </td>		
            <td class="mpage" width=5%>&nbsp</td>
 			<td  align="center" width=30%>
 			<table width=100%>
 			        <tr>
 			            <td><font size=4>Add the Power of Internet Branding!</font></td>
 			        </tr>
 			        <tr>
 			            <td><ul>
                            <li type=square><font size=3 color=green>Company Logo</font></li> 
                            <li type=square><font size=3 color=green>Customizable AD text</font></li>
                            <li type=square><font size=3 color=green>Auto Email Responses</font></li>
                            <li type=square><font size=3 color=green>Email Notifications</font></li>
                            </ul>
                         </td>
 			        </tr>
 			    </table></td>
 			</tr>
 		<tr>
 		    <td width=10%></td>
 			<td align="left" width=30%>
 			    <table>
 			        <tr>
 			            <td><img src="../images/limited_time_offer.gif" height=60></td>
 			            <td>
 			                <table>
 			                    <tr>
 			                        <td><img src="../images/bsPriceA.jpg" ></td>
 			                    </tr>
 			                    <tr>
 			                        <td><img src="../images/bsprice.jpg" ></td>
 			                    </tr>
 			                </table>
 			           </td>
 			        </tr>
 			    </table>
 			 </td>
 			<td class="mpage" width=5%>&nbsp</td>
 			<td align="left" width=30%>
 			    <table>
 			        <tr>
 			            <td><img src="../images/limited_time_offer.gif" height=60></td>
 			            <td>
 			                <table>
 			                    <tr>
 			                        <td><img src="../images/bsbPriceA.jpg" ></td>
 			                    </tr>
 			                    <tr>
 			                        <td><img src="../images/bsbprice.jpg" ></td>
 			                    </tr>
 			                </table>
 			           </td>
 			        </tr>
 			    </table>
 			 </td>
 			
 		</tr>
 		
 		<tr>
 		    <td width=10%></td>
 			<td align="left" width=30%><b><i>1. Choose your Subscription Type </i></b>&nbsp&nbsp&nbsp&nbsp<asp:DropDownList ID="ddbasic" runat=server>
 			                            <asp:ListItem Value="1" Text="1 Month"/>
  	    						                <asp:ListItem Value="3" Text="3 Months"/>
  	    						                <asp:ListItem Value="6" Text="6 Months"/>
  	    						                <asp:ListItem Value="9" Text="9 Months"/>
 		 						                <asp:ListItem Value="12" Text="1 Year"/>
 			                            </asp:DropDownList></td>
 			<td class="mpage" width=5%>&nbsp</td>
 			<td align="left" width=30%><b><i>1. Choose your Subscription Type </i></b>&nbsp&nbsp&nbsp&nbsp<asp:DropDownList ID="ddbasicb" runat=server>
 			                            <asp:ListItem Value="1" Text="1 Month"/>
  	    						                <asp:ListItem Value="3" Text="3 Months"/>
  	    						                <asp:ListItem Value="6" Text="6 Months"/>
  	    						                <asp:ListItem Value="9" Text="9 Months"/>
 		 						                <asp:ListItem Value="12" Text="1 Year"/>
 			                            </asp:DropDownList></td>
 		</tr>
 		<tr>
 		    <td width=10%></td>
 			<td align="left" width=30%><b><i>2. Click Subscribe! </i></b></td>
 			<td class="mpage" width=5%>&nbsp</td>
 			<td align="left" width=30%><b><i>2. Click Subscribe! </i></b></td>
 		</tr>
 		<tr>
 		    <td width=10%></td>
 			<td align="center" width=30%><asp:ImageButton id="imagebutton1" runat="server"  AlternateText="ImageButton 1"  ImageUrl="images/../images/subscribe_button.jpg" height=120  OnClick="AddproductA"/></td>
 			<td class="mpage" width=5%>&nbsp</td>
 			<td align="center" width=30%><asp:ImageButton id="imagebutton2" runat="server"  AlternateText="ImageButton 2"  ImageUrl="images/../images/subscribe_button.jpg" height=120  OnClick="Addproductb"/></td>
 		</tr>
 		
 		
 	</table><br /><br /><br />
 	<hr width=100% />
 	 <table>
        <tr>
            <td>CopyRight 2009   B-Squared Consulting, Inc...</td></tr></table>
    
</form>
</body>

</html>
