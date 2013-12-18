using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class VideoAlbumKategorileriKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MenuKategoriler.Items.Clear();
                VideoAlbumKategorileriVer(MenuKategoriler.Items);
            }
        }

        private void VideoAlbumKategorileriVer(MenuItemCollection Items)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var VAKList =
                Veriler.VideoAlbumKategorileri.Where(p => p.Durum == true && p.DilId == DilId).OrderBy(
                    p => p.VideoAlbumKategoriAdi).ToList();
            foreach (VideoAlbumKategorileri VAK in VAKList)
            {
                MenuItem MI = new MenuItem();
                MI.NavigateUrl = "../videoalbumler/" + VAK.Id.ToString() + "/" +
                                 MenuUrl.MenuUrlDuzenle(VAK.VideoAlbumKategoriAdi) + "/1";
                MI.Text = VAK.VideoAlbumKategoriAdi;
                Items.Add(MI);
            }
        }
    }
}