<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="IhalelerBannerResmiKontrol.aspx.cs" Inherits="EnrollKurumsal.Admin.IhalelerBannerResmiKontrol" %>

<%@ Register Src="Kontroller/IhalelerBannerResmiKontrol.ascx" TagName="IhalelerBannerResmiKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:IhalelerBannerResmiKontrol ID="IhalelerBannerResmiKontrol1" runat="server" />
</asp:Content>