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
    Public Class inp1
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
             If Request.QueryString("page") = "1" Then
	                MainForm.visible = True
	                Thankyou.visible = False 
	                getpage1() 
	            Else
	                MainForm.visible = false
		             Thankyou.visible = true  
		             getpage1()
	            End If
	            If session("brandshowlogo") Then
                    If session("showdlogoA") Then
                        Logoarea.Attributes("src") = System.Configuration.ConfigurationManager.AppSettings("CurrentwebURL") &  "/logos/company/" & session("selectedlogo")
                     Else
                        Logoarea.Attributes("src") = session("logodir") & getlogo(session("selectedlogo"))
                     End If
                Else
                    Logoarea.Attributes("src") = ""
                    logoarea.visible=false
                End If    
                If session("brandshowlogo2") Then
                    If session("showdlogoA2") Then
                         Logoarea2.Attributes("src") = System.Configuration.ConfigurationManager.AppSettings("CurrentwebURL") &  "/logos/company/" & session("selectedlogo2")
                    Else
                         Logoarea2.Attributes("src") = session("logodir") & getlogo(session("selectedlogo2"))
                    End If
                Else
                    Logoarea2.Attributes("src") = ""
                    logoarea2.visible=false                    
                End If    

            End If
             'pagesetup()
        End Sub
        sub clearsessions()
        		session("brandshowlogo")=false
        		session("brandshowlogo2")=false
        		session("showdlogoA")=false
        		session("showdlogoA2")=false
        		session("newleadno")=""
        		session("ADagent")=""
        		session("ADagentFK")=""
        		session("adLeadtype")=""
        		session("adsource")=""
        		session("agentfullname")=""
        		session("selectedlogo")=""
        		session("selectedlogo2")=""
        		session("adleadprogram")=""
        		session("rdurl")=""
        		session("ven")=""
        		session("logodir")=""
        		session("emailfrom")=""
        		session("emailreply")=""
        		session("emailsubject")=""
        		session("sendemailself")=""
        		session("sendemailclient")=""
        		session("sendmailaddress")=""
       
        
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
        
        Sub getpage1() 

            Dim strConnection As String

            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select * " _
							& "from tbl_adbranding " _
							& "join tbl_users on UID=  br_uid_fk " _
							& "left join dbo.tbl_company on co_tbl_pk = company_pk " _
							& "left join dbo.tbl_userimages on ui_tbl_pk=br_img_fk  " _
                     & "where tbl_branding_pk ='" & Request.QueryString("id") & "'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then  
                 If sqldr("br_bstat")="Active"  Then
                   
                        If sqldr("br_text1") IsNot dbnull.value Then
                            brandtext1.text = sqldr("br_text1")
                            'brandtext1.text = brandtext1.text.Replace(vbCrLf, "<br>")
                        End If
                        If sqldr("br_text2") IsNot dbnull.value Then
                            brandtext2.text = sqldr("br_text2")
                            'brandtext2.text = brandtext2.text.Replace(vbCrLf, "<br>")
                        End If
                        If sqldr("br_redirecturl") IsNot dbnull.value Then
                            If sqldr("br_redirecturl") = "" Then
                                session("rdurl") = ""
                            Else
                                session("rdurl") = "http://" & sqldr("br_redirecturl")
                            End If
                        Else
                            session("rdurl") = ""
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
                                    session("selectedlogo") = "Magic-128x128.png"
                                End If
                            Else
                                session("brandshowlogo") = False
                                session("selectedlogo") = "Magic-128x128.png"
                            End If
                         Else
                             session("brandshowlogo") = False
                             session("selectedlogo") = "Magic-128x128.png"
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
                                    session("selectedlogo2") = "Magic-128x128.png"
                                End If
                                                       
                            Else
                                session("brandshowlogo2") = False
                                session("selectedlogo2") = "Magic-128x128.png"

                            End If

                        If sqldr("br_sendemail") IsNot dbnull.value Then
                            If Sqldr("br_sendemail") = "Y" Then
                                session("sendemailclient") = "Y"
                                session("emailfrom") = Sqldr("br_emailfrom")
                                session("emailreply") = Sqldr("br_emailreply")
                                session("emailsubject") = Sqldr("br_emailsubject")
                                emailbody.Text = Sqldr("br_emailbody")
                            Else
                                session("sendemailclient") = "N"
                            End If


                        End If
                        If sqldr("br_getemail") IsNot dbnull.value Then
                            If sqldr("br_getemail") = "Y" Then
                                session("sendemailself") = "Yes"
                                session("sendmailaddress") = sqldr("br_emailaddress")
                            End If
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

                        If sqldr("ui_location") IsNot dbnull.value Then
                            session("logodir") = sqldr("ui_location")
                        Else
                            session("logodir") = System.Configuration.ConfigurationManager.AppSettings("CurrentwebURL") & "/logos/company/"

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
						Else
								session("brandshowlogo") =false
									session("brandshowlogo2") =false
                        brandtext1.text = "Please complete the following information and click Submit"
                        brandtext2.text = "Thank You!  Your request will be processed shortly."
                        session("selectedlogo") = "Magic-128x128.png"
                        coname.text = "www.WebMagicPortal.com"
                        coname2.text = "www.WebMagicPortal.com"
                        session("rdurl") = ""
                        session("sendemailclient") = "N"
                    end if

                    Else
                    	session("brandshowlogo") =false
							session("brandshowlogo2") =false
                        brandtext1.text = "Please complete the following information and click Submit"
                        brandtext2.text = "Thank You!  Your request will be processed shortly."
                        session("selectedlogo") = "Magic-128x128.png"
                        coname.text = "www.WebMagicPortal.com"
                        coname2.text = "www.WebMagicPortal.com"
                        session("rdurl") = ""
                        session("sendemailclient") = "N"
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
                    Return sqldr("x_url")
                End If
            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try

        End Function
	
        Sub btnSendrequest_Click(ByVal sender As Object, ByVal e As EventArgs)
         
             MainForm.Visible = False
             Thankyou.Visible = True
           
        End Sub
        Sub sendemail()

            Dim mail As New MailMessage()
            'Response.Write(session("emailfrom"))
            'Response.Write(session("emailreply"))
            'Response.Write(txtemail.Text)
            'Response.Write(session("emailsubject"))
            'Response.Write(emailbody.Text)
            'Set the properties - send the email to the person who filled out the
            mail.From = New MailAddress(session("emailfrom"))
            mail.ReplyTo = New MailAddress(session("emailreply"))
            mail.To.Add(txtemail.Text)
            mail.Subject = session("emailsubject")
            mail.Body = emailbody.Text

          
            'send the message
            Dim smtp As New SmtpClient("smtp.comcast.net")
            smtp.Send(mail)
           
        End Sub
        Sub sendemailselfSUB()
            Dim RightNowAdd As datetime = datetime.now

            Dim mail As New MailMessage()

            'Set the properties - send the email to the person who filled out the
            mail.From = New MailAddress("System@webmagicportal.com")
            ' mail.ReplyTo = New MailAddress(session("emailreply"))
            mail.To.Add(session("sendmailaddress"))
            mail.Subject = "New Lead Notification- " & RightNowAdd
            mail.Body = "You have received a New Lead from " & session("ven") & vbcrlf _
                        & "Below are some details.  Please login to view all information about this lead" & vbcrlf _
                        & "Lead Name: " & txtFName.text & " " & txtLName.text & vbcrlf _
                        & System.Configuration.ConfigurationManager.AppSettings("CurrentappURL") & "/addlead.aspx?action=view&id=" & session("newleadno")


            'send the message
            Dim smtp As New SmtpClient("smtp.comcast.net")
            smtp.Send(mail)

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
            prmleadno.Value = session("newleadno")
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
            prmleadtype.Value = session("adLeadtype")
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

            Dim prmprogram As New SqlParameter("@ld_program", SqlDbType.VarChar, 20)
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
        


    End Class
End Namespace