<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="VideoAlbumlerKontrol.aspx.cs" Inherits="EnrollKurumsal.Admin.VideoAlbumlerKontrol" %>

<%@ Register Src="Kontroller/VideoAlbumlerKontrol.ascx" TagName="VideoAlbumlerKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:VideoAlbumlerKontrol ID="VideoAlbumlerKontrol1" runat="server" />
</asp:Content>