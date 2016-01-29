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
Imports System.Configuration
Imports FreeTextBoxControls

namespace PageTemplate
    Public Class emailmainteditaddPU
        Inherits page

        Public emname, emdesc, emsub As TextBox
        Public sigchk As CheckBox
        Public emtext 
        public dd_txtstat as dropdownlist

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles mybase.load

            If Not (Page.IsPostBack) Then
            	Fillstattype()                
               bindfieldsA()
               
            End If
            

        End Sub
        Sub click_saveemail(ByVal Source As System.Object, ByVal e As System.EventArgs)
            if dd_txtstat.selecteditem.text <> "Select.." then
	            dbemail()
	            session("newtemppk") = getnewemailpk()
	            Session("pupclosed") = "true"
	            Dim msg As String = ""
	            msg = msg & "<Script Language='JavaScript'>"
	            msg = msg & "window.opener.location.href=window.opener.location.href;"
	            msg = msg & "window.close();"
	            msg = msg & "</Script>"
	            Response.Write(msg)
	            
            else
            	dd_txtstat.BackColor = Red
            end if
            
        End Sub
        Sub click_exitemail(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Session("pupclosed") = "true"
            session("newtemppk")="0"
            Dim msg As String = ""
            msg = msg & "<Script Language='JavaScript'>"
            msg = msg & "window.opener.location.href=window.opener.location.href;"
            msg = msg & "window.close();"
            msg = msg & "</Script>"
            Response.Write(msg)


        End Sub

		  Sub bindfieldsA()
		  		emsub.Text = Session("templateSubject")
		  		emtext.content = Session("templateText")
		  
		  
		  end sub
		  
		  
		  public function getnewemailpk() as string
		    	Dim strUID As String = Session("userid")
            Dim strSql As String = "SELECT max(email_tbl_pk) as 'newpk' from tbl_emails"

            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                
                If Sqldr.Read() Then
				    	return sqldr("newpk")
				    End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
		  
		  end function

        Sub bindfields()

            Dim strUID As String = Session("userid")
            Dim strSql As String = "SELECT * from tbl_emails where email_tbl_pk ='" & Request.QueryString("id") & "'"

            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then

                    If Sqldr("email_name") IsNot DBNull.Value Then
                        emname.Text = Sqldr("email_name")
                    End If
                    If Sqldr("email_descrip") IsNot DBNull.Value Then
                        emdesc.Text = Sqldr("email_descrip")
                    End If
                    If Sqldr("email_subject") IsNot DBNull.Value Then
                        emsub.Text = Sqldr("email_subject")
                    End If
                    If Sqldr("email_text") IsNot DBNull.Value Then
                        emtext.content = Sqldr("email_text")
                    End If
                    If Sqldr("email_usesig") = "Y" Then
                        sigchk.Checked = True
                    Else
                        sigchk.Checked = False
                    End If
                     If Sqldr("email_stat") IsNot DBNull.Value Then
                           dd_txtstat.SelectedIndex = dd_txtstat.Items.IndexOf(dd_txtstat.Items.FindByValue(Sqldr("email_stat")))
                    	else
                          	dd_txtstat.SelectedIndex = dd_txtstat.Items.IndexOf(dd_txtstat.Items.FindByValue("999998"))
                
                    End If
                    if dd_txtstat.selecteditem.text="System Wide" 
                    		if Sqldr("email_uid") = session("userid") then
		                 		dd_txtstat.enabled = true
		                 		emname.enabled = true
		                 		emdesc.enabled = true
		                 		emsub.enabled = true
		                 		'emtext.readonly = false
                    		else
	                    		dd_txtstat.enabled = false
	                    		emname.enabled = false
	                    		emdesc.enabled = false
	                    		emsub.enabled = false
	                    		'emtext.readonly = true
                    		end if
                    elseif dd_txtstat.selecteditem.text="Company Wide" 
                        if Sqldr("email_co") isnot dbnull.value then
                          		if Sqldr("email_co") = session("company_pk").tostring  then
				                    		dd_txtstat.enabled = true
				                    		emname.enabled = true
				                    		emdesc.enabled = true
				                    		emsub.enabled = true
				                    		'emtext.readonly = false
		                    		else
				                    		dd_txtstat.enabled = false
				                    		emname.enabled = false
				                    		emdesc.enabled = false
				                    		emsub.enabled = false
				                    		'emtext.readonly = true
		                    		end if
		                  end if
		              end if   
                 
                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        End Sub
        Public Sub dbemail()
             Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String

            If Request.QueryString("action") = "new" Then
                sqlproc = "sp_Addemail"
            Else
                sqlproc = "sp_updateemail"
            End If

            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

            If Request.QueryString("action") = "edit" Then
                Dim prmempk As New SqlParameter("@empk", SqlDbType.Int)
                prmempk.Value = Request.QueryString("id")
                myCommandADD.Parameters.Add(prmempk)
            End If

            Dim prmname As New SqlParameter("@emname", SqlDbType.VarChar, 255)
            prmname.Value = emname.Text
            myCommandADD.Parameters.Add(prmname)

            Dim prmuid As New SqlParameter("@aduid", SqlDbType.VarChar, 255)
            prmuid.Value = Session("userid")
            myCommandADD.Parameters.Add(prmuid)

            Dim prmemdesc As New SqlParameter("@emdesc", SqlDbType.VarChar, 255)
            prmemdesc.Value = emdesc.Text
            myCommandADD.Parameters.Add(prmemdesc)

            Dim prmemsub As New SqlParameter("@emsub", SqlDbType.VarChar, 255)
            prmemsub.Value = emsub.Text
            myCommandADD.Parameters.Add(prmemsub)

            Dim prmemtext As New SqlParameter("@emtext", SqlDbType.Text)
                prmemtext.Value = emtext.content
             myCommandADD.Parameters.Add(prmemtext)

            Dim prmsig As New SqlParameter("@sig", SqlDbType.VarChar, 2)
            If sigchk.Checked Then
                prmsig.Value = "Y"
            Else
                prmsig.Value = "N"
            End If
            myCommandADD.Parameters.Add(prmsig)

				Dim prmstat As New SqlParameter("@stat", SqlDbType.varchar, 50)
            prmstat.Value = dd_txtstat.selecteditem.value
            myCommandADD.Parameters.Add(prmstat)
            
        		Dim prmcomp As New SqlParameter("@comp", SqlDbType.varchar, 50)
            if dd_txtstat.selecteditem.text="Company Wide" then            
            	prmcomp.Value = session("company_pk")            	
           	else
           		prmcomp.Value = dbnull.value
           	end if
            myCommandADD.Parameters.Add(prmcomp)           
            
            
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
        
         Sub Fillstattype()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='templatestat' and (x_company='All' or x_uid='" & Session("userid") & "')"
           
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                dd_txtstat.DataSource = dataReader
                dd_txtstat.DataTextField = "x_descr"
                dd_txtstat.DataValueField = "tbl_xwalk_pk"
                dd_txtstat.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
            
            dd_txtstat.Items.Insert(0, New ListItem("Select..", "999998"))
        End Sub
        
        
    

    End Class
end namespace