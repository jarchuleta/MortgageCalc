Imports Microsoft.VisualBasic

Public Class MortgageListItem
    '#  	Payment  	Interest  	Principal  	Remaining bal.


    Private mNumber As String
    Public Property Number() As String
        Get
            Return mNumber
        End Get
        Set(ByVal value As String)
            mNumber = value
        End Set
    End Property


    Private mPayment As String
    Public Property Payment() As String
        Get
            Return mPayment
        End Get
        Set(ByVal value As String)
            mPayment = value
        End Set
    End Property


    Private mInterest As String
    Public Property Interest() As String
        Get
            Return mInterest
        End Get
        Set(ByVal value As String)
            mInterest = value
        End Set
    End Property


    Private mPrincipal As String
    Public Property Principal() As String
        Get
            Return mPrincipal
        End Get
        Set(ByVal value As String)
            mPrincipal = value
        End Set
    End Property



End Class
