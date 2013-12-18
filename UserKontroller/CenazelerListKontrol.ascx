<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CenazelerListKontrol.ascx.cs"
            Inherits="EnrollKurumsal.UserKontroller.CenazelerListKontrol" %>
<div class="contentmainbanner" id="Resim" runat="server">
</div>
<div class="contentmainbannertext">
    <div class="contentmainbannerbaslik">
        Cenazeler
    </div>
    <div class="contentmainbannerharita">
    </div>
</div>
<div class="contentmaintop">
</div>
<div class="contentmainmed">
    <asp:Panel ID="PanelCenazelerAra" runat="server" DefaultButton="ImageButtonAra">
        <table style="width: 775px;" cellspacing="2" cellpadding="2">
            <tr>
                <td style="width: 75px;">
                    <b>Ad </b>
                </td>
                <td style="width: 10px;">
                    :
                </td>
                <td style="width: 215px;">
                    <asp:TextBox ID="TextBoxAd" runat="server" Width="215px"></asp:TextBox>
                </td>
                <td style="width: 10px;">
                </td>
                <td style="width: 75px;">
                    <b>Soyad </b>
                </td>
                <td style="width: 10px;">
                    :
                </td>
                <td style="width: 215px;">
                    <asp:TextBox ID="TextBoxSoyad" runat="server" Width="215px"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <b>Doğum Yeri </b>
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:DropDownList ID="DropDownListDogumYeri" runat="server" Width="215px">
                    </asp:DropDownList>
                </td>
                <td>
                </td>
                <td>
                    <b>Defin Yeri </b>
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:DropDownList ID="DropDownListDefinYeri" runat="server" Width="215px">
                    </asp:DropDownList>
                </td>
            </tr>
            <%--  <tr>
                <td>
                    <b>Defin Yılı </b>
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:DropDownList ID="DropDownListDefinYili" runat="server" Width="100px">
                    </asp:DropDownList>
                </td>
                <td>
                </td>
                <td>
                    <b>Mahalle Adı </b>
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:DropDownList ID="DropDownListMahalleAdi" runat="server" Width="215px">
                    </asp:DropDownList>
                </td>
            </tr>--%>
            <tr>
                <td>
                    <b>Açıklama </b>
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:TextBox ID="TextBoxAciklama" runat="server" Width="215px"></asp:TextBox>
                </td>
                <td>
                </td>
                <td>
                    <b>Mahalle Adı </b>
                </td>
                <td>
                    :
                </td>
                <td>
                    <asp:DropDownList ID="DropDownListMahalleAdi" runat="server" Width="215px">
                    </asp:DropDownList>
                </td>
            </tr>
        </tr>
            <tr>
                <td style="height: 30px;">
                </td>
                <td>
                </td>
                <td>
                    <asp:ImageButton ID="ImageButtonAra" runat="server" ImageUrl="/App_Themes/PendikMainTheme/Images/ara.png"
                                     OnClick="ImageButtonAra_Click" />
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
                <td>
                </td>
            </tr>
        </table>
    </asp:Panel>
    <asp:ListView ID="ListView1" runat="server" ItemPlaceholderID="PlaceHolder1">
        <ItemTemplate>
            <div class="List">
                <table cellspacing="0" cellpadding="0" border="0" style="width: 700px;">
                    <tr>
                        <td style="width: 175px;" rowspan="5">
                            <div style="overflow: hidden; width: 175px; max-height: 175px;">
                                <img width="175" src='<%#Eval("Resim") %>' />
                            </div>
                        </td>
                        <td style="width: 15px;" rowspan="5">
                        </td>
                        <td style="width: 100px;">
                            <strong>Ad Soyad</strong>
                        </td>
                        <td style="width: 5px;">
                            :
                        </td>
                        <td style="width: 140px;">
                            <%#Eval("Ad") %>
                            <%#Eval("Soyad") %>
                        </td>
                        <td style="width: 30px;">
                        </td>
                        <td style="width: 100px;">
                            <strong>Vefat Tarihi</strong>
                        </td>
                        <td style="width: 5px;">
                            :
                        </td>
                        <td style="width: 140px;">
                            <%#Eval("VefatTarihi") %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Mahalle/Köy:</strong>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <%#Eval("YasadigiYer") %>
                        </td>
                        <td>
                        </td>
                        <td>
                            <strong>Defin Yeri</strong>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <%#Eval("DefinYeri") %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Doğum Yeri</strong>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <%#Eval("DogumYeri") %>
                        </td>
                        <td>
                        </td>
                        <td>
                            <strong>Defin Tarihi</strong>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <%#Eval("DefinTarihi") %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Açıklama</strong>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <%#Eval("Aciklama") %>
                        </td>
                        <td>
                        </td>
                        <td>
                            <strong>Defin Zamanı</strong>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <%#Eval("DefinZamani") %>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <strong>Yaş</strong>
                        </td>
                        <td>
                            :
                        </td>
                        <td>
                            <%#Eval("Yas") %>
                        </td>
                        <td>
                        </td>
                        <td>
                            <%-- Cinsiyet--%>
                        </td>
                        <td>
                        </td>
                        <td>
                            <%-- <%# Eval("Cinsiyet")%>--%>
                        </td>
                    </tr>
                </table>
            </div>
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
                       QueryStringField="duyurularpage">
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