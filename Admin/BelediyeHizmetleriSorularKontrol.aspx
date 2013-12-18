<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="BelediyeHizmetleriSorularKontrol.aspx.cs" Inherits="EnrollKurumsal.Admin.BelediyeHizmetleriSorularKontrol" %>

<%@ Register Src="Kontroller/BelediyeHizmetleriSorularKontrol.ascx" TagName="BelediyeHizmetleriSorularKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:BelediyeHizmetleriSorularKontrol ID="BelediyeHizmetleriSorularKontrol1" runat="server" />
</asp:Content>