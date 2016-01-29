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
    Public Class postad
        Inherits Page
        Public Advenue As HtmlGenericControl
        Public  AdTitle, adtype, adcat, adsubcat, adcode, Label1, Label1a, uid, password,lbltest1,lblpuid,lblppass As Label
        Public Button1, Button3, Button2 As System.Web.UI.HtmlControls.HtmlInputButton
        Public adresponseurl,pstno As TextBox
        Public pnlourside,  tside,pnlstartpost,pnlendpostprocess,pnlUID As Panel
       
      
        Public pnlpostad, Panel1 As Panel
        Public dd_acctyesno As DropDownList
        Public buttoncnt,finishexitA,bcad,bsad As Button
        public loginissue as boolean = false
        public btn_ADTITLE,btn_chgad,btnShowhtml,btn_ADTEXT,btn_ADTEXTa,btn_ADwebKey as linkbutton
        public adtext, adtext2
        public txtplain,txthtml
        public lblvinst,lblvname,lblcview,lbltest2,lblvnotes,lblwebkey,lbladno as label
        public pnlpreview,pnlplaintext,pnlhtml,pnledit,pnlspacer,pnladtextmain as panel
        public txtareaB
       public finishexitAB,bsadCan,finishexit as button

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

            If Not (Page.IsPostBack) Then
            		clearsessions()
						pnlstartpost.visible=true
                'Response.Write(Request.Cookies("cl_login_cookie").Value)
                'readcookies()

                setvenue()
                'response.write(session("acctsurl") & " " & session("normurl"))
                getadinfo()
              
                getidpass()
                
                Label1.Text = session("Pvenue")
                If Request.Browser.Browser = "IE" Then
                    setbutton()
                End If
              
                If loginissue Then
                    
                    'Panel1.Visible = True
                    'pnlpostad.Visible = False
                    'buttoncnt.Visible = False
                   
                    tside.Visible = true
                    pnlpostad.Visible = True
                    buttoncnt.Visible = False

                   

                Else
                    
                    Panel1.Visible = False
                    pnlpostad.Visible = True
                    buttoncnt.Visible = False

                End If
                	If session("Pmodify") = "Yes" Then
							finishexitA.visible=false
						end if
						
						pnlpreview.visible=true
						lblcview.text="Preview"
						pnlspacer.visible=false
						If Request.Browser.Browser = "IE" Then
            			btn_ADTITLE.visible=true 
            			btn_ADTEXT.visible=true
							btn_ADTEXTA.visible=false  
        				else 
        					btn_ADTITLE.visible=false 
            			btn_ADTEXT.visible=false
							btn_ADTEXTA.visible=false  
        
        				End If
						
						pnladtextmain.visible=true
						session("keepadmfiltersA")="true"
						
            End If
            'pagelayout()
        End Sub
        sub clearsessions()
        	session("normurl")=""
        	session("acctsurl")=""
        
         
        
        end sub
        Sub logincl(ByVal sender As Object, ByVal e As EventArgs)
            Panel1.Visible = False
            pnlpostad.Visible = True
        End Sub
          Sub StartPost(ByVal sender As Object, ByVal e As EventArgs)
             pnlstartpost.visible=false
             pnlendpostprocess.visible=true
             If session("Pmodify") = "Yes" Then
							bcad.visible=true
				 end if
             Response.Write("<script>window.open" & _
                    "('" & session("normurl") & "','_new', 'width=900,height=600,resizable=1,scrollbars=1');</script>")
        End Sub
         Sub CancelPost(ByVal sender As Object, ByVal e As EventArgs)
         	if session("Oposter") = "admanager" then
         	 	Response.Redirect("adpostings.aspx?source=admanager")
         	elseif session("Oposter") = "admanagerQ" then
         		Response.Redirect("adpostings.aspx?source=admanagerQ")
         	elseif session("Oposter") = "admgrpostOnce" then
         		Response.Redirect("admanager.aspx")
         	elseif session("Oposter") = "newpostpub"
         		Response.Redirect("adpostings.aspx?source=newpostpub")
         	elseif session("Oposter") = "planedit" then
        			Response.Redirect("adpostings.aspx?source=planedit")
             	'Response.Redirect("createad.aspx?action=edit&adno=" & session("Padno") & "&pplan=" &  session("Ppplan")& "&nav=posting")
            else
            	Response.Redirect("adpostings.aspx?source=admanager")
            end if
        End Sub
        
        
        
        
        Public Sub readcookies()

            Dim url As String = "https://accounts.craigslist.org/"
            ' must be an external reference !

            Dim xmlhttp = Server.CreateObject("MSXML2.ServerXMLHTTP")
            xmlhttp.open("GET", url, False)
            xmlhttp.send("")

            Dim t As String = xmlhttp.ResponseText

            ' make a source displaying page
            ' t = Replace(t, "<", "&lt;")
            't = Replace(t, vbCrLf, "<br>")
            '
            'Response.Write(t)

            xmlhttp = Nothing


        End Sub
        Public Sub changead(ByVal sender As Object, ByVal e As EventArgs)
           

        End Sub
        Public Sub setbutton()
            Dim vadkey, vadtitle, vadtext As String
            'vadkey = adresponseurl.Text
            'vadtitle = AdTitle.Text
            vadtext = adtext.content
            'Dim ss As String = "copy('" & vadkey & "');"
            'Dim ss1 As String = "copy('" & vadtitle & "');"
            'Dim ss2 As String = "copy('" & adtext.content & "');"
            'Button1.Attributes("onclick") = ss
            'Button2.Attributes("onclick") = ss1
            'lbltest1.text=adtext.content
            btn_ADTITLE.Attributes("onclientclick")= "copy2(document.getElementById('AdTitle'));"
            Button1.Attributes("onclick")= "copy2(document.getElementById('AdTitle'));"
            Button2.Attributes("onclick")= "copy2(document.getElementById('adresponseurl'));"
            Button3.Attributes("onclick") = "copy2(document.getElementById('lbltest1'));"
            Button3.Visible = false
            Button2.Visible = false
            Button1.Visible = false

        End Sub
        
        
       
        Public Sub setvenue()

            'Response.Write(session("Pvenue"))
            Dim strUID As String = Session("userid")
            Dim strSql As String = "SELECT * from tbl_xwalk where x_type='leadsource' and x_descr='" & session("Pvenue") & "'"

            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                		if sqldr("x_loginissue") = "Yes" then
                			loginissue= true
                		else
                		 loginissue= false
                		end if
                     'session("acctsurl") =  Sqldr("x_accounturl")
                		session("normurl") = "http://" & Sqldr("x_url")
                		               

                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try

        End Sub
        Sub getidpass()
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select * from tbl_venueinfo where vpass_venue ='" & session("Pvenue") & "' and vpass_uid='" & session("userid") & "'"

            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    uid.Text = Sqldr("vpass_id")
                    password.Text = Sqldr("vpass_pass")
							pnlUID.visible=true
                End If
            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try

        End Sub
        Sub getadinfo()
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select * from dbo.tbl_LeadADs join dbo.tbl_LeadADVenues on av_leadads_FK =tbl_leadad_pk and " _
            							& "tbl_leadadvenues ='" & session("Pvenueno") & "' " _
            							& "join tbl_xwalk on x_descr = av_name " _
            							& "where tbl_leadadvenues='" & session("Pvenueno") & "' " _

            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then                  
                   	lblvname.text = Sqldr("av_name")
                   	lbladno.text=Sqldr("tbl_leadad_pk") 
                   	If Sqldr("x_instructions") IsNot DBNull.Value Then
                  		if Sqldr("x_instructions")="" then 
                  			lblvinst.text ="None"
                  		else
                  			lblvinst.text = Sqldr("x_instructions")
                  		end if
                 	 	else
                  		lblvinst.text ="None"
                  	end if
                  	If Sqldr("x_notes") IsNot DBNull.Value Then
                  		if Sqldr("x_notes")="" then 
                  			lblvnotes.text ="None"
                  		else
                  			lblvnotes.text = Sqldr("x_notes")
                  		end if
                  		
                 	 	else
                  		lblvnotes.text ="None"
                  	end if
                  	
                    	AdTitle.Attributes("value") = Sqldr("ad_title")
                    	AdTitle.Text = Sqldr("ad_title")
                    	adtext.content = Sqldr("ad_text")                    
                    	
                    	
                     Dim Pass, Pass2,PassA, PassB, data ,data2, Game2, Key1, Key2 As String
                   	Dim PassEnd, passend2,PassEndA, passend2A
                   	data = Sqldr("ad_text")
                   	PassEnd = InStr(Sqldr("ad_text"), "%A")
                   	PassEnd2 =  PassEnd+6
                   	pass = Data.Substring(0, PassEnd - 1)
                   	passA = Data.Substring(0, PassEnd - 1) 
                   	pass= pass & "<b><a href=" & System.Configuration.ConfigurationManager.AppSettings("CurrentwebURL") & "/intake.aspx?adcode=" & Sqldr("av_key") & ">Click Here!</a></b>"
                   	passA= passA & "Click Here-> "& System.Configuration.ConfigurationManager.AppSettings("CurrentwebURL") & "/intake.aspx?adcode=" & Sqldr("av_key")
                   	pass2 = Data.Substring(PassEnd2, data.length-PassEnd2) 
                   	passb =Data.Substring(PassEnd2, data.length-PassEnd2) 
                   	lblwebkey.text = System.Configuration.ConfigurationManager.AppSettings("CurrentwebURL") & "/intake.aspx?adcode=" & Sqldr("av_key")
                   	lblwebkey.attributes.add("value", System.Configuration.ConfigurationManager.AppSettings("CurrentwebURL") & "/intake.aspx?adcode=" & Sqldr("av_key"))
                   
                   	lbltest1.text = pass & pass2
                   	lbltest1.attributes.add("value", pass & pass2)
                   	adcode.Attributes("value") = Sqldr("av_key")
                    	adcode.Text = Sqldr("av_key")
                    	'adtext.content = pass & pass2 
                    	adtext2.content =  passA & passb 
                    	dim stext as string = adtext2.content
                    	adtext2.content = stext.replace("<br />", "--crlf--")
                    	dim stext2 as string =adtext2.plaintext
                    	'lbltest2.text = stext2.replace("--crlf--", vbcrlf)
                   	'lbltest2.attributes.add("value",stext2.replace("--crlf--", vbcrlf))
                    	txtareaB.value = stext2.replace("--crlf--", vbcrlf)
                    
                    	txthtml.text=lbltest1.text
                    	'PlainText.value=adtext.plaintext
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
            
            adresponseurl.Text = System.Configuration.ConfigurationManager.AppSettings("CurrentwebURL") & "/intake.aspx?adcode=" & adcode.Text
        End Sub
        Sub Showhtml(ByVal sender As Object, ByVal e As EventArgs)
 				dim x as dropdownlist=sender
 				if x.selecteditem.text="Preview" then
 					pnlpreview.visible=true
 					pnlplaintext.visible=false
 					pnlhtml.visible=false
 					pnledit.visible=false
 					lblcview.text="Preview"
 					If Request.Browser.Browser = "IE" Then
            			btn_ADTEXT.visible=true
 							btn_ADTEXTA.visible=false
 							btn_ADwebKey.visible=true
 							pnlspacer.visible=false 
        			else 
        					btn_ADTEXT.visible=false
 							btn_ADTEXTA.visible=false
 							pnlspacer.visible=false  
 							btn_ADwebKey.visible=false
        			End If
 				
 	
 				elseif  x.selecteditem.text="Plain Text" then
 					pnlpreview.visible=false
 					pnlplaintext.visible=true
 					pnlhtml.visible=false
 					pnledit.visible=false
 					lblcview.text="Plain text"
 					If Request.Browser.Browser = "IE" Then
            			btn_ADTEXT.visible=false
 							btn_ADTEXTA.visible=true
 							pnlspacer.visible=false
        			else 
        					btn_ADTEXT.visible=false
 							btn_ADTEXTA.visible=false
 							pnlspacer.visible=false 
        			End If
 					
 				else
 					pnlpreview.visible=false
 					pnlplaintext.visible=false
 					pnlhtml.visible=true
 					pnledit.visible=false
 					lblcview.text="HTML"
 					btn_ADTEXT.visible=false
 					btn_ADTEXTA.visible=false
 					pnlspacer.visible=true
 				end if
 				
 				
        End Sub
        
        Sub finexit(ByVal sender As Object, ByVal e As EventArgs)

            'if pstno.text ="" then
            'pstno.BackColor = Red
            'else
            Dim stat As String = "Published"
            updatevstat(stat, pstno.Text)
            updateadstat("Finalized")
           if session("Oposter") = "admanager" then
         	 	Response.Redirect("adpostings.aspx?source=admanager")
         	elseif session("Oposter") = "admanagerQ" then
         		Response.Redirect("adpostings.aspx?source=admanagerQ")
         	elseif session("Oposter") = "admgrpostOnce" then
         		Response.Redirect("admanager.aspx")
         	elseif session("Oposter") = "newpostpub"
         		Response.Redirect("adpostings.aspx?source=newpostpub")
         	elseif session("Oposter") = "planedit" then
        			Response.Redirect("adpostings.aspx?source=planedit")
             	'Response.Redirect("createad.aspx?action=edit&adno=" & session("Padno") & "&pplan=" &  session("Ppplan")& "&nav=posting")
            else
            	Response.Redirect("adpostings.aspx?source=admanager")
            end if
            
        End Sub
        Public Sub test(ByVal sender As Object, ByVal e As EventArgs)


        End Sub

        Sub accountset(ByVal sender As Object, ByVal e As EventArgs)
            if dd_acctyesno.selecteditem.text="Yes" then 
                dd_acctyesno.SelectedIndex = dd_acctyesno.Items.IndexOf(dd_acctyesno.Items.FindByText("Select"))
					tside.visible=true
                Response.Write("<script>window.open" & _
              "('" & session("acctsurl") & "','_new', 'width=800,height=500,resizable=1,scrollbars=1');</script>")
            	'Advenue.Attributes("src") = session("acctsurl")
                buttoncnt.Visible = True
            
            Else
                'Advenue.Attributes("src") = session("normurl")
                Panel1.Visible = False
                pnlpostad.Visible = True
                buttoncnt.Visible = False
                dd_acctyesno.SelectedIndex = dd_acctyesno.Items.IndexOf(dd_acctyesno.Items.FindByText("Select"))
            End If
           
        End Sub
        
        Sub finexitCHG(ByVal sender As Object, ByVal e As EventArgs)
             Dim stat As String = "Published"
            updatevstat(stat, pstno.Text)
            if session("Oposter") = "admanager" then
         	 	Response.Redirect("adpostings.aspx?source=admanager")
         	elseif session("Oposter") = "admanagerQ" then
         		Response.Redirect("adpostings.aspx?source=admanagerQ")
         	elseif session("Oposter") = "admgrpostOnce" then
         		Response.Redirect("admanager.aspx")
         	elseif session("Oposter") = "newpostpub"
         		Response.Redirect("adpostings.aspx?source=newpostpub")
         	elseif session("Oposter") = "planedit" then
        			Response.Redirect("adpostings.aspx?source=planedit")
             	'Response.Redirect("createad.aspx?action=edit&adno=" & session("Padno") & "&pplan=" &  session("Ppplan")& "&nav=posting")
            else
            	Response.Redirect("adpostings.aspx?source=admanager")
            end if
        End Sub
       
        
        
        
        Sub deferad(ByVal sender As Object, ByVal e As EventArgs)
            Dim stat As String = "Unpublished"
            updatevstat(stat,pstno.Text)
         	if session("Oposter") = "admanager" then
         	 	Response.Redirect("adpostings.aspx?source=admanager")
         	elseif session("Oposter") = "admanagerQ" then
         		Response.Redirect("adpostings.aspx?source=admanagerQ")
         	elseif session("Oposter") = "admgrpostOnce" then
         		Response.Redirect("admanager.aspx")
         	elseif session("Oposter") = "newpostpub"
         		Response.Redirect("adpostings.aspx?source=newpostpub")
         	elseif session("Oposter") = "planedit" then
        			Response.Redirect("adpostings.aspx?source=planedit")
             	'Response.Redirect("createad.aspx?action=edit&adno=" & session("Padno") & "&pplan=" &  session("Ppplan")& "&nav=posting")
            else
            	Response.Redirect("adpostings.aspx?source=admanager")
            end if
    

        End Sub
        
         Sub cad(ByVal sender As Object, ByVal e As EventArgs)
        		
        		lbltest1.visible=false
        		adtext.show=true
        		bcad.visible=false
        		bsad.visible=true
        		pnladtextmain.visible=false
        		pnledit.visible=true
        		bcad.visible=false
        		finishexitA.visible=false
        		finishexitAB.visible=false
        		bsadCan.visible=true
        		finishexit.visible=false
        	end sub
        	Sub sad(ByVal sender As Object, ByVal e As EventArgs)
        		saveadchanges()
        		 if pstno.Text ="" then
        		 
        		 else
        		  	Dim stat As String = "Published"
            	updatevstat(stat, pstno.Text)
        		 end if
        		 lbltest1.visible=true
        		 getadinfo()
        		bcad.visible=true
        		finishexitAB.visible=true
        		finishexit.visible=true
        		bsad.visible=false
        		bsadCan.visible=false
        		adtext.show=false        		
        		pnladtextmain.visible=true
        		pnledit.visible=false        		
        		finishexitA.visible=false
        		
        	end sub
        	
        	Sub sadCAN(ByVal sender As Object, ByVal e As EventArgs)
        		lbltest1.visible=true
        		adtext.show=false
        		bcad.visible=true
        		finishexitAB.visible=true
        		finishexit.visible=true
        		bsad.visible=false
        		bsadCan.visible=false
        		adtext.show=false        		
        		pnladtextmain.visible=true
        		pnledit.visible=false        		
        		finishexitA.visible=false
        end sub
        	
        	
        	
        	public sub saveadchanges()
        	 
            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String
	         sqlproc = "sp_Pupdatead"
               

            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure
      		
      		Dim prmadpk As New SqlParameter("@adno", SqlDbType.Int)
                prmadpk.Value = session("Padno")
                myCommandADD.Parameters.Add(prmadpk)
           

            Dim prmadtext As New SqlParameter("@adtext", SqlDbType.text)
            prmadtext.Value = adtext.content
            myCommandADD.Parameters.Add(prmadtext)

           
            Try
                myConnectionADD.Open()
                myCommandADD.ExecuteNonQuery()
                myConnectionADD.Close()
            Catch SQLexc As SqlException
                Response.Write("Insert Failed. Error Details are: " & SQLexc.ToString())
            Finally
                myConnectionADD.Close()
            End Try
       	end sub
        Sub copytext(ByVal sender As Object, ByVal e As EventArgs)
           
        End Sub

        Sub updatevstat(ByVal stat As String, postno as string)
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand

            Dim strSql As String
            If stat = "Unpublished" Then
            	if postno="" then
            		strSql = "update dbo.tbl_LeadADVenues set av_adplaced = '" & stat & "' where tbl_leadadvenues='" & session("Pvenueno") & "' "
         		else
            		strSql = "update dbo.tbl_LeadADVenues set av_adplaced = '" & stat & "', av_Postingno='" & pstno.text & "' where tbl_leadadvenues='" & session("Pvenueno") & "' "
         		end if
            Else
            	if postno="" then
            		strSql = "update dbo.tbl_LeadADVenues set av_adplaced = '" & stat & "', av_APFrom=getdate() where tbl_leadadvenues='" & session("Pvenueno") & "'"
       			else
            		strSql = "update dbo.tbl_LeadADVenues set av_adplaced = '" & stat & "', av_APFrom=getdate(), av_Postingno='" & postno & "' where tbl_leadadvenues='" & session("Pvenueno") & "'"
       
            	end if
            	
            End If

            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then

                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try

        End Sub
        
        
         Sub updateadstat(ByVal stat As String)
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand

            Dim strSql As String
               strSql = "update dbo.tbl_LeadADs set ad_stage = '" & stat & "' where tbl_leadad_pk='" & session("Padno") & "' and ad_userid='" & session("userid") & "'"
           

            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then

                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try

        End Sub
        
        
       
        Public Sub pagelayout()
            'the page template code below represents only a few of the things that
            'you can do. Play around with it, and you'll see just how much power is
            'in your hands

            'width will be calculated automatically, but it is sometimes
            'important to specify height
            ' layout.Width = "1100"
            'body.Height = "600"
            'body.VAlign = "top"
            'body.Width = "1100"
            'body.VAlign = "top"
            'RightNav.VAlign = "top"
            'layout.Border = 0
            'Header.Controls.Add(LoadControl("headersys.ascx"))
            'new LiteralControl("Some text.")
            'leftNav.Controls.Add(LoadControl("navigation2.ascx"))
            'leftNav.VAlign = "top"
            'LeftNav.Controls.Add(new LiteralControl("Some text."))

            'adjust size of LeftNav (just for the heck of it)
            'leftNav.Width = "100"


            'RightNav contents are included here, but try commenting
            'out the code below, to see how the page template dynamically
            'modifies itself (same goes with the LeftNav)
            'RightNav.Controls.Add(LoadControl("quicklink1.ascx"))
            'MiddleNav.Controls.Add(LoadControl("navigation.ascx"))
            'footer.Controls.Add(LoadControl("footer.ascx"))
        End Sub

    End Class
End Namespace