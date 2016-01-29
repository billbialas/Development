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

        Public ddbasic, ddbranding As dropdownlist

   
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
					removec32rec()
					session("url")="http://www.webmagicportal.com/pricing.aspx"
					
				
				
				
            End If
		end sub	 
		
		
   public sub removec32rec()
		    Dim strConnection As String
			 Dim sqlConn As SqlConnection
		    Dim sqlCmd As SqlCommand
			 Dim ip As String = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
         If ip = "" Then
             ip = Request.ServerVariables("REMOTE_ADDR")
         End If
		
			  	Dim strSql as String = "delete from tbL_c32process where c32p_ipno='" & ip & "' and convert(varchar(20),c32p_date,101) = convert(varchar(20), getdate(),101)"
		 		     Try
		           
	 		     	strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
	 		   
		           sqlConn = New SqlConnection(strConnection)
		           sqlCmd = New SqlCommand(strSql, sqlConn)
		           
		           sqlConn.Open()		           
		           Dim Sqldr as SqlDataReader = sqlCmd.ExecuteReader	          		           
				       If Sqldr.Read() Then
		               
		               end if
               
               
			   Catch ex As Exception
		    		Response.Write(ex.ToString())
		    		exit sub
		    	Finally
		       	sqlConn.Close()
			 	End Try
    end sub

	  	
	  	public Sub AddproductA(Source As Object, e As ImageClickEventArgs)
	    			session("PG_Type")="true"
	    		
            'response.write("<frameset ><frame src='" & System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "/bin/cart32.exe/choiceone-AddItem?Qty=" & ddbasic.selecteditem.value & "&PartNo=ProductA&Item=Basic+Subscription&Price=24.99'></frameset>")
				'response.redirect(System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "/bin/cart32.exe/choiceone-AddItem?Qty=" & ddbasic.selecteditem.value & "&PartNo=ProductA&Item=Basic+Subscription&Price=24.99'")
				Dim ip As String = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
				  	 dim c32action, pcode, vcode as string
            	 If ip = "" Then
                	ip = Request.ServerVariables("REMOTE_ADDR")
           		 End If   
           		 pcode="none"
           		 vcode="67"
           		 c32action=request.querystring("action")      
           		' response.redirect("https://shop.gochoiceone.com/direct.aspx?Qty=" & ddbasic.selecteditem.value & "&PartNo=WMPBasicV2&pcode=" & pcode & "&vcode=" & vcode & "&ip=" & ip & "&action=" & c32action & "&url=" & session("url") )
		 			 response.redirect("https://shop.gochoiceone.com/cart.aspx?Qty=" & ddbasic.selecteditem.value & "&PartNo=WMPBasicV2&pcode=" & pcode & "&vcode=" & vcode & "&action=" & c32action & "&UID=" & session("userid") )
	
				
	   end sub
        Public Sub Addproductb(ByVal Source As Object, ByVal e As ImageClickEventArgs)
            session("PG_Type") = "true"

            'response.write("<frameset ><frame src='" & System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "/bin/cart32.exe/choiceone-AddItem?Qty=" & ddbasicb.selecteditem.value & "&PartNo=Productb&Item=BASIC+Subscription+Branding&Price=34.99'></frameset>")
 	 			Dim ip As String = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
			  	 dim c32action, pcode, vcode as string
         	 If ip = "" Then
             	ip = Request.ServerVariables("REMOTE_ADDR")
        		 End If   
        		 pcode="none"
        		 vcode="67"
        		 c32action=request.querystring("action")   
        		 'response.redirect("https://shop.gochoiceone.com/direct.aspx?Qty=" & ddbasic.selecteditem.value & "&PartNo=WMPBrandingV2&pcode=" & pcode & "&vcode=" & vcode & "&ip=" & ip & "&action=" & c32action & "&url=" & session("url") )
		 		 response.redirect("https://shop.gochoiceone.com/cart.aspx?Qty=" & ddbranding.selecteditem.value & "&PartNo=WMPBrandingV2&pcode=" & pcode & "&vcode=" & vcode & "&action=" & c32action & "&UID=" & session("userid") )
	

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