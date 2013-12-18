<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="HaberlerBannerResmiKontrol.aspx.cs" Inherits="EnrollKurumsal.Admin.HaberlerBannerResmiKontrol" %>

<%@ Register Src="Kontroller/HaberlerBannerResmiKontrol.ascx" TagName="HaberlerBannerResmiKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:HaberlerBannerResmiKontrol ID="HaberlerBannerResmiKontrol1" runat="server" />
</asp:Content>