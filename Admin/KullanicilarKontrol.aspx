<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="KullanicilarKontrol.aspx.cs" Inherits="EnrollKurumsal.Admin.KullanicilarKontrol" %>

<%@ Register Src="Kontroller/KullanicilarKontrol.ascx" TagName="KullanicilarKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:KullanicilarKontrol ID="KullanicilarKontrol1" runat="server" />
</asp:Content>