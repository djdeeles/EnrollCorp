<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="SayfaIcerik.ascx.cs"
            Inherits="EnrollKurumsal.UserKontroller.SayfaIcerik" %>
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
    <asp:Label ID="LabelIcerik" runat="server" Text=""></asp:Label>
</div>
<div class="contentmainbottom">
</div>