Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web
Imports System.Web.Security
Imports System.Configuration
imports system.net.mail
Imports System.Text

namespace gredirect
Public Class redirect
   Inherits Page
	
	public pnlthanks as panel	
	public emadd,weblink,ldno as label
	
	 Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            If Not (Page.IsPostBack) Then
            	clearsessions()
              checkredirect()
              weblink.text="<a href=" & System.Configuration.ConfigurationManager.AppSettings("CurrentwebURL") & "/gsignup.aspx>Web Magic Portal</a>"
            End If
        End Sub
        
        
    public sub clearsessions()
    
    	 session("leadno")=""
	    session("emailto")=""
	    session("leadname")=""
	    session("leadaddress")=""
	    session("leadcity")=""
	    session("leadstate")=""
	    session("leadzip")=""
	    session("leadphone1")=""
	    session("leadphone2")=""
	    session("leademail")=""
	    session("leademail2")=""
     	 session("leadnotes")=""
       session("cusname")=""
       session("cuslname")=""
       session("cusfname")=""
       session("cusadd")=""
       session("cuscity")=""
       session("cusstate")=""
       session("cuszip")=""
       session("cusphone")=""
       session("cusemail")=""
       session("newleadno")=""      
    
    end sub
    
   
   public sub checkredirect()
		Dim strConnection As String
		Dim sqlConn As SqlConnection
	   Dim sqlCmd As SqlCommand
		Dim ip As String = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
      If ip = "" Then
          ip = Request.ServerVariables("REMOTE_ADDR")
      End If
	
		  	Dim strSql as String = "select * from tbL_c32process where c32p_ipno='" & ip & "' and convert(varchar(20),c32p_date,101) = convert(varchar(20), getdate(),101)"
		  	Try
	           strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
	           sqlConn = New SqlConnection(strConnection)
	           sqlCmd = New SqlCommand(strSql, sqlConn)
	           
	           sqlConn.Open()		           
	           Dim Sqldr as SqlDataReader = sqlCmd.ExecuteReader	
	           
	             If Sqldr.Read() Then
	             	if sqldr("c32p_action") isnot dbnull.value then
	             	     if sqldr("c32p_action")="Signup" or sqldr("c32p_action")="Activate"  then 
	             	     		response.redirect(System.Configuration.ConfigurationManager.AppSettings("CurrentwebURL") & "/gsetup.aspx")
	             	     elseif sqldr("c32p_action")="branding"
	             	     		response.redirect(System.Configuration.ConfigurationManager.AppSettings("CurrentAppURL") & "/default.aspx?action=branding&uid=" & sqldr("c32p_uid"))
	             	     elseif sqldr("c32p_action")="autopost"
	             	   		response.redirect(System.Configuration.ConfigurationManager.AppSettings("CurrentAppURL") & "/default.aspx?action=autopost&uid=" & sqldr("c32p_uid"))
	            
	             	     elseif sqldr("c32p_action")="LeadPurchase"
	             	     			getleadno()
	             	     			sendleadinfo()
	             	     			updateleadstat()
	             	     			
	             	     			getcustomerinfo()
	             	     			inserthistoryrecord()
	             	     			insertcusaslead()
	             	     			'removec32rec()
	             	     			
	             	     			pnlthanks.visible=true    
	             	     
	             	     else      	     
	             	     		response.redirect(System.Configuration.ConfigurationManager.AppSettings("CurrentAppURL") & "/default.aspx?action=renewed")
	             	     end if
	             	     'response.write("here2")
	             	 end if   
	             end if
	           
	                     		           
	      
		   Catch ex As Exception
	    		Response.Write(ex.ToString())
	    		exit sub
	    	Finally
	       	sqlConn.Close()
		 	End Try
   end sub
   
   	Sub insertcusaslead()
 			Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim CartCustCode As String = Request.Cookies("C32-CHOICEONE-CustCode").Value
            Dim strSql As String = "SELECT * from tbl_leads where ld_lname='" & session("cuslname") & "' and ld_fname='" & session("cusfname") & "'"

            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)


                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                	
                else
                	updatewmleadA()
                end if
                    
                
            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
 		end sub
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
            prmleadno.Value =  session("newleadno")
            myCommandADD.Parameters.Add(prmleadno)

            Dim prmuid As New SqlParameter("@uid", SqlDbType.VarChar, 50)
            prmuid.Value = getagentid(session("leadno"))
            myCommandADD.Parameters.Add(prmuid)
 
            Dim prmlfname As New SqlParameter("@l_fname", SqlDbType.VarChar, 50)
            prmlfname.Value = session("cusfname")
            myCommandADD.Parameters.Add(prmlfname)

            Dim prmllname As New SqlParameter("@l_lname", SqlDbType.VarChar, 50)
            prmllname.Value = session("cuslname")
            myCommandADD.Parameters.Add(prmllname)

            Dim prmhphone As New SqlParameter("@l_hphone", SqlDbType.VarChar, 50)
            prmhphone.Value = session("cusphone")
            myCommandADD.Parameters.Add(prmhphone)

            Dim prmcphone As New SqlParameter("@l_cphone", SqlDbType.VarChar, 50)
            prmcphone.Value = dbnull.value
            myCommandADD.Parameters.Add(prmcphone)

            Dim prmaddress As New SqlParameter("@l_address", SqlDbType.VarChar, 30)
            prmaddress.Value = session("cusadd")
            myCommandADD.Parameters.Add(prmaddress)

            Dim prmcity As New SqlParameter("@l_city", SqlDbType.VarChar, 30)
            prmcity.Value = session("cuscity")
            myCommandADD.Parameters.Add(prmcity)

            Dim prmstate As New SqlParameter("@l_state", SqlDbType.VarChar, 2)
            prmstate.Value = session("cusstate")
            myCommandADD.Parameters.Add(prmstate)

            Dim prmzip As New SqlParameter("@l_zip", SqlDbType.VarChar, 50)
            prmzip.Value = session("cuszip")
            myCommandADD.Parameters.Add(prmzip)

            Dim prmagent As New SqlParameter("@l_agent", SqlDbType.VarChar, 30)
            prmagent.Value = "Master User"
            myCommandADD.Parameters.Add(prmagent)

            Dim prmagentFK As New SqlParameter("@l_agent_FK", SqlDbType.VarChar, 30)
            prmagentFK.Value = "98979"
            myCommandADD.Parameters.Add(prmagentFK)

            Dim prmstatus As New SqlParameter("@l_status", SqlDbType.VarChar, 30)
            prmstatus.Value = "Accepted"
            myCommandADD.Parameters.Add(prmstatus)

            Dim prmleadtype As New SqlParameter("@l_leadtype", SqlDbType.VarChar, 30)
            prmleadtype.Value = "New Lead Buyer"
            myCommandADD.Parameters.Add(prmleadtype)

            Dim prmnotes As New SqlParameter("@l_notes", SqlDbType.Text)
            prmnotes.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmnotes)

            Dim prmemail As New SqlParameter("@l_email", SqlDbType.VarChar, 50)
            prmemail.Value = session("cusemail")
            myCommandADD.Parameters.Add(prmemail)

            Dim prmemail2 As New SqlParameter("@l_email2", SqlDbType.VarChar, 50)
            prmemail2.Value = DBNull.Value
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
        
   
   	Sub getcustomerinfo()
   			Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim CartCustCode As String = Request.Cookies("C32-CHOICEONE-CustCode").Value
            Dim strSql As String = "SELECT * from cart32.dbo.customer where CUSTCODE='" & CartCustCode & "'"

            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)


                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                	if sqldr("B_LNAME") isnot dbnull.value then
                		session("cuslname") =sqldr("B_LNAME")
                	else
                		session("cuslname") =""
                	end if
                		
                	if sqldr("B_LNAME") isnot dbnull.value then
                		session("cuslname") =sqldr("B_LNAME")
                	else
                		session("cuslname") =""
                	end if
                	if sqldr("B_FNAME") isnot dbnull.value then
                		session("cusfname") =sqldr("B_FNAME")
                	else
                		session("cusfname") =""
                	end if
                	session("cusname") = session("cusfname") + " " + session("cuslname")
                	if sqldr("B_ADDR") isnot dbnull.value then
                		session("cusadd")=sqldr("B_ADDR")
                	else
                		session("cusadd") =""
                	end if
                	if sqldr("B_CITY") isnot dbnull.value then
                		session("cuscity")=sqldr("B_CITY")
                	else
                		session("cuscity") =""
                	end if
                	if sqldr("B_STATE") isnot dbnull.value then
                		session("cusstate")=sqldr("B_STATE")                
                	else
                		session("cusstate") =""
                	end if
                	if sqldr("B_ZIP") isnot dbnull.value then
                		session("cuszip")=sqldr("B_ZIP")
                	else
                		session("cuszip") =""
                	end if
                	if sqldr("B_PHONE") isnot dbnull.value then
                		session("cusphone")=sqldr("B_PHONE")
                	else
                		session("cusphone") =""
                	end if
                	if sqldr("B_EMAIL") isnot dbnull.value then
                		session("cusemail")=sqldr("B_EMAIL")
                	else
                		session("cusemail") =""
                	end if
                	
                
                end if
                
                
            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try



		end sub
      Sub inserthistoryrecord()

            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String
            sqlproc = "sp_insertleadcontact"

            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

            ' Add Parameters to SPROC
         
            Dim prmleadno As New SqlParameter("@leadfk", SqlDbType.Int)
            prmleadno.Value = session("leadno")
            myCommandADD.Parameters.Add(prmleadno)

            Dim prmcapdate As New SqlParameter("@capdate", SqlDbType.DateTime)
            prmcapdate.Value = rightNow
            myCommandADD.Parameters.Add(prmcapdate)

            Dim prmnotes As New SqlParameter("@l_notes", SqlDbType.Text)
            Dim nt As String = ""
            nt = "<table><tr><td>Lead was purchased by :</td></tr></table><br><table>"
            nt = nt + "<tr><td>Name:</td><td>"    & session("cusname") & "</td></tr>"
            nt = nt + "<tr><td>Address:</td><td>" & session("cusadd") & "</td></tr>"
            nt = nt + "<tr><td>City:</td><td>"    & session("cuscity") & "</td></tr>"
            nt = nt + "<tr><td>State:</td><td>"   & session("cusstate") & "</td></tr>"
            nt = nt + "<tr><td>Zip:</td><td>"     & session("cuszip") & "</td></tr>"
            nt = nt + "<tr><td>Phone:</td><td>"   & session("cusphone") & "</td></tr>"
            nt = nt + "<tr><td>Email:</td><td>"   & session("cusemail") & "</td></tr></table>"
            prmnotes.Value = nt
            myCommandADD.Parameters.Add(prmnotes)

            Dim prmuid As New SqlParameter("@uid", SqlDbType.VarChar, 50)
            prmuid.Value = "sales@webmagicportal.com"
            myCommandADD.Parameters.Add(prmuid)

            Dim prmtype As New SqlParameter("@LHType", SqlDbType.VarChar, 50)
            prmtype.Value = "Lead Sold"
            myCommandADD.Parameters.Add(prmtype)

            Dim prmfollowup As New SqlParameter("@followup", SqlDbType.VarChar, 50)
            prmfollowup.Value = "No"
            myCommandADD.Parameters.Add(prmfollowup)

            Dim prmaction As New SqlParameter("@followupactions", SqlDbType.Text)
            prmaction.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmaction)

            Dim prmstatus As New SqlParameter("@LHstat", SqlDbType.VarChar, 50)
            prmstatus.Value = "Closed"
            myCommandADD.Parameters.Add(prmstatus)

            Dim prmwho As New SqlParameter("@LHwho", SqlDbType.VarChar, 50)
            prmwho.Value = "System"
            myCommandADD.Parameters.Add(prmwho)

            Dim prmclosedt As New SqlParameter("@closedate", SqlDbType.DateTime)
            prmclosedt.Value = rightNow
            myCommandADD.Parameters.Add(prmclosedt)

            Dim prmfduedt As New SqlParameter("@fduedate", SqlDbType.DateTime)
            prmfduedt.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmfduedt)

            Try
                myConnectionADD.Open()
                myCommandADD.ExecuteNonQuery()
                myConnectionADD.Close()
            Catch SQLexc As SqlException
                Response.Write("Insert Failed. Error Details are: " & SQLexc.ToString())
            End Try
        End Sub



   
   public sub removec32rec()
		    Dim strConnection As String
			 Dim sqlConn As SqlConnection
		    Dim sqlCmd As SqlCommand
			 Dim ip As String = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
         If ip = "" Then
             ip = Request.ServerVariables("REMOTE_ADDR")
         End If
		
			  	Dim strSql as String = "delete from tbL_c32process where c32p_ipno='" & ip & "' and convert(varchar(20),c32p_date,101) = convert(varchar(20), getdate(),101)"
		 		     Try
		           strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
		           sqlConn = New SqlConnection(strConnection)
		           sqlCmd = New SqlCommand(strSql, sqlConn)
		           
		           sqlConn.Open()		           
		           Dim Sqldr as SqlDataReader = sqlCmd.ExecuteReader	          		           
				       If Sqldr.Read() Then
		               
		               end if
               
               
			   Catch ex As Exception
		    		Response.Write(ex.ToString())
		    		exit sub
		    	Finally
		       	sqlConn.Close()
			 	End Try
    end sub
    public sub getleadno()
		    Dim strConnection As String
			 Dim sqlConn As SqlConnection
		    Dim sqlCmd As SqlCommand
			 Dim ip As String = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
         If ip = "" Then
             ip = Request.ServerVariables("REMOTE_ADDR")
         End If
		
			  	Dim strSql as String = "select * from tbL_c32process where c32p_ipno='" & ip & "' and convert(varchar(20),c32p_date,101) = convert(varchar(20), getdate(),101)"
		 		     Try
		           strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
		           sqlConn = New SqlConnection(strConnection)
		           sqlCmd = New SqlCommand(strSql, sqlConn)
		           
		           sqlConn.Open()		           
		           Dim Sqldr as SqlDataReader = sqlCmd.ExecuteReader
		            If Sqldr.Read() Then
		               	session("leadno")= sqldr("c32p_url")
		               	ldno.text = sqldr("c32p_url")
		               	session("emailto")=sqldr("c32p_uid")
		               end if	          		           
		      
			   Catch ex As Exception
		    		Response.Write(ex.ToString())
		    		exit sub
		    	Finally
		       	sqlConn.Close()
			 	End Try
    end sub
    
    public sub sendleadinfo()
      	Dim strConnection As String
			 Dim sqlConn As SqlConnection
		    Dim sqlCmd As SqlCommand
			 
		
			  	Dim strSql as String = "select * from tbL_leads where tbl_leads_pk='" & session("leadno") & "'"
		 		     Try
		           strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
		           sqlConn = New SqlConnection(strConnection)
		           sqlCmd = New SqlCommand(strSql, sqlConn)
		           
		           sqlConn.Open()		           
		           Dim Sqldr as SqlDataReader = sqlCmd.ExecuteReader
		            If Sqldr.Read() Then
		               	if sqldr("ld_lname") isnot dbnull.value and sqldr("ld_fname") isnot dbnull.value then
		               		session("leadname")= sqldr("ld_fname") & " " & sqldr("ld_lname")
		               	elseif  sqldr("ld_lname") isnot dbnull.value then
		               		session("leadname")=sqldr("ld_lname")
		               	elseif sqldr("ld_lname") isnot dbnull.value then
		               		session("leadname")=sqldr("ld_fname") 
		               	else
		               		session("leadname")=""
		               	end if
		               	if sqldr("ld_address") isnot dbnull.value then
		               		session("leadaddress")=sqldr("ld_address")
		               	else
		               		session("leadaddress")=""
		               	end if	
		               	if sqldr("ld_city") isnot dbnull.value then
		               		session("leadcity")=sqldr("ld_city")
		               	else
		               		session("leadcity")=""
		               	end if
		               	if sqldr("ld_state") isnot dbnull.value then
		               		session("leadstate")=sqldr("ld_state")
		               	else
		               		session("leadstate")=""
		               	end if
		               	if sqldr("ld_zip") isnot dbnull.value then
		               		session("leadzip")=sqldr("ld_zip")
		               	else
		               		session("leadzip")=""
		               	end if
		               	if sqldr("ld_hphone") isnot dbnull.value then
		               		session("leadphone1")=sqldr("ld_hphone")
		               	else 
		               		session("leadphone1")=""
		               	end if
		               	if sqldr("ld_cphone") isnot dbnull.value then
		               		session("leadphone2")=sqldr("ld_cphone")
		               	else
		               		session("leadphone2")=""
		               	end if
		               	if sqldr("ld_email") isnot dbnull.value then
		               		session("leademail")=sqldr("ld_email")
		               	else 
		               		session("leademail")=""
		               	end if
		               	if sqldr("ld_email2") isnot dbnull.value then
		               		session("leademail2")=sqldr("ld_email2")
		               	else
		               		session("leademail2")=""
		               	end if
		               	if sqldr("ld_notes") isnot dbnull.value then
		               		 session("leadnotes")=sqldr("ld_notes")
		               	else
		               		 session("leadnotes")=""
		               	end if
		               end if	          		           
		      
			   Catch ex As Exception
		    		Response.Write(ex.ToString())
		    		exit sub
		    	Finally
		       	sqlConn.Close()
			 	End Try
			 	
			 	sendemail()
			 	
    end sub
    
    public sub updateleadstat()
    	Dim strConnection As String
			 Dim sqlConn As SqlConnection
		    Dim sqlCmd As SqlCommand
			 
		
			  	Dim strSql as String = "update  tbL_leads set ld_pstatus='Lead Sold' where tbl_leads_pk='" & session("leadno") & "'"
		 		     Try
		           strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
		           sqlConn = New SqlConnection(strConnection)
		           sqlCmd = New SqlCommand(strSql, sqlConn)
		           
		           sqlConn.Open()		           
		           Dim Sqldr as SqlDataReader = sqlCmd.ExecuteReader
		            
    
			   Catch ex As Exception
		    		Response.Write(ex.ToString())
		    		exit sub
		    	Finally
		       	sqlConn.Close()
			 	End Try
    
    end sub
    
     public function getagentid(id as string) as string
    	Dim strConnection As String
			 Dim sqlConn As SqlConnection
		    Dim sqlCmd As SqlCommand
			 
		
			  	Dim strSql as String = "select ld_assignedtouid  from tbl_leads where tbl_leads_pk='" & session("leadno") & "'"
		 		     Try
		           strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
		           sqlConn = New SqlConnection(strConnection)
		           sqlCmd = New SqlCommand(strSql, sqlConn)
		           
		           sqlConn.Open()		           
		           Dim Sqldr as SqlDataReader = sqlCmd.ExecuteReader
		            If Sqldr.Read() Then
		           		return sqldr("ld_assignedtouid")
		           	else
		           		return "sales@webmagicportal.com"
		           end if
		           
    
			   Catch ex As Exception
		    		Response.Write(ex.ToString())
		    		
		    	Finally
		       	sqlConn.Close()
			 	End Try
    
    end function
    
    
    
    Sub sendemail()
            Dim RightNowAdd As datetime = datetime.now

            Dim mail As New MailMessage()

            'Set the properties - send the email to the person who filled out the
            mail.From = New MailAddress("Sales@webmagicportal.com")
            ' mail.ReplyTo = New MailAddress(emailreply)
            mail.To.Add(session("emailto"))
            emadd.text = session("emailto")
            mail.Subject = "Purchased Lead Detail Information"
          
            mail.Body = "<table><tr><td><b>Lead Detail</b></td></tr></table><br><table> " 
           	mail.Body = mail.Body + "<tr><td>Name:</td><td>" & session("leadname") & "</td></tr>"
				mail.Body = mail.Body + "<tr><td>Address:</td><td>" & session("leadaddress")  & "</td></tr>"
				mail.Body = mail.Body + "<tr><td>City:</td><td>" & session("leadcity") & "</td></tr>"
				mail.Body = mail.Body + "<tr><td>State:</td><td>" & session("leadstate") & "</td></tr>"
				mail.Body = mail.Body + "<tr><td>Zip:</td><td>" & session("leadzip") & "</td></tr>"
				mail.Body = mail.Body + "<tr><td>Phone 1:</td><td>" & session("leadphone1")  & "</td></tr>"
				mail.Body = mail.Body + "<tr><td>Phone 2:</td><td>" & session("leadphone2") & "</td></tr>"
				mail.Body = mail.Body + "<tr><td>Email 1:</td><td>" & session("leademail") & "</td></tr>"
				mail.Body = mail.Body + "<tr><td>Email 2:</td><td>" & session("leademail2") & "</td></tr></table>"
				
				mail.Body = mail.Body + "<table><tr><td>Notes:</td></tr><tr><td>" &  session("leadnotes") & "</td></tr>"
				
				mail.Body = mail.Body + "<tr><td colspan=2>Thank you for letting Web Magic Portal work for you!</td></tr>"
				mail.Body = mail.Body + "</table>"
          		 mail.IsBodyHtml = True
            'send the message
            Dim smtp As New SmtpClient("smtp.comcast.net")
            smtp.Send(mail)

        End Sub
    
		
End Class
end namespace