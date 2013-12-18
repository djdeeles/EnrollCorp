<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SiteHaritasi.ascx.cs"
            Inherits="EnrollKurumsal.UserKontroller.SiteHaritasi" %>
<%@ Register Src="SocialShare.ascx" TagName="SocialShare" TagPrefix="uc1" %>
<div class="contentmainbanner" id="Resim" runat="server">
</div>
<div class="contentmainbannertext">
    <div class="contentmainbannerbaslik">
        <asp:Label ID="LabelUstMenuBaslik" runat="server" Text=""></asp:Label>
    </div>
    <div class="contentmainbannerharita">
        <asp:Label ID="LabelSiteMap" runat="server" Text=""></asp:Label>
    </div>
</div>
<div class="contentmaintop">
</div>
<div class="contentmainmed">
    <div class="contentmainsocial">
        <uc1:SocialShare ID="SocialShare1" runat="server" />
    </div>
    <table style="width: 740px;">
        <tr>
            <td style="width: 370px; vertical-align: top;">
                <div class="siteharitasi">
                    <div class="siteharitabaslik">
                        İçerikler
                    </div>
                    <telerik:RadTreeView runat="server" ID="RadTreeViewMenuler" Height="100%" Width="100%"
                                         Skin="Default">
                        <DataBindings>
                            <telerik:RadTreeNodeBinding TextField="MenuAdi" TargetField="Target" />
                        </DataBindings>
                    </telerik:RadTreeView>
                </div>
                <div class="siteharitasi">
                    <div class="siteharitabaslik">
                        Hizmetler
                    </div>
                    <telerik:RadTreeView runat="server" ID="RadTreeViewHizmetMenuler" Height="100%" Width="100%"
                                         Skin="Default">
                        <DataBindings>
                            <telerik:RadTreeNodeBinding TextField="MenuAdi" TargetField="Target" />
                        </DataBindings>
                    </telerik:RadTreeView>
                </div>
            </td>
            <td style="width: 370px; vertical-align: top;">
                <div class="siteharitasi">
                    <div class="siteharitabaslik">
                        Haberler
                    </div>
                    <telerik:RadTreeView runat="server" ID="RadTreeViewHaberler" Height="100%" Width="100%"
                                         Skin="Default">
                        <DataBindings>
                            <telerik:RadTreeNodeBinding TextField="MenuAdi" TargetField="Target" />
                        </DataBindings>
                    </telerik:RadTreeView>
                </div>
                <div class="siteharitasi">
                    <div class="siteharitabaslik">
                        Duyurular
                    </div>
                    <telerik:RadTreeView runat="server" ID="RadTreeViewDuyurular" Height="100%" Width="100%"
                                         Skin="Default">
                        <DataBindings>
                            <telerik:RadTreeNodeBinding TextField="MenuAdi" TargetField="Target" />
                        </DataBindings>
                    </telerik:RadTreeView>
                </div>
                <div class="siteharitasi">
                    <div class="siteharitabaslik">
                        Etkinlikler
                    </div>
                    <telerik:RadTreeView runat="server" ID="RadTreeViewEtkinlikler" Height="100%" Width="100%"
                                         Skin="Default">
                        <DataBindings>
                            <telerik:RadTreeNodeBinding TextField="MenuAdi" TargetField="Target" />
                        </DataBindings>
                    </telerik:RadTreeView>
                </div>
                <div class="siteharitasi">
                    <div class="siteharitabaslik">
                        İhaleler
                    </div>
                    <telerik:RadTreeView runat="server" ID="RadTreeViewIhaleler" Height="100%" Width="100%"
                                         Skin="Default">
                        <DataBindings>
                            <telerik:RadTreeNodeBinding TextField="MenuAdi" TargetField="Target" />
                        </DataBindings>
                    </telerik:RadTreeView>
                </div>
                <div class="siteharitasi">
                    <div class="siteharitabaslik">
                        Fotoğraf Albümleri</div>
                    <telerik:RadTreeView runat="server" ID="RadTreeViewFotolar" Height="100%" Width="100%"
                                         Skin="Default">
                    </telerik:RadTreeView>
                </div>
                <div class="siteharitasi">
                    <div class="siteharitabaslik">
                        Video Albümleri</div>
                    <telerik:RadTreeView runat="server" ID="RadTreeViewVideolar" Height="100%" Width="100%"
                                         Skin="Default">
                    </telerik:RadTreeView>
                </div>
                <div class="siteharitasi">
                    <div class="siteharitabaslik">
                        Canlı Yayınlar</div>
                    <telerik:RadTreeView runat="server" ID="RadTreeViewCanliYayinlar" Height="100%" Width="100%"
                                         Skin="Default">
                    </telerik:RadTreeView>
                </div>
                <div class="siteharitasi">
                    <div class="siteharitabaslik">
                        Yayınlar</div>
                    <telerik:RadTreeView runat="server" ID="RadTreeViewYayinlar" Height="100%" Width="100%"
                                         Skin="Default">
                    </telerik:RadTreeView>
                </div>
            </td>
        </tr>
    </table>
</div>
<div class="contentmainbottom">
</div>