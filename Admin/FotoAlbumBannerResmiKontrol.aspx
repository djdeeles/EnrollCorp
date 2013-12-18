<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="FotoAlbumBannerResmiKontrol.aspx.cs" Inherits="EnrollKurumsal.Admin.FotoAlbumBannerResmiKontrol" %>

<%@ Register Src="Kontroller/FotoAlbumBannerResmiKontrol.ascx" TagName="FotoAlbumBannerResmiKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:FotoAlbumBannerResmiKontrol ID="FotoAlbumBannerResmiKontrol1" runat="server" />
</asp:Content>