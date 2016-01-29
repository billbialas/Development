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

namespace PageTemplate
	public class addcontact 	
	   inherits PageTemplate
	   
	   public dd_agent
	   public dd_status, dd_leadtype, dd_highpri, dd_source
	   public l_fname, l_lname, l_hphone, l_cphone, l_email, l_address, l_city, l_state, l_zip, l_notes
	   public l_appdate,l_appttime,dd_apptloc,referal,l_referalother,l_comp,l_adcode
	   public option1,option2,option3,l_mailto,l_addresstolist
	 	public 	pnlnew, pnlack	,pnltenant,pnlinitialnotes,pnlviewnotes,pnladdencoutner,pnlassignedby
	   public agentemail, l_edate, l_enote
	   public l_delete,l_capdate
	   private shared pstatus as string
	   public l_savedraft,l_save,l_accept, l_close,l_assigendby
	 
	 	Protected WithEvents lead_history As System.Web.UI.WebControls.DataGrid
  	
	 
	   private Sub Page_Load(byval sender as object, byval e As EventArgs) handles mybase.load
			
	    	if  not (Page.IsPostBack) then
	    			clearsessions()
	    		'response.write (Request.QueryString("action"))
	    		'response.write (Request.QueryString("id"))
	    			dd_status.backcolor=lightgray
					dd_status.Enabled=false
					
	    	      if Request.QueryString("action")= "new" then
      				pnlnew.visible = true
						pnlack.visible = false
						pnlassignedby.visible = false
						l_delete.visible=false
						l_accept.visible=false
						l_close.visible=false
						pnlviewnotes.visible=false
						pnladdencoutner.visible=false
						bindagent()
						bindsource()
						l_capdate.text=DateTime.Now.toShortDateString() 
		   	
					else if Request.QueryString("action")= "view" then
      				pnlnew.visible = true
						pnlack.visible = false
						pnlassignedby.visible = true
						l_assigendby.backcolor=lightgray
						l_assigendby.Enabled=false
						l_delete.visible=true
						l_accept.visible=false
						l_close.visible=false
						pnladdencoutner.visible=false
						bindagent()
						bindsource()
						bindfields()
						BindhistoryGrid()
					
						'response.write(dd_status.SelectedItem.Text)
						if dd_status.SelectedItem.Text = "Draft" then
							pnlviewnotes.visible=false
							l_savedraft.visible = true
							
							l_save.text = "Save & Submit"
						end if
						if dd_status.SelectedItem.Text = "Unassigned" then
							l_accept.visible = true
							l_savedraft.visible = false
							if session("assignedbyUID") = session("userid") then
								l_save.visible = true
								l_save.text="Save"
							else 
								l_save.visible = false
							end if
						end if
						if dd_status.SelectedItem.Text = "Assigned" then
							'l_chnagestatus.visible = true
							l_accept.visible = false
							l_savedraft.visible = false
							l_save.visible = true
							l_save.text="Save"
							l_close.visible=true
						end if
						if dd_status.SelectedItem.Text = "Closed" then
							l_accept.visible = false
							l_savedraft.visible = false
							l_save.visible = false
							l_close.visible=false
						end if
	  	
	  			end if
	   	end if
	   	pagesetup()
	   end sub	   
	 	sub clearsessions()
	 		session("assignedagentid")=""
	 	  	session("pubuidfullname")=""
	   	session("assignedbyUID")=""
	   
	    
	    end sub
	 	
		 Sub BindhistoryGrid()
			Dim strpropID as String = Request.QueryString("id")
			Dim strUID as String = session("userid")
         Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
         dim mycommand as string
        	'	mycommand = "Select *, convert(varchar(20),ld_adddate,101) as ld_adddatef from dbo.tbl_leads Where ((ld_assignedtouid='"& strUID &"' or ld_assignedbyuid='"& strUID &"' or ld_agent='Any') and (ld_status='Unassigned' or ld_status='Assigned')) or ((ld_assignedbyuid='"& strUID &"') and ld_status='Draft') order by tbl_leads_pk desc"
          	mycommand = "Select *,convert(varchar(20),cnt_date,101) as date from tbl_leadscontacthistory Where tbl_leads_FK=" & strpropID
               Try
      				Dim dataAdapter As New SqlDataAdapter(myCommand, myConnection)
      				Dim dataSet As New DataSet()
      				dataAdapter.Fill(dataSet, "strpropID")
      				Dim dvProducts As New DataView(dataSet.Tables("strpropID"))
      				'dvProducts.RowFilter = strstatusFilter
      				lead_history.DataSource = dvProducts
      				lead_history.DataBind
    					Catch exc As System.Exception
      					Response.Write(exc.ToString())
    					Finally
      					myConnection.Dispose()
    					End Try	
           	End Sub
      
	   Sub MyDataGrid_Page(sender As Object, e As DataGridPageChangedEventArgs) 
			lead_history.CurrentPageIndex = e.NewPageIndex 
			BindhistoryGrid() 
		End Sub            
	   
 	Sub bindsource()
		  
			Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
         Dim myCommand As New SqlCommand("Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='leadsource'", myConnection)
               	
               Try
               	myConnection.Open()
                  dd_source.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
                  dd_source.DataBind()
                  Catch SQLexc As SqlException
                       Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
               End Try
    
     		dd_source.Items.Insert(0, new ListItem("None","9999"))
      	   	
     	end sub
 
 
 
 
  Sub bindagent()
		  
			Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
         Dim myCommand As New SqlCommand("Select users_tbl_pk, fname + ' ' + lname as agent from dbo.tbl_users", myConnection)
               	
               Try
               	myConnection.Open()
                  dd_agent.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
                  dd_agent.DataBind()
                  Catch SQLexc As SqlException
                       Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
               End Try
    
     	dd_agent.Items.Insert(0, new ListItem("Any","9999"))
      	   	
     	end sub
    
    
   public Sub  btn_addcontact (sender As Object, e As EventArgs)
	 	pnladdencoutner.visible=true
	 	pnlviewnotes.visible=false
			
   end sub
 
   public Sub  btn_savecontact (sender As Object, e As EventArgs)
	 	pnladdencoutner.visible=false
	 	pnlviewnotes.visible=true
	 		
   end sub
 
   
   public Sub  btn_close (sender As Object, e As EventArgs)
	 	dd_status.SelectedIndex = dd_status.Items.IndexOf(dd_status.Items.FindByText("Closed"))
  		pstatus = dd_status.SelectedItem.Text
      getagentUID()
		insertdb()
   end sub
   
    
   public Sub  btn_acceptlead (sender As Object, e As EventArgs)
	 	getagentfullname()
	 	dd_agent.SelectedIndex = dd_agent.Items.IndexOf(dd_agent.Items.FindByText(session("pubuidfullname")))
	 	dd_status.SelectedIndex = dd_status.Items.IndexOf(dd_status.Items.FindByText("Assigned"))
   	l_accept.visible = false
		l_savedraft.visible = false
		l_save.visible = true
		
   end sub
   
   public Sub  btn_save (sender As Object, e As EventArgs)
		' 	
       if Request.QueryString("action")= "new" then
      	pstatus = dd_status.SelectedItem.Text
      	getagentUID()
      	insertdb()
			if dd_status.SelectedItem.Text = "Unassigned" then 
				getagentemail()
				sendemailagents(agentemail)
			end if			
		else  if Request.QueryString("action")= "view" then
	    	pstatus = dd_status.SelectedItem.Text
	    	if pstatus = "Draft" then 
	    		pstatus = "Unassigned"
				getagentemail()
				sendemailagents(agentemail)
	    	end if
	    	getagentUID()
			'response.write (pstatus)
			
	    	insertdb()
		end if
		response.redirect("leads.aspx")
		'pnlnew.visible = false
		'pnlack.visible = true
      			
	end sub 	
	
	public Sub  btn_savedraft (sender As Object, e As EventArgs)
		pstatus = "Draft"
     	if Request.QueryString("action")= "new" then
      	getagentUID()
      	insertdb()
		else  if Request.QueryString("action")= "view" then
	    	getagentUID()
	    	insertdb()
		end if
     	response.redirect("leads.aspx")
		'pnlnew.visible = false
		'pnlack.visible = true
  end sub	
    
   public Sub  btn_delete (sender As Object, e As EventArgs)
			Dim strpropID as String = Request.QueryString("id")
			Dim strSql as String = "delete from dbo.tbl_leads Where tbl_leads_pk="& strpropID
         Dim sqlCmd As SqlCommand
	 		Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
        			sqlCmd = New SqlCommand(strSql, myConnection)
              	
               Try
               	myConnection.Open()
                 	Dim Sqldr as SqlDataReader = sqlCmd.ExecuteReader
    					Catch exc As System.Exception
      					Response.Write(exc.ToString())
    					Finally
      					myConnection.Dispose()
    					End Try	
			response.redirect("leads.aspx")
			'pnlnew.visible = false
			'pnlack.visible = true
      		
	end sub 		    
    
	public Sub btn_cancel (sender As Object, e As EventArgs)
		response.redirect("leads.aspx")
	end sub

	public Sub btn_continue (sender As Object, e As EventArgs)
		response.redirect("leads.aspx")
	end sub

  	Sub insertdb ()
   		Dim rightNow as DateTime = DateTime.Now.toShortDateString() 
   		'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
			Dim RightNowAdd as datetime = datetime.now
   		Dim supportedFormats() As String = New String() {"M/dd/yyyy","M/d/yyyy","MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
		   Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
  			dim sqlproc as string
  		  
  			if Request.QueryString("action")= "new" then
  				sqlproc = "sp_addlead"
   	   else if Request.QueryString("action")= "view" then
          	sqlproc = "sp_updatelead"
  			end if
  			response.write("OK")
  	     	Dim myCommandADD As New SqlCommand( sqlproc, myConnectionADD)
  		 	myCommandADD.CommandType = CommandType.StoredProcedure
    	    
         ' Add Parameters to SPROC
  		  		if Request.QueryString("action")= "new" then
             	Dim prmuid As New SqlParameter("@uid", SqlDbType.VarChar, 50)
					prmuid.Value = session("userid")
  					myCommandADD.Parameters.Add(prmuid)
  				end if
  				
  				Dim prmlfname As New SqlParameter("@l_fname", SqlDbType.VarChar, 50)
				if l_fname.text = "" then 
					prmlfname.Value = DBNull.Value
				else
					prmlfname.Value = l_fname.text
  				end if
  				myCommandADD.Parameters.Add(prmlfname)
			
				Dim prmllname As New SqlParameter("@l_lname", SqlDbType.VarChar, 50)
				if l_lname.text = "" then 
					prmllname.Value = DBNull.Value
				else
					prmllname.Value = l_lname.text
  				end if
  				myCommandADD.Parameters.Add(prmllname)
			
				Dim prmhphone As New SqlParameter("@l_hphone", SqlDbType.VarChar, 50)
				if l_hphone.text = "" then 
					prmhphone.Value = DBNull.Value
				else
					prmhphone.Value = l_hphone.text
  				end if
  				myCommandADD.Parameters.Add(prmhphone)
		
				Dim prmcphone As New SqlParameter("@l_cphone", SqlDbType.VarChar, 50)
				if l_cphone.text = "" then 
					prmcphone.Value = DBNull.Value
				else
					prmcphone.Value = l_cphone.text
  				end if
  				myCommandADD.Parameters.Add(prmcphone)

				Dim prmaddress As New SqlParameter("@l_address", SqlDbType.VarChar, 30)
				if l_address.text = "" then 
					prmaddress.Value = DBNull.Value
				else
					prmaddress.Value = l_address.text
  				end if
  				myCommandADD.Parameters.Add(prmaddress)

				Dim prmcity As New SqlParameter("@l_city", SqlDbType.VarChar, 30)
				if l_city.text ="" then
					prmcity.Value= DBNull.Value
				else	
					prmcity.Value = l_city.text
				end if
				myCommandADD.Parameters.Add(prmcity)

				Dim prmstate As New SqlParameter("@l_state", SqlDbType.VarChar, 2)
				if l_state.text = "" then
					prmstate.Value = DBNull.Value
				else
					prmstate.Value =l_state.text
				end if
				myCommandADD.Parameters.Add(prmstate)

            Dim prmzip As New SqlParameter("@l_zip", SqlDbType.VarChar, 50)
				if l_zip.text ="" then 
					prmzip.Value = DBNull.Value
				else
					prmzip.Value = l_zip.text
				end if
				myCommandADD.Parameters.Add(prmzip)
				
				Dim prmagent As New SqlParameter("@l_agent", SqlDbType.VarChar, 30)
				prmagent.Value = dd_agent.SelectedItem.Text
				myCommandADD.Parameters.Add(prmagent)

				Dim prmagentFK As New SqlParameter("@l_agent_FK", SqlDbType.VarChar, 30)
				prmagentFK.Value = dd_agent.SelectedItem.value
				myCommandADD.Parameters.Add(prmagentFK)

				Dim prmstatus As New SqlParameter("@l_status", SqlDbType.VarChar, 30)
				prmstatus.Value = pstatus
				myCommandADD.Parameters.Add(prmstatus)

				Dim prmleadtype As New SqlParameter("@l_leadtype", SqlDbType.VarChar, 30)
				prmleadtype.Value = dd_leadtype.SelectedItem.Text
				myCommandADD.Parameters.Add(prmleadtype)
				
				Dim prmnotes As New SqlParameter("@l_notes", SqlDbType.text)
				if l_notes.Text = "" then
					prmnotes.Value = DBNull.Value
				else
					prmnotes.Value = l_notes.Text
				end if
				myCommandADD.Parameters.Add(prmnotes)

				Dim prmemail As New SqlParameter("@l_email", SqlDbType.VarChar, 30)
				prmemail.Value = l_email.Text
				myCommandADD.Parameters.Add(prmemail)
	
				Dim prmadddate As New SqlParameter("@adddate", SqlDbType.datetime)
				prmadddate.Value = RightNowAdd
				myCommandADD.Parameters.Add(prmadddate)

      		if Request.QueryString("action")= "view" then
           		Dim prmtblleadpk As New SqlParameter("@ld_tbl_pk", SqlDbType.int)
					prmtblleadpk.Value = Request.QueryString("id")
  					myCommandADD.Parameters.Add(prmtblleadpk)
  				end if
  				'---------------------------------------------------------------------
  				
  				Dim prmcapdate As New SqlParameter("@capdate", SqlDbType.datetime)
				
				if l_capdate.text = "" then 
					prmcapdate.Value = DBNull.Value
				else
					prmcapdate.Value = DateTime.ParseExact(l_capdate.text, supportedFormats, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None)
  				end if
  				
				myCommandADD.Parameters.Add(prmcapdate)
				
				Dim prmapptdate As New SqlParameter("@apptdate", SqlDbType.datetime)
				if l_appdate.Text = "" then 
					prmapptdate.Value= DBNull.Value
				else 
					prmapptdate.Value = DateTime.ParseExact(l_appdate.text, supportedFormats, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None)
				end if
				myCommandADD.Parameters.Add(prmapptdate)
				
				Dim prmappttime As New SqlParameter("@appttime", SqlDbType.VarChar, 5)
				prmappttime.Value = l_appttime.Text
				myCommandADD.Parameters.Add(prmappttime)
				
				Dim prmapptloc As New SqlParameter("@apptloc", SqlDbType.VarChar, 30)
				prmapptloc.Value = dd_apptloc.SelectedItem.value
				myCommandADD.Parameters.Add(prmapptloc)
				
				Dim prmrefermortgage As New SqlParameter("@refermortg", SqlDbType.VarChar, 5)
				if referal.Items(1).Selected then 
					prmrefermortgage.Value = "Y"
				else
					prmrefermortgage.Value = "N"
				end if
				myCommandADD.Parameters.Add(prmrefermortgage)
				
				Dim prmrefercredit As New SqlParameter("@refercredit", SqlDbType.VarChar, 5)
				if referal.Items(0).Selected then 
					prmrefercredit.Value = "Y"
				else
					prmrefercredit.Value = "N"
				end if
				myCommandADD.Parameters.Add(prmrefercredit)
				
				Dim prmreferother As New SqlParameter("@referother", SqlDbType.VarChar, 5)
				if referal.Items(2).Selected then 
					prmreferother.Value = "Y"
				else
					prmreferother.Value = "N"
				end if
				myCommandADD.Parameters.Add(prmreferother)
				
				Dim prmreferotherex As New SqlParameter("@referotherex", SqlDbType.VarChar,50)
				prmreferotherex.Value = l_referalother.Text
				myCommandADD.Parameters.Add(prmreferotherex)
				
				Dim prmcomp As New SqlParameter("@comp", SqlDbType.varchar,15)
				if l_comp.Text = "" then 
					prmcomp.Value= DBNull.Value
				else 
					prmcomp.Value = l_comp.Text
				end if
				myCommandADD.Parameters.Add(prmcomp)
				
				Dim prmassignedagent As New SqlParameter("@assignedagent", SqlDbType.VarChar,50)
				prmassignedagent.Value = session("assignedagentid")
				myCommandADD.Parameters.Add(prmassignedagent)
				
				Dim prmhighpri As New SqlParameter("@highpri", SqlDbType.VarChar, 5)
				prmhighpri.Value = dd_highpri.SelectedItem.value
				myCommandADD.Parameters.Add(prmhighpri)
				 
				Dim prmldsource As New SqlParameter("@leadsource", SqlDbType.VarChar, 50)
				prmldsource.Value = dd_source.SelectedItem.text
				myCommandADD.Parameters.Add(prmldsource)
				 
				 Dim prmadcode As New SqlParameter("@adcode", SqlDbType.VarChar,50)
				prmadcode.Value = l_adcode.text
				myCommandADD.Parameters.Add(prmadcode)
				
				Dim prmmailtoaddress As New SqlParameter("@mailtoaddress", SqlDbType.VarChar,50)
				if l_mailto.checked then 
					prmmailtoaddress.Value = "Y"
				else
					prmmailtoaddress.Value = "N"
				end if
				myCommandADD.Parameters.Add(prmmailtoaddress)
				
				Dim prmproptolist As New SqlParameter("@proplist", SqlDbType.VarChar,50)
				if l_addresstolist.checked then 
					prmproptolist.Value = "Y"
				else
					prmproptolist.Value = "N"
				end if
				myCommandADD.Parameters.Add(prmproptolist )
	
	
'				Dim prmneeddate As New SqlParameter("@needdate", SqlDbType.datetime)
'				Dim supportedFormats() As String = New String() {"MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
'				if txtDate.text = "" then 
'					prmneeddate.Value = DBNull.Value
'				else	
'					'prmneeddate.Value = Convert.ToDateTime(txtDate.text)
'					prmneeddate.Value = DateTime.ParseExact(txtDate.text, supportedFormats, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None)
'
'				end if
'				myCommandADD.Parameters.Add(prmneeddate)

				
           Try
               myConnectionADD.Open()
               myCommandADD.ExecuteNonQuery()
               myConnectionADD.Close()
                Catch SQLexc As SqlException
                      Response.Write("Insert Failed. Error Details are: " & SQLexc.ToString())
                 End Try
      
   	end sub
 
 public sub getagentemail() 
 		if dd_agent.selecteditem.text <> "Any" then 	
  			Dim strUID as String = session("userid")
 			Dim strSql as String = "SELECT * from tbl_users where users_tbl_pk='"& dd_agent.SelectedItem.value &"'"
         Dim sqlCmd As SqlCommand
	 		
			      Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
        			sqlCmd = New SqlCommand(strSql, myConnection)
              	
               Try
               	myConnection.Open()
                 	Dim Sqldr as SqlDataReader = sqlCmd.ExecuteReader
         				if Sqldr.read() then
         					if Sqldr("email") IsNot DBNull.Value  then
         						agentemail= Sqldr("email2") 
         					end if
         					 	
            			end if
      				Catch SQLexc As SqlException
                       Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
               	finally
               		myConnection.close()
               End Try
        else
        		agentemail="BPONotifications@gochoiceone.com"
  			end if
   End Sub	

 public sub sendemailagents(byval mail_to as string)
 		'notificationemail = bpo_email.text
 		Dim mail As New MailMessage()
 		dim prefered as string
  		
  		'Set the properties - send the email to the person who filled out the
  		mail.From = New MailAddress("mychoice@gochoiceone.com")
  		mail.To.Add(mail_to)
  		mail.cc.add("sbialas@gochoiceone.com")
  		if dd_agent.selecteditem.text = "Any"
  			if dd_highpri.text = "Yes" then
  				mail.Subject = "New Lead Notification-HIGH PRIORITY- Open to all Agents"
  			else 
  				mail.Subject = "New Lead Notification- Open to all Agents"
  			end if
		else
			if dd_highpri.text = "Yes" then
  				mail.Subject = "New Lead Notification-HIGH PRIORITY- Preferred Agent"
			else 
				mail.Subject = "New Lead Notification- Preferred Agent"
			end if
		end if
  		'Set the body
  				if dd_agent.selecteditem.text = "Any"
  						prefered = "There is not a preferred Agent and this Lead is open for all agents."        
               else
               	prefered = "There is a preferred Agent for this Lead.  IF that Agent does not accept within 30 minutes then it will become available to all Agents."
              	end if
         
  		mail.Body = "At " + DateTime.Now + " a  New Lead request was submitted to the Choice One " & _
               "Web site. " & prefered & vbCrLf & vbCrLf & _
      	      "Below you will find the details for this lead. " & _
 					vbCrLf & _
               "____________________________________________________" & vbCrLf & vbCrLf & _
               "Lead Information" & vbcrlf & _
               "-------------------" & vbcrlf & _
               "Name:    " & l_fname.Text & " " & l_lname.Text & vbcrlf & _
               "Home Phone: " & l_hphone.Text &  vbcrlf & _
               "Cell Phone: " & l_cphone.Text 
                
  
 	 	'send the message
  		Dim smtp As New SmtpClient("smtp.comcast.net")
  		smtp.Send(mail)
  		
  	end sub 
  	
 	Sub bindfields()
 					Dim strUID as String = session("userid")
  					Dim strSql as String = "SELECT *,fname + ' ' + lname as assignedby from tbl_leads join dbo.tbl_users on Uid=ld_assignedbyuid where tbl_leads_pk="& Request.QueryString("id")  
         		Dim sqlCmd As SqlCommand
	 		
			      Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
        			sqlCmd = New SqlCommand(strSql, myConnection)
              	
               Try
               	myConnection.Open()
                 	Dim Sqldr as SqlDataReader = sqlCmd.ExecuteReader
         				if Sqldr.read() then
         					if Sqldr("ld_status") IsNot DBNull.Value  then
         						dd_status.SelectedIndex = dd_status.Items.IndexOf(dd_status.Items.FindByText(Sqldr("ld_status")))
   	       					'dd_status.selecteditem.text= Sqldr("ld_status")
         					end if
         					if Sqldr("ld_type") IsNot DBNull.Value  then
         						dd_leadtype.SelectedIndex = dd_leadtype.Items.IndexOf(dd_leadtype.Items.FindByText(Sqldr("ld_type")))
   	       					'dd_leadtype.selecteditem.text= Sqldr("ld_type")
         					end if
         					if Sqldr("ld_agent") IsNot DBNull.Value  then
         						dd_agent.SelectedIndex = dd_agent.Items.IndexOf(dd_agent.Items.FindByText(Sqldr("ld_agent")))
   	       					'dd_agent.selecteditem.text= Sqldr("ld_agent")
         					end if
         					if Sqldr("ld_fname") IsNot dbnull.value then
            				 	   l_fname.Text = Sqldr("ld_fname")
            				end if
         					if Sqldr("ld_lname") IsNot dbnull.value then
            				 	   l_lname.Text = Sqldr("ld_lname")
            				end if
         					if Sqldr("ld_hphone") IsNot dbnull.value then
            				 	   l_hphone.Text = Sqldr("ld_hphone")
            				end if
         					if Sqldr("ld_cphone") IsNot dbnull.value then
            				 	   l_cphone.Text = Sqldr("ld_cphone")
            				end if
         					if Sqldr("ld_email") IsNot dbnull.value then
            				 	   l_email.Text = Sqldr("ld_email")
            				end if
         					if Sqldr("ld_address") IsNot dbnull.value then
            				 	   l_address.Text = Sqldr("ld_address")
            				end if
         					if Sqldr("ld_city") IsNot dbnull.value then
            				 	   l_city.Text = Sqldr("ld_city")
            				end if
         					if Sqldr("ld_state") IsNot dbnull.value then
            				 	   l_state.Text = Sqldr("ld_state")
            				end if
         					if Sqldr("ld_zip") IsNot dbnull.value then
            				 	   l_zip.Text = Sqldr("ld_zip")
            				end if
         					if Sqldr("ld_notes") IsNot dbnull.value then
            				 	   l_notes.Text = Sqldr("ld_notes")
            				end if
            				if Sqldr("ld_capturedate") IsNot dbnull.value then
            				 	   l_capdate.Text = Sqldr("ld_capturedate")
            				end if
            				if Sqldr("ld_apptdate") IsNot dbnull.value then
            				 	   l_appdate.Text = Sqldr("ld_apptdate")
            				end if
            				if Sqldr("ld_appttime") IsNot dbnull.value then
            				 	   l_appttime.Text = Sqldr("ld_appttime")
            				end if
            				if Sqldr("ld_referotherexplain") IsNot dbnull.value then
            				 	   l_referalother.Text = Sqldr("ld_referotherexplain")
            				end if
            				if Sqldr("ld_compensation") IsNot dbnull.value then
            				 	   l_comp.Text = Sqldr("ld_compensation")
            				end if
         					if Sqldr("ld_apptlocation") IsNot DBNull.Value  then
         						dd_apptloc.SelectedIndex = dd_apptloc.Items.IndexOf(dd_apptloc.Items.FindByText(Sqldr("ld_apptlocation")))
   	       					'dd_leadtype.selecteditem.text= Sqldr("ld_type")
         					end if
         					if Sqldr("ld_refermortg") IsNot DBNull.Value then
         						if Sqldr("ld_refermortg") = "Y" then
            				 	   	referal.Items(1).Selected = true
            					else 
            				 		referal.Items(1).Selected = false
            					end if
            				end if
            				if Sqldr("ld_refercredit") IsNot DBNull.Value then
            					if Sqldr("ld_refercredit") = "Y" then
            					 	   referal.Items(0).Selected = true
            					else 
            					 	referal.Items(0).Selected = false
            					end if
            				end if
            				if Sqldr("ld_referother") IsNot DBNull.Value then
            					if Sqldr("ld_referother") = "Y" then
            					 	   referal.Items(2).Selected = true
            					else 
            					 	referal.Items(2).Selected = false
            					end if
            				end if
            				l_assigendby.text= Sqldr("assignedby")
            				session("assignedbyUID") = Sqldr("ld_assignedbyuid")
            				dd_highpri.SelectedIndex = dd_highpri.Items.IndexOf(dd_highpri.Items.FindByText(Sqldr("ld_highpri")))
   	       				
   	       				
   	       				if Sqldr("ld_mailtoaddress") IsNot DBNull.Value then
            					if Sqldr("ld_mailtoaddress") = "Y" then
            					 	 l_mailto.checked   = true
            					else 
            					 	l_mailto.checked = false
            					end if
            				end if
            				if Sqldr("ld_proptolist") IsNot DBNull.Value then
            					if Sqldr("ld_proptolist") = "Y" then
            					 	 l_addresstolist.checked    = true
            					else 
            					 l_addresstolist = false
            					end if
            				end if
            				
            				if Sqldr("ld_adcode") IsNot dbnull.value then
            				 	   l_adcode.Text = Sqldr("ld_adcode")
            				end if
   	       				dd_source.SelectedIndex = dd_source.Items.IndexOf(dd_source.Items.FindByText(Sqldr("ld_adsource")))
   	       				
   	       				
   	       				
            			end if
      				Catch SQLexc As SqlException
                       Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
               	finally
               		myConnection.close()
               End Try
		   End Sub


	Sub displayquestions(Source As System.Object, e As System.EventArgs)
   	 if dd_leadtype.selecteditem.text="Tenant" then
   	 	pnltenant.visible=true
   	 else
   	 	pnltenant.visible=false
   	 end if
  	End Sub
  	
	
sub getagentUID()
	if dd_agent.selecteditem.value <> 9999 then
		Dim strSql as String = "SELECT UID from tbl_users where users_tbl_PK=" & dd_agent.selecteditem.value
   	Dim sqlCmd As SqlCommand
   	Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
      sqlCmd = New SqlCommand(strSql, myConnection)
      
      	Try
         	myConnection.Open()
            Dim Sqldr as SqlDataReader = sqlCmd.ExecuteReader
         	if Sqldr.read() then
   				session("assignedagentid") = Sqldr("UID")
     
   			end if
   			Catch SQLexc As SqlException
      	      Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
           	finally
           		myConnection.close()
         	end Try
    else 
    	session("assignedagentid")="Any"
    end if
	end sub
	
	sub getagentfullname()
		Dim strSql as String = "SELECT fname + ' ' + lname as 'agentname' from tbl_users where UID='" & session("userid") & "'"
   	Dim sqlCmd As SqlCommand
   	Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
      sqlCmd = New SqlCommand(strSql, myConnection)
      
      	Try
         	myConnection.Open()
            Dim Sqldr as SqlDataReader = sqlCmd.ExecuteReader
         	if Sqldr.read() then
   				session("pubuidfullname") = Sqldr("agentname")
     
   			end if
   			Catch SQLexc As SqlException
      	      Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
           	finally
           		myConnection.close()
         	end Try
    
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

 '<mbcbb:ComboBox id="dd_agent" runat="server" DataTextField="agent"
	'				   DataValueField="users_tbl_pk" ></mbcbb:ComboBox></td>
  	'		</t
	end class
end namespace