<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="HizmetSliderKontrol.aspx.cs" Inherits="EnrollKurumsal.Admin.HizmetSliderKontrol" %>

<%@ Register Src="Kontroller/HizmetSliderKontrol.ascx" TagName="HizmetSliderKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:HizmetSliderKontrol ID="HizmetSliderKontrol1" runat="server" />
</asp:Content>