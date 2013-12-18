<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="RssListKontrol.ascx.cs"
            Inherits="EnrollKurumsal.UserKontroller.RssListKontrol" %>
<div class="contentmainbanner" id="Resim" runat="server">
</div>
<div class="contentmainbannertext">
    <div class="contentmainbannerbaslik">
        Pendik Belediye Rss Yayınları
    </div>
    <div class="contentmainbannerharita">
    </div>
</div>
<div class="contentmaintop">
</div>
<div class="contentmainmed">
    <div style="float: left; width: 350px;">
        <asp:Menu ID="MenuKategorilerSol" runat="server" Orientation="Vertical" StaticSubMenuIndent=""
                  MaximumDynamicDisplayLevels="0" ItemWrap="True" StaticItemFormatString="{0}">
        </asp:Menu>
    </div>
    <div style="float: left; width: 350px;">
        <asp:Menu ID="MenuKategorilerSag" runat="server" Orientation="Vertical" StaticSubMenuIndent=""
                  MaximumDynamicDisplayLevels="0" ItemWrap="True" StaticItemFormatString="{0}">
        </asp:Menu>
    </div>
</div>
<div class="contentmainbottom">
</div>