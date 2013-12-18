<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="MansetSliderKontrol.aspx.cs" Inherits="EnrollKurumsal.Admin.MansetSliderKontrol" %>

<%@ Register Src="Kontroller/MansetSliderKontrol.ascx" TagName="MansetSliderKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:MansetSliderKontrol ID="MansetSliderKontrol1" runat="server" />
</asp:Content>