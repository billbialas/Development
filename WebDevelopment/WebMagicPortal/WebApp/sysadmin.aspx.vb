imports System
imports System.Collections
imports System.ComponentModel
imports System.Data
imports System.Drawing
imports System.Web
imports System.Web.SessionState
imports System.Web.UI
imports System.Web.UI.WebControls
imports System.Web.UI.HtmlControls
imports System.Data.SqlClient
imports System.xml
imports System.Configuration

namespace PageTemplate
	public class sysadmin 
	   inherits PageTemplate
	   public Sub Page_Load(byval sender as object, byval e As EventArgs) handles mybase.load
		
		
			'the page template code below represents only a few of the things that
			'you can do. Play around with it, and you'll see just how much power is
			'in your hands

			'width will be calculated automatically, but it is sometimes
			'important to specify height
			Body.Height = "350"
			body.width="580"
			Body.VAlign = "top"
			RightNav.VAlign = "top"
			RightNav.width="200"
			Layout.border = 0
			layout.width="770"
			Header.Controls.Add(LoadControl("headersys.ascx"))
			'Header.Controls.Add(new LiteralControl("Some text."))
			'LeftNav.Controls.Add(LoadControl("sysadmin.ascx"))
			'LeftNav.Controls.Add(new LiteralControl("Some text."))

			'adjust size of LeftNav (just for the heck of it)
			LeftNav.Width = "100"

			'RightNav contents are included here, but try commenting
			'out the code below, to see how the page template dynamically
			'modifies itself (same goes with the LeftNav)
			'RightNav.Controls.Add(LoadControl("rightnav.ascx"))
			MiddleNav.Controls.Add(LoadControl("sysadmin.ascx"))
			Footer.controls.add(LoadControl("footer.ascx"))

	   end sub
	end class
end namespace