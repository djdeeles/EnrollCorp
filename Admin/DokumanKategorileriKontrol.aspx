<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="DokumanKategorileriKontrol.aspx.cs" Inherits="EnrollKurumsal.Admin.DokumanKategorileriKontrol" %>

<%@ Register Src="Kontroller/DokumanKategorileriKontrol.ascx" TagName="DokumanKategorileriKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:DokumanKategorileriKontrol ID="DokumanKategorileriKontrol1" runat="server" />
</asp:Content>