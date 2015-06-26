' Author: James Archuleta
' Date:   10-27-08
' Class:  PRG 409 

Imports System.IO
Imports System.Xml.Serialization
Imports System.Xml

Partial Class _Default
    Inherits System.Web.UI.Page

    Dim mTerms(2) As MortgageLoan
    Dim MonthlyPayment As Double
    Dim term As Double
    Dim loanPrincipal As Double
    Dim Interest As Double

    Shared list As List(Of MortgageListItem)
    Shared Ran As Boolean

    ' Startup
    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        ' Read the details from a file
        If fromXml() = False Then
            mTerms(0) = New MortgageLoan(7, 5.35)
            mTerms(1) = New MortgageLoan(15, 5.5)
            mTerms(2) = New MortgageLoan(30, 5.75)
        End If

        ' if this is the first time ran, do some stuff
        If DropDownListPreDetails.Items.Count <> mTerms.Length Then
            DropDownListPreDetails.DataSource = mTerms
            DropDownListPreDetails.DataBind()
            Session("Values") = ""
            Ran = True
        End If

        ' this is used to generate the amortized list on post-back events 
        If Not list Is Nothing Then
            GridView1.DataSource = list
            GridView1.DataBind()
        End If


    End Sub

    ' used to get monthly payment
    Public Function GetMonthlyPayment(ByVal Amount As Double, ByVal MonthRate As Double, ByVal Term As Double) As Double

        Dim NumPayment As Double

        ' setup the numbers
        MonthRate = MonthRate / 100 / 12
        NumPayment = Term * 12

        'crunch the numbers
        Return Amount * MonthRate / (1 - Math.Pow((1 + MonthRate), -NumPayment))

    End Function


    'Read from an XML file
    Private Function fromXml() As Boolean

        Dim Stream As FileStream
        Try
            Stream = New FileStream("Loans.xml", FileMode.Open)
        Catch ex As Exception
            Return False
        End Try

        ' create a serializer...
        Dim serializer As New XmlSerializer(mTerms.GetType)

        ' save the file...
        Try
            mTerms = serializer.Deserialize(Stream)
        Catch ex As Exception
        End Try

        ' close the file...
        Stream.Close()
        Return True
    End Function
    ' used to generate the list data
    Protected Sub BindGridControl()

        ' used to add rows
        list = New List(Of MortgageListItem)

        'Calculation Variables
        Dim monthlyPrinciple As Double
        Dim monthlyInterest As Double
        Dim newLoanAmount As Double
        Dim tempLoanAmount As Double
        Dim totalInterest As Double



        ' Assign values 
        term = term * 12
        totalInterest = 0

        'Loop to calculate monthly information
        For month As Integer = 1 To term

            Dim item As New MortgageListItem

            ' Calculate monthly interest, monthly principle, and balance
            monthlyInterest = loanPrincipal * (Interest / (12 * 100))
            monthlyPrinciple = MonthlyPayment - monthlyInterest
            newLoanAmount = loanPrincipal - monthlyPrinciple
            totalInterest += monthlyInterest

            item.Number = month
            item.Payment = FormatCurrency(MonthlyPayment)
            item.Principal = FormatCurrency(newLoanAmount)
            item.Interest = FormatCurrency(monthlyInterest)
            list.Add(item)


            ' Set / reset the variable values
            tempLoanAmount = newLoanAmount
            loanPrincipal = tempLoanAmount




        Next


        ' Store values in Summary
        Dim Total As String = FormatCurrency(MonthlyPayment * term)
        LabelTotalPrincipal.Text = Total
        LabelTotalInterest.Text = FormatCurrency(totalInterest)

        'stote values in a session variable for the chart
        Session("Values") = Total & "-" & FormatCurrency(totalInterest)
        GridView1.DataSource = list

    End Sub
    'handler for the button listers
    Protected Sub ButtonList_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonPreList.Click, ButtonCustList.Click

        Dim btn As Button = sender

        ' Get monthly Payment
        If btn.ID Is "ButtonPreList" Then
            ' get/save the figures
            loanPrincipal = CDbl(TextBoxPreAmount.Text)
            Dim mort As MortgageLoan = mTerms(DropDownListPreDetails.SelectedIndex)
            Interest = mort.Interest
            term = mort.Years

            ' calculate
            MonthlyPayment = GetMonthlyPayment(loanPrincipal, Interest, term)
            LabelMonthlyPayment.Text = FormatCurrency(MonthlyPayment)
        Else
            ' get/save the figures
            loanPrincipal = CDbl(TextBoxCusAmount.Text)
            Interest = CDbl(TextBoxCustInterest.Text)
            term = CDbl(TextBoxCustTerm.Text)

            ' calculate
            MonthlyPayment = GetMonthlyPayment(loanPrincipal, Interest, term)
            LabelMonthlyPayment.Text = FormatCurrency(MonthlyPayment)

        End If

        LabelAmount.Text = FormatCurrency(loanPrincipal)
        LabelInterest.Text = String.Format("{0:n}%", Interest)
        LabelTerm.Text = term.ToString()

        ' rebind the list
        BindGridControl()
        GridView1.DataBind()

    End Sub

    ' used to allow the next pages to work
    Protected Sub GridView1_PageIndexChanging(ByVal sender As Object, ByVal e As System.Web.UI.WebControls.GridViewPageEventArgs) Handles GridView1.PageIndexChanging

        'BindGridControl()
        GridView1.PageIndex = e.NewPageIndex
        GridView1.DataBind()
    End Sub

    Protected Sub LinkButtonPre_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButtonPre.Click
        PanelPreMade.Visible = True
        PanelCustom.Visible = False
        Clear()
    End Sub

    Protected Sub LinkButtonCustom_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButtonCustom.Click
        PanelPreMade.Visible = False
        PanelCustom.Visible = True
        Clear()
    End Sub

    'Clear
    Protected Sub Clear()
        ' Clear Pre-Made stuff
        TextBoxPreAmount.Text = ""
        DropDownListPreDetails.SelectedIndex = -1

        'Clear Custom Stuff
        TextBoxCusAmount.Text = ""
        TextBoxCustInterest.Text = ""
        TextBoxCustTerm.Text = ""

        'Clear Summary
        LabelMonthlyPayment.Text = ""
        LabelAmount.Text = ""
        LabelInterest.Text = ""
        LabelTerm.Text = ""
        LabelTotalInterest.Text = ""
        LabelTotalPrincipal.Text = ""

        ' Clear Chart
        Session("Values") = ""

        ' Clear List
        If Not list Is Nothing Then
            list.Clear()
            GridView1.DataBind()
        End If



    End Sub

    Protected Sub ButtonPreClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonPreClear.Click
        Clear()
    End Sub

    Protected Sub ButtonClear_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles ButtonClear.Click
        Clear()
    End Sub

    Protected Sub LinkButtonExit_Click(ByVal sender As Object, ByVal e As System.EventArgs) Handles LinkButtonExit.Click
        Clear()
        Response.Redirect("ThankYou.aspx")
    End Sub
End Class
