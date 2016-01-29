<%@ Page aspcompat=true %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN" >

<script language="vb" runat="server">
Sub Page_Load(byval sender as object, byval e As EventArgs) 
   

Dim FDFAcX         ' FDF Toolkit ActiveX Version Object
Dim objFDF           ' FDF Object

 FDFAcX = Server.CreateObject("FDFApp.FDFApp")

 objFDF = FDFAcX.FDFCreate
'objFDF.FDFSetFile ("http://bmsdev.gochoiceone.com/FormTestFDF.pdf")
'objFDF.FDFSetFile ("http://bmsdev.gochoiceone.com/tt.pdf")
objFDF.FDFSetFile ("http://bmsdev.gochoiceone.com/billapp.pdf")

objFDF.FDFSetValue("propaddress", "1234 Big Road", False)
'objFDF.FDFSetValue("txtLeadName", strLeadName, False)
'objFDF.FDFSetValue("txtLeadHPhone", strLeadHPhone, False)
'objFDF.FDFSetStatus ("You must complete all sections of this form.")
Response.ContentType = "application/vnd.fdf"
Response.BinaryWrite(objFDF.FDFSaveToBuf)
objFDF.FDFClose
 objFDF = Nothing
 FDFAcX = Nothing

end sub
</script>

<HTML>
	<HEAD>
		<title>Choice One Realty- Michigan</title>
	</HEAD>
	
	<body MS_POSITIONING="FlowLayout">
		<form  name="pdftest"  runat="server" >
	
  		
		</form>
	</body>
</HTML>
	