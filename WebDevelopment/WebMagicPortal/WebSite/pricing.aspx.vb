Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web
Imports System.Web.Security
Imports System.Configuration

namespace PageTemplate
Public Class pricing
   Inherits PageTemplate
	
   Public ddbasic, ddbasicb,ddbranding As dropdownlist
	
	 		Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
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
            pagesetup()
        End Sub

	  	public Sub AddproductA(Source As Object, e As ImageClickEventArgs)
	    			session("PG_Type")="true"
	    		createc32rec()
             'response.write("<frameset ><frame src='" & System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "/bin/cart32.exe/choiceone-AddItem?Qty=" & ddbasic.selecteditem.value & "&PartNo=ProductA&Item=Basic+Subscription&Price=34.99'></frameset>")
					       response.redirect(System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "/bin/cart32.exe/choiceone-AddItem?Qty=" & ddbasic.selecteditem.value & "&PartNo=ProductA&Item=Basic+Subscription&Price=24.99'")
	
				
	   end sub
        Public Sub Addproductb(ByVal Source As Object, ByVal e As ImageClickEventArgs)
            session("PG_Type") = "true"
				createc32rec()
            response.write("<frameset ><frame src='" & System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "/bin/cart32.exe/choiceone-AddItem?Qty=" & ddbranding.selecteditem.value & "&PartNo=Productb&Item=BASIC+Subscription+Branding&Price=44.99'></frameset>")


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
      
         Public Sub pagesetup()

         'width will be calculated automatically, but it is sometimes
           	layout.Width = System.Configuration.ConfigurationManager.AppSettings("ScreenWidth")
            'leftNav.Width = System.Configuration.ConfigurationManager.AppSettings("ScreenLeftNav")            
            body.Height = System.Configuration.ConfigurationManager.AppSettings("ScreenBodyHeight")
          	body.Width = System.Configuration.ConfigurationManager.AppSettings("ScreenBodyWidth")
            layout.Border = System.Configuration.ConfigurationManager.AppSettings("ScreenBorder")
            footer.controls.add(LoadControl(System.Configuration.ConfigurationManager.AppSettings("ScreenFooter")))
            Header.Controls.Add(LoadControl(System.Configuration.ConfigurationManager.AppSettings("ScreenHeader")))
            'leftNav.Controls.Add(LoadControl(System.Configuration.ConfigurationManager.AppSettings("ScreenLeftNav")))
           
            body.VAlign = "top"            
            'leftNav.VAlign = "top"
            
            'LeftNav.Controls.Add(new LiteralControl("Some text."))
 				'rightNav.VAlign = "top"           
            'adjust size of LeftNav (just for the heck of it)           

            'LeftNav.Controls.Add(LoadControl("navigation.ascx"));
            'LeftNav.Controls.Add(new LiteralControl("Some text."));

            'adjust size of LeftNav (just for the heck of it)
            'LeftNav.Width = "100";

            'RightNav.Controls.Add(LoadControl("quicklink1.ascx"))
            'MiddleNav.Controls.Add(LoadControl("userid.ascx"))
            

        End Sub

		
End Class
end namespace