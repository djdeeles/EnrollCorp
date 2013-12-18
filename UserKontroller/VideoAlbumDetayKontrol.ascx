<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VideoAlbumDetayKontrol.ascx.cs"
            Inherits="EnrollKurumsal.UserKontroller.VideoAlbumDetayKontrol" %>
<link href="../../../App_Themes/PendikMainTheme/Styles/prettyPhoto.css" rel="stylesheet"
      type="text/css" />
<script src="../../../App_Themes/PendikMainTheme/Scripts/jquery.prettyPhoto.js" type="text/javascript"></script>
<script type="text/javascript" charset="utf-8">
    $(document).ready(function() {
        $("a[rel^='prettyPhoto']").prettyPhoto({ social_tools: false });
    });
</script>
<style type="text/css">
    #gallery {
        width: 100%;
        height: 100%;
    }

    #gallery ul {
        width: 100%;
        height: 100%;
        min-height: 250px;
        margin-left: -40px;
        *margin-left: 0px;
    }

    #gallery ul li {
        float: left;
        width: 160px;
        margin: 0px 12px 12px 0px;
        padding: 5px;
        border: 1px solid #dfdfdf;
        text-align: center;
        height: 160px;
        overflow: hidden;
    }

    #gallery ul img {
        max-width: 150px;
        margin-top: 5px;
        max-height: 110px;
        overflow: hidden;
    }
</style>
<%@ Register Src="SocialShare.ascx" TagName="SocialShare" TagPrefix="uc1" %>
<div class="contentmainbanner" id="Resim" runat="server">
</div>
<div class="contentmainbannertext">
    <div class="contentmainbannerbaslik">
        <asp:Label ID="LabelVideoAlbumKategoriAdi" runat="server" Text=""></asp:Label>
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
    <div id="gallery" style="height: 100%; width: 100%;">
        <div style="text-align: left;">
            <asp:Label ID="lblAlbumName" runat="server" Font-Bold="true"></asp:Label></div>
        <br />
        <asp:ListView ID="ListView1" runat="server" GroupItemCount="4" GroupPlaceholderID="groupPlaceHolder"
                      ItemPlaceholderID="itemPlaceHolder">
            <LayoutTemplate>
                <table>
                    <tr>
                        <td>
                            <ul>
                                <asp:PlaceHolder ID="groupPlaceHolder" runat="server"></asp:PlaceHolder>
                            </ul>
                        </td>
                    </tr>
                </table>
                <br />
            </LayoutTemplate>
            <GroupTemplate>
                <asp:PlaceHolder runat="server" ID="itemPlaceHolder"></asp:PlaceHolder>
            </GroupTemplate>
            <ItemTemplate>
                <li>
                    <div>
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#Eval(
                                                                                          "Id") %>' Font-Bold="true"
                                       rel="prettyPhoto[iframes]" Font-Size="14px" OnDataBinding="HyperLink1_DataBinding">
                            <asp:Image ID="Image2" runat="server" ImageUrl='<%#Eval("Gorsel") %>' />
                            <%--<asp:Image ID="Image1" runat="server" ImageUrl='<%# Eval("Id") %>' OnDataBinding="Image1_DataBinding" />--%>
                        </asp:HyperLink>
                        <p>
                            <%#Eval("VideoAdi") %>
                        </p>
                    </div>
                </li>
            </ItemTemplate>
            <EmptyDataTemplate>
                Veri bulunamadı.
            </EmptyDataTemplate>
        </asp:ListView>
        <% if (DataPager1.TotalRowCount > 19)
           { %>
            <asp:DataPager ID="DataPager1" runat="server" PagedControlID="ListView1" PageSize="20"
                           QueryStringField="videoalbumdetaypage">
                <Fields>
                    <asp:NextPreviousPagerField ButtonType="Link" ShowFirstPageButton="True" ShowNextPageButton="False"
                                                ShowPreviousPageButton="False" FirstPageText="İlk" LastPageText="Son" NextPageText="Sonraki"
                                                PreviousPageText="Önceki" />
                    <asp:NumericPagerField />
                    <asp:NextPreviousPagerField ButtonType="Link" ShowLastPageButton="True" ShowNextPageButton="False"
                                                ShowPreviousPageButton="False" FirstPageText="İlk" LastPageText="Son" NextPageText="Sonraki"
                                                PreviousPageText="Önceki" />
                </Fields>
            </asp:DataPager>
        <% } %>
    </div>
</div>
<div class="contentmainbottom">
</div>