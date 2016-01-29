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

Imports System.Drawing.Imaging

Namespace PageTemplate

    Public Class adresults
        Inherits PageTemplate

        '   Variables declaration
        Private myImage As Bitmap
        Private g As Graphics
        Private p(4) As Integer
        Private towns() As String = {"A", "B", "C", "D"}
        Private myBrushes(4) As Brush

        Private Sub Page_Load(ByVal sender As Object, ByVal e As EventArgs) Handles MyBase.Load

            If Not (Page.IsPostBack) Then
                getdata()
                initialiseGraphics()
                btnPiechart_Click()
            End If
            pagesetup()

        End Sub

        Public Sub getdata()
            Dim x As Integer
            Dim a As Integer = Request.QueryString("total")
            Dim ads As String = ""
            For x = 1 To a
                towns(x - 1) = getdata2(Request.QueryString("ad" & x))
                p(x - 1) = getdata3(Request.QueryString("ad" & x))
            Next
            'response.write(ads)
        End Sub
        Public Function getdata2(ByVal id As String) As String

            Dim strUID As String = Session("userid")

            Dim strSql As String = "SELECT * from tbl_leadads where tbl_leadad_pk ='" & id & "'"

            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    Return Sqldr("ad_title")
                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try

        End Function
        Public Function getdata3(ByVal id As String) As String

            Dim strUID As String = Session("userid")

            Dim strSql As String = "SELECT * from tbl_leadads where tbl_leadad_pk ='" & id & "'"

            Dim sqlCmd As SqlCommand

            Dim myConnection As New SqlConnection(ConfigurationSettings.AppSettings("ConnectionString"))
            sqlCmd = New SqlCommand(strSql, myConnection)

            Try
                myConnection.Open()
                Dim Sqldr As SqlDataReader = sqlCmd.ExecuteReader
                If Sqldr.Read() Then
                    Return Sqldr("ad_totalLeadcount")
                End If
            Catch SQLexc As SqlException
                Response.Write("Error occured while Generating Data. Error is " & SQLexc.ToString())
            Finally
                myConnection.Close()
            End Try

        End Function

        '   Initialises the bitmap, graphics context and pens for drawing
        Private Sub initialiseGraphics()
            Try
                ' Create an in-memory bitmap where you will draw the image. 
                ' The Bitmap is 300 pixels wide and 200 pixels high.
                myImage = New Bitmap(1000, 350, PixelFormat.Format32bppRgb)

                ' Get the graphics context for the bitmap.
                g = Graphics.FromImage(myImage)

                '   Create the brushes for drawing
                createBrushes()
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        '   Method to create brushes of specific colours
        Private Sub createBrushes()
            Try
                Dim a As Integer = Request.QueryString("total")

                If a = 1 Then
                    myBrushes(0) = New SolidBrush(Color.Red)


                ElseIf a = 2 Then
                    myBrushes(0) = New SolidBrush(Color.Red)
                    myBrushes(1) = New SolidBrush(Color.Blue)

                ElseIf a = 3 Then
                    myBrushes(0) = New SolidBrush(Color.Red)
                    myBrushes(1) = New SolidBrush(Color.Blue)
                    myBrushes(2) = New SolidBrush(Color.Yellow)

                ElseIf a = 4 Then

                    myBrushes(0) = New SolidBrush(Color.Red)
                    myBrushes(1) = New SolidBrush(Color.Blue)
                    myBrushes(2) = New SolidBrush(Color.Yellow)
                    myBrushes(3) = New SolidBrush(Color.Green)
                End If
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        '   Draws the barchart
        Private Sub drawBarchart(ByVal g As Graphics)
            Try
                '   Variables declaration
                Dim i As Integer
                Dim xInterval As Integer = 100
                Dim width As Integer = 90
                Dim height As Integer
                Dim blackBrush As New SolidBrush(Color.Black)

                For i = 0 To p.Length - 1
                    height = (p(i) \ 10000)         '   divide by 10000 to adjust barchart to height of Bitmap

                    '   Draws the bar chart using specific colours
                    g.FillRectangle(myBrushes(i), xInterval * i + 50, 280 - height, width, height)

                    '   label the barcharts
                    g.DrawString(towns(i), New Font("Verdana", 12, FontStyle.Bold), Brushes.Black, xInterval * i + 50 + (width / 3), 280 - height - 25)

                    '   Draw the scale
                    g.DrawString(height, New Font("Verdana", 8, FontStyle.Bold), Brushes.Black, 0, 280 - height)

                    '   Draw the axes
                    g.DrawLine(Pens.Brown, 40, 10, 40, 290)         '   y-axis
                    g.DrawLine(Pens.Brown, 20, 280, 490, 280)       '   x-axis
                Next
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        '   Draws the piechart
        Private Sub drawPieChart(ByVal g As Graphics)
            Try
                '   Variables declaration
                Dim i As Integer
                Dim total As Integer
                Dim percentage As Double
                Dim angleSoFar As Double = 0.0
                Dim a As Integer = Request.QueryString("total")
                '   Caculates the total
                For i = 0 To a - 1          'p.Length - 1
                    total += p(i)
                Next
                'response.write(p.Length - 1)
                '   Draws the pie chart
                For i = 0 To a - 1 'p.Length - 1
                    percentage = p(i) / total * 360
                    '
                    g.FillPie(myBrushes(i), 25, 25, 250, 250, CInt(angleSoFar), CInt(percentage))
                    '
                    angleSoFar += percentage

                    '   Draws the lengend
                    g.FillRectangle(myBrushes(i), 350, 25 + (i * 50), 25, 25)

                    '   Label the towns
                    g.DrawString(towns(i), New Font("Verdana", 8, FontStyle.Bold), Brushes.Brown, 390, 25 + (i * 50) + 10)
                Next
            Catch ex As Exception
                Throw ex
            End Try
        End Sub

        Protected Sub btnPiechart_Click()
            Try
                ' Set the background color and rendering quality.
                g.Clear(Color.WhiteSmoke)

                '   draws the barchart
                drawPieChart(g)

                ' Render the image to the HTML output stream.
                myImage.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Jpeg)
            Catch ex As Exception
                Throw ex
            End Try
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
End Namespace