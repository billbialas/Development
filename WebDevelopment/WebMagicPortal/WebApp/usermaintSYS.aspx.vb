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
    Public Class usermaintSYS
        Inherits PageTemplate


        Protected WithEvents users As System.Web.UI.WebControls.DataGrid
        Public btnstatus, btnadduser As Button

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

            If Not (Page.IsPostBack) Then
                If Session("role") <> "GOD" Then
                    Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("CurrentwebURL"))
                End If
                If Session("role") = "user" Then
                    btnstatus.Visible = False
                    btnadduser.Visible = False
                End If

                BindUserGrid()
            End If
            pagesetup()

        End Sub

        Sub newuser(ByVal sender As Object, ByVal e As EventArgs)
            Response.Redirect("usermaintdetail.aspx?action=new")
        End Sub

        Sub togglestatus(ByVal sender As Object, ByVal e As EventArgs)
            If btnstatus.Text = "Show All" Then
                btnstatus.Text = "Hide Inactive"
            Else
                btnstatus.Text = "Show All"
            End If
            BindUserGrid()
        End Sub

        Sub BindUserGrid()

            Dim strUID As String = Session("userid")
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String

            If btnstatus.Text = "Show All" Then
                mycommand = "Select *,convert(varchar(20),sub_expiredate,101) as 'expdate' from tbl_users where status='Active'"
            Else
                mycommand = "Select *,convert(varchar(20),sub_expiredate,101) as 'expdate' from tbl_users"

            End If

            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_users")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_users"))

                users.DataSource = dvProducts
                users.DataBind()

            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
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
        Sub ItemDataBoundEventHandler(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                'Check who steps are for
                Dim removeuser As Button
                removeuser = e.Item.Cells(0).FindControl("bbremoveuser")
                removeuser.Attributes.Add("onClick", "return confirm('Are you sure to delete this item?')")

                Dim itemCellwho As TableCell = e.Item.Cells(3)
                Dim itemCellwhotext As String = itemCellwho.Text
                If itemCellwhotext = Session("userid") Then
                    removeuser.Enabled = False

                End If




            End If
        End Sub

        Sub removeuser(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
            Dim content As String = users.DataKeys(e.Item.ItemIndex)

            deleteuser(content)
            BindUserGrid()

        End Sub


        Sub deleteuser(ByVal id As String)
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "delete a from cart32.dbo.users a join tbl_users b on b.cart32id=a.userid where b.users_tbl_PK=" & id

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

            strSql = "delete from tbl_users where users_tbl_PK=" & id
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
    End Class
end namespace