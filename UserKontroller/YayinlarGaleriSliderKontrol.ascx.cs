using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class YayinlarGaleriSliderKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                YayinGaleriVer();
            }
        }

        private void YayinGaleriVer()
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var YList = (from p in Veriler.YayinKategorileri
                         join p1 in Veriler.Yayinlar
                             on p.Id equals p1.YayinKategoriId
                         where p.DilId == DilId
                               && p.Durum == true
                               && p1.Durum == true
                         select new
                                    {
                                        KategoriId = p.Id,
                                        p.KategoriAdi
                                    }).Distinct().ToList();
            var YayinList = new List<Foto>();
            foreach (var Item in YList)
            {
                Foto F = new Foto();
                F.YayinKategoriId = Item.KategoriId;
                F.YayinKategoriAdi = Item.KategoriAdi;
                var VAV1 = (from p in Veriler.Yayinlar
                            where p.YayinKategoriId == Item.KategoriId
                            orderby Guid.NewGuid()
                            select new
                                       {
                                           p.Id,
                                           Gorsel =
                                p.Gorsel != null ? p.Gorsel : "App_Themes/PendikMainTheme/Images/novideo.jpg",
                                           p.YayinAdi
                                       }).Take(1).First();
                F.Gorsel1Id = VAV1.Id;
                F.Gorsel1 = VAV1.Gorsel.Replace("~/", "");
                F.GorselAdi1 = VAV1.YayinAdi;
                var VAV2 = (from p in Veriler.Yayinlar
                            where p.YayinKategoriId == Item.KategoriId
                            orderby Guid.NewGuid()
                            select new
                                       {
                                           p.Id,
                                           Gorsel =
                                p.Gorsel != null ? p.Gorsel : "App_Themes/PendikMainTheme/Images/novideo.jpg",
                                           p.YayinAdi
                                       }).Take(1).First();
                F.Gorsel2Id = VAV2.Id;
                F.Gorsel2 = VAV2.Gorsel.Replace("~/", "");
                F.GorselAdi2 = VAV2.YayinAdi;
                var VAV3 = (from p in Veriler.Yayinlar
                            where p.YayinKategoriId == Item.KategoriId
                            orderby Guid.NewGuid()
                            select new
                                       {
                                           p.Id,
                                           Gorsel =
                                p.Gorsel != null ? p.Gorsel : "App_Themes/PendikMainTheme/Images/novideo.jpg",
                                           p.YayinAdi
                                       }).Take(1).First();
                F.Gorsel3Id = VAV3.Id;
                F.Gorsel3 = VAV3.Gorsel.Replace("~/", "");
                F.GorselAdi3 = VAV3.YayinAdi;
                YayinList.Add(F);
            }
            RepeaterBannerSlider.DataSource = YayinList;
            RepeaterBannerSlider.DataBind();
        }

        protected void HyperLink1_DataBinding(object sender, EventArgs e)
        {
            HyperLink HL = (HyperLink) sender;
            int Id = Convert.ToInt32(HL.NavigateUrl);
            Yayinlar Y = Veriler.Yayinlar.Where(p => p.Id == Id).First();
            HL.NavigateUrl = "~/yayindetay/" + Id + "/" + MenuUrl.MenuUrlDuzenle(Y.YayinAdi) + "/1";
        }

        protected void HyperLink2_DataBinding(object sender, EventArgs e)
        {
            HyperLink HL = (HyperLink) sender;
            int Id = Convert.ToInt32(HL.NavigateUrl);
            YayinKategorileri YK = Veriler.YayinKategorileri.Where(p => p.Id == Id).First();
            HL.NavigateUrl = "~/yayindetay/" + Id + "/" + MenuUrl.MenuUrlDuzenle(YK.KategoriAdi) + "/1";
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