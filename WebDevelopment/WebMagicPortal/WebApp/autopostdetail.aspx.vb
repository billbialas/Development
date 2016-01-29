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
    Public Class autopostDetail
        Inherits PageTemplate
        
        Public lbladno, lblvenue, lbluid, lbldue, lblfdate, lbltdate As Label
        public txtadtitle,txtadtext,txtpostno as textbox
        public aphistory as datagrid
        public dd_status as dropdownlist
        public pnlaphistory,pnlapdetail as panel
        private shared strvenno,initstat as string

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles mybase.load

            If Not (Page.IsPostBack) Then
					bindfields()
					
            End If
            pagesetup()

        End Sub
        public sub showhist(ByVal Source As System.Object, ByVal e As System.EventArgs)
	        	pnlaphistory.visible=true
	        	pnlapdetail.visible=false
	        	bindhist()
        end sub
        public sub exithist(ByVal Source As System.Object, ByVal e As System.EventArgs)
	        	pnlaphistory.visible=false
	        	pnlapdetail.visible=true
	        
        end sub
        public sub createrqst(ByVal Source As System.Object, ByVal e As System.EventArgs)
	        	
	        
        end sub
         public sub exitdetail(ByVal Source As System.Object, ByVal e As System.EventArgs)
	        	response.redirect("autopost.aspx")
	        
        end sub
        Public Sub savedetail(ByVal Source As System.Object, ByVal e As System.EventArgs)

            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand

            Dim strSql As String
            If dd_status.SelectedItem.Text = "Submitted" Or dd_status.SelectedItem.Text = "Canceled" Then
                strSql = "update dbo.tbl_autopostqueue set ap_status = '" & dd_status.SelectedItem.Text & "' where ap_tbl_pk=" & Request.QueryString("id")
            ElseIf dd_status.SelectedItem.Text = "Inprocess" Then
                strSql = "update dbo.tbl_autopostqueue set ap_status = '" & dd_status.SelectedItem.Text & "', ap_processstart=getdate(), ap_completedby='" & Session("userid") & "' where ap_tbl_pk=" & Request.QueryString("id")

            ElseIf dd_status.SelectedItem.Text = "Completed" Then
                strSql = "update dbo.tbl_autopostqueue set ap_status = '" & dd_status.SelectedItem.Text & "', ap_processend=getdate(), ap_completedby='" & Session("userid") & "' where ap_tbl_pk=" & Request.QueryString("id")
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

            If dd_status.SelectedItem.Text = "Inprocess" And initstat <> "Inprocess" Then
                writehist(dd_status.SelectedItem.Text)
            ElseIf dd_status.SelectedItem.Text = "Completed" And initstat <> "Completed" Then
                writehist(dd_status.SelectedItem.Text)
            ElseIf dd_status.SelectedItem.Text = "Canceled" And initstat <> "Canceled" Then
                writehist(dd_status.SelectedItem.Text)
            End If
            If txtpostno.Text = "" Then

            Else
                updatepostno()
            End If
            If  dd_status.SelectedItem.Text = "Inprocess" Then
                updateadvstat("Inprocess")
            ElseIf dd_status.SelectedItem.Text = "Completed" Then
                updateadvstat("Published")
            End If


        End Sub
        Sub updateadvstat(ByVal res As String)

            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand

            Dim strSql As String
            strSql = "update tbl_LeadADVenues set av_adplaced = '" & res & "' where tbl_leadadvenues=" & strvenno


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
        
         sub writehist(stat as string)
         	Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand

            Dim strSql As String
            dim stattype as string
            dim stattypenote as string
             
             if stat = "Inprocess" then
             	stattype="Posting"
             	stattypenote ="Posting Process has begun for this record"
             elseif stat = "Completed" then
             	stattype="Completed"
             	stattypenote ="Posting Process Completed for this record"
             elseif stat = "Canceled" then
             	stattype="Canceled"
             	stattypenote ="Posting Process has been canceled for this record"
             elseif stat = "Request" then
             	stattype="Request"
             	stattypenote ="A Processing Request has been made."
             	
             end if
            
            strSql = "insert into tbl_autopostqueueHistory (aphist_type, aphist_requestor,aphist_requestdate,aphist_notes,aphist_apfk) values('" & stattype  & "','" & session("userid") & "',getdate(),'" & stattypenote & "','" & request.querystring("id") & "')"
           

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
         
         
         end sub
         
        
        sub updatepostno()
        
        		Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand

            Dim strSql As String
            strSql = "update tbl_LeadADVenues set av_Postingno = '" & txtpostno.text  & "' where tbl_leadadvenues=" & strvenno
           

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
        end sub
        
        
        public sub bindfields()
           Dim strSql As String = "select * from tbl_autopostqueue join tbl_LeadADVenues on tbl_leadadvenues=ap_advenno join tbl_LeadADs on tbl_leadad_pk=ap_adno join tbl_users on uid=ad_userid where ap_tbl_pk=" & request.querystring("id")
           
            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then

                    If Sqldr("ap_adno") IsNot DBNull.Value Then
                        lbladno.Text = Sqldr("ap_adno")
                    End If
                    If Sqldr("av_name") IsNot DBNull.Value Then
                        lblvenue.Text = Sqldr("av_name")
                    End If
                    If Sqldr("ad_userid") IsNot DBNull.Value Then
                        lbluid.Text = Sqldr("ad_userid")
                    End If
                    If Sqldr("ap_duedate") IsNot DBNull.Value Then
                        lbldue.Text = Sqldr("ap_duedate")
                    End If
                    If Sqldr("ad_title") IsNot DBNull.Value Then
                        txtadtitle.Text = Sqldr("ad_title")
                    End If
                    If Sqldr("ad_text") IsNot DBNull.Value Then
                        txtadtext.Text = Sqldr("ad_text")
                    End If
                    If Sqldr("av_Postingno") IsNot DBNull.Value Then
                        txtpostno.Text = Sqldr("av_Postingno")
                    End If      
                    If Sqldr("ap_status") IsNot DBNull.Value Then
                        dd_status.SelectedIndex = dd_status.Items.IndexOf(dd_status.Items.FindByText(Sqldr("ap_status")))
                    End If
                    If Sqldr("av_APFrom") IsNot DBNull.Value Then
                        lblfdate.Text = Sqldr("av_APFrom")
                    End If
                    If Sqldr("av_APTo") IsNot DBNull.Value Then
                        lbltdate.Text = Sqldr("av_APTo")
                    End If
                    strvenno = Sqldr("tbl_leadadvenues")   
                    initstat = Sqldr("ap_status") 
                   
                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        
        end sub    
        public sub bindhist()
        Dim strUID As String = Session("userid")
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String = "Select * from tbl_autopostqueueHistory where aphist_apfk = " & request.querystring("id")
            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_LeadADVenues")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_LeadADVenues"))
						
                aphistory.DataSource = dvProducts
                aphistory.DataBind()
					
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
                   
        end sub 
         sub pagesetup()

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

    End Class
end namespace