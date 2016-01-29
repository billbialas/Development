<%@ Register TagPrefix="skm3" Namespace="skmMenu" Assembly="skmMenu" %> 
<link rel="stylesheet" href="_include/default.css" type="text/css">
<%-- Script Code Block --%> 
<script language="vb" runat="server"> 
   sub Page_Load(source as Object, E as EventArgs) 
    
      if not(Page.IsPostBack) then 
      if session("loggedin")= true and (session("sysaccess")="Agent" or session("sysaccess")="Partner") Then
           if session("Role") = "Administrator" then 
           		skmadminA.UserRoles.Add("Administrator")
           else
           		skmadminA.UserRoles.Add("agent")
           end if
         else if session("loggedin")= true and session("sysaccess")="user" Then
         	skmadminA.UserRoles.Add("loggedin")
         else
           skmadminA.UserRoles.Add("loggedout")
         end if
        
        skmadminA.DataSource = Server.MapPath("skmadmin.xml") 
        skmadminA.DataBind() 
      end if    
      
   end sub 
</script> 
<table cellspacing="1" cellpadding="0"  border="0" name="Nav1" width='99%'>
  				<tr>
    				<td  >
						<skm3:Menu 
	 						DefaultCssClass="sm3"
     	  					id="skmadminA" 
   	  					Layout="horizontal"
   	  					BackColor="#0035AE"
   	  					cursor="pointer"
   	  					Font-Names="Arial" 
   	  					font-color="#FFFFFF"
   	  					Font-Size="13"
   	  					MenuFadeDelay="3" 
   	  					ItemSpacing="0" 
   	  					ItemPadding="2" 
   	 	 				HighlightTopMenu="True"
   	  					SubmenuCSSClass="sm"
   	  					runat="server" > 
   						<SelectedMenuItemStyle 
      						font-size = "12"
      						ForeColor="#ffffff" 
      						BackColor="#DA0000"> 
   						</SelectedMenuItemStyle> 
						</skm3:Menu> 
    				</td>
       		</tr>
  			</table>