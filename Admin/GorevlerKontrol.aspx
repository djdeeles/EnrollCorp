<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="GorevlerKontrol.aspx.cs" Inherits="EnrollKurumsal.Admin.GorevlerKontrol" %>

<%@ Register Src="Kontroller/GorevlerKontrol.ascx" TagName="GorevlerKontrol" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:GorevlerKontrol ID="GorevlerKontrol1" runat="server" />
</asp:Content>