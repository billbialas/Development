<%@ Page language="vb" Codebehind="workflow.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.workflow" Debug="false" trace="false" aspcompat=true  %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >
<script language="JavaScript" src="../_include/default.js"></script>
<link rel="stylesheet" href="../_include/default.css" type="text/css">

<HTML>
	<HEAD>
		<title>www.WebMagicPortal.com</title>
	</HEAD>
	
<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">
	<form id="forms1a" runat="server" enctype="multipart/form-data" method="post">
		<table cellpadding=0 cellspacing=0 border=0 width=100% id="HeaderLD">
	   	 <tr>  				                
             <td class=pgheaders width=18%>Workflows</td>
          </tr>
 		</table>
 		<table>
 			<tr>
				<td width=60><b>Search</b></td>
			 	<td width=50><asp:linkbutton id="clear" Text= "Clear" 
    				 runat="server" Font-Bold="True" Font-underline="True" Style="color:#ff0000; font-family:arial; font-size:9pt; cursor:hand"
     				onClick="clearall" /></td> 
     		</tr>
     	</table>
     	<table>
     		<tr>
   			<td colspan=4><asp:textbox id="l_search" runat=server size=40 ontextchanged="filterWFS" autopostback="true" onmouseover="document.getElementById('myToolTip').className='activeToolTip'"
             onmouseout="document.getElementById('myToolTip').className='idleToolTip'"  /></td>
             		<td><asp:Button  id="btnAddWF" OnClick="newWF" Text="Add Workflow" runat="server" cssclass=frmbuttons /></td>
		
			</tr>   
     	</table>
		 <table width=90% border=0>
              <tr>
                  <td >
  			        <asp:DataGrid 
				        	ID="worflows" 
			        		AutoGenerateColumns=False
			        		Width="100%"
                    	ColumnHeadersVisible = FALSE  
			        		AllowPaging="True" 
                     PageSize="12" 
                     PagerStyle-Mode="NumericPages" 
                     OnPageIndexChanged="workflows_PageChanger" CssClass="dg" Runat=server>
			          	<HeaderStyle CssClass="dgheaders" />
		        			<ItemStyle CssClass="dgitems" />
		        			<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>
			      
			            <Columns >
     		               <asp:hyperlinkcolumn runat="server" datatextfield ="wfm_tbl_pk" headertext="WF #" ItemStyle-CssClass="dglinks"
	                       DataNavigateUrlField ="wfm_tbl_pk" DataNavigateUrlFormatString="addeditwf.aspx?action=view&id={0}"  ItemStyle-HorizontalAlign="left" ItemStyle-Width="50px" />
                        <asp:BoundColumn HeaderText="WFPK"  DataField="wfm_tbl_pk" visible="false" ItemStyle-Width="80px"    />
     		              
                        <asp:BoundColumn HeaderText="Status"  DataField="wfm_status" visible="true" ItemStyle-Width="80px"    />
     		               <asp:BoundColumn HeaderText="Name"  DataField="wfm_name" visible="true" ItemStyle-Width="150px"    />
     		               <asp:BoundColumn HeaderText="Description"  DataField="wfm_descript" visible="true" ItemStyle-Width="200px"    />
     		               <asp:TemplateColumn HeaderText="Actions" ItemStyle-Width="80px" ItemStyle-CssClass="dgitemsNOBD"  >
		                        <ItemTemplate >
		                            <table width=100% cellspacing=1 cellpadding=1>
		                                <tr>
		                                    <td><asp:button id="wfedit" runat=server text="Edit" onclick="EditWf" CausesValidation="false" cssclass=frmbuttons /></td>		
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
			</tr>
		</table>	

	</form>
	</body>	
</HTML>
