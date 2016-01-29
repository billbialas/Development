<%@ Page Language="C#" Debug="true"%>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Drawing.Imaging" %>
<%@ Import Namespace="System.Web.SessionState" %>
<%@ Import Namespace="System.Web.UI" %>
<%@ Import Namespace="System.Collections" %>
<%@ Import Namespace="System.Data" %>
<%@ Import Namespace="System.Web.UI.WebControls" %>
<%@ Import Namespace="System.Configuration" %>
<%@ Import Namespace="System" %>
<%@ Import Namespace="System.Text" %>

<script language="C#" runat="server">
void Page_Load(object sender, EventArgs e)
{
        string imageFolder = "uimg/" + "bbialas@gochoiceoneIMG" ;
        HttpCookie cookie = Request.Cookies["bb"];

        //+ Convert.ToString(HttpContext.Current.Session["uimgdir"]);

        int maxWidth = 100;
        int maxHeight= 100;

        bool ssl = (Page.Request.ServerVariables["HTTPS"]=="on");
        string protocol    = Page.Request.ServerVariables["SERVER_PROTOCOL"].Split('/')[0].ToLower()+(ssl?"s":"");
        string port        = ":"+Page.Request.ServerVariables["SERVER_PORT"];

        if(port==":80" || port==":443") port ="";

        string curPath= protocol+"://"+Page.Request.ServerVariables["SERVER_NAME"]+port+Path.GetDirectoryName(Page.Request.ServerVariables["SCRIPT_NAME"]).Replace("\\","/");
        string folder = System.Web.HttpContext.Current.Server.MapPath(imageFolder);
		 
		 // string folder = left(folderA,56);
			//System.Web.HttpContext.Current.Response.Write(folder);
        if(Page.Request["imgtitle"] != null)
        if(Page.Request["imgtitle"].Length > 0)
        {
          string imageFile = System.Web.HttpContext.Current.Server.MapPath(Page.Request["imgtitle"].Replace("%20"," "));
          string title = "";

          if(File.Exists(imageFile+".description"))
          {
            StreamReader sr   = new StreamReader(imageFile+".description");
            title = sr.ReadLine();
            sr.Close();
          }

          Page.Response.Write("<html><body style='font-size:10px;margin:0px;padding:0px;padding-left:4px;'>");
          Page.Response.Write(title);
          Page.Response.Write("</body></html>");
          Page.Response.End();

          return;
        }

        if(Page.Request["imgprop"] != null)
        if(Page.Request["imgprop"].Length > 0)
        {
          string imageFile = System.Web.HttpContext.Current.Server.MapPath(Page.Request["imgprop"].Replace("%20"," "));

          if(File.Exists(imageFile))
          {
            FileStream binStream = File.OpenRead(imageFile);
            System.Drawing.Image objImage = System.Drawing.Image.FromStream(binStream);
            Page.Response.Write("<html><body style='font-size:10px;margin:0px;padding:0px;padding-left:4px;'>");
            Page.Response.Write("<center><span style='color:blue;'>"+Path.GetExtension(imageFile).ToLower().Replace(".","")+"</span>");
            Page.Response.Write(" w: <b>"+objImage.Width.ToString()+"</b> h: <b>"+objImage.Height.ToString()+"</b>");
            Page.Response.Write("</center></body></html>");
            objImage.Dispose();
            binStream.Close();
            Page.Response.End();
          }

          return;
        }

        if(Page.Request["imgsrc"] != null)
        if(Page.Request["imgsrc"].Length > 0)
        {
          string imageFile = System.Web.HttpContext.Current.Server.MapPath(Page.Request["imgsrc"].Replace("%20"," "));

          if(File.Exists(imageFile))
          {
            MemoryStream         objStream;
            MemoryStream         objStreamN;
            System.Drawing.Image objImage; 
            System.Drawing.Image View;     

            FileStream binStream = File.OpenRead(imageFile);
            byte[] buf = new byte[binStream.Length];

            System.Drawing.Image.GetThumbnailImageAbort myCallback = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);

            binStream.Read(buf, 0, (int)binStream.Length);
            binStream.Close();

            objStream = new MemoryStream(buf);
            objStream.Position = 0;
            objImage = System.Drawing.Image.FromStream(objStream);

            int mWidth  = objImage.Width;
            int mHeight = objImage.Height;

            if(mWidth > maxWidth)
            {
                mHeight = (int)((double)mHeight * ((double)maxWidth / (double)mWidth));
                mWidth  = maxWidth;
            }
            if(mHeight > maxHeight)
            {
                mWidth  = (int)((double)mWidth * ((double)maxHeight / (double)mHeight));
                mHeight = maxHeight;
            }

            if(mHeight == 0) mHeight= 5;
            if(mWidth  == 0) mWidth = 5;

            View = objImage.GetThumbnailImage(mWidth, mHeight, myCallback, IntPtr.Zero);
            objStream.Close();
            objStreamN = new MemoryStream();
            objStreamN.Position = 0;
            View.Save(objStreamN, objImage.RawFormat);
            View.Dispose();
            objImage.Dispose();
            objStreamN.Position = 0;
            buf = new byte[(int)objStreamN.Length];
            objStreamN.Read(buf, 0, (int)objStreamN.Length);
            objStreamN.Close();
            Page.Response.ContentType = "image/gif";
            Page.Response.BinaryWrite(buf);
            Page.Response.End();
          }
          return;
        }

        StringBuilder result = new StringBuilder();

        result.AppendLine("<?xml version=\"1.0\" ?>");
        result.AppendLine("<gallery xmlns=\"obout:editor:external-images-gallery-schema\" name=\""+Page.Request.ServerVariables["SERVER_NAME"]+port+"\" prefix=\""+curPath.Replace("\"","%22")+"\">");

        if(Directory.Exists(folder))
        {
          result.AppendLine("<folder name=\"Images Root\" path=\""+imageFolder+"\">");
          directoryDive(result, folder);
          result.AppendLine("</folder>");
        }

        result.AppendLine("</gallery>");
        Page.Response.ContentType = "text/xml";
        Page.Response.Write(result.ToString());
        Page.Response.End();
}

void directoryDive(StringBuilder result, string folder)
{
  string[] entires = Directory.GetFileSystemEntries (folder);

  foreach (string entire in entires) 
  {
     FileAttributes attr = File.GetAttributes(entire);
     string         name = Path.GetFileName(entire);
     string         pName= Path.GetFileNameWithoutExtension(entire);
     string         title= "";

     if((attr & FileAttributes.Directory) == FileAttributes.Directory)
     {
       result.AppendLine("<folder name=\""+pName.Replace("\"","%22")+"\" path=\""+name.Replace("\"","%22")+"\">");
       directoryDive(result, folder+"/"+name);
       result.AppendLine("</folder>");
     }
     else
     {
       string ext = Path.GetExtension(entire).ToLower();
       if(ext==".gif" || ext==".jpg" || ext==".jpeg" || ext==".png")
       {
         System.Drawing.Image objImage; 
         MemoryStream         objStream;

         FileStream binStream = File.OpenRead(entire);
         byte[] buf = new byte[binStream.Length];

         System.Drawing.Image.GetThumbnailImageAbort myCallback = new System.Drawing.Image.GetThumbnailImageAbort(ThumbnailCallback);

         binStream.Read(buf, 0, (int)binStream.Length);
         binStream.Close();

         objStream = new MemoryStream(buf);
         objStream.Position = 0;
         try
         {
           objImage = System.Drawing.Image.FromStream(objStream);

           int mWidth  = objImage.Width;
           int mHeight = objImage.Height;

           objImage.Dispose();

           if(File.Exists(entire+".description"))
           {
             StreamReader sr   = new StreamReader(entire+".description");
             title = sr.ReadLine();
             sr.Close();
           }
           result.Append("<file name=\""+pName.Replace("\"","%22")+"\"");
           result.Append(" path=\""+name.Replace("\"","%22")+"\"");
           result.Append(" title=\""+title.Replace("\"","%22")+"\"");
           result.Append(" width=\""+mWidth+"\"");
           result.Append(" height=\""+mHeight+"\"");
           result.AppendLine(" />");
         }
         catch{}
         objStream.Close();
       }
     }
  }
}

private bool ThumbnailCallback()
{
   return false;
}
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title></title>
</head>
<body></body>
</html>
