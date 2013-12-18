<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="VideoAlbumKategorileriKontrol.aspx.cs" Inherits="EnrollKurumsal.Admin.VideoAlbumKategorileriKontrol" %>

<%@ Register Src="Kontroller/VideoAlbumKategorileriKontrol.ascx" TagName="VideoAlbumKategorileriKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:VideoAlbumKategorileriKontrol ID="VideoAlbumKategorileriKontrol1" runat="server" />
</asp:Content>