<%@ Page language="vb" Codebehind="leadmatchworkflow.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.leadmatchworkflow" Debug="true" trace="true" aspcompat=true  %>
<%@ Register TagPrefix="mbdl" Namespace="MetaBuilders.WebControls" Assembly="MetaBuilders.WebControls" %>
<%@ Register TagPrefix="DBWC" Namespace="DBauer.Web.UI.WebControls" Assembly="DBauer.Web.UI.WebControls.HierarGrid" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script language="JavaScript" src="../_include/default.js"></script>
<HTML>
	<HEAD>
		<title>Choice One Realty- Michigan</title>
		<meta name="GENERATOR" Content="Microsoft Visual Studio 7.0">
		<meta name="CODE_LANGUAGE" Content="C#">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	
	<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">
		<form id="leadmatchworkflow" runat="server">
			
			<asp:Panel id="pnlallinfo" runat="server">	
			<table>
				<tr>
					<td><b>Tenant Data</b><font size=2>&nbsp&nbsp <asp:linkbutton id="tedit" Text= "edit" 
                    runat="server" Font-Bold="True" Font-underline="True" Style="color:#ff0000; font-family:arial; font-size:8pt; cursor:hand"
                    onClick="tedita" /> &nbsp criteria &nbsp <asp:linkbutton id="tprofile" Text= "profile" 
                    runat="server" Font-Bold="True" Font-underline="True" Style="color:#ff0000; font-family:arial; font-size:8pt; cursor:hand"
                    onClick="viewtprofile" /></font></td>
				</tr>
			<asp:Panel id="pnltenantinfo" runat="server">	
				<tr>
					<td>
						<table cellpadding=2 cellspacing=2 border=0 width=99%>
  							
							<tr>
								<td width=90 align=left><font size=1>First Name</font></td>
	  							<td width=90 align=left><font size=1>Last Name</font></td>
	  							<td width=90 align=left><font size=1>Home Phone</font></td>
	  							<td width=90 align=left><font size=1>Cell Phone</font></td>
	  							<td width=90 align=left><font size=1>Primary Email</font></td>
	  							<td width=90 align=left><font size=1>Other Email</font></td>
	  						
								
	  						</tr>
	  						<tr>
	  							<td><asp:textbox id="t_fname" runat=server size=13 height=18 Style="font-size:8pt" /></td>
	  							<td><asp:textbox id="t_lname" runat=server size=15 height=18 Style="font-size:8pt" /></td>
								<td ><asp:textbox id="t_hphone" runat=server size=13 height=18 Style="font-size:8pt"  /></td>
	  							<td ><asp:textbox id="t_cphone" runat=server size=13 height=18 Style="font-size:8pt" /></td>
								<td ><asp:textbox id="t_email" runat=server size=30 height=18 Style="font-size:8pt" /></td>
								<td ><asp:textbox id="t_email2" runat=server size=30 height=18 Style="font-size:8pt" /></td>
						
	  						</tr>
						</table>
					</td>
				</tr>
			</asp:panel>	
				<tr>
					<td><b>Landlord Data</b>&nbsp&nbsp <asp:linkbutton id="ledit" Text= "edit" 
                    runat="server" Font-Bold="True" Font-underline="True" Style="color:#ff0000; font-family:arial; font-size:8pt; cursor:hand"
                    onClick="ledita" /></td>
				</tr>
				<tr>
					<td>
						<table cellpadding=2 cellspacing=2 border=0 width=99%>
  							
							<tr>
								<td width=90 align=left><font size=1>First Name</font></td>
	  							<td width=90 align=left><font size=1>Last Name</font></td>
	  							<td width=90 align=left><font size=1>Home Phone</font></td>
	  							<td width=90 align=left><font size=1>Cell Phone</font></td>
	  							<td width=80 align=left><font size=1>Primary Email</font></td>
	  							<td width=80 align=left><font size=1>Other Email</font></td>
	  						</tr>
							<tr>
	  							<td><asp:textbox id="l_fname" runat=server size=13 height=18 Style="font-size:8pt" /></td>
		  						<td><asp:textbox id="l_lname" runat=server size=15  height=18 Style="font-size:8pt" /></td>
								<td ><asp:textbox id="l_hphone" runat=server size=13 height=18 Style="font-size:8pt" /></td>
	  							<td ><asp:textbox id="l_cphone" runat=server size=13 height=18 Style="font-size:8pt" /></td>
								<td ><asp:textbox id="l_email" runat=server size=30 height=18 Style="font-size:8pt" /></td>
								<td ><asp:textbox id="l_email2" runat=server size=30 height=18 Style="font-size:8pt" /></td>
	
	  						</tr>
						</table>
						<table>
							<tr>
								<td ><asp:button id="l_save" runat=server text="Save" onclick="btn_save" CausesValidation="false" Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:8pt;width:80px; cursor:hand" /></td>		
							</tr>
						</table>
							
					</td>
				</tr>
				<tr>
					<td>
						<table width=80%>
							<tr>
								
								<td colspan=10></td>
							</tr>
							<tr>
								<td width=20 align=right><font size=1><b>Property Info</b></font></td>
								<td>
									<table width=100% border=1>
										<tr>
											
											<td>Address</td>
											<td>City</td>
											<td>Zip</td>
											<td>Bedrooms</td>
											<td>Baths</td>
											<td>Rent Min</td>
											<td>Rent Max</td>
											<td>Other</td>
										</tr>
										<tr>
											
											<td><asp:label id='lbladdress' runat=server /></td>
											<td><asp:label id='lblcity' runat=server /></td>
											<td><asp:label id='lblzip' runat=server /></td>
											<td><asp:label id='lblbedrooms' runat=server /></td>
											<td><asp:label id='lblbaths' runat=server /></td>
											<td><asp:label id='lblrentmin' runat=server /></td>
											<td><asp:label id='lblrentmax' runat=server /> </td>
											<td>Other</td>
										</tr>	
									</table>
								</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</asp:panel>	
			<table width=50%>
				<tr>
					<td><b>Match Status</b><asp:label id='lblresult' runat=server /></td>
						<td >Tenant</td>
							<td><asp:DropDownList id="dd_tenstatus"  runat="server" >
							<asp:ListItem Value="Pending" Text="Pending"/>
							<asp:ListItem Value="Accepted" Text="Accepted"/>
							<asp:ListItem Value="Rejected" Text="Rejected"/>
						</asp:DropDownList></td>
						<td>Landlord</td>
						<td><asp:DropDownList id="dd_Lanstatus"  runat="server" >
							<asp:ListItem Value="Pending" Text="Pending"/>
							<asp:ListItem Value="Accepted" Text="Accepted"/>
							<asp:ListItem Value="Rejected" Text="Rejected"/>
						</asp:DropDownList></td>
				</tr>
			</table>
			
		<asp:Panel id="pnlworkflow" runat="server">	
			<table>	
				<tr>
					<td><b>Work Flow Steps</b>&nbsp&nbsp <asp:linkbutton id="wadd" Text= "Add" 
                    runat="server" Font-Bold="True" Font-underline="True" Style="color:#ff0000; font-family:arial; font-size:8pt; cursor:hand"  onClick="addworkflow" />
                    &nbsp&nbsp <asp:linkbutton id="wreorder" Text= "Reorder" runat="server" Font-Bold="True" Font-underline="True" Style="color:#ff0000; 
                    font-family:arial; font-size:8pt; cursor:hand" onClick="reorderworkflow" /></td>
				</tr>
			</table>
			<table width=99% >
				<tr>
					<td  ><div style="vertical-align top; height: 300px; overflow:auto;">
							<asp:datagrid 
							ID="DGworkflow" 
							AutoGenerateColumns=False
							Width="90%"
			            ColumnHeadersVisible = FALSE  
							ItemStyle-BackColor=white
							ItemStyle-Font-Name="arial"
							ItemStyle-Font-Size="24px"
							ItemStyle-verticalAlign=top
							BorderColor="#ffffff"
							OnItemDataBound="ItemDataBoundEventHandler"
							 onItemCommand = "ItemDataCommandEventHandler"
							AllowPaging="false"  Runat=server>
					
							<HeaderStyle Font-Size="12px" Font-Bold="True" BackColor="steelblue"></HeaderStyle>
							 
							 <Columns >
							  	<asp:Boundcolumn HeaderText="StepKey"  DataField="tbl_pmws_pk" ItemStyle-Width="1px" Visible="False"    />
								
								<asp:Boundcolumn HeaderText="Order#"  DataField="pmws_seq" ItemStyle-Width="1px" ItemStyle-HorizontalAlign=Center  />

								<asp:Boundcolumn HeaderText="Step"  DataField="pmws_step" ItemStyle-Width="50px"    />
								<asp:Boundcolumn HeaderText="PreReq"  DataField="pmws_prereq" ItemStyle-Width="25px"    />
								<asp:Boundcolumn HeaderText="Who"  DataField="pmws_who" ItemStyle-Width="25px" Visible="False"   />
								<asp:Boundcolumn HeaderText="TenStat"  DataField="pmws_TenStatus" ItemStyle-Width="25px"  Visible="False"  />
								<asp:Boundcolumn HeaderText="LanStat"  DataField="pmws_LanStatus" ItemStyle-Width="25px"  Visible="False"  />
								<asp:TemplateColumn HeaderText="TenStat" ItemStyle-Width="10px" >
									<ItemTemplate >
										<asp:DropDownList id="dd_tenstatus" runat="server" AutoPostBack="true"
											OnSelectedIndexChanged="dd_tenstatus_SelectedIndexChanged" >
			    							<asp:ListItem Value="Not Started" Text="Not Started"/>
			  	    						<asp:ListItem Value="In Process" Text="In Process"/>
			  	    						<asp:ListItem Value="Canceled" Text="Canceled"/>
			  	    						<asp:ListItem Value="Completed" Text="Completed"/>
			 		 					</asp:DropDownList>
		 							</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="LanStat" ItemStyle-Width="10px"  >
									<ItemTemplate >
										<asp:DropDownList id="dd_lanstatus" runat="server" AutoPostBack="True" 
										OnSelectedIndexChanged="dd_lanstatus_SelectedIndexChanged" >
			    							<asp:ListItem Value="Not Started" Text="Not Started"/>
			  	    						<asp:ListItem Value="In Process" Text="In Process"/>
			  	    						<asp:ListItem Value="Canceled" Text="Canceled"/>
			  	    						<asp:ListItem Value="Completed" Text="Completed"/>
			 		 					</asp:DropDownList>
		 							</ItemTemplate>
								</asp:TemplateColumn>
								<asp:TemplateColumn HeaderText="test" ItemStyle-Width="10px"  >
									<ItemTemplate >
									
									</ItemTemplate>
								</asp:TemplateColumn>	
							</Columns>
						</asp:datagrid></div></td>

				</tr>
			</table>
		</asp:panel>
		<asp:Panel id="pnlworkflowreorder" runat="server">	
			<table>
				<tr>
					<td><b>Re-Order Work Flow Steps</b></td>
				</tr>
				<tr>
					<td><asp:listbox id="lstworkflow" runat="server"   
              				 DataTextField="pmws_step" DataValueField="pmws_seq" />
					</td>
					<td><asp:Button  id="wbtn_move"  onclick="DualList1_ItemsMoved" Text="Move Up" runat="server" /></td>
				</tr>
				<tr>	
					<td><asp:Button  id="wbtn_savereorder"  onclick="savereorder" Text="Save" runat="server" /></td>
				</tr>
			</table>
		</asp:panel>
		<asp:Panel id="pnlworkflowadd" runat="server">	
			<table>
				<tr>
					<td><b>Add Work Flow Steps</b></td>
				</tr>
			</table>
			<table>
				<tr>
					<td><asp:DropDownList id="dd_wfstep"  runat="server" DataTextField="pmwms_desc"
					   									DataValueField="tbl_pmwms_pk" ></asp:DropDownList></td>	</td>
				</tr>
				<tr>	
					<td><asp:Button  id="wbtn_savereorderA"  onclick="savereorder" Text="Save" runat="server" /></td>
				</tr>
			</table>
		</asp:panel>
			<table width=100%>
				<tr>
					<td><b>Actions</b></td>
				</tr>
				<tr>
					<td>
						<table>
							<tr>
								<td><asp:Button  id="btn_step1"  Text="Step 1" runat="server" /></td>
								<td><asp:Button  id="btn_step2"  Text="Step 2" runat="server" /></td>
								<td><asp:Button  id="btn_step3"  Text="Step 3" runat="server" /></td>
								<td><asp:Button  id="btn_step4"  Text="Step 4" runat="server" /></td>
							</tr>
						</table>
						<table>	
							<tr>
								<td><asp:Button  id="btn_step5"  Text="Send Contact Info- Landlord" runat="server" /></td>
								<td><asp:Button  id="btn_step6" onclick="btn_printapp" Text="Print Application" runat="server" /></td>
								<td><asp:Button  id="btn_step7"  Text="Show" onclick="btn_hideinfo" runat="server" /></td>
								<td>Action 8</td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
	</body>	
</HTML>
