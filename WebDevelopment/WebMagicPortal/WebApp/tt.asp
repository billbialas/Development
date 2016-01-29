<%@ Language=VBScript %>
<HTML>
<HEAD>
<META NAME="GENERATOR" Content="Microsoft Visual Studio 6.0">
</HEAD>
<BODY>
<H1>Adobe FDF Example</H1>
<FORM NAME=W4Help ACTION=W4.asp METHOD = POST>
<TABLE>
	<TR>
		<TD ALIGN=RIGHT VALIGN=TOP>First Name</TD>
		<TD><INPUT TYPE=TEXT NAME=txtFirstName>
		</TD>
	</TR>
	<TR>
		<TD ALIGN=RIGHT VALIGN=TOP>Middle Initial
		</TD>
		<TD><INPUT TYPE=TEXT NAME=txtMI>
		</TD>
	</TR>
	<TR>
		<TD ALIGN=RIGHT VALIGN=TOP>LastName</TD>
		<TD><INPUT TYPE=TEXT NAME=txtLastName>
</TD>
	</TR>
		<TR>
		<TD ALIGN=RIGHT VALIGN=TOP>Social</TD>
		<TD><INPUT TYPE=TEXT NAME=txtSocial1 SIZE=3>-
<INPUT TYPE=TEXT NAME=txtSocial2 SIZE=2>-
<INPUT TYPE=TEXT NAME=txtSocial3 SIZE=2></TD>
	</TR>
	<TR>
		<TD ALIGN=RIGHT VALIGN=TOP>Street Address</TD>
		<TD><INPUT TYPE=TEXT NAME=txtStreetAddress></TD>
	</TR>
	<TR>
		<TD ALIGN=RIGHT VALIGN=TOP>City
		</TD>
		<TD><INPUT TYPE=TEXT NAME=txtCity></TD>
	</TR>
	<TR>
		<TD ALIGN=RIGHT VALIGN=TOP>State</TD>
		<TD><INPUT TYPE=TEXT NAME=txtState SIZE=2 MAXLENGTH=2></TD>
	</TR>
	<TR>
		<TD ALIGN=RIGHT VALIGN=TOP>Zip</TD>
		<TD><INPUT TYPE=TEXT NAME=txtZip SIZE=10></TD>
	</TR>
	<TR>
		<TD ALIGN=RIGHT VALIGN=TOP>Filing Status</TD>
		<TD>
		<INPUT TYPE=RADIO NAME=radFilingStatus VALUE="1">Single
		<BR>
		<INPUT TYPE=RADIO NAME=radFilingStatus VALUE="2">Married
		<BR>
		<INPUT TYPE=RADIO NAME=radFilingStatus VALUE="3">
		Married but withholding at the higher single rate.
		<BR></TD>
	</TR>
	<TR>
		<TD ALIGN=RIGHT VALIGN=TOP>Allowances Claimed</TD>
		<TD><INPUT TYPE=TEXT NAME=txtAllowances SIZE=2 MAXLENGTH=2>	</TD>
	</TR>
	<TR>
		<TD ALIGN=RIGHT VALIGN=TOP>Additional amount to withhold (if any)</TD>
		<TD><INPUT TYPE=TEXT NAME=txtAdditional SIZE=2 MAXLENGTH=2>
		</TD>
	</TR>
	<TR>
		<TD ALIGN=RIGHT VALIGN=TOP>I want to file Exempt from Withholding
		</TD>
		<TD><INPUT TYPE=CHECKBOX NAME=chkExempt></TD>
	</TR>
</TABLE>
<INPUT TYPE=SUBMIT>
</FORM>
</BODY>
</HTML>
