<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OturumAc.ascx.cs" Inherits="EnrollKurumsal.AdminGiris.Kontroller.OturumAc" %>
<link href="Theme/Styles/AdminGiris.css" rel="stylesheet" type="text/css" />
<script src="../../App_Themes/PendikMainTheme/Scripts/jquery.min.js" type="text/javascript"></script>
<script type="text/javascript">
    $(document).ready(function() {
        $(window).resize(function() {
            $('.container').css({
                position: 'absolute',
                left: ($(window).width() - $('.container').outerWidth()) / 2,
                top: ($(window).height() - $('.container').outerHeight()) / 2
            });
        });
        $(window).resize();

    });
</script>
<div class="container">
    <asp:MultiView ID="MultiView1" runat="server">
        <asp:View ID="View1" runat="server">
            <div class="panel">
                <table style="width: 285px; color: #133E6C; font-weight: bold;">
                    <tr>
                        <td style="width: 75px;">
                            E-Posta
                        </td>
                        <td style="width: 10px;">
                            :
                        </td>
                        <td style="width: 175px;">
                            <asp:TextBox ID="TextBoxEPosta" runat="server" Width="175px"></asp:TextBox>
                        </td>
                        <td style="width: 25px;">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="TextBoxEPosta"
                                                        ValidationGroup="g1" ErrorMessage='&lt;img alt="E-posta adresinizi giriniz!" src="../AdminGiris/Theme/Images/uyari.png" /&gt;'></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Parola
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxParola" runat="server" TextMode="Password" Width="175px"></asp:TextBox>
                        </td>
                        <td>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="TextBoxParola"
                                                        ErrorMessage='&lt;img alt="Parolanızı giriniz!" src="../AdminGiris/Theme/Images/uyari.png" /&gt;'
                                                        ValidationGroup="g1"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            <asp:CheckBox ID="CheckBoxBeniHatirla" runat="server" Text="Beni Hatırla?" />
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="height: 5px;">
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td style="text-align: left;">
                                        <asp:ImageButton ID="ImageButtonGiris" runat="server" ImageUrl="../Theme/Images/login.png"
                                                         OnClick="ImageButtonGiris_Click" ValidationGroup="g1" />
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:ImageButton ID="ImageButtonParolamiUnuttum" runat="server" ImageUrl="../Theme/Images/remind.png"
                                                         OnClick="ImageButtonParolamiUnuttum_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="LabelMesaj" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
                <a href="http://www.enroll.com.tr/" target="_blank">
                    <div class="enroll">
                    </div>
                </a>
            </div>
        </asp:View>
        <asp:View ID="View2" runat="server">
            <div class="panel">
                <table style="width: 285px; color: #133E6C; font-weight: bold;">
                    <tr>
                        <td style="width: 75px;">
                            E-Posta
                        </td>
                        <td style="width: 10px;">
                            :
                        </td>
                        <td style="width: 175px;">
                            <asp:TextBox ID="TextBoxEPosta1" runat="server" Width="175"></asp:TextBox>
                        </td>
                        <td style="width: 25px;">
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="TextBoxEPosta1"
                                                        ErrorMessage='&lt;img alt="E-posta adresinizi giriniz!" src="../AdminGiris/Theme/Images/uyari.png" /&gt;'
                                                        ValidationGroup="g2"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td>
                        </td>
                        <td>
                            <table>
                                <tr>
                                    <td style="text-align: left;">
                                        <asp:ImageButton ID="ImageButtonGonder2" runat="server" ValidationGroup="g2" ImageUrl="../Theme/Images/submit.png"
                                                         OnClick="ImageButtonGonder2_Click" />
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:ImageButton ID="ImageButtonGeri" runat="server" ImageUrl="../Theme/Images/back.png"
                                                         OnClick="ImageButtonGeri_Click" />
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="LabelParolamiUnuttum" runat="server" ForeColor="Red"></asp:Label>
                        </td>
                    </tr>
                </table>
                <a href="http://www.enroll.com.tr/" target="_blank">
                    <div class="enroll">
                    </div>
                </a>
            </div>
        </asp:View>
    </asp:MultiView>
</div>