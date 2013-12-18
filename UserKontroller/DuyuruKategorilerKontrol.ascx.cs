using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class DuyuruKategorilerKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MenuKategoriler.Items.Clear();
                DuyuruKategorileriVer(MenuKategoriler.Items);
            }
        }

        private void DuyuruKategorileriVer(MenuItemCollection Items)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var DKList =
                Veriler.DuyuruKategorileri.Where(p => p.Durum == true && p.DilId == DilId).OrderBy(p => p.SiraNo).ToList
                    ();
            foreach (DuyuruKategorileri DK in DKList)
            {
                MenuItem MI = new MenuItem();
                MI.NavigateUrl = "../duyurular/" + DK.Id.ToString() + "/" + MenuUrl.MenuUrlDuzenle(DK.KategoriAdi) +
                                 "/1";
                MI.Text = DK.KategoriAdi;
                Items.Add(MI);
            }
        }
    }
}