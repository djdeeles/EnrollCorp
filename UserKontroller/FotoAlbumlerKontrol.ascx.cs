using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class FotoAlbumlerKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FotoAlbumleriVer(MenuKategoriler.Items);
            }
        }

        private void FotoAlbumleriVer(MenuItemCollection Items)
        {
            if (Request.QueryString.Count != 0)
            {
                if (Request.QueryString["code"] != null)
                {
                    int DilId = EnrollContext.Current.WorkingLanguage.languageId;
                    int Id = Convert.ToInt32(Request.QueryString["code"]);
                    FotoAlbumler F = Veriler.FotoAlbumler.Where(p => p.Id == Id && p.Durum == true).FirstOrDefault();
                    int FotoAlbumKategoriId = F.FotoAlbumKategoriId.Value;
                    ToString();
                    var FAList = (from p in Veriler.FotoAlbumKategorileri
                                  join p1 in Veriler.FotoAlbumler
                                      on p.Id equals p1.FotoAlbumKategoriId
                                  where p.DilId == DilId
                                        && p.Durum == true
                                        && p1.Durum == true
                                        && p.Id == FotoAlbumKategoriId
                                  select new
                                             {
                                                 p1.Id,
                                                 p1.FotoAlbumAdi
                                             }).ToList();

                    MenuKategoriler.Items.Clear();
                    foreach (var FA in FAList)
                    {
                        MenuItem MI = new MenuItem();
                        MI.NavigateUrl = "../fotoalbumdetay/" + FA.Id.ToString() + "/" +
                                         MenuUrl.MenuUrlDuzenle(FA.FotoAlbumAdi) + "/1";
                        MI.Text = FA.FotoAlbumAdi;
                        Items.Add(MI);
                    }
                }
            }
        }
    }
}