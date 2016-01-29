<%@ Page language="vb" Codebehind="emailmaint.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.emailmaint" Debug="false" trace="false" aspcompat=true  %>
<script language="JavaScript" src="../_include/default.js"></script>
<link rel="stylesheet" href="../_include/default.css" type="text/css">
<HTML>
	<HEAD>
		<title>www.WebMagicPortal.com</title>
	</HEAD>
	
<body onload="countdown();" onmousemove="timer=start" onclick="timer=start" onkeyup="timer=start">
	<form id="forms1a" runat="server" enctype="multipart/form-data" method="post">
	    <table>
	        <tr>
	      <td class=pgheaders width=95%>Text Templates</td>
	            <td><asp:ImageButton id="helptp" runat="server"  AlternateText="View Help" ImageAlign="left" ImageUrl="../images/wizard.jpg" height=50 width=80  OnClick="btn_showhelp" /></td> 
      </tr>
	    </table>
	    <table WIDTH=48%>
	    	<tr>
	    		<td class=pgsubheaders align=right>Search</td>
	    		<td><asp:textbox id="l_search" runat=server size=25 ontextchanged="btnsearch" autopostback="true"  />&nbsp&nbsp
	    			<asp:linkbutton id="clear" Text= "Clear"  runat="server"  cssclass="linkbuttons"  onClick="clearall" /></td>
	    		
	    		<td>Scope:</td>
	    		<td><asp:DropDownList id="dd_txtstat" AutoPostBack="true" OnSelectedIndexChanged="filtertemps"
							                  		DataValueField="x_descr" 
							                  		Runat="server" />   </td>
	    	</tr>
	    </table>
	    <table width=100%>
	        <tr>
	            <td> <asp:DataGrid	            
				        	ID="emails" 
				        	AutoGenerateColumns=False
				        	Width="100%"
			        		AllowPaging="True" 
                    	PageSize="14" 
                    	PagerStyle-Mode="NumericPages" 
                    	OnPageIndexChanged="emails_PageChanger"
			          	Runat=server CssClass="dg">
			        			<HeaderStyle CssClass="dgheaders" />
			        			<ItemStyle CssClass="dgitems" />
			        			<AlternatingItemStyle CssClass="dgAltitems"></AlternatingItemStyle>


		                <Columns >
    		                <asp:hyperlinkcolumn runat="server" datatextfield ="email_tbl_pk" headertext="Temp.#" 
                            DataNavigateUrlField ="email_tbl_pk" DataNavigateUrlFormatString="emailmainteditadd.aspx?action=edit&id={0}"  ItemStyle-HorizontalAlign="center" ItemStyle-Width="50px" />
                         <asp:BoundColumn HeaderText="Scope"  DataField="x_descr" visible="true" ItemStyle-Width="150px"   />
     		              <asp:BoundColumn HeaderText="Template Name"  DataField="email_name" visible="true" ItemStyle-Width="150px"   />
     		               <asp:BoundColumn HeaderText="Template Description"  DataField="email_descrip" visible="true" ItemStyle-Width="300px"    />
     		               <asp:BoundColumn  HeaderText="Subject Line"  DataField="email_subject" visible="false" ItemStyle-Width="150px"    />
     		                        <asp:BoundColumn HeaderText="Create Date"  DataField="emdate" visible="true" ItemStyle-Width="80px"    />
        		   
				        </Columns>
			        </asp:DataGrid>
                 </td>
		    </tr>
	    </table>
	    <table>
	        <tr>
	           <td><asp:button id="btn_Add" Visible=true  runat=server text="Add" width="70" onclick="click_addnewemail" CausesValidation="False"  Style="background-color:steelblue; color:#FFFFFF; font-family:arial; font-size:9pt;width:100px; cursor:hand" /></td>					
		    </tr>
		</table>
 
		
	</form>
	</body>	
</HTML>
