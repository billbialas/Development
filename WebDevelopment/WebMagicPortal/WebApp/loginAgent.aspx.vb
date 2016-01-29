Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web
Imports System.Web.Security
Imports System.Configuration
Imports System.Net.Mail

Namespace database1

    Public Class processloginAgent
        Inherits Page

        Public pnlalreadyloged, pnlsecretcode As Panel
        Public txtUsr, txtcode As TextBox
        Public txtPwd As TextBox
        Public blnIsAuthenticated As Boolean = False
        Public outMessage As System.Web.UI.WebControls.Label
        Dim struserid As Integer = 0
        Dim program As String = ""
        Public resetlogon As LinkButton
        public chkremem as checkbox

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            If Not (Page.IsPostBack) Then
            	checkcookie()
                If Request.QueryString("source") = "NP" Then
                    If Not checkifloggedin() Then
                        setsessions()
                        updatelastlogin()
                        FormsAuthentication.RedirectFromLoginPage(Request.QueryString("id"), False)
                    End If

                End If
            End If
        End Sub
	
			public sub checkcookie()
				chkremem.checked=false
				if (Not Request.Cookies("webmagicportal") Is Nothing) Then
    					txtUsr.Text = Request.Cookies("webmagicportal")("UID") 
    					txtPwd.focus() 
				End If
			
			end sub


        Public Sub setsessions()
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strUsr As String = txtUsr.Text
            Dim strPwd As String = txtPwd.Text
            Dim strSql As String = "SELECT shopcart,users_tbl_PK,status,betauser,brandingPurchased,convert(varchar(20),sub_expiredate,101) as 'expdate', rtrim(password) as password, uid, type, role, company, industry, fname,lname,package,cart32id,company_pk from tbl_users where UID='" & Request.QueryString("id") & "'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    blnIsAuthenticated = True
                    Session("userid") = Sqldr("uid")
                    Session("c32id") = Sqldr("cart32id")
                    Session("sysaccess") = Sqldr("type")
                    Session("company") = Sqldr("Company")
                    Session("role") = Sqldr("role")
                    Session("industry") = Sqldr("industry")
                    Session("Agentname") = Sqldr("fname") & " " & Sqldr("lname")
                    Session("AgentPK") = Sqldr("users_tbl_PK")
                    Session("subexpdate") = Sqldr("expdate")
                    Session("branding") = Sqldr("brandingPurchased")
                    Session("beta") = Sqldr("betauser")
                    If Sqldr("package") IsNot DBNull.Value Then
                        Session("package") = Sqldr("package")
                    Else
                        Session("package") = "Basic"
                    End If
                    If Sqldr("company_pk") IsNot DBNull.Value Then
                        Session("company_pk") = Sqldr("company_pk")
                    Else
                        Session("company_pk") = "0"
                    End If
                    If Sqldr("shopcart") IsNot DBNull.Value Then
                        Session("shopcart") = Sqldr("shopcart")
                    Else
                        Session("shopcart") = "No"
                    End If
                    Session("ustat") = Sqldr("status")
                   session("uimgdir")=  Left(Sqldr("uid"), Len(Sqldr("uid")) - 4) & "IMG"
                    
                    Dim MyCookie As New HttpCookie("bb")
					      MyCookie.Value =   Left(Sqldr("uid"), Len(Sqldr("uid")) - 4) & "IMG"
					      Response.Cookies.Add(MyCookie)   
					     
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try

        End Sub

        Public Sub SubmitBtn_Click(ByVal Sender As Object, ByVal e As EventArgs)
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strUsr As String = txtUsr.Text
            Dim strPwd As String = txtPwd.Text
            Dim strSql As String = "SELECT shopcart,readsysmes,users_tbl_PK,status,betauser,brandingPurchased,convert(varchar(20),sub_expiredate,101) as 'expdate',rtrim(password) as password, uid, type, role, company, industry, fname,lname,package,cart32id,company_pk from tbl_users where UID='" & strUsr & "' and rtrim(password)='" & strPwd & "'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    If Sqldr("password") = strPwd Then
                        blnIsAuthenticated = True
                        Session("userid") = Sqldr("uid")
                        Session("c32id") = Sqldr("cart32id")
                        Session("sysaccess") = Sqldr("type")
                        Session("company") = Sqldr("Company")
                        Session("role") = Sqldr("role")
                        Session("industry") = Sqldr("industry")
                        Session("Agentname") = Sqldr("fname") & " " & Sqldr("lname")
                        Session("AgentPK") = Sqldr("users_tbl_PK")
                        Session("subexpdate") = Sqldr("expdate")
                        Session("branding") = Sqldr("brandingPurchased")
                        Session("beta") = Sqldr("betauser")
                        If Sqldr("package") IsNot DBNull.Value Then
                            Session("package") = Sqldr("package")
                        Else
                            Session("package") = "Basic"
                        End If
                        If Sqldr("company_pk") IsNot DBNull.Value Then
                            Session("company_pk") = Sqldr("company_pk")
                        Else
                            Session("company_pk") = "0"
                        End If
                        If Sqldr("shopcart") IsNot DBNull.Value Then
                        	Session("shopcart") = Sqldr("shopcart")
	                    Else
	                        Session("shopcart") = "No"
	                    End If
                        Session("ustat") = Sqldr("status")
                        session("readsysmes") = Sqldr("readsysmes")
   							session("uimgdir")=  Left(Sqldr("uid"), Len(Sqldr("uid")) - 4) & "IMG"
                  Dim MyCookie As New HttpCookie("bb")
					      MyCookie.Value =   Left(Sqldr("uid"), Len(Sqldr("uid")) - 4) & "IMG"
					      Response.Cookies.Add(MyCookie)   
                    End If
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try

            If blnIsAuthenticated Then
            	if chkremem.checked then             	
		         	Dim newCookie As HttpCookie = New HttpCookie("webmagicportal")
						newCookie.Values.Add("UID", strUsr)					
						newCookie.Expires = DateTime.Now.AddDays(180)
						Response.Cookies.Add(newCookie)            	
            	end if
            	
            	
                updatelastlogin()
                if checksysmessage() then 
                	session("forcesys")="Yes"
                end if
                Session("loggedin") = "true"
                Session("s_userloggedin") = "true"
                Session("s_userid") = struserid
                Response.Cookies("Choiceone").Expires = #12/31/2110#
                Response.Cookies("Choiceone").Value = struserid

                If checkifloggedin() Then
                    pnlalreadyloged.Visible = True
                    outMessage.Text = "Logged in at another station"
                Else
                    loginuserin()
                    FormsAuthentication.RedirectFromLoginPage(txtUsr.Text, False)
                End If
            Else
                outMessage.Text = "Invalid Id or Password.  Please renter"
            End If

        End Sub
        public function  checksysmessage()
         	Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select * from tbl_miscstuff where misc_type = 'SysMessageDsp'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    If Sqldr("misc_text") = "Y" Then
           					return true
           			  Else
                        Return false
                    End If
                Else
                    Return False
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
        end function
        
        Public Function checkifloggedin() As Boolean
            Dim ip As String = Request.ServerVariables("REMOTE_ADDR")

            If ip = "" Then
                ip = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
                
            End If
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select * from tbl_currentlogons where lg_uid = '" & Session("userid") & "'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    If Sqldr("lg_ipnumber") = ip Then
                        Session("loggedinalready") = "yes"
                        Return False
                    Else
                        Return True
                    End If
                Else
                    Session("loggedinalready") = "no"
                    Return False
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
        End Function

        Sub loginuserin()
            If Session("loggedinalready") = "no" Then
                Dim ip As String =   Request.ServerVariables("REMOTE_ADDR")
                'If ip = "" Then
                 '  ip = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
                    
                'End If
                Dim strConnection As String
                Dim sqlConn As SqlConnection
                Dim sqlCmd As SqlCommand
                Dim strSql As String = "insert dbo.tbl_currentlogons (lg_uid,lg_logindate,lg_ipnumber) values ('" & Session("userid") & "', getdate(), '" & ip & "')"
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
            End If
        End Sub


        Public Sub click_resetlogon(ByVal sender As Object, ByVal e As EventArgs)
				pnlalreadyloged.visible=false
            pnlsecretcode.Visible = True
            outMessage.Text = "Please Enter Your Secret Code"
       


        End Sub


        Public Sub click_doreset(ByVal sender As Object, ByVal e As EventArgs)
            If checksecretcode() Then
                pnlsecretcode.Visible = True
                subresetlogon()
                outMessage.Text = "Session reset please try again."
                pnlsecretcode.Visible = False
            Else
                outMessage.Text = "Invalid Code."
                txtcode.text=""
                'pnlsecretcode.Visible = False
            End If

        End Sub

        Public Sub subresetlogon()
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "delete from dbo.tbl_currentlogons where lg_uid='" & Session("userid") & "'"
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
        End Sub

        Function checksecretcode() As Boolean

            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select secretcode from dbo.tbl_users where UID='" & txtUsr.Text & "'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    If Sqldr("secretcode") IsNot DBNull.Value Then
                        If Sqldr("secretcode") = txtcode.text Then
                            Return True
                        Else
                            Return False
                        End If
                    Else
                        Return False
                    End If
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try

        End Function

        Public Sub click_emailpassword(ByVal sender As Object, ByVal e As EventArgs)
            If txtUsr.Text <> "" Then
                Session("lemail") = ""
                getemail()
                If Session("lemail") <> "NO USER" Then


                    Dim mail As New MailMessage()

                    'Set the properties - send the email to the person who filled out the
                    mail.From = New MailAddress("System@gochoiceone.com")
                    mail.To.Add(Session("lemail"))

                    'Set the body

                    mail.Body = "Your Password is " & Session("lpass")
                    mail.Subject = "Lost Password"

                    'send the message
                    Dim smtp As New SmtpClient("smtp.comcast.net")
                    smtp.Send(mail)

                    outMessage.Text = "Password Sent to " & Session("lemail")
                Else
                    outMessage.Text = "User ID not found"
                End If


            Else
                outMessage.Text = "User ID can not be blank"
            End If


        End Sub


        Public Sub getemail()

            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select email, password from dbo.tbl_users where UID='" & txtUsr.Text & "'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    If Sqldr("email") IsNot DBNull.Value Then
                        Session("lemail") = Sqldr("email")
                        Session("lpass") = Sqldr("password")
                    End If
                Else
                    Session("lemail") = "NO USER"
                End If


            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try

        End Sub
        Public Sub updatelastlogin()
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "update dbo.tbl_users " _
                     & "set lastlogindate = case when ((select lastlogindate from dbo.tbl_users where UID='" & session("userid") & "') is null) then " _
                     & "getdate() else " _
                     & "(select currentlogindate from dbo.tbl_users where UID='" & session("userid") & "') " _
                     & "end,currentlogindate=getdate() where UID='" & session("userid") & "'"
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

        End Sub
    End Class
End Namespace