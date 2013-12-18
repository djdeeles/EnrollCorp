<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="MesleklerKontrol.aspx.cs" Inherits="EnrollKurumsal.Admin.MesleklerKontrol" %>

<%@ Register Src="Kontroller/MesleklerKontrol.ascx" TagName="MesleklerKontrol" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:MesleklerKontrol ID="MesleklerKontrol1" runat="server" />
</asp:Content>