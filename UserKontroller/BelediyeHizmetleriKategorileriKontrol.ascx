<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BelediyeHizmetleriKategorileriKontrol.ascx.cs"
            Inherits="EnrollKurumsal.UserKontroller.BelediyeHizmetleriKategorileriKontrol" %>
<script type="text/javascript">
    function AraYaz(obj) {
        if (obj.value == "") {
            obj.value = "Ne aramıştınız?";
        }
    }

    function AraTemizle(obj) {
        if (obj.valueOf != "") {
            obj.value = "";
        }
    }
</script>
<div class="contentsidemodulemed">
    <div class="contentleftmenutext">
        <asp:Label ID="LabelBaslik" runat="server" Text="">A'dan Z'ye Belediye Hizmetleri</asp:Label>
    </div>
    <asp:Menu ID="MenuKategoriler" runat="server" Orientation="Vertical" StaticSubMenuIndent=""
              MaximumDynamicDisplayLevels="0" ItemWrap="True" StaticItemFormatString="{0}">
    </asp:Menu>
    <br />
    <asp:Panel ID="Panel1" runat="server" DefaultButton="ImageButtonAra">
        <div class="contentleftmenutext">
            <asp:Label ID="Label1" runat="server" Text="">A'dan Z'ye Belediye Hizmetlerinde Ara</asp:Label>
        </div>
        <div style="margin-bottom: 7px;">
            <asp:TextBox ID="TextBoxAra" runat="server" Width="170px" onclick="AraTemizle(this);"
                         onmouseout="AraYaz(this);"></asp:TextBox>
        </div>
        <div style="margin-bottom: 7px;">
            <asp:DropDownList ID="DropDownListKategoriler" runat="server" Width="140px">
            </asp:DropDownList>
            &nbsp;
            <asp:ImageButton ID="ImageButtonAra" runat="server" ValidationGroup="garama" ImageUrl="/App_Themes/PendikMainTheme/Images/ara.png"
                             OnClick="ImageButtonAra_Click" />
        </div>
        <div style="margin-bottom: 7px; text-align: left;">
            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="Ne aramıştınız?"
                                        ValidationGroup="garama" ControlToValidate="TextBoxAra" InitialValue="Ne aramıştınız?"
                                        ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ErrorMessage="Ne aramıştınız?"
                                        ValidationGroup="garama" ControlToValidate="TextBoxAra" ForeColor="Red" Display="Dynamic"></asp:RequiredFieldValidator>
        </div>
    </asp:Panel>
</div>
<div class="contentsidemodulebottom">
</div>