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
Imports System.Data.SqlClient
Imports System.Web.Security
Imports System.Configuration
Imports System.IO

namespace setup
	public class gsetup 
        Inherits Page

        Public pnl_ExistingUser, pnl_NewUser As Panel
        
        Public userid, pass As Label
       

	   
        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load
            If Not (Page.IsPostBack) Then
            	clearsessions()
                If checkuserexists() Then
                    'Existing User
                    pnl_ExistingUser.Visible = True
                    if 	session("ustat")="New" then
                    	
                    		updateustatus()
                    		 insertdefaultresp()
                    		createimgdirA()
                    		updatewmleadA()
                    end if
                    if 	session("ustat")="Trial" then
                    		updateustatus()
                    		
                    		convertuser()
                    end if
                    loginuserin()
                Else
                    'New User
                    'If Not checkcompanyexists() Then
                        insertcompany()
                        getcopk()
                        session("urole") = "Admin"
                    'Else
                        'session("urole") = "user"
                    'End If
                    
                    pnl_ExistingUser.Visible = False
                    pnl_NewUser.Visible = True
                    updateLeadUT(session("urole"))
                    getlogininfo()
                    loginuserin()
                    insertDFLTBranding()
                    'insertdefaultresp()
                    createimgdir()
                    updatewmlead()
                End If
            End If
        End Sub
        
        public sub clearsessions()
        
        	session("newleadno") = ""
        	session("newcopk") = ""
        	session("vlname") = ""
        	session("vfname") = ""
        	session("vhphone") = ""
        	session("vcphone") = ""
        	session("vaddress") = ""
        	session("vcity") = ""
        	session("vstate") = ""
        	session("vzip") = ""
        	session("vemail") = ""
        	session("urole") = ""
        	session("ustat") = ""      	
        	
        	
        end sub
        
        
        Sub getuserinfo()
            Dim strSql As String = "SELECT * from tbl_users where UID='" & userid.Text & "'"
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    	session("vlname") = Sqldr("Lname")
                    session("vfname") = Sqldr("Fname")
                    if Sqldr("bphone") isnot dbnull.value then
                    			session("vhphone") = Sqldr("bphone")
                    else
                    			session("vhphone") = ""
                    end if
                  if Sqldr("cphone") isnot dbnull.value then
                   session("vcphone") = Sqldr("cphone")
                  else
                   session("vcphone") = ""
                  end if
                  if Sqldr("Address1") isnot dbnull.value then
                   session("vaddress") = Sqldr("Address1")
                  else
                   session("vaddress") = ""
                  end if
                  if Sqldr("City") isnot dbnull.value then
                   	session("vcity") = Sqldr("City")
                  else
                  	session("vcity") = ""
                  end if
                  if Sqldr("State") isnot dbnull.value then
                   	 session("vstate") = Sqldr("State")
                  else
                  	 session("vstate") = ""
                  end if
                  if Sqldr("Zip") isnot dbnull.value then
                   	 session("vzip") = Sqldr("Zip")
                  else
                  	 session("vzip") = ""
                  end if
                  if Sqldr("email") isnot dbnull.value then
                   	 session("vemail") = Sqldr("email")
                  else
                  	 session("vemail") = ""
                  end if  

                End If

            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try

        End Sub
        Sub getuserinfoA()
            Dim strSql As String = "SELECT * from tbl_users where UID='" & session("userid") & "'"
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    	session("vlname") = Sqldr("Lname")
                    session("vfname") = Sqldr("Fname")
                    if Sqldr("bphone") isnot dbnull.value then
                    			session("vhphone") = Sqldr("bphone")
                    else
                    			session("vhphone") = ""
                    end if
                  if Sqldr("cphone") isnot dbnull.value then
                   session("vcphone") = Sqldr("cphone")
                  else
                   session("vcphone") = ""
                  end if
                  if Sqldr("Address1") isnot dbnull.value then
                   session("vaddress") = Sqldr("Address1")
                  else
                   session("vaddress") = ""
                  end if
                  if Sqldr("City") isnot dbnull.value then
                   	session("vcity") = Sqldr("City")
                  else
                  	session("vcity") = ""
                  end if
                  if Sqldr("State") isnot dbnull.value then
                   	 session("vstate") = Sqldr("State")
                  else
                  	 session("vstate") = ""
                  end if
                  if Sqldr("Zip") isnot dbnull.value then
                   	 session("vzip") = Sqldr("Zip")
                  else
                  	 session("vzip") = ""
                  end if
                  if Sqldr("email") isnot dbnull.value then
                   	 session("vemail") = Sqldr("email")
                  else
                  	 session("vemail") = ""
                  end if  

                End If

            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try

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
        Sub updatewmlead()
            getnewleadno()
            getuserinfo()
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
            prmlfname.Value = session("vfname")
            myCommandADD.Parameters.Add(prmlfname)

            Dim prmllname As New SqlParameter("@l_lname", SqlDbType.VarChar, 50)
            prmllname.Value = 	session("vlname")
            myCommandADD.Parameters.Add(prmllname)

            Dim prmhphone As New SqlParameter("@l_hphone", SqlDbType.VarChar, 50)
            prmhphone.Value = 	session("vhphone")
            myCommandADD.Parameters.Add(prmhphone)

            Dim prmcphone As New SqlParameter("@l_cphone", SqlDbType.VarChar, 50)
            prmcphone.Value = session("vcphone")
            myCommandADD.Parameters.Add(prmcphone)

            Dim prmaddress As New SqlParameter("@l_address", SqlDbType.VarChar, 30)
            prmaddress.Value = session("vaddress")
            myCommandADD.Parameters.Add(prmaddress)

            Dim prmcity As New SqlParameter("@l_city", SqlDbType.VarChar, 30)
            prmcity.Value = session("vcity")
            myCommandADD.Parameters.Add(prmcity)

            Dim prmstate As New SqlParameter("@l_state", SqlDbType.VarChar, 2)
            prmstate.Value = session("vstate")
            myCommandADD.Parameters.Add(prmstate)

            Dim prmzip As New SqlParameter("@l_zip", SqlDbType.VarChar, 50)
            prmzip.Value = session("vzip")
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
            prmleadtype.Value = "New Subscriber"
            myCommandADD.Parameters.Add(prmleadtype)

            Dim prmnotes As New SqlParameter("@l_notes", SqlDbType.Text)
            prmnotes.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmnotes)

            Dim prmemail As New SqlParameter("@l_email", SqlDbType.VarChar, 50)
            prmemail.Value = session("vemail")
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

            Dim prmprogram As New SqlParameter("@ld_program", SqlDbType.VarChar, 20)
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
        Sub createimgdir()
            Directory.CreateDirectory(System.Configuration.ConfigurationManager.AppSettings("CurrentIMGDirectory") & Left(userid.Text, Len(userid.Text) - 4) & "IMG")

        End Sub
        Sub createimgdirA()
            Directory.CreateDirectory(System.Configuration.ConfigurationManager.AppSettings("CurrentIMGDirectory") & Left(session("userid"), Len(session("userid")) - 4) & "IMG")

        End Sub
        
        
        Sub ExistContinue(ByVal sender As Object, ByVal e As EventArgs)
            Dim newCookie As HttpCookie = New HttpCookie("Gimp-UI")
            newCookie.Values.Add("UID", session("userid"))
            newCookie.Expires = #12/31/2009#
            Response.Cookies.Add(newCookie)

            'Response.Write(Request.Cookies("Gimp-UI")("UID"))
            Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("CurrentAppURL") & "/login.aspx?id=" & Session("userid") & "&source=NP")
        End Sub
		
        Public Sub updateLeadUT(ByVal rl As String)
            Dim CartCookieID As String = Request.Cookies("Cart32-CHOICEONE").Value
            Dim CartCustCode As String = Request.Cookies("C32-CHOICEONE-CustCode").Value

            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String = "sp_addleaduser"

            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

            Dim prmuid As New SqlParameter("@uid", SqlDbType.int)
            prmuid.Value = CartCustCode
            myCommandADD.Parameters.Add(prmuid)

            Dim prmrole As New SqlParameter("@prmrole", SqlDbType.varchar, 50)
            If rl = "Admin" Then
                prmrole.Value = "Administrator"
            Else
                prmrole.Value = "user"
            End If
            myCommandADD.Parameters.Add(prmrole)
            
            Dim prmcopk As New SqlParameter("@prmcopk", SqlDbType.varchar, 50)
            prmcopk.Value = session("newcopk")
            myCommandADD.Parameters.Add(prmcopk)
           
            


            Try
                myConnectionADD.Open()
                myCommandADD.ExecuteNonQuery()

            Catch SQLexc As SqlException
                Response.Write("Failed. Error Details are: " & SQLexc.ToString())
            Finally
                myConnectionADD.Close()
            End Try

        End Sub
        Public Sub insertcompany()
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim CartCustCode As String = Request.Cookies("C32-CHOICEONE-CustCode").Value
            Dim strSql As String = "insert into tbl_company (co_name, co_package, co_status, co_customerid) " _
                            & "select rtrim(ltrim(b.company)), c.item,'Active',rtrim(ltrim(b.userid)) from cart32.dbo.users b " _
                            & "join cart32.dbo.orders a on a.userid = b.userid  " _
                            & "join cart32.dbo.orditems c on c.orderno=a.orderno " _
                            & "where a.custcode='" & CartCustCode & "'"

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
        
        public sub getcopk()
        		Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select max(co_tbl_pk) as 'copk' from tbl_company"

            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)

                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
						If Sqldr.read() Then
                    session("newcopk") = Sqldr("copk")
                	End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
        end sub
        
        Public Sub insertdefaultresp()
       		Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim CartCustCode As String = Request.Cookies("C32-CHOICEONE-CustCode").Value
            Dim strSql As String = "insert into tbl_adbranding (br_uid_fk, br_name, br_description, br_showlogo,br_text1,br_text2,br_sendemail,br_getemail) " _
                            & "select x.uid, 'Default','Standard Setting','N','Please complete the following information and click Submit','Thank You!  Your request will be processed shortly.','N','N' from cart32.dbo.users b " _
                            & "join cart32.dbo.orders a on a.userid = b.userid  " _
                            & "join cart32.dbo.orditems c on c.orderno=a.orderno " _ 
                            & "join tbl_users x on cart32id=b.userid " _                          
                            & "where a.custcode='" & CartCustCode & "'"

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
	            prmuid.Value = session("userid")
	            myCommandADD.Parameters.Add(prmuid)
	
	            Dim prmadno As New SqlParameter("@adno", SqlDbType.VarChar, 50)
	            prmadno.Value = DBNull.Value
	            myCommandADD.Parameters.Add(prmadno)
	
	            Dim prmcompany As New SqlParameter("@company", SqlDbType.VarChar, 50)
	            prmcompany.Value = session("company")
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
        
        Public Sub updateustatus()
        		Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "update tbl_users set Status='Active', " _
								& "brandingPurchased = case when(c.partno='ProductB') then 'Yes' else 'No' end, " _
								& "sub_expiredate = DATEADD(DAY,C.qty*30,A.orderdate), package=c.item " _
								& "from cart32.dbo.users b " _ 
								& "join cart32.dbo.orders a on a.userid = b.userid " _
								& "join cart32.dbo.orditems c on c.orderno=a.orderno " _                           
								& "where UID='" & session("userid") & "' " _
								& "and cart32id = b.userid"
            '"update tbl_users set status='Active' where UID='" & session("userid") & "'"

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
            
            updatecostat()
        end sub
        Public Sub  updatecostat()
        		Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "update tbl_company set Co_customerid=cart32id " _
								& "from tbl_users  " _                          
								& "where UID='" & session("userid") & "'"

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
        
        Public Sub convertuser()
        
        end sub
        
        Public Sub getlogininfo()
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim CartCustCode As String = Request.Cookies("C32-CHOICEONE-CustCode").Value
            Dim strSql As String = "SELECT *,a.uid as 'uid',a.password as 'password',a.type as 'type', a.company as 'company',a.role as 'role', a.industry as 'industry', a.fname + ' ' + a.lname as 'agentname',a.cart32id from tbl_users a join cart32.dbo.orders z on z.userid=a.cart32id where z.custcode='" & CartCustCode & "'"

            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)


                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    Session("userid") = Sqldr("uid")
                    userid.Text = Sqldr("uid")
                    pass.Text = Sqldr("password")
                    Session("c32id") = Sqldr("cart32id")
                    Session("sysaccess") = Sqldr("type")
                    Session("company") = Sqldr("Company")
                    Session("role") = Sqldr("role")
                    Session("industry") = Sqldr("industry")
                    Session("Agentname") = Sqldr("agentname")
                      Session("AgentPK") = Sqldr("users_tbl_PK")
                  
                    If Sqldr("package") IsNot DBNull.Value Then
                        Session("package") = Sqldr("package")
                    Else
                        Session("package") = "Basic"
                    End If
							Session("ustat") = Sqldr("status")
                    Session("loggedin") = "true"
                    Session("s_userloggedin") = "true"
                     If Sqldr("company_pk") IsNot DBNull.Value Then
                        Session("company_pk") = Sqldr("company_pk")
                    Else
                        Session("company_pk") = "0"
                    End If
                    Session("ustat") = Sqldr("status")
                   session("uimgdir")=  Left(Sqldr("uid"), Len(Sqldr("uid")) - 4) & "IMG"
                    
                    Dim MyCookie As New HttpCookie("bb")
					      MyCookie.Value =   Left(Sqldr("uid"), Len(Sqldr("uid")) - 4) & "IMG"
					      Response.Cookies.Add(MyCookie)   
                    

                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
        End Sub
        Public Function checkcompanyexists() As Boolean
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim CartCustCode As String = Request.Cookies("C32-CHOICEONE-CustCode").Value
            Dim strSql As String = "select co_name from tbl_company where co_name in ( " _
                    & "SELECT b.company from cart32.dbo.users b " _
                    & "join cart32.dbo.orders a on a.userid = b.userid " _
                    & "where custcode='" & CartCustCode & "')"
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


        Public Function checkuserexists() As Boolean
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim CartCustCode As String = Request.Cookies("C32-CHOICEONE-CustCode").Value
            Dim strSql As String = "SELECT * from dbo.tbl_users " _
                & "join cart32.dbo.orders on userid = cart32id " _
                & "where custcode='" & CartCustCode & "'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)

                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    Session("userid") = Sqldr("uid")
                    Session("sysaccess") = Sqldr("type")
                    Session("company") = Sqldr("Company")
                    Session("role") = Sqldr("role")
                    Session("industry") = Sqldr("industry")
                    Session("Agentname") = Sqldr("fname") & " " & Sqldr("lname")
                    If Sqldr("package") IsNot DBNull.Value Then
                        Session("package") = Sqldr("package")
                    Else
                        Session("package") = "Basic"
                    End If
							
						  	session("ustat") = Sqldr("Status")
			
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

        Sub loginuserin()
                Dim ip As String = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
            If ip = "" Then
                ip = Request.ServerVariables("REMOTE_ADDR")
            End If
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "insert dbo.tbl_currentlogons (lg_uid,lg_logindate,lg_ipnumber) values ('" & Session("userid") & "', getdate(), '" & ip & "')"
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
        
        Sub updatewmleadA()
            getnewleadno()
            getuserinfoA()
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
            prmlfname.Value = session("vfname")
            myCommandADD.Parameters.Add(prmlfname)

            Dim prmllname As New SqlParameter("@l_lname", SqlDbType.VarChar, 50)
            prmllname.Value = 	session("vlname")
            myCommandADD.Parameters.Add(prmllname)

            Dim prmhphone As New SqlParameter("@l_hphone", SqlDbType.VarChar, 50)
            prmhphone.Value = 	session("vhphone")
            myCommandADD.Parameters.Add(prmhphone)

            Dim prmcphone As New SqlParameter("@l_cphone", SqlDbType.VarChar, 50)
            prmcphone.Value = session("vcphone")
            myCommandADD.Parameters.Add(prmcphone)

            Dim prmaddress As New SqlParameter("@l_address", SqlDbType.VarChar, 30)
            prmaddress.Value = session("vaddress")
            myCommandADD.Parameters.Add(prmaddress)

            Dim prmcity As New SqlParameter("@l_city", SqlDbType.VarChar, 30)
            prmcity.Value = session("vcity")
            myCommandADD.Parameters.Add(prmcity)

            Dim prmstate As New SqlParameter("@l_state", SqlDbType.VarChar, 2)
            prmstate.Value = session("vstate")
            myCommandADD.Parameters.Add(prmstate)

            Dim prmzip As New SqlParameter("@l_zip", SqlDbType.VarChar, 50)
            prmzip.Value = session("vzip")
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
            prmleadtype.Value = "New Subscriber"
            myCommandADD.Parameters.Add(prmleadtype)

            Dim prmnotes As New SqlParameter("@l_notes", SqlDbType.Text)
            prmnotes.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmnotes)

            Dim prmemail As New SqlParameter("@l_email", SqlDbType.VarChar, 50)
            prmemail.Value = session("vemail")
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

            Dim prmprogram As New SqlParameter("@ld_program", SqlDbType.VarChar, 20)
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

            Try
                myConnectionADD.Open()
                myCommandADD.ExecuteNonQuery()
                myConnectionADD.Close()
            Catch SQLexc As SqlException
                Response.Write("Insert Failed. Error Details are: " & SQLexc.ToString())
            End Try

        End Sub
    End Class
end namespace