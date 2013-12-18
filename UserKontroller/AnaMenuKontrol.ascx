<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="AnaMenuKontrol.ascx.cs"
            Inherits="EnrollKurumsal.UserKontroller.AnaMenuKontrol" %>
<asp:Menu ID="MenuAnaMenu" runat="server" Orientation="Horizontal" StaticEnableDefaultPopOutImage="False"
          StaticSubMenuIndent="0px" CssClass="verticalStatic" RenderingMode="Table" Width="100%">
    <StaticMenuStyle CssClass="menuStatic" />
    <StaticHoverStyle CssClass="menuStatic_hover" />
    <DynamicMenuStyle CssClass="verticalDynamic" />
    <DynamicMenuItemStyle CssClass="verticalDynamic_item" />
    <DynamicHoverStyle CssClass="verticalDynamic_hover" />
</asp:Menu>