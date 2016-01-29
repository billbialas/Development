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
    Public Class branding
        Inherits PageTemplate
        Public chkshowlogo, chksendemail As CheckBox
        Public txtredirecturl, pg1text, pg2text, TextBox1, TextBox2, TextBox3, emailbody As TextBox
        Private Shared brandno As String
        public pnlbrandup as panel
        public bsaveco as button

       
        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles mybase.load
            If Not (Page.IsPostBack) Then
                If defaultexists() Then
                  bindfields()
                  If Session("branding") = "No" Then
	                  chkshowlogo.enabled = false
	                  chksendemail.enabled = false
	                  txtredirecturl.enabled = false
	                  pg1text.enabled = false
	                  pg2text.enabled = false
	                  TextBox1.enabled = false
	                  TextBox2.enabled = false
	                  TextBox3.enabled = false
	                  emailbody.enabled = false
	                  pnlbrandup.visible=true
	                  bsaveco.visible=false
                  else
                  	chkshowlogo.enabled = true
	                  chksendemail.enabled = true
	                  txtredirecturl.enabled = true
	                  pg1text.enabled = true
	                  pg2text.enabled = true
	                  TextBox1.enabled = true
	                  TextBox2.enabled = true
	                  TextBox3.enabled = true
	                  emailbody.enabled = true
	                  pnlbrandup.visible=false
	                  bsaveco.visible=true
                  
                  end if
                End If

            End If
            pagesetup()

        End Sub
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
        Sub bindfields()
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
                    If Sqldr("br_showlogo") = "Y" Then
                        chkshowlogo.Checked = True
                    Else
                        chkshowlogo.Checked = False
                    End If
                    If Sqldr("br_sendemail") = "Y" Then
                        chksendemail.Checked = True
                    Else
                        chksendemail.Checked = False
                    End If
                    If Sqldr("br_redirecturl") IsNot DBNull.Value Then
                        txtredirecturl.Text = Sqldr("br_redirecturl")
                    End If
                    If Sqldr("br_text1") IsNot DBNull.Value Then
                        pg1text.Text = Sqldr("br_text1")
                    End If
                    If Sqldr("br_text2") IsNot DBNull.Value Then
                        pg2text.Text = Sqldr("br_text2")
                    End If
                    If Sqldr("br_emailfrom") IsNot DBNull.Value Then
                        TextBox1.Text = Sqldr("br_emailfrom")
                    End If
                    If Sqldr("br_emailreply") IsNot DBNull.Value Then
                        TextBox2.Text = Sqldr("br_emailreply")
                    End If
                    If Sqldr("br_emailsubject") IsNot DBNull.Value Then
                        TextBox3.Text = Sqldr("br_emailsubject")
                    End If
                    If Sqldr("br_emailbody") IsNot DBNull.Value Then
                        emailbody.Text = Sqldr("br_emailbody")
                    End If
                    brandno = Sqldr("tbl_branding_pk")
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try

        End Sub
        Sub insertdefault(ByVal Source As System.Object, ByVal e As System.EventArgs)
           

        End Sub

        Sub enablebutts(ByVal Source As System.Object, ByVal e As System.EventArgs)
            If chksendemail.Checked = True Then
                TextBox1.Enabled = True
                TextBox2.Enabled = True
                TextBox3.Enabled = True
                emailbody.Enabled = True
            Else
                TextBox1.Enabled = False
                TextBox2.Enabled = False
                TextBox3.Enabled = False
                emailbody.Enabled = False
                TextBox1.Text = ""
                TextBox2.Text = ""
                TextBox3.Text = ""
                emailbody.Text = ""
            End If
          
        End Sub
        Sub updatebranding(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String
           
            If defaultexists() Then
                sqlproc = "sp_updatebranding"
            Else
                sqlproc = "sp_insertbranding"
            End If

            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

            If defaultexists() Then
                Dim prmbrpk As New SqlParameter("@brpk ", SqlDbType.Int)
                prmbrpk.Value = brandno
                myCommandADD.Parameters.Add(prmbrpk)
            End If

            Dim prmname As New SqlParameter("@name", SqlDbType.VarChar, 50)
            prmname.Value = "Default"
            myCommandADD.Parameters.Add(prmname)

            Dim prmuid As New SqlParameter("@uid", SqlDbType.VarChar, 50)
            prmuid.Value = Session("userid")
            myCommandADD.Parameters.Add(prmuid)

            Dim prmadno As New SqlParameter("@adno", SqlDbType.VarChar, 50)
            prmadno.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmadno)

            Dim prmcompany As New SqlParameter("@company", SqlDbType.VarChar, 50)
            prmcompany.Value = Session("company")
            myCommandADD.Parameters.Add(prmcompany)

            Dim prmdescrip As New SqlParameter("@descript", SqlDbType.VarChar, 255)
            prmdescrip.Value = "Default"
            myCommandADD.Parameters.Add(prmdescrip)

            Dim prmlogo As New SqlParameter("@logo", SqlDbType.VarChar, 1)
            If chkshowlogo.Checked = True Then
                prmlogo.Value = "Y"
            Else
                prmlogo.Value = "N"
            End If
            myCommandADD.Parameters.Add(prmlogo)

            Dim prmemail As New SqlParameter("@email", SqlDbType.VarChar, 1)
            If chksendemail.Checked = True Then
                prmemail.Value = "Y"
            Else
                prmemail.Value = "N"
            End If
            myCommandADD.Parameters.Add(prmemail)

            Dim prmredirect As New SqlParameter("@redirect", SqlDbType.VarChar, 255)
            prmredirect.Value = txtredirecturl.Text
            myCommandADD.Parameters.Add(prmredirect)

            Dim prmtext1 As New SqlParameter("@text1", SqlDbType.Text)
            prmtext1.Value = pg1text.Text
            myCommandADD.Parameters.Add(prmtext1)

            Dim prmtext2 As New SqlParameter("@text2", SqlDbType.Text)
            prmtext2.Value = pg2text.Text
            myCommandADD.Parameters.Add(prmtext2)


            Dim prmemailfrom As New SqlParameter("@emailfrom", SqlDbType.VarChar, 50)
            prmemailfrom.Value = TextBox1.Text
            myCommandADD.Parameters.Add(prmemailfrom)

            Dim prmreplyto As New SqlParameter("@replyto", SqlDbType.VarChar, 50)
            prmreplyto.Value = TextBox2.Text
            myCommandADD.Parameters.Add(prmreplyto)

            Dim prmsubject As New SqlParameter("@subject", SqlDbType.VarChar, 255)
            prmsubject.Value = TextBox3.Text
            myCommandADD.Parameters.Add(prmsubject)

            Dim prmbody As New SqlParameter("@body", SqlDbType.Text)
            prmbody.Value = emailbody.Text
            myCommandADD.Parameters.Add(prmbody)

            Dim prmem As New SqlParameter("@getemail", SqlDbType.VarChar, 50)
            prmem.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmem)

            Dim prmemadd As New SqlParameter("@emailaddress ", SqlDbType.VarChar, 255)
            prmemadd.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmemadd)

            Dim prmimgfk As New SqlParameter("@imgfk", SqlDbType.Int)
            If chkshowlogo.Checked Then
                prmimgfk.Value = "999999"
            Else
                prmimgfk.Value = DBNull.Value
            End If
            myCommandADD.Parameters.Add(prmimgfk)

            Dim prmhdt1 As New SqlParameter("@hdtxt1", SqlDbType.VarChar, 1)
            prmhdt1.Value = "N"
            myCommandADD.Parameters.Add(prmhdt1)

            Dim prmhdt2 As New SqlParameter("@hdtxt2", SqlDbType.VarChar, 1)
            prmhdt2.Value = "N"
            myCommandADD.Parameters.Add(prmhdt2)

            Dim prmhdtxt1t As New SqlParameter("@hdtxt1t ", SqlDbType.VarChar, 255)
            prmhdtxt1t.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmhdtxt1t)

            Dim prmhdtxt2t As New SqlParameter("@hdtxt2t ", SqlDbType.VarChar, 255)
            prmhdtxt2t.Value = DBNull.Value
            myCommandADD.Parameters.Add(prmhdtxt2t)

            Try
                myConnectionADD.Open()
                myCommandADD.ExecuteNonQuery()
                myConnectionADD.Close()
            Catch SQLexc As SqlException
                Response.Write("Insert Failed. Error Details are: " & SQLexc.ToString())
            Finally
                myConnectionADD.Close()
            End Try

            If Request.QueryString("source") = "admgr" Then
                Response.Redirect("admanager.aspx")
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
            leftNav.Controls.Add(LoadControl(System.Configuration.ConfigurationManager.AppSettings("ScreenLeftNavSetup")))

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