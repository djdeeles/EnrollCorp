using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class RssListKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BannerGorseliVer();
                MenuKategorilerSol.Items.Clear();
                KategorileriSolVer(MenuKategorilerSol.Items);
                MenuKategorilerSag.Items.Clear();
                KategorileriSagVer(MenuKategorilerSag.Items);
            }
        }

        private void KategorileriSolVer(MenuItemCollection Items)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var HKList =
                Veriler.HaberKategorileri.Where(p => p.Durum == true && p.DilId == DilId).OrderBy(p => p.SiraNo).ToList();
            MenuItem HKMI = new MenuItem();
            HKMI.NavigateUrl = "../rss/1/0/tumhaberler";
            HKMI.Text = "<div style=\"font-weight:bold; padding-top:20px;\">Tüm Haberler</div>";
            Items.Add(HKMI);
            foreach (HaberKategorileri HK in HKList)
            {
                MenuItem MI = new MenuItem();
                MI.NavigateUrl = "../rss/1/" + HK.Id.ToString() + "/" + MenuUrl.MenuUrlDuzenle(HK.KategoriAdi);
                MI.Text = HK.KategoriAdi;
                Items.Add(MI);
            }
            var DKList =
                Veriler.DuyuruKategorileri.Where(p => p.Durum == true && p.DilId == DilId).OrderBy(p => p.SiraNo).ToList
                    ();
            MenuItem DKMI = new MenuItem();
            DKMI.NavigateUrl = "../rss/2/0/tumduyurular";
            DKMI.Text = "<div style=\"font-weight:bold; padding-top:20px;\">Tüm Duyurular</div>";
            Items.Add(DKMI);
            foreach (DuyuruKategorileri DK in DKList)
            {
                MenuItem MI = new MenuItem();
                MI.NavigateUrl = "../rss/2/" + DK.Id.ToString() + "/" + MenuUrl.MenuUrlDuzenle(DK.KategoriAdi);
                MI.Text = DK.KategoriAdi;
                Items.Add(MI);
            }
        }

        private void KategorileriSagVer(MenuItemCollection Items)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var EKList =
                Veriler.EtkinlikKategorileri.Where(p => p.Durum == true && p.DilId == DilId).OrderBy(p => p.SiraNo).
                    ToList();
            MenuItem EKMI = new MenuItem();
            EKMI.NavigateUrl = "../rss/3/0/tumetkinlikler";
            EKMI.Text = "<div style=\"font-weight:bold; padding-top:20px;\">Tüm Etkinlikler</div>";
            Items.Add(EKMI);
            foreach (EtkinlikKategorileri EK in EKList)
            {
                MenuItem MI = new MenuItem();
                MI.NavigateUrl = "../rss/3/" + EK.Id.ToString() + "/" + MenuUrl.MenuUrlDuzenle(EK.KategoriAdi);
                MI.Text = EK.KategoriAdi;
                Items.Add(MI);
            }
            var IKList =
                Veriler.IhaleKategorileri.Where(p => p.Durum == true && p.DilId == DilId).OrderBy(p => p.SiraNo).ToList();
            MenuItem IKMI = new MenuItem();
            IKMI.NavigateUrl = "../rss/4/0/tumihaleler";
            IKMI.Text = "<div style=\"font-weight:bold; padding-top:20px;\">Tüm İhaleler</div>";
            Items.Add(IKMI);
            foreach (IhaleKategorileri IK in IKList)
            {
                MenuItem MI = new MenuItem();
                MI.NavigateUrl = "../rss/4/" + IK.Id.ToString() + "/" + MenuUrl.MenuUrlDuzenle(IK.KategoriAdi);
                MI.Text = IK.KategoriAdi;
                Items.Add(MI);
            }
        }

        private void BannerGorseliVer()
        {
            Resim.Style.Add("background-image", "../../../App_Themes/PendikMainTheme/Images/Default_banner.png");
        }
    }
}