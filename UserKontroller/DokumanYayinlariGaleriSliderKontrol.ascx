<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DokumanYayinlariGaleriSliderKontrol.ascx.cs"
            Inherits="EnrollKurumsal.UserKontroller.DokumanYayinlariGaleriSliderKontrol" %>
<link href="../App_Themes/PendikMainTheme/Styles/SliderKontrol.css" rel="stylesheet"
      type="text/css" />
<script type="text/javascript">
    $().ready(function() {
        $('#DokumanYayinlari').jcarousel({
            vertical: false,
            scroll: 3,
            auto: 0,
            animation: 400,
            wrap: ''
        });
    });
</script>
<asp:Repeater ID="RepeaterBannerSlider" runat="server">
    <HeaderTemplate>
        <div id='carouselContentHorizantal'>
        <ul id='DokumanYayinlari' class='jcarousel-skin-video'>
    </HeaderTemplate>
    <ItemTemplate>
        <li>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#                                        Bind("YayinKategoriId") %>'
                           OnDataBinding="HyperLink1_DataBinding">
                <div style="overflow: hidden; width: 160px; height: 130px; text-align: center;">
                    <img src='<%#Eval("Gorsel1") %>' style='border: 0; width: 160px;' title='<%#Eval("GorselAdi1") %>'
                         alt='<%#Eval("GorselAdi1") %>'>
                </div> 
            </asp:HyperLink>
            <asp:HyperLink ID="HyperLink4" runat="server" NavigateUrl='<%#Bind("YayinKategoriId") %>'
                           OnDataBinding="HyperLink1_DataBinding">
                <div class="AlbumAdi">
                    <%#Eval("YayinKategoriAdi") %>
                </div>
            </asp:HyperLink>
        </li>
        <li>
            <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#Bind("YayinKategoriId") %>'
                           OnDataBinding="HyperLink1_DataBinding">
                <div style="overflow: hidden; width: 160px; height: 130px; text-align: center;">
                    <img src='<%#Eval("Gorsel2") %>' style='border: 0; width: 160px;' title='<%#Eval("GorselAdi2") %>'
                         alt='<%#Eval("GorselAdi2") %>'>
                </div>
            </asp:HyperLink></li>
        <li>
            <asp:HyperLink ID="HyperLink3" runat="server" NavigateUrl='<%#Bind("YayinKategoriId") %>'
                           OnDataBinding="HyperLink1_DataBinding">
                <div style="overflow: hidden; width: 160px; height: 130px; text-align: center;">
                    <img src='<%#Eval("Gorsel3") %>' style='border: 0; width: 160px;' title='<%#Eval("GorselAdi3") %>'
                         alt='<%#Eval("GorselAdi3") %>'>
                </div>
                <div class="TumAlbumler">
                    <a href="dokumanyayinlari/0/tumdokumanyayinlari/1">Tüm Dökuman Yayınlar</a>
                </div>
            </asp:HyperLink></li>
    </ItemTemplate>
    <FooterTemplate>
    </ul></div>
    </FooterTemplate>
</asp:Repeater>