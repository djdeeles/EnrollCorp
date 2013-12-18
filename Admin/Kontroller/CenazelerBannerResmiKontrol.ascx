<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CenazelerBannerResmiKontrol.ascx.cs"
            Inherits="EnrollKurumsal.Admin.Kontroller.CenazelerBannerResmiKontrol" %>
<%@ Register Src="MesajKontrol.ascx" TagName="MesajKontrol" TagPrefix="uc1" %>
<link href="../Theme/Styles/AdminKontroller.css" rel="stylesheet" type="text/css" />
<asp:MultiView ID="MultiView1" runat="server">
    <asp:View ID="View1" runat="server">
        <table class="AnaTablo">
            <tr>
                <td>
                    <table class="AnaTablo">
                        <tr>
                            <td colspan="3" style="height: 20px; color: White; background: #5D7B9D; padding-left: 5px; width: 700px;">
                                <asp:Label ID="LabelBaslik" runat="server" Text="Banner Görseli Düzenle"></asp:Label>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="3">
                                <asp:Image ID="ImageBannerGorsel" runat="server" Width="700" />
                            </td>
                        </tr>
                        <tr>
                            <td style="vertical-align: top;">
                                &nbsp;
                            </td>
                            <td style="vertical-align: top;">
                                &nbsp;
                            </td>
                            <td>
                                <i>(Banner görseli 775px X 125px boyutlarında olmalıdır.)</i>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                Banner Görsel
                            </td>
                            <td>
                                :
                            </td>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            <asp:TextBox ID="TextBoxBannerGorsel" runat="server" Width="400px"></asp:TextBox>
                                        </td>
                                        <td>
                                            <asp:ImageButton ID="ImageButtonBannerGorsel" runat="server" ImageUrl="../Theme/Images/icon/imaj_yukle_label.png" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                <asp:ImageButton ID="ImageButtonKaydet" runat="server" ImageUrl="../Theme/Images/kaydet.png"
                                                 OnClick="ImageButtonKaydet_Click" ValidationGroup="g1" />
                                <asp:ImageButton ID="ImageButtonIptal" runat="server" ImageUrl="../Theme/Images/iptal.png"
                                                 OnClick="ImageButtonIptal_Click" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                            </td>
                            <td>
                            </td>
                            <td>
                                <uc1:MesajKontrol ID="MesajKontrol1" runat="server" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                &nbsp;
                            </td>
                            <td>
                                <asp:HiddenField ID="HiddenFieldId" runat="server" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
    </asp:View>
    <asp:View ID="View2" runat="server">
        <table class="AnaTablo">
            <tr>
                <td style="font-size: 16px; text-align: center; width: 775px;">
                    <p>
                        Bu bölümü görüntülemek için yetkiniz bulunmamaktadır.<br />
                        Lütfen site yönetici ile iletişime geçiniz.
                    </p>
                </td>
            </tr>
        </table>
    </asp:View>
</asp:MultiView>