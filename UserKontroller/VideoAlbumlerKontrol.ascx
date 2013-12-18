<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VideoAlbumlerKontrol.ascx.cs"
            Inherits="EnrollKurumsal.UserKontroller.VideoAlbumlerKontrol" %>
<div class="contentsidemodulemed">
    <div class="contentleftmenutext">
        <asp:Label ID="LabelBaslik" runat="server" Text="">Albümler</asp:Label>
    </div>
    <asp:Menu ID="MenuKategoriler" runat="server" Orientation="Vertical" StaticSubMenuIndent=""
              MaximumDynamicDisplayLevels="0" StaticItemFormatString="{0}">
    </asp:Menu>
</div>
<div class="contentsidemodulebottom">
</div>