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
    Public Class bugreport
        Inherits PageTemplate
        Public bugs As DataGrid
        Public totalbugs As Label
        Public pnladdbug, pnlcurrentbugs, pnlclosed, pnlsavebtn,pnlsaved As Panel
        Public bdate, burl, bwhat, closedate, TextBox1 As TextBox
        Public bwho, bstatus, closedby, btype As DropDownList
        Public bbgOpen, bbgClosed, bbgAll As LinkButton
        public lbltext as label



        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles mybase.load

            If Not (Page.IsPostBack) Then
                If Request.QueryString("action") = "edit" Then
                    pnladdbug.Visible = True
                    pnlcurrentbugs.Visible = False
                    pnlclosed.Visible = False

                    bindfields()

                Else
                    Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
                    bdate.Text = rightNow
                    checkbgfilter()
                    bingbugs()
                    pnlsavebtn.Visible = False
                    pnlclosed.Visible = False
                End If
                bindusers()
                bindusersC()
                lbltext.text="What in the world did you do?"
                if session("role")="GOD" then
                	'pnlcurrentbugs.visible=true
                	
                else
                	pnlcurrentbugs.visible=false
                	pnladdbug.visible=true
                	
                	bwho.SelectedIndex = bwho.Items.IndexOf(bwho.Items.FindByvalue(session("userid")))
                	pnlclosed.visible=false
                	pnlsavebtn.visible=true
                	bwho.enabled=false
               end if
              
               

            End If
            pagesetup()

        End Sub
        Sub bindusers()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String
    			myCommand = "Select UID, fname + ' ' + lname as 'name' from dbo.tbl_users where betauser='Yes' "
    			Dim objCmd As New SqlCommand(myCommand, myConnection)

            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                bwho.DataSource = dataReader
                bwho.DataTextField = "name"
                bwho.DataValueField = "UID"
                bwho.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
         end sub
         Sub bindusersC()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String
    			myCommand = "Select UID, fname + ' ' + lname as 'name' from dbo.tbl_users where betauser='Yes' "
    			Dim objCmd As New SqlCommand(myCommand, myConnection)

            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                closedby.DataSource = dataReader
                closedby.DataTextField = "name"
                closedby.DataValueField = "UID"
                closedby.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
          

	  	End Sub
        
        Public Sub clickaddbug(ByVal sender As Object, ByVal e As EventArgs)
            pnladdbug.Visible = True
            pnlcurrentbugs.Visible = False
            pnlsavebtn.Visible = True
            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            bdate.Text = rightNow
            burl.Text = ""
            bwhat.Text = ""
            bstatus.SelectedItem.Text = "Open"
        End Sub
       
        Public Sub closethebug(ByVal sender As Object, ByVal e As EventArgs)
            If bstatus.SelectedItem.Text = "Closed" Then
                Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
                closedate.Text = rightNow
                pnladdbug.Visible = False
                pnlsavebtn.Visible = True
                pnlcurrentbugs.Visible = False
                pnlclosed.Visible = True
            Else
               
                bstatus.SelectedIndex = bstatus.Items.IndexOf(bstatus.Items.FindByText("Open"))
                 if btype.selecteditem.text="Bug" then
               	lbltext.text="What in the world did you do?"
               elseif btype.selecteditem.text="Enhancement" then
               	lbltext.text="What is that you think we could improve?"
              elseif btype.selecteditem.text="Feedback" then
               	lbltext.text="What would you like to say?" 
              else	
               	lbltext.text="Just the facts please!" 
              end if
            End If
           
        End Sub

        Public Sub cancelbug(ByVal sender As Object, ByVal e As EventArgs)
           if  session("role")="GOD" then
		           If bstatus.SelectedItem.Text = "Closed" Then
		                pnladdbug.Visible = True
		                pnlsavebtn.Visible = True
		                pnlcurrentbugs.Visible = False
		                pnlclosed.Visible = False
		                bstatus.SelectedIndex = bstatus.Items.IndexOf(bstatus.Items.FindByText("Open"))
		
		            Else
		                pnladdbug.Visible = False
		                pnlsavebtn.Visible = False
		                pnlcurrentbugs.Visible = True
		                pnlclosed.Visible = False
		                checkbgfilter()
		                bingbugs()
		            End If
		      else
		      	response.redirect("default.aspx")
		      	
		      	
		      end if
            
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

        Public Sub bingbugs()
            Dim strpropID As String = Request.QueryString("id")
            Dim strUID As String = Session("userid")
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionStringBUG"))
            Dim mycommand As String

            If Session("bgfilter") = "Open" Then
                mycommand = "Select * from tbl_bugreport where bg_status='Open' order by bg_date desc"
            ElseIf Session("bgfilter") = "Closed" Then
                mycommand = "Select * from tbl_bugreport where bg_status='Closed' order by bg_date desc"
            Else
                mycommand = "Select * from tbl_bugreport"
            End If


            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_bugreport")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_bugreport"))
                bugs.DataSource = dvProducts
                totalbugs.Text = dvProducts.Count.ToString

                bugs.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
        End Sub

        Sub bug_PageChanger(ByVal Source As Object, _
             ByVal E As DataGridPageChangedEventArgs)
            ' Set the CurrentPageIndex before binding the grid 
            bugs.CurrentPageIndex = E.NewPageIndex
            bingbugs()

        End Sub

        Sub savebug(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionStringBUG"))
            Dim sqlproc As String

            If Request.QueryString("action") = "edit" Then
                sqlproc = "sp_updateBug"
            Else
                sqlproc = "sp_addbug"
            End If

            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

            If Request.QueryString("action") = "edit" Then
                Dim prmbugno As New SqlParameter("@bugno", SqlDbType.Int)
                prmbugno.Value = Request.QueryString("bugno")
                myCommandADD.Parameters.Add(prmbugno)
            Else
                Dim prmdate As New SqlParameter("@date", SqlDbType.DateTime)
                prmdate.Value = bdate.Text
                myCommandADD.Parameters.Add(prmdate)
            End If

            Dim prmstatus As New SqlParameter("@status", SqlDbType.VarChar, 50)
            prmstatus.Value = bstatus.SelectedItem.Text
            myCommandADD.Parameters.Add(prmstatus)

            Dim prmtype As New SqlParameter("@type", SqlDbType.VarChar, 50)
            prmtype.Value = btype.SelectedItem.Text
            myCommandADD.Parameters.Add(prmtype)

            Dim prmwho As New SqlParameter("@who", SqlDbType.VarChar, 50)
            prmwho.Value = bwho.SelectedItem.value
            myCommandADD.Parameters.Add(prmwho)

            Dim prmurl As New SqlParameter("@url", SqlDbType.VarChar, 255)
            prmurl.Value = burl.Text
            myCommandADD.Parameters.Add(prmurl)

            Dim prmwhat As New SqlParameter("@what", SqlDbType.Text)
            prmwhat.Value = bwhat.Text
            myCommandADD.Parameters.Add(prmwhat)

            If bstatus.SelectedItem.Text = "Closed" Then

                Dim prmcwho As New SqlParameter("@closedby", SqlDbType.VarChar, 50)
                prmcwho.Value = closedby.SelectedItem.Text
                myCommandADD.Parameters.Add(prmcwho)

                Dim prmcdate As New SqlParameter("@closeddate", SqlDbType.DateTime)
                prmcdate.Value = closedate.Text
                myCommandADD.Parameters.Add(prmcdate)

                Dim prmresult As New SqlParameter("@result", SqlDbType.Text)
                prmresult.Value = TextBox1.Text
                myCommandADD.Parameters.Add(prmresult)
            Else
                Dim prmcwho As New SqlParameter("@closedby", SqlDbType.VarChar, 50)
                prmcwho.Value = DBNull.Value
                myCommandADD.Parameters.Add(prmcwho)

                Dim prmcdate As New SqlParameter("@closeddate", SqlDbType.DateTime)
                prmcdate.Value = DBNull.Value
                myCommandADD.Parameters.Add(prmcdate)

                Dim prmresult As New SqlParameter("@result", SqlDbType.Text)
                prmresult.Value = DBNull.Value
                myCommandADD.Parameters.Add(prmresult)

            End If

            Try
                myConnectionADD.Open()
                myCommandADD.ExecuteNonQuery()
                myConnectionADD.Close()
            Catch SQLexc As SqlException
                Response.Write("Insert Failed. Error Details are: " & SQLexc.ToString())
            Finally
                myConnectionADD.Close()
            End Try

				if session("role")="GOD" then
					pnladdbug.Visible = False
	            pnlcurrentbugs.Visible = True
	            pnlsavebtn.Visible = False
	            pnlclosed.Visible = False
	            checkbgfilter()
	            bingbugs()
				else				
					pnlsaved.visible=true
					pnladdbug.visible=false
                	
                 	pnlsavebtn.visible=false
				end if            

        End Sub

        Sub editbug(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(0).Text
            Response.Redirect("bugreport.aspx?bugno=" & content & "&action=edit")
            ' Response.Write("<script>window.open" & _
            '   "('bugreport.aspx?bugno=" & content & "&action=edit&','_new', 'width=800,height=500');</script>")

        End Sub

        Sub bindfields()
            Dim strUID As String = Session("userid")
            Dim strSql As String = "SELECT * from tbl_bugreport Where bg_tbl_pk=" & Request.QueryString("bugno")
            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionStringBUG"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    If Sqldr("bg_status") IsNot DBNull.Value Then
                        bstatus.SelectedIndex = bstatus.Items.IndexOf(bstatus.Items.FindByText(Sqldr("bg_status")))
                    End If
                    If Sqldr("bg_who") IsNot DBNull.Value Then
                        bwho.SelectedIndex = bwho.Items.IndexOf(bwho.Items.FindByText(Sqldr("bg_who")))
                    End If
                    If Sqldr("bg_date") IsNot DBNull.Value Then
                        bdate.Text = Sqldr("bg_date")
                    End If

                    If Sqldr("bg_url") IsNot DBNull.Value Then
                        burl.Text = Sqldr("bg_url")
                    End If
                    If Sqldr("bg_what") IsNot DBNull.Value Then
                        bwhat.Text = Sqldr("bg_what")
                    End If

                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try


        End Sub

        Sub bgopen(ByVal sender As Object, ByVal e As EventArgs)
            Session("bgfilter") = "Open"
            bugs.CurrentPageIndex = 0
            checkbgfilter()
            bingbugs()
        End Sub
        Sub bgclosed(ByVal sender As Object, ByVal e As EventArgs)
            Session("bgfilter") = "Closed"
            bugs.CurrentPageIndex = 0
            checkbgfilter()
            bingbugs()
        End Sub
        Sub bgAll(ByVal sender As Object, ByVal e As EventArgs)
            Session("bgfilter") = "All"
            bugs.CurrentPageIndex = 0
            checkbgfilter()
            bingbugs()
        End Sub

        Sub checkbgfilter()
            If Session("bgfilter") = "Open" Then
                bbgClosed.Attributes("style") = "color:#000000; font-family:arial; font-size:8pt; cursor:hand"
                bbgOpen.Attributes("style") = "color:#ff0000; font-family:arial; font-size:8pt; cursor:hand"
                bbgAll.Attributes("style") = "color:#000000; font-family:arial; font-size:8pt; cursor:hand"
            ElseIf Session("bgfilter") = "Closed" Then
                bbgClosed.Attributes("style") = "color:#ff0000; font-family:arial; font-size:8pt; cursor:hand"
                bbgOpen.Attributes("style") = "color:#000000; font-family:arial; font-size:8pt; cursor:hand"
                bbgAll.Attributes("style") = "color:#000000; font-family:arial; font-size:8pt; cursor:hand"
            ElseIf Session("bgfilter") = "All" Then
                bbgClosed.Attributes("style") = "color:#000000; font-family:arial; font-size:8pt; cursor:hand"
                bbgOpen.Attributes("style") = "color:#000000; font-family:arial; font-size:8pt; cursor:hand"
                bbgAll.Attributes("style") = "color:#ff0000; font-family:arial; font-size:8pt; cursor:hand"
            Else
                Session("bgfilter") = "Open"
                bbgClosed.Attributes("style") = "color:#000000; font-family:arial; font-size:8pt; cursor:hand"
                bbgOpen.Attributes("style") = "color:#ff0000; font-family:arial; font-size:8pt; cursor:hand"
                bbgAll.Attributes("style") = "color:#000000; font-family:arial; font-size:8pt; cursor:hand"
            End If

        End Sub

        Sub ItemDataBoundEventHandler(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                'Check who steps are for
                Dim itemCellwho0 As TableCell = e.Item.Cells(2)
                
                Dim itemCellwhotext1 As String = itemCellwho0.Text
              
                Dim testbtn As Button
                testbtn = e.Item.Cells(0).FindControl("edittaskB")
                If itemCellwhotext1 = "Closed" Then
                    testbtn.Enabled = False
                End If

            End If
        End Sub

    End Class
end namespace