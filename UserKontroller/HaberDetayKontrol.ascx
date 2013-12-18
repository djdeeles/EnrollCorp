<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="HaberDetayKontrol.ascx.cs"
            Inherits="EnrollKurumsal.UserKontroller.HaberDetayKontrol" %>
<%@ Register Src="SocialShare.ascx" TagName="SocialShare" TagPrefix="uc1" %>
<div class="contentmainbanner" id="Resim" runat="server">
</div>
<div class="contentmainbannertext">
    <div class="contentmainbannerbaslik">
        Haberler
    </div>
    <div class="contentmainbannerharita">
    </div>
</div>
<div class="contentmaintop">
</div>
<div class="contentmainmed">
    <div class="contentmainsocial">
        <uc1:SocialShare ID="SocialShare1" runat="server" />
    </div>
    <div class="Images">
        <script type="text/javascript">
            function ResimDegistir(Resim) {
                document.getElementById('<%= ImageHaberGorsel.ClientID %>').src = "../../" + Resim;
            }
        </script>
        <div id='preview' style="width: 280px; height: 220px; overflow: hidden; padding-bottom: 3px;">
            <asp:Image ID="ImageHaberGorsel" runat="server" Width="280" />
        </div>
        <asp:DataList ID="DataListResimler" runat="server" RepeatColumns="2">
            <ItemTemplate>
                <div onclick="ResimDegistir('<%#                                        Eval("Gorsel") %>')" style="width: 134px; height: 100px; overflow: hidden;">
                    <img src='../../<%#Eval("GorselThumbnail") %>' width="134" style="padding: 3px;" />
                </div>
            </ItemTemplate>
        </asp:DataList>
    </div>
    <div class="Detay">
        <span class="Baslik">
            <asp:Label ID="LabelBaslik" runat="server" Text=""></asp:Label></span> <span class="Tarih">
                                                                                       <asp:Label ID="LabelTarih" runat="server" Text=""></asp:Label></span> <span class="Icerik">
                                                                                                                                                                 <asp:Label ID="LabelIcerik" runat="server" Text=""></asp:Label></span>
    </div>
</div>
<div class="contentmainbottom">
</div>