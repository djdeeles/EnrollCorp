<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="VideoAlbumBannerResmiKontrol.aspx.cs" Inherits="EnrollKurumsal.Admin.VideoAlbumBannerResmiKontrol" %>

<%@ Register Src="Kontroller/VideoAlbumBannerResmiKontrol.ascx" TagName="VideoAlbumBannerResmiKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:VideoAlbumBannerResmiKontrol ID="VideoAlbumBannerResmiKontrol1" runat="server" />
</asp:Content>