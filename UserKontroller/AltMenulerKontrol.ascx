<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AltMenulerKontrol.ascx.cs"
            Inherits="EnrollKurumsal.UserKontroller.AltMenulerKontrol" %>
<div class="footermenu">
    <asp:DataList ID="DataListAltMenuBasliklar" runat="server" RepeatDirection="Horizontal"
                  CellPadding="0" Width="100%">
        <HeaderTemplate>
            <ul style="text-align: left; width: 100%;">
        </HeaderTemplate>
        <ItemStyle HorizontalAlign="Left" VerticalAlign="Top" />
        <ItemTemplate>
            <li style="list-style: none; text-align: left; width: 100%; padding: 0px 15px 0px 15px;">
                <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#                                        Eval("NavigateUrl") %>'
                               Target='<%#Eval("Target") %>' ForeColor="White" CssClass="footermainitem "><%#Eval("Text") %></asp:HyperLink>
                <asp:DataList ID="DataListAltMenuler" runat="server" RepeatDirection="Vertical" CellPadding="0"
                              DataSource='<%#                                        AltMenuleriVer(Convert.ToInt32(Eval("Id"))) %>' Width="100%">
                    <HeaderTemplate>
                        <ul>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <li style="padding-top: 6px; *padding-top: 10px; *margin-left: -16px;">
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#Eval("NavigateUrl") %>'
                                           Target='<%#Eval("Target") %>' ForeColor="White" CssClass="footeritem "><%#Eval("Text") %></asp:HyperLink>
                        </li>
                    </ItemTemplate>
                    <FooterTemplate>
                    </ul>
                    </FooterTemplate>
                </asp:DataList>
            </li>
        </ItemTemplate>
        <FooterTemplate>
        </ul>
        </FooterTemplate>
    </asp:DataList>
</div>