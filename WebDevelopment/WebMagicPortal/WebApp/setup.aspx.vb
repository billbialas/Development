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
	public class usersetupA
	   inherits PageTemplate
	   
	   public clogins as datagrid
	   public pnlclogins as panel
	     
		   private Sub Page_Load(byval sender as object, byval e As EventArgs) handles mybase.load
					
		    	if  not (Page.IsPostBack) then
		    	  	  if session("role")="GOD" then 	
		    	  	  		bindclogins()
		    	  	  		pnlclogins.visible=true
		    	  	  end if
				end if
				pagesetup()
				
		   end sub
		   
		 	sub bindclogins()
		 	  	Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            Dim mycommand As String
            mycommand = "select *, fname + ' ' + lname as 'fullname' from dbo.tbl_currentlogons join tbl_users on uid=lg_uid"

            Try
                Dim dataAdapter As New SqlDataAdapter(mycommand, myConnection)
                Dim dataSet As New DataSet()
                dataAdapter.Fill(dataSet, "tbl_currentlogons")
                Dim dvProducts As New DataView(dataSet.Tables("tbl_currentlogons"))
               
                clogins.DataSource = dvProducts
                clogins.DataBind()
            Catch exc As System.Exception
                Response.Write(exc.ToString())
            Finally
                myConnection.Dispose()
            End Try
		 	
		 	
		 	end sub
		 	 Sub clogins_PageChanger(ByVal Source As Object, ByVal E As DataGridPageChangedEventArgs)
            clogins.CurrentPageIndex = E.NewPageIndex
            bindclogins()
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
     end class
end namespace