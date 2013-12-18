<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="IlcelerKontrol.aspx.cs" Inherits="EnrollKurumsal.Admin.IlcelerKontrol" %>

<%@ Register Src="Kontroller/IlcelerKontrol.ascx" TagName="IlcelerKontrol" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:IlcelerKontrol ID="IlcelerKontrol1" runat="server" />
</asp:Content>