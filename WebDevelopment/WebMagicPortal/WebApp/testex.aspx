<% @Import Namespace="System.Net.Mail" %>
<script language="vb" runat="server">

Sub Main(Source as Object, E as EventArgs)

   ' Variables
   Dim Request As System.Net.HttpWebRequest
   Dim Response As System.Net.HttpWebResponse
   Dim MyCredentialCache As System.Net.CredentialCache
   Dim strPassword As String
   Dim strDomain As String
   Dim strUserName As String
   Dim strCalendarURI As String
   Dim strQuery As String
   Dim bytes() As Byte
   Dim RequestStream As System.IO.Stream
   Dim ResponseStream As System.IO.Stream
   Dim ResponseXmlDoc As System.Xml.XmlDocument
   Dim HrefNodes As System.Xml.XmlNodeList
   Dim SizeNodes As System.Xml.XmlNodeList
   Dim SubjectNodeList As System.Xml.XmlNodeList
   Dim LocationNodeList As System.Xml.XmlNodeList
   Dim StartTimeNodeList As System.Xml.XmlNodeList
   Dim EndTimeNodeList As System.Xml.XmlNodeList
   Dim BusyStatusNodeList As System.Xml.XmlNodeList
   Dim InstanceTypeNodeList As System.Xml.XmlNodeList

   Try
      ' Initialize variables.
      strUserName = "bbialas"
      strPassword = "Superman01"
      strDomain = "c1dom"
      strCalendarURI = "http://outlook.gochoiceone.com/exchange/bbialas/calendar/"


      ' Build the SQL query.
      strQuery = "<?xml version=""1.0""?>" & _
                 "<g:searchrequest xmlns:g=""DAV:"">" & _
                 "<g:sql>SELECT ""urn:schemas:calendar:location"", ""urn:schemas:httpmail:subject"", " & _
                 """urn:schemas:calendar:dtstart"", ""urn:schemas:calendar:dtend"", " & _
                 """urn:schemas:calendar:busystatus"", ""urn:schemas:calendar:instancetype"" " & _
                 "FROM Scope('SHALLOW TRAVERSAL OF """ & strCalendarURI & """') " & _
                 "WHERE NOT ""urn:schemas:calendar:instancetype"" = 1 " & _
                 "AND ""DAV:contentclass"" = 'urn:content-classes:appointment' " & _
                 "AND ""urn:schemas:calendar:dtstart"" > '2003/06/01 00:00:00' " & _
                 "ORDER BY ""urn:schemas:calendar:dtstart"" ASC" & _
                 "</g:sql></g:searchrequest>"

      ' Create a new CredentialCache object and fill it with the network
      ' credentials required to access the server.
      MyCredentialCache = New System.Net.CredentialCache
      MyCredentialCache.Add(New System.Uri(strCalendarURI), _
          "NTLM", _
          New System.Net.NetworkCredential(strUserName, strPassword, strDomain) _
          )

      ' Create the PUT HttpWebRequest object.
       Request = CType(System.Net.WebRequest.Create(strCalendarURI), _
                       System.Net.HttpWebRequest)

      ' Add the network credentials to the request.
      Request.Credentials = MyCredentialCache

      ' Specify the SEARCH method.
      Request.Method = "SEARCH"

      ' Encode the body using UTF-8.
      bytes = System.Text.Encoding.UTF8.GetBytes(strQuery)

      ' Set the content header length.  This must be
      ' done before writing data to the request stream.
      Request.ContentLength = bytes.Length

      ' Get a reference to the request stream.
      RequestStream = Request.GetRequestStream()

      ' Write the message body to the request stream.
      RequestStream.Write(bytes, 0, bytes.Length)

      ' Close the Stream object to release the connection
      ' for further use.
      RequestStream.Close()

      ' Set the Content Type header.
      Request.ContentType = "text/xml"

      ' Set the Translate header.
      Request.Headers.Add("Translate", "F")

      ' Send the SEARCH method request and get the
      ' response from the server.
      Response = CType(Request.GetResponse(), System.Net.HttpWebResponse)

      ' Get the XML response stream.
      ResponseStream = Response.GetResponseStream()

      ' Create the XmlDocument object from the XML response stream.
      ResponseXmlDoc = New System.Xml.XmlDocument
      ResponseXmlDoc.Load(ResponseStream)

      ' Build a list of the DAV:href XML nodes, corresponding to the folders
      ' in the mailbox.  The DAV: namespace is typically assgigned the a:
      ' prefix in the XML response body.
      HrefNodes = ResponseXmlDoc.GetElementsByTagName("a:href")

      ' Build a list of the urn:schemas:httpmail:subject XML nodes,
      ' corresponding to the calendar item subjects returned in the search request.
      ' The urn:schemas:httpmail: namespace is typically
      ' assigned the e: prefix in the XML response body.
      SubjectNodeList = ResponseXmlDoc.GetElementsByTagName("e:subject")

      ' Build a list of the urn:schemas:calendar:location XML nodes,
      ' corresponding to the calendar item locations returned in the search request.
      ' The urn:schemas:calendar: namespace is typically
      ' assigned the d: prefix in the XML response body.
      LocationNodeList = ResponseXmlDoc.GetElementsByTagName("d:location")

      ' Build a list of the urn:schemas:calendar:dtstart XML nodes,
      ' corresponding to the calendar item locations returned in the search request.
      StartTimeNodeList = ResponseXmlDoc.GetElementsByTagName("d:dtstart")

      ' Build a list of the urn:schemas:calendar:dtend XML nodes,
      ' corresponding to the calendar item locations returned in the search request.
      EndTimeNodeList = ResponseXmlDoc.GetElementsByTagName("d:dtend")

      ' Build a list of the urn:schemas:calendar:busystatus XML nodes,
      ' corresponding to the calendar item locations returned in the search request.
      BusyStatusNodeList = ResponseXmlDoc.GetElementsByTagName("d:busystatus")

      ' Build a list of the urn:schemas:calendar:instancetype XML nodes,
      ' corresponding to the calendar item locations returned in the search request.
      InstanceTypeNodeList = ResponseXmlDoc.GetElementsByTagName("d:instancetype")

      ' Loop through the returned items (if any).
      If SubjectNodeList.Count > 0 Then

         Console.WriteLine("Calendar items...")

         Dim i As Integer
         For i = 0 To SubjectNodeList.Count - 1

            ' Display the subject.
            Console.WriteLine("  Subject:       " + SubjectNodeList(i).InnerText)

            ' Display the location.
            Console.WriteLine("  Location:      " + LocationNodeList(i).InnerText)

            ' Display the start time.
            Console.WriteLine("  Start time:    " + StartTimeNodeList(i).InnerText)

            ' Display the end time.
            Console.WriteLine("  End time:      " + EndTimeNodeList(i).InnerText)

            ' Display the busy status.
            Console.WriteLine("  Busy status:   " + BusyStatusNodeList(i).InnerText)

            ' Display the instance type.
            If InstanceTypeNodeList(i).InnerText = "0" Then
                Console.WriteLine("  Instance type: 0-Single appointment")
            ElseIf InstanceTypeNodeList(i).InnerText = "1" Then
                Console.WriteLine("  Instance type: 1-Master recurring appointment")
            ElseIf InstanceTypeNodeList(i).InnerText = "2" Then
                Console.WriteLine("  Instance type: 2-Single instance, recurring appointment")
            ElseIf InstanceTypeNodeList(i).InnerText = "3" Then
                Console.WriteLine("  Instance type: 3-Exception to a recurring appointment")
            Else
                Console.WriteLine("  Instance type: Unknown")
                Console.WriteLine("")
            End If

         Next

      Else
        ' response.write("No calendar items found ...")

      End If
	Console.WriteLine("YEAH")
      ' Clean up.
      ResponseStream.Close()
      Response.Close()

   Catch ex As Exception

      ' Catch any exceptions. Any error codes from the
      ' SEARCH method requests on the server will be caught
      ' here, also.
      Console.WriteLine(ex.Message)

   End Try

End Sub

</script>
<HTML>
	<HEAD>
		<title>Choice One BMS</title>
	</HEAD>
	
<body >
	<form id="forms1a" runat="server">
		<table width=100%>
			<tr>
				<td valign=top width=35%><font size=3>Welcome  <asp:label id="Welcomemessage" runat="server"/></font>  </td>
			</tr>
			<tr height=5>
				<td>
									<td align=left ><asp:button id="l_tasks" runat=server text="Tasks" width="75" onclick="main" CausesValidation="False" /></td>
 			</tr>
		</table><br>
	</form>
	</body>	
</HTML>
