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
    Public Class intake
        Inherits page

        Public logoarea, logoarea2
        Public coname, coname2, brandtext1, brandtext2, hdtxt1, hdtxt2, hdtxt2A, hdtxt1A As label
        Public txtFName, txtLName, txtHphone, txtemail, txtnotes, emailbody As textbox
        Public Thankyou, MainForm, pnlemailphonereq As panel
        
        public pg1hr,pg2hr
        public redirect as button


        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

            If Not (Page.IsPostBack) Then
            
            	clearsessions()
            	
                GetCompanyinfo()
                if session("deadad")="true" then
                	response.redirect("deadad.aspx")
                end if
                'Logoarea.Attributes("src") = "../logos/company/globe5.gif"
                'Logoarea2.Attributes("src") = "../logos/company/globe5.gif"
                If session("brandshowlogo") Then
                    If session("showdlogoA") Then
                        Logoarea.Attributes("src") = "../logos/company/" &  session("selectedlogo")
                    Else
                        Logoarea.Attributes("src") = session("logodir") & getlogo( session("selectedlogo"))
                  End If
                Else
                    Logoarea.Attributes("src") = "../logos/company/Magic-128x128.png"
                   logoarea.visible=false
                End If
                
                If session("brandshowlogo2") Then
                	
                    If session("showdlogoA2") Then
                  
                        Logoarea2.Attributes("src") = "../logos/company/" &  session("selectedlogo2")
                    Else
                    
                    		'response.write(session("logodir2") & getlogo( session("selectedlogo2")))
                        Logoarea2.Attributes("src") = session("logodir2") & getlogo( session("selectedlogo2"))
						  End If
                Else
                    Logoarea2.Attributes("src") = "../logos/company/Magic-128x128.png"
                    Logoarea2.visible=false
                End If         
                

                MainForm.visible = True
                Thankyou.visible = False
                if not Request.UrlReferrer is nothing then 
                
                	ViewState("ReferrerUrl")  = Request.UrlReferrer.ToString()
                	session("orgurl") = ViewState("ReferrerUrl")
						'If session("orgurl") IsNot Nothing Then
						'	response.write(ViewState("ReferrerUrl"))
					Else
						session("orgurl")="Null"
                end if
                
                 
                'response.write(Request.ServerVariables("HTTP_REFERER"))
                'response.write("hey")

            End If
        End Sub
        
        public sub clearsessions()
	         session("newleadno")=""
	        session("ADagent")=""
	        session("ADagentFK")=""
	        session("adLeadtype")=""
	        session("mrktprg")=""
	        session("adsource")=""
	        session("agentfullname")=""
	        session("em1stat")=""
	        session("em1stat2")=""
	        session("selectedlogo")=""
	        session("selectedlogo2")=""
	        session("adleadprogram")=""
	        session("rdurl")=""
	        session("ven")=""
	        session("logodir")=""
	        session("adtitle")=""
	        session("emailfrom")=""
	        session("emailreply")=""
	        session("emailsubject")=""
	        session("sendemailself")=""
	        session("sendemailself2")=""
	        session("sendemailclient")=""
	        session("sendmailaddress")=""
	        session("sendmailaddress2")=""  
	        'session("brandshowlogo")=""
	        'session("brandshowlogo2")=""
	        'session("showdlogoA")=""
	        'session("showdlogoA2")=""
	        'session("em1sendlink2")=""
	        'session("em1sendlink")=""
	        session("mq")=""
	        session("orgurl")=""     
        
        end sub
        
        
        Public Function getlogo(ByVal id As String) As String
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
        
        Sub GetCompanyinfo()

            Dim strConnection As String

            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select co_name,co_logo,ad_userid,users_tbl_PK,ad_Leadtype,av_name, " _
            						& "x.ui_location as 'img1location', y.ui_location as 'img2location', " _
                    				& "fname + ' ' + lname as 'fullname',ad_leadprogram,* from dbo.tbl_users " _
 	                 				& "left join dbo.tbl_company  on co_tbl_pk = company_pk " _
                  				& "join dbo.tbl_LeadADs on ad_userid = uid join dbo.tbl_LeadADVenues on av_leadads_FK = tbl_leadad_pk " _
                    				& "left join tbl_adbranding on tbl_branding_pk=ad_intakeresponse " _
                    				& "join dbo.tbl_xwalk on x_descr = av_name and x_type='Leadsource' " _
                    				& "left join dbo.tbl_userimages x on x.ui_tbl_pk=br_img_fk " _
                     			& "left join dbo.tbl_userimages y on y.ui_tbl_pk=br_img_fk2 " _
                    				& "where av_key ='" & Request.QueryString("adcode") & "'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                		if sqldr("ad_status") = "Inactive" 
                			if sqldr("ad_Stillallowleads") isnot dbnull.value then
                				if sqldr("ad_Stillallowleads")="N" then
                					session("deadad") = "true"
                				else
                					session("deadad") = "false"
                				end if
                			else
                					session("deadad") = "false"
                			end if
                		else
                					session("deadad") = "false"
                		
                		
                		end if
                    session("ADagent") = sqldr("ad_userid")
                    session("ADagentFK") = sqldr("users_tbl_PK")
                    session("adLeadtype") = sqldr("ad_Leadtype")
                    session("adleadprogram") = sqldr("ad_leadprogram")
                    session("admarketprogram") = sqldr("ad_marketprogram")
                    if sqldr("ad_marketprogram") isnot dbnull.value then
                    		session("mrktprg") = sqldr("ad_marketprogram")
                    else
                    		session("mrktprg") = "None"
                    end if
                    session("adsource") = sqldr("av_name")
                    session("agentfullname") = sqldr("fullname")
                    session("adleadprogram") = sqldr("ad_leadprogram")
                    session("ven") = sqldr("av_name")
                    session("adtitle")=sqldr("ad_title")
                    session("adno") = sqldr("tbl_leadad_pk")
                    
                    
                    If sqldr("brandingPurchased") = "Yes" Then
                        If sqldr("br_text1") IsNot dbnull.value Then
                            brandtext1.text = sqldr("br_text1")
                            brandtext1.text = brandtext1.text.Replace(vbCrLf, "<br>")
                        End If
                        If sqldr("br_text2") IsNot dbnull.value Then
                            brandtext2.text = sqldr("br_text2")
                            brandtext2.text = brandtext2.text.Replace(vbCrLf, "<br>")
                        End If
                        If sqldr("br_redirecturl") IsNot dbnull.value Then
                            If sqldr("br_redirecturl") = "" Then
                                
                               If sqldr("x_url") IsNot dbnull.value Then
                            		If sqldr("x_url") = "" Then
                            			session("rdurl") = session("orgurl")
                            		else
                            			session("rdurl") = "http://" & sqldr("x_url")
		                            		end if
		                            else
		                            		session("rdurl") = session("orgurl")
		                            end if
                            Else
                                session("rdurl") = "http://" & sqldr("br_redirecturl")
                            End If

                        Else
                            If sqldr("x_url") IsNot dbnull.value Then
                            		If sqldr("x_url") = "" Then
                            			session("rdurl") = session("orgurl")
                            		else
                            			session("rdurl") = "http://" & sqldr("x_url")
                            		end if
                            else
                            		session("rdurl") = session("orgurl")
                            end if
                            
                            
                        End If

                        If Sqldr("br_showlogo") IsNot dbnull.value Then
                            If Sqldr("br_showlogo") = "Y" Then
                                session("brandshowlogo") = True
                                If Sqldr("br_img_fk") IsNot dbnull.value Then
                                    If Sqldr("br_img_fk") = "999999" Then
                                        session("showdlogoA") = True
                                         session("selectedlogo") = Sqldr("co_logo")
                                    Else
                                        session("showdlogoA") = False
                                         session("selectedlogo") = Sqldr("br_img_fk")
                                    End If
                                Else
                                    session("brandshowlogo") = False
                                     session("selectedlogo") = ""
                                End If
                            Else
                                session("brandshowlogo") = False
                                session("selectedlogo") = ""
                            End If
                      Else
                          session("brandshowlogo") = False
                           session("selectedlogo") = ""
                      End If
                       If Sqldr("br_showlogo2") = "Y" Then
                                session("brandshowlogo2") = True
                                If Sqldr("br_img_fk2") IsNot dbnull.value Then
                                    If Sqldr("br_img_fk2") = "999999" Then
                                        session("showdlogoA2") = True
                                         session("selectedlogo2") = Sqldr("co_logo")
                                    Else
                                        session("showdlogoA2") = False
                                         session("selectedlogo2") = Sqldr("br_img_fk2")
                                    End If
                                Else
                                    session("brandshowlogo2") = False
                                     session("selectedlogo2") = ""
                                End If
                                                       
                      Else
                          session("brandshowlogo2") = False
                           session("selectedlogo2") = ""

                      End If
                        If sqldr("br_sendemail") IsNot dbnull.value Then
                            If Sqldr("br_sendemail") = "Y" Then
                                 session("sendemailclient") = "Y"
                                session("emailfrom") = Sqldr("br_emailfrom")
                                 if Sqldr("br_emailreply") isnot dbnull.value
                                 	if Sqldr("br_emailreply") ="" then
                                 		session("emailreply")= session("emailfrom")
                                 	else
                                 		session("emailreply") = Sqldr("br_emailreply")
                                 	end if
                                else
                                	session("emailreply")= session("emailfrom")
                                end if
                                session("emailsubject") = Sqldr("br_emailsubject")
                                emailbody.Text = Sqldr("br_emailbody")
                            Else
                                 session("sendemailclient") = "N"
                            End If
                        else
                         session("sendemailclient") = "N"
                        
                        End If
                        If sqldr("br_getemail") IsNot dbnull.value Then
                            If sqldr("br_getemail") = "Y" Then
                                session("sendemailself") = "Yes"
                                If sqldr("br_emailaddress") IsNot dbnull.value
                                		session("sendmailaddress") = sqldr("br_emailaddress")
                                	else
                                		session("sendmailaddress") = ""
                                end if
                            End If
                        else
                        		session("sendemailself") = "No"
                        End If
                        If sqldr("br_getemail2") IsNot dbnull.value Then
                            If sqldr("br_getemail2") = "Y" Then
                                session("sendemailself2") = "Yes"
                                	If sqldr("br_emailaddress2") IsNot dbnull.value	
                                		session("sendmailaddress2") = sqldr("br_emailaddress2")
                                	else
                                		session("sendmailaddress2") = ""
                                	end if
                            End If
                        else
                        		session("sendemailself2") = "No"
                        End If
                        
                        
                        If sqldr("br_showconame") IsNot dbnull.value Then
                         		If sqldr("br_showconame") = "Y" then
	                         		If sqldr("br_company_fk") IsNot dbnull.value Then
			                            coname.text = sqldr("br_company_fk")
			                        Else
			                            coname.text = sqldr("co_name")
			                         End If
                         		else
                         			coname.visible=false
                         		end if
		                   else
		                   	coname.visible=false
                         end if
		                     If sqldr("br_showconame2") IsNot dbnull.value Then
                         		If sqldr("br_showconame2") = "Y" then
	                         		If sqldr("br_company_fk") IsNot dbnull.value Then
			                            coname2.text = sqldr("br_company_fk")
			                        Else
			                             coname2.text = sqldr("co_name")
			                        End If
                         		else
                         				coname2.visible=false
                         		end if
		                   else
		                   	coname2.visible=false
		                   end if

                        If sqldr("br_headtxt1Show") IsNot dbnull.value Then
                             If sqldr("br_headtxt1Show") = "Y" Then
                                hdtxt1.text = sqldr("br_headtext1")
                           
                            Else
                                hdtxt1.visible = False
                            End If
                        Else
                            hdtxt1.visible = False
                         End If
                          If sqldr("br_headtxt1Show2") IsNot dbnull.value Then
                            If sqldr("br_headtxt1Show2") = "Y" Then
                                hdtxt1A.text = sqldr("br_headtext1")
									  Else
                                 hdtxt1A.visible = False
                            End If
                        Else
                             hdtxt1A.visible = False
                        End If

                        If sqldr("br_headtxt2Show") IsNot dbnull.value Then
                            If sqldr("br_headtxt2Show") = "Y" Then
                                hdtxt2.text = sqldr("br_headtext2")
                            
                            Else
                                hdtxt2.visible = False
                             End If
                        Else
                            hdtxt2.visible = False
                         
                        End If
                         If sqldr("br_headtxt2Show2") IsNot dbnull.value Then
                            If sqldr("br_headtxt2Show2") = "Y" Then
                               hdtxt2A.text = sqldr("br_headtext2")

                            Else
                                  hdtxt2A.visible = False
                            End If
                        Else
                             hdtxt2A.visible = False

                        End If

                        If sqldr("img1location") IsNot dbnull.value Then
                            session("logodir") = sqldr("img1location")
                        Else
                            session("logodir") = "../logos/company/"

                        End If
                         If sqldr("img2location") IsNot dbnull.value Then
                            session("logodir2") = sqldr("img2location")
                        Else
                            session("logodir2") = "../logos/company/"

                        End If
                        
 							
 							If sqldr("br_showHRpg1") IsNot dbnull.value Then
	                         If sqldr("br_showHRpg1") = "Y" Then
	                               	pg1hr.visible=true
							          Else
	                                pg1hr.visible=False
	                             End If
                        else
                        	pg1hr.visible=False
                         end if
                         If sqldr("br_showHRpg12") IsNot dbnull.value Then
	                         If sqldr("br_showHRpg12") = "Y" Then
	                         			pg2hr.visible=true
	                            Else
	                                  pg2hr.visible=False
	                            End If
                        else
                         	pg2hr.visible=False
                        end if
                        
                        
                        If sqldr("br_showContinue") IsNot dbnull.value Then
	                         If sqldr("br_showContinue") = "Y" Then
	                               	redirect.visible=true
												
	                            Else
	                               redirect.visible=false
	                            End If
                        else
                        	redirect.visible=false
                        end if
                        
                        If sqldr("br_Leadlvl1") IsNot dbnull.value Then
	                        session("em1stat") = sqldr("br_Leadlvl1")
	                     End If
	                     
                       	If sqldr("br_Leadlvl2") IsNot dbnull.value Then
	                        session("em1stat2") = sqldr("br_Leadlvl2")
	                     End If
                        
                        If sqldr("br_emlink") IsNot dbnull.value Then
	                         If sqldr("br_emlink") = "Y" Then
	                            session("em1sendlink")= true
	                         Else
	                            session("em1sendlink")= false
	                         End If
                        else
                      
                        end if
                        If sqldr("br_emlink2") IsNot dbnull.value Then
	                         If sqldr("br_emlink2") = "Y" Then
	                            session("em1sendlink2")= true 
	                         Else
	                            session("em1sendlink2")= false
	                         End If
                        else
                      
                        end if
                        If sqldr("br_mq") IsNot dbnull.value Then
	                         session("mq")=sqldr("br_mq")
                        else
                      		session("mq")="None"
                        end if


                    Else
                    		session("brandshowlogo") =false
								session("brandshowlogo2") =false
                      	session("em1sendlink")= false
                      	session("em1sendlink2")= false
                      	session("sendemailself2") = "No"
                      	session("sendemailself") = "No"
                        brandtext1.text = "Please complete the following information and click Submit"
                        brandtext2.text = "Thank You!  Your request will be processed shortly."
                         session("selectedlogo") = "Magic-128x128.png"
                        coname.text = "www.WebMagicPortal.com"
                        coname2.text = "www.WebMagicPortal.com"
                        session("rdurl") = "http://" & getvenurl(session("ven"))
                         session("sendemailclient") = "N"
                        hdtxt1.text = "<A href='http://www.webmagicportal.com'>Supercharge Your Online Marketting</A>"
                    End If


                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
        End Sub
        Public Function getvenurl(ByVal x As String) As String
            Dim strConnection As String

            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select * from tbl_xwalk where x_type='leadsource' and x_descr='" & x & "'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                	if sqldr("x_url") isnot dbnull.value then
                    Return sqldr("x_url")
                  else
                     return session("orgurl")
                  end if
                End If
            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try

        End Function
	
        Sub btnSendrequest_Click(ByVal sender As Object, ByVal e As EventArgs)
            If txtemail.text = "" And txtHphone.text = "" Then
                pnlemailphonereq.visible = True
            Else
            
            	
	                getnewleadno()
	                InsertDB()
	                inserthistoryrecord("Lead was autocreated and added to system","Internal","")
	                if session("adsource")="Test Venue" then
	                
	                else
		                UpdateVenueCounts()
		                UpdateTotalCounts()
	                end if
	                If  session("sendemailclient") = "Y" And txtemail.text.length > 0 Then                    
	                    sendemail()
	                End If
	                If session("sendemailself") = "Yes" 
	             	if (session("sendmailaddress") = "" or session("sendmailaddress") is nothing) Then
	                   	
	                	else
	                    sendemailselfSUB()
	                  end if
	                End If
	                If session("sendemailself2") = "Yes" 
	              	if (session("sendmailaddress2") = "" or session("sendmailaddress2") is nothing) Then
	                  	
	                	else
	                    sendemailselfSUB2()
	                  end if
	                End If
	                
	                if session("mq") = "None" then
	                
	                elseif session("mq") = "999998"
	                
	                else
	                	'response.write(session("mq"))
	                	sendqmails()
	                
	                end if
	                checkforworkflow()
	                MainForm.Visible = False
	                Thankyou.Visible = True
	             end if
        
        End Sub
        
         Public Sub sendqmails()
           
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            mycommand = "Select eqd_leadno,case when (ld_email is null or ld_email ='') then ld_email2 else ld_email end as 'ldemail' " _
                & "from tbl_leads join tbl_leadexportqueuedetail on eqd_leadno=tbl_leads_pk " _
                & "where (eqd_eq_fk ='" & session("mq") & "' and ((ld_email is not null and ld_email <> '') or " _
                & "(ld_email2 is not null and ld_email2 <> ''))) "
            Dim ad As New SqlDataAdapter(mycommand, myConnection)
            Dim ds As New DataSet()
            Dim str As New StringBuilder()


            Try
                ad.Fill(ds)
                'ds.Tables(0).TableName = "bill"
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

            Dim i As Integer
            dim wt as integer
            For i = 0 To ds.Tables(0).Rows.Count - 1
                'Response.Write(ds.Tables(0).Rows(i)(1).ToString())
                sendemailQ(ds.Tables(0).Rows(i)(0).ToString(), ds.Tables(0).Rows(i)(1).ToString())
                for wt=0 to 100000000
                next
            Next
            

        End Sub
        
        Sub sendemailQ(ByVal ldno As String, ByVal emailid As String)
            Dim mail As New MailMessage()
  				Dim RightNowAdd As datetime = datetime.now
            'Set the properties - send the email to the person who filled out the
            mail.From = New MailAddress("System@webmagicportal.com")
            mail.To.Add(emailid)            
            mail.Subject = "New Lead Notification- " & RightNowAdd
            mail.Body = "<table>"
             	 	'mail.From = New MailAddress("Newleads@webmagicportal.com")
           		mail.Body = mail.Body + "<tr><td><b>Web Magic has a New Lead ready for you to contact!</b></td></tr></table><br><table> " 
          		mail.Body = mail.Body + "<tr><td>This lead has responded to: " + "</td><td>" & session("adtitle") & "</td></tr></table><br><table>"
           		mail.Body = mail.Body + "<tr><td colspan=2><u>Below are the details for this Lead.</u></td></tr> "            
            	mail.Body = mail.Body + "<tr><td>Lead No:</td><td> " &  session("newleadno") & "</td></tr> "
            	mail.Body = mail.Body + "<tr><td>Lead Type:</td><td> " &  session("adLeadtype") & "</td></tr> "
            	mail.Body = mail.Body + "<tr><td>Lead Name:</td><td> " & left(txtFName.text,1)+"xxxx" & " " & left(txtLName.text,1) + "xxxx" & "</td></tr> "
            	mail.Body = mail.Body + "<tr><td>Lead Email:</td><td> " & left(txtemail.text,3)+"xxxx" & "</td></tr> "
            	mail.Body = mail.Body + "<tr><td>Lead Phone:</td><td> " & left(txtHphone.text,3) + "xxxx" & "</td></tr></table><br> "
            	mail.Body = mail.Body + "<table><tr><td><a href=" & System.Configuration.ConfigurationManager.AppSettings("CurrentwebURL") & "/buyleads.aspx?id=" &  session("newleadno") & "&email=" & emailid & ">CLICK HERE</a> To buy this lead NOW and get the contact info immediately by email.</td></tr>"
            	mail.Body = mail.Body + "<tr><td><a href=" & System.Configuration.ConfigurationManager.AppSettings("CurrentwebURL") & "/gsignup.aspx>CLICK HERE</a> To buy access to unlimited lead generation for a LOW monthly flat fee.</td></tr></table><br>"
            	mail.Body = mail.Body + "<table><tr><td>Thank you for letting Web Magic Portal work for you!</td></tr></table><br><br><br>"
        			'mail.Body = mail.Body + "</table>"
            mail.IsBodyHtml = True
            'send the message
           'Dim smtp As New SmtpClient("192.168.235.11")
				Dim smtp As New SmtpClient("192.168.235.12")
           smtp.ServicePoint.MaxIdleTime = 0
           smtp.Send(mail)
           
            inserthistoryrecordQ(ldno, emailid)

        End Sub
        
         Sub inserthistoryrecordQ(ByVal id As String, ByVal emto As String)

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
            prmleadno.Value = id
            myCommandADD.Parameters.Add(prmleadno)

            Dim prmcapdate As New SqlParameter("@capdate", SqlDbType.DateTime)
            prmcapdate.Value = rightNow
            myCommandADD.Parameters.Add(prmcapdate)

            Dim prmnotes As New SqlParameter("@l_notes", SqlDbType.Text)
            Dim nt As String
            nt = "To: " & emto & vbCrLf
           
            prmnotes.Value = nt
            myCommandADD.Parameters.Add(prmnotes)

            Dim prmuid As New SqlParameter("@uid", SqlDbType.VarChar, 50)
            prmuid.Value = session("ADagent")
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
            prmwho.Value = "Lead"
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
       
        
        Sub sendemail()

            Dim mail As New MailMessage()
            'Response.Write(emailfrom)
            'Response.Write(emailreply)
            'Response.Write(txtemail.Text)
            'Response.Write(emailsubject)
            'Response.Write(emailbody.Text)
            'Set the properties - send the email to the person who filled out the
            mail.From = New MailAddress(session("emailfrom"))
            mail.ReplyTo = New MailAddress( session("emailreply"))
            mail.To.Add(txtemail.Text)
            mail.Subject = session("emailsubject")
            mail.Body = emailbody.Text
				dim mnote as string = mail.body
				
            'send the message
          	mail.IsBodyHtml = True
         	Dim smtp As New SmtpClient("192.168.235.12")
         	smtp.Send(mail)
           inserthistoryrecord(mnote, "Email","")
        End Sub
        Sub sendemailselfSUB()
            Dim RightNowAdd As datetime = datetime.now

            Dim mail As New MailMessage()

            'Set the properties - send the email to the person who filled out the
            mail.From = New MailAddress("System@webmagicportal.com")
            ' mail.ReplyTo = New MailAddress(emailreply)
            mail.To.Add(session("sendmailaddress"))
            mail.Subject = "New Lead Notification- " & RightNowAdd
            mail.Body = "<table>"
            if session("em1stat")="Anonymous" then
            mail.From = New MailAddress("Newleads@webmagicportal.com")
               mail.Body = mail.Body + "<tr><td><b>Web Magic has a New Lead ready for you to contact!</b></td></tr></table><br><table> " 
          		mail.Body = mail.Body + "<tr><td>This lead has responded to: " + "</td><td>" & session("adtitle") & "</td></tr></table><br><table>"
           		mail.Body = mail.Body + "<tr><td colspan=2><u>Below are the details for this Lead.</u></td></tr> "            
            	mail.Body = mail.Body + "<tr><td>Lead No:</td><td> " &  session("newleadno") & "</td></tr> "
            	mail.Body = mail.Body + "<tr><td>Lead Type:</td><td> " &  session("adLeadtype") & "</td></tr> "
            	mail.Body = mail.Body + "<tr><td>Lead Name:</td><td> " & left(txtFName.text,1)+"xxxx" & " " & left(txtLName.text,1) + "xxxx" & "</td></tr> "
            	mail.Body = mail.Body + "<tr><td>Lead Email:</td><td> " & left(txtemail.text,3)+"xxxx" & "</td></tr> "
            	mail.Body = mail.Body + "<tr><td>Lead Phone:</td><td> " & left(txtHphone.text,3) + "xxxx" & "</td></tr></table><br>  "
            	mail.Body = mail.Body + "<table><tr><td><a href='" & System.Configuration.ConfigurationManager.AppSettings("CurrentwebURL") & "/buyleads.aspx?id=" &  session("newleadno") & "&email=" & session("sendmailaddress") & "'>CLICK HERE</a> To buy this lead NOW and get the contact info immediately by email.</td></tr>"
            	mail.Body = mail.Body + "<tr><td><a href='" & System.Configuration.ConfigurationManager.AppSettings("CurrentwebURL") & "/gsignup.aspx'>CLICK HERE</a> To buy access to unlimited lead generation for a LOW monthly flat fee.</td></tr></table><br>"
            	mail.Body = mail.Body + "<table><tr><td>Thank you for letting Web Magic Portal work for you!</td></tr></table><br><br><br>"
        
        			'mail.Body = mail.Body + "<table><tr><td><a href=" & System.Configuration.ConfigurationManager.AppSettings("CurrentwebURL") & "/unsubscribe.aspx?email=" & session("sendmailaddress") & "&uid=" & session("ADagent") & ">Get Removed</a></td></tr>"
            
            elseif session("em1stat")="Basic"
               mail.Body = "<tr><td><b>You have received a New Lead!</b></td></tr></table><br><table> " 
         		mail.Body = mail.Body + "<tr><td>This lead has responded to: " + "</td><td>" & session("adtitle") & "</td></tr></table><br><table>"
           		mail.Body = mail.Body + "<tr><td colspan=2>Below are the details for your Lead.</td></tr> "
            	mail.Body = mail.Body + "<tr><td>Lead No:</td><td> " &  session("newleadno") & "</td></tr> "
            	mail.Body = mail.Body + "<tr><td>Lead Name:</td><td> " & txtFName.text & " " & txtLName.text & "</td></tr> "
            	mail.Body = mail.Body + "<tr><td>Lead Email:</td><td> " & txtemail.text & "</td></tr>"
            	
            else
              mail.Body = "<tr><td><b>You have received a New Lead!</b></td></tr></table><br><table> " 
         		mail.Body = mail.Body + "<tr><td>This lead has responded to: " + "</td><td>" & session("adtitle") & "</td></tr></table><br><table>"
           		mail.Body = mail.Body + "<tr><td colspan=2>Below are the details for your Lead.</td></tr> "
            	mail.Body = mail.Body + "<tr><td>Lead No:</td><td> " &  session("newleadno") & "</td></tr> "
            	mail.Body = mail.Body + "<tr><td>Lead Type:</td><td> " &  session("adLeadtype") & "</td></tr> "
            	mail.Body = mail.Body + "<tr><td>Lead Name:</td><td> " & txtFName.text & " " & txtLName.text & "</td></tr> "
            	mail.Body = mail.Body + "<tr><td>Lead Email:</td><td> " & txtemail.text & "</td></tr>"
            	mail.Body = mail.Body + "<tr><td>Lead Phone:</td><td> " & txtHphone.text & "</td></tr>"
          
          	end if
          	if session("em1sendlink") then 
          		mail.Body = mail.Body + "<tr><td colspan=2>You can login to work with this lead immediately.</td></tr>"
          		mail.Body = mail.Body + "<tr><td colspan=2><a href=" & System.Configuration.ConfigurationManager.AppSettings("CurrentappURL") & "/addlead.aspx?action=view&id=" &  session("newleadno") &">Click Here</a>"
          	end if
          	mail.Body = mail.Body + "</table>"
          	dim mnote as string = mail.body
          	 mail.IsBodyHtml = True

            'send the message
            Dim smtp As New SmtpClient("192.168.235.12")
            smtp.Send(mail)
            				
				'inserthistoryrecord(mnote, "email",session("ADagent"))
        End Sub
        Sub sendemailselfSUB2()
            Dim RightNowAdd As datetime = datetime.now

            Dim mail As New MailMessage()

            'Set the properties - send the email to the person who filled out the
            mail.From = New MailAddress("System@webmagicportal.com")
            ' mail.ReplyTo = New MailAddress(emailreply)
            mail.To.Add(session("sendmailaddress2"))
            mail.Subject = "New Lead Notification- " & RightNowAdd
            mail.Body = "<table>"
            if session("em1stat2")="Anonymous" then
            	mail.From = New MailAddress("Newleads@webmagicportal.com")
               mail.Body = mail.Body + "<tr><td><b>Web Magic has a New Lead ready for you to contact!</b></td></tr></table><br><table> " 
          		mail.Body = mail.Body + "<tr><td>This lead has responded to: " + "</td><td>" & session("adtitle") & "</td></tr></table><br><table>"
           		mail.Body = mail.Body + "<tr><td colspan=2><u>Below are the details for this Lead.</u></td></tr> "            
            	mail.Body = mail.Body + "<tr><td>Lead Type:</td><td> " &  session("adLeadtype") & "</td></tr> "
            	mail.Body = mail.Body + "<tr><td>Lead Name:</td><td> " & left(txtFName.text,1)+"xxxx" & " " & left(txtLName.text,1) + "xxxx" & "</td></tr> "
            	mail.Body = mail.Body + "<tr><td>Lead Email:</td><td> " & left(txtemail.text,3)+"xxxx" & "</td></tr> "
            	mail.Body = mail.Body + "<tr><td>Lead Phone:</td><td> " & left(txtHphone.text,3) + "xxxx" & "</td></tr></table><br>  "
            	mail.Body = mail.Body + "<table><tr><td><a href=" & System.Configuration.ConfigurationManager.AppSettings("CurrentwebURL") & "/buyleads.aspx?id=" &  session("newleadno") & "&email=" & session("sendmailaddress") & ">CLICK HERE</a>&nbspTo buy this lead NOW and get the contact info immediately by email</td></tr>"
            	mail.Body = mail.Body + "<tr><td><a href=" & System.Configuration.ConfigurationManager.AppSettings("CurrentwebURL") & "/gsignup.aspx>CLICK HERE</a> To buy access to unlimited lead generation for a LOW monthly flat fee</td></tr></table><br>"
            	mail.Body = mail.Body + "<table><tr><td>Thank you for letting Web Magic Portal work for you!</td></tr></table><br><br><br>"
        
        			'mail.Body = mail.Body + "<table><tr><td><a href=" & System.Configuration.ConfigurationManager.AppSettings("CurrentwebURL") & "/unsubscribe.aspx?email=" & session("sendmailaddress") & "&uid=" & session("ADagent") & ">Get Removed</a></td></tr>"
            
            elseif session("em1stat2")="Basic"
                mail.Body = "<tr><td><b>You have received a New Lead!</b></td></tr></table><br><table> " 
         		mail.Body = mail.Body + "<tr><td>This lead has responded to: " + "</td><td>" & session("adtitle") & "</td></tr></table><br><table>"
           		mail.Body = mail.Body + "<tr><td colspan=2>Below are the details for your Lead.</td></tr> "
            	mail.Body = mail.Body + "<tr><td>Lead No:</td><td> " &  session("newleadno") & "</td></tr> "
            	mail.Body = mail.Body + "<tr><td>Lead Name:</td><td> " & txtFName.text & " " & txtLName.text & "</td></tr> "
            	mail.Body = mail.Body + "<tr><td>Lead Email:</td><td> " & txtemail.text & "</td></tr>"
            	
            else
                   mail.Body = "<tr><td><b>You have received a New Lead!</b></td></tr></table><br><table> " 
         		mail.Body = mail.Body + "<tr><td>This lead has responded to: " + "</td><td>" & session("adtitle") & "</td></tr></table><br><table>"
           		mail.Body = mail.Body + "<tr><td colspan=2>Below are the details for your Lead.</td></tr> "
            	mail.Body = mail.Body + "<tr><td>Lead No:</td><td> " &  session("newleadno") & "</td></tr> "
            	mail.Body = mail.Body + "<tr><td>Lead Type:</td><td> " &  session("adLeadtype") & "</td></tr> "
            	mail.Body = mail.Body + "<tr><td>Lead Name:</td><td> " & txtFName.text & " " & txtLName.text & "</td></tr> "
            	mail.Body = mail.Body + "<tr><td>Lead Email:</td><td> " & txtemail.text & "</td></tr>"
            	mail.Body = mail.Body + "<tr><td>Lead Phone:</td><td> " & txtHphone.text & "</td></tr>"
        
          	end if
          	if session("em1sendlink2") then 
          		mail.Body = mail.Body + "<tr><td colspan=2>You can login to work with this lead immediately.</td></tr>"
          		mail.Body = mail.Body + "<tr><td colspan=2><a href=" & System.Configuration.ConfigurationManager.AppSettings("CurrentappURL") & "/addlead.aspx?action=view&id=" &  session("newleadno") &">Click Here</a>"
          	end if
          	mail.Body = mail.Body + "</table>"
          	dim mnote as string = mail.body
          	 mail.IsBodyHtml = True

            'send the message
            Dim smtp As New SmtpClient("192.168.235.12")
            smtp.Send(mail)
            				
				'inserthistoryrecord(mnote, "Email",session("ADagent"))

        End Sub
       
        Sub btncontinue_Click(ByVal sender As Object, ByVal e As EventArgs)
            response.redirect(session("rdurl"))
        End Sub

        Sub InsertDB()

            Dim RightNowAdd As datetime = datetime.now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}

            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))

            Dim sqlproc As String = "sp_addlead"

            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

            Dim prmleadno As New SqlParameter("@newleadno", SqlDbType.int)
            prmleadno.Value =  session("newleadno")
            myCommandADD.Parameters.Add(prmleadno)

            Dim prmuid As New SqlParameter("@uid", SqlDbType.VarChar, 50)
            prmuid.Value = session("ADagent")
            myCommandADD.Parameters.Add(prmuid)

            Dim prmlfname As New SqlParameter("@l_fname", SqlDbType.VarChar, 50)
            If txtFName.text = "" Then
                prmlfname.Value = DBNull.Value
            Else
                prmlfname.Value = txtFName.text
            End If
            myCommandADD.Parameters.Add(prmlfname)

            Dim prmllname As New SqlParameter("@l_lname", SqlDbType.VarChar, 50)
            If txtLName.text = "" Then
                prmllname.Value = DBNull.Value
            Else
                prmllname.Value = txtLname.text
            End If
            myCommandADD.Parameters.Add(prmllname)

            Dim prmhphone As New SqlParameter("@l_hphone", SqlDbType.VarChar, 50)
            prmhphone.Value = txtHphone.text
            myCommandADD.Parameters.Add(prmhphone)

            Dim prmcphone As New SqlParameter("@l_cphone", SqlDbType.VarChar, 50)
            prmcphone.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmcphone)

            Dim prmaddress As New SqlParameter("@l_address", SqlDbType.VarChar, 30)
            prmaddress.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmaddress)

            Dim prmcity As New SqlParameter("@l_city", SqlDbType.VarChar, 30)
            prmcity.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmcity)

            Dim prmstate As New SqlParameter("@l_state", SqlDbType.VarChar, 2)
            prmstate.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmstate)

            Dim prmzip As New SqlParameter("@l_zip", SqlDbType.VarChar, 50)
            prmzip.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmzip)

            Dim prmagent As New SqlParameter("@l_agent", SqlDbType.VarChar, 30)
            prmagent.Value = session("agentfullname")
            myCommandADD.Parameters.Add(prmagent)

            Dim prmagentFK As New SqlParameter("@l_agent_FK", SqlDbType.VarChar, 30)
            prmagentFK.Value = session("ADagentFK")
            myCommandADD.Parameters.Add(prmagentFK)

            Dim prmstatus As New SqlParameter("@l_status", SqlDbType.VarChar, 30)
            prmstatus.Value = "Accepted"
            myCommandADD.Parameters.Add(prmstatus)

            Dim prmleadtype As New SqlParameter("@l_leadtype", SqlDbType.VarChar, 30)
            prmleadtype.Value =  session("adLeadtype")
            myCommandADD.Parameters.Add(prmleadtype)

            Dim prmnotes As New SqlParameter("@l_notes", SqlDbType.text)
            prmnotes.Value = txtnotes.text
            myCommandADD.Parameters.Add(prmnotes)

            Dim prmemail As New SqlParameter("@l_email", SqlDbType.VarChar, 50)
            prmemail.Value = txtemail.Text
            myCommandADD.Parameters.Add(prmemail)

            Dim prmemail2 As New SqlParameter("@l_email2", SqlDbType.VarChar, 50)
            prmemail2.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmemail2)

            Dim prmadddate As New SqlParameter("@adddate", SqlDbType.datetime)
            prmadddate.Value = RightNowAdd
            myCommandADD.Parameters.Add(prmadddate)

            Dim prmcapdate As New SqlParameter("@capdate", SqlDbType.datetime)
            prmcapdate.Value = RightNowAdd
            myCommandADD.Parameters.Add(prmcapdate)

            Dim prmapptdate As New SqlParameter("@apptdate", SqlDbType.datetime)
            prmapptdate.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmapptdate)

            Dim prmappttime As New SqlParameter("@appttime", SqlDbType.VarChar, 5)
            prmappttime.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmappttime)

            Dim prmapptloc As New SqlParameter("@apptloc", SqlDbType.VarChar, 30)
            prmapptloc.Value = "NA"
            myCommandADD.Parameters.Add(prmapptloc)

            Dim prmrefermortgage As New SqlParameter("@refermortg", SqlDbType.VarChar, 5)
            prmrefermortgage.Value = "N"
            myCommandADD.Parameters.Add(prmrefermortgage)

            Dim prmrefercredit As New SqlParameter("@refercredit", SqlDbType.VarChar, 5)
            prmrefercredit.Value = "N"
            myCommandADD.Parameters.Add(prmrefercredit)

            Dim prmreferother As New SqlParameter("@referother", SqlDbType.VarChar, 5)
            prmreferother.Value = "N"
            myCommandADD.Parameters.Add(prmreferother)

            Dim prmreferotherex As New SqlParameter("@referotherex", SqlDbType.VarChar, 50)
            prmreferotherex.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmreferotherex)

            Dim prmcomp As New SqlParameter("@comp", SqlDbType.varchar, 15)
            prmcomp.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmcomp)

            Dim prmassignedagent As New SqlParameter("@assignedagent", SqlDbType.VarChar, 50)
            prmassignedagent.Value = session("ADagent")
            myCommandADD.Parameters.Add(prmassignedagent)

            Dim prmhighpri As New SqlParameter("@highpri", SqlDbType.VarChar, 5)
            prmhighpri.Value = "No"
            myCommandADD.Parameters.Add(prmhighpri)

            Dim prmldsource As New SqlParameter("@leadsource", SqlDbType.VarChar, 50)
            prmldsource.Value = session("adsource")
            myCommandADD.Parameters.Add(prmldsource)

            Dim prmadcode As New SqlParameter("@adcode", SqlDbType.VarChar, 50)
            prmadcode.Value = Request.QueryString("adcode")
            myCommandADD.Parameters.Add(prmadcode)

            Dim prmmailtoaddress As New SqlParameter("@mailtoaddress", SqlDbType.VarChar, 50)
            prmmailtoaddress.Value = "N"
            myCommandADD.Parameters.Add(prmmailtoaddress)

            Dim prmproptolist As New SqlParameter("@proplist", SqlDbType.VarChar, 50)
            prmproptolist.Value = "N"
            myCommandADD.Parameters.Add(prmproptolist)

            Dim prmpstatus As New SqlParameter("@ld_pstatus", SqlDbType.VarChar, 50)
            prmpstatus.Value = "New"
            myCommandADD.Parameters.Add(prmpstatus)

            Dim prmprogram As New SqlParameter("@ld_program", SqlDbType.VarChar, 50)
            prmprogram.Value = session("adleadprogram")
            myCommandADD.Parameters.Add(prmprogram)

            Dim prmstatdetail As New SqlParameter("@ld_statdetail", SqlDbType.VarChar, 50)
            prmstatdetail.Value = "None"
            myCommandADD.Parameters.Add(prmstatdetail)

            Dim prmentrysource As New SqlParameter("@ld_entrysource", SqlDbType.VarChar, 50)
            prmentrysource.Value = "Auto"
            myCommandADD.Parameters.Add(prmentrysource)

            Dim prmfax As New SqlParameter("@ld_fax", SqlDbType.VarChar, 50)
            prmfax.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmfax)
            
            Dim prmmkprg As New SqlParameter("@marketprog", SqlDbType.VarChar, 50)
            if session("mrktprg").length > 0 then
            	prmmkprg.Value = session("mrktprg")
            else
            	prmmkprg.Value = "None"
            end if
            myCommandADD.Parameters.Add(prmmkprg)
            
            Dim prmmktto As New SqlParameter("@marketto", SqlDbType.VarChar, 50)
            prmmktto.Value = "Yes"
            myCommandADD.Parameters.Add(prmmktto)
            
             Dim prmldext As New SqlParameter("@ld_ext1", SqlDbType.VarChar, 50)
            prmldext.Value = dbnull.value
            myCommandADD.Parameters.Add(prmldext)

            Try
                myConnectionADD.Open()
                myCommandADD.ExecuteNonQuery()
                myConnectionADD.Close()
            Catch SQLexc As SqlException
                Response.Write("Insert Failed. Error Details are: " & SQLexc.ToString())
            End Try

        End Sub

        Sub getnewleadno()
            Dim strUID As String = session("userid")
            Dim strSql As String = "SELECT max(tbl_leads_pk)+1 as 'newpk' from dbo.tbl_leads"
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.read() Then
                     session("newleadno") = Sqldr("newpk")
                End If

            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.close()
            End Try
        End Sub

        Sub UpdateVenueCounts()
            Dim strSql As String = "update tbl_LeadADVenues set av_resultsCnt=av_resultsCnt+1 where av_key = '" & Request.QueryString("adcode") & "'"
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.close()
            End Try

        End Sub

        Sub UpdateTotalCounts()
            Dim strSql As String = "update tbl_LeadADs set ad_totalLeadcount=ad_totalLeadcount+1 from tbl_LeadADs join tbl_LeadADVenues on av_leadads_FK=tbl_leadad_pk where av_key ='" & Request.QueryString("adcode") & "'"
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.close()
            End Try
        End Sub
        
         Sub inserthistoryrecord(lnotes as string, ltype as string,lagent as string)

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
            prmleadno.Value =  session("newleadno")
            myCommandADD.Parameters.Add(prmleadno)

            Dim prmcapdate As New SqlParameter("@capdate", SqlDbType.DateTime)
            prmcapdate.Value = rightNow
            myCommandADD.Parameters.Add(prmcapdate)

            Dim prmnotes As New SqlParameter("@l_notes", SqlDbType.Text)
            'Dim nt As String
            'nt = "Lead was autocreated and added to system"
            prmnotes.Value = lnotes

            myCommandADD.Parameters.Add(prmnotes)

            Dim prmuid As New SqlParameter("@uid", SqlDbType.VarChar, 50)
            prmuid.Value = session("ADagent")
            myCommandADD.Parameters.Add(prmuid)

            Dim prmtype As New SqlParameter("@LHType", SqlDbType.VarChar, 50)
            prmtype.Value = ltype
            myCommandADD.Parameters.Add(prmtype)

            Dim prmfollowup As New SqlParameter("@followup", SqlDbType.VarChar, 50)
            prmfollowup.Value = "No"
            myCommandADD.Parameters.Add(prmfollowup)

            Dim prmaction As New SqlParameter("@followupactions", SqlDbType.Text)
            prmaction.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmaction)

            Dim prmstatus As New SqlParameter("@LHstat", SqlDbType.VarChar, 50)
            prmstatus.Value = "Closed"
            myCommandADD.Parameters.Add(prmstatus)

            Dim prmwho As New SqlParameter("@LHwho", SqlDbType.VarChar, 50)
            prmwho.Value = "System"
            myCommandADD.Parameters.Add(prmwho)

            Dim prmclosedt As New SqlParameter("@closedate", SqlDbType.DateTime)
            prmclosedt.Value = rightNow
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

		
                    
          sub  checkforworkflow()
				'Check for matching workflows and store in data table
				Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            mycommand = "begin " _
								& "select * " _
								& "into #tmpa " _ 
								& "from dbo.tbl_WorkFlowMaster " _
								& "left join dbo.tbl_WorkFlowFilters on wffilters_wfm_fk = wfm_tbl_pk " _
								& "left join dbo.tbl_xwalk on tbl_xwalk_pk = wffilters_value and wffilters_type <> 'AD' " _
								& "where wfm_useridfk='" & session("ADagent") & "' " _
								& "and wfm_trigger='On New Lead' " _
								& "and wfm_effdate <= getdate() and wfm_enddate >= getdate() " _
								& "and wfm_status = 'Active' " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpLTMatch " _
								& "from #tmpa " _
								& "where (wfm_filter1Usage='Include' and (x_descr='" & session("adLeadtype") & "' and x_type='leadtype') or wfm_filter1Usage='Do Not Use') " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpLPMatch " _
								& "from #tmpa " _
								& "where (wfm_filter2Usage='Include' and (x_descr='" & session("adleadprogram") & "' and x_type='leadprogram') or wfm_filter2Usage='Do Not Use') " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpLSMatch " _
								& "from #tmpa " _
								& "where (wfm_filter3Usage='Include' and (x_descr='New' and x_type='leadstatus') or wfm_filter3Usage='Do Not Use') " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpCONMatch " _
								& "from #tmpa " _
								& "where (wfm_filter4Usage='Include' and (x_descr='" & session("adLeadtype") & "' and x_type='contactstatus') or wfm_filter4Usage='Do Not Use') " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpADMatch " _
								& "from #tmpa " _
								& "where (wfm_filter5Usage='Include' and (wffilters_value='" &  session("adno") & "' and wffilters_type='AD') or wfm_filter5Usage='Do Not Use') " _
								& "select distinct wfm_tbl_pk " _
								& "into #tmpMKTMatch " _
								& "from #tmpa " _
								& "where (wfm_filter6Usage='Include' and (x_descr='" &  session("admarketprogram") & "' and x_type='marketprogram') or wfm_filter6Usage='Do Not Use') " _
								& "select  distinct a.wfm_tbl_pk " _
								& "from #tmpa a " _
								& "join #tmpLTMatch b on b.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "join #tmpLPMatch c on c.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "join #tmpLSMatch d on d.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "join #tmpCONMatch e on e.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "join #tmpADMatch f on f.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "join #tmpMKTMatch g on g.wfm_tbl_pk =a.wfm_tbl_pk " _
								& "end"
                        
            Dim ad As New SqlDataAdapter(mycommand, myConnection)
            Dim ds As New DataSet()
            Dim str As New StringBuilder()

            Dim i As Integer

            Try
                ad.Fill(ds)
                'ds.Tables(0).TableName = "bill"
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try


 				For i = 0 To ds.Tables(0).Rows.Count - 1
				
              BuildWRKFLOW(ds.Tables(0).Rows(i)(0).ToString())
              

            Next

			end sub

			sub BuildWRKFLOW(wrkflwid as string)
				session("WFMPK")=wrkflwid
				insertWFStatusRec(wrkflwid)
				getWFStatusRecPK(wrkflwid) 
				getWFMasterData(wrkflwid)
				getWFSteps(wrkflwid)
			
			end sub
			
			 sub getWFStatusRecPK(wrkflwid as string)
		  
		  		Dim strUID As String = Session("userid")
            Dim strSql As String = "select max(lwfs_tbl_pk) as 'maxpk' from tbl_leadWorkFlowsStatus where lwfs_userid_fk='" & session("ADagent") & "' " _
            							& "and lwfs_lead_fk='" &  session("newleadno") & "' and lwfs_wfm_fk='" & session("WFMPK") & "'" 
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                	session("lswfpk") = sqldr("maxpk")
                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
		  
		  end sub
		  
			
			
			sub insertWFStatusRec(wrkflwid as string)
			
			
				Dim strUID As String = Session("userid")
            Dim strSql As String = "insert into tbl_leadWorkFlowsStatus (lwfs_lead_fk,lwfs_userid_fk,lwfs_wfm_fk,lwfs_leadststatus) " _
                                   & "values ('" & session("newleadno") & "','" & session("ADagent") & "', '" & session("WFMPK") & "', 'Active' )"  
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
			


			sub getWFMasterData(id as string)
				 Dim strSql As String = "SELECT * from tbl_WorkFlowMaster where wfm_tbl_pk='" & id & "'"
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    session("WFMStartDate")=Sqldr("wfm_effdate")
                    session("WFMEndDate")=Sqldr("wfm_enddate")
                    
                    
                End If

            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
			
			end sub

			sub  getWFSteps(id as string)

				Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            mycommand = "select wfs_tbl_pk,wfs_Freq,wfs_Sunday,Wfs_Monday,wfs_Tuesday,wfs_Wednesday,wfs_Thursday,wfs_Friday,wfs_Saturday,wfs_DayofMonth, wfs_DependantStep,wfs_stepno,wfs_basedateoffset,* from tbl_WorkFlowSteps where wfs_wfm_fk='" & id & "' and wfs_status='Active' order by wfs_stepno "
                        
            Dim ad As New SqlDataAdapter(mycommand, myConnection)
            Dim ds As New DataSet()
            Dim str As New StringBuilder()

            Dim i As Integer

            Try
                ad.Fill(ds)
                'ds.Tables(0).TableName = "bill"
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

				For i = 0 To ds.Tables(0).Rows.Count - 1
					session("WFSPK")=ds.Tables(0).Rows(i)(0).ToString()
					session("WFSNo")=ds.Tables(0).Rows(i)(11).ToString()
					if ds.Tables(0).Rows(i)(1).ToString() = "Weekly" then
						if ds.Tables(0).Rows(i)(2).ToString() = "Y" then
							
							InsertWFSteps(ds.Tables(0).Rows(i)(1).ToString(),"Sunday","",ds.Tables(0).Rows(i)(10).ToString(),0)
						end if
						if ds.Tables(0).Rows(i)(3).ToString() = "Y" then
						
							InsertWFSteps(ds.Tables(0).Rows(i)(1).ToString(),"Monday","",ds.Tables(0).Rows(i)(10).ToString(),0)
						end if
						if ds.Tables(0).Rows(i)(4).ToString() = "Y" then
						
							InsertWFSteps(ds.Tables(0).Rows(i)(1).ToString(),"Tuesday","",ds.Tables(0).Rows(i)(10).ToString(),0)
						end if
						if ds.Tables(0).Rows(i)(5).ToString() = "Y" then
						
							InsertWFSteps(ds.Tables(0).Rows(i)(1).ToString(),"Wednesday","",ds.Tables(0).Rows(i)(10).ToString(),0)
						end if
						if ds.Tables(0).Rows(i)(6).ToString() = "Y" then
						
							InsertWFSteps(ds.Tables(0).Rows(i)(1).ToString(),"Thursday","",ds.Tables(0).Rows(i)(10).ToString(),0)
						end if
						if ds.Tables(0).Rows(i)(7).ToString() = "Y" then
						
							InsertWFSteps(ds.Tables(0).Rows(i)(1).ToString(),"Friday","",ds.Tables(0).Rows(i)(10).ToString(),0)
						end if
						if ds.Tables(0).Rows(i)(8).ToString() = "Y" then
						
							InsertWFSteps(ds.Tables(0).Rows(i)(1).ToString(),"Saturday","",ds.Tables(0).Rows(i)(10).ToString(),0)
						end if
						
					elseif ds.Tables(0).Rows(i)(1).ToString() = "Monthly" then
						InsertWFSteps(ds.Tables(0).Rows(i)(1).ToString(),"",ds.Tables(0).Rows(i)(9).ToString(),ds.Tables(0).Rows(i)(10).ToString(),0)
				
					elseif ds.Tables(0).Rows(i)(1).ToString() = "Quarterly" then
						InsertWFSteps(ds.Tables(0).Rows(i)(1).ToString(),"",ds.Tables(0).Rows(i)(9).ToString(),ds.Tables(0).Rows(i)(10).ToString(),0)
				
						
					else
                 	InsertWFSteps(ds.Tables(0).Rows(i)(1).ToString(),"","",ds.Tables(0).Rows(i)(10).ToString(),Convert.ToInt32(ds.Tables(0).Rows(i)(12).ToString()))
          
              	end if
		      

            Next

			end sub
			
			sub  InsertWFSteps(freq as string, DOW as string, TOM as string, DStep as string, offset as integer)
				dim sdate, sdateNEW as datetime 
				response.write(DStep)
				if DStep="0" then
					sdate =DateTime.Now
				else
					response.write("H1")
					sdate = GetnewStepDate(dstep)
					response.write(sdate)
				end if
				
				
				dim sdateDOW as string = sdate.DayOfWeek.tostring()
				dim sdateDOWString as string
			
				'response.write(freq)
				'response.write("-->")
				if freq="Once" or Freq="Daily" then
					sdateNEW = sdate.adddays(offset)
				elseif freq="Weekly" then
					
					if DOW = sdateDOW then
						sdateNEW = sdate
					else
						
						while sdateDOW <> DOW
							
							sdate = sdate.AddDays(1)
							sdateDOW = sdate.DayOfWeek.tostring()
						end while
						sdateNEW = sdate
					
					end if							
					
				elseif freq="Monthly" then
					if TOM="First Day of the Month" then
						sdateNEW = sdate.AddDays((sdate.Day - 1) * -1).AddMonths(1)
					else
						sdateNEW = DateAdd(DateInterval.Day, 	(Day(DateAdd(DateInterval.Month, 1, sdate).AddMonths(1))) * -1,  DateAdd(DateInterval.Month, 1, sdate).AddMonths(1))
					end if
					'response.write(Now.AddDays((Now.Day - 1) * -1).AddMonths(1))
					'response.write(DateAdd(DateInterval.Day, 	(Day(DateAdd(DateInterval.Month, 1, sdate).AddMonths(1))) * -1,  DateAdd(DateInterval.Month, 1, sdate).AddMonths(1)))
				elseif freq="Quarterly" then
					Dim currQuarter As Integer = (sdate.Month - 1) / 3 + 1
					response.write(currQuarter)
					if TOM="First Day of the Quarter" then
						
						Dim dtFirstDay As New  DateTime(sdate.Year, 3 * currQuarter-2, 1)
						sdateNEW = dtFirstDay
					else
						Dim dtLastDay As New  DateTime(sdate.Year, 3 * currQuarter, DateTime.DaysInMonth(sdate.Year,3 * currQuarter))
						sdateNEW = dtLastDay
					end if
					'response.write(sdateNEW)
				
				end if
				
				
				'response.write(session("WFMPK"))
				'response.write(" ")
				'response.write(session("WFSPK"))
				'response.write(" ")
				'response.write(session("newleadno"))
				'response.write(" ")
				'response.write(session("ADagent"))
				'response.write(" ")
				'response.write("Pending")
				'response.write(" ")
				'response.write(sdateNEW)
				
				
				Dim strUID As String = Session("userid")
            Dim strSql As String = "insert into tbl_LeadWorkFlows (lwf_lead_fk,lwf_userid_fk,lwf_wfm_fk,lwf_wfs_fk,lwf_stepno,lwf_status,lwf_freq,lwf_DependantStep,lwf_startdate,lwf_lwfs_fk) " _
                                   & "values ('" & session("newleadno") & "','" & session("ADagent") & "', '" & session("WFMPK") & "', " _ 
                                   & "'" & session("WFSPK") & "', '" &  session("WFSNo") & "','Pending','" & freq & "','" & DStep & "','" & sdateNEW & "','" & session("lswfpk") & "')" 
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

			public function GetnewStepDate(dstep as string) as datetime
				Dim strUID As String = Session("userid")
            Dim strSql As String = "select max(lwf_startdate) as 'NewDate' from tbl_LeadWorkFlows where lwf_lead_fk='" & session("newleadno") & "' and lwf_userid_fk='" &  session("ADagent") & "' " _
            								& "and lwf_wfm_fk='" & session("WFMPK")  & "' and lwf_stepno='" & dstep & "'"
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                	return sqldr("NewDate")
                else
                	return Now
                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try

			
			end function
    End Class
End Namespace