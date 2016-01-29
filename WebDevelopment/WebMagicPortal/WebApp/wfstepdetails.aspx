<%@ Page language="vb" Codebehind="wfstepdetails.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.wfstepdetails" Debug="false" trace="false"   enableeventvalidation=false validateRequest=false %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script language="JavaScript" src="../_include/default.js"></script>
<link rel="stylesheet" href="../_include/default.css" type="text/css">

<HTML>
	<HEAD>
		<title>www.WebMagicPortal.com</title>
	</HEAD>
	
<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">
	<form id="forms1a" runat="server" enctype="multipart/form-data" method="post">
		<table style="margin-bottom:10px;">
			<tr>
				<td class=pgheaders width=18%>Work Flow Details</td>
			</tr>
		</table>
		<table style="margin-bottom:10px;" width=50% class=tblborder>
			<tr>
				<td><b>Lead No</b></td>
				<td><b>Lead Name</b></td>
				<td><b>Work Flow No</b></td>
				<td><b>Work Flow Name</b></td>
			</tr>
			<tr>
				<td><asp:label id=lblleadno runat=server /></td>
				<td><asp:label id=lblleadname runat=server /></td>
				<td><asp:label id=lblworkflowno runat=server /></td>
				<td><asp:label id=lblworkflowname runat=server /></td>
			</tr>
		</table>
		<table width = 100%>
			<tr>
				<td class=wfstepheaderA>Work Flow Steps</td>
				<td align=right><asp:button id="stepexit" runat=server text="Exit" onclick="ExitStepDet" CausesValidation="false" cssclass=frmbuttons /></td>		
	                      
			</tr>
		</table>
		<table width=100%>
			<tr>
				<td>
					<div style="vertical-align top; height: 320px; overflow:auto;">
			  				<asp:DataGrid 
							ID="workflowSteps" 
							AutoGenerateColumns=False
							Width="100%"
			            ColumnHeadersVisible = FALSE  
			            OnItemDataBound="ItemDataBoundEventHandlerWFS"  
							AllowPaging="false"  CssClass="dg" Runat=server>
								<HeaderStyle CssClass="dgheaders" />
			        			<ItemStyle CssClass="dgitems" />
			        			<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>        
			        			<Columns >
        		      			<asp:BoundColumn HeaderText="ID"  DataField="lwf_tbl_pk" visible=false ItemStyle-Width="80px"    />
		        		       	<asp:BoundColumn HeaderText="Step No"  DataField="lwf_stepno" ItemStyle-Width="80px"    />
		        		       	<asp:BoundColumn HeaderText="Description"  DataField="wfs_Desc" ItemStyle-Width="170px"    />
		        		       	<asp:BoundColumn HeaderText="Status"  DataField="lwf_status" ItemStyle-Width="80px"    />
		        		       	<asp:BoundColumn HeaderText="Type"  DataField="wfs_type" ItemStyle-Width="135px"    />
									<asp:BoundColumn HeaderText="Freq"  DataField="wfs_Freq" ItemStyle-Width="80px"    />
									<asp:BoundColumn HeaderText="Run Date"  DataField="WFSdate" ItemStyle-Width="80px"    />
								  	<asp:TemplateColumn HeaderText="Actions" ItemStyle-Width="80px" ItemStyle-CssClass="dgitemsNOBD"  >
			                        <ItemTemplate >
			                            <table width=100% cellspacing=1 cellpadding=1>
			                                <tr>
			                                    <td><asp:button id="skipstep" runat=server text="Skip" onclick="skipstep" CausesValidation="false" cssclass=frmbuttons /></td>		
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
		            
 
		
	</form>
	</body>	
</HTML>
