using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class VideoAlbumlerKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                VideoAlbumleriVer(MenuKategoriler.Items);
            }
        }

        private void VideoAlbumleriVer(MenuItemCollection Items)
        {
            if (Request.QueryString.Count != 0)
            {
                if (Request.QueryString["code"] != null)
                {
                    int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                    int Id = Convert.ToInt32(Request.QueryString["code"]);
                    VideoAlbumler V = Veriler.VideoAlbumler.Where(p => p.Id == Id && p.Durum == true).FirstOrDefault();
                    int VideoAlbumKategoriId = V.VideoAlbumKategoriId.Value;
                    ToString();
                    var FAList = (from p in Veriler.VideoAlbumKategorileri
                                  join p1 in Veriler.VideoAlbumler
                                      on p.Id equals p1.VideoAlbumKategoriId
                                  where p.DilId == DilId
                                        && p.Durum == true
                                        && p1.Durum == true
                                        && p.Id == VideoAlbumKategoriId
                                  select new
                                             {
                                                 p1.Id,
                                                 p1.VideoAlbumAdi
                                             }).ToList();
                    MenuKategoriler.Items.Clear();
                    foreach (var VA in FAList)
                    {
                        MenuItem MI = new MenuItem();
                        MI.NavigateUrl = "../videoalbumdetay/" + VA.Id.ToString() + "/" +
                                         MenuUrl.MenuUrlDuzenle(VA.VideoAlbumAdi) + "/1";
                        MI.Text = VA.VideoAlbumAdi;
                        Items.Add(MI);
                    }
                }
            }
        }
    }
}