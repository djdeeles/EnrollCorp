using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class HaberKategorilerKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MenuKategoriler.Items.Clear();
                HaberKategorileriVer(MenuKategoriler.Items);
            }
        }

        private void HaberKategorileriVer(MenuItemCollection Items)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var HKList =
                Veriler.HaberKategorileri.Where(p => p.Durum == true && p.DilId == DilId).OrderBy(p => p.SiraNo).ToList();
            foreach (HaberKategorileri HK in HKList)
            {
                MenuItem MI = new MenuItem();
                MI.NavigateUrl = "../haberler/" + HK.Id.ToString() + "/" + MenuUrl.MenuUrlDuzenle(HK.KategoriAdi) + "/1";
                MI.Text = HK.KategoriAdi;
                Items.Add(MI);
            }
        }
    }
}