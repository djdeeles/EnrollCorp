﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DuyuruKategorilerKontrol.ascx.cs"
            Inherits="EnrollKurumsal.UserKontroller.DuyuruKategorilerKontrol" %>
<div class="contentsidemodulemed">
    <div class="contentleftmenutext">
        <asp:Label ID="LabelBaslik" runat="server" Text="">Duyurular</asp:Label>
    </div>
    <asp:Menu ID="MenuKategoriler" runat="server" Orientation="Vertical" StaticSubMenuIndent=""
              MaximumDynamicDisplayLevels="0" ItemWrap="True" StaticItemFormatString="{0}">
    </asp:Menu>
</div>
<div class="contentsidemodulebottom">
</div>