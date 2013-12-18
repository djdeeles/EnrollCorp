<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
         CodeBehind="Sayfa.aspx.cs" Inherits="EnrollKurumsal.Sayfa" %>

<%@ Register Src="UserKontroller/HaberGrubuUyeligiKontrol.ascx" TagName="HaberGrubuUyeligiKontrol"
             TagPrefix="uc1" %>
<%@ Register Src="UserKontroller/SayfaMenuKontrol.ascx" TagName="SayfaMenuKontrol"
             TagPrefix="uc2" %>
<%@ Register Src="UserKontroller/BannerKontrol.ascx" TagName="BannerKontrol" TagPrefix="uc3" %>
<%@ Register Src="UserKontroller/SayfaIcerik.ascx" TagName="SayfaIcerik" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <div class="content">
        <table class="content">
            <tr>
                <td>
                    <div class="contentleft">
                        <div class="contentleftmenu">
                            <uc2:SayfaMenuKontrol ID="SayfaMenuKontrol1" runat="server" />
                        </div>
                        <div class="contentsidemodule">
                            <uc1:HaberGrubuUyeligiKontrol ID="HaberGrubuUyeligiKontrol1" runat="server" />
                        </div>
                        <div class="contentsidemodule">
                            <uc3:BannerKontrol ID="BannerKontrol1" runat="server" />
                        </div>
                    </div>
                    <div class="contentmain">
                        <uc4:SayfaIcerik ID="SayfaIcerik1" runat="server" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>