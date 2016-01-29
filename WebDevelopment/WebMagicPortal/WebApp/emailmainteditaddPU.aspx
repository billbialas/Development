<%@ Page language="vb" Codebehind="emailmainteditaddPU.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.emailmainteditaddPU" Debug="false" trace="false" aspcompat=true validateRequest=false  %>
<%@ Register TagPrefix="FTB" Namespace="FreeTextBoxControls" Assembly="FreeTextBox" %> 
<%@ Register TagPrefix="ed" Namespace="OboutInc.Editor" Assembly="obout_Editor" %>
<link rel="stylesheet" href="../_include/default.css" type="text/css">
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script language="JavaScript" src="../_include/default.js"></script>

<HTML>
	<HEAD>
		<title>www.WebMagicPortal.com</title>
	</HEAD>
	
<body >
	<form id="forms1a" runat="server" enctype="multipart/form-data" method="post">
	    <table>
	        <tr>
	            <td class=pgheaders>Add/Edit Templates</td>
	        </tr>
	    </table>
	    <div id="fieldtitles" >
	    <table>
	        <tr>
	            <td align=left>Template Name</td>
	             <td align=left>Description</td>
	             <td align=left>Scope</td>
	        </tr>
	        <tr>
	            <td><asp:TextBox ID="emname" runat=server size=35 /></td>
	            <td><asp:TextBox ID="emdesc" runat=server size=70 /></td>
	            <td><asp:DropDownList id="dd_txtstat" AutoPostBack="false"
							                  		DataValueField="x_descr" 
							                  		Runat="server" />
	            <asp:checkbox ID="sigchk" runat=server size=60 visible=false /></td>
	        
	        </tr>
	      </table>
	     	<table>
	        <tr>
	            <td align=left>Subject Line</td>
	         </tr>
	        <tr>
	            <td><asp:TextBox ID="emsub" runat=server size=80 /></td>
	        </tr>
	        <tr>
	            <td align=left valign=top>Body</td>
	         </tr>
	         <tr>
		        		<td><ed:Editor ShowQuickFormat="false" FixedToolbar="false"  AutoFocus="false" height="440" width="1000" submit="false" PathPrefix="Editor_data/" FlashBrowse="myFlashBrowse.aspx" MediaBrowse="myMediaBrowse.aspx" ImageBrowse="Editor_data/myImageBrowseClient.aspx" UrlBrowse="myUrlBrowse.aspx" id="emtext" PreviewMode="true" runat="server">
					                              		
															</ed:Editor></td>
					</tr>
	    </table>
	   </div>
	      <table>
	        <tr>
	           <td><asp:button id="btn_save" Visible=true  runat=server text="Save & Exit" width="70" onclick="click_saveemail" CausesValidation="False"  Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:9pt;width:100px; cursor:hand" /></td>					
	          <td><asp:button id="btnexit" Visible=true  runat=server text="Cancel" width="70" onclick="click_exitemail" CausesValidation="False"  Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:9pt;width:100px; cursor:hand" /></td>					
		    </tr>
		</table>
 
 
		
	</form>
	</body>	
</HTML>
