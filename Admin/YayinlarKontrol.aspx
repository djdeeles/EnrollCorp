<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="YayinlarKontrol.aspx.cs" Inherits="EnrollKurumsal.Admin.YayinlarKontrol" %>

<%@ Register Src="Kontroller/YayinlarKontrol.ascx" TagName="YayinlarKontrol" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:YayinlarKontrol ID="YayinlarKontrol1" runat="server" />
</asp:Content>