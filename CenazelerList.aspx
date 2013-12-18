<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true"
         CodeBehind="CenazelerList.aspx.cs" Inherits="EnrollKurumsal.CenazelerList" %>

<%@ Register Src="UserKontroller/CenazelerListKontrol.ascx" TagName="CenazelerListKontrol"
             TagPrefix="uc1" %>
<%@ Register Src="UserKontroller/HaberGrubuUyeligiKontrol.ascx" TagName="HaberGrubuUyeligiKontrol"
             TagPrefix="uc2" %>
<%@ Register Src="UserKontroller/BannerKontrol.ascx" TagName="BannerKontrol" TagPrefix="uc3" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolderHead" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolderIcerik" runat="server">
    <div class="content">
        <table class="content">
            <tr>
                <td>
                    <div class="contentleft">
                        <div class="contentsidemodule">
                            <uc2:HaberGrubuUyeligiKontrol ID="HaberGrubuUyeligiKontrol1" runat="server" />
                        </div>
                        <div class="contentsidemodule">
                            <uc3:BannerKontrol ID="BannerKontrol1" runat="server" />
                        </div>
                    </div>
                    <div class="contentmain">
                        <uc1:CenazelerListKontrol ID="CenazelerListKontrol1" runat="server" />
                    </div>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>