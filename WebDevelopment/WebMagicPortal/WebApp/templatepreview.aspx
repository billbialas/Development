<%@ Page language="vb" Codebehind="templatepreview.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.templatepreview" Debug="false" trace="false"  validateRequest=false  %>
<%@ Register TagPrefix="ed" Namespace="OboutInc.Editor" Assembly="obout_Editor" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script language="JavaScript" src="../_include/default.js"></script>
<HTML>
	<HEAD>
		<title>Choice One BMS</title>
	</HEAD>
	
<body >
	<form id="forms1a" runat="server" >
	    <asp:panel ID="pnlexport" runat=server visible=true>
	       <table>
	            <tr>
	                <td>Template Preview</td>
	            </tr>
	       </table>
	       
	       <table width=100%>	       
		    <tr>
		        <td>Name</td>
		        <td><asp:TextBox ID="tname" runat=server /></td>
		    </tr>
		    <tr>
		        <td>Subject</td>
		        <td><asp:textbox ID="tsubject" runat=server /></td>
		    </tr>
		</table>
		Text
		<div style="vertical-align: top; height: 450px; overflow:auto;">
		    <table width=100%>
		       
		         <tr>
		            <td><asp:label ID="tptextlbl" runat=server Visible=true /></td>
		         </tr>
		         <tr>
                    <td ><ed:Editor show="false" ShowQuickFormat="false" FixedToolbar="false"  AutoFocus="false" height="425" width="1000" submit="false" PathPrefix="Editor_data/" FlashBrowse="myFlashBrowse.aspx" MediaBrowse="myMediaBrowse.aspx" ImageBrowse="Editor_data/myImageBrowseClient.aspx" UrlBrowse="myUrlBrowse.aspx" id="tptext" PreviewMode="true" runat="server">
                    </ed:Editor> </td>
                </tr>
            </table>  </div>
            <table>
		        <tr>	
		            <td><asp:button id="tempedit" runat=server text="Edit" onclick="edittemplate" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:80px; cursor:hand" /></td>		
                    <td><asp:button id="tempcancel" runat=server text="Close" onclick="cancelexp" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:80px; cursor:hand" /></td>		
    	        </tr>
		    </table>
	  
	    </asp:panel>
	    
	</form>
	</body>	
</HTML>
