<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="BelediyeHizmetleriKategorileriKontrol.aspx.cs" Inherits="EnrollKurumsal.Admin.BelediyeHizmetleriKategorileriKontrol" %>

<%@ Register Src="Kontroller/BelediyeHizmetleriKategorileriKontrol.ascx" TagName="BelediyeHizmetleriKategorileriKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:BelediyeHizmetleriKategorileriKontrol ID="BelediyeHizmetleriKategorileriKontrol1"
                                               runat="server" />
</asp:Content>