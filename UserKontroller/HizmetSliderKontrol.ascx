<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HizmetSliderKontrol.ascx.cs"
            Inherits="EnrollKurumsal.UserKontroller.HizmetSliderKontrol" %>
<link href="../App_Themes/PendikMainTheme/Styles/SliderKontrol.css" rel="stylesheet"
      type="text/css" />
<script type="text/javascript">
    $().ready(function() {
        $('#HizmetSlider').jcarousel({
            vertical: true,
            scroll: 3,
            auto: 5,
            animation: 500,
            wrap: 'circular'
        });
    });
</script>
<asp:Repeater ID="RepeaterHizmetSlider" runat="server">
    <HeaderTemplate>
        <div id='carouselContentVertical'>
        <ul id='HizmetSlider' class='jcarousel-skin-hizmet'>
    </HeaderTemplate>
    <ItemTemplate>
        <li>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#                                        Eval("Url") %>' OnDataBinding="HyperLink1_DataBinding">
                <img src='<%#Eval("Resim") %>' style='border: 0;' alt='<%#Eval("HizmetAdi") %>'></asp:HyperLink>
        </li>
    </ItemTemplate>
    <FooterTemplate>
    </ul></div>
    </FooterTemplate>
</asp:Repeater>