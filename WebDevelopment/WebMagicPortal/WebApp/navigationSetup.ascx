<%@ Register TagPrefix="skm2" Namespace="skmMenu" Assembly="skmMenu" %> 
<link rel="stylesheet" href="../_include/default.css" type="text/css">
<script language="JavaScript" src="../_include/default.js" type="text/javascript"></script>

<%-- Script Code Block --%> 
<script language="vb" runat="server"> 
    Sub Page_Load(ByVal source As Object, ByVal E As EventArgs)
        
    
        If Not (Page.IsPostBack) Then
            If Session("loggedin") = True Then
                If Session("sysaccess") = "Agent" Then
                    navigationSetup.UserRoles.Add("agent")
                ElseIf Session("sysaccess") = "Broker" Then
                    navigationSetup.UserRoles.Add("Broker")
                End If
                If Session("Role") = "Administrator" Then
                    navigationSetup.UserRoles.Add("Administrator")
                End If
 
                If Session("sysaccess") = "Partner" Then
                    navigationSetup.UserRoles.Add("Partner")
                End If
                If Session("role") = "user" Then
                    navigationSetup.UserRoles.Add("user")
                End If
                If Session("role") = "GOD" Then
                    navigationSetup.UserRoles.Add("GOD")
                End If
                If Session("branding") = "Yes" Then
                    navigationSetup.UserRoles.Add("Branding")
                End If
                If Session("shopcart") = "Yes" Then
                    navigationSetup.UserRoles.Add("Shopcart")
                End If
               
            ElseIf Session("loggedin") = True And Session("sysaccess") = "user" Then
                navigationSetup.UserRoles.Add("loggedin")
            Else
                navigationSetup.UserRoles.Add("loggedout")
            End If
         
            navigationSetup.DataSource = Server.MapPath("navigationSetup.xml")
            navigationSetup.DataBind()
        End If
      
    End Sub
</script> 
<style type="text/css" media="all">
.MainNav {
	font-size: 100%;
	color: #4D4D4D;
	BORDER-RIGHT: 2px solid #ffffff; 
	BORDER-TOP: 2px solid #ffffff; 
	BORDER-LEFT: 2px solid #4d4d4d; 
	BORDER-BOTTOM: 2px solid #4d4d4d;
	background-color: #98B1C4;
	font-weight: bold;
	font-family: Georgia ;
	
	
	letter-spacing:0px;
	
	}

</style>
<table cellspacing="0" cellpadding="4"  border="0" name="Nav" width='90%'  bgcolor=#006295 height=555 class=test>
  <tr>
    <td  valign=top align=left width=120>
	<skm2:Menu 
	     
	 	  DefaultCssClass="MainNav"   
	 	   SubmenuCSSClass="subMenu"   
          id="navigationSetup" 
          Layout="vertical"     
	      cursor="pointer"      
	      MenuFadeDelay="3" 
	      ItemSpacing="3" 
	      ItemPadding="3" 
	      HighlightTopMenu="false"        
          runat="server" > 
   		<SelectedMenuItemStyle 
      		font-size = "9"
      		ForeColor="#ffffff" 
      		BackColor="#DA0000"> 
   		</SelectedMenuItemStyle> 
	</skm2:Menu> 
    </td>
     
  </tr>
  
</table>
  


