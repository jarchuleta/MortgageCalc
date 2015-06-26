Imports utCharting

Partial Class GetChart
    Inherits System.Web.UI.Page

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ' collect request data from query string or form posting                              
        'Dim strHeading As String = Request.QueryString("arrHead") & _
        '   Request.Form("arrHead")
        'Dim strValues As String = Request.QueryString("arrval") & _
        'Request.Form("arrval")
        'Dim chartdia As Integer = CInt(Request.QueryString("chartDia")) & _
        'Request.Form("arrval")
        Dim strHeading As String = "Total Principal-Total Interest"
        Dim strValues As String = Session("Values")
        Dim chartdia As Integer = 300
        ' create array from posted data                                                      
        strHeading = strHeading.Trim("(", ")")
        strValues = strValues.Trim("(", ")")
        Dim myheadarr() As String = strHeading.Split("-")
        Dim mystrval() As String = strValues.Split("-")

        Dim myintarr(mystrval.Length - 1) As Single
        ' convert array of values to single from string formate                              


        Dim i As Integer
        Try
            For i = 0 To mystrval.Length - 1
                myintarr(i) = CDbl(mystrval(i))
            Next
        Catch ex As Exception
            Response.ContentType = "image/gif"
            'response.binarywrite(mystream)
            'myimg.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif)
            Dim memImg As System.Drawing.Bitmap
            memImg = New System.Drawing.Bitmap(10, 10, _
                                            Drawing.Imaging.PixelFormat.Format24bppRgb)

            Dim g As System.Drawing.Graphics = System.Drawing.Graphics.FromImage(memImg)
            g.Clear(System.Drawing.Color.White)
            memImg.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif)
            Return

        End Try


        ' create instance of utChart object                                                  
        Dim mychart As New utCharting.utChart()
        ' create instance of chartdata object                                                
        Dim mychartdata As New chartData(myintarr, myheadarr)
        ' get piechart file as Stream                                                        
        Dim mystream As System.IO.Stream = mychart.getpiechart(mychartdata, chartdia)
        Dim myimg As New System.Drawing.Bitmap(mystream)
        ' set content type as "image/gif" and set outputstream to browser                    
        Response.ContentType = "image/gif"
        'response.binarywrite(mystream)
        myimg.Save(Response.OutputStream, System.Drawing.Imaging.ImageFormat.Gif)
    End Sub
End Class
