Imports System.Drawing
Imports System
Imports System.IO
Namespace utCharting
    Public Class chartData
        ' this class is to store data for chart and convert that data to appropriate formate 
        ' that will require to draw chart                                                    
        Dim m_chartVals As Single()
        Dim m_chartNames As String()
        Dim m_percentval As Single()
        Dim m_startangle As Single()
        Dim m_span As Single()

        ' elements describes length of name and Vals array                                   
        Public ReadOnly elements As Integer

        ' we would create array of color class that can be used to provide colors to all pie 
        Public ReadOnly colorval() As Color = {System.Drawing.Color.FromArgb(0, 0, 255), _
                                    System.Drawing.Color.FromArgb(0, 255, 0), _
                                    System.Drawing.Color.FromArgb(255, 0, 0), _
                                    System.Drawing.Color.FromArgb(255, 255, 0), _
                                    System.Drawing.Color.FromArgb(255, 0, 255), _
                                    System.Drawing.Color.FromArgb(0, 255, 255), _
                                    System.Drawing.Color.FromArgb(0, 0, 0), _
                                    System.Drawing.Color.FromArgb(204, 204, 204), _
                                    System.Drawing.Color.FromArgb(153, 0, 102), _
                                    System.Drawing.Color.FromArgb(255, 255, 204)}

        Public Sub New(ByVal chartVals As Single(), ByVal chartNames As String())
            Dim arrsize As Integer = chartVals.Length

            Dim _percentVal(arrsize - 1) As Single
            Dim _startangle(arrsize - 1) As Single
            Dim _span(arrsize - 1) As Single
            Me.m_chartVals = chartVals
            Me.m_chartNames = chartNames
            Me.elements = arrsize

            Dim i As Integer
            Dim totalval As Single
            Dim total As Single = 0
            For i = 0 To arrsize - 1
                totalval = totalval + chartVals(i)
            Next
            ' start angle for Each pie and span of each pie is required to draw pie          
            For i = 0 To arrsize - 1
                _startangle(i) = total
                _span(i) = chartVals(i) * 360 / totalval
                _percentVal(i) = CDbl(CInt(chartVals(i) * 10000 / totalval)) / 100
                total = total + chartVals(i) * 360 / totalval
            Next
            m_percentval = _percentVal.Clone
            m_startangle = _startangle.Clone
            m_span = _span.Clone

        End Sub

        Public ReadOnly Property percentval() As Single()
            Get
                percentval = m_percentval
            End Get
        End Property
        Public ReadOnly Property startangle() As Single()
            Get
                startangle = m_startangle
            End Get
        End Property
        Public ReadOnly Property span() As Single()
            Get
                span = m_span
            End Get
        End Property
        Public ReadOnly Property chartVals() As Single()
            Get
                chartVals = m_chartVals

            End Get
        End Property
        Public ReadOnly Property chartNames() As String()
            Get
                chartNames = m_chartNames
            End Get
        End Property


    End Class

    Public Class utChart
        Dim memImg As Bitmap
        Dim mypen As Drawing.Pen
        Dim mybrush As Brush

        '************************************************************************************
        Public Function getpiechart(ByRef mychartdata As chartData, _
                                    ByVal piedia As Integer) As Stream
            '   function to get pie cart                                                     
            '********************************************************************************

            ' First We would create ractangle object that describes pieSize                  
            'Dim rect As Drawing.Rectangle = New Drawing.Rectangle(10, 10, piedia, piedia * 0.5)
            Dim rect As Drawing.Rectangle = New Drawing.Rectangle(50, 50, piedia, piedia * 0.5)
            ' This stream will send data Is required to send Data                            
            Dim mystream As Stream = New MemoryStream()
            ' for looping only                                                               
            Dim i As Integer
            ' font for text on chart & Brush object                                          
            Dim myfont As New Font("verdana", 8, FontStyle.Bold)
            mybrush = New SolidBrush(Color.Black)
            ' we would initilize size for our Image height as                                
            '  Piedia  + 10( ontop ) + 10( onbottom ) + 20 for copywrite display             
            Dim imgHeight As Integer = piedia * 0.5 + 80 + piedia / 10

            '--------------------------------------------------------------------------------
            ' get dimension of the image to be poduced                                       
            '--------------------------------------------------------------------------------

            ' we would check if no of components in our chart(length of arr of names) we     
            ' should adjust height of our image according to that, we are providing 15       
            ' pixcels for each Vals of chart object                                          
            If mychartdata.elements * 15 + 40 > imgHeight Then
                imgHeight = mychartdata.elements * 15 + 40
            End If
            ' we will generate graphics object to get width of string                        
            Dim gtemp As Graphics = Graphics.FromImage(New Bitmap(10, 10))
            ' we would get maximum width of Names and Values to get text drawing on chart and
            ' also to get width of Image                                                     
            Dim maxNamesWidth As Integer = 0
            Dim maxValsWidth As Integer = 0

            Dim imgwidth, tempNamesWidth, tempValsWidth As Integer
            imgwidth = 200
            For i = 0 To mychartdata.elements - 1
                tempValsWidth = gtemp.MeasureString(FormatCurrency(mychartdata.chartVals(i)), myfont).Width
                tempNamesWidth = gtemp.MeasureString(mychartdata.chartNames(i), myfont).Width

                If tempNamesWidth > maxNamesWidth Then
                    maxNamesWidth = tempNamesWidth
                End If
                If tempValsWidth > maxValsWidth Then
                    maxValsWidth = tempValsWidth
                End If
            Next
            ' image width                                                                    
            imgwidth = piedia + 20 + 10 + 10 + 60 + maxValsWidth + maxNamesWidth + 5
            imgwidth -= 80
            gtemp.Dispose()
            '--------------------------------------------------------------------------------


            ' we would create new bitmap Image and graphics objet on which we can draw chart 
            memImg = New Bitmap(imgwidth, imgHeight, _
                                            Drawing.Imaging.PixelFormat.Format24bppRgb)

            Dim g As Graphics = Graphics.FromImage(memImg)
            '  set background of image as white                                              
            g.Clear(Color.White)
            Dim j As Integer = piedia / 10
            Do While j > 0
                For i = 0 To mychartdata.elements - 1
                    g.FillPie(New SolidBrush(mychartdata.colorval(i)), New Rectangle(rect.X, rect.Y + j, rect.Width, rect.Height), mychartdata.startangle(i), mychartdata.span(i))
                Next
                j = j - 1
            Loop

            piedia -= 100

            For i = 0 To mychartdata.elements - 1
                ' Draws Pie For particular element                                           
                g.FillPie(New SolidBrush(mychartdata.colorval(i)), rect, _
                                            mychartdata.startangle(i), mychartdata.span(i))
                ' draws Names and Vals of chart                                              
                g.DrawString("(" & mychartdata.percentval(i) & "%)", myfont, _
                                         New SolidBrush(Color.Blue), _
                                        piedia + 15 + maxValsWidth + maxNamesWidth, _
                                         i * 15 + 10)
                g.DrawString(FormatCurrency(mychartdata.chartVals(i)), myfont, mybrush, _
                                        piedia + 15, i * 15 + 10)
                g.DrawString(mychartdata.chartNames(i), myfont, mybrush, _
                                        piedia + 15 + maxValsWidth, i * 15 + 10)
                '  Draws rectangle to show color matching on chart                           
                g.FillRectangle(New SolidBrush(mychartdata.colorval(i)), _
                                        New Rectangle(piedia, (i * 15) + 10, 10, 10))
                g.DrawRectangle(New Pen(Color.Black), _
                                        New Rectangle(piedia, (i * 15) + 10, 10, 10))
                ' and Finaly Copyright Information                                           
                'g.DrawString("Copyright © 2007 Rangat.com", _
                ' myfont, mybrush, 5, memImg.Height - 15)
            Next
            memImg.Save(mystream, System.Drawing.Imaging.ImageFormat.Gif)
            Return mystream
        End Function
        '------------------------------------------------------------------------------    

    End Class
End Namespace
