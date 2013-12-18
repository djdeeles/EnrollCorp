<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DokumanYayiniGoster.aspx.cs"
         Inherits="EnrollKurumsal.DokumanYayiniGoster" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <meta http-equiv="Content-Type" content="text/html; charset=utf-8" />
        <style type="text/css" media="screen">
            html, body { height: 100%; }

            body {
                margin: 0;
                padding: 0;
                overflow: auto;
            }

            #flashContent { display: none; }
        </style>
        <script src="App_Themes/PendikMainTheme/Scripts/flexpaper_flash.js" type="text/javascript"></script>
    </head>
    <body>
        <form id="form1" runat="server">
            <div style="position: absolute; left: 10px; top: 10px;">
                <a id="viewerPlaceHolder" style="width: 780px; height: 520px; display: block"></a>
                <asp:Literal ID="LiteralDokumanYayini" runat="server"></asp:Literal>
            </div>
        </form>
    </body>
</html>