<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="BannerSliderKontrol.aspx.cs" Inherits="EnrollKurumsal.Admin.BannerSliderKontrol" %>

<%@ Register Src="Kontroller/BannerSliderKontrol.ascx" TagName="BannerSliderKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:BannerSliderKontrol ID="BannerSliderKontrol1" runat="server" />
</asp:Content>