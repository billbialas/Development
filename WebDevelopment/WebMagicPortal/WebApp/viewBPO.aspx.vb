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
imports system.net.mail

namespace PageTemplate
	public class viewbpo 	
	   inherits PageTemplate
	   
	   public pnlviewbpo, pnlheaderview, pnlacceptwarn	
	   public lit_requestor, lit_company, Lit_phone, lit_fax
	   public status as string	 
	   public repeater1 
	  	public bpo_preferagentA
  		public pnlacceptbpo,pnlbposavewarn, pnlack,pnlacceptbpobut, pnlheaderconfirm,pnlchk2submit
	   private shared pubstatus as string
	   private shared pubuid as string
	   private shared pubrequid as string
	   private shared pubaccess as string
	   private shared pubuidfullname as string
	   private shared pubphotosordered as string
	   private shared pubprefer as string
	   
	   
	   public bpo_requester, bpo_phone, bpo_cphone,bpo_email, bpo_fax, bpo_company, bpo_cusaddress, bpo_cuscity,bpo_cusstate,bpo_cuszip, bpo_cusloanno, bpo_cuscontact
	   public bpo_comp, bpo_rush, txtDate, bpo_cusname,prmcusname,prmcusloanno, bpo_HomeOwnerphone, bpo_type, bpo_photos,bpo_occupancy,bpo_cuscontactphone
	   public bpo_noexterior, bpo_nointerior, bpo_listagent, bpo_Listed, bpo_listagentphone,bpo_incontact, bpo_incontactphone, bpo_instructions
	   public bpo_preferagent, bpo_number,bpo_status
  	   public  bpo_source, bpo_cusno, bpo_photovendor, bpotime
  	   public bpo_acceptbpo ,bpo_edit, bpo_savecontinue, bpo_delete, bpo_savelater, bpo_submit, bpo_cancel,bpo_closebpo
  	   public chktermaccept
	  	public notificationemail as string
	 
	   private Sub Page_Load(byval sender as object, byval e As EventArgs) handles mybase.load
				   
	    	if  not (Page.IsPostBack) then
				initprocess()
				page1setup()			
			end if
	 	
	 		'width will be calculated automatically, but it is sometimes
			'important to specify height
			Body.Height = "400"
			Body.VAlign = "top"
			RightNav.VAlign = "top"
			Layout.border = 0
			Header.Controls.Add(LoadControl("headersys.ascx"))
			LeftNav.Controls.Add(LoadControl("navigation2.ascx"))
			LeftNav.VAlign = "top"
			'LeftNav.Controls.Add(new LiteralControl("Some text."))

			'adjust size of LeftNav (just for the heck of it)
			LeftNav.Width = "100"
			
			'LeftNav.Controls.Add(LoadControl("navigation.ascx"));
			'LeftNav.Controls.Add(new LiteralControl("Some text."));

			'adjust size of LeftNav (just for the heck of it)
			'LeftNav.Width = "100";

			'RightNav.Controls.Add(LoadControl("quicklink1.ascx"))
			'MiddleNav.Controls.Add(LoadControl("navigation.ascx"))
	   end sub

'---Buttons  
	public sub	btn_closbpo(sender As Object, e As EventArgs)

	end sub
	
	public sub	btn_submit(sender As Object, e As EventArgs)
			if pubaccess =  "Partner" or pubaccess =  "Broker" then
				if pubstatus = "Draft" then
	  				if chktermaccept.checked = true then
						pubstatus = "Submitted-Unassigned"
						pubuid = ""
						updatedb()
						sendemailrequester()
	    				sendemailagents()
	    				if  bpo_photos.SelectedItem.Text ="Yes" 
							if pubphotosordered = "Yes" then 
								pubphotosordered = "Yes"
	    					else 
		  						sendemailphotovendor()
	    						pubphotosordered = "Yes"
	    					end if
	    				end if			
						response.redirect("bpo.aspx")
 					else 
	 					pnlchk2submit.visible=true
	 				end if
				end if
			else if pubaccess =  "Agent" then
				if  bpo_photos.SelectedItem.Text ="Yes" 
					if pubphotosordered = "Yes" then 
						pubphotosordered = "Yes"
	    			else 
		  				sendemailphotovendor()
	    				pubphotosordered = "Yes"
	    			end if
	    		end if			
				'response.write(pubuidfullname)
				'response.write(pubuid)
				'response.write(bpo_preferagent.item.Text(2))
   	     	'response.write(bpo_preferagent.SelectedItem.text)
   	     	'response.write(bpo_preferagent.SelectedItem.value)
   	     	
				'getagentfullname()
				updatedb()
				response.redirect("bpo.aspx")
			end if

 	end sub
 		
 		public sub	ClientOnChange(sender As Object, e As EventArgs)
 			'	response.write(chktermaccept.selecteditem.value)
 			'	response.write("DDDD")	
 			'if chktermaccept.checked = true then
 		 	'	bpo_submit.visible=true
 			'else 
 			'		bpo_submit.visible=false
 			'end if
 		end sub
 		
		public Sub btn_acceptterms (sender As Object, e As EventArgs)
			bpo_acceptbpo.visible=false
			pnlacceptbpo.visible=false
		   pnlheaderview.visible=true
		   pnlviewbpo.visible=true
			bpo_submit.visible=True	 					
			bpo_savelater.visible=True
			bpo_cancel.visible=True
			bpo_submit.text ="Save"
			bpo_savelater.visible=false
	  		pnlacceptwarn.visible=false	
			pubstatus = "Submitted-Assigned"
			bpo_status.SelectedItem.text = pubstatus
			pubuid= session("userid")
			getagentfullname()
			setfieldstatus(false)
			setbkgcolor(white)
		  	bpo_number.readonly=true
			bpo_number.backcolor=System.Drawing.Color.lightgray
			bpo_status.enabled=false
			bpo_status.backcolor=System.Drawing.Color.lightgray
			if  bpo_photos.SelectedItem.Text ="Yes" 
				if pubphotosordered = "Yes" then 
					pubphotosordered = "Yes"
	    		else 
		  			sendemailphotovendor()
	    			pubphotosordered = "Yes"
	    		end if
	    	end if			
						
			updatedb()
			'response.redirect("bpo.aspx")
					
 		end sub
     
     public Sub btn_savecontinue (sender As Object, e As EventArgs)
					if pubuid<>session("userid") then
						pubuid = session("userid")
					end if
					updatedb()
					response.redirect("bpo.aspx")
 			
		end sub
   
 		public Sub btn_deletebpo (sender As Object, e As EventArgs)
					deletedb()
 					pnlack.visible=true
					pnlheaderview.visible=false
	 				pnlbposavewarn.visible=false
	 				pnlacceptbpo.visible=false
	 				pnlacceptbpobut.visible=false
	 				pnlviewbpo.visible=false
					pnlchk2submit.visible=false
					pnlheaderconfirm.visible=false
 		end sub
   
 		public Sub btn_continue (sender As Object, e As EventArgs)
			response.redirect("bpo.aspx")
 					
 		end sub
 		
 		public Sub btn_acceptbpo (sender As Object, e As EventArgs)
			'response.redirect("bpo.aspx")
 					
 		end sub
 		
   
 		 Sub btn_cancel (sender As Object, e As EventArgs)
 			response.redirect("bpo.aspx")
 		end sub
 		
 		public Sub btn_savelater (sender As Object, e As EventArgs)
			pubuid=""
			pubuidfullname=""
			updatedb()
			response.redirect("bpo.aspx")
		end sub


 		public Sub btn_editbpo (sender As Object, e As EventArgs)
			bpo_submit.visible=True	 					
			bpo_savelater.visible=True
			bpo_cancel.visible=True
			bpo_edit.visible=false
			
			setfieldstatus(false)
			setbkgcolor(white)
		  	bpo_number.readonly=true
			bpo_number.backcolor=System.Drawing.Color.lightgray
			bpo_status.enabled=false
			bpo_status.backcolor=System.Drawing.Color.lightgray
				
			if pubaccess =  "Partner" or pubaccess =  "Broker" then
				if pubstatus = "Draft" then
	  				'dim orgstatus as string = bpo_status.selecteditem.text
	  				'dim orgpreferagent as string = bpo_preferagent.selecteditem.text
	  				'bindBPOStatus()
	  				'BindPreferAgent()
	  				'bpo_status.selecteditem.text = orgstatus
	  				'bpo_preferagent.selecteditem.text = orgpreferagent
					bpo_delete.visible=true	
					pnlbposavewarn.visible=true
					bpo_edit.visible=false
					pnlheaderconfirm.visible=true
				end if
	  		else if pubaccess =  "Agent" then
	  			if pubstatus = "Submitted-Assigned" then
	  				bpo_submit.text ="Save"
	  				pnlheaderconfirm.visible=false
					bpo_savelater.visible=false
					bpo_closebpo.visible=true

	  			end if
	  				
	  		end if
	  		
		 end sub
 	
  Sub BindCompany()
		  
			dim strbpono as integer = Request.QueryString("id")
			Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
         Dim myCommand As New SqlCommand("Select tbl_bpocompanys_pk, company from dbo.tbl_bpocompanys", myConnection)
               	
               Try
               	myConnection.Open()
                  bpo_company.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
                  bpo_company.DataBind()
                  Catch SQLexc As SqlException
                       Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
               End Try
     	end sub
    
	
		Sub bindbposource()
		  
			dim strbpono as integer = Request.QueryString("id")
			Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
         Dim myCommand As New SqlCommand("Select tbl_bposources_pk, bposource from dbo.tbl_bposources", myConnection)
               	
               Try
               	myConnection.Open()
                  bpo_source.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
                  bpo_source.DataBind()
                  Catch SQLexc As SqlException
                       Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
               End Try
     	end sub
 		
 		Sub bindphotovendor()
		  
			dim strbpono as integer = Request.QueryString("id")
			Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
         Dim myCommand As New SqlCommand("Select tbl_photovendors_pk, bpophotovendor from dbo.tbl_photovendors", myConnection)
               	
               Try
               	myConnection.Open()
                  bpo_photovendor.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
                  bpo_photovendor.DataBind()
                  Catch SQLexc As SqlException
                       Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
               End Try
     	end sub
 		
 		 Sub updatedb ()
      	dim strbpono as integer = Request.QueryString("id")
			
      	Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
         Dim myCommandADD As New SqlCommand("sp_updatebpo" , myConnectionADD)
         myCommandADD.CommandType = CommandType.StoredProcedure
            	   
         ' Add Parameters to SPROC
  
         	Dim prmtbl_bpo_pk As New SqlParameter("@tbl_bpo_pk", SqlDbType.int)
				prmtbl_bpo_pk.Value = strbpono
  				myCommandADD.Parameters.Add(prmtbl_bpo_pk)
         
         	Dim prmuid As New SqlParameter("@uid", SqlDbType.VarChar, 50)
				prmuid.Value = pubrequid
  				myCommandADD.Parameters.Add(prmuid)
  
  				Dim prmrequestor As New SqlParameter("@requestor", SqlDbType.VarChar, 50)
				if bpo_requester.text = "" then 
					prmrequestor.Value = DBNull.Value
				else
					prmrequestor.Value = bpo_requester.text
  				end if
  				myCommandADD.Parameters.Add(prmrequestor)
			
				Dim prmstatus As New SqlParameter("@status", SqlDbType.VarChar, 20)
				prmstatus.Value = pubstatus
				myCommandADD.Parameters.Add(prmstatus)

				Dim prmaddress As New SqlParameter("@address", SqlDbType.VarChar, 30)
				if bpo_cusaddress.text = "" then 
					prmaddress.Value = DBNull.Value
				else
					prmaddress.Value = bpo_cusaddress.text
  				end if
  				myCommandADD.Parameters.Add(prmaddress)

				Dim prmcity As New SqlParameter("@city", SqlDbType.VarChar, 30)
				prmcity.Value = bpo_cuscity.text
				myCommandADD.Parameters.Add(prmcity)

				Dim prmstate As New SqlParameter("@state", SqlDbType.VarChar, 2)
				prmstate.Value =bpo_cusstate.text
				myCommandADD.Parameters.Add(prmstate)

            Dim prmzip As New SqlParameter("@zip", SqlDbType.VarChar, 50)
				prmzip.Value = bpo_cuszip.text
				myCommandADD.Parameters.Add(prmzip)
				
				Dim prmcompany As New SqlParameter("@company", SqlDbType.VarChar, 30)
				prmcompany.Value = bpo_company.SelectedItem.Text
				myCommandADD.Parameters.Add(prmcompany)

				Dim prmphone As New SqlParameter("@rqstrphone", SqlDbType.VarChar, 15)
				prmphone.Value = bpo_phone.Text
				myCommandADD.Parameters.Add(prmphone)

				Dim prmfax As New SqlParameter("@rqstrfax", SqlDbType.VarChar, 15)
				prmfax.Value = bpo_fax.Text
				myCommandADD.Parameters.Add(prmfax)
	
				Dim prmcomp As New SqlParameter("@compensation", SqlDbType.decimal, 8)
				if bpo_comp.text = "" then 
					prmcomp.Value = DBNull.Value
				else
					prmcomp.Value = Convert.Todecimal(bpo_comp.text)
  				end if
  				myCommandADD.Parameters.Add(prmcomp)

				Dim prmagent As New SqlParameter("@agent", SqlDbType.VarChar, 30)
				prmagent.Value = bpo_preferagent.SelectedItem.Text
				myCommandADD.Parameters.Add(prmagent)

				Dim prmrush As New SqlParameter("@rush", SqlDbType.VarChar, 6)
				prmrush.Value = bpo_rush.SelectedItem.Text
				myCommandADD.Parameters.Add(prmrush)

				
				Dim prmneeddate As New SqlParameter("@needdate", SqlDbType.datetime)
				Dim supportedFormats() As String = New String() {"M/d/yyyy","MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
				if txtDate.text = "" then 
					prmneeddate.Value = DBNull.Value
				else	
					'prmneeddate.Value = Convert.ToDateTime(txtDate.text)
					prmneeddate.Value = DateTime.ParseExact(txtDate.text, supportedFormats, System.Globalization.CultureInfo.CurrentCulture, System.Globalization.DateTimeStyles.None)

				end if
				myCommandADD.Parameters.Add(prmneeddate)

				Dim prmcusname As New SqlParameter("@cusname", SqlDbType.VarChar, 30)
				prmcusname.Value = bpo_cusname.Text
				myCommandADD.Parameters.Add(prmcusname)
				
				Dim prmcusloanno As New SqlParameter("@cusloanno", SqlDbType.VarChar, 50)
				prmcusloanno.Value = bpo_cusloanno.Text
				myCommandADD.Parameters.Add(prmcusloanno)
				
				Dim prmoccupancy As New SqlParameter("@occupancy", SqlDbType.VarChar, 10)
				prmoccupancy.Value = bpo_occupancy.SelectedItem.Text
				myCommandADD.Parameters.Add(prmoccupancy)

				Dim prmhomephone As New SqlParameter("@homephone", SqlDbType.VarChar, 15)
				prmhomephone.Value = bpo_HomeOwnerphone.Text
				myCommandADD.Parameters.Add(prmhomephone)

				Dim prmtype As New SqlParameter("@type", SqlDbType.VarChar, 20)
				prmtype.Value = bpo_type.SelectedItem.Text
				myCommandADD.Parameters.Add(prmtype)

				Dim prmphotos As New SqlParameter("@photos", SqlDbType.VarChar, 6)
				prmphotos.Value = bpo_photos.SelectedItem.Text
				myCommandADD.Parameters.Add(prmphotos)
				
				Dim prmphotosint As New SqlParameter("@nointerior", SqlDbType.int)
				if bpo_nointerior.text = "" then 
					prmphotosint.Value = DBNull.Value
				else	
					prmphotosint.Value = bpo_nointerior.text
				end if
				myCommandADD.Parameters.Add(prmphotosint)
			
				Dim prmphotosext As New SqlParameter("@noexterior", SqlDbType.int)
				if bpo_noexterior.text = "" then 
					prmphotosext.Value = DBNull.Value
				else	
					prmphotosext.Value = bpo_noexterior.text
				end if
				myCommandADD.Parameters.Add(prmphotosext)
				
				Dim prmlisted As New SqlParameter("@listed", SqlDbType.VarChar, 10)
				prmlisted.Value = bpo_Listed.SelectedItem.Text
				myCommandADD.Parameters.Add(prmlisted)
				
				Dim prmlistagent As New SqlParameter("@listagent", SqlDbType.VarChar, 25)
				prmlistagent.Value = bpo_listagent.Text
				myCommandADD.Parameters.Add(prmlistagent)
				
				Dim prmlistagentphone As New SqlParameter("@listagentphone", SqlDbType.VarChar, 15)
				prmlistagentphone.Value = bpo_listagentphone.Text
				myCommandADD.Parameters.Add(prmlistagentphone)
				
				Dim prmintcontact As New SqlParameter("@intcontact", SqlDbType.VarChar, 30)
				prmintcontact.Value = bpo_incontact.Text
				myCommandADD.Parameters.Add(prmintcontact)
				
				Dim prmintcontactphone As New SqlParameter("@intcontactphone", SqlDbType.VarChar, 30)
				prmintcontactphone.Value = bpo_incontactphone.Text
				myCommandADD.Parameters.Add(prmintcontactphone)
				
				Dim prminstructions As New SqlParameter("@instructions", SqlDbType.text)
				prminstructions.Value = bpo_instructions.Text
				myCommandADD.Parameters.Add(prminstructions)
				
				Dim prmcuscontact As New SqlParameter("@cuscontact", SqlDbType.VarChar, 30)
				prmcuscontact.Value = bpo_cuscontact.Text
				myCommandADD.Parameters.Add(prmcuscontact)
				
				Dim prmcuscontactphone As New SqlParameter("@cuscontactphone", SqlDbType.VarChar, 15)
				prmcuscontactphone.Value = bpo_cuscontactphone.Text
				myCommandADD.Parameters.Add(prmcuscontactphone)
				
				Dim prmrqstercphone As New SqlParameter("@requestercellphone", SqlDbType.VarChar, 15)
				prmrqstercphone.Value = bpo_cphone.Text
				myCommandADD.Parameters.Add(prmrqstercphone)
				
				Dim prmrqsteremail As New SqlParameter("@requesteremail", SqlDbType.VarChar, 30)
				prmrqsteremail.Value = bpo_email.Text
				myCommandADD.Parameters.Add(prmrqsteremail)
				
				Dim prmbposource As New SqlParameter("@bposource", SqlDbType.VarChar, 50)
				prmbposource.Value = bpo_source.SelectedItem.Text
				myCommandADD.Parameters.Add(prmbposource)
				
				Dim prmcusorderno As New SqlParameter("@cusorderno", SqlDbType.VarChar, 20)
				prmcusorderno.Value = bpo_cusno.Text
				myCommandADD.Parameters.Add(prmcusorderno)
				
				Dim prmbpophotovendor As New SqlParameter("@bpophotovendor", SqlDbType.VarChar, 50)
				prmbpophotovendor.Value = bpo_photovendor.selecteditem.Text
				myCommandADD.Parameters.Add(prmbpophotovendor)
				
				Dim prmbpotime As New SqlParameter("@bpotime", SqlDbType.VarChar, 10)
				prmbpotime.Value = bpotime.Text
				myCommandADD.Parameters.Add(prmbpotime)
								
				Dim prmdisplayagent As New SqlParameter("@displayagent", SqlDbType.char,10)
				if bpo_preferagent.selecteditem.text = "Any" then
					prmdisplayagent.Value = "Y"
				else
					prmdisplayagent.Value = "N"
				end if
				myCommandADD.Parameters.Add(prmdisplayagent)
					
				Dim prmacceptedby As New SqlParameter("@acceptedby", SqlDbType.VarChar, 20)
				prmacceptedby.Value = pubuid
				myCommandADD.Parameters.Add(prmacceptedby)
			
				Dim prmassignedtoname As New SqlParameter("@assignedtoname", SqlDbType.VarChar, 50)
				prmassignedtoname.Value = pubuidfullname
				myCommandADD.Parameters.Add(prmassignedtoname)
							
				Dim prmpubphotosordered As New SqlParameter("@photosordered", SqlDbType.varchar, 6)
				prmpubphotosordered.Value = pubphotosordered
				myCommandADD.Parameters.Add(prmpubphotosordered)
										
            Try
                 myConnectionADD.Open()
                 myCommandADD.ExecuteNonQuery()
                 myConnectionADD.Close()
                 Catch SQLexc As SqlException
                        Response.Write("Update Failed. Error Details are: " & SQLexc.ToString())
                 End Try
         	
      end sub
  
	
 		Sub deletedb ()
      	dim strbpono as integer = Request.QueryString("id")
			
      	Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
         Dim myCommandADD As New SqlCommand("sp_deletebpo" , myConnectionADD)
         myCommandADD.CommandType = CommandType.StoredProcedure
            	   
         ' Add Parameters to SPROC
  
         	Dim prmtbl_bpo_pk As New SqlParameter("@tbl_bpo_pk", SqlDbType.int)
				prmtbl_bpo_pk.Value = strbpono
  				myCommandADD.Parameters.Add(prmtbl_bpo_pk)
         
            Try
                 myConnectionADD.Open()
                 myCommandADD.ExecuteNonQuery()
                 myConnectionADD.Close()
                 Catch SQLexc As SqlException
                        Response.Write("Insert Failed. Error Details are: " & SQLexc.ToString())
                 End Try
         


      end sub
	
  		
	    Sub BindGridDD()
					dim strbpono as integer = Request.QueryString("id")
					Dim strSql as String = "SELECT * from tbl_bpo where tbl_bpo_pk=" & strbpono
         		Dim sqlCmd As SqlCommand
	 		
				 	' Create Instance of Connection and Command Object
   	         Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
              	'Dim myCommand As New SqlCommand("Select * from dbo.tbl_bpo Where tbl_bpo_pk=" & strbpono, myConnection)
        			sqlCmd = New SqlCommand(strSql, myConnection)
              	
               Try
               	myConnection.Open()
                 	Dim Sqldr as SqlDataReader = sqlCmd.ExecuteReader
         				if Sqldr.read() then
         						bpo_number.text= Sqldr("tbl_bpo_pk")
         						bpo_status.SelectedItem.text = Sqldr("status")
         						pubstatus = Sqldr("status")
         						if Sqldr("requester") IsNot DBNull.Value  then
         							bpo_requester.text= Sqldr("requester")
         						end if
            				 	if Sqldr("company") IsNot dbnull.value then
            				 	   bpo_company.SelectedIndex = bpo_company.Items.IndexOf(bpo_company.Items.FindByText(Sqldr("company")))
   	       				 	end if
            				 	if Sqldr("reqphone") IsNot dbnull.value then
            				 	   bpo_phone.Text = Sqldr("reqphone")
            				 	end if
            				 	if Sqldr("reqfax") IsNot dbnull.value then
            				 	   bpo_fax.Text = Sqldr("reqfax")
            				 	end if
            				 	if Sqldr("reqcompensation") IsNot dbnull.value then
            				 	   bpo_comp.Text = Sqldr("reqcompensation")
            				 	end if
            				 	if Sqldr("preferredagent") IsNot dbnull.value then
            				 		bpo_preferagent.SelectedIndex = bpo_preferagent.Items.IndexOf(bpo_preferagent.Items.FindByText(Sqldr("preferredagent")))
   	       				 	end if
									if Sqldr("rushorder") IsNot dbnull.value then
            				 		bpo_rush.SelectedIndex = bpo_rush.Items.IndexOf(bpo_rush.Items.FindByText(Sqldr("rushorder")))
   	       				 	end if     
            					if Sqldr("needbydate") IsNot dbnull.value then
            					   txtdate.Text = Sqldr("needbydate")
            				 	end if
            					if Sqldr("reqemail") IsNot dbnull.value then
            					   bpo_email.Text = Sqldr("reqemail")
            				 	end if
   	        					if Sqldr("cusorderno") IsNot dbnull.value then
            					   bpo_cusno.Text = Sqldr("cusorderno")
            				 	end if
            				 	if Sqldr("reqcellphone") IsNot dbnull.value then
            					   bpo_cphone.Text = Sqldr("reqcellphone")
            				 	end if
            				 	if Sqldr("bposource") IsNot dbnull.value then
            				 	  bpo_source.SelectedIndex = bpo_source.Items.IndexOf(bpo_source.Items.FindByText(Sqldr("bposource")))
            				 	end if
            				 	if Sqldr("bpotime") IsNot dbnull.value then
            				 	   bpotime.Text = Sqldr("bpotime")
            				 	end if
            				 	if Sqldr("instructions") IsNot dbnull.value then
            				 	   bpo_instructions.Text = Sqldr("instructions")
            				 	end if
            				 	
            				 	if Sqldr("bankname") IsNot dbnull.value then
            				 	   bpo_cusname.Text = Sqldr("bankname")
            				 	end if
            				 	if Sqldr("proploanno") IsNot dbnull.value then
            				 	   bpo_cusloanno.Text = Sqldr("proploanno")
            				 	end if
            				 	if Sqldr("bankphone") IsNot dbnull.value then
            				 	   bpo_cuscontactphone.Text = Sqldr("bankphone")
            				 	end if
            				 	if Sqldr("bankcontact") IsNot dbnull.value then
            				 	   bpo_cuscontact.Text = Sqldr("bankphone")
            				 	end if
            				 	if Sqldr("propaddress") IsNot dbnull.value then
            				 	   bpo_cusaddress.Text = Sqldr("propaddress")
            				 	end if
            				 		if Sqldr("propcity") IsNot dbnull.value then
            				 	   bpo_cuscity.Text = Sqldr("propcity")
            				 	end if
            				 	if Sqldr("propstate") IsNot dbnull.value then
            				 	   bpo_cusstate.Text = Sqldr("propstate")
            				 	end if
            				 	if Sqldr("propzip") IsNot dbnull.value then
            				 	   bpo_cuszip.Text = Sqldr("propzip")
            				 	end if
            				 	if Sqldr("occupancy") IsNot dbnull.value then
            				 		bpo_occupancy.SelectedIndex = bpo_occupancy.Items.IndexOf(bpo_occupancy.Items.FindByText(Sqldr("occupancy")))
              				 	end if
            				 	if Sqldr("homephone") IsNot dbnull.value then
            				 	   bpo_HomeOwnerphone.Text = Sqldr("homephone")
            				 	end if
            					if Sqldr("bpotype") IsNot dbnull.value then
            				 	   bpo_type.SelectedIndex = bpo_type.Items.IndexOf(bpo_type.Items.FindByText(Sqldr("bpotype")))
              				 	end if
            				 	if Sqldr("photos") IsNot dbnull.value then
            				 	  bpo_photos.SelectedIndex = bpo_photos.Items.IndexOf(bpo_photos.Items.FindByText(Sqldr("photos")))
									end if
								 	
            				 	if Sqldr("bpophotovendor") IsNot dbnull.value then
            				 	    bpo_photovendor.SelectedIndex = bpo_photovendor.Items.IndexOf(bpo_photovendor.Items.FindByText(Sqldr("bpophotovendor")))
            				 	end if
            				 
            				 	if Sqldr("listed") IsNot dbnull.value then
            				 	 bpo_Listed.SelectedIndex = bpo_Listed.Items.IndexOf(bpo_Listed.Items.FindByText(Sqldr("listed")))  
            				 	end if
            				 	if Sqldr("nointerior") IsNot dbnull.value then
            				 	   bpo_nointerior.Text = Sqldr("nointerior")
            				 	end if
            				 	if Sqldr("noexterior") IsNot dbnull.value then
            				 	   bpo_noexterior.Text = Sqldr("noexterior")
            				 	end if
            				 	if Sqldr("listagent") IsNot dbnull.value then
            				 	   bpo_listagent.Text = Sqldr("listagent")
            				 	end if
            				 	if Sqldr("listagentphone") IsNot dbnull.value then
            				 	   bpo_listagentphone.Text = Sqldr("listagentphone")
            				 	end if
            				 	if Sqldr("interiorcontact") IsNot dbnull.value then
            				 	   bpo_incontact.Text = Sqldr("interiorcontact")
            				 	end if
            				 	if Sqldr("interiorcontactphone") IsNot dbnull.value then
            				 	   bpo_incontactphone.Text = Sqldr("interiorcontactphone")
            				 	end if
            				 	if Sqldr("bpoacceptedby") IsNot dbnull.value then
            				 	   pubuid = Sqldr("bpoacceptedby")
            				 	end if
            				 	if Sqldr("rqstruid") IsNot dbnull.value then
            				 	   pubrequid = Sqldr("rqstruid")
            				 	end if     
            				 	if Sqldr("assignedtoname") IsNot dbnull.value then
            				 	   pubuidfullname = Sqldr("assignedtoname")
            				 	end if     
            				 	if Sqldr("photosordered") IsNot dbnull.value then
            				 	   pubphotosordered = Sqldr("photosordered")
            				 	else
            				 		pubphotosordered = "No"
            				 	end if     
          						
   					end if
      				Catch SQLexc As SqlException
                       Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
               	finally
               		myConnection.close()
               End Try
            	   
		   End Sub
     	
 	  
		Sub FillcompanyDropDown()
					Dim strUID as String = session("userid")
             	' Create Instance of Connection and Command Object
   	         Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
              	Dim myCommand As New SqlCommand("Select users_tbl_Pk ,fname + ' ' + lname as agentname from dbo.tbl_users Where type='Agent' or type='Partner'" , myConnection)
               	
               Try
               	myConnection.Open()
                  bpo_preferagentA.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
                  bpo_preferagentA.DataBind()
                  Catch SQLexc As SqlException
                       Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
               End Try
   	End Sub

     Private Sub BindPreferAgent()
			      ' Create Instance of Connection and Command Object
   	         Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
              	Dim myCommand As New SqlCommand("Select users_tbl_Pk ,fname + ' ' + lname as agentname from dbo.tbl_users Where type='Agent' or type='Partner'" , myConnection)
               	
               Try
               	myConnection.Open()
                  bpo_preferagent.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
                  bpo_preferagent.DataBind()
                  Catch SQLexc As SqlException
                       Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
               End Try
		        'response.write(bpo_preferagent.Items.Count) 
					bpo_preferagent.Items.Insert(0, new ListItem("Any"))
      	    	

     	End Sub
    
     Private Sub bindBPOStatus()
			    	' Create Instance of Connection and Command Object
   	         Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
              	Dim myCommand As New SqlCommand("Select tbl_bpostatus_PK, bpostatus from dbo.tbl_bpostatus" , myConnection)
               	
               Try
               	myConnection.Open()
                  bpo_status.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
                  bpo_status.DataBind()
                  Catch SQLexc As SqlException
                       Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
               End Try
     	End Sub
	
	sub setbkgcolor(varcolor as System.Drawing.Color)
		bpo_number.backcolor=lightgray
		bpo_status.backcolor=lightgray
		bpo_cusno.backcolor=varcolor
 		bpo_cphone.backcolor=varcolor
 		bpo_email.backcolor=varcolor
 		bpo_source.backcolor=varcolor
 		bpo_requester.backcolor=varcolor
	   bpo_phone.backcolor=varcolor
	   bpo_fax.backcolor=varcolor
	   bpo_company.backcolor=varcolor
	   bpo_cusaddress.backcolor=varcolor
	   bpo_cuscity.backcolor=varcolor
	   bpo_cusstate.backcolor=varcolor
	   bpo_cuszip.backcolor=varcolor
	   bpo_cusloanno.backcolor=varcolor
	   bpo_cuscontact.backcolor=varcolor
	   bpo_comp.backcolor=varcolor
	   bpo_rush.backcolor=varcolor
	   txtDate.backcolor=varcolor
	   bpo_cusname.backcolor=varcolor
	   bpo_HomeOwnerphone.backcolor=varcolor
	   bpo_type.backcolor=varcolor
	   bpo_photos.backcolor=varcolor
	   bpo_occupancy.backcolor=varcolor
	   bpo_cuscontactphone.backcolor=varcolor
	   bpo_noexterior.backcolor=varcolor
	   bpo_nointerior.backcolor=varcolor
	   bpo_listagent.backcolor=varcolor
	   bpo_Listed.backcolor=varcolor
	   bpo_listagentphone.backcolor=varcolor
	   bpo_incontact.backcolor=varcolor
	   bpo_incontactphone.backcolor=varcolor
	   bpo_instructions.backcolor=varcolor
	  	bpo_preferagent.backcolor=varcolor
	  	bpo_cusno.backcolor=varcolor
		bpo_photovendor.backcolor=varcolor
		bpotime.backcolor=varcolor

	end sub
	
	sub setfieldstatus(stat as boolean) 		
 		bpo_number.readonly=true
	 	bpo_status.enabled=false	 		
 		bpo_cusno.readonly=stat
 		bpo_cphone.readonly=stat
 		bpo_email.readonly=stat
 		bpo_requester.readonly=stat
 		bpo_phone.readonly=stat
	   bpo_fax.readonly=stat
	   if stat = true then 
	   	bpo_company.Enabled =false
		   bpo_rush.Enabled =false
   		bpo_type.Enabled =false
	   	bpo_photos.Enabled =false
	   	bpo_occupancy.Enabled =false
		   bpo_Listed.Enabled=false
		  	bpo_preferagent.Enabled =false
		  	bpo_source.Enabled =false
		  	bpo_photovendor.Enabled =false
	   else
	   	bpo_company.Enabled =true
	   	bpo_rush.Enabled =true
   		bpo_type.Enabled =true
	   	bpo_photos.Enabled =true
	   	bpo_occupancy.Enabled =true
		   bpo_Listed.Enabled=true
		  	bpo_preferagent.Enabled =true
		  	bpo_source.Enabled =true
		  	bpo_photovendor.Enabled =true
		  	
	   end if	
	   bpo_cusaddress.readonly=stat
	   bpo_cuscity.readonly=stat
	   bpo_cusstate.readonly=stat
	   bpo_cuszip.readonly=stat
	   bpo_cusloanno.readonly=stat
	   bpo_cuscontact.readonly=stat
	   bpo_comp.readonly=stat
	   txtDate.readonly=stat
	   bpo_cusname.readonly=stat
	   bpo_HomeOwnerphone.readonly=stat
	   bpo_cuscontactphone.readonly=stat
	   bpo_noexterior.readonly=stat
	   bpo_nointerior.readonly=stat
	   bpo_listagent.readonly=stat
	   bpo_listagentphone.readonly=stat
	   bpo_incontact.readonly=stat
	   bpo_incontactphone.readonly=stat
	   bpo_instructions.readonly=stat
	 	bpotime.readonly=stat
	end sub  

  	sub initprocess()
  				pubphotosordered="No"
  				pubuidfullname= ""
				
  				'Bind all Data
  	 			BindPreferAgent()
   			
  	 			bindBPOStatus()
	 			BindCompany()
	 			bindbposource()
	    		bindphotovendor()
	    		BindGridDD()
   			
   			'Set Display
				setfieldstatus(true)
				setbkgcolor(lightgray)
    			pnlheaderview.visible=true
    			pnlviewbpo.visible=true
    			bpo_savecontinue.visible=false
    			bpo_delete.visible=false
	 			pnlbposavewarn.visible=false
	 			pnlack.visible=false
	 			bpo_savelater.visible=false
	 			pnlheaderconfirm.visible=false
	 			pnlchk2submit.visible=false
	 			bpo_submit.visible=false
	  			bpo_acceptbpo.visible=false
	  			bpo_edit.visible=false
				bpo_cancel.visible=false
				bpo_closebpo.visible=false
				pnlacceptwarn.visible=false	
				
				
	  			if session("sysaccess")="Partner" then
	  				pubaccess =  "Partner"
	  			else if session("sysaccess")="Agent"
					pubaccess =  "Agent"
				else if session("sysaccess")="Broker"
					pubaccess =  "Broker"
					
				end if
		
	end sub
	
	sub page1setup()		
			bpo_cancel.visible=true
			
			if pubaccess =  "Partner" or pubaccess =  "Broker" then
				if pubstatus = "Draft" then
						bpo_edit.visible=true
						bpo_delete.visible=true		
				else if pubstatus = "Submitted-Unassigned"
						bpo_edit.visible=false
						bpo_delete.visible=false		
				end if	
			else if pubaccess =  "Agent" then
				if pubstatus = "Submitted-Unassigned" then
						bpo_acceptbpo.visible=true		
						pnlacceptbpo.visible = true
				else
						
						bpo_acceptbpo.visible=false		
						pnlacceptbpo.visible = false
						bpo_edit.visible=true
				end if				
			end if
	   		
	   end sub

	sub getagentfullname()
		Dim strSql as String = "SELECT fname + ' ' + lname as 'agentname' from tbl_users where UID='" & pubuid & "'"
   	Dim sqlCmd As SqlCommand
   	Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
      sqlCmd = New SqlCommand(strSql, myConnection)
      
      	Try
         	myConnection.Open()
            Dim Sqldr as SqlDataReader = sqlCmd.ExecuteReader
         	if Sqldr.read() then
   				pubuidfullname = Sqldr("agentname")
     
   			end if
   			Catch SQLexc As SqlException
      	      Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
           	finally
           		myConnection.close()
         	end Try
    
	end sub



 private sub sendemailagents()
 		notificationemail = bpo_email.text
 		Dim mail As New MailMessage()
  		dim bpoprefered as string
  		
  		'Set the properties - send the email to the person who filled out the
  		mail.From = New MailAddress("mychoice@gochoiceone.com")
  		mail.To.Add("BPONotifications@gochoiceone.com")
  
 		if bpo_preferagent.selecteditem.text = "Any"
  			mail.Subject = "BPO Submission Notification-Open to all"
		else
			mail.Subject = "BPO Submission Notification-Preferred Agent"
		end if
 
 		'Set the body
 		if bpo_preferagent.selecteditem.text = "Any"
  						bpoprefered = "There is not a preferred Agent and this BPO is open for all agents."        
               else
               	bpoprefered = "There is a preferred Agent for this BPO.  IF that Agent does not accept within 30 minutes then it will become available to all Agents."
              	end if
  		mail.Body = "At " + DateTime.Now + " a  BPO request was submitted to the Choice One " & _
               "Web site. " & bpoprefered & vbCrLf & vbCrLf & _
      	      "Below you will find the details for the message. " & _
 					vbCrLf & _
               "____________________________________________________" & vbCrLf & vbCrLf & _
               "BPO Information" & vbcrlf & _
               "-------------------" & vbcrlf & _
               "Requestor:    " & bpo_requester.text & vbcrlf & _
               "Address: " &  bpo_cusaddress.text & vbcrlf 
  
 	 	'send the message
  		Dim smtp As New SmtpClient("smtp.comcast.net")
  		smtp.Send(mail)
  		
  	end sub	
  	
	
 private sub sendemailphotovendor()
 		notificationemail = bpo_email.text
 		Dim mail As New MailMessage()
  		dim bpoprefered as string
  		
  		'Set the properties - send the email to the person who filled out the
  		mail.From = New MailAddress("mychoice@gochoiceone.com")
  		mail.To.Add("bbialas@gochoiceone.com")
  
  		mail.Subject = "BPO Submission Photos Needed"
	
 		'Set the body
  		mail.Body = "At " + DateTime.Now + " PHOTOS NEEDED!!!!! " & _
               "Web site. " &  vbCrLf & vbCrLf & _
      	      "Below you will find the details for the message. " & _
 					vbCrLf & _
               "____________________________________________________" & vbCrLf & vbCrLf & _
               "BPO Information" & vbcrlf & _
               "-------------------" & vbcrlf & _
               "Requestor:    " & bpo_requester.text & vbcrlf & _
               "Address: " &  bpo_cusaddress.text & vbcrlf 
  
 	 	'send the message
  		Dim smtp As New SmtpClient("smtp.comcast.net")
  		smtp.Send(mail)
  		
  	end sub	

 	
 private sub sendemailRequester()
 		notificationemail = bpo_email.text
 		Dim mail As New MailMessage()
  		
  		'Set the properties - send the email to the person who filled out the
  		mail.From = New MailAddress("mychoice@gochoiceone.com")
  		mail.To.Add(notificationemail)

		mail.Subject = "BPO Submission Notification"

  		'Set the body
  		mail.Body = "At " + DateTime.Now + " a  BPO request was submitted to the Choice One " & _
               "Web site.  Below you will find the details for the message. " & _
 					vbCrLf & _
               "____________________________________________________" & vbCrLf & vbCrLf & _
               "BPO Information" & vbcrlf & _
               "-------------------" & vbcrlf & _
               "Requestor:    " & bpo_requester.text & vbcrlf & _
               "Address: " &  bpo_cusaddress.text & vbcrlf 
  
 	 	'send the message
  		Dim smtp As New SmtpClient("smtp.comcast.net")
  		smtp.Send(mail)
  		
  	end sub	

  	
	end class
		
end namespace