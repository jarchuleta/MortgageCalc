' Author: James Archuleta
' Date:   10-27-08
' Class:  PRG 409 

Imports System.Xml.Serialization

' This class is used to populate the drop down list. 
Public Class MortgageLoan


    ' Properties
    Private mYears As Double
    <XmlAttribute()> _
   Public Property Years() As Double
        Get
            Return mYears
        End Get
        Set(ByVal value As Double)
            mYears = value
        End Set
    End Property



    Private mInterest As Double
    <XmlAttribute()> _
    Public Property Interest() As Double
        Get
            Return mInterest
        End Get
        Set(ByVal value As Double)
            mInterest = value
        End Set
    End Property

    Public Sub New()
        mYears = 30
        mInterest = 5.6
    End Sub

    ' Constructor
    Public Sub New(ByVal years As Double, ByVal interest As Double)

        mYears = years
        mInterest = interest

    End Sub

    'Overrides
    Public Overrides Function ToString() As String
        Return String.Format("{0} years at {1:n}% ", mYears, mInterest)
    End Function






End Class
