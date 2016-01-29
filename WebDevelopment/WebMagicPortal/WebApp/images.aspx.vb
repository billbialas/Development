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
Imports System.Configuration
Imports System.IO

namespace PageTemplate
    Public Class images
        Inherits PageTemplate

        Public pnladdimg,pnladdfolder,pnladdfolderExists,pnlmanagefolders,pnldelimgconfir,pnldelimgMove,pnlsearch,pnlcnfrmfolderdel As Panel
        public pnlupdateimg,pnlcnfrmfolderrename As Panel
        Protected WithEvents logo As System.Web.UI.HtmlControls.HtmlInputFile
        Public dgimages,dgFolders,dgdelImages,dgMovImages As DataGrid
        Public imgname, imgdesc,txtfldrname,l_search,txtnfldrname,imgnameE,imgdescE As TextBox
        Public dd_imgtype As DropDownList
        Public btnaddimg, btnexit,btnmgfolder,btnMoveImage,btndeleteImages As Button
		  public dd_imgfodler,dd_imgfodlerMove,ddlFolderFilter,ddlimgtypeFilter,dd_imgfodlerE,dd_imgtypeE as dropdownlist
		  public fldrrename,lblimagefile as label
		  Public imglogo,imglogo2 As HtmlImage
			
        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles mybase.load

            If Not (Page.IsPostBack) Then
            	if request.querystring("clr")="true" then
            		 session("keepadmfilters")="false"
            	end if
            		clearsessions()
            	
            	removefromlist(-1)
                bindimages()
                bindfoldersFilter()
                If Request.QueryString("source") = "rspmgr" Then
                    btnexit.Visible = True

                End If
                if  session("FolderPF")="true"  then
      			 	ddlFolderFilter.SelectedIndex = ddlFolderFilter.Items.IndexOf(ddlFolderFilter.Items.FindByText( session("FolderPF")))
      			 end if
      			 if  session("typetf")="true"  then
      			 	ddlimgtypeFilter.SelectedIndex = ddlimgtypeFilter.Items.IndexOf(ddlimgtypeFilter.Items.FindByText( session("typetf")))
      	
      			 end if

            End If
            pagesetup()

        End Sub
        
        sub clearsessions()
        		
        		if session("keepadmfilters")="false" then
	        		session("PubSearchFAD")="false"
	           	session("FolderPF")="false"
	           	session("typetf")="false"	          	
	          	session.remove("PubSearchFADV")
        			session("FolderPFV")="All"
        			session("typetfV")="All"
        			session("addfld")=""
        			
	         else
	         	l_search.text=session("PubSearchFADV")
	         	ddlFolderFilter.SelectedIndex = ddlFolderFilter.Items.IndexOf(ddlFolderFilter.Items.FindByText(session("FolderPFV")))
	         	ddlimgtypeFilter.SelectedIndex = ddlimgtypeFilter.Items.IndexOf(ddlimgtypeFilter.Items.FindByText(session("typetfV")))
	         	
        		end if		
        		
        	
			end sub
        
        
        
          Public Sub btn_showhelp(ByVal Source As Object, ByVal e As ImageClickEventArgs)
      		Response.Write("<script>window.open" & _
                "('" & System.Configuration.ConfigurationManager.AppSettings("CurrentappURL") & "/help/help_im.html','_new','width=1000,height=650,resizable=1,scrollbars=1');</script>")
     
			end sub
        Sub images_PageChanger(ByVal Source As Object, _
                ByVal E As DataGridPageChangedEventArgs)
            ' Set the CurrentPageIndex before binding the grid 
            dgimages.CurrentPageIndex = E.NewPageIndex
            bindimages()

        End Sub
        
         Sub folders_PageChanger(ByVal Source As Object, ByVal E As DataGridPageChangedEventArgs)
            dgFolders.CurrentPageIndex = E.NewPageIndex
            bindfoldersManage()

        End Sub
        
        Sub clearcks(ByVal sender As Object, ByVal e As EventArgs)
            Response.Redirect("images.aspx")
        End Sub
        
         Sub GetSelections_Click2(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Dim x As CheckBox = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(1).Text

            If x.Checked = True Then
               
               addtolist(content)
             
            Else
                removefromlist(content)
            End If

        End Sub
        
        Sub addtolist(ByVal id As String)
        
            Dim strUID As String = Session("userid")
            Dim strSql As String = "insert into tbl_tmpad (tmpad_uid,tmpad_adno) values ('" & Session("userid") & "','" & id & "')"
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
        
        
         Sub removefromlist(ByVal id As String)
            Dim strUID As String = Session("userid")
            Dim strSql As String
            If id = -1 Then
                strSql = "delete from tbl_tmpad where tmpad_uid='" & Session("userid") & "'"
            Else
                strSql = "delete from tbl_tmpad where tmpad_uid='" & Session("userid") & "' and tmpad_adno='" & id & "'"
            End If

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
        
        
         Sub filterDDs(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Dim x As dropdownlist = Source
            dgimages.CurrentPageIndex=0
             if x.ID = "ddlFolderFilter" then
             	if ddlFolderFilter.selecteditem.text = "All" then
             		session("FolderPF")="false"
             		session("FolderPFV")="All"
             	else
             		session("FolderPF")="true"
             		session("FolderPFV")=ddlFolderFilter.selecteditem.text
             	end if
            
             elseif x.ID = "ddlimgtypeFilter" then
             	if ddlimgtypeFilter.selecteditem.text = "All" then
             		session("TypeTF")="false"
             		session("TypeTFV")="All"
             	else
             		session("TypeTF")="true"
             		session("TypeTFV")=ddlimgtypeFilter.selecteditem.text
             	end if            
            
             	
             end if
           	
           
           dgimages.CurrentPageIndex = 0
          	bindimages()

        End Sub
        
        
        Public Sub bindimages()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            mycommand = "Select *,cast(ui_tbl_pk as varchar(255)) as 'imgno', case when (ui_folder='9999') then ui_location else ui_location+ifld_name+'/' end as 'ui_location2',  case when (ui_folder='9999') then 'Root' else ifld_name end as 'foldername' from tbl_userimages left join tbl_imgfolders on ifld_tbl_pk = ui_folder where ui_uid='" & Session("userid") & "'"
            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_userimages")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_userimages"))
                
                dvProducts.RowFilter = "imgno like '%'"
                if session("PubSearchFAD")="true" then
                	
                	dvProducts.RowFilter = dvProducts.RowFilter + " and (imgno like '%" & l_search.text & "%' or ui_name like '%" & l_search.text & "%' or ui_descrip like '%" & l_search.text & "%' or ui_filename like '%" & l_search.text & "%' or ui_type like '%" & l_search.text & "%')"
                end if
                if session("FolderPF")="true" then
                	if ddlFolderFilter.selecteditem.text="Root" then
              			dvProducts.RowFilter = dvProducts.RowFilter + " and ui_folder = '9999'"	
        				else
                	  	dvProducts.RowFilter = dvProducts.RowFilter + " and ifld_name = '" & ddlFolderFilter.selecteditem.text & "'"	
        				end if
			        end if
                if session("TypeTF")="true" then
					 	dvProducts.RowFilter = dvProducts.RowFilter + " and ui_type = '" & ddlimgtypeFilter.selecteditem.text & "'"	
                end if
              
                dgimages.DataSource = dvProducts
                dgimages.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
        End Sub
        Public Sub cdirect(ByVal sender As Object, ByVal e As EventArgs)
           
            Directory.CreateDirectory(System.Configuration.ConfigurationManager.AppSettings("CurrentIMGDirectory") & Session("userid"))

        End Sub
        Public Sub AddImage(ByVal sender As Object, ByVal e As EventArgs)
            dgimages.Visible = False
            pnladdimg.Visible = True
            btnaddimg.Visible = False
            btnmgfolder.Visible = False
            btnMoveImage.Visible = False
            btndeleteImages.Visible = False
            pnlsearch.Visible = False
            imgname.Text = ""
            imgdesc.Text = ""
            bindfolders()
        End Sub
        Public Sub exitimg(ByVal sender As Object, ByVal e As EventArgs)
            If Request.QueryString("source") = "rspmgr" Then
                Response.Redirect("admanager.aspx?source=rspmgr")

            End If
        End Sub

        Sub dgimages_Edit(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
            dgimages.EditItemIndex = e.Item.ItemIndex
            bindimages()
        End Sub
        Sub dgimages_Cancel(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
            dgimages.EditItemIndex = -1
            bindimages()
        End Sub
        Sub dgimages_Update(ByVal sender As Object, ByVal e As DataGridCommandEventArgs)
            'Read in the values of the updated row
            Dim dgID As String = e.Item.Cells(0).Text
            'CType(e.Item.Cells(0).Controls(0), TextBox).Text
            Dim dgname As String = CType(e.Item.Cells(1).Controls(0), TextBox).Text
            Dim dgdesc As String = CType(e.Item.Cells(2).Controls(0), TextBox).Text

            Dim strSql As String = "update tbl_userimages set ui_name='" & dgname & "', ui_descrip='" & dgdesc & "' where ui_tbl_pk='" & dgID & "'"
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
            dgimages.EditItemIndex = -1
            bindimages()
        End Sub

        Public Sub cancelimage(ByVal sender As Object, ByVal e As EventArgs)
            dgimages.Visible = True
            pnladdimg.Visible = False
            btnaddimg.Visible = True
            btnmgfolder.Visible = true
             btnMoveImage.Visible = true
            btndeleteImages.Visible = true
            pnlmanagefolders.Visible = False
            pnldelimgconfir.Visible = False
            pnldelimgMove.visible=false
            pnlsearch.visible=true
            removefromlist(-1)
            bindimages()
            pnlsearch.Visible = true
            pnlupdateimg.visible=false
            

        End Sub
        
        
        
        Public Sub Addfolder(ByVal sender As Object, ByVal e As EventArgs)
            dim x as button=sender
            if x.id="btnaddfolder" then
	            dgimages.Visible = false
	            pnladdimg.Visible = False
	            btnaddimg.Visible = false
	            btnmgfolder.Visible = False
	             btnMoveImage.Visible = False
	            btndeleteImages.Visible = False
	           	pnlsearch.Visible = false
	            pnladdfolder.Visible = true
	            txtfldrname.text=""
	            bindfolders()
	            session("addfld")="image"
	         else
	         	dgimages.Visible = false
	            pnladdimg.Visible = False
	            btnaddimg.Visible = false
	            btnmgfolder.Visible = False
	             btnMoveImage.Visible = False
	            btndeleteImages.Visible = False
	           	pnlsearch.Visible = false
	            pnladdfolder.Visible = true
	            pnlmanagefolders.Visible = False
	            txtfldrname.text=""
	            bindfolders()
	            bindfoldersManage()
	            session("addfld")="manage"
	         end if

        End Sub
        
        Public Sub cancelsavefolder(ByVal sender As Object, ByVal e As EventArgs)
            dgimages.Visible = False
          
            btnaddimg.Visible = False
            btnmgfolder.Visible = False
             btnMoveImage.Visible = False
            btndeleteImages.Visible = False
            
            pnladdfolder.Visible = false
            
            if session("addfld")="image" then
            		pnladdimg.Visible = True            		
            		pnlmanagefolders.Visible = false
            		pnlsearch.Visible = false
            		bindfolders()
            	else
            		pnlmanagefolders.Visible = true
            		pnladdimg.Visible = false
            		 pnlsearch.Visible = false
            		bindfoldersManage()
            	end if
        End Sub
        
        Public Sub SaveFolder(ByVal sender As Object, ByVal e As EventArgs)
        		 if not folderexists(txtfldrname.text) then
             	Directory.CreateDirectory(System.Configuration.ConfigurationManager.AppSettings("CurrentIMGDirectory") & Left(session("userid"), Len(session("userid")) - 4) & "IMG/" & txtfldrname.text)
      		   updatefldrtable()
      		   pnladdfolderExists.Visible = false
      		   dgimages.Visible = False
            	if session("addfld")="image" then
            		pnladdimg.Visible = True
            		pnlmanagefolders.Visible = false
            	else
            		pnlmanagefolders.Visible = true
            		pnladdimg.Visible = false
            		bindfoldersManage()
            	end if
            	
            	btnaddimg.Visible = False  
            	btnmgfolder.Visible = False  
            	btnMoveImage.Visible = False
            	btndeleteImages.Visible = False        
            	pnladdfolder.Visible = false
            	bindfolders()
            	pnlsearch.Visible = false
      		 else
      		 	pnladdfolderExists.Visible = true
      		 end if
      		 
      		 
        End Sub
        
        Public Sub bindfoldersFilter()
              Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select * from tbl_imgfolders where ifld_UID='" & Session("userid") & "'"
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                ddlFolderFilter.DataSource = dataReader
                ddlFolderFilter.DataTextField = "ifld_name"
                ddlFolderFilter.DataValueField = "ifld_tbl_pk"
                ddlFolderFilter.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
            ddlFolderFilter.Items.Insert(0, New ListItem("All", "999966"))
            ddlFolderFilter.Items.Insert(1, New ListItem("Root", "9999"))
            
        End Sub
        
        
         Public Sub bindfolders()
              Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select * from tbl_imgfolders where ifld_UID='" & Session("userid") & "'"
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                dd_imgfodler.DataSource = dataReader
                dd_imgfodler.DataTextField = "ifld_name"
                dd_imgfodler.DataValueField = "ifld_tbl_pk"
                dd_imgfodler.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
            dd_imgfodler.Items.Insert(0, New ListItem("Root", "9999"))
            
        End Sub
        
        Public Sub bindfoldersMov()
              Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select * from tbl_imgfolders where ifld_UID='" & Session("userid") & "'"
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                dd_imgfodlerMove.DataSource = dataReader
                dd_imgfodlerMove.DataTextField = "ifld_name"
                dd_imgfodlerMove.DataValueField = "ifld_tbl_pk"
                dd_imgfodlerMove.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
            dd_imgfodlerMove.Items.Insert(0, New ListItem("Root", "9999"))
            
        End Sub
        
        
        
        
        public function folderexists(id as string) as boolean
        		Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
        
            Dim CartCustCode As String = request.querystring("Ccode")
            Dim strSql As String 
            
            strSql= "select * from tbl_imgfolders where ifld_UID ='" & session("userid") & "' and  ifld_name='" & id & "'"
           
            Try
                strConnection = System.Configuration.ConfigurationManager.AppSettings("ConnectionString")
                sqlConn = New SqlConnection(strConnection)
                sqlCmd = New SqlCommand(strSql, sqlConn)

                sqlConn.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
 						If Sqldr.Read() Then
 							return true
 					   else
 					   	return false
                	End If

            Catch ex As Exception
                Response.Write(ex.ToString())
            Finally
                sqlConn.Close()
            End Try               
        
        end function
        
        public sub updatefldrtable()
         	Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand
        
            Dim CartCustCode As String = request.querystring("Ccode")
            Dim strSql As String = "insert into tbl_imgfolders (ifld_name, ifld_UID, ifld_location) values ('" & txtfldrname.text & "','" & session("userid") & "','" &  System.Configuration.ConfigurationManager.AppSettings("CurrentIMGDirectory") & Left(session("userid"), Len(session("userid")) - 4) & "IMG/" & "')"  
                          

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
        
         
        Sub RemoveImage(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(1).Text
            removeimagedisk(content)
            removeimadeDB(content)
            dgimages.CurrentPageIndex = 0
            bindimages()
        End Sub
        
        
        
        
        
        
        Public Sub removeimagedisk(ByVal id As String)
            Dim FileToDelete As String

            FileToDelete = System.Configuration.ConfigurationManager.AppSettings("CurrentIMGVDirectory") & Left(Session("userid"), Len(Session("userid")) - 4) & "IMG\" & gtimgfilename(id)

            If System.IO.File.Exists(FileToDelete) = True Then
                System.IO.File.Delete(FileToDelete)
            End If

        End Sub
        Public Sub removeimadeDB(ByVal id As String)
            Dim strSql As String = "delete from  tbl_userimages where ui_tbl_pk='" & id & "'"
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
        
        Public Function gtimgfilename(ByVal id As String) As String
            Dim strSql As String = "SELECT ui_filename,case when (ui_folder='9999') then ui_filename else ifld_name+'/'+ui_filename end as 'ui_filename2' from tbl_userimages left join tbl_imgfolders on ifld_tbl_pk = ui_folder where ui_tbl_pk='" & id & "'"
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    Return Sqldr("ui_filename2")
                End If

            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        End Function
        Public Sub SaveImage(ByVal sender As Object, ByVal e As EventArgs)
            Dim fna As String
           
            If Not logo.PostedFile Is Nothing And logo.PostedFile.ContentLength > 0 Then
                Dim fn As String = System.IO.Path.GetFileName(logo.PostedFile.FileName)
                fna = fn
                'Dim SaveLocation As String = Server.MapPath(System.Configuration.ConfigurationManager.AppSettings("CurrentIMGVDirectory") & Session("userid")) & "\" & fn
                '"'F:\ChoiceOne\Development\gimp\LOGOS\" & fn& fn
                'Server.MapPath("Data") & "\" & fn
                Try
                	  if dd_imgfodler.selecteditem.text = "Root" then
                	    logo.PostedFile.SaveAs(System.Configuration.ConfigurationManager.AppSettings("CurrentIMGVDirectory") & Left(Session("userid"), Len(Session("userid")) - 4) & "IMG\" & fn)
              			else
              			 logo.PostedFile.SaveAs(System.Configuration.ConfigurationManager.AppSettings("CurrentIMGVDirectory") & Left(Session("userid"), Len(Session("userid")) - 4) & "IMG\" & dd_imgfodler.selecteditem.text & "\" & fn)
                    end if
                Catch Exc As Exception
                    Response.Write("Error: " & Exc.Message)
                End Try

            Else
                Response.Write("Please select a file to upload.")
            End If

            Dim strConnection As String
            Dim sqlConn As SqlConnection
            Dim sqlCmd As SqlCommand

            Dim strSql As String = "insert into dbo.tbl_userimages  (ui_name,ui_descrip,ui_filename,ui_adddate,ui_type,ui_uid,ui_location,ui_folder) " _
                        & "values('" & imgname.Text & "','" & imgdesc.Text & "','" & fna & "',getdate(),'" & dd_imgtype.SelectedItem.Text & "','" & Session("userid") & "','" & System.Configuration.ConfigurationManager.AppSettings("CurrentIMGURL") & "/UIMG/" & Left(Session("userid"), Len(Session("userid")) - 4) & "IMG/','" & dd_imgfodler.selecteditem.value & "')"

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

            bindimages()
            dgimages.Visible = True
            pnladdimg.Visible = False
            btnaddimg.Visible = True
            btnmgfolder.Visible = true
            btnMoveImage.Visible = true
            btndeleteImages.Visible = true
            pnlsearch.Visible = true
            bindfoldersFilter()
        End Sub
        
         Public Sub MGfolders(ByVal sender As Object, ByVal e As EventArgs)
            dgimages.Visible = False
            pnladdimg.Visible = false
            btnaddimg.Visible = False
            btnmgfolder.Visible = False
             btnMoveImage.Visible = False
            btndeleteImages.Visible = False
            pnlmanagefolders.Visible = True
            pnlsearch.Visible = False
            bindfoldersManage()
        End Sub
        
        
        
         Public Sub bindfoldersManage()
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            mycommand = "Select * from tbl_imgfolders where ifld_UID='" & Session("userid") & "'"
            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_imgfolders")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_imgfolders"))
                dgFolders.DataSource = dvProducts
                dgFolders.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
        End Sub
        
       
        
        Public Sub DelImages(ByVal sender As Object, ByVal e As EventArgs)
           
           pnldelimgconfir.visible=true
           dgimages.Visible = False
            pnladdimg.Visible = True
            btnaddimg.Visible = False
            btnmgfolder.Visible = False
             btnMoveImage.Visible = False
            btndeleteImages.Visible = False
            binddelimages()
            pnladdimg.Visible = False
             pnldelimgMove.visible=false
             pnlsearch.Visible = False
        End Sub
        
         
        Public Sub MvImage(ByVal sender As Object, ByVal e As EventArgs)
            pnldelimgconfir.visible=false
            dgimages.Visible = False
            pnladdimg.Visible = false
            btnaddimg.Visible = False
            btnmgfolder.Visible = False
             btnMoveImage.Visible = False
            btndeleteImages.Visible = False
            bindMovimages()
            pnladdimg.Visible = False
            pnldelimgMove.visible=true
            pnlsearch.Visible = False
            bindfoldersMov()
        End Sub
        
       
        
        sub binddelimages()
          Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            mycommand = "Select *, case when (ui_folder='9999') then ui_location else ui_location+ifld_name+'/' end as 'ui_location2',  case when (ui_folder='9999') then 'Root' else ifld_name end as 'foldername' from tbl_userimages join tbl_tmpad  on tmpad_adno=ui_tbl_pk left join tbl_imgfolders on ifld_tbl_pk = ui_folder where ui_uid='" & Session("userid") & "'"
            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_userimages")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_userimages"))
                dgdelImages.DataSource = dvProducts
                dgdelImages.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
        
        end sub
        
        sub bindMovimages()
          Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            mycommand = "Select *, case when (ui_folder='9999') then ui_location else ui_location+ifld_name+'/' end as 'ui_location2',  case when (ui_folder='9999') then 'Root' else ifld_name end as 'foldername' from tbl_userimages join tbl_tmpad  on tmpad_adno=ui_tbl_pk left join tbl_imgfolders on ifld_tbl_pk = ui_folder where ui_uid='" & Session("userid") & "'"
            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_userimages")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_userimages"))
                dgMovImages.DataSource = dvProducts
                dgMovImages.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
        
        end sub
        
        
        
        
        
        
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
        
        Sub ItemDataBoundEventHandlerIMG(ByVal sender As Object, ByVal e As DataGridItemEventArgs)

            If e.Item.ItemType = ListItemType.Item Or e.Item.ItemType = ListItemType.AlternatingItem Then
   
   				Dim myButton as Button = CType(e.Item.Cells(8).Controls(0), Button)

          		myButton.CssClass = "frmbuttons"

     			End If

        End Sub
        
        
        
         Sub excldimg(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(0).Text
            removefromlist(content)
            binddelimages()
        End Sub
        
          
         Sub excldimgM(ByVal Source As System.Object, ByVal e As System.EventArgs)
            Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(0).Text
            removefromlist(content)
            bindMovimages()
        End Sub
        
        
        
         Public Sub imgdelconfirm(ByVal sender As Object, ByVal e As EventArgs)
           
           Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
             mycommand = "Select ui_tbl_pk from tbl_userimages join tbl_tmpad  on tmpad_adno=ui_tbl_pk left join tbl_imgfolders on ifld_tbl_pk = ui_folder where ui_uid='" & Session("userid") & "'"
            Dim ad As New SqlDataAdapter(mycommand, myConnection)
            Dim ds As New DataSet()
           
            Dim i As Integer

            Try
                ad.Fill(ds)
                'ds.Tables(0).TableName = "bill"
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

           
            For i = 0 To ds.Tables(0).Rows.Count - 1
					removeimagedisk(ds.Tables(0).Rows(i)(0).ToString())
               removeimadeDB(ds.Tables(0).Rows(i)(0).ToString())

            Next
            
           dgimages.Visible = True
            pnladdimg.Visible = False
            btnaddimg.Visible = True
            btnmgfolder.Visible = true
             btnMoveImage.Visible = true
            btndeleteImages.Visible = true
            pnlmanagefolders.Visible = False
            pnldelimgconfir.Visible = False
            pnldelimgMove.Visible = False
            pnlsearch.Visible = true
            removefromlist(-1)
            bindimages()
        
        End Sub        
        
         Public Sub imgmovconfirm(ByVal sender As Object, ByVal e As EventArgs)
        
        		 Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
             mycommand = "Select ui_tbl_pk from tbl_userimages join tbl_tmpad  on tmpad_adno=ui_tbl_pk left join tbl_imgfolders on ifld_tbl_pk = ui_folder where ui_uid='" & Session("userid") & "'"
            Dim ad As New SqlDataAdapter(mycommand, myConnection)
            Dim ds As New DataSet()
           
            Dim i As Integer

            Try
                ad.Fill(ds)
                'ds.Tables(0).TableName = "bill"
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try

           
            For i = 0 To ds.Tables(0).Rows.Count - 1
            
					moveimage(ds.Tables(0).Rows(i)(0).ToString())
               updateimageDB(ds.Tables(0).Rows(i)(0).ToString())

            Next
             dgimages.Visible = True
            pnladdimg.Visible = False
            btnaddimg.Visible = True
            btnmgfolder.Visible = true
             btnMoveImage.Visible = true
            btndeleteImages.Visible = true
            pnlmanagefolders.Visible = False
            pnldelimgconfir.Visible = False
            pnldelimgMove.Visible = False
            pnlsearch.Visible = true
            removefromlist(-1)
            bindimages()
        
        
        end sub
        
          Sub clearall(ByVal Source As System.Object, ByVal e As System.EventArgs)
				session("keepadmfilters")="false"

            Response.Redirect("images.aspx?search=*")
        End Sub
        
        Sub filterAAA(ByVal Source As System.Object, ByVal e As System.EventArgs)
        
           		if l_search.text="" then
           			session("PubSearchFAD")="false"
           			session.remove("PubSearchFADV")
           		else
           			
           			
           			session("PubSearchFAD")="true"
           			session("PubSearchFADV")=l_search.text
           		end if
           
           	 
           dgimages.CurrentPageIndex = 0
           bindimages()

        end sub
        
        
        
        
        
        Public Sub moveimage(ByVal id As String)
        
        		dim FileToMove,MoveLocation, fromdirectory, fromfilename, todirectory as string
            
            Dim strUID As String = Session("userid")
            Dim strSql As String = "select *,case when (ui_folder='9999') then 'Root' else ifld_name end as 'ui_location2' from tbl_userimages left join tbl_imgfolders on ifld_tbl_pk = ui_folder where ui_tbl_pk='" & id & "'" 
            Dim sqlCmd As SqlCommand
            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                	fromdirectory = sqldr("ui_location2")
                	fromfilename = sqldr("ui_filename")
                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
        
         	if fromdirectory = "Root" then
          	    FileToMove= System.Configuration.ConfigurationManager.AppSettings("CurrentIMGVDirectory") & Left(Session("userid"), Len(Session("userid")) - 4) & "IMG\" & fromfilename
     			else
     			 FileToMove= System.Configuration.ConfigurationManager.AppSettings("CurrentIMGVDirectory") & Left(Session("userid"), Len(Session("userid")) - 4) & "IMG\" & fromdirectory & "\" & fromfilename
           end if
               
        
  			  if dd_imgfodlerMove.selecteditem.text = "Root" then
          	    MoveLocation= System.Configuration.ConfigurationManager.AppSettings("CurrentIMGVDirectory") & Left(Session("userid"), Len(Session("userid")) - 4) & "IMG\" & fromfilename
     			else
     			 MoveLocation= System.Configuration.ConfigurationManager.AppSettings("CurrentIMGVDirectory") & Left(Session("userid"), Len(Session("userid")) - 4) & "IMG\" & dd_imgfodlerMove.selecteditem.text & "\" & fromfilename
           end if       
            If System.IO.File.Exists(MoveLocation) then
            	System.IO.File.Delete(MoveLocation)

            end if
                
        		If System.IO.File.Exists(FileToMove) = True Then
					
					System.IO.File.Move(FileToMove, MoveLocation)
			
				End If
        
        End Sub 
        
        Public Sub updateimageDB(ByVal id As String)
        		dim newfolderpk as integer
				if dd_imgfodlerMove.selecteditem.text="Root" then
					newfolderpk=9999
				else
					newfolderpk=dd_imgfodlerMove.selecteditem.value
				end if        
        
            Dim strUID As String = Session("userid")
            Dim strSql As String = "update tbl_userimages set ui_folder='" & newfolderpk & "' where ui_tbl_pk='" & id & "'"
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
        
        
          Sub flddel(ByVal Source As System.Object, ByVal e As System.EventArgs)
        		Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(0).Text
            Dim content2 As String = item.Cells(1).Text
            session("fldtodelPK")=content
            session("fldtodel")=content2
            pnlcnfrmfolderdel.visible=true
       		pnlmanagefolders.visible=false
       		pnlcnfrmfolderrename.visible=false
       		
         end sub
         
         Sub fldrename(ByVal Source As System.Object, ByVal e As System.EventArgs)
        		Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(0).Text
            Dim content2 As String = item.Cells(1).Text
            session("fldtorenPK")=content
            session("fldtoren")=content2
            pnlcnfrmfolderdel.visible=false
       		pnlmanagefolders.visible=false
       		pnlcnfrmfolderrename.visible=true
       		fldrrename.text = content2
         end sub

			Sub flddelyes(ByVal Source As System.Object, ByVal e As System.EventArgs)
        		
        		dim deldir as string
        	   deldir= System.Configuration.ConfigurationManager.AppSettings("CurrentIMGVDirectory") & Left(Session("userid"), Len(Session("userid")) - 4) & "IMG\" & session("fldtodel")
       
        		System.IO.Directory.Delete(deldir, True)
        		
        		delfldrtbl1()
        		delfldrtbl2()    
            
       		pnlcnfrmfolderdel.visible=false
       		pnlmanagefolders.visible=true
       		bindfoldersManage()
       		bindfoldersFilter()
       		bindfolders()
       		
         end sub
         
         Sub fldrenyes(ByVal Source As System.Object, ByVal e As System.EventArgs)
        		 if not folderexists(txtnfldrname.text) then
        		 	dim Orendir as string
        	   	dim Nrendir as string
        	   	Orendir= System.Configuration.ConfigurationManager.AppSettings("CurrentIMGDirectory") & Left(Session("userid"), Len(Session("userid")) - 4) & "IMG\" & session("fldtoren")
      			Nrendir= System.Configuration.ConfigurationManager.AppSettings("CurrentIMGDirectory") & Left(Session("userid"), Len(Session("userid")) - 4) & "IMG\" & txtnfldrname.text
      			'response.write(Orendir)
      			'response.write(Nrendir)
      			System.IO.Directory.Move(Orendir, Nrendir)
					'UpdateDBs
					updateimageDBR(txtnfldrname.text)
					pnladdfolderExists.visible=false
					bindfoldersManage()
       			bindfoldersFilter()
       			bindfolders()
       			 dgimages.Visible = False
            pnladdimg.Visible = false
            btnaddimg.Visible = False
            btnmgfolder.Visible = False
             btnMoveImage.Visible = False
            btndeleteImages.Visible = False
            pnlmanagefolders.Visible = True
            pnlsearch.Visible = False
            pnlcnfrmfolderrename.Visible = False
       			
        		 else 
        		 	pnladdfolderExists.visible=true
        		 end if
        		
       		
         end sub
         
         Public Sub updateimageDBR(ByVal id As String)
        		dim newfolderpk as integer				 
        
            Dim strUID As String = Session("userid")
            Dim strSql As String = "update tbl_imgfolders set ifld_name='" & id & "' where ifld_tbl_pk='" & session("fldtorenPK") & "'"
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
        
         
         
         sub delfldrtbl1()
         	Dim strUID As String = Session("userid")
            Dim strSql As String = "delete from tbl_imgfolders where ifld_tbl_pk='" & session("fldtodelPK") & "' and ifld_UID='" & session("userid") & "'"
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
         
          sub delfldrtbl2()
         	Dim strUID As String = Session("userid")
            Dim strSql As String = "delete from tbl_userimages where ui_folder='" & session("fldtodelPK") & "' and ui_uid='" & session("userid") & "'"
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
         
         
         Sub flddelno(ByVal Source As System.Object, ByVal e As System.EventArgs)
        
       		pnlcnfrmfolderdel.visible=false
       		pnlmanagefolders.visible=true
       		
         end sub

 			Sub fldrenno(ByVal Source As System.Object, ByVal e As System.EventArgs)
        		pnlcnfrmfolderdel.visible=false
       		pnlmanagefolders.visible=true
       		pnlcnfrmfolderrename.visible=false
         end sub


			Sub EditImageA(ByVal Source As System.Object, ByVal e As System.EventArgs)
        		Dim x As Button = Source
            Dim cell As TableCell = x.Parent
            Dim item As DataGridItem = cell.Parent
            Dim content As String = item.Cells(1).Text
        		
        		
        		pnlupdateimg.visible=true
        		dgimages.Visible = False
            pnladdimg.Visible = false
            btnaddimg.Visible = False
            btnmgfolder.Visible = False
            btnMoveImage.Visible = False
            btndeleteImages.Visible = False
            pnlsearch.Visible = False
            bindfoldersE()
        		bindfields(content)
        	
         end sub


		       
			
			Sub UpdateImage(ByVal Source As System.Object, ByVal e As System.EventArgs)
        		Dim strUID As String = Session("userid")
         	Dim strSql As String = "update tbl_userimages set ui_name='" & imgnameE.text & "',ui_descrip='" & imgdescE.text & "',ui_type='" & dd_imgtypeE.selecteditem.text & "'"

            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                
                end if
				
              
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
            
            dgimages.Visible = True
            pnladdimg.Visible = False
            btnaddimg.Visible = True
            btnmgfolder.Visible = true
             btnMoveImage.Visible = true
            btndeleteImages.Visible = true
            pnlmanagefolders.Visible = False
            pnldelimgconfir.Visible = False
            pnldelimgMove.visible=false
            pnlsearch.visible=true
            removefromlist(-1)
            bindimages()
            pnlsearch.Visible = true
            pnlupdateimg.visible=false
                
			end sub

		Public Sub bindfoldersE()
              Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim myCommand As String = "Select * from tbl_imgfolders where ifld_UID='" & Session("userid") & "'"
            Dim objCmd As New SqlCommand(myCommand, myConnection)
            Dim dataReader As SqlDataReader = Nothing
            Try
                myConnection.Open()
                dataReader = objCmd.ExecuteReader()
                dd_imgfodlerE.DataSource = dataReader
                dd_imgfodlerE.DataTextField = "ifld_name"
                dd_imgfodlerE.DataValueField = "ifld_tbl_pk"
                dd_imgfodlerE.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
            dd_imgfodlerE.Items.Insert(0, New ListItem("Root", "9999"))
            
        End Sub


			public sub bindfields(id as string)
			 	'response.write (id)
			 	Dim strUID As String = Session("userid")
         	Dim strSql As String = "SELECT * from tbl_userimages left join tbl_imgfolders on ifld_tbl_pk=ui_folder where ui_tbl_pk='" & id & "'"

            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                   If Sqldr("ui_name") IsNot DBNull.Value Then
                        imgnameE.Text = Sqldr("ui_name")
                    End If
                    If Sqldr("ui_descrip") IsNot DBNull.Value Then
                        imgdescE.Text = Sqldr("ui_descrip")
                    End If
                    If Sqldr("ui_filename") IsNot DBNull.Value Then
                        lblimagefile.Text = Sqldr("ui_filename")
                    End If
                    dd_imgtypeE.SelectedIndex = dd_imgtypeE.Items.IndexOf(dd_imgtypeE.Items.FindByText(Sqldr("ui_type")))
                    if Sqldr("ui_folder")="9999" then
                    	dd_imgfodlerE.SelectedIndex = dd_imgfodlerE.Items.IndexOf(dd_imgfodlerE.Items.FindByText("Root"))
                    	imglogo.Src= System.Configuration.ConfigurationManager.AppSettings("CurrentIMGURL") & "/uimg/" & Left(Session("userid"), Len(Session("userid")) - 4) & "IMG/" & Sqldr("ui_filename")
      				   else
                     dd_imgfodlerE.SelectedIndex = dd_imgfodlerE.Items.IndexOf(dd_imgfodlerE.Items.FindByvalue(Sqldr("ui_type")))
                     imglogo.Src= System.Configuration.ConfigurationManager.AppSettings("CurrentIMGURL") & "/uimg/" & Left(Session("userid"), Len(Session("userid")) - 4) & "IMG/" & Sqldr("ifld_name") & "/" & Sqldr("ui_filename")
      				  end if
                    
							
						
						end if
				
              
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try
			end sub
    End Class
end namespace