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
Imports FreeTextBoxControls


Namespace PageTemplate
    Public Class createad
        Inherits PageTemplate

        Public lb_leadno, lbl_adnoV,adstage,adtitleM,finwarning,autoc As Label
        Public pnlldsource, pnlALL, pnldrspwarn, admain, placead, pnlsavedv, pnlconfirmfinalize, pnlnoadvenues, pnladdvenue, pnltxtbody, pnladtitle, pnlbuttons, pnladsubmain As Panel
        public pnlapfreq as panel
        Public adposts, ADVenues,dgimages,ADPlans,ADVenuesPP,tmpavchk,temppreview As DataGrid
        Public ddlleadtypeFilter, ddlleadprogramFilter, dd_adtype, dd_status, dd_cat, advenue, adautor, adphoto, ddintakeresponse As DropDownList
        Public adtitle As TextBox
        Public B_FinalV, b_savee, B_Final, B_removead, autopost, exitV, test, B_post,btnAddV,btnAddVenue,btnSavePPlan  As Button
        Public venuname, venuecode, venueurl,pstadfrom,pstadto As TextBox
        Public privateven,acctsetup As CheckBox
        Public ddvenonline,dd_Ltemplates As DropDownList
        Public cell1, cell1a
        
        Public xx, tst, dd1, dd2, dd3, dd4, dd1a, dd2a, dd3a, dd4a,pstpno As TextBox
       
        Public qaddtext, adclone, adcloneP, adedit As Button
        Public tsta As System.Web.UI.HtmlControls.HtmlTextArea
        public pnlNewADPlan as panel
    
       
        ' Private Shared apdatacnt As Integer
        Public adtext 
        'As FreeTextBox
        Public lblNoAPDate,adtxtreadonly,adtxtreadonlyH As Label
        Public apsdetail,btn2prev,btnpg1,btnpg2,btoolbar,bKeyPH,btnSpg1,btnSpg2,btneditbranding As LinkButton
        	Public subnavGen, subnavPage1, subnavPage2,subnavPage3,subnavPage3A, subnavPage4,subnavPage5,subnavPage6, subnavresp,subnavimgs As HtmlTableCell
  			public pnlstepmain,pnlgeneral, pnlpage1,pnlpage2,pnlpage3,pnlpage3A,pnlpage4,pnlpage5,pnlpage6, pnlnotifications,pnlimages,pnltempatespre,pnladdstep as panel
      	Public spacer0, spacer1, spacer2,spacer3,spacer3a,spacer4,spacer5,spacer6 As HtmlTableCell
			Protected WithEvents Lgen, lpage1, lpage2,lpage3,lpage3A,lpage4,lpage5,lpage6, lautop,limgs,btnuseremail2,btnuseremail As LinkButton
      	Public Bgpg1,Bgpg2 As HtmlGenericControl
      	public pnlbrdpg1,pnlbrdpg2,pnladdvenueN,pnlpostings,pnlLSdetail,pnlgivepostno,pnlPPdetail,pnlentposts,pnlbrandMain as panel
      	public pstsvenue,pststatus,pstadkey,lbltest,lblxleads,lbledittext as label
      	public pstNDfrom,pstNDto,pstEPdate,TotPosts  as textbox
		
			Public CheckedItems As ArrayList
         Public Results() As String
         Public myList As ArrayList = New ArrayList
	      Protected CheckBox,chkconfirmbrand As System.Web.UI.WebControls.CheckBox
      	Protected dgItem As DataGridItem
      	Public deletedIds As String = ""
         Public ChkdItems As String = ""
         Public SortField As String = ""
         Public ChkBxIndex As String = ""
         Public BxChkd As Boolean = False
         public PPName,PPnoposts,PPDV,PPROP,ppvensearch,adname,PPEXP,PPSdate,PPEdate as textbox
         public dd_PPfreq,dd_PPDperiod,dd_Pubfilter,ddemailcor,dd_mkprg,dd_PPstat as dropdownlist
      	public bdlevel as string
      	public refererA
      	public txtMessage
      	public pnlSbrdpg1,pnlSbrdpg2,pnltbar,pnlPPdetailMain,pnlPPwarn,pnlshowadkeys,pnlstillgetleads,pnlbrandwarning as panel
      	public ProgramPlans,ProgramPlansS,ADKeys as datagrid
      	public ProgramPlansD as datalist
      	public _dataSet As New DataSet()
      	public ppgrid
      	public vwbodyA,showcalc,showcalcz,showcalc3 as linkbutton
      	public cdrCalendar,cdrCalendar2,cdrCalendar2Z
      	public adno,lblnopostsN as label
      	public lb_selvenues as listbox
      	public pnlNewADPostPlan,pnlNewADActivateplan,pnlNewADPlanSVDW,pnlNewADPlanSV,pnlplansched,pnlsverror,pnladcloned as panel
          public  dd_PostPPNow,dd_ActivatePPNow,dd_PPfreqNP as dropdownlist    	
       	public txtNPSdate, txtNPEdate,txtNPNoposs,txtNPNoVens,txtNPNoLeads,txtNPPName as textbox
       	public dgVenueDOW,ADPlanSched as datagrid
       	public lbltotposts as label
       	Protected datesArray, VenueArray,VenueArrayKey,VenueArrayPK As ArrayList
       	
			public labelOutput,lblCvenue,lblcal3,lblerror,lblvenselect,lbladcloned as label
       	public Calendar1,cdrCalendar3
       	protected ADKeyCodeS(100) as string
       	protected VInfoArray(100) as string
       	public Button2,Button3,Button1,ButtonZ,btnNPPNcancel,btnNPPNskip as button
       	public dd_schstatfilter as dropdownlist
       	public chkstillgetleads as checkbox
       	
			Private Sub Page_PreRender(ByVal sender As Object, ByVal e As EventArgs) 
			 	ViewState.Add("selectedvenuesKEY", VenueArrayKEY)
			 	ViewState.Add("selectedvenues", VenueArray)
			 	ViewState.Add("selectedvenues", VenueArrayPK)
        	end sub
        
       	
       	
       	
        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
 				If ViewState("selectedDates") Is Nothing Then
					 datesArray = new ArrayList()
				Else
					datesArray = CType( Viewstate("selectedDates"), ArrayList )
				End If
				
				If ViewState("selectedvenues") Is Nothing Then
					 VenueArray = new ArrayList()
				Else
					VenueArray = CType( Viewstate("selectedvenues"), ArrayList )
				End If
				If ViewState("selectedvenuesKEY") Is Nothing Then
					 VenueArrayKEY = new ArrayList()
				Else
					VenueArrayKEY = CType( Viewstate("selectedvenuesKEY"), ArrayList )
				End If
				If ViewState("selectedvenuesPK") Is Nothing Then
					 VenueArrayPK = new ArrayList()
				Else
					VenueArrayPK = CType( Viewstate("selectedvenuesPK"), ArrayList )
				End If
				



            If Not (Page.IsPostBack) Then
             	clearsessions()
                If defaultexists() Then
                    pnldrspwarn.Visible = False
                    pnlALL.Visible = True
                Else
                    pnldrspwarn.Visible = True
                    pnlALL.Visible = False

                End If
                'bindfields()
                adstage.Text = "* DRAFT *"
                adno.visible=false
                adtitleM.Text = "New AD"
                admain.Visible = True
                pnlsavedv.Visible = False
                
                FillLeadTypeDropDown()
                FillLeadprogramDropDown()
                'FillADCatDropDown()
                B_removead.Visible = False
                finwarning.Visible = False
                pnlnoadvenues.Visible = False
                autoc.Text = "AutoPost Credits: " & checkcredits()
                If Session("branding") = "Yes" Then
                    Fillintakeresponse()
                    ddintakeresponse.Enabled = True
                Else
                    Fillintakeresponse()
                    ddintakeresponse.Enabled = False
                    ddintakeresponse.SelectedIndex = ddintakeresponse.Items.IndexOf(ddintakeresponse.Items.FindByText("Default"))
                End If

                If Session("AdSaved") = "True" Then
                    B_removead.Visible = True
                End If
                If Request.QueryString("source") <> "post" Then
                    Session("AdSaved") = "False"
                    B_post.Visible = False
                Else
                    If Session("ADStage") = "Finalized" Then
                        If Request.QueryString("type") = "quick" Then
                            adstage.Text = "* Finalized *"
                            pnltxtbody.Visible = False
                            B_FinalV.Visible = False
                            B_Final.Visible = False
                            pnladtitle.Visible = False
                            exitV.Visible = true
                            placead.Visible = True
                            bindfields()
                            'hrline.visible = True
                            B_post.Visible = False
									adno.visible=true
                        End If
                        adstage.Text = "* Finalized *"
                        B_FinalV.Visible = False
                        b_savee.Visible = False
                        B_Final.Visible = False
                        adtitle.Enabled = False
                         adname.Enabled = False
                        'dd_status.Enabled = False
                        dd_mkprg.enabled=false
            				ddemailcor.enabled=false
                        ddlleadtypeFilter.Enabled = False
                        ddlleadprogramFilter.Enabled = False
                        ddintakeresponse.Enabled = False
                        'adtext.ReadOnly = True
                        adtxtreadonly.visible=true
                        adtxtreadonlyH.visible=true
                        adtext.show=false
                        'hrline.visible = False
                        B_post.Visible = True
                        admain.Visible = False
                        placead.Visible = True
                        If Request.QueryString("modify") = "Yes" Then
                        	'adtext.ReadOnly = false
                        	adtext.contenteditable="false"
                        	pnltxtbody.Visible = true
                        	b_savee.Visible = true
                        end if
                        lbledittext.visible=true
                        adtext.show=false
                        bindfields()
                    Else
                        admain.Visible = False
                        placead.Visible = True
                        pnlsavedv.Visible = True
                    End If


                    FillADvenues()
                    'bindvenues()
                    lbl_adnoV.Text = Session("adno")

                End If
                If Request.QueryString("action") = "edit" Then
                    adtitleM.Text = "Edit AD"
                    Session("adno") = Request.QueryString("adno")
                    bindfields()
                    Session("AdSaved") = "True"
                    
                    If Session("adstage") = "Finalized" Then
                        lbledittext.visible=true
                        adtext.show=false
                        B_removead.Visible = False
                        B_FinalV.Visible = False
                        adtitle.Enabled = False
                         adname.Enabled = False
                        'dd_status.Enabled = False
                        dd_mkprg.enabled=false
            				ddemailcor.enabled=false
                        ddlleadtypeFilter.Enabled = False
                        ddlleadprogramFilter.Enabled = False
                        ddintakeresponse.Enabled = False
                        'adtext.ReadOnly = True
                        adtxtreadonly.visible=true
                        adtxtreadonlyH.visible=true
                        adclone.Visible = True
                         adcloneP.Visible = True
                        b_savee.Visible = False
                        placead.Visible = True
                        'hrline.visible = false
                        B_Final.Visible = False
                        FillADvenues()
                        'bindvenues()
								adedit.visible=false
                    Else
                        B_removead.Visible = True
                        B_FinalV.Visible = True
                        adtitle.Enabled = True
                         adname.Enabled = true
                        'dd_status.Enabled = True
                        dd_mkprg.enabled=true
            				ddemailcor.enabled=true
                        ddlleadtypeFilter.Enabled = True
                        ddlleadprogramFilter.Enabled = True
                        ddintakeresponse.Enabled = True
                        'adtext.ReadOnly = False
                        
                        adclone.Visible = False
                         adcloneP.Visible = false
                        b_savee.Visible = True
                        B_Final.Visible = True
                    End If
							
                    pnltxtbody.Visible = False

                    pnladtitle.Visible = False
                    
                    exitV.Visible = true
                 
                    B_post.Visible = False
                    qaddtext.Visible = false
                    bindfields()
                    If Request.QueryString("source") = "bradd" then
                           ddintakeresponse.SelectedIndex = ddintakeresponse.Items.IndexOf(ddintakeresponse.Items.FindByText("Default"))
	          		end if
                End If
                If Request.QueryString("action") = "clone" Then
                    Session("adno") = Request.QueryString("adno")
                    bindfields()
                    Session("adstage") = "Draft"
                    adstage.Text = "* " & Session("adstage") & " *"
                    'Session("AdSaved") = "False"
                    B_post.Visible = False
                    Session("adno") = ""
                    B_removead.Visible = False
                    If Request.QueryString("type") = "quick" Then
                        pnltxtbody.Visible = False
                        Session("ADStage") = "Finalized"
                        B_FinalV.Visible = False
                        adstage.Text = "* Finalized *"
                        adtitleM.Text = "Quick AD"
                        B_Final.Visible = False
                        pnladtitle.Visible = False
                        exitV.Visible = true
                        placead.Visible = True
                        'hrline.visible = True
                        B_post.Visible = False
                        qaddtext.Visible = false
                        pnlsavedv.Visible = True
                        FillADvenues()
                        'bindvenues()

                    End If
                End If
                If Request.QueryString("action") = "new" Then
                    If (Request.QueryString("type") = "quick") Then
                        pnltxtbody.Visible = False
                        'adtext.Text = "This was a quick AD.  No text entered."
								adtext.content = "This was a quick AD.  No text entered."

                        B_FinalV.Visible = False

                        adtitleM.Text = "Quick AD"
                        B_Final.Visible = False
                        pnladtitle.Visible = False

                        exitV.Visible = true
                        placead.Visible = False
                        'hrline.visible = True
                        B_post.Visible = False
                        qaddtext.Visible = false

                    End If
                    Session("ADStage") = "Draft"
                    adstage.Text = "* Draft *"
                    Session("adno") = ""
                    btn2prev.enabled=false
              
                Else
                    If (Request.QueryString("type") = "quick" And Request.QueryString("source") <> "post" And Request.QueryString("action") <> "clone") Then
                        pnltxtbody.Visible = False
                        'adtext.Text = "This was a quick AD.  No text entered."
                        
                        adtext.content = "This was a quick AD.  No text entered."
                        Session("ADStage") = "Finalized"
                        B_FinalV.Visible = False
                        adstage.Text = "* Finalized *"
                        adtitleM.Text = "Quick AD"
                        B_Final.Visible = False
                        pnladtitle.Visible = False

                        exitV.Visible = true
                        placead.Visible = False
                        'hrline.visible = True
                        B_post.Visible = False
                        qaddtext.Visible = false
                        bindfields()

                    End If
                End If
                If Request.QueryString("source") = "response" Then

                    ddintakeresponse.SelectedIndex = ddintakeresponse.Items.IndexOf(ddintakeresponse.Items.FindByValue(Request.QueryString("rid")))
                End If
                If Request.QueryString("bname") = "" Then
                Else
                    ddintakeresponse.SelectedIndex = ddintakeresponse.Items.IndexOf(ddintakeresponse.Items.FindByText(Request.QueryString("bname")))

                End If
                
                If Request.QueryString("modify") = "Yes" then
                		'adtext.ReadOnly = false
                		
                  	pnltxtbody.Visible = true
                  	b_savee.Visible = true
                end if
                'ADVenues.Columns(18).Visible = False
                'ADVenues.Columns(19).Visible = False
                'ADVenues.Columns(20).Visible = False
				
					'NEW
					 	subnav("General")
					 	bindimages()
             		btoolbar.attributes.add("onClick", "toggleToolbar(); return false;")
             		bKeyPH.attributes.add("onClick", "insertkey();return false;")
             		if Request.QueryString("action") = "new" then
             			subnavPage3.visible=false
             			subnavPage3A.visible=false
             		end if
             		subnavPage4.visible=false
             		
             		Fillemailcor()
             		FillmarketProgram()
             		bindfields()
             		if Request.QueryString("nav") = "posting" then 
             			Subnav("Venues")
			            Session("fvenues")="Published"
			            bindADPlans()
			            session("CPlanId")=Request.QueryString("pplan")
            			session("CPlanMode")="Edit"
            			editplan(session("CPlanId"))
             		elseif Request.QueryString("nav") = "branding" then 
             			subnav("Branding")
             			
             			Fillintakeresponse()
             				ddintakeresponse.SelectedIndex = ddintakeresponse.Items.IndexOf(ddintakeresponse.Items.FindByvalue(Request.QueryString("bname")))
	    						dim normurl as string = "intakep.aspx?page=1&id=" & ddintakeresponse.selecteditem.value
				        		dim normurl2 as string = "intakep.aspx?page=2&id=" & ddintakeresponse.selecteditem.value
								bgpg1.Attributes("src") = normurl
				        		btn2prev.enabled=true
				        		bgpg2.Attributes("src") = normurl2	 
				          	pnlbrdpg1.visible=true
				            pnlbrdpg2.visible=false           
				            btnpg1.cssclass="linkbuttonsRed"
				            btnpg2.cssclass="linkbuttons"
	    					
             		end if
             		If Request.QueryString("action") = "edit" then
             			adno.visible=true
             			adno.text = "# " & Request.QueryString("adno")
             		else
             			adno.visible=false
             		end if	
             		
             		
             		if session("role") ="GOD" then
             			adtext.ModeSwitch="true"
             		else
             		 	adtext.ModeSwitch="false"
             		end if
             		'Inactive ADs
             		if dd_status.selecteditem.text="Inactive" then
             			pnlstillgetleads.visible=true
             			btnAddVenue.enabled=false
             			'btnSavePPlan.enabled=false
             			btnAddV.enabled=false
             		else
             			pnlstillgetleads.visible=false
             			btnAddVenue.enabled=true
             			btnSavePPlan.enabled=true
             			btnAddV.enabled=true
             		end if
             		'Inactive AD Plans
             		if Request.QueryString("source") = "wrkpsts" then 
            			Subnav("Venues")
             			session("CPlanMode")="Edit"
            			editplan(session("cplanid"))
            			 dd_schstatfilter.SelectedIndex = dd_schstatfilter.Items.IndexOf(dd_schstatfilter.Items.FindByText("Active"))  
           				bindPlansched(session("cplanid"))
             		end if
             		if Request.QueryString("source") = "wrkpstsA" then 
            			Subnav("Venues")
             			Subnav("Venues")
			            'bindvenues()
			            Session("fvenues")="Published"
			            
			            pnlPPwarn.visible=false
			            removefromlist("-1")
			            pnlshowadkeys.visible=false
			            pnlpostings.visible=true
			            pnlNewADPlan.visible=false
			            pnlNewADPlanSV.visible=false
			            cdrCalendar3.visible=false
			            bindADPlans()
             		end if
             		
          			if Request.QueryString("source") = "viewstats" then 
          				   Subnav("Status")
          				     pnlSbrdpg1.visible=true                  
					            btnSpg1.cssclass="linkbuttonsRed"
					            pnlSbrdpg2.visible=false
					            btnSpg2.cssclass="linkbuttons"
					            
					            bindrpts()
					            bindrptsD()
          			end if
          			session("keepadmfilters")="true"
          			if Request.QueryString("cloned") = "true" then 
          				lbladcloned.visible=true
          				pnladcloned.visible=true
          			else
          				lbladcloned.visible=false
          				pnladcloned.visible=false
          			end if
             			
             			
            End If
            
            pagelayout()

        End Sub
        public sub clearsessions()
        		session("VenueArrayCNT")=0
        		session("DurDays")=""
       		session("DurFreq")=""
       		session("venueonline")=""
       		session("adkeycode")=""
       		session("venhtml")=""
        		'Private Shared apbutton(2) As String
        		
        
        end sub
        
        Sub FillmarketProgram()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='marketProgram' and ( x_company='All' or x_uid='" & Session("userid") & "') order by x_descr"
            'x_company='" & Session("company") & "' or
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                dd_mkprg.DataSource = dataReader
                dd_mkprg.DataTextField = "x_descr"
                dd_mkprg.DataValueField = "tbl_xwalk_pk"
                dd_mkprg.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
            dd_mkprg.Items.Insert(0, New ListItem("None", "9999"))
        End Sub
        
        
          Sub ItemDataBoundEventHandlerA(ByVal sender As Object, ByVal e As DataGridItemEventArgs)

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
            	Dim btnappnd As System.Web.UI.HtmlControls.HtmlInputButton
               btnappnd = e.Item.Cells(0).FindControl("BAppendBody")
            	
            	Dim itemcellwho2 As TableCell = e.Item.Cells(8)
               Dim itemCellwhotext2 As String = itemCellwho2.Text
              
               btnappnd.attributes.add("Onclick", "insertDate();return false;")
               lbltest.attributes.add("value", "EAT PORL")
               refererA.attributes.add("value", itemCellwhotext2)
	     			txtMessage.value= itemCellwhotext2
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
        
        Sub Fillemailcor()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select *, convert(varchar(20),email_date,101) as 'emdate' " _
								& "from tbl_emails " _
								& "join tbl_xwalk on tbl_xwalk_pk = email_stat " _
								& "where x_descr <> 'Do Not Use' " _
								& "and ((email_uid='" & Session("userid") & "' ) " _
								& "or (x_descr='Company Wide' and email_co='" & Session("company_pk") & "') " _
								& "or (x_descr='System Wide')) order by email_name"
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
        
         Sub updateallowleads(ByVal Source As System.Object, ByVal e As System.EventArgs)
        		insertad()
        	end sub
        	
        	 Sub adstatchange(ByVal Source As System.Object, ByVal e As System.EventArgs)
        		if request.querystring("action")="edit"  then
        			insertad()
        			if dd_status.selecteditem.text="Inactive" then
        				updateADPLans("Inactive","AD","System")
        				updateADVenues("Inactive","AD","System")
        				UpdateADPostDates("Inactive","AD","System")
        			else
        				updateADPLans("Active","AD","System")
        				updateADVenues("Active","AD","System")
        				UpdateADPostDates("Active","AD","System")
        			end if
        			
        			response.redirect("createad.aspx?action=edit&adno=" & request.querystring("adno"))
        		end if
        	end sub
        	
        	sub updateADPLans(stat as string, who as string, what as string)
        	  Dim strSql As String 
        	  if who="AD" then
	        	  	if stat="Inactive" then
	           		strSql = "update tbl_leadADPlans set lap_status='Inactive',lap_WhoTouched='" & what & "' where lap_adfk='" & session("adno") & "' and (lap_status='Active' or lap_status='Incomplete') "
	       		else
	       		   strSql = "update tbl_leadADPlans set lap_status='Active',lap_WhoTouched='" & what & "' where lap_adfk='" & session("adno") & "' and lap_status='Inactive' and lap_WhoTouched='System'  "
	       		end if
	       	else	       		
	       		
	       	end if
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
        	
        	sub updateADVenues(stat as string, who as string, what as string)
        	  Dim strSql As String 
        	   if who="AD" then
	        	   if stat="Inactive" then
	           		strSql = "update tbl_LeadADPlanVenues set lv_status='Inactive',lv_whotouched='" &  what & "' where lv_ad_fk='" & session("adno") & "' and lv_status='Active' "
	       		else
	       			strSql = "update tbl_LeadADPlanVenues set lv_status='Active',lv_whotouched='" &  what & "' where lv_ad_fk='" & session("adno") & "' and lv_status='Inactive' and lv_whotouched='System' "
	       		
	       		end if
	       	else
	       	 	if stat="Inactive" then
	           		strSql = "update tbl_LeadADPlanVenues set lv_status='Inactive',lv_whotouched='System' where lv_adplan_fk='" & session("Cplanid") & "' and lv_status='Active' "
	       		else
	       			strSql = "update tbl_LeadADPlanVenues set lv_status='Active' where lv_adplan_fk='" & session("Cplanid") & "' and lv_status='Inactive' and lv_whotouched='System' "
	       		
	       		end if
	       	
	       	end if
       		
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
        	
        	  sub UpdateADPostDates(stat as string, who as string, what as string)	
        	  	Dim strSql As String
        	  	if who="AD" then
	        	  	if stat="Inactive" then 
	           		strSql = "update tbl_LeadADVenues set av_adplaced='Canceled', av_whotouched='" & what & "' where av_leadads_FK='" & session("adno") & "' and av_adplaced='Unpublished' "
	       		else
	       			strSql = "update tbl_LeadADVenues set av_adplaced='Unpublished', av_whotouched='" & what & "' where av_leadads_FK='" & session("adno") & "' and av_adplaced='Canceled' and av_whotouched='System' and convert(varchar(20),av_APTo,101) >= convert(varchar(20),getdate(),101)"
	       		end if
	       	elseif who="ADPlan" then
	       		if stat="Inactive" then 
	           		strSql = "update tbl_LeadADVenues set av_adplaced='Canceled', av_whotouched='" & what & "' where av_lapfk='" & session("CPlanid") & "' and av_adplaced='Unpublished' "
	       		else
	       			strSql = "update tbl_LeadADVenues set av_adplaced='Unpublished', av_whotouched='" & what & "' where av_lapfk='" & session("CPlanid") & "' and av_adplaced='Canceled' and av_whotouched='System' and convert(varchar(20),av_APTo,101) >= convert(varchar(20),getdate(),101)"
	       		end if
	       	else 
	       		if stat="Inactive" then 
	           		strSql = "update tbl_LeadADVenues set av_adplaced='Canceled', av_whotouched='" & what & "' where av_lapVenFK='" & session("Cvenuid") & "' and av_adplaced='Unpublished' "
	       		else
	       			strSql = "update tbl_LeadADVenues set av_adplaced='Unpublished', av_whotouched='" & what & "' where av_lapVenFK='" & session("Cvenuid") & "' and av_adplaced='Canceled' and av_whotouched='System' and convert(varchar(20),av_APTo,101) >= convert(varchar(20),getdate(),101)"
	       		end if
	       	
	       	
	       	end if
       		
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
        	
        	
        	
        	
        
         Sub refreshxleads(ByVal Source As System.Object, ByVal e As System.EventArgs)
        		refreshxleadsNOBT()
        	end sub
        	
        	
        	
        	
        	
        Sub refreshxleadsNOBT()
        		
        		dim DurInDays as decimal
        		dim LPD as decimal
        		if dd_PPDperiod.selecteditem.text="Days" then
        			DurInDays = CDec(PPDV.text)
        		elseif dd_PPDperiod.selecteditem.text="Weeks" then
        			DurInDays = CDec(PPDV.text) * 7.5
        		else
        			DurInDays = CDec(PPDV.text) * 30
        		end if
        		        		
        		if dd_PPfreq.selecteditem.text="Daily" then
        			LPD = (CDec(PPnoposts.text) * 1) * DurInDays
        		elseif dd_PPfreq.selecteditem.text="Weekly"
        			LPD = (CDec(PPnoposts.text) / 7.5) * DurInDays
        		else
        			LPD = (CDec(PPnoposts.text) /30) * DurInDays
        		end if
        		
        		lblxleads.text= cdec(PPROP.text) * LPD
        		TotPosts.text = LPD
        end sub
         
        Sub clrtxtbox(ByVal Source As System.Object, ByVal e As System.EventArgs)
            adtext.content = ""
           
        end sub
        
        
          Sub prefillemail(ByVal Source As System.Object, ByVal e As System.EventArgs)
           
            
            If ddemailcor.SelectedItem.Text = "Clear" Then
               
                adtext.content = ""
                pnltempatespre.visible=false
            ElseIf ddemailcor.SelectedItem.Text = "Add" Then
                'Response.Redirect("emailmainteditadd.aspx?action=new&source=sleademail")
            Else
                pnltempatespre.visible=true
                adtext.height=300
            	bindtemppreview()
            	
            
            End If

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
        
         Public Sub hidetemplates(ByVal Source As System.Object, ByVal e As System.EventArgs)
        		pnltempatespre.visible=false
        		 ddemailcor.SelectedIndex = ddemailcor.Items.IndexOf(ddemailcor.Items.FindByText("Select.."))
        end sub
        
        
         Public Sub appendAll(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Dim x As Button = Source

            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim temppkid As String = item.Cells(0).Text
            Dim tsub As String = item.Cells(2).Text
            Dim tbody As String = item.Cells(8).Text
            adtext.content = tbody            
          
            pnltempatespre.visible=true
            ddemailcor.SelectedIndex = ddemailcor.Items.IndexOf(ddemailcor.Items.FindByText("Select.."))
            
        End Sub
         Public Sub appendBody( Source As System.Object, ByVal e As System.EventArgs)
            Dim x As Button = Source

            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim temppkid As String = item.Cells(0).Text
            Dim tsub As String = item.Cells(2).Text
            Dim tbody As String = item.Cells(5).Text
           
            'adtext.content = adtext.content + "<br>" + tbody
            'pnltempatespre.visible=false
            ddemailcor.SelectedIndex = ddemailcor.Items.IndexOf(ddemailcor.Items.FindByText("Select.."))
            
        End Sub
         Public Sub appendSubject(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Dim x As Button = Source

            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim temppkid As String = item.Cells(0).Text
            Dim tsub As String = item.Cells(2).Text
            Dim tbody As String = item.Cells(5).Text
            
           
            'pnltempatespre.visible=false
           
        End Sub
         Public Sub Canceltemplate(ByVal Source As System.Object, ByVal e As System.EventArgs)
            pnltempatespre.visible=false
            ddemailcor.SelectedIndex = ddemailcor.Items.IndexOf(ddemailcor.Items.FindByText("Select.."))
         
         
	   
        End Sub
        
        Sub GetSelections_Click2(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Dim x As CheckBox = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(1).Text

            If x.Checked = True Then
              addtolist(content)
            Else
                removefromlist(content)
            End If

        End Sub
        
        Sub addtolist(ByVal id As String)
            Dim strUID As String = Session("userid")
            Dim strSql As String = "insert into tbl_tmpad (tmpad_uid,tmpad_adno) values ('" & Session("userid") & "','" & id & "')"
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

        End Sub
        
         Sub MyDataGrid_PagePP(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
            GetCheckBoxValues()

            ADVenuesPP.CurrentPageIndex = e.NewPageIndex
            RePopulateCheckBoxes()

        End Sub
        
        Sub removefromlist(ByVal id As String)
            Dim strUID As String = Session("userid")
            Dim strSql As String
            If id = -1 Then
                strSql = "delete from tbl_tmpad where tmpad_uid='" & Session("userid") & "'"
            Else
                strSql = "delete from tbl_tmpad where tmpad_uid='" & Session("userid") & "' and tmpad_adno='" & id & "'"
            End If

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

        End Sub
        
         Sub RePopulateCheckBoxes()
            CheckedItems = New ArrayList
            CheckedItems = Session("CheckedItems")
            If Not IsNothing(CheckedItems) Then
                'Loop through DataGrid Items        
                For Each dgItem In ADVenuesPP.Items
                    ChkBxIndex = ADVenuesPP.DataKeys(dgItem.ItemIndex)
                    'Repopulate DataGrid with items found in Session            
                    If CheckedItems.Contains(ChkBxIndex) Then
                        CheckBox = CType(dgItem.FindControl("myCheckbox"), CheckBox)
                        CheckBox.Checked = True
                    End If
                Next
            End If
            'Copy ArrayList to a new array    
            Results = CheckedItems.ToArray(GetType(String))
            'Concatenate ArrayList with comma to properly send for deletion    
            deletedIds = String.Join(",", Results)
        End Sub
        
        
         Sub GetCheckBoxValues()
            'response.write("her")
            'As paging occurs store checkbox values    
            CheckedItems = New ArrayList
            'Loop through DataGrid Items  

            For Each dgItem In ADVenuesPP.Items
                'Retrieve key value of each record based on DataGrids        
                ' DataKeyField property        
                ChkBxIndex = ADVenuesPP.DataKeys(dgItem.ItemIndex)
                CheckBox = dgItem.FindControl("myCheckbox")
                'Add ArrayList to Session if it doesnt exist        
                If Not IsNothing(Session("CheckedItems")) Then
                    CheckedItems = Session("CheckedItems")
                End If
                If CheckBox.Checked Then
                    BxChkd = True
                    'Add to Session if it doesnt already exist            
                    If Not CheckedItems.Contains(ChkBxIndex) Then
                        CheckedItems.Add(ChkBxIndex.ToString())
                    End If
                Else
                    'Remove value from Session when unchecked            
                    CheckedItems.Remove(ChkBxIndex.ToString())
                End If
            Next
            'Update Session with the list of checked items    
            Session("CheckedItems") = CheckedItems

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
        
        	Sub ItemDataBoundEventHandlerIMG(sender as Object, e as DataGridItemEventArgs)
		   	If e.Item.ItemType = ListItemType.Item OR e.Item.ItemType = ListItemType.AlternatingItem then
			   	
			   end if
		   
		   end sub
        
        Sub btn_bpg1(ByVal sender As Object, ByVal e As EventArgs)
            
            pnlbrdpg1.visible=true
            pnlbrdpg2.visible=false           
            btnpg1.cssclass="linkbuttonsRed"
            btnpg2.cssclass="linkbuttons"
           
        End Sub
        
         Sub btn_bpg2(ByVal sender As Object, ByVal e As EventArgs)
            pnlbrdpg1.visible=false
            pnlbrdpg2.visible=true           
            btnpg1.cssclass="linkbuttons"
            btnpg2.cssclass="linkbuttonsRed"
           
        End Sub
         Sub btn_Sbpg1(ByVal sender As Object, ByVal e As EventArgs)
            
            pnlSbrdpg1.visible=true
            pnlSbrdpg2.visible=false           
            btnSpg1.cssclass="linkbuttonsRed"
            btnSpg2.cssclass="linkbuttons"
             
            bindrpts()
            bindrptsD()
           
        End Sub
        
         Sub btn_Sbpg2(ByVal sender As Object, ByVal e As EventArgs)
            
            pnlSbrdpg1.visible=false
            pnlSbrdpg2.visible=true           
            btnSpg1.cssclass="linkbuttons"
            btnSpg2.cssclass="linkbuttonsRed"
             
            bindrpts()
            bindrptsD()
           
        End Sub
        
        
         Sub subnav(ByVal button As String)
            Dim clickedbutton As String = button

            'Set cell class
            subnavGen.Attributes.Add("class", "tblcelltest")
            subnavPage1.Attributes.Add("class", "tblcelltest")
            subnavPage2.Attributes.Add("class", "tblcelltest") 
            subnavPage3.Attributes.Add("class", "tblcelltest") 
            subnavPage3A.Attributes.Add("class", "tblcelltest") 
            subnavPage4.Attributes.Add("class", "tblcelltest")
            subnavPage5.Attributes.Add("class", "tblcelltest")
            subnavPage6.Attributes.Add("class", "tblcelltest")
                       
            'Set button font color
            Lgen.ForeColor = System.Drawing.Color.Black
            lpage1.ForeColor = System.Drawing.Color.Black
            lpage2.ForeColor = System.Drawing.Color.Black  
            lpage3.ForeColor = System.Drawing.Color.Black 
            lpage3A.ForeColor = System.Drawing.Color.Black   
            lpage4.ForeColor = System.Drawing.Color.Black 
            lpage5.ForeColor = System.Drawing.Color.Black 
            lpage6.ForeColor = System.Drawing.Color.Black 
            
            'Set spacers
            spacer0.Visible = True
            spacer1.Visible = True
            spacer2.Visible = True
            spacer3.Visible = True
             spacer3A.Visible = True
            spacer4.Visible = True
            spacer5.Visible = True
            spacer6.Visible = True
           

            'Set Panels
            pnlgeneral.Visible = False
            pnlpage1.Visible = False
           	pnlpage2.Visible = False
           	pnlpage3.Visible = False
           	'pnlpage3A.Visible = False
           	pnlpage4.Visible = False
           	pnlpage5.Visible = False
           	pnlpage6.Visible = False		
            
            If clickedbutton = "General" Then
                subnavGen.Attributes.Add("class", "tblcelltestSelected")
                Lgen.ForeColor = System.Drawing.Color.White
                spacer0.Visible = true
                pnlgeneral.Visible = True
            ElseIf clickedbutton = "Layout" Then
                subnavPage1.Attributes.Add("class", "tblcelltestSelected")
                lpage1.ForeColor = System.Drawing.Color.White
                spacer0.Visible = True
                spacer1.Visible = True               
                pnlpage1.Visible = True
           
            ElseIf clickedbutton = "Branding" Then
            	subnavPage2.Attributes.Add("class", "tblcelltestSelected")
                lpage2.ForeColor = System.Drawing.Color.White
                spacer0.Visible = True
                spacer1.Visible = True               
                pnlpage2.Visible = True
                 pnlbrdpg1.visible=false
	            pnlbrdpg2.visible=false           
	            btnpg1.cssclass="linkbuttonsRed"
	            btnpg2.cssclass="linkbuttons"
           	ElseIf clickedbutton = "Venues" Then
            	subnavPage3.Attributes.Add("class", "tblcelltestSelected")
                lpage3.ForeColor = System.Drawing.Color.White
                spacer0.Visible = True
                spacer1.Visible = True  
                spacer2.Visible = True              
                pnlpage3.Visible = True
               pnlpostings.visible=true
               pnlPPdetail.visible=false
               pnladdvenueN.visible=false
               pnladdvenue.visible=false
               pnlLSdetail.visible=false
               pnlgivepostno.visible=false
           	ElseIf clickedbutton = "Templates" Then
            	 subnavPage4.Attributes.Add("class", "tblcelltestSelected")
                lpage4.ForeColor = System.Drawing.Color.White
                pnlpage4.Visible = True
            ElseIf clickedbutton = "Images" Then
            	 subnavPage5.Attributes.Add("class", "tblcelltestSelected")
                lpage5.ForeColor = System.Drawing.Color.White
                pnlpage5.Visible = True
            ElseIf clickedbutton = "Status" Then
            	 subnavPage6.Attributes.Add("class", "tblcelltestSelected")
                lpage6.ForeColor = System.Drawing.Color.White
                pnlpage6.Visible = True
            ElseIf clickedbutton = "PubNow" Then
            	response.redirect("Postadi.aspx?source=adplanpost")	
            else
             	subnavGen.Attributes.Add("class", "tblcelltestSelected")
                Lgen.ForeColor = System.Drawing.Color.White
                spacer0.Visible = False
                pnlgeneral.Visible = True          
            End If
        End Sub
        
        Sub btn_Gen(ByVal sender As Object, ByVal e As EventArgs)
            subnav("General")            
        End Sub
        
        Sub btn_pg1(ByVal sender As Object, ByVal e As EventArgs)
            subnav("Layout")
            if Session("ADStage") = "Finalized" then
            	adtext.show = false
            	pnltbar.visible=false
            	lbledittext.visible=true
            end if
				Fillemailcor()
        End Sub
        
        Sub btn_pg2(ByVal sender As Object, ByVal e As EventArgs)
        		if ddintakeresponse.selecteditem.text="Add New" then
        			ddintakeresponse.SelectedIndex = ddintakeresponse.Items.IndexOf(ddintakeresponse.Items.FindByText("Default"))
	         end if
	          'chkconfirmbrand.checked = false
	          'chkconfirmbrand.visible=false 
            Subnav("Branding")
             dim normurl as string = "intakep.aspx?page=1&id=" & ddintakeresponse.selecteditem.value
        		dim normurl2 as string = "intakep.aspx?page=2&id=" & ddintakeresponse.selecteditem.value
				bgpg1.Attributes("src") = normurl
        		btn2prev.enabled=true
        		bgpg2.Attributes("src") = normurl2	 
          	pnlbrdpg1.visible=true
          	
        End Sub
        
        Sub btn_pg3(ByVal sender As Object, ByVal e As EventArgs)
            Subnav("Venues")
            'bindvenues()
            Session("fvenues")="Published"
            
            pnlPPwarn.visible=false
            removefromlist("-1")
            pnlshowadkeys.visible=false
            pnlpostings.visible=true
            pnlNewADPlan.visible=false
            pnlNewADPlanSV.visible=false
            cdrCalendar3.visible=false
            bindADPlans()
        End Sub
        
        Sub btn_pg3A(ByVal sender As Object, ByVal e As EventArgs)
            Subnav("PubNow")
           
        End Sub
        
         Sub btn_pg4(ByVal sender As Object, ByVal e As EventArgs)
            Subnav("Templates")
        End Sub
        
         Sub btn_pg5(ByVal sender As Object, ByVal e As EventArgs)
            Subnav("Images")
        End Sub
        
         Sub btn_pg6(ByVal sender As Object, ByVal e As EventArgs)
            Subnav("Status")
            pnlSbrdpg1.visible=true                  
            btnSpg1.cssclass="linkbuttonsRed"
            pnlSbrdpg2.visible=false
            btnSpg2.cssclass="linkbuttons"
            
            bindrpts()
            bindrptsD()
        End Sub
        
        public sub bindrpts()
         Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            mycommand = "begin " _
									& "select LAP_tbl_pk,count(*) as 'pcount' " _ 
									& "into #tmpa  " _
									& "from dbo.tbl_LeadADVenues  " _
									& "join dbo.tbl_leadADPlans on LAP_tbl_pk=av_lapfk " _
									& "where av_adplaced = 'Published'  " _
									& "group by LAP_tbl_pk " _
									& "select distinct LAP_tbl_pk,count( distinct(tbl_leads_pk)) as 'lcount' " _
									& "into #tmpb " _
									& "from dbo.tbl_leads " _
									& "join dbo.tbl_LeadADVenues on  av_key = ld_adcode " _
									& "join dbo.tbl_leadADPlans on LAP_tbl_pk=av_lapfk " _
									& "join dbo.tbl_LeadADPlanVenues on lv_adplan_fk=LAP_tbl_pk " _
									& "where lv_name <> 'Test Venue' and (ld_adcode is not null and ld_adcode <> ' ') " _
									& "group by LAP_tbl_pk " _
									& "select b.*,cast (lap_totposts as decimal(8,2)) as 'lap_totpostsA', case when (a.pcount is null) then 0 else a.pcount end as 'pcount', " _
									& "case when (cast(cast(cast(a.pcount as decimal(10,2))/cast(b.lap_totposts as decimal(10,2))*100 as decimal(10,2)) as varchar(50))+'%' is null) then '0%' " _
									& "else cast(cast(cast(a.pcount as decimal(10,2))/cast(b.lap_totposts as decimal(10,2))*100 as decimal(10,2)) as varchar(50))+'%' " _
									& "end as 'ppercent', " _
									& "case when (x.lcount is null) then 0 else x.lcount end as 'lcount', " _
									& "case when(cast(cast(x.lcount as decimal(10,2))/cast( cast(cast( lap_totposts as decimal(10,2)) * cast( b.lap_rop as decimal(10,2)) as decimal(10,2)) as decimal(10,2))*100 as decimal(10,2)) is null)  " _
									& "then '0%' else cast(cast(cast(x.lcount as decimal(10,2))/cast( cast(cast( lap_totposts as decimal(10,2)) * cast( b.lap_rop as decimal(10,2)) as decimal(10,2)) as decimal(10,2))*100 as decimal(10,2)) as varchar(50))+'%' " _
									& "end as 'lpercent', cast(cast( lap_totposts as decimal(10,2)) * cast( b.lap_rop as decimal(10,2)) as decimal(10,2)) as 'ELC' " _
									& "from dbo.tbl_leadADPlans b  " _ 
									& "left join #tmpa a on a.LAP_tbl_pk=b.LAP_tbl_pk " _
									& "left join #tmpb x on x.LAP_tbl_pk=b.LAP_tbl_pk " _
									& "where lap_adfk='" & request.querystring("adno") & "' " _
									& "and lap_useridfk='" & session("userid") & "' " _
									& "end; " _
									& "begin " _
									& "select LAP_tbl_pk,case when (av_adplaced='Published') then 1 else 0 end as 'PSpcount' " _
									& "into #tmpaST " _ 
									& "from dbo.tbl_LeadADVenues " _ 
									& "join dbo.tbl_leadADPlans on LAP_tbl_pk=av_lapfk  " _ 
									& "select LAP_tbl_pk,sum(PSpcount) as 'pcount' " _ 
									& "into #tmpaS " _ 
									& "from #tmpaST " _ 
									& "group by LAP_tbl_pk " _          
									& "select distinct LAP_tbl_pk,count( distinct(tbl_leads_pk)) as 'lcount' " _ 
									& "into #tmpbS " _ 
									& "from dbo.tbl_leads " _ 
									& "join dbo.tbl_LeadADVenues on  av_key = ld_adcode " _ 
									& "join dbo.tbl_leadADPlans on LAP_tbl_pk=av_lapfk " _ 
									& "join dbo.tbl_LeadADPlanVenues on lv_adplan_fk=LAP_tbl_pk " _ 
									& "where lv_name <> 'Test Venue' and (ld_adcode is not null and ld_adcode <> ' ') " _ 
									& "group by LAP_tbl_pk " _ 
									& "select LAP_tbl_pk,av_name,case when (av_adplaced='Published') then 1 else 0 end as 'Pdpcount'   " _ 
									& "into #tmpaDA " _ 
									& "from dbo.tbl_LeadADVenues " _ 
									& "join dbo.tbl_leadADPlans on LAP_tbl_pk=av_lapfk " _ 
									& "select LAP_tbl_pk,av_name,sum(Pdpcount) as 'Dpcount' " _ 
									& "into #tmpaD " _ 
									& "from #tmpaDA " _  	 								
									& "group by LAP_tbl_pk ,av_name " _ 
									& "order by LAP_tbl_pk,av_name " _ 
									& "select distinct LAP_tbl_pk,av_name,count( distinct(tbl_leads_pk)) as 'Dlcount' " _ 
									& "into #tmpbD " _ 
									& "from dbo.tbl_leads " _ 
									& "join dbo.tbl_LeadADVenues on  av_key = ld_adcode " _ 
									& "join dbo.tbl_leadADPlans on LAP_tbl_pk=av_lapfk " _ 
									& "join dbo.tbl_LeadADPlanVenues on lv_adplan_fk=LAP_tbl_pk " _ 
									& "where lv_name <> 'Test Venue' and (ld_adcode is not null and ld_adcode <> ' ') " _ 		 							
									& "group by LAP_tbl_pk,av_name " _ 
									& "order by LAP_tbl_pk,av_name   " _ 
									& "select distinct b.* , " _ 
									& "cast (lap_totposts as decimal(8,2)) as 'lap_totpostsA', " _  
									& "ad.av_name,a.pcount, " _ 
									& "case when (a.pcount =0) then 0 else " _ 
									& "cast(cast(b.lap_expectposts as decimal(10,2))/cast(a.pcount as decimal(10,2))*100 as decimal(10,2)) end as 'ppercent', " _  
									& "case when (x.lcount is null) then 0 else x.lcount end as 'lcount', " _ 
									& "case when(cast(cast(x.lcount as decimal(10,2))/cast( b.lap_rop as decimal(10,2))*100 as decimal(10,2)) is null)  " _ 
									& "then 0 else cast(cast(x.lcount as decimal(10,2))/cast( b.lap_rop as decimal(10,2))*100 as decimal(10,2)) " _ 
									& "end as 'lpercent', " _  
									& "case when (ad.Dpcount is null ) then 0 else ad.Dpcount end as 'Dpcount', " _ 
									& "case when (xd.Dlcount is null ) then 0 else xd.Dlcount end as 'Dlcount', " _ 
									& "case when (ad.Dpcount = 0) then '0%' else " _ 
									& "case when(cast(cast(xd.dlcount as decimal(10,2))/cast( b.lap_rop as decimal(10,2))*100 as decimal(10,2)) is null)  " _ 
									& "then '0%' else cast(cast(cast(xd.dlcount as decimal(10,2))/cast( ad.Dpcount as decimal(10,2))*100 as decimal(10,2)) as varchar(50))+'%' " _ 
									& "end end as 'Dlpercent' " _ 
									& "from dbo.tbl_leadADPlans b  " _ 
									& "join dbo.tbl_LeadADVenues z on z.av_lapfk = b.LAP_tbl_pk " _ 
									& "left join #tmpaS a on a.LAP_tbl_pk=b.LAP_tbl_pk  " _ 
									& "left join #tmpbS x on x.LAP_tbl_pk=b.LAP_tbl_pk  " _ 
									& "left join #tmpaD ad on aD.LAP_tbl_pk=b.LAP_tbl_pk and ad.av_name = z.av_name " _ 
									& "left join #tmpbD xd on xD.LAP_tbl_pk=b.LAP_tbl_pk and xd.av_name = z.av_name " _ 
									& "where lap_adfk='" & request.querystring("adno") & "' " _          
									& "and lap_useridfk='" & session("userid") & "' " _
									& "end " 
            Try
               Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
               
               
                dataAdapter.Fill(_dataSet)
                	_dataSet.Tables(0).TableName = "Program"
       				_dataSet.Tables(1).TableName = "ProgramDetail"

                ProgramPlans.DataSource = _dataSet.Tables("Program")              
                ProgramPlans.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
        end sub
        
         sub bindrptsD()
         Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            mycommand = "begin " _
									& "select LAP_tbl_pk,count(*) as 'pcount' " _ 
									& "into #tmpa  " _
									& "from dbo.tbl_LeadADVenues  " _
									& "join dbo.tbl_leadADPlans on LAP_tbl_pk=av_lapfk " _
									& "where av_adplaced = 'Published'  " _
									& "group by LAP_tbl_pk " _
									& "select distinct LAP_tbl_pk,count( distinct(tbl_leads_pk)) as 'lcount' " _
									& "into #tmpb " _
									& "from dbo.tbl_leads " _
									& "join dbo.tbl_LeadADVenues on  av_key = ld_adcode " _
									& "join dbo.tbl_leadADPlans on LAP_tbl_pk=av_lapfk " _
									& "join dbo.tbl_LeadADPlanVenues on lv_adplan_fk=LAP_tbl_pk " _
									& "where lv_name <> 'Test Venue' and (ld_adcode is not null and ld_adcode <> ' ') " _
									& "group by LAP_tbl_pk " _
									& "select b.*,cast (lap_totposts as decimal(8,2)) as 'lap_totpostsA', case when (a.pcount is null) then 0 else a.pcount end as 'pcount', " _
									& "case when (cast(cast(cast(a.pcount as decimal(10,2))/cast(b.lap_totposts as decimal(10,2))*100 as decimal(10,2)) as varchar(50))+'%' is null) then '0%' " _
									& "else cast(cast(cast(a.pcount as decimal(10,2))/cast(b.lap_totposts as decimal(10,2))*100 as decimal(10,2)) as varchar(50))+'%' " _
									& "end as 'ppercent', " _
									& "case when (x.lcount is null) then 0 else x.lcount end as 'lcount', " _
									& "case when(cast(cast(x.lcount as decimal(10,2))/cast( cast(cast( lap_totposts as decimal(10,2)) * cast( b.lap_rop as decimal(10,2)) as decimal(10,2)) as decimal(10,2))*100 as decimal(10,2)) is null)  " _
									& "then '0%' else cast(cast(cast(x.lcount as decimal(10,2))/cast( cast(cast( lap_totposts as decimal(10,2)) * cast( b.lap_rop as decimal(10,2)) as decimal(10,2)) as decimal(10,2))*100 as decimal(10,2)) as varchar(50))+'%' " _
									& "end as 'lpercent', cast(cast( lap_totposts as decimal(10,2)) * cast( b.lap_rop as decimal(10,2)) as decimal(10,2)) as 'ELC' " _
									& "from dbo.tbl_leadADPlans b  " _ 
									& "left join #tmpa a on a.LAP_tbl_pk=b.LAP_tbl_pk " _
									& "left join #tmpb x on x.LAP_tbl_pk=b.LAP_tbl_pk " _
									& "where lap_adfk='" & request.querystring("adno") & "' " _
									& "and lap_useridfk='" & session("userid") & "'  " _
									& "end " 
            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
               
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet)
               
                	dataSet.Tables(0).TableName = "Orders"
       		
                ProgramPlansS.DataSource = dataSet.Tables("Orders")              
                ProgramPlansS.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
        end sub
        
        
        Protected Sub dtgOrders_OnItemDataBound(sender As Object, e As DataGridItemEventArgs)
       If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
         Dim dtgOrderDetails As DataGrid = New DataGrid()
          dtgOrderDetails.Width = Unit.Pixel(720)
         dtgOrderDetails.BorderWidth = Unit.Pixel(1)
         dtgOrderDetails.CellPadding = 2
         dtgOrderDetails.CellSpacing = 0
         dtgOrderDetails.GridLines = GridLines.Horizontal
         dtgOrderDetails.BorderColor = Color.FromName("Black")
         dtgOrderDetails.HeaderStyle.BackColor = Color.FromName("White")
         dtgOrderDetails.HeaderStyle.ForeColor = Color.FromName("White")
         dtgOrderDetails.HeaderStyle.Font.Bold = True
         dtgOrderDetails.HeaderStyle.Font.Size = FontUnit.XSmall
         dtgOrderDetails.ItemStyle.Font.Name = "Verdana"
         dtgOrderDetails.ItemStyle.Font.Size = FontUnit.XSmall
         dtgOrderDetails.AlternatingItemStyle.BackColor = Color.FromName("lightgray")
         dtgOrderDetails.AutoGenerateColumns = False

 					Dim _boundColumn As BoundColumn = New BoundColumn()
         		'Order ID
         		_boundColumn.HeaderText = "Venue"
         		_boundColumn.DataField = "av_name"
         		dtgOrderDetails.Columns.Add(_boundColumn)
         		
         		
         		Dim _boundColumnA As BoundColumn = New BoundColumn()
         		_boundColumnA.HeaderText = "Acutal Posts"
         		_boundColumnA.DataField = "Dpcount"
         		dtgOrderDetails.Columns.Add(_boundColumnA)
         		
         		Dim _boundColumnB As BoundColumn = New BoundColumn()
         		_boundColumnB.HeaderText = "Actual Leads"
         		_boundColumnB.DataField = "Dlcount"
         		dtgOrderDetails.Columns.Add(_boundColumnB)
         		
         		Dim _boundColumnC As BoundColumn = New BoundColumn()
         		_boundColumnC.HeaderText = "Performance Rating"
         		_boundColumnC.DataField = "Dlpercent"
         		dtgOrderDetails.Columns.Add(_boundColumnC)

				Dim _dataView As DataView = _dataSet.Tables("ProgramDetail").DefaultView
         	_dataView.RowFilter = "LAP_tbl_pk='" & e.Item.Cells(0).Text & "'"
 
         'Bind the DataGrid.
         dtgOrderDetails.DataSource = _dataView
         dtgOrderDetails.DataBind()
 
         'Add the dtgOrderDetails DataGrid
         e.Item.Cells(1).Controls.Add(dtgOrderDetails)
       End If
     End Sub
        
        
        Sub clonead(ByVal sender As Object, ByVal e As EventArgs)
             dim x as button = sender
             Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String
            sqlproc = "sp_cloneAD"
            
            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
                Dim prmadpk As New SqlParameter("@ADpk", SqlDbType.Int)
                prmadpk.Value = request.querystring("adno")
                myCommandADD.Parameters.Add(prmadpk)
           
            Try
                myConnectionADD.Open()
                myCommandADD.ExecuteNonQuery()
                myConnectionADD.Close()
            Catch SQLexc As SqlException
                Response.Write("Insert Failed. Error Details are: " & SQLexc.ToString())
            End Try
            Session("AdSaved") = "False"
            getadno()
            if x.id="adcloneP" then
             insertADPLans(Session("adno"),request.querystring("adno"))
            end if
            createsysadplan()
            Session("AdSaved") = "True"
            
            Response.Redirect("createad.aspx?action=edit&cloned=true&adno=" & Session("adno"))
        End Sub
        
        sub  insertADPLans(id as string, OLPK as string)         
             Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String
            sqlproc = "sp_cloneADPlans"
            
            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
                Dim prmadpk As New SqlParameter("@ADpk", SqlDbType.Int)
                prmadpk.Value = id
                myCommandADD.Parameters.Add(prmadpk)
                
                 Dim prmNadpk As New SqlParameter("@ADOpk", SqlDbType.Int)
                prmNadpk.Value = OLPK
                myCommandADD.Parameters.Add(prmNadpk)
           
            Try
                myConnectionADD.Open()
                myCommandADD.ExecuteNonQuery()
                myConnectionADD.Close()
            Catch SQLexc As SqlException
                Response.Write("Insert Failed. Error Details are: " & SQLexc.ToString())
            End Try
       
       
       
       
       
       
       
       	end sub
        Sub editad(ByVal sender As Object, ByVal e As EventArgs)
            'adtext.ReadOnly = false
         
            adtitle.enabled=true
             adname.Enabled = true
            b_savee.visible=true
            pnltxtbody.visible=true
            bindfields()
        End Sub
        
        
        Public Sub previewpg2(ByVal sender As Object, ByVal e As EventArgs)
        		
        	dim normurl as string = "intakep.aspx?page=1&id=" & ddintakeresponse.selecteditem.value
             Response.Write("<script>window.open" & _
                    "('" & normurl & "','_new', 'width=900,height=600,resizable=1,scrollbars=1');</script>")

        End Sub
        public function checkcredits() as string
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "Select autopostc from dbo.tbl_users where uid = '" & Session("userid") & "'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    Return sqldr("autopostc")
                Else
                    Return "0"
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
        
        
        end function
        Public Function defaultexists() As Boolean
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "Select * from dbo.tbl_adbranding where br_uid_fk = '" & Session("userid") & "' and br_name='Default'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
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
        'Buttons
        Sub quickadtext(ByVal sender As Object, ByVal e As EventArgs)
            If qaddtext.Text = "Ad Text" Then
                pnltxtbody.Visible = True
                qaddtext.Text = "Hide Text"
                adtext.show=false
            Else
                pnltxtbody.Visible = False
                qaddtext.Text = "Ad Text"
            End If

        End Sub
        Sub setupdrsp(ByVal sender As Object, ByVal e As EventArgs)
            Response.Redirect("branding.aspx?action=new&source=admgr")

        End Sub
        Sub ExitAD(ByVal sender As Object, ByVal e As EventArgs)
            Session("AdSaved") = "False"
            Session("ADStage") = ""
            Session("adno") = ""
            If Request.QueryString("source") = "leads" Then
                Response.Redirect(Session("qstring"))
            Else
                Response.Redirect("admanager.aspx")
         

            End If
        End Sub
        Sub ExitADV(ByVal sender As Object, ByVal e As EventArgs)
             pnlLSdetail.visible=false
        	pnladdvenue.visible=false
          
          pnladdvenueN.visible=false
          pnlpostings.visible=false
          pnlPPdetail.visible=true
          bindADVenuesPP(session("CPlanId"))
        End Sub
        Sub buycredits(ByVal sender As Object, ByVal e As EventArgs)
            Response.Redirect("upgrades.aspx?source=adcreate&type=" & Request.QueryString("type") & "&adno=" & Session("adno"))
        End Sub
        
        
        Sub cancelfinalize(ByVal sender As Object, ByVal e As EventArgs)
         	if Session("AdSaved") = "False" then
	         	subnav("General")
	         	pnlconfirmfinalize.Visible = false
             
         	else
         		response.redirect ("createad.aspx?action=edit&adno=" & Session("adno"))
         	end if
         	
         	
         	
        end sub
        
         Sub savead(ByVal sender As Object, ByVal e As EventArgs)

           if Request.QueryString("action") = "new" then
      			'chkconfirmbrand.visible=false
      			Session("AdSaved") = "False"
      			insertad()
      			getadno()
      			session("CPlanMode")="Add"
					createsysadplan()
      			response.redirect ("createad.aspx?action=edit&adno=" & Session("adno"))
            else
            	insertad()
            end if
           

        End Sub
        
        sub createsysadplan()
        		PPName.text="System"
        		PPnoposts.text="1"
        		dd_PPfreq.SelectedIndex = dd_PPfreq.Items.IndexOf(dd_PPfreq.Items.FindBytext("Daily"))
        		PPDV.text="12"
        		dd_PPDperiod.SelectedIndex = dd_PPDperiod.Items.IndexOf(dd_PPDperiod.Items.FindBytext("Months"))
        		PPROP.text="1"
        		dd_PPstat.SelectedIndex = dd_PPstat.Items.IndexOf(dd_PPstat.Items.FindBytext("Active"))
        	 	TotPosts.Text = 9999
             PPEXP.text =9999
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim enddate As DateTime = RightNowAdd.adddays(365)
           	PPSdate.text=RightNowAdd.tostring
            PPEdate.text=enddate.tostring
            insertpplan()
     		end sub
     		
        
        Sub finalizeAD(ByVal sender As Object, ByVal e As EventArgs)
           
            If (ddintakeresponse.SelectedItem.Text = "Add New" Or ddintakeresponse.SelectedItem.Text = "Select.." Or ddlleadprogramFilter.SelectedItem.Text = "Select..") Then
                If ddintakeresponse.SelectedItem.Text = "Select.." Or ddintakeresponse.SelectedItem.Text = "Add New" Then
                    ddintakeresponse.BackColor = Red
                End If
                If ddlleadprogramFilter.SelectedItem.Text = "Select.." Then
                    ddlleadprogramFilter.BackColor = Red
                End If
            Else
					subnav("General")
                pnlconfirmfinalize.Visible = True
                pnlgeneral.visible=false
            End If


        End Sub
        
        Sub finalizesave(ByVal sender As Object, ByVal e As EventArgs)

            Session("ADStage") = "Finalized"
            finwarning.Visible = False
            dd_status.SelectedItem.Text = "Active"
            insertad()
            getadno()
            if  Session("AdSaved") = "False"
            	createsysadplan()
            end if
            Session("AdSaved") = "True"
            B_removead.Visible = True
            adstage.Text = "* Finalized *"
            pnlconfirmfinalize.Visible = False
            admain.Visible = True
            placead.Visible = False
            pnlsavedv.Visible = False
            pnlconfirmfinalize.Visible = False

            B_FinalV.Visible = False
            b_savee.Visible = False
            B_Final.Visible = False
            adtitle.Enabled = False
             adname.Enabled = False
            'dd_status.Enabled = False
            dd_mkprg.enabled=false
            ddemailcor.enabled=false
            
            ddlleadtypeFilter.Enabled = False
            ddlleadprogramFilter.Enabled = False
            ddintakeresponse.Enabled = False
            'adtext.ReadOnly = True
          
            B_post.Visible = True
            admain.Visible = False
            placead.Visible = True

            lbl_adnoV.Text = Session("adno")
            FillADvenues()
            'bindvenues()
            Response.Redirect("createad.aspx?action=edit&adno=" & Session("adno"))


        End Sub
        
          Sub postad(ByVal sender As Object, ByVal e As EventArgs)
            adautor.Visible = False
            cell1.visible = False
            cell1a.visible = False
         

            If Session("AdSaved") <> "True" Then
                Session("ADStage") = "Draft"
                insertad()
                getadno()
                Session("AdSaved") = "True"
            Else
                insertad()
            End If
            B_removead.Visible = True
            getadno()
            If Session("ADStage") = "Draft" Then
                B_FinalV.Visible = True
                finwarning.Visible = True
                finwarning.Text = "To publish AD to selected venues, AD must be Finalized"
            Else
                B_FinalV.Visible = False
            End If

            lbl_adnoV.Text = Session("adno")
            admain.Visible = False
            placead.Visible = True

            FillADvenues()
            'bindvenues()
           
        End Sub

        Sub postadVen(ByVal sender As Object, ByVal e As EventArgs)
            'Get next ADKEY and prefix with Venue code

            'Dim adkeycode As String = Left(advenue.SelectedItem.Text, 2) + getadkey()
            'adkeycode = adkeycode + getadkey()
            ' Response.Write(advenue.SelectedItem.Value)
            If advenue.SelectedItem.Text <> "Select.." Then
                getvenueinfo("")
                session("adkeycode") = session("adkeycode") + getadkey()
                'Response.Write(venueonline)
                'insertadV(session("adkeycode"), venueonline, "Master", DBNull.Value.ToString, DBNull.Value.ToString, DBNull.Value.ToString)
                'bindvenues()

                'pnlsavedv.Visible = True
                'advenue.SelectedIndex = advenue.Items.IndexOf(advenue.Items.FindByText("Select.."))
						
						'NEW
							pstsvenue.text=advenue.selecteditem.text
							pststatus.text="Unpublished"
							pstadkey.text= session("adkeycode")
							pnlLSdetail.visible=true
							pstEPdate.text=""
							pstadfrom.text=""
							pstadto.text=""
							pstadfrom.backcolor=white
            End If

            'Response.Write(session("adkeycode"))

            'Response.Redirect("postad.aspx?adno=" & Session("adno") & "&venue=" & advenue.SelectedItem.Text & "&adtype=" & Session("adtype"))
        End Sub
        
        public function checknoadvenues() as integer
	         Dim TotalNumberOfRowInDataGrid As Integer 
				return TotalNumberOfRowInDataGrid = CType(ADVenues.DataSource, DataView).Table.Rows.Count
				'response.write(TotalNumberOfRowInDataGrid)
        end function
        
       	sub removeven(Source As System.Object, e As System.EventArgs)
			
            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(0).Text
            'If ConfirmUserInput() Then
             '   Response.Write("Yes")
            'Else
             '   Response.Write("No")
'
            'End If
            deletevenue(content)
           'bindvenues()
				
        End Sub
        Sub setstatpub(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(0).Text
            pnlpostings.visible=false
            pnlgivepostno.visible=true
            Session("cadvno")=content
            

        End Sub
        Sub SavePSTNO(ByVal Source As System.Object, ByVal e As System.EventArgs)

				updatevstat(Session("cadvno"),pstpno.text,pstNDfrom.text,pstNDto.text)
				pnlpostings.visible=true
            pnlgivepostno.visible=false
            'bindvenues()
        end sub
        
           Sub ExitPlan(ByVal Source As System.Object, ByVal e As System.EventArgs)
        		Subnav("Venues")
            'bindvenues()
            Session("fvenues")="Published"
            bindADPlans()
            pnlPPwarn.visible=false
            removefromlist("-1")
            pnlshowadkeys.visible=false
            pnlpostings.visible=true
            pnlNewADPlan.visible=false
            pnlNewADPlanSV.visible=false
            cdrCalendar3.visible=false
        
        
        		end sub
         	
         	
         	
         Sub SavePPlan(ByVal Source As System.Object, ByVal e As System.EventArgs)
				if PPName.text.length > 0 then
					if PPnoposts.text.length <=0  then
						PPnoposts.text=1
					end if
					if PPDV.text.length <=0  then
						PPDV.text=1
					end if
					if PPROP.text.length <=0  then
						PPROP.text=1
					end if
					
					if session("CPlanMode")="Add" then 
						
						session("CPlanId")=getnewplanid()
						session("CPlanMode")="Edit"
					else
						
					end if
					pnlentposts.visible=false
					bindADVenuesPP(session("CPlanId"))
					refreshxleadsNOBT()
					insertpplan()
					if session("CPlanMode")="Edit" then
						if dd_PPstat.selecteditem.text="Inactive" then
							updateADVenues("Inactive","ADPlan","User")
	        				UpdateADPostDates("Inactive","ADPlan","User")
        				else
	        				updateADVenues("Active","ADPlan","User")
	        				UpdateADPostDates("Active","ADPlan","User")
						end if
					end if
					PPName.BackColor = white
					response.redirect("createad.aspx?action=edit&source=wrkpsts&adno=" & request.querystring("adno"))					
					
					
					
				else
					PPName.text="Required"
					PPName.BackColor = Red
				end if
        end sub
        
         Sub CancelNewVen(ByVal Source As System.Object, ByVal e As System.EventArgs)
					
					pnlPPdetail.visible=true
					pnlNewADPlanSV.visible=false
					
					pnlsverror.visible=false
					lblerror.text=""
					datesArray.Clear
			
			end sub
        
        
        
          Sub AddnewVen(ByVal Source As System.Object, ByVal e As System.EventArgs)
					session("CPlanMode")="Edit"
					pnlPPdetail.visible=false
					pnlNewADPlanSV.visible=true
					btnNPPNcancel.visible=true
					FillVenues()
					pnlsverror.visible=false
					lblerror.text=""
					datesArray.Clear
			
			end sub
        
          Sub NewPPlanPost(ByVal Source As System.Object, ByVal e As System.EventArgs)
				NewPPlanPostNOBT()
			end sub
         Sub NewPPlanPostNOBT()
				
				
						pnlPPdetail.visible=false
						pnladdvenueN.visible=true
						FillADvenues()
						if session("CPlanMode")="Add" then 
							insertpplan()
							session("CPlanId")=getnewplanid()
							session("CPlanMode")="Edit"
							refreshxleadsNOBT()
						elseif session("CPlanMode")="Edit"
							insertpplan()
							refreshxleadsNOBT()					
						end if
						pnlentposts.visible=false
						bindADVenuesPP(session("CPlanId"))
						
		
        end sub
        
        sub ppwarncontinue(ByVal Source As System.Object, ByVal e As System.EventArgs)
       	 	pnlPPwarn.visible=false
       	 	if session("adpoststyle") = "edit" then
								pnlPPdetailMain.visible=true
								pnlentposts.visible=false
								pnlPPdetail.visible=true
							else
								pnlPPdetailMain.visible=false
								pnlentposts.visible=false
								pnlPPdetail.visible=true
							end if	
        
        end sub
        
        public function checkpostexits() as boolean
          Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select * from tbl_LeadADVenues where av_lapfk='" & session("CPlanId") & "'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                   
                        Return True
                    Else
                        Return False
                   
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
        
        
        
        end function
        
        
        Sub SkipPSTNO(ByVal Source As System.Object, ByVal e As System.EventArgs)

				updatevstat(Session("cadvno"),"",pstNDfrom.text,pstNDto.text)
				pnlpostings.visible=true
            pnlgivepostno.visible=false
            'bindvenues()
        end sub
        
        Sub changevstat(ByVal Source As System.Object, ByVal e As System.EventArgs)
			
			 	Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim venupk As String = item.Cells(0).Text
             Dim venuname As String = item.Cells(1).Text
				dim venuestat as string = item.Cells(10).Text
				
				dim newstat as string
				if venuestat="Active" then
					newstat="Inactive"
					UpdateADPostDates("Inactive","ADPlanVenue","User")
        		else
					newstat="Active"
					UpdateADPostDates("Active","ADPlanVenue","User")
				end if
				doupdatevenue(newstat,venupk)
				bindPlansched(session("CPlanId"))
				cdrCalendar3.visible=false
            showcalc3.visible=false
            
            lblcal3.visible=false
        
      end sub
         Sub addpdates(ByVal Source As System.Object, ByVal e As System.EventArgs)
			
        	Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim venupk As String = item.Cells(0).Text
            Dim venuname As String = item.Cells(1).Text
            Dim venucode As String = item.Cells(9).Text
				dim venuestat as string = item.Cells(10).Text
				cdrCalendar3.visible=false
            showcalc3.visible=false
            
            lblcal3.visible=false
				session("CPlanMode")="Edit"
				session("vaddtevname")=venuname
				session("vaddtevkey")=venucode
				session("vaddtevPK")=venupk
					pnlPPdetail.visible=false
					pnlNewADPlanSV.visible=false
					pnlNewADPlanSVDW.visible=true
					FillVenues()
					pnlsverror.visible=false
					lblerror.text=""
					datesArray.Clear
					
					Calendar1.SelectedDates.Clear
        		
        				Button3.visible=true
        				
							lblCvenue.text =venuname & " " & venucode
	        				pnlNewADPlanSVDW.visible=true
	        				pnlNewADPlanSV.visible=false
	        				pnlNewADPlan.visible=false
	        				Button1.visible=false
	        				Button2.visible=false
	        				ButtonZ.visible=true
	        				Button3.visible=true
			
			end sub	
        
        
        
        
        
        Sub dopublish(ByVal Source As System.Object, ByVal e As System.EventArgs)
			
			 	Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim venupk As String = item.Cells(0).Text
            Dim venuname As String = item.Cells(1).Text
				dim venuestat as string = item.Cells(10).Text
				
					'session("Padno") = adcode
					
            	session("Pvenue") = venuname
            	session("Pvenueno") =venupk       
            	     	
            	session("keepadmfiltersA")="false"
            	session.remove("PubSearchFV")
            	session.remove("PubStatFV")
            	session.remove("PubADSFV")
            	session.remove("PubADPlanFV")
            	session.remove("PubTargetDateFV")
            	session.remove("PubADVenueFV")
            	
            	
				 	Response.Redirect("adpostings.aspx?source=planedit")
        
      end sub
        
        
        
        
        
        
        sub doupdatevenue(stat as string, id as string)
        			
            Dim strSql As String 
           	strSql = "update tbl_LeadADPlanVenues set lv_status='" & stat & "' where LV_tbl_pk='" & ID & "'"
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
        
         sub doupdateunpubs(name as string, id as string, venid as string)
        		
            Dim strSql As String 
           	strSql = "update tbl_LeadADVenues set av_adplaced='Canceled' where av_lapVenFK='" & venid & "' and av_lapfk='" & id & "' and av_adplaced='Unpublished'"
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
        
        
        
        
        

 			Sub ShowVschd(ByVal Source As System.Object, ByVal e As System.EventArgs)
				 Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim venupk As String = item.Cells(0).Text
            dim venuename as string = item.Cells(1).Text
            dim venuecode as string = item.Cells(9).Text
            cdrCalendar3.visible=true
            showcalc3.visible=true
            lblcal3.text =venuename
            lblcal3.visible=true
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            mycommand = "Select av_APTo  from tbl_LeadADVenues where av_lapVenFK = '" & venupk & "' and av_APTo is not null and av_adplaced <> 'Canceled'"
                       
            Dim ad As New SqlDataAdapter(mycommand, myConnection)
            Dim ds As New DataSet()
            Dim str As New StringBuilder()

            Dim sVenue As Integer

            Try
                ad.Fill(ds)
              
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
              cdrCalendar3.SelectedDates.Clear

          		Dim i As Integer

          		For i = 0 to ds.Tables(0).Rows.Count - 1
					if i =0 then
					'response.write(ds.Tables(0).Rows(i)(0))
						cdrCalendar3.VisibleDate  = ds.Tables(0).Rows(i)(0)
					end if
             	cdrCalendar3.SelectedDates.Add( cdate(ds.Tables(0).Rows(i)(0)) )
					
					
          Next
          session("UPTdateValue")=venupk
          session("UPTdate")="true"
					bindPlansched(session("cplanid"))


			end sub

        Sub pubad(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim advenuePK As String = item.Cells(1).Text
            Dim adcode As String = item.Cells(12).Text
            Dim advenue As String = item.Cells(2).Text
            'Dim adurl As String = item.Cells(25).Text
            'adurl = "http://" & adurl
            'Response.Redirect("postad.aspx?adno=" & adcode & "&venue=" & advenue & "&venueno=" & advenuePK & "&type=" & Request.QueryString("type"))
            ' If chkslfpub(advenue) Then
            	session("Padno") = adcode
            	session("Pvenue") = advenue
            	session("Pvenueno") =advenuePK
            	session("Ptype") = "Complete"
            	session("Pmodify") = chkcanmod(adcode)
            	session("Ppplan") = session("CPlanId")
            if checkforadkey(advenuePK) then
            	'Response.Redirect("postad.aspx?adno=" & adcode & "&venue=" & advenue & "&venueno=" & advenuePK & "&type=" & Request.QueryString("type") & "&modify=" & chkcanmod(adcode)& "&pplan=" & session("CPlanId"))
            	
            else
            	adtext.content = adtext.content + "<br ><mycustomtag class='myclass' contenteditable='false'>%ADKEY%</mycustomtag>"
               insertad()
               'Response.Redirect("postad.aspx?adno=" & adcode & "&venue=" & advenue & "&venueno=" & advenuePK & "&type=" & Request.QueryString("type") & "&modify=" & chkcanmod(adcode)& "&pplan=" & session("CPlanId"))
       
            end if
            Response.Redirect("postad.aspx")
            'Else
            'Response.Write("<script>window.open" & _
            ' "('" & adurl & "','_new', 'width=800,height=500');</script>")

            'End If

        End Sub
        
        Sub ModTGD(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim advenuePK As String = item.Cells(1).Text
           	session("UPTdate")= "true"
           	session("UPTdateControl")= "TargetDate"
           	session("UPTdateValue") = advenuePK
           	ADVenuesPP.Columns(6).Visible = true
        		ADVenuesPP.Columns(7).Visible = false
        		ADVenuesPP.Columns(10).Visible = false
        		ADVenuesPP.Columns(11).Visible = false
        		ADVenuesPP.Columns(13).Visible = false
        		ADVenuesPP.Columns(14).Visible = true
        		ADVenuesPP.Columns(15).Visible = true
        		bindADVenuesPP(session("CPlanId"))
        		
        end sub
        
        Sub EditPNO(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim advenuePK As String = item.Cells(1).Text
           	session("UPTdate")= "true"
           	session("UPTdateControl")= "PostNo"
           	session("UPTdateValue") = advenuePK
           	Dim txtPNO2 As textbox
           	txtPNO2 = item.Cells(9).FindControl("txtPNO")
           	txtPNO2.visible=true
           	Dim lblPNO2 As label
           	lblPNO2 = item.Cells(9).FindControl("lblPNO")
           	lblPNO2.visible=false
           	ADVenuesPP.Columns(10).Visible = false
        		ADVenuesPP.Columns(11).Visible = false
        		ADVenuesPP.Columns(13).Visible = false
        		ADVenuesPP.Columns(14).Visible = true
        		ADVenuesPP.Columns(15).Visible = true
        	
        end sub
        
         Sub SetVPub(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim advenuePK As String = item.Cells(1).Text
           	session("UPTdate")= "true"
           	session("UPTdateControl")= "PubDate"
           	session("UPTdateValue") = advenuePK
           	Dim txtPD2 As textbox
           	txtPD2 = item.Cells(8).FindControl("txtPdate")
           	txtPD2.visible=true
           	Dim lblPD2 As label
           	lblPD2 = item.Cells(8).FindControl("lblPdate")
           	lblPD2.visible=false
           	Dim txtPNO2 As textbox
           	txtPNO2 = item.Cells(9).FindControl("txtPNO")
           	txtPNO2.visible=true
           	Dim lblPNO2 As label
           	lblPNO2 = item.Cells(9).FindControl("lblPNO")
           	lblPNO2.visible=false
           	ADVenuesPP.Columns(10).Visible = false
        		ADVenuesPP.Columns(11).Visible = false
        		ADVenuesPP.Columns(13).Visible = false
        		ADVenuesPP.Columns(14).Visible = true
        		ADVenuesPP.Columns(15).Visible = true
        		
        end sub
        
        
        
        Sub ModTGDS(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim advenuePK As String = item.Cells(1).Text
            if session("UPTdateControl")= "TargetDate" then
             	Dim txttrgdate As textbox
            	txttrgdate = item.Cells(6).FindControl("txtTPD")            	
	      		updatevTPD(advenuePK,txttrgdate.text,"")
	      	elseif session("UPTdateControl")= "PostNo" then
	      		Dim txtPNO2 As textbox
           		txtPNO2 = item.Cells(9).FindControl("txtPNO")           	
	      		updatevTPD(advenuePK,txtPNO2.text,"") 
           		txtPNO2.visible=false
           		Dim lblPNO2 As label
	           	lblPNO2 = item.Cells(9).FindControl("lblPNO")
	           	lblPNO2.visible=true
	      	elseif session("UPTdateControl")= "PubDate" then
	      		Dim txtPD2 As textbox
           		txtPD2 = item.Cells(8).FindControl("txtPdate")
           		txtPD2.visible=false
           		Dim lblPD2 As label
           		lblPD2 = item.Cells(8).FindControl("lblPdate")
           		lblPD2.visible=true
           		Dim txtPNO2 As textbox
           		txtPNO2 = item.Cells(9).FindControl("txtPNO")           	
	      		updatevTPD(advenuePK,txtPD2.text,txtPNO2.text) 
           		txtPNO2.visible=false
           		Dim lblPNO2 As label
	           	lblPNO2 = item.Cells(9).FindControl("lblPNO")
	           	lblPNO2.visible=true
           		if Session("ADStage") <> "Finalized" then
            		dofinalize()       
	           	end if
	      	
            end if
           	ADVenuesPP.Columns(6).Visible = false
        		ADVenuesPP.Columns(7).Visible = true
        		ADVenuesPP.Columns(10).Visible = true
        		ADVenuesPP.Columns(11).Visible = true
        		ADVenuesPP.Columns(13).Visible = true
        		ADVenuesPP.Columns(14).Visible = false
        		ADVenuesPP.Columns(15).Visible = false
           	session("UPTdate")= "false"
           	session("UPTdateValue") = advenuePK
           	session("UPTdateControl")= ""
        		bindADVenuesPP(session("CPlanId"))
        		
        end sub
        
         Sub ModTGDSC(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim advenuePK As String = item.Cells(1).Text
            session("UPTdate")= "false"
           	session("UPTdateValue") = advenuePK
           	Dim txtPNO2 As textbox
           	txtPNO2 = item.Cells(9).FindControl("txtPNO")
           	txtPNO2.visible=false
           	Dim lblPNO2 As label
           	lblPNO2 = item.Cells(9).FindControl("lblPNO")
           	lblPNO2.visible=true
           	ADVenuesPP.Columns(6).Visible = false
        		ADVenuesPP.Columns(7).Visible = true
        		ADVenuesPP.Columns(10).Visible = true
        		ADVenuesPP.Columns(11).Visible = true
        		ADVenuesPP.Columns(13).Visible = true
        		ADVenuesPP.Columns(14).Visible = false
        		ADVenuesPP.Columns(15).Visible = false
      		bindADVenuesPP(session("CPlanId"))
      		session("UPTdateControl")= ""
        		
        end sub
        
        sub updatevTPD(id as string, tdate as string,tdate2 as string)
        
          'Read in the values of the updated row
          
							
            Dim strSql As String 
            if session("UPTdateControl")= "TargetDate" then
           		strSql = "update tbl_LeadADVenues set av_APTo='" & tdate & "' where tbl_leadadvenues='" & id & "'"
       		elseif session("UPTdateControl")= "PostNo" then
       			strSql = "update tbl_LeadADVenues set av_Postingno='" & tdate & "' where tbl_leadadvenues='" & id & "'"
       		elseif session("UPTdateControl")= "PubDate" then
       			strSql = "update tbl_LeadADVenues set av_APFrom='" & tdate & "', av_Postingno='" & tdate2 & "', av_adplaced='Published' where tbl_leadadvenues='" & id & "'"
       	
       		end if
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
        
        
        
        
        
			
			public function checkforadkey(id as string) as boolean
			   Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select *  from dbo.tbl_LeadADs " _
            					& "where ad_text like '%%ADKEY%%' " _
            					& "and tbl_leadad_pk = '" & request.querystring("adno") & "' and ad_userid='" & session("userid") & "'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    
                        Return True
                    Else
                        Return False
                  

                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try

			
			
			end function	



        Public Function chkslfpub(ByVal ldsrc As String) As Boolean
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select x_canselfpub  from tbl_xwalk where x_type='leadsource' and x_descr='" & ldsrc & "'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    If Sqldr("x_canselfpub") = "Yes" Then
                        Return True
                    Else
                        Return False
                    End If

                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try

        End Function
        Public Function chkcanmod(id as string) As string
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select distinct av_adplaced from dbo.tbl_LeadADs " _
							& "join dbo.tbl_LeadADVenues on av_leadads_FK = tbl_leadad_pk " _
							& "where tbl_leadad_pk =" & id & " and av_adplaced='Published'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    Return "No"
                else
                	  Return "Yes"
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try

        End Function
        
        
        Sub clipboarda(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(4).Text
            'Response.Write(content)

            xx.Text = content
            Dim msg As String = ""
            msg = msg & "<Script Language='JavaScript'>"
            msg = msg & "copy(document.getElementById('xx'));"
            msg = msg & "</Script>"
            Response.Write(msg)
            ' javascript:window.clipboardData.setData('Text', txtProblemDescription.innerText)")

        End Sub
        
        sub Addposting (Source As System.Object, e As System.EventArgs)
			
			pnlNewADPlan.visible=true
			pnlPPdetail.visible=false
			pnlpostings.visible=false
			pnlentposts.visible=false
			pnlNewADPlanSVDW.visible=false
	        		pnlNewADPlanSV.visible=false
	       btnNPPNskip.visible=true 		
			dd_Pubfilter.SelectedIndex = dd_Pubfilter.Items.IndexOf(dd_Pubfilter.Items.FindByText("All"))
			Session("fvenues")="All"
			session("CPlanMode")="Add"
			'bindADVenuesPP()
			'Check if this is the second post, if so has ad been finalized.  If not error, if so let post
			'pnladdvenueN.visible=true
			'pnlpostings.visible=false
			'FillADvenues()
			FillVenues()
			session("CPlanMode")="Add"
			txtNPSdate.text=""
			txtNPEdate.text=""
			dd_PPfreqNP.SelectedIndex = dd_PPfreqNP.Items.IndexOf(dd_PPfreqNP.Items.FindByText("Select..")) 
			txtNPNoposs.text=""
			txtNPNoVens.text=""
			txtNPNoLeads.text=""
			txtNPPName.text=""
			dd_ActivatePPNow.SelectedIndex = dd_ActivatePPNow.Items.IndexOf(dd_ActivatePPNow.Items.FindByText("Select..")) 
			dd_PostPPNow.SelectedIndex = dd_PostPPNow.Items.IndexOf(dd_PostPPNow.Items.FindByText("Select..")) 
		
			
        end sub
        
         Sub FillVenues()
				Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='leadsource' and  (x_company='All' or x_uid='" & Session("userid") & "') order by x_descr"
            '(x_company='" & Session("company") & "' or
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                lb_selvenues.DataSource = dataReader
                lb_selvenues.DataTextField = "x_descr"
                lb_selvenues.DataValueField = "tbl_xwalk_pk"
                lb_selvenues.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

        End Sub
        
        
         sub updateNPlabel (Source As System.Object, e As System.EventArgs)
       		dim x as string
       		if dd_PPfreqNP.selecteditem.text="Daily" then
       			x="Day"
       		elseif dd_PPfreqNP.selecteditem.text="Weekly"
       			x="Week"
       		else
       			x="Month"
       		end if
       
       		lblnopostsN.text=x
       
       
       	end sub
       	
       	
        sub removead (Source As System.Object, e As System.EventArgs)
        		Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "delete from tbl_LeadADs where tbl_leadad_pk='" & session("adno") & "'"
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
            
            strSql = "delete from tbl_LeadADVenues where av_leadads_FK='" & session("adno") & "'"
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
            
            Session("AdSaved") = "False"
            Session("ADStage") = ""
            session("aadno")=	""
            Response.Redirect("admanager.aspx")
        End Sub
        
        Public function getnewplanid() as string
        		Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select cast(max(LAP_tbl_pk) as varchar(255)) as 'PPPK'  from dbo.tbl_leadADPlans"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    return  Sqldr("PPPK")
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
        
        end function
        
        Public Sub getvenueinfo(src as string)

            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            
            Dim strSql As String 
            if src = "" then
            	strSql= "select * from tbl_xwalk where tbl_xwalk_pk='" & advenue.SelectedItem.Value & "'"
            else
            		strSql= "select * from tbl_xwalk where tbl_xwalk_pk='" & src  & "'"
            
            end if
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    If Sqldr("x_online") IsNot DBNull.Value Then
                        session("venueonline") = Sqldr("x_online")
                    Else
                        session("venueonline") = "No"
                    End If
                    If Sqldr("x_id") IsNot DBNull.Value Then
                        session("adkeycode") = Sqldr("x_id")
                    Else
                        session("adkeycode") = Left(advenue.SelectedItem.Text, 2)
                    End If
                    If Sqldr("x_html") IsNot DBNull.Value Then
                        session("venhtml") = Sqldr("x_html")
                    Else
                        session("venhtml") = "No"
                    End If

                End If


            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try


        End Sub
        
        
        'DB Binds     
        
        sub deletevenue(id as string)
         	Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "delete from tbl_LeadADVenues where tbl_leadadvenues='" & id & "'"
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

        end sub
        
        Sub bindfields()

            Dim strUID As String = Session("userid")
         Dim strSql As String = "SELECT *, cast(tbl_leadad_pk as varchar(255)) as 'ADno' from tbl_leadads where tbl_leadad_pk ='" & Session("adno") & "'"

            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
							adno.text= "# " &  Sqldr("ADno")
                    If Sqldr("ad_title") IsNot DBNull.Value Then
                        adtitle.Text = Sqldr("ad_title")
                    End If
                    If Sqldr("ad_cat") IsNot DBNull.Value Then
                        adname.Text = Sqldr("ad_cat")
                    End If
                    If Sqldr("ad_status") IsNot DBNull.Value Then
                        dd_status.SelectedIndex = dd_status.Items.IndexOf(dd_status.Items.FindByText(Sqldr("ad_status")))
                    End If
                    If Sqldr("ad_Leadtype") IsNot DBNull.Value Then
                        ddlleadtypeFilter.SelectedIndex = ddlleadtypeFilter.Items.IndexOf(ddlleadtypeFilter.Items.FindByText(Sqldr("ad_Leadtype")))
                    End If
                    If Sqldr("ad_Leadprogram") IsNot DBNull.Value Then
                        ddlleadprogramFilter.SelectedIndex = ddlleadprogramFilter.Items.IndexOf(ddlleadprogramFilter.Items.FindByText(Sqldr("ad_Leadprogram")))
                    End If
                    If Sqldr("ad_text") IsNot DBNull.Value Then
                        'adtext.Text = Sqldr("ad_text")
                        adtext.content = Sqldr("ad_text")
                        'adtxtreadonly.text=Sqldr("ad_text")
                        if Session("ADStage") = "Finalized" then
                        	lbledittext.text = Sqldr("ad_text")
                        end if
                    End If
                    If Sqldr("ad_intakeresponse") IsNot DBNull.Value Then
                        ddintakeresponse.SelectedIndex = ddintakeresponse.Items.IndexOf(ddintakeresponse.Items.FindByValue(Sqldr("ad_intakeresponse")))
                    End If
                    If Sqldr("ad_marketprogram") IsNot DBNull.Value Then
                        dd_mkprg.SelectedIndex = dd_mkprg.Items.IndexOf(dd_mkprg.Items.FindBytext(Sqldr("ad_marketprogram")))
                    End If
                    If Sqldr("ad_Stillallowleads") IsNot DBNull.Value Then
                       if Sqldr("ad_Stillallowleads")="Y" then
                       		chkstillgetleads.checked=true
                       else
                       		chkstillgetleads.checked=false
                       end if
                    else
                    		chkstillgetleads.checked=false
                       
                    End If
                    
                    
                    
                    Session("adstage") = Sqldr("ad_stage")
                    adstage.Text = "* " & Sqldr("ad_stage") & " *"

                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        End Sub

       
      
        Public Function getadkey() As String
            Dim keycode As String
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select cast (AK_NextKey as nvarchar(255)) as 'kc'  from dbo.tbl_nextadkey"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    keycode = Sqldr("kc")                  
                    return Sqldr("kc")
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
			end function
			
			public sub incADKEY()
			 	Dim keycode As String
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "update dbo.tbl_nextadkey set AK_NextKey =  AK_NextKey+1 "
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
           
        End sub
        
        Sub FillLeadTypeDropDown()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='leadtype' and (x_uid='" & Session("userid") & "' or x_company='All') order by x_descr"
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
        Sub FillLeadprogramDropDown()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='leadprogram' and (x_uid='" & Session("userid") & "' or x_company='All') order by x_descr"
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
            ddlleadprogramFilter.Items.Insert(0, New ListItem("None", "9999"))
            'ddlleadprogramFilter.Items.Insert(1, New ListItem("Add New", "99992"))
        End Sub
        Sub FillADCatDropDown()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='AdType' and (x_uid='" & Session("userid") & "' or x_company='All')"
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                dd_adtype.DataSource = dataReader
                dd_adtype.DataTextField = "x_descr"
                dd_adtype.DataValueField = "tbl_xwalk_pk"
                dd_adtype.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

        End Sub
        
        Sub FillADvenues()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='leadsource' and (x_company='All' or x_Uid='" & Session("userid") & "') order by x_descr"
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                advenue.DataSource = dataReader
                advenue.DataTextField = "x_descr"
                advenue.DataValueField = "tbl_xwalk_pk"
                advenue.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

            advenue.Items.Insert(0, New ListItem("Select..", "9999"))


        End Sub
        Sub Fillintakeresponse()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select * from  dbo.tbl_adbranding where br_uid_fk='" & Session("userid") & "' and br_bstat='Active' order by br_name"
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                ddintakeresponse.DataSource = dataReader
                ddintakeresponse.DataTextField = "br_name"
                ddintakeresponse.DataValueField = "tbl_branding_pk"
                ddintakeresponse.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
            'ddintakeresponse.Items.Insert(0, New ListItem("Select..", "9999"))
            ddintakeresponse.Items.Insert(1, New ListItem("Add New", "99992"))
            ddintakeresponse.SelectedIndex = ddintakeresponse.Items.IndexOf(ddintakeresponse.Items.FindByText("Default"))
        End Sub


			Sub insertpplan()
            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String

		     	If session("CPlanMode")="Edit" Then
             	sqlproc = "sp_updateADPlan"
          	Else
              	sqlproc = "sp_AddADPlan"
          	End If
            

            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure
		
				if session("CPlanMode")="Edit" Then
					Dim prmPPpk As New SqlParameter("@PPpk", SqlDbType.Int)
	            prmPPpk.Value = session("CPlanId")
	            myCommandADD.Parameters.Add(prmPPpk)
				end if
				
		      Dim prmadpk As New SqlParameter("@adno", SqlDbType.Int)
            prmadpk.Value = session("adno")
            myCommandADD.Parameters.Add(prmadpk)
            
            Dim prmuid As New SqlParameter("@aduid", SqlDbType.VarChar, 50)
            prmuid.Value = Session("userid")
            myCommandADD.Parameters.Add(prmuid)

            Dim prmPPname As New SqlParameter("@PPname", SqlDbType.VarChar, 50)
            prmPPname.Value = PPName.text
            myCommandADD.Parameters.Add(prmPPname)
				
				Dim prmPPexpposts As New SqlParameter("@PPexpposts", SqlDbType.VarChar, 50)
            prmPPexpposts.Value = PPnoposts.text
            myCommandADD.Parameters.Add(prmPPexpposts)
          
            Dim prmppfreq As New SqlParameter("@ppfreq", SqlDbType.VarChar, 50)
            prmppfreq.Value = dd_PPfreq.SelectedItem.Text
            myCommandADD.Parameters.Add(prmppfreq)
            
            Dim prmPPDno As New SqlParameter("@PPDno", SqlDbType.VarChar, 50)
            prmPPDno.Value = PPDV.text
            myCommandADD.Parameters.Add(prmPPDno)
            
            Dim prmppduration As New SqlParameter("@ppduration", SqlDbType.VarChar, 50)
            prmppduration.Value = dd_PPDperiod.SelectedItem.Text
            myCommandADD.Parameters.Add(prmppduration)
            
            Dim prmPPROP As New SqlParameter("@PPROP", SqlDbType.VarChar, 50)
            prmPPROP.Value = PPROP.text
            myCommandADD.Parameters.Add(prmPPROP)
            
            Dim prmppstat As New SqlParameter("@ppstat", SqlDbType.VarChar, 50)
            prmppstat.Value = dd_PPstat.SelectedItem.Text
            myCommandADD.Parameters.Add(prmppstat)
            
            Dim prmtotposts As New SqlParameter("@totposts", SqlDbType.nVarChar, 50)
            prmtotposts.Value = TotPosts.Text
            myCommandADD.Parameters.Add(prmtotposts)
            
             Dim prmEvenues As New SqlParameter("@Evenues", SqlDbType.nVarChar, 50)
            prmEvenues.Value = PPEXP.text
            myCommandADD.Parameters.Add(prmEvenues)
            
            Dim prmAvenues As New SqlParameter("@Avenues", SqlDbType.nVarChar, 50)
            prmAvenues.Value = "0"
            myCommandADD.Parameters.Add(prmAvenues)
            
            Dim prmadstage As New SqlParameter("@Adstage", SqlDbType.nVarChar, 50)
            prmadstage.Value = dbnull.value
            myCommandADD.Parameters.Add(prmadstage)

				Dim prmadsdate As New SqlParameter("@Sdate", SqlDbType.nVarChar, 50)
            prmadsdate.Value = PPSdate.text
            myCommandADD.Parameters.Add(prmadsdate)
            
            Dim prmadedate As New SqlParameter("@Edate", SqlDbType.nVarChar, 50)
            prmadedate.Value = PPEdate.text
            myCommandADD.Parameters.Add(prmadedate)            

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







        Sub insertad()
            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String

            If (Request.QueryString("action") = "new" or Request.QueryString("action") = "clone" ) Then
                If Session("AdSaved") = "True" Then
                    sqlproc = "sp_updateAD"
                Else
                    sqlproc = "sp_AddAD"
                End If
            ElseIf Request.QueryString("action") = "edit" Then
                sqlproc = "sp_updateAD"
            End If

            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

            If Session("AdSaved") = "True" Then
                Dim prmadpk As New SqlParameter("@adno", SqlDbType.Int)
                prmadpk.Value = Session("adno")
                myCommandADD.Parameters.Add(prmadpk)
            End If

            Dim prmstage As New SqlParameter("@adstage", SqlDbType.VarChar, 50)
            prmstage.Value = Session("ADStage")
            myCommandADD.Parameters.Add(prmstage)

            Dim prmuid As New SqlParameter("@aduid", SqlDbType.VarChar, 50)
            prmuid.Value = Session("userid")
            myCommandADD.Parameters.Add(prmuid)

            Dim prmadtitle As New SqlParameter("@adtitle", SqlDbType.VarChar, 255)
            prmadtitle.Value = adtitle.Text
            myCommandADD.Parameters.Add(prmadtitle)

            Dim prmadtext As New SqlParameter("@adtext", SqlDbType.Text)
            prmadtext.Value = adtext.content
            'adtext.Text '.Replace(vbCrLf, "<br>")
            myCommandADD.Parameters.Add(prmadtext)

            Dim prmadstat As New SqlParameter("@dd_status", SqlDbType.VarChar, 30)
            'if Session("ADStage") = "Finalized" then
           	'	prmadstat.Value = "Active"
           '	else
            	prmadstat.Value = dd_status.SelectedItem.Text
            'end if
            myCommandADD.Parameters.Add(prmadstat)

            Dim prmadleadtype As New SqlParameter("@ddlleadtypeFilter", SqlDbType.VarChar, 30)
            prmadleadtype.Value = ddlleadtypeFilter.SelectedItem.Text
            myCommandADD.Parameters.Add(prmadleadtype)

            Dim prmadleadprogram As New SqlParameter("@ddlleadprogramFilter", SqlDbType.VarChar, 30)
            prmadleadprogram.Value = ddlleadprogramFilter.SelectedItem.Text
            myCommandADD.Parameters.Add(prmadleadprogram)

            Dim prmadresponse As New SqlParameter("@adresponse", SqlDbType.VarChar, 50)
            prmadresponse.Value = ddintakeresponse.SelectedItem.Value
            myCommandADD.Parameters.Add(prmadresponse)

				Dim prmmkprg As New SqlParameter("@marketprog", SqlDbType.VarChar, 50)
            prmmkprg.Value = dd_mkprg.SelectedItem.Text
            myCommandADD.Parameters.Add(prmmkprg)

            'Dim prmadtype As New SqlParameter("@dd_adtype", SqlDbType.VarChar, 30)
            'prmadtype.Value = dd_adtype.SelectedItem.Text
            'myCommandADD.Parameters.Add(prmadtype)
            'Session("adtype") = dd_adtype.SelectedItem.Text

            Dim prmadname As New SqlParameter("@dd_adname", SqlDbType.VarChar, 50)
            prmadname.Value = adname.Text
            myCommandADD.Parameters.Add(prmadname)
            
            Dim prmAllowLeads As New SqlParameter("@allowleads", SqlDbType.VarChar, 50)
            if chkstillgetleads.checked then 
            	prmAllowLeads.Value = "Y"
            else
            	prmAllowLeads.Value = "N"
            end if
            myCommandADD.Parameters.Add(prmAllowLeads)
            
            

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
        
        Sub getadno()
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select cast(max(tbl_leadad_pk) as varchar(255)) as 'AdPK'  from dbo.tbl_LeadADs"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    Session("adno") = Sqldr("AdPK")
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
        End Sub
        
        Sub insertadV(ByVal adcode As String, ByVal vonline As String, ByVal stat As String, ByVal d1 As String, ByVal d2 As String, ByVal tot As String, adplaced as string, PPID as string)
            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String
            sqlproc = "sp_AddADVenue"

            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

            Dim prmadno As New SqlParameter("@av_leadads_FK", SqlDbType.Int)
            prmadno.Value = request.querystring("adno")
            myCommandADD.Parameters.Add(prmadno)

            Dim prmadvenue As New SqlParameter("@av_name", SqlDbType.VarChar, 50)
            prmadvenue.Value = advenue.SelectedItem.Text
            myCommandADD.Parameters.Add(prmadvenue)

            Dim prmarepsond As New SqlParameter("@av_autorespond", SqlDbType.VarChar, 50)
            prmarepsond.Value = adautor.SelectedItem.Text
            myCommandADD.Parameters.Add(prmarepsond)

            Dim prmaphoto As New SqlParameter("@av_photo", SqlDbType.VarChar, 50)
            prmaphoto.Value = adphoto.SelectedItem.Text
            myCommandADD.Parameters.Add(prmaphoto)

            Dim prmadcode As New SqlParameter("@av_key", SqlDbType.NVarChar, 255)
            prmadcode.Value = adcode
            myCommandADD.Parameters.Add(prmadcode)

            Dim prmadplaced As New SqlParameter("@av_adplaced", SqlDbType.VarChar, 50)
            If vonline = "No" Then
                prmadplaced.Value = "Published"
            ElseIf stat = "Slave" Then
                prmadplaced.Value = "Autoposting"
            Else
            	if adplaced ="MP" then
            		prmadplaced.Value = "Published"
            	else
                	prmadplaced.Value = "Unpublished"
           		end if
            End If
            myCommandADD.Parameters.Add(prmadplaced)

            Dim prmonline As New SqlParameter("@av_online", SqlDbType.VarChar, 50)
            prmonline.Value = vonline
            myCommandADD.Parameters.Add(prmonline)

            Dim prmkeyurl As New SqlParameter("@av_keyurl", SqlDbType.NVarChar, 255)
            prmkeyurl.Value = System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "/intake.aspx?adcode=" & adcode
            myCommandADD.Parameters.Add(prmkeyurl)

            Dim prmAllText As New SqlParameter("@av_textAll", SqlDbType.Text)
            prmAllText.Value = adtext.content
            'adtext.Text.Replace(vbCrLf, "<br>") & "<br><br>Please Click-> " & System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "/intake.aspx?adcode=" & adcode
            myCommandADD.Parameters.Add(prmAllText)

            Dim prmstat As New SqlParameter("@av_apstat", SqlDbType.VarChar, 50)
            prmstat.Value = stat
            myCommandADD.Parameters.Add(prmstat)

				Dim prmppid As New SqlParameter("@av_ppid", SqlDbType.int)
            prmppid.Value = ppid
            myCommandADD.Parameters.Add(prmppid)

				Dim prmpostno As New SqlParameter("@av_postno", SqlDbType.VarChar, 50)
            prmpostno.Value = pstadto.text
            myCommandADD.Parameters.Add(prmpostno)


            '---
            If stat = "Slave" Then
                Dim prmapfrom As New SqlParameter("@av_APFrom", SqlDbType.DateTime)
                prmapfrom.Value = d1
                myCommandADD.Parameters.Add(prmapfrom)

                Dim prmapto As New SqlParameter("@av_APTo", SqlDbType.DateTime)
                prmapto.Value = d2
                myCommandADD.Parameters.Add(prmapto)

                Dim prmcnt As New SqlParameter("@av_APUnitCount", SqlDbType.Int)
                prmcnt.Value = tot
                myCommandADD.Parameters.Add(prmcnt)

                Dim prmautop As New SqlParameter("@av_autopost", SqlDbType.VarChar, 50)
                prmautop.Value = "Yes"
                myCommandADD.Parameters.Add(prmautop)
            Else
                Dim prmapfrom As New SqlParameter("@av_APFrom", SqlDbType.datetime)
                if pstadfrom.text = "" then
                	prmapfrom.Value = dbnull.value
                else
                	 prmapfrom.Value = pstadfrom.text
                end if       
               
                myCommandADD.Parameters.Add(prmapfrom)

                Dim prmapto As New SqlParameter("@av_APTo", SqlDbType.datetime)
               	
               	if pstadfrom.text = "" then
                	 prmapto.Value = dbnull.value
                else
                	 prmapto.Value = pstEPdate.text
                end if      
               
                myCommandADD.Parameters.Add(prmapto)

                Dim prmcnt As New SqlParameter("@av_APUnitCount", SqlDbType.Int)            
                
                prmcnt.Value = DBNull.Value
                myCommandADD.Parameters.Add(prmcnt)

                Dim prmautop As New SqlParameter("@av_autopost", SqlDbType.VarChar, 50)
                prmautop.Value = "No"
                myCommandADD.Parameters.Add(prmautop)
            End If

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
        
        Public Sub bindvenues()
            Dim strUID As String = Session("userid")
            If Session("adno") = "" Then
                Session("adno") = Request.QueryString("adno")
            End If
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String = "Select cast(av_leadads_FK as nvarchar(255)) as 'adno', cast(tbl_leadadvenues as nvarchar(255)) as 'venno', " _
                & "convert(varchar(20),av_APFrom,101) as 'APFrom', convert(varchar(20),av_APTo,101) as 'APFTo', x_canselfpub,* " _
                & "from tbl_LeadADVenues " _
                & "join tbl_LeadADs on tbl_leadad_pk=av_leadads_FK  " _
                & "join dbo.tbl_xwalk on x_descr  =av_name and x_type='leadsource' " _
                & "where av_leadads_FK ='" & Session("adno") & "'"

            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_LeadADVenues")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_LeadADVenues"))
						
                ADVenues.DataSource = dvProducts
                ADVenues.DataBind()
					
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
            'response.write(checknoadvenues())
            
            if checknoadvenues()  < 0 then
                 pnlnoadvenues.visible=true
                 pnlsavedv.visible=false
            else
             	pnlnoadvenues.visible=false
             	pnlsavedv.visible=true
             end if  
            
        End Sub

 		Public Sub bindADPlans()
            Dim strUID As String = Session("userid")
           
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String = "Select * from tbl_leadADPlans where lap_adfk='" & request.querystring("adno") & "'"

            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_leadADPlans")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_leadADPlans"))
						
                ADPlans.DataSource = dvProducts
                ADPlans.DataBind()
					
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
            
            
        End Sub
        
          Sub btnsearch(ByVal Source As System.Object, ByVal e As System.EventArgs)
            ADVenuesPP.CurrentPageIndex = 0
            bindADVenuesPP(session("CPlanId"))
        End Sub
        
       Public Sub bindadkeys(id as string)
             Dim strUID As String = Session("userid")
            
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
             mycommand = "select lv_name,lv_key,cast (lk_keyurl as varchar(255)) as 'KeyUrl' " _
									& "from dbo.tbl_LeadADPlanVenues " _
									& "where lv_adplan_fk ='" & id & "' " _
		   						& "group by lv_name,lv_key,cast (lk_keyurl as varchar(255)) " 
		                		
		   Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_LeadADPlanVenues")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_LeadADPlanVenues"))
					 ADKeys.DataSource = dvProducts
                ADKeys.DataBind()
					
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
         
        end sub
        
          
        Public Sub bindPlansched(id as string)
           Dim strUID As String = Session("userid")
            
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
             mycommand =    "Begin " _
					   &"select av_name,av_key,count(*) as 'Unpubnt' " _
					   &"into #tmpa  " _
					   &"from tbl_LeadADVenues " _
					   &"where av_lapfk ='" & id & "' " _
					   &"and av_adplaced = 'Unpublished' " _
					   &"group by av_name,av_key " _
					   &"select av_name,av_key,count(*) as 'Pubcnt' " _
					   &"into #tmpb " _
					   &"from tbl_LeadADVenues " _
					   &"where av_lapfk ='" & id & "' " _
					   &"and av_adplaced = 'Published' " _
					   &"group by av_name,av_key " _
					   &"select A.LV_tbl_pk,a.lv_name,a.lv_status,a.lv_key,lv_sunday,lv_monday,lv_tuesday,lv_wednesday,lv_thursday,lv_firday,lv_saturday, " _
					   &"case when (X.Unpubnt is null) then 0 else X.Unpubnt end as 'Unpubcnt', " _
					   &"case when (Y.Pubcnt is null) then 0 else Y.Pubcnt end as 'Pubcnt' " _
					   &"from  dbo.tbl_LeadADPlanVenues A " _
					   &"left join #tmpa X on X.av_name=A.lv_name and  X.av_key=a.lv_key " _
					   &"left join #tmpb Y on Y.av_name=A.lv_name  and  y.av_key=a.lv_key " _
					   &"where a.lv_adplan_fk='" & id & "' " _
					   &"end " _
        
	        Try
	                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
	                Dim dataSet As New DataSet()
	                dataAdapter.Fill(dataSet, "tbl_LeadADPlanVenues")
	                Dim dvProducts As New DataView(dataSet.Tables("tbl_LeadADPlanVenues"))
						 if dd_schstatfilter.selecteditem.text <> "All" then
						 		dvProducts.RowFilter = "lv_status = '" & dd_schstatfilter.selecteditem.text & "'"
						 end if
						 
	                ADPlanSched.DataSource = dvProducts
	                ADPlanSched.DataBind()
						
	            Catch exc As System.Exception
	                Response.Write(exc.ToString())
	            Finally
	                myConnection.Dispose()
	            End Try
	        
	       end sub
        
        
        Public Sub bindADVenuesPP(id as string)
             Dim strUID As String = Session("userid")
            
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            if Session("fvenues")="Published" then
             mycommand = "Select cast(av_leadads_FK as nvarchar(255)) as 'adno', cast(tbl_leadadvenues as nvarchar(255)) as 'venno', " _
		                & "convert(varchar(20),av_APFrom,101) as 'APFrom', convert(varchar(20),av_APTo,101) as 'APFTo', x_canselfpub,* " _
		                & "from tbl_LeadADVenues " _
		                & "join tbl_LeadADs on tbl_leadad_pk=av_leadads_FK  " _
		                & "join dbo.tbl_xwalk on x_descr  =av_name and x_type='leadsource' " _
		                & "where av_lapfk ='" & id & "' and av_adplaced='Published'"
		      elseif Session("fvenues")="Unpublished"
					mycommand = "Select cast(av_leadads_FK as nvarchar(255)) as 'adno', cast(tbl_leadadvenues as nvarchar(255)) as 'venno', " _
		                & "convert(varchar(20),av_APFrom,101) as 'APFrom', convert(varchar(20),av_APTo,101) as 'APFTo', x_canselfpub,* " _
		                & "from tbl_LeadADVenues " _
		                & "join tbl_LeadADs on tbl_leadad_pk=av_leadads_FK  " _
		                & "join dbo.tbl_xwalk on x_descr  =av_name and x_type='leadsource' " _
		                & "where av_lapfk ='" & id & "' and av_adplaced='Unpublished'"
		      else
		      mycommand = "Select cast(av_leadads_FK as nvarchar(255)) as 'adno', cast(tbl_leadadvenues as nvarchar(255)) as 'venno', " _
		                & "convert(varchar(20),av_APFrom,101) as 'APFrom', convert(varchar(20),av_APTo,101) as 'APFTo', x_canselfpub,* " _
		                & "from tbl_LeadADVenues " _
		                & "join tbl_LeadADs on tbl_leadad_pk=av_leadads_FK  " _
		                & "join dbo.tbl_xwalk on x_descr  =av_name and x_type='leadsource' " _
		                & "where av_lapfk ='" & id & "'"
		      
		      end if
            	
            
            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_LeadADVenues")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_LeadADVenues"))
					 if ppvensearch.text <> "" then
					 	dvProducts.RowFilter = "(av_key like '%" & ppvensearch.text & "%' or av_Postingno like '%" & ppvensearch.text & "%' or av_name like '%" & ppvensearch.text & "%' or APFrom like '%" & ppvensearch.text & "%')"	
                end if
                ADVenuesPP.DataSource = dvProducts
                ADVenuesPP.DataBind()
					
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
            
            
        End Sub


			'Page Layout
        Public Sub pagelayout()
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


 			Sub ItemDataBoundEventHandlerKeys(ByVal sender As Object, ByVal e As DataGridItemEventArgs)

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
        			 Dim itemCellAVKey As TableCell = e.Item.Cells(2)
        			 Dim itemCellAVKeyTEXT As String = itemCellAVKey.Text
               
        			 
        			Dim BtnGrabKeyDG As System.Web.UI.HtmlControls.HtmlInputButton
               BtnGrabKeyDG = e.Item.Cells(0).FindControl("BtnGrabKey")
         		Dim newurl As String =  itemCellAVKeyTEXT 
               Dim ss As String = "copy('" & newurl & "');"
					BtnGrabKeyDG.Attributes("onclick") = ss
        
        		end if
        	end sub
        
        Sub ItemDataBoundEventHandler(ByVal sender As Object, ByVal e As DataGridItemEventArgs)

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                'Check who steps are for
                Dim itemCellwho0 As TableCell = e.Item.Cells(0)
                Dim itemCellwho As TableCell = e.Item.Cells(5)
                Dim itemcellwho2 As TableCell = e.Item.Cells(12)
                Dim itemcellwho14 As TableCell = e.Item.Cells(14)
                Dim itemcellwho22 As TableCell = e.Item.Cells(22)
                Dim itemcellwho23 As TableCell = e.Item.Cells(23)

                Dim itemcelladtext As TableCell = e.Item.Cells(4)
                Dim itemcellwho3 As TableCell = e.Item.Cells(2)
                Dim itemcellwho4 As TableCell = e.Item.Cells(4)
                Dim itemcellwho6 As TableCell = e.Item.Cells(6)
                Dim itemCellwhotext0 As String = itemCellwho0.Text
                Dim itemCellwhotext As String = itemCellwho.Text
                Dim itemCellwhotext2 As String = itemcellwho2.Text
                Dim itemCellwhotext3 As String = itemcellwho3.Text
                Dim itemCellwhotext4 As String = itemcellwho4.Text
                Dim itemcelladtexttext As String = itemcelladtext.Text
                Dim itemCellwhotext14 As String = itemcellwho14.Text
                Dim itemCellwhotext22 As String = itemcellwho22.Text
                Dim itemCellwhotext23 As String = itemcellwho23.Text

                Dim autopostbtn, removevenueA, markpubA, pubadA As Button

                pubadA = e.Item.Cells(0).FindControl("Publish")
                markpubA = e.Item.Cells(0).FindControl("markpub")
                autopostbtn = e.Item.Cells(0).FindControl("autopost")
                removevenueA = e.Item.Cells(0).FindControl("removevenue")

                Dim testbtn As System.Web.UI.HtmlControls.HtmlInputButton
                testbtn = e.Item.Cells(0).FindControl("test")

                Dim newurl As String = itemcelladtexttext '+ vbCrLf + "Please click here-> " + itemCellwhotext4
                Dim ss As String = "copy('" & newurl & "');"

                testbtn.Attributes("onclick") = ss
                If Request.Browser.Browser = "IE" Then
                    itemcellwho6.Visible = True
                Else
                    itemcellwho6.Visible = False

                End If
                If itemCellwhotext = "Published" Then
                    Dim btnmkpub As Button
                    btnmkpub = e.Item.Cells(0).FindControl("markpub")
                    btnmkpub.Enabled = False
                    Dim btnpub As Button
                    btnpub = e.Item.Cells(0).FindControl("Publish")
                    btnpub.Enabled = False
                    autopostbtn.Enabled = False

                Else
                    If itemCellwhotext = "Inprocess" Then
                        autopostbtn.Enabled = False
                        markpubA.Enabled = False
                        pubadA.Enabled = False
                        removevenueA.Enabled = False
                    Else
                        If itemCellwhotext14 = "No" Then
                            autopostbtn.Text = "AutoPost <Submit>"
                            markpubA.Enabled = True
                            pubadA.Enabled = True
                        Else
                            autopostbtn.Text = "AutoPost <Cancel>"
                            markpubA.Enabled = False
                            pubadA.Enabled = False
                            removevenueA.Enabled = False
                        End If
                        If checkcredits() <= 0 Then
                            autopostbtn.Enabled = False
                        Else
                            autopostbtn.Enabled = True
                        End If
                    End If
                End If
                If itemCellwhotext22 = "Slave" And itemCellwhotext = "Published" Then
                    autopostbtn.Text = "AutoPost <Cancel>"
                    markpubA.Enabled = False
                    pubadA.Enabled = False
                    removevenueA.Enabled = False
               
                End If
                'If itemCellwhotext23 = "No" Then
                'pubadA.Enabled = False

                'End If
            End If

        End Sub

        Sub setadtext(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(18).Text
            'Response.Write(content)
            tst.Text = item.Cells(18).Text

            Dim msg As String = ""
            msg = msg & "<Script Language='JavaScript'>"
            msg = msg & "copy2(document.getElementById('tst'));"
            msg = msg & "</Script>"
            Response.Write(msg)
            ' javascript:window.clipboardData.setData('Text', txtProblemDescription.innerText)")

        End Sub
        Sub updatevstat(ByVal venupk As String, pnum as string, fdate as datetime, tdate as datetime)
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand

            Dim strSql As String
            strSql = "update dbo.tbl_LeadADVenues set av_adplaced = 'Published', av_createdate=getdate(), av_Postingno='" & pnum & "' where tbl_leadadvenues='" & venupk & "'"


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
        
      Sub addnewvenue (Source As System.Object, e As System.EventArgs)
			pnladdvenue.visible=true
			venuname.text=""
			venuecode.text=""
			venueurl.text=""
			privateven.checked=false
		end sub 
		  Sub ltfilter(ByVal Source As System.Object, ByVal e As System.EventArgs)
      		If (ddlleadprogramFilter.SelectedItem.Text = "Select..") Then
      				ddlleadprogramFilter.BackColor = Red
      		else
      			ddlleadprogramFilter.BackColor = white
      		end if
      		
      	end sub
      	
      	 Sub doLtemplates(ByVal Source As System.Object, ByVal e As System.EventArgs)
      		if dd_Ltemplates.selecteditem.text="Template 1" then
      			adtext.content="<table cellspacing='0' cellpadding='0' style='border-left: #000000 1px solid; width: 100%; height=120; border-top: #000000 1px solid'> " 
					adtext.content=adtext.content &	"<tbody><tr><td style='border-bottom: #000000 1px solid; border-right: #000000 1px solid'><br/></td></tr></tbody></table> " 
					adtext.content=adtext.content &	"<table cellspacing='0' cellpadding='0' style='border-left: #000000 1px solid; width: 100%; height=120; border-top: #000000 1px solid'> " 
					adtext.content=adtext.content &	"<tbody><tr><td style='border-bottom: #000000 1px solid; border-right: #000000 1px solid'><br/></td>" 
					adtext.content=adtext.content &	"<td style='border-bottom: #000000 1px solid; border-right: #000000 1px solid'><br/></td></tr>" 
					adtext.content=adtext.content &	"<tr><td style='border-bottom: #000000 1px solid; border-right: #000000 1px solid'><br/></td>" 
					adtext.content=adtext.content &	"<td style='border-bottom: #000000 1px solid; border-right: #000000 1px solid'><br/></td></tr></tbody></table>"
      		else
      		
      		end if
      	end sub
      	Sub testme(ByVal Source As System.Object, ByVal e As System.EventArgs)
      		adtext.InsertHTML("OK")
      	end sub
      	
      	
      	
      
        Sub addnew(ByVal Source As System.Object, ByVal e As System.EventArgs)
            if ddintakeresponse.SelectedItem.Text = "Select.." then 
            	btn2prev.enabled=false
            else
	
	            if ddintakeresponse.SelectedItem.Text = "Add New" Then
	            	'If (ddlleadprogramFilter.SelectedItem.Text = "Select..") Then
	               ' 	ddlleadprogramFilter.BackColor = Red
	               ' 	ddintakeresponse.SelectedIndex = ddintakeresponse.Items.IndexOf(ddintakeresponse.Items.FindByText("Default"))
	            	'else 
		            	ddintakeresponse.BackColor = white
		            	ddlleadprogramFilter.BackColor = white
                        If Session("ADStage") <> "Finalized" Then
                            Session("ADStage") = "Draft"
                        End If
                        If Session("AdSaved") <> "True" Then
                            
                            insertad()
                            Session("AdSaved") = "True"
                            getadno()
                        Else
                            insertad()
                        End If
	                    Response.Redirect("brandingadd.aspx?action=new&adno=" & Session("adno") & "&type=complete&source=admanager")
	            	'End If
	           else
	            	
	           		
	           		dim normurl as string = "intakep.aspx?page=1&id=" & ddintakeresponse.selecteditem.value
	           		dim normurl2 as string = "intakep.aspx?page=2&id=" & ddintakeresponse.selecteditem.value
						bgpg1.Attributes("src") = normurl
	           		btn2prev.enabled=true
	           		bgpg2.Attributes("src") = normurl2	           	
	           end if
	         end if
        End Sub
        
        public sub editbranding(Source As System.Object, e As System.EventArgs)
        		if brandhasmultipleads() then
        			pnlbrandMain.visible=false
        			pnlbrandwarning.visible=true
        		else
        			pnlbrandMain.visible=true
        			pnlbrandwarning.visible=false
        		   Response.Redirect("brandingadd.aspx?action=edit&adno=" & Session("adno") & "&type=complete&id=" & ddintakeresponse.selecteditem.value & "&source=admanager")
	  			end if
        
       
        end sub  
        
         public sub EBYes(Source As System.Object, e As System.EventArgs)
      		pnlbrandMain.visible=true
        			pnlbrandwarning.visible=false
        		   Response.Redirect("brandingadd.aspx?action=edit&adno=" & Session("adno") & "&type=complete&id=" & ddintakeresponse.selecteditem.value & "&source=admanager")
	  	
      
      	end sub
      	
      	public sub EBNo(Source As System.Object, e As System.EventArgs)
      		pnlbrandMain.visible=true
        			pnlbrandwarning.visible=false
        	
      
      	end sub
        
        public function brandhasmultipleads() as boolean
         	Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand

            Dim strSql As String = "select count(tbl_leadad_pk) as 'Bads' from tbl_LeadADs where ad_intakeresponse='" & ddintakeresponse.selecteditem.value & "'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    if Sqldr("Bads") > 1 then
                    		return true
                    else
                    		return false
                    end if
                else   
                    return false
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
         end function
            
        
        
        
        
        
		Sub savenewvenue (Source As System.Object, e As System.EventArgs)
			if checkifcodeexists()=false then
				
				Dim strConnection As String
	            Dim sqlConn As SqlConnection
	            Dim sqlCmd As SqlCommand
	            
	            Dim strSql As String 
	            Dim hasaccts as string
	            if acctsetup.checked then 
	               hasaccts = "Yes"
	            else
	             	hasaccts = "No"
	            end if
	            
	            
	            if privateven.checked then 
                    strSql = "insert into dbo.tbl_xwalk  (x_type,x_descr,x_id,x_url,x_UID,x_online,x_hasaccounts,x_accounturl,x_loginissue) values('leadsource','" & venuname.Text & "','" & venuecode.Text & "','" & venueurl.Text & "','" & Session("userid") & "','" & ddvenonline.SelectedItem.Text & "','" & hasaccts & "','http://" & venueurl.Text & "','No')"
	         	else
                    strSql = "insert into dbo.tbl_xwalk  (x_type,x_descr,x_company,x_id,x_url,x_UID,x_online,x_hasaccounts,x_accounturl,x_loginissue) values('leadsource','" & venuname.Text & "','All','" & venuecode.Text & "','" & venueurl.Text & "','" & Session("userid") & "','" & ddvenonline.SelectedItem.Text & "','" & hasaccts & "','" & venueurl.Text & "','No')"
	           
	           	end if
	           
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
            	pnladdvenue.visible=false
            	FillADvenues()
            	FillVenues()
           else 
                venuname.Text = "EXISTS"
          end if
          
            
        End Sub
        Sub savenewvenueExit(ByVal Source As System.Object, ByVal e As System.EventArgs)
            pnladdvenue.Visible = False
        End Sub


        Public Function checkifcodeexists() As Boolean

            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select * from tbl_xwalk where x_descr='" & venuname.Text & "' and x_type='leadsource' and (x_company='All' or x_UID='" & Session("userid") & "')"

            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
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
        Public Function gadvpk() As String
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand

            Dim strSql As String = "select max(tbl_leadadvenues) as 'maxadvpk' from tbl_LeadADVenues"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    Return Sqldr("maxadvpk")
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
        End Function
        Sub updatemvdates(ByVal d1 As String, ByVal d2 As String, ByVal tot As Integer)
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            'Response.Write(d1)
            Dim strSql As String = "update tbl_LeadADVenues set av_APFrom=cast('" & d1 & "' as datetime),av_APTo=cast('" & d2 & "' as datetime),av_APUnitCount=" & tot & "  where tbl_leadadvenues='" & Session("cadvno") & "'"

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
        Sub cancelap(ByVal Source As System.Object, ByVal e As System.EventArgs)
            pnlapfreq.Visible = False
            pnlsavedv.Visible = True
            pnlldsource.Visible = True
            autoc.Text = "AutoPost Credits: " & checkcredits()
            'bindvenues()
        End Sub

        Sub saveapdates(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Dim icnt As Integer = 0
            If dd1.Text <> "" Then
                icnt = +1
                Dim dtStartDate As Date = dd1.Text
                Dim dtEndDate As Date = dd1a.Text
                Dim tsTimeSpan As TimeSpan
                Dim iNumberOfDays As Integer
                tsTimeSpan = dtEndDate.Subtract(dtStartDate)
                iNumberOfDays = tsTimeSpan.Days
                updatevenue("", "")
                updatemvdates(dd1.Text, dd1a.Text, iNumberOfDays)
                autopostsubmit(Session("cadvno"), "insert", dd1.Text)
                autopostsubmitHistory(getapid(), "insert")
                updatecredits("minus", iNumberOfDays)
            Else
            

            End If
            If dd2.Text <> "" Then
                icnt = +1
                Dim dtStartDate2 As Date = dd2.Text
                Dim dtEndDate2 As Date = dd2a.Text
                Dim tsTimeSpan2 As TimeSpan
                Dim iNumberOfDays2 As Integer
                tsTimeSpan2 = dtEndDate2.Subtract(dtStartDate2)
                iNumberOfDays2 = tsTimeSpan2.Days
                insertadV(session("adkeycode"), session("venueonline"), "Slave", dd2.Text, dd2a.Text, iNumberOfDays2,"","")
                autopostsubmit(gadvpk(), "insert", dd2.Text)
                autopostsubmitHistory(getapid(), "insert")
                updatecredits("minus", iNumberOfDays2)

            End If
            If dd3.Text <> "" Then
                icnt = +1
                Dim dtStartDate3 As Date = dd3.Text
                Dim dtEndDate3 As Date = dd3a.Text
                Dim tsTimeSpan3 As TimeSpan
                Dim iNumberOfDays3 As Integer
                tsTimeSpan3 = dtEndDate3.Subtract(dtStartDate3)
                iNumberOfDays3 = tsTimeSpan3.Days
                insertadV(session("adkeycode"), session("venueonline"), "Slave", dd3.Text, dd3a.Text, iNumberOfDays3,"","")
                autopostsubmit(gadvpk(), "insert", dd3.Text)
                autopostsubmitHistory(getapid(), "insert")
                updatecredits("minus", iNumberOfDays3)

            End If

            If dd4.Text <> "" Then
                icnt = +1
                Dim dtStartDate4 As Date = dd4.Text
                Dim dtEndDate4 As Date = dd4a.Text
                Dim tsTimeSpan4 As TimeSpan
                Dim iNumberOfDays4 As Integer
                tsTimeSpan4 = dtEndDate4.Subtract(dtStartDate4)
                iNumberOfDays4 = tsTimeSpan4.Days
                insertadV(session("adkeycode"), session("venueonline"), "Slave", dd4.Text, dd4a.Text, iNumberOfDays4,"","")
                autopostsubmit(gadvpk(), "insert", dd4.Text)
                autopostsubmitHistory(getapid(), "insert")
                updatecredits("minus", iNumberOfDays4)

            End If
            If (dd1.Text = "" Or dd1.Text.Length <= 0) And (dd2.Text = "" Or dd2.Text.Length <= 0) And _
            (dd3.Text = "" Or dd3.Text.Length <= 0) And (dd4.Text = "" Or dd4.Text.Length <= 0) Then
                lblNoAPDate.Visible = True
                lblNoAPDate.Text = "Please Enter a Schedule Date"

            Else
                lblNoAPDate.Visible = False
                'strMsgText = "The total number of days elapsed since " & dtStartDate.ToShortDateString() & " is: " & iNumberOfDays.ToString()
                'Response.Write(strMsgText)
                'Response.Write(icnt)
                pnlapfreq.Visible = False
                pnlsavedv.Visible = True
                pnlldsource.Visible = True
                autoc.Text = "AutoPost Credits: " & checkcredits()
                'bindvenues()
            End If
           
        End Sub

        Public Sub autopost_Click(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Dim x As Button = Source

            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(0).Text
            Dim content2 As String = item.Cells(14).Text
            Dim content21 As String = item.Cells(21).Text
            Dim content22 As String = item.Cells(22).Text
            'apbutton(0) = content
           ' apbutton(1) = content2

            If content2 = "No" Then
                'autopostsubmit(content, "insert")
                'autopostsubmitHistory(getapid(), "insert")
                'updatecredits("minus")
                ' bindvenues()
                dd1.Text = ""
                dd1a.Text = ""
                dd2.Text = ""
                dd2a.Text = ""
                dd3.Text = ""
                dd3a.Text = ""
                dd4.Text = ""
                dd4a.Text = ""
                Session("cadvno") = content
                pnlapfreq.Visible = True
                pnlsavedv.Visible = False
                pnlldsource.Visible = False
                lblNoAPDate.Visible = False
            Else

                '  autopostsubmitHistory(getapid(), "delete")
                ' autopostsubmit(content, "delete")
                'updatecredits("plus")
                ' 
                autopostsubmitHistory(getapid(), "delete")
                autopostsubmit(Session("cadvno"), "delete", "")
                If content22 = "Master" Then
                    updatevenue(content, "apdate")
                Else
                    updatevenue(content, "delete")
                End If
                updatecredits("plus", content21)
                pnlapfreq.Visible = False
                pnlsavedv.Visible = True
                pnlldsource.Visible = True
                'bindvenues()

            End If
            autoc.Text = "AutoPost Credits: " & checkcredits()

        End Sub
        
         Public Sub  editplan_Click(ByVal Source As System.Object, ByVal e As System.EventArgs)
            removefromlist("-1")
            Dim x As Button = Source
				
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(0).Text  
            Dim contentName As String = item.Cells(1).Text 
            Dim contentStat As String = item.Cells(2).Text
                  
            session("CPlanId")=content
            session("CPlanMode")="Edit"
            if contentStat="Inactive" then
        			btnAddV.enabled=false
        			ADPlanSched.enabled=false
        		
        		else
        			btnAddV.enabled=true
        			ADPlanSched.enabled=true
        		
        		end if
        		if contentName="System" then
        			dd_PPstat.enabled=false
        			btnAddV.enabled=false
        			ADPlanSched.enabled=false
        			PPName.enabled=false
        			PPSdate.enabled=false
        			PPEdate.enabled=false
        		end if
            editplan(content)
            dd_schstatfilter.SelectedIndex = dd_schstatfilter.Items.IndexOf(dd_schstatfilter.Items.FindByText("Active"))  
            bindPlansched(session("cplanid"))
            
         	         
         end sub
         
         Public Sub  getkeys_Click(ByVal Source As System.Object, ByVal e As System.EventArgs)
           
            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(0).Text           
            session("CPlanId")=content
        		pnlshowadkeys.visible=true
        		pnlpostings.visible=false
        		bindadkeys(session("CPlanId"))
         
         end sub
         
          Public Sub  showadplans(ByVal Source As System.Object, ByVal e As System.EventArgs)
           
           
        		pnlshowadkeys.visible=false
        		pnlpostings.visible=true
        		bindADPlans()
         
         end sub 
         
         
         
          Public Sub  qpost_Click(ByVal Source As System.Object, ByVal e As System.EventArgs)
            removefromlist("-1")
            Dim x As Button = Source

            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(0).Text           
            session("CPlanId")=content
            session("CPlanMode")="Edit"
           	dd_Pubfilter.SelectedIndex = dd_Pubfilter.Items.IndexOf(dd_Pubfilter.Items.FindByText("All"))
        		Session("fvenues")="All"
        		pnlPPdetailMain.visible=false
        		pnlpostings.visible=false
        		pnlPPdetail.visible=true
        		pnlentposts.visible=false
        		bindpplanfields(content)
        		bindADVenuesPP(content)
        		refreshxleadsNOBT()
        		ppgrid.attributes.add("style", "vertical-align top; height: 380px; overflow:auto;")
         	'session("adpoststyle")="noedit"
         end sub
         
         Public Sub  qpost_ClickAdd(ByVal Source As System.Object, ByVal e As System.EventArgs)
            removefromlist("-1")
            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(0).Text           
            session("CPlanId")=content
            session("CPlanMode")="Skip"
           dd_Pubfilter.SelectedIndex = dd_Pubfilter.Items.IndexOf(dd_Pubfilter.Items.FindByText("All"))
        		Session("fvenues")="All"
        		ppgrid.attributes.add("style", "vertical-align top; height: 380px; overflow:auto;")
         	'session("adpoststyle")="noedit"
         	bindpplanfields(content)
        		bindADVenuesPP(content)
        		pnlpostings.visible=false
         	NewPPlanPostNOBT()
         end sub
         						  										
         
         
        

        public sub editplan(id as string)
           	dd_Pubfilter.SelectedIndex = dd_Pubfilter.Items.IndexOf(dd_Pubfilter.Items.FindByText("All"))
           	Session("fvenues")="All"
           	pnlPPdetailMain.visible=true
        		pnlpostings.visible=false
        		pnlPPdetail.visible=true
        		pnlentposts.visible=false
        		pnlsverror.visible=false
        		bindpplanfields(id)
        		bindADVenuesPP(id)
        		refreshxleadsNOBT()
        		ppgrid.attributes.add("style", "vertical-align top; height: 300px; overflow:auto;")
        		'session("adpoststyle")="edit"
        		pnlplansched.visible=true
        		
        		bindPlansched(id)
        		if dd_PPstat.selecteditem.text="Inactive" then
        			'btnSavePPlan.enabled=false
        			btnAddV.enabled=false
        			ADPlanSched.enabled=false
        			ADPlanSched.enabled=false
        		else
        			btnSavePPlan.enabled=true
        		end if
        		
        		
        		
        end sub
        
          Sub bindpplanfields(id as string)

            Dim strUID As String = Session("userid")
            Dim strSql As String = "SELECT * from tbl_leadADPlans where LAP_tbl_pk ='" & id & "'"

            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then

        		   		If Sqldr("lap_name") IsNot DBNull.Value Then
                        PPName.Text = Sqldr("lap_name")
                    End If
                    If Sqldr("lap_expectPosts") IsNot DBNull.Value Then
                        PPnoposts.Text = Sqldr("lap_expectPosts")
                    End If
                    
                    If Sqldr("lap_freq") IsNot DBNull.Value Then
                        dd_PPfreq.SelectedIndex = dd_PPfreq.Items.IndexOf(dd_PPfreq.Items.FindByText(Sqldr("lap_freq")))
                    End If
                    If Sqldr("lap_durationno") IsNot DBNull.Value Then
                        PPDV.Text = Sqldr("lap_durationno")
                    End If
                    If Sqldr("lap_rop") IsNot DBNull.Value Then
                        PPROP.Text = Sqldr("lap_rop")
                    End If
                    If Sqldr("lap_duration") IsNot DBNull.Value Then
                        dd_PPDperiod.SelectedIndex = dd_PPDperiod.Items.IndexOf(dd_PPDperiod.Items.FindByText(Sqldr("lap_duration")))
                    End If
                    If Sqldr("lap_status") IsNot DBNull.Value Then
                        dd_PPstat.SelectedIndex = dd_PPstat.Items.IndexOf(dd_PPstat.Items.FindByText(Sqldr("lap_status")))
                    End If
                    If Sqldr("lap_totposts") IsNot DBNull.Value Then
                        TotPosts.Text = Sqldr("lap_totposts")
                    End If
                    If Sqldr("lap_expvenues") IsNot DBNull.Value Then
                        PPEXP.Text = Sqldr("lap_expvenues")
                    End If
                    If Sqldr("lap_startdate") IsNot DBNull.Value Then
                        PPSdate.Text = Sqldr("lap_startdate")
                    End If
                    If Sqldr("lap_endate") IsNot DBNull.Value Then
                        PPEdate.Text = Sqldr("lap_endate")
                    End If
                    
                   
                End If
	            Catch SQLexc As SqlException
	                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
	            Finally
	                myConnection.Close()
	            End Try
	        End Sub

        
        
        
       

        Sub showcalendar(ByVal sender As Object, ByVal e As EventArgs)
            Dim x As LinkButton = sender
            Session("apdate") = x.ID
            cdrCalendar.visible = True
        End Sub

        Public Sub Calendar1_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
            If Session("apdate") = "showcal1" Then
                dd1.Text = cdrCalendar.SelectedDate
            ElseIf Session("apdate") = "showcal1a" Then
                dd1a.Text = cdrCalendar.SelectedDate
            ElseIf Session("apdate") = "showcal2" Then
                dd2.Text = cdrCalendar.SelectedDate
            ElseIf Session("apdate") = "showcal2a" Then
                dd2a.Text = cdrCalendar.SelectedDate
            ElseIf Session("apdate") = "showcal3" Then
                dd3.Text = cdrCalendar.SelectedDate
            ElseIf Session("apdate") = "showcal3a" Then
                dd3a.Text = cdrCalendar.SelectedDate
            ElseIf Session("apdate") = "showcal4" Then
                dd4.Text = cdrCalendar.SelectedDate
            ElseIf Session("apdate") = "showcal4a" Then
                dd4a.Text = cdrCalendar.SelectedDate

            End If

            cdrCalendar.visible = False
        End Sub

        Public Function getapid() As String
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand

            Dim strSql As String = "select max (ap_tbl_pk) as 'maxid' from tbl_autopostqueue where ap_adno='" & Session("adno") & "'"

            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    Return Sqldr("maxid")
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
        End Function

        Public Sub updatecredits(ByVal action As String, ByVal cnt As Integer)
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand

            Dim strSql As String

            If action = "plus" Then
                strSql = "update tbl_users set autopostc = autopostc+" & cnt & " where uid='" & Session("userid") & "'"
            Else
                strSql = "update tbl_users set autopostc = autopostc-" & cnt & " where uid='" & Session("userid") & "'"

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

        Public Sub autopostsubmit(ByVal id As String, ByVal action As String, ByVal sdate As String)
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand

            Dim strSql As String

            If action = "insert" Then
                strSql = "insert into dbo.tbl_autopostqueue  (ap_adno,ap_advenno,ap_duedate,ap_status) values('" & Session("adno") & "','" & id & "', cast ('" & sdate & "' as datetime)+2 ,'Submitted')"
            Else
                strSql = "delete from dbo.tbl_autopostqueue  where ap_advenno='" & id & "'"

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
        Public Sub autopostsubmitHistory(ByVal id As String, ByVal action As String)
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand

            Dim strSql As String

            If action = "insert" Then
                strSql = "insert into dbo.tbl_autopostqueueHistory  (aphist_type,aphist_requestor,aphist_requestdate,aphist_notes,aphist_apfk) values('Autopost','" & Session("userid") & "', getdate() ,'Request Submitted','" & id & "')"
            Else
                strSql = "delete from dbo.tbl_autopostqueueHistory  where aphist_apfk='" & id & "'"

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
        Public Sub updatevenue(ByVal id As String, ByVal ap As String)
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand

            Dim strSql As String
            If ap = "Yes" Then
                strSql = "update tbl_LeadADVenues  set av_autopost='No', av_adplaced='Unpublished' from tbl_LeadADVenues where tbl_leadadvenues='" & id & "'"
            ElseIf ap = "apdate" Then
                strSql = "update tbl_LeadADVenues  set av_APFrom=null, av_APTo=null, av_APUnitCount=0 from tbl_LeadADVenues where tbl_leadadvenues='" & id & "'"
            ElseIf ap = "delete" Then
                strSql = "delete from tbl_LeadADVenues where tbl_leadadvenues='" & id & "'"
            Else
                strSql = "update tbl_LeadADVenues  set av_autopost='Yes',av_adplaced='Autoposting'  from tbl_LeadADVenues where tbl_leadadvenues='" & id & "'"

            End If

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
        Function ConfirmUserInput() As Boolean

            'adtext.Text.Replace(vbCrLf, "<br>")


            Dim msg As String
            Dim title As String
            Dim style As MsgBoxStyle
            Dim response As MsgBoxResult
            msg = "Do you want to continue?" ' Define message.
            style = MsgBoxStyle.DefaultButton2 Or _
            MsgBoxStyle.Critical Or MsgBoxStyle.YesNo
            title = "MsgBox Demonstration" ' Define title.
            ' Display message.
            response = MsgBox(msg, style, title)
            If response = MsgBoxResult.Yes Then ' User chose Yes.
                Return True
            Else
                Return False
            End If

        End Function
        
        Sub MarkPublished(ByVal Source As System.Object, ByVal e As System.EventArgs)
        	 
        	 if pstadfrom.text= "" then
        	 	pstadfrom.text="Required!"
        	 	pstadfrom.backcolor=red
        	 else
		        	        	 
		        	 insertadV(session("adkeycode"), session("venueonline"), "Master", DBNull.Value.ToString, DBNull.Value.ToString, DBNull.Value.ToString, "MP",session("CPlanId"))
		          pnlLSdetail.visible=false
		        	pnladdvenue.visible=false
		          
		          pnladdvenueN.visible=false
		          pnlpostings.visible=false
		          pnlPPdetail.visible=true
		          bindADVenuesPP(session("CPlanId"))
		           if Session("ADStage") <> "Finalized" then
		            	dofinalize()
			         end if
			         pstadfrom.backcolor=white
       		END IF
        end sub
        
        Sub PublishLAter(ByVal Source As System.Object, ByVal e As System.EventArgs)
         insertadV(session("adkeycode"), session("venueonline"), "Master", DBNull.Value.ToString, DBNull.Value.ToString, DBNull.Value.ToString, "UP",session("CPlanId"))
        	pnlLSdetail.visible=false
        	pnladdvenue.visible=false
          
          pnladdvenueN.visible=false
          pnlpostings.visible=false
          pnlPPdetail.visible=true
          bindADVenuesPP(session("CPlanId"))
        end sub
        
        Sub PublishNow(ByVal Source As System.Object, ByVal e As System.EventArgs)
        
        end sub
        
        Sub rerunpost(ByVal Source As System.Object, ByVal e As System.EventArgs)
        				
			        		'response.write(session("CPlanId"))
			        		Dim strUID As String = Session("userid")
			           	 Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
			            Dim mycommand As String
			            mycommand = "Select av_leadads_FK,av_name,av_key ,av_online,av_textAll,av_lapfk,tmpad_adno from tbl_tmpad " _
																& "join dbo.tbl_LeadADVenues on tbl_leadadvenues = tmpad_adno " _
			            	            				& "where tmpad_uid='" & Session("userid") & "' and av_lapfk='" & session("CPlanId") & "'"
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
			           
			            'response.write(ds.Tables(0).Rows.Count - 1)
			            Dim i As Integer
			            For i = 0 To ds.Tables(0).Rows.Count - 1
			                'Response.Write(ds.Tables(0).Rows(i)(1).ToString())
			                cloneadv(ds.Tables(0).Rows(i)(0).ToString(), ds.Tables(0).Rows(i)(1).ToString(),ds.Tables(0).Rows(i)(2).ToString(),ds.Tables(0).Rows(i)(3).ToString(),ds.Tables(0).Rows(i)(4).ToString(),ds.Tables(0).Rows(i)(5).ToString())
			            	 removefromlist(ds.Tables(0).Rows(i)(6).ToString())
			            Next
			            bindADVenuesPP(session("CPlanId"))
			      		
					
			            
        end sub
        
        Sub deleteposts(ByVal Source As System.Object, ByVal e As System.EventArgs)
     	
     					'response.write(session("CPlanId"))
			        		Dim strUID As String = Session("userid")
			           	 Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
			            Dim mycommand As String
			            mycommand = "Select tmpad_adno from tbl_tmpad " _
									  	& "where tmpad_uid='" & Session("userid") & "'"
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
			           
			            'response.write(ds.Tables(0).Rows.Count - 1)
			            Dim i As Integer
			            For i = 0 To ds.Tables(0).Rows.Count - 1
			                'Response.Write(ds.Tables(0).Rows(i)(1).ToString())
			                killtheposts(ds.Tables(0).Rows(i)(0).ToString())
			                removefromlist(ds.Tables(0).Rows(i)(0).ToString())
			            Next
			            bindADVenuesPP(session("CPlanId"))
     	
     	
     	
     		end sub
     		
     		sub killtheposts(id as string)		
	        	Dim strUID As String = Session("userid")
            Dim strSql As String
               strSql = "delete from dbo.tbl_LeadADVenues where tbl_leadadvenues='" & id & "' and av_adplaced='Unpublished'"
            

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
	         bindADVenuesPP(session("CPlanId"))
	      		
			
			            
        end sub
        Sub filterVenues(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Session("fvenues")=dd_Pubfilter.selecteditem.text
            ADVenuesPP.CurrentPageIndex = 0
            bindADVenuesPP(session("CPlanId"))

        End Sub
        
        Sub schfilter(ByVal Source As System.Object, ByVal e As System.EventArgs)
            bindPlansched(session("cplanid"))	

        End Sub
        
        
        sub cloneadv(adno as string, avname as string, adcodeS as string,vonlineS as string,adtextS as string,ppidS as string)
        		Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String
            sqlproc = "sp_AddADVenue"

            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

            Dim prmadno As New SqlParameter("@av_leadads_FK", SqlDbType.Int)
            prmadno.Value = adno
            myCommandADD.Parameters.Add(prmadno)

            Dim prmadvenue As New SqlParameter("@av_name", SqlDbType.VarChar, 50)
            prmadvenue.Value = avname
            myCommandADD.Parameters.Add(prmadvenue)

            Dim prmarepsond As New SqlParameter("@av_autorespond", SqlDbType.VarChar, 50)
            prmarepsond.Value = "No"
            myCommandADD.Parameters.Add(prmarepsond)

            Dim prmaphoto As New SqlParameter("@av_photo", SqlDbType.VarChar, 50)
            prmaphoto.Value = "No"
            myCommandADD.Parameters.Add(prmaphoto)

            Dim prmadcode As New SqlParameter("@av_key", SqlDbType.NVarChar, 255)
            prmadcode.Value = adcodeS
            myCommandADD.Parameters.Add(prmadcode)

            Dim prmadplaced As New SqlParameter("@av_adplaced", SqlDbType.VarChar, 50)
           	prmadplaced.Value = "Unpublished"
           	myCommandADD.Parameters.Add(prmadplaced)

            Dim prmonline As New SqlParameter("@av_online", SqlDbType.VarChar, 50)
            prmonline.Value = vonlineS
            myCommandADD.Parameters.Add(prmonline)

            Dim prmkeyurl As New SqlParameter("@av_keyurl", SqlDbType.NVarChar, 255)
            prmkeyurl.Value = System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "/intake.aspx?adcode=" & adcodeS
            myCommandADD.Parameters.Add(prmkeyurl)

            Dim prmAllText As New SqlParameter("@av_textAll", SqlDbType.Text)
            prmAllText.Value = adtextS
            'adtext.Text.Replace(vbCrLf, "<br>") & "<br><br>Please Click-> " & System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "/intake.aspx?adcode=" & adcode
            myCommandADD.Parameters.Add(prmAllText)

            Dim prmstat As New SqlParameter("@av_apstat", SqlDbType.VarChar, 50)
            prmstat.Value = "Master"
            myCommandADD.Parameters.Add(prmstat)

				Dim prmppid As New SqlParameter("@av_ppid", SqlDbType.int)
            prmppid.Value = ppidS
            myCommandADD.Parameters.Add(prmppid)

				Dim prmpostno As New SqlParameter("@av_postno", SqlDbType.VarChar, 50)
            prmpostno.Value = dbnull.value
            myCommandADD.Parameters.Add(prmpostno)

      		Dim prmapfrom As New SqlParameter("@av_APFrom", SqlDbType.DateTime)
            prmapfrom.Value = dbnull.value
            myCommandADD.Parameters.Add(prmapfrom)

            Dim prmapto As New SqlParameter("@av_APTo", SqlDbType.DateTime)
            prmapto.Value = dbnull.value
            myCommandADD.Parameters.Add(prmapto)

            Dim prmcnt As New SqlParameter("@av_APUnitCount", SqlDbType.Int)
            prmcnt.Value = dbnull.value
            myCommandADD.Parameters.Add(prmcnt)

            Dim prmautop As New SqlParameter("@av_autopost", SqlDbType.VarChar, 50)
            prmautop.Value = "No"
            myCommandADD.Parameters.Add(prmautop)          
           

            
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
        
        
        
        
        Sub ADVenuesPP_Edit(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
            ADVenuesPP.EditItemIndex = e.Item.ItemIndex
         	bindADVenuesPP(session("CPlanId"))
        End Sub
        
        Sub ADVenuesPP_Cancel(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
            ADVenuesPP.EditItemIndex = -1
           bindADVenuesPP(session("CPlanId"))
        End Sub
        
        Sub ADVenuesPP_Update(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
            'Read in the values of the updated row
            Dim dgID As String = e.Item.Cells(1).Text
            'CType(e.Item.Cells(0).Controls(0), TextBox).Text
            Dim dgname As String = CType(e.Item.Cells(8).Controls(0), TextBox).Text
            Dim dgdesc As String = CType(e.Item.Cells(9).Controls(0), TextBox).Text
				
				if dgname.length <= 0 then
					dgname =  DateTime.Now.ToShortDateString()
				end if
							
            Dim strSql As String 
            strSql = "update tbl_LeadADVenues set av_adplaced='Published',av_APFrom='" & dgname & "', av_Postingno='" & dgdesc & "' where tbl_leadadvenues='" & dgID & "'"
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
            ADVenuesPP.EditItemIndex = -1
            bindADVenuesPP(session("CPlanId"))
            if Session("ADStage") <> "Finalized" then
            	dofinalize()       
	           
	         end if
        End Sub
        
        public sub dofinalize()
         	Session("ADStage") = "Finalized"
	            insertad()
	            adstage.Text = "* Finalized *"
	             lbledittext.visible=true
                  adtext.show=false
                  B_removead.Visible = False
                  B_FinalV.Visible = False
                  adtitle.Enabled = False
                  adname.Enabled = False
                  'dd_status.Enabled = False
                  dd_mkprg.enabled=false
      				ddemailcor.enabled=false
                  ddlleadtypeFilter.Enabled = False
                  ddlleadprogramFilter.Enabled = False
                  ddintakeresponse.Enabled = False
                  'adtext.ReadOnly = True
                  adtxtreadonly.visible=true
                  adtxtreadonlyH.visible=true
                  adclone.Visible = True
                  adcloneP.Visible = True
                  b_savee.Visible = False
                  placead.Visible = True
                  'hrline.visible = false
                  B_Final.Visible = False
                  FillADvenues()
                  'bindvenues()
						adedit.visible=false
						 bindADVenuesPP(session("CPlanId"))
        end sub
        
         Sub ItemDataBoundEventHandlerPP(ByVal sender As Object, ByVal e As DataGridItemEventArgs)

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
   
   				'Dim myButton as Button = CType(e.Item.Cells(10).Controls(0), Button)
          		'myButton.CssClass = "frmbuttons"
          		
          		 'Check who steps are for
          		 Dim itemCellKey As TableCell = e.Item.Cells(1)
             	 Dim itemCellEDIT As TableCell = e.Item.Cells(10)                
					 Dim itemCellSelfPub As TableCell = e.Item.Cells(11) 
					 Dim itemCellTGUpdate As TableCell = e.Item.Cells(13)      
					 Dim itemCellPublished As TableCell = e.Item.Cells(5)
					 Dim itemCellPNO As TableCell = e.Item.Cells(16)
					 Dim itemCellPDate As TableCell = e.Item.Cells(17)
					 
					 Dim itemCellKeytext As String = itemCellKey.Text
                Dim itemCellEDITtext As String = itemCellEDIT.Text
                Dim itemCellPNOtext As String = itemCellPNO.Text
                Dim itemCellPDatetext As String = itemCellPDate.Text
                 
                Dim itemCellPublishedtext As String = itemCellPublished.Text
               
               Dim btnPublishPPDG As button
               btnPublishPPDG = e.Item.Cells(0).FindControl("PublishPP")	
               Dim btnUpdatePNDG As button
               btnUpdatePNDG = e.Item.Cells(0).FindControl("UpdatePN")
               
               Dim lblpnoDG As label
               lblpnoDG = e.Item.Cells(0).FindControl("lblPNO")
               lblpnoDG.text=itemCellPNO.text
               
               Dim lblPDDG As label
               lblPDDG = e.Item.Cells(0).FindControl("lblPdate")
               lblPDDG.text=itemCellPDatetext
               
                If itemCellPublishedtext = "Published" Then
                   itemCellEDIT.enabled=false
                   'itemCellSelfPub.enabled=false
                   btnPublishPPDG.visible=false
                   btnUpdatePNDG.visible=true
                   itemCellTGUpdate.enabled=false
                   
                Else
                  itemCellEDIT.enabled=true
                  'itemCellSelfPub.enabled=true
                   btnPublishPPDG.visible=true
                   btnUpdatePNDG.visible=false
                   itemCellTGUpdate.enabled=true
					 end if
					 
					 if session("UPTdate")= "true" then
					 
					  	if itemCellKeytext <> session("UPTdateValue") then
					  
					 		e.item.Visible = False
					 	end if
					 else
					 	e.item.Visible = true
					 end if
					 
					 
					 
					 
     			End If

        End Sub
        
        Sub showvendetails(ByVal Source As System.Object, ByVal e As System.EventArgs)
            If apsdetail.Text = "Show Details" Then
                apsdetail.Text = "Hide Details"
                ADVenues.Columns(11).Visible = False
                ADVenues.Columns(12).Visible = False
                ADVenues.Columns(13).Visible = False
                ADVenues.Columns(15).Visible = False
                ADVenues.Columns(6).Visible = False
                ADVenues.Columns(18).Visible = True
                ADVenues.Columns(19).Visible = True
                ADVenues.Columns(20).Visible = True
                ADVenues.Columns(24).Visible = True
            Else
                apsdetail.Text = "Show Details"
                ADVenues.Columns(6).Visible = True
                ADVenues.Columns(11).Visible = True
                ADVenues.Columns(12).Visible = True
                ADVenues.Columns(13).Visible = True
                ADVenues.Columns(15).Visible = True
                ADVenues.Columns(18).Visible = False
                ADVenues.Columns(19).Visible = False
                ADVenues.Columns(20).Visible = False
                ADVenues.Columns(24).Visible = False

            End If

        End Sub
        
         Sub showcalendar2(ByVal sender As Object, ByVal e As EventArgs)
            Dim x As LinkButton = sender
            Session("CalDchk") = x.ID
            if x.id ="lnkAPStart" or x.id ="lnkAPend"  then
            	cdrCalendar2z.visible = True
            	showcalcz.visible = True
            else
             	cdrCalendar2.visible = True
            	showcalc.visible = True
            end if
            
        End Sub
        
        Sub closecalendar2(ByVal sender As Object, ByVal e As EventArgs)
           	dim x as linkbutton = sender
           	if x.id="showcalcZ" then
           		showcalcz.visible = false
            	cdrCalendar2z.visible = false
           	elseif x.id="showcalc3" then
           		showcalc3.visible=false
           		cdrCalendar3.visible=false
           		lblcal3.visible=false
           		session("UPTdate")="false"
					bindPlansched(session("cplanid"))
           	else
           		showcalc.visible = false
            	cdrCalendar2.visible = false
          	
          	end if 
          	
        End Sub
        
         Sub refreshsched(ByVal sender As Object, ByVal e As EventArgs)     
              
              session("UPTdate")="false"
					bindPlansched(session("cplanid"))
        end sub
        
         Public Sub Calendar2_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
            If Session("CalDchk") = "lnkEPstDate" Then
                pstEPdate.Text = cdrCalendar2.SelectedDate
            ElseIf Session("CalDchk") = "lnkEPubDate" Then
                pstadfrom.Text = cdrCalendar2.SelectedDate
				ElseIf Session("CalDchk") = "lnkAPStart" Then
            	txtNPSdate.text = cdrCalendar2z.SelectedDate
            ElseIf Session("CalDchk") = "lnkAPend" Then
           		txtNPEdate.text = cdrCalendar2z.SelectedDate
            End If

        		showcalc.visible = false
            cdrCalendar2.visible = false
            showcalcz.visible = false
            cdrCalendar2z.visible = false
        End Sub
        
        Public Sub NPNextStep1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        		session("CPlanMode")="Add"
        		if txtNPPName.text.length > 0 then				
	        		if plannameok() then
	        			pnlNewADPlanSV.visible=true
	        			pnlNewADPlan.visible=false
	        			getADPDuration() 
	        			PPDV.text=session("DurDays")
	        			dd_PPDperiod.SelectedIndex = dd_PPDperiod.Items.IndexOf(dd_PPDperiod.Items.FindByText(session("DurFreq")))
        				PPnoposts.text = txtNPNoposs.text
        				dd_PPfreq.SelectedIndex = dd_PPfreq.Items.IndexOf(dd_PPfreq.Items.FindByText(dd_PPfreqNP.selecteditem.text)) 
	        			PPROP.text=   txtNPNoLeads .text   			
	        			refreshxleadsNOBT()       		
	        			SavePPlanNEW("Stage1")
	        			txtNPPName.BackColor = white
	        		else
	        			txtNPPName.text="Duplicate! Please change."
						txtNPPName.BackColor = Red					
	        		end if
	        	else
					txtNPPName.text="Required"
					txtNPPName.BackColor = Red
				end if
        end sub
        
        public sub getADPDuration()
        		Dim wY As Long = DateDiff(DateInterval.Month , cdate(txtNPSdate.text),cdate(txtNPEdate.text))
        		Dim wW As Long = DateDiff(DateInterval.Weekday  , cdate(txtNPSdate.text),cdate(txtNPEdate.text))
        		Dim wD As Long = DateDiff(DateInterval.day  , cdate(txtNPSdate.text),cdate(txtNPEdate.text))
        		if wy < 1 then
        			if ww = 4 then
        				session("DurDays")= 1
        				session("DurFreq")= "Months"
        			elseif ww=0 then
        				session("DurDays")= wd
        				session("DurFreq")= "Days"
        			else
        				session("DurDays")= ww
        				session("DurFreq")= "Weeks"
        			end if
        		else
        			session("DurDays")= wy
        			session("DurFreq")= "Months"
        		end if
        		
        		
        		
        end sub 
        
         Public Sub NPSaveStep1(ByVal sender As System.Object, ByVal e As System.EventArgs)
        		pnlNewADPlanSV.visible=false
        		pnlNewADPlan.visible=false
        		pnlpostings.visible=true
        		session("CPlanMode")="Add"
        		SavePPlanNEW("")
        end sub
        
        Public Sub NPCancel(ByVal sender As System.Object, ByVal e As System.EventArgs)
        		pnlNewADPlanSV.visible=false
        		pnlNewADPlan.visible=false
        		pnlpostings.visible=true
        end sub
        
          Public Sub SkipNextStep2(ByVal sender As System.Object, ByVal e As System.EventArgs)
        			
	        			pnlNewADPlanSVDW.visible=false
		        		pnlNewADPlanSV.visible=false
		        		pnlNewADPlan.visible=false
		        		pnlPPdetail.visible=true
		        		pnlPPdetailMain.visible=true
		        		pnlplansched.visible=true
		 
        	end sub
        
         Public Sub NPNextStep2(ByVal sender As System.Object, ByVal e As System.EventArgs)
        			dim status as string = "bad"
        			dim i as integer
        			dim x as integer				
					x=lb_selvenues.items.count-1				
        			for i=0 to x
						if (lb_selvenues.Items(i).Selected) then
							status="ok"
						end if
						
					next
        			if status="ok" then
				        		if session("CPlanMode")="Add" then 
					        		pnlNewADPlanSVDW.visible=true
					        		pnlNewADPlanSV.visible=false
					        		pnlNewADPlan.visible=false
					        		'response.write(VenueArray.Count)
					        		SaveVenuesNew()
					        		bindvenuesDOW()
					        		Dim VenueArraykeyL As ArrayList = CType(Session("VenueArraykey"), ArrayList)
									Dim VenueArrayL As ArrayList = CType(Session("VenueArray"), ArrayList)
									dim VenueArrayPKx as ArrayList = CType(Session("VenueArrayPK"), ArrayList)
					        		lblCvenue.text =VenueArrayL(session("VenueArrayCNT")) & " " & VenueArraykeyL(session("VenueArrayCNT"))
					        		Calendar1.visibledate = cdate(txtNPSdate.text)
					        		Button3.visible=false
				        		else
				        				Calendar1.SelectedDates.Clear
				        		
				        				Button3.visible=true
				        				SaveVenuesNew()
				        				if session("vaderror")="false" then
				        					Dim VenueArraykeyL As ArrayList = CType(Session("VenueArraykey"), ArrayList)
											Dim VenueArrayL As ArrayList = CType(Session("VenueArray"), ArrayList)
											dim VenueArrayPKx as ArrayList = CType(Session("VenueArrayPK"), ArrayList)
											lblCvenue.text =VenueArrayL(session("VenueArrayCNT")) & " " & VenueArraykeyL(session("VenueArrayCNT"))
					        				pnlNewADPlanSVDW.visible=true
					        				pnlNewADPlanSV.visible=false
					        				pnlNewADPlan.visible=false
					        			else
					        				pnlNewADPlanSVDW.visible=false
					        				pnlNewADPlanSV.visible=false
					        				pnlNewADPlan.visible=false
					        				pnlPPdetail.visible=true
					        			end if
					        		
				        		end if
				        		lblvenselect.text=""
				   else
				   	lblvenselect.text="Please select a venue"
				   
				   end if
        end sub
        
        public function checkvenexits(name as string) as boolean
         Dim strUID As String = Session("userid")
            Dim strSql As String = "select lv_name,* from tbl_LeadADPlanVenues where lv_name = '" &  name & "' and " _
            							& "lv_userid_fk='" & session("userid") & "' and lv_adplan_fk='" & session("cplanid") & "' and lv_ad_fk='" & session("adno") & "'"
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                	if sqldr("lv_name") isnot dbnull.value then 
                		return true
                	else
                		return false
                	end if
                else
                	return false
                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        
        end function
        
        
        
        
         Public Sub NPSaveStep2(ByVal sender As System.Object, ByVal e As System.EventArgs)
        		pnlNewADPlanSVDW.visible=false
        		pnlNewADPlanSV.visible=false
        		pnlNewADPlan.visible=false
        		pnlpostings.visible=true
        end sub
        
         Public Sub NPNextStep3(ByVal sender As System.Object, ByVal e As System.EventArgs)
        		pnlNewADActivateplan.visible=true
        		pnlNewADPlanSVDW.visible=false
        		pnlNewADPlanSV.visible=false
        		pnlNewADPlan.visible=false
        		'dosavepostdays()
        		
        end sub
        
         Public Sub NPSaveStep3(ByVal sender As System.Object, ByVal e As System.EventArgs)
        		pnlNewADActivateplan.visible=false
        		pnlNewADPlanSVDW.visible=false
        		pnlNewADPlanSV.visible=false
        		pnlNewADPlan.visible=false
        		pnlpostings.visible=true
        end sub
        
        Public Sub updateNPActivate(ByVal sender As System.Object, ByVal e As System.EventArgs)
        	if dd_ActivatePPNow.selecteditem.text="Yes" then
        		'response.write(session("CPlanId"))
        		updateADPlanStatus(session("CPlanId"),"Active","User")      		
        		updateADPlanVStatus(session("CPlanId"))
        		pnlNewADPostPlan.visible=true
        	else
        	
        		updateADPlanStatus(session("CPlanId"),"Inactive", "User") 
        		'session("keepadmfiltersA")="false"
        		'response.redirect("adpostings.aspx?source=newpostpub")
				response.redirect("createad.aspx?action=edit&source=wrkpsts&adno=" & request.querystring("adno"))        		
        		pnlNewADPostPlan.visible=false
        		pnlNewADActivateplan.visible=false
        		pnlNewADPlanSVDW.visible=false
        		pnlNewADPlanSV.visible=false
        		pnlNewADPlan.visible=false
        		pnlpostings.visible=true
        	end if
        end sub
        
         sub updateADPlanStatus(id as string, stat as string, what as string)
         	Dim strUID As String = Session("userid")
            Dim strSql As String = "update tbl_leadADPlans set lap_status='" & stat & "', lap_WhoTouched='User' where LAP_tbl_pk='" & id & "'"
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
        
        sub updateADPlanVStatus(id as string)
         	Dim strUID As String = Session("userid")
            Dim strSql As String = "update tbl_LeadADPlanVenues set lv_status='Active', lv_whotouched='User' where lv_adplan_fk='" & id & "'"
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
        
        Public Sub updateNPPostNow(ByVal sender As System.Object, ByVal e As System.EventArgs)
        	if dd_PostPPNow.selecteditem.text="Yes" then
        		session("adno")=request.querystring("adno")
        		session("adplanno")= session("CPlanId")
        		session("keepadmfiltersA")="false"
        		session.remove("PubSearchFV")
            	session.remove("PubStatFV")
            	session.remove("PubADSFV")
            	session.remove("PubADPlanFV")
            	session.remove("PubTargetDateFV")
            	session.remove("PubADVenueFV")
        		response.redirect("adpostings.aspx?source=newpostpubP")
        	else
        		response.redirect("createad.aspx?action=edit&source=wrkpsts&adno=" & request.querystring("adno"))
        		
        		pnlNewADPostPlan.visible=false
        		pnlNewADActivateplan.visible=false
        		pnlNewADPlanSVDW.visible=false
        		pnlNewADPlanSV.visible=false
        		pnlNewADPlan.visible=false
        		pnlpostings.visible=false
		      pnlPPdetail.visible=false
		      pnlPPdetailMain.visible=false
      
        	end if
        end sub
        
        
         Sub SavePPlanNEW(stage as string)
					
						if session("CPlanMode")="Add" then 
							insertpplanNEW(stage)
							session("CPlanId")=getnewplanid()
							'session("CPlanMode")="Edit"
						
						end if
						
				
        end sub
        
        public function plannameok() as boolean 
        
        	Dim strUID As String = Session("userid")
            Dim strSql As String = "select lap_name from  tbl_leadADPlans where lap_useridfk='" & session("userid") & "' and lower(lap_name)= lower('" & txtNPPName.text & "')"
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
               
                		return false
                
                else 
                	return true
                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        
        
        end function
        
        
        
			Sub insertpplanNEW(stage as string)
            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String

		     	If session("CPlanMode")="Edit" Then
             	sqlproc = "sp_updateADPlan"
          	Else
              	sqlproc = "sp_AddADPlan"
          	End If
            

            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure
		
				if session("CPlanMode")="Edit" Then
					Dim prmPPpk As New SqlParameter("@PPpk", SqlDbType.Int)
	            prmPPpk.Value = session("CPlanId")
	            myCommandADD.Parameters.Add(prmPPpk)
				end if
				
		      Dim prmadpk As New SqlParameter("@adno", SqlDbType.Int)
            prmadpk.Value = Request.QueryString("adno")
            myCommandADD.Parameters.Add(prmadpk)
            
            Dim prmuid As New SqlParameter("@aduid", SqlDbType.VarChar, 50)
            prmuid.Value = Session("userid")
            myCommandADD.Parameters.Add(prmuid)

            Dim prmPPname As New SqlParameter("@PPname", SqlDbType.VarChar, 50)
            prmPPname.Value = txtNPPName.text
            myCommandADD.Parameters.Add(prmPPname)
				
				Dim prmPPexpposts As New SqlParameter("@PPexpposts", SqlDbType.VarChar, 50)
            prmPPexpposts.Value = txtNPNoposs.text
            myCommandADD.Parameters.Add(prmPPexpposts)
          
            Dim prmppfreq As New SqlParameter("@ppfreq", SqlDbType.VarChar, 50)
            prmppfreq.Value = dd_PPfreqNP.SelectedItem.Text
            myCommandADD.Parameters.Add(prmppfreq)
            
            Dim prmPPDno As New SqlParameter("@PPDno", SqlDbType.VarChar, 50)
            prmPPDno.Value = session("DurDays")
            myCommandADD.Parameters.Add(prmPPDno)
            
            Dim prmppduration As New SqlParameter("@ppduration", SqlDbType.VarChar, 50)
            prmppduration.Value = session("DurFreq")
            myCommandADD.Parameters.Add(prmppduration)
            
            Dim prmPPROP As New SqlParameter("@PPROP", SqlDbType.VarChar, 50)
            prmPPROP.Value = txtNPNoLeads.text
            myCommandADD.Parameters.Add(prmPPROP)
            
            Dim prmppstat As New SqlParameter("@ppstat", SqlDbType.VarChar, 50)
            prmppstat.Value = "Incomplete"
            myCommandADD.Parameters.Add(prmppstat)
            
            Dim prmtotposts As New SqlParameter("@totposts", SqlDbType.nVarChar, 50)
            prmtotposts.Value = TotPosts.Text
            myCommandADD.Parameters.Add(prmtotposts)

 				Dim prmSdate As New SqlParameter("@Sdate", SqlDbType.datetime)
            prmSdate.Value = txtNPSdate.text
            myCommandADD.Parameters.Add(prmSdate)
            
            Dim prmEdate As New SqlParameter("@Edate", SqlDbType.datetime)
            prmEdate.Value = txtNPEdate.text
            myCommandADD.Parameters.Add(prmEdate)
            
            Dim prmEvenues As New SqlParameter("@Evenues", SqlDbType.nVarChar, 50)
            prmEvenues.Value = txtNPNoVens.text
            myCommandADD.Parameters.Add(prmEvenues)
            
            Dim prmAvenues As New SqlParameter("@Avenues", SqlDbType.nVarChar, 50)
            prmAvenues.Value = "0"
            myCommandADD.Parameters.Add(prmAvenues)
            
            Dim prmadstage As New SqlParameter("@Adstage", SqlDbType.nVarChar, 50)
            prmadstage.Value = stage
            myCommandADD.Parameters.Add(prmadstage)
            
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
        
        
        Public Function getNewVenPK(id as string) As String
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand

            Dim strSql As String = "select max(LV_tbl_pk) as 'maxadvpk' from tbl_LeadADPlanVenues where lv_name='" & id & "' " _
                                  & "and lv_ad_fk='" & session("adno") & "' and lv_userid_fk='" & session("userid") & "' and lv_adplan_fk='" & session("cplanid") & "'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    Return Sqldr("maxadvpk")
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
        End Function
        
        

		sub SaveVenuesNew()
			
			
			Dim i As Integer   
			dim x as integer
			dim vcounter as integer =0
			session("VenueArrayCNT")=0
			x=lb_selvenues.items.count-1
			'response.write(VenueArray.Count)
			for i=0 to x
				if (lb_selvenues.Items(i).Selected) then
					if not checkvenexits(Convert.ToString(lb_selvenues.items(i))) then				
						
						getvenueinfo(Convert.ToString(lb_selvenues.items(i).value))
                	session("adkeycode") = session("adkeycode") + getadkey()
						VenueArray.Insert(vcounter, Convert.ToString(lb_selvenues.items(i))) 
						VenueArrayKey.Insert(vcounter,session("adkeycode").tostring)
						DoSaveVenuesNew(session("adkeycode"),Convert.ToString(lb_selvenues.items(i)),session("venueonline"))
						
						VenueArrayPK.Insert(vcounter,getNewVenPK(Convert.ToString(lb_selvenues.items(i))))
						vcounter = vcounter + 1
					else
						lblerror.text = lblerror.text & Convert.ToString(lb_selvenues.items(i)) & "<br>"
					
					end if
				end if
				
			next
			incADKEY()
			if VenueArray.count>0 then
				Session("VenueArray") = VenueArray
		 		Session("VenueArrayKey") = VenueArrayKey
		 		session("VenueArrayPK") = VenueArrayPK
		 		session("vaderror")="false"
		 	else 
		 		session("vaderror")="true"
		 	end if
			if lblerror.text = "" then
				pnlsverror.visible=false
			else
				pnlsverror.visible=true
			end if
			
		end sub
		
		sub DoSaveVenuesNew(key as string, svalue as string,svenonline as string)
			
				Dim strUID As String = Session("userid")
            Dim strSql As String = "insert into tbl_LeadADPlanVenues (lv_ad_fk,lv_adplan_fk,lv_userid_fk,lv_name,lv_status,lv_key, " _
            								& "lv_sunday,lv_monday,lv_tuesday,lv_wednesday,lv_thursday,lv_firday,lv_saturday, lv_count, lv_online, lk_keyurl) " _
            								& " values ('" & session("adno") & "','" & session("CPlanId") & "','" & Session("userid") & "','" & svalue & "', " _
            								& "'Active','" & key & "', 'false','false','false','false','false','false','false',0,'" & svenonline & "', " _
            								& "'" &  System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "/intake.aspx?adcode=" & key & "')"
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

         Public Sub bindvenuesDOW()
            Dim strUID As String = Session("userid")
            If Session("adno") = "" Then
                Session("adno") = Request.QueryString("adno")
            End If
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String = "Select * from tbl_LeadADPlanVenues " _
                							& "where lv_ad_fk ='" & Session("adno") & "' " _
                							& "and lv_adplan_fk ='" & session("CPlanId") & "' " _

            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_LeadADVenues")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_LeadADVenues"))
						
                dgVenueDOW.DataSource = dvProducts
                dgVenueDOW.DataBind()
					
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
          
        End Sub

		  sub dosavepostdays(ByVal Source As System.Object, ByVal e As System.EventArgs)
		  		Dim x As checkbox = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim advenuePK As String = item.Cells(0).Text
            if x.checked then
            	if x.id="chksunday" then
            		dim Tdow as string = "Sunday"
            		dim TdowV as integer = 0
            		updatevendates(tdow,advenuePK,"true")
            	elseif x.id="chkMonday" then
            		'response.write("Mon")
            		dim Tdow as string = "Monday"
            		dim TdowV as integer = 1
            		updatevendates(tdow,advenuePK,"true")
            	elseif x.id="chkTuesday" then
            		dim Tdow as string = "Tuesday"
            		dim TdowV as integer = 2
            		updatevendates(tdow,advenuePK,"true")
            	elseif x.id="chkWednesday" then
            		dim Tdow as string = "Wednesday"
            		dim TdowV as integer = 3
            		updatevendates(tdow,advenuePK,"true")
            	elseif x.id="chkThursday" then
            		dim Tdow as string = "Thursday"
            		dim TdowV as integer = 4
            		updatevendates(tdow,advenuePK,"true")
            	elseif x.id="chkFriday" then
            		dim Tdow as string = "Friday"
            		dim TdowV as integer = 5
            		updatevendates(tdow,advenuePK,"true")
            	elseif x.id="chkSaturday" then
            		dim Tdow as string = "Saturday"
            		dim TdowV as integer = 6
            		updatevendates(tdow,advenuePK,"true")
            	end if
            
            else
            	if x.id="chksunday" then
            		dim Tdow as string = "Sunday"
            		dim TdowV as integer = 0
            		updatevendates(tdow,advenuePK,"false")
            	elseif x.id="chkMonday" then
            	
            		dim Tdow as string = "Monday"
            		dim TdowV as integer = 1
            		updatevendates(tdow,advenuePK,"false")
            	elseif x.id="chkTuesday" then
            		dim Tdow as string = "Tuesday"
            		dim TdowV as integer = 2
            		updatevendates(tdow,advenuePK,"false")
            	elseif x.id="chkWednesday" then
            		dim Tdow as string = "Wednesday"
            		dim TdowV as integer = 3
            		updatevendates(tdow,advenuePK,"false")
            	elseif x.id="chkThursday" then
            		dim Tdow as string = "Thursday"
            		dim TdowV as integer = 4
            		updatevendates(tdow,advenuePK,"false")
            	elseif x.id="chkFriday" then
            		dim Tdow as string = "Friday"
            		dim TdowV as integer = 5
            		updatevendates(tdow,advenuePK,"false")
            	elseif x.id="chkSaturday" then
            		dim Tdow as string = "Saturday"
            		dim TdowV as integer = 6
            		updatevendates(tdow,advenuePK,"false")
            	end if
            end if
		  		session("savePDOW")="true"
		  		'bindvenuesDOW()
		  
		  end sub
		  
		  
		  
          Sub ItemDataBoundEventHandlerDOW(ByVal sender As Object, ByVal e As DataGridItemEventArgs)

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
   
   				Dim itemCellKey As TableCell = e.Item.Cells(0)
          		Dim itemCellKeytext As String = itemCellKey.Text
               
               Dim DGchksunday As checkbox
             	DGchksunday = e.Item.Cells(0).FindControl("chksunday")	
					
					 
     			End If

        End Sub
        
        Sub ItemDataBoundEventHandlerAPsched(ByVal sender As Object, ByVal e As DataGridItemEventArgs)

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
   
   				Dim itemCellKey As TableCell = e.Item.Cells(0)
   				Dim itemCellSun As TableCell = e.Item.Cells(2)
   				Dim itemCellMon As TableCell = e.Item.Cells(3)
   				Dim itemCellTue As TableCell = e.Item.Cells(4)
   				Dim itemCellWed As TableCell = e.Item.Cells(5)
   				Dim itemCellThu As TableCell = e.Item.Cells(6)
   				Dim itemCellFri As TableCell = e.Item.Cells(7)
   				Dim itemCellSat As TableCell = e.Item.Cells(8)
          		Dim itemCellKeytext As String = itemCellKey.Text
          		Dim itemCellSuntext As String = itemCellSun.Text
          		Dim itemCellMontext As String = itemCellMon.Text
          		Dim itemCellTuetext As String = itemCellTue.Text
          		Dim itemCellWedtext As String = itemCellWed.Text
          		Dim itemCellThutext As String = itemCellThu.Text
          		Dim itemCellFritext As String = itemCellFri.Text
          		Dim itemCellSattext As String = itemCellSat.Text
               
               Dim DGchksunday,DGchkmonday,DGchktuesday,DGchkwednesday,DGchkthursday,DGchkfriday,DGchksaturday As checkbox
             	DGchksunday = e.Item.Cells(0).FindControl("chksundayV")
             	DGchkmonday = e.Item.Cells(0).FindControl("chkMondayV")
             	DGchktuesday = e.Item.Cells(0).FindControl("chkTuesdayV")
             	DGchkwednesday = e.Item.Cells(0).FindControl("chkWednesdayV")
             	DGchkthursday = e.Item.Cells(0).FindControl("chkThursdayV")
             	DGchkfriday = e.Item.Cells(0).FindControl("chkFridayV")
             	DGchksaturday = e.Item.Cells(0).FindControl("chkSaturday")	
             	
             	if itemCellSuntext ="true" then
             		DGchksunday.checked = true
             	else
             		DGchksunday.checked=false
             	end if
             	if itemCellMontext ="true" then
             		DGchkmonday.checked = true
             	else
             		DGchkmonday.checked=false
             	end if
             	if itemCellTuetext ="true" then
             		DGchktuesday.checked = true
             	else
             		DGchktuesday.checked=false
             	end if
             	if itemCellWedtext ="true" then
             		DGchkwednesday.checked = true
             	else
             		DGchkwednesday.checked=false
             	end if
             	if itemCellThutext ="true" then
             		DGchkthursday.checked = true
             	else
             		DGchkthursday.checked=false
             	end if
             	if itemCellFritext ="true" then
             		DGchkfriday.checked = true
             	else
             		DGchkfriday.checked=false
             	end if
             	if itemCellSattext ="true" then
             		DGchksaturday.checked = true
             	else
             		DGchksaturday.checked=false
             	end if
             		
					
					 
     			End If

        End Sub
        
        
        
        sub updatevendates(tdow as string, id as string, action as string)
        		Dim strUID As String = Session("userid")
            Dim strSql As String 
            if tdow = "Sunday" then
            	strSql = "update tbl_LeadADPlanVenues set lv_sunday='" & action & "' where LV_tbl_pk = '" & id & "'"
            elseif tdow = "Monday" then
              	strSql = "update tbl_LeadADPlanVenues set lv_monday='" & action & "' where LV_tbl_pk = '" & id & "'"
          
            elseif tdow = "Tuesday" then
             	strSql = "update tbl_LeadADPlanVenues set lv_tuesday='" & action & "' where LV_tbl_pk = '" & id & "'"
           
            elseif tdow = "Wednesday" then
              	strSql = "update tbl_LeadADPlanVenues set lv_wednesday='" & action & "' where LV_tbl_pk = '" & id & "'"
          
            elseif tdow = "Thursday" then
              	strSql = "update tbl_LeadADPlanVenues set lv_thursday='" & action & "' where LV_tbl_pk = '" & id & "'"
          
            elseif tdow = "Friday" then
              	strSql = "update tbl_LeadADPlanVenues set lv_firday='" & action & "' where LV_tbl_pk = '" & id & "'"
          
            elseif tdow = "Saturday" then
            	strSql = "update tbl_LeadADPlanVenues set lv_saturday='" & action & "' where LV_tbl_pk = '" & id & "'"
          
            end if
            
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
        
        
		  
		  public sub Dosavevrec(tdow as string,tdowv as integer)
		  		'get 1st run date
		  		Dim SDayOfWeek As Integer
        		SDayOfWeek = cdate(txtNPSdate.text).DayOfWeek
				dim Ddiff as integer = SDayOfWeek - tdowv
				dim Dadd as integer
				dim initialrundate as datetime
				if SDayOfWeek = tdowv then
					dadd =0
					initialrundate= cdate(txtNPSdate.text).AddDays(dadd)
				elseif SDayOfWeek < tdowv
					dadd = tdowv - SDayOfWeek
					initialrundate= cdate(txtNPSdate.text).AddDays(dadd)

				else
					dadd =  (6-SDayOfWeek) + tdowv + 1
					initialrundate= cdate(txtNPSdate.text).AddDays(dadd)
				
				end if
				'response.write(initialrundate)

		  end sub
		  
		  sub Buildvenposts()
		  		
		  		Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            mycommand = "Select lv_name,lv_sunday,lv_monday,lv_tuesday,lv_wednesday,lv_thursday,lv_firday,lv_saturday,lv_key from  tbl_LeadADPlanVenues where lv_adplan_fk='" & session("CPlanId") & "'"
                       
            Dim ad As New SqlDataAdapter(mycommand, myConnection)
            Dim ds As New DataSet()
            Dim str As New StringBuilder()

            Dim sVenue As Integer

            Try
                ad.Fill(ds)
              
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

           	dim TOTpostDays As Long = DateDiff(DateInterval.day  , cdate(txtNPSdate.text),cdate(txtNPEdate.text))
            For sVenue = 0 To ds.Tables(0).Rows.Count - 1
            	if ds.Tables(0).Rows(sVenue)(1).ToString()="true" then
            		'Sunday
            		Dim Ccnt as long = TOTpostDays
            		Dim SDayOfWeek As Integer = cdate(txtNPSdate.text).DayOfWeek
            		dim initialrundate, prevrundate, nextrundate as datetime
            		dim DaysToAdd as integer
							if SDayOfWeek = 0 then
								DaysToAdd =0
								initialrundate= cdate(txtNPSdate.text).AddDays(DaysToAdd)
							elseif SDayOfWeek < 0
								DaysToAdd = 0 - SDayOfWeek
								initialrundate= cdate(txtNPSdate.text).AddDays(DaysToAdd)
							else
								DaysToAdd =  (6-SDayOfWeek) + 0 + 1
								initialrundate= cdate(txtNPSdate.text).AddDays(DaysToAdd)
							end if
							nextrundate = initialrundate
							
							
							insertadVNEW(ds.Tables(0).Rows(sVenue)(0), ds.Tables(0).Rows(sVenue)(8), initialrundate, session("vaddtevPK")       )
							
							dim DaystoRemove As Long = DateDiff(DateInterval.day  , cdate(txtNPSdate.text),initialrundate,session("vaddtevPK"))
							
							Ccnt = Ccnt - DaystoRemove
							Ccnt = Ccnt -7
							'response.write("D"& DaystoRemove & "C" & Ccnt & "T" & TOTpostDays)
							Do Until Ccnt < 0
								nextrundate =	nextrundate.AddDays(7)
								insertadVNEW(ds.Tables(0).Rows(sVenue)(0), ds.Tables(0).Rows(sVenue)(8),  nextrundate,session("vaddtevPK"))
								Ccnt = Ccnt -7
								
							Loop

							
            	elseif ds.Tables(0).Rows(sVenue)(2).ToString()="true" then
            	
            	end if
            
            Next

		  
		  end sub
		  
		  
		  Sub insertadVNEW(Sadname as string, Sadcode as string, STdate as datetime, SvenuePK as string)
            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String
            sqlproc = "sp_AddADVenue"

            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

            Dim prmadno As New SqlParameter("@av_leadads_FK", SqlDbType.Int)
            prmadno.Value = session("adno")
            myCommandADD.Parameters.Add(prmadno)

            Dim prmadvenue As New SqlParameter("@av_name", SqlDbType.VarChar, 50)
            prmadvenue.Value = Sadname
            myCommandADD.Parameters.Add(prmadvenue)

            Dim prmarepsond As New SqlParameter("@av_autorespond", SqlDbType.VarChar, 50)
            prmarepsond.Value = "N"
            myCommandADD.Parameters.Add(prmarepsond)

            Dim prmaphoto As New SqlParameter("@av_photo", SqlDbType.VarChar, 50)
            prmaphoto.Value = "N"
            myCommandADD.Parameters.Add(prmaphoto)

            Dim prmadcode As New SqlParameter("@av_key", SqlDbType.NVarChar, 255)
            prmadcode.Value = Sadcode
            myCommandADD.Parameters.Add(prmadcode)

            Dim prmadplaced As New SqlParameter("@av_adplaced", SqlDbType.VarChar, 50)
            prmadplaced.Value = "Unpublished"
           	myCommandADD.Parameters.Add(prmadplaced)

            Dim prmonline As New SqlParameter("@av_online", SqlDbType.VarChar, 50)
            prmonline.Value = dbnull.value
            myCommandADD.Parameters.Add(prmonline)

            Dim prmkeyurl As New SqlParameter("@av_keyurl", SqlDbType.NVarChar, 255)
            prmkeyurl.Value = System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "/intake.aspx?adcode=" & Sadcode
            myCommandADD.Parameters.Add(prmkeyurl)

            Dim prmAllText As New SqlParameter("@av_textAll", SqlDbType.Text)
            prmAllText.Value = dbnull.value
            myCommandADD.Parameters.Add(prmAllText)

            Dim prmstat As New SqlParameter("@av_apstat", SqlDbType.VarChar, 50)
            prmstat.Value = "Master"
            myCommandADD.Parameters.Add(prmstat)

				Dim prmppid As New SqlParameter("@av_ppid", SqlDbType.int)
            prmppid.Value = session("CPlanId")
            myCommandADD.Parameters.Add(prmppid)

				Dim prmpostno As New SqlParameter("@av_postno", SqlDbType.VarChar, 50)
            prmpostno.Value = dbnull.value
            myCommandADD.Parameters.Add(prmpostno)

	         Dim prmapfrom As New SqlParameter("@av_APFrom", SqlDbType.datetime)
	        	prmapfrom.Value = dbnull.value
	         myCommandADD.Parameters.Add(prmapfrom)

            Dim prmapto As New SqlParameter("@av_APTo", SqlDbType.datetime)
            prmapto.Value = STdate
 		      myCommandADD.Parameters.Add(prmapto)

           	Dim prmcnt As New SqlParameter("@av_APUnitCount", SqlDbType.Int)            
            prmcnt.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmcnt)

            Dim prmautop As New SqlParameter("@av_autopost", SqlDbType.VarChar, 50)
            prmautop.Value = "No"
            myCommandADD.Parameters.Add(prmautop)
            
            Dim prmVenuePK As New SqlParameter("@av_VenuPK", SqlDbType.Int)            
            prmVenuePK.Value = SvenuePK
            myCommandADD.Parameters.Add(prmVenuePK)
            
            
            
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
		  
		  
		  public sub Dodelvrec()
		  		'response.write("Del")
		  end sub
		  
		  
		  Sub Button2_Click(sender As Object, e As EventArgs)
		 	 	Dim dt As DateTime
       		dim i as integer
       		For each dt in Calendar1.SelectedDates
       		
      			insertadVNEW(session("vaddtevname"), session("vaddtevkey"),  dt, session("vaddtevPK"))
 						
					Next
					pnlNewADPlanSVDW.visible=false
					pnlPPdetailMain.visible=true
					pnlPPdetail.visible=true
					bindPlansched(session("cplanid"))				
			end sub
       	
       	
		Sub Button1_Click(sender As Object, e As EventArgs)
			dim x as button = sender
       	Dim dt As DateTime
       	dim i as integer
       	Dim VenueArraykeyL As ArrayList = CType(Session("VenueArraykey"), ArrayList)
			Dim VenueArrayL As ArrayList = CType(Session("VenueArray"), ArrayList)
			dim VenueArrayPKx as ArrayList = CType(Session("VenueArrayPK"), ArrayList)       	
			
			if x.id= "Button2" then
      			For each dt in Calendar1.SelectedDates
      				session("VenueArrayCNT")=0
						while session("VenueArrayCNT") <= VenueArrayL.Count -1
 							insertadVNEW(VenueArrayL(session("VenueArrayCNT")), VenueArraykeyL(session("VenueArrayCNT")),  dt,VenueArrayPKx(session("VenueArrayCNT")))
 							session("VenueArrayCNT")=session("VenueArrayCNT")+1
						end while
					Next
					if session("CPlanMode")="Add" then
						pnlNewADPlanSVDW.visible=false
						pnlNewADActivateplan.visible=true
					else
						pnlNewADPlanSVDW.visible=false
						pnlpostings.visible=true						
            		editplan(session("cplanid"))						
					end if
			elseif x.id= "Button1" then
					Button2.visible=false
					For each dt in Calendar1.SelectedDates
      				insertadVNEW(VenueArrayL(session("VenueArrayCNT")), VenueArraykeyL(session("VenueArrayCNT")),  dt,VenueArrayPKx(session("VenueArrayCNT")))
 					Next
 					session("VenueArrayCNT")=session("VenueArrayCNT")+1
      		if session("VenueArrayCNT") <= VenueArrayL.Count -1
					DoNextVenue() 	
				else
					if session("CPlanMode")="Add" then
						pnlNewADPlanSVDW.visible=false
						pnlNewADActivateplan.visible=true
					else
						pnlNewADPlanSVDW.visible=false
						pnlpostings.visible=true
						editplan(session("cplanid"))	
						
					end if
	    		end if
	    	elseif x.id= "Button3" then
	    		pnlNewADPlanSVDW.visible=false
	    		pnlPPdetail.visible=true
	    		pnlPPdetailMain.visible=true
	    		pnlpostings.visible=false
	    	
    		end if
    	End Sub

		sub DoNextVenue() 
			Dim VenueArraykeyL As ArrayList = CType(Session("VenueArraykey"), ArrayList)
			Dim VenueArrayL As ArrayList = CType(Session("VenueArray"), ArrayList)
			dim VenueArrayPKx as ArrayList = CType(Session("VenueArrayPK"), ArrayList)
			
			lblCvenue.text =VenueArrayL(session("VenueArrayCNT")) & " " & VenueArraykeyL( session("VenueArrayCNT"))
			Calendar1.SelectedDates.Clear
			datesArray.Clear
		end sub




Sub Calendar1_SelectionChangedAA(sender As Object, e As EventArgs)

       ' Is the date already selected? If so, unselect it

       dim index As Integer = -1

       index = datesArray.indexOf( Calendar1.SelectedDate )

       If index >= 0 Then

          datesArray.RemoveAt( index )

       Else

          datesArray.Add( Calendar1.SelectedDate )

       End if
			Viewstate("selectedDates") = datesArray
		  DisplaySelectedDates()

    End Sub
Sub DisplaySelectedDates()

          Calendar1.SelectedDates.Clear

          Dim i As Integer

          For i = 0 to datesArray.Count - 1

             Calendar1.SelectedDates.Add( datesArray(i) )

          Next

    End Sub
    
    
      Sub ItemDataBoundEventHandlerPSCH(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

					Dim itemCellKey As TableCell = e.Item.Cells(0)
 					Dim itemCellSCHStatus As TableCell = e.Item.Cells(10)
					
					
					Dim itemCellSCHStatusTEXT as string = itemCellSCHStatus.text
					Dim itemCellKeytext as string = itemCellKey.text

                Dim ChkSelected,DGbtnAddPosts,DGbtnPublish As Button
                ChkSelected = e.Item.Cells(0).FindControl("btnstatchg")
                DGbtnAddPosts = e.Item.Cells(0).FindControl("btnAddPosts")
                DGbtnPublish = e.Item.Cells(0).FindControl("btnPublish")

                If itemCellSCHStatusTEXT = "Active" Then
                    ChkSelected.Text = "Inactivate"
                    	DGbtnAddPosts.enabled=true
							DGbtnPublish.enabled=true
                Else
                    ChkSelected.Text = "Activate"
                    DGbtnAddPosts.enabled=false
							DGbtnPublish.enabled=false

                End If
              
              
               if session("UPTdate")= "true" then
               	
					 	if itemCellKeytext <> session("UPTdateValue") then
					 		
					  		e.item.Visible = False
					 	end if
					else
					 	e.item.Visible = true
					end if
					 

            End If
        End Sub


		  
    End Class
End Namespace