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
    Public Class cartsys
        Inherits PageTemplate
        
        public txtsysnotes as textbox
        public resetusrstat as checkbox
        public csHtext,csltext
	 		Protected WithEvents Lgen, lpage1, lpage2, lautop,limgs,btnuseremail2,btnuseremail,lads As LinkButton
	 		Public spacer0, spacer1, spacer2,spacer3,spacer4 As HtmlTableCell
	 		Public subnavGen, subnavPage1, subnavPage2, subnavresp,subnavimgs,subnavADS As HtmlTableCell
	 		public pnlheader,pnlleft as panel
            
            
        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles mybase.load

            If Not (Page.IsPostBack) Then
            		if not checkifrecexists() then
							createentry()
						end if
						bindcartdata()
						subnav("Header")
            End If
            pagesetup()

        End Sub
        
        public function  checkifrecexists() as boolean
        
        		Dim strUID As String = Session("userid")
            Dim strSql As String = "SELECT * from tbl_cartsetup where cs_copk='" & session("company_pk") & "'"
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
        
        public sub createentry()
        
        		 Dim strUID As String = Session("userid")
            Dim strSql As String = "insert into tbl_cartsetup (cs_Header, cs_Lnav, cs_copk) values ('','','" & Session("company_pk")& "')"
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
        
        end sub
        
        
        
        
        
        
        
        Sub bindcartdata()
            Dim strUID As String = Session("userid")
            Dim strSql As String = "SELECT * from tbl_cartsetup where cs_copk='" & session("company_pk") & "'"
             Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                		If Sqldr("cs_Header") IsNot DBNull.Value Then
                    		csHtext.content = Sqldr("cs_Header")
                    	end if
                    	If Sqldr("cs_Lnav") IsNot DBNull.Value Then
                    		csLtext.content = Sqldr("cs_Lnav")
                    	end if
                 End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        End Sub
         Sub updatecartdata(ByVal Source As Object, ByVal E As EventArgs)
            Dim strUID As String = Session("userid")
            Dim strSql As String = "update tbl_cartsetup set cs_Header ='" & csHtext.content & "', cs_Lnav ='" & csltext.content & "' where cs_copk='" & Session("company_pk") & "'"
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
        sub updateuserstat()
				Dim strUID As String = Session("userid")
            Dim strSql As String = "update tbl_users set readsysmes ='N'"
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
        
        end sub
        
        Sub Exitcart(ByVal sender As Object, ByVal e As EventArgs)
           response.redirect("setup.aspx")
            
        End Sub
        
        
        
        Sub btn_header(ByVal sender As Object, ByVal e As EventArgs)
            subnav("Header")
          
             session("lsttab")="Header"
            
        End Sub
        
        Sub btn_pg1(ByVal sender As Object, ByVal e As EventArgs)
            subnav("Left")           
          	session("lsttab")="Left"
            
        End Sub
        
        
 		Sub subnav(ByVal button As String)
            Dim clickedbutton As String = button

            'Set cell class
            subnavGen.Attributes.Add("class", "tblcelltest")
            subnavPage1.Attributes.Add("class", "tblcelltest")
                      
            'Set button font color
            Lgen.ForeColor = System.Drawing.Color.Black
            lpage1.ForeColor = System.Drawing.Color.Black
           
            
            'Set spacers
            spacer0.Visible = True           
           

            'Set Panels
            pnlheader.Visible = False
            pnlleft.Visible = False
                      
            If clickedbutton = "Header" Then
                subnavGen.Attributes.Add("class", "tblcelltestSelected")
                Lgen.ForeColor = System.Drawing.Color.White
                spacer0.Visible = False
                pnlheader.Visible = True
            ElseIf clickedbutton = "Left" Then
                subnavPage1.Attributes.Add("class", "tblcelltestSelected")
                lpage1.ForeColor = System.Drawing.Color.White
                spacer0.Visible = False                            
                pnlleft.Visible = True
          
            else
             	subnavGen.Attributes.Add("class", "tblcelltestSelected")
                Lgen.ForeColor = System.Drawing.Color.White
                spacer0.Visible = False
                pnlheader.Visible = True          
            End If
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
           middleNav.Controls.Add(LoadControl(System.Configuration.ConfigurationManager.AppSettings("ScreenmiddleNav")))
     
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