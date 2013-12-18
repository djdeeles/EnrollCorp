<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="VefatNedenleriKontrol.aspx.cs" Inherits="EnrollKurumsal.Admin.VefatNedenleriKontrol" %>

<%@ Register Src="Kontroller/VefatNedenleriKontrol.ascx" TagName="VefatNedenleriKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:VefatNedenleriKontrol ID="VefatNedenleriKontrol1" runat="server" />
</asp:Content>