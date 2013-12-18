<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HaberGrubuUyeligiKontrol.ascx.cs"
            Inherits="EnrollKurumsal.UserKontroller.HaberGrubuUyeligiKontrol" %>
<script type="text/javascript">
    function yaz(obj) {
        if (obj.value == "") {
            obj.value = "E-posta adresiniz.";
        }
    }

    function temizle(obj) {
        if (obj.valueOf != "") {
            obj.value = "";
        }
    }
</script>
<div class="contentsidemoduletop">
    <div class="contentsidemodulelabel">
        HABER GRUBUNA ÜYE OL</div>
</div>
<div class="contentsidemodulemed">
    Haber grubuna üye olun, bütün yenilikler ve Pendik'teki haberler mailinize gelsin.<br />
    <br />
    <asp:TextBox ID="TextBoxHaberGrubuUyeligi" runat="server" Width="170px" onclick="temizle(this);"
                 onmouseout="yaz(this);"></asp:TextBox>
    <br />
    <br />
    <span style="float: right; font-size: 19px; font-family: MyriadProCondensed; width: 50px; text-align: right;">
        <asp:LinkButton ID="LinkButtonHaberGrubuUyeligiKayit" runat="server" ValidationGroup="g1"
                        OnClick="LinkButtonHaberGrubuUyeligiKayit_Click">Kayıt Ol</asp:LinkButton>
    </span><span>
               <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ErrorMessage="E-posta adresinizi giriniz."
                                           ControlToValidate="TextBoxHaberGrubuUyeligi" Display="Dynamic" ValidationGroup="g1"></asp:RequiredFieldValidator>
               <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" ErrorMessage="Geçerli bir e-posta adresi giriniz."
                                               ControlToValidate="TextBoxHaberGrubuUyeligi" Display="Dynamic" ValidationGroup="g1"
                                               ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
               <asp:Label ID="LabelHaberGrubuUyeligiMesaj" runat="server" Text=""></asp:Label>
           </span>
</div>
<div class="contentsidemodulebottom">
</div>