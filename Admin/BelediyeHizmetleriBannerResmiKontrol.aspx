<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="BelediyeHizmetleriBannerResmiKontrol.aspx.cs" Inherits="EnrollKurumsal.Admin.BelediyeHizmetleriBannerResmiKontrol" %>

<%@ Register Src="Kontroller/BelediyeHizmetleriBannerResmiKontrol.ascx" TagName="BelediyeHizmetleriBannerResmiKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:BelediyeHizmetleriBannerResmiKontrol ID="BelediyeHizmetleriBannerResmiKontrol1"
                                              runat="server" />
</asp:Content>