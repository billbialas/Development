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
    Public Class leaddropdowns
        Inherits PageTemplate
        Public lstleadtypes, leadstatus, leadsource, leadprograms, tasks, mktprg As ListBox
        Public pnldropmain,pnladdleadtype, confirmremove, pnlldstatwarn, pnladdleadstatus, pnlldsourcewarn, pnlleadsourceadd, pnlldprogramswarn, pnlleadprogramsadd As Panel
        Public pnltaskwarn, pnladdtasktype, pnladdmkttype,pnlmktwarn As Panel
        Public newmkttype, newleadtype, newleadstatus, newleadsource, newleadprograms, newtasktype,venuname,venuecode,venueurl,venunameorg As TextBox
        Public rmvmkt,ldtyperemove, Removeleadstat, ldsource, ldprograms, deltasktypecan, savettype, rmvtask As Button
        public ddvenonline as DropDownList
		  public acctsetup,privateven,htmltext as checkbox
		  public lsnotes,lsinst 
		  
		  public lblnovensel,lblnoeditvenue as label
			
        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

            If Not (Page.IsPostBack) Then
            	clearsessions()
                bindleadtypes()
                bindleadstatus()
                bindleadsource()
                bindleadprograms()
                bindtasktypes()
                bindmarkettypes()
            End If
            pagesetup()

        End Sub
        sub clearsessions()
        		session("selectedv")=""
        		
      
        
        end sub
              
         Public Sub btn_showhelp(ByVal Source As Object, ByVal e As ImageClickEventArgs)
      		Response.Write("<script>window.open" & _
                "('" & System.Configuration.ConfigurationManager.AppSettings("CurrentappURL") & "/help/help_dp.html','_new','width=1000,height=650,resizable=1,scrollbars=1');</script>")
     
			end sub

        Sub bindleadtypes()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='leadtype' and (x_company='All' or x_uid='" & Session("userid") & "' or x_company='" & Session("company_pk") & "' ) order by x_descr", myConnection)
            'or x_company='" & Session("company") & "' 
            Try
                myConnection.Open()
                lstleadtypes.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
                lstleadtypes.DataBind()
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            End Try
            'lstleadtypes.SelectedIndex = 0

        End Sub
        
        Sub bindleadstatus()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='contactstatus' and (x_company='All'  or x_uid='" & Session("userid") & "'or x_company='" & Session("company_pk") & "' ) order by x_descr", myConnection)
            'or x_company='" & Session("company") & "'
            Try
                myConnection.Open()
                leadstatus.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
                leadstatus.DataBind()
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            End Try
            'leadstatus.SelectedIndex = 0

        End Sub
        
        Sub bindleadsource()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='leadsource' and (x_company='All'  or x_uid='" & Session("userid") & "' or x_company='" & Session("company_pk") & "' ) order by x_descr", myConnection)
            'or x_company='" & Session("company") & "'
            Try
                myConnection.Open()
                leadsource.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
                leadsource.DataBind()
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            End Try
            'leadsource.SelectedIndex = 0

        End Sub
       
        Sub bindleadprograms()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='leadprogram' and (x_company='All' or x_uid='" & Session("userid") & "' or x_company='" & Session("company_pk") & "') order by x_descr", myConnection)
            'or x_company='" & Session("company") & "' 
            Try
                myConnection.Open()
                leadprograms.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
                leadprograms.DataBind()
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            End Try
            'leadprograms.SelectedIndex = 0
        End Sub
       
        Sub bindtasktypes()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='tasktype' and (x_company='All'  or x_uid='" & Session("userid") & "' or x_company='" & Session("company_pk") & "') order by x_descr", myConnection)
            'or x_company='" & Session("company") & "'
            Try
                myConnection.Open()
                tasks.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
                tasks.DataBind()
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            End Try
            'leadprograms.SelectedIndex = 0
        End Sub
        
        Sub bindmarkettypes()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As New SqlCommand("Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='marketprogram' and (x_company='All'  or x_uid='" & Session("userid") & "' or x_company='" & Session("company_pk") & "') order by x_descr", myConnection)
            'or x_company='" & Session("company") & "'
            Try
                myConnection.Open()
                mktprg.DataSource = myCommand.ExecuteReader(CommandBehavior.CloseConnection)
                mktprg.DataBind()
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            End Try
            'leadprograms.SelectedIndex = 0
        End Sub
        
        
        Sub Addleadtype(ByVal sender As Object, ByVal e As EventArgs)
            newleadtype.Text = ""
            pnladdleadtype.Visible = True
        End Sub
        Sub Addleadstatus(ByVal sender As Object, ByVal e As EventArgs)
            newleadstatus.Text = ""
            pnladdleadstatus.Visible = True
        End Sub
        Sub Addleadsource(ByVal sender As Object, ByVal e As EventArgs)
           	lblnoeditvenue.visible=false
         	lblnovensel.visible=false
         	session("LSstat")="add"
            pnlleadsourceadd.Visible = True
            pnldropmain.visible=false
            venuname.text=""
         	venuecode.text=""
         	venueurl.text=""
         	acctsetup.checked=false
         	privateven.checked=true
         	if session("role")="GOD" then         		
         		privateven.enabled=true
         	else
         		privateven.enabled=false
         	end if
         	htmltext.checked=false
         	lsnotes.content=""
         	lsinst.content=""
        End Sub
        Sub Addleadprograms(ByVal sender As Object, ByVal e As EventArgs)
            newleadprograms.Text = ""
            pnlleadprogramsadd.Visible = True
        End Sub
        Sub Addtasktype(ByVal sender As Object, ByVal e As EventArgs)
            newtasktype.Text = ""
            pnladdtasktype.Visible = True
        End Sub
        Sub Addmkttype(ByVal sender As Object, ByVal e As EventArgs)
            newmkttype.Text = ""
            pnladdmkttype.Visible = True
        End Sub
        
        
        Sub Saveleadtype(ByVal sender As Object, ByVal e As EventArgs)
            If checkifcodeexists(newleadtype.Text, "leadtype") = False Then

                Dim strConnection As String
                Dim sqlConn As SqlConnection
                Dim sqlCmd As SqlCommand

                Dim strSql As String
                strSql = "insert into dbo.tbl_xwalk  (x_type,x_descr,x_UID,x_company) values('leadtype','" & newleadtype.Text & "','" & Session("userid") & "','" & Session("company_pk") & "')"

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
                pnladdleadtype.Visible = False
                bindleadtypes()

            Else
                newleadtype.Text = "EXISTS"
            End If
        End Sub
        Sub saveleadstatus(ByVal sender As Object, ByVal e As EventArgs)
            If checkifcodeexists(newleadstatus.Text, "contactstatus") = False Then

                Dim strConnection As String
                Dim sqlConn As SqlConnection
                Dim sqlCmd As SqlCommand

                Dim strSql As String
                strSql = "insert into dbo.tbl_xwalk  (x_type,x_descr,x_UID,x_company) values('contactstatus','" & newleadstatus.Text & "','" & Session("userid") & "','" & Session("company_pk") & "')"

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
                pnladdleadstatus.Visible = False
                bindleadstatus()

            Else
                newleadstatus.Text = "EXISTS"
            End If
        End Sub
        Sub saveleadsource(ByVal sender As Object, ByVal e As EventArgs)
            If checkifcodeexists(newleadsource.Text, "leadsource") = False Then

                Dim strConnection As String
                Dim sqlConn As SqlConnection
                Dim sqlCmd As SqlCommand

                Dim strSql As String
                strSql = "insert into dbo.tbl_xwalk  (x_type,x_descr,x_UID,x_company) values('leadsource','" & newleadsource.Text & "','" & Session("userid") & "','" & Session("company_pk") & "')"

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
                pnlleadsourceadd.Visible = False
                bindleadsource()

            Else
                newleadsource.Text = "EXISTS"
            End If
        End Sub
          Sub saveleadprograms(ByVal sender As Object, ByVal e As EventArgs)
            If checkifcodeexists(newleadprograms.Text, "leadprogram") = False Then

                Dim strConnection As String
                Dim sqlConn As SqlConnection
                Dim sqlCmd As SqlCommand

                Dim strSql As String
                strSql = "insert into dbo.tbl_xwalk  (x_type,x_descr,x_UID,x_company) values('leadprogram','" & newleadprograms.Text & "','" & Session("userid") & "','" & Session("company_pk") & "')"

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
                pnlleadprogramsadd.Visible = False
                bindleadprograms()

            Else
                newleadprograms.Text = "EXISTS"
            End If
        End Sub
        Sub savetasktype(ByVal sender As Object, ByVal e As EventArgs)
            If checkifcodeexists(newleadprograms.Text, "tasktype") = False Then

                Dim strConnection As String
                Dim sqlConn As SqlConnection
                Dim sqlCmd As SqlCommand

                Dim strSql As String
                strSql = "insert into dbo.tbl_xwalk  (x_type,x_descr,x_UID,x_company) values('tasktype','" & newtasktype.Text & "','" & Session("userid") & "','" & Session("company_pk") & "')"

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
                pnladdtasktype.Visible = False
                bindtasktypes()

            Else
                newtasktype.Text = "EXISTS"
            End If
        End Sub
        
         Sub savemkttype(ByVal sender As Object, ByVal e As EventArgs)
            If checkifcodeexists(newmkttype.Text, "marketprogram") = False Then

                Dim strConnection As String
                Dim sqlConn As SqlConnection
                Dim sqlCmd As SqlCommand

                Dim strSql As String
                strSql = "insert into dbo.tbl_xwalk  (x_type,x_descr,x_UID,x_company) values('marketprogram','" & newmkttype.Text & "','" & Session("userid") & "','" & Session("company_pk") & "')"

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
                pnladdmkttype.Visible = False
                bindmarkettypes()

            Else
                newmkttype.Text = "EXISTS"
            End If
        End Sub

        Public Function checkifcodeexists(ByVal code As String, ByVal type As String) As Boolean

            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select * from tbl_xwalk where x_type='" & type & "' and x_descr='" & code & "' and (x_company='All' or x_company='" & Session("company_pk") & "' or x_uid='" & Session("userid") & "')"

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

        Sub leadtypecommit(ByVal sender As Object, ByVal e As EventArgs)
            If (lstleadtypes.SelectedItem.Text = "Deleted" Or lstleadtypes.SelectedItem.Text = "Unknown" Or lstleadtypes.SelectedItem.Text = "All" Or lstleadtypes.SelectedItem.Text Is Nothing Or lstleadtypes.SelectedItem.Text = "") Then

            Else
                If checkifleadattached(lstleadtypes.SelectedItem.Text, "leadtype") Then

                    confirmremove.Visible = True
                    ldtyperemove.Visible = False
                Else

                    confirmremove.Visible = False
                    ldtyperemove.Visible = True
                    leadtyperemoveNOBT()
                End If
            End If
        End Sub
        Sub leadstatcommit(ByVal sender As Object, ByVal e As EventArgs)

            If (leadstatus.SelectedItem.Text = "Deleted" Or leadstatus.SelectedItem.Text = "Unknown" Or leadstatus.SelectedItem.Text = "All" Or leadstatus.SelectedItem.Text Is Nothing) Then

            Else
                If checkifleadattached(leadstatus.SelectedItem.Text, "contactstatus") Then
                    pnlldstatwarn.Visible = True
                    Removeleadstat.Visible = False
                Else
                    pnlldstatwarn.Visible = False
                    Removeleadstat.Visible = True
                    leadstatremoveNOBT()
                End If
            End If
        End Sub
        Sub leadsourcecommit(ByVal sender As Object, ByVal e As EventArgs)
				if checkvenselected() then
         		if (allowedit() or (session("role")="GOD" and session("selectedT")<>"Test Venue" ))  then
	                If leadsource.SelectedItem IsNot Nothing Then
	                    If checkifleadattached(leadsource.SelectedItem.Text, "leadsource") Then
	                        pnlldsourcewarn.Visible = True
	                        ldsource.Visible = False
	                    Else
	                        pnlldsourcewarn.Visible = False
	                        ldsource.Visible = True
	                        leadsourceremoveNOBT()
	                    End If
	                End If
	             else
		          	lblnoeditvenue.visible=true
	            end if
	          else
       			lblnovensel.visible=true
       		end if
           
        End Sub
        Sub leadprogramscommit(ByVal sender As Object, ByVal e As EventArgs)

            If (leadprograms.SelectedItem.Text = "Deleted" Or leadprograms.SelectedItem.Text = "Unknown" Or leadprograms.SelectedItem.Text = "All" Or leadprograms.SelectedItem.Text Is Nothing) Then

            Else
                If checkifleadattached(leadprograms.SelectedItem.Text, "leadprogram") Then
                    pnlldprogramswarn.Visible = True
                    ldprograms.Visible = False
                Else
                    pnlldprogramswarn.Visible = False
                    ldprograms.Visible = True
                    leadprogramsremoveNOBT()
                End If
            End If
        End Sub
        Sub tasktypecommit(ByVal sender As Object, ByVal e As EventArgs)

            If (tasks.SelectedItem.Text = "Mail" Or tasks.SelectedItem.Text = "Email" Or tasks.SelectedItem.Text Is Nothing Or tasks.SelectedItem.Text = "Phone Call" Or tasks.SelectedItem.Text = "Other") Then

            Else

                pnltaskwarn.Visible = True
                rmvtask.Visible = False

            End If
        End Sub
        Sub mkttypecommit(ByVal sender As Object, ByVal e As EventArgs)

            If (mktprg.SelectedItem.Text = "Deleted" Or mktprg.SelectedItem.Text = "Unknown" Or mktprg.SelectedItem.Text = "All" Or mktprg.SelectedItem.Text Is Nothing) Then

            Else
					If checkifleadattached(mktprg.SelectedItem.Text, "marketprogram") Then
                    pnlmktwarn.Visible = True
                    rmvmkt.Visible = False
                Else
                    pnlmktwarn.Visible = False
                    rmvmkt.Visible = True
                    mkttyperemoveNOBT()
                End If

            End If
        End Sub

        Sub leadtyperemove(ByVal sender As Object, ByVal e As EventArgs)
            If lstleadtypes.SelectedItem IsNot Nothing Then
                Dim i As Integer
                i = 0
                While lstleadtypes.SelectedItem IsNot Nothing
                    updateleads(lstleadtypes.SelectedItem.Text, "leadtype")
                    removetype(lstleadtypes.SelectedItem.Value)

                    lstleadtypes.Items.Remove(lstleadtypes.SelectedItem)
                End While
            End If
            confirmremove.Visible = False
            ldtyperemove.Visible = True

        End Sub
        Sub leadtyperemoveNOBT()
            If lstleadtypes.SelectedItem IsNot Nothing Then
                Dim i As Integer
                i = 0
                While lstleadtypes.SelectedItem IsNot Nothing

                    removetype(lstleadtypes.SelectedItem.Value)
                    lstleadtypes.Items.Remove(lstleadtypes.SelectedItem)
                End While
            End If
            confirmremove.Visible = False
            ldtyperemove.Visible = True
        End Sub

        Sub leadstatremove(ByVal sender As Object, ByVal e As EventArgs)
            If leadstatus.SelectedItem IsNot Nothing Then
                Dim i As Integer
                i = 0
                While leadstatus.SelectedItem IsNot Nothing
                    updateleads(leadstatus.SelectedItem.Text, "contactstatus")
                    removetype(leadstatus.SelectedItem.Value)
                    leadstatus.Items.Remove(leadstatus.SelectedItem)
                End While
            End If
            pnlldstatwarn.Visible = False
            Removeleadstat.Visible = True
        End Sub
        Sub leadstatremoveNOBT()
            If leadstatus.SelectedItem IsNot Nothing Then
                Dim i As Integer
                i = 0
                While leadstatus.SelectedItem IsNot Nothing

                    removetype(leadstatus.SelectedItem.Value)
                    leadstatus.Items.Remove(leadstatus.SelectedItem)
                End While
            End If
            pnlldstatwarn.Visible = False
            Removeleadstat.Visible = True
        End Sub
        Sub leadsourceremove(ByVal sender As Object, ByVal e As EventArgs)
            If leadsource.SelectedItem IsNot Nothing Then
                Dim i As Integer
                i = 0
                While leadsource.SelectedItem IsNot Nothing
                    updateleads(leadsource.SelectedItem.Text, "leadsource")
                    removetype(leadsource.SelectedItem.Value)
                    leadsource.Items.Remove(leadsource.SelectedItem)
                End While
            End If
            pnlldsourcewarn.Visible = False
            ldsource.Visible = True
        End Sub
         
         Sub leadsourceEdit(ByVal sender As Object, ByVal e As EventArgs)
         	lblnoeditvenue.visible=false
         	lblnovensel.visible=false
         	if checkvenselected() then
         		if (allowedit() or (session("role")="GOD" and session("selectedT")<>"Test Venue" )) then
         			pnlleadsourceadd.Visible = True
		            pnldropmain.visible=false
		         	venuname.text=""
		         	venuecode.text=""
		         	venueurl.text=""
		         	acctsetup.checked=false
		         	privateven.checked=false
		         	htmltext.checked=false
		         	lsnotes.content=""
		         	lsinst.content=""
		         	session("LSstat")="edit"
		            pnlleadsourceadd.Visible = True
		            bindLSfields(session("selectedv"))
		            lblnovensel.visible=false
		            lblnoeditvenue.visible=false
		          else
		          	lblnoeditvenue.visible=true
		          end if
	         else
       			lblnovensel.visible=true
       		end if
        End Sub
        
        public function allowedit() as boolean
        	Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String
             strSql = "select * from tbl_xwalk where tbl_xwalk_pk='" & session("selectedv")  & "'"

            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                	if sqldr("x_descr") <> "Test Venue" then
		                    if sqldr("x_company") isnot dbnull.value then 
		                    		if sqldr("x_company") = "All" then
		                    			Return False
		                    		else
		                    			Return true
		                   		 end if
		                    else
		                    		Return true
		                    end if
		             else
		             	Return False
		             end if
                Else
                    Return False
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try

        
        end function 
        
        
        
         sub doinfo(ByVal Source As System.Object, ByVal e As System.EventArgs)
        		venuname.text=""
         	venuecode.text=""
         	venueurl.text=""
         	acctsetup.checked=false
         	privateven.checked=false
         	htmltext.checked=false
         	lsnotes.content=""
         	lsinst.content=""
        		
	        
	            bindLSfields(session("selectedv"))
	           
        
        end sub 
        
         public function checkvenselected() as boolean
        			dim stat as string = "false"
        			dim i as integer
        			dim x as integer				
					x=leadsource.items.count-1				
        			for i=0 to x
						if (leadsource.Items(i).Selected) then
							stat="true"
							session("selectedv") = Convert.ToString(leadsource.items(i).value)
							session("selectedT") = Convert.ToString(leadsource.items(i))
						end if
						
					next
					if stat="true"  then
						return true
					else
						return false
					end if
        
        end function
        
        
          Sub bindLSfields(id as string)

            Dim strUID As String = Session("userid")
         	Dim strSql As String = "SELECT * from tbl_xwalk where tbl_xwalk_pk ='" & id & "'"

            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
						
                    If Sqldr("x_descr") IsNot DBNull.Value Then
                        venuname.Text = Sqldr("x_descr")
                        venunameORG.Text = Sqldr("x_descr")
                    End If
                    If Sqldr("x_ID") IsNot DBNull.Value Then
                        venuecode.Text = Sqldr("x_ID")
                    End If
                    
                    If Sqldr("x_url") IsNot DBNull.Value Then
                        venueurl.Text = Sqldr("x_url")
                    End If
                    
                    If Sqldr("x_notes") IsNot DBNull.Value Then
                        lsnotes.content = Sqldr("x_notes")
                    End If
                    If Sqldr("x_instructions") IsNot DBNull.Value Then
                        lsinst.content = Sqldr("x_instructions")
                    End If
                   
                    If Sqldr("x_online") IsNot DBNull.Value Then
                       ddvenonline.SelectedIndex = ddvenonline.Items.IndexOf(ddvenonline.Items.FindByValue(Sqldr("x_online")))
             
                    End If
                    If Sqldr("x_hasaccounts") IsNot DBNull.Value Then
                       if Sqldr("x_hasaccounts")="Y" then
                       		acctsetup.checked = true
                       else
                       		acctsetup.checked = false
                       end if
             
                    End If
                    
                    If Sqldr("x_company") IsNot DBNull.Value Then
                       		if Sqldr("x_company")="All" then
                       			privateven.checked = false
                       		else
                       			privateven.checked = true
                       		end if
           
                       else
                       		privateven.checked = false
                    End If
                    If Sqldr("x_html") IsNot DBNull.Value Then
                       if Sqldr("x_html")="Y" then
                       		htmltext.checked = true
                       else
                       		htmltext.checked = false
                       end if
             
                    End If
                    
             	End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        End Sub
        
        
        
        Sub leadsourceremoveNOBT()
            If leadsource.SelectedItem IsNot Nothing Then
                Dim i As Integer
                i = 0
                While leadsource.SelectedItem IsNot Nothing

                    removetype(leadsource.SelectedItem.Value)
                    leadsource.Items.Remove(leadsource.SelectedItem)
                End While
            End If
            pnlldsourcewarn.Visible = False
            ldsource.Visible = True
        End Sub
        
         Sub leadprogramsremove(ByVal sender As Object, ByVal e As EventArgs)
            If leadprograms.SelectedItem IsNot Nothing Then
                Dim i As Integer
                i = 0
                While leadprograms.SelectedItem IsNot Nothing
                    updateleads(leadprograms.SelectedItem.Text, "leadprogram")
                    removetype(leadprograms.SelectedItem.Value)
                    leadprograms.Items.Remove(leadprograms.SelectedItem)
                End While
            End If
            pnlldprogramswarn.Visible = False
            ldprograms.Visible = True
        End Sub
        Sub leadprogramsremoveNOBT()
            If leadprograms.SelectedItem IsNot Nothing Then
                Dim i As Integer
                i = 0
                While leadprograms.SelectedItem IsNot Nothing

                    removetype(leadprograms.SelectedItem.Value)
                    leadprograms.Items.Remove(leadprograms.SelectedItem)
                End While
            End If
            pnlldprogramswarn.Visible = False
            ldprograms.Visible = True
        End Sub


        Sub tasktyperemove(ByVal sender As Object, ByVal e As EventArgs)
            If tasks.SelectedItem IsNot Nothing Then
                Dim i As Integer
                i = 0
                While tasks.SelectedItem IsNot Nothing
                    'updateleads(leadprograms.SelectedItem.Text, "leadprogram")
                    removetype(tasks.SelectedItem.Value)
                    tasks.Items.Remove(tasks.SelectedItem)
                End While
            End If
            pnltaskwarn.Visible = False
            rmvtask.Visible = True
        End Sub
        Sub tasktyperemoveNOBT()
            If tasks.SelectedItem IsNot Nothing Then
                Dim i As Integer
                i = 0
                While tasks.SelectedItem IsNot Nothing

                    removetype(tasks.SelectedItem.Value)
                    tasks.Items.Remove(tasks.SelectedItem)
                End While
            End If
            pnltaskwarn.Visible = False
            rmvtask.Visible = True
        End Sub
		  Sub mkttyperemove(ByVal sender As Object, ByVal e As EventArgs)
            If mktprg.SelectedItem IsNot Nothing Then
                Dim i As Integer
                i = 0
                While mktprg.SelectedItem IsNot Nothing
                    updateleads(mktprg.SelectedItem.Text, "marketprogram")
                    removetype(mktprg.SelectedItem.Value)
                    mktprg.Items.Remove(mktprg.SelectedItem)
                End While
            End If
            pnlmktwarn.Visible = False
            rmvmkt.Visible = True
        End Sub
        Sub mkttyperemoveNOBT()
            If mktprg.SelectedItem IsNot Nothing Then
                Dim i As Integer
                i = 0
                While mktprg.SelectedItem IsNot Nothing

                    removetype(mktprg.SelectedItem.Value)
                    mktprg.Items.Remove(mktprg.SelectedItem)
                End While
            End If
            pnlmktwarn.Visible = False
            rmvmkt.Visible = True
        End Sub
        Sub leadtypecancel(ByVal sender As Object, ByVal e As EventArgs)
            confirmremove.visible = False
            ldtyperemove.Visible = True
        End Sub

        Sub leadstatcancel(ByVal sender As Object, ByVal e As EventArgs)
            pnlldstatwarn.Visible = False
            Removeleadstat.Visible = True
        End Sub
        Sub leadsourcecancel(ByVal sender As Object, ByVal e As EventArgs)
            pnlldsourcewarn.Visible = False
            ldsource.Visible = True
        End Sub
        Sub leadprogramscancel(ByVal sender As Object, ByVal e As EventArgs)
            pnlldprogramswarn.Visible = False
            ldprograms.Visible = True
        End Sub
        Sub tasktypecancel(ByVal sender As Object, ByVal e As EventArgs)
            pnltaskwarn.Visible = False
            rmvtask.Visible = True
        End Sub
        Sub mkttypecancel(ByVal sender As Object, ByVal e As EventArgs)
            pnlmktwarn.Visible = False
            rmvmkt.Visible = True
        End Sub

        Public Sub removetype(ByVal ddtype As String)
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String
            if session("role")="GOD" then
                strSql = "delete from tbl_xwalk where tbl_xwalk_pk='" & ddtype & "' and (x_company='All' or x_uid='" & Session("userid") & "')"
				else
                strSql = "delete from tbl_xwalk where tbl_xwalk_pk='" & ddtype & "' and (x_company='" & Session("company_pk") & "' or x_uid='" & Session("userid") & "')"
			  	end if
         
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

        Public Function checkifleadattached(ByVal ldvalue As String, ByVal type As String) As Boolean

            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String
            If Type = "leadtype" Then
                strSql = "select * from tbl_leads where ld_type='" & ldvalue & "' and (ld_assignedbyuid='" & Session("userid") & "' or ld_assignedtouid='" & Session("userid") & "')"
            ElseIf Type = "contactstatus" Then
                strSql = "select * from tbl_leads where ld_pstatus='" & ldvalue & "' and (ld_assignedbyuid='" & Session("userid") & "' or ld_assignedtouid='" & Session("userid") & "')"
            ElseIf type = "leadsource" Then
                strSql = "select * from tbl_leads where ld_adsource='" & ldvalue & "' and (ld_assignedbyuid='" & Session("userid") & "' or ld_assignedtouid='" & Session("userid") & "')"
            ElseIf type = "leadprogram" Then
                strSql = "select * from tbl_leads where ld_program='" & ldvalue & "' and (ld_assignedbyuid='" & Session("userid") & "' or ld_assignedtouid='" & Session("userid") & "')"
				ElseIf type = "marketprogram" Then
                strSql = "select * from tbl_leads where ld_marketingprg='" & ldvalue & "' and (ld_assignedbyuid='" & Session("userid") & "' or ld_assignedtouid='" & Session("userid") & "')"

            End If

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

        Public Sub updateleads(ByVal ldvalue As String, ByVal type As String)
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String
            If type = "leadtype" Then
              

                strSql = "update tbl_leads set ld_type='Deleted' from tbl_leads where ld_type='" & ldvalue & "' and (ld_assignedbyuid='" & Session("userid") & "' or ld_assignedtouid='" & Session("userid") & "')"
            ElseIf type = "contactstatus" Then
                strSql = "update tbl_leads set ld_pstatus='Deleted' from tbl_leads where ld_pstatus='" & ldvalue & "' and (ld_assignedbyuid='" & Session("userid") & "' or ld_assignedtouid='" & Session("userid") & "')"
            ElseIf type = "leadsource" Then
                strSql = "update tbl_leads set ld_adsource='Deleted' from tbl_leads where ld_adsource='" & ldvalue & "' and (ld_assignedbyuid='" & Session("userid") & "' or ld_assignedtouid='" & Session("userid") & "')"
 				ElseIf type = "leadprogram" Then
                strSql = "update tbl_leads set ld_program='Deleted' from tbl_leads where ld_program='" & ldvalue & "' and (ld_assignedbyuid='" & Session("userid") & "' or ld_assignedtouid='" & Session("userid") & "')"
				ElseIf type = "marketprogram" Then
                strSql = "update tbl_leads set ld_marketingprg='Deleted' from tbl_leads where ld_marketingprg='" & ldvalue & "' and (ld_assignedbyuid='" & Session("userid") & "' or ld_assignedtouid='" & Session("userid") & "')"

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
        
        Sub savenewvenue (Source As System.Object, e As System.EventArgs)
			
			
				if session("LSstat")="add" then
					if checkifcodeexists()=false then
						savenewvenueNOBT("add")
						bindleadsource()
            		pnlleadsourceadd.visible=false
					else
				 		venuname.Text = "EXISTS"
					end if
				else
					 savenewvenueNOBT("edit")
						bindleadsource()
            		pnlleadsourceadd.visible=false
            		if venuname.text <> venunameorg.text then
            		
            			updateADPlansV()
            			UpdateADPostDates()
            		end if
				end if
				 pnlleadsourceadd.Visible = false
            pnldropmain.visible=true
				
		end sub
		
		sub UpdateADPostDates()
		 	 Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String
            strSql = "update tbl_LeadADVenues set av_name='" & venuname.text & "' from tbl_LeadADVenues join tbl_LeadADs on tbl_leadad_pk=av_leadads_FK where ad_userid='" & session("userid") & "' and av_name='" & venunameorg.text & "'"
            
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
		
		
		
		
		sub updateADPlansV()
		 	 Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String
            strSql = "update tbl_LeadADPlanVenues set lv_name='" & venuname.text & "' where lv_userid_fk='" & session("userid") & "' and lv_name='" & venunameorg.text & "'"
            
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
		
		
		sub savenewvenueNOBT(action as string)
			
             Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String
            if action ="add" then
           	 sqlproc = "sp_Addldsource"	
            else
            	sqlproc = "sp_Updateldsource" 
            end if
            
            
            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

 				' Add Parameters to SPROC
				if action ="edit" then
					 Dim prmlspk As New SqlParameter("@xpk", SqlDbType.Int)
                prmlspk.Value = session("selectedv")
                myCommandADD.Parameters.Add(prmlspk)
				end if
           
                Dim prmtype As New SqlParameter("@xtype", SqlDbType.varchar,50)
                prmtype.Value = "leadsource"
                myCommandADD.Parameters.Add(prmtype)
                
                Dim prmdesc As New SqlParameter("@xdesc", SqlDbType.varchar,255)
                prmdesc.Value = venuname.text
                myCommandADD.Parameters.Add(prmdesc)
                
                 Dim prmcomp As New SqlParameter("@xcompany", SqlDbType.varchar,50)
                 if privateven.checked then 
                 		prmcomp.Value = Session("company_pk")
                 else
                 		prmcomp.Value = "All"
                 end if
                myCommandADD.Parameters.Add(prmcomp)
                
                Dim prmcode As New SqlParameter("@xid", SqlDbType.varchar,50)
                prmcode.Value = venuecode.text
                myCommandADD.Parameters.Add(prmcode)
                
                Dim prmurl As New SqlParameter("@xurl", SqlDbType.varchar,255)
                prmurl.Value = venueurl.text
                myCommandADD.Parameters.Add(prmurl)
                
                Dim prmuid As New SqlParameter("@xuid", SqlDbType.varchar,50)
                prmuid.Value = session("userid")
                myCommandADD.Parameters.Add(prmuid)
      
      			 Dim prmxind As New SqlParameter("@xind", SqlDbType.varchar,50)
                prmxind.Value = dbnull.value
                myCommandADD.Parameters.Add(prmxind)
      	
      	 		Dim prmonline As New SqlParameter("@xonline", SqlDbType.varchar,50)
                prmonline.Value = ddvenonline.selecteditem.text
                myCommandADD.Parameters.Add(prmonline)
                
                 Dim prmxhtml As New SqlParameter("@xhtml", SqlDbType.varchar,50)
                 if htmltext.checked then 
                 		prmxhtml.Value = "Yes"
                 else
                 		prmxhtml.Value = "No"
                 end if
                myCommandADD.Parameters.Add(prmxhtml)
                
                 Dim prmxhasacct As New SqlParameter("@xhasacct", SqlDbType.varchar,50)
                 if acctsetup.checked then 
                 		prmxhasacct.Value = "Yes"
                 else
                 		prmxhasacct.Value = "No"
                 end if
                myCommandADD.Parameters.Add(prmxhasacct)

 					Dim prmaurl As New SqlParameter("@xaccturl", SqlDbType.varchar,50)
                prmaurl.Value = dbnull.value
                myCommandADD.Parameters.Add(prmaurl)
                
                Dim prmlgi As New SqlParameter("@xloginissue", SqlDbType.varchar,50)
                prmlgi.Value = "N"
                myCommandADD.Parameters.Add(prmlgi)
                
                Dim prmSP As New SqlParameter("@xselfpub", SqlDbType.varchar,50)
                prmSP.Value = "Y"
                myCommandADD.Parameters.Add(prmSP)
                
                Dim prmnote As New SqlParameter("@xnote", SqlDbType.text)
                prmnote.Value = lsnotes.content
                myCommandADD.Parameters.Add(prmnote)
                
                Dim prminst As New SqlParameter("@xinst", SqlDbType.text)
                prminst.Value = lsinst.content
                myCommandADD.Parameters.Add(prminst)
                
 
           
           
            Try
                myConnectionADD.Open()
                myCommandADD.ExecuteNonQuery()
                myConnectionADD.Close()
            Catch SQLexc As SqlException
                Response.Write("Insert Failed. Error Details are: " & SQLexc.ToString())
            End Try
			
            
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
         Sub savenewvenueExit(ByVal Source As System.Object, ByVal e As System.EventArgs)
            pnlleadsourceadd.Visible = False
             
            pnldropmain.visible=true
        End Sub

    End Class
end namespace