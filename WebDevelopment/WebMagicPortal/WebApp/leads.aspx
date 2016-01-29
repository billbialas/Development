<%@ Page language="vb" Codebehind="leads.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.leads" Debug="false" trace="false" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script language="JavaScript" src="../_include/default.js"></script>
<link rel="stylesheet" href="../_include/default.css" type="text/css">


<HTML>
	<HEAD>
		<title>www.WebMagicPortal.com</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	
<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">
	<form id="bpo" runat="server"  >
		<table cellpadding=0 cellspacing=0 width=99%>
			<tr>
				<td class=pgheaders width=95%>Lead Manager</td>
				<td><asp:ImageButton id="helptp" runat="server"  AlternateText="View Help" ImageAlign="left" ImageUrl="../images/wizard.jpg" height=25 width=50  OnClick="btn_showhelp" /></td> 

			</tr>
		</table>
		<table>
			<tr>
				<td ><asp:Label ID="lblviewtype"  visible=false Runat="server" font-bold="true"/></td></td>
			</tr>
		</table>
		<table>
			<tr>
				<td ><font color=red><asp:Label ID="lblwfleadadd"  visible=false Runat="server" font-bold="true"/></font></td></td>
			</tr>
		</table>
		


		
		
<asp:Panel id="pnlleads" runat="server">
		
		<table width=100% border=0 cellpadding=0 cellspacing=0>	
			
			<tr>
				<td >
					<table border=0 width=50% cellpadding=0 cellspacing=0>
						<tr>
							<td class=pgsubheaders width=50>Search </td>
						
							<td width=70%><asp:textbox id="l_search" runat=server size=60 ontextchanged="btnsearch" autopostback="true" onmouseover="document.getElementById('myToolTip').className='activeToolTip'"
   onmouseout="document.getElementById('myToolTip').className='idleToolTip'"  /></td>
						<td width=50><asp:linkbutton id="clear" Text= "Clear" 
                    runat="server" cssclass=linkbuttons onClick="clearall" /> </td>
							<td ><asp:linkbutton id="btn_search" Text= "Go" 
                    runat="server" Font-Bold="True" Font-underline="false" Style="color:#00AF33; font-family:arial; font-size:9pt; cursor:hand"
                    onClick="btnsearch" /> 
							
						</td>
						</tr>	
					
					</table>
				</td>
				</tr><tr>
				<td  valign=top>
					<table cellpadding=2 cellspacing=2 border=0 width=100% >
						<tr>
							<td class="LeadDrops" width=80>Entry Source</td>
							<td class="LeadDrops" >Lead Program</td>
							<td align="left"><asp:Label ID="lblleadtype" Text="Lead Type" Runat="server" cssclass="LeadDrops" /></td>
							<td align="left"><asp:Label ID="lblstatus" Text="Marketing Program" Runat="server" cssclass="LeadDrops"  /></td>
							<td align="left"><asp:Label ID="lblcstatus" Text="Lead Status" Runat="server" cssclass="LeadDrops"  /></td>
							<td align="left"><asp:Label ID="lblassignedto" visible=false Text="Assigned To" Runat="server" cssclass="LeadDrops"  /></td>
							<td align="left"><asp:Label ID="lblassignedby" visible=false Text="Entered By" Runat="server" cssclass="LeadDrops"  /></td>
							<td align="left" width=110><asp:Label ID="lbladcode" Text="Ad Number" Runat="server" cssclass="LeadDrops"  /></td>
							</tr>
						<tr>
							
						    <td ><asp:DropDownList id="dd_esource" cssclass="LeadDrops" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ChangetypeFilter">
				                <asp:ListItem Value="All" Text="All"/>
				                <asp:ListItem Value="Manual" Text="Manual"/>
				                <asp:ListItem Value="Auto" Text="Auto"/>
				                </asp:DropDownList></td>
							<td>	
								<asp:DropDownList ID="ddlleadprogramFilter" cssclass="LeadDrops"
                  		AutoPostBack="True"
                  		OnSelectedIndexChanged="ChangetypeFilter"
                  		DataValueField="x_descr" 
                  		Runat="server" /></td>			
							<td>	
								<asp:DropDownList ID="ddlleadtypeFilter" cssclass="LeadDrops"
                  		AutoPostBack="True"
                  		OnSelectedIndexChanged="ChangetypeFilter"
                  		DataValueField="x_descr" 
                  		Runat="server" /></td>
							<td>		
								<asp:DropDownList ID="ddlMarketFilter" cssclass="LeadDrops"
                  		AutoPostBack="True"
                  		OnSelectedIndexChanged="ChangetypeFilter"
                  		DataValueField="x_descr" 
                  		Runat="server" />				
								<asp:DropDownList ID="ddlstatusFilter" cssclass="LeadDrops"
                  		AutoPostBack="True"
                  		OnSelectedIndexChanged="ChangetypeFilter"
                  		DataValueField="x_descr" visible=false
                  		Runat="server" />	</td>
           				<td>
								<asp:DropDownList ID="ddlcstatusFilter" cssclass="LeadDrops"
                  		AutoPostBack="True"
                  		OnSelectedIndexChanged="ChangetypeFilter"
                  		DataValueField="x_descr" 
                  		Runat="server" />	</td>
                  	<td>
								<asp:DropDownList ID="ddlassignedtoFilter" cssclass="LeadDrops"
                  		AutoPostBack="True" visible=false
                  		OnSelectedIndexChanged="ChangetypeFilter"
                  		DataValueField="name" 
                  		Runat="server" />	</td>
                  	<td>
								<asp:DropDownList ID="ddlassignedbyFilter" cssclass="LeadDrops"
                  		AutoPostBack="True" visible=false
                  		OnSelectedIndexChanged="ChangetypeFilter"
                  		DataValueField="nameby" 
                  		Runat="server" />	</td>
                  	
                  		<td>
								<asp:DropDownList ID="ddadcode" cssclass="LeadDrops"
                  		AutoPostBack="True"
                  		OnSelectedIndexChanged="ChangetypeFilter"
                  		DataValueField="ld_adcode" 
                  		Runat="server" />	
                  	</td>
                  
						</tr>
						
					</table>
				</td>
				
          </tr>
      </table><div id="myToolTipA" class="idleToolTip">Lead #</div>
      			<div id="myToolTip" class="idleToolTip">Lead #,Last & First Name, Home & Cell Phone, Email, Notes</div>
		<table width=100% cellspacing=0 cellpadding=0>
			<tr>
				<td>
				                	
      <asp:DataGrid Runat=server
				ID="lead_status" 
            AutoGenerateColumns=False
            Width="100%"  
            AllowPaging="True"            
            PageSize="12" 
            PagerStyle-Mode="nextprev"  
				PagerStyle-HorizontalAlign="Right" 
				PagerStyle-NextPageText="Next" 
				PagerStyle-PrevPageText="Prev"
				OnPageIndexChanged="MyDataGrid_Page" 
				PagerStyle-Visible = "False"
				OnItemDataBound="ItemDataBoundEventHandler"
				CssClass="dg" >
					<HeaderStyle CssClass="dgheaders" />
        			<ItemStyle CssClass="dgitems" />
        			<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>
            
			   <Columns >
           		<asp:hyperlinkcolumn runat="server" datatextfield ="tbl_leads_pk" headertext="Lead #" ItemStyle-CssClass="dglinks"
            		DataNavigateUrlField ="tbl_leads_pk"
            		DataNavigateUrlFormatString="addlead.aspx?action=view&id={0}"  ItemStyle-HorizontalAlign=center ItemStyle-Width="60px" />
        		
        			<asp:BoundColumn HeaderText="Name"  DataField="contact" ItemStyle-Width="250px"    />
           		<asp:BoundColumn HeaderText="Phone"  DataField="ld_hphone" ItemStyle-Width="100px"    />
        			<asp:BoundColumn HeaderText="Phone Other" visible=false DataField="ld_Cphone" ItemStyle-Width="140px"    />
        			<asp:BoundColumn HeaderText="Email" visible=true DataField="ld_email" ItemStyle-Width="200px"    />
        		
        			<asp:BoundColumn HeaderText="Add Date"  DataField="ld_adddateF" ItemStyle-Width="90px"    />
        			<asp:BoundColumn HeaderText="Lead Status"  DataField="ld_pstatus" ItemStyle-Width="180px"    />
        		
        			<asp:BoundColumn  visible=false HeaderText="Entry Source"  DataField="ld_entrysource" ItemStyle-Width="130px"    />
        			<asp:BoundColumn  HeaderText="ad code" visible=false DataField="ld_adcode" ItemStyle-Width="130px"    />
        			<asp:BoundColumn HeaderText="ad code" visible=false DataField="ld_adcode" ItemStyle-Width="130px"    />
        			<asp:TemplateColumn visible=false >
       					<ItemTemplate>
         					<asp:RadioButton   id="rbsira" visible=false  runat="server"/>
       					</ItemTemplate>
     				</asp:TemplateColumn>
     					<asp:hyperlinkcolumn runat="server" datatextfield ="tbl_leadad_pk" headertext="AD #" ItemStyle-CssClass="dglinks"
            		DataNavigateUrlField ="tbl_leadad_pk"
            		DataNavigateUrlFormatString="createad.aspx?action=edit&adno={0}&source=leads"  ItemStyle-HorizontalAlign=center ItemStyle-Width="60px" />
        		
      			
      				<asp:BoundColumn HeaderText="Assigned To" visible=false DataField="ld_agent" ItemStyle-Width="130px"    />
        		    <asp:TemplateColumn HeaderText="Actions" ItemStyle-Width="150px" visible=false >
					    <ItemTemplate >
					        <table cellspacing=1 cellpadding=1>
					            <tr>
					            	<td><asp:button id="btnaddtowf" runat=server text="Add to WorkFlow" onclick="addleadtoworkflow" CausesValidation="false" cssclass=frmbuttonsXLG /></td>		
									                        
					            </tr>
					        </table>
					    </ItemTemplate>                                                     
				    </asp:TemplateColumn>   			
      				
				    <asp:TemplateColumn HeaderText="Actions" ItemStyle-Width="150px" visible=false >
					    <ItemTemplate >
					        <table>
					            <tr>
					                <td width=35><asp:Hyperlink runat="server" Text='Note' NavigateUrl=<%# "note.aspx?LeadNo=" + databinder.eval(container.dataitem,"leadpk") + "&LeadType=" + databinder.eval(container.dataitem,"ld_type") +"&action=new&source=main"   %> Target="_blank"  ID="HyperlinkHist" NAME="HyperlinkHist"  /></td>
					                <td width=35><asp:Hyperlink runat="server" Text='Task' NavigateUrl=<%# "leadhistory.aspx?history=0&LeadNo=" + databinder.eval(container.dataitem,"leadpk") + "&LeadType=" + databinder.eval(container.dataitem,"ld_type") +"&action=new&source=main"   %>   ID="HyperlinkTask" NAME="HyperlinkTask" Target="_blank" /></td>
					                <td><asp:Hyperlink runat="server" Text='Email' NavigateUrl=<%# "email.aspx?LeadNo=" + databinder.eval(container.dataitem,"leadpk")   %> Target="_blank"  ID="HyperlinkEmail" NAME="HyperlinkEmail"  /></td>
					            </tr>
					        </table>
					    </ItemTemplate>                                                     
				    </asp:TemplateColumn>
               <asp:BoundColumn HeaderText="LeadPK"  DataField="tbl_leads_pk" ItemStyle-Width="250px" visible=false   />
           		
        		  

		     </Columns>
	

		</asp:DataGrid></td></tr></table>
		<table width=100% cellspacing=0 cellpadding=0 bgcolor=#98B1C4>
		<tr>
			
			<td width=86%>
	   <asp:linkbutton id="Firstbutton" Text="<< 1st Page" CommandArgument="0" runat="server" cssclass=linkbuttons  onClick="PagerButtonClick"/> &nbsp
          
    	<asp:linkbutton id="Prevbutton" Text= "" CommandArgument="prev" runat="server" cssclass=linkbuttons  onClick="PagerButtonClick"/> &nbsp
      
			
    <%-- Display the Next Page/Last Page buttons --%>
    	<asp:linkbutton id="Nextbutton" Text= ""  CommandArgument="next" runat="server" cssclass=linkbuttons  onClick="PagerButtonClick"/> &nbsp
    	<asp:linkbutton id="Lastbutton" Text="Last Page >>"  CommandArgument="last" runat="server" cssclass=linkbuttons  onClick="PagerButtonClick"/> &nbsp


		</td>
		
			<td >	<asp:Label id="lblPageCount" runat="server" Style="color:#333333"/>&nbsp&nbsp&nbsp&nbsp
 			 		<asp:label id="RecordCount" runat="server" />
 			 		<font color=white>Total=<asp:Label ID="totalleads" runat=server /></font>
			</td>
	</tr>
	
	</table>
	<table><tr>
				<td><asp:Button  id="btnAddlead" OnClick="newlead" Text="Add Lead" runat="server" cssclass=frmbuttons /></td>
				<td><asp:Button  id="btnupload" OnClick="uploadleads" Text="Upload Leads" runat="server" cssclass=frmbuttons /></td>
				<td><asp:Button  id="btnexport" OnClick="exportleads" Text="Export Leads" runat="server" cssclass=frmbuttons /></td>
            <td><asp:Button  id="btndeltedleads" visible=true OnClick="deletedleads" Text="Show Deleted" runat="server"  cssclass=frmbuttons /></td>
			
				<td><asp:Button  id="btnrefresh" OnClick="refresh" Text="Refresh" runat="server" cssclass=frmbuttons /></td>
				<td><asp:Button  id="btnreferals" OnClick="referals" Text="Referals" runat="server"  cssclass=frmbuttons /></td>
				<td><asp:Button  id="btnnocontact" visible=false OnClick="btn_nocontact" Text="No Contact" runat="server"  cssclass=frmbuttons /></td>
				<td><asp:Button  id="btnaging"  visible=false OnClick="btn_aging" Text="Aging" runat="server"  cssclass=frmbuttons /></td>
				<td><asp:Button  id="btnreports" visible=false OnClick="btn_reports" Text="Reports" runat="server"  cssclass=frmbuttons /></td>
				<td><asp:Button  id="btnpropmatch" visible=false OnClick="btndopropmatch" Text="Match Process" runat="server" cssclass=frmbuttons /></td>
         	<td><asp:Button  id="btnbacktoworkflow" OnClick="returntoworkflow" visible=false Text="Exit" runat="server" cssclass=frmbuttons /></td>
		    
		<asp:Panel id="pnlaging" runat="server">
				<td>
					<table>
						<tr>
							<td># Days Since Last Contact:</td>
						</tr>
						<tr>
							<td align=right><asp:textbox id="l_aging" runat=server size=5  />&nbsp&nbsp<asp:linkbutton id="go" Text= "Go" 
                    runat="server" Font-Bold="True" Font-underline="True" Style="color:#ff0000; font-family:arial; font-size:8pt; cursor:hand"
                    onClick="aginggo" /> </td>
                   </tr>
                </table>
             </td>
		</asp:panel>
	
	</tr>
	</table>
	</asp:panel>
	<asp:Panel id="pnlupload" runat="server">
		<table>
			<tr>
				<td class=pgsubheaders>Lead Upload</td>
				
			</tr>
			
		</table>
			<br>
		<table width=50%>
			<tr>
	        	<td ><b><u>Lead Type</u></b></td>
	        	<td ><b><u>Lead Status</u></b> </td>
		    	<td ><b><u>Lead Program</u></b></td>  										    
				<td ><b><u>Lead Source</u></b></td>
			</tr>
			<tr>        
	         <td><asp:DropDownList id="dd_leadtype" AutoPostBack="false"
            		DataValueField="x_descr" Runat="server" />	</td>  
              <td><asp:DropDownList id="dd_ldstat" runat="server" AutoPostBack="false"  
	               DataValueField="x_descr" Runat="server" /></td>
	           <td><asp:DropDownList id="dd_leadprogram"  AutoPostBack="false"
            		DataValueField="x_descr" Runat="server" />	</td>
              <td width=90><asp:DropDownList id="dd_source"  runat="server" DataTextField="x_descr"
						DataValueField="tbl_xwalk_pk" ></asp:DropDownList></td>
			</tr>
			<tr><td>&nbsp</td></tr>
			<tr>
				<td colspan=4><b>Choose Your File  To Upload</b></td>
         </tr>
         	<td colspan=4><Input ID="MyFile" Type="File" RunAt="Server" Size="40"></td>
			</tr>
		</table>
		<br>
		<table width=25%>
			<tr>
				<td width=10> <Input Type="Submit" Value="Upload" OnServerclick="Upload_Click" RunAt="Server"></td>
				<td width=100> <Input Type="Submit" Value="Cancel" OnServerclick="continue_Click" RunAt="Server"></td>
				<td onmouseover="document.getElementById('myToolTipA1').className='activeToolTipA'"
   onmouseout="document.getElementById('myToolTipA1').className='idleToolTipA'" ><b><u>View File Layout</u></b></td>
			</tr>
			
					
		</table>
		<div id="myToolTipA1" class="idleToolTip">Format is Comma Delimited with a carriage return line feed for each record and Named as *.csv<br>
				Layout-> Last Name, First Name, Address, City, State, Zip, Phone 1, Phone 2, FAx, Email 1, Email 2, Notes </div>
	
	</asp:panel>
	<asp:Panel id="pnluploadresult" runat="server">
		<table>
		<tr>
				<td><asp:label id=lblsplittext runat=server /> </td>
			</tr>
					
			<tr>
				<td><Div ID="UploadDetails" Visible="true" RunAt="Server">
            File Name: <Span ID="FileName" RunAt="Server"/> <BR>
            File Content: <Span ID="FileContent" RunAt="Server"/><BR>
            File Size: <Span ID="FileSize" RunAt="Server"/>bytes<BR><br>
            Total Leads: <Span ID="totleads" RunAt="Server"/></Div>
           <Span ID="Span1" Style="Color:Red" RunAt="Server"/>
           <Span ID="Span2" Style="Color:Red" RunAt="Server"/>
         
				</td>
			</tr>
			<tr>
				<td>
					<Input Type="Submit" Value="Continue" OnServerclick="continue_Click" RunAt="Server">
				</td>
			</tr>
		</table>
	</asp:panel>
	<asp:Panel ID="pnldelleads" runat=server Visible=false>
	<table>
		<tr>
			<td class=pgsubheaders>Deleted Leads</td>
		</tr>
	</table>
	 <table width=100%>
                <tr>
                    <td><asp:DataGrid Runat=server
						    	ID="delleads" 
		                  AutoGenerateColumns=False
				            Width="100%"          
				            AllowPaging="True"            
				            PageSize="10" 
				            PagerStyle-Mode="NumericPages" 
                       	OnPageIndexChanged="MyDataGridR_Page" 
								OnItemDataBound="ItemDataBoundEventHandlerDL" CssClass="dg">
									<HeaderStyle CssClass="dgheaders" />
				        			<ItemStyle CssClass="dgitems" />
				        			<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>
    	            
				       <Columns >
	           		        	<asp:BoundColumn HeaderText="Lead No"  DataField="dleadno" ItemStyle-Width="300px"   />
	        			    		<asp:BoundColumn HeaderText="Name"  DataField="name" ItemStyle-Width="300px"   />
	        			    		<asp:BoundColumn HeaderText="Phone No"  DataField="ld_hphone" ItemStyle-Width="300px"   />
	        			   		<asp:BoundColumn HeaderText="Email"  DataField="ld_email" ItemStyle-Width="300px"   />
	        			      	<asp:TemplateColumn HeaderText="" visible=true ItemStyle-Width="100px" ItemStyle-CssClass="dgitemsNOBD" >
					            <ItemTemplate >
					                <table width=100%>
					                    <tr>
					                        <td><asp:button id="restoredellead" runat=server text="Restore"  visible=true onclick="restorelead" CausesValidation="false" cssclass=frmbuttons /></td>		
			  
					                    </tr>    
					                </table>   
					            </ItemTemplate>                                                     
				            </asp:TemplateColumn>	    
				            
			         </Columns>
		            </asp:DataGrid>
		            </td>
                </tr>
            </table>	
	        <table>
	            <tr>
	                <td><asp:Button  id="btnbacktoleads" OnClick="showleads" Text="Exit" runat="server" cssclass=frmbuttons /></td>
		        </tr>
	        </table>
        	
	
	</asp:Panel>
	</form>
	</body>	
</HTML>
