using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class VideoGaleriSliderKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                VideoGaleriVer();
            }
        }

        private void VideoGaleriVer()
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var VideoAlbumList = (from p in Veriler.VideoAlbumKategorileri
                                  join p1 in Veriler.VideoAlbumler
                                      on p.Id equals p1.VideoAlbumKategoriId
                                  join p2 in Veriler.VideoAlbumVideolari
                                      on p1.Id equals p2.VideoAlbumId
                                  where p.DilId == DilId
                                        && p.Durum == true
                                        && p1.Durum == true
                                        && p2.Durum == true
                                  select new
                                             {
                                                 p1.Id,
                                                 VideoAlbumKategoriId = p.Id,
                                                 AlbumAdi = p1.VideoAlbumAdi
                                             }).Distinct().ToList();
            var VideoList = new List<Foto>();
            foreach (var Item in VideoAlbumList)
            {
                Foto F = new Foto();
                F.Id = Item.Id;
                F.VideoAlbumKategoriId = Item.VideoAlbumKategoriId;
                F.AlbumAdi = Item.AlbumAdi;
                //VideoAlbumVideolari VAV1 = Veriler.VideoAlbumVideolari.Where(p => p.VideoAlbumId == Item.Id).OrderBy(p => Guid.NewGuid()).Take(1).FirstOrDefault();
                //F.Gorsel1 = VAV1.Gorsel.Replace("~/", "");
                //F.GorselAdi1 = VAV1.VideoAdi;
                var VAV1 = (from p in Veriler.VideoAlbumVideolari
                            where p.VideoAlbumId == Item.Id
                            orderby Guid.NewGuid()
                            select new
                                       {
                                           Gorsel =
                                p.Gorsel != null ? p.Gorsel : "App_Themes/PendikMainTheme/Images/novideo.jpg",
                                           p.VideoAdi
                                       }).Take(1).First();
                F.Gorsel1 = VAV1.Gorsel.Replace("~/", "");
                F.GorselAdi1 = VAV1.VideoAdi;

                //VideoAlbumVideolari VAV2 = Veriler.VideoAlbumVideolari.Where(p => p.VideoAlbumId == Item.Id).OrderBy(p => Guid.NewGuid()).Take(1).FirstOrDefault();
                //F.Gorsel2 = VAV2.Gorsel.Replace("~/", "");
                //F.GorselAdi2 = VAV2.VideoAdi;
                var VAV2 = (from p in Veriler.VideoAlbumVideolari
                            where p.VideoAlbumId == Item.Id
                            orderby Guid.NewGuid()
                            select new
                                       {
                                           Gorsel =
                                p.Gorsel != null ? p.Gorsel : "App_Themes/PendikMainTheme/Images/novideo.jpg",
                                           p.VideoAdi
                                       }).Take(1).First();
                F.Gorsel2 = VAV1.Gorsel.Replace("~/", "");
                F.GorselAdi2 = VAV1.VideoAdi;

                //VideoAlbumVideolari VAV3 = Veriler.VideoAlbumVideolari.Where(p => p.VideoAlbumId == Item.Id).OrderBy(p => Guid.NewGuid()).Take(1).FirstOrDefault();
                //F.Gorsel3 = VAV3.Gorsel.Replace("~/", "");
                //F.GorselAdi3 = VAV3.VideoAdi;
                var VAV3 = (from p in Veriler.VideoAlbumVideolari
                            where p.VideoAlbumId == Item.Id
                            orderby Guid.NewGuid()
                            select new
                                       {
                                           Gorsel =
                                p.Gorsel != null ? p.Gorsel : "App_Themes/PendikMainTheme/Images/novideo.jpg",
                                           p.VideoAdi
                                       }).Take(1).First();
                F.Gorsel3 = VAV1.Gorsel.Replace("~/", "");
                F.GorselAdi3 = VAV1.VideoAdi;
                VideoList.Add(F);
            }
            RepeaterBannerSlider.DataSource = VideoList;
            RepeaterBannerSlider.DataBind();
        }

        protected void HyperLink1_DataBinding(object sender, EventArgs e)
        {
            HyperLink HL = (HyperLink) sender;
            int Id = Convert.ToInt32(HL.NavigateUrl);
            VideoAlbumler VA = Veriler.VideoAlbumler.Where(p => p.Id == Id).First();
            HL.NavigateUrl = "~/videoalbumdetay/" + Id + "/" + MenuUrl.MenuUrlDuzenle(VA.VideoAlbumAdi + "/1");
        }

        #region Nested type: Foto

        public class Foto
        {
            public int Id { get; set; }
            public int VideoAlbumKategoriId { get; set; }
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