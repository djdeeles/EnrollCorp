<%@ Page Title="" Language="C#" MasterPageFile="~/Admin/Admin.Master" AutoEventWireup="true"
         CodeBehind="YayinKategorileriKontrol.aspx.cs" Inherits="EnrollKurumsal.Admin.YayinKategorileriKontrol" %>

<%@ Register Src="Kontroller/YayinKategorileriKontrol.ascx" TagName="YayinKategorileriKontrol"
             TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <uc1:YayinKategorileriKontrol ID="YayinKategorileriKontrol1" runat="server" />
</asp:Content>