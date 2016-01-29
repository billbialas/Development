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
Imports FreeTextBoxControls

namespace PageTemplate
    Public Class brandingADD
        Inherits PageTemplate
        Public chkshowlogo,chkshowlogo2, chksendemail, chkemailself, chkhead1, chkhead2,chkshowco,chkshowhr,chkshowcntbtn As CheckBox
        public chkshowco2,chkshowhr2,chkhead12,chkhead22 As CheckBox
        Public txtredirecturl, TextBox1, TextBox2, TextBox3,  txtname, txtdescription, selfemailaddress,selfemailaddress2 As TextBox
        Public txtconame, txthead1, txthead2 As TextBox
        
        Public dd_sellogo,dd_sellogo2,ddexportque As DropDownList
        Public imglogo,imglogo2 As HtmlImage
       
        Public pg1text,pg2text,emailbody
 
        public pnlgeneral, pnlpage1,pnlpage2, pnlnotifications,pnlimages,pnltempatespre,pnlADS as panel
        Public subnavGen, subnavPage1, subnavPage2, subnavresp,subnavimgs,subnavADS As HtmlTableCell
        Public spacer0, spacer1, spacer2,spacer3,spacer4 As HtmlTableCell
        Protected WithEvents Lgen, lpage1, lpage2, lautop,limgs,btnuseremail2,btnuseremail,lads As LinkButton
        Public ddemailcor As DropDownList
        public dgimages as datagrid
         public btnshowtemps,bsaveco as button
        public emails,temppreview,dglinkedADS as datagrid
        public vwbodyA as linkbutton
        
        public dd_status,dd_status2,dd_Bstatus as dropdownlist
        public chkemailloglink,chkemailloglink2,chkemailself2
    

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

            If Not (Page.IsPostBack) Then
            clearsessions()
						selfemailaddress.enabled=false
                    selfemailaddress2.enabled=false
                    ddexportque.enabled=false
                    btnuseremail.enabled=false
                    btnuseremail2.enabled=false              
                    dd_status.enabled=false
                    dd_status2.enabled=false
                    chkemailloglink.enabled=false
                    chkemailloglink2.enabled=false
                If Request.QueryString("action") = "new" Then
                    bindconame()
                    Filllogos()
                    Filllogos2()
                    dd_sellogo.Enabled = False
                    dd_sellogo2.Enabled = False
                    txtname.Focus()
                    chkshowcntbtn.checked =true
                    session("lsttab")=""
                    fillexportque()
                    
                Else
                    Filllogos()
                    Filllogos2()
                     fillexportque()
                    bindfields()
                    session("brno")=request.querystring("id")
                End If
                If Request.QueryString("subaction") = "preview" Then
                	if Request.QueryString("page") = "1" Then
	                    Dim normurl As String = "intakep.aspx?page=1&id=" & Request.QueryString("id")
	                    Response.Write("<script>window.open" & _
	                        "('" & normurl & "','_new', 'width=900,height=600,resizable=1,scrollbars=1');</script>")
                  else
                  	Dim normurl As String = "intakep.aspx?page=2&id=" & Request.QueryString("id")
	                  Response.Write("<script>window.open" & _
	                       "('" & normurl & "','_new', 'width=900,height=600,resizable=1,scrollbars=1');</script>")
                  end if
                   subnav(session("lsttab"))
                else
	                subnav("General")
	               
                End If
                 Fillemailcor()
               if session("role")<>"GOD" then
          			Dim removeListItem As ListItem = dd_status.Items.FindByText("Anonymous") 
          			dd_status.Items.remove(removeListItem)
          			Dim removeListItem2 As ListItem = dd_status2.Items.FindByText("Anonymous") 
          			dd_status2.Items.remove(removeListItem)
          			
          			
          		end if
          		If Request.QueryString("source") = "admanager" Then
         			 bsaveco.visible=false
          		else
          		 	bsaveco.visible=true
          		end if
          		 If Request.QueryString("action") = "edit"
	          		if txtname.text = "Default" then
	          			txtname.enabled=false
	          		else
	          			txtname.enabled=true
	          		end if
	          	end if
          	if session("role") ="GOD" then
             			pg1text.ModeSwitch="true"
             		else
             		 	pg1text.ModeSwitch="false"
             		end if
          		
          		
            End If
            pagesetup()

        End Sub
        
         Sub uploadimage(ByVal sender As Object, ByVal e As EventArgs)
         	
            Response.Redirect("images.aspx?source=rspmgr")
        End Sub 
        
        sub clearsessions()
        		session("currentbrandno")=""
       	 session("bdlevel")=""
       		 session("selectedlogo")=""
     			session("brandno")=""
        
        end sub
        
        
        Sub btn_Gen(ByVal sender As Object, ByVal e As EventArgs)
            subnav("General")
             txtname.Focus()
             session("lsttab")="General"
            
        End Sub
        
        Sub btn_pg1(ByVal sender As Object, ByVal e As EventArgs)
            subnav("Intake")
            chkshowco.focus()
          	session("lsttab")="Intake"
        End Sub
        
        Sub btn_pg2(ByVal sender As Object, ByVal e As EventArgs)
            subnav("Confirmation")
           	chkshowco2.focus()
           	session("lsttab")="Confirmation"
        End Sub
        
        Sub continbutton(ByVal sender As Object, ByVal e As EventArgs)
            if chkshowcntbtn.checked then
            	txtredirecturl.enabled=true
            else
	            txtredirecturl.enabled=false
	            txtredirecturl.text=""
            end if
            
        End Sub
        Sub sndpriemail(ByVal sender As Object, ByVal e As EventArgs)
           if chkemailself.checked then
	           selfemailaddress.enabled=true           
	           ddexportque.enabled=false
	           btnuseremail.enabled=true                     
	           dd_status.enabled=true
	           chkemailloglink.enabled=true
	        else
	         	selfemailaddress.enabled=false 
	           btnuseremail.enabled=false                     
	           dd_status.enabled=false
	           chkemailloglink.enabled=false
	            ddexportque.SelectedIndex = ddexportque.Items.IndexOf(ddexportque.Items.FindBytext("None"))
		        dd_status.SelectedIndex = dd_status.Items.IndexOf(dd_status.Items.FindBytext("Select.."))
		        ddexportque.enabled=false
		        selfemailaddress.text=""
	        end if                     
	           
        End Sub
         
         Sub sndsecemail(ByVal sender As Object, ByVal e As EventArgs)
         if chkemailself2.checked then
	           selfemailaddress2.enabled=true 
	           btnuseremail2.enabled=true     
	           dd_status2.enabled=true
	           chkemailloglink2.enabled=true   
	            selfemailaddress2.text=""
	           chkemailloglink2.checked=false
	      else
	     		  selfemailaddress2.enabled=false 
	           btnuseremail2.enabled=false     
	           dd_status2.enabled=false
	           chkemailloglink2.enabled=false
	           selfemailaddress2.text=""
	           chkemailloglink2.checked=false
	           dd_status2.SelectedIndex = dd_status2.Items.IndexOf(dd_status2.Items.FindBytext("Select.."))
	    	end if 
           
        End Sub
         
       	public function namecheckdup(action as string) as boolean
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String 
            if action="new" then
            	strSql= "select * from dbo.tbl_adbranding where br_name = '" & txtname.text  & "' and br_uid_fk ='" & session("userid") & "'"
        	   else
            	strSql= "select * from dbo.tbl_adbranding where br_name = '" & txtname.text  & "' and br_uid_fk ='" & session("userid") & "' and tbl_branding_pk <> '" & request.querystring("id") & "'"
        	   end if
                Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                  	return true
                Else
                 		return false
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
            
        End function
        
        
        sub DupBname()
         	txtname.text = "Duplicate Name!  Can not use."
	        	txtname.BackColor = Red
	        	txtname.focus() 	
	        	
        end sub
        
        
        
        Sub btn_rsp(ByVal sender As Object, ByVal e As EventArgs)
            subnav("Responses")
            chkemailself.focus()
            session("lsttab")="Responses"
            
            
        End Sub
        
        Sub btn_imgs(ByVal sender As Object, ByVal e As EventArgs)
            subnav("Images")            
            bindimages()
            session("lsttab")="Images"
        End Sub
        
          Sub btn_ADS(ByVal sender As Object, ByVal e As EventArgs)
            subnav("ADS")            
            bindads()
            session("lsttab")="ADS"
        End Sub
        
        sub bindads()
           Dim strUID As String = Session("userid")
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            Dim RcdCount As Integer
            mycommand = "select * from tbl_LeadADs where ad_intakeresponse='" & session("brno") & "' and ad_status='Active' order by tbl_leadad_pk desc"
           
            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_LeadADs")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_LeadADs"))
               
                dglinkedADS.DataSource = dvProducts
                dglinkedADS.DataBind()
           
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
        
        end sub
        
         Sub MyDataGridR_PageLADS(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
            dglinkedADS.CurrentPageIndex = e.NewPageIndex
             bindads()
        End Sub
        
        
        Sub bindconame()
            txtconame.Text = Session("company")
        End Sub
        Sub chkshowlogoA(ByVal Source As System.Object, ByVal e As System.EventArgs)
            If chkshowlogo.Checked Then
                dd_sellogo.Enabled = True
                dd_sellogo.focus()
            Else
            	chkshowlogo.focus()
                dd_sellogo.Enabled = False
            End If

        End Sub
         Sub chkshowlogoA2(ByVal Source As System.Object, ByVal e As System.EventArgs)
            If chkshowlogo2.Checked Then
                dd_sellogo2.Enabled = True
                dd_sellogo2.focus()
            Else
            	chkshowlogo2.focus()
                dd_sellogo2.Enabled = False
            End If

        End Sub
        Sub chkshowconame(ByVal Source As System.Object, ByVal e As System.EventArgs)
            If chkshowco.Checked Then
                txtconame.Enabled = True
                txtconame.focus()
            Else
                txtconame.Enabled = False
                chkshowco.focus()
            End If

        End Sub
        Sub chkshowhead1(ByVal Source As System.Object, ByVal e As System.EventArgs)
            If chkhead1.Checked Then
                txthead1.Enabled = True
                txthead1.focus()
            Else
                txthead1.Enabled = False
                chkhead1.focus()
            End If

        End Sub
        Sub chkshowhead2A(ByVal Source As System.Object, ByVal e As System.EventArgs)
            If chkhead2.Checked Then
                txthead2.Enabled = True
                txthead2.focus()
            Else
                txthead2.Enabled = False
                chkhead2.focus()
            End If

        End Sub
        Sub refreshlogo(ByVal Source As System.Object, ByVal e As System.EventArgs)

            If dd_sellogo.SelectedItem.Text = "Default" Then
                If checkforlogo() Then
                    imglogo.Src = System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "/logos/company/" & session("selectedlogo")
                Else
                    imglogo.Src = System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "/logos/company/default.jpg"
                End If
            Else

                imglogo.Src = System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "/UIMG/" & Left(Session("userid"), Len(Session("userid")) - 4) & "IMG/" & gtimgfilename(dd_sellogo.SelectedItem.Value)
            End If
            chkshowhr.focus()

        End Sub
        Sub refreshlogo2(ByVal Source As System.Object, ByVal e As System.EventArgs)

            If dd_sellogo2.SelectedItem.Text = "Default" Then
                If checkforlogo() Then
                    imglogo2.Src = System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "/logos/company/" & session("selectedlogo")
                Else
                    imglogo2.Src = System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "/logos/company/default.jpg"
                End If
            Else

                imglogo2.Src = System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "/UIMG/" & Left(Session("userid"), Len(Session("userid")) - 4) & "IMG/" & gtimgfilename(dd_sellogo2.SelectedItem.Value)
            End If
            chkshowhr2.focus()

        End Sub
         Sub statcheck(ByVal Source As System.Object, ByVal e As System.EventArgs)

            If dd_status.SelectedItem.Text = "Anonymous" Then
                chkemailloglink.checked=false
                chkemailloglink.enabled=false
                ddexportque.enabled=true
                
          	else
             chkemailloglink.enabled=true
             ddexportque.enabled=false
          	
            end if
            
        End Sub
        Sub enableexpq(ByVal Source As System.Object, ByVal e As System.EventArgs)

            If ddexportque.SelectedItem.Text <> "None" Then
                selfemailaddress.text=""
            end if
            
        End Sub
        
        
         Sub statcheck2(ByVal Source As System.Object, ByVal e As System.EventArgs)

            If dd_status2.SelectedItem.Text = "Anonymous" Then
                chkemailloglink2.checked=false
                chkemailloglink2.enabled=false
            else
            	chkemailloglink2.enabled=true
            end if
            
        End Sub
        
        
        Function checkforlogo() As Boolean
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select co_logo from dbo.tbl_company where co_tbl_pk = '" & Session("company_pk") & "' and co_logo is not null"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    session("selectedlogo") = Sqldr("co_logo")
                    Return True
                Else
                    Return False
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
        End Function
        Public Function gtimgfilename(ByVal id As String) As String
            Dim strSql As String = "SELECT ui_filename from tbl_userimages where ui_tbl_pk='" & id & "'"
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    Return Sqldr("ui_filename")
                End If

            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        End Function
        Sub Filllogos()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select * from dbo.tbl_userimages where ui_uid='" & Session("userid") & "' order by ui_name "
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                dd_sellogo.DataSource = dataReader
                dd_sellogo.DataTextField = "ui_name"
                dd_sellogo.DataValueField = "ui_tbl_pk"
                dd_sellogo.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
            dd_sellogo.Items.Insert(0, New ListItem("Select..", "999998"))
            dd_sellogo.Items.Insert(1, New ListItem("Default", "999999"))
        End Sub
        Sub Filllogos2()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select * from dbo.tbl_userimages where ui_uid='" & Session("userid") & "' order by ui_name"
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                dd_sellogo2.DataSource = dataReader
                dd_sellogo2.DataTextField = "ui_name"
                dd_sellogo2.DataValueField = "ui_tbl_pk"
                dd_sellogo2.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
            dd_sellogo2.Items.Insert(0, New ListItem("Select..", "999998"))
            dd_sellogo2.Items.Insert(1, New ListItem("Default", "999999"))
        End Sub
        Public Sub getuseremail(ByVal sender As Object, ByVal e As EventArgs)
            Dim strSql As String = "SELECT email from tbl_users where UID='" & Session("userid") & "'"
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    If Sqldr("email") IsNot DBNull.Value Then
                        selfemailaddress.Text = Sqldr("email")
                    Else
                        selfemailaddress.Text = "None Found"
                    End If
                End If

            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try

        End Sub
        Public Sub getuseremail2(ByVal sender As Object, ByVal e As EventArgs)
            Dim strSql As String = "SELECT email from tbl_users where UID='" & Session("userid") & "'"
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    If Sqldr("email") IsNot DBNull.Value Then
                        selfemailaddress2.Text = Sqldr("email")
                    Else
                        selfemailaddress2.Text = "None Found"
                    End If
                End If

            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try

        End Sub
        Public Sub getuseremailA(ByVal sender As Object, ByVal e As EventArgs)
            Dim strSql As String = "SELECT email from tbl_users where UID='" & Session("userid") & "'"
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    If Sqldr("email") IsNot DBNull.Value Then
                        TextBox1.Text = Sqldr("email")
                    Else
                        TextBox1.Text = "None Found"
                    End If
                End If

            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try

        End Sub
        Public Sub previewpg2(ByVal sender As Object, ByVal e As EventArgs)
            Dim nwpk As String
            if request.querystring("action")="new" then
			  		if not namecheckdup("new") then
						savebranding()
						txtname.BackColor = white
      		      txtdescription.focus()
            		nwpk = getbraandpk()
            	  	Response.Redirect("brandingadd.aspx?id=" & nwpk & "&action=edit&source=" & Request.QueryString("source") & "&subaction=preview&page=2")
					else
	            	DupBname()
   	         	subnav("General")
    				end if
         	else
         		if not namecheckdup("edit") then
					   txtname.BackColor = white
      		      txtdescription.focus()
            		nwpk = Request.QueryString("id")
						savebranding()
            	  	Response.Redirect("brandingadd.aspx?id=" & nwpk & "&action=edit&source=" & Request.QueryString("source") & "&subaction=preview&page=2")
      			else
						DupBname()
   	         	subnav("General")
    				end if
				end if
      
        End Sub
        Public Sub previewpg1(ByVal sender As Object, ByVal e As EventArgs)
          Dim nwpk As String
            if request.querystring("action")="new" then
			  		if not namecheckdup("new") then
						savebranding()
						txtname.BackColor = white
      		      txtdescription.focus()
            		nwpk = getbraandpk()
            	   Response.Redirect("brandingadd.aspx?id=" & nwpk & "&action=edit&source=" & Request.QueryString("source") & "&subaction=preview&page=1")
					else
	            	DupBname()
   	         	subnav("General")
    				end if
         	else
         		if not namecheckdup("edit") then
					   txtname.BackColor = white
      		      txtdescription.focus()
            		nwpk = Request.QueryString("id")
						savebranding()
						Response.Redirect("brandingadd.aspx?id=" & nwpk & "&action=edit&source=" & Request.QueryString("source") & "&subaction=preview&page=1")
					else
						DupBname()
   	         	subnav("General")
    				end if
				end if
          
      End Sub
       
        Sub bindfields()
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "Select * from dbo.tbl_adbranding where tbl_branding_pk = '" & Request.QueryString("id") & "'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    If Sqldr("br_showlogo") = "Y" Then
                        chkshowlogo.Checked = True
                        dd_sellogo.Enabled = True
                    Else
                        chkshowlogo.Checked = False
                        dd_sellogo.Enabled = False
                    End If
                    If Sqldr("br_showlogo2") = "Y" Then
                        chkshowlogo2.Checked = True
                        dd_sellogo2.Enabled = True
                    Else
                        chkshowlogo2.Checked = False
                        dd_sellogo2.Enabled = False
                    End If
                    If Sqldr("br_sendemail") = "Y" Then
                        chksendemail.Checked = True
                        TextBox1.Enabled = True
                        TextBox2.Enabled = True
                        TextBox3.Enabled = True
                        'emailbody.readonly = false
                    Else
                        chksendemail.Checked = False
                    End If
                    If Sqldr("br_redirecturl") IsNot DBNull.Value Then
                        txtredirecturl.Text = Sqldr("br_redirecturl")
                    End If
                    If Sqldr("br_text1") IsNot DBNull.Value Then
                        pg1text.content = Sqldr("br_text1")
                    End If
                    If Sqldr("br_text2") IsNot DBNull.Value Then
                        pg2text.content = Sqldr("br_text2")
                    End If
                    If Sqldr("br_emailfrom") IsNot DBNull.Value Then
                        TextBox1.Text = Sqldr("br_emailfrom")
                    End If
                    If Sqldr("br_emailreply") IsNot DBNull.Value Then
                        TextBox2.Text = Sqldr("br_emailreply")
                    End If
                    If Sqldr("br_emailsubject") IsNot DBNull.Value Then
                        TextBox3.Text = Sqldr("br_emailsubject")
                    End If
                    If Sqldr("br_emailbody") IsNot DBNull.Value Then
                        emailbody.content = Sqldr("br_emailbody")
                    End If
                    If Sqldr("br_name") IsNot DBNull.Value Then
                        txtname.Text = Sqldr("br_name")
                    End If
                    If Sqldr("br_description") IsNot DBNull.Value Then
                        txtdescription.Text = Sqldr("br_description")
                    End If
                    If Sqldr("br_emailaddress") IsNot DBNull.Value Then
                        selfemailaddress.Text = Sqldr("br_emailaddress")
                    End If
                    If Sqldr("br_emailaddress2") IsNot DBNull.Value Then
                        selfemailaddress2.Text = Sqldr("br_emailaddress2")
                    End If
                    If Sqldr("br_getemail") = "Y" Then
                        chkemailself.Checked = True
                        selfemailaddress.enabled=true
                    		btnuseremail.enabled=true
                        dd_status.enabled=true
                    		chkemailloglink.enabled=true  
                    		ddexportque.enabled=true                  
                    Else
                        chkemailself.Checked = False
                    End If
                    If Sqldr("br_getemail2") = "Y" Then
                        chkemailself2.Checked = True
                    		selfemailaddress2.enabled=true
	                     btnuseremail2.enabled=true 
	                     dd_status2.enabled=true	                     
	                     chkemailloglink2.enabled=true
                    Else
                        chkemailself2.Checked = False
                    End If
                    If Sqldr("br_company_fk") IsNot DBNull.Value Then
                        txtconame.Text = Sqldr("br_company_fk")
                    End If
                    If Sqldr("br_img_fk") IsNot DBNull.Value Then
                        dd_sellogo.SelectedIndex = dd_sellogo.Items.IndexOf(dd_sellogo.Items.FindByValue(Sqldr("br_img_fk")))
                        If Sqldr("br_img_fk") = "999999" Then
                            If checkforlogo() Then
                                imglogo.Src = System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "/logos/company/" & session("selectedlogo")
                            Else
                                imglogo.Src = System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "/logos/company/default.jpg"
                            End If
                        Else
                            imglogo.Src = System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "/UIMG/" & Left(Session("userid"), Len(Session("userid")) - 4) & "IMG/" & gtimgfilename(dd_sellogo.SelectedItem.Value)
                        End If
             
                    End If
                    If Sqldr("br_img_fk2") IsNot DBNull.Value Then
                        dd_sellogo2.SelectedIndex = dd_sellogo2.Items.IndexOf(dd_sellogo2.Items.FindByValue(Sqldr("br_img_fk2")))
                        If Sqldr("br_img_fk") = "999999" Then
                            If checkforlogo() Then
                                imglogo2.Src = System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "/logos/company/" & session("selectedlogo")
                            Else
                                imglogo2.Src = System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "/logos/company/default.jpg"
                            End If
                        Else
                            imglogo2.Src = System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "/UIMG/" & Left(Session("userid"), Len(Session("userid")) - 4) & "IMG/" & gtimgfilename(dd_sellogo2.SelectedItem.Value)
                        End If
             
                    End If
                    If Sqldr("br_headtxt1Show") IsNot DBNull.Value Then
                        If Sqldr("br_headtxt1Show") = "Y" Then
                            chkhead1.Checked = True
                        Else
                            chkhead1.Checked = False
                        End If
                    Else
                        chkhead1.Checked = False
                    End If
                      If Sqldr("br_headtxt1Show2") IsNot DBNull.Value Then
                        If Sqldr("br_headtxt1Show2") = "Y" Then
                            chkhead12.Checked = True
                        Else
                            chkhead12.Checked = False
                        End If
                    Else
                        chkhead12.Checked = False
                    End If

                    If Sqldr("br_headtxt2Show") IsNot DBNull.Value Then
                        If Sqldr("br_headtxt2Show") = "Y" Then
                            chkhead2.Checked = True
                        Else
                            chkhead2.Checked = False
                        End If
                    Else
                        chkhead2.Checked = False
                    End If
                    If Sqldr("br_headtxt2Show2") IsNot DBNull.Value Then
                        If Sqldr("br_headtxt2Show2") = "Y" Then
                            chkhead22.Checked = True
                        Else
                            chkhead22.Checked = False
                        End If
                    Else
                        chkhead22.Checked = False
                    End If
                    If Sqldr("br_headtext1") IsNot DBNull.Value Then
                        txthead1.Text = Sqldr("br_headtext1")
                    End If
                    If Sqldr("br_headtext2") IsNot DBNull.Value Then
                        txthead2.Text = Sqldr("br_headtext2")
                    End If  
                    
                    If Sqldr("br_showconame") = "Y" Then
                        chkshowco.Checked = True
                      
                    Else
                        chkshowco.Checked = False
                        
                    End If
                      If Sqldr("br_showconame2") = "Y" Then
                        chkshowco2.Checked = True
                        
                    Else
                        chkshowco2.Checked = False
                       
                    End If
                     If Sqldr("br_showHRpg1") = "Y" Then
                        chkshowhr.Checked = True
                       
                    Else
                        chkshowhr.Checked = False
                        
                    End If
                    If Sqldr("br_showHRpg12") = "Y" Then
                        chkshowhr2.Checked = True
                       
                    Else
                        chkshowhr2.Checked = False
                        
                    End If
                    If Sqldr("br_showContinue") = "Y" Then
                        chkshowcntbtn.Checked = True
                       
                    Else
                        chkshowcntbtn.Checked = False
                        
                    End If
                     session("brandno") = Sqldr("tbl_branding_pk")
                    
                   If Sqldr("br_Leadlvl1") IsNot DBNull.Value Then
                        dd_status.SelectedIndex = dd_status.Items.IndexOf(dd_status.Items.FindByValue(Sqldr("br_Leadlvl1")))
                  		If Sqldr("br_Leadlvl1") = "Anonymous" Then
	                			chkemailloglink.checked=false
				               chkemailloglink.enabled=false
						      else
					             chkemailloglink.enabled=true
            				end if
                  end if
                   If Sqldr("br_Leadlvl2") IsNot DBNull.Value Then
                        dd_status2.SelectedIndex = dd_status2.Items.IndexOf(dd_status2.Items.FindByValue(Sqldr("br_Leadlvl2")))
                  		If Sqldr("br_Leadlvl2") = "Anonymous" Then
	                			chkemailloglink2.checked=false
				               chkemailloglink2.enabled=false
						      else
					             chkemailloglink2.enabled=true
            				end if
                  end if
                   If Sqldr("br_emlink") = "Y" Then
                        chkemailloglink.Checked = True                       
                    Else
                        chkemailloglink.Checked = False                        
                    End If
                   If Sqldr("br_emlink2") = "Y" Then
                        chkemailloglink2.Checked = True                       
                    Else
                        chkemailloglink2.Checked = False                        
                    End If
                    
                     If Sqldr("br_bstat") IsNot DBNull.Value Then
                        dd_Bstatus.SelectedIndex = dd_Bstatus.Items.IndexOf(dd_Bstatus.Items.FindByValue(Sqldr("br_bstat")))
                  end if
                   If Sqldr("br_mq") IsNot DBNull.Value Then
                        ddexportque.SelectedIndex = ddexportque.Items.IndexOf(ddexportque.Items.FindByValue(Sqldr("br_mq")))
                  	
                  end if 
                  
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try

        End Sub
        Sub insertdefault(ByVal Source As System.Object, ByVal e As System.EventArgs)


        End Sub

        Sub enablebutts(ByVal Source As System.Object, ByVal e As System.EventArgs)
            If chksendemail.Checked = True Then
                TextBox1.Enabled = True
                TextBox2.Enabled = True
                TextBox3.Enabled = True
                'emailbody.readonly = false
            Else
                TextBox1.Enabled = False
                TextBox2.Enabled = False
                TextBox3.Enabled = False
                'emailbody.readonly = true
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                emailbody.content = ""
            End If
			TextBox1.focus()
        End Sub
        Sub enablebuttsA(ByVal Source As System.Object, ByVal e As System.EventArgs)
            If chkemailself.Checked = True Then
                selfemailaddress.Enabled = True
            Else
                selfemailaddress.Enabled = False
                selfemailaddress.Text = ""
            End If

        End Sub
        Sub Cancelbranding(ByVal Source As System.Object, ByVal e As System.EventArgs)
            If Request.QueryString("source") = "rspmgr" Then
                Response.Redirect("admanager.aspx?source=rspmgr")
            End If
            If Request.QueryString("type") = "quick" Then
                Response.Redirect("createad.aspx?action=edit&adno=" & Session("adno"))
            End If
            If Request.QueryString("type") = "complete" Then
            	if Request.QueryString("id") = "" then 
                	Response.Redirect("createad.aspx?action=edit&adno=" & Session("adno") & "&nav=branding&bname=cancel")
            	else
            	   Response.Redirect("createad.aspx?adno=" & Request.QueryString("adno") & "&action=edit&nav=branding&bname=" & request.querystring("id"))
   
            	end if	
            End If
            
        End Sub
        Public Function getbraandpk() As String

            If Request.QueryString("action") = "new" Then
                Dim nwpk As String = getrespid()
                Return nwpk
            Else
                Return  session("brandno")
            End If
        End Function
			 
			Sub updatebranding(ByVal Source As System.Object, ByVal e As System.EventArgs)
			  	if request.querystring("action")="new" then
			  		if not namecheckdup("new") then
						savebranding()
            		getbrandno()
     		         txtname.BackColor = white
      		      txtdescription.focus()
            		response.redirect("brandingadd.aspx?action=edit&source=" & request.querystring("source") & "&id=" & session("currentbrandno"))
          		else
	            	DupBname()
   	         	subnav("General")
    				end if
         	else
         		if not namecheckdup("edit") then
					   txtname.BackColor = white
      		      txtdescription.focus()
            		savebranding()
					else
						DupBname()
   	         	subnav("General")
    				end if
				end if
          
        	End Sub
        Sub updatebrandingE(ByVal Source As System.Object, ByVal e As System.EventArgs)
				if request.querystring("action")="new" then
			  		if not namecheckdup("new") then
						savebranding()
            		getbrandno()
     		         txtname.BackColor = white
      		      txtdescription.focus()
          			If Request.QueryString("source") = "rspmgr" Then
                		Response.Redirect("admanager.aspx?source=rspmgr")
            		Else
               		Response.Redirect("createad.aspx?adno=" & Request.QueryString("adno") & "&action=edit&nav=branding&bname=" & txtname.Text)
             		End If
          		else
	            	DupBname()
   	         	subnav("General")
    				end if
         	else
         		if not namecheckdup("edit") then
					   txtname.BackColor = white
      		      txtdescription.focus()
            		savebranding()
            		If Request.QueryString("source") = "rspmgr" Then
                		Response.Redirect("admanager.aspx?source=rspmgr")
            		Else
                		Response.Redirect("createad.aspx?adno=" & Request.QueryString("adno") & "&action=edit&nav=branding&bname=" & request.querystring("id"))
            		End If
					else
						DupBname()
   	         	subnav("General")
    				end if
				end if
            
            
        End Sub

 		sub getbrandno()

            Dim strSql As String = "SELECT max(tbl_branding_pk) as 'newpk' from dbo.tbl_adbranding"
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    session("currentbrandno") = Sqldr("newpk")
                End If

            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try


        End Sub
        
        Sub savebranding()
            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String
				
	            If Request.QueryString("action") = "new" Then
	                sqlproc = "sp_insertbranding"
	            Else
	                sqlproc = "sp_updatebranding"
	            End If
	
	            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
	            myCommandADD.CommandType = CommandType.StoredProcedure
	
	            If Request.QueryString("action") = "edit" Then
	                Dim prmbrpk As New SqlParameter("@brpk ", SqlDbType.Int)
	                prmbrpk.Value =  session("brandno")
	                myCommandADD.Parameters.Add(prmbrpk)
	            End If
	
	            Dim prmname As New SqlParameter("@name", SqlDbType.VarChar, 50)
	            prmname.Value = txtname.Text
	            myCommandADD.Parameters.Add(prmname)
	
	            Dim prmuid As New SqlParameter("@uid", SqlDbType.VarChar, 50)
	            prmuid.Value = Session("userid")
	            myCommandADD.Parameters.Add(prmuid)
	
	            Dim prmadno As New SqlParameter("@adno", SqlDbType.VarChar, 50)
	            prmadno.Value = DBNull.Value
	            myCommandADD.Parameters.Add(prmadno)
	
	            Dim prmcompany As New SqlParameter("@company", SqlDbType.VarChar, 50)
	            prmcompany.Value = txtconame.Text
	            myCommandADD.Parameters.Add(prmcompany)
	
	            Dim prmdescrip As New SqlParameter("@descript", SqlDbType.VarChar, 255)
	            prmdescrip.Value = txtdescription.Text
	            myCommandADD.Parameters.Add(prmdescrip)
	
	            Dim prmlogo As New SqlParameter("@logo", SqlDbType.VarChar, 1)
	            If chkshowlogo.Checked = True Then
	                prmlogo.Value = "Y"
	            Else
	                prmlogo.Value = "N"
	            End If
	            myCommandADD.Parameters.Add(prmlogo)
	
	            Dim prmlogo2 As New SqlParameter("@logo2", SqlDbType.VarChar, 1)
	            If chkshowlogo2.Checked = True Then
	                prmlogo2.Value = "Y"
	            Else
	                prmlogo2.Value = "N"
	            End If
	            myCommandADD.Parameters.Add(prmlogo2)
	
			      Dim prmemail As New SqlParameter("@email", SqlDbType.VarChar, 1)
	            If chksendemail.Checked = True Then
	                prmemail.Value = "Y"
	            Else
	                prmemail.Value = "N"
	            End If
	            myCommandADD.Parameters.Add(prmemail)
	
	            Dim prmredirect As New SqlParameter("@redirect", SqlDbType.VarChar, 255)
	            prmredirect.Value = txtredirecturl.Text
	            myCommandADD.Parameters.Add(prmredirect)
	
	            Dim prmtext1 As New SqlParameter("@text1", SqlDbType.Text)
	            prmtext1.Value = pg1text.content '  .Replace(vbCrLf, "")
	            myCommandADD.Parameters.Add(prmtext1)
	
	            Dim prmtext2 As New SqlParameter("@text2", SqlDbType.Text)
	            prmtext2.Value = pg2text.content
	            myCommandADD.Parameters.Add(prmtext2)
	
	
	            Dim prmemailfrom As New SqlParameter("@emailfrom", SqlDbType.VarChar, 50)
	            prmemailfrom.Value = TextBox1.Text
	            myCommandADD.Parameters.Add(prmemailfrom)
	
	            Dim prmreplyto As New SqlParameter("@replyto", SqlDbType.VarChar, 50)
	            prmreplyto.Value = TextBox1.Text
	            myCommandADD.Parameters.Add(prmreplyto)
	
	            Dim prmsubject As New SqlParameter("@subject", SqlDbType.VarChar, 255)
	            prmsubject.Value = TextBox3.Text
	            myCommandADD.Parameters.Add(prmsubject)
	
	            Dim prmbody As New SqlParameter("@body", SqlDbType.Text)
	            prmbody.Value = emailbody.content
	            myCommandADD.Parameters.Add(prmbody)
	
	            Dim prmem As New SqlParameter("@getemail", SqlDbType.VarChar, 50)
	            If chkemailself.Checked = True Then
	                prmem.Value = "Y"
	            Else
	                prmem.Value = "N"
	            End If
	            myCommandADD.Parameters.Add(prmem)
	            
	            Dim prmem2 As New SqlParameter("@getemail2", SqlDbType.VarChar, 50)
	            If chkemailself2.Checked = True Then
	                prmem2.Value = "Y"
	            Else
	                prmem2.Value = "N"
	            End If
	            myCommandADD.Parameters.Add(prmem2)
	
	            Dim prmemadd As New SqlParameter("@emailaddress", SqlDbType.VarChar, 255)
	            prmemadd.Value = selfemailaddress.Text
	            myCommandADD.Parameters.Add(prmemadd)
	            
	            Dim prmemadd2 As New SqlParameter("@emailaddress2", SqlDbType.VarChar, 255)
	            prmemadd2.Value = selfemailaddress2.Text
	            myCommandADD.Parameters.Add(prmemadd2)
	
	            Dim prmimgfk As New SqlParameter("@imgfk", SqlDbType.Int)
	            prmimgfk.Value = dd_sellogo.SelectedItem.Value
	            myCommandADD.Parameters.Add(prmimgfk)
	            
	            Dim prmimgfk2 As New SqlParameter("@imgfk2", SqlDbType.Int)
	            prmimgfk2.Value = dd_sellogo2.SelectedItem.Value
	            myCommandADD.Parameters.Add(prmimgfk2)
	
	            Dim prmhdt1 As New SqlParameter("@hdtxt1", SqlDbType.VarChar, 1)
	            If chkhead1.Checked = True Then
	                prmhdt1.Value = "Y"
	            Else
	                prmhdt1.Value = "N"
	            End If
	            myCommandADD.Parameters.Add(prmhdt1)
	
	            Dim prmhdt12 As New SqlParameter("@hdtxt12", SqlDbType.VarChar, 1)
	            If chkhead12.Checked = True Then
	                prmhdt12.Value = "Y"
	            Else
	                prmhdt12.Value = "N"
	            End If
	            myCommandADD.Parameters.Add(prmhdt12)
	
					Dim prmhdt2 As New SqlParameter("@hdtxt2", SqlDbType.VarChar, 1)
	            If chkhead2.Checked = True Then
	                prmhdt2.Value = "Y"
	            Else
	                prmhdt2.Value = "N"
	            End If
	            myCommandADD.Parameters.Add(prmhdt2)
	
	      		Dim prmhdt22 As New SqlParameter("@hdtxt22", SqlDbType.VarChar, 1)
	            If chkhead22.Checked = True Then
	                prmhdt22.Value = "Y"
	            Else
	                prmhdt22.Value = "N"
	            End If
	            myCommandADD.Parameters.Add(prmhdt22)
	
	      
	            Dim prmhdtxt1t As New SqlParameter("@hdtxt1t ", SqlDbType.VarChar, 255)
	            prmhdtxt1t.Value = txthead1.Text
	            myCommandADD.Parameters.Add(prmhdtxt1t)
	
	            Dim prmhdtxt2t As New SqlParameter("@hdtxt2t ", SqlDbType.VarChar, 255)
	            prmhdtxt2t.Value = txthead2.Text
	            myCommandADD.Parameters.Add(prmhdtxt2t)
	
					Dim prmconame As New SqlParameter("@sconame", SqlDbType.VarChar, 1)
	            If chkshowco.Checked = True Then
	                prmconame.Value = "Y"
	            Else
	                prmconame.Value = "N"
	            End If
	            myCommandADD.Parameters.Add(prmconame)
	            
	            Dim prmconame2 As New SqlParameter("@sconame2", SqlDbType.VarChar, 1)
	            If chkshowco2.Checked = True Then
	                prmconame2.Value = "Y"
	            Else
	                prmconame2.Value = "N"
	            End If
	            myCommandADD.Parameters.Add(prmconame2)
	            
	            Dim prmhrline As New SqlParameter("@shrline", SqlDbType.VarChar, 1)
	            If chkshowhr.Checked = True Then
	                prmhrline.Value = "Y"
	            Else
	                prmhrline.Value = "N"
	            End If
	            myCommandADD.Parameters.Add(prmhrline)
	            
	            Dim prmhrline2 As New SqlParameter("@shrline2", SqlDbType.VarChar, 1)
	            If chkshowhr2.Checked = True Then
	                prmhrline2.Value = "Y"
	            Else
	                prmhrline2.Value = "N"
	            End If
	            myCommandADD.Parameters.Add(prmhrline2)
	          
	            Dim prmcont As New SqlParameter("@scont", SqlDbType.VarChar, 1)
	            If chkshowcntbtn.Checked = True Then
	                prmcont.Value = "Y"
	            Else
	                prmcont.Value = "N"
	            End If
	            myCommandADD.Parameters.Add(prmcont)
	            
	            Dim prmldlvl1 As New SqlParameter("@ldlvl1", SqlDbType.VarChar, 50)
	            prmldlvl1.Value = dd_status.SelectedItem.Value
	            myCommandADD.Parameters.Add(prmldlvl1)
	            
	            Dim prmldlvl2 As New SqlParameter("@ldlvl2", SqlDbType.VarChar, 50)
	            prmldlvl2.Value = dd_status2.SelectedItem.Value
	            myCommandADD.Parameters.Add(prmldlvl2)
	            
	            Dim prmloginlnk As New SqlParameter("@lglink", SqlDbType.VarChar, 1)
	            If chkemailloglink.Checked = True Then
	                prmloginlnk.Value = "Y"
	            Else
	                prmloginlnk.Value = "N"
	            End If
	            myCommandADD.Parameters.Add(prmloginlnk)
	            
	            Dim prmloginlnk2 As New SqlParameter("@lglink2", SqlDbType.VarChar, 1)
	            If chkemailloglink2.Checked = True Then
	                prmloginlnk2.Value = "Y"
	            Else
	                prmloginlnk2.Value = "N"
	            End If
	            myCommandADD.Parameters.Add(prmloginlnk2)
	            
	            Dim prmbstat As New SqlParameter("@bstat", SqlDbType.VarChar, 50)
	            prmbstat.Value = dd_Bstatus.SelectedItem.Value
	            myCommandADD.Parameters.Add(prmbstat)
	            
	            Dim prmmq As New SqlParameter("@mq", SqlDbType.VarChar, 50)
	            prmmq.Value = ddexportque.SelectedItem.Value
	            myCommandADD.Parameters.Add(prmmq)           
	
	            Try
	                myConnectionADD.Open()
	                myCommandADD.ExecuteNonQuery()
	                myConnectionADD.Close()
	            Catch SQLexc As SqlException
	                Response.Write("Insert Failed. Error Details are: " & SQLexc.ToString())
	            Finally
	                myConnectionADD.Close()
	            End Try
	     End Sub
        Public Function getrespid() As String

            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "Select max(tbl_branding_pk) as 'maxpk' from dbo.tbl_adbranding where br_uid_fk = '" & Session("userid") & "'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    Return Sqldr("maxpk")
                End If
            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try

        End Function

        Public Sub pagesetup()

            'width will be calculated automatically, but it is sometimes
            layout.Width = System.Configuration.ConfigurationManager.AppSettings("ScreenWidth")
            leftNav.Width = System.Configuration.ConfigurationManager.AppSettings("ScreenLeftNav")
            body.Height = System.Configuration.ConfigurationManager.AppSettings("ScreenBodyHeight")
            body.Width = System.Configuration.ConfigurationManager.AppSettings("ScreenBodyWidth")
            layout.Border = System.Configuration.ConfigurationManager.AppSettings("ScreenBorder")
            footer.Controls.Add(LoadControl(System.Configuration.ConfigurationManager.AppSettings("ScreenFooter")))
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

 		Sub subnav(ByVal button As String)
            Dim clickedbutton As String = button

            'Set cell class
            subnavGen.Attributes.Add("class", "tblcelltest")
            subnavPage1.Attributes.Add("class", "tblcelltest")
            subnavPage2.Attributes.Add("class", "tblcelltest")
            subnavresp.Attributes.Add("class", "tblcelltest")
            subnavimgs.Attributes.Add("class", "tblcelltest")
            subnavADS.Attributes.Add("class", "tblcelltest")
            
            'Set button font color
            Lgen.ForeColor = System.Drawing.Color.Black
            lpage1.ForeColor = System.Drawing.Color.Black
            lpage2.ForeColor = System.Drawing.Color.Black
            lautop.ForeColor = System.Drawing.Color.Black
            limgs.ForeColor = System.Drawing.Color.Black
            lads.ForeColor = System.Drawing.Color.Black
            
            'Set spacers
            spacer0.Visible = True
            spacer1.Visible = True
            spacer2.Visible = True 
            spacer3.Visible = True   
            spacer4.Visible = True   

            'Set Panels
            pnlgeneral.Visible = False
            pnlpage1.Visible = False
            pnlpage2.visible=false
            pnlnotifications.visible=false
            pnlimages.visible=false
            pnlADS.visible=false
           
            If clickedbutton = "General" Then
                subnavGen.Attributes.Add("class", "tblcelltestSelected")
                Lgen.ForeColor = System.Drawing.Color.White
                spacer0.Visible = False
                pnlgeneral.Visible = True
            ElseIf clickedbutton = "Intake" Then
                subnavPage1.Attributes.Add("class", "tblcelltestSelected")
                lpage1.ForeColor = System.Drawing.Color.White
                spacer0.Visible = False
                spacer1.Visible = False               
                pnlpage1.Visible = True
            ElseIf clickedbutton = "Confirmation" Then
                subnavPage2.Attributes.Add("class", "tblcelltestSelected")
                lpage2.ForeColor = System.Drawing.Color.White
                spacer1.Visible = False
                spacer2.Visible = False
                pnlpage2.Visible = True
            ElseIf clickedbutton = "Responses" Then
                subnavresp.Attributes.Add("class", "tblcelltestSelected")
                lautop.ForeColor = System.Drawing.Color.White
                spacer2.Visible = False
                 Spacer3.Visible = False
                pnlnotifications.Visible = true
            ElseIf clickedbutton = "Images" Then
                subnavimgs.Attributes.Add("class", "tblcelltestSelected")
                limgs.ForeColor = System.Drawing.Color.White
                spacer3.Visible = False
               
                pnlimages.Visible = true 
           ElseIf clickedbutton = "ADS" Then
                subnavADS.Attributes.Add("class", "tblcelltestSelected")
                lads.ForeColor = System.Drawing.Color.White
                spacer4.Visible = False
               
                pnlADS.Visible = true         
            else
             	subnavGen.Attributes.Add("class", "tblcelltestSelected")
                Lgen.ForeColor = System.Drawing.Color.White
                spacer0.Visible = False
                pnlgeneral.Visible = True          
            End If
        End Sub
        
         Sub prefillemail(ByVal Source As System.Object, ByVal e As System.EventArgs)
           
            
            If ddemailcor.SelectedItem.Text = "Clear" Then
                TextBox3.Text = ""
                emailbody.content = ""
                pnltempatespre.visible=false
            ElseIf ddemailcor.SelectedItem.Text = "Add" Then
                Response.Redirect("emailmainteditadd.aspx?action=new&source=sleademail")
            Else
                pnltempatespre.visible=true
            	bindtemppreview()
            End If

        End Sub
        
        Public Sub getemailcor()

         Dim strConnection As String
         Dim sqlConn As SqlConnection
         Dim sqlCmd As SqlCommand
         Dim strSql As String = "select * from tbl_emails where email_tbl_pk='" & ddemailcor.SelectedItem.Value & "'"

         Try
             strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
             sqlConn = New SqlConnection(strConnection)
             sqlCmd = New SqlCommand(strSql, sqlConn)
             sqlConn.Open()
             Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

             If Sqldr.Read() Then
                 If Sqldr("email_subject") IsNot DBNull.Value Then
                     TextBox3.Text = Sqldr("email_subject")
                 End If
                 If Sqldr("email_text") IsNot DBNull.Value Then
                          emailbody.content = Sqldr("email_text")
                    

                 End If

             End If

         Catch ex As Exception
             Response.Write(ex.ToString())
         Finally
             sqlConn.Close()
         End Try

     End Sub
     
      Sub Fillemailcor()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select *, convert(varchar(20),email_date,101) as 'emdate', " _
            				& "case when (len(email_name) > 30) then cast (email_tbl_pk as varchar(255)) + ' ' + " _
            				& "left(email_name,30) else cast (email_tbl_pk as varchar(255)) + ' ' + email_name end as 'Tname' " _
            				& "from tbl_emails " _
								& "join tbl_xwalk on tbl_xwalk_pk = email_stat " _
								& "where x_descr <> 'Do Not Use' " _
								& "and ((email_uid='" & Session("userid") & "' and x_descr='Private') " _
								& "or (x_descr='Company Wide' and email_co='" & Session("company_pk") & "') " _
								& "or (x_descr='System Wide')) order by email_name"
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                ddemailcor.DataSource = dataReader
                ddemailcor.DataTextField = "Tname"
                ddemailcor.DataValueField = "email_tbl_pk"
                ddemailcor.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
            ddemailcor.Items.Insert(0, New ListItem("Select..", "9999"))
            'ddemailcor.Items.Insert(0, New ListItem("Add New", "99992"))
            ddemailcor.Items.Insert(1, New ListItem("Clear", "99992"))

        End Sub
        
         Public Sub bindimages()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            mycommand = "Select *, '" & System.Configuration.ConfigurationManager.AppSettings("CurrentwebURL") & "'+ '/uimg/' + Left(ui_uid, Len(ui_uid) - 4) + 'IMG/' + ui_filename as 'imgurl', " _
            				& " '<img src=''' + '" & System.Configuration.ConfigurationManager.AppSettings("CurrentwebURL") & "'+ '/uimg/' + Left(ui_uid, Len(ui_uid) - 4) + 'IMG/' + ui_filename + ''' />' as 'imghtml' " _
            				& "from tbl_userimages where ui_uid='" & Session("userid") & "'"
            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
               
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_userimages")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_userimages"))
                dgimages.DataSource = dvProducts                
                dgimages.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
        End Sub
        
        Sub images_PageChanger(ByVal Source As Object, ByVal E As DataGridPageChangedEventArgs)
            ' Set the CurrentPageIndex before binding the grid 
            dgimages.CurrentPageIndex = E.NewPageIndex
            bindimages()

        End Sub
        
        	Sub ItemDataBoundEventHandler(sender as Object, e as DataGridItemEventArgs)
		   	If e.Item.ItemType = ListItemType.Item OR e.Item.ItemType = ListItemType.AlternatingItem then
			   	
			   end if
		   
		   end sub
		   
		    Sub ItemDataBoundEventHandlerA(ByVal sender As Object, ByVal e As DataGridItemEventArgs)

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            	
		    	end if
		    	If e.Item.ItemType = ListItemType.Header Then
					 Dim testbtn As linkbutton            	 
            	 testbtn = e.Item.Cells(3).FindControl("vwbodyA")
            	 if session("bdlevel")="Full" then
	            	testbtn.text="[Brief View]"            	
	           	else
	           		testbtn.text="[Detail View]"
	           	end if
				End If

        end sub
        
         Sub bindtemppreview()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            
            if session("bdlevel")="Full" then
            	mycommand = "select *,email_text as 'bdtext' from tbl_emails where email_tbl_pk='" & ddemailcor.SelectedItem.Value & "'"
    
            else
            	mycommand = "select *,left(convert(varchar(255),email_text),25)+'...' as 'bdtext' from tbl_emails where email_tbl_pk='" & ddemailcor.SelectedItem.Value & "'"

            end if
            
            

            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_emails")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_emails"))
               
                temppreview.DataSource = dvProducts
                temppreview.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
        End Sub
        
         Public Sub appendAll(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Dim x As Button = Source

            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim temppkid As String = item.Cells(0).Text
            Dim tsub As String = item.Cells(2).Text
            Dim tbody As String = item.Cells(5).Text
            emailbody.content = tbody            
            TextBox3.text = tsub
            pnltempatespre.visible=false
            ddemailcor.SelectedIndex = ddemailcor.Items.IndexOf(ddemailcor.Items.FindByText("Select.."))
        End Sub
         Public Sub appendBody( Source As System.Object, ByVal e As System.EventArgs)
            Dim x As Button = Source

            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim temppkid As String = item.Cells(0).Text
            Dim tsub As String = item.Cells(2).Text
            Dim tbody As String = item.Cells(5).Text
            
            emailbody.content = emailbody.content + "<br>" + tbody
            pnltempatespre.visible=false
            ddemailcor.SelectedIndex = ddemailcor.Items.IndexOf(ddemailcor.Items.FindByText("Select.."))
        End Sub
         Public Sub appendSubject(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Dim x As Button = Source

            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim temppkid As String = item.Cells(0).Text
            Dim tsub As String = item.Cells(2).Text
            Dim tbody As String = item.Cells(5).Text
            
            TextBox3.text = TextBox3.text + "<br>" + tsub
            pnltempatespre.visible=false
        End Sub
        
        
       Public Sub showbdyA(ByVal Source As System.Object, ByVal e As System.EventArgs)
           
            if session("bdlevel")="Full" then
            	session("bdlevel")="Brief"
            	bindtemppreview()
            	
            	
           	else
           		session("bdlevel")="Full"
           		bindtemppreview()
           		
           	end if
        End Sub
         Public Sub Canceltemplate(ByVal Source As System.Object, ByVal e As System.EventArgs)
            pnltempatespre.visible=false
            ddemailcor.SelectedIndex = ddemailcor.Items.IndexOf(ddemailcor.Items.FindByText("Select.."))
	   
        End Sub
        
        
         Sub fillexportque()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select distinct eq_tbl_pk,eq_name from tbl_leadexportqueue where eq_uid='" & Session("userid") & "'"
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                ddexportque.DataSource = dataReader
                ddexportque.DataTextField = "eq_name"
                ddexportque.DataValueField = "eq_tbl_pk"

                ddexportque.DataBind()

            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
             ddexportque.Items.Insert(0, New ListItem("None", "999998"))
				
        End Sub
    End Class
end namespace