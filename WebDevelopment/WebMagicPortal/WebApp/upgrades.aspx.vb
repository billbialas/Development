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

namespace PageTemplate
    Public Class upgradesA
        Inherits PageTemplate
       
        public bcost,apcost as label
        public bcostarray(2) as string
        public pnlautopostcredits,pnlbranding as panel
        public dd_apqty as dropdownlist

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles mybase.load

            If Not (Page.IsPostBack) Then
            	session("url")="http://www.webmagicportal.com/pricing.aspx"
					
            		clearsessions()
                Dim msg As String
                msg = ""
                msg = msg & "<Script Language='JavaScript'>"
                msg = msg & "if (self != top) top.location = self.location;"
                msg = msg & "</Script>"
                Response.Write(msg)

            	if session("branding")="No" then
            		pnlbranding.visible=true
            	end if
            	apcost.text="1.00"
            	pnlautopostcredits.visible=false
            	getbrandingcost("branding")
					bcost.text = "$ " & session("brndcost")
            End If
            pagesetup()

        End Sub
        sub clearsessions()
        	session("brndcost")=""	
        	session("brndqty")=""
       
         
         end sub
        public sub getbrandingcost(upgrade as string)
        		Dim strConnection As String
				Dim sqlConn As SqlConnection
			   Dim sqlCmd As SqlCommand
				
				Dim strSql as String = "select datediff(d,getdate(),sub_expiredate) as 'qty',cast(datediff(d,getdate(),sub_expiredate)*.333 as decimal(10,2)) as 'cost' from tbl_users where uid='" & session("userid") & "'"
	 		     Try
	           strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
	           sqlConn = New SqlConnection(strConnection)
	           sqlCmd = New SqlCommand(strSql, sqlConn)
	           
	           sqlConn.Open()		           
	           Dim Sqldr as SqlDataReader = sqlCmd.ExecuteReader	
	           If Sqldr.read() Then
	           	
                 session("brndcost")= sqldr("cost")
                 session("brndqty")= sqldr("qty")
              
             End If          		           
	      
				   Catch ex As Exception
			    		Response.Write(ex.ToString())
			    	
			    	Finally
			       	sqlConn.Close()
				 	End Try
	        
        end sub
        
         Public Sub click_cancelup(ByVal Source As Object, ByVal E As EventArgs)
            if request.querystring("source") = "default" then
            	Response.redirect("default.aspx")
            elseif request.querystring("source") = "adcreate" then
            	if request.querystring("type")="quick" then
            		if request.querystring("adno")="" then
            			Response.redirect("createad.aspx?action=new&type=quick")
            		else
            			Response.redirect("createad.aspx?action=clone&type=quick&adno=" & request.querystring("adno"))
            		end if
            	else
            		if request.querystring("adno")="" then
            			Response.redirect("createad.aspx?action=new&type=complete")
            		else
            			Response.redirect("createad.aspx?action=clone&type=complete&adno=" & request.querystring("adno"))
            		end if
            	end if
            		
            else
            	response.redirect("branding.aspx")
            end if

        End Sub
        Public Sub click_upgrade(ByVal Source As Object, ByVal E As EventArgs)
       	 Dim ip As String = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
		  	 dim c32action, pcode, vcode as string
      	 If ip = "" Then
          	ip = Request.ServerVariables("REMOTE_ADDR")
     		 End If   
     		 pcode="none"
     		 vcode="67"
     		 c32action="branding"    
     		 response.redirect("https://shop.gochoiceone.com/cart.aspx?Qty=" & session("brndqty") & "&PartNo=WMPBrandUpgradeV1&pcode=" & pcode & "&vcode=" & vcode & "&action=" & c32action & "&UID=" & session("userid") )

             'createc32rec("branding")
             'Response.Write("<frameset ><frame src='" & System.Configuration.ConfigurationManager.AppSettings("CurrentwebURL") & "/bin/cart32.exe/choiceone-AddItem?Qty=" & session("brndqty") & "&PartNo=Productx'></frameset>")
          
        End Sub
        Public Sub click_appurchase(ByVal Source As Object, ByVal E As EventArgs)
             createc32rec("autopost")
             Response.Write("<frameset ><frame src='" & System.Configuration.ConfigurationManager.AppSettings("CurrentwebURL") & "/bin/cart32.exe/choiceone-AddItem?Qty=" & dd_apqty.selecteditem.text & "&PartNo=Producty'></frameset>")
          
        End Sub
        public sub createc32rec(type as string)
		    Dim strConnection As String
			 Dim sqlConn As SqlConnection
		    Dim sqlCmd As SqlCommand
			 Dim ip As String = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
         If ip = "" Then
             ip = Request.ServerVariables("REMOTE_ADDR")
         End If	
			  	
			  	Dim strSql as String
			  	strSql= "insert into tbL_c32process (c32p_ipno,c32p_date,c32p_action,c32p_url,c32p_uid) values ('" & ip & "',getdate(),'" & type & "','" & System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "','" & session("userid") & "')"
		 	   
		 	    
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
		      
			   Catch ex As Exception
		    		Response.Write(ex.ToString())
		    		exit sub
		    	Finally
		       	sqlConn.Close()
			 	End Try
    end sub
        
         Public Sub pagesetup()

         'width will be calculated automatically, but it is sometimes
           	layout.Width = System.Configuration.ConfigurationManager.AppSettings("ScreenWidth")
            leftNav.Width = System.Configuration.ConfigurationManager.AppSettings("ScreenLeftNav")            
            body.Height = System.Configuration.ConfigurationManager.AppSettings("ScreenBodyHeight")
          	body.Width = System.Configuration.ConfigurationManager.AppSettings("ScreenBodyWidth")
            layout.Border = System.Configuration.ConfigurationManager.AppSettings("ScreenBorder")
            footer.controls.add(LoadControl(System.Configuration.ConfigurationManager.AppSettings("ScreenFooter")))
            Header.Controls.Add(LoadControl(System.Configuration.ConfigurationManager.AppSettings("ScreenHeader")))
            leftNav.Controls.Add(LoadControl(System.Configuration.ConfigurationManager.AppSettings("ScreenLeftNav")))
                 	MiddleNav.Controls.Add(LoadControl(System.Configuration.ConfigurationManager.AppSettings("ScreenmiddleNav")))
     
            body.VAlign = "top"            
            leftNav.VAlign = "top"
            
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