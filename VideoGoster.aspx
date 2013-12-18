<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="VideoGoster.aspx.cs" Inherits="EnrollKurumsal.VideoGoster" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
    <head runat="server">
        <title></title>
        <style type="text/css">
            body {
                margin: 0 auto;
                padding: 0 auto;
            }
        </style>
    </head>
    <body>
        <form id="form1" runat="server">
            <div>
                <div id="mediaplayer" style="text-align: center; width: 100%;">
                    <asp:Label ID="LabelVideo" runat="server" Text=""></asp:Label>
                </div>
                <script src="App_Themes/PendikMainTheme//VideoPlayer/jwplayer.js" type="text/javascript"></script>
                <asp:Literal ID="LiteralVideo" runat="server"></asp:Literal>
            </div>
        </form>
    </body>
</html>