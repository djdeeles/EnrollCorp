<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="VideoAlbumlerListKontrol.ascx.cs"
            Inherits="EnrollKurumsal.UserKontroller.VideoAlbumlerListKontrol" %>
<div class="contentmainbanner" id="Resim" runat="server">
</div>
<div class="contentmainbannertext">
    <div class="contentmainbannerbaslik">
        Albümler
    </div>
    <div class="contentmainbannerharita">
        <asp:Label ID="LabelSiteMap" runat="server" Text=""></asp:Label>
    </div>
</div>
<div class="contentmaintop">
</div>
<div class="contentmainmed">
    <asp:ListView ID="ListView1" runat="server" ItemPlaceholderID="PlaceHolder1">
        <ItemTemplate>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#Bind
                                                                                                   ("Id") %>' OnDataBinding="HyperLink1_DataBinding">
                <div class="List">
                    <div class="ListImage">
                        <asp:Image ID="ImageResim" runat="server" ImageUrl='<%#Eval("GorselThumbnail") %>' />
                    </div>
                    <div class="ListDetay">
                        <span class="ListBaslik">
                        <%#Eval("Ad") %>
                        <span class="ListOzet">
                            <%#Eval("Aciklama") %></span>
                    </div>
                </div>
            </asp:HyperLink>
        </ItemTemplate>
        <LayoutTemplate>
            <table style="border: 0px;">
                <tr>
                    <td style="margin-bottom: 15px;">
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
        <EmptyDataTemplate>
            Veri bulunamadı.
        </EmptyDataTemplate>
    </asp:ListView>
    <% if (DataPager1.TotalRowCount > 5)
       {%>
        <asp:DataPager ID="DataPager1" runat="server" PagedControlID="ListView1" PageSize="6"
                       QueryStringField="videoalbumlerpage">
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
<div class="contentmainbottom">
</div>