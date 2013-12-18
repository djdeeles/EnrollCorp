using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class EtkinliklerKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                EtkinlikleriVer();
            }
        }

        private void EtkinlikleriVer()
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var E = (from p in Veriler.EtkinlikKategorileri
                     join p1 in Veriler.Etkinlikler
                         on p.Id equals p1.EtkinlikKategoriId
                     where p.DilId == DilId
                           && p.Durum == true
                           && p1.Durum == true
                     select new
                                {
                                    p1.Id,
                                    GorselThumbnail =
                         p1.GorselThumbnail1 != null
                             ? p1.GorselThumbnail1.Replace("~/", "../")
                             : "/App_Themes/PendikMainTheme/Images/noimage.png",
                                    p1.BaslangicTarihi,
                                    p1.Ad,
                                    p1.Ozet
                                }).ToList();
            RepeaterEtkinlikler.DataSource = E;
            RepeaterEtkinlikler.DataBind();
        }

        protected void HyperLink1_DataBinding(object sender, EventArgs e)
        {
            HyperLink HL = (HyperLink) sender;
            int Id = Convert.ToInt32(HL.NavigateUrl);
            Etkinlikler E = Veriler.Etkinlikler.Where(p => p.Id == Id).First();
            HL.NavigateUrl = "~/etkinlikdetay/" + E.Id + "/" + MenuUrl.MenuUrlDuzenle(E.Ad);
            HL.Text = " [devamı]";
        }
    }
}