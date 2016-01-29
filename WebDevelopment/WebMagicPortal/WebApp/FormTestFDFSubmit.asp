<%@ LANGUAGE = VBScript%>
<%

' SET UP YOUR PAGE 
'********************************************************

Option Explicit
Response.Buffer = True
Response.Expires = -1441

' DECLARE ALL THE VARIABLES 
'*********************************************************

Dim strFirstName      ' First Name value for the txtFirstName field
Dim strProgExp        ' Value of description of programming experience
Dim strVBS           ' Value of Program Language(s) most familiar w/-VBScript
Dim strC             ' Value of Program Language(s) most familiar w/-C++
Dim strJava          ' Value of Program Language(s) most familiar w/-Java
Dim strGender         ' Gender of individual
Dim strFavSite        ' Favorite Web site
Dim strFavColor       ' String of favorite HTML color
Dim i                 ' Counter used to iterate all field names
Dim FDFAcX            ' FDF Toolkit ActiveX Version Object
Dim objFDF            ' FDF Object
Dim strVersion        ' FDF version number of this toolkit
Dim strColor          ' Font color to compliment the favorite color
Dim strFormFileName  ' PDF file name complete with the path
Dim strFld         ' Temporary reference passed to the FDFNextFieldName method
Dim strFieldName     ' Field Name extracted from the FDFNextFieldName method
Dim strFieldValue     ' Field Value extracted from the FDFGetValue method
Dim intUserLevel      ' User's position within a workplace environment

' INITIALIZE NECESSARY VARIABLES
'***********************************************************
' 0 = Requestor
' 1 = Requestor's immediate supervisor
' 2 = Level 1's supervisor
' 3 = Level 2's supervisor, has final authority

intUserLevel = 0   ' Normally this value would be extracted from 
			  'a Session variable.


' BEGIN FORM DATA COLLECTION USING THE FDF OBJECT HERE 
'************************************************************ 
Set FDFAcX = Server.CreateObject("FDFApp.FDFApp")

Set objFDF = FDFAcX.FDFOpenFromBuf (Request.BinaryRead(Request.TotalBytes))

' USE THE FDFGetValue METHOD TO COLLECT THE PDF's FORM FIELDS VALUES
' This is one method of collecting the field values.
'************************************************************
strFirstName = objFDF.FDFGetValue("txtFirstName")
strProgExp = objFDF.FDFGetValue("txtExperience")
strVBS = objFDF.FDFGetValue("chkVBS")
strC = objFDF.FDFGetValue("chkC")
strJava = objFDF.FDFGetValue("chkJava")
strGender = objFDF.FDFGetValue("radGender")
strFavSite = objFDF.FDFGetValue("selSite")
strFavColor = objFDF.FDFGetValue("selColor")

' Additional FDF information
strFormFileName = objFDF.FDFGetFile
strVersion = FDFAcX.FDFGetVersion

If strFavColor = "#FFFF00" OR strFavColor = "#FFCC00" Then
strColor = "black"
Else
strColor = "white"
End If

' SET UP YOUR CONNECTION OBJECT HERE
'**************************************************************

'SET UP YOUR RECORDSETS OR COMMAND OBJECTS HERE AND STORE THE
' ABOVE-EXTRACTED DATA
'****************************************************************

' CLOSE & CLEAN UP YOUR RECORDSETS, COMMAND OBJECTS, 
'& CONNECTION OBJECT HERE
'****************************************************************

' DISPLAY THE ABOVE-EXTRACTED DATA
'****************************************************************
%>

<html>

<head>
<title>FormTestFDF.pdf Posted Results </title>
<meta http-equiv="Content-Type" content="text/html; charset=iso-8859-1">
</head>

<body bgcolor="#FFFFFF">
<p>This will accept the data from FormTestFDF.pdf</p>
<!-- Provide a link using the actual saved FDF file (Saved below) -->
<p><a href="http://Inetpub/wwwroot/PDF/FormTestFDF.fdf">View this form again</a>
 to demonstrate a User's Level (position) within a workplace environment.</p>
<table width="50%" border="1" cellspacing="5" cellpadding="5">
<tr>
<td>This is the value from the txtFirstName field:</td>
<td> <% = strFirstName %> </td>
</tr>
<tr>
<td>This is the value from the txtExperience field:</td>
<td> <% = strProgExp %> </td>
</tr>
<tr>
<td>This is the value from the chkVBS field:</td>
<td> <% = strVBS %> </td>
</tr>
<tr>
<td>This is the value from the chkC field:</td>
<td> <% = strC %> </td>
</tr>
<tr>
<td>This is the value from the chkJava field:</td>
<td> <% = strJava %> </td>
</tr>
<tr>
<td>This is the value from the radGender field:</td>
<td> <% = strGender %> </td>
</tr>
<tr>
<td>This is the value from the selSite field:</td>
<td> <% = strFavSite %> </td>
</tr>
<tr>
<td>This is the value from the selColor field:</td>
<td bgcolor="<% = strFavColor %>"><font color="<% = strColor %>">
 <% = strFavColor %> </font></td>
</tr>
<tr>
<td>This FDF Version is:</td>.
<td> <% = strVersion %> </td>
</tr>
<tr>
<td>This PDF File Name is:</td>
<td> <% = strFormFileName %> </td>
</tr>

<%
' USE THE FDFNextFieldName METHOD TO COLLECT THE PDF's FORM FIELDS NAMES
' First field name is unknown so specify "". 
' Store the name to a variable and pass it to your next FDFNextFieldName method
' Use the Field Name variable and pass it to the FDFGetValue method
' In this case the first field is the Submit button which has no value and
' returns an error if passed to the FDFGetValue method
' This is another method of collecting the field values
' but you must check for an empty value as there is no Length or Count property
' of the FDFNextFieldName
'*************************************************************************
strFld = objFDF.FDFNextFieldName("") 'Collect the first field name by passing ""
%>

<tr>
<td>Field0's name is:</font></td>
<td> <% = strFld %> </td>
</tr>

<%
i = 1
Do Until i = 0
strFieldName = objFDF.FDFNextFieldName(strFld)
If strFieldName & "" = "" Then 'Check for an empty value, end iteration if empty
i = 0
Else
strFieldValue = objFDF.FDFGetValue(strFieldName)
%>

<tr>
<td>Field<% = i %>'s name is:</font></td>
<td> <% = strFieldName %> </td>
<td> <% = strFieldValue %> </td>
</tr>

<%
strFld = strFieldName
i = i + 1
End If
Loop
%> 

</table>

</body>
</html>

<%
' The object objFDF was initially set to equal all the FDF properties from 
' the buffer during the FDFAcX.FDFOpenFromBuf method. So, not only can we
' read all of this data (which we have just shown), but we can modify it,
' add to it, subtract from it, or whatever, and save this new data as an FDF
' file. This is a physical file that can be processed later on or even
' attached to an e-mail and sent to a client.
'******************************************************************

' Let's say we have a request form that has multiple levels in a workplace 
' environment. We can collect and identify a user via a session variable,
' and depending on what position the user holds in their company, we can choose
' to ignore or change form values that they have no business modifying. For
' instance, the originating user (the requestor) cannot recommend whether their
' request should be approved or not. So we would ignore or change these
' Recommendation blocks to the value of "Off."
' Let's also take into account that if a user does have the FINAL say
' in the matter but makes this determination based upon what their trusted
' subordinates have recommended. They would need to see what that actual 
' subordinate recommended, not what
' their immediate subordinate may have changed these values to
'(FDF cannot modify a field's read-only attribute, unfortunately.).

Select Case intUserLevel
    Case 0 '-- Requestor cannot make recommendations
        objFDF.FDFSetValue "radApprv1", "Off", False
        objFDF.FDFSetValue "radApprv2", "Off", False
        objFDF.FDFSetValue "radApprvFinal", "Off", False
        objFDF.FDFSetStatus "Requestor submitted this form, all recommendation blocks set to Off"
    Case 1 '-- Level 1, can only make Level 1 recommendations
        objFDF.FDFSetValue "radApprv2", "Off", False
        objFDF.FDFSetValue "radApprvFinal", "Off", False
        objFDF.FDFSetStatus "Level 1 submitted this form, only recommendation Level 1 was saved"
    Case 2 '-- Level 2, can only make Level 2 recommendations
        objFDF.FDFSetValue "radApprv1", "Yes", False ' Whatever was previously collected
 ' from Level 1 (We'll say 'Yes')
        objFDF.FDFSetValue "radApprvFinal", "Off", False
        objFDF.FDFSetStatus "Level 2 submitted this Form, only recommendation Level 2 was saved"
    Case 3 '-- Level 3, has final authority
        objFDF.FDFSetValue "radApprv1", "Yes", False 'Whatever was collected
        objFDF.FDFSetValue "radApprv2", "Yes", False 'from Level 2 (We'll say 'Yes')
        objFDF.FDFSetStatus "Level 3 submitted this form, only recommendation Level 3 was saved"
    Case Else
        'Normally we would place the FDFSavetoFile method in each case except this one.
End Select

'-- Save the collected data and any modified data to an FDF file.
objFDF.FDFSavetoFile "c:\Inetpub\wwwroot\PDF\FormTestFDF.fdf"

'-- CLOSE YOUR FDF OBJECT AND CLEAN UP --
objFDF.FDFClose
Set objFDF = Nothing
Set FDFAcX = Nothing
'-- The following causes the HTML to actually be sent to the client.
' Up to this point, no HTML has actually been downloaded to the client
' browser.
Response.End
%>

