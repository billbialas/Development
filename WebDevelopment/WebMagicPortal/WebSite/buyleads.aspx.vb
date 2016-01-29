imports System
imports System.Collections
imports System.Data.SqlClient
imports System.ComponentModel
imports System.Data
imports System.Drawing
imports System.Drawing.Color 
imports System.Web
imports System.Web.SessionState
imports System.Web.UI
imports System.Web.UI.WebControls
imports System.Web.UI.HtmlControls
imports System.xml
imports System.Configuration
imports system.Globalization
imports system.net.mail
Imports System.Text

Namespace PageTemplate
    Public Class buyleads
        Inherits page
        
        public pnlleadnotavail as panel
     		public emadd,weblink,ldno as label
     		
        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

            If Not (Page.IsPostBack) Then
          If (Not Request.Cookies("Cart32-CHOICEONE") Is Nothing) Then
    					Dim myCookie As HttpCookie
    					myCookie = New HttpCookie("Cart32-CHOICEONE")
    					myCookie.Expires = DateTime.Now.AddDays(-5)
    					Response.Cookies.Add(myCookie)
				End If
				
				if (Not Request.Cookies("C32-CHOICEONE-CustCode") Is Nothing) Then
    					Dim myCookie As HttpCookie
    					myCookie = New HttpCookie("C32-CHOICEONE-CustCode")
    					myCookie.Expires = DateTime.Now.AddDays(-5)
    					Response.Cookies.Add(myCookie)
				End If
				if (Not Request.Cookies("C32-CHOICEONE-NoRegister") Is Nothing) Then
    					Dim myCookie As HttpCookie
    					myCookie = New HttpCookie("C32-CHOICEONE-NoRegister")
    					myCookie.Expires = DateTime.Now.AddDays(-5)
    					Response.Cookies.Add(myCookie)
				End If
					Dim myCookieA As HttpCookie
					myCookieA = New HttpCookie("C32-CHOICEONE-NoRegister")
					myCookieA.value="Y"
					myCookieA.Expires = DateTime.Now.AddDays(1)
				 	Response.Cookies.Add(myCookieA)

            	if checkifleadavail() then
            		createc32rec()            		
            		response.write("<frameset ><frame src='" & System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "/bin/cart32.exe/choiceone-AddItem?Qty=1&PartNo=Productc'></frameset>")

            	else
            		pnlleadnotavail.visible=true
            		              weblink.text="<a href=" & System.Configuration.ConfigurationManager.AppSettings("CurrentwebURL") & "/gsignup.aspx>Web Magic Portal</a>"
            	end if
                               

            End If
        End Sub
        
        public function checkifleadavail() as boolean
         	Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "Select * from dbo.tbl_leads where tbl_leads_pk = '" & Request.QueryString("id") & "'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                	if sqldr("ld_pstatus") = "Lead Sold" then
                		return false
                	else
                		return true
                	end if
                end if
            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try

        
        
        end function
        
         Public Sub createc32rec()

            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            dim c32action as string
            Dim ip As String = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
            If ip = "" Then
                ip = Request.ServerVariables("REMOTE_ADDR")
            End If            
            c32action="LeadPurchase"            
            

            Dim strSql As String = "insert into tbL_c32process (c32p_ipno,c32p_date,c32p_action,c32p_url,c32p_uid) values ('" & ip & "',getdate(),'" & c32action & "','" & request.querystring("id")  & "','" & request.querystring("email") & "')"
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
       
    End Class
End Namespace