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
    Public Class createEM
        Inherits PageTemplate
        public dgcampaigns as datagrid
        Public pnlstepmain, pnlgeneral, pnlpage1, pnlpage2, pnlpage3, pnlpage3A, pnlpage4, pnlpage5, pnlpage6, pnlnotifications, pnlimages, pnltempatespre, pnladdstep As Panel
        public pnlasscamps, pnladdtocamp as panel
        Public spacer0, spacer1, spacer2, spacer3, spacer3a, spacer4, spacer5, spacer6 As HtmlTableCell
        Public subnavGen, subnavPage1, subnavPage2, subnavPage3, subnavPage3A, subnavPage4, subnavPage5, subnavPage6, subnavresp, subnavimgs As HtmlTableCell
        Protected WithEvents Lgen, lpage1, lpage2, lpage3, lpage3A, lpage4, lpage5, lpage6, lautop, limgs, btnuseremail2, btnuseremail As LinkButton
        Public ddemailcor,ddcamps As DropDownList
        Public emtext
        Public refererA
        public adtitleM as label
        public emsubject as textbox
		  public chklsupdate,chkltupdate,chklpupdate,chkrunonce,chkrundaily,chkrunweekly,chkrunMonthly,chkrunQtrly as checkbox
		  public pnlstepdetails,pnlstepconditions,pnl_addtask,pnlleaddds,pnlselectdoW,pnlselectdom,pnlselectdomQ,pnlWFfilters as panel
        public dd_status,dd_weekselect,dd_Monthselect,dd_Stepreorder,dd_StepStat as dropdownlist
        public dd_sdate,dd_wfstat,dd_trigger,dd_dstep,dd_freq,dd_duration,dd_condition,dd_emailcond as dropdownlist
        public lblstepno,dsp,lbllsfrom,lbllsto,lblsdateoffest as label
        public pnlsenmail,pnlsenmailLeads,pnlsenmailgroups,pnlsenmailindivid as panel
        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

            If Not (Page.IsPostBack) Then
                clearsessions()
                Fillemailcor()
                Fillcamps()
                If Session("pupclosed") = "true" Then
                	 if session("pupsource")="preview" then
	                    subnav("pg1")
	                    ddemailcor.SelectedIndex = ddemailcor.Items.IndexOf(ddemailcor.Items.FindByValue(Session("templatepk")))
	                    emtext.content = Session("templateText")
	                    Session("pupclosed") = "false"
	                    session("pupsource")="none" 
	                    refererA.attributes.add("value", gettemplatetext())
	                else
	                	  session("pupsource")="none" 
	                	  Session("pupclosed") = "false"
	                	  Fillemailcor()
	                	  subnav("pg1")
	                	  if session("newtemppk")="0" then
	                	  		ddemailcor.SelectedIndex = ddemailcor.Items.IndexOf(ddemailcor.Items.FindByValue(Session("templatepk")))
	                	  		'refererA.attributes.add("value", gettemplatetext())
	                	  		emtext.content = Session("templateText")
	                	  		emsubject.Text = Session("templateSubject")
	                	  else
	                	  		ddemailcor.SelectedIndex = ddemailcor.Items.IndexOf(ddemailcor.Items.FindByValue(session("newtemppk"))) 
	                	  		bindtempfields()
	                	  end if
	                	  
	                	  
	                	  
	                	  session("newtemppk")=0
	                end if
                Else
                    subnav("General")
                End If
					 if request.querystring("action")="new"
					 	adtitleM.text="New Email"
					 else
					 	adtitleM.text="Edit Email"
					 end if

            End If
            pagelayout()

        End Sub
        
        Sub bindtempfields()

            Dim strUID As String = Session("userid")
            Dim strSql As String = "SELECT * from tbl_emails where email_tbl_pk ='" & session("newtemppk") & "'"

            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then                                       
                    If Sqldr("email_subject") IsNot DBNull.Value Then
                        emsubject.Text = Sqldr("email_subject")
                    End If
                    If Sqldr("email_text") IsNot DBNull.Value Then
                        emtext.content = Sqldr("email_text")
                    End If 
                 
                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        End Sub
        
        
        
        
        Public Sub clearsessions()
				
        End Sub
        
        
        Sub addtocamp(ByVal sender As Object, ByVal e As EventArgs)
            pnlasscamps.visible=false
            pnladdtocamp.visible=true
            
        End Sub
        
        
        
        
        
        Sub btn_Gen(ByVal sender As Object, ByVal e As EventArgs)
            subnav("General")
        End Sub

        Sub btn_pg1(ByVal sender As Object, ByVal e As EventArgs)
            subnav("pg1")
        End Sub

        Sub btn_pg2(ByVal sender As Object, ByVal e As EventArgs)
            Subnav("pg2")
        End Sub

        Sub btn_pg3(ByVal sender As Object, ByVal e As EventArgs)
            Subnav("pg3")
        End Sub

        Sub btn_pg3A(ByVal sender As Object, ByVal e As EventArgs)
            Subnav("pg4")
        End Sub

        Sub btn_pg4(ByVal sender As Object, ByVal e As EventArgs)
            Subnav("pg4")
        End Sub

        Sub btn_pg5(ByVal sender As Object, ByVal e As EventArgs)
            Subnav("pg5")
        End Sub

        Sub btn_pg6(ByVal sender As Object, ByVal e As EventArgs)
            Subnav("pg6")
        End Sub

		
        Sub subnav(ByVal button As String)
            Dim clickedbutton As String = button

            'Set cell class
            subnavGen.Attributes.Add("class", "tblcelltest")
            subnavPage1.Attributes.Add("class", "tblcelltest")
            subnavPage2.Attributes.Add("class", "tblcelltest")
            subnavPage3.Attributes.Add("class", "tblcelltest")
            ' subnavPage4.Attributes.Add("class", "tblcelltest")
            'subnavPage5.Attributes.Add("class", "tblcelltest")
            'subnavPage6.Attributes.Add("class", "tblcelltest")

            'Set button font color
            Lgen.ForeColor = System.Drawing.Color.Black
            lpage1.ForeColor = System.Drawing.Color.Black
            lpage2.ForeColor = System.Drawing.Color.Black
            lpage3.ForeColor = System.Drawing.Color.Black
            'lpage4.ForeColor = System.Drawing.Color.Black
            'lpage5.ForeColor = System.Drawing.Color.Black
            'lpage6.ForeColor = System.Drawing.Color.Black

            'Set spacers
            spacer0.Visible = True
            spacer1.Visible = True
            spacer2.Visible = True
            spacer3.Visible = True
            'spacer4.Visible = True
            'spacer5.Visible = True
            'spacer6.Visible = True


            'Set Panels
            pnlgeneral.Visible = False
            pnlpage1.Visible = False
            pnlpage2.Visible = False
            pnlpage3.Visible = False
            pnlpage4.Visible = False
            pnlpage5.Visible = False
            pnlpage6.Visible = False

            If clickedbutton = "General" Then
                subnavGen.Attributes.Add("class", "tblcelltestSelected")
                Lgen.ForeColor = System.Drawing.Color.White
                spacer0.Visible = True
                pnlgeneral.Visible = True
            ElseIf clickedbutton = "pg1" Then
                subnavPage1.Attributes.Add("class", "tblcelltestSelected")
                lpage1.ForeColor = System.Drawing.Color.White
                spacer0.Visible = True
                spacer1.Visible = True
                pnlpage1.Visible = True
            ElseIf clickedbutton = "pg2" Then
                subnavPage2.Attributes.Add("class", "tblcelltestSelected")
                lpage2.ForeColor = System.Drawing.Color.White
                spacer0.Visible = True
                spacer1.Visible = True
                pnlpage2.Visible = True
            ElseIf clickedbutton = "pg3" Then
                subnavPage3.Attributes.Add("class", "tblcelltestSelected")
                lpage3.ForeColor = System.Drawing.Color.White
                spacer0.Visible = True
                spacer1.Visible = True
                spacer2.Visible = True
                pnlpage3.Visible = True                
            ElseIf clickedbutton = "pg4" Then
                subnavPage4.Attributes.Add("class", "tblcelltestSelected")
                lpage4.ForeColor = System.Drawing.Color.White
                pnlpage4.Visible = True
            ElseIf clickedbutton = "pg5" Then
                subnavPage5.Attributes.Add("class", "tblcelltestSelected")
                lpage5.ForeColor = System.Drawing.Color.White
                pnlpage5.Visible = True
            ElseIf clickedbutton = "pg6" Then
                subnavPage6.Attributes.Add("class", "tblcelltestSelected")
                lpage6.ForeColor = System.Drawing.Color.White
                pnlpage6.Visible = True
            Else
                subnavGen.Attributes.Add("class", "tblcelltestSelected")
                Lgen.ForeColor = System.Drawing.Color.White
                spacer0.Visible = False
                pnlgeneral.Visible = True
            End If
        End Sub

        Sub savead(ByVal sender As Object, ByVal e As EventArgs)

        End Sub
        
        Sub sendemail(ByVal sender As Object, ByVal e As EventArgs)
				subnav("pg3")
        End Sub
        
        

        Sub Fillemailcor()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select *,left(email_name,75) as 'ABemail_name', convert(varchar(20),email_date,101) as 'emdate' " _
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
                ddemailcor.DataTextField = "ABemail_name"
                ddemailcor.DataValueField = "email_tbl_pk"
                ddemailcor.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
            ddemailcor.Items.Insert(0, New ListItem("Select..", "9999"))
           

        End Sub

        Sub clrtxtbox(ByVal Source As System.Object, ByVal e As System.EventArgs)
            emtext.content = ""

        End Sub
        Sub tempreview(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Session("templatepk") = ddemailcor.SelectedItem.Value
            Session("templateText") = emtext.content
            session("pupsource")="preview"
            Response.Write("<script>window.open" & _
                    "('templatepreview.aspx?id=" & ddemailcor.SelectedItem.Value & "','_new', 'width=1100,height=650');</script>")

        End Sub
        
        Sub tempsave(ByVal Source As System.Object, ByVal e As System.EventArgs)
        		Session("templatepk") = ddemailcor.SelectedItem.Value
            Session("templatepk") = ddemailcor.SelectedItem.Value
            Session("templateText") = emtext.content
            Session("templateSubject")= emsubject.text
            session("pupsource")="newsource"
            Response.Write("<script>window.open" & _
                    "('emailmainteditaddPU.aspx?action=new&source=emmanager','_new', 'width=1100,height=650');</script>")

        End Sub
        
        
        
        
        Sub doLtemplates(ByVal Source As System.Object, ByVal e As System.EventArgs)
            refererA.attributes.add("value", gettemplatetext())
        End Sub
        
        
        Sub semrefresh(ByVal Source As System.Object, ByVal e As System.EventArgs)
            dim x as dropdownlist = source
            
            if x.selecteditem.value="Individual" then
            	pnlsenmail.visible=false
            	pnlsenmailLeads.visible=false
           		pnlsenmailgroups.visible=false
           		pnlsenmailindivid.visible=true
           		
            elseif x.selecteditem.value="Leads" then
            	pnlsenmail.visible=false
            	pnlsenmailLeads.visible=true
           		pnlsenmailgroups.visible=false
           		pnlsenmailindivid.visible=false
            else
            	pnlsenmail.visible=false
            	pnlsenmailLeads.visible=false
           		pnlsenmailgroups.visible=true
           		pnlsenmailindivid.visible=false
            end if
            
            
            
            
        End Sub
        
        
        
        
        
        Public Function gettemplatetext() As String
            Dim strUID As String = Session("userid")
            Dim strSql As String = "select email_text as 'bdtext' from tbl_emails where email_tbl_pk='" & ddemailcor.SelectedItem.Value & "'"

            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    Return Sqldr("bdtext")

                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        End Function
        
			Sub Fillcamps()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select * from tbl_CampaignMaster where cmp_useridfk='" & session("userid")& "'"
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                ddcamps.DataSource = dataReader
                ddcamps.DataTextField = "cmp_name"
                ddcamps.DataValueField = "cmp_tbl_pk"
                ddcamps.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
            ddcamps.Items.Insert(0, New ListItem("Select..", "9999"))
            
        End Sub


        'Page Layout
        Public Sub pagelayout()
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
        		end if
        	
        	elseif x.id="chkrundaily" then
        		pnlselectdom.visible=false
        		pnlselectdoW.visible=false
        		pnlselectdomQ.visible=false
        		
        		
        		if x.checked  then
        			chkrunonce.checked=false
        			chkrunweekly.checked=false
        			chkrunMonthly.checked=false
        			chkrunQtrly.checked=false
        			
        		end if
        	elseif x.id="chkrunweekly"  then
        			
        			
        		if x.checked then
        			pnlselectdom.visible=false
        			pnlselectdoW.visible=true
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
        		
        			
        		if x.checked  then
        			pnlselectdom.visible=true
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
        			
        			
        		if x.checked  then
        			pnlselectdom.visible=false
        			pnlselectdoW.visible=false
        			chkrunonce.checked=false
        			chkrundaily.checked=false
        			chkrunweekly.checked=false    			
        			pnlselectdomQ.visible=true
        			chkrunMonthly.checked=false
        		else
        			pnlselectdom.visible=false
        			pnlselectdoW.visible=false  
        			pnlselectdomQ.visible=false   
        		end if	
        		
        	
        	end if
        
       end sub
       
       
       
        Sub addnewemcamp(ByVal sender As Object, ByVal e As EventArgs)
        		'temp till dbsave done  replace with PKID of this email id
        		dim a as string
        		a=1
                Response.Write("<script>window.open" & _
                    "('addeditcmp.aspx?id=" & a & "&action=new','_new', 'width=1100,height=650');</script>")

        End Sub








    End Class
End Namespace