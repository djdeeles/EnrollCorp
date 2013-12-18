<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AramaKategoriKontrol.ascx.cs"
            Inherits="EnrollKurumsal.UserKontroller.AramaKategoriKontrol" %>
<div class="contentsidemodulemed">
    <div class="contentleftmenutext">
        <asp:Label ID="LabelBaslik" runat="server" Text="">Detaylı Arama</asp:Label>
    </div>
    <div style="margin-bottom: 7px;">
        <asp:TextBox ID="TextBoxAra" runat="server" Width="175"></asp:TextBox>
    </div>
    <div style="margin-bottom: 7px;">
        <asp:DropDownList ID="DropDownListKategoriler" runat="server" Width="140px">
        </asp:DropDownList>
        &nbsp;
        <asp:ImageButton ID="ImageButtonAra" runat="server" ValidationGroup="garama" ImageUrl="../App_Themes/PendikMainTheme/Images/ara.png"
                         OnClick="ImageButtonAra_Click" />
    </div>
    <div style="margin-bottom: 7px; text-align: left;">
        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Ne aramıştınız?"
                                    ValidationGroup="garama" ControlToValidate="TextBoxAra" InitialValue="Ne aramıştınız?"
                                    ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Ne aramıştınız?"
                                    ValidationGroup="garama" ControlToValidate="TextBoxAra" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
    </div>
</div>
<div class="contentsidemodulebottom">
</div>