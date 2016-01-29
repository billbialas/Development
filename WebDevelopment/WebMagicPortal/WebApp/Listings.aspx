<%@ Page language="vb" Codebehind="listings.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.listings" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<HTML>
	<HEAD>
		<title>Choice One Realty- Michigan</title>
	</HEAD>
	
	<body MS_POSITIONING="FlowLayout">
		<form  name="listings"  runat="server" >
			<table>
				<tr>
					<td><font size=3><b>Active Listings</b></font></td>
				</tr>
			</table>
			<table>
				<tr>
					<td><span id="subsrch" runat="server"> 
							Search Within results 
							<input type="text" name="sub"> 
							<input type="submit" Value="Go"> 
						</span>
					</td>
				</tr>
				<tr>
					<td>
			<asp:DataGrid 
				ID="c1_listings" 
            AutoGenerateColumns=false
            Width="100%"
            ColumnHeadersVisible = false  
            ItemStyle-BackColor=white
            ItemStyle-Font-Name="arial"
            ItemStyle-Font-Size="24px"
            BorderColor="#ffffff"
            AllowPaging="True" 
            PageSize="4" 
            PagerStyle-Mode="NumericPages"  
				PagerStyle-HorizontalAlign="Right" 
				PagerStyle-NextPageText="Next" PagerStyle-PrevPageText="Prev"
				OnPageIndexChanged="MyDataGrid_Page" 

            Runat=server>
            
            <HeaderStyle Font-Size="24px" Font-Bold="True" BackColor="LightSalmon"></HeaderStyle>
            
            <Columns >
            	<asp:hyperlinkcolumn runat="server" datatextfield ="tbl_initprop_pk" headertext="C1 Listing#"
            		DataNavigateUrlField ="tbl_initprop_pk"
            		DataNavigateUrlFormatString="listingdetail.aspx?id={0}"  ItemStyle-HorizontalAlign=center/>
        			
          		<asp:BoundColumn HeaderText="Street#"  DataField="p_address" ItemStyle-Width="20px"    />
          		<asp:BoundColumn HeaderText="Street Name"  DataField="p_street" ItemStyle-Width="150px"    />
          		<asp:BoundColumn HeaderText="City"  DataField="p_city" ItemStyle-Width="150px"    />
        			<asp:BoundColumn HeaderText="Lock Box"  DataField="p_lockbox" ItemStyle-Width="20px"    />
        			<asp:BoundColumn HeaderText="Contact"  DataField="contact" ItemStyle-Width="120px"    />
        			<asp:BoundColumn HeaderText="Contact Phone"  DataField="cn_hphone" ItemStyle-Width="120px"    />
        			<asp:BoundColumn HeaderText="Showing Inst"  DataField="p_showinginst" ItemStyle-Width="150px"    />
        			
        </Columns>

     		</asp:DataGrid>
					</td>
				</tr>
			</table>
			<table>
				<tr>
					<td><asp:Button  id="btnAddlisting" OnClick="addlisting" Text="Add Listing" runat="server" /></td>
				</tr>
			</table>
	
		</form>
	</body>
</HTML>
