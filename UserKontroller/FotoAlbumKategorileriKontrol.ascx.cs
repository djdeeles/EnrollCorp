using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class FotoAlbumKategorileriKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MenuKategoriler.Items.Clear();
                FotoAlbumKategorileriVer(MenuKategoriler.Items);
            }
        }

        private void FotoAlbumKategorileriVer(MenuItemCollection Items)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var FAKList =
                Veriler.FotoAlbumKategorileri.Where(p => p.Durum == true && p.DilId == DilId).OrderBy(
                    p => p.FotoAlbumKategoriAdi).ToList();
            foreach (FotoAlbumKategorileri FAK in FAKList)
            {
                MenuItem MI = new MenuItem();
                MI.NavigateUrl = "../fotoalbumler/" + FAK.Id.ToString() + "/" +
                                 MenuUrl.MenuUrlDuzenle(FAK.FotoAlbumKategoriAdi) + "/1";
                MI.Text = FAK.FotoAlbumKategoriAdi;
                Items.Add(MI);
            }
        }
    }
}