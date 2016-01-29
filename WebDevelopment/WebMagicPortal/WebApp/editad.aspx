<%@ Page language="vb" Codebehind="editad.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.editad" Debug="false" trace="false" validateRequest=false  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script language="JavaScript" src="../_include/default.js"></script>
<script type="text/javascript">
<!--
    function copy(text) {
   
        window.clipboardData.setData('Text', text);
   
     }
     function copy2(text) {
         var email = text;
         //ar email = document.getElementById('adtitle');
         var emailValue = email.value;
         //copy to clipboard 
         // window.clipboardData.setData('Text' , emailValue );
         window.clipboardData.setData("text", emailValue);
         // get the clipboard data
     }

     function confirmDelete() {
         return window.confirm("are you sure?");
     }

     -->
    </script>

<HTML>
	<HEAD>
		<title>www.WebMagicPortal.com</title>
	</HEAD>
	
	<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">
		<form  name="editad"  runat="server" >
		    <table width=100%>
		        <tr>
		            <td width=90%><font size=3><b>Viewing AD</b></font></td>
		               <td><asp:button id="adclone" runat=server text="Clone AD" onclick="clonead" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:80px; cursor:hand" /></td>		
                     <td><asp:button id="exitad" runat=server text="Exit" onclick="ExitA" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:80px; cursor:hand" /></td>		
             
		        </tr>
		    </table>
		    	
		     <table width=100%>
          		<tr>
	                 <td  width=50><font size=2> <b>Ad No:</b></font></td>
	                 <td width=50><font color=red><asp:Label ID="adno" runat=server /></font></td>
	            
	                 <td  width=80><font size=2><b>Lead Type:</b></td>
	                 <td width=100><font color=red><asp:Label ID="adtype" runat=server /></font></td>
	            
	                 <td  width=100><font size=2><b>Lead Program:</b></td>
	                 <td width=150><asp:Label ID="adprog" runat=server /></td>
	           
	                 <td  width=110><font size=2><b>Response Page:</b></td>
	                 <td><asp:Label ID="adresp" runat=server /></td>
	            </tr>
	          </table>
	          <table width=100%>
	            <tr>
	                 <td valign=top width=60><font size=2> <b>Title:</b></font></td>
	                 <td><asp:Label ID="AdTitle" runat=server /></td>
	            </tr>
	           
	            
	             
	            <tr>
	              
	                 <td valign=top width=60><font size=2 ><b>Text:</b></td>
	                 <td><div id="textarea" style="vertical-align top; height: 100px; overflow:auto;" runat=server >
	                     <asp:Label ID="adtext" runat=server /></div></td>
	               
	            </tr>
	             
	       </table>
                   <asp:Panel ID="placead" runat="server" Visible="false">
            <table width=100%>
                    <tr>    
                        <td>
                            <hr id ='hrline' runat=server visible=false />
                        </td>
                    </tr>
                </table>           
               
                    <asp:Panel ID="pnladdvenue" runat="server" Visible="false">
                	    <table>
                		    <tr>	
                			    <td>Venue Name</td>
                			     <td>Venue Online</td>
                			    <td>Venue Code</td>
                			    <td>URL</td>
                			    <td>Private</td>
                			    <td><asp:checkbox id="privateven" runat=server /></td>
                		    </tr>
                		    <tr>
                		     <td><asp:textbox id="venuname" runat=server /></td>
                			   <td><asp:DropDownList id="ddvenonline" runat="server" >    							               
    							                 <asp:ListItem Value="Yes" Text="Yes"/>
  	    						                 <asp:ListItem Value="No" Text="No"/>
  	    						                </asp:DropDownList></td>
                			    <td><asp:textbox id="venuecode" runat=server /></td>
                			   <td><asp:textbox id="venueurl" runat=server /></td>
                			    
                		    
                		    </tr>
                		 </table>
                		 <table>
                		    <tr>	
                			    <td colspan=8><asp:button id="addnewv" runat=server text="Save" onclick="savenewvenue" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:80px; cursor:hand" /></td>		
    			                <td colspan=8><asp:button id="addnewvexit" runat=server text="Exit" onclick="savenewvenueExit" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:80px; cursor:hand" /></td>		
    			   
                		    </tr>
                	    </table>               
                    
                    </asp:panel>
                
                     
                <table width=100% border=0>
                    <tr>
                        <td width=10% valign=top>
                            <table width=100% runat=server id="table1">
                                                 
                            <tr>
                                <td><b>Ad Venues</b></td>
                                <td id="cell1" visible=false>Auto Responder</td>
                                <td visible=false>Photo</td>  
                                          
                            </tr>
                            <tr>
                                <td><asp:DropDownList ID="advenue" DataTextField="x_descr"
                                            autopostback=true OnSelectedIndexChanged="postadVen"
                  		                        DataValueField="tbl_xwalk_pk" 
                  		                        Runat="server" />
		                        </td>  
		                         <td id="cell1a"><asp:DropDownList ID="adautor" runat=server Visible=false>
                              		
		                            <asp:ListItem Value="No" Text="No"/>
		                            <asp:ListItem Value="Yes" Text="Yes"/>
		                            </asp:DropDownList>
		                        </td>    
		                         <td><asp:DropDownList ID="adphoto" runat=server Visible=false>
                              	
		                            <asp:ListItem Value="No" Text="No"/>
		                	            <asp:ListItem Value="Yes" Text="Yes"/>
		                            </asp:DropDownList>
		                        </td>  
            		              
		                       </tr>
		                       </table>
		                      <table>
		                       <tr> 
		                       
		                         <td><asp:button id="postadV" runat=server text="Not Listed" onclick="addnewvenue" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:80px; cursor:hand" /></td>		
			                         
	                        </tr>
	                    </table>
	                        <br />
	                    
                       </td>
                                             
                    <td valign=top>
                        <asp:Panel ID="pnlnoadvenues" runat="server" Visible="false">
                        <table>
                            <tr>
                                <td><b>Currently Selected venues</b></td>
                            </tr>
                        
                         <tr>
            	 	            <td>NONE</td>
            	             </tr>
           	            </table>
           	            </asp:panel>
                    
            <asp:Panel ID="pnlsavedv" runat="server" Visible="false">
            <table>
                <tr>
                    <td><b>Currently Selected venues</b></td>
                </tr>
            </table>
            <table width=100%>
            	
                <tr>
                    <td>
                       <div style="vertical-align top; height: 200px; overflow:auto;">
			 				<asp:DataGrid Runat=server
					    ID="ADVenues" 
	                    AutoGenerateColumns=False
	                    Width="100%"          
	                    ItemStyle-BackColor=white
	                    ItemStyle-Font-Name="arial"
	                    ItemStyle-Font-Size="12px"
	                    BorderColor="#000000"    	           
					    PagerStyle-Visible = "False"	
					    HeaderStyle-BackColor="steelblue"
					    HeaderStyle-ForeColor="White"
					    OnItemDataBound="ItemDataBoundEventHandler"
					    OnDeleteCommand="removeven"
					    DataKeyField="venno">
    	            
				       <Columns >
	           		   <asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Venue ID</b></font>" visible=false DataField="venno" ItemStyle-Width="10px"   />
	           		    <asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Venue</b></font>"  DataField="av_name" ItemStyle-Width="200px"   />    
	        			<asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Online</b></font>" visible=true DataField="av_online" ItemStyle-Width="80px"    />
	        		
	        			<asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Ad Code</b></font>"  DataField="av_key" ItemStyle-Width="80px"    />
	        	        <asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Key</b></font>" visible=true DataField="av_keyurl" ItemStyle-Width="100px"    />
	        		    <asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Published Status</b></font>" visible=true DataField="av_adplaced" ItemStyle-Width="160px"    />
	        		    <asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Lead Count</b></font>" visible=true DataField="av_resultsCnt" ItemStyle-Width="160px"    />
	        		 
	        		    <asp:TemplateColumn HeaderText="<font color=#FFF8C6><b></b></font>" visible=true ItemStyle-Width="100px"  >
					            <ItemTemplate >
					                <table width=100%>
					                    <tr>
					                        <td><input id="test" runat=server type="button" value="Grab AD" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:80px; cursor:hand"   /></td>		
			           </tr>    
					                </table>   
					            </ItemTemplate>                                                     
				            </asp:TemplateColumn>
	        	     	
	        			<asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Published Date</b></font>"  visible = false DataField="av_createdate" ItemStyle-Width="220px"    />
	        			    <asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Auto Respond</b></font>"  visible=false DataField="av_autorespond" ItemStyle-Width="160px"    />
	        			    <asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Photos</b></font>"  visible=false DataField="av_photos" ItemStyle-Width="160px"    />
	        			      
	        			      
	        			     
	        			     <asp:TemplateColumn HeaderText="<font color=#FFF8C6><b>Published Status</b></font>" visible=false ItemStyle-Width="100px"  >
					            <ItemTemplate >
					                <table width=100%>
					                    <tr>
					                        <td><asp:Hyperlink runat="server" Text='Unpublished' NavigateUrl=<%# "postad.aspx?adno=" + databinder.eval(container.dataitem,"adno") + "&venue=" + databinder.eval(container.dataitem,"av_name") + "&venueno="  + databinder.eval(container.dataitem,"venno") %>    ID="Hyperlinkresult" NAME="Hyperlinkresult"  /></td>
					                    </tr>    
					                </table>   
					            </ItemTemplate>                                                     
				            </asp:TemplateColumn>
				            <asp:TemplateColumn HeaderText="<font color=#FFF8C6><b>Auto Post</b></font>" visible=false ItemStyle-Width="100px"  >
					            <ItemTemplate >
					                <table width=100%>
					                    <tr>
					                        <td><asp:button id="autopost" runat=server text="Submit"  visible=false onclick="autopost_Click" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:80px; cursor:hand" /></td>		
			  
					                    </tr>    
					                </table>   
					            </ItemTemplate>                                                     
				            </asp:TemplateColumn>
				             <asp:TemplateColumn HeaderText="<font color=#FFF8C6><b></b></font>" ItemStyle-Width="100px"  >
					            <ItemTemplate >
					                <table width=100%>
					                    <tr>
					                         <td><asp:button id="markpub" runat=server text="Set Published" onclick="setstatpub" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:80px; cursor:hand" /></td>		
			         
					                    </tr>    
					                </table>   
					            </ItemTemplate>                                                     
				            </asp:TemplateColumn>
				             <asp:TemplateColumn HeaderText="<font color=#FFF8C6><b></b></font>" ItemStyle-Width="100px"  >
					            <ItemTemplate >
					                <table width=100%>
					                    <tr>
					                         <td><asp:button id="Publish" runat=server text="Publish" onclick="pubad" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:80px; cursor:hand" /></td>		
			         
					                    </tr>    
					                </table>   
					            </ItemTemplate>                                                     
				            </asp:TemplateColumn>
				            <asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Autopost</b></font>" visible=false DataField="av_autopost" ItemStyle-Width="220px"    />
	        			
			                <asp:TemplateColumn HeaderText="<font color=#FFF8C6><b></b></font>" ItemStyle-Width="100px"  >
					            <ItemTemplate >
					                <table width=100%>
					                    <tr>
					                         <td><asp:button id="removevenue" CommandName="Delete"  runat=server text="Remove"  CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:70px; cursor:hand" /></td>		
			         
					                    </tr>    
					                </table>   
					            </ItemTemplate>                                                     
				            </asp:TemplateColumn>
				            <asp:BoundColumn HeaderText="<font color=#FFF8C6><b>Autopost</b></font>" visible=false DataField="av_autopost" ItemStyle-Width="220px"    />
	        			<asp:BoundColumn HeaderText="<font color=#FFF8C6><b></b></font>" visible=false DataField="av_leadads_FK" ItemStyle-Width="80px"    />
	        			<asp:BoundColumn HeaderText="<font color=#FFF8C6><b>adtext</b></font>" visible=false DataField="av_textAll" ItemStyle-Width="80px"    />
	        	
			         </Columns>
		            </asp:DataGrid></div></td>
                </tr>
            </table>
             </asp:panel> 
                    </td>
                </tr>
            </table>
            </asp:Panel>   
            
            
            
	    </form>
	</body>
</HTML>
	