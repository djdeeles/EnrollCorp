using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class DokumanYayinlariGaleriSliderKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                DokumanYayinlariVer();
            }
        }

        private void DokumanYayinlariVer()
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var DYList = (from p in Veriler.DokumanKategorileri
                          join p1 in Veriler.DokumanYayinlari
                              on p.Id equals p1.DokumanKategoriId
                          where p.DilId == DilId
                                && p.Durum == true
                                && p1.Durum == true
                          select new
                                     {
                                         KategoriId = p.Id,
                                         p.KategoriAdi
                                     }).Distinct().ToList();
            var DokumanYayinList = new List<Foto>();
            foreach (var Item in DYList)
            {
                Foto F = new Foto();
                F.YayinKategoriId = Item.KategoriId;
                F.YayinKategoriAdi = Item.KategoriAdi;
                var VAV1 = (from p in Veriler.DokumanYayinlari
                            where p.DokumanKategoriId == Item.KategoriId
                                  && p.Durum == true
                            orderby Guid.NewGuid()
                            select new
                                       {
                                           p.Id,
                                           Gorsel =
                                p.GorselThumbnail != null
                                    ? p.GorselThumbnail
                                    : "App_Themes/PendikMainTheme/Images/noimage.png",
                                           YayinAdi = p.DokumanAdi
                                       }).Take(1).First();
                F.Gorsel1Id = VAV1.Id;
                F.Gorsel1 = VAV1.Gorsel.Replace("~/", "");
                F.GorselAdi1 = VAV1.YayinAdi;

                var VAV2 = (from p in Veriler.DokumanYayinlari
                            where p.DokumanKategoriId == Item.KategoriId
                                  && p.Durum == true
                            orderby Guid.NewGuid()
                            select new
                                       {
                                           p.Id,
                                           Gorsel =
                                p.GorselThumbnail != null
                                    ? p.GorselThumbnail
                                    : "App_Themes/PendikMainTheme/Images/noimage.png",
                                           YayinAdi = p.DokumanAdi
                                       }).Take(1).First();
                F.Gorsel2Id = VAV2.Id;
                F.Gorsel2 = VAV2.Gorsel.Replace("~/", "");
                F.GorselAdi2 = VAV2.YayinAdi;

                var VAV3 = (from p in Veriler.DokumanYayinlari
                            where p.DokumanKategoriId == Item.KategoriId
                                  && p.Durum == true
                            orderby Guid.NewGuid()
                            select new
                                       {
                                           p.Id,
                                           Gorsel =
                                p.GorselThumbnail != null
                                    ? p.GorselThumbnail
                                    : "App_Themes/PendikMainTheme/Images/noimage.png",
                                           YayinAdi = p.DokumanAdi
                                       }).Take(1).First();
                F.Gorsel3Id = VAV3.Id;
                F.Gorsel3 = VAV3.Gorsel.Replace("~/", "");
                F.GorselAdi3 = VAV3.YayinAdi;
                DokumanYayinList.Add(F);
            }
            RepeaterBannerSlider.DataSource = DokumanYayinList;
            RepeaterBannerSlider.DataBind();
        }

        protected void HyperLink1_DataBinding(object sender, EventArgs e)
        {
            HyperLink HL = (HyperLink) sender;
            int Id = Convert.ToInt32(HL.NavigateUrl);
            DokumanKategorileri DK = Veriler.DokumanKategorileri.Where(p => p.Id == Id).First();
            HL.NavigateUrl = "~/dokumanyayinlari/" + DK.Id + "/" + MenuUrl.MenuUrlDuzenle(DK.KategoriAdi) + "/1";
        }

        #region Nested type: Foto

        public class Foto
        {
            public int YayinKategoriId { get; set; }
            public string YayinKategoriAdi { get; set; }
            public int Gorsel1Id { get; set; }
            public string Gorsel1 { get; set; }
            public string GorselAdi1 { get; set; }
            public int Gorsel2Id { get; set; }
            public string Gorsel2 { get; set; }
            public string GorselAdi2 { get; set; }
            public int Gorsel3Id { get; set; }
            public string Gorsel3 { get; set; }
            public string GorselAdi3 { get; set; }
        }

        #endregion
    }
}