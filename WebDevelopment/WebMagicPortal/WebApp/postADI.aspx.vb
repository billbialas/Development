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
Imports System.Reflection


namespace PageTemplate
    Public Class postADI
        Inherits PageTemplate
        
        public DD_existplan as dropdownlist
        public lb_selvenues as listbox
        public pnlNewpost,pnlpostdue,pnlPublishAds,pnlselpostdays,pnlvnotes,pnlvints as panel
		  public ADVenuesPP,dgvinst,dgvnotes,VenueNotes as datagrid
		  public Calendar1,cdrCalendar3
		  Protected datesArray, VenueArray,VenueArrayKey As ArrayList
		  public vcal as linkbutton
		  public chkassociatepaln as checkbox
		  public lblvnotes,lblvinst,lblnovensel as label
		  public btnSPExit as button
		  
        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles mybase.load
				If ViewState("selectedDates") Is Nothing Then
					 datesArray = new ArrayList()
				Else
					datesArray = CType( Viewstate("selectedDates"), ArrayList )
				End If
            If Not (Page.IsPostBack) Then
            	clearsessions()
					chkassociatepaln.checked=false
					DD_existplan.visible=false
					FillAdPlans()
					FillVenues()
					if request.querystring("source")="newplanposts" then
						pnlNewpost.visible=false
						pnlPublishAds.visible=true
						bindADVenuesPP(session("adplanno"))
					end if
					if request.querystring("source")="admgrpost" then
						if chkifunpubpsts() then
							pnlpostdue.visible=true
							pnlNewpost.visible=false
						else
							pnlNewpost.visible=true
							pnlPublishAds.visible=false
							
						end if
					end if
					if request.querystring("source")="admgrpostOnce" then
							vcal.visible=false
							pnlselpostdays.visible=false
							pnlNewpost.visible=true
							pnlPublishAds.visible=false
							Calendar1.selecteddate=datetime.today
							lb_selvenues.selectionmode=listselectionmode.single 
							btnSPExit.visible=false
							chkassociatepaln.visible=false
							
				
					end if
					if request.querystring("source")="adplanpost" then
						if chkifunpubpsts() then
							pnlpostdue.visible=true
							pnlNewpost.visible=false
						else
							pnlNewpost.visible=true
							pnlPublishAds.visible=false
							
						end if
					end if
					session("keepadmfilters")="true"
					
            End If
            pagesetup()

        End Sub
        sub clearsessions()
        	session("strVOnline")=""
        		session("strVenKey")=""
        		session("SvenPK")=0
        		session("selectedv")=""
       
        
        end sub
        
        	
         sub toggleplan(ByVal Source As System.Object, ByVal e As System.EventArgs)
        		if chkassociatepaln.checked then
        			DD_existplan.enabled=true
        			DD_existplan.visible=true
        		else
        			DD_existplan.enabled=false
        			DD_existplan.visible=false
        			DD_existplan.SelectedIndex = DD_existplan.Items.IndexOf(DD_existplan.Items.FindBytext("System"))
        		end if
        		
        end sub
        
        
        sub Addnewpost(ByVal Source As System.Object, ByVal e As System.EventArgs)
        		pnlpostdue.visible=false
        		pnlNewpost.visible=true
        		Calendar1.selecteddate=datetime.today
        		
        end sub
        
         sub showinst(ByVal Source As System.Object, ByVal e As System.EventArgs)
        		lblvnotes.text=""
        			lblvinst.text=""
        		session("vinfoview")="inst"
        		if checkvenselected() then
	        		pnlselpostdays.visible=false
	        		pnlvints.visible=true
	        		pnlvnotes.visible=false
	        		bindvinst(session("selectedv"))
	        	else
       				lblnovensel.visible=true
       		end if
        end sub
        
        sub showCalendar(ByVal Source As System.Object, ByVal e As System.EventArgs)
        		pnlvnotes.visible=false
        		pnlselpostdays.visible=true
        		lblnovensel.visible=false
        end sub
         
          sub shownotes(ByVal Source As System.Object, ByVal e As System.EventArgs)
          	
        			if checkvenselected() then
	        			pnlvnotes.visible=true
	          		pnlselpostdays.visible=false
	        			Dim i As Integer   
						dim x as integer
						dim strnotepks as string = ""
						dim strnotepksA as string = ""
						x=lb_selvenues.items.count-1
	        			session("vinfoview")="notes"
	        			for i=0 to x
							if (lb_selvenues.Items(i).Selected) then
								if i=0 and i=x then
									strnotepks = Convert.ToString(lb_selvenues.items(i).value)
								elseif i=0 and i<x then
									strnotepks = Convert.ToString(lb_selvenues.items(i).value) + ","
								
								elseif i < x then								
									strnotepks=strnotepks + Convert.ToString(lb_selvenues.items(i).value) + ","
	        				
	        					end if
	        				end if
	        			next
	        			strnotepksA = left(strnotepks,len(strnotepks)-1)
	        			'response.write(strnotepksA)
	        			bindvnotes(strnotepksA)
	        			lblnovensel.visible=false
	        		else
	        			lblnovensel.visible=true
       			end if
       			
        end sub
        
        
           sub gobacktocal(ByVal Source As System.Object, ByVal e As System.EventArgs)
        			pnlselpostdays.visible=true
        			pnlvnotes.visible=false
        			pnlvints.visible=false
        			lblnovensel.visible=false
        		
        end sub
        public function checkvenselected() as boolean
        			dim stat as string = "false"
        			dim i as integer
        			dim x as integer				
					x=lb_selvenues.items.count-1				
        			for i=0 to x
						if (lb_selvenues.Items(i).Selected) then
							stat="true"
							session("selectedv") = Convert.ToString(lb_selvenues.items(i).value)
						end if
						
					next
					if stat="true" then
						return true
					else
						return false
					end if
        
        end function
        
        
        sub bindvnotes(id as string)
              Dim strUID As String = Session("userid")
            
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
           
            
		      mycommand = "select * from tbl_xwalk where tbl_xwalk_pk in ( " & id & ")"
		    
            
            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_xwalk ")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_xwalk "))
					 
                VenueNotes.DataSource = dvProducts
                VenueNotes.DataBind()
					
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
	      
        
        end sub
        
         sub bindvinst(id as string)
              Dim strUID As String = Session("userid")
            Dim strSql As String = "SELECT * from tbl_xwalk where tbl_xwalk_pk ='" & id & "'"

            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then

        		   		If Sqldr("x_instructions") IsNot DBNull.Value Then
                        lblvinst.Text = Sqldr("x_instructions")
                    End If
                 End If
	            Catch SQLexc As SqlException
	                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
	            Finally
	                myConnection.Close()
	            End Try
	    
        
        end sub
        
        
        
        
        
        
          sub Addexstpost(ByVal Source As System.Object, ByVal e As System.EventArgs)
          	session("keepadmfiltersA")="false"
          	session.remove("PubSearchFV")
            	session.remove("PubStatFV")
            	session.remove("PubADSFV")
            	session.remove("PubADPlanFV")
            	session.remove("PubTargetDateFV")
            	session.remove("PubADVenueFV")
        		response.redirect("adpostings.aspx?source=newpostpubR")
        		
        end sub
        
        sub AddnewpostC(ByVal Source As System.Object, ByVal e As System.EventArgs)
          	session("keepadmfiltersA")="false"
          	session.remove("PubSearchFV")
            	session.remove("PubStatFV")
            	session.remove("PubADSFV")
            	session.remove("PubADPlanFV")
            	session.remove("PubTargetDateFV")
            	session.remove("PubADVenueFV")
        		response.redirect("admanager.aspx")
        		
        end sub
        
        
        
        
        
         public function  chkifunpubpsts() as boolean
        		'response.write(session("adno"))
        		Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = 	"select case when(av_adplaced='Unpublished') then 'true'  end as 'postnotpub' " _
												& "from dbo.tbl_LeadADVenues " _
												& "join dbo.tbl_LeadADPlanVenues on av_lapVenFK = 	LV_tbl_pk " _
												& "join dbo.tbl_leadADPlans on LAP_tbl_pk=lv_adplan_fk " _	 		
												& "where av_leadads_FK = '" & session("adno") & "' " _
												& "and (lap_status='Active' and lv_status='Active') " _
												& "and case when(av_adplaced='Unpublished') then 'true'  end is not null  " _
												& "group by case when(av_adplaced='Unpublished') then 'true'  end  " 
	         Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                	return true
                	if sqldr("postnotpub")="true" then
                		return true
                	else
                		return false
                	end if
                else
                	return false
                end if
                  
            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
        
        
        
        end function
        
        Sub FillAdPlans()
				 Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String 
           	myCommand= "Select *,cast(LAP_tbl_pk as varchar(255)) as 'lplanno', cast(LAP_tbl_pk as varchar(255)) + ' ' + lap_name as 'Ftitle' from dbo.tbl_leadADPlans where lap_adfk='" & session("adno")  & "' and  lap_useridfk='" & Session("userid") & "' and lap_status='Active'"
           
            
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                DD_existplan.DataSource = dataReader
                DD_existplan.DataTextField = "Ftitle"
                DD_existplan.DataValueField = "lplanno"
                DD_existplan.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
				DD_existplan.SelectedIndex = DD_existplan.Items.IndexOf(DD_existplan.Items.FindBytext("System"))
				
				
				
    					

        End Sub
        
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
		                & "where av_lapfk ='" & id & "'"
		      
		      end if
            	
            
            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_LeadADVenues")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_LeadADVenues"))
					 
                ADVenuesPP.DataSource = dvProducts
                ADVenuesPP.DataBind()
					
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
            
            
        End Sub

        

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

 
 
 
 Sub Button1_Click(sender As Object, e As EventArgs)
 		session("keepadmfiltersA")="false"
 		session.remove("PubSearchFV")
            	session.remove("PubStatFV")
            	session.remove("PubADSFV")
            	session.remove("PubADPlanFV")
            	session.remove("PubTargetDateFV")
            	session.remove("PubADVenueFV")
			dim bt as button = sender
			Dim i As Integer   
			dim x as integer
			Dim dt As DateTime
			dim singleven as string
			x=lb_selvenues.items.count-1			
			if bt.id = "btnSPExit" or   bt.id = "btnSPPost" then
				if checkvenselected() then
				
						For each dt in Calendar1.SelectedDates
							for i=0 to x
								if (lb_selvenues.Items(i).Selected) then
									if checkvenexits(Convert.ToString(lb_selvenues.items(i))) then
																			
										getvenueinfo(Convert.ToString(lb_selvenues.items(i).text))
										'session("strVenKey") = session("strVenKey") + getadkey()										
										
									else
										getvenueinfoNEW(Convert.ToString(lb_selvenues.items(i).value))							
										session("strVenKey") = session("strVenKey") + getadkey()
										DoSaveVenuesNew(session("strVenKey"),Convert.ToString(lb_selvenues.items(i)),	session("strVOnline"))
										getNewVenuePK()
									end if	
									insertadVNEW(Convert.ToString(lb_selvenues.items(i)), session("strVenKey"),  dt, session("SvenPK"))
									singleven = Convert.ToString(lb_selvenues.items(i))
									if session("strlvstat")="Inactive" then
										updatevenstatus(session("SvenPK"))
									end if
			 					end if
							Next
						next
						incADKEY()
						
						if bt.id = "btnSPExit" then
							if request.querystring("source")="adplanpost"
								response.redirect("createad.aspx?action=edit&adno=" & session("adno"))
							else
								response.redirect("admanager.aspx")
							end if
						elseif bt.id = "btnSPPost" then
						
							if request.querystring("source")="admgrpostOnce" then
									
									getnewPDPK()
									session("Padno") = session("adno")
				            	session("Pvenue") = singleven
				            	session("Pvenueno") = session("SNEWPDPK")
				            	session("Ptype") = "Complete"
				            	session("Oposter") = "admgrpostOnce"
				            	if session("adstage")="Finalized" then
				            		session("Pmodify") = "No"
				            	else
				            		session("Pmodify") = "Yes"
				            	end if
				            	session("Ppplan") = DD_existplan.selecteditem.value
				        			if not checkforadkey(session("adno")) then
				            		updateadtextwkey(session("adno"))            		
				        			end if
				        			Response.Redirect("postad.aspx")
							else
								session("cplanid")=DD_existplan.selecteditem.value
								response.redirect("adpostings.aspx?source=newpostpub")
							end if	
						end if
					else
						lblnovensel.visible=true
				
				end if
			else
				if request.querystring("source")="adplanpost"
					response.redirect("createad.aspx?action=edit&adno=" & session("adno"))
				else
					response.redirect("admanager.aspx")
				end if
			end if
			
			
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
			
			
	
	public function getsyspplanid() as integer
			Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select LAP_tbl_pk from dbo.tbl_leadADPlans where lap_adfk='" & session("adno") & "' and  lap_useridfk='" & session("userid") & "'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    return  sqldr("LAP_tbl_pk")
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
        
	
	end function
	
	 Public sub getnewPDPK()
        		Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select cast (max(tbl_leadadvenues) as int) as 'MaxPK' from dbo.tbl_LeadADVenues join tbl_LeadADs on tbl_leadad_pk=av_leadads_FK where ad_userid='" & session("userid") & "'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    session("SNEWPDPK") = sqldr("MaxPK")
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
        
        end sub
	
	
	
	 Public sub getNewVenuePK() 
        		Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select cast (max(LV_tbl_pk) as int) as 'MaxPK' from dbo.tbl_LeadADPlanVenues where lv_userid_fk='" & session("userid") & "'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    session("SvenPK") = sqldr("MaxPK")
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
        
        end sub
	
	public sub updatevenstatus(id as string)
			   Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "update dbo.tbl_LeadADPlanVenues set lv_status = 'Active' where LV_tbl_pk='" & id & "'"
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
	
	
		sub DoSaveVenuesNew(key as string, svalue as string,svenonline as string)
			
				Dim strUID As String = Session("userid")
            Dim strSql As String = "insert into tbl_LeadADPlanVenues (lv_ad_fk,lv_adplan_fk,lv_userid_fk,lv_name,lv_status,lv_key, " _
            								& "lv_sunday,lv_monday,lv_tuesday,lv_wednesday,lv_thursday,lv_firday,lv_saturday, lv_count, lv_online, lk_keyurl) " _
            								& " values ('" & session("adno") & "','" & DD_existplan.selecteditem.value  & "','" & Session("userid") & "','" & svalue & "', " _
            								& "'Inactive','" & key & "', 'false','false','false','false','false','false','false',0,'" & svenonline & "', " _
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
			
			
	   Public Sub getvenueinfoNEW(src as string)

            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            
            Dim strSql As String 
            
            		strSql= "select * from tbl_xwalk where tbl_xwalk_pk='" & src  & "'"
            
          
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                     If Sqldr("x_online") IsNot DBNull.Value Then
                        	session("strVOnline") = Sqldr("x_online")
                    Else
                        	session("strVOnline") = "No"
                    End If
                    If Sqldr("x_id") IsNot DBNull.Value Then
                        session("strVenKey") = Sqldr("x_id")
                    
                    End If
                   

                End If


            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try


        End Sub
        
	
	
	 Public Sub getvenueinfo(src as string)
				
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            
            Dim strSql As String 
           
          	strSql= "select * from tbl_LeadADPlanVenues where lv_name='" & src  & "' and " _
            							& "lv_userid_fk='" & session("userid") & "' and lv_adplan_fk='" & DD_existplan.selecteditem.value & "' and lv_ad_fk='" & session("adno") & "' "
            
       
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    If Sqldr("LV_tbl_pk") IsNot DBNull.Value Then
                        session("SvenPK") = Sqldr("LV_tbl_pk")
                    End If
                    If Sqldr("lv_key") IsNot DBNull.Value Then
                        session("strVenKey") = Sqldr("lv_key")
                    
                    End If
                    If Sqldr("lv_status") IsNot DBNull.Value Then
                        session("strlvstat") = Sqldr("lv_status")
                    
                    End If
                   

                End If


            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
				
				

        End Sub
        
        
        
        
        
        
	
	 public function checkvenexits(name as string) as boolean
         	
         	Dim strUID As String = Session("userid")
            Dim strSql As String = "select lv_name,* from tbl_LeadADPlanVenues where lv_name = '" &  name & "' and " _
            							& "lv_userid_fk='" & session("userid") & "' and lv_adplan_fk='" & DD_existplan.selecteditem.value & "' and lv_ad_fk='" & session("adno") & "'"
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
	
	
	Sub insertadVNEW(Sadname as string, Sadcode as string, STdate as datetime, SVenPK as integer )
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
            prmppid.Value = DD_existplan.selecteditem.value
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
            prmVenuePK.Value = SVenPK
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