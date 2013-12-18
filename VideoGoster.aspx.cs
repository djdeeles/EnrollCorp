using System;
using System.Linq;
using System.Web.UI;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal
{
    public partial class VideoGoster : Page
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString.Count != 0)
            {
                if (Request.QueryString["code"] != null)
                {
                    VideoVer();
                }
            }
        }

        private void VideoVer()
        {
            int Id = Convert.ToInt32(Request.QueryString["code"]);
            var VAV = (from p in Veriler.VideoAlbumVideolari
                       where p.Id == Id
                       select new
                                  {
                                      p.Video,
                                      Gorsel =
                           p.Gorsel != null ? p.Gorsel : "../App_Themes/PendikMainTheme/Images/novideo.jpg",
                                  }).First();
            LabelVideo.Text = VAV.Video;
            LiteralVideo.Text =
                "<script type=\"text/javascript\"> jwplayer(\"mediaplayer\").setup({flashplayer: \"App_Themes/PendikMainTheme/VideoPlayer/player.swf\", width:'640', height:'480', file: \""
                + VAV.Video.Replace("~/", "")
                + "\", image: \""
                + VAV.Gorsel.Replace("~/", "")
                + "\"});</script>";
        }
    }
}