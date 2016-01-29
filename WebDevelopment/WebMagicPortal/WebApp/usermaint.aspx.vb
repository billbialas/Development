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
	public class usermaint
        Inherits PageTemplate

	     
        Protected WithEvents users As System.Web.UI.WebControls.DataGrid
        Public btnstatus, btnadduser,btnconvert As Button
	      public l_search as textbox
	     public dd_userstat,dd_userrole as dropdownlist
	     	
        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

            If Not (Page.IsPostBack) Then
                If Session("role") = "user" Then
                    btnstatus.Visible = False
                    btnadduser.Visible = False
                End If
                If Session("ustat") = "Trial" Then
                	btnadduser.visible=false
                	btnconvert.visible=true
                else
                	btnconvert.visible=false
                end if
 					 Fillrole()
 					 filluserstat()
 					 checkquerystring()
                BindUserGrid()
            End If
            pagesetup()

        End Sub
        
        Sub ChangetypeFilter(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Session("pgindex") = 0
            searchstring()
        End Sub
        
         Sub filluserstat()

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='userstatus' and (x_company='" & Session("company_pk") & "' or x_company='All')"
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                dd_userstat.DataSource = dataReader
                dd_userstat.DataTextField = "x_descr"
                dd_userstat.DataValueField = "tbl_xwalk_pk"
                dd_userstat.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
           dd_userstat.Items.Insert(0, New ListItem("All", "9999"))
        End Sub
         Sub Fillrole()

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='userrole' and (x_company='" & Session("company_pk") & "' or x_company='All')"
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                dd_userrole.DataSource = dataReader
                dd_userrole.DataTextField = "x_descr"
                dd_userrole.DataValueField = "tbl_xwalk_pk"
                dd_userrole.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
            If Session("role") = "GOD" Then
                dd_userrole.Items.Insert(0, New ListItem("GOD", "8888"))
            End If
            
            dd_userrole.Items.Insert(0, New ListItem("All", "9999"))
        End Sub
		   
		   Sub newuser(sender As Object, e As EventArgs)
     			response.redirect("usermaintdetail.aspx?action=new")
     		End Sub
     		Sub convertuser(sender As Object, e As EventArgs)
     			createc32rec()
     			Response.Redirect(System.Configuration.ConfigurationManager.AppSettings("CurrentappURL") & "/gsignup.aspx")
     			
     			
     		End Sub
     		
     		 Public Sub createc32rec()

            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            dim c32action as string
            Dim ip As String = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
            If ip = "" Then
                ip = Request.ServerVariables("REMOTE_ADDR")
            End If
           	c32action="Activate"
          
            Dim strSql As String = "insert into tbL_c32process (c32p_ipno,c32p_date,c32p_action,c32p_url,c32p_uid) values ('" & ip & "',getdate(),'" & c32action & "','" & System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "','" & Session("userid") & "')"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)

                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

            Catch ex As Exception
                Response.Write(ex.ToString())
                Exit Sub
            Finally
                sqlConn.Close()
            End Try
        End Sub
     		
		   
		   Sub togglestatus(sender As Object, e As EventArgs)
		   	if btnstatus.text="Show All" then
		   		btnstatus.text="Hide Inactive"
		   	else 
		   		btnstatus.text="Show All"
		   	end if
		   	BindUserGrid() 
		   End Sub
		   
        Sub BindUserGrid()

            Dim strUID As String = Session("userid")
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String

            If btnstatus.Text = "Show All" Then
                If Session("role") = "user" Then
                    mycommand = "Select *,convert(varchar(20),sub_expiredate,101) as 'expdate' from tbl_users where company_pk ='" & Session("company_pk") & "' and (status='Active' or status='New') and uid='" & Session("userid") & "'"
                Elseif Session("role") = "GOD" then
                    mycommand = "Select *,convert(varchar(20),sub_expiredate,101) as 'expdate' from tbl_users "     
                else
                    mycommand = "Select *,convert(varchar(20),sub_expiredate,101) as 'expdate' from tbl_users where company_pk ='" & Session("company_pk") & "' and (status='Active' or status='New' or status='Trial')"
                End If

            Else
                If Session("role") = "user" Then
                    mycommand = "Select *,convert(varchar(20),sub_expiredate,101) as 'expdate' from tbl_users where company_pk ='" & Session("company_pk") & "' and uid='" & Session("userid") & "'"
                 Elseif Session("role") = "GOD" then
                    mycommand = "Select *,convert(varchar(20),sub_expiredate,101) as 'expdate' from tbl_users "   
                 Else
                    mycommand = "Select *,convert(varchar(20),sub_expiredate,101) as 'expdate' from tbl_users where company_pk ='" & Session("company_pk") & "'"
                End If

            End If

            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_users")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_users"))
					 	dvProducts.RowFilter = "(UID like '%" & Request.QueryString("search") & "%' or lname like '%" & Request.QueryString("search") & "%' or  fname like '%" & Request.QueryString("search") & "%' or company LIKE '%" & Request.QueryString("search") & "%')  "
              	
              	 If Request.QueryString("status") <> "*" Then
                    dvProducts.RowFilter = dvProducts.RowFilter + " and status like '" & Request.QueryString("status") & "'"
                End If
                If Request.QueryString("role") <> "*" Then
                    dvProducts.RowFilter = dvProducts.RowFilter + " and role like '" & Request.QueryString("role") & "'"
                End If
                users.DataSource = dvProducts
                users.DataBind()

            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

        End Sub
        
         Sub myDataGrid_PageChangerA(ByVal Source As Object, ByVal E As DataGridPageChangedEventArgs)
            ' Set the CurrentPageIndex before binding the grid 
            users.CurrentPageIndex = E.NewPageIndex
            BindUserGrid()

        End Sub
        
         Sub btnsearch(ByVal Source As System.Object, ByVal e As System.EventArgs)
           
				users.CurrentPageIndex = 0
            searchstring()
        End Sub
        
         Sub searchstring()

            Dim psearch, pustat, urole  As String
            
         	If l_search.Text = "" Then
                psearch = "*"
            Else
                psearch = l_search.Text
                Session("search") = "text"
            End If
            
            If dd_userstat.SelectedItem.Text = "All" Then
                pustat = "*"
            Else
                pustat = dd_userstat.SelectedItem.Text
            End If
            If dd_userrole.SelectedItem.Text = "All" Then
                urole = "*"
            Else
                urole = dd_userrole.SelectedItem.Text
            End If
            
            dim querystring as string = "usermaint.aspx?search=" & psearch & "&status=" & pustat & "&role=" & urole
				Session("qstringA") = querystring
            Response.Redirect(querystring)
         End Sub
         
           Sub checkquerystring()

            If Request.QueryString("search") <> "*" Then
                Session("search") = "Text"
                l_search.Text = Request.QueryString("search")
            Else
                l_search.Text = ""
            End If

            If Request.QueryString("status") = "*" Then
                dd_userstat.SelectedIndex = dd_userstat.Items.IndexOf(dd_userstat.Items.FindByText("All"))
            Else
                dd_userstat.SelectedIndex = dd_userstat.Items.IndexOf(dd_userstat.Items.FindByText(Request.QueryString("status")))
            End If
            If Request.QueryString("role") = "*" Then
                dd_userrole.SelectedIndex = dd_userrole.Items.IndexOf(dd_userrole.Items.FindByText("All"))
            Else
                dd_userrole.SelectedIndex = dd_userrole.Items.IndexOf(dd_userrole.Items.FindByText(Request.QueryString("role")))
            End If
            
         end sub
         
         Sub clearall (Source As System.Object, e As System.EventArgs)
		
             Response.Redirect("usermaint.aspx?search=*&status=*&role=*")
            
       end sub 
       
        
         Public Sub Logoutuser(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Dim x As Button = Source

            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(3).Text
            logoutuser(content)
            
        end sub
        public sub logoutuser(id as string)
        	 Dim strConnection As String
         Dim sqlConn As SqlConnection
         Dim sqlCmd As SqlCommand
         Dim strSql As String = "delete from dbo.tbl_currentlogons where lg_uid ='" & id & "'"
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
        Sub ItemDataBoundEventHandler(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                'Check who steps are for
                Dim removeuser As Button
                dim logoutuserA as button
                
                removeuser = e.Item.Cells(9).FindControl("bbremoveuser")
                logoutuserA = e.Item.Cells(9).FindControl("bbloguserout")
                removeuser.Attributes.Add("onClick", "return confirm('Are you sure to delete this item?')")

                Dim itemCellwho As TableCell = e.Item.Cells(3)
                Dim itemCellwhotext As String = itemCellwho.Text
                if session("role")="GOD" then
                	logoutuserA.visible=true
                else
                	logoutuserA.visible=false
                end if
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
            Dim strSql As String = "delete from tbl_users where users_tbl_PK=" & id
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
     end class
end namespace