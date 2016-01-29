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
Imports System.Configuration
Imports System.Web.Security
Imports FreeTextBoxControls


namespace PageTemplate
	public class default1
	   inherits PageTemplate
	   
        Public Welcomemessage, label1, screenresolution, totnewleads, totaltasks, subscriptstat,lblsysinfo As Label
        Protected WithEvents leadsneedingfollowup, leadsaction, test, tasksdue, newleads As System.Web.UI.WebControls.DataGrid
        Public pnl_inactiveuser, pnl_activeuser, pnl5daywarn, pnl_upgrade,pnlsysinfo,pnlallinfo, pnlhelp As Panel
       
        Public Duetoday, AllOpen, Alltasks, leadnew, lead15, lead30 As LinkButton
        Public Logoarea As HtmlControls.HtmlImage
        Public btn_renewcont, btn_renewA As Button
        Public imgbtnupgrade As ImageButton
        public txtsysnotes as textbox
        public txthelp as FreeTextBox
        public  pnlreminders as panel
        public lblunpubads,lblOrderBy as label

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

            If Not (Page.IsPostBack) Then
					clearsessions()
                Dim msg As String
                msg = ""
                msg = msg & "<Script Language='JavaScript'>"
                msg = msg & "if (self != top) top.location = self.location;"
                msg = msg & "</Script>"
                Response.Write(msg)
					bindsysmessage()
					bindhelp()
					
					
                'Response.Write(Request.Browser.Browser)
                removec32rec()
                If Request.QueryString("action") = "renewed" Then
                    updateexpiredate()
                    loguserin()
                    Response.Redirect("default.aspx")

                End If
                If Request.QueryString("action") = "branding" Then
                	  updatebrandingflag(Request.QueryString("uid"))
                    loguserin()
                    pnl_upgrade.visible=true
                    pnl_activeuser.visible=false
                elseif Request.QueryString("action") = "autopost" Then
                		loguserin()
                		updatecredits(Request.QueryString("uid"))
                		response.redirect("admanager.aspx")
                
                else
                If checkbillingstat() = "New" Then
                    pnl_inactiveuser.Visible = True
                    pnl_activeuser.Visible = False
                    Session("loggedin") = False
                    subscriptstat.Text = "New User you must activate your subscription."
                    btn_renewA.Text = "Activate Subscription"

                ElseIf checkbillingstat() = "Inactive" Then
                    pnl_inactiveuser.Visible = True
                    pnl_activeuser.Visible = False
                    Session("loggedin") = False
                    subscriptstat.Text = "Your subscription is no longer active."

                ElseIf checkbillingstat() = "5 Day" Then

                    If Session("show5day") <> "false" Then
                        Session("loggedin") = True
                        pnl_inactiveuser.Visible = True
                        pnl_activeuser.Visible = False
                        subscriptstat.Text = "You have " & session("expdays") & " days until your subscription will expire."
                        btn_renewcont.Visible = True
                    Else
                        Session("loggedin") = True

                        pnl_activeuser.Visible = True
                        BindWelcome()

                        Logoarea.Attributes("src") = System.Configuration.ConfigurationManager.AppSettings("CurrentIMGURL") & "/logos/company/" & session("selectedlogo")

                        checktaskfilter()
                        checkleadfilter()
                        bindtasksdue()
                        bindnewleads()
                    End If


                ElseIf checkbillingstat() = "Active" Then
                    Session("loggedin") = True


                    pnl_activeuser.Visible = True
                    BindWelcome()

                    Logoarea.Attributes("src") = System.Configuration.ConfigurationManager.AppSettings("CurrentIMGURL") & "/logos/company/" & session("selectedlogo")

                    checktaskfilter()
                    checkleadfilter()
                    bindtasksdue()
                    bindnewleads()
                    if session("forcesys")="Yes" then
                    		if session("readsysmes")="N" then
					        		pnlsysinfo.visible=true
					        		pnlallinfo.visible=false
					        		updatesysmessstat()
		              		else
      			        		pnlsysinfo.visible=false
					        		pnlallinfo.visible=true              		
                    		end if
     						else 
      			        		pnlsysinfo.visible=false
					        		pnlallinfo.visible=true              		
     
                    	end if
                    	

                End If
                 End If
                If Session("branding") = "No" Then
                    imgbtnupgrade.Visible = True
                Else
                    imgbtnupgrade.Visible = False
                End If
            Else
                'Session("taskfilter") = "Due"
            End If
            if session("hiderems")="true" then
            	pnlreminders.visible=false
               lblunpubads.visible=false
            else
           		 checkforunpubads()
           	end if
            pagesetup()

        End Sub
        sub clearsessions()
        	session("selectedlogo")=""
        	session("expdays")=""
        	lblOrderBy.Text = "lt_tbl_pk desc"
       
        
        
        end sub
        
        
        
      Sub SortCommand_Click(sender As Object, e As DataGridSortCommandEventArgs)
          
				if session("sortexpression") =  e.SortExpression then 
					if session("sortdirection") = "ASC" then
						session("sortdirection") = "DESC"
					else
						session("sortdirection") = "ASC" 
					end if
				else 
					session("sortexpression") =  e.SortExpression
					session("sortdirection") = "ASC" 
				end if
				
          	lblOrderBy.Text =  session("sortexpression") + " " + session("sortdirection") 
     			bindtasksdue()

        
        end sub
        
        
        
        
        sub checkforunpubads()
        		Dim strUID As String = Session("userid")
            Dim strSql As String = "select * from dbo.tbl_LeadADVenues join dbo.tbl_LeadADs on tbl_leadad_pk =av_leadads_FK " _
						 & "where ad_userid= '" & session("userid") & "' and av_adplaced='Unpublished' and av_apto is not null " _
						 & "and av_apto <= getdate()"
             Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    pnlreminders.visible=true
                    lblunpubads.visible=true
                else
                	pnlreminders.visible=false
                  lblunpubads.visible=false
                
                 End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        
        
        end sub
        
        
         Sub bindsysmessage()
            Dim strUID As String = Session("userid")
            Dim strSql As String = "SELECT * from tbl_miscstuff where misc_type='SysMessage'"
             Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    txtsysnotes.Text = Sqldr("misc_text")
                 End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        End Sub
        sub updatesysmessstat()
				Dim strUID As String = Session("userid")
            Dim strSql As String = "update tbl_users set readsysmes='Y' where uid='" & session("userid") & "'"
             Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                 End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        
        end sub
        
        Sub bindhelp()
        		txthelp.readonly=true
        		txthelp.ToolbarLayout =""

            Dim strUID As String = Session("userid")
            Dim strSql As String = "SELECT * from tbl_miscstuff where misc_type='SysHelp'"
             Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    txthelp.Text = Sqldr("misc_text")
                 End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        End Sub
        
        
       
        Sub playvideo1(ByVal Source As Object, ByVal e As ImageClickEventArgs)
            Response.Write("<script>window.open" & _
               "('video1.aspx','_new', 'width=800,height=500');</script>")
        End Sub
        Sub buyupgrades(ByVal Source As Object, ByVal e As ImageClickEventArgs)
        		response.redirect("upgrades.aspx?source=default")
           
        End Sub
        
        sub hidereminders(ByVal Source As Object, ByVal E As EventArgs)
        	session("hiderems")="true"
        	pnlreminders.visible=false
         lblunpubads.visible=false
        end sub
        
        Public Sub createc32rec()

            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            dim c32action as string
            Dim ip As String = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
            If ip = "" Then
                ip = Request.ServerVariables("REMOTE_ADDR")
            End If
            if btn_renewA.Text = "Activate Subscription" then
            	c32action="Activate"
            else
            	c32action="Renew"            
            end if

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
    
    public sub updatebrandingflag(id as string)
		    Dim strConnection As String
			 Dim sqlConn As SqlConnection
		    Dim sqlCmd As SqlCommand
			Dim strSql as String = "update tbL_users set brandingPurchased='Yes', package='Branding Monthly' where uid='" & id & "'"
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
    public sub updatecredits(id as string)
		    Dim strConnection As String
			 Dim sqlConn As SqlConnection
		    Dim sqlCmd As SqlCommand
			Dim strSql as String = "update tbL_users set autopostc= autopostc + " & getapcredits(id) & " where uid='" & id & "'"
		 		     Try
		           strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
		           sqlConn = New SqlConnection(strConnection)
		           sqlCmd = New SqlCommand(strSql, sqlConn)
		           
		           sqlConn.Open()		           
		           Dim Sqldr as SqlDataReader = sqlCmd.ExecuteReader	          		           
		      
			   Catch ex As Exception
		    		Response.Write(ex.ToString())
	
		    	Finally
		       	sqlConn.Close()
			 	End Try
    end sub
        Public Function getapcredits(ByVal id1 As String) As Integer

            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select qty,* from cart32.dbo.orditems oi join cart32.dbo.orders o on o.orderno=oi.orderno " _
              & "join tbl_users tu on tu.cart32id = o.userid " _
              & "where partno='Producty' and tu.uid='" & id1 & "' " _
              & "and o.orderno = (select max (orderno) from cart32.dbo.orders join tbl_users tu on tu.cart32id = userid " _
              & "where tu.uid='" & id1 & "')"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)

                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    Return Sqldr("qty")
                Else
                    Return 0
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())

            Finally
                sqlConn.Close()
            End Try
        End Function
    
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
    
        Public Sub updateexpiredate()

            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim ip As String = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
            If ip = "" Then
                ip = Request.ServerVariables("REMOTE_ADDR")
            End If

            Dim strSql As String = "update dbo.tbl_users set Status='Active',sub_expiredate=DATEADD(DAY,x.qty*30,z.orderdate), " _
                            & "brandingPurchased=case when(rtrim(ltrim(cast (x.partno as varchar(255))))='ProductB') then 'Yes' else 'No' end,package=x.item  " _
       & "from cart32.dbo.orders z " _
       & "join dbo.tbl_users on cart32id=userid " _
       & "join cart32.dbo.orditems x on x.orderno = z.orderno " _
       & "where uid='" & Session("userid") & "' " _
       & "and z.orderno = (select max(orderno) from cart32.dbo.orders " _
       & "join dbo.tbl_users on cart32id=userid where uid='" & Session("userid") & "')"
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
				if compcartnoempty() then
				 	updatecostat()
				end if
        End Sub
        
        Public Sub  updatecostat()
        		Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "update tbl_company set Co_customerid=cart32id " _
								& "from tbl_users  " _                          
								& "where UID='" & session("userid") & "'"

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
            
           
        end sub
        
        public function compcartnoempty() as boolean
        Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select Co_customerid from tbl_company join tbl_users on company_pk = co_tbl_pk " _
											& "where UID='" & session("userid") & "'"

            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)

                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
						if sqldr.read() then
							if sqldr("Co_customerid") isnot dbnull.value then
								return false
							else 
								return true
							end if
						else
								return true
						end if
						

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
            
        
        
        
        end function
        
        
        
        Public Sub click_renewsubscriptionA(ByVal Source As Object, ByVal E As EventArgs)
				'createc32rec()
            'Response.Write("<frameset ><frame src='" & System.Configuration.ConfigurationManager.AppSettings("CurrentwebURL") & "/bin/cart32.exe/choiceone-AddItem?Qty=1&PartNo=ProductA&Item=ProductA&Price=19.99'></frameset>")
            dim c32action as string
            if btn_renewA.Text = "Activate Subscription" then
            	c32action="Activate"
            else
            	c32action="Renew"            
            end if

            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("CurrentappURL") & "/gsignup.aspx?action=" & c32action )
          
        End Sub
        Public Sub click_continuenorenew(ByVal Source As Object, ByVal E As EventArgs)
            Session("loggedin") = True

            pnl_inactiveuser.Visible = False
            pnl_activeuser.Visible = True
            BindWelcome()

            Logoarea.Attributes("src") = System.Configuration.ConfigurationManager.AppSettings("CurrentIMGURL") & "/logos/company/" & session("selectedlogo")

            checktaskfilter()
            checkleadfilter()
            bindtasksdue()
            bindnewleads()
            Session("show5day") = "false"

        End Sub

        Public Sub click_renewsubscriptionB(ByVal Source As Object, ByVal E As EventArgs)
		  	 	Dim ip As String = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
			  	 dim c32action, pcode, vcode as string
         	 If ip = "" Then
             	ip = Request.ServerVariables("REMOTE_ADDR")
        		 End If   
        		 pcode="none"
        		 vcode="webmagicportal"
        		 c32action="register"    
        		 'response.redirect("https://shop.gochoiceone.com/direct.aspx?Qty=" & ddbasic.selecteditem.value & "&PartNo=WMPBasicV2&pcode=" & pcode & "&vcode=" & vcode & "&ip=" & ip & "&action=" & c32action & "&url=" & session("url") )
	

            Response.Write("<frameset ><frame src='" & System.Configuration.ConfigurationManager.AppSettings("CurrentwebURL") & "/bin/cart32.exe/choiceone-AddItem?Qty=1&PartNo=ProductA&Item=ProductA&Price=19.99'></frameset>")


        End Sub
        Public Sub click_renewsubscriptionC(ByVal Source As Object, ByVal E As EventArgs)
            Response.Write("<frameset ><frame src='" & System.Configuration.ConfigurationManager.AppSettings("CurrentwebURL") & "/bin/cart32.exe/choiceone-AddItem?Qty=1&PartNo=ProductA&Item=ProductA&Price=19.99'></frameset>")

        End Sub
        Public Sub click_upcon(ByVal Source As Object, ByVal E As EventArgs)
        		Response.Redirect("default.aspx")
        End Sub
        Public Sub viewsystinfo(ByVal Source As Object, ByVal e As ImageClickEventArgs)
        		pnlsysinfo.visible=true
        		pnlallinfo.visible=false
        End Sub
        Public Sub viewhelp(ByVal Source As Object, ByVal e As ImageClickEventArgs)
        		pnlhelp.visible=true
        		pnlallinfo.visible=false
        End Sub
        
        
         Public Sub click_hidesysnews(ByVal Source As Object, ByVal E As EventArgs)
        		pnlsysinfo.visible=false
        		pnlallinfo.visible=true
        End Sub
        
        Public Sub click_hidehelp(ByVal Source As Object, ByVal E As EventArgs)
        		pnlhelp.visible=false
        		pnlallinfo.visible=true
        End Sub
        
        

        Sub BindWelcome()

            Dim strUID As String = Session("userid")
            Dim strSql As String = "SELECT * from tbl_users join tbl_company on co_tbl_pk = company_pk where UID='" & strUID & "'"
            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    If Sqldr("lname") IsNot DBNull.Value Then
                        Welcomemessage.Text = Sqldr("fname") & " " & Sqldr("lname")
                    End If
                    If Sqldr("co_logo") IsNot DBNull.Value Then
                        session("selectedlogo") = Sqldr("co_logo")
                    Else
                        session("selectedlogo") = "default.jpg"
                    End If
                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        End Sub

        Sub BindGrid()

            'Dim strpropID as String = Request.QueryString("id")
            Dim strUID As String = Session("userid")
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            mycommand = "Select *, convert(varchar(20),ld_adddate,101) as ld_adddatef, case when (ld_lname is null) then ld_fname else ld_fname + ' ' + ld_lname end  as 'assignedby' from dbo.tbl_leads left join dbo.tbl_users on Uid=ld_assignedbyuid left join tbl_leadtasks on leads_fk = tbl_leads_pk where datediff (d,ld_adddate,getdate()) > =7 and (tsk_initialfollowupdone='No' or tsk_initialfollowupdone is null) order by tbl_leads_pk desc"
            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_leads")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_leads"))
                leadsneedingfollowup.DataSource = dvProducts
                leadsneedingfollowup.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
        End Sub
   
        Sub BindGridFollowup()

            'Dim strpropID as String = Request.QueryString("id")
            Dim strUID As String = Session("userid")
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            '	mycommand = "Select *, convert(varchar(20),ld_adddate,101) as ld_adddatef, case when (ld_lname is null) then ld_fname else ld_fname + ' ' + ld_lname end  as 'assignedby' from dbo.tbl_leads left join dbo.tbl_users on Uid=ld_assignedbyuid left join tbl_leadtasks on leads_fk = tbl_leads_pk where datediff (d,ld_adddate,getdate()) > =7 and (tsk_initialfollowupdone='No' or tsk_initialfollowupdone is null) order by tbl_leads_pk desc"
            ' mycommand= "select leads_pk,case when (ld_lname is null) then ld_fname else ld_fname + ' ' + ld_lname " _
            '					& "end as 'Contact',  convert(varchar(20),ld_adddate,101) as ld_adddatef, " _
            '						& "fname + ' ' + lname as assignedby, " _
            '						& "ld_status,ld_status,cnt_date,cnt_followupaction, " _
            '						& "case 	when (select distinct cnt_followup from dbo.tbl_leadscontacthistory " _
            '								& "where tbl_leads_fk = tbl_leads_pk and cnt_followup='Yes') = 'Yes' then " _
            '								& "'Yes' else 	'No' end as 'Followup' ,* " _
            '					& "from tbl_leads " _
            '					& "left join tbl_leadscontacthistory on tbl_leads_fk = tbl_leads_pk " _
            '					& "join dbo.tbl_users on Uid=ld_assignedbyuid " _
            '					& "where case when (select distinct cnt_followup from dbo.tbl_leadscontacthistory " _
            '								& "where tbl_leads_fk = tbl_leads_pk and cnt_followup='Yes') = 'Yes' then " _
            '								& "'Yes' else 	'No' end = 'Yes' and ld_assignedtouid ='" & strUID & "'" _
            '								 & "order by tbl_leads_pk desc"
            '								 
            mycommand = "select cast(tbl_leadcnthistory_pk as varchar(20)) as 'hnum', cast(tbl_leads_fk as varchar(20)) as 'lnum2',cnt_followupaction, convert(varchar(20),cnt_fduedate,101), tbl_leads_fk, " _
                & "ld_fname + ' ' + ld_lname as 'name', ld_hphone, ld_cphone,* " _
                & "from tbl_leadscontacthistory " _
                & "join tbl_leads on tbl_leads_pk = tbl_leads_fk " _
                & "where cnt_followup = 'Yes' and cnt_agentid='" & Session("userid") & "'"
            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_leads")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_leads"))
                leadsaction.DataSource = dvProducts
                leadsaction.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
        End Sub

        Public Function checkbillingstat() As String

                Dim strConnection As String
                Dim sqlConn As SqlConnection
                Dim sqlCmd As SqlCommand

                'Dim strSql As String = " SELECT case when (datediff(d,max (orderdate),getdate()) > 25 and datediff(d,max (orderdate),getdate()) <=30) then 'Yes' " _
                '   & "when (datediff(d,max (orderdate),getdate()) is not null) then 'No' end as '5daywarn', case when (datediff(d,max (orderdate),getdate()) > 25) then  'Yes' " _
                '  & "when (datediff(d,max (orderdate),getdate()) is not null) then 'No' end as 'Inactivate' from cart32.dbo.orders  where cast(userid as varchar(20))='" & Session("c32id") & "'"
                Dim strSql As String = "SELECT datediff(d,getdate(),sub_expiredate) as 'daystoexpire',status from tbl_users where uid='" & Session("userid") & "'"


                Try
                    strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                    sqlConn = New SqlConnection(strConnection)
                    sqlCmd = New SqlCommand(strSql, sqlConn)

                    sqlConn.Open()
                    Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                    If Sqldr.Read() Then
                        If Sqldr("status") <> "New" Then
                            If Sqldr("daystoexpire") > 0 And Sqldr("daystoexpire") < 5 Then
                                session("expdays") = Sqldr("daystoexpire")
                                Return "5 Day"

                            ElseIf (Sqldr("daystoexpire") < 0 or Sqldr("status")="Inactive")  Then
                                Return "Inactive"
                            Else
                                Return "Active"
                            End If
                        Else
                            Return "New"
                        End If


                        ' If Sqldr("Inactivate") IsNot DBNull.Value Then
                        'If Sqldr("Inactivate") = "No" Then
                        'If Sqldr("5daywarn") = "No" Then
                        'Return "Active"
                        'Else
                        '   Return "5 Day"
                        'End If
                        'Else
                        'Return "Inactive"
                        'End If
                        'Else
                        'Return "Inactive"
                        'End If
                        'Else
                        'Return "Not Found"
                    End If

                Catch ex As Exception
                    Response.Write(ex.ToString())
                Finally
                    sqlConn.Close()
                End Try
          
        End Function
        Public Sub loguserintocartsys()

            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim CartCookieID As String = Request.Cookies("Cart32-CHOICEONE").Value
            Dim ip As String = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()


            Dim strSql As String = " insert into cart32.dbo.userlogon (code,userid,cookie,ip,logondate,logontime) " _
                 & "values ('CHOICEONE','" & Session("c32id") & "','" & CartCookieID & "','" & ip & "'," & rightNow & ",'" & TimeOfDay() & "')"

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

        Public Sub loguserin()
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String
            	strSql = "SELECT shopcart,users_tbl_PK,status,betauser,brandingPurchased,convert(varchar(20),sub_expiredate,101) as 'expdate',rtrim(password) as password, uid, type, role, company, industry, fname,lname,package,cart32id,company_pk from tbl_users where UID='" & session("userid") & "'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then

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
                    session("loggedin") = "true"
                    session("s_userloggedin") = "true"
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try


        End Sub

       Public Sub bindtasksdue()
            Dim strpropID As String = Request.QueryString("id")
            Dim strUID As String = Session("userid")
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            If Session("taskfilter") = "Open" Then
                mycommand = "Select *, left(convert(varchar(255),lt_desc),50)+'...' as 'briefnotes', cast (lt_tbl_pk as varchar(20)) as 'tpk' , cast (lt_leadpk_fk as varchar(20)) as 'lpk', convert(varchar(20),lt_duedate,101) as 'Ddate', case when (ld_lname is null) then ld_fname else ld_fname + ' ' + ld_lname end  as 'ldname', case when (lt_leadpk_fk is null) then 0 else lt_leadpk_fk end as 'Lno' from tbl_tasksuser left join tbl_leads on tbl_leads_pk=lt_leadpk_fk Where lt_uid='" & Session("userid") & "' and (lt_status='New' or lt_status='Inprocess') order by " + lblOrderBy.Text
            ElseIf Session("taskfilter") = "Due" Then
                mycommand = "Select *, left(convert(varchar(255),lt_desc),50)+'...' as 'briefnotes', cast (lt_tbl_pk as varchar(20)) as 'tpk' , cast (lt_leadpk_fk as varchar(20)) as 'lpk',  convert(varchar(20),lt_duedate,101) as 'Ddate', case when (ld_lname is null) then ld_fname else ld_fname + ' ' + ld_lname end  as 'ldname',case when (lt_leadpk_fk is null) then 0 else lt_leadpk_fk end as 'Lno' from tbl_tasksuser left join tbl_leads on tbl_leads_pk=lt_leadpk_fk Where lt_uid='" & Session("userid") & "' and datediff(d,lt_duedate,getdate()) =0 and (lt_status='New' or lt_status='Inprocess') order by " + lblOrderBy.Text
            Else
                mycommand = "Select *, left(convert(varchar(255),lt_desc),50)+'...' as 'briefnotes', cast (lt_tbl_pk as varchar(20)) as 'tpk' , cast (lt_leadpk_fk as varchar(20)) as 'lpk',  convert(varchar(20),lt_duedate,101) as 'Ddate', case when (ld_lname is null) then ld_fname else ld_fname + ' ' + ld_lname end  as 'ldname',case when (lt_leadpk_fk is null) then 0 else lt_leadpk_fk end as 'Lno' from tbl_tasksuser left join tbl_leads on tbl_leads_pk=lt_leadpk_fk Where lt_uid='" & Session("userid") & "' order by " + lblOrderBy.Text
            End If

            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_tasksuser")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_tasksuser"))
                tasksdue.DataSource = dvProducts
                totaltasks.Text = dvProducts.Count.ToString
                tasksdue.DataBind()

            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
        End Sub



        Public Sub bindnewleads()
            Dim strpropID As String = Request.QueryString("id")
            Dim strUID As String = Session("userid")
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            If Session("leadfilter") = "New" Then
                mycommand = "Select distinct tbl_leads_pk,ld_type,ld_adsource,ld_adcode,ad_title, case when (ld_lname is null) then ld_fname else ld_fname + ' ' + ld_lname end  as 'fullname',convert(varchar(20),ld_adddate,101) as 'adddate' from tbl_leads left join tbl_LeadADVenues on av_key=ld_adcode left join tbl_LeadADs on tbl_leadad_pk=av_leadads_FK left join tbl_users on uid=ld_assignedbyuid or uid=ld_assignedtouid  Where (ld_assignedtouid='" & Session("userid") & "' or ld_assignedbyuid='" & Session("userid") & "') and ((ld_adddate >=  lastlogindate and ld_adddate <= Getdate()) or convert(varchar(20),ld_adddate,101)=convert(varchar(20),Getdate(),101)) order by tbl_leads_pk desc"
            ElseIf Session("leadfilter") = "15" Then
                mycommand = "Select distinct tbl_leads_pk,ld_type,ld_adsource,ld_adcode,ad_title, case when (ld_lname is null) then ld_fname else ld_fname + ' ' + ld_lname end as 'fullname',convert(varchar(20),ld_adddate,101) as 'adddate' from tbl_leads left join tbl_LeadADVenues on av_key=ld_adcode left join tbl_LeadADs on tbl_leadad_pk=av_leadads_FK  Where (ld_assignedtouid='" & Session("userid") & "' or ld_assignedbyuid='" & Session("userid") & "') and convert(varchar(20),ld_adddate,101) between convert(varchar(20),getdate()-6,101) and convert(varchar(20),getdate(),101) order by tbl_leads_pk desc"
            Else
                mycommand = "Select distinct tbl_leads_pk,ld_type,ld_adsource,ld_adcode,ad_title, case when (ld_lname is null) then ld_fname else ld_fname + ' ' + ld_lname end  as 'fullname',convert(varchar(20),ld_adddate,101) as 'adddate' from tbl_leads left join tbl_LeadADVenues on av_key=ld_adcode left join tbl_LeadADs on tbl_leadad_pk=av_leadads_FK  Where (ld_assignedtouid='" & Session("userid") & "' or ld_assignedbyuid='" & Session("userid") & "') and  datediff(d,ld_adddate,getdate()) > 30 order by tbl_leads_pk desc "
            End If

            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_leads")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_leads"))
                newleads.DataSource = dvProducts
                totnewleads.Text = dvProducts.Count.ToString

                newleads.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
        End Sub

        Sub closetask(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(0).Text
            closetasksave(content)
            bindtasksdue()

        End Sub

        Public Sub closetasksave(ByVal id As String)
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "update tbl_tasksuser set lt_status='Completed' where lt_tbl_pk='" & id & "'"
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

        Sub Edittask(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(0).Text
            Dim content2 As String = item.Cells(1).Text

            'Response.Redirect("leadhistory.aspx?history=0&LeadNo=" & Request.QueryString("id") & "&LeadType=" & dd_leadtype.SelectedItem.Text & "&action=new")
            Response.Write("<script>window.open" & _
               "('leadhistory.aspx?type=task&task=" & content & "&LeadNo=" & content2 & "&action=view&source=popup','_new', 'width=800,height=500');</script>")

        End Sub
        Sub taskfilterdue(ByVal sender As Object, ByVal e As EventArgs)
            Session("taskfilter") = "Due"
            tasksdue.CurrentPageIndex = 0
            checktaskfilter()
            bindtasksdue()
        End Sub
        Sub taskfilteropen(ByVal sender As Object, ByVal e As EventArgs)
            Session("taskfilter") = "Open"
            tasksdue.CurrentPageIndex = 0
            checktaskfilter()
            bindtasksdue()
        End Sub
        Sub taskfilterall(ByVal sender As Object, ByVal e As EventArgs)
            Session("taskfilter") = "All"
            tasksdue.CurrentPageIndex = 0
            checktaskfilter()
            bindtasksdue()
        End Sub
        Sub taskadd(ByVal sender As Object, ByVal e As EventArgs)
           
           response.redirect("leadhistory.aspx?type=task&LeadNo=0&action=new")
           
           
           
        End Sub
        
        Sub leadfilternew(ByVal sender As Object, ByVal e As EventArgs)
            Session("leadfilter") = "New"
            newleads.CurrentPageIndex = 0
            checkleadfilter()
            bindnewleads()
        End Sub
        Sub leadfilter15(ByVal sender As Object, ByVal e As EventArgs)
            Session("leadfilter") = "15"
            newleads.CurrentPageIndex = 0
            checkleadfilter()
            bindnewleads()
        End Sub
        Sub leadfilter30(ByVal sender As Object, ByVal e As EventArgs)
            Session("leadfilter") = "30"
            newleads.CurrentPageIndex = 0
            checkleadfilter()
            bindnewleads()
        End Sub

        Sub checktaskfilter()
            If Session("taskfilter") = "Open" Then
                Duetoday.Attributes("style") = "color:#000000; font-family:arial; font-size:8pt; cursor:hand"
                AllOpen.Attributes("style") = "color:#ff0000; font-family:arial; font-size:8pt; cursor:hand"
                Alltasks.Attributes("style") = "color:#000000; font-family:arial; font-size:8pt; cursor:hand"
            ElseIf Session("taskfilter") = "Due" Then
                Duetoday.Attributes("style") = "color:#ff0000; font-family:arial; font-size:8pt; cursor:hand"
                AllOpen.Attributes("style") = "color:#000000; font-family:arial; font-size:8pt; cursor:hand"
                Alltasks.Attributes("style") = "color:#000000; font-family:arial; font-size:8pt; cursor:hand"
            ElseIf Session("taskfilter") = "All" Then
                Duetoday.Attributes("style") = "color:#000000; font-family:arial; font-size:8pt; cursor:hand"
                AllOpen.Attributes("style") = "color:#000000; font-family:arial; font-size:8pt; cursor:hand"
                Alltasks.Attributes("style") = "color:#ff0000; font-family:arial; font-size:8pt; cursor:hand"
            Else
                Session("taskfilter") = "Due"
                Duetoday.Attributes("style") = "color:#ff0000; font-family:arial; font-size:8pt; cursor:hand"
                AllOpen.Attributes("style") = "color:#000000; font-family:arial; font-size:8pt; cursor:hand"
                Alltasks.Attributes("style") = "color:#000000; font-family:arial; font-size:8pt; cursor:hand"
            End If

        End Sub

        Sub checkleadfilter()
           
            If Session("leadfilter") = "15" Then
                lead30.Attributes("style") = "color:#000000; font-family:arial; font-size:8pt; cursor:hand"
                lead15.Attributes("style") = "color:#ff0000; font-family:arial; font-size:8pt; cursor:hand"
                leadnew.Attributes("style") = "color:#000000; font-family:arial; font-size:8pt; cursor:hand"
            ElseIf Session("leadfilter") = "30" Then
                lead30.Attributes("style") = "color:#ff0000; font-family:arial; font-size:8pt; cursor:hand"
                lead15.Attributes("style") = "color:#000000; font-family:arial; font-size:8pt; cursor:hand"
                leadnew.Attributes("style") = "color:#000000; font-family:arial; font-size:8pt; cursor:hand"
            ElseIf Session("leadfilter") = "New" Then
                lead30.Attributes("style") = "color:#000000; font-family:arial; font-size:8pt; cursor:hand"
                lead15.Attributes("style") = "color:#000000; font-family:arial; font-size:8pt; cursor:hand"
                leadnew.Attributes("style") = "color:#ff0000; font-family:arial; font-size:8pt; cursor:hand"
            Else
                Session("leadfilter") = "New"
                lead30.Attributes("style") = "color:#000000; font-family:arial; font-size:8pt; cursor:hand"
                lead15.Attributes("style") = "color:#000000; font-family:arial; font-size:8pt; cursor:hand"
                leadnew.Attributes("style") = "color:#ff0000; font-family:arial; font-size:8pt; cursor:hand"
            End If

        End Sub

        Sub myDataGrid_PageChangerA(ByVal Source As Object, _
                  ByVal E As DataGridPageChangedEventArgs)
            ' Set the CurrentPageIndex before binding the grid 
            tasksdue.CurrentPageIndex = E.NewPageIndex
            bindtasksdue()

        End Sub
        Sub newleads_PageChanger(ByVal Source As Object, _
                ByVal E As DataGridPageChangedEventArgs)
            ' Set the CurrentPageIndex before binding the grid 
            newleads.CurrentPageIndex = E.NewPageIndex
            bindnewleads()

        End Sub
       
    End Class

    
end namespace