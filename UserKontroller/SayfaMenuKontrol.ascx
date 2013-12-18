<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SayfaMenuKontrol.ascx.cs"
            Inherits="EnrollKurumsal.UserKontroller.SayfaMenuKontrol" %>
<div class="contentsidemodulemed">
    <div class="contentleftmenutext">
        <asp:Label ID="LabelBaslik" runat="server" Text=""></asp:Label>
    </div>
    <asp:Menu ID="MenuHizmetMenu" runat="server" Orientation="Vertical" StaticSubMenuIndent=""
              MaximumDynamicDisplayLevels="0" ItemWrap="True" StaticItemFormatString="{0}">
    </asp:Menu>
</div>
<div class="contentsidemodulebottom">
</div>