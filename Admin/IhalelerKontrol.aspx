﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="IhalelerKontrol.aspx.cs" Inherits="EnrollKurumsal.Admin.IhalelerKontrol" %>

<%@ Register Src="Kontroller/IhalelerKontrol.ascx" TagName="IhalelerKontrol" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:IhalelerKontrol ID="IhalelerKontrol1" runat="server" />
</asp:Content>