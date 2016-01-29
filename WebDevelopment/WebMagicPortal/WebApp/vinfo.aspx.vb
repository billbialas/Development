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
    Public Class vinfo
        Inherits PageTemplate
        Public pnladdvenue As Panel
        Public venuname, venuecode, venueurl, uid, password As TextBox
        Public privateven, acctsetup As CheckBox
        Public ddvenonline, advenue As DropDownList
        public lblrecexists as label


        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles mybase.load

            If Not (Page.IsPostBack) Then
                If Request.QueryString("action") = "new" Then
                    FillADvenues()
                    session("lsstat") = "add"
                Else
                    FillADvenues()
                    bindfields()
                    session("lsstat") = "edit"
							session("cvenuid") = request.querystring("id")
                End If

            End If
            pagesetup()

        End Sub
        Sub savenewvenue(ByVal Source As System.Object, ByVal e As System.EventArgs)
            If checkifcodeexists() = False Then

                Dim strConnection As String
                Dim sqlConn As SqlConnection
                Dim sqlCmd As SqlCommand

                Dim strSql As String
                Dim hasaccts As String
                If acctsetup.checked Then
                    hasaccts = "Yes"
                Else
                    hasaccts = "No"
                End If


                If privateven.checked Then
                    strSql = "insert into dbo.tbl_xwalk  (x_type,x_descr,x_id,x_url,x_UID,x_online,x_hasaccounts,x_accounturl,x_loginissue) values('leadsource','" & venuname.Text & "','" & venuecode.Text & "','" & venueurl.Text & "','" & Session("userid") & "','" & ddvenonline.SelectedItem.Text & "','" & hasaccts & "','http://" & venueurl.Text & "','No')"
                Else
                    strSql = "insert into dbo.tbl_xwalk  (x_type,x_descr,x_company,x_id,x_url,x_UID,x_online,x_hasaccounts,x_accounturl,x_loginissue) " _
                             & "values('leadsource','" & venuname.Text & "','All','" & venuecode.Text & "','" & venueurl.Text & "','" & Session("userid") & "','" & ddvenonline.SelectedItem.Text & "','" & hasaccts & "','" & venueurl.Text & "','No')"

                End If

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
                pnladdvenue.visible = False
                FillADvenues()

            Else
                venuname.Text = "EXISTS"
            End If


        End Sub
        
        Sub checkifvexists(ByVal Source As System.Object, ByVal e As System.EventArgs)
            if vid() then
            	
            	bindvidfields()
            	session("lsstat") = "edit"
            	lblrecexists.visible=true
            else
            	session("lsstat") = "add"
            	uid.text=""
            	password.text=""
            	lblrecexists.visible=false
            end if
            
        End Sub
        
        
         public function vid() as boolean
        		Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select * from tbl_venueinfo where vpass_uid='" & session("userid") & "' and vpass_venue='" & advenue.selecteditem.text & "'"

            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                	
                    If Sqldr("vpass_venue") IsNot DBNull.Value Then
                         session("cvenuid") = sqldr("vpass_tbl_pk")
                         return true
                       
                    else
                    		return false
                   
                    End If
                else
                	return false
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
        
        end function
        
        sub bindvidfields()
        		Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select * from tbl_venueinfo where vpass_uid='" & session("userid") & "' and vpass_venue='" & advenue.selecteditem.text & "'"

            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                	
                    If Sqldr("vpass_id") IsNot DBNull.Value Then
                        uid.text = Sqldr("vpass_id")
                   
                    End If
                     If Sqldr("vpass_pass") IsNot DBNull.Value Then
                        password.text = Sqldr("vpass_pass")
                   
                    End If
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
        
        end sub
        
        Sub savenewvenueExit(ByVal Source As System.Object, ByVal e As System.EventArgs)
            pnladdvenue.Visible = False
        End Sub
        Sub savevinfoExit(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Response.Redirect("admanager.aspx?source=vinfo")
        End Sub
        
        Sub FillADvenues()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='leadsource' and (x_company='" & Session("company_pk") & "' or x_company='All' or x_Uid='" & Session("userid") & "') order by x_descr"
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                advenue.DataSource = dataReader
                advenue.DataTextField = "x_descr"
                advenue.DataValueField = "tbl_xwalk_pk"
                advenue.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

            advenue.Items.Insert(0, New ListItem("Select..", "9999"))


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
        Sub insertvinfo(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand

            Dim strSql As String
            If session("lsstat") = "add" Then
                strSql = "insert into dbo.tbl_venueinfo (vpass_venue,vpass_id,vpass_pass,vpass_uid) values ('" & advenue.SelectedItem.Text & "','" & uid.Text & "','" & password.Text & "','" & Session("userid") & "')"
            Else
                strSql = "update dbo.tbl_venueinfo set vpass_venue ='" & advenue.SelectedItem.text & "' , vpass_id='" & uid.Text & "',vpass_pass='" & password.Text & "'  where vpass_tbl_pk='" & session("cvenuid") & "'"
            End If

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
            Response.Redirect("admanager.aspx?source=vinfo")

        End Sub
        Sub bindfields()

            Dim strSql As String = "SELECT * from tbl_venueinfo where vpass_tbl_pk ='" & Request.QueryString("id") & "'"

            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    If Sqldr("vpass_id") IsNot DBNull.Value Then
                        uid.Text = Sqldr("vpass_id")
                    End If
                    If Sqldr("vpass_pass") IsNot DBNull.Value Then
                        password.Text = Sqldr("vpass_pass")
                    End If
                    If Sqldr("vpass_venue") IsNot DBNull.Value Then
                        advenue.SelectedIndex = advenue.Items.IndexOf(advenue.Items.FindByText(Sqldr("vpass_venue")))
                    End If

                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
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