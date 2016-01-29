<%@ Page language="vb" Codebehind="help.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.help" Debug="false" trace="false" aspcompat=true  %>
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
			</tr>
		</table>
		<div style="padding:10">
			<table cellpadding=2 cellspacing=2 width=50%>
				<tr>
					
					<td><img src="../images/icons-people-p.png" runat=server height=30 width=30 /></td>
					<td  class=pgsubheadersB valign=center>Lead Manager</td>
			
					<td><img src="../images/configuration-icon.jpg" runat=server height=30 width=30 /></td>
					<td  class=pgsubheadersB valign=center>System Setup</td>
				</tr>
				<tr>
					<td></td>
					<td valign=top>
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
				
					<td></td>
					<td  valign=top>
						<ul class=helpscreen>
							<li><asp:linkbutton id="comaint" Text= "Company Maintenance" visible=true runat="server" cssclass=linkbuttons onClick="cohelp" /></li>
							<li><asp:linkbutton id="tpmaint" Text= "Template Maintenance" visible=true runat="server" cssclass=linkbuttons onClick="tphelp" /></li>
							<li><asp:linkbutton id="dpmaint" Text= "DropDown Maintenance" visible=true runat="server" cssclass=linkbuttons onClick="dphelp" /></li>
							<li><asp:linkbutton id="immaint" Text= "Image Maintenance" visible=true runat="server" cssclass=linkbuttons onClick="imhelp" /></li>
						
						
						</ul>	
					</td>
				</tr>		
				
			</table>		
		</div>
 
		
	</form>
	</body>	
</HTML>
