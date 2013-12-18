<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="VideoAlbumVideolariKontrol.aspx.cs" Inherits="EnrollKurumsal.Admin.VideoAlbumVideolariKontrol" %>

<%@ Register Src="Kontroller/VideoAlbumVideolariKontrol.ascx" TagName="VideoAlbumVideolariKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:VideoAlbumVideolariKontrol ID="VideoAlbumVideolariKontrol1" runat="server" />
</asp:Content>