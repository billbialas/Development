<% @Import Namespace="System.Net.Mail" %>
<% @Import Namespace="System.Data.SqlClient" %>
<% @Import Namespace="System.Data" %>
<% @Import Namespace="System.Configuration" %>
 <script language="vb" runat="server"> 

     Public Sub Page_Load(ByVal source As Object, ByVal E As EventArgs)
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
         ViewState("restore") = "false"
         FormsAuthentication.SignOut()
         Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") )
     End Sub


</script> 
