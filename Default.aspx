<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
         CodeBehind="Default.aspx.cs" Inherits="EnrollKurumsal.Default" MaintainScrollPositionOnPostback="true" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>--%>
<%@ Register Src="UserKontroller/BaskanMenuKontrol.ascx" TagName="BaskanMenuKontrol"
             TagPrefix="uc1" %>
<%@ Register Src="UserKontroller/AnketKontrol.ascx" TagName="AnketKontrol" TagPrefix="uc2" %>
<%@ Register Src="UserKontroller/HaberGrubuUyeligiKontrol.ascx" TagName="HaberGrubuUyeligiKontrol"
             TagPrefix="uc3" %>
<%@ Register Src="UserKontroller/HizmetMenuKontrol.ascx" TagName="HizmetMenuKontrol"
             TagPrefix="uc4" %>
<%@ Register Src="UserKontroller/MansetSliderKontrol.ascx" TagName="MansetSliderKontrol"
             TagPrefix="uc6" %>
<%@ Register Src="UserKontroller/HizmetSliderKontrol.ascx" TagName="HizmetSliderKontrol"
             TagPrefix="uc7" %>
<%@ Register Src="UserKontroller/BannerSliderKontrol.ascx" TagName="BannerSliderKontrol"
             TagPrefix="uc8" %>
<%@ Register Src="UserKontroller/EtkinliklerKontrol.ascx" TagName="EtkinliklerKontrol"
             TagPrefix="uc9" %>
<%@ Register Src="UserKontroller/HaberlerMansetKontrol.ascx" TagName="HaberlerMansetKontrol"
             TagPrefix="uc10" %>
<%@ Register Src="UserKontroller/DuyurularMansetKontrol.ascx" TagName="DuyurularMansetKontrol"
             TagPrefix="uc11" %>
<%@ Register Src="UserKontroller/EtkinliklerMansetKontrol.ascx" TagName="EtkinliklerMansetKontrol"
             TagPrefix="uc12" %>
<%@ Register Src="UserKontroller/IhalelerMansetKontrol.ascx" TagName="IhalelerMansetKontrol"
             TagPrefix="uc13" %>
<%@ Register Src="UserKontroller/FotoGaleriSliderKontrol.ascx" TagName="FotoGaleriSliderKontrol"
             TagPrefix="uc14" %>
<%@ Register Src="UserKontroller/VideoGaleriSliderKontrol.ascx" TagName="VideoGaleriSliderKontrol"
             TagPrefix="uc5" %>
<%@ Register Src="UserKontroller/YayinlarGaleriSliderKontrol.ascx" TagName="YayinlarGaleriSliderKontrol"
             TagPrefix="uc15" %>
<%@ Register Src="UserKontroller/DokumanYayinlariGaleriSliderKontrol.ascx" TagName="DokumanYayinlariGaleriSliderKontrol"
             TagPrefix="uc16" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <script src="App_Themes/PendikMainTheme/Scripts/SliderKontrol.js" type="text/javascript"></script>
    <script src="App_Themes/PendikMainTheme/Scripts/Manset.js" type="text/javascript"></script>
    <link href="App_Themes/PendikMainTheme/Styles/Manset.css" rel="stylesheet" type="text/css" />
    <script type="text/javascript">
        $(document).ready(function() {
            $('.habertabs a').click(function() {
                haber_switch_tabs($(this));
            });
            haber_switch_tabs($('.haberdefaulttab'));
        });

        function haber_switch_tabs(obj) {
            $('.habertab-content').addClass("haberdelected");
            $('.habertabs a').removeClass("haberselected");
            var id = obj.attr("rel");
            $('#' + id).removeClass("haberdelected");
            obj.addClass("haberselected");
        }

        $(document).ready(function() {
            $('.galeritabs a').click(function() {
                galeri_switch_tabs($(this));
            });
            galeri_switch_tabs($('.galeridefaulttab'));
        });

        function galeri_switch_tabs(obj) {
            $('.galeritab-content').addClass("galeridelected");
            $('.galeritabs a').removeClass("galeriselected");
            var id = obj.attr("rel");
            $('#' + id).removeClass("galeridelected");
            obj.addClass("galeriselected");
        }
    </script>
    <div class="content">
        <table class="content">
            <tr>
                <td>
                    <div class="contentleft">
                        <div class="baskan">
                            <uc1:BaskanMenuKontrol ID="BaskanMenuKontrol1" runat="server" />
                        </div>
                        <div class="contentsidemodule">
                            <uc9:EtkinliklerKontrol ID="EtkinliklerKontrol1" runat="server" />
                        </div>
                        <div class="contentsidemodule">
                            <div class="contentsidemoduletop">
                                <div class="contentsidemodulelabel">
                                    BİZİ TAKİP EDİN
                                </div>
                            </div>
                            <div class="contentsidemodulemed">
                                <!-- AddThis Button BEGIN -->
                                <div class="addthis_toolbox addthis_default_style">
                                    <p>
                                        <a href="https://twitter.com/pendik_belediye" class="twitter-follow-button" data-show-count="false"
                                           data-lang="tr" data-show-screen-name="false">Takip et: @pendik_belediye</a>
                                        <script> !function(d, s, id) {
        var js, fjs = d.getElementsByTagName(s)[0];
        if (!d.getElementById(id)) {
            js = d.createElement(s);
            js.id = id;
            js.src = "//platform.twitter.com/widgets.js";
            fjs.parentNode.insertBefore(js, fjs);
        }
    }(document, "script", "twitter-wjs");</script>
                                        <a class="addthis_button_facebook_like"></a>
                                    </p>
                                    <p>
                                    <a class="addthis_button_preferred_4"></a><a class="addthis_button_preferred_11">
                                                                              </a><a class="addthis_button_compact"></a><a class="addthis_counter addthis_bubble_style">
                                                                                                                        </a>
                                    </>
                                </div>
                                <script type="text/javascript" src="http://s7.addthis.com/js/250/addthis_widget.js#pubid=xa-4f85925a53814f41"></script>
                                <!-- AddThis Button END -->
                            </div>
                            <div class="contentsidemodulebottom">
                            </div>
                        </div>
                        <div class="contentsidemodule">
                            <div class="contentsidemoduletop">
                                <div class="contentsidemodulelabel">
                                    ANKET
                                </div>
                            </div>
                            <div class="contentsidemodulemed">
                                <uc2:AnketKontrol ID="AnketKontrol1" runat="server" />
                            </div>
                            <div class="contentsidemodulebottom">
                            </div>
                        </div>
                    </div>
                    <div class="contentcenter">
                        <div class="contentmainmodule">
                            <uc6:MansetSliderKontrol ID="MansetSliderKontrol1" runat="server" />
                        </div>
                        <div class="contentmainmodule">
                            <div class="contentmainmoduletop" style="margin-top: 30px;">
                            </div>
                            <div class="contentmainmodulemed">
                                <div id="habertab">
                                    <ul class="habertabs">
                                        <li><a class="haberdefaulttab" rel="habertab1">Haberler </a></li>
                                        <li><a rel="habertab2">Duyurular </a></li>
                                        <li><a rel="habertab3">Etkinlikler </a></li>
                                        <li><a rel="habertab4">İhaleler </a></li>
                                    </ul>
                                    <div class="habertab-content" id="habertab1">
                                        <uc10:HaberlerMansetKontrol ID="HaberlerMansetKontrol1" runat="server" />
                                    </div>
                                    <div class="habertab-content" id="habertab2">
                                        <uc11:DuyurularMansetKontrol ID="DuyurularMansetKontrol1" runat="server" />
                                    </div>
                                    <div class="habertab-content" id="habertab3">
                                        <uc12:EtkinliklerMansetKontrol ID="EtkinliklerMansetKontrol1" runat="server" />
                                    </div>
                                    <div class="habertab-content" id="habertab4">
                                        <uc13:IhalelerMansetKontrol ID="IhalelerMansetKontrol1" runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="contentmainmodulebottom">
                            </div>
                        </div>
                        <div class="contentmainmodule">
                            <div class="contentmainmoduletop" style="margin-top: 30px;">
                            </div>
                            <div class="contentmainmodulemed">
                                <div id="galeritab">
                                    <ul class="galeritabs">
                                        <li><a class="galeridefaulttab" rel="galeritab1">Foto Galeri </a></li>
                                        <li><a rel="galeritab2">Videolar</a> </li>
                                        <li><a rel="galeritab3">Canlı Yayın </a></li>
                                        <li><a rel="galeritab4">Yayınlar </a></li>
                                    </ul>
                                    <div class="galeritab-content" id="galeritab1">
                                        <uc14:FotoGaleriSliderKontrol ID="FotoGaleriSliderKontrol1" runat="server" />
                                    </div>
                                    <div class="galeritab-content" id="galeritab2">
                                        <uc5:VideoGaleriSliderKontrol ID="VideoGaleriSliderKontrol1" runat="server" />
                                    </div>
                                    <div class="galeritab-content" id="galeritab3">
                                        <uc15:YayinlarGaleriSliderKontrol ID="YayinlarGaleriSliderKontrol1" runat="server" />
                                    </div>
                                    <div class="galeritab-content" id="galeritab4">
                                        <uc16:DokumanYayinlariGaleriSliderKontrol ID="DokumanYayinlariGaleriSliderKontrol1"
                                                                                  runat="server" />
                                    </div>
                                </div>
                            </div>
                            <div class="contentmainmodulebottom">
                            </div>
                        </div>
                        <div class="contentmainmodule">
                            <div class="contentmainmoduletop">
                            </div>
                            <div class="contentmainmodulemed">
                                <uc8:BannerSliderKontrol ID="BannerSliderKontrol1" runat="server" />
                            </div>
                            <div class="contentmainmodulebottom">
                            </div>
                        </div>
                    </div>
                    <div class="contentright">
                        <div class="contentsidemodule">
                            <div class="hizmetlertop">
                            </div>
                            <div class="hizmetlermed">
                                <uc4:HizmetMenuKontrol ID="HizmetMenuKontrol1" runat="server" />
                            </div>
                            <div class="hizmetlerbottom">
                            </div>
                        </div>
                        <div class="contentsidemodule">
                            <div class="hizmetlertop">
                            </div>
                            <div class="hizmetlermed">
                                <uc7:HizmetSliderKontrol ID="HizmetSliderKontrol1" runat="server" />
                            </div>
                            <div class="hizmetlerbottom">
                            </div>
                        </div>
                        <div class="contentsidemodule">
                            <uc3:HaberGrubuUyeligiKontrol ID="HaberGrubuUyeligiKontrol1" runat="server" />
                        </div>
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>