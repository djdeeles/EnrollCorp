using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class EtkinlikKategorilerKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MenuKategoriler.Items.Clear();
                EtkinlikKategorileriVer(MenuKategoriler.Items);
            }
        }

        private void EtkinlikKategorileriVer(MenuItemCollection Items)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var EKList =
                Veriler.EtkinlikKategorileri.Where(p => p.Durum == true && p.DilId == DilId).OrderBy(p => p.SiraNo).
                    ToList();
            foreach (EtkinlikKategorileri EK in EKList)
            {
                MenuItem MI = new MenuItem();
                MI.NavigateUrl = "../etkinlikler/" + EK.Id.ToString() + "/" + MenuUrl.MenuUrlDuzenle(EK.KategoriAdi) +
                                 "/1";
                MI.Text = EK.KategoriAdi;
                Items.Add(MI);
            }
        }
    }
}