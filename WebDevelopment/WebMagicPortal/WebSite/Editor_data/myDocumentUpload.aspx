<%@ Page Language="C#" Debug="true"%>
<%@ Import Namespace="System.IO" %>
<%@ Import Namespace="System.Drawing.Imaging" %>
<%@ Register TagPrefix="ed" Namespace="OboutInc.Editor" Assembly="obout_Editor" %>

<script language="C#" runat="server">
void Page_Load(object sender, EventArgs e)
{
  string uploadingFolder = ResolveUrl("~/Editor/files/");

  if(Page.Request["folder"] != null)
  if(Page.Request["folder"].Length > 0)
    uploadingFolder = Page.Request["folder"]; 

  Page.Response.Write("<html>");
  Page.Response.Write("<head>");
  Page.Response.Write("<scr"+"ipt>");
  Page.Response.Write("var documentSaved = \""+Page.Request["notsaved"]+"\";");
  Page.Response.Write("var documentFileName = null;");
  Page.Response.Write("var documentFileTitle = null;");

  if(Page.Request.Files.Count > 0)
  {
    System.Web.HttpPostedFile PFile = Page.Request.Files[0];
    string ext = Path.GetExtension(PFile.FileName).ToLower();

    // check extension of uploaded file
    if(ext==".doc" || ext==".pdf")
    if(PFile.FileName.Trim().Length > 0 && PFile.ContentLength > 0)
    {
      string fName = Page.MapPath(uploadingFolder+Path.GetFileName(PFile.FileName));

      try
      {
         if(File.Exists(fName)) File.Delete(fName);
         PFile.SaveAs(fName);

         string title = Page.Request["title"];

         Page.Response.Write("documentSaved = \""+Page.Request["saved"]+"\";");
         Page.Response.Write("documentFileName = \""+uploadingFolder+Path.GetFileName(PFile.FileName)+"\";");
         Page.Response.Write("documentFileTitle = \""+title+"\";");
      }
      catch(Exception ev)
      {
         Page.Response.Write("documentSaved = \""+ev.Message.Replace("\n"," ").Replace("\r"," ").Replace("\\","\\\\").Replace("\"","\\\"")+"\\n\\nTurn to your System Administrator.\";");
      }
    }
  }

  Page.Response.Write("</scr"+"ipt>");
  Page.Response.Write("</head>");
  Page.Response.Write("<body>");
  Page.Response.Write("</body>");
  Page.Response.Write("</html>");
  Page.Response.End();
}
</script>
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title></title>
</head>
<body></body>
</html>