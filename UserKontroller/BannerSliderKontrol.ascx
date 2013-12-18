<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BannerSliderKontrol.ascx.cs"
            Inherits="EnrollKurumsal.UserKontroller.BannerSliderKontrol" %>
<link href="../App_Themes/PendikMainTheme/Styles/SliderKontrol.css" rel="stylesheet"
      type="text/css" />
<script type="text/javascript">
    $().ready(function() {
        $('#BannerSlider').jcarousel({
            vertical: false,
            scroll: 2,
            auto: 10,
            animation: 400,
            wrap: 'circular'
        });
    });
</script>
<asp:Repeater ID="RepeaterBannerSlider" runat="server">
    <HeaderTemplate>
        <div id='carouselContentHorizantal'>
        <ul id='BannerSlider' class='jcarousel-skin-banner'>
    </HeaderTemplate>
    <ItemTemplate>
        <li>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#                                        Eval("Url") %>' OnDataBinding="HyperLink1_DataBinding">
                <img src='<%#Eval("Resim") %>' style='border: 0;' alt='<%#Eval("BannerAdi") %>'></asp:HyperLink>
        </li>
    </ItemTemplate>
    <FooterTemplate>
    </ul></div>
    </FooterTemplate>
</asp:Repeater>