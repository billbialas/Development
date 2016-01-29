<%@ Page language="vb" Codebehind="emmanager.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.emmanager" Debug="false" trace="false" validateRequest=false  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script language="JavaScript" src="../_include/default.js"></script>
<link rel="stylesheet" href="../_include/default.css" type="text/css">

<HTML>
	<HEAD>
		<title>WebMagicPortal.com</title>
	</HEAD>
	
	<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">
		<form  name="addemail"  runat="server" >
            <asp:panel ID="pnlemmanager" runat=server Visible=true>
                <table width=100% border =0>
                    <tr>
	                    <td width=100% class=pgheaders>Email Manager</td>
	                </tr>
	            </table>
	            <table width=100% border=0>
	                <tr>
						<td width=27%>
							<table width=100% border=0>
								<tr>
									<td width=60><b>Search</b></td>
								 	<td width=50><asp:linkbutton id="clear" Text= "Clear" 
                   				        runat="server" Font-Bold="True" Font-underline="True" Style="color:#ff0000; font-family:arial; font-size:9pt; cursor:hand"
                    				    onClick="clearall" /></td>                    		 	
                   			        <td  width=25% align=middle><asp:linkbutton id="btn_search" Text= "Go" 
                    				    runat="server" Font-Bold="True" Font-underline="false" Style="color:#00AF33; font-family:arial; font-size:9pt; cursor:hand"
                    					onClick="filterVenuesAADSLK" /></td>                    		
					            </tr>	
                  	            <tr>
                  			        <td colspan=4><asp:textbox id="l_search" runat=server size=40 ontextchanged="filterVenuesAADS" autopostback="true" onmouseover="document.getElementById('myToolTip').className='activeToolTip'"
                                    onmouseout="document.getElementById('myToolTip').className='idleToolTip'"  /></td>
 	 				            </tr>                   
                            </table>
                        </td> 
                    </tr>
                </table>
                <table width=99% cellspacing=0 cellpadding=0>
                    <tr>
                        <td><asp:DataGrid Runat=server
						        ID="ads" 
		                        AutoGenerateColumns=False
				                Width="100%" 
				                AllowPaging="True"            
				                PageSize="10" 
				                PagerStyle-Mode="nextprev"  
								PagerStyle-HorizontalAlign="Right" 
								PagerStyle-NextPageText="Next" 
								PagerStyle-PrevPageText="Prev"
								OnPageIndexChanged="MyDataGrid_Page" 
								PagerStyle-Visible = "False"
								DataKeyField="tbl_leadad_pk"
								AllowSorting="True"
               			        OnSortCommand="SortCommand_Click"
								OnItemDataBound="ItemDataBoundEventHandler" CssClass="dg">
    	            			<HeaderStyle CssClass="dgheaders" />
        					    <ItemStyle CssClass="dgitems" />
        					    <AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>     
		                        <Columns >	           		   
	           		                <asp:TemplateColumn HeaderText="<font color=#FFF8C6><b>Select</b></font>" ItemStyle-Width="80px" visible=false>
	        			  		        <HeaderTemplate  >
	        			  			        <asp:linkbutton id="clearcks" Text= "Clear Checked" runat="server" cssclass=linkbuttons visible =true onClick="clearcks" />
	        			  		        </HeaderTemplate>
	        			  		        <ItemTemplate >
									        <asp:CheckBox ID="myCheckbox" Runat="server" AutoPostBack=True OnCheckedChanged="GetSelections_Click2" />
								        </ItemTemplate>
							        </asp:TemplateColumn>
							        <asp:hyperlinkcolumn runat="server"  Text="AD No" datatextfield ="adno" headertext="AD No" ItemStyle-CssClass="dglinks"
	            		            DataNavigateUrlField ="adno" SortExpression="adno"
	            		            DataNavigateUrlFormatString="editad.aspx?&adno={0}&action=edit"  ItemStyle-HorizontalAlign=center ItemStyle-Width="40px" />
	           		                    <asp:BoundColumn visible=false HeaderText="<font color=red>*</font>Ad #"  DataField="adno" ItemStyle-Width="50px" SortExpression="adno"   />
							            <asp:BoundColumn visible=false HeaderText="<font color=#FFF8C6><b>Ad #</b></font>"  DataField="ad_status" ItemStyle-Width="10px"   />
	        			                <asp:BoundColumn ItemStyle-HorizontalAlign=center visible=true HeaderText="<font color=red>*</font>Leads"   SortExpression="ad_totalLeadcount" DataField="ad_totalLeadcount" ItemStyle-Width="40px"    />
	        			 	            <asp:BoundColumn HeaderText="<font color=red>*</font>Ad Title"  DataField="ad_title" ItemStyle-Width="300px" SortExpression="ad_title"  />
	        			                <asp:BoundColumn HeaderText="Published" visible=true DataField="ADPlaced" ItemStyle-Width="60px"    />
	        			                <asp:BoundColumn visible=true HeaderText="Status"  DataField="ad_Stage" ItemStyle-Width="60px"    />
	        			                <asp:BoundColumn visible=false HeaderText="Lead Type"  DataField="ad_leadtype" ItemStyle-Width="80px"    />
	        			                <asp:BoundColumn visible=false HeaderText="Lead Program"  DataField="ad_leadprogram" ItemStyle-Width="80px"    />
	        			                <asp:BoundColumn visible=false HeaderText="Marketting Program"  DataField="ad_marketprogram" ItemStyle-Width="80px"    />
	        			                <asp:BoundColumn HeaderText="<font color=red>*</font>Create Date"  DataField="cdate" ItemStyle-Width="80px" SortExpression="cdate"    />
	        			                <asp:TemplateColumn HeaderText="Actions" ItemStyle-Width="100px" visible=true  ItemStyle-CssClass="dgitemsNOBD">
					                        <ItemTemplate >
					                            <table cellspacing=0 cellpadding=1>
					                                <tr>
					                                     <td><asp:button id="BtnEditAD" visible=false runat=server text="Edit AD" onclick="EditPosting" CausesValidation="false" cssclass=frmbuttons /></td>		
			                                             <td><asp:button id="BtnPostAD" runat=server text="Publish AD" onclick="NewPosting" CausesValidation="false" cssclass=frmbuttons /></td>		
			                                        </tr>
					                            </table>
					                        </ItemTemplate>                                                     
				                        </asp:TemplateColumn>
			                    </Columns>
		                    </asp:DataGrid>
		                </td>
                    </tr>
                </table>
                <table width=99% cellspacing=1 cellpadding=0 bgcolor=#98B1C4>
		            <tr>			
			            <td width=900><asp:linkbutton id="Firstbutton" Text="<< 1st Page" 
                        CommandArgument="0" runat="server" cssclass=linkbuttons onClick="PagerButtonClick"/> &nbsp
          
    	                <asp:linkbutton id="Prevbutton" Text= "" 
                        CommandArgument="prev" runat="server" cssclass=linkbuttons onClick="PagerButtonClick"/> &nbsp
			
                        <asp:linkbutton id="Nextbutton" Text= "" 
                        CommandArgument="next" runat="server" cssclass=linkbuttons onClick="PagerButtonClick"/> &nbsp
    	         
    	                <asp:linkbutton id="Lastbutton" Text="Last Page >>" 
                        CommandArgument="last" runat="server" cssclass=linkbuttons  onClick="PagerButtonClick"/> &nbsp</td>
			            <td ><asp:Label id="lblPageCount" runat="server" Style="color:#333333"/>
 			 		    <asp:label id="RecordCount" runat="server" /></td>
	                </tr>
	            </table>
	            <table>
	                <tr>
	                    <td><asp:Button  id="btnCreateAdd" OnClick="createad" Text="New Email" runat="server" cssclass=frmbuttonsXLG /></td>
     	                 <td><asp:Button  id="btnManCamps" OnClick="Managecamps" Text="Campaigns" runat="server" cssclass=frmbuttonsXLG /></td>
     

	                </tr>
                </table>
            </asp:panel>        
	    </form>
	</body>
</HTML>
	