<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AramaKontrol.ascx.cs"
            Inherits="EnrollKurumsal.UserKontroller.AramaKontrol" %>
<div class="contentmainbanner" id="Resim" runat="server">
</div>
<div class="contentmainbannertext">
    <div class="contentmainbannerbaslik">
        Arama
    </div>
    <div class="contentmainbannerharita">
    </div>
</div>
<div class="contentmaintop">
</div>
<div class="contentmainmed">
    <asp:ListView ID="ListView1" runat="server" ItemPlaceholderID="PlaceHolder1">
        <ItemTemplate>
            <%#Eval
                                                                                                   ("Ara") %>
        </ItemTemplate>
        <LayoutTemplate>
            <table style="border: 0px;">
                <tr>
                    <td>
                        <asp:PlaceHolder ID="PlaceHolder1" runat="server"></asp:PlaceHolder>
                    </td>
                </tr>
            </table>
        </LayoutTemplate>
        <EmptyDataTemplate>
            Veri bulunamadı.
        </EmptyDataTemplate>
    </asp:ListView>
    <% if (DataPager1.TotalRowCount > 14)
       {%>
        <asp:DataPager ID="DataPager1" runat="server" PagedControlID="ListView1" PageSize="15"
                       QueryStringField="aramapage">
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