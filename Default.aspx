<%-- Author: James Archuleta --%>
<%-- Date:   10-27-08 --%>
<%-- Class:  PRG 409  --%>

<%@ Page Language="VB" AutoEventWireup="false" CodeFile="Default.aspx.vb" Inherits="_Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Mortgage Calculator by James Archuleta</title>
<link href="style/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="style/template.css" rel="stylesheet" type="text/css" />

    </head>
<body><form id="form1" runat="server">

<table width="100%" border="0" cellspacing="0" cellpadding="0">
  <tr>
    <td class="shadow_left">&nbsp;</td>
    <td class="header_column">
	<table width="100%" border="0" cellspacing="10" cellpadding="0">
      <tr>
        <td class="logo_area">Mortgage Calculator by James Archuleta </td>
      </tr>
    </table></td>
    <td class="shadow_right">&nbsp;</td>
  </tr>
  <tr>
    <td class="horizontal_column">&nbsp;</td>
    <td class="horizontal_center">
	<table width="100%" border="0" cellpadding="0" cellspacing="0" class="linkcontainer">
      <tr>
        <td><div class="navigation"><asp:LinkButton ID="LinkButtonPre" CssClass="main_link" runat="server">Pre-Made Loans</asp:LinkButton>  </div></td>
        <td><div class="navigation"><asp:LinkButton ID="LinkButtonCustom" CssClass="main_link" runat="server">Custom Loans</asp:LinkButton></div></td>
        <td><div class="navigation"><asp:LinkButton ID="LinkButtonExit" CssClass="main_link" runat="server">Exit</asp:LinkButton></div></td>
      </tr>
    </table></td>
    <td class="horizontal_column">&nbsp;</td>
  </tr>
  <tr>
    <td class="shadow_left">&nbsp;</td>
    <td class="below_header">  <asp:Panel ID="PanelPreMade" runat="server">
        <div class="Panels">
            <table width="100%">
                <tr>
                    <td colspan="2">
                        <h3 class="center">
                            Pre-Made Loans</h3>
                    </td>
                </tr>
                <tr>
                    <td>
                        Amount:
                    </td>
                    <td>
                        <asp:TextBox ID="TextBoxPreAmount" runat="server"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" 
                            ControlToValidate="TextBoxPreAmount" ErrorMessage="Required " 
                            ValidationGroup="Pre"></asp:RequiredFieldValidator>
                        
                        <asp:RangeValidator ID="RangeValidator1" runat="server" 
                            ControlToValidate="TextBoxPreAmount" 
                            ErrorMessage="Must be between 1 and 100,000,000" ValidationGroup="Pre" 
                            MaximumValue="100000000" MinimumValue="1" Type="Double"></asp:RangeValidator>
                    </td>
                </tr>
                <tr>
                    <td>
                        Details:
                    </td>
                    <td>
                        <asp:DropDownList ID="DropDownListPreDetails" runat="server">
                        </asp:DropDownList>
                        &nbsp;
                        </td>
                </tr>
                <tr>
                    <td>
                        &nbsp;
                    </td>
                    <td>
                        <asp:Button ID="ButtonPreList" runat="server" Text="Get Summary" 
                            ValidationGroup="Pre" />
                        <asp:Button ID="ButtonPreClear" runat="server" Text="Clear" />
                        <br />
                    </td>
                </tr>
            </table>
        </div>
    </asp:Panel>
 <div class="Summary">
        <table width="100%">
            <tr><td colspan="2"><h3 class="center">Summary</h3></td></tr>
            <tr><td><b>Monthly Payment:</b></td><td><b><asp:Label ID="LabelMonthlyPayment" runat="server"></asp:Label></b></td></tr>
            <tr><td>Amount(Principal)</td><td>
                    <asp:Label ID="LabelAmount" runat="server"></asp:Label>
                    </td></tr>
            <tr><td>Interest</td><td>
                    <asp:Label ID="LabelInterest" runat="server"></asp:Label>
                    </td></tr>
            <tr><td>Term</td><td>
                    <asp:Label ID="LabelTerm" runat="server"></asp:Label>
                    </td></tr>
            <tr><td colspan="2"><hr /></td></tr>
            <tr><td>Total Interest</td><td>
                    <asp:Label ID="LabelTotalInterest" runat="server"></asp:Label>
                    </td></tr>
            <tr><td>Total Principal</td><td>
                    <asp:Label ID="LabelTotalPrincipal" runat="server"></asp:Label>
                    </td></tr>
        </table>
    </div>
        <asp:Panel ID="PanelCustom" runat="server" Visible="False">
            <div class="Panels">
                <table width="100%">
                    <tr>
                        <td colspan="2">
                            <h3 class="center">
                            Custom Loans</h3>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        Amount:
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxCusAmount" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" 
                                ErrorMessage="Required" ControlToValidate="TextBoxCusAmount" 
                                ValidationGroup="Cust"></asp:RequiredFieldValidator>&nbsp;
                            <asp:RangeValidator ID="RangeValidator3" runat="server" 
                                ErrorMessage="Must be between 1 and 100,000,000" MaximumValue="100000000" 
                                MinimumValue="1" ControlToValidate="TextBoxCusAmount" 
                                ValidationGroup="Cust" Type="Double"></asp:RangeValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        Terms:
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxCustTerm" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator4" runat="server" 
                                ErrorMessage="Required" ControlToValidate="TextBoxCustTerm" 
                                ValidationGroup="Cust"></asp:RequiredFieldValidator>&nbsp;
                            <asp:RangeValidator ID="RangeValidator4" runat="server" 
                                ErrorMessage="Must be whole numbers between 1 and 100 years" MaximumValue="100" 
                                MinimumValue="1" ControlToValidate="TextBoxCustTerm" 
                                ValidationGroup="Cust" Type="Integer"></asp:RangeValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        Interest:
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxCustInterest" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" 
                                ErrorMessage="Required" ControlToValidate="TextBoxCustInterest" 
                                ValidationGroup="Cust"></asp:RequiredFieldValidator>&nbsp;
                            <asp:RangeValidator ID="RangeValidator5" runat="server" 
                                ErrorMessage="Must be between 1 and 100" MaximumValue="100" 
                                MinimumValue="1" ControlToValidate="TextBoxCustInterest" 
                                ValidationGroup="Cust" Type="Double"></asp:RangeValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        &nbsp;
                        </td>
                        <td>
                            <asp:Button ID="ButtonCustList" runat="server" Text="Get Summary" 
                                ValidationGroup="Cust" />
                            <asp:Button ID="ButtonClear" runat="server" Text="Clear" /><br />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel></td>
    <td class="shadow_right">&nbsp;</td>
  </tr>
  <tr>
    <td class="shadow_left">&nbsp;</td>
    <td class="main_content_box"><table width="100%" border="0" cellspacing="0" cellpadding="0">
      <tr>
        <td class="left_content"> 
            <div class="List">
            <asp:GridView ID="GridView1" runat="server" AllowPaging="True" 
                    CellPadding="4" 
                    EmptyDataText="Click &quot;Get Summary&quot; if not Visible" 
                    ForeColor="Black" PageSize="12" BackColor="#CCCCCC" BorderColor="#999999" 
                    BorderStyle="Solid" BorderWidth="3px" CellSpacing="2">
                    <FooterStyle BackColor="#CCCCCC" />
                    <RowStyle BackColor="White" />
                    <PagerStyle BackColor="#CCCCCC" ForeColor="Black" HorizontalAlign="Left" />
                    <SelectedRowStyle BackColor="#000099" Font-Bold="True" ForeColor="White" />
                    <HeaderStyle BackColor="Black" Font-Bold="True" ForeColor="White" />
                    <AlternatingRowStyle BackColor="#CCCCCC" />
                </asp:GridView>
            </div>
                        </td>
        <td class="body_content"><div class="Chart"><strong>Principal Vs Interest (Click "Get Summary" if not Visible)</strong> <br />
          <br />
          
        <br /><asp:Image ID="Image1" runat="server" ImageUrl="GetChart.aspx" />
    </div>
                        </td>
      </tr>
    </table></td>
    <td class="shadow_right">&nbsp;</td>
  </tr>
  <tr>
    <td class="shadow_left">&nbsp;</td>
    <td class="middle_spacer"><div class="bottom_content"></div></td>
    <td class="shadow_right">&nbsp;</td>
  </tr>
  <tr>
    <td class="shadow_left">&nbsp;</td>
    <td class="bottom_link_container"><p>&nbsp;</p>
    <p>All Right Reserved &copy; 2008 by James Archuleta<br />
      <a href= "http://www.jamesarchuleta.com">JamesArchuleta.com</a> 
    </p></td>
    <td class="shadow_right">&nbsp;</td>
  </tr>
</table>
    
    <div>
        <h1 class="center">
            &nbsp;</h1>
    </div>
 

    </form>
<script type="text/javascript">
var gaJsHost = (("https:" == document.location.protocol) ? "https://ssl." : "http://www.");
document.write(unescape("%3Cscript src='" + gaJsHost + "google-analytics.com/ga.js' type='text/javascript'%3E%3C/script%3E"));
</script>
<script type="text/javascript">
try{
var pageTracker = _gat._getTracker("UA-10089726-1");
pageTracker._trackPageview();
} catch(err) {}
</script>

</body>
</html>
