<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="EtkinliklerListKontrol.ascx.cs"
            Inherits="EnrollKurumsal.UserKontroller.EtkinliklerListKontrol" %>
<div class="contentmainbanner" id="Resim" runat="server">
</div>
<div class="contentmainbannertext">
    <div class="contentmainbannerbaslik">
        Etkinlikler
    </div>
    <div class="contentmainbannerharita">
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
                        <asp:Image ID="ImageResim" runat="server" ImageUrl='<%#Eval("GorselThumbnail1") %>' />
                    </div>
                    <div class="ListDetay">
                        <span class="ListBaslik">
                            <%#Eval("Ad") %>
                        </span><span class="ListTarih">
                                   <%#                Convert.ToDateTime(Eval("BaslangicTarihi")).ToString("dd/MM/yyyy") %></span>
                        <span class="ListOzet">
                            <%#Eval("Ozet") %></span>
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
    </asp:ListView>
    <% if (DataPager1.TotalRowCount > 5)
       {%>
        <asp:DataPager ID="DataPager1" runat="server" PagedControlID="ListView1" PageSize="6"
                       QueryStringField="etkinliklerpage">
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