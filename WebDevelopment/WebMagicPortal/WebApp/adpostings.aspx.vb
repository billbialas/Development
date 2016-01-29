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

namespace PageTemplate
    Public Class adpostings
        Inherits PageTemplate
        
        public Pl_search as textbox
        public dd_PTDue,dd_PStat,dd_ADs,dd_ADPlan,dd_Venues as dropdownlist
        public ADVenuesPP,ADVenuesPPDate as datagrid
        public chkvwdate,chkvwven,chkvwAll as checkbox
			public _dataSet As New DataSet()
			public pnlDG2,pnlDG1,pnlLSdetail,pnlPMain  as panel
			public pstsvenue,pststatus,pstEPdate,pstsadno,pstsadstage,lbpgtitle,lblOrderBy as label
			public pstadfrom,pstadto as textbox
			public cdrCalendar2
			public showcalc ,lnkEPubDate as linkbutton
			public BTNSetPub , BTNPublater as button
			
        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles mybase.load

            If Not (Page.IsPostBack) Then
       			if request.querystring("org")="default" then
       				session("keepadmfiltersA")="false"
       				session.remove("PubSearchFV")
            		session.remove("PubStatFV")
            		session.remove("PubADSFV")
            		session.remove("PubADPlanFV")
            		session.remove("PubTargetDateFV")
            		session.remove("PubADVenueFV")
       			end if
            	lblOrderBy.Text = "tbl_leadadvenues desc"
					FillADS()
					'response.write(session("cplanid"))
					
					if request.querystring("source")="planedit" then
						FillPlanS("All")
						FillVenues("All")
						if session("keepadmfiltersA")="false" then	
								dd_ADs.SelectedIndex = dd_ADs.Items.IndexOf(dd_ADs.Items.FindByvalue(session("adno")))
							dd_ADPlan.SelectedIndex = dd_ADPlan.Items.IndexOf(dd_ADPlan.Items.FindByvalue(session("CPlanId")))
							dd_Venues.SelectedIndex = dd_Venues.Items.IndexOf(dd_Venues.Items.FindByvalue(session("Pvenue")))
							
							dd_PStat.SelectedIndex = dd_PStat.Items.IndexOf(dd_PStat.Items.FindByvalue("Unpublished"))
							dd_PTDue.SelectedIndex = dd_PTDue.Items.IndexOf(dd_PTDue.Items.FindByvalue("All"))
							filterVenuesNOBT()	
							filterVenuesANOBT()		
			
						else
							Pl_search.text=session("PubSearchFV")
			         	dd_PStat.SelectedIndex = dd_PStat.Items.IndexOf(dd_PStat.Items.FindByText(session("PubStatFV")))
			         	dd_ADs.SelectedIndex = dd_ADs.Items.IndexOf(dd_ADs.Items.FindByText(session("PubADSFV")))
			         	dd_ADPlan.SelectedIndex = dd_ADPlan.Items.IndexOf(dd_ADPlan.Items.FindByvalue(session("PubADPlanFV")))	         		
			         	dd_PTDue.SelectedIndex = dd_PTDue.Items.IndexOf(dd_PTDue.Items.FindByText(session("PubTargetDateFV")))
			         	dd_Venues.SelectedIndex = dd_Venues.Items.IndexOf(dd_Venues.Items.FindByText(session("PubADVenueFV")))
							
						end if
							
						bindADVenuesPP(session("adno"))
						pnlDG2.visible=true
						pnlDG1.visible=false
						chkvwAll.checked=true
						ADVenuesPP.Columns(4).Visible = false
							ADVenuesPP.Columns(6).Visible = false
							ADVenuesPP.Columns(7).Visible = false
							dd_PStat.enabled=false
							dd_ADs.enabled=false
							dd_ADPlan.enabled=false
							dd_Venues.enabled=false
							lbpgtitle.text="Publishing AD# " + session("adno")
						
					elseif request.querystring("source")="newpostview" then
						FillPlanS("All")
						FillVenues("All")
						if session("keepadmfiltersA")="false" then
							dd_ADs.SelectedIndex = dd_ADs.Items.IndexOf(dd_ADs.Items.FindByvalue(session("adno")))
							dd_ADPlan.SelectedIndex = dd_ADPlan.Items.IndexOf(dd_ADPlan.Items.findbyvalue("99999"))
							dd_Venues.SelectedIndex = dd_Venues.Items.IndexOf(dd_Venues.Items.FindBytext("All"))
							
							dd_PStat.SelectedIndex = dd_PStat.Items.IndexOf(dd_PStat.Items.FindByvalue("Unpublished"))
							dd_PTDue.SelectedIndex = dd_PTDue.Items.IndexOf(dd_PTDue.Items.FindByvalue("All"))
							filterVenuesNOBT()
							filterVenuesANOBT()
						else
							Pl_search.text=session("PubSearchFV")
			         	dd_PStat.SelectedIndex = dd_PStat.Items.IndexOf(dd_PStat.Items.FindByText(session("PubStatFV")))
			         	dd_ADs.SelectedIndex = dd_ADs.Items.IndexOf(dd_ADs.Items.FindByText(session("PubADSFV")))
			         	dd_ADPlan.SelectedIndex = dd_ADPlan.Items.IndexOf(dd_ADPlan.Items.FindByvalue(session("PubADPlanFV")))	         		
			         	dd_PTDue.SelectedIndex = dd_PTDue.Items.IndexOf(dd_PTDue.Items.FindByText(session("PubTargetDateFV")))
			         	dd_Venues.SelectedIndex = dd_Venues.Items.IndexOf(dd_Venues.Items.FindByText(session("PubADVenueFV")))
						
						end if
						
						bindADVenuesPP(session("adno"))
						pnlDG2.visible=true
						pnlDG1.visible=false
						chkvwAll.checked=true
						
					elseif request.querystring("source")="newpostpub" then
						FillPlanS("All")
						FillVenues("All")
						
							if session("keepadmfiltersA")="false" then
								dd_ADs.SelectedIndex = dd_ADs.Items.IndexOf(dd_ADs.Items.FindByvalue(session("adno")))
								dd_ADPlan.SelectedIndex = dd_ADPlan.Items.IndexOf(dd_ADPlan.Items.FindByvalue(session("cplanid")))
								dd_Venues.SelectedIndex = dd_Venues.Items.IndexOf(dd_Venues.Items.FindBytext("All"))
								dd_PStat.SelectedIndex = dd_PStat.Items.IndexOf(dd_PStat.Items.FindByvalue("Unpublished"))
								dd_PTDue.SelectedIndex = dd_PTDue.Items.IndexOf(dd_PTDue.Items.FindByvalue("Due Today"))
								filterVenuesNOBT()
								filterVenuesANOBT()		
							else
								Pl_search.text=session("PubSearchFV")
				         	dd_PStat.SelectedIndex = dd_PStat.Items.IndexOf(dd_PStat.Items.FindByText(session("PubStatFV")))
				         	dd_ADs.SelectedIndex = dd_ADs.Items.IndexOf(dd_ADs.Items.FindByText(session("PubADSFV")))
				         	dd_ADPlan.SelectedIndex = dd_ADPlan.Items.IndexOf(dd_ADPlan.Items.FindByvalue(session("PubADPlanFV")))	         		
				         	dd_PTDue.SelectedIndex = dd_PTDue.Items.IndexOf(dd_PTDue.Items.FindByText(session("PubTargetDateFV")))
				         	dd_Venues.SelectedIndex = dd_Venues.Items.IndexOf(dd_Venues.Items.FindByText(session("PubADVenueFV")))
							
							end if
						
						bindADVenuesPP("All")
						pnlDG2.visible=true
						pnlDG1.visible=false
						chkvwAll.checked=true
						ADVenuesPP.Columns(4).Visible = false
							ADVenuesPP.Columns(6).Visible = false
							ADVenuesPP.Columns(7).Visible = false
							'--
							dd_PStat.enabled=true
							dd_ADs.enabled=true
							dd_ADPlan.enabled=true
							dd_Venues.enabled=true
							lbpgtitle.text="Publishing AD# " + session("adno")
							
					elseif request.querystring("source")="newpostpubR" then
						FillPlanS("All")
						FillVenues("All")
						if session("keepadmfiltersA")="false" then
							
							dd_ADs.SelectedIndex = dd_ADs.Items.IndexOf(dd_ADs.Items.FindByvalue(session("adno")))
							dd_ADPlan.SelectedIndex = dd_ADPlan.Items.IndexOf(dd_ADPlan.Items.FindByvalue("99999"))
							dd_Venues.SelectedIndex = dd_Venues.Items.IndexOf(dd_Venues.Items.FindBytext("All"))
							
							dd_PStat.SelectedIndex = dd_PStat.Items.IndexOf(dd_PStat.Items.FindBytext("Unpublished"))
							dd_PTDue.SelectedIndex = dd_PTDue.Items.IndexOf(dd_PTDue.Items.FindBytext("All"))
							filterVenuesNOBT()		
							filterVenuesANOBT()
		
						else
							Pl_search.text=session("PubSearchFV")
			         	dd_PStat.SelectedIndex = dd_PStat.Items.IndexOf(dd_PStat.Items.FindByText(session("PubStatFV")))
			         	dd_ADs.SelectedIndex = dd_ADs.Items.IndexOf(dd_ADs.Items.FindByText(session("PubADSFV")))
			         	dd_ADPlan.SelectedIndex = dd_ADPlan.Items.IndexOf(dd_ADPlan.Items.FindByvalue(session("PubADPlanFV")))	         		
			         	dd_PTDue.SelectedIndex = dd_PTDue.Items.IndexOf(dd_PTDue.Items.FindByText(session("PubTargetDateFV")))
			         	dd_Venues.SelectedIndex = dd_Venues.Items.IndexOf(dd_Venues.Items.FindByText(session("PubADVenueFV")))
						
						end if
					
						bindADVenuesPP(session("adno"))
						pnlDG2.visible=true
						pnlDG1.visible=false
						chkvwAll.checked=true
						ADVenuesPP.Columns(4).Visible = false
							ADVenuesPP.Columns(6).Visible = false
							ADVenuesPP.Columns(7).Visible = false
							dd_PStat.enabled=false
							dd_ADs.enabled=false
							dd_ADPlan.enabled=false
							dd_Venues.enabled=false
							lbpgtitle.text="Publishing AD# " + session("adno")	
						
						
					elseif request.querystring("source")="newpostpubP" then
						FillPlanS("All")
						FillVenues("All")
						if session("keepadmfiltersA")="false" then
							dd_ADs.SelectedIndex = dd_ADs.Items.IndexOf(dd_ADs.Items.FindByvalue(session("adno")))
							dd_ADPlan.SelectedIndex = dd_ADPlan.Items.IndexOf(dd_ADPlan.Items.FindByvalue(session("cplanid")))
							dd_Venues.SelectedIndex = dd_Venues.Items.IndexOf(dd_Venues.Items.FindBytext("All"))
							
							dd_PStat.SelectedIndex = dd_PStat.Items.IndexOf(dd_PStat.Items.FindByvalue("Unpublished"))
							dd_PTDue.SelectedIndex = dd_PTDue.Items.IndexOf(dd_PTDue.Items.FindByvalue("Due Today"))
							filterVenuesNOBT()		
							filterVenuesANOBT()
						else
							Pl_search.text=session("PubSearchFV")
			         	dd_PStat.SelectedIndex = dd_PStat.Items.IndexOf(dd_PStat.Items.FindByText(session("PubStatFV")))
			         	dd_ADs.SelectedIndex = dd_ADs.Items.IndexOf(dd_ADs.Items.FindByText(session("PubADSFV")))
			         	dd_ADPlan.SelectedIndex = dd_ADPlan.Items.IndexOf(dd_ADPlan.Items.FindByvalue(session("PubADPlanFV")))	         		
			         	dd_PTDue.SelectedIndex = dd_PTDue.Items.IndexOf(dd_PTDue.Items.FindByText(session("PubTargetDateFV")))
			         	dd_Venues.SelectedIndex = dd_Venues.Items.IndexOf(dd_Venues.Items.FindByText(session("PubADVenueFV")))
						
						end if
						
						bindADVenuesPP(session("adno"))
						pnlDG2.visible=true
						pnlDG1.visible=false
						chkvwAll.checked=true
						ADVenuesPP.Columns(4).Visible = false
							ADVenuesPP.Columns(6).Visible = false
							ADVenuesPP.Columns(7).Visible = false
							dd_PStat.enabled=false
							dd_ADs.enabled=false
							dd_ADPlan.enabled=false
							dd_Venues.enabled=false
							lbpgtitle.text="Publishing AD# " + session("adno")
							
					elseif request.querystring("source")="admanagerQ" then
					
								FillPlanS("All")
								FillVenues("All")
								if session("keepadmfiltersA")="false" then
									dd_PTDue.SelectedIndex = dd_PTDue.Items.IndexOf(dd_PTDue.Items.FindByvalue("Due Today"))
									dd_PStat.SelectedIndex = dd_PStat.Items.IndexOf(dd_PStat.Items.FindByvalue("Unpublished"))
									dd_ADs.SelectedIndex = dd_ADs.Items.IndexOf(dd_ADs.Items.FindBytext("All"))
									dd_ADPlan.SelectedIndex = dd_ADPlan.Items.IndexOf(dd_ADPlan.Items.FindByvalue("99999"))
									dd_Venues.SelectedIndex = dd_Venues.Items.IndexOf(dd_Venues.Items.FindBytext("All"))
									filterVenuesNOBT()
									filterVenuesANOBT()
								else
									Pl_search.text=session("PubSearchFV")
					         	dd_PStat.SelectedIndex = dd_PStat.Items.IndexOf(dd_PStat.Items.FindByText(session("PubStatFV")))
					         	dd_ADs.SelectedIndex = dd_ADs.Items.IndexOf(dd_ADs.Items.FindByText(session("PubADSFV")))
					         	dd_ADPlan.SelectedIndex = dd_ADPlan.Items.IndexOf(dd_ADPlan.Items.FindByvalue(session("PubADPlanFV")))	         		
					         	dd_PTDue.SelectedIndex = dd_PTDue.Items.IndexOf(dd_PTDue.Items.FindByText(session("PubTargetDateFV")))
					         	dd_Venues.SelectedIndex = dd_Venues.Items.IndexOf(dd_Venues.Items.FindByText(session("PubADVenueFV")))
								
								end if
								
								
								bindADVenuesPP("All")
								pnlDG2.visible=true
								pnlDG1.visible=false
								chkvwAll.checked=true
								ADVenuesPP.Columns(4).Visible = false
								ADVenuesPP.Columns(6).Visible = false
								ADVenuesPP.Columns(7).Visible = false
								dd_PStat.enabled=false
								lbpgtitle.text="Publishing Work Queue"
								
					elseif request.querystring("source")="admanager" then
					
								FillPlanS("All")
								FillVenues("All")
								if session("keepadmfiltersA")="false" then
									dd_PTDue.SelectedIndex = dd_PTDue.Items.IndexOf(dd_PTDue.Items.FindByvalue("All"))
									dd_PStat.SelectedIndex = dd_PStat.Items.IndexOf(dd_PStat.Items.FindBytext("Published"))
									dd_ADs.SelectedIndex = dd_ADs.Items.IndexOf(dd_ADs.Items.FindBytext("All"))
									dd_ADPlan.SelectedIndex = dd_ADPlan.Items.IndexOf(dd_ADPlan.Items.FindByvalue("99999"))
									dd_Venues.SelectedIndex = dd_Venues.Items.IndexOf(dd_Venues.Items.FindBytext("All"))
									filterVenuesNOBT()		
									filterVenuesANOBT()
								else
									Pl_search.text=session("PubSearchFV")
					         	dd_PStat.SelectedIndex = dd_PStat.Items.IndexOf(dd_PStat.Items.FindByText(session("PubStatFV")))
					         	dd_ADs.SelectedIndex = dd_ADs.Items.IndexOf(dd_ADs.Items.FindByText(session("PubADSFV")))
					         	dd_ADPlan.SelectedIndex = dd_ADPlan.Items.IndexOf(dd_ADPlan.Items.FindByvalue(session("PubADPlanFV")))	         		
					         	dd_PTDue.SelectedIndex = dd_PTDue.Items.IndexOf(dd_PTDue.Items.FindByText(session("PubTargetDateFV")))
					         	dd_Venues.SelectedIndex = dd_Venues.Items.IndexOf(dd_Venues.Items.FindByText(session("PubADVenueFV")))
								
								end if
								
								bindADVenuesPP("All")
								pnlDG2.visible=true
								pnlDG1.visible=false
								chkvwAll.checked=true
								ADVenuesPP.Columns(5).Visible = false
							
								dd_PTDue.enabled=false
								lbpgtitle.text="Publishing Date Details"
					else
							if session("adno")="All" then
								FillPlanS("All")
								FillVenues("All")
									if session("keepadmfiltersA")="false" then
				
					dd_PTDue.SelectedIndex = dd_PTDue.Items.IndexOf(dd_PTDue.Items.FindByvalue("Due Today"))
								dd_PStat.SelectedIndex = dd_PStat.Items.IndexOf(dd_PStat.Items.FindByvalue("Unpublished"))
								dd_ADs.SelectedIndex = dd_ADs.Items.IndexOf(dd_ADs.Items.FindBytext("All"))
								dd_ADPlan.SelectedIndex = dd_ADPlan.Items.IndexOf(dd_ADPlan.Items.FindByvalue("99999"))
								dd_Venues.SelectedIndex = dd_Venues.Items.IndexOf(dd_Venues.Items.FindBytext("All"))
								
								filterVenuesNOBT()
								filterVenuesANOBT()
				
								else
									Pl_search.text=session("PubSearchFV")
					         	dd_PStat.SelectedIndex = dd_PStat.Items.IndexOf(dd_PStat.Items.FindByText(session("PubStatFV")))
					         	dd_ADs.SelectedIndex = dd_ADs.Items.IndexOf(dd_ADs.Items.FindByText(session("PubADSFV")))
					         	dd_ADPlan.SelectedIndex = dd_ADPlan.Items.IndexOf(dd_ADPlan.Items.FindByvalue(session("PubADPlanFV")))	         		
					         	dd_PTDue.SelectedIndex = dd_PTDue.Items.IndexOf(dd_PTDue.Items.FindByText(session("PubTargetDateFV")))
					         	dd_Venues.SelectedIndex = dd_Venues.Items.IndexOf(dd_Venues.Items.FindByText(session("PubADVenueFV")))
								
								end if
							
								bindADVenuesPP("All")
								pnlDG2.visible=true
								pnlDG1.visible=false
								chkvwAll.checked=true
							else
								dd_ADs.SelectedIndex = dd_ADs.Items.IndexOf(dd_ADs.Items.FindByvalue(session("adno")))
								FillPlanS(session("adno"))	
								FillVenues(session("adno"))					
								dd_PTDue.SelectedIndex = dd_PTDue.Items.IndexOf(dd_PTDue.Items.FindByvalue("Due Today"))
								dd_PStat.SelectedIndex = dd_PStat.Items.IndexOf(dd_PStat.Items.FindByvalue("Unpublished"))
								filterVenuesNOBT()
								bindADVenuesPP(session("adno"))
								bindADVenuesPPDate(session("adno"))
								pnlDG2.visible=true
								pnlDG1.visible=false
								chkvwAll.checked=true
							end if
					end if
					
					session("keepadmfilters")="true"
					bindADVenuesPP("All")
            End If
            pagesetup()

        End Sub
     
        Sub closecalendar2(ByVal sender As Object, ByVal e As EventArgs)
           	dim x as linkbutton = sender
           	
           		showcalc.visible = false
            	cdrCalendar2.visible = false
          	
          
          	
        End Sub
        
        Sub showcalendar2(ByVal sender As Object, ByVal e As EventArgs)
            Dim x As LinkButton = sender
            Session("CalDchk") = x.ID
           
             	cdrCalendar2.visible = True
            	showcalc.visible = True
           
            
        End Sub
        
          Public Sub Calendar2_SelectionChanged(ByVal sender As System.Object, ByVal e As System.EventArgs)
             if Session("CalDchk") = "lnkEPubDate" Then
                pstadfrom.Text = cdrCalendar2.SelectedDate
				
            End If

        		showcalc.visible = false
            cdrCalendar2.visible = false
           
        End Sub
        
        Sub ExitADV(ByVal sender As Object, ByVal e As EventArgs)
              pnlLSdetail.visible=false
		        	pnlDG2.visible=true
		        	pnlPMain.visible=true
		        	 bindADVenuesPPDate("All")
        	
        End Sub
        
        Sub showbydate(ByVal Source As System.Object, ByVal e As System.EventArgs)
        		Dim x As checkbox = Source
             if x.ID = "chkvwAll" then
             	chkvwdate.checked=false
             	chkvwven.checked=false
             	pnlDG2.visible=true
					pnlDG1.visible=false
					filterVenuesNOBT()
					bindADVenuesPP("All")
             elseif x.ID = "chkvwdate" then
             	chkvwven.checked=false
             	chkvwAll.checked=false
             	pnlDG2.visible=false
					pnlDG1.visible=true
             	bindADVenuesPPDate("All")
             
             else
             	chkvwdate.checked=false
             	chkvwAll.checked=false
             end if
             
        
         
         
        end sub
        
        
        Sub filterVenuesA(ByVal Source As System.Object, ByVal e As System.EventArgs)
         dim y as textbox = Source
           if y.ID = "Pl_search" then
           		
           		if Pl_search.text.length > 0 then
           			session("PubSearchF")="true"
           			session("PubSearchFV")=Pl_search.text
           		else
           			session("PubSearchF")="false"
           		end if
           	end if
           	 
           ADVenuesPP.CurrentPageIndex = 0
           if session("adno")="All" then
					bindADVenuesPP("All")
					
				else
					bindADVenuesPP(session("adno"))
				end if

        end sub
        
        Sub filterVenuesANOBT()
           		
           		if Pl_search.text.length > 0 then
           			session("PubSearchF")="true"
           			session("PubSearchFV")=Pl_search.text
           		else
           			session("PubSearchF")="false"
           			session.remove("PubSearchFV")
           		end if
      
        end sub
        
        
        Sub filterVenuesNOBT() 
        			
        		 	if dd_PStat.selecteditem.text = "All" then
        		 
             		session("PubStatF")="false"
             		session("PubStatFV")="All"
             	else
             	
             		session("PubStatF")="true"
             		session("PubStatFV")=dd_PStat.selecteditem.text
             	end if
             	if dd_ADs.selecteditem.text = "All" then
             	
             		session("PubADSF")="false"
             		session("PubADSFV")="All"
             	else
             	
             		session("PubADSF")="true"
             		session("PubADSFV")=dd_ADs.selecteditem.text
             		if dd_ADs.selecteditem.text = "Actives" or dd_ADs.selecteditem.text = "Inactives" then
             		
             		else
             		
							if request.querystring("source")="planedit" or request.querystring("source")="newpost" or request.querystring("source")="newpostpub"  or request.querystring("source")="newpostpubP"      then             		
             			
             			else
             			
             				FillPlanS(dd_ADs.selecteditem.value)
             				dd_ADPlan.SelectedIndex = dd_ADPlan.Items.IndexOf(dd_ADPlan.Items.FindBytext("All"))
             			end if
             		end if
             	end if
             	if dd_ADPlan.selecteditem.text = "All" then
             
             		session("PubADPlanF")="false"
             		session("PubADPlanFV")="99999"
             	else
             
             		session("PubADPlanF")="true"
             		session("PubADPlanFV")=dd_ADPlan.selecteditem.value
             	end if
             	if dd_PTDue.selecteditem.text = "All" then
             
             		session("PubTargetDateF")="false"
             		session("PubTargetDateFV")="All"
             	else
             	
             		session("PubTargetDateF")="true"
             		session("PubTargetDateFV")=dd_PTDue.selecteditem.text
             	end if
             	if dd_Venues.selecteditem.text = "All" then
             	
             		session("PubADVenueF")="false"
             		session("PubADVenueFV")="All"
             	else
             	
             		session("PubADVenueF")="true"
             		session("PubADVenueFV")=dd_Venues.selecteditem.text
             	end if
                  		
        end sub
         Sub filterVenues(ByVal Source As System.Object, ByVal e As System.EventArgs)
        
            Dim x As dropdownlist = Source
             if x.ID = "dd_PStat" then
             	if dd_PStat.selecteditem.text = "All" then
             		session("PubStatF")="false"
             		session("PubStatFV")="All"
             	else
             		session("PubStatF")="true"
             		session("PubStatFV")=dd_PStat.selecteditem.text
             	end if
             elseif x.ID = "dd_ADs" then
             	if dd_ADs.selecteditem.text = "All" then
             		session("PubADSF")="false"
             		session("PubADSFV")="All"
             		FillPlanS("All")
             		dd_ADPlan.SelectedIndex = dd_ADPlan.Items.IndexOf(dd_ADPlan.Items.FindBytext("All"))
             	else
             		session("PubADSF")="true"
             		session("PubADSFV")=dd_ADs.selecteditem.text
             		if dd_ADs.selecteditem.text = "Actives" or dd_ADs.selecteditem.text = "Inactives" then
             		else
             			FillPlanS(dd_ADs.selecteditem.value)
             			dd_ADPlan.SelectedIndex = dd_ADPlan.Items.IndexOf(dd_ADPlan.Items.FindBytext("All"))
             		end if
             	end if
             elseif x.ID = "dd_ADPlan" then
             	if dd_ADPlan.selecteditem.text = "All" then
             		session("PubADPlanF")="false"
             		session("PubADPlanFV")="99999"
             	else
             		session("PubADPlanF")="true"
             		session("PubADPlanFV")=dd_ADPlan.selecteditem.value
             	end if
             elseif x.ID = "dd_PTDue" then
             	if dd_PTDue.selecteditem.text = "All" then
             		session("PubTargetDateF")="false"
             	else
             		session("PubTargetDateF")="true"
             		session("PubTargetDateFV")=dd_PTDue.selecteditem.text
             	end if
             elseif x.ID = "dd_Venues" then
             	if dd_Venues.selecteditem.text = "All" then
             		session("PubADVenueF")="false"
             		session("PubADVenueFV")="All"
             	else
             		session("PubADVenueF")="true"
             		session("PubADVenueFV")=dd_Venues.selecteditem.text
             	end if
             end if
           	
           
           ADVenuesPP.CurrentPageIndex = 0
           bindADVenuesPP("All")
           bindADVenuesPPDate("All")

        End Sub
        
         Sub clearall(ByVal Source As System.Object, ByVal e As System.EventArgs)
				session("keepadmfiltersA")="false"

            Response.Redirect("adpostings.aspx?source=" & request.querystring("source"))
        End Sub
        
        
         Sub MyDataGrid_PagePP(ByVal sender As Object, ByVal e As DataGridPageChangedEventArgs)
            'GetCheckBoxValues()

            ADVenuesPP.CurrentPageIndex = e.NewPageIndex
            'RePopulateCheckBoxes()

        End Sub
        
          Sub SortCommand_Click(sender As Object, e As DataGridSortCommandEventArgs)
				if session("sortexpression") =  e.SortExpression then 
					if session("sortdirection") = "ASC" then
						session("sortdirection") = "DESC"
					else
						session("sortdirection") = "ASC" 
					end if
				else 
					session("sortexpression") =  e.SortExpression
					session("sortdirection") = "ASC" 
				end if
          	lblOrderBy.Text =  session("sortexpression") + " " + session("sortdirection") 
     			bindADVenuesPP(session("adno"))

        
        end sub
        
        
         
         Public Sub bindADVenuesPP(type as string)
             Dim strUID As String = Session("userid")
            
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            if request.querystring("source")="planedit" then
            
				      mycommand = "Select cast(av_leadads_FK as nvarchar(255)) as 'adno', cast(tbl_leadadvenues as nvarchar(255)) as 'venno', " _
				                & "convert(varchar(20),av_APFrom,101) as 'APFrom', convert(varchar(20),av_APTo,101) as 'APFTo', x_canselfpub,*, " _
				                & "cast(LAP_tbl_pk as nvarchar(255)) as 'adplanno', case when (len(ad_title) > 50) then left(ad_title,50)+ '...' else cast(tbl_leadad_pk as varchar(255)) + ' ' + ad_title end  as 'sadtitle', " _
				                 & "case when (len(lap_name) > 50) then left(lap_name,50)+ '...' else lap_name end as 'saptitle', " _
				                & "convert(varchar(20),av_APTo,101) as 'APTo', convert(varchar(20),getdate(),101) as 'Today', " _
				                & "convert(varchar(20),getdate()+2,101) as 'Today2', convert(varchar(20),getdate()-1,101) as 'yesterday', " _
				                & "convert(varchar(20),getdate()+5,101) as 'Today5' " _
				                & "from tbl_LeadADVenues " _
				                & "join tbl_LeadADs on tbl_leadad_pk=av_leadads_FK  " _
				                & "left join dbo.tbl_xwalk on x_descr  =av_name and x_type='leadsource' " _
				                & "left join dbo.tbl_leadADPlans on lap_adfk = av_leadads_FK and LAP_tbl_pk= av_lapfk  " _
				                & "where ad_userid ='" & session("userid") & "' " _				                
				                & "and ad_status ='Active' and lap_status='Active' order by " + lblOrderBy.Text
				                
				else
				
				  	mycommand = "Select cast(av_leadads_FK as nvarchar(255)) as 'adno', cast(tbl_leadadvenues as nvarchar(255)) as 'venno', " _
				                & "convert(varchar(20),av_APFrom,101) as 'APFrom', convert(varchar(20),av_APTo,101) as 'APFTo', x_canselfpub,*, " _
				                & "cast(LAP_tbl_pk as nvarchar(255)) as 'adplanno', case when (len(ad_title) > 50) then left(ad_title,50)+ '...' else cast(tbl_leadad_pk as varchar(255)) + ' ' + ad_title end  as 'sadtitle', " _
				                 & "case when (len(lap_name) > 50) then left(lap_name,50)+ '...' else lap_name end as 'saptitle', " _
				                & "convert(varchar(20),av_APTo,101) as 'APTo', convert(varchar(20),getdate(),101) as 'Today', " _
				                 & "convert(varchar(20),getdate()+2,101) as 'Today2', convert(varchar(20),getdate()-1,101) as 'yesterday', " _
				                & "convert(varchar(20),getdate()+5,101) as 'Today5' " _
				                & "from tbl_LeadADVenues " _
				                & "join tbl_LeadADs on tbl_leadad_pk=av_leadads_FK  " _
				                & "left join dbo.tbl_xwalk on x_descr  =av_name and x_type='leadsource' " _
				                & "left join dbo.tbl_leadADPlans on lap_adfk = av_leadads_FK and LAP_tbl_pk= av_lapfk  " _
				                & "where ad_userid ='" & session("userid") & "' " _				                
				                & "and ad_status ='Active' and lap_status='Active' order by " + lblOrderBy.Text 
		      end if
		      Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_LeadADVenues")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_LeadADVenues"))
					 dvProducts.RowFilter = "venno like '%'"
					 if session("PubStatF")="true" then
					 	dvProducts.RowFilter = dvProducts.RowFilter + " and av_adplaced = '" & dd_PStat.selecteditem.text & "'"	
                end if
                if session("PubADSF")="true" then
                	if dd_ADs.selecteditem.text="Actives" then
                		dvProducts.RowFilter = dvProducts.RowFilter + " and ad_status = 'Active'"	
            		elseif dd_ADs.selecteditem.text="Inactives" then
                		dvProducts.RowFilter = dvProducts.RowFilter + " and ad_status = 'Inactive'"	
            		else
                		dvProducts.RowFilter = dvProducts.RowFilter + " and adno= '" & dd_ADs.selecteditem.value & "'"	
                	end if
                end if
                if session("PubADPlanF")="true" then
                 	if dd_ADPlan.selecteditem.text="Actives" then
                		dvProducts.RowFilter = dvProducts.RowFilter + " and lap_status = 'Active'"	
            		elseif dd_ADPlan.selecteditem.text="Inactives" then
                		dvProducts.RowFilter = dvProducts.RowFilter + " and lap_status = 'Inactive'"	
            		else
                     	dvProducts.RowFilter = dvProducts.RowFilter + " and adplanno = '" & dd_ADPlan.selecteditem.value & "'"	
           			end if
                end if
                if session("PubSearchF")="true" then
                	dvProducts.RowFilter = dvProducts.RowFilter + " and (adno like '%" & Pl_search.text & "%' or venno like '%" & Pl_search.text & "%' or lap_name like '%" & Pl_search.text & "%' or ad_title like '%" & Pl_search.text & "%' or av_key like '%" & Pl_search.text & "%' or av_name like '%" & Pl_search.text & "%' or av_Postingno like '%" & Pl_search.text & "%' or APFrom like '%" & Pl_search.text & "%')"   
                end if
                if session("PubTargetDateF")="true" then
               	if dd_PTDue.selecteditem.text="Due Today" then 
               	   dvProducts.RowFilter = dvProducts.RowFilter + " and APTo = Today "
               	elseif  dd_PTDue.selecteditem.text="Yesterday" then 
               		dvProducts.RowFilter = dvProducts.RowFilter + " and APTo = yesterday "
               	   
             		elseif dd_PTDue.selecteditem.text="Past Due" then 
             			dvProducts.RowFilter = dvProducts.RowFilter + " and APTo < Today "
                	elseif dd_PTDue.selecteditem.text="In 2 Days" then 
                	  	dvProducts.RowFilter = dvProducts.RowFilter + " and APTo > Today and APTo <= Today2 "
                	else
                		dvProducts.RowFilter = dvProducts.RowFilter + " and APTo > Today and APTo <= Today5 "
                	end if
                end if
                if session("PubADVenueF")="true" then
                		dvProducts.RowFilter = dvProducts.RowFilter + " and av_name = '" & dd_Venues.selecteditem.text & "'"	
           		 end if
                
                
                ADVenuesPP.DataSource = dvProducts
                ADVenuesPP.DataBind()
					
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
            
            
        End Sub
        
        
         Public Sub bindADVenuesPPDate(type as string)
             Dim strUID As String = Session("userid")
           
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            if type="All"
            
				      mycommand = "begin " _
											& "select av_APTo,case when(av_adplaced = 'Unpublished') then 1 else 0 end as p, " _ 
											& "case when(av_adplaced = 'Published') then 1 else 0 end as a " _
											& "into #tmpy " _
											& "from dbo.tbl_LeadADVenues a " _
											& "join tbl_leadads on tbl_leadad_pk = av_leadads_FK " _
											& "where ad_userid ='" & session("userid") & "' " _
											& "and av_APTo is not null " _
											& "select convert(varchar(20),av_APTo,101) as 'APTo', sum(p) as 'Unpubv', sum(a) as 'Pubv' " _
											& "from #tmpy " _
											& "group by convert(varchar(20),av_APTo,101)  " _
											& "end " 
				else   
				
				 mycommand = "begin " _
											& "select av_APTo,case when(av_adplaced = 'Unpublished') then 1 else 0 end as p, " _ 
											& "case when(av_adplaced = 'Published') then 1 else 0 end as a " _
											& "into #tmpy " _
											& "from dbo.tbl_LeadADVenues a " _
											& "join tbl_leadads on tbl_leadad_pk = av_leadads_FK " _
											& "where ad_userid ='" & session("userid") & "' " _
											& "and tbl_leadad_pk='" & type & "' " _
											& "and av_APTo is not null " _
											& "select convert(varchar(20),av_APTo,101) as 'APTo', sum(p) as  'Unpubv', sum(a) as 'Pubv' " _
											& "from #tmpy " _
											& "group by convert(varchar(20),av_APTo,101) " _
											& "end " 
				  
		      end if
		      Try
                'Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                'dataAdapter.Fill(_dataSet)
                '	_dataSet.Tables(0).TableName = "Program"
       			 'ADVenuesPPDate.DataSource = _dataSet.Tables("Program")              
                'ADVenuesPPDate.DataBind()
					
					
					 Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_LeadADVenues")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_LeadADVenues"))
                 dvProducts.RowFilter = "APTo like '%'"
                 if session("PubTargetDateF")="true" then
               	if dd_PTDue.selecteditem.text="Due Today" then 
               		'response.write(Today().tostring("d"))
               	   dvProducts.RowFilter = dvProducts.RowFilter + " and APTo = '" & Today().tostring("d") & "'"

             		elseif dd_PTDue.selecteditem.text="Past Due" then 
             			dvProducts.RowFilter = dvProducts.RowFilter + " and APTo < '" & Today().tostring("d") & "'"
                	else
                	  	dvProducts.RowFilter = dvProducts.RowFilter + " and APTo > '" & Today().tostring("d") & "' and APTo <= '" & Today().tostring("d") & "'"
                	end if
                end if
                ADVenuesPPDate.DataSource = dvProducts
                ADVenuesPPDate.DataBind()
					
                
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
            
            
        End Sub
        
        
        
        Sub FillADS()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select *, case when (len(ad_title) > 30) then cast(tbl_leadad_pk as varchar(255)) + ' ' + left(ad_title,30)+ '...' else cast(tbl_leadad_pk as varchar(255)) + ' ' + ad_title end as 'Ftitle' " _
            						& "from dbo.tbl_LeadADs where ad_userid='" & Session("userid") & "' " _
            					 	& "and ad_status ='Active' order by ad_title"
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                dd_ADs.DataSource = dataReader
                dd_ADs.DataTextField = "Ftitle"
                dd_ADs.DataValueField = "tbl_leadad_pk"
                dd_ADs.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
            dd_ADs.Items.Insert(0, New ListItem("All", "99999"))
            dd_ADs.Items.Insert(1, New ListItem("Actives", "99998"))
            dd_ADs.Items.Insert(2, New ListItem("Inactives", "99997"))
        End Sub
        
         Sub FillPlanS(type as string)
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String 
           	  if request.querystring("source")="planedit" then
          			myCommand= "Select *,cast(LAP_tbl_pk as varchar(255)) as 'lplanno', case when (len(lap_name) > 30) then left(lap_name,30)+ '...' else lap_name end as 'Ftitle' from dbo.tbl_leadADPlans where lap_useridfk='" & Session("userid") & "'"
            else
            	if type="All" then
            				myCommand= "Select *,cast(LAP_tbl_pk as varchar(255)) as 'lplanno',  case when (len(lap_name) > 30) then left(lap_name,30)+ '...' else lap_name end as 'Ftitle' from dbo.tbl_leadADPlans " _
            						& "where lap_useridfk='" & Session("userid") & "' " _
            						 & "and lap_status='Active' order by lap_name " 
         
            	else
            	
            		myCommand= "Select *, cast(LAP_tbl_pk as varchar(255)) as 'lplanno',  case when (len(lap_name) > 50) then " _
            						& "left(lap_name,50)+ '...' else lap_name end as 'Ftitle' from dbo.tbl_leadADPlans " _
            						& "where lap_useridfk='" & Session("userid") & "' and lap_adfk='" & type & "' " _
            						 & "and lap_status='Active' order by lap_name" 
            	end if
            end if
            
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                dd_ADPlan.DataSource = dataReader
                dd_ADPlan.DataTextField = "Ftitle"
                dd_ADPlan.DataValueField = "lplanno"
                dd_ADPlan.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
            dd_ADPlan.Items.Insert(0, New ListItem("All", "99999"))
            dd_ADPlan.Items.Insert(1, New ListItem("Actives", "99998"))
            dd_ADPlan.Items.Insert(2, New ListItem("Inactives", "99997"))
        End Sub
        
        Sub FillVenues(type as string)
				Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String 
            if request.querystring("source")="planedit" then
             	myCommand = "Select distinct lv_name from tbl_LeadADPlanVenues join tbl_LeadADs on tbl_leadad_pk=lv_ad_fk where ad_userid='" & session("userid") & "' order by lv_name"
            else
            	if type="All" then
            	   	myCommand = "Select distinct lv_name from tbl_LeadADPlanVenues join tbl_LeadADs on " _
            	   				& "tbl_leadad_pk=lv_ad_fk where ad_userid='" & session("userid") & "' " _
            	   				 & "and lv_status ='Active' order by lv_name" 
          
            	else
            	
               	myCommand = "Select distinct lv_name from tbl_LeadADPlanVenues join tbl_LeadADs on tbl_leadad_pk=lv_ad_fk " _
               					& "where ad_userid='" & session("userid") & "' and lv_ad_fk='" & type & "' " _
               					 & "and lv_status ='Active' order by lv_name" 
       
       			end if
            end if
            
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                dd_Venues.DataSource = dataReader
                dd_Venues.DataTextField = "lv_name"
                dd_Venues.DataValueField = "lv_name"
                dd_Venues.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
  				dd_Venues.Items.Insert(0, New ListItem("All", "99999"))
        End Sub
        
        Sub cancelposting(ByVal Source As System.Object, ByVal e As System.EventArgs)

        
            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim advenuePK As String = item.Cells(0).Text
            Dim venuename As String = item.Cells(1).Text
           	Dim venuestat As String = item.Cells(4).Text
           	Dim venuetdate As String = item.Cells(5).text
           	
           
           	if x.text = "Cancel Date" then           	
           		updatevenue(advenuePK,"Canceled","","")
        		else
        			updatevenue(advenuePK,"Unpublished","","")
        		end if
        		bindADVenuesPP("All")
        end sub
        
         Sub publishposting(ByVal Source As System.Object, ByVal e As System.EventArgs)
 				Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim advenuePK As String = item.Cells(0).Text
            Dim venuename As String = item.Cells(1).Text
            Dim adno As String = item.Cells(8).Text
            Dim planno As String = item.Cells(9).Text
            Dim adstage As String = item.Cells(10).Text
        			session("Padno") = adno
            	session("Pvenue") = venuename
            	session("Pvenueno") =advenuePK
            	session("Ptype") = "Complete"
            	session("Oposter") = request.querystring("source")
            	if adstage="Finalized" then
            		session("Pmodify") = "No"
            	else
            		session("Pmodify") = "Yes"
            	end if
            	session("Ppplan") = planno
        			if not checkforadkey(adno) then
            		updateadtextwkey(adno)            		
        			end if
        			Response.Redirect("postad.aspx")
        		end sub
        		
        	Sub updateadtextwkey(id as string)
          	Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String = "sp_updateADtext"
            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

            Dim prmadpk As New SqlParameter("@adno", SqlDbType.int)
            prmadpk.Value = ctype(id,integer)
            myCommandADD.Parameters.Add(prmadpk)
      
				Dim prmadtext As New SqlParameter("@adtext", SqlDbType.varchar, 255)
            prmadtext.Value = "<br ><mycustomtag class='myclass' contenteditable='false'>%ADKEY%</mycustomtag>"
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
        End Sub
        		
        		
        		
        	public function checkforadkey(id as string) as boolean
			   Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select *  from dbo.tbl_LeadADs " _
            					& "where ad_text like '%%ADKEY%%' " _
            					& "and tbl_leadad_pk = '" & id & "' and ad_userid='" & session("userid") & "'"
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
        
         Sub updateposting(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim advenuePK As String = item.Cells(0).Text
            Dim venuename As String = item.Cells(1).Text
           	Dim venuestat As String = item.Cells(4).Text
           	Dim venuetdate As String = item.Cells(5).text
           	Dim adstage As String = item.Cells(10).Text
           	Dim adno As String = item.Cells(8).Text
           	
           
            pnlLSdetail.visible = true
            pnlPMain.visible=false
            pnlDG2.visible=false
            pstsvenue.text=venuename
            pststatus.text=venuestat
            pstEPdate.text=venuetdate
            pstsadno.text=adno
            pstsadstage.text=adstage
            pstadfrom.text=""
            pstadto.text=""
            session("venuepk") = advenuePK
            Bindvenupdatefield(advenuePK)
            if venuestat = "Published" then
            	pstadfrom.enabled=false  
            	BTNSetPub.text="Save"
            	BTNPublater.visible=false
            	lnkEPubDate.enabled=false
            	          	
           	else
           		
            	pstadfrom.enabled=true 
            	BTNPublater.visible=true
            	lnkEPubDate.enabled=true
            	BTNSetPub.text="Save as Published"
           end if
        			bindADVenuesPP("All")
        			bindADVenuesPPDate("All")
        end sub
        
        Sub MarkPublished(ByVal Source As System.Object, ByVal e As System.EventArgs)
        	 
        	 	if pstadfrom.text="" then
	        	   	pstadfrom.text="Required!"
	        	   	pstadfrom.backcolor=red
	       	else
	       		updatevenue(session("venuepk"),"Published",pstadto.text,pstadfrom.text)
		         pnlLSdetail.visible=false
		        	pnlDG2.visible=true
		         pnlPMain.visible=true
		   
	           
		         if pstsadstage.text <> "Finalized" then
		         	dofinalize()
			     	end if
			      bindADVenuesPP("All")
	       		bindADVenuesPPDate("All")
	       		pstadfrom.text=""
	        	   	pstadfrom.backcolor=white
	       	end if
        end sub
        
         public sub dofinalize()
         		'Session("ADStage") = "Finalized"
	            updatead()
	            
	      end sub
        
          Sub PublishLAter(ByVal Source As System.Object, ByVal e As System.EventArgs)
          	updatevenue(session("venuepk"),"Unpublished",pstadto.text,pstadfrom.text)
	         pnlLSdetail.visible=false
	        	pnlDG2.visible=true
	         pnlPMain.visible=true
	         bindADVenuesPP("All")
        		bindADVenuesPPDate("All")
        end sub
        
        sub UpdatePostingSave()
        
        end sub
        
        Sub exitout(ByVal Source As System.Object, ByVal e As System.EventArgs)
        		session("keepadmfiltersA")="false"
        		if request.querystring("source")="planedit" or request.querystring("source")="newpostpubP"  then
        				response.redirect("createad.aspx?action=edit&source=wrkpsts&adno=" & session("adno"))
        		elseif request.querystring("source")="newpostpub"  or request.querystring("source")="newpostpubR" 
        			response.redirect("createad.aspx?action=edit&adno=" & session("adno"))
        		
        		elseif  request.querystring("source")="admanager" then
        			response.redirect("admanager.aspx")
        		else
        			response.redirect("admanager.aspx")
        		end if
        
        
        
        end sub
        
         sub updatevenue(id as string, stat as string, pno as string, pubdate as string)
        		Dim strUID As String = Session("userid")
            Dim strSql As String 
            if stat="Unpublished" then
             	strSql = "update tbl_LeadADVenues set av_adplaced='" & stat & "', " _ 
	            			& "av_Postingno='" & Pno & "' " _         
	            			& "where tbl_leadadvenues = '" & id & "'"
            else
	            strSql = "update tbl_LeadADVenues set av_adplaced='" & stat & "', " _ 
	            			& "av_Postingno='" & Pno & "', av_APFrom='" & Pubdate & "' " _         
	            			& "where tbl_leadadvenues = '" & id & "'"
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
        
         sub updatead()
        		Dim strUID As String = Session("userid")
            Dim strSql As String 
            strSql = "update tbl_LeadADs set ad_stage='Finalized' " _ 
            			& "where tbl_leadad_pk = '" & pstsadno.text & "'"
          
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
        
         sub bindvenupdatefield(id as string)
	         Dim strUID As String = Session("userid")
	            Dim strSql As String 
	            strSql = "select * from tbl_LeadADVenues where tbl_leadadvenues='" & id & "'"
	          
	            Dim sqlCmd As SqlCommand
	            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
	            sqlCmd = New SqlCommand(strSql, myConnection)
	
	            Try
	                myConnection.Open()
	                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
	                If Sqldr.Read() Then
	                	If Sqldr("av_Postingno") IsNot DBNull.Value Then
	                        pstadto.Text = Sqldr("av_Postingno")
	                    End If
	                   	If Sqldr("av_APFrom") IsNot DBNull.Value Then
	                        pstadfrom.Text = Sqldr("av_APFrom")
	                    End If
	                
	                End If
	            Catch SQLexc As SqlException
	                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
	            Finally
	                myConnection.Close()
	            End Try
	        
         
         
         
         end sub
        
        
         Sub ItemDataBoundEventHandlerAVens(ByVal sender As Object, ByVal e As DataGridItemEventArgs)

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
   
   				Dim itemCellKey As TableCell = e.Item.Cells(0)
   				Dim itemCellVenName As TableCell = e.Item.Cells(1)
   				Dim itemCellStat As TableCell = e.Item.Cells(4)
   				Dim itemCellKeytext As String = itemCellKey.Text
          		Dim itemCellVenNametext As String = itemCellVenName.Text
          		Dim itemCellStattext As String = itemCellStat.Text
               
               Dim DGBTNcancel,DGBTNUpdate,DGBTNSetPub as button               
             	DGBTNcancel = e.Item.Cells(0).FindControl("BTNcancel")
             	DGBTNSetPub = e.Item.Cells(0).FindControl("BTNSetPub")
             	
             	if itemCellStattext ="Canceled" then
             		DGBTNcancel.text="Reactivate Date"
             	else
             		DGBTNcancel.text="Cancel Date"
             	end if
             	
             	if itemCellStattext ="Published" then
             		DGBTNSetPub.enabled=false
             		DGBTNcancel.enabled=false
             	else
             		DGBTNSetPub.enabled=true
             		DGBTNcancel.enabled=true
             	end if
             
					
					 
     			End If

        End Sub
        
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

    End Class
end namespace