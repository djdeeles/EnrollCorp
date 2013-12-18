<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="BelediyeHizmetleriKontrol.aspx.cs" Inherits="EnrollKurumsal.Admin.BelediyeHizmetleriKontrol" %>

<%@ Register Src="Kontroller/BelediyeHizmetleriKontrol.ascx" TagName="BelediyeHizmetleriKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:BelediyeHizmetleriKontrol ID="BelediyeHizmetleriKontrol1" runat="server" />
</asp:Content>