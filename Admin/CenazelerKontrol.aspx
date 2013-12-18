<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="CenazelerKontrol.aspx.cs" Inherits="EnrollKurumsal.Admin.CenazelerKontrol" %>

<%@ Register Src="Kontroller/CenazelerKontrol.ascx" TagName="CenazelerKontrol" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:CenazelerKontrol ID="CenazelerKontrol1" runat="server" />
</asp:Content>