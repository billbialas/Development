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
imports System.Data.SqlClient
imports System.xml
imports System.Configuration


namespace PageTemplate
	public class leadprofile 
	   inherits PageTemplate
	   
	    public chkbed,chkbath,chklevel
	    public lstproperties
	    public lstcities,lstAllcities as listbox
	    public pnlallcities,pnlprofileall,pnlmatch
	    'public ctysave
	    public txt_lp_notes,txt_lp_movedate,txt_lp_rentmin,txt_lp_rentmax
	    public lblprofileNo,lblleadno
	    public dd_lp_status,dd_lp_proptype, dd_lp_bedrooms, dd_lp_baths, dd_lp_levels, dd_lp_basement
	    public dd_lp_pets,dd_lp_fence,dd_lp_county,dd_lp_schooldist,dd_lp_credit,dd_lp_schooldist2,dd_lp_sec8
	    public bt_save,bt_savemore,bt_exit,bt_match
	    private shared newprofileno as integer=0
	    private shared storedcounty as string
	   	
	   private Sub Page_Load(byval sender as object, byval e As EventArgs) handles mybase.load
				Pagesetup()
				
				if  not (Page.IsPostBack) then
					
					SessionInit()
					dobindings() 
					
					if Request.QueryString("action")= "view" then
						bindprofile()
						bindcities()
					else 	if Request.QueryString("action")= "print"
						bindprofile()
						bindcities()
						bt_save.visible=false
						bt_savemore.visible=false
						bt_exit.visible=false
						bt_match.visible=false
					end if
		   	end if
		
	  end sub

	sub SessionInit()
		lblprofileNo.text =Request.QueryString("profile")
		lblleadno.text =Request.QueryString("LeadNo")
		if Request.QueryString("action")= "view" then
			pnlallcities.visible=false
			'ctysave.visible=false
		else 
			pnlallcities.visible=true
			'ctysave.visible=true
			
		end if
		pnlprofileall.visible=true
  	pnlmatch.visible=false
	end sub 
	
	sub dobindings()
		bindpropertytype()
		bindbedrooms()
		bindbaths()
		bindlevels()
		bindbasment()
		bindpets()
		bindfence()
		bindcounty()
		bindschooldist()
		bindschooldist2()
		bindcredit()
		bindallcities()
		
	end sub	   
 	  
  sub btn_saveprofile(sender As Object, e As EventArgs)
  	 if Request.QueryString("action")= "new" then
	  	 insertdb ()
	  	 if lstcities.items.count > 0 then
	  	   savecities()
	  	 else 
	  	 '	newprofileno=0
	  	 '	getprofileno()
	  	 end if	
	  	 'response.redirect("leadprofile.aspx?profile=" & newprofileno & "&LeadNo=" & Request.QueryString("leadno") & "&LeadType=" & Request.QueryString("leadtype") & "&action=view")
	else
		insertdb ()
	  	if lstcities.items.count > 0 then
	  	   savecities()
	  	 end if	
	  	 bindprofile()
	end if
 	response.redirect("addlead.aspx?action=view&id=" & Request.QueryString("LeadNo")& "&source=profile")
  end sub
  
    
  sub btn_saveprofileadd(sender As Object, e As EventArgs)
  	 if Request.QueryString("action")= "new" then
	  	 insertdb ()
	  	 if lstcities.items.count > 0 then
	  	   savecities()
	  	 else 
	  	 	'newprofileno=0
	  	 	'getprofileno()
	  	 end if	
	else
		insertdb ()
	  	if lstcities.items.count > 0 then
	  	   savecities()
	  	 end if	
	end if
  	response.redirect("leadprofile.aspx?profile=0&LeadNo=" & Request.QueryString("leadno") & "&LeadType=" & Request.QueryString("leadtype") & "&action=new")

  end sub
  
  
  sub btn_exit(sender As Object, e As EventArgs)
  	response.redirect("addlead.aspx?action=view&id=" & Request.QueryString("LeadNo")& "&source=profile")
  end sub
  
  sub btn_match(sender As Object, e As EventArgs)
  	pnlprofileall.visible=false
  	pnlmatch.visible=true

  end sub
  
  sub btn_Search(sender As Object, e As EventArgs)

	searchprops()

  end sub

	sub searchprops()  
  		Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
      Dim strSql as String
		strSql="select * from dbo.tbl_lead_profile join tbl_leads on tbl_leads_pk = tbl_leads_fk where ld_type='Landlord' and "
   	if chkbed.checked then
   	 	if dd_lp_bedrooms.selecteditem.text = "At least 1" then
   	 		strSql= strSql + "right(lp_num_bed,1) = 1 " 
			else if dd_lp_bedrooms.selecteditem.text = "At least 2" then
		 		strSql= strSql + "right(lp_num_bed,1) <= 2 " 
			else if dd_lp_bedrooms.selecteditem.text = "At least 3" then 	   
  	 			strSql= strSql + "right(lp_num_bed,1) <= 3 "
			else if dd_lp_bedrooms.selecteditem.text = "At least 4" then 	   
  	 			strSql= strSql + "right(lp_num_bed,1) <= 4 "
  	 		end if	 
	 	end if
	 	
	 	if chkbath.checked then
	 		if chkbed.checked then
	 			strSql= strSql + "and "
	 		end if
	 	 	if dd_lp_baths.selecteditem.text = "At least 1" then
   	 		strSql= strSql + "right(lp_num_bath,1) = 1 " 
			else if dd_lp_baths.selecteditem.text = "At least 2" then
		 		strSql= strSql + "right(lp_num_bath,1) <= 2 " 
			else if dd_lp_baths.selecteditem.text = "At least 3" then 	   
  	 			strSql= strSql + "right(lp_num_bath,1) <= 3 "
			else if dd_lp_baths.selecteditem.text = "At least 4" then 	   
  	 			strSql= strSql + "right(lp_num_bath,1) <= 4 "
  	 		end if	 
  	 	end if
	 	
	 	if chklevel.checked then
   		if chkbed.checked or chkbath.checked then
	 			strSql= strSql + "and "
	 		end if
	 		strSql= strSql + "lp_levels ='" & dd_lp_levels.selecteditem.text & "'"
	 	end if 
	 	
   
      Dim sqlCmd As New SqlCommand
      
      sqlCmd = New SqlCommand(strSql, myConnection)
      
      response.write(strSql)
        Try
         	myConnection.Open()
            lstproperties.DataSource = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection)
            lstproperties.DataBind()
           Catch SQLexc As SqlException
                 Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
         End Try
	end sub







  
  
  
 sub btn_cityedit(sender As Object, e As EventArgs)
  	pnlallcities.visible=true
  	'ctysave.visible=true
  	bindAllcities()
 end sub
 
 sub btn_citysave(sender As Object, e As EventArgs)
 	 savecities()
 end sub
 
 
  sub btn_cityremove(sender As Object, e As EventArgs)
		if lstcities.SelectedItem isnot nothing	then
  		 	Dim i As Integer 
		 	i=0
		 	while lstcities.SelectedItem isnot nothing
				lstcities.Items.Remove(lstcities.SelectedItem)
			end while
		end if
			  
	end sub
 
 
 	sub btn_cityadd(sender As Object, e As EventArgs)
  		Dim i As Integer 
		for i=0 to lstAllcities.items.count -1 
			if lstAllcities.items(i).selected then
				'Response.write(lstAllcities.items(i))
				lstcities.Items.Add(lstAllcities.items(i))

			end if
		next
  	
 	end sub
 
  sub bindcities()
  		Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
      Dim strSql as String
      if Request.QueryString("action")= "new" then 
      	strSql="Select tbl_leadprofilecities_pk, lpc_city_name from dbo.tbl_leadprofilecities where tbl_leadprofile_fk=" & newprofileno 
       else
      	strSql="Select tbl_leadprofilecities_pk, lpc_city_name from dbo.tbl_leadprofilecities where tbl_leadprofile_fk=" & Request.QueryString("profile")
      end if 
      Dim sqlCmd As New SqlCommand
      
      sqlCmd = New SqlCommand(strSql, myConnection)
      
        Try
         	myConnection.Open()
            lstcities.DataSource = sqlCmd.ExecuteReader(CommandBehavior.CloseConnection)
            lstcities.DataBind()
            Catch SQLexc As SqlException
                 Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
         End Try
  end sub
  
  sub bindAllcities()
  		Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
      Dim myCommand As New SqlCommand("Select cty_idx_cid, cty_city_name from dbo.tbl_idx_cities where cty_county='" & dd_lp_county.SelectedItem.Text & "'" , myConnection)
      'Dim myCommand As New SqlCommand("Select cty_idx_cid, cty_city_name from dbo.tbl_idx_cities" , myConnection)
           	
        Try
         	myConnection.Open()
            lstAllcities.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
            lstAllcities.DataBind()
            Catch SQLexc As SqlException
                 Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
         End Try
  end sub
	
	sub bindpropertytype()  
		 Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
		 Dim myCommand As New SqlCommand("Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='lpproptype' order by x_descr ", myConnection)
		               	
		               Try
		               	myConnection.Open()
		                  dd_lp_proptype.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
		                  dd_lp_proptype.DataBind()
		                  Catch SQLexc As SqlException
		                       Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
		               End Try
	end sub
	
	
	sub bindbedrooms() 
		 Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
		 Dim myCommand As New SqlCommand("Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='lpbedrooms' order by x_descr ", myConnection)
		               	
		               Try
		               	myConnection.Open()
		                  dd_lp_bedrooms.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
		                  dd_lp_bedrooms.DataBind()
		                  Catch SQLexc As SqlException
		                       Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
		               End Try
	end sub	
				
	
	sub bindbaths()
		 Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
		 Dim myCommand As New SqlCommand("Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='lpbaths' order by x_descr ", myConnection)
		               	
		               Try
		               	myConnection.Open()
		                  dd_lp_baths.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
		                  dd_lp_baths.DataBind()
		                  Catch SQLexc As SqlException
		                       Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
		               End Try
	end sub					
	
	sub bindlevels()
		 Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
		 Dim myCommand As New SqlCommand("Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='lplevels' order by x_descr ", myConnection)
		               	
		               Try
		               	myConnection.Open()
		                  dd_lp_levels.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
		                  dd_lp_levels.DataBind()
		                  Catch SQLexc As SqlException
		                       Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
		               End Try
	end sub									
			
	sub bindbasment()
		 Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
		 Dim myCommand As New SqlCommand("Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='lpgeneric' order by x_descr ", myConnection)
		               	
		               Try
		               	myConnection.Open()
		                  dd_lp_basement.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
		                  dd_lp_basement.DataBind()
		                  Catch SQLexc As SqlException
		                       Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
		               End Try
	end sub						
	
	sub bindpets()
		 Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
		 Dim myCommand As New SqlCommand("Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='lpgeneric' order by x_descr ", myConnection)
		               	
		               Try
		               	myConnection.Open()
		                  dd_lp_pets.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
		                  dd_lp_pets.DataBind()
		                  Catch SQLexc As SqlException
		                       Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
		               End Try
	end sub						

	sub bindfence()
		 Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
		 Dim myCommand As New SqlCommand("Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='lpgeneric' order by x_descr ", myConnection)
		               	
		               Try
		               	myConnection.Open()
		                  dd_lp_fence.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
		                  dd_lp_fence.DataBind()
		                  Catch SQLexc As SqlException
		                       Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
		               End Try
	end sub			
		
	sub bindcounty()
		 Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
		 Dim myCommand As New SqlCommand("Select tbl_county_pk, cnty_name from dbo.tbl_counties order by cnty_name ", myConnection)
		               	
		               Try
		               	myConnection.Open()
		                  dd_lp_county.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
		                  dd_lp_county.DataBind()
		                  Catch SQLexc As SqlException
		                       Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
		               End Try
		if Request.QueryString("action")= "new" then 
			dd_lp_county.SelectedIndex = dd_lp_county.Items.IndexOf(dd_lp_county.Items.FindByText("Macomb"))
      else
      	dd_lp_county.SelectedIndex = dd_lp_county.Items.IndexOf(dd_lp_county.Items.FindByText(storedcounty))
      end if 		
	end sub			
		
	sub bindschooldist()
		 Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
		 Dim myCommand As New SqlCommand("select agency_name  from tbl_schools where county_name = '" &   dd_lp_county.SelectedItem.Text  & "' group by agency_name order by agency_name", myConnection)
		               	
		               Try
		               	myConnection.Open()
		                  dd_lp_schooldist.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
		                  dd_lp_schooldist.DataBind()
		                  Catch SQLexc As SqlException
		                       Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
		               End Try
		               
		 dd_lp_schooldist.Items.Insert(0, new ListItem("None","9999"))
	end sub			
	
	sub bindschooldist2()
		 Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
		 Dim myCommand As New SqlCommand("select agency_name  from tbl_schools where county_name = '" &   dd_lp_county.SelectedItem.Text  & "' group by agency_name order by agency_name", myConnection)
	               	
		               Try
		               	myConnection.Open()
		                  dd_lp_schooldist2.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
		                  dd_lp_schooldist2.DataBind()
		                  Catch SQLexc As SqlException
		                       Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
		               End Try
		               
		 dd_lp_schooldist2.Items.Insert(0, new ListItem("None","9999"))
	end sub			
	
	sub bindcredit()
		 	Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
		 	Dim myCommand As New SqlCommand("Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='lpcredit' order by x_descr ", myConnection)
		             	
		               Try
		               	myConnection.Open()
		                  dd_lp_credit.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
		                  dd_lp_credit.DataBind()
		                  Catch SQLexc As SqlException
		                       Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
		               End Try
	end sub			
	
	
 	Sub bindprofile()
 					Dim strUID as String = session("userid")
  					Dim strSql as String = "SELECT * from tbl_lead_profile where tbl_lead_profile_pk="& Request.QueryString("profile")  
         		Dim sqlCmd As SqlCommand
	 		
			      Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
        			sqlCmd = New SqlCommand(strSql, myConnection)
              	
               Try
               	myConnection.Open()
                 	Dim Sqldr as SqlDataReader = sqlCmd.ExecuteReader
         				if Sqldr.read() then
         				
         					if Sqldr("lp_status") IsNot DBNull.Value  then
         						dd_lp_status.SelectedIndex = dd_lp_status.Items.IndexOf(dd_lp_status.Items.FindByText(Sqldr("lp_status")))
   	       				end if
         					if Sqldr("lp_credit_rate") IsNot DBNull.Value  then
         						dd_lp_credit.SelectedIndex = dd_lp_credit.Items.IndexOf(dd_lp_credit.Items.FindByText(Sqldr("lp_credit_rate")))
         					end if
         					if Sqldr("lp_move_in_dt") IsNot dbnull.value then
            				 	   txt_lp_movedate.Text = Sqldr("lp_move_in_dt")
            				end if
            				if Sqldr("lp_rent_amt_min") IsNot dbnull.value then
            				 	   txt_lp_rentmin.Text = Sqldr("lp_rent_amt_min")
            				end if
            				if Sqldr("lp_rent_amt_max") IsNot dbnull.value then
            				 	   txt_lp_rentmax.Text = Sqldr("lp_rent_amt_max")
            				end if
            				if Sqldr("lp_proptype") IsNot DBNull.Value  then
         						dd_lp_proptype.SelectedIndex = dd_lp_proptype.Items.IndexOf(dd_lp_proptype.Items.FindByText(Sqldr("lp_proptype")))
         					end if
         					if Sqldr("lp_num_bed") IsNot DBNull.Value  then
         						dd_lp_bedrooms.SelectedIndex = dd_lp_bedrooms.Items.IndexOf(dd_lp_bedrooms.Items.FindByText(Sqldr("lp_num_bed")))
         					end if
         					if Sqldr("lp_num_bath") IsNot DBNull.Value  then
         						dd_lp_baths.SelectedIndex = dd_lp_baths.Items.IndexOf(dd_lp_baths.Items.FindByText(Sqldr("lp_num_bath")))
         					end if
         					if Sqldr("lp_levels") IsNot DBNull.Value  then
         						dd_lp_levels.SelectedIndex = dd_lp_levels.Items.IndexOf(dd_lp_levels.Items.FindByText(Sqldr("lp_levels")))
         					end if
         					if Sqldr("lp_basement") IsNot DBNull.Value  then
         						dd_lp_basement.SelectedIndex = dd_lp_basement.Items.IndexOf(dd_lp_basement.Items.FindByText(Sqldr("lp_basement")))
         					end if
         					if Sqldr("lp_pets") IsNot DBNull.Value  then
         						dd_lp_pets.SelectedIndex = dd_lp_pets.Items.IndexOf(dd_lp_pets.Items.FindByText(Sqldr("lp_pets")))
         					end if
         					if Sqldr("lp_fence") IsNot DBNull.Value  then
         						dd_lp_fence.SelectedIndex = dd_lp_fence.Items.IndexOf(dd_lp_fence.Items.FindByText(Sqldr("lp_fence")))
         					end if
         					if Sqldr("lp_county") IsNot DBNull.Value  then
         						dd_lp_county.SelectedIndex = dd_lp_county.Items.IndexOf(dd_lp_county.Items.FindByText(Sqldr("lp_county")))
         						storedcounty = dd_lp_county.SelectedItem.text
         					end if
         					if Sqldr("lp_school1") IsNot DBNull.Value  then
         						dd_lp_schooldist.SelectedIndex = dd_lp_schooldist.Items.IndexOf(dd_lp_schooldist.Items.FindByText(Sqldr("lp_school1")))
         					else
         						dd_lp_schooldist.SelectedIndex = dd_lp_schooldist.Items.IndexOf(dd_lp_schooldist.Items.FindByText("None"))
         					end if
         					if Sqldr("lp_school2") IsNot DBNull.Value  then
         						dd_lp_schooldist2.SelectedIndex = dd_lp_schooldist2.Items.IndexOf(dd_lp_schooldist2.Items.FindByText(Sqldr("lp_school2")))
         					else
         						dd_lp_schooldist2.SelectedIndex = dd_lp_schooldist2.Items.IndexOf(dd_lp_schooldist2.Items.FindByText("None"))
         					end if
         					if Sqldr("lp_note") IsNot dbnull.value then
            				 	   txt_lp_notes.Text = Sqldr("lp_note")
            				end if
         					if Sqldr("lp_sec8") IsNot dbnull.value then
            				 	 dd_lp_sec8.SelectedIndex = dd_lp_sec8.Items.IndexOf(dd_lp_sec8.Items.FindByText(Sqldr("lp_sec8")))	
            				end if
            			end if
      				Catch SQLexc As SqlException
                       Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
               	finally
               		myConnection.close()
               End Try
		   End Sub
	
	
  	Sub insertdb ()
   		Dim rightNow as DateTime = DateTime.Now.toShortDateString() 
  			Dim RightNowAdd as datetime = datetime.now
   		Dim supportedFormats() As String = New String() {"M/dd/yyyy","M/d/yyyy","MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
		   Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
  			dim sqlproc as string
  		  
  			if Request.QueryString("action")= "new" then
  				sqlproc = "sp_addleadprofile"
   	   else if Request.QueryString("action")= "view" then
          	sqlproc = "sp_updateleadprofile"
  			end if
  		 	
  	     	Dim myCommandADD As New SqlCommand( sqlproc, myConnectionADD)
  		 	myCommandADD.CommandType = CommandType.StoredProcedure
    	    
         ' Add Parameters to SPROC
  			if Request.QueryString("action")= "new" then
         	        
         	Dim prmuid As New SqlParameter("@Adduid", SqlDbType.VarChar, 50)
				prmuid.Value = session("userid")
				myCommandADD.Parameters.Add(prmuid)
				
				Dim prmleadno As New SqlParameter("@LeadNo", SqlDbType.int)
				prmleadno.Value = Request.QueryString("LeadNo")
  				myCommandADD.Parameters.Add(prmleadno)
  				  				  				
  				Dim prmprofilepk As New SqlParameter("@lp_leadprofile_pk", SqlDbType.int)
				prmprofilepk.Value = getleadprofilepk()
  				myCommandADD.Parameters.Add(prmprofilepk)
  				
  				updateprofilepk(prmprofilepk.Value)       				
         else 
  				Dim prmprofileno As New SqlParameter("@profileno", SqlDbType.int)
				prmprofileno.Value = Request.QueryString("profile")
  				myCommandADD.Parameters.Add(prmprofileno)
  				
  				Dim prmUPuid As New SqlParameter("@Updateuid", SqlDbType.VarChar, 50)
				prmUPuid.Value = session("userid")
				myCommandADD.Parameters.Add(prmUPuid)
         end if
  			
  			dim prmstatus As New SqlParameter("@lp_status", SqlDbType.VarChar, 50)
			prmstatus.Value = dd_lp_status.SelectedItem.Text
			myCommandADD.Parameters.Add(prmstatus)
  			
  			dim prmcredit As New SqlParameter("@lp_credit", SqlDbType.VarChar, 50)
			prmcredit.Value = dd_lp_credit.SelectedItem.Text
			myCommandADD.Parameters.Add(prmcredit)
  			
  			Dim prmmovedate As New SqlParameter("@lp_movedate", SqlDbType.datetime)
			if txt_lp_movedate.Text = "" then 
				prmmovedate.Value = DBNull.Value
			else
				prmmovedate.Value = txt_lp_movedate.Text
			end if
			myCommandADD.Parameters.Add(prmmovedate)
		
			Dim prmrentmin As New SqlParameter("@l_rentmin", SqlDbType.decimal)
			if txt_lp_rentmin.text ="" then 
				prmrentmin.Value = DBNull.Value
			else
				prmrentmin.Value = txt_lp_rentmin.text
			end if
			myCommandADD.Parameters.Add(prmrentmin)
  			
  			Dim prmrentmax As New SqlParameter("@l_rentmax", SqlDbType.decimal)
			if txt_lp_rentmax.text ="" then 
				prmrentmax.Value = DBNull.Value
			else
				prmrentmax.Value = txt_lp_rentmax.text
			end if
			myCommandADD.Parameters.Add(prmrentmax)			
  			
  			dim prmproptype As New SqlParameter("@lp_proptype", SqlDbType.VarChar, 50)
			prmproptype.Value = dd_lp_proptype.SelectedItem.Text
			myCommandADD.Parameters.Add(prmproptype)
  			
  			dim prmbeds As New SqlParameter("@lp_beds", SqlDbType.VarChar, 50)
			prmbeds.Value = dd_lp_bedrooms.SelectedItem.Text
			myCommandADD.Parameters.Add(prmbeds)
  				
  			dim prmbaths As New SqlParameter("@lp_baths", SqlDbType.VarChar, 50)
			prmbaths.Value = dd_lp_baths.SelectedItem.Text
			myCommandADD.Parameters.Add(prmbaths)
  				
  			dim prmlevels As New SqlParameter("@lp_levels", SqlDbType.VarChar, 50)
			prmlevels.Value = dd_lp_levels.SelectedItem.Text
			myCommandADD.Parameters.Add(prmlevels)
  			
  			dim prmbasement As New SqlParameter("@lp_basement", SqlDbType.VarChar, 50)
			prmbasement.Value = dd_lp_basement.SelectedItem.Text
			myCommandADD.Parameters.Add(prmbasement)
  		
  			dim prmpets As New SqlParameter("@lp_pets", SqlDbType.VarChar, 50)
			prmpets.Value = dd_lp_pets.SelectedItem.Text
			myCommandADD.Parameters.Add(prmpets)
  			
  			dim prmfence As New SqlParameter("@lp_fence", SqlDbType.VarChar, 50)
			prmfence.Value = dd_lp_fence.SelectedItem.Text
			myCommandADD.Parameters.Add(prmfence)
			
			dim prmcnty As New SqlParameter("@lp_county", SqlDbType.VarChar, 50)
			prmcnty.Value = dd_lp_county.SelectedItem.Text
			myCommandADD.Parameters.Add(prmcnty)
			
			dim prmschool1 As New SqlParameter("@lp_school1", SqlDbType.VarChar, 50)
			prmschool1.Value = dd_lp_schooldist.SelectedItem.Text
			myCommandADD.Parameters.Add(prmschool1)
			
			dim prmschool2 As New SqlParameter("@lp_school2", SqlDbType.VarChar, 50)
			prmschool2.Value = dd_lp_schooldist2.SelectedItem.Text
			myCommandADD.Parameters.Add(prmschool2)
			
			Dim prmnotes As New SqlParameter("@lp_notes", SqlDbType.text)
			if txt_lp_notes.Text = "" then
				prmnotes.Value = DBNull.Value
			else
				prmnotes.Value = txt_lp_notes.Text
			end if
			myCommandADD.Parameters.Add(prmnotes)
			
     		dim prmsec8 As New SqlParameter("@lp_sec8", SqlDbType.VarChar, 50)
			prmsec8.Value = dd_lp_sec8.SelectedItem.Text
			myCommandADD.Parameters.Add(prmsec8)
			
			  Try
            myConnectionADD.Open()
            myCommandADD.ExecuteNonQuery()
          	myConnectionADD.Close()
             Catch SQLexc As SqlException
                   Response.Write("Insert Failed. Error Details are: " & SQLexc.ToString())
    
           End Try
      
   	end sub
	
	sub savecities()
		'newprofileno=0
		Dim i As Integer 
		
		if Request.QueryString("action")= "view" then
			Dim strSql as String 
			strSql = "delete  from dbo.tbl_leadprofilecities where tbl_leadprofile_fk="& Request.QueryString("profile")
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
		end if
	   
		for i=0 to lstcities.items.count-1
			'response.write(lstcities.items(i))

			Dim rightNow as DateTime = DateTime.Now.toShortDateString() 
   		''Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
			Dim RightNowAdd as datetime = datetime.now
   		Dim supportedFormats() As String = New String() {"M/dd/yyyy","M/d/yyyy","MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
		   Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
  			dim sqlproc as string
  			sqlproc = "sp_insertcities"
  			Dim myCommandADD As New SqlCommand( sqlproc, myConnectionADD)
  		 	myCommandADD.CommandType = CommandType.StoredProcedure
   	  
   	  	if Request.QueryString("action")= "new" then
   	  		'response.write(newprofileno)
   	  		'if newprofileno =0 then 
   	  		'	getprofileno()
   	  		'end if
   	  		Dim prmleadprofile As New SqlParameter("@lppk", SqlDbType.int)
				prmleadprofile.Value = newprofileno
  				myCommandADD.Parameters.Add(prmleadprofile)
   	  	else
   	  		Dim prmleadprofile As New SqlParameter("@lppk", SqlDbType.int)
				prmleadprofile.Value = Request.QueryString("profile")
  				myCommandADD.Parameters.Add(prmleadprofile)
   	  	end if
   	  	  		
	  		Dim prmcityname As New SqlParameter("@cityname", SqlDbType.VarChar, 50)
			prmcityname.Value = Convert.ToString(lstcities.items(i))
	  		myCommandADD.Parameters.Add(prmcityname)
	  		
	  		Try
               myConnectionADD.Open()
               myCommandADD.ExecuteNonQuery()
               myConnectionADD.Close()
                Catch SQLexc As SqlException
                      Response.Write("Insert Failed. Error Details are: " & SQLexc.ToString())
                 End Try
	  		
  		  
		next
		pnlallcities.visible=false
		'ctysave.visible=false
		bindcities()
	end sub
	
	sub getprofileno()
		Dim strUID as String = session("userid")
  		Dim strSql as String = "SELECT max(tbl_lead_profile_pk) as 'newpk' from dbo.tbl_lead_profile where tbl_leads_fk='" & Request.QueryString("leadno") & "'"  
      Dim sqlCmd As SqlCommand
	 	Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
      sqlCmd = New SqlCommand(strSql, myConnection)
              	
         Try
          	myConnection.Open()
             	Dim Sqldr as SqlDataReader = sqlCmd.ExecuteReader
       				if Sqldr.read() then
       					newprofileno = Sqldr("newpk")
        				end if

   				Catch SQLexc As SqlException
                      Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
               	finally
               		myConnection.close()
          End Try

	
	end sub
	
	Sub cntychange	(Source As System.Object, e As System.EventArgs)
   	bindAllcities()
   	bindschooldist()
   	bindschooldist2()
   	
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
	
	public function  getleadprofilepk() as integer
		Dim strUID as String = session("userid")
  		Dim strSql as String = "SELECT k_next_key_value from dbo.tbl_keys where K_table_name='tbl_lead_profile'"  
      Dim sqlCmd As SqlCommand
	 	Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
      sqlCmd = New SqlCommand(strSql, myConnection)
      dim newleadprofilepk as integer
              	
         Try
          	myConnection.Open()
             	Dim Sqldr as SqlDataReader = sqlCmd.ExecuteReader
       				if Sqldr.read() then
       					newleadprofilepk = Sqldr("k_next_key_value")
       					newprofileno = newleadprofilepk
        					return newleadprofilepk
        				end if

   				Catch SQLexc As SqlException
                      Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
               	finally
               		myConnection.close()
          End Try
        
	end function
	
	sub updateprofilepk(value as integer)
		dim newpk as integer = value + 1
		Dim strSql as String = "update dbo.tbl_keys set k_next_key_value=" & newpk & " where K_table_name='tbl_lead_profile'"  
      Dim sqlCmd As SqlCommand
	 	Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
      sqlCmd = New SqlCommand(strSql, myConnection)
      dim newleadprofilepk as integer
              	
         Try
          	myConnection.Open()
            Dim Sqldr as SqlDataReader = sqlCmd.ExecuteReader
       		Catch SQLexc As SqlException
            	Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            finally
            	myConnection.close()
          End Try
	
	end sub
	
	
	
   end class
end namespace