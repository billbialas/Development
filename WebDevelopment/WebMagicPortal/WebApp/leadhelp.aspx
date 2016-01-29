<%@ Page language="vb" Codebehind="leadhelp.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.leadhelp" Debug="false" trace="false" aspcompat=true  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<link rel="stylesheet" href="../_include/default.css" type="text/css">


<HTML>
	<HEAD>
		<title>webmagicportal.com</title>
	</HEAD>
	
<body >
	<form id="forms1a" runat="server" enctype="multipart/form-data" method="post">
		<table>
			<tr>
				<td class=pgheaders>Help Tutorials</td>
				<td width=200 align=right>
					<table>
						<tr>
							<td><asp:button id="l_closehelp" runat=server text="Help Index" width="70" visible=false onclick="btn_closehelp" CausesValidation="False"   cssclass=frmbuttonsXLG /></td>
							<td><asp:button id="l_closehelpA" runat=server text="Close Help" width="70" visible=true onclick="btn_closehelpA" CausesValidation="False"   cssclass=frmbuttonsXLG /></td>
						</tr>
					</table>
				</td>
			</tr>
		</table>
		
		<div id="divhelp" style="padding:10;margin-left:20" runat=server >
			<table cellpadding=2 cellspacing=2>
				<tr>
					
					<td><img src="../images/icons-people-p.png" runat=server height=30 width=30 /></td>
					<td  class=pgsubheadersB valign=center>Lead Manager</td>
				</tr>
				<tr>
					<td></td>
					<td>
						<ul class=helpscreen>
							<li><asp:linkbutton id="ld3maint" Text= "Lead Creation Overview" visible=true runat="server" cssclass=linkbuttons onClick="ld3help" /></li>
							<li><asp:linkbutton id="ldmanual" Text= "Manual Lead Creation" visible=true runat="server" cssclass=linkbuttons onClick="ldmanualhelp" /></li>
							<li><asp:linkbutton id="ldedit"   Text= "Editing Leads" visible=true runat="server" cssclass=linkbuttons onClick="ldedithelp" /></li>
							<li><asp:linkbutton id="lddelete" Text= "Deleting/Undeleting Leads" visible=true runat="server" cssclass=linkbuttons onClick="lddeletehelp" /></li>
							<li><asp:linkbutton id="ldtaska"  Text= "Adding Tasks" visible=true runat="server" cssclass=linkbuttons onClick="ldtaskahelpa" /></li>
							<li><asp:linkbutton id="ldtaskt"  Text= "Task Tracking" visible=true runat="server" cssclass=linkbuttons onClick="ldtaskthelpt" /></li>
							<li><asp:linkbutton id="ldemail"  Text= "Emailing" visible=true runat="server" cssclass=linkbuttons onClick="ldemailhelp" /></li>
							<li><asp:linkbutton id="ldnote"   Text= "Adding Notes" visible=true runat="server" cssclass=linkbuttons onClick="ldNotehelp" /></li>
							<li><asp:linkbutton id="ldupload" Text= "Uploading Leads" visible=true runat="server" cssclass=linkbuttons onClick="lduploadhelp" /></li>
							<li><asp:linkbutton id="ldexport" Text= "Exporting Leads" visible=true runat="server" cssclass=linkbuttons onClick="ldexporthelp" /></li>
							
						
						</ul>	
					</td>
				</tr>		
				
			</table>		
		</div>
  		<iframe id="frmhelp" src="" height="645" width=100% runat=server visible=false />
		
	</form>
	</body>	
</HTML>
