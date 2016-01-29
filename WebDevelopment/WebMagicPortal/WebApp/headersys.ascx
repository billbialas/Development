<% @Import Namespace="System.Net.Mail" %>
<% @Import Namespace="System.Data.SqlClient" %>
<% @Import Namespace="System.Data" %>
<% @Import Namespace="System.Configuration" %>
<script language="JavaScript" src="../_include/default.js"></script>
<link rel="stylesheet" href="../_include/default.css" type="text/css">

<script language="vb" runat="server">
	  'Protected WithEvents lbl_UID As System.Web.UI.WebControls.Label
    
    sub Page_Load(source as Object, E as EventArgs) 
    
      if not(Page.IsPostBack) then 
      	   	lbl_UID.text = session("userid")
      	lbl_co.text = session("company")
      	lbl_expdate.text = Session("subexpdate")
      	lbl_subtype.text =Session("package")
    
      end if    
      
   end sub 
   
   
	sub logoutbtnclick(sender As Object, e As EventArgs)
		Dim strConnection As String
         Dim sqlConn As SqlConnection
         Dim sqlCmd As SqlCommand
         Dim strSql As String = "delete from dbo.tbl_currentlogons where lg_uid ='" & session("userid") & "'"
         Try
             strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
             sqlConn = New SqlConnection(strConnection)
             sqlCmd = New SqlCommand(strSql, sqlConn)
             sqlConn.Open()
             Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

         Catch ex As Exception
             Response.Write(ex.ToString())
         Finally
             sqlConn.Close()
         End Try
         Session.Abandon()
		FormsAuthentication.SignOut()
		response.redirect (System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") )
	end sub
	
   
   
   
</script>
<div id="header">
<table border=0 width="100%" cellspacing=0 cellpadding=0>
  <tr>	
    
    <td width=90><img  height="50" width="90" alt="" src="../images/Magic2.png" border="0"></td>
    <td  valign=top> 
        <table>
            <tr>
                <td valign=top width=250><h1>WebMagicPortal.com</h1></td>
                <td><h2>Version 5.2</h2></td>
           </tr>
          
       </table>
    </td> 				
  </tr>
  
</table>
</div>
<div id="infobar">
<table width=100% border=0 cellspacing=0 cellpadding=0>
	<tr>
		<td  width=108 bgcolor=006295></td>
		<td width=340><h1>User:&nbsp<font color=white><asp:label id="lbl_UID" runat="server" />&nbsp&nbsp
		    <font color=white><asp:label id="lbl_co" visible=false runat="server" /></font><asp:linkbutton id="wlogout" Text= "[Log Out]" 
                   runat="server" Font-Bold="True" cssclass=linkbuttonsred onClick="logoutbtnclick" /></h1></td>
    	<td width=31% ><h1>Subscription Type: &nbsp&nbsp<font color=white><asp:label id="lbl_subtype" runat="server" /></h1></td>
    	<td width=22% ><h1>Subscription Expire Date:&nbsp&nbsp<font color=white><asp:label id="lbl_expdate" runat="server" /></font></h1></td>		
		<td align=right ><h1>InActivity logoff:</h1></td>
		<td ><h1><font color=white>&nbsp<span id="SpnDisp"></span></font></h1></td>
	</tr>
</table>
</div>
   

