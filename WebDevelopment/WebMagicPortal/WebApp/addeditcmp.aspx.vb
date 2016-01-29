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
Imports FreeTextBoxControls

namespace PageTemplate
    Public Class addeditcmp
        Inherits page
   		
   		Protected WithEvents Lgen, lpage1, lpage2, lautop,limgs,btnuseremail2,btnuseremail As LinkButton
      	Public subnavGen, subnavPage1, subnavPage2, subnavresp,subnavimgs As HtmlTableCell
        	Public spacer0, spacer1, spacer2,spacer3 As HtmlTableCell
      	public pnlstepmain,pnlgeneral, pnlpage1,pnlpage2, pnlnotifications,pnlimages,pnltempatespre,pnladdstep,pnlinporcess as panel
      	public pnlsendemail,pnlconditionemail,pnlemailmain,pnlemailto as panel
      	public baddstep,btnstepnext,bwfactive as button
      	public ddemailcor,dd_leadstatusFrom, dd_leadstatusTo,dd_treminder as dropdownlist
      	Public pg1text, pg2text As FreeTextBox
      	Public txtdescA,txtredirecturl, TextBox1, TextBox2, TextBox3,   txtdescription, selfemailaddress,selfemailaddress2 As TextBox
       	public bdlevel as string
       	public temppreview,WFsteps as datagrid
       	public sdoffset,txtname,txtdesc,txtsdate,txtedate,dno as textbox
       	public dd_sdate,dd_wfstat,dd_trigger,dd_dstep,dd_freq,dd_duration,dd_condition,dd_emailcond as dropdownlist
       	public lblstepno,dsp,lbllsfrom,lbllsto,lblsdateoffest as label
      	public emailbody,hst_action
      	public bfulls,btoolbar,vstepinfo,sconditions,sdetail,showcal1,showcalc,showcalc2 as linkbutton
      	public dd_ldtypeinc,dd_ldpginc,dd_ldAstatinc,dd_ldstatinc,dd_adsinc,dd_MTPinc as dropdownlist
      	public ddlleadtypeFilter,ddlleadprogramFilter,ddlstatusFilter,ddlcstatusFilter,ddadFilter,dd_action,ddMKFilter as listbox
      	public pnlstepdetails,pnlstepconditions,pnl_addtask,pnlleaddds,pnlselectdoW,pnlselectdom,pnlselectdomQ,pnlWFfilters as panel
      	Public cdrCalendar,cdrCalendar2,cdrCalendarWFS
      	'related to tasks
      	public ddlMarketprogramFilter2A,ddlMarketprogramFilter2,ddtasktype,ddtaskstat, ddlstatusFilter2,ddlstatusFilter2A,ddlleadprogramFilter2,ddlleadprogramFilter2A,ddlleadtypeFilter2,ddlleadtypeFilter2A  as dropdownlist
      	public l_treminderEM,l_treminder,wflsearch as textbox
      	public btnuseremailTask,Ldprocess,ldexception  as linkbutton
      	public dd_status,dd_weekselect,dd_Monthselect,dd_Stepreorder,dd_StepStat as dropdownlist
      	public chklsupdate,chkltupdate,chklpupdate,chkrunonce,chkrundaily,chkrunweekly,chkrunMonthly,chkrunQtrly as checkbox
      	public bsaveGenAddS,bwfclone as button
      	public chkStepSun,chkStepMon,chkStepTue,chkStepWed,chkStepThu,chkStepFri,chkStepSat,chkrunSched,chkMPupdate as checkbox
      	public WFLeads as datagrid
      	public lblwffilterstat as label
      	public pnlCalendart,pnlSdateMain as panel
      	public breorder,bsavereorder,bcancelreorder as button
      	  
         Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles mybase.load

            If Not (Page.IsPostBack) Then
            	 subnav("General") 
 					 FillstatusDropDown()
                FillLeadTypeDropDown()
                FillLeadprogramDropDown()
                FillcontactstatusDropDown()
                FillMKTropDown()
                FillADDropDown()
               ' FillLDtstatFrom()
               ' FillLDtstatTo()
                cdrCalendar.visible=false
                cdrCalendar2.visible=false
                showcalc.visible=false
                showcalc2.visible=false
	            if request.querystring("action")="new" then
	            	session("currentwfpk")=""
	            	lpage1.enabled=false
	            	lpage2.enabled=false
	            	subnavPage2.visible=false
	            	subnavPage1.visible=false
	            	bwfclone.visible=false
	            	bwfactive.visible=false
	            	lblwffilterstat.text="This Work Flow has Filtering OFF"
	            	lblwffilterstat.visible=true
	            else
	            	lblwffilterstat.text="This Work Flow has Filtering OFF"
	            	binswfmfields()
	            	bindFilters()
	            	session("currentwfpk")=request.querystring("id")
	            	bsaveGenAddS.visible=false
	            	lblwffilterstat.visible=true
	            end if   
 					
               btoolbar.attributes.add("onClick", "toggleToolbar(); return false;")



                bindwfsteps()
	            if request.querystring("addstep")="yes" then
	            	btnaddstepA()
	            	pnlgeneral.visible=false
	            	pnlpage1.visible=true
	            	pnlpage2.visible=false
	            end if
	            
	            if request.querystring("nav")="wfsdetails" then
		         	Subnav("Status")
	            	Ldprocess.cssclass="linkbuttonsRed"
	            	ldexception.cssclass="linkbuttons"
	            	pnlinporcess.visible=true
	            	bindwfleads()
	          end if
	          
		         
	          End If
	        
	          
            'pagesetup()

        End Sub
        
        
       Public Sub updatedreq(ByVal sender As Object, ByVal e As EventArgs)
        	dim x as checkbox = sender
        	
        	if x.id="chkrunonce" then
        		pnlselectdom.visible=false
        		pnlselectdoW.visible=false
        		pnlselectdomQ.visible=false
        		if x.checked  then        			
        			chkrundaily.checked=false
        			chkrunweekly.checked=false
        			chkrunMonthly.checked=false
        			chkrunQtrly.checked=false
        			if dd_trigger.selecteditem.text = "On A Schedule"  then
        				lblsdateoffest.visible=false
	        			sdoffset.visible=false
        			else
	        			lblsdateoffest.visible=true
	        			sdoffset.visible=true
	        		end if
        		else
        			lblsdateoffest.visible=false
        			sdoffset.visible=false
        		end if        		
        	
        	elseif x.id="chkrundaily" then
        		pnlselectdom.visible=false
        		pnlselectdoW.visible=false
        		pnlselectdomQ.visible=false
        		lblsdateoffest.visible=false
        		sdoffset.visible=false
        		if x.checked  then
        			chkrunonce.checked=false
        			chkrunweekly.checked=false
        			chkrunMonthly.checked=false
        			chkrunQtrly.checked=false
        			
        		end if
        	elseif x.id="chkrunweekly"  then
        			lblsdateoffest.visible=false
        			sdoffset.visible=false
        		if x.checked then
        			pnlselectdom.visible=false
        			if dd_trigger.selecteditem.text = "On A Schedule"  then
        				pnlselectdoW.visible=false
        			else
        				pnlselectdoW.visible=true
        			end if
        			chkrunonce.checked=false
        			chkrundaily.checked=false
        			chkrunMonthly.checked=false
        			pnlselectdomQ.visible=false
        			chkrunQtrly.checked=false
        		else
        			pnlselectdom.visible=false
        			pnlselectdoW.visible=false  
        			pnlselectdomQ.visible=false     		
        		end if
        	elseif x.id="chkrunMonthly" then
        			lblsdateoffest.visible=false
        			sdoffset.visible=false
        		if x.checked  then
        			if dd_trigger.selecteditem.text = "On A Schedule"  then
        				pnlselectdom.visible=false
        			else
        				pnlselectdom.visible=true
        			end if
        			pnlselectdoW.visible=false
        			chkrunonce.checked=false
        			chkrundaily.checked=false
        			chkrunweekly.checked=false
        			pnlselectdomQ.visible=false
        			chkrunQtrly.checked=false
        		else
        			pnlselectdom.visible=false
        			pnlselectdoW.visible=false  
        			pnlselectdomQ.visible=false   
        		end if
        	elseif x.id="chkrunQtrly" then
        			lblsdateoffest.visible=false
        			sdoffset.visible=false
        		if x.checked  then
        			pnlselectdom.visible=false
        			pnlselectdoW.visible=false
        			chkrunonce.checked=false
        			chkrundaily.checked=false
        			chkrunweekly.checked=false
        			if dd_trigger.selecteditem.text = "On A Schedule"  then
        				pnlselectdomQ.visible=false
        			else
        				pnlselectdomQ.visible=true
        			end if
        			
        			chkrunMonthly.checked=false
        		else
        			pnlselectdom.visible=false
        			pnlselectdoW.visible=false  
        			pnlselectdomQ.visible=false   
        		end if	
        		
        	
        	end if
        
       end sub
        
               
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
        
         Public Sub getuseremailT(ByVal sender As Object, ByVal e As EventArgs)
            Dim strSql As String = "SELECT email from tbl_users where UID='" & Session("userid") & "'"
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    If Sqldr("email") IsNot DBNull.Value Then
                        l_treminderEM.Text = Sqldr("email")
                    Else
                        l_treminderEM.Text = "None Found"
                    End If
                End If

            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try

        End Sub
        
        
         Sub statcheck(ByVal Source As System.Object, ByVal e As System.EventArgs)

           
        End Sub
        
        Sub bindtasktype()

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='tasktype' and (x_company='" & Session("company_pk") & "' or x_company='All' or x_uid='" & Session("userid") & "') order by x_descr"
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                ddtasktype.DataSource = dataReader
                ddtasktype.DataTextField = "x_descr"
                ddtasktype.DataValueField = "tbl_xwalk_pk"
                ddtasktype.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

        End Sub
        Sub bindtaskstat()

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='taskstatus' and (x_company='" & Session("company_pk") & "' or x_company='All' or x_uid='" & Session("userid") & "') order by x_descr"
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                ddtaskstat.DataSource = dataReader
                ddtaskstat.DataTextField = "x_descr"
                ddtaskstat.DataValueField = "tbl_xwalk_pk"
                ddtaskstat.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
            ddtaskstat.SelectedIndex = ddtaskstat.Items.IndexOf(ddtaskstat.Items.FindByText("New"))

        End Sub
        
        Sub updatestepdate(ByVal sender As Object, ByVal e As EventArgs)
            
           if dd_dstep.selecteditem.text = "None" then
           		dd_sdate.enabled=true
           		dd_sdate.SelectedIndex = dd_sdate.Items.IndexOf(dd_sdate.Items.FindByText("Lead Capture Date"))
           else
           	 	dd_sdate.SelectedIndex = dd_sdate.Items.IndexOf(dd_sdate.Items.FindByText("Previous Step Complete Date"))
           		dd_sdate.enabled=false
           end if
           
        End Sub
        Sub updatefreqdate(ByVal sender As Object, ByVal e As EventArgs)
            
           if dd_freq.selecteditem.text = "Once" then
           		dno.enabled=false
           		dno.text=""
           		dd_duration.enabled=false
           else
           	 	dno.enabled=true
           
           		dd_duration.enabled=true
           end if
           
        End Sub
        Sub updatetaskremninder(ByVal sender As Object, ByVal e As EventArgs)
            
           if dd_freq.selecteditem.text = "No" then
           		l_treminder.enabled=false
           		l_treminder.text=""
           		l_treminderEM.enabled=false
           		l_treminderEM.text=""
           		btnuseremailTask.enabled=false
           else
           	 	l_treminder.enabled=false  
           	 	l_treminder.text="1" 
           		l_treminderEM.enabled=true  
           			btnuseremailTask.enabled=true 
           			  
           end if
           
        End Sub
        
        
        
        
        
        
        
        
        
         Sub btn_scond(ByVal sender As Object, ByVal e As EventArgs)
            
            pnlstepconditions.visible=true
            pnlstepdetails.visible=false           
            sconditions.cssclass="linkbuttonsRed"
            sdetail.cssclass="linkbuttons"
           
        End Sub
        
        Sub btn_sdetails(ByVal sender As Object, ByVal e As EventArgs)
            
            pnlstepconditions.visible=false
            pnlstepdetails.visible=true           
            sconditions.cssclass="linkbuttons"
            sdetail.cssclass="linkbuttonsRed"
            if dd_trigger.selecteditem.text="On A Schedule"  then
           		dd_dstep.enabled=false
           	 	dd_sdate.enabled=false
            	pnlCalendart.visible=true      	
            
            end if
            
            if request.querystring("action")="view"  and session("stepaction")<>"new" then
            	dd_dstep.enabled=false
            	chkrunonce.enabled=false
            	chkrundaily.enabled=false
            	chkrunweekly.enabled=false
            	chkrunMonthly.enabled=false
            	chkrunQtrly.enabled=false
            	pnlSdateMain.enabled=false
            
            
            end if
            
            
        End Sub
        
        
        
        
        
        Sub showldfromto(ByVal sender As Object, ByVal e As EventArgs)
           if dd_trigger.selecteditem.text <> "On New Lead" and dd_trigger.selecteditem.text <> "On A Schedule" then
           		lbllsfrom.visible=false
           		lbllsto.visible=true
           		dd_leadstatusFrom.visible=false
           		dd_leadstatusTo.visible=true
           		if dd_trigger.selecteditem.text="On Lead Type Change" then
           			FillLDtstatFrom("leadtype")
           			FillLDtstatTo("leadtype")
           			dd_ldtypeinc.SelectedIndex = dd_ldtypeinc.Items.IndexOf(dd_ldtypeinc.Items.FindByText("Do Not Use"))
           			dd_ldtypeinc.enabled=false
           			ddlleadtypeFilter.enabled=false
           			dd_ldpginc.enabled=true
           			ddlleadprogramFilter.enabled=true
           			dd_MTPinc.enabled=true
           			ddMKFilter.enabled=true
           			dd_ldstatinc.enabled=true
           			ddlcstatusFilter.enabled=true
           			
           		elseif dd_trigger.selecteditem.text="On Lead Program Change" then
           			FillLDtstatFrom("leadprogram")
           			FillLDtstatTo("leadprogram")
           			dd_ldpginc.SelectedIndex = dd_ldpginc.Items.IndexOf(dd_ldpginc.Items.FindByText("Do Not Use"))
           			dd_ldpginc.enabled=false
           			ddlleadprogramFilter.enabled=false
           			dd_ldtypeinc.enabled=true
           			ddlleadtypeFilter.enabled=true
           			dd_MTPinc.enabled=true
           			ddMKFilter.enabled=true
           			dd_ldstatinc.enabled=true
           			ddlcstatusFilter.enabled=true
           		
           		elseif dd_trigger.selecteditem.text="On Marketing Program Change" then
           			FillLDtstatFrom("marketprogram")
           			FillLDtstatTo("marketprogram")
           			dd_MTPinc.SelectedIndex = dd_MTPinc.Items.IndexOf(dd_MTPinc.Items.FindByText("Do Not Use"))
           			dd_MTPinc.enabled=false
           			ddMKFilter.enabled=false
           			dd_ldpginc.enabled=true
           			ddlleadprogramFilter.enabled=true
           			dd_ldtypeinc.enabled=true
           			ddlleadtypeFilter.enabled=true           		
           			dd_ldstatinc.enabled=true
           			ddlcstatusFilter.enabled=true
           		
           		elseif dd_trigger.selecteditem.text="On Lead Status Change" then
           			FillLDtstatFrom("leadstatus")
           			FillLDtstatTo("leadstatus")
           			dd_ldstatinc.SelectedIndex = dd_ldstatinc.Items.IndexOf(dd_ldstatinc.Items.FindByText("Do Not Use"))
           			dd_ldstatinc.enabled=false
           			ddlcstatusFilter.enabled=false
           			dd_ldpginc.enabled=true
           			ddlleadprogramFilter.enabled=true
           			dd_ldtypeinc.enabled=true
           			ddlleadtypeFilter.enabled=true
           			dd_MTPinc.enabled=true
           			ddMKFilter.enabled=true
           			
          		end if
          
           else
           		lbllsfrom.visible=false
           		lbllsto.visible=false
           		dd_leadstatusFrom.visible=false
           		dd_leadstatusTo.visible=false
          
           end if 
           
           
           
        End Sub
        
        
         Public Sub bindwfsteps()
            Dim strpropID As String = Request.QueryString("id")
            Dim strUID As String = Session("userid")
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            
            if dd_StepStat.selecteditem.text="All" then
            	mycommand = "Select * from tbl_WorkFlowSteps Where wfs_wfm_fk='" & session("currentwfpk") & "' order by wfs_stepno"
          	elseif dd_StepStat.selecteditem.text="Active" then
          		mycommand = "Select * from tbl_WorkFlowSteps Where wfs_wfm_fk='" & session("currentwfpk") & "' and wfs_status='Active' order by wfs_stepno "
          	else
          		mycommand = "Select * from tbl_WorkFlowSteps Where wfs_wfm_fk='" & session("currentwfpk") & "' and wfs_status='Inactive' order by wfs_stepno "
          
          	end if
            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_WorkFlowSteps")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_WorkFlowSteps"))
                WFsteps.DataSource = dvProducts
                WFsteps.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
        End Sub

        
         Sub newwfs_PageChanger(ByVal Source As Object, _
                ByVal E As DataGridPageChangedEventArgs)
            ' Set the CurrentPageIndex before binding the grid 
            WFsteps.CurrentPageIndex = E.NewPageIndex
            bindwfsteps()

        End Sub
        
         Sub FillLeadprogramDropDown()

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='leadprogram' and  (x_company='All' or x_uid='" & Session("userid") & "') order by x_descr"
            '(x_company='" & Session("company") & "' or
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                ddlleadprogramFilter.DataSource = dataReader
                ddlleadprogramFilter.DataTextField = "x_descr"
                ddlleadprogramFilter.DataValueField = "tbl_xwalk_pk"
                ddlleadprogramFilter.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

        End Sub
         Sub FillLeadTypeDropDown()

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='leadtype' and  (x_company='All' or x_uid='" & Session("userid") & "') order by x_descr"
            '(x_company='" & Session("company") & "' or
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                ddlleadtypeFilter.DataSource = dataReader
                ddlleadtypeFilter.DataTextField = "x_descr"
                ddlleadtypeFilter.DataValueField = "tbl_xwalk_pk"
                ddlleadtypeFilter.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

        End Sub
        Sub FillstatusDropDown()
				Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='leadstatus' and (x_company='" & Session("company_pk") & "' or x_company='All' or x_uid='" & Session("userid") & "') order by x_descr"
    			Dim objCmd As New SqlCommand(myCommand, myConnection)
    			Dim dataReader As SqlDataReader = Nothing
    				Try
      				myConnection.Open()
      				dataReader = objCmd.ExecuteReader()
      				ddlstatusFilter.DataSource = dataReader
      				ddlstatusFilter.DataTextField = "x_descr"
      				ddlstatusFilter.DataValueField = "tbl_xwalk_pk"
      				ddlstatusFilter.DataBind()
    					Catch exc As System.Exception
      					Response.Write(exc.ToString())
    					Finally
      					myConnection.Dispose()
    					End Try

        End Sub
        
        Sub FillLDtstatFrom(type as string)
				Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='" & type & "' and (x_company='" & Session("company_pk") & "' or x_company='All' or x_uid='" & Session("userid") & "') order by x_descr"
    			Dim objCmd As New SqlCommand(myCommand, myConnection)
    			Dim dataReader As SqlDataReader = Nothing
    				Try
      				myConnection.Open()
      				dataReader = objCmd.ExecuteReader()
      				dd_leadstatusFrom.DataSource = dataReader
      				dd_leadstatusFrom.DataTextField = "x_descr"
      				dd_leadstatusFrom.DataValueField = "tbl_xwalk_pk"
      				dd_leadstatusFrom.DataBind()
    					Catch exc As System.Exception
      					Response.Write(exc.ToString())
    					Finally
      					myConnection.Dispose()
    					End Try
    					dd_leadstatusFrom.Items.Insert(0, New ListItem("Select..", "0"))
    					dd_leadstatusFrom.Items.Insert(1, New ListItem("Any", "Any"))

        End Sub
          Sub   FillLDtstatTo(type as string)
				Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='" & type & "' and (x_company='" & Session("company_pk") & "' or x_company='All' or x_uid='" & Session("userid") & "') order by x_descr"
    			Dim objCmd As New SqlCommand(myCommand, myConnection)
    			Dim dataReader As SqlDataReader = Nothing
    				Try
      				myConnection.Open()
      				dataReader = objCmd.ExecuteReader()
      				dd_leadstatusTo.DataSource = dataReader
      				dd_leadstatusTo.DataTextField = "x_descr"
      				dd_leadstatusTo.DataValueField = "tbl_xwalk_pk"
      				dd_leadstatusTo.DataBind()
    					Catch exc As System.Exception
      					Response.Write(exc.ToString())
    					Finally
      					myConnection.Dispose()
    					End Try
						dd_leadstatusTo.Items.Insert(0, New ListItem("Select..", "0"))
        End Sub
              
                
                
                
                
        	Sub FillcontactstatusDropDown()
			Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='contactstatus' and ( x_company='All' or x_uid='" & Session("userid") & "') order by x_descr"
            'x_company='" & Session("company") & "' or
            Dim objCmd As New SqlCommand(myCommand, myConnection)
    		Dim dataReader As SqlDataReader = Nothing
    			Try
      			myConnection.Open()
      			dataReader = objCmd.ExecuteReader()
      			ddlcstatusFilter.DataSource = dataReader
      			ddlcstatusFilter.DataTextField = "x_descr"
      			ddlcstatusFilter.DataValueField = "tbl_xwalk_pk"
      			ddlcstatusFilter.DataBind()
    				Catch exc As System.Exception
      				Response.Write(exc.ToString())
    				Finally
      				myConnection.Dispose()
    				End Try

	  	End Sub     
	  	
	  		  	
	  	Sub FillMKTropDown()
			Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
             Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='marketprogram' and ( x_company='All' or x_uid='" & Session("userid") & "') order by x_descr"
            'x_company='" & Session("company") & "' or
           Dim objCmd As New SqlCommand(myCommand, myConnection)
    		Dim dataReader As SqlDataReader = Nothing
    			Try
      			myConnection.Open()
      			dataReader = objCmd.ExecuteReader()
      			ddMKFilter.DataSource = dataReader
      			ddMKFilter.DataTextField = "x_descr"
      			ddMKFilter.DataValueField = "tbl_xwalk_pk"
      			ddMKFilter.DataBind()
    				Catch exc As System.Exception
      				Response.Write(exc.ToString())
    				Finally
      				myConnection.Dispose()
    				End Try

	  	End Sub      
	  	
	  	
	  	 
	  	Sub FillADDropDown()
			Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select * from dbo.tbl_LeadADs where ad_userid='" & Session("userid") & "' and ad_status='Active' order by ad_title"
            'x_company='" & Session("company") & "' or
            Dim objCmd As New SqlCommand(myCommand, myConnection)
    		Dim dataReader As SqlDataReader = Nothing
    			Try
      			myConnection.Open()
      			dataReader = objCmd.ExecuteReader()
      			ddadFilter.DataSource = dataReader
      			ddadFilter.DataTextField = "ad_title"
      			ddadFilter.DataValueField = "tbl_leadad_pk"
      			ddadFilter.DataBind()
    				Catch exc As System.Exception
      				Response.Write(exc.ToString())
    				Finally
      				myConnection.Dispose()
    				End Try

	  	End Sub      
			 Sub fillwfactions()
				Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='wfstepaction' and (x_company='" & Session("company_pk") & "' or x_company='All' or x_uid='" & Session("userid") & "') order by x_descr"
    			Dim objCmd As New SqlCommand(myCommand, myConnection)
    			Dim dataReader As SqlDataReader = Nothing
    				Try
      				myConnection.Open()
      				dataReader = objCmd.ExecuteReader()
      				dd_action.DataSource = dataReader
      				dd_action.DataTextField = "x_descr"
      				dd_action.DataValueField = "tbl_xwalk_pk"
      				dd_action.DataBind()
    					Catch exc As System.Exception
      					Response.Write(exc.ToString())
    					Finally
      					myConnection.Dispose()
    					End Try
    			'dd_action.Items.Insert(0, New ListItem("Select..", "0"))

        End Sub
        Sub fillprevsteps()
				Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select *, 'Step ' + cast (wfs_stepno as varchar(20)) + ' ' + wfs_Desc as 'fullstepno'  from tbl_WorkFlowSteps where wfs_wfm_fk='" & session("currentwfpk") & "' and wfs_useridFK='" & session("userid") & "' and wfs_status='Active' order by wfs_stepno "
    			Dim objCmd As New SqlCommand(myCommand, myConnection)
    			Dim dataReader As SqlDataReader = Nothing
    				Try
      				myConnection.Open()
      				dataReader = objCmd.ExecuteReader()
      				dd_dstep.DataSource = dataReader
      				dd_dstep.DataTextField = "fullstepno"
      				dd_dstep.DataValueField = "wfs_stepno"
      				dd_dstep.DataBind()
    					Catch exc As System.Exception
      					Response.Write(exc.ToString())
    					Finally
      					myConnection.Dispose()
    					End Try
	
		 		dd_dstep.Items.Insert(0, New ListItem("None", "0"))
        End Sub
        Sub fillprevstepsDG()
				Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select * from tbl_WorkFlowSteps where wfs_wfm_fk='" & session("currentwfpk") & "' and wfs_useridFK='" & session("userid") & "'"
    			Dim objCmd As New SqlCommand(myCommand, myConnection)
    			Dim dataReader As SqlDataReader = Nothing
    				Try
      				myConnection.Open()
      				dataReader = objCmd.ExecuteReader()
      				dd_Stepreorder.DataSource = dataReader
      				dd_Stepreorder.DataTextField = "wfs_stepno"
      				dd_Stepreorder.DataValueField = "wfs_stepno"
      				dd_Stepreorder.DataBind()
    					Catch exc As System.Exception
      					Response.Write(exc.ToString())
    					Finally
      					myConnection.Dispose()
    					End Try
	
        End Sub
        
         Sub getnextstepno()
				Dim strSql As String = "Select max(wfs_stepno)+1 as 'cstepno' from tbl_WorkFlowSteps where wfs_wfm_fk='" & session("currentwfpk") & "' and wfs_useridFK='" & session("userid") & "' and wfs_status='Active'" 
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    If Sqldr("cstepno") IsNot DBNull.Value Then
                        lblstepno.Text = Sqldr("cstepno")
                        session("newstepno")=Sqldr("cstepno")
                    Else
                        lblstepno.Text = "1"
                        session("newstepno")= "1"
                    End If
                End If

            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        End Sub
        Sub fillfreq()
				Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='wfstepfreq' and (x_company='" & Session("company_pk") & "' or x_company='All' or x_uid='" & Session("userid") & "') order by x_descr"
    			Dim objCmd As New SqlCommand(myCommand, myConnection)
    			Dim dataReader As SqlDataReader = Nothing
    				Try
      				myConnection.Open()
      				dataReader = objCmd.ExecuteReader()
      				dd_freq.DataSource = dataReader
      				dd_freq.DataTextField = "x_descr"
      				dd_freq.DataValueField = "tbl_xwalk_pk"
      				dd_freq.DataBind()
    					Catch exc As System.Exception
      					Response.Write(exc.ToString())
    					Finally
      					myConnection.Dispose()
    					End Try
        End Sub
        Sub fillduration()
				Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='wfstepduration' and (x_company='" & Session("company_pk") & "' or x_company='All' or x_uid='" & Session("userid") & "') order by x_descr"
    			Dim objCmd As New SqlCommand(myCommand, myConnection)
    			Dim dataReader As SqlDataReader = Nothing
    				Try
      				myConnection.Open()
      				dataReader = objCmd.ExecuteReader()
      				dd_duration.DataSource = dataReader
      				dd_duration.DataTextField = "x_descr"
      				dd_duration.DataValueField = "tbl_xwalk_pk"
      				dd_duration.DataBind()
    					Catch exc As System.Exception
      					Response.Write(exc.ToString())
    					Finally
      					myConnection.Dispose()
    					End Try
        End Sub
        
        
        Public Sub showbdyA(ByVal Source As System.Object, ByVal e As System.EventArgs)
           
            if bdlevel="Full" then
            	bdlevel="Brief"
            	bindtemppreview()
            	
            	
           	else
           		bdlevel="Full"
           		bindtemppreview()
           		
           	end if
        End Sub
        
        Public Sub btn_SHWFFilters(ByVal Source As System.Object, ByVal e As System.EventArgs)
            dim x as linkbutton = source
            if x.text = "Show Filters" then
            	x.text="Hide Filters"
            	pnlWFfilters.visible=true
            else
            	x.text="Show Filters"
            	pnlWFfilters.visible=false
            end if
            
        End Sub
        
        
        
        
        
        
        
        
        
        Public Sub chooseaction(ByVal Source As System.Object, ByVal e As System.EventArgs)
           
            if dd_action.SelectedItem.Text  = "Send Email" then
            	
           		pnlsendemail.visible=true
           		pnl_addtask.visible=false
           		pnlleaddds.visible=false
           		pnlemailto.visible=false
           		Fillemailcor()
           		TextBox1.focus()
            elseif dd_action.SelectedItem.Text  = "Create Task"  then
            	pnlsendemail.visible=false
            	pnl_addtask.visible=true
            	pnlleaddds.visible=false
            	bindtaskstat()
            	bindtasktype() 
            	ddtasktype.focus()  	
          
            		
           elseif dd_action.SelectedItem.Text  = "Send Notification"  then
           		pnlsendemail.visible=true
           		pnl_addtask.visible=false
           		pnlleaddds.visible=false
           		pnlemailto.visible=true
           		Fillemailcor()
           		selfemailaddress.focus()
           
           else           
           	pnlleaddds.visible=true
           	pnlsendemail.visible=false
            pnl_addtask.visible=false
        	   FillstatusDropDown2()
        	   FillstatusDropDown2A()
            FillLeadTypeDropDown2()
            FillLeadprogramDropDown2()
            FillLeadTypeDropDown2A()
            FillLeadprogramDropDown2A()
            FillMarketDropDown2()
            FillMarketDropDown2A()
            
           end if
           
           cdrCalendar.visible=false
                cdrCalendar2.visible=false
                showcalc.visible=false
                showcalc2.visible=false
           'pnlstepconditions.visible=false
            'pnlstepdetails.visible=true           
            'sconditions.cssclass="linkbuttons"
            'sdetail.cssclass="linkbuttonsRed"
            'sdetail.text=sdetail.text & "- " & dd_action.selecteditem.text 
        End Sub
        
           Sub FillLeadprogramDropDown2()

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='leadprogram' and  (x_company='All' or x_uid='" & Session("userid") & "') order by x_descr"
            '(x_company='" & Session("company") & "' or
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                ddlleadprogramFilter2.DataSource = dataReader
                ddlleadprogramFilter2.DataTextField = "x_descr"
                ddlleadprogramFilter2.DataValueField = "tbl_xwalk_pk"
                ddlleadprogramFilter2.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

        End Sub
        Sub FillLeadprogramDropDown2A()

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='leadprogram' and  (x_company='All' or x_uid='" & Session("userid") & "') order by x_descr"
            '(x_company='" & Session("company") & "' or
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                ddlleadprogramFilter2A.DataSource = dataReader
                ddlleadprogramFilter2A.DataTextField = "x_descr"
                ddlleadprogramFilter2A.DataValueField = "tbl_xwalk_pk"
                ddlleadprogramFilter2A.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

        End Sub
         Sub FillLeadTypeDropDown2()

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='leadtype' and  (x_company='All' or x_uid='" & Session("userid") & "') order by x_descr"
            '(x_company='" & Session("company") & "' or
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                ddlleadtypeFilter2.DataSource = dataReader
                ddlleadtypeFilter2.DataTextField = "x_descr"
                ddlleadtypeFilter2.DataValueField = "tbl_xwalk_pk"
                ddlleadtypeFilter2.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

        End Sub
         Sub FillLeadTypeDropDown2A()

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='leadtype' and  (x_company='All' or x_uid='" & Session("userid") & "') order by x_descr"
            '(x_company='" & Session("company") & "' or
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                ddlleadtypeFilter2A.DataSource = dataReader
                ddlleadtypeFilter2A.DataTextField = "x_descr"
                ddlleadtypeFilter2A.DataValueField = "tbl_xwalk_pk"
                ddlleadtypeFilter2A.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

        End Sub
        Sub FillstatusDropDown2()
				Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='leadstatus' and (x_company='" & Session("company_pk") & "' or x_company='All' or x_uid='" & Session("userid") & "') order by x_descr"
    			Dim objCmd As New SqlCommand(myCommand, myConnection)
    			Dim dataReader As SqlDataReader = Nothing
    				Try
      				myConnection.Open()
      				dataReader = objCmd.ExecuteReader()
      				ddlstatusFilter2.DataSource = dataReader
      				ddlstatusFilter2.DataTextField = "x_descr"
      				ddlstatusFilter2.DataValueField = "tbl_xwalk_pk"
      				ddlstatusFilter2.DataBind()
    					Catch exc As System.Exception
      					Response.Write(exc.ToString())
    					Finally
      					myConnection.Dispose()
    					End Try

        End Sub
        Sub FillstatusDropDown2A()
				Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='leadstatus' and (x_company='" & Session("company_pk") & "' or x_company='All' or x_uid='" & Session("userid") & "') order by x_descr"
    			Dim objCmd As New SqlCommand(myCommand, myConnection)
    			Dim dataReader As SqlDataReader = Nothing
    				Try
      				myConnection.Open()
      				dataReader = objCmd.ExecuteReader()
      				ddlstatusFilter2A.DataSource = dataReader
      				ddlstatusFilter2A.DataTextField = "x_descr"
      				ddlstatusFilter2A.DataValueField = "tbl_xwalk_pk"
      				ddlstatusFilter2A.DataBind()
    					Catch exc As System.Exception
      					Response.Write(exc.ToString())
    					Finally
      					myConnection.Dispose()
    					End Try

        End Sub
         Sub FillMarketDropDown2()
				Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='marketprogram' and (x_company='" & Session("company_pk") & "' or x_company='All' or x_uid='" & Session("userid") & "') order by x_descr"
    			Dim objCmd As New SqlCommand(myCommand, myConnection)
    			Dim dataReader As SqlDataReader = Nothing
    				Try
      				myConnection.Open()
      				dataReader = objCmd.ExecuteReader()
      				ddlMarketprogramFilter2.DataSource = dataReader
      				ddlMarketprogramFilter2.DataTextField = "x_descr"
      				ddlMarketprogramFilter2.DataValueField = "tbl_xwalk_pk"
      				ddlMarketprogramFilter2.DataBind()
    					Catch exc As System.Exception
      					Response.Write(exc.ToString())
    					Finally
      					myConnection.Dispose()
    					End Try

        End Sub
         Sub FillMarketDropDown2A()
				Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='marketprogram' and (x_company='" & Session("company_pk") & "' or x_company='All' or x_uid='" & Session("userid") & "') order by x_descr"
    			Dim objCmd As New SqlCommand(myCommand, myConnection)
    			Dim dataReader As SqlDataReader = Nothing
    				Try
      				myConnection.Open()
      				dataReader = objCmd.ExecuteReader()
      				ddlMarketprogramFilter2A.DataSource = dataReader
      				ddlMarketprogramFilter2A.DataTextField = "x_descr"
      				ddlMarketprogramFilter2A.DataValueField = "tbl_xwalk_pk"
      				ddlMarketprogramFilter2A.DataBind()
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
            dsp.visible=true
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
              dsp.visible=true
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
              dsp.visible=true
        End Sub
         Public Sub Canceltemplate(ByVal Source As System.Object, ByVal e As System.EventArgs)
            pnltempatespre.visible=false
            ddemailcor.SelectedIndex = ddemailcor.Items.IndexOf(ddemailcor.Items.FindByText("Select.."))
              dsp.visible=true
	   
        End Sub
        
        Sub Fillemailcor()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select *, convert(varchar(20),email_date,101) as 'emdate' " _
								& "from tbl_emails " _
								& "join tbl_xwalk on tbl_xwalk_pk = email_stat " _
								& "where x_descr <> 'Do Not Use' " _
								& "and ((email_uid='" & Session("userid") & "' ) " _
								& "or (x_descr='Company Wide' and email_co='" & Session("company_pk") & "') " _
								& "or (x_descr='System Wide'))"
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                ddemailcor.DataSource = dataReader
                ddemailcor.DataTextField = "email_name"
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
        
        Sub nextstep(ByVal sender As Object, ByVal e As EventArgs)
           if dd_action.selecteditem.text = "Send Email" then
           		pnlsendemail.visible=true
           		Fillemailcor()
           		
           else
           
           end if
            pnlstepconditions.visible=false
            pnlstepdetails.visible=false           
            'sconditions.cssclass="linkbuttons"
            'sdetail.cssclass="linkbuttonsRed"
            'sdetail.text=sdetail.text & "- " & dd_action.selecteditem.text 
            insertwfs()
            pnlsendemail.visible=false
        		pnladdstep.visible=false
        		pnlstepmain.visible=true
        		pnl_addtask.visible=false
        		pnlleaddds.visible=false
        			pnlstepconditions.visible=false
        		Lgen.enabled=true
            lpage1.enabled=true
            lpage2.enabled=true
            
           	pnlstepconditions.visible=false
        	  	bindwfsteps()
        	  	'fillprevstepsDG()
           
        End Sub
        Sub btn_Fullscreen(ByVal sender As Object, ByVal e As EventArgs)
         
         if bfulls.text = "Full Screen" then
         	bfulls.text = "Normal"
         	pnlemailmain.visible=false
         	emailbody.width="1000"
          	emailbody.height="440"
         else
        	 	bfulls.text = "Full Screen"
         	pnlemailmain.visible=true
         	emailbody.width="800"
          	emailbody.height="275"
        end if
         
         'session("fscdata")=emailbody.content
          'Response.Write("<script>window.open" & _
          ' "('WFfullscreen.aspx','_new','width=1000,height=700,resizable=1,scrollbars=1');</script>")
  			 
          
        End Sub
        
        
        
        
        
        Sub checkcondition(ByVal sender As Object, ByVal e As EventArgs)
           if dd_condition.selecteditem.text = "Yes" then
           		pnlconditionemail.visible=true
           		
           else
           		pnlconditionemail.visible=false
           end if
           
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
            	
            	dsp.visible=false
            End If

        End Sub
        
        Sub bindtemppreview()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            
            if bdlevel="Full" then
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
                
        sub btnaddstep(ByVal sender As Object, ByVal e As EventArgs)
        
        		btnaddstepA()
        end sub
        
         sub stepreorderA(ByVal sender As Object, ByVal e As EventArgs)
        
        		WFsteps.Columns(2).Visible = true
        		breorder.visible=false
        		bsavereorder.visible=true
        		bcancelreorder.visible=true
        		baddstep.visible=false
        		bindwfsteps()
        end sub
        
         sub cancelstepreorder(ByVal sender As Object, ByVal e As EventArgs)
        
        		WFsteps.Columns(2).Visible = false
        		breorder.visible=true
        		bsavereorder.visible=false
        		bcancelreorder.visible=false
        		baddstep.visible=true
        end sub
        
        
        
        
         sub savestepreorder(ByVal sender As Object, ByVal e As EventArgs)
        		Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "update tbl_WorkFlowSteps set wfs_stepno=wfs_newstepno where wfs_wfm_fk='" & session("currentwfpk") & "'"
          
            Try
               strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
               sqlConn = New SqlConnection(strConnection)
               sqlCmd = New SqlCommand(strSql, sqlConn)
               sqlConn.Open()
               Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

               If Sqldr.Read() Then
             
                end if
		
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                sqlConn.Close()
            End Try
              
        		strSql = "update tbl_WorkFlowSteps set wfs_newstepno=null where wfs_wfm_fk='" & session("currentwfpk") & "'"
                       
            Try
               strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
               sqlConn = New SqlConnection(strConnection)
               sqlCmd = New SqlCommand(strSql, sqlConn)
               sqlConn.Open()
               Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

               If Sqldr.Read() Then
             
                end if
		
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                sqlConn.Close()
            End Try
        
        
        		WFsteps.Columns(2).Visible = false
        		breorder.visible=true
        		bsavereorder.visible=false
        		bcancelreorder.visible=false
        		baddstep.visible=true
        		bindwfsteps()
    					
        		
        end sub
        
        
        
        
        
        Sub btnaddstepA()
            session("stepaction")="new"
            if session("currentwfpk")="" then
            	insertwfm()
            	getwfpk()
            else
            	insertwfm()
            end if
            pnladdstep.visible=true
            pnlstepmain.visible=false
            pnlleaddds.visible=false
           	fillwfactions()
           	getnextstepno()
           	fillprevsteps()
           	fillfreq()
           	fillduration()
           	fillstartdate()
           	 pnlstepconditions.visible=true
            pnlstepdetails.visible=false           
            sconditions.cssclass="linkbuttonsRed"
            sdetail.cssclass="linkbuttons"
            Lgen.enabled=false
            lpage1.enabled=false
            lpage2.enabled=false
            cdrCalendar.visible=false
          cdrCalendar2.visible=false
          showcalc.visible=false
          showcalc2.visible=false
          txtdescA.text=""
          
          sdoffset.text=""
          dno.text=""
          selfemailaddress.text=""
          dd_status.SelectedIndex = dd_status.Items.IndexOf(dd_status.Items.FindByText("Select.."))
          ddemailcor.SelectedIndex = ddemailcor.Items.IndexOf(ddemailcor.Items.FindByText("Select.."))
          dd_treminder.SelectedIndex = dd_treminder.Items.IndexOf(dd_treminder.Items.FindByText("No"))
          dd_dstep.SelectedIndex = dd_dstep.Items.IndexOf(dd_dstep.Items.FindByvalue("0"))
         
          if  dd_trigger.selecteditem.text = "On Lead Type Change" or dd_trigger.selecteditem.text = "On Lead Program Change" or dd_trigger.selecteditem.text = "On Marketing Program Change" or dd_trigger.selecteditem.text = "On Lead Status Change" then
          	dd_sdate.SelectedIndex = dd_sdate.Items.IndexOf(dd_sdate.Items.FindByText("Status Change Date"))
          	dd_sdate.items.remove(dd_sdate.Items.FindByText("On A Schedule"))
          	dd_sdate.items.remove(dd_sdate.Items.FindByText("Lead Capture Date"))
          	dd_sdate.items.remove(dd_sdate.Items.FindByText("Work Flow Assigned Date"))
          	
          elseif dd_trigger.selecteditem.text = "On New Lead"          
          	dd_sdate.SelectedIndex = dd_sdate.Items.IndexOf(dd_sdate.Items.FindByText("Lead Capture Date"))
          	dd_sdate.items.remove(dd_sdate.Items.FindByText("On A Schedule"))
          	dd_sdate.items.remove(dd_sdate.Items.FindByText("Status Change Date"))
          elseif dd_trigger.selecteditem.text = "On A Schedule"          
          	dd_sdate.SelectedIndex = dd_sdate.Items.IndexOf(dd_sdate.Items.FindByText("On A Schedule"))
          	dd_sdate.items.remove(dd_sdate.Items.FindByText("Lead Capture Date"))
          	dd_sdate.items.remove(dd_sdate.Items.FindByText("Work Flow Assigned Date"))
          		dd_sdate.items.remove(dd_sdate.Items.FindByText("Status Change Date"))
          	
          	
          end if
          'dd_freq.SelectedIndex = dd_freq.Items.IndexOf(dd_freq.Items.FindByText("Once"))
          
          'dd_duration.SelectedIndex = dd_duration.Items.IndexOf(dd_duration.Items.FindByText("Days"))
          TextBox1.text=""
          TextBox2.text=""
          TextBox3.text=""
          emailbody.content=""
          'l_duedate.text=""
          l_treminder.text=""
          l_treminderEM.text=""
          hst_action.content=""
          chklsupdate.checked=false
          chkltupdate.checked=false
          chklpupdate.checked=false
          chkMPupdate.checked=false
          txtdescA.focus()
      	dd_sdate.enabled=false
             
             
        End Sub
        
        Sub canceladdstep(ByVal sender As Object, ByVal e As EventArgs)
        		Lgen.enabled=true
            lpage1.enabled=true
            lpage2.enabled=true
             pnladdstep.visible=false
            pnlstepmain.visible=true
            pnlsendemail.visible=false           
           	pnl_addtask.visible=false
           	pnlstepconditions.visible=false
        end sub
        
        sub fillstartdate()
         	Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='wfstepSdate' and  (x_company='All' or x_uid='" & Session("userid") & "') order by x_descr"
            '(x_company='" & Session("company") & "' or
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                dd_sdate.DataSource = dataReader
                dd_sdate.DataTextField = "x_descr"
                dd_sdate.DataValueField = "tbl_xwalk_pk"
                dd_sdate.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
        
        
        
        end sub
        
        
        Sub savestep(ByVal sender As Object, ByVal e As EventArgs)
        		insertwfs()
        		pnlsendemail.visible=false
        		pnladdstep.visible=false
        		pnlstepmain.visible=true
        	  	bindwfsteps()
        end sub
        
        
        Sub updatefilters(ByVal sender As Object, ByVal e As EventArgs)
        		Dim x As dropdownlist = sender
            if  x.ID = "dd_ldtypeinc" then
            	if dd_ldtypeinc.selecteditem.text = "Do Not Use"
            		ddlleadtypeFilter.enabled=false
            	else
            		ddlleadtypeFilter.enabled=true
            	end if            	
            elseif x.ID = "dd_ldpginc" then
            	if dd_ldpginc.selecteditem.text = "Do Not Use"
            		ddlleadprogramFilter.enabled=false
            	else
            		ddlleadprogramFilter.enabled=true
            	end if            
            elseif x.ID = "dd_ldAstatinc" then
            	if dd_ldAstatinc.selecteditem.text = "Do Not Use"
            		ddlstatusFilter.enabled=false
            	else
            		ddlstatusFilter.enabled=true
            	end if            
            elseif x.ID = "dd_ldstatinc" then
            	if dd_ldstatinc.selecteditem.text = "Do Not Use"
            		ddlcstatusFilter.enabled=false
            	else
            		ddlcstatusFilter.enabled=true
            	end if            
            elseif x.ID = "dd_adsinc" then
            	if dd_adsinc.selecteditem.text = "Do Not Use"
            		ddadFilter.enabled=false
            	else
            		ddadFilter.enabled=true
            	end if      
           	elseif x.ID = "dd_MTPinc" then
            	if dd_MTPinc.selecteditem.text = "Do Not Use"
            		ddMKFilter.enabled=false
            	else
            		ddMKFilter.enabled=true
            	end if            
            end if
          
        		
        end sub
        
        
        
        
        sub getwfpk()
        
           Dim strSql As String = "SELECT max(wfm_tbl_pk) as 'maxpk' from tbl_WorkFlowMaster"
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    session("currentwfpk") = Sqldr("maxpk")
                   
                End If

            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        
        end sub
          Sub insertwfm()

            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String
            If Request.QueryString("action") = "new" Then
                sqlproc = "sp_insertwfmaster"
            ElseIf Request.QueryString("action") = "view"  Then
                sqlproc = "sp_updatewfmaster"           
            End If


            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
            If Request.QueryString("action") = "view" Then
                Dim prmpk As New SqlParameter("@tblpk", SqlDbType.Int)
                prmpk.Value = Request.QueryString("id")
                myCommandADD.Parameters.Add(prmpk)
            End If
            
            Dim prmuserid As New SqlParameter("@userfk", SqlDbType.varchar,50)
            prmuserid.Value = session("userid")
            myCommandADD.Parameters.Add(prmuserid)
            
            Dim prmname As New SqlParameter("@name", SqlDbType.varchar,255)
            prmname.Value = txtname.text
            myCommandADD.Parameters.Add(prmname)

            Dim prmdescript As New SqlParameter("@desc", SqlDbType.varchar,255)
            prmdescript.Value = txtdesc.text
            myCommandADD.Parameters.Add(prmdescript)

            Dim prmstatus As New SqlParameter("@status", SqlDbType.varchar,50)
            prmstatus.Value = dd_wfstat.selecteditem.text
            myCommandADD.Parameters.Add(prmstatus)

 				Dim prmtrigger As New SqlParameter("@trigger", SqlDbType.VarChar, 50)
            prmtrigger.Value = dd_trigger.selecteditem.text
            myCommandADD.Parameters.Add(prmtrigger)
           
            Dim prmsdate As New SqlParameter("@effdate", SqlDbType.datetime)
            prmsdate.Value = txtsdate.text
            myCommandADD.Parameters.Add(prmsdate)

            Dim prmedate As New SqlParameter("@enddate", SqlDbType.DateTime)
            prmedate.Value = txtedate.text
            myCommandADD.Parameters.Add(prmedate)
            
            Dim prmleadtype As New SqlParameter("@leadtype", SqlDbType.VarChar, 50)
            prmleadtype.Value = "LeadType"
            myCommandADD.Parameters.Add(prmleadtype)
             
            Dim prmleadprg As New SqlParameter("@leadprg", SqlDbType.VarChar, 50)
            prmleadprg.Value = "LeadProgram"
            myCommandADD.Parameters.Add(prmleadprg)
             
            Dim prmassignedstat As New SqlParameter("@assignstat", SqlDbType.VarChar, 50)
            prmassignedstat.Value = "AssignedStatus"
            myCommandADD.Parameters.Add(prmassignedstat)
             
            Dim prmleadstat As New SqlParameter("@leadstat", SqlDbType.VarChar, 50)
            prmleadstat.Value = "LeadStatus"
            myCommandADD.Parameters.Add(prmleadstat)
            
            Dim prmad As New SqlParameter("@adstat", SqlDbType.VarChar, 50)
            prmad.Value = "AD"
            myCommandADD.Parameters.Add(prmad)
            
            Dim prmleadtypeI As New SqlParameter("@leadtypeI", SqlDbType.VarChar, 50)
            prmleadtypeI.Value = dd_ldtypeinc.SelectedItem.Text
            myCommandADD.Parameters.Add(prmleadtypeI)
            
            Dim prmleadprgI As New SqlParameter("@leadprgI", SqlDbType.VarChar, 50)
            prmleadprgI.Value = dd_ldpginc.SelectedItem.Text
            myCommandADD.Parameters.Add(prmleadprgI)
            
            Dim prmassignedstatI As New SqlParameter("@assignstatI", SqlDbType.VarChar, 50)
            prmassignedstatI.Value = dd_ldAstatinc.SelectedItem.Text
            myCommandADD.Parameters.Add(prmassignedstatI)
            
            Dim prmleadstatI As New SqlParameter("@leadstatI", SqlDbType.VarChar, 50)
            prmleadstatI.Value = dd_ldstatinc.SelectedItem.Text
            myCommandADD.Parameters.Add(prmleadstatI)
            
            Dim prmadI As New SqlParameter("@adstatI", SqlDbType.VarChar, 50)
            prmadI.Value = dd_adsinc.SelectedItem.Text
            myCommandADD.Parameters.Add(prmadI)
            
            Dim prmTLSfrom As New SqlParameter("@TLSfrom", SqlDbType.VarChar, 50)
            if dd_trigger.selecteditem.text <> "On New Lead" and dd_trigger.selecteditem.text <> "On A Schedule" then    
            	prmTLSfrom.Value = dd_leadstatusFrom.SelectedItem.Text
            else
           		prmTLSfrom.Value = dbnull.value
            end if
            myCommandADD.Parameters.Add(prmTLSfrom)
            
            Dim prmTLSTo As New SqlParameter("@TLSto", SqlDbType.VarChar, 50)
            if dd_trigger.selecteditem.text <> "On New Lead" and dd_trigger.selecteditem.text <> "On A Schedule" then            
            	prmTLSTo.Value = dd_leadstatusTo.SelectedItem.Text
            else 
            	prmTLSTo.Value = dbnull.value
            end if
            myCommandADD.Parameters.Add(prmTLSTo)
            
            Dim prmMKTI As New SqlParameter("@marketStatI", SqlDbType.VarChar, 50)
            prmMKTI.Value =  dd_MTPinc.SelectedItem.Text
            myCommandADD.Parameters.Add(prmMKTI)
            
            Dim prmMKT As New SqlParameter("@marketstat", SqlDbType.VarChar, 50)
            prmMKT.Value = "MarketProgram"
            myCommandADD.Parameters.Add(prmMKT)
           
            Try
                myConnectionADD.Open()
                myCommandADD.ExecuteNonQuery()
                myConnectionADD.Close()
            Catch SQLexc As SqlException
                Response.Write("Insert Failed. Error Details are: " & SQLexc.ToString())
            End Try
        End Sub
        
        Sub clearall(ByVal Source As System.Object, ByVal e As System.EventArgs)
				session("PubSearchFWF")="false"
				wflsearch.text=""
				bindwfleads()
        End Sub
        
        Sub filterVenuesAADSLK(ByVal Source As System.Object, ByVal e As System.EventArgs)
   			session("PubSearchFWF")="true"
     			session.remove("PubSearchFWFV")
           	
           	WFLeads.CurrentPageIndex = 0
           	bindwfleads()

        end sub
        
        
        
        
        
         Sub insertwfs()

            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String
            If session("stepaction") = "new" Then
                sqlproc = "sp_insertwfsteps"
            Else
                sqlproc = "sp_updatewfsteps"           
            End If


            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
            If session("stepaction") <> "new"  Then
                Dim prmSpk As New SqlParameter("@Stblpk", SqlDbType.Int)
                prmSpk.Value = session("wfstepPK")
                myCommandADD.Parameters.Add(prmSpk)
            End If
            
            If session("stepaction") = "new" Then
                Dim prmStepstat As New SqlParameter("@stepstat", SqlDbType.varchar,50)
                prmStepstat.Value = "Active"
                myCommandADD.Parameters.Add(prmStepstat)
        
            
            end if
             
          	Dim prmWFpk As New SqlParameter("@wftblpk", SqlDbType.Int)
          	prmWFpk.Value = Request.QueryString("id")
          	myCommandADD.Parameters.Add(prmWFpk)
            
            Dim prmuserid As New SqlParameter("@userfk", SqlDbType.varchar,50)
            prmuserid.Value = session("userid")
            myCommandADD.Parameters.Add(prmuserid)            
         
            Dim prmdescript As New SqlParameter("@desc", SqlDbType.varchar,255)
            prmdescript.Value = txtdescA.text
            myCommandADD.Parameters.Add(prmdescript)

            Dim prmaction As New SqlParameter("@type", SqlDbType.varchar,50)
            prmaction.Value = dd_action.selecteditem.text
            myCommandADD.Parameters.Add(prmaction)

 				Dim prmfreq As New SqlParameter("@freq", SqlDbType.VarChar, 50)
            if chkrunonce.checked then 
            	prmfreq.Value = "Once"
            elseif chkrundaily.checked then
           		prmfreq.Value = "Daily"
            elseif chkrunweekly.checked then
           		prmfreq.Value = "Weekly"
           	elseif chkrunMonthly.checked then
           		prmfreq.Value = "Monthly"
           	elseif chkrunQtrly.checked then
           		prmfreq.Value = "Quarterly"
           	else
            	prmfreq.Value = "Once"
            end if
           
            myCommandADD.Parameters.Add(prmfreq)
            
            Dim prmduration As New SqlParameter("@duration", SqlDbType.VarChar, 50)
            prmduration.Value = dd_duration.selecteditem.text
            myCommandADD.Parameters.Add(prmduration)
            
            Dim prmdamount As New SqlParameter("@damount", SqlDbType.nvarchar,50)
            prmdamount.Value = dno.text
            myCommandADD.Parameters.Add(prmdamount)
            
            Dim prmdstep As New SqlParameter("@dstep", SqlDbType.VarChar, 50)
            prmdstep.Value = dd_dstep.selecteditem.value
            myCommandADD.Parameters.Add(prmdstep)
            
            Dim prmcond As New SqlParameter("@cond", SqlDbType.VarChar, 50)
            prmcond.Value = dd_condition.selecteditem.text
            myCommandADD.Parameters.Add(prmcond)
            
            Dim prmcondI1 As New SqlParameter("@condI1", SqlDbType.VarChar, 50)
            if dd_action.selecteditem.text="Send Email" then
            	prmcondI1.Value = "Lead Email"
            else
            	prmcondI1.Value = dbnull.value
            end if            
            myCommandADD.Parameters.Add(prmcondI1)
            
            Dim prmcondOP As New SqlParameter("@condOP", SqlDbType.VarChar, 50)
            if dd_action.selecteditem.text="Send Email" then
            	prmcondOP.Value = "Equals"
            else 
            	prmcondOP.Value = dbnull.value
            end if            
            myCommandADD.Parameters.Add(prmcondOP)
            
            Dim prmcondI2 As New SqlParameter("@condI2", SqlDbType.VarChar, 50)
            if dd_action.selecteditem.text="Send Email" then
            	prmcondI2.Value = "Blank"
            else 
            	prmcondI2.Value = dbnull.value       
            end if            
            myCommandADD.Parameters.Add(prmcondI2)
            
            Dim prmcondR As New SqlParameter("@condR", SqlDbType.VarChar, 50)
            if dd_action.selecteditem.text="Send Email" then
            	prmcondR.Value = dd_emailcond.selecteditem.text
            else
            	prmcondR.Value = dbnull.value
            end if
            myCommandADD.Parameters.Add(prmcondR)
            
            Dim prmstepno As New SqlParameter("@stepno", SqlDbType.VarChar, 50)
            prmstepno.Value = lblstepno.Text
            myCommandADD.Parameters.Add(prmstepno)
            
            Dim prmmailfrom As New SqlParameter("@mailfrom", SqlDbType.VarChar, 50)
            if dd_action.selecteditem.text="Send Email" or dd_action.selecteditem.text="Send Notification" then
            	prmmailfrom.Value = TextBox1.text
            else
            	prmmailfrom.Value =  dbnull.value
            end if
            
            myCommandADD.Parameters.Add(prmmailfrom)
            
            Dim prmmailsubject As New SqlParameter("@mailsub", SqlDbType.VarChar, 50)
            if dd_action.selecteditem.text="Send Email" or dd_action.selecteditem.text="Send Notification" then
            	prmmailsubject.Value = TextBox3.text
            else
            	prmmailsubject.Value =  dbnull.value
            end if            
            myCommandADD.Parameters.Add(prmmailsubject)
            
            Dim prmmailbody As New SqlParameter("@mailbody", SqlDbType.text)
            if dd_action.selecteditem.text="Send Email" or dd_action.selecteditem.text="Send Notification" then
            	prmmailbody.Value = emailbody.content
            else
            	prmmailbody.Value =  dbnull.value
            end if            
            myCommandADD.Parameters.Add(prmmailbody)
            
            Dim prmbasedate As New SqlParameter("@basedate", SqlDbType.varchar,50)
            prmbasedate.Value = dd_sdate.selecteditem.text
            myCommandADD.Parameters.Add(prmbasedate)    
                          
            Dim prmbasedateoff As New SqlParameter("@basedateoff", SqlDbType.nVarChar, 50)
            if sdoffset.text.length > 0 then
            	prmbasedateoff.Value = sdoffset.text
            else
            	prmbasedateoff.Value = "0"
            end if
            myCommandADD.Parameters.Add(prmbasedateoff)      
        '--
        		Dim prmmailto As New SqlParameter("@mailto", SqlDbType.VarChar, 50)
            if dd_action.selecteditem.text="Send Email" or dd_action.selecteditem.text="Send Notification" then
            	prmmailto.Value = selfemailaddress.text
            else
            	prmmailto.Value =  dbnull.value
            end if
				myCommandADD.Parameters.Add(prmmailto)				
				
				Dim prmleaddetail As New SqlParameter("@lddetail", SqlDbType.varchar,50)
				if  dd_action.selecteditem.text="Send Notification" then
            	prmleaddetail.Value = dd_status.selecteditem.text
            else
            	prmleaddetail.Value =  dbnull.value
            end if
            myCommandADD.Parameters.Add(prmleaddetail)    
            
            Dim prmtasktype As New SqlParameter("@tasktype", SqlDbType.varchar,50)
				if  dd_action.selecteditem.text="Create Task" then
            	prmtasktype.Value = ddtasktype.selecteditem.text
            else
            	prmtasktype.Value =  dbnull.value
            end if
            myCommandADD.Parameters.Add(prmtasktype) 
            
            Dim prmtaskstat As New SqlParameter("@taskstat", SqlDbType.varchar,50)
				if  dd_action.selecteditem.text="Create Task" then
            	prmtaskstat.Value = ddtaskstat.selecteditem.text
            else
            	prmtaskstat.Value =  dbnull.value
            end if
            myCommandADD.Parameters.Add(prmtaskstat) 
            
            Dim prmtaskdate As New SqlParameter("@taskdate", SqlDbType.datetime)
				
            	prmtaskdate.Value =  dbnull.value
           
            myCommandADD.Parameters.Add(prmtaskdate) 
            
            Dim prmtaskremind As New SqlParameter("@taskremind", SqlDbType.varchar,50)
				if  dd_action.selecteditem.text="Create Task" then
            	prmtaskremind.Value = dd_treminder.selecteditem.text
            else
            	prmtaskremind.Value =  dbnull.value
            end if
            myCommandADD.Parameters.Add(prmtaskremind)
            
            Dim prmtaskdateDays As New SqlParameter("@taskdateDays", SqlDbType.nvarchar,40)
				if  dd_action.selecteditem.text="Create Task" then
            	prmtaskdateDays.Value = l_treminder.text
            else
            	prmtaskdateDays.Value =  dbnull.value
            end if
            myCommandADD.Parameters.Add(prmtaskdateDays) 
            
            Dim prmtaskemailto As New SqlParameter("@taskemailto", SqlDbType.varchar,50)
				if  dd_action.selecteditem.text="Create Task" then
            	prmtaskemailto.Value = l_treminderEM.text
            else
            	prmtaskemailto.Value =  dbnull.value
            end if
            myCommandADD.Parameters.Add(prmtaskemailto) 
             
				Dim prmtaskdesc As New SqlParameter("@taskdesc", SqlDbType.text)
				if  dd_action.selecteditem.text="Create Task" then
            	 prmtaskdesc.Value = hst_action.content
            else
            	prmtaskdesc.Value =  dbnull.value
            end if
            myCommandADD.Parameters.Add(prmtaskdesc) 
            
            Dim prmLSUpdate As New SqlParameter("@LSUpdate", SqlDbType.varchar,1)
				if  dd_action.selecteditem.text="Update Lead Info" then
					if chklsupdate.checked then
						prmLSUpdate.Value = "Y"
					else
						prmLSUpdate.Value = "N"
					end if
            else
            	prmLSUpdate.Value =  dbnull.value
            end if
            myCommandADD.Parameters.Add(prmLSUpdate)
            
            Dim prmLtUpdate As New SqlParameter("@LtUpdate", SqlDbType.varchar,1)
				if  dd_action.selecteditem.text="Update Lead Info" then
					if chkltupdate.checked then
						prmLtUpdate.Value = "Y"
					else
						prmLtUpdate.Value = "N"
					end if
            else
            	prmLtUpdate.Value =  dbnull.value
            end if
            myCommandADD.Parameters.Add(prmLtUpdate)
            
            Dim prmLpUpdate As New SqlParameter("@LpUpdate", SqlDbType.varchar,1)
				if  dd_action.selecteditem.text="Update Lead Info" then
					if chklpupdate.checked then
						prmLpUpdate.Value = "Y"
					else
						prmLpUpdate.Value = "N"
					end if
            else
            	prmLpUpdate.Value =  dbnull.value
            end if
            myCommandADD.Parameters.Add(prmLpUpdate)
            
             Dim prmMPUpdate As New SqlParameter("@MPUpdate", SqlDbType.varchar,1)
				if  dd_action.selecteditem.text="Update Lead Info" then
					if chkMPupdate.checked then
						prmMPUpdate.Value = "Y"
					else
						prmMPUpdate.Value = "N"
					end if
            else
            	prmMPUpdate.Value =  dbnull.value
            end if
            myCommandADD.Parameters.Add(prmMPUpdate)
            
            
            
            Dim prmLSUpdateFrom As New SqlParameter("@LSUpdateFrom", SqlDbType.varchar,50)
				if  dd_action.selecteditem.text="Update Lead Info" then
            	prmLSUpdateFrom.Value = ddlstatusFilter2.selecteditem.text
            else
            	prmLSUpdateFrom.Value =  dbnull.value
            end if
            myCommandADD.Parameters.Add(prmLSUpdateFrom)
            
             Dim prmLSUpdateTo As New SqlParameter("@LSUpdateTo", SqlDbType.varchar,50)
				if  dd_action.selecteditem.text="Update Lead Info" then
            	prmLSUpdateTo.Value = ddlstatusFilter2A.selecteditem.text
            else
            	prmLSUpdateTo.Value =  dbnull.value
            end if
            myCommandADD.Parameters.Add(prmLSUpdateTo)
            
             Dim prmLTUpdateFrom As New SqlParameter("@LTUpdateFrom", SqlDbType.varchar,50)
				if  dd_action.selecteditem.text="Update Lead Info" then
            	prmLTUpdateFrom.Value = ddlleadtypeFilter2.selecteditem.text
            else
            	prmLTUpdateFrom.Value =  dbnull.value
            end if
            myCommandADD.Parameters.Add(prmLTUpdateFrom)
            
            Dim prmLTUpdateTo As New SqlParameter("@LTUpdateTo", SqlDbType.varchar,50)
				if  dd_action.selecteditem.text="Update Lead Info" then
            	prmLTUpdateTo.Value = ddlleadtypeFilter2A.selecteditem.text
            else
            	prmLTUpdateTo.Value =  dbnull.value
            end if
            myCommandADD.Parameters.Add(prmLTUpdateTo)
            
             Dim prmLPUpdateFrom As New SqlParameter("@LPUpdateFrom", SqlDbType.varchar,50)
				if  dd_action.selecteditem.text="Update Lead Info" then
            	prmLPUpdateFrom.Value = ddlleadprogramFilter2.selecteditem.text
            else
            	prmLPUpdateFrom.Value =  dbnull.value
            end if
            myCommandADD.Parameters.Add(prmLPUpdateFrom)
            
            Dim prmLPUpdateTo As New SqlParameter("@LpUpdateTo", SqlDbType.varchar,50)
				if  dd_action.selecteditem.text="Update Lead Info" then
            	prmLPUpdateTo.Value = ddlleadprogramFilter2A.selecteditem.text
            else
            	prmLPUpdateTo.Value =  dbnull.value
            end if
            myCommandADD.Parameters.Add(prmLPUpdateTo)
            
             Dim prmMPUpdateFrom As New SqlParameter("@MPUpdateFrom", SqlDbType.varchar,50)
				if  dd_action.selecteditem.text="Update Lead Info" then
            	prmMPUpdateFrom.Value = ddlMarketprogramFilter2.selecteditem.text
            else
            	prmMPUpdateFrom.Value =  dbnull.value
            end if
            myCommandADD.Parameters.Add(prmMPUpdateFrom)
            
            Dim prmMPUpdateTo As New SqlParameter("@MPUpdateTo", SqlDbType.varchar,50)
				if  dd_action.selecteditem.text="Update Lead Info" then
            	prmMPUpdateTo.Value = ddlMarketprogramFilter2A.selecteditem.text
            else
            	prmMPUpdateTo.Value =  dbnull.value
            end if
            myCommandADD.Parameters.Add(prmMPUpdateTo)
            
            
            
            Dim prmDOM As New SqlParameter("@prmDOM", SqlDbType.varchar,50)
				if  chkrunMonthly.checked then
            	prmDOM.Value = dd_weekselect.selecteditem.text
            elseif chkrunQtrly.checked then
            	prmDOM.Value = dd_Monthselect.selecteditem.text
            else
            	prmDOM.Value =  dbnull.value
            end if
            myCommandADD.Parameters.Add(prmDOM)
            
            
            Dim prmSunday As New SqlParameter("@prmSunday", SqlDbType.varchar,2)
				if  chkStepSun.checked then
            	prmSunday.Value = "Y"
            else
            	prmSunday.Value = "N"
            end if
            myCommandADD.Parameters.Add(prmSunday)
            
            
            Dim prmMonday As New SqlParameter("@prmMonday", SqlDbType.varchar,2)
				if  chkStepMon.checked then
            	prmMonday.Value = "Y"
            else
            	prmMonday.Value = "N"
            end if
            myCommandADD.Parameters.Add(prmMonday)
            
            
            Dim prmTuesday As New SqlParameter("@prmTuesday", SqlDbType.varchar,2)
				if  chkStepTue.checked then
            	prmTuesday.Value = "Y"
            else
            	prmTuesday.Value = "N"
            end if
            myCommandADD.Parameters.Add(prmTuesday)
            
            
            Dim prmwed As New SqlParameter("@prmwed", SqlDbType.varchar,2)
				if  chkStepWed.checked then
            	prmwed.Value = "Y"
            else
            	prmwed.Value = "N"
            end if
            myCommandADD.Parameters.Add(prmwed)
            
            
            Dim prmThurs As New SqlParameter("@prmThurs", SqlDbType.varchar,2)
				if  chkStepThu.checked then
            	prmThurs.Value = "Y"
            else
            	prmThurs.Value = "N"
            end if
            myCommandADD.Parameters.Add(prmThurs)
            
            
            Dim prmFriday As New SqlParameter("@prmFriday", SqlDbType.varchar,2)
				if  chkStepFri.checked then
            	prmFriday.Value = "Y"
            else
            	prmFriday.Value = "N"
            end if
            myCommandADD.Parameters.Add(prmFriday)
            
            
            Dim prmSaturday As New SqlParameter("@prmSaturday", SqlDbType.varchar,2)
				if  chkStepSat.checked then
            	prmSaturday.Value = "Y"
            else
            	prmSaturday.Value = "N"
            end if
            myCommandADD.Parameters.Add(prmSaturday)
            
            Dim prmSCHDate As New SqlParameter("@prmschwfdate", SqlDbType.datetime)
				if session("SCHdate")="" then
					prmSCHDate.Value = dbnull.value
				else
					prmSCHDate.Value = session("SCHdate")
				end if
           	myCommandADD.Parameters.Add(prmSCHDate)
            
          
            Try
                myConnectionADD.Open()
                myCommandADD.ExecuteNonQuery()
                myConnectionADD.Close()
            Catch SQLexc As SqlException
                Response.Write("Insert Failed. Error Details are: " & SQLexc.ToString())
            End Try
        End Sub
        
        sub binswfmfields()
         	Dim strUID As String = Session("userid")
            Dim strSql As String = "SELECT * from tbl_WorkFlowMaster Where wfm_tbl_pk=" & Request.QueryString("id")
            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    If Sqldr("wfm_name") IsNot DBNull.Value Then
                        txtname.Text = Sqldr("wfm_name")
                    End If
                    If Sqldr("wfm_descript") IsNot DBNull.Value Then
                        txtdesc.Text = Sqldr("wfm_descript")
                    End If
                    If Sqldr("wfm_effdate") IsNot DBNull.Value Then
                        txtsdate.text = Sqldr("wfm_effdate")
                    End If
                    If Sqldr("wfm_enddate") IsNot DBNull.Value Then
                        txtedate.text = Sqldr("wfm_enddate")
                    End If
                    If Sqldr("wfm_status") IsNot DBNull.Value Then
                        dd_wfstat.SelectedIndex = dd_wfstat.Items.IndexOf(dd_wfstat.Items.FindByText(Sqldr("wfm_status")))
                        if Sqldr("wfm_status")= "Active" then 
                        	bwfactive.text="Make Inactive"
                        else
                        	bwfactive.text="Make Active"
                        end if
                    End If
                    If Sqldr("wfm_trigger") IsNot DBNull.Value Then
                        dd_trigger.SelectedIndex = dd_trigger.Items.IndexOf(dd_trigger.Items.FindByText(Sqldr("wfm_trigger")))
                        if Sqldr("wfm_trigger")="On Lead Status Change" then
				           		lbllsfrom.visible=false
				           		lbllsto.visible=true
				           		dd_leadstatusFrom.visible=false
				           		dd_leadstatusTo.visible=true
				           		
				           		dd_ldstatinc.enabled=false
			           			ddlcstatusFilter.enabled=false
			           			dd_ldpginc.enabled=true
			           			ddlleadprogramFilter.enabled=true
			           			dd_ldtypeinc.enabled=true
			           			ddlleadtypeFilter.enabled=true
			           			dd_MTPinc.enabled=true
			           			ddMKFilter.enabled=true
			           			FillLDtstatFrom("leadstatus")
           						FillLDtstatTo("leadstatus")
				          
				          elseif Sqldr("wfm_trigger")="On Lead Type Change" then
				          		lbllsfrom.visible=false
				           		lbllsto.visible=true
				           		dd_leadstatusFrom.visible=false
				           		dd_leadstatusTo.visible=true
				           		
						         dd_ldstatinc.enabled=true
			           			ddlcstatusFilter.enabled=true
			           			dd_ldpginc.enabled=true
			           			ddlleadprogramFilter.enabled=true
			           			dd_ldtypeinc.enabled=false
			           			ddlleadtypeFilter.enabled=false
			           			dd_MTPinc.enabled=true
			           			ddMKFilter.enabled=true
			           			FillLDtstatFrom("leadtype")
           						FillLDtstatTo("leadtype")
				           elseif Sqldr("wfm_trigger")="On Marketing Program Change" then
				          		lbllsfrom.visible=false
				           		lbllsto.visible=true
				           		dd_leadstatusFrom.visible=false
				           		dd_leadstatusTo.visible=true
				           		
						         dd_ldstatinc.enabled=true
			           			ddlcstatusFilter.enabled=true
			           			dd_ldpginc.enabled=true
			           			ddlleadprogramFilter.enabled=true
			           			dd_ldtypeinc.enabled=true
			           			ddlleadtypeFilter.enabled=true
			           			dd_MTPinc.enabled=false
			           			ddMKFilter.enabled=false
			           			FillLDtstatFrom("marketprogram")
           						FillLDtstatTo("marketprogram")
				          elseif Sqldr("wfm_trigger")="On Lead Program Change" then
				          		lbllsfrom.visible=false
				           		lbllsto.visible=true
				           		dd_leadstatusFrom.visible=false
				           		dd_leadstatusTo.visible=true
				           		
						         dd_ldstatinc.enabled=true
			           			ddlcstatusFilter.enabled=true
			           			dd_ldpginc.enabled=false
			           			ddlleadprogramFilter.enabled=false
			           			dd_ldtypeinc.enabled=true
			           			ddlleadtypeFilter.enabled=true
			           			dd_MTPinc.enabled=true
			           			ddMKFilter.enabled=true
				          		FillLDtstatFrom("leadprogram")
           						FillLDtstatTo("leadprogram")
           				 elseif Sqldr("wfm_trigger")="On A Schedule" then
           				 	lbllsfrom.visible=false
				           		lbllsto.visible=false
           				 
				           else
				           		lbllsfrom.visible=false
				           		lbllsto.visible=false
				           		dd_leadstatusFrom.visible=false
				           		dd_leadstatusTo.visible=false
				          
				           end if 
                    End If
                     If Sqldr("wfm_filter1Usage") IsNot DBNull.Value Then
                        dd_ldtypeinc.SelectedIndex = dd_ldtypeinc.Items.IndexOf(dd_ldtypeinc.Items.FindByText(Sqldr("wfm_filter1Usage")))
                        if Sqldr("wfm_filter1Usage")="Do Not Use" then
                        	ddlleadtypeFilter.enabled=false
                        else
                        	ddlleadtypeFilter.enabled=true
                        	lblwffilterstat.text="This Work Flow has Filtering ON"
                        end if
                        	
                    End If
                    If Sqldr("wfm_filter2Usage") IsNot DBNull.Value Then
                        dd_ldpginc.SelectedIndex = dd_ldpginc.Items.IndexOf(dd_ldpginc.Items.FindByText(Sqldr("wfm_filter2Usage")))
                        if Sqldr("wfm_filter2Usage")="Do Not Use" then
                        	ddlleadprogramFilter.enabled=false
                        else
                        	ddlleadprogramFilter.enabled=true
                        	lblwffilterstat.text="This Work Flow has Filtering ON"
                        end if
                    End If
                     If Sqldr("wfm_filter3Usage") IsNot DBNull.Value Then
                        dd_ldAstatinc.SelectedIndex = dd_ldAstatinc.Items.IndexOf(dd_ldAstatinc.Items.FindByText(Sqldr("wfm_filter3Usage")))
                        if Sqldr("wfm_filter3Usage")="Do Not Use" then
                        	ddlstatusFilter.enabled=false
                        else
                        	ddlstatusFilter.enabled=true
                        	lblwffilterstat.text="This Work Flow has Filtering ON"
                        end if
                    End If
                    If Sqldr("wfm_filter4Usage") IsNot DBNull.Value Then
                        dd_ldstatinc.SelectedIndex = dd_ldstatinc.Items.IndexOf(dd_ldstatinc.Items.FindByText(Sqldr("wfm_filter4Usage")))
                        if Sqldr("wfm_filter4Usage")="Do Not Use" then
                        	ddlcstatusFilter.enabled=false
                        else
                        	ddlcstatusFilter.enabled=true
                        	lblwffilterstat.text="This Work Flow has Filtering ON"
                        end if
                    End If
                    If Sqldr("wfm_filter5Usage") IsNot DBNull.Value Then
                        dd_adsinc.SelectedIndex = dd_adsinc.Items.IndexOf(dd_adsinc.Items.FindByText(Sqldr("wfm_filter5Usage")))
                        if Sqldr("wfm_filter5Usage")="Do Not Use" then
                        	ddadFilter.enabled=false
                        else
                        	ddadFilter.enabled=true
                        	lblwffilterstat.text="This Work Flow has Filtering ON"
                        end if
                    End If
                      If Sqldr("wfm_filter6Usage") IsNot DBNull.Value Then
                        dd_MTPinc.SelectedIndex = dd_MTPinc.Items.IndexOf(dd_MTPinc.Items.FindByText(Sqldr("wfm_filter6Usage")))
                        if Sqldr("wfm_filter6Usage")="Do Not Use" then
                        	ddMKFilter.enabled=false
                        else
                        	ddMKFilter.enabled=true
                        	lblwffilterstat.text="This Work Flow has Filtering ON"
                        end if
                    End If
                    
                    If Sqldr("wfm_leadstatfrom") IsNot DBNull.Value Then
                        dd_leadstatusFrom.SelectedIndex = dd_leadstatusFrom.Items.IndexOf(dd_leadstatusFrom.Items.FindByText(Sqldr("wfm_leadstatfrom")))
                    End If
                    If Sqldr("wfm_leadstatto") IsNot DBNull.Value Then
                        dd_leadstatusTo.SelectedIndex = dd_leadstatusTo.Items.IndexOf(dd_leadstatusTo.Items.FindByText(Sqldr("wfm_leadstatto")))
                    End If
                   
                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        end sub
        
              
               
        Sub btncanceladd(ByVal sender As Object, ByVal e As EventArgs)
            pnladdstep.visible=false
            baddstep.visible=true
           
        End Sub
        
        Sub vwstepinfo(ByVal sender As Object, ByVal e As EventArgs)
            if vstepinfo.text= "View Step Info" then
            	pnladdstep.visible=true
            	vstepinfo.text = "Hide Step Info"
            else
            	pnladdstep.visible=false
            	vstepinfo.text = "View Step Info"
            end if
            btnstepnext.visible=false
                     
            
        End Sub
        
        
         Sub btn_Gen(ByVal sender As Object, ByVal e As EventArgs)
            subnav("General")           
             pnladdstep.visible=false
           	pnlsendemail.visible=false
          
        End Sub
        
        Sub btn_pg1(ByVal sender As Object, ByVal e As EventArgs)
            subnav("Steps")
            pnlstepmain.visible=true
            pnladdstep.visible=false
           pnlsendemail.visible=false
           
        End Sub
        
        Sub btn_pg2(ByVal sender As Object, ByVal e As EventArgs)
            Subnav("Status")
            Ldprocess.cssclass="linkbuttonsRed"
            ldexception.cssclass="linkbuttons"
            pnlinporcess.visible=true
            bindwfleads()
            'pnlstepmain.visible=false
            'pnladdstep.visible=false
           'pnlsendemail.visible=false
        End Sub
        
         Sub Addleadtowf(ByVal sender As Object, ByVal e As EventArgs)
         	session("WFMPK")=request.querystring("id")
           	response.redirect("leads.aspx?search=*&leadtype=*&status=*&constatus=*&assignedto=*&assignedby=*&adcode=*&source=workflow&entrysource=*&program=*&mprogram=*")
           
        End Sub
        
        Sub StepReorder(ByVal sender As Object, ByVal e As EventArgs)
           Dim x As dropdownlist = sender

            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim StepNoPK As String = item.Cells(0).Text
          	' ctype(cell.controls(8),DropDownList) 
            'e.FindControl("dd_Stepreorder")
           	updateStepNew(StepNoPK,x.selecteditem.value )
        End Sub
        
        
        sub updatestepnew(id as string, svalue as string)
        		Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "update tbl_WorkFlowSteps set wfs_newstepno='" & svalue & "' where wfs_tbl_pk='" & id & "'"
           'response.write(strSql)
            
            Try
               strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
               sqlConn = New SqlConnection(strConnection)
               sqlCmd = New SqlCommand(strSql, sqlConn)
               sqlConn.Open()
               Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

               If Sqldr.Read() Then
             
                end if
		
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                sqlConn.Close()
            End Try
			
			
        
        
        
        end sub
        
        
        
        
        
           Sub ItemDataBoundEventHandlerPP(ByVal sender As Object, ByVal e As DataGridItemEventArgs)

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
   				  Dim itemCellPKA As TableCell = e.Item.Cells(0)
        			 Dim itemCellPKTEXTA As String = itemCellPKA.Text
   				 
   				 Dim itemCellPK As TableCell = e.Item.Cells(1)
        			 Dim itemCellPKTEXT As String = itemCellPK.Text
        			 
   				 Dim itemCellStepStat As TableCell = e.Item.Cells(5)
        			 Dim itemCellStepStatTEXT As String = itemCellStepStat.Text
   				
   				Dim DGSteps As dropdownlist
               DGSteps = e.Item.Cells(0).FindControl("dd_Stepreorder")
               Dim DGRemove As button
               DGRemove = e.Item.Cells(0).FindControl("bdeletestep")
              
   				Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
           		Dim myCommand As String = "Select * from tbl_WorkFlowSteps where wfs_wfm_fk='" & session("currentwfpk") & "' and wfs_useridFK='" & session("userid") & "' and wfs_status='Active' order by wfs_stepno"
    				Dim objCmd As New SqlCommand(myCommand, myConnection)
    				Dim dataReader As SqlDataReader = Nothing
    				Try
      				myConnection.Open()
      				dataReader = objCmd.ExecuteReader()
      				DGSteps.DataSource = dataReader
      				DGSteps.DataTextField = "wfs_stepno"
      				DGSteps.DataValueField = "wfs_stepno"
      				DGSteps.DataBind()
    					Catch exc As System.Exception
      					Response.Write(exc.ToString())
    					Finally
      					myConnection.Dispose()
    					End Try
    					'DGSteps.Items.Insert(0, New ListItem("Select", "9999"))
   				  	DGSteps.SelectedIndex = DGSteps.Items.IndexOf(DGSteps.Items.FindBytext(itemCellPKTEXT))
   				  
   				  	updateStepNew(itemCellPKTEXTA,itemCellPKTEXT )
   				  
   				  
   				  	if itemCellStepStatTEXT = "Active" then
     						DGRemove.text="Remove"
     					else
     						DGRemove.text="Add"
     					end if
     			
     			End If
     			
     		

        End Sub
        
      Sub filterVenues(ByVal Source As System.Object, ByVal e As System.EventArgs)
        bindwfsteps()
       
       end sub
        
        
        
        Sub subnav(ByVal button As String)
            Dim clickedbutton As String = button

            'Set cell class
            subnavGen.Attributes.Add("class", "tblcelltest")
            subnavPage1.Attributes.Add("class", "tblcelltest")
            subnavPage2.Attributes.Add("class", "tblcelltest") 
                     
            'Set button font color
            Lgen.ForeColor = System.Drawing.Color.Black
            lpage1.ForeColor = System.Drawing.Color.Black
            lpage2.ForeColor = System.Drawing.Color.Black  
            
            'Set spacers
            spacer0.Visible = True
            spacer1.Visible = True
            spacer2.Visible = True
           

            'Set Panels
            pnlgeneral.Visible = False
            pnlpage1.Visible = False
           	pnlpage2.Visible = False
           
            If clickedbutton = "General" Then
                subnavGen.Attributes.Add("class", "tblcelltestSelected")
                Lgen.ForeColor = System.Drawing.Color.White
                spacer0.Visible = true
                pnlgeneral.Visible = True
            ElseIf clickedbutton = "Steps" Then
                subnavPage1.Attributes.Add("class", "tblcelltestSelected")
                lpage1.ForeColor = System.Drawing.Color.White
                spacer0.Visible = True
                spacer1.Visible = True               
                pnlpage1.Visible = True
           
            ElseIf clickedbutton = "Status" Then
            	subnavPage2.Attributes.Add("class", "tblcelltestSelected")
                lpage2.ForeColor = System.Drawing.Color.White
                spacer0.Visible = True
                spacer1.Visible = True               
                pnlpage2.Visible = True
            
            else
             	subnavGen.Attributes.Add("class", "tblcelltestSelected")
                Lgen.ForeColor = System.Drawing.Color.White
                spacer0.Visible = False
                pnlgeneral.Visible = True          
            End If
        End Sub
        
        Sub ItemDataBoundEventHandlerA(ByVal sender As Object, ByVal e As DataGridItemEventArgs)

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            	
		    	end if
		    	If e.Item.ItemType = ListItemType.Header Then
					 Dim testbtn As linkbutton            	 
            	 testbtn = e.Item.Cells(3).FindControl("vwbodyA")
            	 if bdlevel="Full" then
	            	testbtn.text="[Brief View]"            	
	           	else
	           		testbtn.text="[Detail View]"
	           	end if
				End If

        end sub
        
       Sub Removestep(ByVal Source As System.Object, ByVal e As System.EventArgs)
        	Dim x As Button = Source
         Dim cell As TableCell = x.Parent
         Dim item As DataGridItem = cell.Parent
         Dim wfstepPK As String = item.Cells(0).Text
         Dim wfstepNO As String = item.Cells(1).Text
         Dim wfstepStat As String = item.Cells(5).Text
       	session("wfstepPK")=item.Cells(0).Text
       	session("WFMPK")=request.querystring("id")
       	if wfstepStat = "Active" then
       		updatSstat(wfstepPK,"Inactive","")
       		reorderactivesteps(wfstepNO)
       	else
       		getnextstepno()
       		updatSstat(wfstepPK,"Active", session("newstepno"))
       	end if
       	 bindwfsteps()
       end sub
      
      sub reorderactivesteps(id as string)
       		Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String  
            
            strSql="begin declare @x as int set @x='" & id & "' update tbl_WorkFlowSteps set wfs_stepno = @x+(wfs_stepno-@x-1) " _
							& "from dbo.tbl_WorkFlowSteps where wfs_wfm_fk='" & session("WFMPK") & "' and wfs_status ='Active' and wfs_stepno > @x " _
							& "end"
           
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
       
          			end if
                
            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
       
      
      end sub
    
       
     



       
       sub updatSstat(id as string, stat as string, newstepno as string)
       
        Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String  
            if newstepno="" then
            	strSql = "update tbl_WorkFlowSteps set wfs_status='" & stat & "' where wfs_tbl_pk='" & id & "'"
       	   else
            	strSql = "update tbl_WorkFlowSteps set wfs_status='" & stat & "', wfs_stepno='" & newstepno & "' where wfs_tbl_pk='" & id & "'"
            end if
            
           
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
       
          			end if
                
            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
       
       
       
       end sub
        
        
        
        
        
        
        Sub Editstep(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim wfstepPK As String = item.Cells(0).Text
            Dim wfstepAction As String = item.Cells(4).Text
            session("wfstepPK")=item.Cells(0).Text
				session("stepaction")="edit"
				pnlstepmain.visible=false
				pnladdstep.visible=true
				'btnstepnext.visible=false
				pnlstepdetails.visible=false
				pnlstepconditions.visible=true
				sconditions.cssclass="linkbuttonsRed"
            sdetail.cssclass="linkbuttons"
				
				fillwfactions()
           	fillprevsteps()
           	fillfreq()
           	fillduration()
           	fillstartdate()
				if wfstepAction="Send Email" then
					pnlsendemail.visible=true
					pnl_addtask.visible=false
					pnlemailto.visible=false
					pnlleaddds.visible=false
					Fillemailcor()
					TextBox1.focus()
				elseif wfstepAction="Create Task" then
					pnlsendemail.visible=false
					pnl_addtask.visible=true
					pnlleaddds.visible=false
					pnlemailto.visible=false
					bindtaskstat()
            	bindtasktype() 
            	ddtasktype.focus()  
				elseif wfstepAction="Send Notification" then
					pnlemailto.visible=true
					pnlsendemail.visible=true
					pnl_addtask.visible=false
					pnlleaddds.visible=false
					Fillemailcor()
				elseif wfstepAction="Update Lead Info" then
					pnlsendemail.visible=false
					pnl_addtask.visible=false
					pnlleaddds.visible=true
					pnlemailto.visible=false
					FillstatusDropDown2()
	        	   FillstatusDropDown2A()
	            FillLeadTypeDropDown2()
	            FillLeadprogramDropDown2()
	            FillLeadTypeDropDown2A()
	            FillLeadprogramDropDown2A()
	            FillMarketDropDown2()
	            FillMarketDropDown2A()
					
					
					
				end if
				bindstepfields(wfstepPK)
				

        End Sub
        
        
        Sub bindstepfields(id as string)
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "Select * from tbl_WorkFlowSteps where wfs_tbl_pk = '" & id & "'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                	lblstepno.text=Sqldr("wfs_stepno")
                	If Sqldr("wfs_Desc") isnot dbnull.value then
                        txtdescA.text=Sqldr("wfs_Desc")
                  end if
                  If Sqldr("wfs_type") IsNot DBNull.Value Then
                     dd_action.SelectedIndex = dd_action.Items.IndexOf(dd_action.Items.FindBytext(Sqldr("wfs_type")))
                  end if
                  If Sqldr("wfs_Freq") IsNot DBNull.Value Then
                     if Sqldr("wfs_Freq")="Once" then
                     	chkrunonce.checked=true
                     	chkrundaily.checked=false
                     	chkrunweekly.checked=false
                     	chkrunMonthly.checked=false
                     	chkrunQtrly.checked=false
                     	pnlselectdom.visible=false
                     	pnlselectdoW.visible=false
                     	pnlselectdomQ.visible=false
                     	lblsdateoffest.visible=true
                     	sdoffset.visible=true
                     elseif Sqldr("wfs_Freq")="Daily" then
                     	chkrundaily.checked=true
                     	chkrunonce.checked=false
                     	chkrunweekly.checked=false
                     	chkrunMonthly.checked=false
                     	chkrunQtrly.checked=false
                     	pnlselectdom.visible=false
                     	pnlselectdoW.visible=false
                     	pnlselectdomQ.visible=false
                     elseif Sqldr("wfs_Freq")="Weekly" then
                      	chkrunweekly.checked=true
                      	chkrundaily.checked=false
                     	chkrunonce.checked=false                     	
                     	chkrunMonthly.checked=false
                     	chkrunQtrly.checked=false
                     	pnlselectdom.visible=false
                     	pnlselectdoW.visible=true
                     elseif Sqldr("wfs_Freq")="Monthly" then
                    		chkrunMonthly.checked=true
                    		chkrunweekly.checked=false
                     	chkrundaily.checked=false
                     	chkrunonce.checked=false  
                     	chkrunQtrly.checked=false
                     	pnlselectdom.visible=true
                     	pnlselectdoW.visible=false  
                     	pnlselectdomQ.visible=false
                     elseif Sqldr("wfs_Freq")="Quarterly" then
                    		chkrunQtrly.checked=true
                    		chkrunMonthly.checked=false
                    		chkrunweekly.checked=false
                     	chkrundaily.checked=false
                     	chkrunonce.checked=false  
                     	pnlselectdom.visible=false
                     	pnlselectdoW.visible=false  
                     	pnlselectdomQ.visible=true
                     	
                     elseif Sqldr("wfs_Freq")="Schedule" then
                    		chkrunSched.checked=true
                    		chkrunQtrly.checked=false
                    		chkrunMonthly.checked=false
                    		chkrunweekly.checked=false
                     	chkrundaily.checked=false
                     	chkrunonce.checked=false  
                     	pnlselectdom.visible=false
                     	pnlselectdoW.visible=false  
                     	pnlselectdomQ.visible=false
                     	pnlSdateMain.visible=false
                     	pnlCalendart.visible=true
                     	
                     end if
                  end if
                  If Sqldr("wfs_durationamt") isnot dbnull.value then
                      dno.text=Sqldr("wfs_durationamt")
                  end if
                  If Sqldr("wfs_duration") IsNot DBNull.Value Then
                     dd_duration.SelectedIndex = dd_duration.Items.IndexOf(dd_duration.Items.FindBytext(Sqldr("wfs_duration")))
                  end if
                   If Sqldr("wfs_DependantStep") IsNot DBNull.Value Then
                     dd_dstep.SelectedIndex = dd_dstep.Items.IndexOf(dd_dstep.Items.FindByvalue(Sqldr("wfs_DependantStep")))
                  end if
                  If Sqldr("wfs_condition") IsNot DBNull.Value Then
                     dd_condition.SelectedIndex = dd_condition.Items.IndexOf(dd_condition.Items.FindBytext(Sqldr("wfs_condition")))
                  end if
                  If Sqldr("wfs_conditionYes") IsNot DBNull.Value Then
                     dd_emailcond.SelectedIndex = dd_emailcond.Items.IndexOf(dd_emailcond.Items.FindBytext(Sqldr("wfs_conditionYes")))
                  end if
                  If Sqldr("wfs_basedate") IsNot DBNull.Value Then
                     dd_sdate.SelectedIndex = dd_sdate.Items.IndexOf(dd_sdate.Items.FindBytext(Sqldr("wfs_basedate")))
                  end if
                  If Sqldr("wfs_basedateoffset") isnot dbnull.value then
                      sdoffset.text=Sqldr("wfs_basedateoffset")
                  end if
                  If Sqldr("wfs_emailfrom") isnot dbnull.value then
                      TextBox1.text=Sqldr("wfs_emailfrom")
                  end if
                   If Sqldr("wfs_Emailsubject") isnot dbnull.value then
                      TextBox3.text=Sqldr("wfs_Emailsubject")
                  end if
                  If Sqldr("wfs_emailbody") isnot dbnull.value then
                      emailbody.content=Sqldr("wfs_emailbody")
                  end if
                  If Sqldr("wfs_emailto") isnot dbnull.value then
                      selfemailaddress.text=Sqldr("wfs_emailto")
                  end if
                  If Sqldr("wfs_leaddetail") IsNot DBNull.Value Then
                     dd_status.SelectedIndex = dd_status.Items.IndexOf(dd_status.Items.FindBytext(Sqldr("wfs_leaddetail")))
                  end if
                   If Sqldr("wfs_tasktype") IsNot DBNull.Value Then
                     ddtasktype.SelectedIndex = ddtasktype.Items.IndexOf(ddtasktype.Items.FindBytext(Sqldr("wfs_tasktype")))
                  end if
                  If Sqldr("wfs_taskstat") IsNot DBNull.Value Then
                     ddtaskstat.SelectedIndex = ddtaskstat.Items.IndexOf(ddtaskstat.Items.FindBytext(Sqldr("wfs_taskstat")))
                  end if
                 
                   If Sqldr("wfs_taskremind") IsNot DBNull.Value Then
                   	if Sqldr("wfs_taskremind")="Yes" then
                   		l_treminder.enabled=true
                   		l_treminderEM.enabled=true
                   	else
                   		l_treminder.enabled=false
                   		l_treminderEM.enabled=false
                   	end if
                     dd_treminder.SelectedIndex = dd_treminder.Items.IndexOf(dd_treminder.Items.FindBytext(Sqldr("wfs_taskremind")))
                  end if
                  If Sqldr("wfs_taskdaybefore") isnot dbnull.value then
                      l_treminder.text=Sqldr("wfs_taskdaybefore")
                  end if
                  If Sqldr("wfs_taskemailto") isnot dbnull.value then
                      l_treminderEM.text=Sqldr("wfs_taskemailto")
                  end if
                  If Sqldr("wfs_taskdesc") isnot dbnull.value then
                      hst_action.content=Sqldr("wfs_taskdesc")
                  end if
                  If Sqldr("wfs_lsupdate") isnot dbnull.value then
                      if Sqldr("wfs_lsupdate")="Y" then
                      	chklsupdate.checked=true
                      else
                      	chklsupdate.checked=false
                      end if
                   
                  end if
                  If Sqldr("wfs_ltupdate") isnot dbnull.value then
                      if Sqldr("wfs_ltupdate")="Y" then
                      	chkltupdate.checked=true
                      else
                      	chkltupdate.checked=false
                      end if
                   
                  end if
                   If Sqldr("wfs_lpupdate") isnot dbnull.value then
                      if Sqldr("wfs_lpupdate")="Y" then
                      	chklpupdate.checked=true
                      else
                      	chklpupdate.checked=false
                      end if
                   
                  end if
                  
                     If Sqldr("wfs_MPupdate") isnot dbnull.value then
                      if Sqldr("wfs_MPupdate")="Y" then
                      	chkMPupdate.checked=true
                      else
                      	chkMPupdate.checked=false
                      end if
                   
                  end if
                  
                  
                  
                  If Sqldr("wfs_lsfrom") isnot dbnull.value then
                    ddlstatusFilter2.SelectedIndex = ddlstatusFilter2.Items.IndexOf(ddlstatusFilter2.Items.FindBytext(Sqldr("wfs_lsfrom")))
               
                  end if
                  If Sqldr("wfs_lsto") isnot dbnull.value then
                    ddlstatusFilter2A.SelectedIndex = ddlstatusFilter2A.Items.IndexOf(ddlstatusFilter2A.Items.FindBytext(Sqldr("wfs_lsto")))
               
                  end if
                   If Sqldr("wfs_ltfrom") isnot dbnull.value then
                    ddlleadtypeFilter2.SelectedIndex = ddlleadtypeFilter2.Items.IndexOf(ddlleadtypeFilter2.Items.FindBytext(Sqldr("wfs_ltfrom")))
               
                  end if
                    If Sqldr("wfs_ltto") isnot dbnull.value then
                    ddlleadtypeFilter2A.SelectedIndex = ddlleadtypeFilter2A.Items.IndexOf(ddlleadtypeFilter2A.Items.FindBytext(Sqldr("wfs_ltto")))
               
                  end if
                   If Sqldr("wfs_lpfrom") isnot dbnull.value then
                    ddlleadprogramFilter2.SelectedIndex = ddlleadprogramFilter2.Items.IndexOf(ddlleadprogramFilter2.Items.FindBytext(Sqldr("wfs_lpfrom")))
               
                  end if
                   If Sqldr("wfs_lpto") isnot dbnull.value then
                    ddlleadprogramFilter2A.SelectedIndex = ddlleadprogramFilter2A.Items.IndexOf(ddlleadprogramFilter2A.Items.FindBytext(Sqldr("wfs_lpto")))
               
                  end if
                  If Sqldr("wfs_Mpfrom") isnot dbnull.value then
                    ddlMarketprogramFilter2.SelectedIndex = ddlMarketprogramFilter2.Items.IndexOf(ddlMarketprogramFilter2.Items.FindBytext(Sqldr("wfs_Mpfrom")))
               
                  end if
                   If Sqldr("wfs_MPTo") isnot dbnull.value then
                    ddlMarketprogramFilter2A.SelectedIndex = ddlMarketprogramFilter2A.Items.IndexOf(ddlMarketprogramFilter2A.Items.FindBytext(Sqldr("wfs_MPTo")))
               
                  end if
                  
                   If Sqldr("wfs_DayofMonth") isnot dbnull.value then
                    dd_weekselect.SelectedIndex = dd_weekselect.Items.IndexOf(dd_weekselect.Items.FindBytext(Sqldr("wfs_DayofMonth")))
              	  		dd_Monthselect.SelectedIndex = dd_Monthselect.Items.IndexOf(dd_Monthselect.Items.FindBytext(Sqldr("wfs_DayofMonth")))
                  end if
                  If Sqldr("wfs_Sunday") isnot dbnull.value then
                    if Sqldr("wfs_Sunday") = "Y" then
                    		chkStepSun.checked=true
                    else
                    		chkStepSun.checked=false
                    end if               
                  end if
                   If Sqldr("Wfs_Monday") isnot dbnull.value then
                    if Sqldr("Wfs_Monday") = "Y" then
                    		chkStepMon.checked=true
                    else
                    		chkStepMon.checked=false
                    end if               
                  end if
                   If Sqldr("wfs_Tuesday") isnot dbnull.value then
                    if Sqldr("wfs_Tuesday") = "Y" then
                    		chkStepTue.checked=true
                    else
                    		chkStepTue.checked=false
                    end if               
                  end if
                   If Sqldr("wfs_Wednesday") isnot dbnull.value then
                    if Sqldr("wfs_Wednesday") = "Y" then
                    		chkStepWed.checked=true
                    else
                    		chkStepWed.checked=false
                    end if               
                  end if
                   If Sqldr("wfs_Thursday") isnot dbnull.value then
                    if Sqldr("wfs_Thursday") = "Y" then
                    		chkStepThu.checked=true
                    else
                    		chkStepThu.checked=false
                    end if               
                  end if
                   If Sqldr("wfs_Friday") isnot dbnull.value then
                    if Sqldr("wfs_Friday") = "Y" then
                    		chkStepFri.checked=true
                    else
                    		chkStepFri.checked=false
                    end if               
                  end if
                   If Sqldr("wfs_Saturday") isnot dbnull.value then
                    if Sqldr("wfs_Saturday") = "Y" then
                    		chkStepSat.checked=true
                    else
                    		chkStepSat.checked=false
                    end if               
                  end if
                   
                    If Sqldr("wfs_ScheduleSelectedDate") IsNot DBNull.Value Then
                        cdrCalendarWFS.SelectedDates.Add(Sqldr("wfs_ScheduleSelectedDate"))
                    End If

                  
               end if
                
            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
        end sub
        
        sub CancelWFGen(ByVal Source As System.Object, ByVal e As System.EventArgs)
        		session("currentwfpk")=""
        		Session("apdate")=""
        		session("stepaction")=""
        		 session("wfstepPK")=""
       		response.redirect("workflow.aspx")
        end sub
        
         sub CloneWFGen(ByVal Source As System.Object, ByVal e As System.EventArgs)
        		 Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String
            sqlproc = "sp_clonewfmaster"
            
            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
                Dim prmSpk As New SqlParameter("@wftblpk", SqlDbType.Int)
                prmSpk.Value = request.querystring("id")
                myCommandADD.Parameters.Add(prmSpk)
           
            Try
                myConnectionADD.Open()
                myCommandADD.ExecuteNonQuery()
                myConnectionADD.Close()
            Catch SQLexc As SqlException
                Response.Write("Insert Failed. Error Details are: " & SQLexc.ToString())
            End Try
            dim newwfpk as string =getclwfpk()
            insertclonesteps(newwfpk)
            insertclonefilters(newwfpk)
            response.redirect("addeditwf.aspx?action=view&id=" & newwfpk )
        end sub
            
        sub  insertclonesteps(id as string)         
             Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String
            sqlproc = "sp_clonewfsteps"
            
            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure            
           ' Add Parameters to SPROC
                Dim prmSSpk As New SqlParameter("@wftblpk", SqlDbType.Int)
                prmSSpk.Value = request.querystring("id")
                myCommandADD.Parameters.Add(prmSSpk)
                 
                Dim prmNpk As New SqlParameter("@newwfpk", SqlDbType.Int)
                prmNpk.Value = id
                myCommandADD.Parameters.Add(prmNpk)
                
            
            Try
                myConnectionADD.Open()
                myCommandADD.ExecuteNonQuery()
                myConnectionADD.Close()
            Catch SQLexc As SqlException
                Response.Write("Insert Failed. Error Details are: " & SQLexc.ToString())
            End Try
                    		
        end sub
        
         sub  insertclonefilters(id as string)         
             Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String
            sqlproc = "sp_clonewffilters"
            
            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure            
           ' Add Parameters to SPROC
                Dim prmSSpk As New SqlParameter("@wftblpk", SqlDbType.Int)
                prmSSpk.Value = request.querystring("id")
                myCommandADD.Parameters.Add(prmSSpk)
                 
                Dim prmNpk As New SqlParameter("@newwfpk", SqlDbType.Int)
                prmNpk.Value = id
                myCommandADD.Parameters.Add(prmNpk)
                
            
            Try
                myConnectionADD.Open()
                myCommandADD.ExecuteNonQuery()
                myConnectionADD.Close()
            Catch SQLexc As SqlException
                Response.Write("Insert Failed. Error Details are: " & SQLexc.ToString())
            End Try
                    		
        end sub
        
        
        public function  getclwfpk() as string
        		Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "Select max(wfm_tbl_pk) as 'newwfpk' from tbl_WorkFlowMaster where wfm_useridfk = '" & session("userid") & "'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                	return sqldr("newwfpk")
                else
                	return 0
                end if
                  
            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
        
        
        
        end function
        
        
        
         Sub namecheckdup(ByVal sender As Object, ByVal e As EventArgs)
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select * from dbo.tbl_WorkFlowMaster where wfm_name = '" & txtname.text  & "' and wfm_useridfk ='" & session("userid") & "'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    txtname.text = "Duplicate Name!  Can not use."
				        txtname.BackColor = Red
				        txtname.focus()
                Else
                  txtname.BackColor = white
                  txtdesc.focus()
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
            
        End Sub
        
        sub MakeActiveWFGen(ByVal Source As System.Object, ByVal e As System.EventArgs)
       	if bwfactive.text="Make Active" then
       		bwfactive.text="Make Inactive" 
       		dd_wfstat.SelectedIndex = dd_wfstat.Items.IndexOf(dd_wfstat.Items.FindByText("Active"))
         else
       		bwfactive.text="Make Active" 
       		dd_wfstat.SelectedIndex = dd_wfstat.Items.IndexOf(dd_wfstat.Items.FindByText("Inactive"))
       	
       	end if
       	SaveWFGenA()
       
        end sub
        
        
       sub SaveWFGen(ByVal Source As System.Object, ByVal e As System.EventArgs)
       	Dim x As Button = Source
       	SaveWFGenA()
       	
       	if Request.QueryString("action")= "new" then
       		if x.id = "bsaveGen" then
       			response.redirect("addeditwf.aspx?action=view&id=" & session("currentwfpk"))
       		else 
       			response.redirect("addeditwf.aspx?action=view&id=" & session("currentwfpk") & "&addstep=yes")
       		end if
       	end if
       end sub
        
        public sub SaveWFGenA()
        	insertwfm()
       	if Request.QueryString("action")= "new" then
       		getwfpk()
       	end if
       	if dd_ldtypeinc.selecteditem.text <> "Do Not Use" then
       		SaveTypes("LeadType")
       	end if
       	if dd_ldpginc.selecteditem.text <> "Do Not Use" then
       		SaveTypes("LeadProgram")
       	end if
       	if dd_ldAstatinc.selecteditem.text <> "Do Not Use" then
       		SaveTypes("LeadStatusA")
       	end if
       	if dd_ldstatinc.selecteditem.text <> "Do Not Use" then
       		SaveTypes("LeadStatus")
       	end if
       	if dd_adsinc.selecteditem.text <> "Do Not Use" then
       		SaveTypes("AD")
       	end if
       	if dd_MTPinc.selecteditem.text <> "Do Not Use" then
       		SaveTypes("MarketProgram")
       	end if
        end sub
        
       sub SaveTypes(ldtype as string)
			if Request.QueryString("action")= "view" then			
				Dim strSql as String 
				strSql = "delete from dbo.tbl_WorkFlowFilters where wffilters_wfm_fk='" & session("currentwfpk") & "' and wffilters_userid_fk='" & session("userid") & "' and wffilters_type='" & ldtype & "'"
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
			Dim i As Integer   
			dim x as integer

			if ldtype="LeadType" then
				x=ddlleadtypeFilter.items.count-1
			elseif ldtype="LeadProgram" then 
				x=ddlleadprogramFilter.items.count-1
			elseif ldtype="LeadStatusA" then 
				x=ddlstatusFilter.items.count-1
			elseif ldtype="LeadStatus" then 
				x=ddlcstatusFilter.items.count-1
			elseif ldtype="AD" then 
				x=ddadFilter.items.count-1
			elseif ldtype="MarketProgram" then 
				x=ddMKFilter.items.count-1	
				
			end if
			for i=0 to x
				if ldtype="LeadType" then
					if (ddlleadtypeFilter.Items(i).Selected) then
							dosavetype(ldtype,Convert.ToString(ddlleadtypeFilter.items(i).value))
					end if
				end if
				if ldtype="LeadProgram" then
					if (ddlleadprogramFilter.Items(i).Selected) then
							dosavetype(ldtype,Convert.ToString(ddlleadprogramFilter.items(i).value))
					end if
				end if
				if ldtype="LeadStatusA" then
					if (ddlstatusFilter.Items(i).Selected) then
							dosavetype(ldtype,Convert.ToString(ddlstatusFilter.items(i).value))
					end if
				end if
				if ldtype="LeadStatus" then
					if (ddlcstatusFilter.Items(i).Selected) then
							dosavetype(ldtype,Convert.ToString(ddlcstatusFilter.items(i).value))
					end if
				end if
				if ldtype="AD" then
					if (ddadFilter.Items(i).Selected) then
							dosavetype(ldtype,Convert.ToString(ddadFilter.items(i).value))
					end if
				end if
				if ldtype="MarketProgram" then
					if (ddMKFilter.Items(i).Selected) then
							dosavetype(ldtype,Convert.ToString(ddMKFilter.items(i).value))
					end if
				end if
				
				
			next
		end sub
		
			sub dosavetype(sldtype as string, svalue as string)
			
					Dim rightNow as DateTime = DateTime.Now.toShortDateString() 
		   		Dim RightNowAdd as datetime = datetime.now
		   		Dim supportedFormats() As String = New String() {"M/dd/yyyy","M/d/yyyy","MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
				   Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
		  			dim sqlproc as string
		  			sqlproc = "sp_insertWFLeadtypes"
		  			Dim myCommandADD As New SqlCommand( sqlproc, myConnectionADD)
		  		 	myCommandADD.CommandType = CommandType.StoredProcedure
		   	  	   	  	
			  		Dim prmWFMPK As New SqlParameter("@WFMPK", SqlDbType.int)
					prmWFMPK.Value = session("currentwfpk")
			  		myCommandADD.Parameters.Add(prmWFMPK)
			  		   	  	
			  		Dim prmUserID As New SqlParameter("@userid", SqlDbType.varchar,50)
					prmUserID.Value = session("userid")
			  		myCommandADD.Parameters.Add(prmUserID)
			  		
			  		Dim prmtype As New SqlParameter("@type", SqlDbType.varchar,50)
					prmtype.Value = sldtype
			  		myCommandADD.Parameters.Add(prmtype)
			  		
			  		Dim prmtypeValue As New SqlParameter("@typeValue", SqlDbType.varchar,50)
			  		
						prmtypeValue.Value = svalue
					
			  		myCommandADD.Parameters.Add(prmtypeValue)
			  		
			  		
			  		Try
		               myConnectionADD.Open()
		               myCommandADD.ExecuteNonQuery()
		               myConnectionADD.Close()
		                Catch SQLexc As SqlException
		                      Response.Write("Insert Failed. Error Details are: " & SQLexc.ToString())
		                 End Try
		  
			 
			end sub
			
			sub bindFilters()
				Dim i As Integer   
				'Lead Filter First
				for i=0 to ddlleadtypeFilter.items.count-1
					if checkfilterselected(Convert.ToString(ddlleadtypeFilter.items(i).value),"LeadType" ) then
						ddlleadtypeFilter.Items(i).Selected=true
					end if
				next 
				for i=0 to ddlleadprogramFilter.items.count-1
					if checkfilterselected(Convert.ToString(ddlleadprogramFilter.items(i).value),"LeadProgram" ) then
						ddlleadprogramFilter.Items(i).Selected=true
					end if
				next 
				for i=0 to ddlstatusFilter.items.count-1
					if checkfilterselected(Convert.ToString(ddlstatusFilter.items(i).value),"LeadStatusA" ) then
						ddlstatusFilter.Items(i).Selected=true
					end if
				next 
				for i=0 to ddlcstatusFilter.items.count-1
					if checkfilterselected(Convert.ToString(ddlcstatusFilter.items(i).value),"LeadStatus" ) then
						ddlcstatusFilter.Items(i).Selected=true
					end if
				next 
				for i=0 to ddadFilter.items.count-1
					if checkfilterselected(Convert.ToString(ddadFilter.items(i).value),"AD" ) then
						ddadFilter.Items(i).Selected=true
					end if
				next 
				for i=0 to ddMKFilter.items.count-1
					if checkfilterselected(Convert.ToString(ddMKFilter.items(i).value),"MarketProgram" ) then
						ddMKFilter.Items(i).Selected=true
					end if
				next 
				
				       
			end sub
			
			public function checkfilterselected(chkvalue as string, type as string) as boolean
			  'response.write(request.querystring("id"))
			  'response.write(chkvalue)
			  'response.write(type)
			  
			  Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "Select * from tbl_WorkFlowFilters where wffilters_wfm_fk = '" & request.querystring("id") & "' " _
            								& "and wffilters_userid_fk='" & session("userid") & "' and wffilters_type='" & type & "' " _
            								& "and wffilters_value='" & chkvalue & "'"
           'response.write(strSql)
            
            Try
               strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
               sqlConn = New SqlConnection(strConnection)
               sqlCmd = New SqlCommand(strSql, sqlConn)
               sqlConn.Open()
               Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

               If Sqldr.Read() Then
              	return true
             else
               	return false
                end if
		
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                sqlConn.Close()
            End Try
			
			
			end function
			
			
			 Sub showcalendar2(ByVal sender As Object, ByVal e As EventArgs)
            Dim x As LinkButton = sender
            Session("apdate") = x.ID
            cdrCalendar2.visible = True
            showcalc2.visible = True
        End Sub
        
        Sub showcalendar(ByVal sender As Object, ByVal e As EventArgs)
            Dim x As LinkButton = sender
            Session("apdate") = x.ID
            cdrCalendar.visible = True
            showcalc.visible = True
        End Sub
        
        Sub closecalendar(ByVal sender As Object, ByVal e As EventArgs)
           	showcalc.visible = false
            cdrCalendar.visible = false
            showcalc2.visible = false
            cdrCalendar2.visible = false
        End Sub
        
        
        
        
         Public Sub Calendar1_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
            If Session("apdate") = "showcal1" Then
                txtsdate.Text = cdrCalendar.SelectedDate
            Else
                txtedate.Text = cdrCalendar.SelectedDate

            End If

           showcalc.visible = false
            cdrCalendar.visible = false
        End Sub
        
         Public Sub Calendar2_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
          	session("SCHdate") = cdrCalendarWFS.SelectedDate
        End Sub
			        
        ' Public Sub pagesetup()
 			'	'width will be calculated automatically, but it is sometimes
         '   layout.Width = System.Configuration.ConfigurationManager.AppSettings("ScreenWidth")
         '   leftNav.Width = System.Configuration.ConfigurationManager.AppSettings("ScreenLeftNav")
         '   body.Height = System.Configuration.ConfigurationManager.AppSettings("ScreenBodyHeight")
         '   body.Width = System.Configuration.ConfigurationManager.AppSettings("ScreenBodyWidth")
         '   layout.Border = System.Configuration.ConfigurationManager.AppSettings("ScreenBorder")
         '   footer.Controls.Add(LoadControl(System.Configuration.ConfigurationManager.AppSettings("ScreenFooter")))
         '   Header.Controls.Add(LoadControl(System.Configuration.ConfigurationManager.AppSettings("ScreenHeader")))
         '   leftNav.Controls.Add(LoadControl(System.Configuration.ConfigurationManager.AppSettings("ScreenLeftNavSetup")))
 		'		MiddleNav.Controls.Add(LoadControl(System.Configuration.ConfigurationManager.AppSettings("ScreenmiddleNav")))
       '   
       '     body.VAlign = "top"
       '     leftNav.VAlign = "top"'
'
 '           'LeftNav.Controls.Add(new LiteralControl("Some text."))
  '          'rightNav.VAlign = "top"           
   '         'adjust size of LeftNav (just for the heck of it)           
'
 '           'LeftNav.Controls.Add(LoadControl("navigation.ascx"));
  '          'LeftNav.Controls.Add(new LiteralControl("Some text."));
'
 '           'adjust size of LeftNav (just for the heck of it)
  '          'LeftNav.Width = "100";
'
 '           'RightNav.Controls.Add(LoadControl("quicklink1.ascx"))
  '          'MiddleNav.Controls.Add(LoadControl("userid.ascx"))
'
 '           
'
'        End Sub
        
         Sub bindwfleads()
            Dim strpropID As String = Request.QueryString("id")
            Dim strUID As String = Session("userid")
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
			   mycommand = "select distinct lwfs_lead_fk,cast(lwfs_lead_fk as varchar(255)) as 'wfpk',  ld_fname + ' ' + ld_lname as 'LeadName',ld_hphone,ld_email from dbo.tbl_leadWorkFlowsStatus " _
			   				& "join tbl_leads on tbl_leads_pk=lwfs_lead_fk where lwfs_wfm_fk='" & request.querystring("id") & "'"
            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_LeadWorkFlows")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_LeadWorkFlows"))
					 dvProducts.RowFilter = "wfpk like '%'"
                if session("PubSearchFWF")="true" then
                	dvProducts.RowFilter = dvProducts.RowFilter + " and (wfpk like '%" & wflsearch.text & "%' or LeadName like '%" & wflsearch.text & "%' or ld_hphone like '%" & wflsearch.text & "%' or ld_email like '%" & wflsearch.text & "%')"
                end if
                WFLeads.DataSource = dvProducts
                WFLeads.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
        End Sub
        
          Sub vwfdetails(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(0).Text
            response.redirect("wfstepdetails.aspx?id=" & request.querystring("id") & "&nav=wfsetup&leadno=" & content )

        End Sub
        
         Sub showleaddetails(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(0).Text
            response.redirect("addlead.aspx?action=view&id=" & content & "&source=wfsetup&wfpk=" & request.querystring("id"))
           

        End Sub
        
        
        
        
          Sub removewf(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(0).Text
            removeleadfromWF(content)
				bindwfleads()           

        End Sub
        
        Sub removeleadfromWF(ByVal id As String)
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "update tbl_leadWorkFlowsStatus set lwfs_leadststatus='Inactive' where lwfs_wfm_fk ='" & id & "' and lwfs_lead_fk='" & request.querystring("id") & "'"
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

    End Class
end namespace