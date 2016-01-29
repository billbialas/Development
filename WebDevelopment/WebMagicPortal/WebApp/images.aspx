<%@ Page language="vb" Codebehind="images.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.images" Debug="false" trace="false" aspcompat=true  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >


<HTML>
	<HEAD>
		<title>WebMagicPortal.com</title>
	</HEAD>
	
<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">
	<form id="forms1a" runat="server" enctype="multipart/form-data" method="post">
	
	<asp:Panel ID="pnlsearch" runat=server Visible=true>
	<table>
	    <tr>
	           <td class=pgheaders width=95%>Image Library</td>
	      <td><asp:ImageButton id="helpim" runat="server"  AlternateText="View Help" ImageAlign="left" ImageUrl="../images/wizard.jpg" height=50 width=80  OnClick="btn_showhelp" /></td> 
	  
	    </tr>
	</table>
		<table width=100% border=0 cellpadding=0 cellspacing=0>
				<tr>
					<td width=60><b>Search</b></td>
				 	<td width=50><asp:linkbutton id="clear" Text= "Clear" 
       				 runat="server" Font-Bold="True" Font-underline="True" Style="color:#ff0000; font-family:arial; font-size:9pt; cursor:hand"
        				onClick="clearall" /></td>         		 	
       			<td width=220  align=middle></td>
        			<td width=70></td>
        			<td width=260>Folder:</td>
        			<td >Type:</td>
				</tr>
		</table>
		<table width=100% border=0 cellpadding=2 cellspacing=2>
      		<tr>
      			<td width=280><asp:textbox id="l_search" runat=server size=40  /></td>
                <td width=105><asp:linkbutton id="btn_searchA" Text= "Go" 
        				runat="server" Font-Bold="True" Font-underline="false" Style="color:#00AF33; font-family:arial; font-size:11pt; cursor:hand"
        					onClick="filterAAA" /></td>
                <td width=255>	<asp:DropDownList ID="ddlFolderFilter" 
                  		 autopostback=true  OnSelectedIndexChanged="filterDDs"
                  		DataValueField="ifld_name" 
                  		Runat="server" /></td>
					 	
					 <td>	<asp:DropDownList id="ddlimgtypeFilter" runat="server" autopostback=true  OnSelectedIndexChanged="filterDDs" >
             			<asp:ListItem Value="All" Text="All"/>
             			<asp:ListItem Value="Logo" Text="Logo"/>
             			<asp:ListItem Value="Other" Text="Other"/>			                
             			</asp:DropDownList></td>
             	 
		                 
				</tr>                   
     		</table>
    </asp:panel>
	 <table width=90% border=0>
            <tr>
                <td >
  			        <asp:DataGrid 
				        	ID="dgimages" 
			        		AutoGenerateColumns=False
			        		Width="100%"
                    	ColumnHeadersVisible = FALSE  
			         	AllowPaging="True" 
	                    PageSize="5" 
	                    PagerStyle-Mode="NumericPages" 
	                    OnPageIndexChanged="images_PageChanger"
	                    OnEditCommand="dgimages_edit"
	                    OnUpdateCommand="dgimages_Update"
	                    OnCancelCommand="dgimages_Cancel"
							CssClass="dg" 
                    EditItemStyle-BackColor="#eeeeee"
						OnItemDataBound="ItemDataBoundEventHandlerIMG"
			          Runat=server>
			          		<HeaderStyle CssClass="dgheaders" />
			        			<ItemStyle CssClass="dgitems" />
			        			<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>
							  <Columns >
							  <asp:TemplateColumn HeaderText="<font color=#FFF8C6><b>Select</b></font>" ItemStyle-Width="40px" visible=true>
	        			  			<HeaderTemplate  >
		        			  			<asp:linkbutton id="clearcks" Text= "select" runat="server" cssclass=linkbuttons visible =true onClick="clearcks" />
		        			  		</HeaderTemplate>
		        			  		<ItemTemplate >
										<asp:CheckBox ID="myCheckbox" Runat="server" AutoPostBack=True OnCheckedChanged="GetSelections_Click2" />
									</ItemTemplate>
								</asp:TemplateColumn>
    		                <asp:BoundColumn HeaderText="ID"  DataField="ui_tbl_pk" visible="true" readonly=true ItemStyle-Width="10px"    />
    		                <asp:BoundColumn HeaderText="Name"  DataField="ui_name" visible="true" ItemStyle-Width="150px"    />
    		                <asp:BoundColumn HeaderText="Description"  DataField="ui_descrip" visible="true" ItemStyle-Width="250px"    />
    		                <asp:BoundColumn HeaderText="Type"  readonly=true DataField="ui_type" visible="true" ItemStyle-Width="100px"    />
    		                <asp:BoundColumn HeaderText="Folder"  readonly=true DataField="foldername" visible="true" ItemStyle-Width="100px"    />
    		               <asp:BoundColumn HeaderText="File Name" ReadOnly=true  DataField="ui_filename" visible="false" ItemStyle-Width="80px"    />
    		               	 
    		               <asp:TemplateColumn ItemStyle-Width=100 >
									<ItemTemplate >
										<asp:hyperlink CssClass="dglinks" ID="Hyperlink1" runat="server" NavigateUrl=<%# DataBinder.Eval(Container.DataItem, "ui_location2").tostring + DataBinder.Eval(Container.DataItem, "ui_filename").tostring %> >
										<asp:Image ID="Image1" Width="80" Height="70" 									
										src='<%# DataBinder.Eval(Container.DataItem, "ui_location2").tostring +  DataBinder.Eval(Container.DataItem, "ui_filename").tostring %>'
								  		Runat=server />
								  		</asp:hyperlink>
									</ItemTemplate>                               
								</asp:TemplateColumn>
						         <asp:EditCommandColumn EditText="Edit" ItemStyle-Width=50
                                     ButtonType=PushButton  
                                      UpdateText="Update" CancelText="Cancel" visible=false >
									</asp:EditCommandColumn>

                         <asp:TemplateColumn HeaderText="" ItemStyle-Width="40px" ItemStyle-CssClass="dgitemsNOBD"  >
                                <ItemTemplate >
                                    <table width=100%>
                                        <tr>
                                            <td><asp:button id="editIMG" runat=server text="Edit" onclick="EditImageA" CausesValidation="false" cssclass=frmbuttons /></td>		
                                            <td><asp:button id="edittaskB" runat=server text="Remove" onclick="RemoveImage" CausesValidation="false" cssclass=frmbuttons /></td>		
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
	            <td><asp:Button ID="btng" runat=server OnClick="cdirect" text="Create" Visible=false cssclass=frmbuttonslg/></td>
	            <td><asp:Button ID="btnaddimg" runat=server OnClick="AddImage" text="Add" cssclass=frmbuttonslg/></td>
	            <td ><asp:Button ID="btnmgfolder" runat=server OnClick="MGfolders" text="Folders" cssclass=frmbuttonslg/></td>	
	            <td ><asp:Button ID="btnMoveImage" runat=server OnClick="MvImage" text="Move Images" cssclass=frmbuttonslg/></td>
	            <td ><asp:Button ID="btndeleteImages" runat=server OnClick="DelImages" text="Delete Images" cssclass=frmbuttonslg/></td>   
	            <td><asp:Button ID="btnexit" runat=server OnClick="exitimg" text="Return" Visible=false cssclass=frmbuttonslg/></td>
	        </tr>
	    </table>
	    <asp:Panel ID="pnladdimg" runat=server Visible=false>
	        <table width=100% cellpadding=4 cellspacing=0>
	            <tr>    
	                <td colspan=2><font size=3><b>Add Image</b></font></td>
	            </tr>
	            <tr height=4>
	            	<td></td>
	            </tr>
	            <tr> 
	            	<td align=left width=70>Folder:</td>
	               <td><asp:DropDownList ID="dd_imgfodler" 
   		            	DataValueField="ifld_name" 
   		            	Runat="server" /></td>
			         
	            </tr>
	            <tr>
	                <td align=left width=70>Type:</td>
	                <td ><asp:DropDownList id="dd_imgtype" runat="server" >
			                <asp:ListItem Value="Logo" Text="Logo"/>
			                <asp:ListItem Value="Other" Text="Other"/>			                
			                </asp:DropDownList></td>
	            </tr>
	            <tr>
	                <td align=left width=70>Name:</td>
	                <td ><asp:TextBox ID="imgname" runat=server size=40 /></td>
	            </tr>
	            <tr>
	                <td align=left width=70>Description:</td>
	                <td > <asp:TextBox ID="imgdesc" runat=server size=60 /></td>
	            </tr>
	            <tr>
	            		<td align=left width=70>Image:</td>
	                 <td ><input type=file id=logo name=File1 runat="server" size=50/></td> 
	            </tr>
	        </table>
	        <table>
	            <tr>
	                <td ><asp:Button ID="btnsaveimage" runat=server OnClick="SaveImage" text="Upload" cssclass=frmbuttons/></td>
	                <td ><asp:Button ID="btnncancel" runat=server OnClick="cancelimage" text="Cancel" cssclass=frmbuttons/></td>
	                <td ><asp:Button ID="btnaddfolder" runat=server OnClick="Addfolder" text="Add Folder" cssclass=frmbuttons/></td>
	   
	            </tr>
	        </table>
	    </asp:Panel>
	    <asp:Panel ID="pnladdfolder" runat=server Visible=false>
	       <table width=100% cellpadding=4 cellspacing=0>
	           <tr>    
	           		<td width=90>Folder Name:</td>
	           		<td ><asp:TextBox ID="txtfldrname" runat=server size=40 /></td>
	           	</tr>
	       </table>
	       <table>
	            <tr>
	                <td ><asp:Button ID="btnsavefolder" runat=server OnClick="SaveFolder" text="Save Folder" cssclass=frmbuttons/></td>
	                <td ><asp:Button ID="btnncancelfolder" runat=server OnClick="cancelsavefolder" text="Cancel" cssclass=frmbuttons/></td>
	          
	            </tr>
	        </table>
	     </asp:Panel>
	     
	    <asp:Panel ID="pnlmanagefolders" runat=server Visible=false>
			    <table width=100% cellpadding=4 cellspacing=0>
			            <tr>    
			                <td colspan=2><font size=3><b>Manage Image Folders</b></font></td>
			            </tr>
			       </table>
			       <table width=90% border=0>
		            <tr>
		                <td >
		  			        <asp:DataGrid 
						        	ID="dgFolders" 
					        		AutoGenerateColumns=False
					        		Width="100%"
		                    	ColumnHeadersVisible = FALSE  
					         	AllowPaging="True" 
			                    PageSize="5" 
			                    PagerStyle-Mode="NumericPages" 
			                    OnPageIndexChanged="folders_PageChanger"
			                   
									CssClass="dg" 
		                    EditItemStyle-BackColor="#eeeeee"
								
					          Runat=server>
					          		<HeaderStyle CssClass="dgheaders" />
					        			<ItemStyle CssClass="dgitems" />
					        			<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>
									  	<Columns >
		    		               <asp:BoundColumn HeaderText="ID"  DataField="ifld_tbl_pk" visible="true" readonly=true ItemStyle-Width="10px"    />
		    		               <asp:BoundColumn HeaderText="Name"  DataField="ifld_name" visible="true" ItemStyle-Width="300px"    />
		    		               <asp:TemplateColumn HeaderText="" ItemStyle-Width="40px" ItemStyle-CssClass="dgitemsNOBD"  >
		                                <ItemTemplate >
		                                    <table width=100%>
		                                        <tr>
		                                            <td><asp:button id="viewimages" runat=server text="Rename Folder"  onclick="fldrename" CausesValidation="false" cssclass=frmbuttonsXLG /></td>
		                                            <td><asp:button id="removefolder" runat=server text="Delete Folder" onclick="flddel"  CausesValidation="false" cssclass=frmbuttonsXLG /></td>		
		                        		
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
		        		 <td ><asp:Button ID="btnaddfolderM" runat=server OnClick="Addfolder" text="Add Folder" cssclass=frmbuttons/></td>
		        		 <td ><asp:Button ID="btnncancelM" runat=server OnClick="cancelimage" text="Cancel" cssclass=frmbuttons/></td>
			    
		        	</tr>
		        </table>
	       
	     </asp:Panel>
	     <asp:Panel ID="pnldelimgconfir" runat=server Visible=false>
			    <table width=100% cellpadding=4 cellspacing=0>
			            <tr>    
			                <td colspan=2><font size=3><b>Confirm Delete</b></font></td>
			            </tr>
			       </table>
			       <table width=90% border=0>
		            <tr>
		                <td >
		  			        <asp:DataGrid 
						        	ID="dgdelImages" 
					        		AutoGenerateColumns=False
					        		Width="100%"
		                    	ColumnHeadersVisible = FALSE  
					         	AllowPaging="True" 
			                    PageSize="5" 
			                    PagerStyle-Mode="NumericPages" 
			                   
			                   
									CssClass="dg" 
		                    EditItemStyle-BackColor="#eeeeee"
								
					          Runat=server>
					          		<HeaderStyle CssClass="dgheaders" />
					        			<ItemStyle CssClass="dgitems" />
					        			<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>
									  	<Columns >
		    		                 	 <asp:BoundColumn HeaderText="ID"  DataField="ui_tbl_pk" visible="true" readonly=true ItemStyle-Width="10px"    />
			    		                <asp:BoundColumn HeaderText="Name"  DataField="ui_name" visible="true" ItemStyle-Width="150px"    />
			    		                <asp:BoundColumn HeaderText="Description"  DataField="ui_descrip" visible="true" ItemStyle-Width="250px"    />
			    		                <asp:BoundColumn HeaderText="Type"  readonly=true DataField="ui_type" visible="true" ItemStyle-Width="100px"    />
			    		                <asp:BoundColumn HeaderText="Folder"  readonly=true DataField="foldername" visible="true" ItemStyle-Width="100px"    />
    		             				 <asp:TemplateColumn ItemStyle-Width=100 >
									<ItemTemplate >
										<asp:hyperlink CssClass="dglinks" ID="Hyperlink1" runat="server" NavigateUrl=<%# DataBinder.Eval(Container.DataItem, "ui_location2").tostring + DataBinder.Eval(Container.DataItem, "ui_filename").tostring %> >
										<asp:Image ID="Image1" Width="80" Height="70" 									
										src='<%# DataBinder.Eval(Container.DataItem, "ui_location2").tostring +  DataBinder.Eval(Container.DataItem, "ui_filename").tostring %>'
								  		Runat=server />
								  		</asp:hyperlink>
									</ItemTemplate>                               
								</asp:TemplateColumn>
		    		               <asp:TemplateColumn HeaderText="" ItemStyle-Width="40px" ItemStyle-CssClass="dgitemsNOBD"  >
		                                <ItemTemplate >
		                                    <table width=100%>
		                                        <tr>
		                                            <td><asp:button id="removeimgfrmlist" runat=server text="Exclude Image" OnClick="excldimg" CausesValidation="false" cssclass=frmbuttonsXLG /></td>		
		                        		
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
		        		 <td ><asp:Button ID="btnimgdelconfirm" runat=server OnClick="imgdelconfirm" text="Confirm Delete" cssclass=frmbuttonsLG/></td>
		        		 <td ><asp:Button ID="btnimgdelcancel" runat=server OnClick="cancelimage" text="Cancel" cssclass=frmbuttonsLG/></td>
			    
		        	</tr>
		        </table>
	       
	     </asp:Panel>
	    
	    
	    
	       <asp:Panel ID="pnldelimgMove" runat=server Visible=false>
			    <table width=100% cellpadding=4 cellspacing=0>
			            <tr>    
			                	<td width=270><font size=3><b>Confirm Move To Folder</b></font></td>
			                	<td align=left width=170><b><font size=2 color=red>Select Destination Folder:</font></b></td>
	               			<td><asp:DropDownList ID="dd_imgfodlerMove" 
   		            				DataValueField="ifld_name" 
   		            				Runat="server" /></td>
			            </tr>
			       </table>
			       <table width=90% border=0>
		            <tr>
		                <td >
		  			        <asp:DataGrid 
						        	ID="dgMovImages" 
					        		AutoGenerateColumns=False
					        		Width="100%"
		                    	ColumnHeadersVisible = FALSE  
					         	AllowPaging="True" 
			                    PageSize="5" 
			                    PagerStyle-Mode="NumericPages" 
			                   
			                   
									CssClass="dg" 
		                    EditItemStyle-BackColor="#eeeeee"
								
					          Runat=server>
					          		<HeaderStyle CssClass="dgheaders" />
					        			<ItemStyle CssClass="dgitems" />
					        			<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>
									  	<Columns >
		    		                 	 <asp:BoundColumn HeaderText="ID"  DataField="ui_tbl_pk" visible="true" readonly=true ItemStyle-Width="10px"    />
			    		                <asp:BoundColumn HeaderText="Name"  DataField="ui_name" visible="true" ItemStyle-Width="150px"    />
			    		                <asp:BoundColumn HeaderText="Description"  DataField="ui_descrip" visible="true" ItemStyle-Width="250px"    />
			    		                <asp:BoundColumn HeaderText="Type"  readonly=true DataField="ui_type" visible="true" ItemStyle-Width="100px"    />
			    		                <asp:BoundColumn HeaderText="Folder"  readonly=true DataField="foldername" visible="true" ItemStyle-Width="100px"    />
    		             				 <asp:TemplateColumn ItemStyle-Width=100 >
									<ItemTemplate >
										<asp:hyperlink CssClass="dglinks" ID="Hyperlink1" runat="server" NavigateUrl=<%# DataBinder.Eval(Container.DataItem, "ui_location2").tostring + DataBinder.Eval(Container.DataItem, "ui_filename").tostring %> >
										<asp:Image ID="Image1" Width="80" Height="70" 									
										src='<%# DataBinder.Eval(Container.DataItem, "ui_location2").tostring +  DataBinder.Eval(Container.DataItem, "ui_filename").tostring %>'
								  		Runat=server />
								  		</asp:hyperlink>
									</ItemTemplate>                               
								</asp:TemplateColumn>
		    		               <asp:TemplateColumn HeaderText="" ItemStyle-Width="40px" ItemStyle-CssClass="dgitemsNOBD"  >
		                                <ItemTemplate >
		                                    <table width=100%>
		                                        <tr>
		                                            <td><asp:button id="removeimgfrmlistMove" runat=server text="Exclude Image" OnClick="excldimgM" CausesValidation="false" cssclass=frmbuttonsXLG /></td>		
		                        		
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
		        		 <td ><asp:Button ID="btnimgMovconfirm" runat=server OnClick="imgmovconfirm" text="Confirm Move" cssclass=frmbuttonsLG/></td>
		        		 <td ><asp:Button ID="btnimgMovcancel" runat=server OnClick="cancelimage" text="Cancel" cssclass=frmbuttonsLG/></td>
			    
		        	</tr>
		        </table>
	       
	     </asp:Panel>
	    <asp:Panel ID="pnlcnfrmfolderdel" runat=server Visible=false>
	       <table width=100% cellpadding=4 cellspacing=0>
	            <tr>    
	           		<td colspan=4><font size=3><b>Delete Folder</b></font></td>
	           	</tr>
	           <tr>    
	           		<td colspan=4><font size=3 color=red>Warning </font><font size=3 >deleteing a folder will PERMANENTLY delete all images within the folder and will affect ADs and Branding Points that use those images.</font></td>
	           	</tr>
	           	<tr>
	           		<td width=200><font size=3>Are you sure?</font></td>
	           		<td ><asp:Button ID="btnflddelconfirm" runat=server OnClick="flddelyes" text="Delete" cssclass=frmbuttonsLG/></td>
	           		<td><asp:Button ID="btnflddelcancel" runat=server  OnClick="flddelno" text="Cancel" cssclass=frmbuttonsLG/></td>
	           		<td width=70%></td>
	           	</tr>
	           	
	       </table>
	       
	     </asp:Panel>
	    <asp:Panel ID="pnlcnfrmfolderrename" runat=server Visible=false>
	       <table width=100% cellpadding=4 cellspacing=0>
	           <tr>    
	           		<td colspan=2><font size=3><b>Rename Folder</b></font></td>
	           	</tr>
	           	<tr>
	           		<td width=120>Current Name</td>
	           		<td><asp:label id=fldrrename runat=server /></td>
	           	<tr>
	           		<td>New Name</td>
	           		<td><asp:textbox id=txtnfldrname runat=server size=30 /></td>
	           	</tr>
	        </table>
	        <table>
	        		<tr>
	           		<td ><asp:Button ID="btnfldrenconfirm" runat=server OnClick="fldrenyes" text="Rename" cssclass=frmbuttonsLG/></td>
	           		<td><asp:Button ID="btnfldrencancel" runat=server  OnClick="fldrenno" text="Cancel" cssclass=frmbuttonsLG/></td>
	           	</tr>
	           	
	       </table>
	       
	     </asp:Panel>
	    <asp:Panel ID="pnladdfolderExists" runat=server Visible=false>
	       <table width=100% cellpadding=4 cellspacing=0>
	           <tr>    
	           		<td>File/Folder already exists.  Please choose another.</td>
	           	</tr>
	       </table>
	       
	     </asp:Panel>
	     <asp:Panel ID="pnlupdateimg" runat=server Visible=false>
				 <table width=100% cellpadding=4 cellspacing=0>
				 	<tr>
				 		<td width=25% valign=top>
				 			<table width=100% cellpadding=4 cellspacing=0>
				            <tr>    
				                <td colspan=2><font size=3><b>Update Image</b></font></td>
				            </tr>
				            <tr height=4>
				            	<td></td>
				            </tr>
				            <tr> 
				            	<td align=left width=70>Folder:</td>
				               <td><asp:DropDownList ID="dd_imgfodlerE" 
			   		            	DataValueField="ifld_name" enabled=false
			   		            	Runat="server" /></td>
						         
				            </tr>
				            <tr>
				                <td align=left width=70>Type:</td>
				                <td ><asp:DropDownList id="dd_imgtypeE" runat="server" >
						                <asp:ListItem Value="Logo" Text="Logo"/>
						                <asp:ListItem Value="Other" Text="Other"/>			                
						                </asp:DropDownList></td>
				            </tr>
				            <tr>
				                <td align=left width=70>Name:</td>
				                <td ><asp:TextBox ID="imgnameE" runat=server size=40 /></td>
				            </tr>
				            <tr>
				                <td align=left width=70>Description:</td>
				                <td > <asp:TextBox ID="imgdescE" runat=server size=60 /></td>
				            </tr>
				            <tr>
				            		<td align=left width=70>Image:</td>
				                 <td ><asp:label id=lblimagefile runat=server /></td> 
				            </tr>
				        </table>
				      </td>
				      <td align=center><img ID="imglogo" runat=server src="" Height=250 Width=250 />
				      </td>
				     </tr>
				  </table>
	        <table>
	            <tr>
	                <td ><asp:Button ID="btnsaveimageE" runat=server OnClick="UpdateImage" text="Save" cssclass=frmbuttons/></td>
	                <td ><asp:Button ID="btnncancelE" runat=server OnClick="cancelimage" text="Cancel" cssclass=frmbuttons/></td>
	       
	            </tr>
	        </table>
	    </asp:Panel>
 
		
	</form>
	</body>	
</HTML>
