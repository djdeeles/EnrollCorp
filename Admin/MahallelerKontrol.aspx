<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="MahallelerKontrol.aspx.cs" Inherits="EnrollKurumsal.Admin.MahallelerKontrol" %>

<%@ Register Src="Kontroller/MahallelerKontrol.ascx" TagName="MahallelerKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:MahallelerKontrol ID="MahallelerKontrol1" runat="server" />
</asp:Content>