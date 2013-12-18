using System;
using System.Linq;
using System.Web.UI;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal
{
    public partial class YayinGoster : Page
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count != 0)
            {
                if (Request.QueryString["code"] != null)
                {
                    YayinVer();
                }
            }
        }

        private void YayinVer()
        {
            int Id = Convert.ToInt32(Request.QueryString["code"]);
            var Y = (from p in Veriler.Yayinlar
                     where p.Id == Id
                           && p.Durum == true
                     select new
                                {
                                    p.Url,
                                    Gorsel =
                         p.Gorsel != null ? p.Gorsel : "../App_Themes/PendikMainTheme/Images/novideo.jpg",
                                }).First();

            LabelVideo.Text = Y.Url;
            LiteralVideo.Text =
                "<script type=\"text/javascript\"> jwplayer(\"mediaplayer\").setup({flashplayer: \"App_Themes/PendikMainTheme/VideoPlayer/player.swf\", width:'640', height:'480', file: \""
                + Y.Url
                + "\", image: \""
                + Y.Gorsel.Replace("~/", "")
                + "\"});</script>";
        }
    }
}