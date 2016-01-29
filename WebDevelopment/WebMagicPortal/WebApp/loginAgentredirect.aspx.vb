Imports System
Imports System.Data
Imports System.Data.SqlClient
Imports System.Web.UI
Imports System.Web.UI.WebControls
Imports System.Web.UI.HtmlControls
Imports System.Web
Imports System.Web.Security
Imports System.Configuration

namespace database1
Public Class test
   Inherits Page
   	
   	 
	   private Sub Page_Load(byval sender as object, byval e As EventArgs) handles mybase.load
	
 		
				
	  	 dim msg as string =""
          msg = msg & "<Script Language='JavaScript'  >"
         msg = msg & "window.opener.location.href=window.opener.location.href;"
          msg = msg & "window.open();"
         
          msg = msg & "</Script>"
         Response.Write(msg)
		
		
		
	   	end sub	
	
	
	
End Class
end namespace