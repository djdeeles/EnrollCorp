<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="YayinlarBannerResmiKontrol.aspx.cs" Inherits="EnrollKurumsal.Admin.YayinlarBannerResmiKontrol" %>

<%@ Register Src="Kontroller/YayinlarBannerResmiKontrol.ascx" TagName="YayinlarBannerResmiKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:YayinlarBannerResmiKontrol ID="YayinlarBannerResmiKontrol1" runat="server" />
</asp:Content>