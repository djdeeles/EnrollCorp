<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="YayinKategorileriKontrol.ascx.cs"
            Inherits="EnrollKurumsal.UserKontroller.YayinKategorileriKontrol" %>
<div class="contentsidemodulemed">
    <div class="contentleftmenutext">
        <asp:Label ID="LabelBaslik" runat="server" Text="">Kategoriler</asp:Label>
    </div>
    <asp:Menu ID="MenuKategoriler" runat="server" Orientation="Vertical" StaticSubMenuIndent=""
              MaximumDynamicDisplayLevels="0" ItemWrap="True" StaticItemFormatString="{0}">
    </asp:Menu>
</div>
<div class="contentsidemodulebottom">
</div>