'option strict on
imports System
imports System.Web.UI
imports System.Web.UI.WebControls
imports System.Web.UI.HtmlControls

namespace PageTemplate
   public class PageTemplate 
   'Implements ITemplate.InstantiateIn
	  inherits Page
	
		'this object acts the same as the <form runat="server"> tag
		'when it is added to the Page's Controls collection
		
		private mainForm as HtmlForm  
	   private _layout as HtmlTable 
		private _header as HtmlTableCell 
		private _leftNav as HtmlTableCell
		private _body as HtmlTableCell 
		private _rightNav as HtmlTableCell 
		private _footer as HtmlTableCell 
		private _MiddleNav as HtmlTableCell 

	   dim _topRow as HtmlTableRow  
		dim _middleRow1 as HtmlTableRow 
		dim _middleRow as HtmlTableRow 
		dim _bottomRow as HtmlTableRow 
	
	        public Property layout as HtmlTable
	          Get
	            return _layout
		  end get
		  set 
		    _layout = value
		  end set
		end Property
		
		public overloads Property header as HtmlTableCell
	          Get
	            return _header
		  end get
		  set 
		    _header = value
		  end set
		end Property
		
		public Property MiddleNav as HtmlTableCell
	          Get
	            return _MiddleNav
		  end get
		  set 
		    _MiddleNav = value
		  end set
		end Property
		
		public Property leftNav as HtmlTableCell
	          Get
	            return _leftNav
		  end get
		  set 
		      _leftNav = value
		  end set
		end Property
		
		public Property body as HtmlTableCell
	          Get
	            return _body
		  end get
		  set 
		    _body = value
		  end set
		end Property
		
		public Property rightNav as HtmlTableCell
	          Get
	            return _rightNav
		  end get
		  set 
		    _rightNav = value
		  end set
		end Property
		
		public Property footer as HtmlTableCell
	          Get
	            return _footer
		  end get
		  set 
		      _footer = value
		  end set
		end Property
		
	        private const NAV_REGION_WIDTH as string = "200"	
		
		public Sub New()
				
			'initialize the web form
			mainForm = new HtmlForm()
			'initialize layout Table
			_layout = new HtmlTable()
			
			_header = new HtmlTableCell()
			
			'set Header colspan to 3, which will cover the contents of the middle
			'section of the layout. This can be adjusted if the LeftNav or
			'RightNav is omitted from the layout
			_layout.cellspacing =0
			_layout.cellpadding = 0
			_header.ColSpan = 3
			
		
			_MiddleNav = new HtmlTableCell()
			_MiddleNav.colspan = 3
			_leftNav = new HtmlTableCell()
			_body = new HtmlTableCell()
			_rightNav = new HtmlTableCell()
			_footer = new HtmlTableCell()
			'set Footer colspan to 3, which will cover the contents of the middle
			'section of the layout. This can be adjusted if the LeftNav or
			'RightNav is omitted from the layout
			_footer.ColSpan = 3

			'set layout default properties. You can modify these
			'to suit the needs of your layout
			_layout.Width = "780"
			_layout.Border = 1

			'set page region default properties
			_leftNav.Width = NAV_REGION_WIDTH
			_rightNav.Width = NAV_REGION_WIDTH

			'declare three major regions of the layout
			_topRow = new HtmlTableRow()
			_middleRow1 = new HtmlTableRow()
			_middleRow = new HtmlTableRow()
			_bottomRow = new HtmlTableRow()

			'add all page regions to the layout. The LeftNav and RightNav regions can be
			'removed later, if no controls are added to them in the code-behind
			'class of the ASPX page. All other page regions will always exist
			'(you can change this behavior to suit the requirements of you project)
			_topRow.Cells.Add(header)
			_middleRow1.Cells.Add(MiddleNav)
			_middleRow.Cells.Add(leftNav)
			_middleRow.Cells.Add(body)
			_middleRow.Cells.Add(rightNav)
			_bottomRow.Cells.Add(footer)

			'add three major regions to the layout
			layout.Rows.Add(_topRow)
			layout.Rows.Add(_middleRow1)
			layout.Rows.Add(_middleRow)
			layout.Rows.Add(_bottomRow)

			'add layout to web form
			mainForm.Controls.Add(_layout)
		end sub	

		Protected Overrides Sub AddParsedSubObject(obj As Object)
		  if typeof obj is system.web.ui.htmlcontrols.htmlform then
				'insert page template web form at the proper place
				Controls.Add(mainForm)

				'get a reference to the form that is passed in, so that we can move
				'all of its controls to the body region of the page template
				dim f as HtmlForm  
				f = ctype (obj, system.web.ui.htmlcontrols.htmlform)

				'get the count of controls in the form that is passed in, because it will
				'decrement as we move the controls into the page template
				dim cnt as integer
				cnt = f.Controls.Count

				'loop through the form that is passed in, and move each control to the
				'body region of the template. The body region will later be added to the
				'page template's web form (mainForm)
				dim i as integer
				i = 0
				
				do while i < cnt
				  'always add the first item in the controls collection, because
				  'the indexes of the remaining controls change as controls are removed 
				  Body.Controls.Add(f.controls(0))
				  i = i +1
				loop
				
		     else
				'we're not specifically looking to handle this object, so
				'just move it onto the page like normal
				'response.write ("Here the obj is something else")
				controls.Add(ctype (obj, system.web.ui.control))
		     end if
		  end sub
	
	#Region "Web Form Designer generated code"    
	Protected Overrides Sub OnInit(ByVal e As EventArgs)
	      '      
	      ' CODEGEN: This call is required by the ASP.NET Web Form Designer.      
	      '      
	      InitializeComponent()      
	      MyBase.OnInit(e)    
	 End Sub    
	 
	 ' Required method for Designer support - do not modify    
	 ' the contents of this method with the code editor.    
	 Private Sub InitializeComponent()      
	    AddHandler Me.Load, New EventHandler(AddressOf Page_Load)
	 End Sub
	 #End Region	 
		
	private Sub Page_Load(sender as system.object, e As system.EventArgs) 
                  	' ***************************************************
			' * This method is where the contents of the page   *
			' * are customized, and validations are performed,  *
			' * based on what happened in the code-behind class *
			' * of the ASPX page                                *
			' * *************************************************/

			'add Header and Footer defaults, if they were not specified
			
			if Header.Controls.Count = 0 then
				Header.Controls.Add(LoadControl("header.ascx"))
			end if

			if Footer.Controls.Count = 0 then
				'Footer.Controls.Add(LoadControl("uc_footer.ascx"))
			end if

			'remove LeftNav if it does not contain any controls
			if LeftNav.Controls.Count = 0 then
			
				'optionally notify developer that they forgot to implement a LeftNav for this page
				'un-comment the throw statement below if you want to enforce the
				'presence of the LeftNav. If you are enforcing the presence of the LeftNav,
				'then you won't need the rest of the code in this if() construct, either,
				'because when you throw the exception, all processing stops for this method.
				'You can apply this same concept to other page regions in the layout, as well
				'throw new Exception("You must specify content for the LeftNav property of each page that implements the PageTemplate class.");

				'only adjust ColSpans when the page is first loaded, because these settings
				'are stored in ViewState for each element. Applying them again would
				'skew the layout
				if not IsPostBack then
				
					'adjust header and footer ColSpans to account for presence of LeftNav
					Header.ColSpan-=1
					Footer.ColSpan-=1
				end if

				'adjust LeftNav size to 0, so that it is not factored in
				'the Body width calculation
				LeftNav.Width = "0"

				'make LeftNav invisible
				_middleRow.Cells(0).Visible = false
			end if

			'remove RightNav if it does not contain any controls
			if RightNav.Controls.Count = 0 then
				
				'only adjust ColSpans when the page is first loaded, because these settings
				'are stored in ViewState for each element. Applying them again would
				'skew the layout
				if not IsPostBack then
				
					'adjust Header and Footer ColSpans to account for presence of RightNav
					Header.ColSpan-=1
					Footer.ColSpan-=1
				end if

				'adjust RightNav size to 0, so that it is not factored in
				'the Body width calculation
				RightNav.Width = "0"

				'remove LeftNav from the layout
				_middleRow.Cells(2).Visible = false
			end if

			'calcuate Body width based on the widths of the LeftNav and RightNav
			'/(either of which will be 0, if they were removed from the layout)
			Body.Width = Convert.ToString(Convert.ToInt32(Layout.Width) - Convert.ToInt32(LeftNav.Width) - Convert.ToInt32(RightNav.Width))
		        'Body.Width = "400"
			end sub
	                	
	end class
end namespace	