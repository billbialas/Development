<%@ Page language="vb" Codebehind="agentarea.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.agentarea" Debug="false" trace="false" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script language="JavaScript" src="../_include/default.js"></script>
<HTML>
	<HEAD>
		<title>Choice One Realty- Michigan</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	
<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">
	<form id="bpo" runat="server">
		

		<table>
			<tr>
				<td>
				                	
      <asp:DataGrid Runat=server
				ID="agentarea" 
            AutoGenerateColumns=False
            Width="100%"          
            ItemStyle-BackColor=white
            ItemStyle-Font-Name="arial"
            ItemStyle-Font-Size="12px"
            BorderColor="#ffffff"
            AllowPaging="false"            
          
				PagerStyle-Visible = "False"	
				HeaderStyle-BackColor="steelblue"
				HeaderStyle-ForeColor="White">
            
			   <Columns >
           		   		
        			<asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Agent</b></font>"  DataField="nameby" ItemStyle-Width="150px"   />
        			<asp:BoundColumn HeaderText="<font color=#FFF8C6><b>County</b></font>"  DataField="countyworked" ItemStyle-Width="200px"   />   		
        			<asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Cities</b></font>"  DataField="citiesworked" ItemStyle-Width="200px"   />
      			
		     </Columns>
	

		</asp:DataGrid></td></tr></table>
		
         
	</form>
	</body>	
</HTML>
