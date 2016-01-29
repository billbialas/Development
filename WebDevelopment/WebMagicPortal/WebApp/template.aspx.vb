imports System
imports System.Collections
imports System.Data.SqlClient
imports System.ComponentModel
imports System.Data
imports System.Drawing
imports System.Drawing.Color 
imports System.Web
imports System.Web.SessionState
imports System.Web.UI
imports System.Web.UI.WebControls
imports System.Web.UI.HtmlControls
imports System.xml
imports System.Configuration
imports System.text
imports System.net
Imports System.IO

namespace PageTemplate
    Public Class template
        Inherits PageTemplate

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles mybase.load

            If Not (Page.IsPostBack) Then

            End If
            pagesetup()

        End Sub
    
    public Sub btntestX(ByVal Source As System.Object, ByVal e As System.EventArgs) 
				     Dim uri As New Uri("http://www.salespider.com/classifieds_post_ad.php")
				Dim data As String = "title=michigan"
				Dim request As HttpWebRequest = HttpWebRequest.Create(uri)
				   	
				Dim boundary As String = System.Guid.NewGuid().ToString()
				
				request.ContentType = String.Format("multipart/form-data; boundary={0}", boundary)
				request.Method = "POST"
				
				' Build Contents for Post
				Dim header As String = String.Format("--{0}", boundary)
				Dim footer As String = header & "--"
				
				Dim contents As New StringBuilder()
				
				
				' Text
				contents.AppendLine(header)
				contents.AppendLine("Content-Disposition: form-data; name=""title""")
				contents.AppendLine()
				contents.AppendLine("Iowa")
				
				' Footer
				contents.AppendLine(footer)
				
				' This is sent to the Post
				Dim bytes As Byte() = System.Text.Encoding.GetEncoding(1251).GetBytes(contents.ToString())
				
				request.ContentLength = bytes.Length
				
				 request.GetRequestStream().Write(bytes, 0, bytes.Length)
				     'request.Close()
				    
						Dim response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
						Dim result As String = New StreamReader(response.GetResponseStream()).ReadToEnd()
						response.close
						
						Console.WriteLine(result)
						Console.ReadLine()
     
    end sub
        
        
        
        
        
        
        
        public Sub btntestC(ByVal Source As System.Object, ByVal e As System.EventArgs) 
        Dim uri As New Uri("http://www.salespider.com/classifieds_post_ad.php")
        Dim data As String = "title=michigan&email=b@yahoo.com&optone=Real Estate&geo=UnitedStates&desc=Well I am going to see how this works.  I hope it works like I planned!"
            Dim request As HttpWebRequest = HttpWebRequest.Create(uri)
            request.Method = WebRequestMethods.Http.Post
            request.ContentLength = data.Length
            request.ContentType = "application/x-www-form-urlencoded"
				request.AllowAutoRedirect = true
				request.KeepAlive = True
				request.CookieContainer = New CookieContainer()
            Dim writer As New StreamWriter(request.GetRequestStream)
            writer.Write(data)
            writer.Close()

            Dim oResponse As HttpWebResponse = request.GetResponse()
             Dim ccCookies As CookieCollection = oResponse.Cookies
            Dim reader As New StreamReader(oResponse.GetResponseStream())
            Dim tmp As String = reader.ReadToEnd()
            oResponse.Close()

            Response.Write(tmp)
       
       end sub

public Sub btntestA(ByVal Source As System.Object, ByVal e As System.EventArgs)     
    
    Dim appURL As String = "http://www.salespider.com/classifieds_post_ad.php"
    Dim strPostData As String = "title=michigan&email=DD@yahoo.com&optone=Real Estate&geo=Iowa&desc=Well I am going to see how this works."
    '[String].Format("txtUsr={0}&txtPwd={1}", "bbialas@gochoiceone.com", "s")
    
    ' Setup the http request.
    Dim wrWebRequest As HttpWebRequest = TryCast(WebRequest.Create(appURL), HttpWebRequest)
    wrWebRequest.Method = "POST"
    wrWebRequest.KeepAlive = True
		wrWebRequest.AllowAutoRedirect = False
    wrWebRequest.ContentLength = strPostData.Length
    wrWebRequest.ContentType = "application/x-www-form-urlencoded"
    wrWebRequest.CookieContainer = New CookieContainer()
    
    ' Post to the login form.
    Dim swRequestWriter As New StreamWriter(wrWebRequest.GetRequestStream())
    swRequestWriter.Write(strPostData)
    swRequestWriter.Close()
    
    ' Get the response.
    Dim hwrWebResponse As HttpWebResponse = DirectCast(wrWebRequest.GetResponse(), HttpWebResponse)
    
    ' Have some cookies.
    Dim ccCookies As CookieCollection = hwrWebResponse.Cookies
    
    ' Read the response
    Dim srResponseReader As New StreamReader(hwrWebResponse.GetResponseStream())
    Dim strResponseData As String = srResponseReader.ReadToEnd()
    srResponseReader.Close()
    hwrWebResponse.close()
    
    ' Display the response.
    Response.Write(strResponseData)
End Sub
        
        
                      
public Sub btntestb(ByVal Source As System.Object, ByVal e As System.EventArgs) 
		Dim request As HttpWebRequest = DirectCast(WebRequest.Create("https://post.craigslist.org/det/S/vgm/mcb/x/eumrF8C3fEaYqt6y/Dj4dd"), HttpWebRequest)
		request.Method = "POST"
		request.KeepAlive = True
		request.UserAgent = "Mozilla/5.0 (Windows; U; Windows NT 5.1; ru; rv:1.8.1.11) Gecko/20071127 Firefox/2.0.0.11"
		request.Accept = "text/xml,application/xml,application/xhtml+xml,text/html;q=0.9,text/plain;q=0.8,image/png,*/*;q=0.5"
		'request.AllowAutoRedirect = False
		'request.Connection = "keep-alive"
		request.Headers.Add("Accept-Language", "ru-ru,ru;q=0.8,en-us;q=0.5,en;q=0.3")
		request.Headers.Add("Accept-Encoding", "gzip,deflate")
		request.Headers.Add("Accept-Charset", "windows-1251,utf-8;q=0.7,*;q=0.7")
		request.Headers.Add("Keep-Alive", "300")
		request.Referer = "https://post.craigslist.org/det/S/vgm/mcb/x/eumrF8C3fEaYqt6y/Dj4dd"
		request.Headers.Add("Cookie", "cl_def_hp=stpetersburg; cl_def_lang=en")
		request.ContentType = "multipart/form-data"
		Dim ByteArr As Byte() = System.Text.Encoding.GetEncoding(1251).GetBytes("U2FsdGVkX18yNDcwMzI0.N2zEvfoQTkyEmtupmNAZHkDwtCgmYRY3RrlMtufyHgnrX_JBAm8zJ34=Thisisatestasdasd")
		request.ContentLength = ByteArr.Length
		request.GetRequestStream().Write(ByteArr, 0, ByteArr.Length)
		Dim response As HttpWebResponse = DirectCast(request.GetResponse(), HttpWebResponse)
		Dim result As String = New StreamReader(response.GetResponseStream()).ReadToEnd()
		response.close
		
		Console.WriteLine(result)
		Console.ReadLine()

end sub
        
		public Sub OnPostInfoClick(ByVal sender As Object, ByVal e As System.EventArgs)                                               																																																																																								
		    Dim strId As String = "bbialas@gochoiceone.com"                                                                                  																																																																																								
		    Dim strName As String = "s"                                                                                 																																																																																								
		                                                                                                                               																																																																																								
		    Dim encoding As New ASCIIEncoding()                                                                                      																																																																																								
		    'Dim postData As String =  "U2FsdGVkX18xNjU5MzE2Nem7iNUQJEVRPS0_3zjRsjGOnIJpuDjmYN6s2DNcGihvzlVfcRTRA9k=Thisisaest&FromEMail=bialasw@yahoocom&ConfirmEMail=bialasw@yahoocom&U2FsdGVkX:18xNjU5MzE2NTL:HLo8CLzLUPFWJ7ZV4tH4.s2ZwQ3KHaRz_UsYzRjYKN=test"
                           Dim postData As String ="U2FsdGVkX18yNDcwMzI0.N2zEvfoQTkyEmtupmNAZHkDwtCgmYRY3RrlMtufyHgnrX_JBAm8zJ34=Thisisatestasdasd&FromEMail=bialasw@yahoo.com&ConfirmEMail=bialasw@yahoo.com&U2FsdGVkX18yNDcwMzI0N5rqrRAU.RWerTZHt6mlk2-I:LIEFAVNV6Jo6jG_STu:.ypl=test"                                                            																																																																																								
		    'postData += ("&username=" & strName)                                                                                       																																																																																								
		    Dim data As Byte() = encoding.GetBytes(postData)                                                                           																																																																																								
		                                                                                                                               																																																																																								
		    ' Prepare web request...                                                                                                   																																																																																								
		    Dim myRequest As HttpWebRequest = DirectCast(WebRequest.Create("https://post.craigslist.org/det/S/vgm/mcb/x/eumrF8C3fEaYqt6y/Dj4dd"), HttpWebRequest)																																																																																								
		    myRequest.Method = "POST"                                                                                                  																																																																																								
		    myRequest.ContentType = "multipart/form-data"                                                                																																																																																								
		    myRequest.ContentLength = data.Length 
		    myRequest.Timeout = 30000                                                                                     																																																																																								
		    Dim newStream As Stream = myRequest.GetRequestStream()                                                                     																																																																																								
		    ' Send the data.                                                                                                           																																																																																								
		    newStream.Write(data, 0, data.Length)                                                                                      																																																																																								
		    newStream.Close()  
		    
		      Dim oResponse As HttpWebResponse = myRequest.GetResponse()
             Dim ccCookies As CookieCollection = oResponse.Cookies
            Dim reader As New StreamReader(oResponse.GetResponseStream())
            Dim tmp As String = reader.ReadToEnd()
            oResponse.Close()

            Response.Write(tmp)
       
		                                                                                                            																																																																																								
		End Sub                                                                                                                        																																																																																								


         Public Sub pagesetup()
 			'width will be calculated automatically, but it is sometimes
            layout.Width = System.Configuration.ConfigurationManager.AppSettings("ScreenWidth")
            leftNav.Width = System.Configuration.ConfigurationManager.AppSettings("ScreenLeftNav")
            body.Height = System.Configuration.ConfigurationManager.AppSettings("ScreenBodyHeight")
            body.Width = System.Configuration.ConfigurationManager.AppSettings("ScreenBodyWidth")
            layout.Border = System.Configuration.ConfigurationManager.AppSettings("ScreenBorder")
            footer.Controls.Add(LoadControl(System.Configuration.ConfigurationManager.AppSettings("ScreenFooter")))
            Header.Controls.Add(LoadControl(System.Configuration.ConfigurationManager.AppSettings("ScreenHeader")))
            leftNav.Controls.Add(LoadControl(System.Configuration.ConfigurationManager.AppSettings("ScreenLeftNav")))
 				MiddleNav.Controls.Add(LoadControl(System.Configuration.ConfigurationManager.AppSettings("ScreenmiddleNav")))
          
            body.VAlign = "top"
            leftNav.VAlign = "top"

            'LeftNav.Controls.Add(new LiteralControl("Some text."))
            'rightNav.VAlign = "top"           
            'adjust size of LeftNav (just for the heck of it)           

            'LeftNav.Controls.Add(LoadControl("navigation.ascx"));
            'LeftNav.Controls.Add(new LiteralControl("Some text."));

            'adjust size of LeftNav (just for the heck of it)
            'LeftNav.Width = "100";

            'RightNav.Controls.Add(LoadControl("quicklink1.ascx"))
            'MiddleNav.Controls.Add(LoadControl("userid.ascx"))

            

        End Sub

    End Class
end namespace