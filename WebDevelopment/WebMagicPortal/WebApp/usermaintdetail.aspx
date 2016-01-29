<%@ Page language="vb" Codebehind="usermaintdetail.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.usermaintdetail" Debug="false" trace="false" validateRequest=true  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script language="JavaScript" src="../_include/default.js"></script>
<link rel="stylesheet" href="../_include/default.css" type="text/css">
<HTML>
	<HEAD>
		<title>www.WebMagicPortal.com</title>
	</HEAD>
	
<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">
	<form id="forms1a" runat="server">
		<asp:Panel id="pnlUIDEXISTS" runat="server" visible=false>
			<table>
		        <tr>
			        <td><font size=4><marquee ALIGN="Top" LOOP="infinite" BEHAVIOR="slide" BGCOLOR="#FF0000" DIRECTION="left" HEIGHT=20 WIDTH=400>
					     User ID Exists. Please Choose Another one.</marquee></font></td>
		        </tr>
		   </table>
       </asp:panel>	
		<asp:Panel id="pnlRecordSaved" runat="server">
			<table>
		        <tr>
			        <td><font size=3>User has been saved.</font></td>
		        </tr>
		        <tr>
			        	<td align=right ><asp:button id="u_AddAnother" runat=server text="Add Another" onclick="AddAnotherUser" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:80px; cursor:hand" /></td>		
			  			<td align=right ><asp:button id="u_exit" runat=server text="Exit" width="70" onclick="btn_cancel" CausesValidation="False" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:60px; cursor:hand" /></td>
				
		        </tr>
		   </table>
       </asp:panel>	
	
	
	<asp:Panel id="pnlmainscreen" runat="server" visible=true >
	<table cellpadding=1 cellspacing=1 border=0 width=100% >
			<tr>
				<td Colspan=3><b>Personal Information</b></td>
			</tr>
	</table>
	<hr />
		<table cellpadding=1 cellspacing=1 border=0 width=100% >
			
			<tr>
			    <td  align=left>First Name</td>
			   <td  align=left>Last Name</td>
			   <td  align=left>Primary Email</td>
			    <td  align=left>Other Email</td>
			 
			</tr>
		    <tr>
			    <td><asp:textbox id="u_fname" runat=server size=20 /></td>
			    <td><asp:textbox id="u_lname" runat=server size=20 /></td>
			     <td ><asp:textbox id="u_email" runat=server size=30 AutoPostBack=true OnTextChanged="filluid"/></td>
			     <td ><asp:textbox id="u_email2" runat=server size=30 /></td>
		    </tr>
		    <tr>
		    		<td><asp:RequiredFieldValidator runat="server" id="Vu_fname"
          				ControlToValidate="u_fname" display="dynamic">
          				Required
      				</asp:RequiredFieldValidator></td>
      		<td></td>
		    	<td><asp:RequiredFieldValidator runat="server" id="Vu_lname"
          				ControlToValidate="u_lname" display="dynamic">
          				Required
      				</asp:RequiredFieldValidator></td>
      				<td></td>
		    	<td><asp:RequiredFieldValidator runat="server" id="Vu_email"
          				ControlToValidate="u_email" display="dynamic">
          				Required
      				</asp:RequiredFieldValidator></td>
      		</tr>
      	
			<tr>
		    <td  align=left>Address</td>	
		    <td  align=left>Address 2</td>
		    <td  align=left>City</td>
		   <td  align=left>State</td>
			<td  align=left>Zip</td>
			 
		</tr>
		<tr>
			    <td ><asp:textbox id="u_address" runat=server size=30 /></td>
			    <td ><asp:textbox id="u_address2" runat=server size=20 /></td>
			    <td ><asp:textbox id="u_city" runat=server size=15 /></td>
			    <td ><asp:DropDownList id="ddstate"  AutoPostBack="false"
               		DataValueField="statabb" 
               		Runat="server" /></td>
			    <td ><asp:textbox id="u_Zip" runat=server size=2 /></td>
		    </tr>
		    
		</table><br />
		
		<table cellpadding=1 cellspacing=1 border=0 width=100%>
		   <tr>
				<td Colspan=3><b>Company Information</b></td>
			</tr> 
		</table>
		<hr />
		<table cellpadding=1 cellspacing=1 border=0 width=100% >
		   <tr>
		   	<td  align=left>Company</td>
		   	<td  align=left>Company PK</td>
			   <td align=left>License No</td>
			     <td  align=left>Industry</td>
			
		   </tr>
			<tr>
		       <td ><asp:textbox id="u_company" runat=server size=20 /></td>
		       <td ><asp:textbox id="u_companyPK" runat=server size=20 /></td>
			     <td ><asp:textbox id="u_license" runat=server size=30 /></td>
			    <td ><asp:DropDownList id="dd_industry"  AutoPostBack="false"
               		DataValueField="x_descr" 
               		Runat="server" /></td>
          </tr>
          <tr>
		    	
		    	<td><asp:RequiredFieldValidator runat="server" id="Vu_company"
          				ControlToValidate="u_company" display="dynamic">
          				Required
      				</asp:RequiredFieldValidator></td>
      		<td></td>
		    	<td><asp:RequiredFieldValidator runat="server" id="Vdd_industry"
          				ControlToValidate="dd_industry" display="dynamic">
          				Required
      				</asp:RequiredFieldValidator></td>
      		</tr>
      		
      		<tr>
      		    <td  align=left>Business Phone</td>
			  <td  align=left>Cell Phone</td>
			     <td  align=left>Fax</td>
			 
      		</tr>
		    <tr>    
			    <td ><asp:textbox id="u_hphone" runat=server size=20 /></td>
			    <td ><asp:textbox id="u_cphone" runat=server size=20 /></td>
			    <td ><asp:textbox id="u_fax" runat=server size=20 /></td>
			   
		    </tr>
		</table><br />
		<table cellpadding=1 cellspacing=1 border=0 width=100%> 
			 <tr>
				<td Colspan=3><b>System Information</b></td>
			</tr>      
		</table>
		<hr />
		<table width=100%><tr><td>
						<table cellpadding=1 cellspacing=1 border=0 width=100%> 
							<tr>
							  <td  align=left>User ID</td>
							    <td  align=left>Password</td>
							     <td  align=left>Secret Code</td>
							     <td  align=left>Status</td>
							    <td  align=left>Role</td>
							  
				          
							</tr> 
						    <tr>
						    	
							    <td ><asp:textbox id="u_uid" runat=server size=20 /></td>
							    <td ><asp:textbox id="u_password" runat=server size=20 /></td>
							    <td ><asp:textbox id="u_scode" runat=server size=20 /></td>			    
							    
							    <td><asp:DropDownList id="dd_userstat"  AutoPostBack="true" OnSelectedIndexChanged ="statchange"
				               		DataValueField="x_descr" 
				               		Runat="server" /></td>
							    
							    <td ><asp:DropDownList id="dd_userrole"  AutoPostBack="false"
				               		DataValueField="x_descr" 
				               		Runat="server" /></td>
				             
							   
						    </tr>
						     <tr>
						    	<td></td>
						    	
						    	<td><asp:RequiredFieldValidator runat="server" id="Vu_password"
				          				ControlToValidate="u_password" display="dynamic">
				          				Required
				      				</asp:RequiredFieldValidator></td>
				      			
				      		</tr>
					    </table>
					 </td>
					 <td>
					 <asp:panel id="pnlbetauser" runat=server visible=false>
				           
				             	<table width=100% border=0>
				             		<tr>
				             			<td align=left valign=top>Subscription Type</td>
										  	<td  align=left>Beta User</td>
										   <td  align=left>Shopping Cart</td>
										</tr>
				             		<tr>
							             <td><asp:DropDownList id="dd_substat" runat="server" >
							    							                <asp:ListItem Value="Normal" Text="Normal"/>
							  	    						                <asp:ListItem Value="5 Day" Text="5 Day"/>
							  	    						                <asp:ListItem Value="10 Day" Text="10 Day"/>
							  	    						                <asp:ListItem Value="20 Day" Text="20 Day"/>
							 		 						                <asp:ListItem Value="30 Day" Text="30 Day"/>
							 		 						                 <asp:ListItem Value="Unlimitted" Text="Unlimitted"/>
							 								                </asp:DropDownList></td>
							              <td><asp:checkbox id="chkbeta" runat=server /></td>
							              <td><asp:checkbox id="chkcart" runat=server /></td>
							          </tr>
							      </table>
							   
				            </asp:panel>
					 </td>
			</tr>
		</table>
      <table width=10% border=0 cellspacing=2 cellpadding=0>
		  <tr>	
		    	<td align=right ><asp:button id="u_save" runat=server text="Save" onclick="SaveUser" CausesValidation="true" cssclass=frmbuttons /></td>		
		  		<td align=right ><asp:button id="l_cancel" runat=server text="Cancel" onclick="btn_cancel" CausesValidation="False" cssclass=frmbuttons /></td>
				<td align=right ><asp:button id="l_Advanced" runat=server text="Advanced" onclick="btn_usradvanced" CausesValidation="False" cssclass=frmbuttons /></td>
	
			</tr>
		</table>

	  </asp:panel>
	  <asp:Panel id="pnladvscreen" runat="server" visible=false >
	  		<table width=100%>
	  			<tr>
	  				<td width=90%><b><u>User Sub Account Role Assignment</u></b></td>
	  				<td ><asp:button id="l_usureturn" runat=server text="Return" onclick="btn_usradvancedR" CausesValidation="False" cssclass=frmbuttons /></td>
			
	  			</tr>
	  		</table>
	    	<table width=100%>   	
      		<tr>
		      	<td>
				      <div runat=server style="vertical-align top; height: 330px; overflow:auto;">
		 				<asp:DataGrid Runat=server visible=true
				   		ID="DGusradv" 
		              	AutoGenerateColumns=False
		              	Width="100%" 
		              	OnItemDataBound="ItemDataBoundEventHandlerADV" 
		                    
				    		CssClass="dg">
		         		<HeaderStyle CssClass="dgheaders" />
		        			<ItemStyle CssClass="dgitems" />
		        			<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>
			       		<Columns >
			       			<asp:BoundColumn HeaderText="User ID" visible=true DataField="UID" ItemStyle-Width="140px"   />
        			    		<asp:BoundColumn HeaderText="User Name"  DataField="FullName" ItemStyle-Width="200px"   />
        			    		<asp:TemplateColumn HeaderText="Leads- View" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" visible=true>
  										<ItemTemplate >
											<asp:CheckBox ID="chkLDVW" Runat="server"  />
										</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="Leads- Edit" ItemStyle-Width="80px" ItemStyle-HorizontalAlign="Center" visible=true>
  										<ItemTemplate >
											<asp:CheckBox ID="chkLDED" Runat="server"  />
										</ItemTemplate>
								</asp:TemplateColumn>	
								<asp:BoundColumn HeaderText="LeadView" visible=false DataField="LeadView" ItemStyle-Width="20px"   />
        			    		<asp:BoundColumn HeaderText="LeadEdit" visible=false  DataField="LeadEdit" ItemStyle-Width="20px"   />
        			    					        			    		
			       			<asp:TemplateColumn HeaderText="" visible=true ItemStyle-Width="80px" ItemStyle-CssClass="dgitemsNOBD" >
						            <ItemTemplate >
						                <table width=100%>
						                    <tr>
						                        <td><asp:button id="btnPublish" runat=server text="Update"  onclick="updateu2u" visible=true  CausesValidation="false" cssclass=frmbuttonsXLG /></td>		
						      					</tr>    
						                </table>   
						            </ItemTemplate>                                                     
					           	 </asp:TemplateColumn>
							 		
				         	</Columns>
			       		</asp:DataGrid>
								</div>
				 		</td>
					</tr>
				</table>  
	    		<table>
	    			<tr>
	    			</tr>
				</table>
	    
	  </asp:panel>  
	    
	   
	</form>
</body>	
</HTML>
