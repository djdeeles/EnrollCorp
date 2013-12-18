<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="DuyurularBannerResmiKontrol.aspx.cs" Inherits="EnrollKurumsal.Admin.DuyurularBannerResmiKontrol" %>

<%@ Register Src="Kontroller/DuyurularBannerResmiKontrol.ascx" TagName="DuyurularBannerResmiKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:DuyurularBannerResmiKontrol ID="DuyurularBannerResmiKontrol1" runat="server" />
</asp:Content>