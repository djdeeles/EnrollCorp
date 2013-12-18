<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="UstMenuKontrol.ascx.cs"
            Inherits="EnrollKurumsal.UserKontroller.UstMenuKontrol" %>
<div style="margin-top: 7px;">
    <asp:DataList ID="DataListUstMenuler" runat="server" RepeatDirection="Horizontal">
        <ItemTemplate>
            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#                                        Eval("NavigateUrl") %>'
                           Target='<%#Eval("Target") %>'><%#Eval("Text") %></asp:HyperLink>
        </ItemTemplate>
    </asp:DataList>
</div>