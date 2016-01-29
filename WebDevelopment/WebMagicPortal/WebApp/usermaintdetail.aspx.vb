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
Imports System.IO

namespace PageTemplate
	public class usermaintdetail
	   inherits PageTemplate
	     
        Protected WithEvents users,DGusradv As System.Web.UI.WebControls.DataGrid
        Public dd_userrole, dd_industry, dd_userstat, ddstate,dd_substat As DropDownList
        Public u_uid, u_fname, u_lname, u_email, u_email2, u_scode,u_companyPK As TextBox
        Public u_address, u_address2, u_city, u_state, u_Zip As TextBox
        Public u_company, u_license, u_hphone, u_cphone, u_fax, u_password As TextBox
        Public pnlUIDEXISTS, pnlRecordSaved, pnlmainscreen,pnlbetauser,pnladvscreen As Panel
        public chkbeta,chkcart  as checkbox
       
	      
        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

            If Not (Page.IsPostBack) Then
            	clearsessions()
                Fillindustry()
                Fillrole()
                fillstates()

                filluserstat()
                pnlUIDEXISTS.Visible = False
                pnlRecordSaved.Visible = False
                pnlmainscreen.Visible = True
                If Request.QueryString("action") = "new" Then
                    u_company.Text = Session("company")
                    u_company.Enabled = false
                    dd_userrole.SelectedIndex = dd_userrole.Items.IndexOf(dd_userrole.Items.FindByText("user"))
                    
                    dd_userrole.Enabled = true
                    u_uid.Enabled = False
                    'dd_userstat.SelectedIndex = dd_userstat.Items.IndexOf(dd_userstat.Items.FindByText("New"))
                    dd_userstat.selecteditem.text="New"
                    if session("role")="GOD" then
                    	dd_userstat.enabled=true
                    else
                    	dd_userstat.enabled=false
                    end if
                    
                Else
                    u_uid.Enabled = False
                    filluserfields()
                    If Session("role") = "user" Then
                        dd_userrole.Enabled = False
                        dd_userstat.Enabled = False
                    End If
                u_company.Enabled = False
                End If
                if session("role")="GOD" then
                	pnlbetauser.visible=true
                End If
                if  Session("ustat")="Trial" then
                	dd_userrole.enabled=false
                	dd_userstat.enabled=false
                else
                if session("role")<>"GOD" then
                	Dim removeListItem As ListItem = dd_userstat.Items.FindByText("Trial") 
          			dd_userstat.Items.remove(removeListItem)
          			removeListItem = dd_userrole.Items.FindByText("GOD") 
          			dd_userrole.Items.remove(removeListItem)
                End If
                
                end if
            End If
            pagesetup()

        End Sub
        sub clearsessions()
        
        	session("c32cartid")=0
        	session("copk")=0
        		session("newleadno")=""
        
        end sub
		   
        Public Sub SaveUser(ByVal sender As Object, ByVal e As EventArgs)

            If Request.QueryString("action") = "new" Then
                If checkuseridexists(u_uid.Text) Then
                    pnlUIDEXISTS.Visible = True
                Else
                    pnlUIDEXISTS.Visible = False
                    pnlRecordSaved.Visible = True
                    pnlmainscreen.Visible = False
                    If dd_userstat.SelectedItem.Text = "Trial" and u_company.text <> session("company") Then
                        if compexist() then 
                        	getcopkE()
                        else                        
                        	insertcompany()
                        	getcopk()
                       end if
                    End If
                    
                    insertdb()
                    insertcartdb()
                    If dd_userstat.SelectedItem.Text = "Trial"  Then
                    		insertDFLTBranding()
                     	createimgdirA()
                     	updatewmleadA()
                     end if
                End If
            Else
                insertdb()
                insertcartdb()

                if Session("qstringA")="" then
			 Response.Redirect("usermaint.aspx?search=*&role=*&status=*")
			else
            Response.Redirect(Session("qstringA"))
         end if
            End If
        End Sub
        
         Sub insertDFLTBranding()
            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String
				 sqlproc = "sp_insertbranding"
	         
	            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
	            myCommandADD.CommandType = CommandType.StoredProcedure
	        
	
	            Dim prmname As New SqlParameter("@name", SqlDbType.VarChar, 50)
	            prmname.Value = "Default"
	            myCommandADD.Parameters.Add(prmname)
	
	            Dim prmuid As New SqlParameter("@uid", SqlDbType.VarChar, 50)
	            prmuid.Value = u_uid.text
	            myCommandADD.Parameters.Add(prmuid)
	
	            Dim prmadno As New SqlParameter("@adno", SqlDbType.VarChar, 50)
	            prmadno.Value = DBNull.Value
	            myCommandADD.Parameters.Add(prmadno)
	
	            Dim prmcompany As New SqlParameter("@company", SqlDbType.VarChar, 50)
	            prmcompany.Value = u_company.text
	            myCommandADD.Parameters.Add(prmcompany)
	
	            Dim prmdescrip As New SqlParameter("@descript", SqlDbType.VarChar, 255)
	            prmdescrip.Value = "Default Branding Setup"
	            myCommandADD.Parameters.Add(prmdescrip)
	
	            Dim prmlogo As New SqlParameter("@logo", SqlDbType.VarChar, 1)
	            prmlogo.Value = "N"
	           
	            myCommandADD.Parameters.Add(prmlogo)
	
	            Dim prmlogo2 As New SqlParameter("@logo2", SqlDbType.VarChar, 1)
	               prmlogo2.Value = "N"
	        
	            myCommandADD.Parameters.Add(prmlogo2)
	
			      Dim prmemail As New SqlParameter("@email", SqlDbType.VarChar, 1)
	                prmemail.Value = "N"
	        
	            myCommandADD.Parameters.Add(prmemail)
	
	            Dim prmredirect As New SqlParameter("@redirect", SqlDbType.VarChar, 255)
	            prmredirect.Value = DBNull.Value
	            myCommandADD.Parameters.Add(prmredirect)
	
	            Dim prmtext1 As New SqlParameter("@text1", SqlDbType.Text)
	            prmtext1.Value = "Please complete the following information and then click Submit."
	            myCommandADD.Parameters.Add(prmtext1)
	
	            Dim prmtext2 As New SqlParameter("@text2", SqlDbType.Text)
	            prmtext2.Value = "Thank You!  Your request will be processed shortly."
	            myCommandADD.Parameters.Add(prmtext2)
	
	
	            Dim prmemailfrom As New SqlParameter("@emailfrom", SqlDbType.VarChar, 50)
	            prmemailfrom.Value = DBNull.Value
	            myCommandADD.Parameters.Add(prmemailfrom)
	
	            Dim prmreplyto As New SqlParameter("@replyto", SqlDbType.VarChar, 50)
	            prmreplyto.Value = DBNull.Value
	            myCommandADD.Parameters.Add(prmreplyto)
	
	            Dim prmsubject As New SqlParameter("@subject", SqlDbType.VarChar, 255)
	            prmsubject.Value = DBNull.Value
	            myCommandADD.Parameters.Add(prmsubject)
	
	            Dim prmbody As New SqlParameter("@body", SqlDbType.Text)
	            prmbody.Value = DBNull.Value
	            myCommandADD.Parameters.Add(prmbody)
	
	            Dim prmem As New SqlParameter("@getemail", SqlDbType.VarChar, 50)
	                prmem.Value = "N"
	            myCommandADD.Parameters.Add(prmem)
	            
	            Dim prmem2 As New SqlParameter("@getemail2", SqlDbType.VarChar, 50)
	              prmem2.Value = "N"
	            myCommandADD.Parameters.Add(prmem2)
	
	            Dim prmemadd As New SqlParameter("@emailaddress", SqlDbType.VarChar, 255)
	            prmemadd.Value = DBNull.Value
	            myCommandADD.Parameters.Add(prmemadd)
	            
	            Dim prmemadd2 As New SqlParameter("@emailaddress2", SqlDbType.VarChar, 255)
	            prmemadd2.Value = DBNull.Value
	            myCommandADD.Parameters.Add(prmemadd2)
	
	            Dim prmimgfk As New SqlParameter("@imgfk", SqlDbType.Int)
	            prmimgfk.Value = "999998"
	            myCommandADD.Parameters.Add(prmimgfk)
	            
	            Dim prmimgfk2 As New SqlParameter("@imgfk2", SqlDbType.Int)
	            prmimgfk2.Value = "999998"
	            myCommandADD.Parameters.Add(prmimgfk2)
	
	            Dim prmhdt1 As New SqlParameter("@hdtxt1", SqlDbType.VarChar, 1)
	               prmhdt1.Value = "N"
	            myCommandADD.Parameters.Add(prmhdt1)
	
	            Dim prmhdt12 As New SqlParameter("@hdtxt12", SqlDbType.VarChar, 1)
	                prmhdt12.Value = "N"
	            myCommandADD.Parameters.Add(prmhdt12)
	
					Dim prmhdt2 As New SqlParameter("@hdtxt2", SqlDbType.VarChar, 1)
	                prmhdt2.Value = "N"
	            myCommandADD.Parameters.Add(prmhdt2)
	
	      		Dim prmhdt22 As New SqlParameter("@hdtxt22", SqlDbType.VarChar, 1)
	               prmhdt22.Value = "N"
	            myCommandADD.Parameters.Add(prmhdt22)
	
	      
	            Dim prmhdtxt1t As New SqlParameter("@hdtxt1t ", SqlDbType.VarChar, 255)
	            prmhdtxt1t.Value = DBNull.Value
	            myCommandADD.Parameters.Add(prmhdtxt1t)
	
	            Dim prmhdtxt2t As New SqlParameter("@hdtxt2t ", SqlDbType.VarChar, 255)
	            prmhdtxt2t.Value = DBNull.Value
	            myCommandADD.Parameters.Add(prmhdtxt2t)
	
					Dim prmconame As New SqlParameter("@sconame", SqlDbType.VarChar, 1)
	                 prmconame.Value = "N"
	            myCommandADD.Parameters.Add(prmconame)
	            
	            Dim prmconame2 As New SqlParameter("@sconame2", SqlDbType.VarChar, 1)
	               prmconame2.Value = "N"
	            myCommandADD.Parameters.Add(prmconame2)
	            
	            Dim prmhrline As New SqlParameter("@shrline", SqlDbType.VarChar, 1)
	                prmhrline.Value = "Y"
	            myCommandADD.Parameters.Add(prmhrline)
	            
	            Dim prmhrline2 As New SqlParameter("@shrline2", SqlDbType.VarChar, 1)
	                prmhrline2.Value = "Y"
	             myCommandADD.Parameters.Add(prmhrline2)
	          
	            Dim prmcont As New SqlParameter("@scont", SqlDbType.VarChar, 1)
	                prmcont.Value = "Y"
	             myCommandADD.Parameters.Add(prmcont)
	            
	            Dim prmldlvl1 As New SqlParameter("@ldlvl1", SqlDbType.VarChar, 50)
	            prmldlvl1.Value = "Select.."
	            myCommandADD.Parameters.Add(prmldlvl1)
	            
	            Dim prmldlvl2 As New SqlParameter("@ldlvl2", SqlDbType.VarChar, 50)
	            prmldlvl2.Value =  "Select.."
	            myCommandADD.Parameters.Add(prmldlvl2)
	            
	            Dim prmloginlnk As New SqlParameter("@lglink", SqlDbType.VarChar, 1)
	                prmloginlnk.Value = "N"
	            myCommandADD.Parameters.Add(prmloginlnk)
	            
	            Dim prmloginlnk2 As New SqlParameter("@lglink2", SqlDbType.VarChar, 1)
	                prmloginlnk2.Value = "N"
	            myCommandADD.Parameters.Add(prmloginlnk2)
	            
	            Dim prmbstat As New SqlParameter("@bstat", SqlDbType.VarChar, 50)
	            prmbstat.Value = "Active"
	            myCommandADD.Parameters.Add(prmbstat)
	            
	            Dim prmmq As New SqlParameter("@mq", SqlDbType.VarChar, 50)
	            prmmq.Value = "999998"
	            myCommandADD.Parameters.Add(prmmq)           
	
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
        
        Public Sub insertdefaultresp()
       		Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
             Dim strSql As String = "insert into tbl_adbranding (br_uid_fk, br_name, br_description, br_showlogo,br_text1,br_text2,br_sendemail,br_getemail) " _
                            & "select '" & u_uid.text & "', 'Default','Standard Setting','N','Please complete the following information and click Submit','Thank You!  Your request will be processed shortly.','N','N'"
                            
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
        
        Sub createimgdirA()
            Directory.CreateDirectory(System.Configuration.ConfigurationManager.AppSettings("CurrentIMGDirectory") & Left(u_uid.text, Len(u_uid.text) - 4) & "IMG")

        End Sub        
              
        Sub getnewleadno()
            Dim strUID As String = session("userid")
            Dim strSql As String = "SELECT max(tbl_leads_pk)+1 as 'newpk' from dbo.tbl_leads"
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.read() Then
                    session("newleadno") = Sqldr("newpk")
                End If

            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.close()
            End Try
        End Sub
        
         Sub updatewmleadA()
            getnewleadno()
           
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}

            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))

            Dim sqlproc As String = "sp_addlead"

            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

            Dim prmleadno As New SqlParameter("@newleadno", SqlDbType.Int)
            prmleadno.Value = session("newleadno")
            myCommandADD.Parameters.Add(prmleadno)

            Dim prmuid As New SqlParameter("@uid", SqlDbType.VarChar, 50)
            prmuid.Value = "sales@webmagicportal.com"
            myCommandADD.Parameters.Add(prmuid)

            Dim prmlfname As New SqlParameter("@l_fname", SqlDbType.VarChar, 50)
            prmlfname.Value = u_fname.text
            myCommandADD.Parameters.Add(prmlfname)

            Dim prmllname As New SqlParameter("@l_lname", SqlDbType.VarChar, 50)
            prmllname.Value = u_lname.text
            myCommandADD.Parameters.Add(prmllname)

            Dim prmhphone As New SqlParameter("@l_hphone", SqlDbType.VarChar, 50)
            prmhphone.Value = u_hphone.text
            myCommandADD.Parameters.Add(prmhphone)

            Dim prmcphone As New SqlParameter("@l_cphone", SqlDbType.VarChar, 50)
            prmcphone.Value = u_cphone.text
            myCommandADD.Parameters.Add(prmcphone)

            Dim prmaddress As New SqlParameter("@l_address", SqlDbType.VarChar, 30)
            prmaddress.Value = u_address.text
            myCommandADD.Parameters.Add(prmaddress)

            Dim prmcity As New SqlParameter("@l_city", SqlDbType.VarChar, 30)
            prmcity.Value = u_city.text
            myCommandADD.Parameters.Add(prmcity)

            Dim prmstate As New SqlParameter("@l_state", SqlDbType.VarChar, 2)
            prmstate.Value = ddstate.SelectedItem.Value
            myCommandADD.Parameters.Add(prmstate)

            Dim prmzip As New SqlParameter("@l_zip", SqlDbType.VarChar, 50)
            prmzip.Value = u_Zip.text
            myCommandADD.Parameters.Add(prmzip)

            Dim prmagent As New SqlParameter("@l_agent", SqlDbType.VarChar, 30)
            prmagent.Value = "Sales User"
            myCommandADD.Parameters.Add(prmagent)

            Dim prmagentFK As New SqlParameter("@l_agent_FK", SqlDbType.VarChar, 30)
            prmagentFK.Value = "98979"
            myCommandADD.Parameters.Add(prmagentFK)

            Dim prmstatus As New SqlParameter("@l_status", SqlDbType.VarChar, 30)
            prmstatus.Value = "Accepted"
            myCommandADD.Parameters.Add(prmstatus)

            Dim prmleadtype As New SqlParameter("@l_leadtype", SqlDbType.VarChar, 30)
            prmleadtype.Value = "Trial Subscriber"
            myCommandADD.Parameters.Add(prmleadtype)

            Dim prmnotes As New SqlParameter("@l_notes", SqlDbType.Text)
            prmnotes.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmnotes)

            Dim prmemail As New SqlParameter("@l_email", SqlDbType.VarChar, 50)
            prmemail.Value = u_email.text
            myCommandADD.Parameters.Add(prmemail)

            Dim prmemail2 As New SqlParameter("@l_email2", SqlDbType.VarChar, 50)
            prmemail2.Value = u_email2.text
            myCommandADD.Parameters.Add(prmemail2)

            Dim prmadddate As New SqlParameter("@adddate", SqlDbType.DateTime)
            prmadddate.Value = RightNowAdd
            myCommandADD.Parameters.Add(prmadddate)

            Dim prmcapdate As New SqlParameter("@capdate", SqlDbType.DateTime)
            prmcapdate.Value = RightNowAdd
            myCommandADD.Parameters.Add(prmcapdate)

            Dim prmapptdate As New SqlParameter("@apptdate", SqlDbType.DateTime)
            prmapptdate.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmapptdate)

            Dim prmappttime As New SqlParameter("@appttime", SqlDbType.VarChar, 5)
            prmappttime.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmappttime)

            Dim prmapptloc As New SqlParameter("@apptloc", SqlDbType.VarChar, 30)
            prmapptloc.Value = "NA"
            myCommandADD.Parameters.Add(prmapptloc)

            Dim prmrefermortgage As New SqlParameter("@refermortg", SqlDbType.VarChar, 5)
            prmrefermortgage.Value = "N"
            myCommandADD.Parameters.Add(prmrefermortgage)

            Dim prmrefercredit As New SqlParameter("@refercredit", SqlDbType.VarChar, 5)
            prmrefercredit.Value = "N"
            myCommandADD.Parameters.Add(prmrefercredit)

            Dim prmreferother As New SqlParameter("@referother", SqlDbType.VarChar, 5)
            prmreferother.Value = "N"
            myCommandADD.Parameters.Add(prmreferother)

            Dim prmreferotherex As New SqlParameter("@referotherex", SqlDbType.VarChar, 50)
            prmreferotherex.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmreferotherex)

            Dim prmcomp As New SqlParameter("@comp", SqlDbType.VarChar, 15)
            prmcomp.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmcomp)

            Dim prmassignedagent As New SqlParameter("@assignedagent", SqlDbType.VarChar, 50)
            prmassignedagent.Value = "sales@webmagicportal.com"
            myCommandADD.Parameters.Add(prmassignedagent)

            Dim prmhighpri As New SqlParameter("@highpri", SqlDbType.VarChar, 5)
            prmhighpri.Value = "No"
            myCommandADD.Parameters.Add(prmhighpri)

            Dim prmldsource As New SqlParameter("@leadsource", SqlDbType.VarChar, 50)
            prmldsource.Value = "None"
            myCommandADD.Parameters.Add(prmldsource)

            Dim prmadcode As New SqlParameter("@adcode", SqlDbType.VarChar, 50)
            prmadcode.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmadcode)

            Dim prmmailtoaddress As New SqlParameter("@mailtoaddress", SqlDbType.VarChar, 50)
            prmmailtoaddress.Value = "N"
            myCommandADD.Parameters.Add(prmmailtoaddress)

            Dim prmproptolist As New SqlParameter("@proplist", SqlDbType.VarChar, 50)
            prmproptolist.Value = "N"
            myCommandADD.Parameters.Add(prmproptolist)

            Dim prmpstatus As New SqlParameter("@ld_pstatus", SqlDbType.VarChar, 50)
            prmpstatus.Value = "New"
            myCommandADD.Parameters.Add(prmpstatus)

            Dim prmprogram As New SqlParameter("@ld_program", SqlDbType.VarChar, 50)
            prmprogram.Value = "New"
            myCommandADD.Parameters.Add(prmprogram)

            Dim prmstatdetail As New SqlParameter("@ld_statdetail", SqlDbType.VarChar, 50)
            prmstatdetail.Value = "None"
            myCommandADD.Parameters.Add(prmstatdetail)

            Dim prmentrysource As New SqlParameter("@ld_entrysource", SqlDbType.VarChar, 50)
            prmentrysource.Value = "Auto"
            myCommandADD.Parameters.Add(prmentrysource)

            Dim prmfax As New SqlParameter("@ld_fax", SqlDbType.VarChar, 50)
            prmfax.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmfax)
            
                Dim prmmkprg As New SqlParameter("@marketprog", SqlDbType.VarChar, 50)
            prmmkprg.Value = "None"
            myCommandADD.Parameters.Add(prmmkprg)
            
            Dim prmmktto As New SqlParameter("@marketto", SqlDbType.VarChar, 50)
            prmmktto.Value = "Yes"
            myCommandADD.Parameters.Add(prmmktto)
            
             Dim prmldext As New SqlParameter("@ld_ext1", SqlDbType.VarChar, 50)
            prmldext.Value = dbnull.value
            myCommandADD.Parameters.Add(prmldext)


            Try
                myConnectionADD.Open()
                myCommandADD.ExecuteNonQuery()
                myConnectionADD.Close()
            Catch SQLexc As SqlException
                Response.Write("Insert Failed. Error Details are: " & SQLexc.ToString())
            End Try

        End Sub
		  
        Public Sub AddAnotherUser(ByVal sender As Object, ByVal e As EventArgs)

            Response.Redirect("usermaintdetail.aspx?action=new")
        End Sub
		  
        Public Sub btn_cancel(ByVal sender As Object, ByVal e As EventArgs)
			if Session("qstringA")="" then
			 Response.Redirect("usermaint.aspx?search=*&role=*&status=*")
			else
            Response.Redirect(Session("qstringA"))
         end if
        End Sub
        
        Public Sub btn_usradvanced(ByVal sender As Object, ByVal e As EventArgs)
				pnladvscreen.visible=true
				pnlUIDEXISTS.visible=false
				pnlmainscreen.visible=false
				bindusers(u_uid.text,u_companyPK.text)
			
        End Sub
        Public Sub btn_usradvancedR(ByVal sender As Object, ByVal e As EventArgs)
				pnladvscreen.visible=false
				pnlUIDEXISTS.visible=false
				pnlmainscreen.visible=true
				
			
        End Sub
        
        
        
         Public Sub bindusers(uid as string, copk as string)
           Dim strUID As String = Session("userid")
            
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
             mycommand =    "select *, fname + ' ' + lname as 'FullName' from dbo.tbl_users " _
                            & "left join tbl_Usr2UsrRoles on sUID='" & uid & "' and uid=tuid  " _
                            & "where company_pk ='" & copk & "' and UID <> '" & uid & "'"
        
	        Try
	                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
	                Dim dataSet As New DataSet()
	                dataAdapter.Fill(dataSet, "tbl_LeadADPlanVenues")
	                Dim dvProducts As New DataView(dataSet.Tables("tbl_LeadADPlanVenues"))
						
	                DGusradv.DataSource = dvProducts
	                DGusradv.DataBind()
						
	            Catch exc As System.Exception
	                Response.Write(exc.ToString())
	            Finally
	                myConnection.Dispose()
	            End Try
	        
	       end sub
	       
	        Sub ItemDataBoundEventHandlerADV(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then

					Dim itemLeadView As TableCell = e.Item.Cells(4)
 					Dim itemLeadEdit As TableCell = e.Item.Cells(5)
					
					
					Dim itemCellLeadViewTEXT as string = itemLeadView.text
					Dim itemCellLeadEditTEXT as string = itemLeadEdit.text

                Dim DGchkLDVW, DGchkLDED as checkbox
                DGchkLDVW = e.Item.Cells(0).FindControl("chkLDVW")
                DGchkLDED = e.Item.Cells(0).FindControl("chkLDED")
               
                If itemCellLeadViewTEXT = "True" Then
                    DGchkLDVW.checked=true
                Else
                   DGchkLDVW.checked=false

                End If
              		
              	 If itemCellLeadEditTEXT = "True" Then
                    DGchkLDED.checked=true
                Else
                   DGchkLDED.checked=false

                End If
              
              
              

            End If
        End Sub
        
        
        
         Sub updateu2u(ByVal Source As System.Object, ByVal e As System.EventArgs)
			
			 	Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim TUID As String = item.Cells(0).Text
            dim LDVW as string
            dim LDED as string
            Dim DGchkLDVW As checkbox
            Dim DGchkLDED As checkbox
            DGchkLDVW = Item.Cells(0).FindControl("chkLDVW")
            DGchkLDED = Item.Cells(0).FindControl("chkLDED")
            if DGchkLDVW.checked then 
            	LDVW="True"
            else
            	LDVW="False"
            end if
            if DGchkLDED.checked then 
            	LDED="True"
            else
            	LDED="False"
            end if
            if u2urecexists(TUID) then
            	
            	updateu2u(TUID,LDVW,LDED)
            else
            	
            	insertu2u(TUID,LDVW,LDED)
            end if
            bindusers(u_uid.text,u_companyPK.text)      
      end sub
        
        
        Public Function u2urecexists(TUID as string) As boolean

            Dim strSql As String
             strSql = "SELECT * from tbl_Usr2UsrRoles where TUID='" & TUID & "' and suid='" & u_uid.text & "'"
            
            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                		return true
                else
                		return false
                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        End Function
        
        
        
        sub insertu2u(TUID as string, LDVW as string, LDED as string)
        	Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
             Dim strSql As String = "insert into tbl_Usr2UsrRoles (SUID, TUID, LeadView, LeadEdit) " _
                            & "values ('" & u_uid.text & "', '" & TUID & "','" & LDVW & "','" & LDED & "')"
                            
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
        
        
        end sub
        
         sub updateu2u(TUID as string, LDVW as string, LDED as string)
        		Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "update tbl_Usr2UsrRoles set SUID='" & u_uid.text & "', TUID='" & TUID & "', LeadView='" & LDVW & "', LeadEdit= '" & LDED & "' "  _
                                   & "where TUID='" & TUID & "' and  SUID='" & u_uid.text & "'"
                            
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
        
        
        end sub
        
        
        
        
        Public Sub filluid(ByVal sender As Object, ByVal e As EventArgs)
             If Request.QueryString("action") = "new" Then
            	u_uid.Text = u_email.Text
            end if
			u_email2.focus()
        End Sub
        Sub Fillindustry()

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='userindustry' and (x_company='" & Session("company_pk") & "' or x_company='All')"
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                dd_industry.DataSource = dataReader
                dd_industry.DataTextField = "x_descr"
                dd_industry.DataValueField = "tbl_xwalk_pk"
                dd_industry.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
        End Sub
        Sub Fillstates()

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select * from dbo.tbl_states"
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                ddstate.DataSource = dataReader
                ddstate.DataTextField = "state"
                ddstate.DataValueField = "statabb"
                ddstate.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
        End Sub
	  			
        Sub filluserstat()

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='userstatus' and (x_company='" & Session("company_pk") & "' or x_company='All')"
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                dd_userstat.DataSource = dataReader
                dd_userstat.DataTextField = "x_descr"
                dd_userstat.DataValueField = "tbl_xwalk_pk"
                dd_userstat.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
           
        End Sub
	  		
        Sub Fillrole()

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='userrole' and (x_company='" & Session("company_pk") & "' or x_company='All')"
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                dd_userrole.DataSource = dataReader
                dd_userrole.DataTextField = "x_descr"
                dd_userrole.DataValueField = "tbl_xwalk_pk"
                dd_userrole.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
            If Session("role") = "GOD" Then
                dd_userrole.Items.Insert(0, New ListItem("GOD", "8888"))
            End If
        End Sub
	  		
        Public Sub insertdb()

            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String

            If Request.QueryString("action") = "new" Then
                sqlproc = "sp_adduserA"
            ElseIf Request.QueryString("action") = "edit" Then
                sqlproc = "sp_updateuserA"
            End If

            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

            If Request.QueryString("action") = "edit" Then
                Dim prmtblpk As New SqlParameter("@u_uidpk", SqlDbType.Int)
                prmtblpk.Value = Request.QueryString("id")
                myCommandADD.Parameters.Add(prmtblpk)
            End If

            Dim prmfname As New SqlParameter("@u_fname", SqlDbType.VarChar, 50)
            prmfname.Value = u_fname.Text
            myCommandADD.Parameters.Add(prmfname)

            Dim prmlname As New SqlParameter("@u_lname", SqlDbType.VarChar, 50)
            prmlname.Value = u_lname.Text
            myCommandADD.Parameters.Add(prmlname)

            Dim prmemail As New SqlParameter("@u_email", SqlDbType.VarChar, 50)
            prmemail.Value = u_email.Text
            myCommandADD.Parameters.Add(prmemail)

            Dim prmemail2 As New SqlParameter("@u_email2", SqlDbType.VarChar, 50)
            If u_email2.Text = "" Then
                prmemail2.Value = u_email.Text
            Else
                prmemail2.Value = u_email2.Text
            End If
            myCommandADD.Parameters.Add(prmemail2)

            Dim prmaddress As New SqlParameter("@u_address", SqlDbType.VarChar, 50)
            If u_address.Text = "" Then
                prmaddress.Value = DBNull.Value
            Else
                prmaddress.Value = u_address.Text
            End If
            myCommandADD.Parameters.Add(prmaddress)

            Dim prmaddress2 As New SqlParameter("@u_address2", SqlDbType.VarChar, 50)
            If u_address2.Text = "" Then
                prmaddress2.Value = DBNull.Value
            Else
                prmaddress2.Value = u_address2.Text
            End If
            myCommandADD.Parameters.Add(prmaddress2)

            Dim prmcity As New SqlParameter("@u_city", SqlDbType.VarChar, 50)
            If u_city.Text = "" Then
                prmcity.Value = DBNull.Value
            Else
                prmcity.Value = u_city.Text
            End If
            myCommandADD.Parameters.Add(prmcity)

            Dim prmstate As New SqlParameter("@u_state", SqlDbType.VarChar, 50)
            prmstate.Value = ddstate.SelectedItem.Value
            myCommandADD.Parameters.Add(prmstate)

            Dim prmzip As New SqlParameter("@u_Zip", SqlDbType.VarChar, 50)
            If u_Zip.Text = "" Then
                prmzip.Value = DBNull.Value
            Else
                prmzip.Value = u_Zip.Text
            End If
            myCommandADD.Parameters.Add(prmzip)

            Dim prmcompany As New SqlParameter("@u_company", SqlDbType.VarChar, 50)
            prmcompany.Value = u_company.Text
            myCommandADD.Parameters.Add(prmcompany)

            Dim prmlic As New SqlParameter("@u_license", SqlDbType.VarChar, 50)
            If u_license.Text = "" Then
                prmlic.Value = DBNull.Value
            Else
                prmlic.Value = u_license.Text
            End If
            myCommandADD.Parameters.Add(prmlic)

            Dim prmind As New SqlParameter("@dd_industry", SqlDbType.VarChar, 30)
            prmind.Value = dd_industry.SelectedItem.Text
            myCommandADD.Parameters.Add(prmind)

            Dim prmbizphone As New SqlParameter("@u_hphone", SqlDbType.VarChar, 50)
            prmbizphone.Value = u_hphone.Text
            myCommandADD.Parameters.Add(prmbizphone)

            Dim prmcphone As New SqlParameter("@u_cphone", SqlDbType.VarChar, 50)
            If u_cphone.Text = "" Then
                prmcphone.Value = DBNull.Value
            Else
                prmcphone.Value = u_cphone.Text
            End If
            myCommandADD.Parameters.Add(prmcphone)

            Dim prmfax As New SqlParameter("@u_fax", SqlDbType.VarChar, 50)
            If u_fax.Text = "" Then
                prmfax.Value = DBNull.Value
            Else
                prmfax.Value = u_fax.Text
            End If
            myCommandADD.Parameters.Add(prmfax)

            Dim prmuid As New SqlParameter("@u_uid", SqlDbType.VarChar, 50)
            prmuid.Value = u_uid.Text
            myCommandADD.Parameters.Add(prmuid)

            Dim prmpassword As New SqlParameter("@u_password", SqlDbType.VarChar, 50)
            prmpassword.Value = u_password.Text
            myCommandADD.Parameters.Add(prmpassword)

            Dim prmstat As New SqlParameter("@dd_userstat", SqlDbType.VarChar, 30)
                 prmstat.Value = dd_userstat.SelectedItem.Text
           
            myCommandADD.Parameters.Add(prmstat)

            Dim prmtype As New SqlParameter("@dd_usertype", SqlDbType.VarChar, 30)
            prmtype.Value = "Agent"
            myCommandADD.Parameters.Add(prmtype)

            Dim prmrole As New SqlParameter("@dd_userrole", SqlDbType.VarChar, 30)
            prmrole.Value = dd_userrole.SelectedItem.Text
            myCommandADD.Parameters.Add(prmrole)

            Dim prmscode As New SqlParameter("@scode", SqlDbType.VarChar, 30)
            prmscode.Value = u_scode.Text
            myCommandADD.Parameters.Add(prmscode)

            Dim prmbeta As New SqlParameter("@beta", SqlDbType.VarChar, 30)
            If chkbeta.Checked Then
                prmbeta.Value = "Yes"
            Else
                prmbeta.Value = "No"
            End If
            myCommandADD.Parameters.Add(prmbeta)
            
            Dim prmcart As New SqlParameter("@cart", SqlDbType.VarChar, 30)
            If chkcart.Checked Then
                prmcart.Value = "Yes"
            Else
                prmcart.Value = "No"
            End If
            myCommandADD.Parameters.Add(prmcart)
            

            If Request.QueryString("action") = "new" Then
                Dim prmsubd As New SqlParameter("@subdate", SqlDbType.Int)
                If dd_substat.SelectedItem.Text = "Normal" Then
                    prmsubd.Value = 0
                ElseIf dd_substat.SelectedItem.Text = "5 Day" Then
                    prmsubd.Value = 5
                ElseIf dd_substat.SelectedItem.Text = "10 Day" Then
                    prmsubd.Value = 10
                ElseIf dd_substat.SelectedItem.Text = "20 Day" Then
                    prmsubd.Value = 20
                ElseIf dd_substat.SelectedItem.Text = "30 Day" Then
                    prmsubd.Value = 30
                ElseIf dd_substat.SelectedItem.Text = "Unlimitted" Then
                    prmsubd.Value = 9999
                End If
                myCommandADD.Parameters.Add(prmsubd)

                Dim prmc32id As New SqlParameter("@c32id", SqlDbType.Int)
                prmc32id.Value = getcartuid()
                myCommandADD.Parameters.Add(prmc32id)
                

                Dim prmcopk As New SqlParameter("@copk", SqlDbType.Int)
                If dd_userstat.SelectedItem.Text = "Trial" Then
                    prmcopk.Value = session("copk")
                Else
                    prmcopk.Value = Session("company_pk")
                End If
                myCommandADD.Parameters.Add(prmcopk)
                
                Dim prmsubstat As New SqlParameter("@substat", SqlDbType.varchar,50)
                prmsubstat.Value = dd_substat.SelectedItem.Text
                myCommandADD.Parameters.Add(prmsubstat)
                
                Dim prmbrd As New SqlParameter("@branding", SqlDbType.varchar,5)
                If dd_userstat.SelectedItem.Text = "Trial" Then
                    prmbrd.Value = "Yes"
                Else
                    prmbrd.Value = "No"
                End If
                
                myCommandADD.Parameters.Add(prmbrd)



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
        Sub insertcartdb()

            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String

            If Request.QueryString("action") = "new" Then
                sqlproc = "sp_adduserCART"
            ElseIf Request.QueryString("action") = "edit" Then
                'Response.Write("here")
                sqlproc = "sp_updateuserCART"
            End If

            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

            Dim prmuid As New SqlParameter("@u_uid", SqlDbType.Int)
            prmuid.Value = session("c32cartid") 
            myCommandADD.Parameters.Add(prmuid)

            Dim prmfname As New SqlParameter("@u_fname", SqlDbType.VarChar, 50)
            prmfname.Value = u_fname.Text
            myCommandADD.Parameters.Add(prmfname)

            Dim prmlname As New SqlParameter("@u_lname", SqlDbType.VarChar, 50)
            prmlname.Value = u_lname.Text
            myCommandADD.Parameters.Add(prmlname)

            Dim prmemail As New SqlParameter("@u_email", SqlDbType.VarChar, 50)
            If Request.QueryString("action") = "new" Then
                prmemail.Value = u_email.Text
            Else
                prmemail.Value = u_uid.Text
            End If
            myCommandADD.Parameters.Add(prmemail)


            Dim prmaddress As New SqlParameter("@u_address", SqlDbType.VarChar, 50)
            If u_address.Text = "" Then
                prmaddress.Value = DBNull.Value
            Else
                prmaddress.Value = u_address.Text
            End If
            myCommandADD.Parameters.Add(prmaddress)

            Dim prmaddress2 As New SqlParameter("@u_address2", SqlDbType.VarChar, 50)
            If u_address2.Text = "" Then
                prmaddress2.Value = DBNull.Value
            Else
                prmaddress2.Value = u_address2.Text
            End If
            myCommandADD.Parameters.Add(prmaddress2)

            Dim prmcity As New SqlParameter("@u_city", SqlDbType.VarChar, 50)
            If u_city.Text = "" Then
                prmcity.Value = DBNull.Value
            Else
                prmcity.Value = u_city.Text
            End If
            myCommandADD.Parameters.Add(prmcity)

            Dim prmstate As New SqlParameter("@u_state", SqlDbType.VarChar, 50)
            prmstate.Value = ddstate.SelectedItem.Value
            myCommandADD.Parameters.Add(prmstate)

            Dim prmzip As New SqlParameter("@u_Zip", SqlDbType.VarChar, 50)
            If u_Zip.Text = "" Then
                prmzip.Value = DBNull.Value
            Else
                prmzip.Value = u_Zip.Text
            End If
            myCommandADD.Parameters.Add(prmzip)

            Dim prmcompany As New SqlParameter("@u_company", SqlDbType.VarChar, 50)
            prmcompany.Value = u_company.Text
            myCommandADD.Parameters.Add(prmcompany)

            Dim prmind As New SqlParameter("@dd_industry", SqlDbType.VarChar, 30)
            prmind.Value = dd_industry.SelectedItem.Text
            myCommandADD.Parameters.Add(prmind)

            Dim prmbizphone As New SqlParameter("@u_hphone", SqlDbType.VarChar, 50)
            prmbizphone.Value = u_hphone.Text
            myCommandADD.Parameters.Add(prmbizphone)

            Dim prmfax As New SqlParameter("@u_fax", SqlDbType.VarChar, 50)
            If u_fax.Text = "" Then
                prmfax.Value = DBNull.Value
            Else
                prmfax.Value = u_fax.Text
            End If
            myCommandADD.Parameters.Add(prmfax)

            Dim prmpassword As New SqlParameter("@u_password", SqlDbType.VarChar, 50)
            prmpassword.Value = u_password.Text
            myCommandADD.Parameters.Add(prmpassword)

            Dim prmscode As New SqlParameter("@scode", SqlDbType.VarChar, 30)
            prmscode.Value = u_scode.Text
            myCommandADD.Parameters.Add(prmscode)

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
        
        Sub getcopk()
            Dim strSql As String = "SELECT max(co_tbl_pk) as 'copk' from tbl_company"
            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    session("copk") = Sqldr("copk")

                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try

        End Sub
        
        Sub getcopkE()
            Dim strSql As String = "SELECT co_tbl_pk as 'copk' from tbl_company where co_name='" & u_company.text & "'"
            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    session("copk") = Sqldr("copk")

                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try

        End Sub
        
        
        
        Public Function getcartuid() As Integer

            Dim strSql As String
             	If Request.QueryString("action") = "new" Then
            		strSql = "SELECT cast(max(USERID)+1 as integer) as 'newid' from cart32.dbo.users"
           		else 
            		strSql = "SELECT cast(cart32id as integer) as 'newid' from tbl_users where uid='" & session("userid") & "'"
            	end if
            
            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                		session("c32cartid")= Sqldr("newid")
                    Return Sqldr("newid")
                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        End Function

 			Sub statchange(ByVal Source As System.Object, ByVal e As System.EventArgs)
       		if dd_userstat.selecteditem.text = "Trial" then
       			u_company.enabled=true
       			u_company.BackColor = Red
       			u_company.focus()
       			Dim removeListItem As ListItem = dd_substat.Items.FindByText("Normal") 
          		dd_substat.Items.remove(removeListItem)
          		removeListItem  = dd_userrole.Items.FindByText("GOD") 
          		dd_userrole.Items.remove(removeListItem)
       		end if
       
       	end sub
        Sub insertcompany()
            Dim strSql As String = "insert into tbl_company (co_name,co_status) values ('" & u_company.Text & "','Active')"
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
     			
   
        public function compexist() as boolean
        		Dim strSql As String = "select * from  tbl_company where co_name='" & u_company.text & "'"
            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                	return true
                	
                else
                	return false
                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        end function
        
        Sub filluserfields()

            Dim strSql As String = "SELECT * from tbl_users where users_tbl_PK =" & Request.QueryString("id")
            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    u_fname.Text = Sqldr("fname")
                    u_lname.Text = Sqldr("lname")
                    u_email.Text = Sqldr("email")
                    u_uid.Text = Sqldr("UID")
                    u_password.Text = Sqldr("password")
                    If Sqldr("email") IsNot DBNull.Value Then
                        u_email2.Text = Sqldr("email2")
                    End If
                    If Sqldr("address1") IsNot DBNull.Value Then
                        u_address.Text = Sqldr("address1")
                    End If
                    If Sqldr("address2") IsNot DBNull.Value Then
                        u_address2.Text = Sqldr("address2")
                    End If
                    If Sqldr("city") IsNot DBNull.Value Then
                        u_city.Text = Sqldr("city")
                    End If
                    If Sqldr("state") IsNot DBNull.Value Then
                        ddstate.SelectedIndex = ddstate.Items.IndexOf(ddstate.Items.FindByValue(Sqldr("state")))
                    End If
                    If Sqldr("zip") IsNot DBNull.Value Then
                        u_Zip.Text = Sqldr("zip")
                    End If
                    If Sqldr("company") IsNot DBNull.Value Then
                        u_company.Text = Sqldr("company")
                    End If
                    If Sqldr("company_pk") IsNot DBNull.Value Then
                        u_companyPK.Text = Sqldr("company_pk")
                    End If
                    If Sqldr("licenseno") IsNot DBNull.Value Then
                        u_license.Text = Sqldr("licenseno")
                    End If
                    
                    If Sqldr("bphone") IsNot DBNull.Value Then
                        u_hphone.Text = Sqldr("bphone")
                    End If
                    If Sqldr("cphone") IsNot DBNull.Value Then
                        u_cphone.Text = Sqldr("cphone")
                    End If
                    If Sqldr("fax") IsNot DBNull.Value Then
                        u_fax.Text = Sqldr("fax")
                    End If
                    If Sqldr("industry") IsNot DBNull.Value Then
                        dd_industry.SelectedIndex = dd_industry.Items.IndexOf(dd_industry.Items.FindByText(Sqldr("industry")))
                    End If
                    If Sqldr("status") IsNot DBNull.Value Then
                        dd_userstat.SelectedIndex = dd_userstat.Items.IndexOf(dd_userstat.Items.FindByText(Sqldr("status")))
                    End If

                    If Sqldr("role") IsNot DBNull.Value Then
                        dd_userrole.SelectedIndex = dd_userrole.Items.IndexOf(dd_userrole.Items.FindByText(Sqldr("role")))
                    End If

                    If Sqldr("secretcode") IsNot DBNull.Value Then
                        u_scode.Text = Sqldr("secretcode")
                    End If
                    
                    If Sqldr("BetaUser") IsNot DBNull.Value Then
                        If Sqldr("BetaUser")="Yes" then
                        	chkbeta.checked=true
                        else
                        	chkbeta.checked=false
                        end if                      
                        
                    End If
                    
                    If Sqldr("shopcart") IsNot DBNull.Value Then
                        If Sqldr("shopcart")="Yes" then
                        	chkcart.checked=true
                        else
                        	chkcart.checked=false
                        end if                      
                        
                    End If
                    
                    
                    If Sqldr("package") IsNot DBNull.Value Then
                        dd_substat.SelectedIndex = dd_substat.Items.IndexOf(dd_substat.Items.FindByText(Sqldr("package")))
                    End If
                    
                    

                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try

        End Sub

        Public Function checkuseridexists(ByVal uid As String) As Boolean
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "SELECT uid from tbl_users where UID='" & uid & "'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    Return True
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try

        End Function

         Public Sub pagesetup()

         'width will be calculated automatically, but it is sometimes
           	layout.Width = System.Configuration.ConfigurationManager.AppSettings("ScreenWidth")
            leftNav.Width = System.Configuration.ConfigurationManager.AppSettings("ScreenLeftNav")            
            body.Height = System.Configuration.ConfigurationManager.AppSettings("ScreenBodyHeight")
          	body.Width = System.Configuration.ConfigurationManager.AppSettings("ScreenBodyWidth")
            layout.Border = System.Configuration.ConfigurationManager.AppSettings("ScreenBorder")
            footer.controls.add(LoadControl(System.Configuration.ConfigurationManager.AppSettings("ScreenFooter")))
            Header.Controls.Add(LoadControl(System.Configuration.ConfigurationManager.AppSettings("ScreenHeader")))
            leftNav.Controls.Add(LoadControl(System.Configuration.ConfigurationManager.AppSettings("ScreenLeftNavSetup")))
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