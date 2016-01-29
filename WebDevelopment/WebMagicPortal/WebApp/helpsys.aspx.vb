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
Imports FreeTextBoxControls

namespace PageTemplate
    Public Class helpsys
        Inherits PageTemplate
        
        public txtsysnotes as FreeTextBox

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles mybase.load

            If Not (Page.IsPostBack) Then
						bindsysmessage()
            End If
            pagesetup()

        End Sub
        Sub bindsysmessage()
            Dim strUID As String = Session("userid")
            Dim strSql As String = "SELECT * from tbl_miscstuff where misc_type='SysHelp'"
             Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    txtsysnotes.Text = Sqldr("misc_text")
                 End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        End Sub
         Sub updatesysmessage(ByVal Source As Object, ByVal E As EventArgs)
            Dim strUID As String = Session("userid")
            Dim strSql As String = "update tbl_miscstuff set misc_text ='" & txtsysnotes.Text & "' where misc_type='SysHelp'"
             Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    txtsysnotes.Text = Sqldr("misc_text")
                 End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try

            Response.Redirect("setup.aspx")
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