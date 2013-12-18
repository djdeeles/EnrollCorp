<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true" CodeBehind="GorevSablonlariKontrol.aspx.cs" Inherits="EnrollKurumsal.Admin.GorevSablonlariKontrol" %>
<%@ Register src="Kontroller/GorevSablonlariKontrol.ascx" tagname="GorevSablonlariKontrol" tagprefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:GorevSablonlariKontrol ID="GorevSablonlariKontrol1" runat="server" />
</asp:Content>