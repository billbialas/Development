<%@ Page language="vb" Codebehind="tlistings.aspx.vb" AutoEventWireup="false" Inherits="PageTemplate.tlistings" %>
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
					<td><IFRAME src="http://intranet.gochoiceone.com/pages/alistings.aspx" width="900" height="400"
             scrolling="auto" frameborder="0">
  				[Your user agent does not support frames or is currently configured
  						not to display frames. However, you may visit
  					<A href="foo.html">the related document.</A>]
  						</IFRAME>
 
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
