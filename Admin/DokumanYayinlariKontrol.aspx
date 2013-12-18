<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="DokumanYayinlariKontrol.aspx.cs" Inherits="EnrollKurumsal.Admin.DokumanYayinlariKontrol" %>

<%@ Register Src="Kontroller/DokumanYayinlariKontrol.ascx" TagName="DokumanYayinlariKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:DokumanYayinlariKontrol ID="DokumanYayinlariKontrol1" runat="server" />
</asp:Content>