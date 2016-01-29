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
Imports System.Text
Imports System.Net.Mail
Imports FreeTextBoxControls

namespace PageTemplate
    Public Class emaillead
        Inherits Page
        Public emailhistory, emailmain, pnlldfields, pnltempates, emailhead, pnltempatespre, pnltemplates As Panel
        Public lead_history,temppreview As DataGrid
        Public myCheckbox, chkattachlead As CheckBox
        Public attachments As Table
        Public emailfrom, emailto, emailcc, emailsubject,l_search As TextBox
       
        Public ddemailcor As DropDownList
        public emailbody As FreeTextBox
        public chkldno,chkldname,chkldtype,chklddate,chkldph,chkldem,chkldnotem as checkbox
        public btnshowtemps as button
        public emails as datagrid
        public vwbodyA as linkbutton
        Public dd_status As DropDownList
        Public lbltemplatepreview As Label

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

            If Not (Page.IsPostBack) Then
                clearsessions()
                Session("bdlevel") = "Brief"
                Fillemailcor()
                chkldno.Enabled = False
                chkldname.Enabled = False
                chkldtype.Enabled = False
                chklddate.Enabled = False
                chkldph.Enabled = False
                chkldem.Enabled = False
                chkldnotem.Enabled = False
            End If


        End Sub
        sub clearsessions()
        		session("mbody")=""
        		session("bdlevel")=""
         
         
         end sub
        
        Sub btn_histscreen(ByVal sender As Object, ByVal e As EventArgs)
            emailhistory.Visible = True
            emailmain.Visible = False
            BindhistoryGrid()
        End Sub
        Sub showtemplates(ByVal Source As Object, ByVal e As ImageClickEventArgs)
            pnltemplates.Visible = True
            Fillemailcor()

        End Sub



        Sub BindhistoryGrid()
            Dim strpropID As String = Session("clead")
            Dim strUID As String = session("userid")
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
                 mycommand = "Select *, cast(tbl_leadcnthistory_pk as varchar(20)) as 'hnum',cast(tbl_leads_fk as varchar(20)) as 'lnum2', convert(varchar(20),cnt_date,101) as date from tbl_leadscontacthistory Where tbl_leads_FK=" & strpropID
            
            Try
                Dim dataAdapter As New SqlDataAdapter(myCommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_leadscontacthistory")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_leadscontacthistory"))
                lead_history.DataSource = dvProducts
                lead_history.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
        End Sub

        Sub btn_histexit(ByVal sender As Object, ByVal e As EventArgs)
            emailhistory.Visible = False
            emailmain.Visible = True

        End Sub
        Public Sub chkaddhistory(ByVal sender As System.Object, ByVal e As System.EventArgs)
            Dim rowCount As Integer = 0
            Dim gridSelections As StringBuilder = New StringBuilder()

            'Loop through each DataGridItem, and determine which CheckBox controls
            'have been selected.
            Dim DemoGridItem As DataGridItem
            For Each DemoGridItem In lead_history.Items

                Dim myCheckbox As CheckBox = CType(DemoGridItem.Cells(0).Controls(1), CheckBox)
                If myCheckbox.Checked = True Then
                    rowCount += 1
                    'Response.Write("The checkbox for " & lead_history.DataKeys(DemoGridItem.ItemIndex).ToString())
                    'gridSelections.AppendFormat("The checkbox for " &  DGPropMatches.DataKeys(DemoGridItem.ItemIndex).ToString() & "was selected<br>")
                    ', _
                    '                           DGPropMatches.DataKeys(DemoGridItem.ItemIndex).ToString())
                End If
            Next
            gridSelections.Append("<hr>")
            gridSelections.AppendFormat("Total number selected is: {0}<br>", rowCount.ToString())
            Response.Write(gridSelections.ToString())
        End Sub
          
          Public Sub showlfs(ByVal sender As Object, ByVal e As EventArgs)
          	if chkattachlead.checked then
          		pnlldfields.visible=true
          		dd_status.visible=true
          		if session("role")<>"GOD" then
          			Dim removeListItem As ListItem = dd_status.Items.FindByText("Anonymous") 
          			dd_status.Items.remove(removeListItem)
          		end if
          		dd_status.Items.Insert(1, New ListItem("Discrete", "99999"))
          	else
          		pnlldfields.visible=false
          		dd_status.visible=false
          	end if
          	
          end sub
      

        Public Sub click_cancelemail(ByVal sender As Object, ByVal e As EventArgs)
            Dim msg As String = ""
            msg = msg & "<Script Language='JavaScript'>"
            msg = msg & "window.opener.location.href=window.opener.location.href;"
            msg = msg & "window.close();"
            msg = msg & "</Script>"
            Response.Write(msg)
        End Sub
        
      
        
        Public Sub click_sendemail(ByVal sender As Object, ByVal e As EventArgs)
            Dim mail As New MailMessage()

            'Set the properties - send the email to the person who filled out the
            mail.From = New MailAddress(emailfrom.Text)
            mail.To.Add(emailto.Text)
            If emailcc.Text <> "" Then
                mail.CC.Add(emailcc.Text)
            End If
            If emailsubject.Text <> "" Then
                mail.Subject = emailsubject.Text
            Else
                mail.Subject = ""
            End If

            'Set the body
            If emailbody.Text <> "" Then
                mail.Body = emailbody.Text.Replace(vbCrLf, "")
            End If

            If chkattachlead.Checked Then
                mail.Body = mail.Body + "<br ><br>" + sendleadinfo()
            End If
            session("mbody") = mail.Body
            'send the message
            mail.IsBodyHtml = True
            'Dim smtp As New SmtpClient("smtp.comcast.net")
          		Dim smtp As New SmtpClient(System.Configuration.ConfigurationManager.AppSettings("CurrentEmailServer"))
      	  smtp.Send(mail)

            inserthistoryrecord()

            Dim msg As String = ""
            msg = msg & "<Script Language='JavaScript'>"
            msg = msg & "window.opener.location.href=window.opener.location.href;"
            msg = msg & "window.close();"
            msg = msg & "</Script>"
            Response.Write(msg)
        End Sub

        Public Function sendleadinfo() As String
            Dim strSql As String = "SELECT *,case when (ld_lname is null) then ld_fname else ld_fname + ' ' + ld_lname end  as 'fullname'  from tbl_leads where tbl_leads_pk='" & Request.QueryString("id") & "'"
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim ldinfo As String
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    ldinfo = "<table><tr><td><b><u>Below are the lead details.</u></b></td></tr></table><br><table> "  
                		if dd_status.selecteditem.text="Basic" then	
                			ldinfo =  ldinfo + "<tr><td>Lead #:</td><td>" & Request.QueryString("id") & "</td></tr>" 
			         		ldinfo =  ldinfo + "<td>Lead Type:</td><td>" & Sqldr("ld_type") & "</td></tr>" 
					         ldinfo =  ldinfo + "<td>Name:</td><td>" & Sqldr("fullname") & "</td></tr>"
					         if sqldr("ld_email") isnot dbnull.value then
									ldinfo =  ldinfo +  "<td>Email:</td><td>" & Sqldr("ld_email") & "</td></tr>" 
								end if
							elseif dd_status.selecteditem.text="Full Information" then	
         					ldinfo =  ldinfo + "<tr><td>Lead #:</td><td>" & Request.QueryString("id") & "</td></tr>" 
		                  ldinfo =  ldinfo + "<td>Lead Type:</td><td>" & Sqldr("ld_type") & "</td></tr>" 
		                  ldinfo =  ldinfo + "<td>Lead Date:</td><td>" & Sqldr("ld_capturedate") & "</td></tr>"  
		                  ldinfo =  ldinfo + "<td>Name:</td><td>" & Sqldr("fullname") & " </td></tr>"  
		                  if sqldr("ld_hphone") isnot dbnull.value then	           
		                  	ldinfo =  ldinfo + "<td>Home Phone:</td><td>" & Sqldr("ld_hphone") & "</td></tr>"
                    		end if
                        if sqldr("ld_cphone") isnot dbnull.value then	           
		              			ldinfo =  ldinfo + "<td>Cell Phone:</td><td>" & Sqldr("ld_cphone") & "</td></tr>"
		                  end if
		                  if sqldr("ld_email") isnot dbnull.value then	           
		              		   ldinfo =  ldinfo +  "<td>Email:</td><td>" & Sqldr("ld_email") & "</td></tr>" 
		                	end if
		               	if sqldr("ld_email2") isnot dbnull.value then	           
		              	  		ldinfo =  ldinfo + "<td>Other Email:</td><td>" & Sqldr("ld_email2") & "</td></tr>" 
		                  end if
		               	if sqldr("ld_notes") isnot dbnull.value then	           
		              		   ldinfo =  ldinfo +  "<td>Notes:</td></tr><tr><td>" &  Sqldr("ld_notes") & "</td></tr>" 
                    		end if
                    elseif dd_status.selecteditem.text="Anonymous" then 	                    	
			               ldinfo =  ldinfo + "<tr><td><b>Web Magic has a New Lead ready for you to contact!</b></td></tr></table><br><table> " 
			          		ldinfo =  ldinfo + "<tr><td colspan=2><u>Below are the details for this Lead.</u></td></tr> "            
			            	ldinfo =  ldinfo + "<tr><td>Lead Type:</td><td> " & Sqldr("ld_type") & "</td></tr> "
			            	ldinfo =  ldinfo + "<tr><td>Lead Name:</td><td> " & left(Sqldr("fullname"),3)+"xxxx" & "</td></tr> "
			            	if sqldr("ld_email") isnot dbnull.value then	           
		              			ldinfo =  ldinfo + "<tr><td>Lead Email:</td><td> " & left(Sqldr("ld_email"),3)+"xxxx" & "</td></tr> "
			            	end if
			            	if sqldr("ld_hphone") isnot dbnull.value then	           
		              		  	ldinfo =  ldinfo + "<tr><td>Lead Phone:</td><td> " & left(Sqldr("ld_hphone"),3) + "xxxx" & "</td></tr></table><br>  "
			            	end if
			            	ldinfo =  ldinfo + "<table><tr><td><a href=" & System.Configuration.ConfigurationManager.AppSettings("CurrentwebURL") & "/buyleads.aspx?id=" & Request.QueryString("id") & "&email=" & emailto.text & ">CLICK HERE</a> &nbsp To buy this lead NOW and get the contact info immediately by email</td></tr>"
			            	ldinfo =  ldinfo + "<tr><td><a href=" & System.Configuration.ConfigurationManager.AppSettings("CurrentwebURL") & "/gsignup.aspx>CLICK HERE</a> To buy access to unlimited lead generation for a LOW monthly flat fee</td></tr></table><br>"
			            	ldinfo =  ldinfo + "<table><tr><td>Thank you for letting Web Magic Portal work for you!</td></tr>"
			        
                    else
                    		 if chkldno.checked then 
		                    		ldinfo =  ldinfo + "<tr><td>Lead #:</td><td>" & Request.QueryString("id") & "</td></tr>" 
		                    end if
		                    if chkldtype.checked then 
		                    		ldinfo =  ldinfo + "<td>Lead Type:</td><td>" & Sqldr("ld_type") & "</td></tr>" 
		                    end if
		                    if chklddate.checked then 
		                    		ldinfo =  ldinfo + "<td>Lead Date:</td><td>" & Sqldr("ld_capturedate") & "</td></tr>"  
		                    end if
		                    if chkldname.checked then 
		                    		ldinfo =  ldinfo + "<td>Name:</td><td>" & Sqldr("fullname") & "</td></tr>"  
		                    end if
		                    if chkldph.checked then 
		                  		if sqldr("ld_hphone") isnot dbnull.value then	           
		              		  		ldinfo =  ldinfo + "<td>Home Phone:</td><td>" & Sqldr("ld_hphone") & "</td></tr>"
		                    		end if
		                    		ldinfo =  ldinfo + "<td>Cell Phone:</td><td>" & Sqldr("ld_cphone") & "</td></tr>"
		                    end if		                    
		                    if chkldem.checked then 
		                    		ldinfo =  ldinfo +  "<td>Email:</td><td>" & Sqldr("ld_email") & "</td></tr>" 
		                    		ldinfo =  ldinfo + "<td>Other Email:</td><td>" & Sqldr("ld_email2") & "</td></tr>" 
		                    end if
		                     if chkldnotem.checked then 
		                    		ldinfo =  ldinfo +  "<td>Notes:</td></tr><tr><td>" &  Sqldr("ld_notes") & "</td></tr>" 
		                    end if
		              end if  
                     ldinfo =  ldinfo +  "</table>"
                       
                    Return ldinfo
                Else
                    Return ""
                End If

            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try

        End Function
        Public Sub getuseremail(ByVal sender As Object, ByVal e As EventArgs)
          
            Dim strSql As String = "select br_emailfrom,email,ad_intakeresponse,* " _
                    & "from tbl_leads " _
                    & "join dbo.tbl_users on UID=ld_assignedbyuid " _
                    & "left join dbo.tbl_LeadADVenues on av_key=ld_Adcode " _
                    & "left join dbo.tbl_LeadADs on av_leadads_FK=tbl_leadad_pk " _
                    & "left join dbo.tbl_adbranding on tbl_branding_pk=ad_intakeresponse " _
                    & "where tbl_leads_pk ='" & Request.QueryString("id") & "'"

            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then

                    If Sqldr("br_emailaddress") IsNot DBNull.Value Then
                        If Sqldr("br_emailaddress") = "" Then
                            emailfrom.Text = Sqldr("email")
                        Else
                            emailfrom.Text = Sqldr("br_emailaddress")
                        End If
                    ElseIf Sqldr("email") IsNot DBNull.Value Then
                        emailfrom.Text = Sqldr("email")
                    Else
                        emailfrom.Text = "None Found"
                    End If

                End If

            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try

        End Sub
        Public Sub getleademail(ByVal sender As Object, ByVal e As EventArgs)
            Dim strSql As String = "SELECT ld_email from tbl_leads where tbl_leads_pk='" & Request.QueryString("id") & "'"
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    If Sqldr("ld_email") IsNot DBNull.Value Then
                        emailto.Text = Sqldr("ld_email")
                    Else
                        emailto.Text = "None Found"
                    End If
                End If

            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try

        End Sub
        Sub inserthistoryrecord()

            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String
            sqlproc = "sp_insertleadcontact"

            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
         
            Dim prmleadno As New SqlParameter("@leadfk", SqlDbType.Int)
            prmleadno.Value = Request.QueryString("id")
            myCommandADD.Parameters.Add(prmleadno)

            Dim prmcapdate As New SqlParameter("@capdate", SqlDbType.DateTime)
            prmcapdate.Value = rightNow
            myCommandADD.Parameters.Add(prmcapdate)

            Dim prmnotes As New SqlParameter("@l_notes", SqlDbType.Text)
            Dim nt As String
            nt = "<table><tr><td>To:</td><td> " & emailto.Text & "</td></tr>"
            If emailcc.Text <> "" Then
            nt = nt & "<tr><td>CC:</td><td> " & emailcc.Text & "</td></tr>"
             End If
            If emailsubject.Text <> "" Then
                nt = nt & "<tr><td>Subject:</td><td> " & emailsubject.Text & "</td></tr>"
            End If
           	If session("mbody") <> "" Then
            nt = nt & "<tr><td>Body:</td></tr><tr><td> " & session("mbody") & "</td></tr></table>"
             End If
            prmnotes.Value = nt

            myCommandADD.Parameters.Add(prmnotes)

            Dim prmuid As New SqlParameter("@uid", SqlDbType.VarChar, 50)
            prmuid.Value = Session("userid")
            myCommandADD.Parameters.Add(prmuid)

            Dim prmtype As New SqlParameter("@LHType", SqlDbType.VarChar, 50)
            prmtype.Value = "Email"
            myCommandADD.Parameters.Add(prmtype)

            Dim prmfollowup As New SqlParameter("@followup", SqlDbType.VarChar, 50)
            prmfollowup.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmfollowup)

            Dim prmaction As New SqlParameter("@followupactions", SqlDbType.Text)
            prmaction.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmaction)

            Dim prmstatus As New SqlParameter("@LHstat", SqlDbType.VarChar, 50)
            prmstatus.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmstatus)

            Dim prmwho As New SqlParameter("@LHwho", SqlDbType.VarChar, 50)
            if chkattachlead.checked then
            	prmwho.Value = "Other"
            else
            	prmwho.Value = "Lead"
            end if
            
            myCommandADD.Parameters.Add(prmwho)

            Dim prmclosedt As New SqlParameter("@closedate", SqlDbType.DateTime)
            prmclosedt.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmclosedt)

            Dim prmfduedt As New SqlParameter("@fduedate", SqlDbType.DateTime)
            prmfduedt.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmfduedt)

            Try
                myConnectionADD.Open()
                myCommandADD.ExecuteNonQuery()
                myConnectionADD.Close()
            Catch SQLexc As SqlException
                Response.Write("Insert Failed. Error Details are: " & SQLexc.ToString())
            End Try
        End Sub
        Sub prefillemail(ByVal Source As System.Object, ByVal e As System.EventArgs)
            If ddemailcor.SelectedItem.Text = "Clear" Then
                emailsubject.Text = ""
                emailbody.Text = ""
                pnltempatespre.visible=false
            	
           	else
                pnltempatespre.Visible = False
            	bindtemppreview()
                gettemppreviewtext()
            End If

        End Sub
        Public Sub gettemppreviewtext()
            lbltemplatepreview.Text = ""
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
                        lbltemplatepreview.Text = "<b><u>Subject:</u></b> <br> " & Sqldr("email_subject") & " <br> "
                    Else
                        lbltemplatepreview.Text = "<br>"
                    End If
                    If Sqldr("email_text") IsNot DBNull.Value Then
                        lbltemplatepreview.Text = lbltemplatepreview.Text & "<b><u>Body:</u></b> <br> " & Sqldr("email_text")
                    End If

                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try

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
                        emailsubject.Text = Sqldr("email_subject")
                    End If
                    If Sqldr("email_text") IsNot DBNull.Value Then
                        If Sqldr("email_usesig") = "Y" Then
                            emailbody.Text = Sqldr("email_text") + vbCrLf + vbCrLf + "<font size=4><b>" + Session("Agentname") + "<br>" + vbCrLf + Session("company") + "</b></font>"
                        Else
                            emailbody.Text = Sqldr("email_text")
                        End If

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
            Dim myCommand As String = "Select *,  email_name + '[' + cast(email_tbl_pk as varchar(20)) + ']' as 'dfull' " _
         & "from  tbl_emails join  dbo.tbl_xwalk on tbl_xwalk_pk = email_stat " _
         & "where x_descr <> 'Do Not Use' and ((email_uid='" & Session("userid") & "') " _
         & "or (x_descr='Company Wide' and email_co='" & Session("company_pk") & "') " _
         & "or (x_descr='System Wide')) order by email_name"
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                ddemailcor.DataSource = dataReader
                ddemailcor.DataTextField = "dfull"
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
        
         Public Sub click_showtemplates(ByVal sender As Object, ByVal e As EventArgs)
           if btnshowtemps.text="Templates" then
           		pnltempates.visible=true
           		emailhead.visible=false
           		btnshowtemps.text="Hide"           		
           		bindemails()
           else
           		btnshowtemps.text="Templates"
           		pnltempates.visible=false
           		emailhead.visible=true
           end if
        End Sub

			Sub clearall (Source As System.Object, e As System.EventArgs)
					l_search.text=""
					bindemails()
            
       end sub 
        Sub btnsearch(ByVal Source As System.Object, ByVal e As System.EventArgs)
            emails.CurrentPageIndex = 0
            bindemails()
        End Sub
       
       Sub bindemails()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            mycommand = "Select *, convert(varchar(20),email_date,101) as 'emdate' " _
								& "from tbl_emails " _
								& "join tbl_xwalk on tbl_xwalk_pk = email_stat " _
								& "where x_descr <> 'Do Not Use' " _
								& "and ((email_uid='" & Session("userid") & "' and x_descr='Private') " _
								& "or (x_descr='Company Wide' and email_co='" & Session("company_pk") & "') " _
								& "or (x_descr='System Wide'))"

            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_emails")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_emails"))
                If l_search.text="" Then
                
                else
                	dvProducts.RowFilter = "(email_name like '%" & l_search.text & "%' or email_descrip like '%" & l_search.text & "%' or email_subject like '%" & l_search.text & "%' or email_text LIKE '%" & l_search.text & "%')  "
              	 end if
                emails.DataSource = dvProducts
                emails.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
        End Sub
        
        Sub emails_PageChanger(ByVal Source As Object, ByVal E As DataGridPageChangedEventArgs)
            emails.CurrentPageIndex = E.NewPageIndex
            bindemails()
        End Sub
        
       
         Sub ItemDataBoundEventHandler(ByVal sender As Object, ByVal e As DataGridItemEventArgs)

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            	Dim itemcelladtext As TableCell = e.Item.Cells(3)
               Dim itemcelladtexttext As string  = itemcelladtext.Text
              	'itemcelladtexttext.text = itemcelladtext.Text
                'itemcelladtexttext.text = "jj"
		    		 Dim testbtn As System.Web.UI.HtmlControls.HtmlInputButton
                testbtn = e.Item.Cells(0).FindControl("btnpaste")
                testbtn.Attributes("onclick") = "copy('" & itemcelladtexttext.Replace(vbCrLf, "") & "');"
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
            emailbody.text = tbody            
            emailsubject.text = tsub
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
            
            emailbody.text = emailbody.text + "<br>" + tbody
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
            
            emailsubject.text = emailsubject.text + "<br>" + tsub
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
         Sub statcheck(ByVal Source As System.Object, ByVal e As System.EventArgs)

            If dd_status.SelectedItem.Text = "Discrete" Then
            		chkldno.enabled=true
						chkldname.enabled=true
						chkldtype.enabled=true
						chklddate.enabled=true
						chkldph.enabled=true 
						chkldem.enabled=true
						chkldnotem.enabled=true	
            
            else        		
						chkldno.checked=false
						chkldname.checked=false
						chkldtype.checked=false
						chklddate.checked=false
						chkldph.checked=false 
						chkldem.checked=false
						chkldnotem.checked=false
						chkldno.enabled=false
						chkldname.enabled=false
						chkldtype.enabled=false
						chklddate.enabled=false
						chkldph.enabled=false 
						chkldem.enabled=false
						chkldnotem.enabled=false 
            end if
            
        End Sub
        
        
    End Class
End Namespace