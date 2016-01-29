<%@ Page language="vb" Codebehind="bugreport.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.bugreport" Debug="false" trace="false" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script language="JavaScript" src="../_include/default.js"></script>
<HTML>
	<HEAD>
		<title>WebMagicPortal.com</title>
	</HEAD>
	
<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">
	<form id="forms1a" runat="server" enctype="multipart/form-data" method="post">
	    <table>
	        <tr>
	              <td><img src="../images/bug.jpg.jpg" height=100 width=100 alt="I Likem DEAD!" /></td>
	              <td >
	                <table>
	                    <tr>
	                         <td> <font size=4 color=red><b>Bug/Enhancement/Feedback/FAQ Recording</b></font></td>
	                    </tr>
	                    <tr>
	                         <td>Report it here for them varmits to be SQUASHED!!! </td>
	                    </tr>
	                    <tr>
	                         <td> or jot down something new or different to make more folks happy!</td>
	                    </tr>
	                   
	                </table>     
	              </td>
	        </tr>
	    </table>
	    <asp:Panel ID="pnlsaved" runat=server Visible=false> 
	    	<table>
	    		<tr>
	    			<td><font size=3 color=red>Your information has been saved.  You will be notified once this has been resolved.  Thank You</font></td>
	    		</tr>
	    	</table>      
	        
	   </asp:panel>
	    <asp:Panel ID="pnlcurrentbugs" runat=server Visible=true><div style="vertical-align: top; height: 400px; overflow:auto;">
	    <table width=100%>
	        <tr>
	            <td width=190><b>Current Bugs/Enhancements</b></td>
	             <td width=40><asp:linkbutton id="bbgOpen" Text= "Open" visible=true
                    						runat="server" Font-Bold="True" Font-underline="True" Style="color:#ff0000; font-family:arial; font-size:8pt; cursor:hand"
                  							onClick="bgopen"  /></td>
			 <td width=50><asp:linkbutton id="bbgClosed" Text= "Closed" visible=true
                    						runat="server" Font-Bold="True" Font-underline="True" Style="color:#ff0000; font-family:arial; font-size:8pt; cursor:hand"
                  							onClick="bgclosed"  /></td>
            <td width=40><asp:linkbutton id="bbgAll" Text= "All" visible=true
                    						runat="server" Font-Bold="True" Font-underline="True" Style="color:#ff0000; font-family:arial; font-size:8pt; cursor:hand"
                  							onClick="bgAll"  /></td>	
                <td width=100><asp:button id="addbug" runat=server text="Add New" OnClick="clickaddbug"  CausesValidation="False" cssclass=frmbuttons />	</td>
 						
	            <td align=right>Filter Total&nbsp&nbsp<asp:Label ID="totalbugs" runat=server /></td>
	        </tr>
	    </table>
	    <table width=100%>
	        <tr>
	            <td> <asp:DataGrid 
					        ID="bugs" 
				        AutoGenerateColumns=False
				        Width="100%"
	                    ColumnHeadersVisible = FALSE  
				        ItemStyle-BackColor=white
				        ItemStyle-Font-Name="arial"
				        ItemStyle-Font-Size="24px"
				        BorderColor="#000000"
				         AllowPaging="True" 
                        PageSize="5" 
                        PagerStyle-Mode="NumericPages" 
                        OnPageIndexChanged="bug_PageChanger"
                         OnItemDataBound="ItemDataBoundEventHandler"
				          Runat=server>
				        <HeaderStyle Font-Size="24px" Font-Bold="True" BackColor="steelblue" Forecolor="#ff0000"></HeaderStyle>
			                <Columns >
        		              
        		               <asp:BoundColumn HeaderText="Bug #"  DataField="bg_tbl_pk" visible=true ItemStyle-Width="30px"    />
		        		         <asp:BoundColumn HeaderText="Type"  DataField="bg_type" visible=true ItemStyle-Width="40px"    />
		        		        <asp:BoundColumn HeaderText="Status"  DataField="bg_status" visible=true ItemStyle-Width="40px"    />
		        		       
		        		      <asp:BoundColumn HeaderText="Entered By"  DataField="bg_who" visible=true ItemStyle-Width="60px"    />
		        		      <asp:BoundColumn HeaderText="What"  DataField="bg_what" visible=true ItemStyle-Width="500px" ItemStyle-Font-Size =X-Small   />
		        		    
		        		     	 <asp:TemplateColumn HeaderText="<font color=#FFF8C6><b></b></font>" ItemStyle-Width="50px"  >
					                                <ItemTemplate >
					                                    <table width=100%>
					                                        <tr>
					                                            <td><asp:button id="edittaskB" runat=server text="View/Edit" onclick="editbug" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:70px; cursor:hand" /></td>		
			                                                </tr>    
					                                    </table>   
					                                </ItemTemplate>                                                     
				                                </asp:TemplateColumn>
					        </Columns>
				        </asp:DataGrid></td>
	        </tr>
	    </table>
	    	       </asp:Panel> </div>    
	    <asp:Panel ID="pnladdbug" runat=server Visible=false>    
	    
	    <table>
	        <tr>
	            <td>Date</td>
	            <td><asp:TextBox ID="bdate"  size=20 runat=server /></td>
	        </tr>
	        <tr>
	            <td>Type</td>
	            <td><asp:DropDownList id="btype" runat="server" AutoPostBack=true OnSelectedIndexChanged ="closethebug"  >
                    <asp:ListItem Value="Bug" Text="Bug"/>
                    <asp:ListItem Value="Enhancement" Text="Enhancement"/>
                    <asp:ListItem Value="Feedback" Text="Feedback"/> 
                    <asp:ListItem Value="FAQ" Text="FAQ"/>                                     
                    </asp:DropDownList></td>

	        </tr>
	        <tr>
	            <td>Status</td>
	            <td><asp:DropDownList id="bstatus" runat="server" AutoPostBack=true OnSelectedIndexChanged ="closethebug"  >
                    <asp:ListItem Value="Open" Text="Open"/>
                    <asp:ListItem Value="Closed" Text="Closed"/>                                    
                    </asp:DropDownList></td>

	        </tr>
	         <tr>
	            <td>Found By</td>
	            <td><asp:DropDownList id="bwho" runat="server" 	DataValueField="name" /></td>

	        </tr>
	         <tr>
	            <td>URL</td>
	            <td><asp:TextBox ID="burl" runat=server size=50 /></td>
	        </tr>
	        <tr>
	            <td colspan=2><asp:label id="lbltext" runat="server" /></td>
	        </tr><tr>
	            <td colspan=2><asp:TextBox ID="bwhat" runat=server size=16 TextMode="MultiLine" Columns="90"  Rows="10"  /></td>
	        </tr>
	    </table>
	     </asp:Panel>
	      <asp:panel ID="pnlclosed" runat=server  >
	     <table>
	        <tr>
	            <td>Close Date</td>
	            <td><asp:TextBox ID="closedate"  size=20 runat=server /></td>
	        </tr>
	        <tr>
	            <td>Closed by</td>
	            <td><asp:DropDownList id="closedby" runat="server" DataValueField="name" /></td>

	        </tr>
	        <tr>
	            <td colspan=2>Result</td>
	        </tr><tr>
	            <td colspan=2><asp:TextBox ID="TextBox1" runat=server size=16 TextMode="MultiLine" Columns="90"  Rows="10"  /></td>
	        </tr>
	        </table>
	       
	      </asp:panel>
	     <asp:panel ID="pnlsavebtn" runat=server  >
	        <table>
	            <tr>
	                 <td><asp:button id="savebuga" OnClick="savebug" runat=server text="Save" CausesValidation="False" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;	width:80px; cursor:hand" />	</td>
     		        <td><asp:button id="cancelbuga" OnClick="cancelbug" runat=server text="Cancel" CausesValidation="False" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;	width:80px; cursor:hand" />	</td>
     		
	            </tr>
	        </table>
	    </asp:panel>
	    
	    
	</form>
	</body>	
</HTML>
