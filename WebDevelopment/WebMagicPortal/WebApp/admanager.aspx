<%@ Page language="vb" Codebehind="admanager.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.admanager" Debug="false" trace="false" validateRequest=false  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script language="JavaScript" src="../_include/default.js"></script>
<link rel="stylesheet" href="../_include/default.css" type="text/css">

<HTML>
	<HEAD>
		<title>WebMagicPortal.com</title>
	</HEAD>
	
	<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">
		<form  name="addlead"  runat="server" >
            <asp:panel ID="pnladmanager" runat=server Visible=true>
            <table width=100% border =0>
                <tr>
	                <td width=100% class=pgheaders>Ad Manager</td>
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
                    		 	<td ><asp:linkbutton id="btnshowvens" Text= "Include Venues" 
                                    runat="server" Style="color:#ff0000; font-family:arial; font-size:9pt; cursor:hand"
                                    visible =true onClick="showvenues" /></td> 
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
                 
                 <td>
                   	<table><tr>
									<td  >Lead Program</td>
									<td >Lead Type</td>
									<td >Marketing Program</td>
								</tr>
								<tr>
									<td>	
								<asp:DropDownList ID="ddlleadprogramFilter" 
                  		 autopostback=true  OnSelectedIndexChanged="filterADS"
                  		DataValueField="x_descr" 
                  		Runat="server" /></td>			
							<td>	
								<asp:DropDownList ID="ddlleadtypeFilter" 
                  		 autopostback=true  OnSelectedIndexChanged="filterADS"
                  		DataValueField="x_descr" 
                  		Runat="server" /></td>
							<td>		
								<asp:DropDownList ID="ddlMarketFilter"
                  		 autopostback=true  OnSelectedIndexChanged="filterADS"
                  		DataValueField="x_descr" 
                  		Runat="server" />	</td>
                  	</tr>
                  </table>
                  </td>     
                </tr>
             </table>
            <asp:panel id=pnlcadresults runat=server visible=false>
            	<table width=47%>
            		<tr>	
            			<td><font color=red>Select up to 4 ADs you would like to see then click</font> &nbsp <asp:linkbutton id="btnShowCAds" Text= "GO" runat="server" Font-Bold="True" visible =true onClick="chartresults" /> &nbsp <asp:linkbutton id="btncanCAds" Text= "Cancel" runat="server" Font-Bold="True" visible =true onClick="cancelchart" /></td>
            		</tr>
            	</table>
            </asp:panel>
            <asp:Label id="lblOrderBy" runat="server" Visible="False" />

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
			                             <td><asp:button id="BtnPostADOnce" runat=server text="Run Once" onclick="NewPostingA" CausesValidation="false" cssclass=frmbuttons /></td>		
			                             <td><asp:button id="Btnviewstats" runat=server text="View Stats" onclick="Viewstats" CausesValidation="false" cssclass=frmbuttons /></td>		
			                             <td><asp:button id="BtnWADPlans" runat=server text="View Plans" onclick="WrkWPlans" CausesValidation="false" cssclass=frmbuttons /></td>		
			                            
			                             <td><asp:button id="changestatDG" visible=false runat=server text="Change" onclick="Changestat" CausesValidation="false" cssclass=frmbuttons /></td>		
			                           
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
	                <td><asp:Button  id="btnCreateAdd" OnClick="createad" Text="New Ad" runat="server" cssclass=frmbuttonsXLG /></td>
            		<td><asp:Button  visible=false id="btnqucikadd" OnClick="quickad" Text="Quick Ad" runat="server" cssclass=frmbuttonsXLG /></td>
            		<td><asp:Button  id="btnstatus" OnClick="togglestatus" Text="Show All ADS" runat="server" cssclass=frmbuttonsXLG /></td>
	   			    <td><asp:Button  visible=false id="btnresponses" OnClick="mgresponses" Text="Branding" runat="server" cssclass=frmbuttonsXLG /></td>
            	    <td><asp:Button  id="btnvifno" visible=false OnClick="showvinfo" Text="LS Info" runat="server" cssclass=frmbuttonsXLG /></td>
	   			    <td><asp:Button  id="btnchartresults" OnClick="chartresultsA" Text="Chart AD Results" runat="server" cssclass=frmbuttonsXLG /></td>
	   			    <td><asp:Button  visible=false id="btnAPStat" OnClick="clickapstat" Text="AutoPost Stat" runat="server" cssclass=frmbuttons /></td>
	  			    	<td><asp:Button  visible=True id="btnshowposts" OnClick="showpostings" Text="Publishing Dates" runat="server" cssclass=frmbuttonsxLG /></td>
						<td><asp:Button  visible=True id="btnshowpostsQ" OnClick="showpostingsQ" Text="Publishing Work Q" runat="server" cssclass=frmbuttonsxLG /></td>
	
	    </tr>
            </table>
            </asp:panel>
            <asp:Panel ID="pnlrspmanager" runat=server Visible=false>
              <table width=100% border =0>
                <tr>
	                <td width=100% class=pgheaders>Custom Branding</td>
	            </tr>
	        </table>
            	<table width=100%>
           		<tr>
           			<td><b>Search</b></td>
           		</tr>
           		<tr>
           			<td width=20%><asp:textbox id="Pl_searchA" runat=server size=30  autopostback="true" ontextchanged="filterVenuesAb" onmouseover="document.getElementById('myToolTip').className='activeToolTip'"
                      onmouseout="document.getElementById('myToolTip').className='idleToolTip'"  />&nbsp
                      <asp:linkbutton id="clearRSP" Text= "Clear" 
                    runat="server" Font-Bold="True" Font-underline="True" Style="color:#ff0000; font-family:arial; font-size:8pt; cursor:hand"
                    onClick="clearallRSP" /></td>
                   
						
               </tr>
            </table>	
           			
           			
           			
           			
            <table width=100%>
                <tr>
                    <td><asp:DataGrid Runat=server
						    		ID="dgresponese" 
		                    	AutoGenerateColumns=False
				            	Width="100%"          
				             	AllowPaging="True" 
                           PageSize="11" 
                           PagerStyle-Mode="NumericPages" 
                           OnPageIndexChanged="MyDataGridR_Page">
                           <HeaderStyle CssClass="dgheaders" />
			        				<ItemStyle CssClass="dgitems" />
			        				<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>
    	            
				       <Columns >
				          <asp:hyperlinkcolumn runat="server"  Text="Edit" datatextfield ="rno" headertext="EDIT"
	            		    DataNavigateUrlField ="rno"
	            		    DataNavigateUrlFormatString="brandingadd.aspx?&id={0}&action=edit&source=rspmgr"  ItemStyle-HorizontalAlign=center ItemStyle-Width="50px" />
    	        		    
	           		     		<asp:BoundColumn HeaderText="Branding ID"  visible=false DataField="tbl_branding_pk" ItemStyle-Width="60px"   />
	           		     		<asp:BoundColumn HeaderText="Branding Name"  DataField="br_name" ItemStyle-Width="300px"   />
	        			    		<asp:BoundColumn HeaderText="Description"  DataField="br_description" ItemStyle-Width="300px"   />
	        			    		<asp:BoundColumn HeaderText="Show Logo" visible=false DataField="br_showlogo" ItemStyle-Width="300px"   />
	        			   		<asp:BoundColumn HeaderText="Send Auto Email" visible=false DataField="br_sendemail" ItemStyle-Width="300px"   />
	        			       	<asp:BoundColumn HeaderText="Receive Email on New Lead" visible=false DataField="br_getemail" ItemStyle-Width="300px"   />
	        			     		<asp:BoundColumn HeaderText="Status"  DataField="br_bstat" ItemStyle-Width="120px"   />
	        			     		<asp:TemplateColumn HeaderText="" ItemStyle-Width="100px" visible=true  ItemStyle-CssClass="dgitemsNOBD">
					            <ItemTemplate >
					                <table width=100%>
					                    <tr>
					                       <td><asp:button id="BtnEditAD"  runat=server text="Edit" onclick="editbranding" CausesValidation="false" cssclass=frmbuttons /></td>		
			                            
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
                    <td><asp:Button  id="btnaddrsps" OnClick="Addresp" Text="Add New" runat="server" cssclass="frmbuttons" /></td>
	   			       <td><asp:Button  id="btnshwinactives" OnClick="showinacts" Text="Show All" runat="server" cssclass="frmbuttons" /></td>
	   			  
	   			     <td><asp:Button  visible=false id="btnshowads" OnClick="toggleads" Text="Show ADS" runat="server" cssclass="frmbuttons" /></td>
	   			  
	   			  
                   </tr></table>
            
            
            </asp:Panel>   
            <asp:Panel ID="pnlvifno" runat=server Visible=false>
              <table width=100% border =0>
                <tr>
	                <td width=100%><font size=3><b>Lead Source User Information</b></font></td>
	            </tr>
	        </table>
            <br />
            <table width=100%>
                <tr>
                    <td><asp:DataGrid Runat=server
						    	ID="dgvinfo" 
		                  AutoGenerateColumns=False
				            Width="100%"          
				            ItemStyle-BackColor=white
				            ItemStyle-Font-Name="arial"
				            ItemStyle-Font-Size="12px"
				            BorderColor="#000000"
				            AllowPaging="True" 
                                        PageSize="10" 
                                        PagerStyle-Mode="NumericPages" 
                                        OnPageIndexChanged="vinfo_PageChanger"
								HeaderStyle-BackColor="steelblue"
								HeaderStyle-ForeColor="White"
								>
    	            
				       <Columns >
	           		     <asp:hyperlinkcolumn runat="server" datatextfield ="vpass_tbl_pk" headertext="<font color=#FFF8C6><b>Rec #</b></font>"
            		                            DataNavigateUrlField ="vpass_tbl_pk" DataNavigateUrlFormatString="vinfo.aspx?action=edit&id={0}"  ItemStyle-HorizontalAlign="left" ItemStyle-Width="50px" />
        		                                <asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Venue</b></font>"  DataField="vpass_venue" visible="true" ItemStyle-Width="150px"    />
				        		                <asp:BoundColumn HeaderText="<font color=#FFF8C6><b>ID</b></font>"  DataField="vpass_id" visible="true" ItemStyle-Width="80px"    />
				        		               <asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Password</b></font>"  DataField="vpass_pass" visible="true" ItemStyle-Width="80px"    />
				        		             
				            
			         </Columns>
		            </asp:DataGrid>
		            </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td><asp:Button  id="Button1" OnClick="Addvinfo" Text="ADD" runat="server" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;	width:80px; cursor:hand" /></td>
	   			     <td><asp:Button  id="Button2" OnClick="toggleads" Text="Show ADS" runat="server" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;	width:80px; cursor:hand" /></td>
	   			   
                   </tr></table>
            
            
            </asp:Panel>   
            
            <asp:panel id="pnlchart" runat=server visible=false>
            <table width=100%>
            	<tr>
            		<td width=40% valign=top><asp:DataGrid Runat=server
						    	ID="dgadresults" 
		                  AutoGenerateColumns=False
				            Width="100%"          
				            ItemStyle-BackColor=white
				            ItemStyle-Font-Name="arial"
				            ItemStyle-Font-Size="12px"
				            BorderColor="#000000"
				            AllowPaging="false"            
				          
								HeaderStyle-BackColor="steelblue"
								HeaderStyle-ForeColor="White"
								>
    	            
				       <Columns >
	           		    	<asp:BoundColumn HeaderText="<font color=#FFF8C6><b>AD #</b></font>"  DataField="tbl_leadad_pk" ItemStyle-Width="300px"   />
	        			    	
	           		    	<asp:BoundColumn HeaderText="<font color=#FFF8C6><b>AD Title</b></font>"  DataField="ad_title" ItemStyle-Width="300px"   />
	        			    	<asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Lead Count</b></font>"  DataField="ad_totalLeadcount" ItemStyle-Width="300px"   />
	        			    	<asp:BoundColumn HeaderText="<font color=#FFF8C6><b>% of Total Leads</b></font>"  DataField="PercentOfTotal" ItemStyle-Width="300px"   />
	        			   	<asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Total Leads</b></font>"  DataField="TotalLeads" ItemStyle-Width="300px"   />
	        			  
	           		     
	           		     
	           		     
				            
			         </Columns>
		            </asp:DataGrid></td>
            		<td width=60%><iframe id="fadresults" src="adresults.aspx" height="400" width=100% runat=server /></td>
             	</tr>
             	<tr>
             			   <td colspan=2><asp:Button  id="btnshowads2" OnClick="toggleads2" Text="Show ADS" runat="server" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;	width:80px; cursor:hand" /></td>
	   			  </tr>
	   			</table>
            </asp:Panel>
            <asp:Panel ID="pnlAPstat" runat=server Visible=false>
             <table width=100%>
                <tr>
                    <td><asp:DataGrid Runat=server
						    	ID="APstat" 
		                  AutoGenerateColumns=False
				            Width="100%"          
				            ItemStyle-BackColor=white
				            ItemStyle-Font-Name="arial"
				            ItemStyle-Font-Size="12px"
				            BorderColor="#000000"
				            AllowPaging="True" 
                            PageSize="10" 
                            PagerStyle-Mode="NumericPages" 
                            OnPageIndexChanged="Apstat_PageChanger"
							HeaderStyle-BackColor="steelblue"
							HeaderStyle-ForeColor="White">
    	            
				       <Columns >
	           		        <asp:BoundColumn HeaderText="<font color=#FFF8C6><b>AD #</b></font>"  DataField="tbl_leadad_pk" ItemStyle-Width="100px"   />
	        			   	<asp:BoundColumn HeaderText="<font color=#FFF8C6><b>AD Title</b></font>"  DataField="ad_title" ItemStyle-Width="100px"   />
	        			    <asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Venue</b></font>"  DataField="av_name" ItemStyle-Width="300px"   />
	        			    <asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Status</b></font>"  DataField="av_adplaced" ItemStyle-Width="100px"   />
	        			  	<asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Start Date</b></font>"  DataField="fdate" ItemStyle-Width="100px"   />
	        			    <asp:BoundColumn HeaderText="<font color=#FFF8C6><b>End Date</b></font>"  DataField="tdate" ItemStyle-Width="100px"   />
	        			    	             
				            
			         </Columns>
		            </asp:DataGrid>
		            </td>
                </tr>
            </table>
            <table>
                <tr>
                    <td colspan=2><asp:Button  id="apstatads" OnClick="toggleads3" Text="Show ADS" runat="server" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;	width:80px; cursor:hand" /></td>
	   			</tr>
	   	    </table>
            </asp:Panel>
            <asp:Panel ID="pnlpostings" runat=server Visible=false>
            <table>
            	<tr>
            		<td class=wfstepheaderA>AD Postings</td>
            	</tr>
            </table>
           	<table width=100%>
           		<tr>
           			<td><b>Search</b></td>
           			<td>Target Post Date</td>
           			<td>Published Status</td>
           			<td>Ad #</td>
           			<td>AD Plan #</td>
           		</tr>
           		<tr>
           			<td width=20%><asp:textbox id="Pl_search" runat=server size=40  autopostback="true" ontextchanged="filterVenuesA" onmouseover="document.getElementById('myToolTip').className='activeToolTip'"
                      onmouseout="document.getElementById('myToolTip').className='idleToolTip'"  /></td>
           			<td><asp:DropDownList ID="dd_PTDue" runat=server Visible=true autopostback=true  OnSelectedIndexChanged="filterVenues">
            			  	<asp:ListItem Value="All" Text="All"/>
    	            		<asp:ListItem Value="Due Today" Text="Due Today"/>
    	            		<asp:ListItem Value="Past Due" Text="Past Due"/>
                			<asp:ListItem Value="In 2 Days" Text="In 2 Days"/>
                			</asp:DropDownList></td> 
                	<td><asp:DropDownList ID="dd_PStat" runat=server Visible=true autopostback=true  OnSelectedIndexChanged="filterVenues">
            			  	<asp:ListItem Value="All" Text="All"/>
    	            		<asp:ListItem Value="Published" Text="Published"/>
                			<asp:ListItem Value="Unpublished" Text="Unpublished"/>
                			</asp:DropDownList></td> 
                	<td><asp:DropDownList ID="dd_ADs" DataTextField="Ftitle" autopostback=true  OnSelectedIndexChanged="filterVenues"
                          
	                        DataValueField="tbl_leadad_pk" 
	                        Runat="server" /></td>	
	              <td><asp:DropDownList ID="dd_ADPlan" DataTextField="Ftitle" autopostback=true  OnSelectedIndexChanged="filterVenues"
                          
	                        DataValueField="tbl_leadad_pk" 
	                        Runat="server" /></td>	
	                        
	            <td ><asp:button id="btnNewPPlanPost" visible=false runat=server text="New Post" onclick="NewPPlanPost" CausesValidation="false" cssclass=frmbuttons /></td>		
			    	<td ><asp:button id="btnrerunpost" runat=server text="Rerun Post" onclick="rerunpost" CausesValidation="false" cssclass=frmbuttons /></td>		
					<td ><asp:button id="btndelpost" runat=server text="Remove Post" onclick="deleteposts" CausesValidation="false" cssclass=frmbuttons /></td>		
							    			         		    			                  
           		</tr>
           	</table>
           		<asp:panel id=pnlentposts runat=server visible=true>
						                    
								              <table width=100%>   	
				         							<tr>
										            	<td >
										               <div id="ppgrid" runat=server style="vertical-align top; height: 420px; overflow:auto;">
													 			<asp:DataGrid Runat=server visible=true
															   		ID="ADVenuesPP" 
											                    	AutoGenerateColumns=False
											                    	Width="100%"          
															    		AllowPaging="false"            
														            PageSize="8" 
														            PagerStyle-Mode="NumericPages" 
																		OnPageIndexChanged="MyDataGrid_PagePP" 
																		DataKeyField="tbl_leadadvenues"
																		
												               	OnItemDataBound="ItemDataBoundEventHandlerPP"
												                   
															    		CssClass="dg">
										    	            		<HeaderStyle CssClass="dgheaders" />
													        			<ItemStyle CssClass="dgitems" />
													        			<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>
														       		<Columns >
															       		<asp:TemplateColumn HeaderText="<font color=#FFF8C6><b>Select</b></font>" ItemStyle-Width="20px">
					        			  										<HeaderTemplate  >
					        			  											<asp:linkbutton id="clearcks" Text= "All" runat="server" cssclass=linkbuttons visible =true  />
					        			  										</HeaderTemplate>
																				<ItemTemplate >
																					<asp:CheckBox ID="myCheckbox" Runat="server" AutoPostBack=True OnCheckedChanged="GetSelections_Click2" />
																				</ItemTemplate>
																			</asp:TemplateColumn>
											           		   		<asp:BoundColumn HeaderText="ID" visible=true DataField="venno" ItemStyle-Width="40px" readonly=true   />
											           		    		<asp:BoundColumn HeaderText="Venue"  DataField="av_name" ItemStyle-Width="160px" readonly=true    />    
											        						<asp:BoundColumn HeaderText="Can Self Publish" visible=false DataField="x_canselfpub" ItemStyle-Width="140px" readonly=true  />
											                        <asp:BoundColumn HeaderText="Ad Code" visible=true DataField="av_key" ItemStyle-Width="60px" readonly=true    />
											        	        			<asp:BoundColumn HeaderText="Status" visible=true DataField="av_adplaced" ItemStyle-Width="60px" readonly=true  />
											        			  			<asp:TemplateColumn HeaderText="Target Post Date" ItemStyle-Width="100px"  ItemStyle-CssClass="dgitemsNOBD" visible=false>
													            			<ItemTemplate >
													                			<table width=100%>
													                   			 <tr>
													                         			<td><asp:textbox id="txtTPD" runat=server  /></td>		
											         								 </tr>    
													                			</table>   
													            			</ItemTemplate>                                                     
												            			</asp:TemplateColumn>
												            			<asp:BoundColumn HeaderText="Target Post Date" visible=true DataField="APFTo" readonly=true ItemStyle-Width="100px"    />
												                		<asp:TemplateColumn HeaderText="Published Date" ItemStyle-Width="100px"  ItemStyle-CssClass="dgitemsNOBD" visible=true>
													            			<ItemTemplate >
													                			<table width=100%>
													                   			 <tr>
													                   			 		<td><asp:label id=lblPdate runat=server /></td>
													                         			<td><asp:textbox id="txtPdate" runat=server visible=false  /></td>		
											         								 </tr>    
													                			</table>   
													            			</ItemTemplate>                                                     
												            			</asp:TemplateColumn>
												                		
												                		
										        							
										        							<asp:TemplateColumn HeaderText="Posting Number" ItemStyle-Width="100px"  ItemStyle-CssClass="dgitemsNOBD" visible=true>
													            			<ItemTemplate >
													                			<table width=100%>
													                   			 <tr>
													                   			 		<td><asp:label id=lblPNO runat=server /></td>
													                         			<td><asp:textbox id="txtPNO" runat=server visible=false  /></td>		
											         								 </tr>    
													                			</table>   
													            			</ItemTemplate>                                                     
												            			</asp:TemplateColumn>
										        							
										        							
										        							<asp:TemplateColumn HeaderText="" ItemStyle-Width="20px"  ItemStyle-CssClass="dgitemsNOBD" visible=true>
													            			<ItemTemplate >
													                			<table width=100%>
													                   			 <tr>
													                         			<td><asp:button id="BTNSetPub" runat=server text="Set Published" onclick="SetVPub"  CausesValidation="false" cssclass=frmbuttonsLG /></td>		
											         								 </tr>    
													                			</table>   
													            			</ItemTemplate>                                                     
												            			</asp:TemplateColumn>
												                		<asp:TemplateColumn HeaderText="" ItemStyle-Width="20px"  ItemStyle-CssClass="dgitemsNOBD" visible=true>
													            			<ItemTemplate >
													                			<table cellpadding=0 cellspacing=0>
													                   			 <tr>
													                         			<td><asp:button id="PublishPP" runat=server text="Self Publish" onclick="pubad" CausesValidation="false" cssclass=frmbuttonslg /></td>		
											         										<td><asp:button id="UpdatePN" runat=server text="Edit Posting No" onclick="EditPNO"   CausesValidation="false" cssclass=frmbuttonsLG /></td>		
											         								
											         								 </tr>    
													                			</table>   
													            			</ItemTemplate>                                                     
												            			</asp:TemplateColumn>
												            			<asp:BoundColumn HeaderText="<font color=#FFF8C6><b></b></font>" visible=false DataField="av_leadads_FK" ItemStyle-Width="80px"    />
							        										<asp:TemplateColumn HeaderText="" ItemStyle-Width="20px"  ItemStyle-CssClass="dgitemsNOBD" visible=true>
													            			<ItemTemplate >
													                			<table width=100%>
													                   			 <tr>
													                         			<td><asp:button id="btnTD" runat=server text="Edit Target Date" onclick="ModTGD"  CausesValidation="false" cssclass=frmbuttonsLG /></td>		
											         								 </tr>    
													                			</table>   
													            			</ItemTemplate>                                                     
												            			</asp:TemplateColumn>
												            			<asp:TemplateColumn HeaderText="" ItemStyle-Width="20px"  ItemStyle-CssClass="dgitemsNOBD" visible=false>
													            			<ItemTemplate >
													                			<table width=100%>
													                   			 <tr>
													                         			<td><asp:button id="btnSTD" runat=server text="Save" onclick="ModTGDS"  CausesValidation="false" cssclass=frmbuttonsLG /></td>		
											         								 </tr>    
													                			</table>   
													            			</ItemTemplate>                                                     
												            			</asp:TemplateColumn>
												            			<asp:TemplateColumn HeaderText="" ItemStyle-Width="20px"  ItemStyle-CssClass="dgitemsNOBD" visible=false>
													            			<ItemTemplate >
													                			<table width=100%>
													                   			 <tr>
													                         			<td><asp:button id="btnSTDC" runat=server text="Cancel" onclick="ModTGDSC"  CausesValidation="false" cssclass=frmbuttonsLG /></td>		
											         								 </tr>    
													                			</table>   
													            			</ItemTemplate>                                                     
												            			</asp:TemplateColumn>
												            			<asp:BoundColumn HeaderText="Status" visible=false DataField="av_Postingno" ItemStyle-Width="60px" readonly=true  />
											        			  			<asp:BoundColumn HeaderText="Status" visible=false DataField="APFrom" ItemStyle-Width="60px" readonly=true  />
											        			  		
											        			  		</Columns>
														      </asp:DataGrid>
														  	</div>
												    		</td>
												    	</tr>
												 	</table>  
												</asp:panel>
           <asp:panel id="pnladdvenueN" runat=server visible=false>
							       	<table>
	                            	<tr>
	                              	<td class=wfstepheaderA>Add New Posting</td>
	                      		
	                            	</tr>                        
	           	            	</table>
								       	<table width=40% runat=server id="table1" style="margin-top:10">                                                 
		                           	<tr>
		                                <td width=120><b>Select Lead Source</b></td>
		                              </tr>
		                              <tr>
		                                <td width=25%><asp:DropDownList ID="advenue" DataTextField="x_descr"
		                                                autopostback=true OnSelectedIndexChanged="postadVen"
		                  		                        DataValueField="tbl_xwalk_pk" 
		                  		                        Runat="server" />
				                        	</td>  
				                          	<td width=10><asp:button id="postadV" runat=server text="Not Listed" onclick="addnewvenue" CausesValidation="false" cssclass=frmbuttons /></td>		
					                     	<td width=60%> <asp:button visible=true id="exitV" runat=server text="Exit" onclick="ExitADV" CausesValidation="false" cssclass=frmbuttons /></td>		
		            			      		<td id="cell1" visible=false>Auto Responder</td>
		                                <td visible=false>Photo</td>  
			                           </tr>
			                           
				                    	</table>
			                    	 <asp:Panel ID="pnladdvenue" runat="server" Visible="false" style="margin-top:10;">
				                	    <table width=100% class=tblbordera>
				                	    	 <tr>
				                	    	 	<td colspan=6><b>Add New Lead Source</b></td>
				                	    	 </tr>
				                		    <tr>	
				                			    <td valign=top>Lead Source Name</td>
				                			     <td valign=top>Online?</td>
				                			    <td valign=top>Lead Source Code <img src="../images/help_icon.jpg" alt="Help" height=15 width=15 onmouseover="document.getElementById('myToolTipc').className='activeToolTip'" onmouseout="document.getElementById('myToolTipc').className='idleToolTip'"/></td>
				                			    <td valign=top>URL</td>
				                			    <td>Has Account Setup?</td>
				                			     <td valign=top>Private?</td>
				                			  
				                		    </tr>
				                		    <tr>
				                		     <td valign=top><asp:textbox id="venuname" runat=server /></td>
				                			   <td valign=top><asp:DropDownList id="ddvenonline" runat="server" >    							               
				    							                 <asp:ListItem Value="Yes" Text="Yes"/>
				  	    						                 <asp:ListItem Value="No" Text="No"/>
				  	    						                </asp:DropDownList></td>
				                			    <td valign=top><asp:textbox id="venuecode" runat=server size=4 /></td>
				                			   <td valign=top><asp:textbox id="venueurl" runat=server size=30 /></td>
				                			     <td valign=top><asp:checkbox id="acctsetup" runat=server /></td>
				                		    
				                			    <td valign=top><asp:checkbox id="privateven" runat=server /></td>
				                		    
				                		    </tr>
				                		    <tr>
				                		        <td colspan=6><div id="myToolTipc" class="idleToolTip">Enter characters to help ID this AD Code, ie. Craigs List = CL</div></td></tr>
				                		 </table>
				                		 <table>
				                		    <tr>	
				                			    <td colspan=8><asp:button id="addnewv" runat=server text="Save" onclick="savenewvenue" CausesValidation="false" cssclass=frmbuttons /></td>		
				    			                <td colspan=8><asp:button id="addnewvexit" runat=server text="Exit" onclick="savenewvenueExit" CausesValidation="false" cssclass=frmbuttons /></td>		
				    			   
				                		    </tr>
				                	    </table>               
				                    
				                    </asp:panel> 
			       				    <asp:panel id="pnlLSdetail" runat=server visible=false >
				                    	<table width=65% style="margin-top:10">
				                    		<tr>
				                    			<td><b>Selected Venue</b></td>
				                    			<td><b>Status</b></td>
				                    			<td><b>AD Key</b></td>
				                    			<td><b>Expected Post Date</b></td>
				                    			<td><b>Published Date</b></td>
				                    			<td><b>Posting No</b></td>
				                    		</tr>
				                    		<tr>
				                    			<td><asp:label id=pstsvenue runat=server /></td>
				                    			<td><asp:label id=pststatus runat=server /></td>
				                    			<td><asp:label id=pstadkey runat=server /></td>
				                    			<td><asp:textbox id="pstEPdate" runat=server /></td>
				                    			<td><asp:textbox id="pstadfrom" runat=server /></td>
				                    			<td><asp:textbox id="pstadto" runat=server /></td>
				                    		</tr>
				                    </table>
				                    <table>
				                    		<tr>
				                    			<td><asp:button id="BTNSetPub" runat=server text="Set as Published" onclick="MarkPublished" CausesValidation="false" cssclass=frmbuttonsxlg /></td>		
					                     	<td><asp:button id="BTNPubNow" visible=false runat=server text="Publish Now" onclick="PublishNow" CausesValidation="false" cssclass=frmbuttonsxlg /></td>		
		            			          	<td><asp:button id="BTNPublater" runat=server text="Save & Exit" onclick="PublishLAter" CausesValidation="false" cssclass=frmbuttonsxlg /></td>		
		            			        		<td><asp:button id="BTNCancel" runat=server text="Cancel" onclick="ExitADV" CausesValidation="false" cssclass=frmbuttonsxlg /></td>		
		            			      	</tr>
		            			      </table>
				                 	 </asp:panel>	
						       	</asp:panel>
			       				    
           
           
           
           
            
            
            </asp:Panel>
            
            
            
            
	    </form>
	</body>
</HTML>
	