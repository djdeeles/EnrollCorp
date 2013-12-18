<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="UlkelerKontrol.aspx.cs" Inherits="EnrollKurumsal.Admin.UlkelerKontrol" %>

<%@ Register Src="Kontroller/UlkelerKontrol.ascx" TagName="UlkelerKontrol" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:UlkelerKontrol ID="UlkelerKontrol1" runat="server" />
</asp:Content>