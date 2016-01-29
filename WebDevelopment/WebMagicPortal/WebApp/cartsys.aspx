<%@ Page language="vb" Codebehind="cartsys.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.cartsys" Debug="false" trace="false" aspcompat=true  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<%@ Register TagPrefix="ed" Namespace="OboutInc.Editor" Assembly="obout_Editor" %>
<link rel="stylesheet" href="../_include/default.css" type="text/css">
<script language="JavaScript" src="../_include/default.js"></script>

<HTML>
	<HEAD>
		<title>www.WebMagicPortal.com</title>
	</HEAD>
	
<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">
	<form id="forms1a" runat="server" enctype="multipart/form-data" method="post">
		<table>
			<tr>
				<td class=pgheaders>Shopping Cart Setup</td>
			</tr>
		</table>
		
		<table border=0  width=100% cellspacing=0 cellpadding=0 id="SubNav">
			<tr >
				<td width="100%">
					<table runat="Server" id="subnac" cellspacing=0 cellpadding=0 width=100% border=0>
						<tr height=22>
							<td id="subnavGen" align=center width=110><asp:linkbutton id="Lgen" Text= "Header"  runat="server" Font-underline="false" Style="cursor:hand" onClick="btn_header" /> </td>
							<td id="spacer0" class="tblcelltestc" width=1>&nbsp</td> 			           
							<td id="subnavPage1" align=center width=110><asp:linkbutton id="lpage1" Text= "Left Side"  runat="server" Font-underline="false" Style="cursor:hand" onClick="btn_pg1" /> </td>
							<td  runat="Server" class="tblcelltestc" width=850>
		               	<table width=100% border=0  cellspacing=0 cellpadding=0>
		               		<tr height=22>
		               				<td width=90%></td>
											<td><asp:button id="btnsavesysmess" Visible=true  runat=server text="Update" width="70" onclick="updatecartdata" CausesValidation="False"  Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:10pt;width:120px; cursor:hand" /></td>					
							        		<td><asp:button id="btnscexit" Visible=true  runat=server text="Exit" width="70" onclick="Exitcart" CausesValidation="False"  Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:10pt;width:120px; cursor:hand" /></td>					
						
								   </tr>           	
		               	</table>
		               </td>
 			        	</tr>
 			      </table>
		
					<table class=tblcelltestb width=100% >
 			      	<tr>
 			      		<td>
 			       	<asp:panel id="pnlheader" runat=server visible=false >
 			       		<div id="fieldtitles" style="vertical-align: top; height: 480px; ">			
							<table>								
								<tr>
							   	<td><ed:Editor ShowQuickFormat="false" FixedToolbar="false"  AutoFocus="false" height="450" width="1100" submit="false" PathPrefix="Editor_data/" FlashBrowse="myFlashBrowse.aspx" MediaBrowse="myMediaBrowse.aspx" ImageBrowse="Editor_data/myImageBrowseClientSSL.aspx" UrlBrowse="myUrlBrowse.aspx" id="csHtext" PreviewMode="true" runat="server">
										</ed:Editor></td>
								</tr>
								 	
							</table></div>
						</asp:panel>
						<asp:panel id="pnlleft" runat=server visible=false >	
							<div id="fieldtitles" style="vertical-align: top; height: 480px; ">		
							<table>
								
								<tr>
							   	<td><ed:Editor ShowQuickFormat="false" FixedToolbar="false"  AutoFocus="false" height="450" width="1100" submit="false" PathPrefix="Editor_data/" FlashBrowse="myFlashBrowse.aspx" MediaBrowse="myMediaBrowse.aspx" ImageBrowse="Editor_data/myImageBrowseClientSSL.aspx" UrlBrowse="myUrlBrowse.aspx" id="csLtext" PreviewMode="true" runat="server">
										</ed:Editor></td>
								</tr>
								 	
							</table></div>
						</asp:panel>
								</td>
						</tr>
				</table>
			</td>
		</tr>
	</table>
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
		
	</form>
	</body>	
</HTML>
