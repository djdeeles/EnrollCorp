<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="CenazelerBannerResmiKontrol.aspx.cs" Inherits="EnrollKurumsal.Admin.CenazelerBannerResmiKontrol" %>

<%@ Register Src="Kontroller/CenazelerBannerResmiKontrol.ascx" TagName="CenazelerBannerResmiKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:CenazelerBannerResmiKontrol ID="CenazelerBannerResmiKontrol1" runat="server" />
</asp:Content>