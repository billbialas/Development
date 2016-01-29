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
imports system.Globalization
imports system.net.mail
Imports System.Text


Namespace PageTemplate
    Public Class editad
        Inherits PageTemplate

        Public sideid, AdTitle, adtype, adcat, adsubcat, adtext, adcode, adno As Label
        Public ADVenues, dg_advenue, AdvenuesADD As DataGrid
        Public placead, viewmain As Panel
        Public textarea As HtmlGenericControl
        Public autopost As Button
        '--------------
        Public lb_leadno, lbl_adnoV, adstage, adtitleM, finwarning As Label
        Public admain, pnlsavedv, pnlconfirmfinalize, pnlnoadvenues, pnladdvenue, pnltxtbody, pnladtitle As Panel
        Public adposts As DataGrid
        Public ddlleadtypeFilter, ddlleadprogramFilter, dd_adtype, dd_status, dd_cat, advenue, adautor, adphoto As DropDownList
        Public B_FinalV, b_savee, B_Final, B_removead, exitV, test, B_post As Button
        Public venuname, venuecode, venueurl As TextBox
        Public privateven As CheckBox
        Public ddvenonline As DropDownList
        Public cell1, cell1a
        Private Shared venueonline, adkeycode, venhtml As String
        Public xx As TextBox
        Public hrline

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

            If Not (Page.IsPostBack) Then
                getadinfo()
                	Response.Redirect("createad.aspx?action=edit&adno=" & Request.QueryString("adno"))



                bindvenues()
                FillADvenues()
                placead.Visible = True
                hrline.visible = True
                'response.write( len(adtext.text))
                If Len(adtext.Text) > 300 Then
                    textarea.Attributes("style") = "vertical-align top; height: 100px; overflow:auto;"
                Else
                    textarea.Attributes("style") = "vertical-align top; height: 50px; overflow:auto;"
                End If
            End If
            pagesetup()

        End Sub
        
        Sub getadinfo()
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select * from dbo.tbl_LeadADs where tbl_leadad_pk='" & Request.QueryString("adno") & "'"

            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    adno.Text = Sqldr("tbl_leadad_pk")
                    AdTitle.Attributes("value") = Sqldr("ad_title")
                    AdTitle.Text = Sqldr("ad_title")
                    adtype.Attributes("value") = Sqldr("ad_leadtype")
                    adtype.Text = Sqldr("ad_leadtype")
                    adtext.Attributes("value") = Sqldr("ad_text")
                    adtext.Text = Sqldr("ad_text")
							session("adstage")=Sqldr("ad_stage")

                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
        End Sub
        Sub clonead(ByVal sender As Object, ByVal e As EventArgs)
            Response.Redirect("createad.aspx?action=clone&adno=" & adno.Text  )
        End Sub
        Sub ExitA(ByVal sender As Object, ByVal e As EventArgs)
            if request.querystring("source")="leads" then
            	response.redirect(session("qstring"))
            else 
             	Response.Redirect("admanager.aspx")
            end if
           
        End Sub
        Sub Addvenue(ByVal sender As Object, ByVal e As EventArgs)
            placead.Visible = True
            viewmain.Visible = False
            FillADvenues()
            bindvenuesADD()
        End Sub
        Sub ExitADEdit(ByVal sender As Object, ByVal e As EventArgs)
            Response.Redirect("editad.aspx?action=edit&adno=" & Request.QueryString("adno"))

        End Sub
       
        Sub insertadV(ByVal adcode As String)
            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String
            sqlproc = "sp_AddADVenue"

            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

            Dim prmadno As New SqlParameter("@av_leadads_FK", SqlDbType.Int)
            prmadno.Value = Request.QueryString("adno")
            myCommandADD.Parameters.Add(prmadno)

            Dim prmadvenue As New SqlParameter("@av_name", SqlDbType.VarChar, 50)
            prmadvenue.Value = advenue.SelectedItem.Text
            myCommandADD.Parameters.Add(prmadvenue)

            Dim prmarepsond As New SqlParameter("@av_autorespond", SqlDbType.VarChar, 50)
            prmarepsond.Value = adautor.SelectedItem.Text
            myCommandADD.Parameters.Add(prmarepsond)

            Dim prmaphoto As New SqlParameter("@av_photo", SqlDbType.VarChar, 50)
            prmaphoto.Value = adphoto.SelectedItem.Text
            myCommandADD.Parameters.Add(prmaphoto)

            Dim prmadcode As New SqlParameter("@av_key", SqlDbType.NVarChar, 255)
            prmadcode.Value = adcode
            myCommandADD.Parameters.Add(prmadcode)

            Dim prmadplaced As New SqlParameter("@av_adplaced", SqlDbType.VarChar, 50)
            prmadplaced.Value = "Unpublished"
            myCommandADD.Parameters.Add(prmadplaced)


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
       
        Public Sub bindvenuesADD()
            Dim strUID As String = Session("userid")
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String = "Select *,cast(av_leadads_FK as nvarchar(255)) as 'adno', cast(tbl_leadadvenues as nvarchar(255)) as 'venno' from tbl_LeadADVenues where av_leadads_FK ='" & Request.QueryString("adno") & "'"

            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_LeadADVenues")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_LeadADVenues"))

                AdvenuesADD.DataSource = dvProducts
                AdvenuesADD.DataBind()

            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
        End Sub
        Sub ItemDataBoundEventHandlerADD(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                 'Check who steps are for
                Dim itemCellwho As TableCell = e.Item.Cells(3)
                Dim itemcellwho2 As TableCell = e.Item.Cells(10)
                Dim itemCellwhotext As String = itemCellwho.Text
                Dim itemCellwhotext2 As String = itemcellwho2.Text
                Dim ChkSelected As HyperLink
                Dim autopostbtn As Button
                ChkSelected = e.Item.Cells(0).FindControl("Hyperlinkresult")
                autopostbtn = e.Item.Cells(0).FindControl("autopost")

               If itemCellwhotext2 = "Yes" Then
                    autopostbtn.Text = "Cancel"
                    e.Item.Cells(7).Enabled = False
                    ChkSelected.Text = "AutoPost"
                Else
                    If (itemCellwhotext = "Deferred" Or itemCellwhotext = "Unpublished") Then
                        ChkSelected.Text = "Unpublished"
                        e.Item.Cells(7).Enabled = True
                    Else
                        ChkSelected.Text = "Completed"
                        e.Item.Cells(7).Enabled = False
                        e.Item.Cells(8).Enabled = False
                    End If
                End If
            End If
        End Sub
        

        Public Sub autopost_ClickAdd(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(0).Text
            Dim content2 As String = item.Cells(10).Text
            updatevenue(content, content2)
            If content2 = "No" Then
                autopostsubmit(content, "insert")
            Else
                autopostsubmit(content, "delete")
            End If
            bindvenuesADD()
        End Sub
        '__________________________
        Sub ExitAD(ByVal sender As Object, ByVal e As EventArgs)
            Session("AdSaved") = "False"
            Session("ADStage") = ""
            session("aadno") = ""
            Response.Redirect("admanager.aspx")
        End Sub

        Sub ExitADV(ByVal sender As Object, ByVal e As EventArgs)
            admain.Visible = True
            placead.Visible = False
            pnlsavedv.Visible = False
            pnlconfirmfinalize.Visible = False
            bindfields()
            pnlnoadvenues.visible = False
        End Sub

        Sub cancelfinalize(ByVal sender As Object, ByVal e As EventArgs)
            pnlconfirmfinalize.Visible = False
            admain.Visible = True
            placead.Visible = False
            pnlsavedv.Visible = False
            Session("ADStage") = "Draft"
        End Sub

        Sub savead(ByVal sender As Object, ByVal e As EventArgs)

            If Request.QueryString("type") = "quick" Then
                If Session("AdSaved") <> "True" Then
                    Session("ADStage") = "Finalized"
                    insertad()
                    getadno()
                    Session("AdSaved") = "True"
                    placead.Visible = True
                    FillADvenues()
                    bindvenues()
                Else
                    insertad()
                End If
            Else
                If Session("AdSaved") <> "True" Then
                    Session("ADStage") = "Draft"
                    insertad()
                    getadno()
                    Session("AdSaved") = "True"
                Else
                    insertad()
                End If
            End If
            B_removead.Visible = True
        End Sub

        Sub finalize(ByVal sender As Object, ByVal e As EventArgs)
            'response.write("HEY")
            pnlconfirmfinalize.Visible = True
            admain.Visible = True
            placead.Visible = False
            pnlsavedv.Visible = False
            pnlnoadvenues.Visible = False


        End Sub

        Sub finalizesave(ByVal sender As Object, ByVal e As EventArgs)
            Session("ADStage") = "Finalized"
            finwarning.visible = False
            dd_status.SelectedItem.Text = "Active"
            insertad()
            getadno()
            Session("AdSaved") = "True"
            B_removead.visible = True
            adstage.text = "* Finalized *"
            pnlconfirmfinalize.Visible = False
            admain.Visible = True
            placead.Visible = False
            pnlsavedv.Visible = False
            pnlconfirmfinalize.Visible = False

            B_FinalV.visible = False
            b_savee.visible = False
            B_Final.visible = False
            adtitle.enabled = False
            dd_status.enabled = False
            ddlleadtypeFilter.Enabled = False
            ddlleadprogramFilter.Enabled = False
            adtext.Enabled = False
            B_post.Visible = True
            admain.Visible = False
            placead.Visible = True

            lbl_adnoV.Text = Request.QueryString("adno")
            FillADvenues()
            bindvenues()


        End Sub

        Sub postad(ByVal sender As Object, ByVal e As EventArgs)
            adautor.Visible = False
            cell1.visible = False
            cell1a.visible = False


            If Session("AdSaved") <> "True" Then
                Session("ADStage") = "Draft"
                insertad()
                getadno()
                Session("AdSaved") = "True"
            Else
                insertad()
            End If
            B_removead.Visible = True
            getadno()
            If Session("ADStage") = "Draft" Then
                B_FinalV.Visible = True
                finwarning.Visible = True
                finwarning.Text = "To publish AD to selected venues, AD must be Finalized"
            Else
                B_FinalV.Visible = False
            End If

            lbl_adnoV.Text = Request.QueryString("adno")
            admain.Visible = False
            placead.Visible = True

            FillADvenues()
            bindvenues()

        End Sub

        Sub postadVen(ByVal sender As Object, ByVal e As EventArgs)
            'Get next ADKEY and prefix with Venue code

            'Dim adkeycode As String = Left(advenue.SelectedItem.Text, 2) + getadkey()
            'adkeycode = adkeycode + getadkey()
            ' Response.Write(advenue.SelectedItem.Value)
            If advenue.SelectedItem.Text <> "Select.." Then
                getvenueinfo()
                adkeycode = adkeycode + getadkey()
                'Response.Write(venueonline)
                insertadV(adkeycode, venueonline)
                bindvenues()

                pnlsavedv.Visible = True
            End If
            'Response.Write(adkeycode)

            'Response.Redirect("postad.aspx?adno=" & Session("adno") & "&venue=" & advenue.SelectedItem.Text & "&adtype=" & Session("adtype"))
        End Sub

        Public Function checknoadvenues() As Integer
            Dim TotalNumberOfRowInDataGrid As Integer
            Return TotalNumberOfRowInDataGrid = CType(ADVenues.DataSource, DataView).Table.Rows.Count
            'response.write(TotalNumberOfRowInDataGrid)
        End Function

        ' Sub removeven(ByVal Source As System.Object, ByVal e As System.EventArgs)

        Sub removeven(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
            Dim content As String = ADVenues.DataKeys(e.Item.ItemIndex)

            'Dim x As Button = Source
            'Dim cell As TableCell = x.Parent
            'Dim item As DataGridItem = cell.Parent
            'Dim content As String = item.Cells(0).Text
            deletevenue(content)
            bindvenues()

        End Sub
        Sub setstatpub(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(0).Text
            updatevstat(content)
            bindvenues()

        End Sub


        Sub pubad(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim advenuePK As String = item.Cells(0).Text
            Dim adcode As String = item.Cells(18).Text
            Dim advenue As String = item.Cells(1).Text

            'If Request.Browser.Browser <> "IE" Then
               Response.Redirect("postadnotie.aspx?adno=" & adcode & "&venue=" & advenue & "&venueno=" & advenuePK & "&source=viewad")
				'
            'Else
            '    Response.Redirect("postad.aspx?adno=" & adcode & "&venue=" & advenue & "&venueno=" & advenuePK & "&source=viewad")
				'
            'End If

        End Sub
        Sub clipboard(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(4).Text
            'Response.Write(content)

            xx.Text = content
            Dim msg As String = ""
            msg = msg & "<Script Language='JavaScript'>"
            msg = msg & "copy(document.getElementById('xx'));"
            msg = msg & "</Script>"
            Response.Write(msg)
            ' javascript:window.clipboardData.setData('Text', txtProblemDescription.innerText)")





        End Sub

        Sub removead(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "delete from tbl_LeadADs where tbl_leadad_pk='" & Request.QueryString("adno") & "'"
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

            strSql = "delete from tbl_LeadADVenues where av_leadads_FK='" & Request.QueryString("adno") & "'"
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

            Session("AdSaved") = "False"
            Session("ADStage") = ""
            Session("aadno") = ""
            Response.Redirect("admanager.aspx")
        End Sub
        Public Sub getvenueinfo()

            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select * from tbl_xwalk where tbl_xwalk_pk='" & advenue.SelectedItem.Value & "'"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    If Sqldr("x_online") IsNot DBNull.Value Then
                        venueonline = Sqldr("x_online")
                    Else
                        venueonline = "No"
                    End If
                    If Sqldr("x_id") IsNot DBNull.Value Then
                        adkeycode = Sqldr("x_id")
                    Else
                        adkeycode = Left(advenue.SelectedItem.Text, 2)
                    End If
                    If Sqldr("x_html") IsNot DBNull.Value Then
                        venhtml = Sqldr("x_html")
                    Else
                        venhtml = "No"
                    End If

                End If


            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try


        End Sub


        'DB Binds     

        Sub deletevenue(ByVal id As String)
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "delete from tbl_LeadADVenues where tbl_leadadvenues='" & id & "'"
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

        End Sub

        Sub bindfields()
            Dim strUID As String = Session("userid")
            Dim strSql As String = "SELECT * from tbl_leadads where tbl_leadad_pk ='" & Request.QueryString("adno") & "'"

            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then

                    If Sqldr("ad_title") IsNot DBNull.Value Then
                        AdTitle.Text = Sqldr("ad_title")
                    End If
                    If Sqldr("ad_status") IsNot DBNull.Value Then
                        dd_status.SelectedIndex = dd_status.Items.IndexOf(dd_status.Items.FindByText(Sqldr("ad_status")))
                    End If
                    If Sqldr("ad_Leadtype") IsNot DBNull.Value Then
                        ddlleadtypeFilter.SelectedIndex = ddlleadtypeFilter.Items.IndexOf(ddlleadtypeFilter.Items.FindByText(Sqldr("ad_Leadtype")))
                    End If
                    If Sqldr("ad_Leadprogram") IsNot DBNull.Value Then
                        ddlleadprogramFilter.SelectedIndex = ddlleadprogramFilter.Items.IndexOf(ddlleadprogramFilter.Items.FindByText(Sqldr("ad_Leadprogram")))
                    End If
                    If Sqldr("ad_text") IsNot DBNull.Value Then
                        adtext.Text = Sqldr("ad_text")
                    End If
                    Session("adstage") = Sqldr("ad_stage")
                    adstage.Text = "* " & Sqldr("ad_stage") & " *"

                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        End Sub



        Public Function getadkey() As String
            Dim keycode As String
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select cast (AK_NextKey as nvarchar(255)) as 'kc'  from dbo.tbl_nextadkey"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    keycode = Sqldr("kc")
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try

            strSql = "update dbo.tbl_nextadkey set AK_NextKey =  AK_NextKey+1 "
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
            Return keycode
        End Function

        Sub FillLeadTypeDropDown()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='leadtype' and (x_company='" & Session("company_pk") & "' or x_company='All')"
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                ddlleadtypeFilter.DataSource = dataReader
                ddlleadtypeFilter.DataTextField = "x_descr"
                ddlleadtypeFilter.DataValueField = "tbl_xwalk_pk"
                ddlleadtypeFilter.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

        End Sub
        Sub FillLeadprogramDropDown()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='leadprogram' and (x_company='" & Session("company_pk") & "' or x_company='All')"
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                ddlleadprogramFilter.DataSource = dataReader
                ddlleadprogramFilter.DataTextField = "x_descr"
                ddlleadprogramFilter.DataValueField = "tbl_xwalk_pk"
                ddlleadprogramFilter.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

        End Sub
        Sub FillADCatDropDown()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='AdType' and (x_company='" & Session("company_pk") & "' or x_company='All')"
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                dd_adtype.DataSource = dataReader
                dd_adtype.DataTextField = "x_descr"
                dd_adtype.DataValueField = "tbl_xwalk_pk"
                dd_adtype.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

        End Sub

        Sub FillADvenues()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select tbl_xwalk_pk, x_descr from dbo.tbl_xwalk where x_type='leadsource' and (x_company='" & Session("company_pk") & "' or x_company='All' or x_Uid='" & Session("userid") & "')"
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

        Sub insertad()
            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String

            If (Request.QueryString("action") = "new" Or Request.QueryString("action") = "clone") Then
                If Session("AdSaved") = "True" Then
                    sqlproc = "sp_updateAD"
                Else
                    sqlproc = "sp_AddAD"
                End If
            ElseIf Request.QueryString("action") = "edit" Then
                sqlproc = "sp_updateAD"
            End If

            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

            If Session("AdSaved") = "True" Then
                Dim prmadpk As New SqlParameter("@adno", SqlDbType.Int)
                prmadpk.Value = Request.QueryString("adno")
                myCommandADD.Parameters.Add(prmadpk)
            End If

            Dim prmstage As New SqlParameter("@adstage", SqlDbType.VarChar, 50)
            prmstage.Value = Session("ADStage")
            myCommandADD.Parameters.Add(prmstage)

            Dim prmuid As New SqlParameter("@aduid", SqlDbType.VarChar, 50)
            prmuid.Value = Session("userid")
            myCommandADD.Parameters.Add(prmuid)

            Dim prmadtitle As New SqlParameter("@adtitle", SqlDbType.VarChar, 255)
            prmadtitle.Value = AdTitle.Text
            myCommandADD.Parameters.Add(prmadtitle)

            Dim prmadtext As New SqlParameter("@adtext", SqlDbType.Text)
            prmadtext.Value = adtext.Text
            myCommandADD.Parameters.Add(prmadtext)

            Dim prmadstat As New SqlParameter("@dd_status", SqlDbType.VarChar, 30)
            If Session("ADStage") = "Finalized" Then
                prmadstat.Value = "Active"
            Else
                prmadstat.Value = dd_status.SelectedItem.Text
            End If
            myCommandADD.Parameters.Add(prmadstat)

            Dim prmadleadtype As New SqlParameter("@ddlleadtypeFilter", SqlDbType.VarChar, 30)
            prmadleadtype.Value = ddlleadtypeFilter.SelectedItem.Text
            myCommandADD.Parameters.Add(prmadleadtype)

            Dim prmadleadprogram As New SqlParameter("@ddlleadprogramFilter", SqlDbType.VarChar, 30)
            prmadleadprogram.Value = ddlleadprogramFilter.SelectedItem.Text
            myCommandADD.Parameters.Add(prmadleadprogram)


            'Dim prmadtype As New SqlParameter("@dd_adtype", SqlDbType.VarChar, 30)
            'prmadtype.Value = dd_adtype.SelectedItem.Text
            'myCommandADD.Parameters.Add(prmadtype)
            'Session("adtype") = dd_adtype.SelectedItem.Text

            'Dim prmadcat As New SqlParameter("@dd_cat", SqlDbType.VarChar, 30)
            'prmadcat.Value = dd_cat.SelectedItem.Text
            'myCommandADD.Parameters.Add(prmadcat)

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

        Sub getadno()
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
            Dim strSql As String = "select cast(max(tbl_leadad_pk) as varchar(255)) as 'AdPK'  from dbo.tbl_LeadADs"
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)
                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader

                If Sqldr.Read() Then
                    Session("adno") = Sqldr("AdPK")
                End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try
        End Sub

        Sub insertadV(ByVal adcode As String, ByVal vonline As String)
            Dim rightNow As DateTime = DateTime.Now.ToShortDateString()
            'Dim rightNow as string= DateTime.Now.ToString("MM/dd/yyyy")
            Dim RightNowAdd As DateTime = DateTime.Now
            Dim supportedFormats() As String = New String() {"M/dd/yyyy", "M/d/yyyy", "MM/dd/yyyy", "MM/dd/yy", "ddMMMyyyy", "dMMMyyyy"}
            Dim myConnectionADD As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim sqlproc As String
            sqlproc = "sp_AddADVenue"

            Dim myCommandADD As New SqlCommand(sqlproc, myConnectionADD)
            myCommandADD.CommandType = CommandType.StoredProcedure

            Dim prmadno As New SqlParameter("@av_leadads_FK", SqlDbType.Int)
            prmadno.Value = Request.QueryString("adno")
            myCommandADD.Parameters.Add(prmadno)

            Dim prmadvenue As New SqlParameter("@av_name", SqlDbType.VarChar, 50)
            prmadvenue.Value = advenue.SelectedItem.Text
            myCommandADD.Parameters.Add(prmadvenue)

            Dim prmarepsond As New SqlParameter("@av_autorespond", SqlDbType.VarChar, 50)
            prmarepsond.Value = adautor.SelectedItem.Text
            myCommandADD.Parameters.Add(prmarepsond)

            Dim prmaphoto As New SqlParameter("@av_photo", SqlDbType.VarChar, 50)
            prmaphoto.Value = adphoto.SelectedItem.Text
            myCommandADD.Parameters.Add(prmaphoto)

            Dim prmadcode As New SqlParameter("@av_key", SqlDbType.NVarChar, 255)
            prmadcode.Value = adcode
            myCommandADD.Parameters.Add(prmadcode)

            Dim prmadplaced As New SqlParameter("@av_adplaced", SqlDbType.VarChar, 50)
            If vonline = "No" Then
                prmadplaced.Value = "Published"
            Else
                prmadplaced.Value = "Unpublished"
            End If

            myCommandADD.Parameters.Add(prmadplaced)

            Dim prmonline As New SqlParameter("@av_online", SqlDbType.VarChar, 50)
            prmonline.Value = vonline
            myCommandADD.Parameters.Add(prmonline)

            Dim prmkeyurl As New SqlParameter("@av_keyurl", SqlDbType.NVarChar, 255)
            prmkeyurl.Value = System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & adcode
            myCommandADD.Parameters.Add(prmkeyurl)

            Dim prmAllText As New SqlParameter("@av_textAll", SqlDbType.Text)
            prmAllText.Value = adtext.Text.Replace(vbCrLf, "<br>") & "<br><br>Please Click-> " & System.Configuration.ConfigurationManager.AppSettings("CurrentWebURL") & "/intake.aspx?adcode=" & adcode
            myCommandADD.Parameters.Add(prmAllText)

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

        Public Sub bindvenues()
            Dim strUID As String = Session("userid")
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String = "Select *,cast(av_leadads_FK as nvarchar(255)) as 'adno', cast(tbl_leadadvenues as nvarchar(255)) as 'venno' from tbl_LeadADVenues where av_leadads_FK ='" & Request.QueryString("adno") & "'"

            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_LeadADVenues")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_LeadADVenues"))

                ADVenues.DataSource = dvProducts
                ADVenues.DataBind()

            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
            'response.write(checknoadvenues())

            If checknoadvenues() < 0 Then
                pnlnoadvenues.Visible = True
                pnlsavedv.Visible = False
            Else
                pnlnoadvenues.Visible = False
                pnlsavedv.Visible = True
            End If


        End Sub

        'Page Layout
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

        Sub ItemDataBoundEventHandler(ByVal sender As Object, ByVal e As DataGridItemEventArgs)
            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
                'Check who steps are for
                Dim itemCellwho0 As TableCell = e.Item.Cells(0)
                Dim itemCellwho As TableCell = e.Item.Cells(5)
                Dim itemcellwho2 As TableCell = e.Item.Cells(12)
                Dim itemcelladtext As TableCell = e.Item.Cells(19)
                Dim itemCellgrabtext As TableCell = e.Item.Cells(7)

                Dim itemcellwho3 As TableCell = e.Item.Cells(2)
                Dim itemcellwho4 As TableCell = e.Item.Cells(4)
                Dim itemCellwhotext0 As String = itemCellwho0.Text
                Dim itemCellwhotext As String = itemCellwho.Text
                Dim itemCellwhotext2 As String = itemcellwho2.Text
                Dim itemCellwhotext3 As String = itemcellwho3.Text
                Dim itemCellwhotext4 As String = itemcellwho4.Text
                Dim itemcelladtexttext As String = itemcelladtext.Text

                'Dim ChkSelected As HyperLink
                'Dim autopostbtn As Button
                'ChkSelected = e.Item.Cells(0).FindControl("Hyperlinkresult")
                'autopostbtn = e.Item.Cells(0).FindControl("autopost")
                Dim testbtn As System.Web.UI.HtmlControls.HtmlInputButton
                testbtn = e.Item.Cells(0).FindControl("test")
                Dim removeadv As Button
                removeadv = e.Item.Cells(0).FindControl("removevenue")

                removeadv.Attributes.Add("onClick", "return confirm('Are you sure to delete this item?')")

                'Dim ss As String = "copy(document.getElementById('lbl" + itemCellwhotext0 + "'));"
                Dim newurl As String
                ' If venhtml = "Yes" Then
                'newurl = "<a href='" & itemCellwhotext4 & "'><img src='http://gimp.gochoiceone.com/images/clickherer01.jpg' alt='http://gimp.gochoiceone.com/images/clickherer01.jpg' /></a>"
                'Else
                newurl = itemcelladtexttext
                'End If
                Dim ss As String = "copy('" & newurl & "');"
                ' Response.Write(ss)
                testbtn.Attributes("onclick") = ss
                'testbtn.Attributes.Add("onclick", "copy(document.getElementById('xx'));")
                '"javascript:window.clipboardData.setData('Text', document.getElementById('xx'))")
                If itemCellwhotext = "Published" Then
                    Dim btnmkpub As Button
                    btnmkpub = e.Item.Cells(0).FindControl("markpub")
                    btnmkpub.Enabled = False
                    Dim btnpub As Button
                    btnpub = e.Item.Cells(0).FindControl("Publish")
                    btnpub.Enabled = False
                End If
                If Request.Browser.Browser <> "IE" Then
                    testbtn.Visible = False
                    itemCellgrabtext.Visible = False

                End If

            End If
        End Sub
        Sub updatevstat(ByVal venupk As String)
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand

            Dim strSql As String
            strSql = "update dbo.tbl_LeadADVenues set av_adplaced = 'Published', av_createdate=getdate() where tbl_leadadvenues='" & venupk & "'"


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

        End Sub

        Sub addnewvenue(ByVal Source As System.Object, ByVal e As System.EventArgs)
            pnladdvenue.Visible = True
            venuname.Text = ""
            venuecode.Text = ""
            venueurl.Text = ""
            privateven.Checked = False
        End Sub

        Sub savenewvenue(ByVal Source As System.Object, ByVal e As System.EventArgs)
            If checkifcodeexists() = False Then

                Dim strConnection As String
                Dim sqlConn As SqlConnection
                Dim sqlCmd As SqlCommand

                Dim strSql As String
                If privateven.Checked Then
                    strSql = "insert into dbo.tbl_xwalk  (x_type,x_descr,x_id,x_url,x_UID,x_online) values('leadsource','" & venuname.Text & "','" & venuecode.Text & "','" & venueurl.Text & "','" & Session("userid") & "','" & ddvenonline.SelectedItem.Text & "')"
                Else
                    strSql = "insert into dbo.tbl_xwalk  (x_type,x_descr,x_company,x_id,x_url,x_UID,x_online) values('leadsource','" & venuname.Text & "','All','" & venuecode.Text & "','" & venueurl.Text & "','" & Session("userid") & "','" & ddvenonline.SelectedItem.Text & "')"

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
                pnladdvenue.Visible = False
                FillADvenues()

            Else
                venuname.Text = "EXISTS"
            End If


        End Sub
        Sub savenewvenueExit(ByVal Source As System.Object, ByVal e As System.EventArgs)
            pnladdvenue.Visible = False
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

        Public Sub autopost_Click(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(0).Text
            Dim content2 As String = item.Cells(10).Text
            updatevenue(content, content2)
            If content2 = "No" Then
                autopostsubmit(content, "insert")
            Else
                autopostsubmit(content, "delete")
            End If
            bindvenues()
        End Sub

        Public Sub autopostsubmit(ByVal id As String, ByVal action As String)
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand

            Dim strSql As String

            If action = "insert" Then
                strSql = "insert into dbo.tbl_autopostqueue  (ap_adno,ap_advenno,ap_duedate,ap_status) values('" & Session("adno") & "','" & id & "', getdate()+1 ,'Submitted')"
            Else
                strSql = "delete from dbo.tbl_autopostqueue  where ap_advenno='" & id & "'"

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
        End Sub

        Public Sub updatevenue(ByVal id As String, ByVal ap As String)
            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand

            Dim strSql As String
            If ap = "Yes" Then
                strSql = "update tbl_LeadADVenues  set av_autopost='No' from tbl_LeadADVenues where tbl_leadadvenues='" & id & "'"
            Else
                strSql = "update tbl_LeadADVenues  set av_autopost='Yes' from tbl_LeadADVenues where tbl_leadadvenues='" & id & "'"

            End If

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
End Namespace