<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
         CodeBehind="YayinDetay.aspx.cs" Inherits="EnrollKurumsal.YayinDetay" %>

<%@ Register Src="UserKontroller/YayinDetayKontrol.ascx" TagName="YayinDetayKontrol"
             TagPrefix="uc1" %>
<%@ Register Src="UserKontroller/YayinKategorileriKontrol.ascx" TagName="YayinKategorileriKontrol"
             TagPrefix="uc2" %>
<%@ Register Src="UserKontroller/HaberGrubuUyeligiKontrol.ascx" TagName="HaberGrubuUyeligiKontrol"
             TagPrefix="uc3" %>
<%@ Register Src="UserKontroller/BannerKontrol.ascx" TagName="BannerKontrol" TagPrefix="uc4" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <div class="content">
        <table class="content">
            <tr>
                <td>
                    <div class="contentleft">
                        <div class="contentleftmenu">
                            <uc2:YayinKategorileriKontrol ID="YayinKategorileriKontrol1" runat="server" />
                        </div>
                        <div class="contentsidemodule">
                            <uc3:HaberGrubuUyeligiKontrol ID="HaberGrubuUyeligiKontrol1" runat="server" />
                        </div>
                        <div class="contentsidemodule">
                            <uc4:BannerKontrol ID="BannerKontrol1" runat="server" />
                        </div>
                    </div>
                    <div class="contentmain">
                        <uc1:YayinDetayKontrol ID="YayinDetayKontrol1" runat="server" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>