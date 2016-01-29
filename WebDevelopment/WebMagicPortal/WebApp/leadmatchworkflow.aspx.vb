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
imports DataGridTemplate
imports leadmatchworkflow


namespace PageTemplate
	public class leadmatchworkflow 
	   inherits PageTemplate
	   
	   public t_lname,t_fname,t_hphone,t_cphone,t_email,t_email2
	   public l_lname,l_fname,l_hphone,l_cphone,l_email,l_email2
	   public dd_tenstatus,dd_Lanstatus,dd_wfstep	
	   public l_save
	   public lbladdress,lblcity,lblzip,lblbedrooms,lblbaths,lblrentmin,lblrentmax,testAAA,bb,xxx,lblresult
	   private shared tprofileno as string
	   public pnltenantinfo,pnlallinfo,pnlworkflow,pnlworkflowreorder,pnlworkflowadd
	   public btn_step7
	   public lstcities,lstworkflow as listbox
	   public DGworkflow
	   public ds as new DataSet()
	   
	   private Sub Page_Init(byval sender as object, byval e As EventArgs) handles mybase.load
	   			dim  xxx as  new label()
	   			xxx.text=session("tempbill")
	   			xxx.id="bb"
	   		Controls.Add(xxx)
	   	Dim a As String = Request.Form("__EVENTTARGET") 
	   	'response.write(a)
	   	If Not String.IsNullOrEmpty(a) then 
				if a.Substring(0, 1) = "w" then 
					response.write(a)
				else
					if a.Substring(6,7)="wlogout" then 
						'response.write(a.Substring(6,7))
					else
						if a.Substring(17, 2) = "dd" then 
							'response.write(a.Substring(17, 2))
						else if a.Substring(29, 7) = "tenstat" then
							'response.write(a.Substring(29, 7))
								bindworkflow()
						else
							bindworkflow()
						end if
					end if
				end if
				'if a.Substring(0, 2) = "DG" then 
				'	bindworkflow()
				'end if
			else
				bindworkflow()
			end if
			
'			if(ViewState("restore")<> nothing) then
'				if(ViewState("restore").ToString()="true") then
'			
'					bindworkflow()
'			  	end if
'			end if
			'dim postbackControlName as string  = page.Request.Params.Get("__EVENTTARGET")
			'dim  postbackControlInstance as new Control()
	   	'	postbackControlInstance = page.FindControl(postbackControlName)
	   end sub
	   
	   private Sub Page_prerender(byval sender as object, byval e As EventArgs) handles mybase.load
	   	Dim a As String = Request.Form("__EVENTTARGET") 
	   		'response.write(a)
	   		If Not String.IsNullOrEmpty(a) then 
					if a.Substring(0, 1) = "w" then 
						'response.write(a)
					else
						if a.Substring(6,7)="wlogout" then 
							'response.write(a.Substring(6,7))
						else
							if a.Substring(17, 2) = "dd" then 
								'response.write(a.Substring(17, 2))
							else if a.Substring(29, 7) = "tenstat" then
								'response.write(a.Substring(29, 7))
								'bindworkflow()
								'	if session("BBtest")="set" then
								'		session("BBtest")=""
								'		response.write("<script language=javascript1.2>window.location.reload(true);</script>")
								'	end if
							else
								'bindworkflow()
							end if
						end if
					end if
				end if
	   end sub

	  	private Sub Page_Load(byval sender as object, byval e As EventArgs) handles mybase.load
			'response.write("<script language=javascript1.2>window.location.reload(true);</script>")
			Pagesetup()
		
			if  not (Page.IsPostBack) then
				'lblresult.text = "sdasdasd"
				pnlallinfo.visible=false
				pnltenantinfo.visible=true
				pnlworkflowreorder.visible=false
				pnlworkflowadd.visible=false
				settenantfields(false)
				bindwfreorder()
				bindworkflow()
				bindfields()
			else
				Dim a As String = Request.Form("__EVENTTARGET") 
	   		'response.write(a)
	   		If Not String.IsNullOrEmpty(a) then 
					if a.Substring(0, 1) = "w" then 
						'response.write(a)
					else
						if a.Substring(6,7)="wlogout" then 
							'response.write(a.Substring(6,7))
						else
							if a.Substring(17, 2) = "dd" then 
								'response.write(a.Substring(17, 2))
							else if a.Substring(29, 7) = "tenstat" then
								'response.write(a.Substring(29, 7))
								'bindworkflow()
									'if session("BBtest")="set" then
									'	session("BBtest")=""
										'response.write("<script language=javascript1.2>window.location.reload(true);</script>")
									'end if
							else
								'bindworkflow()
							end if
						end if
					end if
				end if
	 		end if
	 	end sub
	
		sub settenantfields(value as boolean)
			dim x as boolean = value
			t_lname.enabled=x
			t_fname.enabled=x
			t_hphone.enabled=x
			t_cphone.enabled=x
			t_email.enabled=x
			t_email2.enabled=x
			l_lname.enabled=x
			l_fname.enabled=x
			l_hphone.enabled=x
			l_cphone.enabled=x
			l_email.enabled=x
			l_email2.enabled=x
			dd_tenstatus.enabled=x
			dd_Lanstatus.enabled=x 
			l_save.visible=false
		end sub
		
		sub setlanfields(value as boolean)
			dim x as boolean = value
			l_lname.enabled=x
			l_fname.enabled=x
			l_hphone.enabled=x
			l_cphone.enabled=x
			l_email.enabled=x
			l_email2.enabled=x
		end sub
	
		Sub binddata()
			Dim strUID as String = session("userid")
			Dim strSql as String = "select * from choiceonedev.dbo.tbl_propmatch match " _
					& "join tbl_leads lan on lan.tbl_leads_pk = pm_landlordleadno " _
					& "join tbl_leads ten on ten.tbl_leads_pk = pm_tenantleadno " _
					& "join tbl_lead_profile lanp on lanp.tbl_lead_profile_pk= pm_landlordprofileno " _ 
					& "where pm_tenantleadno = " & Request.QueryString("profile")   
   		Dim sqlCmd As SqlCommand
 		
	      Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
  			sqlCmd = New SqlCommand(strSql, myConnection)
        	
         Try
         	myConnection.Open()
           	Dim Sqldr as SqlDataReader = sqlCmd.ExecuteReader
      			if Sqldr.read() then
      					
         		end if
   				Catch SQLexc As SqlException
                    Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            	finally
            		myConnection.close()
            End Try
		   End Sub
		
		Sub bindfields()
 					Dim strUID as String = session("userid")
  					Dim strSql as String ="select ten.ld_lname as 'tlname',ten.ld_fname as 'tfname',ten.ld_hphone as 'thphone',ten.ld_cphone as 'tcphone', ten.ld_email as 'temail',ten.ld_email2 as 'temail2', " _
						& "lan.ld_lname as 'llname',lan.ld_fname as 'lfname',lan.ld_hphone as 'lhphone',lan.ld_cphone as 'lcphone', lan.ld_email as 'lemail',lan.ld_email2 as 'lemail2', " _
						& "pm_tenstatus,pm_lanstatus,pw_step1_status,pw_step2_status,pw_step3_status,pw_step4_status, " _
						& "lp_address,lp_city,lp_state,lp_zip,lp_rent_amt_min,lp_rent_amt_max,lp_school1,pw_tenantprofile_fk, " _
						& "lp_num_bed, lp_num_bath " _
						& "from tbl_propmatch " _
						& "join tbl_leads ten on ten.tbl_leads_pk = pm_tenantleadno " _
						& "join tbl_leads lan on lan.tbl_leads_pk = pm_landlordleadno " _
						& "join tbl_propmatchworkflow on pw_propmatch_fk = tbl_propmatch_pk " _
						& "join tbl_lead_profile  on tbl_lead_profile_pk= pm_landlordprofileno " _
						& "where tbl_propmatch_pk = " & Request.QueryString("profile") 
  					
  					Dim sqlCmd As SqlCommand
	 		
			      Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
        			sqlCmd = New SqlCommand(strSql, myConnection)
              	
               Try
               	myConnection.Open()
                 	Dim Sqldr as SqlDataReader = sqlCmd.ExecuteReader
         				if Sqldr.read() then
         					tprofileno= Sqldr("pw_tenantprofile_fk")
         					if Sqldr("tlname") IsNot dbnull.value then
            				 	   t_lname.Text = Sqldr("tlname")
            				end if
								if Sqldr("tfname") IsNot dbnull.value then
            				 	   t_fname.Text = Sqldr("tfname")
            				end if
								if Sqldr("thphone") IsNot dbnull.value then
            				 	   t_hphone.Text = Sqldr("thphone")
            				end if
								if Sqldr("tcphone") IsNot dbnull.value then
            				 	   t_cphone.Text = Sqldr("tcphone")
            				end if
								if Sqldr("temail") IsNot dbnull.value then
            				 	   t_email.Text = Sqldr("temail")
            				end if
								if Sqldr("temail2") IsNot dbnull.value then
            				 	   t_email2.Text = Sqldr("temail2")
            				end if
      						if Sqldr("llname") IsNot dbnull.value then
            				 	   l_lname.Text = Sqldr("llname")
            				end if
								if Sqldr("lfname") IsNot dbnull.value then
            				 	   l_fname.Text = Sqldr("lfname")
            				end if
								if Sqldr("lhphone") IsNot dbnull.value then
            				 	   l_hphone.Text = Sqldr("lhphone")
            				end if
								if Sqldr("lcphone") IsNot dbnull.value then
            				 	   l_cphone.Text = Sqldr("lcphone")
            				end if
								if Sqldr("lemail") IsNot dbnull.value then
            				 	   l_email.Text = Sqldr("lemail")
            				end if
								if Sqldr("lemail2") IsNot dbnull.value then
            				 	   l_email2.Text = Sqldr("lemail2")
            				end if
              				if Sqldr("pm_tenstatus") IsNot DBNull.Value  then
         						dd_tenstatus.SelectedIndex = dd_tenstatus.Items.IndexOf(dd_tenstatus.Items.FindByText(Sqldr("pm_tenstatus")))
   	       				end if
								if Sqldr("pm_lanstatus") IsNot DBNull.Value  then
         						dd_Lanstatus.SelectedIndex = dd_Lanstatus.Items.IndexOf(dd_Lanstatus.Items.FindByText(Sqldr("pm_lanstatus")))
   	       				end if
   	       				
            				if Sqldr("lp_address") IsNot dbnull.value then
            				 	  lbladdress.text = Sqldr("lp_address")
            				end if	
            				if Sqldr("lp_city") IsNot dbnull.value then
            				 	  lblcity.text = Sqldr("lp_city")
            				end if	
            				if Sqldr("lp_zip") IsNot dbnull.value then
            				 	  lblzip.text = Sqldr("lp_zip")
            				end if	
            				if Sqldr("lp_num_bed") IsNot dbnull.value then
            				 	  lblbedrooms.text = Sqldr("lp_num_bed")
            				end if
            				if Sqldr("lp_num_bath") IsNot dbnull.value then
            				 	  lblbaths.text = Sqldr("lp_num_bath")
            				end if
            				if Sqldr("lp_rent_amt_min") IsNot dbnull.value then
            				 	  lblrentmin.text = Sqldr("lp_rent_amt_min")
            				end if
            				if Sqldr("lp_rent_amt_max") IsNot dbnull.value then
            				 	  lblrentmax.text = Sqldr("lp_rent_amt_max")
            				end if
            				
   	       				
							end if
   				Catch SQLexc As SqlException
                    Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            	finally
            		myConnection.close()
            End Try
		
		end sub

		sub DualList1_ItemsMoved(sender As Object, e As EventArgs)
			MoveListboxItem(lstworkflow, -1, lstworkflow.SelectedIndex - 1)

			'Sub MoveSelectedItemUp(ByVal Box As ListBox)
        'Dim Index As Integer = Box.SelectedIndex    'Index of selected item
        'Dim Swap As Object = Box.SelectedItem       'Selected Item
        'If Not (Swap Is Nothing) Then               'If something is selected...
        '  Box.Items.RemoveAt(Index)                   'Remove it
        '    Box.Items.Insert(Index - 1, Swap)           'Add it back in one spot up
        '    'Box.SelectedItem = Swap                     'Keep this item selected
       ' End If
       dim i as integer 
       for i=0 to lstworkflow.items.count-1
       	response.write (lstworkflow.items(i))
       next
    
		end sub
		
		Sub MoveListboxItem(ByVal ctl As ListBox, ByVal fromIndex As Integer, _
		    ByVal toIndex As Integer)
		    If fromIndex = toIndex Then Exit Sub
		    ' provide a default
		    If fromIndex < 0 Then fromIndex = ctl.SelectedIndex
		    ' exit if argument not in range
		    If toIndex < 0 Or toIndex > ctl.Items.Count - 1 Then Exit Sub
		
		    With ctl
		        ' save the data of the current item
		        Dim data As Object = .Items(fromIndex)
		        ' remove the item
		        .Items.RemoveAt(fromIndex)
		        ' add the item
		        .Items.Insert(toIndex, data)
		        ' select the new item
		        .SelectedIndex = toIndex
		    End With
		End Sub
		
		sub viewtprofile(sender As Object, e As EventArgs)
			dim s as string
			s= "<script>window.open("
			s=s+ "'leadprofile.aspx?profile=" + tprofileno + "&LeadNo=" + Request.QueryString("Tenleadnum")
			s=s+ "&LeadType=Tenant&action=print'"
			s=s+",'_blank');</script>"
			
			Response.Write(s) 

		end sub
		
		sub btn_hideinfo(sender As Object, e As EventArgs)
			if btn_step7.text = "Hide" then
				btn_step7.text="Show"
				pnlallinfo.visible=false
			else 
				btn_step7.text="Hide"
				pnlallinfo.visible=true
			end if
		end sub
	
		sub tedita(sender As Object, e As EventArgs)
			pnltenantinfo.visible=true
			settenantfields(true)
			l_save.visible=true
		end sub

		sub ledita(sender As Object, e As EventArgs)
			setlanfields(true)
			l_save.visible=true
		end sub

		sub reorderworkflow(sender As Object, e As EventArgs)
			pnlworkflow.visible=false
			pnlworkflowreorder.visible=true
		end sub

		sub addworkflow(sender As Object, e As EventArgs)
			pnlworkflow.visible=false
			pnlworkflowreorder.visible=false
			pnlworkflowadd.visible=true
			bindmasterwfsteps()
		end sub

		sub savereorder(sender As Object, e As EventArgs)
			pnlworkflow.visible=true
			pnlworkflowreorder.visible=false
			pnlworkflowadd.visible=false
		end sub

		sub btn_save(sender As Object, e As EventArgs)
	
		end sub


		sub btn_printapp(sender As Object, e As EventArgs)
			printapp()
		end sub



 		sub pagesetup()
	
			'the page template code below represents only a few of the things that
			'you can do. Play around with it, and you'll see just how much power is
			'in your hands

			'width will be calculated automatically, but it is sometimes
			'important to specify height
			layout.width="1000"
			Body.Height = "400"
			Body.VAlign = "top"
			body.width = "1000"
			Body.VAlign = "top"
			'RightNav.VAlign = "top"
			
			Header.Controls.Add(LoadControl("headersys.ascx"))
	
			'''LeftNav.Controls.Add(LoadControl("navigation2.ascx"))
			''''LeftNav.VAlign = "top"
			'LeftNav.Controls.Add(new LiteralControl("Some text."))
			Layout.border = 0
			'adjust size of LeftNav (just for the heck of it)
			''''''	LeftNav.Width = "100"
			
			'RightNav contents are included here, but try commenting
			'out the code below, to see how the page template dynamically
			'modifies itself (same goes with the LeftNav)
			'RightNav.Controls.Add(LoadControl("quicklink1.ascx"))
			'MiddleNav.Controls.Add(LoadControl("navigation.ascx"))
			'MiddleNav.Controls.Add(LoadControl("userid.ascx"))
			'footer.controls.add(LoadControl("footer.ascx"))
	
		end sub
	
		sub printapp()
	   
			'  DECLARE ALL THE VARIABLES
			'***********************************************************************
			
			Dim FDFAcX         ' FDF Toolkit ActiveX Version Object
			Dim objFDF           ' FDF Object
			
			 FDFAcX = Server.CreateObject("FDFApp.FDFApp")
			
			 objFDF = FDFAcX.FDFCreate
			'objFDF.FDFSetFile ("http://bmsdev.gochoiceone.com/FormTestFDF.pdf")
			'objFDF.FDFSetFile ("http://bmsdev.gochoiceone.com/billapp.pdf")
			objFDF.FDFSetFile ("http://bmsdev.gochoiceone.com/tt.pdf")
			'objFDF.FDFSetValue("propaddress", lbladdress.text, False)
			objFDF.FDFSetValue("txtLeadName", "Bialas", False)
			'objFDF.FDFSetValue("txtLeadHPhone", strLeadHPhone, False)
			'objFDF.FDFSetStatus ("You must complete all sections of this form.")
			Response.ContentType = "application/vnd.fdf"
			Response.BinaryWrite(objFDF.FDFSaveToBuf)
			objFDF.FDFClose
			 objFDF = Nothing
			 FDFAcX = Nothing
			
		end sub
		
		sub bindwfreorder()
	  		Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
	       Dim myCommand As New SqlCommand("select pmws_step,pmws_seq from tbl_propmatchworkflowsteps " _
					& "join tbl_propmatchworkflow on tbl_propmatchworkflow_pk = pmws_pmw_fk " _
					& "where pw_propmatch_fk =" &  Request.QueryString("profile") , myConnection)
	           	
	        Try
	         	myConnection.Open()
	            lstworkflow.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
	            lstworkflow.DataBind()
	            Catch SQLexc As SqlException
	                 Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
	         End Try
 		end sub
	
	sub bindmasterwfsteps()
	  		Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
	       Dim myCommand As New SqlCommand("select pmwms_desc,tbl_pmwms_pk  from tbl_propmatchworkflowmastersteps", myConnection)
	           	
	        Try
	         	myConnection.Open()
	            dd_wfstep.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
	            dd_wfstep.DataBind()
	            Catch SQLexc As SqlException
	             Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
	         End Try
	         bindworkflow()
 		end sub
 		
		sub bindworkflow()
			Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
	      Dim myCommand As New SqlCommand("select * from tbl_propmatchworkflowsteps " _
					& "join tbl_propmatchworkflow on tbl_propmatchworkflow_pk = pmws_pmw_fk " _
					& "where pmws_wftype='Master' " _ 
					& "and pw_propmatch_fk =" &  Request.QueryString("profile") , myConnection)
	           	
	    dim strFAQSQL = "select pmws_wfmasterid,pmws_step,tbl_pmws_pk,pmws_TenStatus,pmws_LanStatus from tbl_propmatchworkflowsteps"
      	Dim myFAQCommand as New SqlCommand(strFAQSQL, myConnection)    
    		Dim myFAQDA as New SqlDataAdapter(myFAQCommand)
   		  
      	
	        Try
	         	myConnection.Open()
	         	myFAQDA.Fill(ds)
	         	ds.Tables(0).TableName = "bill"
	            DGworkflow.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
	            DGworkflow.DataBind()
	            Catch SQLexc As SqlException
	                 Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
	         End Try
	         
	         'DGworkflow.Columns(1).Visible=false

		end sub
	
		Sub ItemDataBoundEventHandler(sender as Object, e as DataGridItemEventArgs)
	   	If e.Item.ItemType = ListItemType.Item OR e.Item.ItemType = ListItemType.AlternatingItem then
   			'Check who steps are for
   			Dim itemCellwho As TableCell = e.Item.Cells(4)
   			Dim itemCellwhotext As String = itemCellwho.Text
   			if itemCellwhotext ="Ten" then
   				e.Item.Cells(8).enabled=false
   			else if itemCellwhotext ="Lan" then
   				e.Item.Cells(7).enabled=false
   			end if
   			'Check Status of Tenat in Master Step
   			Dim itemCellTenantStatus As TableCell = e.Item.Cells(5)
   			Dim itemCellTenantStatusDD As TableCell = e.Item.Cells(7)
   			Dim itemTenStatus As String = itemCellTenantStatus.Text
   		
				if itemTenStatus ="Completed" then 
	   			Dim list as DropDownList = e.Item.FindControl("dd_tenstatus")
	 			 	list.SelectedIndex = list.Items.IndexOf(list.Items.FindByText("Completed"))
	 			 
	   		else if itemTenStatus ="Not Started" then
	   			Dim list as DropDownList = e.Item.FindControl("dd_tenstatus")
	 			 	list.SelectedIndex = list.Items.IndexOf(list.Items.FindByText("Not Started"))
	   		else if itemTenStatus ="In Process" then
	   			Dim list as DropDownList = e.Item.FindControl("dd_tenstatus")
	 			 	list.SelectedIndex = list.Items.IndexOf(list.Items.FindByText("In Process"))
	   		else if itemTenStatus ="Canceled" then 
	   			Dim list as DropDownList = e.Item.FindControl("dd_tenstatus")
	 			 	list.SelectedIndex = list.Items.IndexOf(list.Items.FindByText("Canceled"))
	 			end if
	 		
	 			'Check Status of Lan in Master Step
   			Dim itemCellLANStatus As TableCell = e.Item.Cells(6)
   			Dim itemCellLANStatusDD As TableCell = e.Item.Cells(8)
   			Dim itemLANStatus As String = itemCellLANStatus.Text
   		
				if itemLANStatus ="Completed" then 
	   			Dim list as DropDownList = e.Item.FindControl("dd_lanstatus")
	 			 	list.SelectedIndex = list.Items.IndexOf(list.Items.FindByText("Completed"))
	 			 
	   		else if itemLANStatus ="Not Started" then
	   			Dim list as DropDownList = e.Item.FindControl("dd_lanstatus")
	 			 	list.SelectedIndex = list.Items.IndexOf(list.Items.FindByText("Not Started"))
	   		else if itemLANStatus ="In Process" then
	   			Dim list as DropDownList = e.Item.FindControl("dd_lanstatus")
	 			 	list.SelectedIndex = list.Items.IndexOf(list.Items.FindByText("In Process"))
	   		else if itemLANStatus ="Canceled" then 
	   			Dim list as DropDownList = e.Item.FindControl("dd_lanstatus")
	 			 	list.SelectedIndex = list.Items.IndexOf(list.Items.FindByText("Canceled"))
	 			end if
	   	
	   	
	   		
	   	'Lets build the sub step table
	   		 Dim  dg as New DataGrid()
	   		 dg.AutoGenerateColumns = false
	   		 dg.width=400
				 AddHandler dg.ItemDataBound, AddressOf dg_ItemDataBound

				 Dim masid as Integer = e.Item.DataItem("tbl_pmws_pk")
				 Dim tenstat as string = e.Item.DataItem("pmws_TenStatus")
	
	 			dim substep as DataView = ds.Tables("bill").DefaultView
	 			substep.RowFilter = "pmws_wfmasterid=" & masid
	
	         dg.DataSource = substep
	         
	         dim bc as new BoundColumn() 
       		bc.HeaderText = "Task"
       		bc.DataField = "pmws_step"
        		dg.Columns.Add(bc)
	         
	         dim bc1 as new BoundColumn() 
    			bc1.HeaderText = "Key"
    		 	bc1.DataField = "tbl_pmws_pk"
        		dg.Columns.Add(bc1)
        		
        		dim bc2 as new BoundColumn() 
    			bc2.HeaderText = "TenStat"
    		 	bc2.DataField = "pmws_TenStatus"
        		dg.Columns.Add(bc2)
        		
        		dim bc3 as new BoundColumn() 
    			bc3.HeaderText = "LanStat"
    		 	bc3.DataField = "pmws_LanStatus"
        		dg.Columns.Add(bc3)
        		
	        	
	         dim tc as new TemplateColumn()
	        	tc.HeaderTemplate = New DataGridTemplate(ListItemType.Header, "Tenant")
				tc.ItemTemplate = New DataGridTemplate(ListItemType.item,"tenstat")
				dg.Columns.Add(tc)
								
	         dim tc1 as new TemplateColumn()
				tc1.HeaderTemplate = New DataGridTemplate(ListItemType.Header, "Landlord")
				tc1.ItemTemplate = New DataGridTemplate(ListItemType.item,"lanstat")
				dg.Columns.Add(tc1)
				
				dg.DataBind() 
	    		e.Item.Cells(9).Controls.Add(dg)
	    	

	   	end if
	   	
		End Sub
		
		Sub dg_ItemDataBound(sender as Object, e as DataGridItemEventArgs)
			'response.write("YIPPIE")
				'Check Status of Tenat in Master Step
   			Dim itemCellTenantStatus As TableCell = e.Item.Cells(2)
   			Dim itemCellTenantStatuschk As TableCell = e.Item.Cells(4)
   			Dim itemTenStatus As String = itemCellTenantStatus.Text
   			
   			Dim itemCellLandlordStatus As TableCell = e.Item.Cells(3)
   			Dim itemCellLandlordStatuschk As TableCell = e.Item.Cells(5)
   			Dim itemlanStatus As String = itemCellLandlordStatus.Text
   			
   			if itemTenStatus ="Completed" then 
	   			dim chk as checkbox = e.Item.FindControl("tenstat")
	   		 	chk.checked = true
	   		'else 
	   		'	dim chk as checkbox = e.Item.FindControl("tenstat")
	   		' 	chk.checked = false
	   		end if
	   		
	   		if itemlanStatus ="Completed" then 
	   			dim chkLan as checkbox = e.Item.FindControl("lanstat")
	   		 	chkLan.checked = true
	   		end if
	   		
	   		'dim dg1 as datagrid = sender		
   			'dg1.databind()
   			
		end sub
		
		Sub ItemDataCommandEventHandler(sender as Object, e as DataGridCommandEventArgs )
	   	response.redirect("helloAAAAAAAAAAA")
	   	
		End Sub
	
		sub dd_tenstatus_SelectedIndexChanged(Source As System.Object, e As System.EventArgs)
			
			dim x as dropdownlist = Source
			dim cell as tablecell = x.parent
			dim item as DataGridItem = cell.Parent
			dim content as string = item.Cells(0).Text
			lblresult.text = content + " " + x.text
			updateworkflowmasterstep(content,x.text,"Tenant" )
			'dim cell as tablecell = Source
			'Dim list as DropDownList = ctype(cell.controls(8),DropDownList)
			
			'dim xx as label
			'xx.text=((DataGridItem)((DropDownList)sender).NamingContainer ).ItemIndex 
			'Dim list as DropDownList = e.Item.FindControl("dd_tenstatus")
			'response.write(content)
			'TableCell cell = list.Parent as TableCell
			bindworkflow()
		end sub
		
		sub dd_lanstatus_SelectedIndexChanged(Source As System.Object, e As System.EventArgs)
			
			dim x as dropdownlist = Source
			dim cell as tablecell = x.parent
			dim item as DataGridItem = cell.Parent
			dim content as string = item.Cells(0).Text
			lblresult.text = content + " " + x.text
			updateworkflowmasterstep(content,x.text,"Landlord" )
			bindworkflow()
		end sub

	 
 		
 		 public Sub chkCheckBox_CheckedChanged( ByVal sender As System.Object, ByVal e As System.EventArgs ) 
        	'session("BBtest")=""
        	Dim chkBoxCont,chkbox,chkbox2 As CheckBox
        	Dim dgiItem As DataGridItem
        	Dim dgGrid As DataGrid
        	
        	'Dim blnIsHeaderChecked As Boolean
         'Dim blnIsHeaderChecked2 As Boolean
       
        	chkBoxCont = CType(sender, CheckBox)
        	dgiItem = chkBoxCont.NamingContainer()
        	dgGrid = dgiItem.NamingContainer()
        	chkBox = CType(dgiItem.Cells(4).Controls(0), CheckBox)
        	chkbox2 = CType(dgiItem.Cells(5).Controls(0), CheckBox)
    
         dim SubstepID as TableCell = dgiItem.Cells(1)
       	dim SubstepIDValue as string = SubstepID.text
         dim ChkBoxClicked as string = chkBoxCont.id
     	   
     	   session("chkboxclicked")=ChkBoxClicked
			session("chkboxVal")=SubstepIDValue
			session("bbtest")="YAHOO"
			   	session("BBtest")="set"
     	   updateworkflowsubstep (SubstepIDValue ,ChkBoxClicked)
     
         'dim chkboxid as string = chkbox.id
        	'blnIsHeaderChecked = chkBox.Checked
        	'blnIsHeaderChecked2 = chkBox2.Checked
        	
			'session("chkboxclicked")=chkBoxCont.id
			'session("chkboxTEN")=blnIsHeaderChecked
			'session("chkboxLAN")=blnIsHeaderChecked2
			
			'if chkBoxCont.id = "tenstat" then
        	'   updateworkflowsubstep ()
        	'else
        '		chkBox2.Checked = false
        '	end if  
        	'Next
        	'ViewState("restore")="true"
    End Sub
    
    
    	public sub updateworkflowmasterstep(idpk as string, mstep as string, who as string)
			dim in_id as string = idpk
			dim in_step as string = mstep
			dim in_who as string = who
			
			Dim rightNow as DateTime = DateTime.Now.toShortDateString() 
   		'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
			Dim RightNowAdd as datetime = datetime.now
   		Dim supportedFormats() As String = New String() {"M/dd/yyyy","M/d/yyyy","MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
		   Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
  			dim sqlproc as string 
  			if in_who = "Tenant" then 
  				sqlproc ="sp_updateworkflowmasterstep"
  			else
  				response.write(in_id + " " + in_step + " " + in_who)
  				sqlproc ="sp_updateworkflowmasterstepLan"
  			end if
  			
  			dim myCommandADD As New SqlCommand( sqlproc, myConnectionADD)
  		 	myCommandADD.CommandType = CommandType.StoredProcedure
  		 	
	 		Dim prmsubsteppk As New SqlParameter("@prmsubsteppk", SqlDbType.int)
			prmsubsteppk.Value =  in_id
			myCommandADD.Parameters.Add(prmsubsteppk)
			
			Dim prmtenantstatus As New SqlParameter("@prmtenantstatus", SqlDbType.varchar, 50)
			prmtenantstatus.Value = in_step
			myCommandADD.Parameters.Add(prmtenantstatus)
			
		  Try
            myConnectionADD.Open()
            myCommandADD.ExecuteNonQuery()
            myConnectionADD.Close()
            Catch SQLexc As SqlException
               Response.Write("Insert Failed. Error Details are: " & SQLexc.ToString())
        End Try
		end sub
		
		Sub updateworkflowsubstep (idpk as string, chkboxid as string)
   		dim in_id as string = idpk
			dim in_chkboxid as string = chkboxid
			
			Dim rightNow as DateTime = DateTime.Now.toShortDateString() 
   		'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
			Dim RightNowAdd as datetime = datetime.now
   		Dim supportedFormats() As String = New String() {"M/dd/yyyy","M/d/yyyy","MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
		   Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
  			dim sqlproc as string = "sp_updateworkflowsubstep"
  			dim myCommandADD As New SqlCommand( sqlproc, myConnectionADD)
  		 	myCommandADD.CommandType = CommandType.StoredProcedure
  		 	
	 		Dim prmsubsteppk As New SqlParameter("@prmsubsteppk", SqlDbType.int)
			prmsubsteppk.Value =  in_id
			myCommandADD.Parameters.Add(prmsubsteppk)
			
			Dim prmchkboxid As New SqlParameter("@prmchkboxid", SqlDbType.varchar, 50)
			prmchkboxid.Value = in_chkboxid
			myCommandADD.Parameters.Add(prmchkboxid)
			
			
			
		  Try
            myConnectionADD.Open()
            myCommandADD.ExecuteNonQuery()
            myConnectionADD.Close()
            Catch SQLexc As SqlException
               Response.Write("Insert Failed. Error Details are: " & SQLexc.ToString())
        End Try
				'bindworkflow()
	end sub
	
end class
   
   

public Class DataGridTemplate
   Implements ITemplate
   Dim templateType As ListItemType
   Dim columnName As String
   Dim documentLogic As New leadmatchworkflow
	dim boxchkd As boolean
   Sub New(ByVal type As ListItemType, ByVal ColName As String)
      templateType = type
      columnName = ColName
    
   End Sub

   Sub InstantiateIn(ByVal container As Control) _
      Implements ITemplate.InstantiateIn
      Dim lc As New Literal()
      Select Case templateType
         Case ListItemType.Header
            lc.Text = "<B>" & columnName & "</B>"
            container.Controls.Add(lc)
         Case ListItemType.Item
            dim x as  new checkbox()
            x.id = columnName
           '	x.checked=boxchkd
            x.autopostback=true
            'x.Attributes.Add ("onclick","bindworkflow()")
            AddHandler x.CheckedChanged, AddressOf documentLogic.chkCheckBox_CheckedChanged
            lc.Text = "Item " & columnName
            container.Controls.Add(x)
         Case ListItemType.EditItem
            Dim tb As New TextBox()
            tb.Text = ""
            container.Controls.Add(tb)
         Case ListItemType.Footer
            lc.Text = "<I>Footer</I>"
            container.Controls.Add(lc)
      End Select
   End Sub
   
   


End Class

   
   
end namespace
