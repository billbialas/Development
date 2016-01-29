Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web
Imports System.Web.Security
Imports System.Configuration

namespace gmainlogin
Public Class glogin
   Inherits Page
	
	public txtUID as textbox
	public txtPASS as textbox
	public blnIsAuthenticated as Boolean = False
	public outMessage as System.Web.UI.WebControls.Label 
	public APPurl
	'public PersistCookie
	dim struserid as integer = 0
	dim program as string = ""
	
	 Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            If Not (Page.IsPostBack) Then

                Dim msg As String
                msg = ""
                msg = msg & "<Script Language='JavaScript'>"
                msg = msg & "if (self != top) top.location = self.location;"
                msg = msg & "</Script>"
                Response.Write(msg)
                removec32rec()
                APPurl.Attributes("src") = System.Configuration.ConfigurationManager.AppSettings("CurrentAppURL")
                Dim aCookie As HttpCookie
                Dim cookieName As String
                If Not Request.Cookies("Cart32-CHOICEONE") Is Nothing Then
                    removecartitems(Request.Cookies("Cart32-CHOICEONE").value)

                    cookieName = Request.Cookies("Cart32-CHOICEONE").Name
                    aCookie = New HttpCookie(cookieName)
                    aCookie.Expires = DateTime.Now.AddDays(-1)
                    Response.Cookies.Add(aCookie)

                    If Not Request.Cookies("C32-CHOICEONE-CustCode") Is Nothing Then
                        cookieName = Request.Cookies("C32-CHOICEONE-CustCode").Name
                        aCookie = New HttpCookie(cookieName)
                        aCookie.Expires = DateTime.Now.AddDays(-1)
                        Response.Cookies.Add(aCookie)
                    End If
                End If

            End If
        End Sub
        
        Public Sub click_signup(ByVal Source As Object, ByVal e As ImageClickEventArgs)
            createc32rec()
            'Dim redirecturl As String = "<frameset ><frame src='" & System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "/bin/cart32.exe/choiceone-AddItem?Qty=5&PartNo=ProductA&Item=Product+A&Price=10.00'></frameset>"
            Dim redirecturl As String = "gsignup.aspx"
            response.redirect(redirecturl)
        End Sub
    public sub createc32rec()
		    Dim strConnection As String
			 Dim sqlConn As SqlConnection
		    Dim sqlCmd As SqlCommand
			 Dim ip As String = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
         If ip = "" Then
             ip = Request.ServerVariables("REMOTE_ADDR")
         End If
		
			  	Dim strSql as String = "insert into tbL_c32process (c32p_ipno,c32p_date,c32p_action,c32p_url) values ('" & ip & "',getdate(),'Signup','" & System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "')"
		 		     Try
		           strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
		           sqlConn = New SqlConnection(strConnection)
		           sqlCmd = New SqlCommand(strSql, sqlConn)
		           
		           sqlConn.Open()		           
		           Dim Sqldr as SqlDataReader = sqlCmd.ExecuteReader	          		           
		      
			   Catch ex As Exception
		    		Response.Write(ex.ToString())
		    		exit sub
		    	Finally
		       	sqlConn.Close()
			 	End Try
    end sub
        Public Sub removec32rec()

            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim ip As String = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
            If ip = "" Then
                ip = Request.ServerVariables("REMOTE_ADDR")
            End If

            Dim strSql As String = "delete from tbL_c32process where c32p_ipno='" & ip & "' and convert(varchar(20),c32p_date,101) = convert(varchar(20), getdate(),101)"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)

                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

            Catch ex As Exception
                Response.Write(ex.ToString())
                Exit Sub
            Finally
                sqlConn.Close()
            End Try
        End Sub
	
	public Sub SubmitBtn_Click(Sender As Object, e As EventArgs) 
     Dim strConnection As String
	  Dim sqlConn As SqlConnection
     Dim sqlCmd As SqlCommand
	  'Dim sqlCmd2 As SqlCommand
	  Dim strUsr as String = txtUID.text
	  Dim strPwd as String = txtPASS.text
	 

	  	Dim strSql as String = "SELECT rtrim(password) as password, uid, type, role, company from tbl_users where UID='" &strUsr & "' and rtrim(password)='" & strPwd & "'"
 		'dim strSql2 as string = "SELECT * from contact where userid='" &strUsr & "' and password='" & strPwd & "'"
          Try
           strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
           'strConnection = ConfigurationSettings.AppSettings("ConnectionString")
           sqlConn = New SqlConnection(strConnection)
           sqlCmd = New SqlCommand(strSql, sqlConn)
          ' sqlCmd2 = New SqlCommand(strSql2, sqlConn)
      	   
           sqlConn.Open()
	   		Dim Sqldr as SqlDataReader = sqlCmd.ExecuteReader
            
           	if Sqldr.read() then
             if Sqldr("password") = strPwd then
               blnIsAuthenticated = True
            '    struserid = Sqldr("uid")
            '   'dim test as string = Sqldr("access")
               session("userid") = Sqldr("uid")
               session("sysaccess") = Sqldr("type")
               session("company") = Sqldr("Company")
                session("role") = Sqldr("role")
               if sqldr("role") IsNot DBNull.Value  then
               	session("Role") = sqldr("role")
             	end if
             end if
           	end if
     	
           ' Dim Sqldr2 as SqlDataReader = sqlCmd.ExecuteReader
      		'	if Sqldr2.read() then
            '   	'dim test as string = Sqldr("access")
           	'	end if
           
           
      
	   Catch ex As Exception
    		Response.Write(ex.ToString())
    		exit sub
    	Finally
       	sqlConn.Close()
	 	End Try

          if blnIsAuthenticated then
            session("loggedin")= "true"
            session("s_userloggedin")= "true"
   	  		session("s_userid")=struserid
   	  		Response.Cookies("Choiceone").Expires = #12/31/2110#
				Response.Cookies("Choiceone").value = struserid
			  						
   	  		'				Dim strClientIP As String
				'				strClientIP = Request.UserHostAddress()
   	  		'				
   	  		'				sqlCmd = New SqlCommand("sp_loguserin", sqlConn)
         	'				sqlCmd.CommandType = CommandType.StoredProcedure
            '
       	   '		 		dim prmuserid2 As New SqlParameter("@userid", SqlDbType.int)
        		'				prmuserid2.Value = struserid
       	 	'				sqlCmd.Parameters.Add(prmuserid2)
       	 	'				
       	 	'				Dim prmip As New SqlParameter("@ip", SqlDbType.varchar, 15)
        		'				prmip.Value = strClientIP
       	 	'				sqlCmd.Parameters.Add(prmip)
       	 	'				
       	 	'				Dim prmcookie As New SqlParameter("@cookie", SqlDbType.char, 150)
        		'				prmcookie.Value = ""
       	 	'				sqlCmd.Parameters.Add(prmcookie)
       	 	'				
            '
        		'				Try
         	'					sqlConn.Open()
        		'					sqlCmd.ExecuteNonQuery()
            '					
            ' 					sqlConn.Close()
         
         
         	'					Catch ex As Exception
      		'					response.write ("ERROR4")
      		'					Exit sub
   	  		'				End Try
   	  						
   	  	'Check if user has listing steps to complete.  If so redirect to myspace
   	  	'	strSql  = "SELECT * from  tbl_ClientListingSteps where UID='" &struserid & "' " & _
   	  	'	          "and status='Not Complete'"
 			'	Try
         '  		strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
         '  		'strConnection = ConfigurationSettings.AppSettings("ConnectionString")
         '  		sqlConn = New SqlConnection(strConnection)
         '  		sqlCmd = New SqlCommand(strSql, sqlConn)
         ' 	
         '  		sqlConn.Open()
	   	'		Dim Sqldr2 as SqlDataReader = sqlCmd.ExecuteReader
         '   
         '  		if Sqldr2.read() then
         '  			session("s_liststepscompleted")= "false"
         '  		end if
   	  	'		sqlConn.Close()
	
	   	'		Catch ex As Exception
      	'			response.write ("ERROR")
      	'		Exit sub
    	  	'	End Try		
   	 	'response.redirect ("bms_default.aspx")
        	response.write("EERE")
         FormsAuthentication.RedirectFromLoginPage(txtUID.text,false)
        else
         outMessage.text = "Invalid Id or Password.  Please renter"
        end if         
        
    End Sub
    
    	public sub removecartitems(cookie as string)
			Dim strConnection As String
			Dim sqlConn As SqlConnection
			Dim sqlCmd As SqlCommand
		  	Dim strSql as String = "delete from cart32.dbo.cart where COOKIE='" & cookie & "'"
		      Try
			    strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
			           sqlConn = New SqlConnection(strConnection)
			           sqlCmd = New SqlCommand(strSql, sqlConn)
			      	   
			           sqlConn.Open()
				   		Dim Sqldr as SqlDataReader = sqlCmd.ExecuteReader
				   		  Catch ex As Exception
    		
    		Catch ex As Exception
    			Response.Write(ex.ToString())
    		Finally
       		sqlConn.Close()
	 		End Try
            
		end sub
		
End Class
end namespace