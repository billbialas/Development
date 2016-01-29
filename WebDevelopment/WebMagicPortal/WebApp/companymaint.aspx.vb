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
    Public Class companymaint
        Inherits PageTemplate
        Protected WithEvents logo As System.Web.UI.HtmlControls.HtmlInputFile
        Protected WithEvents Submit1 As System.Web.UI.HtmlControls.HtmlInputButton
        Public billinghist As DataGrid
        Public CONAME, coestore, cowebsite, COID As TextBox
        Public logoyes As Boolean
        Public logoimg As System.Web.UI.HtmlControls.HtmlImage

       

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles mybase.load

            If Not (Page.IsPostBack) Then
            	clearsessions()
                CONAME.Enabled = False
                COID.Enabled = False
                bindconame()
                If checkforlogo() Then
                    logoimg.Attributes("src") = System.Configuration.ConfigurationManager.AppSettings("CurrentIMGURL")  & "/logos/company/" & session("selectedlogo")
                Else
                    logoimg.Attributes("src") = System.Configuration.ConfigurationManager.AppSettings("CurrentIMGURL")  & "/logos/company/default.jpg"
                End If
                bindbillhist()
            End If
            pagesetup()

        End Sub
       sub clearsessions()
       	session("selectedlogo")=""
        
        
        end sub
            Public Sub btn_showhelp(ByVal Source As Object, ByVal e As ImageClickEventArgs)
      		Response.Write("<script>window.open" & _
                "('" & System.Configuration.ConfigurationManager.AppSettings("CurrentappURL") & "/help/help_um.html','_new','width=1000,height=650,resizable=1,scrollbars=1');</script>")
     
			end sub
        Sub myDataGrid_PageChanger(ByVal Source As Object, _
                   ByVal E As DataGridPageChangedEventArgs)
            ' Set the CurrentPageIndex before binding the grid 
            billinghist.CurrentPageIndex = E.NewPageIndex
            bindbillhist()

        End Sub
        Sub bindconame()
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "Select co_name,* from dbo.tbl_company where co_tbl_pk = '" & Session("company_pk") & "'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                		coid.text = Sqldr("co_tbl_pk")
                    CONAME.Text = Sqldr("co_name")
                    If Sqldr("co_website") IsNot DBNull.Value Then
                        cowebsite.Text = Sqldr("co_website")
                    End If
                    If Sqldr("co_estore") IsNot DBNull.Value Then
                        coestore.Text = Sqldr("co_estore")
                    End If

                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try

        End Sub
        Private Sub Submit1_ServerClick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Submit1.ServerClick

            If Not logo.PostedFile Is Nothing And logo.PostedFile.ContentLength > 0 Then
                Dim fn As String = System.IO.Path.GetFileName(logo.PostedFile.FileName)
                Dim SaveLocation As String = Server.MapPath(".\") & fn
                '"'F:\ChoiceOne\Development\gimp\LOGOS\" & fn& fn
                'Server.MapPath("Data") & "\" & fn
                Try
                    logo.PostedFile.SaveAs(System.Configuration.ConfigurationManager.AppSettings("CurrentLogoDirectory") & fn)
                    ' Response.Write("The file has been uploaded.")
                Catch Exc As Exception
                    Response.Write("Error: " & Exc.Message)
                End Try

                Dim strConnection As String
                Dim sqlConn As SqlConnection
                Dim sqlCmd As SqlCommand
                Dim strSql As String = "update dbo.tbl_company set co_logo = '" & fn & "' from dbo.tbl_company where co_tbl_pk = '" & Session("company_pk") & "'"
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

                Response.Redirect("companymaint.aspx")

            Else
                Response.Write("Please select a file to upload.")
            End If

        End Sub
        Function checkforlogo() As Boolean
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select co_logo from dbo.tbl_company where co_tbl_pk = '" & Session("company_pk") & "' and co_logo is not null"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    session("selectedlogo") = Sqldr("co_logo")
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
        Sub bindbillhist()
            Dim strUID As String = Session("userid")
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String = "select cart32.dbo.orders.orderno as 'orderno',cart32.dbo.orders.orderdate as 'orderdate',cart32.dbo.orditems.item as 'package', " _
                    & "cast (cart32.dbo.orditems.qty * cart32.dbo.orditems.price as decimal(10,2)) as 'Cost' " _
                    & "from dbo.tbl_company join cart32.dbo.orders on cart32.dbo.orders.userid = co_customerid " _
                    & "join  cart32.dbo.orditems on cart32.dbo.orders.orderno=cart32.dbo.orditems.orderno where co_tbl_pk = '" & Session("company_pk") & "'"

            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_company")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_company"))

                billinghist.DataSource = dvProducts
                billinghist.DataBind()

            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

        End Sub
        Sub updatecoinfo(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand

            Dim strSql As String
            strSql = "update dbo.tbl_company set co_name ='" & CONAME.Text & "',co_estore ='" & coestore.Text & "', co_website='" & cowebsite.Text & "' where co_tbl_pk='" & Session("company_pk") & "'"


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
            
            strSql = "update dbo.cart32.users set COMPANY ='" & CONAME.Text & "' join tbl_users on cart32id=userid where UID='" & session("userid") & "'"


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
            Response.Redirect("Companymaint.aspx")
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

    End Class
end namespace