<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EtkinliklerKontrol.ascx.cs"
            Inherits="EnrollKurumsal.UserKontroller.EtkinliklerKontrol" %>
<link href="../App_Themes/PendikMainTheme/Styles/SliderKontrol.css" rel="stylesheet"
      type="text/css" />
<script type="text/javascript">
    $().ready(function() {
        $('#EtkinlikSlider').jcarousel({
            vertical: false,
            scroll: 1,
            auto: 0,
            animation: 300,
            wrap: ''
        });
    });
</script>
<div class="contentsidemoduletop">
    <div class="contentsidemodulelabel">
        ETKİNLİK HABERCİSİ
    </div>
</div>
<div class="contentsidemodulemed">
    <div style="width: 175px; height: 275px; overflow: hidden;">
        <asp:Repeater ID="RepeaterEtkinlikler" runat="server">
            <HeaderTemplate>
                <div id='carouselContentHorizantal'>
                <ul id='EtkinlikSlider' class='jcarousel-skin-etkinlik'>
            </HeaderTemplate>
            <ItemTemplate>
                <li>
                    <div style="width: 175px; max-height: 120px; height: 100%; overflow: hidden;">
                        <asp:Image ID="ImageResim1" runat="server" ImageUrl='<%#                                        Eval("GorselThumbnail") %>' /></div>
                    <p style="width: 175px; max-height: 125px; height: 100%;">
                        <b>Etkinlik:&nbsp;</b><%#Eval("Ad") %>
                        <br />
                        <b>Tarih:&nbsp;</b><%#Eval("BaslangicTarihi") %>
                        <br />
                        <%#Eval("Ozet") %>
                        <span style="float: right;">
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#Bind("Id") %>' OnDataBinding="HyperLink1_DataBinding"></asp:HyperLink></span>
                    </p>
                </li>
            </ItemTemplate>
            <FooterTemplate>
            </ul></div>
            </FooterTemplate>
        </asp:Repeater>
    </div>
    <div style="font-family: MyriadProCondensed; margin-top: -18px; font-size: 19px;">
        <a href="/etkinlikler/0/tumetkinlikler/1">Etkinlikler'e Gözat</a></div>
</div>
<div class="contentsidemodulebottom">
</div>