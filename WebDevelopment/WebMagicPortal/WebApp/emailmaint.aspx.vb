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
    Public Class emailmaint
        Inherits PageTemplate
        Public emails As DataGrid
        public l_search as textbox
		  public dd_txtstat as dropdownlist

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles mybase.load
 			
 			
            If Not (Page.IsPostBack) Then
                
                Fillstattype()
          
                 if Request.QueryString("search") <> "*" Then
                 		l_search.text =Request.QueryString("search")
                 else 
                 		Session("qstring") ="None"
                 end if
                 if session("tfilter")= "Select.." then
                 
                 else 
                 	 dd_txtstat.SelectedIndex = dd_txtstat.Items.IndexOf(dd_txtstat.Items.FindBytext(session("tfilter")))
                 end if
                 bindemails()
            End If
            pagesetup()

        End Sub
         Public Sub btn_showhelp(ByVal Source As Object, ByVal e As ImageClickEventArgs)
      		Response.Write("<script>window.open" & _
                "('" & System.Configuration.ConfigurationManager.AppSettings("CurrentappURL") & "/help/help_tp.html','_new','width=1000,height=650,resizable=1,scrollbars=1');</script>")
     
		 end sub
     
       
        Sub emails_PageChanger(ByVal Source As Object, ByVal E As DataGridPageChangedEventArgs)
            emails.CurrentPageIndex = E.NewPageIndex
            bindemails()
        End Sub
        Sub click_addnewemail(ByVal Source As Object, ByVal E As EventArgs)
            Response.Redirect("emailmainteditadd.aspx?action=new")
        End Sub

        Sub bindemails()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            if dd_txtstat.selecteditem.text = "Select.." or dd_txtstat.selecteditem.text = "All"  then
            		mycommand = "Select *, convert(varchar(20),email_date,101) as 'emdate' " _
									& "from tbl_emails " _
									& "join tbl_xwalk on tbl_xwalk_pk = email_stat " _
									& "where  " _
									& " ((email_uid='" & Session("userid") & "') " _
									& "or (x_descr='Company Wide' and email_co='" & Session("company_pk") & "') " _
									& "or (x_descr='System Wide')) order by email_tbl_pk desc"
            else
	            mycommand = "Select *, convert(varchar(20),email_date,101) as 'emdate' " _
									& "from tbl_emails " _
									& "join tbl_xwalk on tbl_xwalk_pk = email_stat " _
									& "where email_stat='" & dd_txtstat.selecteditem.value & "' " _
									& "and ((email_uid='" & Session("userid") & "' ) " _
									& "or (x_descr='Company Wide' and email_co='" & Session("company_pk") & "') " _
									& "or (x_descr='System Wide')) order by email_tbl_pk desc"
				end if

            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_emails")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_emails"))
                If Request.QueryString("search") <> "*" Then
                	dvProducts.RowFilter = "(email_name like '%" & Request.QueryString("search") & "%' or email_descrip like '%" & Request.QueryString("search") & "%' or  email_subject like '%" & Request.QueryString("search") & "%' or email_text LIKE '%" & Request.QueryString("search") & "%')  "
              	 end if
                emails.DataSource = dvProducts
                emails.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
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
            dd_txtstat.Items.Insert(1, New ListItem("All", "999997"))
            
        End Sub
         Sub filtertemps(ByVal Source As Object, ByVal E As EventArgs)
            bindemails()
            session("tfilter")=dd_txtstat.selecteditem.text
            
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
        
        		
        Sub btnsearch(ByVal Source As System.Object, ByVal e As System.EventArgs)
           
				emails.CurrentPageIndex = 0
            searchstring()
        End Sub
        
        Sub searchstring()

            Dim psearch As String
         	If l_search.Text = "" Then
                psearch = "*"
            Else
                psearch = l_search.Text
                Session("search") = "text"
            End If
            
            dim querystring as string = "emailmaint.aspx?search=" & psearch
				Session("qstring") = querystring
            Response.Redirect(querystring)
         End Sub

			Sub clearall (Source As System.Object, e As System.EventArgs)
			 	session("tfilter")= "Select.."
             Response.Redirect("emailmaint.aspx?search=*")
            
       end sub 
       
       
    End Class
end namespace