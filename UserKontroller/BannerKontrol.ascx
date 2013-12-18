<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="BannerKontrol.ascx.cs"
            Inherits="EnrollKurumsal.UserKontroller.BannerKontrol" %>
<asp:DataList ID="DataListBanner" runat="server">
    <ItemTemplate>
        <div class="contentsidemodulebannertop">
        </div>
        <div class="contentsidemodulemed">
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#                                        Eval("Url") %>' OnDataBinding="HyperLink1_DataBinding">
                <img src='<%#Eval("Resim") %>' style='border: 0;' alt='<%#Eval("BannerAdi") %>'
                     width="175"></asp:HyperLink>
        </div>
        <div class="contentsidemodulebottom">
        </div>
    </ItemTemplate>
</asp:DataList>