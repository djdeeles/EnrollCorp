using System;
using System.Linq;
using System.Web.UI;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal
{
    public partial class DokumanYayiniGoster : Page
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count != 0)
            {
                if (Request.QueryString["code"] != null)
                {
                    DokumanYayiniVer();
                }
            }
        }

        private void DokumanYayiniVer()
        {
            int Id = Convert.ToInt32(Request.QueryString["code"]);
            var DY = (from p in Veriler.DokumanYayinlari
                      where p.Id == Id
                            && p.Durum == true
                      select new
                                 {
                                     p.Dokuman,
                                     Gorsel =
                          p.Gorsel != null ? p.Gorsel : "../App_Themes/PendikMainTheme/Images/novideo.jpg",
                                 }).First();
            LiteralDokumanYayini.Text = string.Empty;
            LiteralDokumanYayini.Text =
                "<script type=\"text/javascript\">"
                + " var fp = new FlexPaperViewer("
                + "'App_Themes/PendikMainTheme/DokumanPlayer/FlexPaperViewer_Zine',"
                + "'viewerPlaceHolder', { config : {"
                + "SwfFile : escape('" + DY.Dokuman.Replace("~/", "") + "'),"
                + "Scale : 0.6,"
                + "key : \"$abe505dbdf99673339c\","
                + "ZoomTransition : 'easeOut',"
                + "ZoomTime : 0.5,"
                + "ZoomInterval : 0.2,"
                + "FitPageOnLoad : true,"
                + "FitWidthOnLoad : false,"
                + "FullScreenAsMaxWindow : false,"
                + "ProgressiveLoading : false,"
                + "MinZoomSize : 0.2,"
                + "MaxZoomSize : 5,"
                + "SearchMatchAll : false,"
                + "InitViewMode : 'Portrait',"
                + "PrintPaperAsBitmap : false,"
                + "ViewModeToolsVisible : true,"
                + "ZoomToolsVisible : true,"
                + "NavToolsVisible : true,"
                + "CursorToolsVisible : true,"
                + "SearchToolsVisible : true,"
                + "BackgroundColor : '#222222',"
                + "PanelColor : '#555555',"
                + "localeChain: 'tr_TR'"
                + "}});"
                + "</script>";
        }
    }
}