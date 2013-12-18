using System;
using System.Linq;
using System.Web.UI;
using System.Web.UI.WebControls;
using EnrollKurumsal.Kutuphaneler;
using EnrollKurumsal.Kutuphaneler.DataModel;

namespace EnrollKurumsal.UserKontroller
{
    public partial class IhaleKategorilerKontrol : UserControl
    {
        private readonly EnrollKurumsalEntities Veriler = new EnrollKurumsalEntities();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                MenuKategoriler.Items.Clear();
                IhaleKategorileriVer(MenuKategoriler.Items);
            }
        }

        private void IhaleKategorileriVer(MenuItemCollection Items)
        {
            int DilId = EnrollContext.Current.WorkingLanguage.languageId;
            var IKList =
                Veriler.IhaleKategorileri.Where(p => p.Durum == true && p.DilId == DilId).OrderBy(p => p.SiraNo).ToList();
            foreach (IhaleKategorileri IK in IKList)
            {
                MenuItem MI = new MenuItem();
                MI.NavigateUrl = "../ihaleler/" + IK.Id.ToString() + "/" + MenuUrl.MenuUrlDuzenle(IK.KategoriAdi) + "/1";
                MI.Text = IK.KategoriAdi;
                Items.Add(MI);
            }
        }
    }
}