﻿<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="RolKontrol.aspx.cs" Inherits="EnrollKurumsal.Admin.RolKontrol" %>

<%@ Register Src="Kontroller/RollerKontrol.ascx" TagName="RollerKontrol" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:RollerKontrol ID="RollerKontrol1" runat="server" />
</asp:Content>