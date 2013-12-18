<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="IhaleKategorileriKontrol.aspx.cs" Inherits="EnrollKurumsal.Admin.IhaleKategorileriKontrol" %>

<%@ Register Src="Kontroller/IhaleKategorileriKontrol.ascx" TagName="IhaleKategorileriKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:IhaleKategorileriKontrol ID="IhaleKategorileriKontrol1" runat="server" />
</asp:Content>