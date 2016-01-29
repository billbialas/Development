imports System
imports System.Collections
imports System.ComponentModel
imports System.Data
imports System.Drawing
imports System.Web
imports System.Web.SessionState
imports System.Web.UI
imports System.Web.UI.WebControls
imports System.Web.UI.HtmlControls
Imports System.Data.SqlClient
Imports System.Web.Security
Imports System.Configuration

namespace PageTemplate
	public class gsignup 
	   inherits page

        Public ddbasic, ddbasicb As dropdownlist

   
	   private Sub Page_Load(byval sender as object, byval e As EventArgs) handles mybase.load
			dim CartCookieID as string = ""
      	dim CartCustCode as string = ""
      	dim userid as integer =0
      	Dim strConnection As String = ""
		  	Dim sqlConn As SqlConnection
	     	Dim sqlCmd As SqlCommand
		  	'Dim sqlCmd2 As SqlCommand
		  	Dim rightNow as DateTime = DateTime.Now
            If Not (Page.IsPostBack) Then

                'Clear out frame from shopping cart
                Dim msg As String
                msg = ""
                msg = msg & "<Script Language='JavaScript'>"
                msg = msg & "if (self != top) top.location = self.location;"
                msg = msg & "</Script>"
                Response.Write(msg)
			
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
						'Dim myCookieA As HttpCookie
	    			
						'	myCookieA = New HttpCookie("Cart32-CHOICEONE")
	    				'	mycookieA.value="98989898"
	    				'	myCookieA.Expires = #12/31/2110#'
	    				'	DateTime.Now.AddDays(5)
	    				'	Response.Cookies.Add(myCookieA)
						
						
						'Dim myCookieB As HttpCookie
						'myCookieB = New HttpCookie("C32-CHOICEONE-CustCode")
    					'mycookieB.value="999999999"
    					'myCookieB.Expires = #12/31/2110#
    					''DateTime.Now.AddDays(5)
    					'Response.Cookies.Add(myCookieB)	
					
				
            End If
		end sub	 
	  	
	  	public Sub AddproductA(Source As Object, e As ImageClickEventArgs)
	    			session("PG_Type")="true"
	    		createc32rec()
            'response.write("<frameset ><frame src='" & System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "/bin/cart32.exe/choiceone-AddItem?Qty=" & ddbasic.selecteditem.value & "&PartNo=ProductA&Item=Basic+Subscription&Price=24.99'></frameset>")
					       response.redirect(System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "/bin/cart32.exe/choiceone-AddItem?Qty=" & ddbasic.selecteditem.value & "&PartNo=ProductA&Item=Basic+Subscription&Price=24.99'")
	
				
	   end sub
        Public Sub Addproductb(ByVal Source As Object, ByVal e As ImageClickEventArgs)
            session("PG_Type") = "true"
				createc32rec()
            response.write("<frameset ><frame src='" & System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "/bin/cart32.exe/choiceone-AddItem?Qty=" & ddbasicb.selecteditem.value & "&PartNo=Productb&Item=BASIC+Subscription+Branding&Price=34.99'></frameset>")


        End Sub
        
         Public Sub createc32rec()

            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            dim c32action as string
            Dim ip As String = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
            If ip = "" Then
                ip = Request.ServerVariables("REMOTE_ADDR")
            End If            
            c32action="Signup"
           

            Dim strSql As String = "insert into tbL_c32process (c32p_ipno,c32p_date,c32p_action,c32p_url,c32p_uid) values ('" & ip & "',getdate(),'" & c32action & "','" & System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "','" & Session("userid") & "')"
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

	   
        end class
end namespace