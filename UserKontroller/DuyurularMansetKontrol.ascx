<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DuyurularMansetKontrol.ascx.cs"
            Inherits="EnrollKurumsal.UserKontroller.DuyurularMansetKontrol" %>
<script type="text/javascript">
    $(document).ready(function() {
        $("#DuyuruManset").tabs({ fx: { opacity: "toggle" } }).tabs("rotate", 10000, false);
    });
</script>
<div id="DuyuruManset">
    <asp:Repeater ID="RepeaterSag" runat="server">
        <HeaderTemplate>
            <ul class="ui-tabs-nav">
        </HeaderTemplate>
        <ItemTemplate>
            <li class="ui-tabs-nav-item ui-tabs-selected" id='<%#                                        Eval("Id1") %>'><a href='<%= "#" %><%#Eval("Id2") %>'>
                                                                                     <table>
                                                                                         <tr>
                                                                                             <td style="width: 80;">
                                                                                                 <div class="HaberMansetImgThumbnail">
                                                                                                     <img src='<%#Eval("GorselThumbnail") %>' alt='<%#Eval("Baslik") %>' style="border: none;" />
                                                                                                 </div>
                                                                                             </td>
                                                                                             <td style="width: 5px;">
                                                                                             </td>
                                                                                             <td>
                                                                                                 <span>
                                                                                                     <%#Eval("Baslik") %></span>
                                                                                             </td>
                                                                                         </tr>
                                                                                     </table>
                                                                                 </a></li>
        </ItemTemplate>
        <FooterTemplate>
        </ul>
        </FooterTemplate>
    </asp:Repeater>
    <asp:Repeater ID="RepeaterSol" runat="server">
        <HeaderTemplate>
        </HeaderTemplate>
        <ItemTemplate>
            <div id='<%#Eval("Id2") %>' class="ui-tabs-panel">
                <div class="HaberMansetImg">
                    <img width="250px;" src='<%#Eval("Gorsel") %>' alt="" /></div>
                <div class="info">
                    <h2>
                        <asp:HyperLink ID="HyperLink2" runat="server" NavigateUrl='<%#Bind("Id1") %>' OnDataBinding="HyperLink2_DataBinding">
                            <%#Eval("Baslik") %>
                        </asp:HyperLink>
                    </h2>
                    <p>
                        <%#Eval("Ozet") %>....&nbsp;
                        <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#Bind("Id1") %>' OnDataBinding="HyperLink1_DataBinding"></asp:HyperLink>
                    </p>
                </div>
            </div>
        </ItemTemplate>
        <FooterTemplate>
        </FooterTemplate>
    </asp:Repeater>
</div>
<div class="allcontent">
    <a href="/duyurular/0/tumduyurular/1">Tüm Duyurular</a> • <a href="rsslistesi">RSS Listesi</a></div>