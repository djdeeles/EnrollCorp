using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class FotoGaleriSliderKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                FotoGaleriVer();
            }
        }

        private void FotoGaleriVer()
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var FotoAlbumList = (from p in Veriler.FotoAlbumKategorileri
                                 join p1 in Veriler.FotoAlbumler
                                     on p.Id equals p1.FotoAlbumKategoriId
                                 join p2 in Veriler.FotoAlbumGorselleri
                                     on p1.Id equals p2.FotoAlbumId
                                 where p.DilId == DilId
                                       && p.Durum == true
                                       && p1.Durum == true
                                       && p2.Durum == true
                                 select new
                                            {
                                                p1.Id,
                                                FotoAlbumKategoriId = p.Id,
                                                AlbumAdi = p1.FotoAlbumAdi
                                            }).Distinct().ToList();
            var FotoList = new List<Foto>();
            foreach (var Item in FotoAlbumList)
            {
                Foto F = new Foto();
                F.Id = Item.Id;
                F.FotoAlbumKategoriId = Item.FotoAlbumKategoriId;
                F.AlbumAdi = Item.AlbumAdi;
                FotoAlbumGorselleri FAG1 =
                    Veriler.FotoAlbumGorselleri.Where(p => p.FotoAlbumId == Item.Id).OrderBy(p => Guid.NewGuid()).Take(1)
                        .FirstOrDefault();
                F.Gorsel1 = FAG1.GorselThumbnail.Replace("~/", "");
                F.GorselAdi1 = FAG1.GorselAdi;
                FotoAlbumGorselleri FAG2 =
                    Veriler.FotoAlbumGorselleri.Where(p => p.FotoAlbumId == Item.Id).OrderBy(p => Guid.NewGuid()).Take(1)
                        .FirstOrDefault();
                F.Gorsel2 = FAG2.GorselThumbnail.Replace("~/", "");
                F.GorselAdi2 = FAG2.GorselAdi;
                FotoAlbumGorselleri FAG3 =
                    Veriler.FotoAlbumGorselleri.Where(p => p.FotoAlbumId == Item.Id).OrderBy(p => Guid.NewGuid()).Take(1)
                        .FirstOrDefault();
                F.Gorsel3 = FAG3.GorselThumbnail.Replace("~/", "");
                F.GorselAdi3 = FAG3.GorselAdi;
                FotoList.Add(F);
            }
            RepeaterBannerSlider.DataSource = FotoList;
            RepeaterBannerSlider.DataBind();
        }

        protected void HyperLink1_DataBinding(object sender, EventArgs e)
        {
            HyperLink HL = (HyperLink) sender;
            int Id = Convert.ToInt32(HL.NavigateUrl);
            FotoAlbumler FA = Veriler.FotoAlbumler.Where(p => p.Id == Id).First();
            HL.NavigateUrl = "~/fotoalbumdetay/" + Id + "/" + MenuUrl.MenuUrlDuzenle(FA.FotoAlbumAdi + "/1");
        }

        #region Nested type: Foto

        public class Foto
        {
            public int Id { get; set; }
            public int FotoAlbumKategoriId { get; set; }
            public string AlbumAdi { get; set; }
            public string Gorsel1 { get; set; }
            public string GorselAdi1 { get; set; }
            public string Gorsel2 { get; set; }
            public string GorselAdi2 { get; set; }
            public string Gorsel3 { get; set; }
            public string GorselAdi3 { get; set; }
        }

        #endregion
    }
}