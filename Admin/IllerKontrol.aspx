<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="IllerKontrol.aspx.cs" Inherits="EnrollKurumsal.Admin.IllerKontrol" %>

<%@ Register Src="Kontroller/IllerKontrol.ascx" TagName="IllerKontrol" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:IllerKontrol ID="IllerKontrol1" runat="server" />
</asp:Content>