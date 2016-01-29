<%@ Register TagPrefix="skm2" Namespace="skmMenu" Assembly="skmMenu" %> 
<link rel="stylesheet" href="../_include/default.css" type="text/css">
<% @Import Namespace="System.Data.SqlClient" %>
<%-- Script Code Block --%> 
<script language="vb" runat="server"> 
   
    
    Sub Page_Load(ByVal source As Object, ByVal E As EventArgs)
    
        If Not (Page.IsPostBack) Then
            If Session("loggedin") = True Then
                If Session("sysaccess") = "Agent" Then
                    navigation2.UserRoles.Add("agent")
                ElseIf Session("sysaccess") = "Broker" Then
                    navigation2.UserRoles.Add("Broker")
                End If
                If Session("Role") = "Administrator" Then
                    navigation2.UserRoles.Add("Administrator")
                End If
 
                If Session("sysaccess") = "Partner" Then
                    navigation2.UserRoles.Add("Partner")
                End If
                If Session("role") = "user" Then
                    navigation2.UserRoles.Add("user")
                End If
                If Session("role") = "GOD" Then
                    navigation2.UserRoles.Add("GOD")
                End If
                If Session("branding") = "Yes" Then
                    navigation2.UserRoles.Add("Branding")
                End If
                
            ElseIf Session("loggedin") = True And Session("sysaccess") = "user" Then
                navigation2.UserRoles.Add("loggedin")
                
                
            Else
                navigation2.UserRoles.Add("loggedout")
            End If
            If Session("beta") = "Yes" Then
                navigation2.UserRoles.Add("Beta")
            End If
            
            LogoareaN.Attributes("src") = System.Configuration.ConfigurationManager.AppSettings("CurrentwebURL") & "/logos/company/" & BindWelcome()
         
                
            navigation2.DataSource = Server.MapPath("navigation2.xml")
            navigation2.DataBind()
        End If
      
    End Sub
    
    function  BindWelcome() as string

            Dim strUID As String = Session("userid")
            Dim strSql As String = "SELECT * from tbl_users join tbl_company on co_tbl_pk = company_pk where UID='" & session("userid") & "'"
            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                   
                    If Sqldr("co_logo") IsNot DBNull.Value Then
                        return Sqldr("co_logo")
                    Else
                        return "default.jpg"
                    End If
                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        End function
</script> 
<style type="text/css" media="all">
.MainNav {
	font-size: 90%;
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

<table cellspacing="0" cellpadding="4"  border="0" name="Nav" width='90%' bgcolor=#006295 height=555 class=test>
  <tr>
    <td valign=top align=left width=120>
	<skm2:Menu 	 
      DefaultCssClass="MainNav"  
      id="navigation2" 
      Layout="vertical"     
      cursor="pointer"      
      MenuFadeDelay="3" 
      ItemSpacing="4" 
      ItemPadding="3" 
      HighlightTopMenu="true"
         
      runat="server" > 
   		<SelectedMenuItemStyle 
      		font-size = "9"
      		ForeColor="#ffffff" 
      		BackColor="#DA0000"> 
   		</SelectedMenuItemStyle> 
	</skm2:Menu> 
    </td>
     
  </tr>
  <tr>
  	<td>&nbsp</td>
  </tr>
 </table>
      <img id="LogoareaN" alt="" visible=false src="images/default.jpg" height=60 width=80 border="0" runat=server></td>
  


