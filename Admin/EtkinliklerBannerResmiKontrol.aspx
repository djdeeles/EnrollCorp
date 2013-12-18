<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="EtkinliklerBannerResmiKontrol.aspx.cs" Inherits="EnrollKurumsal.Admin.EtkinliklerBannerResmiKontrol" %>

<%@ Register Src="Kontroller/EtkinliklerBannerResmiKontrol.ascx" TagName="EtkinliklerBannerResmiKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:EtkinliklerBannerResmiKontrol ID="EtkinliklerBannerResmiKontrol1" runat="server" />
</asp:Content>