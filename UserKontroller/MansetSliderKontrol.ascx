<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MansetSliderKontrol.ascx.cs"
            Inherits="EnrollKurumsal.UserKontroller.MansetSliderKontrol" %>
<script src="../App_Themes/PendikMainTheme/Scripts/MansetSliderKontrol.js" type="text/javascript"></script>
<link href="../App_Themes/PendikMainTheme/Styles/MansetSliderKontrol.css" rel="stylesheet"
      type="text/css" />
<script type="text/javascript">
    $(function() {
        $('#mansetslides').slides({
            preload: true,
            preloadImage: '/App_Themes/PendikMainTheme/Images/loading.gif',
            play: 5000,
            pause: 2500,
            hoverPause: true,
            animationStart: function(current) {
                $('.mansetcaption').animate({
                    bottom: -30
                }, 100);
                if (window.console && console.log) {
                    // example return of current slide number
                    console.log('animationStart on slide: ', current);
                }
                ;
            },
            animationComplete: function(current) {
                $('.mansetcaption').animate({
                    bottom: 0
                }, 200);
                if (window.console && console.log) {
                    // example return of current slide number
                    console.log('animationComplete on slide: ', current);
                }
                ;
            },
            slidesLoaded: function() {
                $('.mansetcaption').animate({
                    bottom: 0
                }, 200);
            }
        });
    });

</script>
<div id="mansetcontainer">
    <div id="mansetcontent">
        <div id="mansetslides">
            <div class="mansetslidescontainer">
                <asp:Repeater ID="RepeaterMansetSlider" runat="server" ViewStateMode="Disabled">
                    <ItemTemplate>
                        <div class="mansetslide">
                            <asp:HyperLink ID="HyperLink1" runat="server" NavigateUrl='<%#                                        Eval("Url") %>' OnDataBinding="HyperLink1_DataBinding">
                                <asp:Image ID="imgResim" ImageUrl='<%#Eval("Resmi") %>' runat="server" AlternateText='<%#Eval("MansetAdi") %>'
                                           BorderStyle="None" />
                            </asp:HyperLink>
                            <div class="mansetcaption" style="bottom: 0">
                                <p>
                                    <%#Eval("MansetAdi") %></p>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
            <a href="#" class="mansetprev">
                <img src="../App_Themes/PendikMainTheme/Images/MansetSlider/arrow-prev.png" width="24"
                     height="43" alt="Önceki" style="border: none;" /></a> <a href="#" class="mansetnext">
                                                                               <img src="../App_Themes/PendikMainTheme/Images/MansetSlider/arrow-next.png" width="24"
                                                                                    height="43" alt="Sonraki" style="border: none;" /></a>
        </div>
        <img src="../App_Themes/PendikMainTheme/Images/MansetSlider/mansetframe.png" width="560"
             height="225" id="mansetframe" />
    </div>
</div>