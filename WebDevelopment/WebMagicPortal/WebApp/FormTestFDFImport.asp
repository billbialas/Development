<%@ LANGUAGE = VBScript%>
<%
' SET UP YOUR PAGE
'***********************************************************************

Option Explicit

'  DECLARE ALL THE VARIABLES
'***********************************************************************

Dim strFirstName   ' First Name value for the txtFirstName field
Dim strProgExp     ' Short description of programming experience
Dim strVBS         ' Title for Program Language(s) most familiar w/-VBScript
Dim strC           ' Title for Program Language(s) most familiar w/-C++
Dim strJava        ' Title for Program Language(s) most familiar w/-Java
Dim strGender      ' Gender of individual
Dim strFavSite      ' Favorite Web site
Dim arrColors       ' An array of HTML colors
Dim intFavColor      ' Index of HTML colors array - Favorite
Dim i                ' Counter used to iterate through arrColors
Dim FDFAcX         ' FDF Toolkit ActiveX Version Object
Dim objFDF           ' FDF Object

' SET UP YOUR CONNECTION OBJECT HERE 
'************************************************************************

' SET UP YOUR RECORDSETS OR COMMAND OBJECTS HERE 
'************************************************************************

' SET THE VARIABLES BASED ON DATABASE RESULTS 
' (Here, I am setting them with literals as there is no actual database.)
'****************************************************************************

strFirstName = "William"
strProgExp = "I am the Master!"
strProgExp = strProgExp & " (Combination commonly referred to as DHTML)"
strVBS = "Yes"
strC = "No"
strJava = "Yes"
strGender = "Male"
strFavSite = "www.gochoiceone.com"
arrColors = Array("FFFF00", "000080", "DF0029", "666666", "009F62")
intFavColor = 1

' CLOSE & CLEAN UP YOUR RS, COMMAND OBJECTS, & CONNECTION OBJECT HERE 
'**************************************************************************

' END DATA COLLECTION, CREATE THE FDF OBJECT HERE
'********************************************************** 
Set FDFAcX = Server.CreateObject("FDFApp.FDFApp")

Set objFDF = FDFAcX.FDFCreate

' SET THE FULL ABSOLUTE URL OF YOUR PDF FILE
'*****************************************************
objFDF.FDFSetFile "http://bmsdev.gochoiceone.com/FormTestFDF.pdf"

' USE THE FDFSetValue METHOD TO POPULATE THE PDF's FORM FIELDS 
'*********************************************************************
' PDF form field name, Value you want entered, a value used to comply
' with Adobe Acrobat version 3.0 and below.
objFDF.FDFSetValue ("txtFirstName", strFirstName, False)
objFDF.FDFSetValue ("txtExperience", strProgExp, False)


' Loop through the arrColors array to populate the selColor drop down 
' FDFSetOpt "Field Name", Option Index (Begins at 0), Export Value,
' Option Item Name
' Option Item Name is Null or Empty if there is no
' Export Value(Item Name will then equal the Export Value)
' Setting FDFSetOpt to "selColor", i, "#" & arrColors(i), "#" & arrColors(i)
' has exactly the same affect as below in the actual code. But,
' setting FDFSetOpt to "SelColor", i, i, "#" & arrColors(i) will
' appear normally, however
' the numeric value of i will be sent to the server when submitted.
For i = 0 to UBound(arrColors)
objFDF.FDFSetOpt "selColor", i, "#" & arrColors(i), ""
Next

' Set i to the indexed Favorite Color
i = intFavColor

' Select the Favorite color from the selColor drop down just populated above
objFDF.FDFSetValue "selColor", "#" & arrColors(i), False

' USE THE FDFSetStatus METHOD TO DISPLAY AN ALERT BOX 
' (recommend that you don't, but it's cool that you can)
objFDF.FDFSetStatus "You must complete all sections of this form."

' WRITE THE ASSOCIATED VALUES INTO THE BUFFER STREAM AS AN FDF
Response.ContentType = "application/vnd.fdf"
Response.BinaryWrite objFDF.FDFSaveToBuf

' CLOSE YOUR FDF OBJECT AND CLEAN UP
objFDF.FDFClose
Set objFDF = Nothing
Set FDFAcX = Nothing

Response.End
%>
